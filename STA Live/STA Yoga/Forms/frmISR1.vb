Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmISR1
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnQueueId As Long
    Dim mnFileName As String
    Dim mnSignatureId As Long
    Dim msTranCode As String
    Dim mnChklstValid As Long
    Dim mnChklstDisc As Long
    Dim mnChkLstAllStatus As Long = 0
    Dim mnChkLstSelected As Long = 0
    Dim msGroupCode As String = ""
    Dim lsCurrQueue As String = ""

#End Region

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmISRChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsCurrQueue As String
        Dim lsSql As String

        ' bank
        lsSql = ""
        lsSql &= " select * from sta_mst_tbank "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by bank_name asc "

        Call gpBindCombo(lsSql, "bank_name", "bank_code", cboNewBank, gOdbcConn)

        ' bank a/c type
        lsSql = ""
        lsSql &= " select * from sta_mst_tbankacctype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by bankacctype_name asc "

        Call gpBindCombo(lsSql, "bankacctype_name", "bankacctype_code", cboNewAccType, gOdbcConn)

        ' Relationship
        lsSql = ""
        lsSql &= " select relationship_gid,relationship_name from sta_mst_trelationship "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by relationship_name asc "

        Call gpBindCombo(lsSql, "relationship_name", "relationship_gid", cmbNewRelation, gOdbcConn)

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
                dgvChklst.Enabled = False
                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False
                grpPropAddr.Enabled = False
                grpPropBank.Enabled = False
                grpPropPanSign.Enabled = False
                grpPropISR3.Enabled = False
                btnBrowse.Enabled = False
                txtRemark.Enabled = False
            Case Else
                grpPropAddr.Enabled = False
                grpPropBank.Enabled = False
                grpPropPanSign.Enabled = False
                grpPropISR3.Enabled = False
                dgvChklst.Enabled = False
                btnBrowse.Enabled = False
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

        Dim lnSignatureId As Long
        Dim lsFileName As String
        Dim lsSrcFile As String
        Dim lsDestFile As String

        cmd = New MySqlCommand("pr_sta_get_folioisr", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_queue_gid", mnQueueId)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtHolder1.Text = .Rows(0).Item("holder1_name").ToString
                txtHolder2.Text = .Rows(0).Item("holder2_name").ToString
                txtHolder3.Text = .Rows(0).Item("holder3_name").ToString
                'Address current
                txtCurrAddr1.Text = .Rows(0).Item("curr_addr1").ToString
                txtCurrAddr2.Text = .Rows(0).Item("curr_addr2").ToString
                txtCurrAddr3.Text = .Rows(0).Item("curr_addr3").ToString
                txtCurrCity.Text = .Rows(0).Item("curr_city").ToString
                txtCurrState.Text = .Rows(0).Item("curr_state").ToString
                txtCurrCountry.Text = .Rows(0).Item("curr_country").ToString
                txtCurrPincode.Text = .Rows(0).Item("curr_pincode").ToString
                txtCurrPhoneNo.Text = .Rows(0).Item("curr_contact_no").ToString
                txtCurrEmailId.Text = .Rows(0).Item("curr_mail_id").ToString
                'Address new
                txtNewAddr1.Text = .Rows(0).Item("new_addr1").ToString
                txtNewAddr2.Text = .Rows(0).Item("new_addr2").ToString
                txtNewAddr3.Text = .Rows(0).Item("new_addr3").ToString
                txtNewCity.Text = .Rows(0).Item("new_city").ToString
                txtNewState.Text = .Rows(0).Item("new_state").ToString
                txtNewCountry.Text = .Rows(0).Item("new_country").ToString
                txtNewPincode.Text = .Rows(0).Item("new_pincode").ToString
                txtNewPhoneNo.Text = .Rows(0).Item("new_contact_no").ToString
                txtNewEmailId.Text = .Rows(0).Item("new_mail_id").ToString
                'Bank Current
                txtCurrBeneficiary.Text = .Rows(0).Item("curr_beneficiary").ToString
                txtCurrBank.Text = .Rows(0).Item("curr_bank_name").ToString
                txtCurrBranch.Text = .Rows(0).Item("curr_branch").ToString
                txtCurrAddr.Text = .Rows(0).Item("curr_addr").ToString
                txtCurrIfscCode.Text = .Rows(0).Item("curr_ifsc_code").ToString
                txtCurrAccType.Text = .Rows(0).Item("curr_acc_type").ToString
                txtCurrAccNo.Text = .Rows(0).Item("curr_acc_no").ToString
                txtCurrMicrCode.Text = .Rows(0).Item("curr_micr_code").ToString
                'Bank New
                cboNewBank.Text = .Rows(0).Item("new_bank_name").ToString
                cboNewAccType.Text = .Rows(0).Item("new_acc_type").ToString
                txtNewBeneficiary.Text = .Rows(0).Item("new_beneficiary").ToString
                txtNewBranch.Text = .Rows(0).Item("new_branch").ToString
                txtNewAddr.Text = .Rows(0).Item("new_addr").ToString
                txtNewIfscCode.Text = .Rows(0).Item("new_ifsc_code").ToString
                txtNewAccNo.Text = .Rows(0).Item("new_acc_no").ToString
                txtNewMicrCode.Text = .Rows(0).Item("new_micr_code").ToString
                'PAN Current
                txtCurrHolder1PanNo.Text = .Rows(0).Item("curr_holder1_pan_no").ToString
                txtCurrHolder2PanNo.Text = .Rows(0).Item("curr_holder2_pan_no").ToString
                txtCurrHolder3PanNo.Text = .Rows(0).Item("curr_holder3_pan_no").ToString
                'PAN New
                txtNewHolder1PanNo.Text = .Rows(0).Item("new_holder1_pan_no").ToString
                txtNewHolder2PanNo.Text = .Rows(0).Item("new_holder2_pan_no").ToString
                txtNewHolder3PanNo.Text = .Rows(0).Item("new_holder3_pan_no").ToString
                'Signature
                mnFileName = .Rows(0).Item("file_name").ToString
                mnSignatureId = .Rows(0).Item("signature_gid")
                'Nominee Current
                txtCurrNomineeName.Text = .Rows(0).Item("curr_nominee_name").ToString
                txtCurrNomAddr1.Text = .Rows(0).Item("curr_nominee_addr1").ToString
                txtCurrNomaddr2.Text = .Rows(0).Item("curr_nominee_addr2").ToString
                txtCurrNomaddr3.Text = .Rows(0).Item("curr_nominee_addr3").ToString
                txtCurrNomcity.Text = .Rows(0).Item("curr_nominee_city").ToString
                txtCurrNomstate.Text = .Rows(0).Item("curr_nominee_state").ToString
                txtCurrNomCoun.Text = .Rows(0).Item("curr_nominee_country").ToString
                txtCurrNomPin.Text = .Rows(0).Item("curr_nominee_pincode").ToString

                If .Rows(0).Item("curr_nominee_dob").ToString <> "" Then
                    dtpDOB.Checked = True
                    dtpCurrDOB.Value = .Rows(0).Item("curr_nominee_dob")
                End If

                txtCurrFMS.Text = .Rows(0).Item("curr_nominee_fms_name").ToString
                txtCurrGuard.Text = .Rows(0).Item("curr_nominee_guardian").ToString
                txtCurrOccup.Text = .Rows(0).Item("curr_nominee_occupation").ToString
                txtCurrNation.Text = .Rows(0).Item("curr_nominee_nationality").ToString
                txtCurrEmail.Text = .Rows(0).Item("curr_nominee_emailid").ToString
                cmbCurrRelation.Text = .Rows(0).Item("curr_nominee_relationship").ToString
                'Nominee New
                If .Rows(0).Item("nominee_assign_flag").ToString = "Y" Then
                    rbtYes.Checked = True
                ElseIf .Rows(0).Item("nominee_assign_flag").ToString = "N" Then
                    rbtNo.Checked = True
                End If
                txtNewNomineeName.Text = .Rows(0).Item("new_nominee_name").ToString
                txtNewNomaddr1.Text = .Rows(0).Item("new_nominee_addr1").ToString
                txtNewNomaddr2.Text = .Rows(0).Item("new_nominee_addr2").ToString
                txtNewNomaddr3.Text = .Rows(0).Item("new_nominee_addr3").ToString
                txtNewNomCity.Text = .Rows(0).Item("new_nominee_city").ToString
                txtNewNomState.Text = .Rows(0).Item("new_nominee_state").ToString
                txtNewNomCoun.Text = .Rows(0).Item("new_nominee_country").ToString
                txtNewNomPin.Text = .Rows(0).Item("new_nominee_pincode").ToString

                If .Rows(0).Item("new_nominee_dob").ToString() <> "" Then
                    dtpDOB.Checked = True
                    dtpDOB.Value = .Rows(0).Item("new_nominee_dob")
                End If

                txtFMS.Text = .Rows(0).Item("new_nominee_fms_name").ToString
                txtNewGuard.Text = .Rows(0).Item("new_nominee_guardian").ToString
                txtOccup.Text = .Rows(0).Item("new_nominee_occupation").ToString
                txtNation.Text = .Rows(0).Item("new_nominee_nationality").ToString
                txtEmail.Text = .Rows(0).Item("new_nominee_emailid").ToString
                cmbNewRelation.Text = .Rows(0).Item("new_nominee_relationship").ToString

                lnChkLstValid = .Rows(0).Item("chklst_valid")
                lnChkLstDisc = .Rows(0).Item("chklst_disc")

            Else
                Call frmCtrClear(Me)
            End If
        End With

        ' Beneficiary name 
        txtNewBeneficiary.Text = txtHolder1.Text.ToString()
        If txtNewBeneficiary.Text <> "" Then
            txtNewBeneficiary.Enabled = False
        Else
            txtNewBeneficiary.Enabled = True
        End If

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()

        'Image Display
        If mnSignatureId > 0 Then
            lnSignatureId = mnSignatureId
            lsFileName = mnFileName
            lsSrcFile = gsSignaturePath & "\" & lnSignatureId.ToString & ".sig"
            lsDestFile = gsReportPath & "\" & lsFileName
            File.Copy(lsSrcFile, lsDestFile, True)

            PictureSignature.ImageLocation = lsDestFile
            txtFileName.Text = lsDestFile

        End If

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
            .Columns("Check List").Width = 225

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

    Private Sub UpdateInformation(ActionStatus As Integer)
        Dim i As Integer
        Dim n As Integer

        Dim lnChklstValid As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnChklstValue As Long = 0

        Dim lnResult As Long
        Dim lsTxt As String

        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsContactNo As String
        Dim lsMailId As String
        Dim lsBankName As String
        Dim lsBeneficiary As String
        Dim lsBranch As String
        Dim lsAddr As String
        Dim lsIfscCode As String
        Dim lsBankAccType As String = ""
        Dim lsBankAccNo As String
        Dim lsBankMicrCode As String = ""
        Dim lsHolder1PanNo As String
        Dim lsHolder2PanNo As String
        Dim lsHolder3PanNo As String
        Dim lsSrcFile As String
        Dim lsFileName As String
        Dim lsFolioNo As String
        Dim lsnmassignflag As String
        Dim lsnmdob As Date
        Dim lsnmName As String
        Dim lsnmFms As String
        Dim lsnmGuardian As String
        Dim lsnmOccupation As String
        Dim lsnmNationality As String
        Dim lsnmEmailid As String
        Dim lsnmRelationship As String
        Dim lsnmAddr1 As String
        Dim lsnmAddr2 As String
        Dim lsnmAddr3 As String
        Dim lsnmCity As String
        Dim lsnmState As String
        Dim lsnmCountry As String
        Dim lsnmPincode As String
        Dim lsRemark As String

        Dim lsCertId As String = ""
        Dim lnNewFolioId As Long = 0

        Dim lnSignatureId As Long
        Dim lsDestFile As String



        Try

            lsAddr1 = QuoteFilter(txtNewAddr1.Text)
            lsAddr2 = QuoteFilter(txtNewAddr2.Text)
            lsAddr3 = QuoteFilter(txtNewAddr3.Text)
            lsCity = QuoteFilter(txtNewCity.Text)
            lsState = QuoteFilter(txtNewState.Text)
            lsCountry = QuoteFilter(txtNewCountry.Text)
            lsPincode = QuoteFilter(txtNewPincode.Text)
            lsContactNo = QuoteFilter(txtNewPhoneNo.Text)
            lsMailId = QuoteFilter(txtNewEmailId.Text)
            lsRemark = QuoteFilter(txtRemark.Text)
            lsBankName = QuoteFilter(cboNewBank.Text)
            lsBeneficiary = QuoteFilter(txtNewBeneficiary.Text)
            lsBranch = QuoteFilter(txtNewBranch.Text)
            lsAddr = QuoteFilter(txtNewAddr.Text)
            lsIfscCode = QuoteFilter(txtNewIfscCode.Text)
            lsBankAccNo = QuoteFilter(txtNewAccNo.Text)
            lsBankMicrCode = QuoteFilter(txtNewMicrCode.Text)
            lsHolder1PanNo = QuoteFilter(txtNewHolder1PanNo.Text)
            lsHolder2PanNo = QuoteFilter(txtNewHolder2PanNo.Text)
            lsHolder3PanNo = QuoteFilter(txtNewHolder3PanNo.Text)
            If rbtYes.Checked = True Then
                lsnmassignflag = "Y"
            ElseIf rbtNo.Checked = True Then
                lsnmassignflag = "N"
            End If

            lsnmName = QuoteFilter(txtNewNomineeName.Text)
            lsnmAddr1 = QuoteFilter(txtNewNomaddr1.Text)
            lsnmAddr2 = QuoteFilter(txtNewNomaddr2.Text)
            lsnmAddr3 = QuoteFilter(txtNewNomaddr3.Text)
            lsnmCity = QuoteFilter(txtNewNomCity.Text)
            lsnmState = QuoteFilter(txtNewNomState.Text)
            lsnmCountry = QuoteFilter(txtNewNomCoun.Text)
            lsnmPincode = QuoteFilter(txtNewNomPin.Text)
            If dtpDOB.Checked = True Then
                lsnmdob = Format(dtpDOB.Value, "yyyy-MM-dd")
            Else
                lsnmdob = "1900-01-01"

            End If

            lsnmFms = QuoteFilter(txtFMS.Text)
            lsnmGuardian = QuoteFilter(txtNewGuard.Text)
            lsnmOccupation = QuoteFilter(txtOccup.Text)
            lsnmNationality = QuoteFilter(txtNation.Text)
            lsnmEmailid = QuoteFilter(txtEmail.Text)
            lsnmRelationship = QuoteFilter(cmbNewRelation.Text)
            'If cmbCurrRelation.SelectedIndex <> -1 Then
            '    lsnmRelationshipid = Val(cmbCurrRelation.SelectedValue.ToString)
            'Else
            '    lsnmRelationshipid = 0
            'End If

            lsRemark = QuoteFilter(txtRemark.Text)

            lsSrcFile = txtFileName.Text
            lsFileName = lsSrcFile.Split("\")(lsSrcFile.Split("\").Length - 1)
            lsFolioNo = txtFolioNo.Text.Trim

            If cboNewAccType.Text <> "" And cboNewAccType.SelectedIndex <> -1 Then
                lsBankAccType = cboNewAccType.SelectedValue
            End If

            ' get certificate id
            lsCertId = ""

            If File.Exists(txtFileName.Text) = False Then
                MessageBox.Show("Please select the file !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnBrowse.PerformClick()
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

            Using cmd As New MySqlCommand("pr_sta_set_folioentryisr", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_signature_gid", 0)
                cmd.Parameters("?in_signature_gid").Direction = ParameterDirection.InputOutput
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_folio_addr1", lsAddr1)
                cmd.Parameters.AddWithValue("?in_folio_addr2", lsAddr2)
                cmd.Parameters.AddWithValue("?in_folio_addr3", lsAddr3)
                cmd.Parameters.AddWithValue("?in_folio_city", lsCity)
                cmd.Parameters.AddWithValue("?in_folio_state", lsState)
                cmd.Parameters.AddWithValue("?in_folio_country", lsCountry)
                cmd.Parameters.AddWithValue("?in_folio_pincode", lsPincode)
                cmd.Parameters.AddWithValue("?in_folio_contact_no", lsContactNo)
                cmd.Parameters.AddWithValue("?in_folio_mail_id", lsMailId)
                cmd.Parameters.AddWithValue("?in_bank_name", lsBankName)
                cmd.Parameters.AddWithValue("?in_bank_acc_no", lsBankAccNo)
                cmd.Parameters.AddWithValue("?in_bank_ifsc_code", lsIfscCode)
                cmd.Parameters.AddWithValue("?in_bank_branch", lsBranch)
                cmd.Parameters.AddWithValue("?in_bank_beneficiary", lsBeneficiary)
                cmd.Parameters.AddWithValue("?in_bank_acc_type", lsBankAccType)
                cmd.Parameters.AddWithValue("?in_bank_addr", lsAddr)
                cmd.Parameters.AddWithValue("?in_bank_micr_code", lsBankMicrCode)
                cmd.Parameters.AddWithValue("?in_holder1_pan_no", lsHolder1PanNo)
                cmd.Parameters.AddWithValue("?in_holder2_pan_no", lsHolder2PanNo)
                cmd.Parameters.AddWithValue("?in_holder3_pan_no", lsHolder3PanNo)
                cmd.Parameters.AddWithValue("?in_comp_gid", mnCompId)
                cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters.AddWithValue("?in_nominee_assign_flag", lsnmassignflag)
                cmd.Parameters.AddWithValue("?in_nominee_name", lsnmName)
                cmd.Parameters.AddWithValue("?in_nominee_dob", Format(lsnmdob, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_nominee_fms_name", lsnmFms)
                cmd.Parameters.AddWithValue("?in_nominee_guardian", lsnmGuardian)
                cmd.Parameters.AddWithValue("?in_nominee_occupation", lsnmOccupation)
                cmd.Parameters.AddWithValue("?in_nominee_nationality", lsnmNationality)
                cmd.Parameters.AddWithValue("?in_nominee_emailid", lsnmEmailid)
                cmd.Parameters.AddWithValue("?in_nominee_relationship", lsnmRelationship)
                cmd.Parameters.AddWithValue("?in_nominee_addr1", lsnmAddr1)
                cmd.Parameters.AddWithValue("?in_nominee_addr2", lsnmAddr2)
                cmd.Parameters.AddWithValue("?in_nominee_addr3", lsnmAddr3)
                cmd.Parameters.AddWithValue("?in_nominee_city", lsnmCity)
                cmd.Parameters.AddWithValue("?in_nominee_state", lsnmState)
                cmd.Parameters.AddWithValue("?in_nominee_country", lsnmCountry)
                cmd.Parameters.AddWithValue("?in_nominee_pincode", lsnmPincode)
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

                    lnSignatureId = Val(cmd.Parameters("?in_signature_gid").Value.ToString())
                    If Directory.Exists(gsSignaturePath) = False Then Directory.CreateDirectory(gsSignaturePath)
                    lsDestFile = gsSignaturePath & "\" & lnSignatureId.ToString & ".sig"

                    If File.Exists(lsDestFile) Then
                        Dim pathvalue = gsSignaturePath & "\" & lnSignatureId.ToString & "_" & Now.ToString() & ".sig"
                        File.Copy(lsDestFile, gsSignaturePath & "\" & lnSignatureId.ToString & "_" & Now.ToString("ddMMyyhhmmss") & ".sig")
                        File.Delete(lsDestFile)
                    End If

                    Call File.Copy(lsSrcFile, lsDestFile)


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
            If e.RowIndex >= 0 Then
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

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        With OpenFileDialog1
            .Filter = "All Files (*.*)|*.*"
            .FileName = ""
            .ShowDialog()

            txtFileName.Text = .FileName
            PictureSignature.ImageLocation = txtFileName.Text
        End With
    End Sub

    Private Sub rbtYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbtYes.CheckedChanged
        If msGroupCode = "M" Then
            grpPropISR3.Enabled = True
        End If
    End Sub

    Private Sub rbtNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbtNo.CheckedChanged
        grpPropISR3.Enabled = False
        nomineeclear()
    End Sub

    Private Sub nomineeclear()
        txtNewNomineeName.Text = ""
        txtFMS.Text = ""
        txtNewGuard.Text = ""
        txtOccup.Text = ""
        txtNation.Text = ""
        txtEmail.Text = ""
        cmbNewRelation.Text = ""
        txtNewNomaddr1.Text = ""
        txtNewNomaddr2.Text = ""
        txtNewNomaddr3.Text = ""
        txtNewNomCity.Text = ""
        txtNewNomState.Text = ""
        txtNewNomCoun.Text = ""
        txtNewNomPin.Text = ""
    End Sub

    Private Sub btn_autofill_Click(sender As Object, e As EventArgs) Handles btn_autofill.Click
        txtNewNomaddr1.Text = txtNewAddr1.Text.ToString()
        txtNewNomaddr2.Text = txtNewAddr2.Text.ToString()
        txtNewNomaddr3.Text = txtNewAddr3.Text.ToString()
        txtNewNomCity.Text = txtNewCity.Text.ToString()
        txtNewNomState.Text = txtNewState.Text.ToString()
        txtNewNomPin.Text = txtNewPincode.Text.ToString()
        txtNewNomCoun.Text = txtNewCountry.Text.ToString()
    End Sub
End Class