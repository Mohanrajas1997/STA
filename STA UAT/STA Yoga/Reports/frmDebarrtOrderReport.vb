Public Class frmDebarrtOrderReport

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        dtpFrom.Checked = False
        dtpTo.Checked = False

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

    Private Sub frmDebarrtOrderReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

        If cboReporttype.Text.Trim() = "" Then
            MsgBox("Please select the report type..!", MsgBoxStyle.Information, gsProjectName)
            cboReporttype.Focus()
            Exit Sub
        End If
        btnRefresh.Enabled = False

        If cboReporttype.Text = "Released" Then
            Call LoadReleaseData()
        Else
            Call LoadData()
        End If

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim i As Integer

        Try
            lsCond = ""

            If dtpFrom.Checked = True Then lsCond &= " and a.debarrtorder_date >= '" & Format(CDate(dtpFrom.Value), "yyyy-MM-dd") & "' "
            If dtpTo.Checked = True Then lsCond &= " and a.debarrtorder_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

            If txtPanno.Text <> "" Then lsCond &= " and b.pan_no like '" & QuoteFilter(txtPanno.Text) & "%' "
            If txtHoldername.Text <> "" Then lsCond &= " and b.pan_holdername like '" & QuoteFilter(txtHoldername.Text) & "%' "
            If txtOrderno.Text <> "" Then lsCond &= " and a.debarrtorder_no like '" & QuoteFilter(txtOrderno.Text) & "%' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsFld = ""
            lsFld &= " b.pan_no as 'Pan No',"
            lsFld &= " b.pan_holdername as 'Holder Name',"
            lsFld &= " a.debarrtorder_no as 'Order No',"
            lsFld &= " a.debarrtorder_date as 'Order Date',"
            lsFld &= " a.debarrtorder_remark as 'Remark',"
            lsFld &= " a.debarrtorder_gid as 'Order Gid',"
            lsFld &= " b.debarrtorderdtl_gid as 'OrderDtl Gid'"

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld

            lsSql &= " from sta_trn_tdebarrtorder as a "
            lsSql &= " inner join sta_trn_tdebarrtorderdtl as b on a.debarrtorder_gid = b.debarrtorder_gid and b.delete_flag = 'N' and b.debarrt_flag = 'Y' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.debarrt_flag = 'Y' and a.delete_flag = 'N' "
            lsSql &= " order by a.debarrtorder_gid,a.debarrtorder_date desc"

            Call gpPopGridView(dgvReport, lsSql, gOdbcConn)

            For i = 0 To dgvReport.Columns.Count - 1
                dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadReleaseData()
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim i As Integer

        Try
            lsCond = ""

            If dtpFrom.Checked = True Then lsCond &= " and a.debarrtorder_date >= '" & Format(CDate(dtpFrom.Value), "yyyy-MM-dd") & "' "
            If dtpTo.Checked = True Then lsCond &= " and a.debarrtorder_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

            If txtPanno.Text <> "" Then lsCond &= " and b.pan_no like '" & QuoteFilter(txtPanno.Text) & "%' "
            If txtHoldername.Text <> "" Then lsCond &= " and b.pan_holdername like '" & QuoteFilter(txtHoldername.Text) & "%' "
            If txtOrderno.Text <> "" Then lsCond &= " and a.debarrtorder_no like '" & QuoteFilter(txtOrderno.Text) & "%' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsFld = ""
            lsFld &= " b.pan_no as 'Pan No',"
            lsFld &= " b.pan_holdername as 'Holder Name',"
            lsFld &= " a.debarrtorder_no as 'Order No',"
            lsFld &= " a.debarrtorder_date as 'Release Date',"
            lsFld &= " a.debarrtorder_remark as 'Remark',"
            lsFld &= " a.debarrtorder_gid as 'Order Gid',"
            lsFld &= " b.debarrtorderdtl_gid as 'OrderDtl Gid'"

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld

            lsSql &= " from sta_trn_tdebarrtorder as a "
            lsSql &= " inner join sta_trn_tdebarrtorderdtl as b on a.debarrtorder_gid = b.debarrtorder_gid and b.delete_flag = 'N' and b.debarrt_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.debarrt_flag = 'N' and a.delete_flag = 'N' "
            lsSql &= " order by a.debarrtorder_gid,a.debarrtorder_date desc"

            Call gpPopGridView(dgvReport, lsSql, gOdbcConn)

            For i = 0 To dgvReport.Columns.Count - 1
                dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class