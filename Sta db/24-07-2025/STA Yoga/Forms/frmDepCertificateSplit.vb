Imports MySql.Data.MySqlClient

Public Class frmDepCertificateSplit
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
                dgvCert.ReadOnly = True
                dgvCertDist.ReadOnly = True
                dgvChklst.ReadOnly = True

                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False

                txtRemark.Enabled = False
            Case Else
                dgvCert.ReadOnly = True
                dgvCertDist.ReadOnly = True
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
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtDepository.Text = .Rows(0).Item("shareholder_name").ToString

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
            .DataSource = dt

            .Columns("cert_gid").Visible = False
            .Columns("cert_status").Visible = False
            .Columns("Share Count").Width = 70
            .Columns("Dist Series").Width = 120
            .Columns("Status").Width = 70

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

        ' load distinctive series
        Call LoadDistinctiveSeries(0)
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

    Private Sub LoadDistinctiveSeries(CertId As Long)
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lobjChkBoxColumn As DataGridViewCheckBoxColumn
        Dim lnTotShares As Long = 0
        Dim i As Integer
        Dim n As Integer
        Dim lnCertDistEntryFlag As Integer = 0

        ' load certificate distinctive series
        cmd = New MySqlCommand("pr_sta_get_certdistentry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_cert_gid", CertId)

        'Out put Para
        cmd.Parameters.Add("?out_certdistentry_flag", MySqlDbType.Int32)
        cmd.Parameters("?out_certdistentry_flag").Direction = ParameterDirection.Output

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        lnCertDistEntryFlag = Val(cmd.Parameters("?out_certdistentry_flag").Value.ToString())

        With dgvCertDist
            .Columns.Clear()
            .DataSource = dt

            .Columns("certdist_gid").Visible = False
            .Columns("cert_gid").Visible = False
            .Columns("cert_status").Visible = False
            .Columns("Cert No").Width = 150
            .Columns("Dist From").Width = 150
            .Columns("Dist To").Width = 150
            .Columns("Shares").Width = 150

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
                lnTotShares += .Rows(i).Cells("Shares").Value

                If .Rows(i).Cells("cert_status").Value = gnCertInactive Then
                    .Rows(i).ReadOnly = True
                End If

                If (lnCertDistEntryFlag = 1) Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If
            Next i
        End With

        lblTotal.Text = lnTotShares.ToString

        Call RefreshShareCount()
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

        Dim lnCertId As Long = 0
        Dim lsCertDistId As String = ""

        Dim lsRemark As String = ""

        Try
            lsRemark = QuoteFilter(txtRemark.Text)

            If dgvCertDist.RowCount <= 1 Then
                MessageBox.Show("Certificate distinctive series should be more than one !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCertDist.Focus()
                Exit Sub
            End If

            If lblShareCount.Text = lblTotal.Text Then
                MessageBox.Show("All distinctive series are not allowed to transfer !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCertDist.Focus()
                Exit Sub
            End If

            ' get certificate id
            With dgvCert
                For i = 0 To .RowCount - 1
                    If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                        lnCertId = Val(.Rows(i).Cells("cert_gid").Value.ToString)
                    End If
                Next i
            End With

            If lnCertId = 0 Then
                MessageBox.Show("Please select the certificate !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Exit Sub
            End If

            ' get certdist gid
            With dgvCertDist
                For i = 0 To .RowCount - 1
                    If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                        lsCertDistId &= .Rows(i).Cells("certdist_gid").Value.ToString & ","
                    End If
                Next i
            End With

            If lsCertDistId = "" Then
                MessageBox.Show("Please select the certificate distinctive series !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Exit Sub
            End If

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

            Using cmd As New MySqlCommand("pr_sta_set_certentrydepsplit", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_cert_gid", lnCertId)
                cmd.Parameters.AddWithValue("?in_certdist_gid", lsCertDistId)

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
        Dim i As Long = 0
        Dim lnCertId As Long = 0

        With dgvCert
            .EndEdit()

            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1
                        For i = 0 To .Rows.Count - 1
                            If e.RowIndex = i Then
                                .Rows(i).Cells(e.ColumnIndex).Value = True
                                lnCertId = Val(.Rows(i).Cells("cert_gid").Value)
                            Else
                                .Rows(i).Cells(e.ColumnIndex).Value = False
                            End If
                        Next i

                        Call LoadDistinctiveSeries(lnCertId)
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

        With dgvCertDist
            For i = 0 To .RowCount - 1
                If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                    lnShareCount += .Rows(i).Cells("Shares").Value
                End If
            Next i

            lblShareCount.Text = lnShareCount.ToString()
        End With
    End Sub

    Private Sub dgvCertDist_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCertDist.CellContentClick
        With dgvCertDist
            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1

                        .EndEdit()

                        Call RefreshShareCount()
                End Select
            End If
        End With
    End Sub

    Private Sub grpHeader_Enter(sender As Object, e As EventArgs) Handles grpHeader.Enter

    End Sub
End Class