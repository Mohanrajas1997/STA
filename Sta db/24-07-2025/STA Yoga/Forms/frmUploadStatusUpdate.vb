Public Class frmUploadStatusUpdate
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

        If cboUpload.Text <> "" And cboUpload.SelectedIndex <> -1 Then
            lsCond &= " and a.upload_type = " & Val(cboUpload.SelectedValue.ToString) & " "
        End If

        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_gid,"
        lsSql &= " a.upload_type,"
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " date_format(a.upload_date,'%d-%m-%Y') as 'Upload Date',"
        lsSql &= " a.upload_no as 'Upload No',"
        lsSql &= " make_set(a.upload_type," & gsUploadTypeDesc & ") as 'Upload Type',"
        lsSql &= " a.transfer_count as 'Transfer',"
        lsSql &= " a.cert_count as 'Certificate',"
        lsSql &= " a.objx_count as 'Objection' "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.upload_status = " & gnUploadStatusDone & " "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.upload_gid "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Success"
            lobjbtnColumn.Name = "Success"
            lobjbtnColumn.Text = "Success"

            .Columns.Add(lobjbtnColumn)

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Failure"
            lobjbtnColumn.Name = "Failure"
            lobjbtnColumn.Text = "Failure"

            .Columns.Add(lobjbtnColumn)

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Rejected"
            lobjbtnColumn.Name = "Rejected"
            lobjbtnColumn.Text = "Rejected"

            .Columns.Add(lobjbtnColumn)

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "View"
            lobjbtnColumn.Name = "View"
            lobjbtnColumn.Text = "View"

            .Columns.Add(lobjbtnColumn)

            n = .Columns.Count - 1

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n - 3).Value = "Success"
                .Rows(i).Cells(n - 2).Value = "Failure"
                .Rows(i).Cells(n - 1).Value = "Rejected"
                .Rows(i).Cells(n).Value = "View"
            Next i

            .Columns("upload_gid").Visible = False
            .Columns("upload_type").Visible = False
            .Columns("Upload Type").Width = 200

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
        Dim lsCompName As String
        Dim frm As Object

        With dgvList
            If e.RowIndex >= 0 Then
                lnUploadId = .Rows(e.RowIndex).Cells("upload_gid").Value

                Select Case e.ColumnIndex
                    Case .ColumnCount - 1       ' view
                        lsCompName = .Rows(e.RowIndex).Cells("Company").Value

                        frm = New frmUploadView(lnUploadId, lsCompName)
                        frm.ShowDialog()
                    Case .ColumnCount - 2       ' rejected
                        Select Case .Rows(e.RowIndex).Cells("upload_type").Value
                            Case gnUploadCDSLUpload, gnUploadNSDLUpload
                                lsCompName = .Rows(e.RowIndex).Cells("Company").Value

                                frm = New frmUploadView(lnUploadId, lsCompName, True)
                                frm.ShowDialog()

                                Call btnSearch.PerformClick()
                            Case Else
                                MessageBox.Show("Access denied !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Select
                    Case .ColumnCount - 3       ' failure
                        frm = New frmUploadStatus(lnUploadId, gnUploadStatusFailure)
                        frm.ShowDialog()

                        Call btnSearch.PerformClick()
                    Case .ColumnCount - 4       ' success
                        frm = New frmUploadStatus(lnUploadId, gnUploadStatusSuccess)
                        frm.ShowDialog()

                        Call btnSearch.PerformClick()
                End Select
            End If
        End With
    End Sub
End Class