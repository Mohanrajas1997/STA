Public Class frmAuditLog_Rpt
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



        dtpFrom.Value = Now
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False
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

        If dtpFrom.Checked = True Then lsCond &= " and d.received_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and d.received_date <= '" & Format(dtpTo.Value, "yyyy-MM-dd") & "' "

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and d.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            lsCond &= " and d.tran_code = '" & cboDocType.SelectedValue.ToString & "' "
        End If

        If txtHolderName.Text <> "" Then lsCond &= " and d.shareholder_name like '" & QuoteFilter(txtHolderName.Text) & "%'"
        If txtFolioNo.Text <> "" Then lsCond &= " and d.folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%'"

        If txtInwardNo.Text <> "" Then lsCond &= " and d.inward_no = '" & Val(txtInwardNo.Text) & "' "

        If mnUploadId > 0 Then lsCond &= " and d.upload_gid = " & mnUploadId & " "

        If lsCond = "" Then lsCond = " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " d.inward_no as 'Inward No',"
        lsSql &= " e.comp_name as 'Company',"
        lsSql &= " f.trantype_desc as 'Document Type',"
        lsSql &= " g.folio_no as 'Folio No',"
        lsSql &= " concat(b.group_name,'-',a.queue_from_user) as 'From',"
        lsSql &= " concat(c.group_name,ifnull(concat('-',a.action_by),'')) as 'To'"

        lsSql &= " from sta_trn_tqueue as a  inner join sta_mst_tgroup as b on b.group_code = a.queue_from and b.delete_flag = 'N'  "
        lsSql &= " inner join sta_mst_tgroup as c on c.group_code = a.queue_to "
        lsSql &= " and c.delete_flag = 'N'  inner join sta_trn_tinward as d on a.inward_gid = d.inward_gid and d.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as e on d.comp_gid=e.comp_gid and e.delete_flag='N' "
        lsSql &= " inner join sta_mst_ttrantype as f on d.tran_code=f.trantype_code and f.delete_flag='N' "
        lsSql &= " inner join sta_trn_tfolio as g on g.folio_gid=d.folio_gid and g.delete_flag='N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.inward_gid,a.queue_gid"


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