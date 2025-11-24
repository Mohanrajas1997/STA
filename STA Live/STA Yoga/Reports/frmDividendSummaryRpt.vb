
Imports MySql.Data.MySqlClient
Public Class frmDividendSummaryRpt
    Private Sub frmDividendSummaryRpt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        Sql = ""
        Sql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by finyear_code asc "

        Call gpBindCombo(Sql, "finyear_code", "finyear_gid", CboFinyear, gOdbcConn)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Dividend Summary.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadGrid()
        Dim lsSql As String

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If (CboFinyear.Text = "" Or CboFinyear.SelectedIndex = -1) Then
            MessageBox.Show("Please select the finyear !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CboFinyear.Focus()
            Exit Sub
        End If

        If (dtpPaidDate.Checked = False) Then
            MessageBox.Show("Please select the paid date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpPaidDate.Focus()
            Exit Sub
        End If


        lsSql = ""
        lsSql &= " select "
        lsSql &= " comp_gid as 'Company Gid',"
        lsSql &= " finyear_gid as 'FinYear Gid',"
        lsSql &= " count(*) as 'No of shareholder',"
        lsSql &= " sum(share_count) as 'Total Shares',"
        lsSql &= " sum(net_amount) as 'Total Amount',"
        lsSql &= " 'Issued' as 'status'"
        lsSql &= " from div_trn_tdividend as a "
        lsSql &= " where true "
        lsSql &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " group by a.comp_gid,a.finyear_gid  "

        lsSql &= " Union All "

        lsSql &= " select "
        lsSql &= " comp_gid as 'Company Gid',"
        lsSql &= " finyear_gid as 'FinYear Gid',"
        lsSql &= " count(*) as 'No of shareholder',"
        lsSql &= " sum(share_count) as 'Total Shares',"
        lsSql &= " sum(net_amount) as 'Total Amount',"
        lsSql &= " 'Unpaid' as 'status'"
        lsSql &= " from div_trn_tdividend as a "
        lsSql &= " where true "
        lsSql &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " group by a.comp_gid,a.finyear_gid  "

        lsSql &= " Union All "

        lsSql &= " select "
        lsSql &= " comp_gid as 'Company Gid',"
        lsSql &= " finyear_gid as 'FinYear Gid',"
        lsSql &= " count(*) as 'No of shareholder',"
        lsSql &= " sum(share_count) as 'Total Shares',"
        lsSql &= " sum(net_amount) as 'Total Amount',"
        lsSql &= " 'Paid' as 'status'"
        lsSql &= " from div_trn_tdividend as a "
        lsSql &= " where true "
        lsSql &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        lsSql &= " and a.paid_date <= '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " group by a.comp_gid,a.finyear_gid  "

        gpPopGridView(dgvList, lsSql, gOdbcConn)


        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub frmQueue_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim objfrm As frmQuickView
        Dim lsQry As String
        Dim row As DataGridViewRow = dgvList.Rows(e.RowIndex)

        lsQry = ""

        If row.Cells("Status").Value = "Issued" Then
            lsQry &= "select comp_gid,finyear_gid,folio_dpid,shar_holder,share_count,net_amount From div_trn_tdividend Where comp_gid = " & Val(cboCompany.SelectedValue.ToString) &
                    " And finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & " And delete_flag = 'N' ;"

        ElseIf row.Cells("Status").Value = "Unpaid" Then
            lsQry &= "select comp_gid,finyear_gid,folio_dpid,shar_holder,share_count,net_amount From div_trn_tdividend Where comp_gid = " & Val(cboCompany.SelectedValue.ToString) &
                    " And finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & " And ( paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or paid_date is null) And delete_flag = 'N' ;"

        ElseIf row.Cells("Status").Value = "Paid" Then
            lsQry &= "select comp_gid,finyear_gid,folio_dpid,shar_holder,share_count,net_amount From div_trn_tdividend Where comp_gid = " & Val(cboCompany.SelectedValue.ToString) &
                    " And finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & " And paid_date <= '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' And delete_flag = 'N' ;"
        End If

        objfrm = New frmQuickView(gOdbcConn, lsQry)
        objfrm.ShowDialog()
    End Sub
End Class