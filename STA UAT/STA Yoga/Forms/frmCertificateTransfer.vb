Imports MySql.Data.MySqlClient

Public Class frmCertificateTransfer
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
                dgvChklst.ReadOnly = True

                btnUpdateFolioPan.Visible = False
                btnSearchFolio.Visible = False
                btnCreateNewFolio.Visible = False

                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False

                txtRemark.Enabled = False
            Case Else
                txtAddr1.Enabled = False
                txtAddr2.Enabled = False
                txtAddr3.Enabled = False
                txtCity.Enabled = False
                txtState.Enabled = False
                txtCountry.Enabled = False
                txtPincode.Enabled = False
                txtShareCount.Enabled = False
                txtShareValue.Enabled = False
                txtConsider.Enabled = False
                txtStampDuty.Enabled = False

                dgvCert.ReadOnly = True
                dgvChklst.ReadOnly = True

                grpName.Enabled = False
                lnkAddAttachment.Visible = False
                btnUpdateFolioPan.Visible = False
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
                txtShareHolder.Text = .Rows(0).Item("shareholder_name").ToString
                txtPanNo.Text = .Rows(0).Item("shareholder_pan_no").ToString

                txtNewFolioId.Text = .Rows(0).Item("tran_folio_gid")
                txtNewFolioNo.Text = .Rows(0).Item("tran_folio_no").ToString

                txtShareCount.Text = .Rows(0).Item("share_count").ToString
                txtShareValue.Text = .Rows(0).Item("share_value").ToString
                txtConsider.Text = .Rows(0).Item("cons_amount").ToString
                txtStampDuty.Text = .Rows(0).Item("stamp_duty").ToString

                If Not IsDBNull(.Rows(0).Item("execution_date")) Then
                    dtpExecDate.Value = .Rows(0).Item("execution_date")
                Else
                    dtpExecDate.Value = Now
                End If

                txtWitnessName.Text = .Rows(0).Item("witness_name").ToString
                txtWitnessAddr.Text = .Rows(0).Item("witness_addr").ToString

                txtHolder1.Text = .Rows(0).Item("holder1_name").ToString
                txtFHName1.Text = .Rows(0).Item("holder1_fh_name").ToString
                txtPanNo1.Text = .Rows(0).Item("holder1_pan_no").ToString

                txtHolder2.Text = .Rows(0).Item("holder2_name").ToString
                txtFHName2.Text = .Rows(0).Item("holder2_fh_name").ToString
                txtPanNo2.Text = .Rows(0).Item("holder2_pan_no").ToString

                txtHolder3.Text = .Rows(0).Item("holder3_name").ToString
                txtFHName3.Text = .Rows(0).Item("holder3_fh_name").ToString
                txtPanNo3.Text = .Rows(0).Item("holder3_pan_no").ToString

                txtAddr1.Text = .Rows(0).Item("folio_addr1").ToString
                txtAddr2.Text = .Rows(0).Item("folio_addr2").ToString
                txtAddr3.Text = .Rows(0).Item("folio_addr3").ToString
                txtCity.Text = .Rows(0).Item("folio_city").ToString
                txtState.Text = .Rows(0).Item("folio_state").ToString
                txtCountry.Text = .Rows(0).Item("folio_country").ToString
                txtPincode.Text = .Rows(0).Item("folio_pincode").ToString

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

        Dim lsRemark As String = ""

        Dim lsExecDate As String = ""
        Dim lsWitnessName As String = ""
        Dim lsWitnessAddr As String = ""

        Dim lnShares As Long = 0
        Dim lnRate As Double = 0
        Dim lnCons As Double = 0
        Dim lnStampDuty As Double = 0

        Try
            lnNewFolioId = Val(txtNewFolioId.Text)

            lsRemark = QuoteFilter(txtRemark.Text)

            lsExecDate = Format(dtpExecDate.Value, "yyyy-MM-dd")
            lsWitnessName = QuoteFilter(txtWitnessName.Text)
            lsWitnessAddr = QuoteFilter(txtWitnessAddr.Text)

            lnShares = Val(txtShareCount.Text)
            lnRate = Math.Round(Val(txtShareValue.Text), 2)
            lnCons = Math.Round(Val(txtConsider.Text), 2)
            lnStampDuty = Math.Round(Val(txtStampDuty.Text), 2)

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

            Using cmd As New MySqlCommand("pr_sta_set_certentrytransfer", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_cert_gid", lsCertId)
                cmd.Parameters.AddWithValue("?in_src_folio_gid", mnFolioId)
                cmd.Parameters.AddWithValue("?in_new_folio_gid", lnNewFolioId)

                cmd.Parameters.AddWithValue("?in_exec_date", CDate(lsExecDate))
                cmd.Parameters.AddWithValue("?in_witness_name", lsWitnessName)
                cmd.Parameters.AddWithValue("?in_witness_addr", lsWitnessAddr)
                cmd.Parameters.AddWithValue("?in_share_count", lnShares)
                cmd.Parameters.AddWithValue("?in_share_value", lnRate)
                cmd.Parameters.AddWithValue("?in_cons_amount", lnCons)
                cmd.Parameters.AddWithValue("?in_stamp_duty", lnStampDuty)

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

    Private Sub NewFolioEnable(Status As Boolean)
        txtNewFolioNo.Enabled = Not Status
        btnSearchFolio.Enabled = Not Status

        txtHolder1.Enabled = Status
        txtFHName1.Enabled = Status
        txtPanNo1.Enabled = Status

        txtHolder2.Enabled = Status
        txtFHName2.Enabled = Status
        txtPanNo2.Enabled = Status

        txtHolder3.Enabled = Status
        txtFHName3.Enabled = Status
        txtPanNo3.Enabled = Status

        txtAddr1.Enabled = Status
        txtAddr2.Enabled = Status
        txtAddr3.Enabled = Status
        txtCity.Enabled = Status
        txtState.Enabled = Status
        txtCountry.Enabled = Status
        txtPincode.Enabled = Status
    End Sub

    Private Sub btnCreateNewFolio_Click(sender As Object, e As EventArgs) Handles btnCreateNewFolio.Click
        Dim frm As frmFolioCreate_new

        gnSearchId = 0

        frm = New frmFolioCreate_new(mnCompId, msCompName)
        frm.ShowDialog()

        If gnSearchId <> 0 Then
            Call LoadFolio(gnSearchId)
        End If
    End Sub

    Private Sub btnSearchFolio_Click(sender As Object, e As EventArgs) Handles btnSearchFolio.Click
        Dim frm As frmFolioSearch

        frm = New frmFolioSearch(mnCompId)
        frm.ShowDialog()

        If gnSearchId <> 0 Then
            Call LoadFolio(gnSearchId)
        End If
    End Sub

    Private Sub LoadFolio(FolioId As Long)
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_folio_gid", FolioId)
        cmd.Parameters.AddWithValue("?in_comp_gid", 0)
        cmd.Parameters.AddWithValue("?in_folio_no", "")

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                txtNewFolioId.Text = FolioId

                txtNewFolioNo.Text = .Rows(0).Item("folio_no").ToString

                txtHolder1.Text = .Rows(0).Item("holder1_name").ToString
                txtFHName1.Text = .Rows(0).Item("holder1_fh_name").ToString
                txtPanNo1.Text = .Rows(0).Item("holder1_pan_no").ToString

                txtHolder2.Text = .Rows(0).Item("holder2_name").ToString
                txtFHName2.Text = .Rows(0).Item("holder2_fh_name").ToString
                txtPanNo2.Text = .Rows(0).Item("holder2_pan_no").ToString

                txtHolder3.Text = .Rows(0).Item("holder3_name").ToString
                txtFHName3.Text = .Rows(0).Item("holder3_fh_name").ToString
                txtPanNo3.Text = .Rows(0).Item("holder3_pan_no").ToString

                txtAddr1.Text = .Rows(0).Item("folio_addr1").ToString
                txtAddr2.Text = .Rows(0).Item("folio_addr2").ToString
                txtAddr3.Text = .Rows(0).Item("folio_addr3").ToString
                txtCity.Text = .Rows(0).Item("folio_city").ToString
                txtState.Text = .Rows(0).Item("folio_state").ToString
                txtCountry.Text = .Rows(0).Item("folio_country").ToString
                txtPincode.Text = .Rows(0).Item("folio_pincode").ToString
            Else
                txtNewFolioId.Text = ""

                txtNewFolioNo.Text = ""

                txtHolder1.Text = ""
                txtFHName1.Text = ""
                txtPanNo1.Text = ""

                txtHolder2.Text = ""
                txtFHName2.Text = ""
                txtPanNo2.Text = ""

                txtHolder3.Text = ""
                txtFHName3.Text = ""
                txtPanNo3.Text = ""

                txtAddr1.Text = ""
                txtAddr2.Text = ""
                txtAddr3.Text = ""
                txtCity.Text = ""
                txtState.Text = ""
                txtCountry.Text = ""
                txtPincode.Text = ""
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()
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

            If txtShareCount.Enabled = True Then
                txtShareCount.Text = lnShareCount.ToString()
                txtConsider.Text = Format(lnShareCount * Val(txtShareValue.Text), "0.00")
            End If
        End With
    End Sub

    Private Sub btnUpdateFolioPan_Click(sender As Object, e As EventArgs) Handles btnUpdateFolioPan.Click
        Dim frm As New frmFolioPanUpdate(mnFolioId)
        frm.ShowDialog()
    End Sub

    Private Sub txtShareValue_TextChanged(sender As Object, e As EventArgs) Handles txtShareValue.TextChanged
        If txtShareValue.Enabled = True Then txtConsider.Text = Format(Val(txtShareCount.Text) * Val(txtShareValue.Text), "0.00")
    End Sub
End Class