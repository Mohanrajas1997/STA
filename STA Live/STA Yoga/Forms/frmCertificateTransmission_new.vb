Imports MySql.Data.MySqlClient

Public Class frmCertificateTransmission_new
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnSrcFolioId As Long
    Dim mnQueueId As Long
    Dim msTranCode As String
    Dim mnChklstValid As Long
    Dim mnChklstDisc As Long
    Dim mnChkLstAllStatus As Long = 0
    Dim mnChkLstSelected As Long = 0
    Dim msGroupCode As String = ""
#End Region

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmAddressChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

                'btnReject.Text = "Inex"
                'btnReject.Visible = False
                'btnSubmit.Left = btnReject.Left
            Case "C"
                btnReject.Text = "Send Back"
            Case "U"
                btnSubmit.Visible = False
                btnReject.Text = "Send Back"
        End Select

        Select Case msTranCode
            Case "TP"
                btnCreateNewFolio.Text = "Transposition Folio"
            Case "FC"
                btnCreateNewFolio.Visible = False
            Case Else
                btnCreateNewFolio.Text = "Create Folio"
        End Select

        Call LoadData()
    End Sub

    Public Sub New(GroupCode As String, InwardId As Long, QueueId As Long, TranCode As String, Optional CreateNewFolioFlag As Boolean = True)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        msGroupCode = GroupCode
        mnInwardId = InwardId
        mnQueueId = QueueId
        msTranCode = TranCode

        btnCreateNewFolio.Visible = CreateNewFolioFlag

        Select Case GroupCode
            Case "M"
            Case "V"
                dgvCert.ReadOnly = True
                dgvChklst.ReadOnly = True

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

                dgvCert.ReadOnly = True
                dgvChklst.ReadOnly = True

                'grpName.Enabled = False
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

        Dim lnFolioId As Long
        Dim lnTotShares As Long = 0
        Dim lnCertEntryFlag As Integer = 0
        Dim lnNewFolioId As Long = 0

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
                mnCompId = .Rows(0).Item("comp_gid")
                msCompName = .Rows(0).Item("comp_name").ToString

                lnFolioId = .Rows(0).Item("folio_gid")
                mnSrcFolioId = .Rows(0).Item("folio_gid")

                txtNewFolioId.Text = .Rows(0).Item("tran_folio_gid")
                txtNewFolioNo.Text = .Rows(0).Item("tran_folio_no").ToString

                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolder.Text = .Rows(0).Item("shareholder_name").ToString
                txtPanNo.Text = .Rows(0).Item("shareholder_pan_no").ToString

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

                If (lnCertEntryFlag = 1) Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If
            Next i
        End With

        lblTotal.Text = lnTotShares.ToString

        Call RefreshShareCount()
        'Call LoadFolioISRData()
        lnNewFolioId = Val(txtNewFolioId.Text)
        Call LoadFolio(lnNewFolioId)

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

    Private Sub LoadFolioISRData()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lnChkLstValid As Integer
        Dim lnChkLstDisc As Integer

        cmd = New MySqlCommand("pr_sta_get_foliotm", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_queue_gid", mnQueueId)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then

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

                'Bank Details
                txtNewBeneficiary.Text = .Rows(0).Item("bank_beneficiary").ToString
                cboNewBank.Text = .Rows(0).Item("bank_name").ToString
                txtNewBranch.Text = .Rows(0).Item("bank_branch").ToString
                txtNewAddr.Text = .Rows(0).Item("bank_branch_addr").ToString
                txtNewIfscCode.Text = .Rows(0).Item("bank_ifsc_code").ToString
                txtNewAccNo.Text = .Rows(0).Item("bank_acc_no").ToString
                txtNewMicrCode.Text = .Rows(0).Item("bank_micr_code").ToString
                cboNewAccType.Text = .Rows(0).Item("bankacctype_name").ToString

                'Nominee Details
                If .Rows(0).Item("nominee_name").ToString <> "" Then
                    rbtYes.Checked = True
                Else
                    rbtNo.Checked = False
                End If
                txtNewNomineeName.Text = .Rows(0).Item("nominee_name").ToString
                txtFMS.Text = .Rows(0).Item("nominee_fms_name").ToString
                txtNewGuard.Text = .Rows(0).Item("nominee_guardian").ToString
                txtOccup.Text = .Rows(0).Item("nominee_occupation").ToString
                txtNation.Text = .Rows(0).Item("nominee_nationality").ToString
                txtNewNomaddr1.Text = .Rows(0).Item("nominee_addr1").ToString
                txtNewNomaddr2.Text = .Rows(0).Item("nominee_addr2").ToString
                txtNewNomaddr3.Text = .Rows(0).Item("nominee_addr3").ToString
                txtEmail.Text = .Rows(0).Item("nominee_emailid").ToString
                cmbNewRelation.Text = .Rows(0).Item("nominee_relationship").ToString
                txtNewNomCity.Text = .Rows(0).Item("nominee_city").ToString
                txtNewNomState.Text = .Rows(0).Item("nominee_state").ToString
                txtNewNomCoun.Text = .Rows(0).Item("nominee_country").ToString
                txtNewNomPin.Text = .Rows(0).Item("nominee_pincode").ToString

                If .Rows(0).Item("nominee_dob").ToString <> "" Then
                    dtpDOB.Checked = True
                    dtpDOB.Value = DateTime.Parse(.Rows(0).Item("nominee_dob").ToString)
                Else
                    dtpDOB.Checked = False
                End If
                
                'lsFolioNo = txtFolioNo.Text.Trim

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


        'Dim lsRemark As String


        Dim lsCertId As String = ""
        Dim lnNewFolioId As Long = 0

        Dim lsRemark As String = ""

        Try
            lnNewFolioId = Val(txtNewFolioId.Text)
            'lsRemark = QuoteFilter(txtRemark.Text)


            lsRemark = QuoteFilter(txtRemark.Text)

            'lsSrcFile = txtFileName.Text
            'lsFileName = lsSrcFile.Split("\")(lsSrcFile.Split("\").Length - 1)


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

            'Using cmd As New MySqlCommand("pr_sta_set_certentrytransmission", gOdbcConn)
            '    cmd.CommandType = CommandType.StoredProcedure
            '    cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
            '    cmd.Parameters.AddWithValue("?in_cert_gid", lsCertId)
            '    cmd.Parameters.AddWithValue("?in_src_folio_gid", mnSrcFolioId)
            '    cmd.Parameters.AddWithValue("?in_new_folio_gid", lnNewFolioId)
            '    cmd.Parameters.AddWithValue("?in_bank_name", lsBankName)
            '    cmd.Parameters.AddWithValue("?in_bank_acc_no", lsBankAccNo)
            '    cmd.Parameters.AddWithValue("?in_bank_ifsc_code", lsIfscCode)
            '    cmd.Parameters.AddWithValue("?in_bank_branch", lsBranch)
            '    cmd.Parameters.AddWithValue("?in_bank_beneficiary", lsBeneficiary)
            '    cmd.Parameters.AddWithValue("?in_bank_acc_type", lsBankAccType)
            '    cmd.Parameters.AddWithValue("?in_bank_addr", lsAddr)
            '    cmd.Parameters.AddWithValue("?in_bank_micr_code", lsBankMicrCode)
            '    cmd.Parameters.AddWithValue("?in_comp_gid", mnCompId)
            '    cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
            '    cmd.Parameters.AddWithValue("?in_nominee_assign_flag", lsnmassignflag)
            '    cmd.Parameters.AddWithValue("?in_nominee_name", lsnmName)
            '    cmd.Parameters.AddWithValue("?in_nominee_dob", Format(lsnmdob, "yyyy-MM-dd"))
            '    cmd.Parameters.AddWithValue("?in_nominee_fms_name", lsnmFms)
            '    cmd.Parameters.AddWithValue("?in_nominee_guardian", lsnmGuardian)
            '    cmd.Parameters.AddWithValue("?in_nominee_occupation", lsnmOccupation)
            '    cmd.Parameters.AddWithValue("?in_nominee_nationality", lsnmNationality)
            '    cmd.Parameters.AddWithValue("?in_nominee_emailid", lsnmEmailid)
            '    cmd.Parameters.AddWithValue("?in_nominee_relationship", lsnmRelationship)
            '    cmd.Parameters.AddWithValue("?in_nominee_addr1", lsnmAddr1)
            '    cmd.Parameters.AddWithValue("?in_nominee_addr2", lsnmAddr2)
            '    cmd.Parameters.AddWithValue("?in_nominee_addr3", lsnmAddr3)
            '    cmd.Parameters.AddWithValue("?in_nominee_city", lsnmCity)
            '    cmd.Parameters.AddWithValue("?in_nominee_state", lsnmState)
            '    cmd.Parameters.AddWithValue("?in_nominee_country", lsnmCountry)
            '    cmd.Parameters.AddWithValue("?in_nominee_pincode", lsnmPincode)
            '    cmd.Parameters.AddWithValue("?in_chklst_valid", lnChklstValid)
            '    cmd.Parameters.AddWithValue("?in_chklst_disc", lnChklstDisc)
            '    cmd.Parameters.AddWithValue("?in_remark", lsRemark)
            '    cmd.Parameters.AddWithValue("?in_action_status", ActionStatus)
            '    cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

            '    'Out put Para
            '    cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
            '    cmd.Parameters("?out_result").Direction = ParameterDirection.Output
            '    cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
            '    cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

            '    cmd.CommandTimeout = 0

            '    cmd.ExecuteNonQuery()

            '    lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
            '    lsTxt = cmd.Parameters("?out_msg").Value.ToString()

            '    If lnResult = 1 Then
            '        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Me.Close()
            '    Else
            '        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Sub
            '    End If
            'End Using

            Using cmd As New MySqlCommand("pr_sta_set_certentrytransmission", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_cert_gid", lsCertId)
                cmd.Parameters.AddWithValue("?in_src_folio_gid", mnSrcFolioId)
                cmd.Parameters.AddWithValue("?in_new_folio_gid", lnNewFolioId)
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
                        If .ReadOnly = False Then
                            .Rows(e.RowIndex).Cells(.Columns.Count - 1).Value = False
                            .Rows(e.RowIndex).Cells(.Columns.Count - 2).Value = False
                        End If

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

        If msTranCode = "TP" Then
            frm = New frmFolioCreate_new(mnCompId, msCompName, mnSrcFolioId, True)
        Else
            frm = New frmFolioCreate_new(mnCompId, msCompName)
        End If

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
        Dim frm As New frmFolioPanUpdate(mnSrcFolioId)
        frm.ShowDialog()
    End Sub

End Class