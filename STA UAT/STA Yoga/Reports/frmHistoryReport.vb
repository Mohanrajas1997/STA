Public Class frmHistoryReport
    Dim mnUploadId As Long

    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' doc type
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "

        Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' valid
        With cboValid
            .Items.Clear()
            .Items.Add("Yes")
            .Items.Add("No")
        End With

        Call frmDtpCtrClear(Me)
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

        lsCond = ""

        If dtpInwardFrom.Checked = True Then lsCond &= " and a.tran_inward_date >= '" & Format(dtpInwardFrom.Value, "yyyy-MM-dd") & "' "
        If dtpInwardTo.Checked = True Then lsCond &= " and a.tran_inward_date <= '" & Format(dtpInwardTo.Value, "yyyy-MM-dd") & "' "

        If dtpOutwardFrom.Checked = True Then lsCond &= " and a.tran_outward_date >= '" & Format(dtpOutwardFrom.Value, "yyyy-MM-dd") & "' "
        If dtpOutwardTo.Checked = True Then lsCond &= " and a.tran_outward_date <= '" & Format(dtpOutwardTo.Value, "yyyy-MM-dd") & "' "

        If dtpTranFrom.Checked = True Then lsCond &= " and a.tran_date >= '" & Format(dtpInwardFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTranTo.Checked = True Then lsCond &= " and a.tran_date <= '" & Format(dtpInwardTo.Value, "yyyy-MM-dd") & "' "

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            lsCond &= " and a.tran_code = '" & cboDocType.SelectedValue.ToString & "' "
        End If

        If txtInwardNo.Text <> "" Then lsCond &= " and a.tran_inward_no like '" & QuoteFilter(txtInwardNo.Text) & "%' "
        If txtOutwardNo.Text <> "" Then lsCond &= " and a.tran_outward_no like " & QuoteFilter(txtOutwardNo.Text) & "%'"

        If txtHolderName.Text <> "" Then lsCond &= " and a.folio_holder like '" & QuoteFilter(txtHolderName.Text) & "%'"
        If txtFolioNo.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%'"

        If txtToHolderName.Text <> "" Then lsCond &= " and a.tran_folio_holder like '" & QuoteFilter(txtToHolderName.Text) & "%'"
        If txtToFolioNo.Text <> "" Then lsCond &= " and a.tran_folio_no like '" & QuoteFilter(txtToFolioNo.Text) & "%'"

        If txtTranNo.Text <> "" Then lsCond &= " and a.tran_no like " & QuoteFilter(txtTranNo.Text) & "%'"
        If txtCertNo.Text <> "" Then lsCond &= " and a.cert_no = " & Val(txtCertNo.Text) & " "
        If txtDistFrom.Text <> "" Then lsCond &= " and a.certdist_from = " & Val(txtDistFrom.Text) & " "
        If txtDistTo.Text <> "" Then lsCond &= " and a.certdist_to " & Val(txtDistTo.Text) & " "

        If txtClientId.Text <> "" Then lsCond &= " and a.client_id like " & QuoteFilter(txtClientId.Text) & "%'"
        If txtClientName.Text <> "" Then lsCond &= " and a.client_name like " & QuoteFilter(txtClientName.Text) & "%'"
        If txtDPId.Text <> "" Then lsCond &= " and a.dp_id like " & QuoteFilter(txtDPId.Text) & "%'"
        If txtDPName.Text <> "" Then lsCond &= " and a.dp_name like " & QuoteFilter(txtDPName.Text) & "%'"

        If txtRemark.Text <> "" Then lsCond &= " and a.tran_remark like " & QuoteFilter(txtRemark.Text) & "%'"

        Select Case cboValid.Text.ToUpper
            Case "YES"
                lsCond &= " and a.chklst_disc = 0 "
            Case "NO"
                lsCond &= " and a.chklst_disc > 0 "
        End Select

        If lsCond = "" Then lsCond = " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.tran_no as 'Tran No',"
        lsSql &= " a.tran_date as 'Tran Date',"
        lsSql &= " a.tran_code as 'Tran Code',"
        lsSql &= " a.tran_status as 'Tran Status',"
        lsSql &= " a.tran_remark as 'Remark',"
        lsSql &= " a.folio_no as 'Folio No',"
        lsSql &= " a.folio_holder as 'Folio Holder',"
        lsSql &= " a.tran_folio_no as 'Tran Folio No',"
        lsSql &= " a.tran_folio_holder as 'Tran Folio Holder',"
        lsSql &= " a.cert_no as 'Cert No',"
        lsSql &= " a.tot_shares as 'Tot Shares',"
        lsSql &= " a.certdist_from as 'Dist From',"
        lsSql &= " a.certdist_to as 'Dist To',"
        lsSql &= " a.cert_shares as 'Cert Shares',"
        lsSql &= " a.tran_cert_no as 'Tran Cert No',"
        lsSql &= " a.tran_cert_tot_shares as 'Tran Cert Tot Shares',"
        lsSql &= " a.tran_certdist_from as 'Tran Dist From',"
        lsSql &= " a.tran_certdist_to as 'Tran Dist To',"
        lsSql &= " a.tran_cert_shares as 'Tran Cert Shares',"
        lsSql &= " a.client_id as 'Client Id',"
        lsSql &= " a.client_name as 'Client Name',"
        lsSql &= " a.dp_id as 'DP Id',"
        lsSql &= " a.dp_name as 'DP Name',"
        lsSql &= " a.tran_ref_no as 'Tran Ref No',"
        lsSql &= " a.tran_inward_no as 'Inward No',"
        lsSql &= " a.tran_inward_date as 'Inward Date',"
        lsSql &= " a.tran_outward_no as 'Outward No',"
        lsSql &= " a.tran_outward_date as 'Outward Date',"
        lsSql &= " a.history_gid as 'History Id',"
        lsSql &= " a.file_gid as 'File Id',"
        lsSql &= " a.comp_gid as 'Comp Id',"
        lsSql &= " a.folio_gid as 'Folio Id',"
        lsSql &= " a.tran_folio_gid as 'Tran Folio Id',"
        lsSql &= " a.cert_gid as 'Cert Id' "
        lsSql &= " from sta_trn_thistory as a "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.history_gid asc"

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        Dim lnInwardId As Long
        Dim objFrm As frmDocHistory

        If e.RowIndex >= 0 Then
            lnInwardId = dgvList.Rows(e.RowIndex).Cells("Inward Id").Value
            objFrm = New frmDocHistory(lnInwardId)
            objFrm.ShowDialog()
        End If
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

    Public Sub New(UploadId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnUploadId = UploadId
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub
End Class