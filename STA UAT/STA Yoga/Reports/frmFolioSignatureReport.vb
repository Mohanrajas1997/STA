Public Class frmFolioSignatureReport

    Private Sub frmFolioSignatureReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
    End Sub

    Private Sub frmFolioSignatureReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""

        lsCond = ""

        'If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
        '    MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    cboCompany.Focus()
        '    Exit Sub
        'End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If lsCond = "" Then lsCond = " and 1 = 1 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.comp_gid as 'Comp id',"
        lsSql &= " a.comp_name as 'Company',"
        lsSql &= " a.isin_id as 'ISIN No',"
        lsSql &= " fn_sta_get_foliosigncount(a.comp_gid,'Available') as 'Available Count',"
        lsSql &= " fn_sta_get_foliosigncount(a.comp_gid,'NotAvaliable') as 'Not Available Count',"
        lsSql &= " sum(fn_sta_get_foliosigncount(a.comp_gid,'Available') + fn_sta_get_foliosigncount(a.comp_gid,'NotAvaliable')) as 'Total'"
        lsSql &= " from sta_mst_tcompany as a "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' and a.active_flag = 'Y' "
        lsSql &= " group by a.comp_name"

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        dgvList.DataSource = Nothing
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\FolioSignature_RPT.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim lsqry = ""

        If dgvList.Columns(e.ColumnIndex).Name = "Available Count" Then
            lsqry = ""
            lsqry &= " select "
            lsqry &= " b.comp_name as 'Company',"
            lsqry &= " a.folio_no as 'Folio No',"
            lsqry &= " a.folio_sno as 'Folio SNo',"
            lsqry &= " a.folio_shares as 'Share Count',"
            lsqry &= " d.bene_name as 'Beneficiary Name ',"
            lsqry &= " d.pan_no as 'Pan No',"
            lsqry &= " a.holder1_name as 'Holder1',"
            lsqry &= " a.holder1_fh_name as 'Holder1 F/H Name',"
            lsqry &= " a.holder1_pan_no as 'Holder1 Pan No',"
            lsqry &= " a.holder2_name as 'Holder2',"
            lsqry &= " a.holder2_fh_name as 'Holder2 F/H Name',"
            lsqry &= " a.holder2_pan_no as 'Holder2 Pan No',"
            lsqry &= " a.holder3_name as 'Holder3',"
            lsqry &= " a.holder3_fh_name as 'Holder3 F/H Name',"
            lsqry &= " a.holder3_pan_no as 'Holder3 Pan No',"
            lsqry &= " a.folio_addr1 as 'Addr1',"
            lsqry &= " a.folio_addr2 as 'Addr2',"
            lsqry &= " a.folio_addr3 as 'Addr3',"
            lsqry &= " a.folio_city as 'City',"
            lsqry &= " a.folio_state as 'State',"
            lsqry &= " a.folio_country as 'Country',"
            lsqry &= " a.folio_pincode as 'Pincode',"
            lsqry &= " a.folio_mail_id as 'Email Id',"
            lsqry &= " a.folio_contact_no as 'Contact No',"
            lsqry &= " a.nominee_reg_no as 'Nominee Reg No',"
            lsqry &= " a.nominee_name as 'Nominee Name',"
            lsqry &= " a.nominee_addr1 as 'Nominee Addr1',"
            lsqry &= " a.nominee_addr2 as 'Nominee Addr2',"
            lsqry &= " a.nominee_addr3 as 'Nominee Addr3',"
            lsqry &= " a.nominee_city as 'Nominee City',"
            lsqry &= " a.nominee_state as 'Nominee State',"
            lsqry &= " a.nominee_country as 'Nominee Country',"
            lsqry &= " a.nominee_pincode as 'Nominee Pincode',"
            lsqry &= " a.bank_name as 'Bank Name',"
            lsqry &= " a.bank_acc_no as 'Bank A/C No',"
            lsqry &= " a.bank_micr_code as 'Micr Code',"
            lsqry &= " a.bank_ifsc_code as 'IFSC Code',"
            lsqry &= " a.bank_branch as 'Bank Branch',"
            lsqry &= " a.bank_beneficiary as 'Bank Beneficiary',"
            lsqry &= " a.bank_acc_type as 'Bank A/C Type',"
            lsqry &= " a.bank_branch_addr as 'Bank Branch',"
            lsqry &= " a.witness_name as 'Witness Name',"
            lsqry &= " a.witness_addr as 'Witness Addr',"
            lsqry &= " a.repatrition_flag as 'Repatrition Flag',"
            lsqry &= " c.category_name as 'Category',"
            lsqry &= " a.insert_date as 'Insert Date',"
            lsqry &= " a.insert_by as 'Insert By',"
            lsqry &= " a.signature_gid as 'Signature Id',"
            lsqry &= " a.comp_gid as 'Comp Id',"
            lsqry &= " a.folio_gid as 'Folio Id' "
            lsqry &= " from sta_trn_tfolio as a "
            lsqry &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
            lsqry &= " left join sta_mst_tcategory as c on a.category_gid = c.category_gid and a.delete_flag = 'N' "
            lsqry &= " left join sta_trn_tfoliobeneficiary as d on a.folio_gid=d.folio_gid and b.comp_gid=d.comp_gid and d.delete_flag='N'"
            lsqry &= " where true "
            lsqry &= " and a.comp_gid = " & dgvList.Rows(e.RowIndex).Cells("Comp id").Value & " "
            lsqry &= " and a.signature_gid > 0 and a.folio_shares > 0 and a.folio_no not in('00555555','00666666','00777777','00888888','00999999')"
            lsqry &= " and a.delete_flag = 'N' "
            lsqry &= " order by a.folio_sno asc"

            ShowQuery(lsqry, gOdbcConn)
        ElseIf dgvList.Columns(e.ColumnIndex).Name = "Not Available Count" Then
            lsqry = ""
            lsqry &= " select "
            lsqry &= " b.comp_name as 'Company',"
            lsqry &= " a.folio_no as 'Folio No',"
            lsqry &= " a.folio_sno as 'Folio SNo',"
            lsqry &= " a.folio_shares as 'Share Count',"
            lsqry &= " d.bene_name as 'Beneficiary Name ',"
            lsqry &= " d.pan_no as 'Pan No',"
            lsqry &= " a.holder1_name as 'Holder1',"
            lsqry &= " a.holder1_fh_name as 'Holder1 F/H Name',"
            lsqry &= " a.holder1_pan_no as 'Holder1 Pan No',"
            lsqry &= " a.holder2_name as 'Holder2',"
            lsqry &= " a.holder2_fh_name as 'Holder2 F/H Name',"
            lsqry &= " a.holder2_pan_no as 'Holder2 Pan No',"
            lsqry &= " a.holder3_name as 'Holder3',"
            lsqry &= " a.holder3_fh_name as 'Holder3 F/H Name',"
            lsqry &= " a.holder3_pan_no as 'Holder3 Pan No',"
            lsqry &= " a.folio_addr1 as 'Addr1',"
            lsqry &= " a.folio_addr2 as 'Addr2',"
            lsqry &= " a.folio_addr3 as 'Addr3',"
            lsqry &= " a.folio_city as 'City',"
            lsqry &= " a.folio_state as 'State',"
            lsqry &= " a.folio_country as 'Country',"
            lsqry &= " a.folio_pincode as 'Pincode',"
            lsqry &= " a.folio_mail_id as 'Email Id',"
            lsqry &= " a.folio_contact_no as 'Contact No',"
            lsqry &= " a.nominee_reg_no as 'Nominee Reg No',"
            lsqry &= " a.nominee_name as 'Nominee Name',"
            lsqry &= " a.nominee_addr1 as 'Nominee Addr1',"
            lsqry &= " a.nominee_addr2 as 'Nominee Addr2',"
            lsqry &= " a.nominee_addr3 as 'Nominee Addr3',"
            lsqry &= " a.nominee_city as 'Nominee City',"
            lsqry &= " a.nominee_state as 'Nominee State',"
            lsqry &= " a.nominee_country as 'Nominee Country',"
            lsqry &= " a.nominee_pincode as 'Nominee Pincode',"
            lsqry &= " a.bank_name as 'Bank Name',"
            lsqry &= " a.bank_acc_no as 'Bank A/C No',"
            lsqry &= " a.bank_micr_code as 'Micr Code',"
            lsqry &= " a.bank_ifsc_code as 'IFSC Code',"
            lsqry &= " a.bank_branch as 'Bank Branch',"
            lsqry &= " a.bank_beneficiary as 'Bank Beneficiary',"
            lsqry &= " a.bank_acc_type as 'Bank A/C Type',"
            lsqry &= " a.bank_branch_addr as 'Bank Branch',"
            lsqry &= " a.witness_name as 'Witness Name',"
            lsqry &= " a.witness_addr as 'Witness Addr',"
            lsqry &= " a.repatrition_flag as 'Repatrition Flag',"
            lsqry &= " c.category_name as 'Category',"
            lsqry &= " a.insert_date as 'Insert Date',"
            lsqry &= " a.insert_by as 'Insert By',"
            lsqry &= " a.signature_gid as 'Signature Id',"
            lsqry &= " a.comp_gid as 'Comp Id',"
            lsqry &= " a.folio_gid as 'Folio Id' "
            lsqry &= " from sta_trn_tfolio as a "
            lsqry &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
            lsqry &= " left join sta_mst_tcategory as c on a.category_gid = c.category_gid and a.delete_flag = 'N' "
            lsqry &= " left join sta_trn_tfoliobeneficiary as d on a.folio_gid=d.folio_gid and b.comp_gid=d.comp_gid and d.delete_flag='N'"
            lsqry &= " where true "
            lsqry &= " and a.comp_gid = " & dgvList.Rows(e.RowIndex).Cells("Comp id").Value & " "
            lsqry &= " and a.signature_gid = 0 and a.folio_shares > 0 and a.folio_no not in('00555555','00666666','00777777','00888888','00999999')"
            lsqry &= " and a.delete_flag = 'N' "
            lsqry &= " order by a.folio_sno asc"

            ShowQuery(lsqry, gOdbcConn)
        End If
    End Sub
End Class