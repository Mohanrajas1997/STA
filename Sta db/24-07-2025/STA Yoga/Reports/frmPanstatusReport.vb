Public Class frmPanstatusReport

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
        btnRefresh.Enabled = False

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim i As Integer
        Dim lsPanstatus As String

        Try
            lsCond = ""

            If cboPanststus.Text = "Released" Then
                lsPanstatus = "N"
            Else
                lsPanstatus = "Y"
            End If

            If txtPanno.Text.Trim() <> "" Then lsCond &= " and b.pan_no like '" & QuoteFilter(txtPanno.Text) & "%' "
            If cboPanststus.Text.Trim() <> "" Then lsCond &= " and b.debarrt_flag = '" & QuoteFilter(lsPanstatus) & "' "

            If lsCond = "" Then lsCond &= " and 1 = 1 "

            lsFld = ""
            lsFld &= " b.pan_no as 'Pan No',"
            lsFld &= " b.pan_holdername as 'Holder Name',"
            lsFld &= " case when b.debarrt_flag = 'Y' then 'Debarred' else 'Released' end as 'Pan Status',"
            lsFld &= " b.panmaster_gid as 'Pan Gid'"

            lsSql = ""
            lsSql &= " select Distinct"
            lsSql &= lsFld

            lsSql &= " from sta_trn_tdebarrtorderdtl as a "
            lsSql &= " inner join sta_mst_tpanmaster as b on a.pan_no = b.pan_no and a.pan_holdername = b.pan_holdername and b.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by b.pan_holdername "

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