Public Class frmRudCorresReport
    Dim mnFolioId As Long = 0
    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String


        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", CboCompany, gOdbcConn)

        ' doc type
        With cboDoctype
            .Items.Clear()
            .Items.Add("Return Undelivered")
            .Items.Add("Correspondence")
        End With

        'Status
        With cboStatus
            .Items.Clear()
            .Items.Add("Available")
            .Items.Add("Dispatched")
        End With

        dtpFrom.Value = Now
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False

        CboCompany.Focus()

    End Sub

    Private Sub cboDoctype_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboDoctype.SelectedIndexChanged
        cboSubtype.Items.Clear()
        cboSubtype.Text = String.Empty
        Dim doctype As Integer
        doctype = cboDoctype.SelectedIndex
        If doctype = 0 Then
            With cboSubtype
                .Items.Clear()
                .Items.Add("Allotment")
                .Items.Add("Transfers")
                .Items.Add("New Share Certificate")
                .Items.Add("Demat Rejection")
                .Items.Add("Alteration")
            End With
        Else
            With cboSubtype
                .Items.Clear()
                .Items.Add("PA-POWER OF ATTORNEY")
                .Items.Add("MN-MANDATE INSTRUCTIONS")
                .Items.Add("DR-REVALIDATION OF DIVIDEND WARRANT(S)")
                .Items.Add("EN-CALL MONEY ENDORSEMENT ON SHARE CERTIFICATE(S)")
                .Items.Add("RE-RECTIFICATION OF SHARE CERTIFICATE(S)")
                .Items.Add("LN-MARKING LIEN ON SHARES")
                .Items.Add("NC-NAME CORRECTION")
                .Items.Add("DW-ISSUE OF DUPLICATE DIVIDEND WARRANT")
                .Items.Add("VS-VERIFICATION OF SIGNATURE")
                .Items.Add("DD-DIVIDEND WARRANT CORRECTION")
                .Items.Add("FR-FINANCIAL RESULTS")
                .Items.Add("MU-MUTILATED SHARE CERTIFICATE")
                .Items.Add("RA-MARKET PRICE")
                .Items.Add("CI-COMPANY DETAILS")
                .Items.Add("EC-ECS INSTRUCTION")
                .Items.Add("SD-SHARE HOLDING DETAILS")
                .Items.Add("AS-AUTHORISED SIGNATURE")
                .Items.Add("DI-DIVIDEND DETAILS")
                .Items.Add("MS-CHANGE OF SIGNATURE")
                .Items.Add("EM-REGISTRATION OF EMAIL ID")
                .Items.Add("RV-NON RECEIPT OF REFUND ORDER AFTER REVALIDATION")
                .Items.Add("CT-NON RECEIPT OF SHARE CERTIFICATE(S) AFTER TRANSFER")
                .Items.Add("CE-NON RECEIPT OF SHARE CERTIFICATE(S) AFTER ENDORSEMENT")
                .Items.Add("CS-NON RECEIPT OF CERTIFICATE(S) AFTER SPLIT/CONSOLIDATION")
                .Items.Add("DW-NON RECEIPT OF DIVIDEND WARRANT(S)")
                .Items.Add("DV-NON RECEIPT OF DIVIDEND WARRANT(S) AFTER REVALIDATION")
                .Items.Add("NA-NON RECEIPT OF ANNUAL REPORTS")
                .Items.Add("CM-NON RECEIPT OF SHARE CERTIFICATE DULY REPLACED")
                .Items.Add("CD-NON RECEIPT OF DUPLICATE CERTIFICATE")
                .Items.Add("EC-ECS QUERIES")
                .Items.Add("DT-NON RECEIPT OF DEMAT REJECTION")
                .Items.Add("BR-REQUEST FOR REMATERIALISATION")
                .Items.Add("DC-DEMAT CREDIT PENDING")
            End With
        End If

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
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub
    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""


        'If (CboCompany.Text = "" Or CboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
        '    MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    CboCompany.Focus()
        '    Exit Sub
        'End If

        If (cboDoctype.Text = "" Or cboDoctype.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the Doc Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboDoctype.Focus()
            Exit Sub
        End If


        'If (cboSubtype.Text = "" Or cboSubtype.SelectedIndex = -1) And mnFolioId = 0 Then
        '    MessageBox.Show("Please select the SubType !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    cboSubtype.Focus()
        '    Exit Sub
        'End If

        'If (CboStatus.Text = "" Or CboStatus.SelectedIndex = -1) And mnFolioId = 0 Then
        '    MessageBox.Show("Please Select the Status !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    CboStatus.Focus()
        '    Exit Sub
        'End If


        If CboCompany.Text <> "" And CboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid= " & Val(CboCompany.SelectedValue.ToString) & ""
        End If

        If cboDoctype.Text <> "" And cboDoctype.SelectedIndex <> -1 Then
            lsCond &= " and a.document_type= '" & cboDoctype.Text & "' "
        End If

        If cboSubtype.Text <> "" And cboSubtype.SelectedIndex <> -1 Then
            lsCond &= " and a.corres_sub_type= '" & cboSubtype.Text & "' "
        End If

        If CboStatus.Text <> "" And CboStatus.SelectedIndex <> -1 Then
            lsCond &= " and a.remarks = '" & CboStatus.Text & "' "
        End If

        If txtFoliono.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtFoliono.Text) & "%' "

        If TxtCertificate.Text <> "" Then lsCond &= " and a.certificate_no like '" & QuoteFilter(TxtCertificate.Text) & "%' "
        If txtDisAWD.Text <> "" Then lsCond &= " and a.dispatch_awb_no like '" & QuoteFilter(txtDisAWD.Text) & "%' "
        If txtInwardNo.Text <> "" Then lsCond &= " and a.corres_slno like '" & QuoteFilter(txtInwardNo.Text) & "%' "
        If txtRcdAwd.Text <> "" Then lsCond &= " and a.awb_no like '" & QuoteFilter(txtRcdAwd.Text) & "%' "
        If Txtholdername.Text <> "" Then lsCond &= " and a.holder_name like '" & QuoteFilter(Txtholdername.Text) & "%' "


        If mnFolioId > 0 Then lsCond &= " and a.corres_gid = " & mnFolioId & " "

        If dtpFrom.Checked = True Then
            lsCond &= " and a.return_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpTo.Checked = True Then
            lsCond &= " AND a.return_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "'"
        End If

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.corres_slno as 'Inward No',"
        lsSql &= " a.certificate_no as 'Certificate No',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " a.document_type as 'Document Type',"
        lsSql &= " a.corres_sub_type as 'Sub Type',"
        lsSql &= " a.folio_no as 'Folio No',"
        lsSql &= " a.holder_name as 'Holder Name',"
        lsSql &= " c.courier_name as 'Courier Name',"
        lsSql &= " a.awb_no as 'AWB No',"
        lsSql &= " a.return_date as 'Received Date',"
        lsSql &= " d.user_name as 'Received By',"
        lsSql &= " a.remarks as 'Status',"
        lsSql &= " a.return_reason as 'Reason', "
        lsSql &= " a.dispatch_date as 'Dispatched Date', "
        lsSql &= " e.courier_name as 'Dispatched Courier Name', "
        lsSql &= " a.dispatch_awb_no as 'Dispatched AWB No', "
        lsSql &= " a.dispatch_address as 'Dispatched Address', "
        lsSql &= " f.user_name as 'Dispatched By',"
        lsSql &= " a.dispatch_remark as 'Dispatch Reason', "
        lsSql &= " a.corres_gid as 'Correspondence Gid' "
        lsSql &= " from sta_trn_tcorrespondence as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcourier as c on a.Courier_gid = c.Courier_gid and c.delete_flag = 'N' "
        lsSql &= " inner join soft_mst_tuser as d on d.user_code=a.return_received_by and d.delete_flag='N' "
        lsSql &= " left join sta_mst_tcourier as e on a.dispatch_courier_gid=e.courier_gid and e.delete_flag='N' "
        lsSql &= " left join soft_mst_tuser as f on a.dispatched_by=f.user_code and f.delete_flag='N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.corres_slno asc"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
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
                lnFolioId = .CurrentRow.Cells("Correspondence Gid").Value

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