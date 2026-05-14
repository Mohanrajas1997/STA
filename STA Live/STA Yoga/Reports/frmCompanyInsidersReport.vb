Public Class frmCompanyInsidersReport

    Private Sub frmCompanyInsidersReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

    End Sub

    Private Sub frmCompanyInsidersReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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
        Dim lsSql As String
        Dim lsCond As String
        Dim i As Integer

        Try
            lsCond = ""

            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond &= " and b.comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            If txtinsidername.Text <> "" Then lsCond &= " and a.insider_name like '" & QuoteFilter(txtinsidername.Text) & "%' "
            If txtpanno.Text <> "" Then lsCond &= " and a.pan_no like '" & QuoteFilter(txtpanno.Text) & "%' "


            lsSql = ""
            lsSql &= " select "
            lsSql &= " b.comp_name as 'Company Name',"
            lsSql &= " a.insider_name as 'Insider Name',"
            lsSql &= " a.pan_no as 'Pan No',"
            lsSql &= " case when a.status_flag = 'Y' then 'Active' else 'Inactive' end as 'Status'"
            lsSql &= " from sta_mst_tcompinsiders as a "
            lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.insider_name "


            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            For i = 0 To dgvList.Columns.Count - 1
                dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & dgvList.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class