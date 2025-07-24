Public Class frmFolioSharesReport
    Dim mnFolioId As Long = 0

    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
        dtpDate.Value = Now
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

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lsDate As String = ""

        lsDate = Format(dtpDate.Value, "yyyy-MM-dd")

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If txtFolioNo.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%' "

        If txtHolder1.Text <> "" Then lsCond &= " and a.holder1_name like '" & QuoteFilter(txtHolder1.Text) & "%' "
        If txtHolder2.Text <> "" Then lsCond &= " and a.holder2_name like '" & QuoteFilter(txtHolder2.Text) & "%' "
        If txtHolder3.Text <> "" Then lsCond &= " and a.holder3_name like '" & QuoteFilter(txtHolder3.Text) & "%' "

        If txtFHName1.Text <> "" Then lsCond &= " and a.holder1_fh_name like '" & QuoteFilter(txtFHName1.Text) & "%' "
        If txtFHName2.Text <> "" Then lsCond &= " and a.holder2_fh_name like '" & QuoteFilter(txtFHName2.Text) & "%' "
        If txtFHName3.Text <> "" Then lsCond &= " and a.holder3_fh_name like '" & QuoteFilter(txtFHName3.Text) & "%' "

        If txtPanNo1.Text <> "" Then lsCond &= " and a.holder1_pan_no like '" & QuoteFilter(txtPanNo1.Text) & "%' "
        If txtPanNo2.Text <> "" Then lsCond &= " and a.holder2_pan_no like '" & QuoteFilter(txtPanNo2.Text) & "%' "
        If txtPanNo3.Text <> "" Then lsCond &= " and a.holder3_pan_no like '" & QuoteFilter(txtPanNo3.Text) & "%' "

        If mnFolioId > 0 Then lsCond &= " and a.folio_gid = " & mnFolioId & " "

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.folio_no as 'Folio No',"
        lsSql &= " a.folio_sno as 'Folio SNo',"
        lsSql &= " fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') as '" & Format(dtpDate.Value, "dd-MM-yyyy") & " Closing',"
        lsSql &= " a.folio_shares as 'Current Shares Count',"
        lsSql &= " a.holder1_name as 'Holder1',"
        lsSql &= " a.holder1_fh_name as 'Holder1 F/H Name',"
        lsSql &= " a.holder1_pan_no as 'Holder1 Pan No',"
        lsSql &= " a.holder2_name as 'Holder2',"
        lsSql &= " a.holder2_fh_name as 'Holder2 F/H Name',"
        lsSql &= " a.holder2_pan_no as 'Holder2 Pan No',"
        lsSql &= " a.holder3_name as 'Holder3',"
        lsSql &= " a.holder3_fh_name as 'Holder3 F/H Name',"
        lsSql &= " a.holder3_pan_no as 'Holder3 Pan No',"
        lsSql &= " a.folio_addr1 as 'Addr1',"
        lsSql &= " a.folio_addr2 as 'Addr2',"
        lsSql &= " a.folio_addr3 as 'Addr3',"
        lsSql &= " a.folio_city as 'City',"
        lsSql &= " a.folio_state as 'State',"
        lsSql &= " a.folio_country as 'Country',"
        lsSql &= " a.folio_pincode as 'Pincode',"
        lsSql &= " a.folio_mail_id as 'Email Id',"
        lsSql &= " a.folio_contact_no as 'Contact No',"
        lsSql &= " a.nominee_reg_no as 'Nominee Reg No',"
        lsSql &= " a.nominee_name as 'Nominee Name',"
        lsSql &= " a.nominee_addr1 as 'Nominee Addr1',"
        lsSql &= " a.nominee_addr2 as 'Nominee Addr2',"
        lsSql &= " a.nominee_addr3 as 'Nominee Addr3',"
        lsSql &= " a.nominee_city as 'Nominee City',"
        lsSql &= " a.nominee_state as 'Nominee State',"
        lsSql &= " a.nominee_country as 'Nominee Country',"
        lsSql &= " a.nominee_pincode as 'Nominee Pincode',"
        lsSql &= " a.bank_name as 'Bank Name',"
        lsSql &= " a.bank_acc_no as 'Bank A/C No',"
        lsSql &= " a.bank_micr_code as 'Micr Code',"
        lsSql &= " a.bank_ifsc_code as 'IFSC Code',"
        lsSql &= " a.bank_branch as 'Bank Branch',"
        lsSql &= " a.bank_beneficiary as 'Bank Beneficiary',"
        lsSql &= " a.bank_acc_type as 'Bank A/C Type',"
        lsSql &= " a.bank_branch_addr as 'Bank Branch',"
        lsSql &= " a.witness_name as 'Witness Name',"
        lsSql &= " a.witness_addr as 'Witness Addr',"
        lsSql &= " a.repatrition_flag as 'Repatrition Flag',"
        lsSql &= " c.category_name as 'Category',"
        lsSql &= " a.insert_date as 'Insert Date',"
        lsSql &= " a.insert_by as 'Insert By',"
        lsSql &= " a.signature_gid as 'Signature Id',"
        lsSql &= " a.comp_gid as 'Comp Id',"
        lsSql &= " a.folio_gid as 'Folio Id' "
        lsSql &= " from sta_trn_tfolio as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tcategory as c on a.category_gid = c.category_gid and a.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.folio_sno asc"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
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

    Public Sub New(FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvList.KeyDown
        Dim lnFolioId As Long = 0
        Dim frm As Object

        With dgvList
            If .CurrentCell.RowIndex >= 0 Then
                lnFolioId = .CurrentRow.Cells("Folio Id").Value

                Select Case e.KeyCode
                    Case Keys.F1
                        frm = New frmFolioDetail(lnFolioId)
                        frm.ShowDialog()
                    Case Keys.F2
                        frm = New frmCertReport(lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                    Case Keys.F3
                        frm = New frmCertDistReport(0, lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                    Case Keys.F4
                        frm = New frmFolioTranReport(lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                End Select
            End If
        End With
    End Sub
End Class