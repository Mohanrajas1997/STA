Imports DocumentFormat.OpenXml.VariantTypes

Public Class frmBenpostBulkImportReport
    Private Sub frmBenpostBulkImportReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        dtpFrom.Value = Now
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False

        btnClear.PerformClick()
        ' load file type
        lsSql = ""
        lsSql &= " select * from sta_mst_tfile "
        lsSql &= " where delete_flag = 'N' and file_desc in ('Benpost - NSDL','Benpost - CDSL') "
        lsSql &= " order by file_desc "

        Call gpBindCombo(lsSql, "file_desc", "file_type", cboFileType, gOdbcConn)

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False

        LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String
        Dim i As Integer

        Try
            lsCond = ""

            If dtpFrom.Checked = True Then lsCond &= " and b.insert_date >= '" & Format(CDate(dtpFrom.Value), "yyyy-MM-dd") & "' "
            If dtpTo.Checked = True Then lsCond &= " and b.insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

            If cboFileType.Text <> "" And cboFileType.SelectedIndex >= 0 Then lsCond &= " and b.file_type = '" & QuoteFilter(cboFileType.SelectedValue.ToString) & "' "


            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= "SELECT "
            lsSql &= " b.file_name AS 'File Name',"
            lsSql &= " f.file_desc AS 'File Type',"
            lsSql &= " b.isin_id AS 'ISIN ID',"
            lsSql &= " b.benpost_date AS 'Benpost Date',"
            lsSql &= " b.file_remark AS 'File Remark',"
            lsSql &= " b.insert_date AS 'Insert Date',"
            lsSql &= " b.insert_by AS 'Inserted By' "
            lsSql &= " FROM sta_trn_tbenposlog as b "
            lsSql &= " inner join sta_mst_tfile as f "
            lsSql &= " on f.delete_flag = 'N' and "
            lsSql &= " f.file_desc in ('Benpost - NSDL','Benpost - CDSL') "
            lsSql &= " and f.file_type=b.file_type "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " ORDER BY b.benposlog_gid DESC"

            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            For i = 0 To dgvList.Columns.Count - 1
                dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvList.RowCount
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

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        dgvList.DataSource = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmBenpostBulkImportReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvList
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvList.Top + dgvList.Height + 1
        pnlExport.Width = Me.Width
        btnExport.Left = pnlExport.Width - btnExport.Width - 32
    End Sub
End Class