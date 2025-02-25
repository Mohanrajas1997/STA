Public Class frmUploadPrint
    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' upload
        lsSql = ""
        lsSql &= " select upload_type,upload_desc from sta_mst_tupload "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by upload_desc asc "

        Call gpBindCombo(lsSql, "upload_desc", "upload_type", cboUpload, gOdbcConn)

        dtpFrom.Value = DateAdd(DateInterval.Day, -7, Now)
        dtpTo.Value = Now
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
        Dim lobjbtnColumn As DataGridViewButtonColumn
        Dim n As Integer

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cboUpload.Text <> "" And cboUpload.SelectedValue <> -1 Then
            lsCond &= " and a.upload_type = " & Val(cboUpload.SelectedValue.ToString) & " "
        End If

        lsCond &= " and a.upload_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        lsCond &= " and a.upload_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

        If Val(txtUploadNo.Text) > 0 Then lsCond &= " and a.upload_no = " & Val(txtUploadNo.Text) & " "
        If txtUploadId.Text <> "" Then lsCond &= " and a.upload_gid in (" & txtUploadId.Text & ") "

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_gid,"
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " date_format(a.upload_date,'%d-%m-%Y') as 'Upload Date',"
        lsSql &= " a.upload_no as 'Upload No',"
        lsSql &= " c.upload_desc as 'Upload Type',"
        lsSql &= " a.transfer_count as 'Transfer',"
        lsSql &= " a.cert_count as 'Certificate',"
        lsSql &= " a.objx_count as 'Objection',"
        lsSql &= " a.upload_filename as 'Demat File Name',"
        lsSql &= " make_set(a.upload_status," & gsUploadStatusDesc & ") as 'Status' "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tupload as c on a.upload_type = c.upload_type and c.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.upload_no desc "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Regenerate"
            lobjbtnColumn.Name = "Regenerate"
            lobjbtnColumn.Text = "Regenerate"

            .Columns.Add(lobjbtnColumn)

            n = .Columns.Count - 1

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n).Value = "Regenerate"
            Next i

            .Columns("upload_gid").Visible = False

            .Columns("Upload Type").Width = 150
            .Columns("Transfer").Width = 75
            .Columns("Certificate").Width = 75
            .Columns("Objection").Width = 75
            .Columns("Demat File Name").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
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

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim lnUploadId As Long
        Dim lobjUpload As clsUpload

        With dgvList
            If e.RowIndex >= 0 Then
                lnUploadId = .Rows(e.RowIndex).Cells("upload_gid").Value

                Select Case e.ColumnIndex
                    Case .ColumnCount - 1       ' print
                        lobjUpload = New clsUpload
                        lobjUpload.ShowUpload(lnUploadId)
                End Select
            End If
        End With
    End Sub
End Class