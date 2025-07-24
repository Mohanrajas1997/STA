Public Class frmInwardList
    Dim mnFolioId As Long = 0
    Dim mbAttachmentFlag As Boolean = False
    Dim mnInwardId As Long = 0
    Dim mbUpdateRcvdDate As Boolean = False
    Dim mbUpdateApproveDate As Boolean = False

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

        ' queue
        lsSql = ""
        lsSql &= " select group_status,group_code,group_name from sta_mst_tgroup "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by group_name asc "

        Call gpBindCombo(lsSql, "group_name", "group_status", cboQueue, gOdbcConn)

        ' updated
        With cboUpdated
            .Items.Clear()
            .Items.Add("Yes")
            .Items.Add("No")
        End With

        ' completed
        With cboCompleted
            .Items.Clear()
            .Items.Add("Yes")
            .Items.Add("No")
        End With

        ' valid
        With cboValid
            .Items.Clear()
            .Items.Add("Yes")
            .Items.Add("No")
        End With

        dtpFrom.Value = Now
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False

        If mnFolioId > 0 Or mnInwardId > 0 Then btnSearch.PerformClick()
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
        Dim n As Integer = 0

        Dim lobjbtnColumn As DataGridViewButtonColumn
        Dim lobjViewLinkButton As DataGridViewLinkColumn

        lsCond = ""

        If dtpFrom.Checked = True Then lsCond &= " and b.received_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and b.received_date <= '" & Format(dtpTo.Value, "yyyy-MM-dd") & "' "

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            lsCond &= " and b.tran_code = '" & cboDocType.SelectedValue.ToString & "' "
        End If

        If txtHolderName.Text <> "" Then lsCond &= " and b.shareholder_name like '" & QuoteFilter(txtHolderName.Text) & "%'"
        If txtFolioNo.Text <> "" Then lsCond &= " and b.folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%'"
        If txtInwardNo.Text <> "" Then lsCond &= " and b.inward_comp_no = '" & QuoteFilter(txtInwardNo.Text.ToString) & "' "

        If cboQueue.Text <> "" And cboQueue.SelectedIndex <> -1 Then
            lsCond &= " and b.queue_status = " & Val(cboQueue.SelectedValue.ToString) & " "
        End If

        Select Case cboUpdated.Text.ToUpper
            Case "YES"
                lsCond &= " and b.update_completed = 'Y' "
            Case "NO"
                lsCond &= " and b.update_completed = 'N' "
        End Select

        Select Case cboCompleted.Text.ToUpper
            Case "YES"
                lsCond &= " and b.inward_completed = 'Y' "
            Case "NO"
                lsCond &= " and b.inward_completed = 'N' "
        End Select

        Select Case cboValid.Text.ToUpper
            Case "YES"
                lsCond &= " and b.chklst_disc = 0 "
            Case "NO"
                lsCond &= " and b.chklst_disc > 0 "
        End Select

        If mnFolioId > 0 Then lsCond &= " and b.folio_gid = " & mnFolioId & " "
        If mnInwardId > 0 Then lsCond &= " and b.inward_gid = " & mnInwardId & " "

        If lsCond = "" Then lsCond = " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.inward_no as 'Auto Inward No',"
        lsSql &= " b.inward_comp_no as 'Inward No',"
        lsSql &= " e.comp_name as 'Company',"
        lsSql &= " b.folio_no as 'Folio No',"
        lsSql &= " b.shareholder_name as 'Share Holder',"
        lsSql &= " b.tran_code,"
        lsSql &= " c.trantype_desc as 'Document',"
        lsSql &= " make_set(b.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
        lsSql &= " make_set(b.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
        lsSql &= " b.inward_gid "
        lsSql &= " from sta_trn_tinward as b "
        lsSql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and b.delete_flag = 'N' "
        lsSql &= " order by b.inward_gid desc"

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)


        With dgvList
            For i = 0 To dgvList.ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            n = .ColumnCount

            ' view button
            lobjViewLinkButton = New DataGridViewLinkColumn

            lobjViewLinkButton.HeaderText = "Add Attachment"
            lobjViewLinkButton.Text = "Add Attachment"
            lobjViewLinkButton.Width = 150

            .Columns.Add(lobjViewLinkButton)

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Update Received Date"
            lobjbtnColumn.Name = "Update Received Date"
            lobjbtnColumn.Text = "Update Received Date"
            lobjbtnColumn.Width = 150

            .Columns.Add(lobjbtnColumn)

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Update Approved Date"
            lobjbtnColumn.Name = "Update Approved Date"
            lobjbtnColumn.Text = "Update Approved Date"
            lobjbtnColumn.Width = 150

            .Columns.Add(lobjbtnColumn)

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n).Value = "Add Attachment"
                .Rows(i).Cells(n + 1).Value = "Update Received Date"
                .Rows(i).Cells(n + 2).Value = "Update Approved Date"
            Next i
        End With

        dgvList.Columns("tran_code").Visible = False
        dgvList.Columns("inward_gid").Visible = False

        If mbAttachmentFlag = False Then dgvList.Columns(n).Visible = False
        If mbUpdateRcvdDate = False Then dgvList.Columns(n + 1).Visible = False
        If mbUpdateApproveDate = False Then dgvList.Columns(n + 2).Visible = False

        txtTotRec.Text = "Total Records : " & dgvList.RowCount
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim lnInwardId As Long

        If e.RowIndex >= 0 Then
            lnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value

            Select Case e.ColumnIndex
                Case dgvList.Columns.Count - 3
                    Dim objFrm = New frmAttachmentAdd(lnInwardId, True)
                    objFrm.ShowDialog()
                Case dgvList.Columns.Count - 2
                    Dim objFrm = New frmInwardUpdateRcvdDate(lnInwardId)
                    objFrm.ShowDialog()
                Case dgvList.Columns.Count - 1
                    Dim objFrm = New frmInwardUpdateApprDate(lnInwardId)
                    objFrm.ShowDialog()
            End Select
        End If
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        Dim lnInwardId As Long
        Dim objFrm As frmDocHistory

        If e.RowIndex >= 0 And e.ColumnIndex < dgvList.Columns.Count - 1 Then
            lnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value
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

    Public Sub New(AttachmentFlag As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mbAttachmentFlag = AttachmentFlag
    End Sub

    Public Sub New(FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub

    Public Sub New(InwardId As Long, UpdateRcvdDate As Boolean)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnInwardId = InwardId
        mbUpdateRcvdDate = UpdateRcvdDate
    End Sub

    Public Sub New(InwardId As Long, UpdateRcvdDate As Boolean, UpdateApprDate As Boolean)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnInwardId = InwardId
        mbUpdateRcvdDate = False
        mbUpdateApproveDate = UpdateApprDate
    End Sub

End Class