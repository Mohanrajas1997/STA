Imports MySql.Data.MySqlClient
Public Class frmtop_shareholders

    Private Sub frmtop_shareholders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
        dtp_topshares.Value = Now
        dtp_topshares.Checked = False
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt, dt1 As DataTable
        Dim lsSql As String
        Dim ls_Sql, ls_Sql1 As String
        Dim lsCond As String = ""
        Dim lsdate As String = ""
        Dim lsshare As String = ""
        Dim lsdatephy As String = ""
        Dim lsCondben As String = ""

        lsCond = ""
        lsdate = ""
        lsshare = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCondben &= " and comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If


        If dtp_topshares.Checked = True Then lsdate &= " and benpost_date = '" & Format(dtp_topshares.Value, "yyyy-MM-dd") & "' "
        If dtp_topshares.Checked = True Then lsdatephy &= " and tran_date <= '" & Format(dtp_topshares.Value, "yyyy-MM-dd") & "' "

        'If Not sharelist.Text = "" Then
        '    lsshare &= " and folio_shares > " & sharelist.Text.ToString & " "
        'End If

        'If Not sharelist.Text = "" Then
        '    lsdate &= " and share_count > " & sharelist.Text.ToString & " "
        'End If

        lsshare &= Val(sharelist.Text).ToString & " "

        ls_Sql1 = ""
        ls_Sql1 &= "SELECT * FROM sta_trn_tbenpost where true"
        ls_Sql1 &= lsCondben
        ls_Sql1 &= lsdate
        ls_Sql1 &= " and delete_flag = 'N' "
        cmd = New MySqlCommand(ls_Sql1, gOdbcConn)
        dt1 = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt1)

        ls_Sql = ""
        ls_Sql &= "SELECT sum(share_count) as sharecapital FROM sta_trn_tcert where true " & lsCond & " and cert_status !='2' "
        cmd = New MySqlCommand(ls_Sql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        lsSql = ""
        lsSql &= " SELECT a.holder1_name as Name,a.folio_no as folio,null as dp_id,a.folio_shares as Shares,b.tran_date as Date,round(a.folio_shares*100/(" & ls_Sql & "),2) as percentage FROM sta_trn_tfolio a left join sta_trn_tfoliotran b on b.folio_gid = a.folio_gid where true and a.folio_no NOT IN ('00888888','00999999') "
        lsSql &= lsCond
        lsSql &= lsdatephy
        lsSql &= " and a.delete_flag = 'N' group by a.holder1_name "
        lsSql &= " union"
        lsSql &= " SELECT holder1_name as Name,client_id as folio,dp_id,share_count as Shares,benpost_date as date ,round(share_count*100/(" & ls_Sql & "),2) as percentage FROM sta_trn_tbenpost where true "
        lsSql &= lsCondben
        lsSql &= lsdate
        lsSql &= " and delete_flag = 'N' order by shares desc limit 0, "
        lsSql &= lsshare

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        If (dt1.Rows.Count <= 0) Then
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
            .Width = Me.Width - 24
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

   
    Private Sub sharelist_TextChanged(sender As Object, e As EventArgs) Handles sharelist.TextChanged

    End Sub
End Class