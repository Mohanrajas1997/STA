Imports MySql.Data.MySqlClient
Public Class frmBenpostsharesabove1000
    Private Sub frmBenpostsharesabove1000_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim Sql As String
        Dim ls_Sql As String
        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)


        ls_Sql = ""
        ls_Sql &= " select depository_code,depository_name from sta_mst_tdepository "
        ls_Sql &= " where delete_flag = 'N' "

        Call gpBindCombo(ls_Sql, "depository_name", "depository_code", cb_benpost, gOdbcConn)

        cb_shares.Items.Add(" >= 1000")
        cb_shares.Items.Add(" < 1000")

        dtpBenpost.Value = Now
        dtpBenpost.Checked = False

    End Sub
    Public Sub gpPopGridView(ByVal GridName As DataGridView, ByVal Qry As String, ByVal odbcConn As Odbc.OdbcConnection)
        Dim da As New Odbc.OdbcDataAdapter(Qry, odbcConn)
        Dim ds As New DataSet
        Dim dt As DataTable
        Try
            da.Fill(ds, "tbl")
            dt = ds.Tables("tbl")
            GridName.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lsSql As String
        Dim ls_Sql As String
        Dim lsCond As String = ""
        lsCond = ""
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If
        If cb_benpost.Text <> "" And cb_benpost.SelectedIndex <> -1 Then
            lsCond &= " and depository_code = '" & cb_benpost.SelectedValue.ToString & "' "
        End If
        If dtpBenpost.Checked = True Then lsCond &= " and benpost_date = '" & Format(dtpBenpost.Value, "yyyy-MM-dd") & "' "

        If cb_shares.Text <> "" And cb_shares.SelectedIndex <> -1 Then
            lsCond &= " and share_count " & cb_shares.SelectedItem.ToString & " "
        End If

        ls_sql = ""
        ls_Sql &= "SELECT sum(share_count) as sharecapital FROM sta_trn_tcert where comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " and cert_status !='2' "
        cmd = New MySqlCommand(ls_Sql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)


        lsSql = ""
        lsSql &= " SELECT @s:=@s+1 Sno,dp_id,client_id,holder1_name,share_count,benpost_date as Date,round(share_count*100/(" & ls_Sql & "),2) as percentage FROM sta_trn_tbenpost,(SELECT @s:= 0) AS s where true "
        lsSql &= lsCond
        lsSql &= " and delete_flag = 'N' order by share_count desc"

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        If (dt.Rows.Count <= 0) Then
            MessageBox.Show("Please Enter Valid Benpost Date")

        Else

            With dgvList
                .DataSource = dt
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i

                txtTotRec.Text = "Total Records : " & .RowCount.ToString
            End With
        End If

    End Sub
    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With dgvList
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 38
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvList.Top + dgvList.Height + 6
        pnlExport.Left = dgvList.Left
        pnlExport.Width = dgvList.Width
        btnExport.Left = Math.Abs(pnlExport.Width - btnExport.Width)
    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    
End Class