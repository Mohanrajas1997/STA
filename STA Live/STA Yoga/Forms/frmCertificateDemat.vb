Imports MySql.Data.MySqlClient

Public Class frmCertificateDemat
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
    Dim msDocRcvdDate As String = ""
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
                dgvReason.ReadOnly = True

                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False
                btnUpdateFolioPan.Visible = False
                txtRemark.Enabled = False
            Case Else
                dgvCert.ReadOnly = True
                dgvReason.ReadOnly = True
                dgvChklst.ReadOnly = True

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
        Dim r As Integer
        Dim lsDepositoryCode As String = ""
        Dim lsDematRejectCode As String = ""
        Dim lsNameChangeFlag As String = ""
        Dim lsNameChangeCode As String = ""
        Dim lnChkLstValid As Integer
        Dim lnChkLstDisc As Integer

        Dim lnCertEntryFlag As Integer = 0
        Dim lnTotShares As Long = 0
        Dim lsRejectCode() As String
        Dim lsNameChageCodeS() As String

        cmd = New MySqlCommand("pr_sta_get_certdematentry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_queue_gid", mnQueueId)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                mnCompId = .Rows(0).Item("comp_gid")
                msCompName = .Rows(0).Item("comp_name").ToString

                mnFolioId = .Rows(0).Item("folio_gid")
                msDocRcvdDate = Format(.Rows(0).Item("received_date"), "yyyy-MM-dd")

                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolder.Text = .Rows(0).Item("shareholder_name").ToString
                txtPanNo.Text = .Rows(0).Item("shareholder_pan_no").ToString

                lsDepositoryCode = .Rows(0).Item("depository_code").ToString
                lsDematRejectCode = .Rows(0).Item("dematpend_reject_code").ToString

                lsNameChangeFlag = .Rows(0).Item("namechangeref_flag").ToString
                lsNameChangeCode = .Rows(0).Item("namechageref_code").ToString
                txtIsinId.Text = .Rows(0).Item("isin_id").ToString()
                txtDematPendId.Text = .Rows(0).Item("dematpend_gid").ToString()
                txtDrnNo.Text = .Rows(0).Item("drn_no").ToString()
                txtDepository.Text = .Rows(0).Item("depository_name").ToString()

                If .Rows(0).Item("drn_req_date").ToString <> "" Then
                    txtReqDate.Text = Format(.Rows(0).Item("drn_req_date"), "dd-MM-yyyy")
                Else
                    txtReqDate.Text = ""
                End If

                txtNameElect.Text = .Rows(0).Item("drn_cust_name").ToString
                txtJoint1Name.Text = .Rows(0).Item("drn_joint1_name").ToString
                txtJoint2Name.Text = .Rows(0).Item("drn_joint2_name").ToString

                txtDpId.Text = .Rows(0).Item("dp_id").ToString()
                txtClientid.Text = .Rows(0).Item("client_id").ToString()
                If msGroupCode <> "M" Then
                    txtInputClientid.Text = txtClientid.Text
                End If
                txtSharesDrn.Text = .Rows(0).Item("drn_share_count").ToString

                lnChkLstValid = .Rows(0).Item("chklst_valid")
                lnChkLstDisc = .Rows(0).Item("chklst_disc")

                mnChklstValid = lnChkLstValid
            Else
                Call frmCtrClear(Me)
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()

        If lsNameChangeFlag = "Y" Then
            ChkNameChng.Checked = True
        Else
            ChkNameChng.Checked = False
        End If



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

                If (lnCertEntryFlag = 1) Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If

                If .Rows(i).Cells("cert_status").Value <> gnCertActive Then
                    .Rows(i).ReadOnly = True
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
            .Columns("Check List").Width = 320

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

            mnChklstValid = lnChkLstValid

            If mnChkLstAllStatus = lnChkLstValid And Val(lblShareSelected.Text) = Val(txtSharesDrn.Text) And txtClientid.Text = txtInputClientid.Text Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
            End If
        End With

        ' load reject reason
        cmd = New MySqlCommand("pr_sta_get_dematrejectreason", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_depository_code", lsDepositoryCode)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvReason
            .DataSource = dt

            .Columns("depository_code").Visible = False

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

            lsRejectCode = lsDematRejectCode.Split(",")

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells(n + 1).Value = False

                If lsRejectCode.Contains(.Rows(i).Cells("Code").Value) = True Then
                    .FirstDisplayedScrollingRowIndex = i
                    .Rows(i).Cells(n + 1).Value = True
                End If
            Next i

            .Columns("Code").Width = 75
            .Columns("Rejection Reason").Width = 350
        End With

        'load Name Mismatch Reason
        cmd = New MySqlCommand("pr_sta_get_namechangeref", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_depository_code", lsDepositoryCode)
        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvNameChange
            .DataSource = dt
            .Columns("depository_code").Visible = False
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

            lsNameChageCodeS = lsNameChangeCode.Split(",")

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells("Select").Value = False

                If lsNameChageCodeS.Contains(.Rows(i).Cells("Code").Value) = True Then
                    .FirstDisplayedScrollingRowIndex = i
                    '.Rows(i).Cells(n + 1).Value = True
                    .Rows(i).Cells("Select").Value = True
                End If
            Next i

            .Columns("Code").Width = 75
            .Columns("Reason Description").Width = 350
        End With
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim lnResult As Long
        Dim i As Integer
        Dim n As Integer
        Dim lnChklstValue As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnNamelstValue As Long = 0
        Dim lnNamelstDis As Long = 0

        ' check list value
        With dgvChklst
            n = .Columns.Count - 1

            For i = 0 To .Rows.Count - 1
                lnChklstValue = .Rows(i).Cells("chklst_value").Value

                If .Rows(i).Cells(n).Value = True Then
                    lnChklstDisc = lnChklstDisc Or lnChklstValue
                End If
            Next i
        End With



        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            lnResult = DateDiff(DateInterval.Day, CDate(msDocRcvdDate), Now())

            If (lnResult > gnDematProcessDays Or lnResult < 0) And lnChklstDisc = 0 Then
                MessageBox.Show("Trying to process received date more than " & gnDematProcessDays.ToString & " day(s) document !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvChklst.Focus()
                Exit Sub
            End If



            If msGroupCode = "M" Then
                Call UpdateInformation(gnQueueSuccess)
            Else
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueSuccess)

                If lnResult = 1 Then Me.Close()
            End If
        End If
    End Sub

    Private Sub UpdateInformation(ActionStatus As Integer)
        Dim i As Integer
        Dim n As Integer

        Dim lnChklstValid As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnChklstValue As Long = 0

        Dim lnResult As Long
        Dim lsTxt As String

        Dim lsCertId As String = ""
        Dim lsNameCode As String = ""
        Dim lsDematRejectCode As String = ""
        Dim lnDematPendId As Long = 0

        Dim lsRemark As String = ""
        Dim lsNameChangeFlag As String = ""
        Dim lsClientId As String = ""

        Try
            lsRemark = QuoteFilter(txtRemark.Text)
            lsClientId = QuoteFilter(txtInputClientid.Text)


            If lsClientId = "" Then
                MessageBox.Show("Client id cannot be empty !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Exit Sub
            End If

            ' get certificate id
            lsCertId = ""

            With dgvCert
                For i = 0 To .RowCount - 1
                    If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                        lsCertId = lsCertId & .Rows(i).Cells("cert_gid").Value.ToString & ","
                    End If
                Next i
            End With


            If lsCertId = "" Then
                MessageBox.Show("Please select the certificate !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Exit Sub
            End If


            ' check Name change Reason value 
            'With dgvNameChange
            '    For i = 0 To .RowCount - 1
            '        'If lsNameCode = "" Then
            '        If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
            '            lsNameCode = .Rows(i).Cells("Code").Value
            '            'End If
            '        End If
            '    Next i
            'End With

            If ChkNameChng.CheckState = 1 Then
                ' check Name change Reason value
                With dgvNameChange
                    n = .Columns.Count
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Cells("Select").Value = True Then
                            Dim selectvalues As String
                            selectvalues = .Rows(i).Cells(n - 4).Value
                            'If lsNameCode = "" Then
                            lsNameCode = .Rows(i).Cells("Code").Value
                            'End If
                        End If
                    Next i
                End With
            End If

            If ChkNameChng.CheckState = 1 Then
                If lsNameCode = "" Then
                    MessageBox.Show("Please select the Name Change Reason!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dgvNameChange.Focus()
                    Exit Sub
                End If
            End If

            If ChkNameChng.CheckState = 1 Then
                lsNameChangeFlag = "Y"
            Else
                lsNameChangeFlag = "N"
            End If

            lnDematPendId = Val(txtDematPendId.Text)

            If lnDematPendId = 0 Then
                MessageBox.Show("DRN No cannot be empty !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDrnNo.Focus()
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

            If Val(lblShareSelected.Text) <> Val(txtSharesDrn.Text) Then
                If lnChklstDisc = 0 Then
                    If MessageBox.Show("Shares selected and electronic not matched ! Are you sure to continue ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        txtDrnNo.Focus()
                        Exit Sub
                    End If
                Else
                    If MessageBox.Show("Shares selected and electronic not matched ! Are you sure to continue ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        txtDrnNo.Focus()
                        Exit Sub
                    End If
                End If
            End If

            lsDematRejectCode = ""

            If lnChklstDisc > 0 Then
                With dgvReason
                    n = .Columns.Count

                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Cells(n - 1).Value = True Then
                            If lsDematRejectCode = "" Then
                                lsDematRejectCode = .Rows(i).Cells("Code").Value
                            Else
                                lsDematRejectCode &= "," & .Rows(i).Cells("Code").Value
                            End If
                        End If
                    Next i
                End With

                If lsDematRejectCode = "" Then
                    MessageBox.Show("Please select the rejection code !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dgvReason.Focus()
                    Exit Sub
                End If
            End If

            Using cmd As New MySqlCommand("pr_sta_set_certentrydemat", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_cert_gid", lsCertId)
                cmd.Parameters.AddWithValue("?in_src_folio_gid", mnFolioId)
                cmd.Parameters.AddWithValue("?in_dematpend_gid", lnDematPendId)
                cmd.Parameters.AddWithValue("?in_dematpend_reject_code", lsDematRejectCode)

                cmd.Parameters.AddWithValue("?in_chklst_valid", lnChklstValid)
                cmd.Parameters.AddWithValue("?in_chklst_disc", lnChklstDisc)
                cmd.Parameters.AddWithValue("?in_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_action_status", ActionStatus)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
                cmd.Parameters.AddWithValue("?in_namechangeref_flag", lsNameChangeFlag)
                cmd.Parameters.AddWithValue("?in_namechageref_code", lsNameCode)
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

                        mnChklstValid = lnChklstValid

                        If mnChkLstAllStatus = lnChklstValid And Val(lblShareSelected.Text) = Val(txtSharesDrn.Text) And txtClientid.Text = txtInputClientid.Text Then
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

    Private Sub RefreshShareCount()
        Dim i As Integer
        Dim lnShareCount As Long = 0

        With dgvCert
            For i = 0 To .RowCount - 1
                If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                    lnShareCount += .Rows(i).Cells("Share Count").Value
                End If
            Next i

            lblShareSelected.Text = lnShareCount.ToString()

            If mnChkLstAllStatus = mnChklstValid And Val(lblShareSelected.Text) = Val(txtSharesDrn.Text) And txtClientid.Text = txtInputClientid.Text Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
            End If
        End With
    End Sub

    Private Sub txtDrnNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDrnNo.Validating
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lobjChkBoxColumn As DataGridViewCheckBoxColumn
        Dim lobjChkBoxColumnNmae As DataGridViewCheckBoxColumn
        Dim lsDepositoryCode As String = ""
        Dim r As Integer
        Dim n As Integer

        cmd = New MySqlCommand("pr_sta_get_dematdrnno", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_drn_no", txtDrnNo.Text)
        cmd.Parameters.AddWithValue("?in_isin_id", txtIsinId.Text)
        cmd.Parameters.AddWithValue("?in_dp_id", txtDpId.Text)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        If dt.Rows.Count = 1 Then
            txtDematPendId.Text = dt.Rows(0).Item("dematpend_gid").ToString()
            txtDepository.Text = dt.Rows(0).Item("depository_name").ToString()
            lsDepositoryCode = dt.Rows(0).Item("depository_code").ToString

            If dt.Rows(0).Item("req_date").ToString <> "" Then
                txtReqDate.Text = Format(dt.Rows(0).Item("req_date"), "dd-MM-yyyy")
            Else
                txtReqDate.Text = ""
            End If

            txtNameElect.Text = dt.Rows(0).Item("cust_name").ToString
            txtJoint1Name.Text = dt.Rows(0).Item("joint1_name").ToString
            txtJoint2Name.Text = dt.Rows(0).Item("joint2_name").ToString

            txtDpId.Text = dt.Rows(0).Item("dp_id").ToString()
            txtClientid.Text = dt.Rows(0).Item("client_id").ToString()
            txtSharesDrn.Text = dt.Rows(0).Item("share_count").ToString

            If msGroupCode <> "M" Then
                txtInputClientid.Text = txtClientid.Text
            End If

        ElseIf dt.Rows.Count > 1 Then
            MessageBox.Show("DRN No duplicated ! Please input DP ID as additional input for validation !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDpId.Focus()
        Else
            MessageBox.Show("Invalid DRN No ! Please clear DP ID value (If available) !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        ' load reject reason
        cmd = New MySqlCommand("pr_sta_get_dematrejectreason", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_depository_code", lsDepositoryCode)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvReason
            .Columns.Clear()

            .DataSource = dt

            .Columns("depository_code").Visible = False

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            r = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Select"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Select"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells(r + 1).Value = False
            Next i

            .Columns("Code").Width = 75
            .Columns("Rejection Reason").Width = 350
        End With

        ' load name change reason
        cmd = New MySqlCommand("pr_sta_get_namechangeref", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_depository_code", lsDepositoryCode)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvNameChange
            .Columns.Clear()

            .DataSource = dt

            .Columns("depository_code").Visible = False

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumnNmae = New DataGridViewCheckBoxColumn
            lobjChkBoxColumnNmae.HeaderText = "Select"
            lobjChkBoxColumnNmae.Width = 50
            lobjChkBoxColumnNmae.Name = "Select"
            lobjChkBoxColumnNmae.Selected = False

            .Columns.Add(lobjChkBoxColumnNmae)

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells(n + 1).Value = False
            Next i

            .Columns("Code").Width = 75
            .Columns("Reason Description").Width = 350
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

    Private Sub btnUpdateFolioPan_Click(sender As Object, e As EventArgs) Handles btnUpdateFolioPan.Click
        Dim frm As New frmFolioPanUpdate(mnFolioId)
        frm.ShowDialog()
    End Sub

    Private Sub txtInputClientid_Leave(sender As Object, e As EventArgs) Handles txtInputClientid.Leave
        Dim mst_client_id As String = ""
        Dim input_client_id As String = ""

        mst_client_id = txtClientid.Text.ToString().Trim()
        input_client_id = txtInputClientid.Text.ToString().Trim()

        If mst_client_id = "" Then
            MessageBox.Show("Client Id Cannot be blank..!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtInputClientid.Focus()
            'ElseIf mst_client_id <> input_client_id Then
            '    MessageBox.Show("Drn Client Id & Entered Client Id Missmatch..!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    txtInputClientid.Focus()
        End If
    End Sub

    Private Sub txtInputClientid_TextChanged(sender As Object, e As EventArgs) Handles txtInputClientid.TextChanged
        Call RefreshShareCount()
    End Sub
End Class