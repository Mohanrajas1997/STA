Imports MySql.Data.MySqlClient

Public Class frmUploadView
    Dim mnUploadId As Long
    Dim mbRejectFlag As Boolean = False

    Private Sub frmUploadView_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Call LoadGrid()
    End Sub

    Public Sub New(UploadId As Long, CompName As String, Optional RejectFlag As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnUploadId = UploadId
        txtCompName.Text = CompName
        mbRejectFlag = RejectFlag
    End Sub

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim n As Integer
        Dim lobjbtnColumn As DataGridViewButtonColumn

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.inward_no as 'Inward No',"
        lsSql &= " b.folio_no as 'Folio No',"
        lsSql &= " b.shareholder_name as 'Share Holder',"
        lsSql &= " b.tran_code,"
        lsSql &= " c.trantype_desc as 'Document',"
        lsSql &= " make_set(b.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
        lsSql &= " make_set(b.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
        lsSql &= " b.inward_gid "
        lsSql &= " from sta_trn_tqueue as a "
        lsSql &= " inner join sta_trn_tinward as b on b.inward_gid = a.inward_gid and b.queue_gid = a.queue_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tgroup as d on d.group_code = a.queue_from and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= " and b.upload_gid = " & mnUploadID & " "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by b.inward_no "

        With dgvList
            .Columns.Clear()

            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            .Columns("tran_code").Visible = False
            .Columns("inward_gid").Visible = False

            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Rejected"
            lobjbtnColumn.Width = 100
            lobjbtnColumn.Name = "Rejected"
            lobjbtnColumn.Text = "Rejected"

            .Columns.Add(lobjbtnColumn)

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "View"
            lobjbtnColumn.Width = 100
            lobjbtnColumn.Name = "View"
            lobjbtnColumn.Text = "View"

            .Columns.Add(lobjbtnColumn)

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n).Value = "Rejected"
                .Rows(i).Cells(n + 1).Value = "View"
            Next i

            If mbRejectFlag = False Then
                .Columns(n).Visible = False
            End If

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub frmUploadView_Load(sender As Object, e As EventArgs) Handles Me.Load
        CancelButton = btnClose
    End Sub

    Private Sub frmUpload_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim lnInwardId As Long
        Dim frm As frmDocHistory

        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case dgvList.Columns.Count - 2
                    If MessageBox.Show("Are you sure to revoke ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        lnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value
                        Call InwardUploadRevoke(mnUploadId, lnInwardId)
                        Call LoadGrid()
                    End If
                Case dgvList.Columns.Count - 1
                    lnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value
                    frm = New frmDocHistory(lnInwardId)
                    frm.ShowDialog()
            End Select
        End If
    End Sub

    Private Function InwardUploadRevoke(UploadId As Long, InwardId As Long) As Long
        Dim lnResult As Long = 0
        Dim lsTxt As String

        Try
            Using cmd As New MySqlCommand("pr_sta_set_inwarduploadrevoke", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", InwardId)
                cmd.Parameters.AddWithValue("?in_upload_gid", UploadId)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                If lnResult = 1 Then
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                Return lnResult
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return lnResult
        End Try
    End Function
End Class