Public Class frmCompMstvsFolioMstReport

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

    Private Sub frmCompMasterReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

        If txtCompName.Text <> "" Then lsCond &= " and a.comp_name like '" & QuoteFilter(txtCompName.Text) & "%' "
        If txtISIN.Text <> "" Then lsCond &= " and a.isin_id like '" & QuoteFilter(txtISIN.Text) & "%' "


        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.isin_id as 'ISIN id',"
        lsSql &= " a.comp_name as 'Company Name',"
        lsSql &= " a.start_date as 'Start Date',"
        lsSql &= " a.share_qty as 'As Per Company Master',"
        lsSql &= " sum(ifnull(b.folio_shares,0)) as 'As Per Folio Master',"
        lsSql &= " (a.share_qty - sum(ifnull(b.folio_shares,0))) as 'Difference',"
        lsSql &= " case when a.comp_listed = 'Y' then 'Listed'"
        lsSql &= " else 'Unlisted' end as `Listed/Unlisted`,"
        lsSql &= " case when sum(ifnull(b.folio_shares,0)) = 0 then 'Folio Mst NA'"
        lsSql &= " when sum(ifnull(b.folio_shares,0)) = a.share_qty then 'Tally'"
        lsSql &= " else 'Not Tally' end as Status"
        lsSql &= " from sta_mst_tcompany as a"
        lsSql &= " left join sta_trn_tfolio as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N'"
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.active_flag = 'Y' and a.delete_flag = 'N' "
        lsSql &= " group by a.isin_id,a.comp_name "
        lsSql &= " order by a.start_date"

        gpPopGridView(dgvReport, lsSql, gOdbcConn)

        For i = 0 To dgvReport.ColumnCount - 1
            dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtRecCount.Text = "Total Records : " & dgvReport.RowCount.ToString
    End Sub
End Class