Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports Newtonsoft.Json.Linq

Public Class frmInwardEntryNew
    Dim msMode As String
    Dim mnInwardId As Long
    Dim mbGenerateInwardNo As Boolean = True
    Dim lsMailmessageBody As String
    Dim mnInwardGID As Long

    Public Sub New(Mode As String, Optional InwardId As Long = 0, Optional GenerateInwardNoFlag As Boolean = True)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        msMode = Mode
        mnInwardId = InwardId
        mbGenerateInwardNo = GenerateInwardNoFlag

        If mbGenerateInwardNo = True Then
            txtInwardNo.Enabled = False
        Else
            txtInwardNo.Enabled = True
        End If

        Select Case msMode.ToUpper()
            Case "ADD"
                btnSave.Visible = True
                btnClear.Visible = True
            Case "UPDATE"
                btnSave.Visible = True
                btnClear.Visible = True
            Case "VIEW"
                btnSave.Visible = False
                btnClear.Visible = False
        End Select
    End Sub

    Public Function GetDocsubtypeDT() As DataTable
        '' Create new DataTable instance.
        'Dim cmd As MySqlCommand
        'Dim da As MySqlDataAdapter
        Dim dt As DataTable

        Dim lsSql As String
        lsSql = ""
        lsSql &= " select docsubtype_code,docsubtype_desc from sta_mst_tdocsubtype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " and trantype_code = '" & cboDocType.SelectedValue.ToString & "' "
        lsSql &= " order by docsubtype_code asc "

        'cmd = New MySqlCommand(lsSql, gOdbcConn)
        'dt = New DataTable
        dt = GetDataTable(lsSql)
        'da = New MySqlDataAdapter(cmd)
        'da.Fill(dt)

        Return dt
    End Function

    Private Sub ListAll(ByVal InwardId As Long)
        Dim lsSql As String
        Dim lobjDataReader As MySqlDataReader
        Dim v_trancode As String
        Try
            lsSql = ""
            lsSql &= " select * from sta_trn_tinward "
            lsSql &= " where inward_gid = " & mnInwardId & " "
            lsSql &= " and delete_flag = 'N' "

            lobjDataReader = gfExecuteQry(lsSql, gOdbcConn)

            cboCourier.SelectedIndex = -1
            cboDocType.SelectedIndex = -1
            cboCompany.SelectedIndex = -1
            cboRcvdMode.SelectedIndex = -1
            cboDocSubType.SelectedIndex = -1

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("inward_gid").ToString
                        txtInwardNo.Text = .Item("inward_no").ToString
                        v_trancode = .Item("tran_code").ToString
                        cboRcvdMode.SelectedValue = .Item("received_mode").ToString
                        dtpRcvdDate.Value = .Item("received_date")
                        cboCourier.SelectedValue = .Item("courier_gid").ToString
                        txtAwbNo.Text = .Item("awb_no").ToString
                        cboCompany.SelectedValue = .Item("comp_gid").ToString
                        txtFolioNo.Text = .Item("folio_no").ToString
                        txtShareHolderName.Text = .Item("shareholder_name").ToString
                        txtPanNo.Text = .Item("shareholder_pan_no").ToString
                        txtContactNo.Text = .Item("shareholder_contact_no").ToString
                        txtMailId.Text = .Item("shareholder_email_id").ToString
                        txtRemark.Text = .Item("inward_remark").ToString
                        txtInwardShareCount.Text = .Item("inward_share_count").ToString
                        txtMarketPrice.Text = .Item("market_price").ToString
                        txtMarketValue.Text = .Item("market_value").ToString
                        txtdpclientid.Text = .Item("dp_client_id").ToString
                    End If
                End If
                .Close()
                cboDocType.SelectedValue = v_trancode
            End With

            lsSql = ""
            lsSql &= " select group_concat(docsubtype_desc) from sta_trn_tinwarddocsubtype "
            lsSql &= " where inward_gid = " & mnInwardId & " "
            lsSql &= " and delete_flag = 'N' "
            Dim dtSelected As DataTable
            dtSelected = GetDataTable(lsSql)

            If dtSelected IsNot Nothing AndAlso dtSelected.Rows.Count > 0 Then
                Dim concatenatedValue As String = dtSelected.Rows(0)(0).ToString()
                Dim selectedList As List(Of String) =
                    concatenatedValue.Split(","c).Select(Function(s) s.Trim()).ToList()

                ' Loop through items in CheckedListBox and check matches
                For i As Integer = 0 To DocSubType.Items.Count - 1
                    Dim itemText As String = DocSubType.GetItemText(DocSubType.Items(i))
                    If selectedList.Contains(itemText) Then
                        DocSubType.SetItemChecked(i, True)
                    End If
                Next
            End If

            Call gpAutoFillCombo(cboDocType)
            Call gpAutoFillCombo(cboRcvdMode)
            Call gpAutoFillCombo(cboCompany)
            Call gpAutoFillCombo(cboCourier)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmInwardEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmBankMater_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        'e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        cboDocSubType.Enabled = False
        lnkAddAttachment.Enabled = False

        ' received mode
        lsSql = ""
        lsSql &= " select receivedmode_code,receivedmode_desc from sta_mst_treceivedmode "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by receivedmode_desc asc "

        Call gpBindCombo(lsSql, "receivedmode_desc", "receivedmode_code", cboRcvdMode, gOdbcConn)

        ' doc type
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "

        Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

        ' courier
        lsSql = ""
        lsSql &= " select courier_gid,courier_name from sta_mst_tcourier "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by courier_name asc "

        Call gpBindCombo(lsSql, "courier_name", "courier_gid", cboCourier, gOdbcConn)

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpRcvdDate.Value = Now

        'Call ClearControl()

        If mnInwardId > 0 Then
            Call ListAll(mnInwardId)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnInwardId As Long
        Dim lnInwardNo As Long
        Dim lnCompInwardNo As String
        Dim lnCompId As Long
        Dim lsRcvdMode As String
        Dim ldRcvdDate As Date
        Dim lnCourierId As Long
        Dim lsAwbNo As String
        Dim lsTranCode As String
        Dim lsDocSubTypeCode As String
        Dim lsDocSubTypeDesc As String = ""
        Dim lsFolioNo As String
        Dim lsShareHolderName As String
        Dim lsShareHolderAddr As String = ""
        Dim lsShareHolderPanNo As String = ""
        Dim lsShareHolderContactNo As String
        Dim lsShareHolderEmailId As String
        Dim lsInwardShareCount As Long
        Dim lsMarketPrice As Decimal
        Dim lsMarketValue As Decimal
        Dim lsThreshold As String
        Dim lsRemark As String
        Dim lsDpClientId As String
        Dim lsAction As String

        Try
            If cboCompany.SelectedIndex <> -1 Then
                lnCompId = Val(cboCompany.SelectedValue.ToString)
            Else
                lnCompId = 0
            End If

            lnInwardNo = Val(QuoteFilter(txtInwardNo.Text))

            If mnInwardId = 0 Then
                If mbGenerateInwardNo = True Then
                    If lnInwardNo > 0 Then
                        MessageBox.Show("Inward no is should be generated automatically !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cboDocType.Focus()
                        Exit Sub
                    End If
                Else
                    If lnInwardNo = 0 Then
                        MessageBox.Show("Inward no cannot be zero !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtInwardNo.Focus()
                        Exit Sub
                    End If
                End If
            End If

            If cboRcvdMode.SelectedIndex <> -1 Then
                lsRcvdMode = cboRcvdMode.SelectedValue.ToString
            Else
                lsRcvdMode = ""
            End If

            ldRcvdDate = dtpRcvdDate.Value
            'Modified by Mohan 05-06-2024
            If cboDocType.Text = "OT-Others" Or cboDocType.Text = "TM-Transmission" Or cboDocType.Text = "AM-CA Allotment" Then
                If cboDocSubType.Text = "" Then
                    MessageBox.Show("Document SubType cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cboDocSubType.Focus()
                    Exit Sub
                End If
            End If

            If cboDocType.Text = "DM-Demat" Then

            End If

            If cboCourier.SelectedIndex <> -1 Then
                lnCourierId = Val(cboCourier.SelectedValue.ToString)
            Else
                lnCourierId = 0
            End If

            lsAwbNo = QuoteFilter(txtAwbNo.Text)

            If cboDocType.SelectedIndex <> -1 Then
                lsTranCode = cboDocType.SelectedValue.ToString
            Else
                lsTranCode = ""
            End If

            If lsTranCode = "CD" Then
                If DocSubType.CheckedItems.Count = 0 Then
                    MessageBox.Show("Please select at least one document subtype.", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If txtdpclientid.TextLength <> 16 Then
                    MessageBox.Show("Dp/Client Id length should be 16 !..", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                ' Get codes
                Dim selectedCodes As String = String.Join(","c, DocSubType.CheckedItems.Cast(Of DataRowView)().
                                                          Select(Function(row) row("docsubtype_code").ToString()))

                ' Get descriptions
                Dim selectedDescs As String = String.Join(","c, DocSubType.CheckedItems.Cast(Of DataRowView)().
                                                          Select(Function(row) row("docsubtype_desc").ToString()))

                ' Assign to variables
                lsDocSubTypeCode = selectedCodes
                lsDocSubTypeDesc = selectedDescs
            Else
                If cboDocSubType.SelectedIndex <> -1 Then
                    lsDocSubTypeCode = cboDocSubType.SelectedValue.ToString
                    lsDocSubTypeDesc = cboDocSubType.Text.ToString
                Else
                    lsDocSubTypeCode = ""
                End If
            End If

            'If cboDocSubType.SelectedIndex <> -1 Then
            '    lsDocSubTypeCode = cboDocSubType.SelectedValue.ToString
            'Else
            '    lsDocSubTypeCode = ""
            'End If

            lsFolioNo = QuoteFilter(txtFolioNo.Text)
            lsShareHolderName = QuoteFilter(txtShareHolderName.Text)
            lsShareHolderPanNo = QuoteFilter(txtPanNo.Text)
            lsShareHolderContactNo = QuoteFilter(txtContactNo.Text)
            lsShareHolderEmailId = QuoteFilter(txtMailId.Text)
            lsInwardShareCount = Val(txtInwardShareCount.Text)
            lsMarketPrice = Val(txtMarketPrice.Text)
            lsMarketValue = Val(txtMarketValue.Text)
            lsThreshold = QuoteFilter(cboThreshold.Text)
            lsRemark = QuoteFilter(txtRemark.Text)
            lsDpClientId = QuoteFilter(txtdpclientid.Text)

            lnInwardId = Val(txtId.Text)

            'Mohan Changes on 28-05-2024
            'And cboDocType.Text <> "TM-Transmission" And cboDocType.Text <> "IS-ISR" And cboDocType.Text <> "CP-PAN Update" 
            If cboDocType.Text <> "OT-Others" Then
                Using cmd As New MySqlCommand("pr_sta_validate_debarrtpan", gOdbcConn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("?in_folio_gid", 0)
                    cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                    cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                    cmd.Parameters.AddWithValue("?in_tran_type", cboDocType.Text)

                    'Out put Para
                    cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                    cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                    cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                    cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                    cmd.CommandTimeout = 0
                    cmd.ExecuteNonQuery()

                    lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                    lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                    If lnResult = 0 Then
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtFolioNo.Focus()
                        Exit Sub
                    End If
                End Using
            End If



            If lnInwardId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_trn_tinwardnew", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", lnInwardId)
                cmd.Parameters.AddWithValue("?in_entity_gid", gnEntityId)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_inward_no", lnInwardNo)
                cmd.Parameters.AddWithValue("?in_received_date", Format(ldRcvdDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_received_mode", lsRcvdMode)
                cmd.Parameters.AddWithValue("?in_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_awb_no", lsAwbNo)
                cmd.Parameters.AddWithValue("?in_tran_code", lsTranCode)
                cmd.Parameters.AddWithValue("?in_docsubtype_code", lsDocSubTypeCode)
                cmd.Parameters.AddWithValue("?in_docsubtype_desc", lsDocSubTypeDesc)
                cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                cmd.Parameters.AddWithValue("?in_shareholder_name", lsShareHolderName)
                cmd.Parameters.AddWithValue("?in_shareholder_addr", lsShareHolderAddr)
                cmd.Parameters.AddWithValue("?in_shareholder_pan_no", lsShareHolderPanNo)
                cmd.Parameters.AddWithValue("?in_shareholder_contact_no", lsShareHolderContactNo)
                cmd.Parameters.AddWithValue("?in_shareholder_email_id", lsShareHolderEmailId)
                cmd.Parameters.AddWithValue("?in_inward_share_count", lsInwardShareCount)
                cmd.Parameters.AddWithValue("?in_market_price", lsMarketPrice)
                cmd.Parameters.AddWithValue("?in_market_value", lsMarketValue)
                cmd.Parameters.AddWithValue("?in_threshold_level", lsThreshold)
                cmd.Parameters.AddWithValue("?in_share_count", 0)
                cmd.Parameters.AddWithValue("?in_inward_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_dp_client_id", lsDpClientId)
                cmd.Parameters.AddWithValue("?in_action", lsAction)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_comp_inward_no", MySqlDbType.VarChar)
                cmd.Parameters("?out_comp_inward_no").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_inward_no", MySqlDbType.Int32)
                cmd.Parameters("?out_inward_no").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()
                lnInwardNo = Val(cmd.Parameters("?out_inward_no").Value.ToString())
                lnCompInwardNo = cmd.Parameters("?out_comp_inward_no").Value.ToString()

                If lnResult = 1 Then
                    If lnInwardId = 0 Then
                        MessageBox.Show("Inward No :" & lnInwardNo & ", New Inward No : " & lnCompInwardNo, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mnInwardId = lnInwardNo
                        'Modified by Mohan 05-06-2024
                        If cboDocType.Text = "OT-Others" Or cboDocType.Text = "TM-Transmission" Or cboDocType.Text = "AM-CA Allotment" Then
                            lnkAddAttachment.Enabled = True
                        Else
                            lnkAddAttachment.Enabled = False
                        End If

                        ' Send Email to Shareholder
                        If Not String.IsNullOrEmpty(lsShareHolderEmailId) Then
                            SendEmailToShareholder(lsShareHolderEmailId, lsShareHolderName, lnInwardNo, lnCompInwardNo)
                        End If

                        'Send SMS to Shareholder
                        If Not String.IsNullOrEmpty(lsShareHolderContactNo) Then
                            Dim SmsContent As String
                            SmsContent = gsInwardSmsContent
                            SmsContent = SmsContent.Replace("#recived_date", ldRcvdDate.ToString("yyyy-MM-dd"))
                            ' Get Inward GID for tracking
                            mnInwardGID = Val(gfExecuteScalar("SELECT inward_gid FROM sta_trn_tinward WHERE inward_no = " & lnInwardNo & " AND delete_flag = 'N'", gOdbcConn))
                            SendSms(mnInwardGID, lsShareHolderContactNo, SmsContent, gsSenderCode, gsTemplateid)
                        End If

                    Else
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        lnkAddAttachment.Enabled = False
                    End If

                    'If mnInwardId > 0 Then Me.Close()

                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using

            If cboDocType.Text <> "OT-Others" Or cboDocType.Text <> "TM-Transmission" Then
                Call ClearControl()
            End If
            DocSubType.DataSource = Nothing
            If mbGenerateInwardNo = True Then
                cboDocType.Focus()
            Else
                txtInwardNo.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub SendEmailToShareholder(ByVal emailTo As String, ByVal shareholdername As String, ByVal inwardNo As Integer, ByVal compInwardNo As String)
        Dim errorMessage As String = ""
        Dim deliveryStatus As String = "Not Delivered"
        Dim lsMailmessageBody As String = ""

        Try
            ' Validate email format before proceeding
            If Not IsValidEmail(emailTo) Then
                Throw New Exception("Invalid email format.")
            End If

            ' SMTP Configuration
            Dim smtpClient As New SmtpClient(gssmtpClient)
            smtpClient.Port = 587
            smtpClient.Credentials = New NetworkCredential(gssmtpClientUsername, gssmtpClientpswd)
            smtpClient.EnableSsl = True

            Dim mailMessage As New MailMessage()
            mailMessage.From = New MailAddress(gssmtpClientUsername)
            mailMessage.To.Add(emailTo)
            mailMessage.Subject = "Acknowledgement"
            mailMessage.IsBodyHtml = True

            ' Request delivery notification & read receipt
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure Or DeliveryNotificationOptions.Delay
            mailMessage.Headers.Add("Disposition-Notification-To", gssmtpClientUsername)

            If shareholdername.Trim() = "" Then
                shareholdername = " Sir/Madam "
            End If

            ' Compose Email Body
            'lsMailmessageBody = "<p>Dear " & shareholdername & ",</p>" & _
            '                    "<p><b>Inward No:</b> " & compInwardNo & "</p>" & _
            '                    "<p>We are in receipt of your communication on the captioned subject.</p>" & _
            '                    "<p>We shall review the information & revert to you in due course.</p>" & _
            '                    "<p><i><span style='color:red;'>This is a system-generated email. Please do not reply to this message.</i></p>" & _
            '                    "<p><br>Thank you. </p> " & _
            '                    "<p>Yours sincerely,</p>" & _
            '                    "<p><b>GNSA Infotech P Ltd</b> " & _
            '                    "<br>Nelson Chambers," & _
            '                    "<br>F - Block, 4th & 5th Floor," & _
            '                    "<br>No.115, Nelson Manickam Road," & _
            '                    "<br>Aminjikarai, Chennai - 600 030.</p>"

            lsMailmessageBody = "<p>Dear " & shareholdername & ",</p>" & _
                               "<p><b>Inward No:</b> " & compInwardNo & "</p>" & _
                               "<p>We are in receipt of your communication on the captioned subject.</p>" & _
                               "<p>We shall review the information & revert to you in due course.</p>" & _
                               "<p><i><span style='color:red;'>This is a system-generated email. Please do not reply to this message.</i></p>" & _
                               "<p><br>Thank you. </p> " & _
                               "<p>Yours sincerely,</p>" & _
                               "<p><b>STA DEPARTMENT," & _
                               "<br><b>GNSA Infotech P Ltd" & _
                               "<br><b>Nelson Chambers," & _
                               "<br><b>F - Block, 4th & 5th Floor," & _
                               "<br><b>No.115, Nelson Manickam Road," & _
                               "<br><b>Aminjikarai, Chennai - 600 030.</p>"

            mailMessage.Body = lsMailmessageBody

            ' Send the email
            smtpClient.Send(mailMessage)

            ' If successful, mark as delivered
            deliveryStatus = "Delivered"
            errorMessage = "Mail Sent Successfully"

        Catch ex As SmtpFailedRecipientException
            ' Specific failure related to recipient (invalid email, mailbox full, etc.)
            errorMessage = "Recipient failed: " & ex.FailedRecipient & " - " & ex.StatusCode.ToString() & " - " & ex.Message
            deliveryStatus = "Not Delivered"

        Catch ex As SmtpException
            ' General SMTP error (e.g., server issue, network error)
            errorMessage = "SMTP Error: " & ex.StatusCode.ToString() & " - " & ex.Message
            deliveryStatus = "Not Delivered"

        Catch ex As Exception
            ' Other unexpected errors
            errorMessage = "Error: " & ex.Message
            deliveryStatus = "Not Delivered"

        Finally
            ' Get Inward GID for tracking
            mnInwardGID = Val(gfExecuteScalar("SELECT inward_gid FROM sta_trn_tinward WHERE inward_no = " & inwardNo & " AND delete_flag = 'N'", gOdbcConn))

            ' Log the result in Mail History Table
            InsertMailHistory(mnInwardGID, emailTo, lsMailmessageBody, errorMessage, deliveryStatus)

            ' Show error message if failed
            If deliveryStatus = "Not Delivered" Then
                MessageBox.Show("Failed to send email: " & errorMessage, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub InsertMailHistory(inwardGID As Integer, email As String, mailContent As String, remarks As String, status As String)
        Try
            Using cmd As New MySqlCommand("pr_sta_trn_tmailhistory", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", inwardGID)
                cmd.Parameters.AddWithValue("?in_mail_id", email)
                cmd.Parameters.AddWithValue("?in_mail_content", mailContent)
                cmd.Parameters.AddWithValue("?in_remarks", remarks)
                cmd.Parameters.AddWithValue("?in_mail_status", status)
                cmd.Parameters.AddWithValue("?in_action", "INSERT")
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                ' Output Parameters
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error inserting mail history: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim mail As New System.Net.Mail.MailAddress(email)
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub SendEmailToShareholderDynamic(ByVal emailTo As String, ByVal shareholdername As String, ByVal inwardNo As Integer, ByVal compInwardNo As String)
        Try
            Dim cmd As MySqlCommand
            Dim da As MySqlDataAdapter
            Dim ds As System.Data.DataSet
            Dim dt As System.Data.DataTable
            Dim lsMailBody As String

            ' SMTP Configuration
            Dim smtpClient As New SmtpClient("smtp.gmail.com") ' Replace with your SMTP server
            smtpClient.Port = 587 ' Use the appropriate port (e.g., 465 for SSL, 587 for TLS)
            smtpClient.Credentials = New NetworkCredential("noreplysta@gnsaindia.com", "Gnsa@123456789")
            smtpClient.EnableSsl = True

            Dim mailMessage As New MailMessage()
            mailMessage.From = New MailAddress("mohanraja.s@flexicodeindia.com") ' Replace with sender email
            mailMessage.To.Add(emailTo)
            mailMessage.Subject = "Acknowledgement "

            ' Fetch email content from database
            cmd = New MySqlCommand("pr_sta_get_mailcontent", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_content_name", "inward_acknowledge")

            cmd.CommandTimeout = 0

            dt = New System.Data.DataTable
            ds = New System.Data.DataSet
            da = New MySqlDataAdapter(cmd)
            da.Fill(ds)
            dt = ds.Tables(0)

            ' Retrieve email content and replace variable
            If dt.Rows.Count > 0 Then
                lsMailBody = dt.Rows(0)("content_text").ToString() ' Get content from first row
                lsMailBody = lsMailBody.Replace("#inward_no", inwardNo)
                lsMailBody = lsMailBody.Replace("#comp_inward_no", compInwardNo)
                lsMailBody = lsMailBody.Replace("#share_holder_name", shareholdername)
            Else
                lsMailBody = "Dear Shareholder, Your inward request has been received successfully."
            End If

            ' Set email body
            mailMessage.Body = lsMailBody
            mailMessage.IsBodyHtml = True ' Set to True if your content has HTML formatting

            smtpClient.Send(mailMessage)

            MessageBox.Show("Email sent successfully to " & emailTo, "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Failed to send email: " & ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SendSms(ByVal TranId As Long, ByVal MobileNo As String, ByVal SmsTxt As String, ByVal SenderCode As String, ByVal SmsTemplateId As String) As Integer
        Dim wb As New WebClient
        'Dim url = "https://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=heYy2TmZoEmWpIGRbusETw&senderid=GNSAIN&channel=2&DCS=0&flashsms=0&number=919600016921&text=Test SMS&route=1"
        'Dim url = "https://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=heYy2TmZoEmWpIGRbusETw&senderid=GNSAIN&channel=2&DCS=0&flashsms=0&number=916380611603&text=Test SMS&route=1"
        'Dim url = msSmsUrl & "APIKey=" & msSmsApiKey & "&senderid=" & SenderCode & "&channel=2&DCS=0&flashsms=0&number=" & MobileNo & "&text=" & SmsTxt & "&route=" & msSmsRouteId
        'Dim url = "http://push.smsc.co.in/api/mt/SendSMS?APIkey=PJA3OFO9pkqgQx8s44AqsA&senderid=GNSAIN&channel=Trans&DCS=0&flashsms=0&number=916380611603&text=smstxt&route=47&dlttemplateid=GNSAIN"

        Dim url = gsApiUrl & "APIKey=" & gssmsApiKey & "&senderid=" & SenderCode & "&channel=Trans&DCS=0&flashsms=0&number=" & MobileNo & "&text=" & SmsTxt & "&route=" & gsRouteid & "&DLTTemplateId=" & SmsTemplateId

        Dim lsErrCode As String
        Dim lsErrmsg As String
        Dim lsJobId As String
        Dim lnDeliveredStatus As Integer
        Dim sResponse As String
        Dim parsejson As JObject

        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        ' Enable TLS 1.2 and TLS 1.3 (if supported)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or CType(12288, SecurityProtocolType)

        ' Attach the certificate validation callback
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate


        sResponse = GetResponse(url)
        parsejson = JObject.Parse(sResponse)

        lsErrCode = parsejson.SelectToken("ErrorCode").ToString()
        lsErrmsg = parsejson.SelectToken("ErrorMessage").ToString()
        lsJobId = parsejson.SelectToken("JobId").ToString()

        If lsErrCode = "000" Then
            InsertSMSHistory(mnInwardGID, MobileNo, SmsTxt, "SMS Sent Successfully", "Delivered")
        Else
            InsertSMSHistory(mnInwardGID, MobileNo, SmsTxt, lsErrmsg, "Not Delivered")
        End If

        Return lnDeliveredStatus
    End Function

    Public Shared Function GetResponse(ByVal sURL As String) As String
        Dim request As HttpWebRequest = CType(WebRequest.Create(sURL), HttpWebRequest)
        request.MaximumAutomaticRedirections = 4
        request.Credentials = CredentialCache.DefaultCredentials

        Try
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim receiveStream As Stream = response.GetResponseStream()
            Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.UTF8)
            Dim sResponse As String = readStream.ReadToEnd()
            response.Close()
            readStream.Close()
            Return sResponse
        Catch ex As Exception
            Return ex.ToString()
        End Try
    End Function

    Public Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub InsertSMSHistory(inwardGID As Integer, phoneno As String, smsContent As String, remarks As String, status As String)
        Try
            Using cmd As New MySqlCommand("pr_sta_trn_tsmshistory", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", inwardGID)
                cmd.Parameters.AddWithValue("?in_phone_no", phoneno)
                cmd.Parameters.AddWithValue("?in_sms_content", smsContent)
                cmd.Parameters.AddWithValue("?in_remarks", remarks)
                cmd.Parameters.AddWithValue("?in_sms_status", status)
                cmd.Parameters.AddWithValue("?in_action", "INSERT")
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                ' Output Parameters
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error inserting sms history: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtInwardNo.Focus()
    End Sub

    Private Sub txtRemark_GotFocus(sender As Object, e As EventArgs) Handles txtRemark.GotFocus
        KeyPreview = False
    End Sub

    Private Sub txtRemark_LostFocus(sender As Object, e As EventArgs) Handles txtRemark.LostFocus
        KeyPreview = True
    End Sub

    Private Sub txtRemark_TextChanged(sender As Object, e As EventArgs) Handles txtRemark.TextChanged

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        mnInwardId = 0
        cboDocSubType.Enabled = False
        lnkAddAttachment.Enabled = False
        txtMarketPrice.Text = "1"
        DocSubType.DataSource = Nothing
    End Sub

    Private Sub txtFolioNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFolioNo.Validating
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lnCompId As Long = 0
        Dim lsFolioNo As String

        If btnSave.Visible = True And txtShareHolderName.Text = "" Then
            If cboCompany.SelectedIndex <> -1 And cboCompany.Text <> "" Then lnCompId = Val(cboCompany.SelectedValue.ToString)
            lsFolioNo = QuoteFilter(txtFolioNo.Text)

            If lnCompId = 0 Then lsFolioNo = ""

            cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_folio_gid", 0)
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)

            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                txtShareHolderName.Text = dt.Rows(0).Item("holder1_name").ToString
                txtPanNo.Text = dt.Rows(0).Item("holder1_pan_no").ToString
                txtInwardShareCount.Text = dt.Rows(0).Item("folio_shares").ToString
            End If

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
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolderName.Text = .Rows(0).Item("holder1_name").ToString
                txtPanNo.Text = .Rows(0).Item("holder1_pan_no").ToString
                txtInwardShareCount.Text = .Rows(0).Item("folio_shares").ToString
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()
        Call Marketvaluecalc()
    End Sub

    Private Sub btnSearchFolio_Click(sender As Object, e As EventArgs) Handles btnSearchFolio.Click
        Dim frm As frmFolioSearch
        Dim lnCompId As Long = 0

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        End If

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            gsDocType = cboDocType.SelectedValue.ToString
        End If

        If gsDocType = "" Then
            MessageBox.Show("Kindly select the document type", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        frm = New frmFolioSearch(lnCompId)
        frm.ShowDialog()

        If gnSearchId <> 0 Then
            Call LoadFolio(gnSearchId)
        End If
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDocType.SelectedIndexChanged
        Dim lsSql As String
        'Modified by Mohan 05-06-2024
        If cboDocType.Text = "OT-Others" Or cboDocType.Text = "TM-Transmission" Or cboDocType.Text = "AM-CA Allotment" Then
            cboDocSubType.Visible = True
            cboDocSubType.Enabled = True

            ' doc sub type
            lsSql = ""
            lsSql &= " select docsubtype_code,docsubtype_desc from sta_mst_tdocsubtype "
            lsSql &= " where delete_flag = 'N' and trantype_code = '" & cboDocType.SelectedValue.ToString & "' "
            lsSql &= " order by docsubtype_code asc "

            Call gpBindCombo(lsSql, "docsubtype_desc", "docsubtype_code", cboDocSubType, gOdbcConn)

            cboDocSubType.Focus()
            DocSubType.DataSource = Nothing
            DocSubType.Enabled = False

        ElseIf cboDocType.Text = "CD-Correspondence" Then
            'DocSubType.Visible = True
            DocSubType.Enabled = True
            cboDocSubType.Visible = False
            'List of DocSubType
            Dim dt1 As DataTable = GetDocsubtypeDT()

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                'Assign DataTable as DataSource.
                DocSubType.DataSource = dt1
                DocSubType.DisplayMember = "docsubtype_desc"
                DocSubType.ValueMember = "docsubtype_code"
            End If
            DocSubType.CheckOnClick = True
        Else
            cboDocSubType.Text = ""
            cboDocSubType.SelectedIndex = -1
            cboDocSubType.Enabled = False
            cboDocSubType.Visible = True
            DocSubType.Enabled = False
        End If

    End Sub

    Private Sub lnkAddAttachment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAttachment.LinkClicked
        Dim frm As New frmAttachmentAdd(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs)
        Dim frm As New frmDocHistory(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtMarketPrice_TextChanged(sender As Object, e As EventArgs) Handles txtMarketPrice.TextChanged
        Call Marketvaluecalc()
    End Sub

    Private Sub txtInwardShareCount_TextChanged(sender As Object, e As EventArgs) Handles txtInwardShareCount.TextChanged
        Call Marketvaluecalc()
    End Sub

    Private Sub Marketvaluecalc()
        Dim lsShareCount As Long
        Dim lsMarketprice As Decimal
        Dim lsMarketValue As Decimal
        Dim lsThreshold As Long

        lsShareCount = Val(txtInwardShareCount.Text)
        lsMarketprice = Val(txtMarketPrice.Text)
        lsMarketValue = Convert.ToDecimal(lsShareCount * lsMarketprice)

        txtMarketValue.Text = lsMarketValue
        'Get Thresold value from config table
        lsThreshold = gnInwardThresholdValue
        If lsMarketValue > lsThreshold Then
            cboThreshold.Text = "High"
        Else
            cboThreshold.Text = "Low"
        End If
    End Sub

End Class