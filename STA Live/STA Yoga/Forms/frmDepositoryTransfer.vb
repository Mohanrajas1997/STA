Imports MySql.Data.MySqlClient

Public Class frmDepositoryTransfer
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnFolioId As Long
    Dim mnQueueId As Long
    Dim msTranCode As String
    Dim mnChklstValid As Long
    Dim mnChklstDisc As Long
    Dim mnChkLstAllStatus As Long = 0
    Dim mnChkLstSelected As Long = 0
    Dim msGroupCode As String = ""
    Dim msQueueCompleteCode As String = ""
#End Region

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmAddressChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsCurrQueue As String
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select b.queue_to from sta_trn_tinward as a "
        lsSql &= " inner join sta_trn_tqueue as b on b.queue_gid = a.queue_gid and b.delete_flag = 'N' "
        lsSql &= " where a.inward_gid = " & mnInwardId & " "
        lsSql &= " and a.delete_flag = 'N' "

        lsCurrQueue = gfExecuteScalar(lsSql, gOdbcConn)

        Select Case lsCurrQueue
            Case "I"
                btnSubmit.Text = "Inex"
                btnReject.Text = "Reprocess"
            Case "M"
                btnReject.Text = "Send Back"

                'btnReject.Text = "Inex"
                'btnReject.Visible = False
                'btnSubmit.Left = btnReject.Left
            Case "C"
                btnReject.Text = "Send Back"
            Case "U"
                btnSubmit.Visible = False
                btnReject.Text = "Send Back"
        End Select

        Call LoadData()
    End Sub

    Public Sub New(GroupCode As String, InwardId As Long, QueueId As Long, TranCode As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        msGroupCode = GroupCode
        mnInwardId = InwardId
        mnQueueId = QueueId
        msTranCode = TranCode

        Select Case GroupCode
            Case "M"
            Case "V"
                dtpTransferDate.Enabled = False
                dgvCert.ReadOnly = True
                dgvChklst.ReadOnly = True

                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False
                txtRemark.Enabled = False
            Case Else
                dtpTransferDate.Enabled = False
                dgvCert.ReadOnly = True
                dgvChklst.ReadOnly = True

                lnkAddAttachment.Visible = False
        End Select
    End Sub

    Private Sub LoadData()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lobjChkBoxColumn As DataGridViewCheckBoxColumn
        Dim i As Integer
        Dim n As Integer
        Dim lnChkLstValid As Integer
        Dim lnChkLstDisc As Integer

        Dim lnTotShares As Long = 0
        Dim lnCertEntryFlag As Integer = 0

        cmd = New MySqlCommand("pr_sta_get_certtranentry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_queue_gid", mnQueueId)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                If msGroupCode <> .Rows(0).Item("queue_to").ToString And msGroupCode <> "V" Then
                    MessageBox.Show("Document not in current queue !")
                    Exit Sub
                End If

                msQueueCompleteCode = .Rows(0).Item("complete_queue_code").ToString

                mnCompId = .Rows(0).Item("comp_gid")
                msCompName = .Rows(0).Item("comp_name").ToString

                mnFolioId = .Rows(0).Item("folio_gid")

                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFromFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtFromDepository.Text = .Rows(0).Item("shareholder_name").ToString
                txtShareCount.Text = .Rows(0).Item("share_count").ToString

                txtToFolioId.Text = .Rows(0).Item("tran_folio_gid")
                txtToFolioNo.Text = .Rows(0).Item("tran_folio_no").ToString
                txtToDep.Text = .Rows(0).Item("holder1_name").ToString

                If .Rows(0).Item("val_date").ToString = "" Then
                    dtpTransferDate.Value = Now
                Else
                    dtpTransferDate.Value = CDate(.Rows(0).Item("val_date").ToString)
                End If

                lnChkLstValid = .Rows(0).Item("chklst_valid")
                lnChkLstDisc = .Rows(0).Item("chklst_disc")
            Else
                Call frmCtrClear(Me)
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()

        ' load certificate
        cmd = New MySqlCommand("pr_sta_get_foliocertentrylist", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)

        'Out put Para
        cmd.Parameters.Add("?out_certentry_flag", MySqlDbType.Int32)
        cmd.Parameters("?out_certentry_flag").Direction = ParameterDirection.Output

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        lnCertEntryFlag = Val(cmd.Parameters("?out_certentry_flag").Value.ToString())

        With dgvCert
            If .Columns.Count > 0 Then
                .Columns.Clear()
            End If

            .DataSource = dt

            .Columns("cert_gid").Visible = False
            .Columns("cert_status").Visible = False

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Select"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Select"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            For i = 0 To .Rows.Count - 1
                lnTotShares += .Rows(i).Cells("Share Count").Value

                If .Rows(i).Cells("cert_status").Value = gnCertInactive Then
                    .Rows(i).ReadOnly = True
                End If

                If (lnCertEntryFlag = 1) Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If
            Next i
        End With

        lblTotal.Text = lnTotShares.ToString

        Call RefreshShareCount()

        ' load check list
        cmd = New MySqlCommand("pr_sta_get_checklist", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_tran_code", msTranCode)
        cmd.Parameters.AddWithValue("?in_auto_flag", "")

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvChklst
            If .Columns.Count > 0 Then
                .Columns.Clear()
            End If

            .DataSource = dt

            .Columns("chklst_gid").Visible = False
            .Columns("chklst_value").Visible = False
            .Columns("Check List").Width = 210

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Ok"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Ok"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Not Ok"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Not Ok"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            For i = 0 To .Rows.Count - 1
                mnChkLstAllStatus = mnChkLstAllStatus Or .Rows(i).Cells("chklst_value").Value

                If (lnChkLstValid And .Rows(i).Cells("chklst_value").Value) > 0 Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If

                If (lnChkLstDisc And .Rows(i).Cells("chklst_value").Value) > 0 Then
                    .Rows(i).Cells(n + 2).Value = True
                Else
                    .Rows(i).Cells(n + 2).Value = False
                End If
            Next i

            If mnChkLstAllStatus = lnChkLstValid Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
            End If
        End With
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim lnResult As Long

        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            If msGroupCode = "M" Then
                Call UpdateInformation(gnQueueSuccess)
            Else
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueSuccess)

                If lnResult = 1 Then Me.Close()
            End If
        End If
    End Sub

    Private Function UpdateQueueComplete(InwardId As Long, ApprDate As Date, Remark As String, ActionStatus As Integer) As Long
        Dim lnResult As Long = 0
        Dim lsTxt As String
        Dim lsRemark As String

        Try
            lsRemark = QuoteFilter(Remark)

            Using cmd As New MySqlCommand("pr_sta_set_certapprove", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", InwardId)
                cmd.Parameters.AddWithValue("?in_approved_date", ApprDate)
                cmd.Parameters.AddWithValue("?in_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_action_status", ActionStatus)
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

    Private Sub UpdateInformation(ActionStatus As Integer)
        Dim i As Integer
        Dim n As Integer

        Dim lnChklstValid As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnChklstValue As Long = 0

        Dim lnResult As Long
        Dim lsTxt As String

        Dim lsCertId As String = ""
        Dim lnNewFolioId As Long = 0
        Dim lsTransferDate As String = ""
        Dim lsRemark As String = ""

        Try
            lnNewFolioId = Val(txtToFolioId.Text)

            lsRemark = QuoteFilter(txtRemark.Text)

            ' get certificate id
            lsCertId = ""

            With dgvCert
                For i = 0 To .RowCount - 1
                    If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                        lsCertId &= .Rows(i).Cells("cert_gid").Value.ToString & ","
                    End If
                Next i
            End With

            If lsCertId = "" Then
                MessageBox.Show("Please select the certificate !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Exit Sub
            End If

            ' transfer date
            If DateDiff(DateInterval.Day, dtpTransferDate.Value, CDate(Now())) < 0 Then
                MessageBox.Show("Future transfer date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpTransferDate.Focus()
                Exit Sub
            End If

            lsTransferDate = Format(dtpTransferDate.Value, "yyyy-MM-dd")

            ' check list value
            With dgvChklst
                n = .Columns.Count - 1

                For i = 0 To .Rows.Count - 1
                    lnChklstValue = .Rows(i).Cells("chklst_value").Value
                    mnChkLstAllStatus = mnChkLstAllStatus Or lnChklstValue

                    If .Rows(i).Cells(n - 1).Value = True Then
                        lnChklstValid = lnChklstValid Or lnChklstValue
                    End If

                    If .Rows(i).Cells(n).Value = True Then
                        lnChklstDisc = lnChklstDisc Or lnChklstValue
                    End If
                Next i

                If mnChkLstAllStatus <> (lnChklstValid + lnChklstDisc) Then
                    MessageBox.Show("Please complete the check list !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End With

            Using cmd As New MySqlCommand("pr_sta_set_certentrydeptransfer", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_cert_gid", lsCertId)
                cmd.Parameters.AddWithValue("?in_src_folio_gid", mnFolioId)
                cmd.Parameters.AddWithValue("?in_new_folio_gid", lnNewFolioId)
                cmd.Parameters.AddWithValue("?in_transfer_date", lsTransferDate)

                cmd.Parameters.AddWithValue("?in_chklst_valid", lnChklstValid)
                cmd.Parameters.AddWithValue("?in_chklst_disc", lnChklstDisc)
                cmd.Parameters.AddWithValue("?in_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_action_status", ActionStatus)
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

    'Private Sub UpdateQueue(ActionStatus As Integer)
    '    Dim lnResult As Long
    '    Dim lsTxt As String
    '    Dim lsRemark As String

    '    Try
    '        lsRemark = QuoteFilter(txtRemark.Text)


    '        Using cmd As New MySqlCommand("pr_sta_set_queuemove", gOdbcConn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
    '            cmd.Parameters.AddWithValue("?in_remark", lsRemark)
    '            cmd.Parameters.AddWithValue("?in_action_status", ActionStatus)
    '            cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

    '            'Out put Para
    '            cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
    '            cmd.Parameters("?out_result").Direction = ParameterDirection.Output
    '            cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
    '            cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

    '            cmd.CommandTimeout = 0

    '            cmd.ExecuteNonQuery()

    '            lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
    '            lsTxt = cmd.Parameters("?out_msg").Value.ToString()

    '            If lnResult = 1 Then
    '                MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Else
    '                MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Sub
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Dim lnResult As Long

        If txtRemark.Text = "" Then
            MessageBox.Show("Remark cannot be empty !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRemark.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            If msGroupCode <> "I" Then
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueReject)
            Else
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueReprocess)
            End If

            If lnResult = 1 Then Me.Close()
        End If
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim frm As New frmDocHistory(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub lnkAddAttachment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAttachment.LinkClicked
        Dim frm As New frmAttachmentAdd(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub dgvChklst_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChklst.CellContentClick
        Dim i As Integer
        Dim lnChklstValue As Long
        Dim lnChklstValid As Long
        Dim lnChklstDisc As Long

        With dgvChklst
            If e.RowIndex >= 0 And dgvChklst.ReadOnly = False Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1, .Columns.Count - 2
                        .Rows(e.RowIndex).Cells(.Columns.Count - 1).Value = False
                        .Rows(e.RowIndex).Cells(.Columns.Count - 2).Value = False

                        .EndEdit()

                        For i = 0 To .Rows.Count - 1
                            lnChklstValue = .Rows(i).Cells("chklst_value").Value
                            mnChkLstAllStatus = mnChkLstAllStatus Or lnChklstValue

                            If .Rows(i).Cells(.Columns.Count - 2).Value = True Then
                                lnChklstValid = lnChklstValid Or lnChklstValue
                            Else
                                lnChklstDisc = lnChklstDisc Or lnChklstValue
                            End If
                        Next i

                        If mnChkLstAllStatus = lnChklstValid Then
                            lblDocStatus.Text = "Valid"
                            lblDocStatus.ForeColor = Color.DarkGreen
                        Else
                            lblDocStatus.Text = "Invalid"
                            lblDocStatus.ForeColor = Color.Red
                        End If
                End Select

            End If
        End With
    End Sub

    Private Sub dgvCert_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCert.CellContentClick
        Dim lnCertId As Long = 0
        Dim lsTxt As String = ""

        With dgvCert
            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1
                        .EndEdit()

                        lnCertId = .Rows(e.RowIndex).Cells("cert_gid").Value

                        If .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
                            lsTxt = GetInwardCertificate(mnInwardId, lnCertId)

                            If lsTxt <> "" Then
                                .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
                                MessageBox.Show("Certificate already mapped with Inward : " & lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If

                        Call RefreshShareCount()
                End Select
            End If
        End With
    End Sub

    Private Sub txtCertNo_TextChanged(sender As Object, e As EventArgs) Handles txtCertNo.TextChanged
        Dim i As Integer

        With dgvCert
            If txtCertNo.Text <> "" Then
                .ClearSelection()

                For i = 0 To .Rows.Count - 1
                    If InStr(.Rows(i).Cells("Certificate No").Value, txtCertNo.Text) > 0 Then
                        .FirstDisplayedScrollingRowIndex = i
                        .Rows(i).Selected = True
                        Exit For
                    End If
                Next i
            End If
        End With
    End Sub

    Private Sub RefreshShareCount()
        Dim i As Integer
        Dim lnShareCount As Long = 0

        With dgvCert
            For i = 0 To .RowCount - 1
                If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                    lnShareCount += .Rows(i).Cells("Share Count").Value
                End If
            Next i

            lblShareCount.Text = lnShareCount.ToString()
        End With
    End Sub

    Private Function AutoSplitCert(ShareCount As Long) As Integer
        Dim lnCertId As Long = 0
        Dim lnCertDistId As Long = 0
        Dim lnNewCertId As Long = 0
        Dim lnCompId As Long = 0
        Dim lsCertNo As String = ""
        Dim lnNewCertSubNo As Long = 0
        Dim lnOldShareCount As Long = 0
        Dim lnNewShareCount As Long = 0

        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lnResult As Long = 0

        If dgvCert.RowCount = 0 Then
            ' check dist serious
            lsSql = ""
            lsSql &= " select a.cert_gid,a.cert_no,a.cert_sub_no,b.certdist_gid,a.share_count "
            lsSql &= " from sta_trn_tcert as a "
            lsSql &= " inner join sta_trn_tcertdist as b on a.cert_gid = b.cert_gid and b.delete_flag = 'N' "
            lsSql &= " where a.folio_gid = " & mnFolioId & " "
            lsSql &= " and a.cert_status <> " & gnCertInactive & " "
            lsSql &= " and b.dist_count = " & ShareCount & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.cert_gid,b.certdist_gid "
            lsSql &= " limit 0,1"

            Call gpDataSet(lsSql, "certdist", gOdbcConn, ds)

            With ds.Tables("certdist")
                If .Rows.Count > 0 Then
                    lnCertId = .Rows(0).Item("cert_gid")
                    lsCertNo = QuoteFilter(.Rows(0).Item("cert_no").ToString)
                    lnCertDistId = .Rows(0).Item("certdist_gid")
                    lnOldShareCount = .Rows(0).Item("share_count")

                    ' get certificate new sub no
                    lsSql = ""
                    lsSql &= " select max(cert_sub_no) from sta_trn_tcert "
                    lsSql &= " where folio_gid = " & mnFolioId & " "
                    lsSql &= " and cert_no = " & lsCertNo & " "
                    lsSql &= " and delete_flag = 'N' "

                    lnNewCertSubNo = Val(gfExecuteScalar(lsSql, gOdbcConn)) + 1

                    ' certificate split
                    lsSql = ""
                    lsSql &= " insert into sta_trn_tcert ("
                    lsSql &= " comp_gid,folio_gid,cert_no,cert_sub_no,issue_date,expired_date,share_count,"
                    lsSql &= " lockin_period_from,lockin_period_to,hold_date,hold_release_date,cert_status,cert_remark) "
                    lsSql &= " select "
                    lsSql &= " comp_gid,folio_gid,cert_no," & lnNewCertSubNo.ToString & ",issue_date,expired_date," & ShareCount.ToString & ","
                    lsSql &= " lockin_period_from,lockin_period_to,hold_date,hold_release_date,cert_status,cert_remark "
                    lsSql &= " from sta_trn_tcert "
                    lsSql &= " where cert_gid = " & lnCertId & " "
                    lsSql &= " and delete_flag = 'N'"

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    ' get new certificate id
                    lsSql = ""
                    lsSql &= " select cert_gid from sta_trn_tcert "
                    lsSql &= " where folio_gid = " & mnFolioId & " "
                    lsSql &= " and cert_no = " & lsCertNo & " "
                    lsSql &= " and cert_sub_no = " & lnNewCertSubNo.ToString & " "
                    lsSql &= " and delete_flag = 'N' "

                    lnNewCertId = Val(gfExecuteScalar(lsSql, gOdbcConn))

                    ' update old cert share count
                    lsSql = ""
                    lsSql &= " update sta_trn_tcert set "
                    lsSql &= " share_count = share_count - " & ShareCount.ToString & " "
                    lsSql &= " where cert_gid = " & lnCertId & " "
                    lsSql &= " and delete_flag = 'N' "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    ' redirect dist series to new cert
                    lsSql = ""
                    lsSql &= " update sta_trn_tcertdist set "
                    lsSql &= " cert_gid = " & lnNewCertId & " "
                    lsSql &= " where certidist_gid = " & lnCertDistId & " "
                    lsSql &= " and delete_flag = 'N' "

                    .Rows.Clear()

                    Return 1
                Else
                    lnResult = AutoSplitCertDist(ShareCount)

                    If lnResult = 1 Then Return AutoSplitCert(ShareCount)
                End If
            End With
        End If

        Return 0
    End Function

    Private Function AutoSplitCertDist(ShareCount As Long) As Integer
        Dim lnCertId As Long = 0
        Dim lnCertDistId As Long = 0
        Dim lnNewCertDistId1 As Long = 0
        Dim lnNewCertDistId2 As Long = 0
        Dim lnNewCertId As Long = 0
        Dim lnCompId As Long = 0
        Dim lnDistFrom As Long = 0
        Dim lnDistTo As Long = 0
        Dim lnDistShareCount As Long = 0
        Dim lnNewDistFrom1 As Long = 0
        Dim lnNewDistTo1 As Long = 0
        Dim lnNewDistShareCount1 As Long = 0
        Dim lnNewDistFrom2 As Long = 0
        Dim lnNewDistTo2 As Long = 0
        Dim lnNewDistShareCount2 As Long = 0

        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lnResult As Long = 0

        If dgvCert.RowCount = 0 Then
            ' check dist serious
            lsSql = ""
            lsSql &= " select a.cert_gid,b.certdist_gid,b.dist_from,b.dist_to,b.dist_count "
            lsSql &= " from sta_trn_tcert as a "
            lsSql &= " inner join sta_trn_tcertdist as b on a.cert_gid = b.cert_gid and b.delete_flag = 'N' "
            lsSql &= " where a.folio_gid = " & mnFolioId & " "
            lsSql &= " and a.cert_status <> " & gnCertInactive & " "
            lsSql &= " and b.dist_count = " & ShareCount & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.cert_gid,b.certdist_gid "
            lsSql &= " limit 0,1"

            Call gpDataSet(lsSql, "certdist", gOdbcConn, ds)

            If ds.Tables("certdist").Rows.Count = 0 Then
                lsSql = ""
                lsSql &= " select a.cert_gid,b.certdist_gid,b.dist_from,b.dist_to,b.dist_count "
                lsSql &= " from sta_trn_tcert as a "
                lsSql &= " inner join sta_trn_tcertdist as b on a.cert_gid = b.cert_gid and b.delete_flag = 'N' "
                lsSql &= " where a.folio_gid = " & mnFolioId & " "
                lsSql &= " and a.cert_status <> " & gnCertInactive & " "
                lsSql &= " and b.dist_count > " & ShareCount & " "
                lsSql &= " and a.delete_flag = 'N' "
                lsSql &= " order by b.dist_count,a.cert_gid,b.certdist_gid "
                lsSql &= " limit 0,1"

                Call gpDataSet(lsSql, "certdist", gOdbcConn, ds)

                With ds.Tables("certdist")
                    If .Rows.Count = 1 Then
                        lnCertId = .Rows(0).Item("cert_gid")
                        lnCertDistId = .Rows(0).Item("certdist_gid")
                        lnDistShareCount = .Rows(0).Item("dist_count")
                        lnDistFrom = .Rows(0).Item("dist_from")
                        lnDistTo = .Rows(0).Item("dist_to")

                        lnNewDistShareCount1 = ShareCount
                        lnNewDistShareCount2 = lnDistShareCount - ShareCount

                        lnNewDistFrom1 = lnDistFrom
                        lnNewDistTo1 = lnNewDistFrom1 + lnNewDistShareCount1 - 1

                        lnNewDistFrom2 = lnNewDistTo1 + 1
                        lnNewDistTo2 = lnNewDistFrom2 + lnNewDistShareCount2 - 1

                        ' soft delete old dist series
                        lsSql = ""
                        lsSql &= " update sta_trn_tcertdist set "
                        lsSql &= " delete_flag = 'S' "
                        lsSql &= " where certdist_gid = " & lnCertDistId & " "
                        lsSql &= " and delete_flag = 'N' "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        ' split1
                        lsSql = ""
                        lsSql &= " insert into sta_trn_tcertdist "
                        lsSql &= " (cert_gid,dist_from,dist_to,dist_count) values ("
                        lsSql &= " " & lnCertId & ","
                        lsSql &= " " & lnNewDistFrom1 & ","
                        lsSql &= " " & lnNewDistTo1 & ","
                        lsSql &= " " & lnNewDistShareCount1 & ")"

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        ' split2
                        lsSql = ""
                        lsSql &= " insert into sta_trn_tcertdist "
                        lsSql &= " (cert_gid,dist_from,dist_to,dist_count) values ("
                        lsSql &= " " & lnCertId & ","
                        lsSql &= " " & lnNewDistFrom2 & ","
                        lsSql &= " " & lnNewDistTo2 & ","
                        lsSql &= " " & lnNewDistShareCount2 & ")"

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        .Rows.Clear()

                        Return 1
                    End If
                End With
            End If
        End If

        Return 0
    End Function

    Private Sub btnAutoSplit_Click(sender As Object, e As EventArgs) Handles btnAutoSplit.Click
        Dim lnResult As Long = 0

        If dgvCert.RowCount = 0 Then
            If MessageBox.Show("Are you sure to split ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            lnResult = AutoSplitCert(Val(txtShareCount.Text))

            If lnResult = 1 Then
                Call LoadData()
                MessageBox.Show("Share splited successfully !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub
End Class