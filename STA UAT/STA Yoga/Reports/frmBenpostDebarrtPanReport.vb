Public Class frmBenpostDebarrtPanReport

    Private Sub frmBenpostDebarrtPanReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Public Sub frmCtrClear(ByVal frmName As Object)
        Dim ctrl As Control
        Dim dtp As DateTimePicker

        For Each ctrl In frmName.Controls
            If ctrl.Tag <> "*" Then
                If TypeOf ctrl Is TextBox Then ctrl.Text = ""
                If TypeOf ctrl Is ComboBox Then
                    ctrl.Text = ""
                End If

                If TypeOf ctrl Is DateTimePicker Then
                    dtp = ctrl

                    If dtp.ShowCheckBox = True Then
                        dtp.Checked = False
                    End If
                End If

                If TypeOf ctrl Is Panel Then frmCtrClear(ctrl)
                If TypeOf ctrl Is GroupBox Then frmCtrClear(ctrl)
            End If
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvReport, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmBenpostDebarrtPanReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvReport
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvReport.Top + dgvReport.Height + 1
        pnlExport.Width = Me.Width
        btnExport.Left = pnlExport.Width - btnExport.Width - 32
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String = ""

        'Holder1 Name and Holder1 pan no conditions
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If txtDpId.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtDpId.Text) & "%' "
        If txtClientId.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtClientId.Text) & "%' "
        If txtHoldername.Text <> "" Then lsCond &= " and a.holder1_name like '" & QuoteFilter(txtHoldername.Text) & "%' "
        If txtPanno.Text <> "" Then lsCond &= " and a.holder1_pan_no like '" & QuoteFilter(txtPanno.Text) & "%' "


        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.dp_id as 'Dp Id',"
        lsSql &= " a.client_id as 'Client Id',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " c.pan_no as 'Pan No',"
        lsSql &= " a.holder1_name as 'Holder Name'"
        'lsSql &= " a.comp_gid as 'Comp Id',"
        'lsSql &= " a.benpost_gid as 'Benpost Id' "
        lsSql &= " from sta_trn_tcurrentbenpost as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tdebarrtorderdtl as c on a.holder1_pan = c.pan_no and c.delete_flag = 'N' and c.debarrt_flag = 'Y' "
        lsSql &= " inner join sta_trn_tdebarrtorder as d on c.debarrtorder_gid = d.debarrtorder_gid and d.delete_flag = 'N' and d.debarrt_flag = 'Y' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        'Holder2 Name and Holder2 pan no conditions
        lsCond = ""
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If txtDpId.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtDpId.Text) & "%' "
        If txtClientId.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtClientId.Text) & "%' "
        If txtHoldername.Text <> "" Then lsCond &= " and a.holder2_name like '" & QuoteFilter(txtHoldername.Text) & "%' "
        If txtPanno.Text <> "" Then lsCond &= " and a.holder1_pan_no like '" & QuoteFilter(txtPanno.Text) & "%' "


        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql &= " union all"
        lsSql &= " (select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.dp_id as 'Dp Id',"
        lsSql &= " a.client_id as 'Client Id',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " c.pan_no as 'Pan No',"
        lsSql &= " a.holder2_name as 'Holder Name'"
        'lsSql &= " a.comp_gid as 'Comp Id',"
        'lsSql &= " a.benpost_gid as 'Benpost Id' "
        lsSql &= " from sta_trn_tcurrentbenpost as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tdebarrtorderdtl as c on a.holder2_pan = c.pan_no and c.delete_flag = 'N' and c.debarrt_flag = 'Y' "
        lsSql &= " inner join sta_trn_tdebarrtorder as d on c.debarrtorder_gid = d.debarrtorder_gid and d.delete_flag = 'N' and d.debarrt_flag = 'Y' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N') "

        'Holder3 Name and Holder3 pan no conditions
        lsCond = ""
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If txtDpId.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtDpId.Text) & "%' "
        If txtClientId.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtClientId.Text) & "%' "
        If txtHoldername.Text <> "" Then lsCond &= " and a.holder2_name like '" & QuoteFilter(txtHoldername.Text) & "%' "
        If txtPanno.Text <> "" Then lsCond &= " and a.holder1_pan_no like '" & QuoteFilter(txtPanno.Text) & "%' "


        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql &= " union all"
        lsSql &= " (select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.dp_id as 'Dp Id',"
        lsSql &= " a.client_id as 'Client Id',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " c.pan_no as 'Pan No',"
        lsSql &= " a.holder3_name as 'Holder Name'"
        'lsSql &= " a.comp_gid as 'Comp Id',"
        'lsSql &= " a.benpost_gid as 'Benpost Id' "
        lsSql &= " from sta_trn_tcurrentbenpost as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tdebarrtorderdtl as c on a.holder3_pan = c.pan_no and c.delete_flag = 'N' and c.debarrt_flag = 'Y' "
        lsSql &= " inner join sta_trn_tdebarrtorder as d on c.debarrtorder_gid = d.debarrtorder_gid and d.delete_flag = 'N' and d.debarrt_flag = 'Y' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N') "

        gpPopGridView(dgvReport, lsSql, gOdbcConn)

        For i = 0 To dgvReport.ColumnCount - 1
            dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtRecCount.Text = "Total Records : " & dgvReport.RowCount.ToString
    End Sub

End Class