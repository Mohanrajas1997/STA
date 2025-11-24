Public Class frmOutwardReport
    Dim mnFolioId As Long = 0

    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' courier
        lsSql = ""
        lsSql &= " select courier_gid,courier_name from sta_mst_tcourier "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by courier_name asc "

        Call gpBindCombo(lsSql, "courier_name", "courier_gid", cboCourier, gOdbcConn)

        dtpFrom.Value = DateAdd(DateInterval.Day, -7, Now)
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False

        If mnFolioId > 0 Then btnRefresh.PerformClick()
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

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If dtpFrom.Checked = True Then lsCond &= " and a.outward_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and a.outward_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

        If Val(txtInwardNo.Text) > 0 Then lsCond &= " and b.inward_no = " & Val(txtInwardNo.Text) & " "
        If txtFolioNo.Text <> "" Then lsCond &= " and b.folio_no = '" & txtFolioNo.Text & "' "
        If txtAwbNo.Text <> "" Then lsCond &= " and a.awb_no = '" & txtAwbNo.Text & "' "

        If Val(txtInwardId.Text) > 0 Then lsCond &= " and a.inward_gid = " & Val(txtInwardId.Text) & " "
        If Val(txtOutwardId.Text) > 0 Then lsCond &= " and a.outward_gid = " & Val(txtOutwardId.Text) & " "

        If cboCourier.Text <> "" And cboCourier.SelectedIndex <> -1 Then
            lsCond &= " and a.courier_gid = " & Val(cboCourier.SelectedValue.ToString) & " "
        End If

        If mnFolioId > 0 Then
            lsCond &= " and b.folio_gid = " & mnFolioId.ToString & " "
        End If

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " c.comp_name as 'Company',"
        lsSql &= " e.trantype_desc as 'Document Type',"
        lsSql &= " date_format(a.outward_date,'%d-%m-%Y') as 'Outward Date',"
        lsSql &= " date_format(b.received_date,'%d-%m-%Y') as 'Inward Date',"
        lsSql &= " b.inward_comp_no as 'Inward No',"
        lsSql &= " b.folio_no as 'Folio No',"
        lsSql &= " b.shareholder_name as 'Share Holder Name',"
        'lsSql &= " b.shareholder_addr as 'Share Holder Address',"
        'lsSql &= " a.outward_mode as 'Outward Mode',"
        lsSql &= " d.courier_name as 'Courier Name',"
        lsSql &= " a.awb_no as 'Awb No',"
        lsSql &= " a.outward_remark as 'Remark',"
        'lsSql &= " b.inward_no as 'Inward No',"
        lsSql &= " a.outward_gid as 'Outward Id',"
        lsSql &= " a.inward_gid as 'Inward Id',"
        lsSql &= " b.folio_gid as 'Folio Id',"
        lsSql &= " a.courier_gid as 'Courier Id',"
        lsSql &= " b.comp_gid as 'Comp Id' "
        lsSql &= " from sta_trn_toutward as a "
        lsSql &= " inner join sta_trn_tinward as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as c on b.comp_gid = c.comp_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_ttrantype as e on b.tran_code = e.trantype_code and e.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tcourier as d on a.courier_gid = d.courier_gid and d.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
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
End Class