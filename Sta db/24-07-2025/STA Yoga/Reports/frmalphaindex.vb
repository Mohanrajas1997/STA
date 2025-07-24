Imports MySql.Data.MySqlClient
Public Class frmalphaindex

    Private Sub frmalphaindex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
        dtp_alphaindex.Value = Now
        dtp_alphaindex.Checked = False
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt, dt1 As DataTable
        Dim lsSql As String
        Dim ls_Sql As String
        Dim lsCond As String = ""
        Dim lsCondben As String = ""
        Dim lsdate As String = ""
        Dim lsdatephy As String = ""

        lsCond = ""
        lsCondben = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
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


        If dtp_alphaindex.Checked = True Then lsdate &= " and benpost_date = '" & Format(dtp_alphaindex.Value, "yyyy-MM-dd") & "' "

        lsdatephy = Format(dtp_alphaindex.Value, "yyyy-MM-dd")
        ls_Sql = ""
        ls_Sql &= "SELECT distinct benpost_date FROM sta_trn_tbenpost where true"
        ls_Sql &= lsCondben
        ls_Sql &= lsdate
        ls_Sql &= " and delete_flag = 'N' "
        cmd = New MySqlCommand(ls_Sql, gOdbcConn)
        dt1 = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt1)

        lsSql = ""
        lsSql &= " SELECT a.holder1_name as Name,a.holder2_name as name2,a.holder3_name as name3,null as dp_id,"
        lsSql &= " a.folio_no as folio,fn_sta_get_folioshares(a.folio_gid,'" & lsdatephy & "') as shares"
        lsSql &= " FROM sta_trn_tfolio a left join sta_trn_tfoliotran b on b.folio_gid = a.folio_gid where true"
        lsSql &= lsCond
        lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsdatephy & "') > 0 "
        lsSql &= " and a.folio_no NOT IN ('00888888','00999999') and a.delete_flag = 'N' "
        lsSql &= " union"
        lsSql &= " SELECT holder1_name as Name,holder2_name as name2,"
        lsSql &= " holder3_name as name3,dp_id,client_id as folio,share_count as Shares FROM sta_trn_tbenpost where true "
        lsSql &= lsCondben
        lsSql &= lsdate
        lsSql &= " and delete_flag = 'N' ORDER BY Name "

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
End Class