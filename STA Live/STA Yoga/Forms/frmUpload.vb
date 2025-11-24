Imports MySql.Data.MySqlClient

Public Class frmUpload
    Dim msGroupCode As String
    Dim mnCompId As Long
    Dim msUploadTypeDesc As String
    Dim mnUploadType As Integer

    Private Sub frmUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpMeetingDate.Value = Now
        Call LoadGrid()
    End Sub

    Public Sub New(GroupCode As String, CompId As Long, CompName As String, UploadType As Integer, UploadTypeDesc As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnCompId = CompId
        txtCompName.Text = CompName
        msGroupCode = GroupCode
        mnUploadType = UploadType
        msUploadTypeDesc = UploadTypeDesc
    End Sub

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String
        Dim n As Integer
        Dim lobjbtnColumn As DataGridViewButtonColumn

        lsCond = ""

        Select Case mnUploadType
            Case gnUploadTransferRegister
                lsCond &= " and c.transfer_flag = 'Y' and a.queue_to = '" & msGroupCode & "' "
            Case gnUploadCertRegister
                lsCond &= " and c.cert_flag = 'Y' and a.queue_to = '" & msGroupCode & "' "
            Case gnUploadObjxRegister
                lsCond &= " and a.queue_to = 'D' and c.objx_flag = 'Y' "
                lsCond &= " and b.chklst_disc > 0 "
            Case gnUploadCDSLUpload
                lsCond &= " and c.demat_flag = 'Y' and (a.queue_to = '" & msGroupCode & "' "
                'CDSL Demat Reject Start
                lsCond &= " or (a.queue_to = 'D' and b.inward_all_status & " & gnInwardInex & " > 0) "
                'CDSL Demat Reject end
                lsCond &= " ) and f.depository_code = 'C' "
            Case gnUploadNSDLUpload
                lsCond &= " and c.demat_flag = 'Y' "
                lsCond &= " and (a.queue_to = '" & msGroupCode & "' "
                lsCond &= " or (a.queue_to = 'D' and b.inward_all_status & " & gnInwardInex & " > 0)) "
                lsCond &= " and f.depository_code = 'N' "
            Case gnAllotmentNSDL
                lsCond = " and a.queue_to = '" & msGroupCode & "' "
                lsCond &= " and g.folio_no = '00999999' "
            Case gnAllotmentCDSL
                lsCond = " and a.queue_to = '" & msGroupCode & "' "
                lsCond &= " and g.folio_no = '00888888' "
            Case gnPhyToiepfNSDL
                lsCond = " and a.queue_to = '" & msGroupCode & "' "
                lsCond &= " and g.folio_no = '00999999' "
            Case gnPhyToiepfCDSL
                lsCond = " and a.queue_to = '" & msGroupCode & "' "
                lsCond &= " and g.folio_no = '00888888' "
        End Select

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.inward_comp_no as 'Inward No',"
        'lsSql &= " b.inward_no as 'Inward No',"
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
        lsSql &= " left join sta_trn_tdematpend as f on b.dematpend_gid = f.dematpend_gid and f.delete_flag = 'N' "
        lsSql &= " left join sta_trn_tfolio as g on b.folio_gid = g.folio_gid and g.delete_flag = 'N' "
        lsSql &= " where b.comp_gid = " & mnCompId & " "
        lsSql &= lsCond
        lsSql &= " and a.action_status = 0 "
        lsSql &= " and b.upload_gid = 0 "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by b.inward_no "

        With dgvList
            .Columns.Clear()

            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            n = .Columns.Count

            For i = 0 To .ColumnCount - 1
                .Columns(i).ReadOnly = True
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "View"
            lobjbtnColumn.Width = 50
            lobjbtnColumn.Name = "View"
            lobjbtnColumn.Text = "View"

            .Columns.Add(lobjbtnColumn)

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Send Back"
            lobjbtnColumn.Width = 100
            lobjbtnColumn.Name = "Send Back"
            lobjbtnColumn.Text = "Send Back"

            .Columns.Add(lobjbtnColumn)

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n).Value = "View"
                .Rows(i).Cells(n + 1).Value = "Send Back"
            Next i

            .Columns("tran_code").Visible = False
            .Columns("inward_gid").Visible = False

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
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

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If DateDiff(DateInterval.Day, dtpMeetingDate.Value, Now) < 0 Then
            MessageBox.Show("System will not accept future meeting date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpMeetingDate.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to generate ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Call GenerateUpload()
        End If
    End Sub

    Private Sub GenerateUpload()
        Dim lnResult As Long
        Dim lsTxt As String
        Dim lnUploadId As Long
        Dim lobjUpload As clsUpload

        Try
            Using cmd As New MySqlCommand("pr_sta_generate_upload", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_comp_gid", mnCompId)
                cmd.Parameters.AddWithValue("?in_upload_type", mnUploadType)
                cmd.Parameters.AddWithValue("?in_meeting_date", dtpMeetingDate.Value)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_upload_gid", MySqlDbType.Int32)
                cmd.Parameters("?out_upload_gid").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()
                lnUploadId = Val(cmd.Parameters("?out_upload_gid").Value.ToString())

                If lnResult = 1 Then
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lobjUpload = New clsUpload

                    Select Case mnUploadType
                        Case gnUploadTransferRegister
                            Call lobjUpload.TRUpload(lnUploadId)
                        Case gnUploadCertRegister
                            Call lobjUpload.CRUpload(lnUploadId)
                        Case gnUploadObjxRegister
                            Call lobjUpload.ORUpload(lnUploadId)
                        Case gnUploadCDSLUpload
                            'Call lobjUpload.CDSLUploadold(lnUploadId)
                            Call lobjUpload.CDSLUpload(lnUploadId)
                        Case gnUploadNSDLUpload
                            Call lobjUpload.NSDLUpload(lnUploadId)
                        Case gnAllotmentCDSL
                            Call lobjUpload.CDSLAllotment(lnUploadId)
                        Case gnAllotmentNSDL
                            Call lobjUpload.NSDLAllotment(lnUploadId)
                        Case gnPhyToiepfCDSL
                            Call lobjUpload.CDSLPhysToIEPF(lnUploadId)
                        Case gnPhyToiepfNSDL
                            Call lobjUpload.NSDLPhysToIEPF(lnUploadId)
                    End Select

                    Me.Close()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim ds As New DataSet
        Dim lnInwardId As Long
        Dim frm As frmDocHistory

        Dim lsSql As String
        Dim lsTxt As String

        If e.RowIndex >= 0 Then
            lnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value

            Select Case e.ColumnIndex
                Case dgvList.ColumnCount - 2
                    frm = New frmDocHistory(lnInwardId)
                    frm.ShowDialog()
                Case dgvList.ColumnCount - 1
                    If MessageBox.Show("Are you sure to send back ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If

                    Do
                        lsTxt = InputBox("Remark", "Send Back Remark")
                    Loop Until lsTxt <> ""

                    lsSql = ""
                    lsSql &= " select "
                    lsSql &= " q.queue_to,"
                    lsSql &= " q.action_status,"
                    lsSql &= " i.chklst_disc,"
                    lsSql &= " i.upload_gid,"
                    lsSql &= " i.outward_gid "
                    lsSql &= " from sta_trn_tinward as i "
                    lsSql &= " inner join sta_trn_tqueue as q on i.queue_gid = q.queue_gid and q.delete_flag = 'N' "
                    lsSql &= " where i.inward_gid = " & lnInwardId & " "
                    lsSql &= " and i.delete_flag = 'N' "

                    Call gpDataSet(lsSql, "queue", gOdbcConn, ds)

                    With ds.Tables("queue")
                        If ds.Tables("queue").Rows.Count > 0 Then
                            If .Rows(0).Item("upload_gid") = 0 And .Rows(0).Item("action_status") = 0 And _
                               .Rows(0).Item("queue_to") = "U" Then

                                Call UpdateQueue(lnInwardId, "U", lsTxt, gnQueueReject)

                                Call LoadGrid()
                            Else
                                MessageBox.Show("Access denied !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End If

                        ds.Tables("queue").Rows.Clear()
                    End With
            End Select
        End If
    End Sub
End Class