Imports MySql.Data.MySqlClient

Public Class frmCompanyMaster

    Private Sub frmCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call EnableSave(False)
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Call ClearControl()
        Call EnableSave(True)
        txtFolioNoFormat.Text = "8"
        txtCertSlno.Text = "1"
        txtFolioSlno.Text = "1"
        txtFolioTranSlno.Text = "1"
        txtInwardSlno.Text = "1"
        txtObjxSlno.Text = "1"
        txtUploadSlno.Text = "1"
        txtShareCapital.Text = "0"
        txtCompanyCode.Focus()
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtCompanyCode.Focus()
    End Sub

    Public Sub frmCtrClear(ByVal frmName As Object)
        Dim ctrl As Control
        Dim dtp As DateTimePicker

        For Each ctrl In frmName.Controls
            If ctrl.Tag <> "*" Then
                If TypeOf ctrl Is TextBox Then ctrl.Text = ""
                If TypeOf ctrl Is ComboBox Then
                    ctrl.Text = ""
                End If

                If TypeOf ctrl Is DateTimePicker Then
                    dtp = ctrl

                    If dtp.ShowCheckBox = True Then
                        dtp.Checked = False
                    End If
                End If

                If TypeOf ctrl Is Panel Then frmCtrClear(ctrl)
                If TypeOf ctrl Is GroupBox Then frmCtrClear(ctrl)
            End If
        Next
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Call ClearControl()
        Call EnableSave(False)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If txtid.Text = "" Then
                If MsgBox("Select Record to edit", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                    EnableSave(False)
                End If
            Else
                EnableSave(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            'SearchDialog = New frmSearch(gOdbcConn, "select comp_gid as 'Comp Id'," & _
            '"entity_gid as 'Entity Id',comp_code as 'Company Code',comp_short_code as 'Company Short Code',comp_name as 'Company Name'," & _
            '"isin_id as 'Isin Id',folio_no_format as 'Folio No Format',folio_prefix_flag as 'Folio Prefix Flag',folio_prefix_sno_flag as 'Folio Prefix Slno Flag', " & _
            '"folio_prefix as 'Folio Prefix',folio_prefix_field as 'Folio Prefix Field',folio_prefix_length as 'Folio Prefix Length',upload_sno as 'Upload Sno', " & _
            '"folio_sno as 'Folio Sno',transfer_sno as 'Transfer Sno',cert_sno as 'Certificate Sno',objx_sno as 'Objx Sno',inward_sno as 'Inward Sno',comp_listed as 'Comp Listed',active_flag as 'Active Flag',share_captial as 'Share Capital' FROM sta_mst_tcompany ", _
            '"comp_gid,entity_gid,comp_code,comp_short_code,comp_name,isin_id,folio_no_format,folio_prefix_flag,folio_prefix_sno_flag,folio_prefix,folio_prefix_field,folio_prefix_length,upload_sno,folio_sno,transfer_sno,cert_sno,objx_sno,inward_sno,comp_listed,active_flag,share_captial", " 1 = 1 and delete_flag = 'N' ")
            'SearchDialog.ShowDialog()

            SearchDialog = New frmSearch(gOdbcConn, "select comp_gid as 'Comp Id'," & _
           "entity_gid as 'Entity Id',comp_code as 'Company Code',comp_short_code as 'Company Short Code',comp_name as 'Company Name'," & _
           "isin_id as 'Isin Id',folio_no_format as 'Folio No Format',folio_prefix_flag as 'Folio Prefix Flag',folio_prefix_sno_flag as 'Folio Prefix Slno Flag',electronics_flag as 'Electronics Flag', " & _
           "folio_prefix as 'Folio Prefix',folio_prefix_field as 'Folio Prefix Field',folio_prefix_length as 'Folio Prefix Length',upload_sno as 'Upload Sno', " & _
           "folio_sno as 'Folio Sno',transfer_sno as 'Transfer Sno',cert_sno as 'Certificate Sno',objx_sno as 'Objx Sno',inward_sno as 'Inward Sno',comp_listed as 'Comp Listed',active_flag as 'Active Flag',share_captial as 'Share Capital',share_type as 'Security Type',cin_no as 'Cin No',pan_no as 'Pan No',share_qty as 'Share Quantity',paid_up_value as 'Paid Up Value',address1 as Address1,address2 as Address2,address3 as Address3,city as City,state as State,country as Country, pincode as Pincode FROM sta_mst_tcompany ", _
           "comp_gid,entity_gid,comp_code,comp_short_code,comp_name,isin_id,folio_no_format,folio_prefix_flag,folio_prefix_sno_flag,folio_prefix,folio_prefix_field,folio_prefix_length,upload_sno,folio_sno,transfer_sno,cert_sno,objx_sno,inward_sno,comp_listed,active_flag,share_captial," & _
           "share_type,share_qty,paid_up_value,address1,address2,address3,city,state,country, pincode ", _
           " 1 = 1 and delete_flag = 'N' ")
            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tcompany " _
                    & "where comp_gid = " & gnSearchId & " " _
                    & "and delete_flag = 'N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySqlConnection)
        Dim lobjDataReader As MySqlDataReader
        Dim lsFolioPrefixFlag As String
        Dim lsFolioPrefixSnoFlag As String
        Dim lsElectronicsFlag As String
        Dim lsCompListed As String
        Dim lsActiveFlag As String

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            If lobjDataReader.HasRows Then
                If lobjDataReader.Read Then
                    txtid.Text = lobjDataReader.Item("comp_gid").ToString
                    txtCompanyCode.Text = lobjDataReader.Item("comp_code").ToString
                    txtCompShortCode.Text = lobjDataReader.Item("comp_short_code").ToString
                    txtCompanyName.Text = lobjDataReader.Item("comp_name").ToString
                    txtIsinId.Text = lobjDataReader.Item("isin_id").ToString
                    txtFolioNoFormat.Text = lobjDataReader.Item("folio_no_format").ToString
                    txtFolioPrefix.Text = lobjDataReader.Item("folio_prefix").ToString
                    txtFolioPrefixField.Text = lobjDataReader.Item("folio_prefix_field").ToString
                    txtUploadSlno.Text = lobjDataReader.Item("upload_sno").ToString
                    txtFolioSlno.Text = lobjDataReader.Item("folio_sno").ToString
                    txtFolioTranSlno.Text = lobjDataReader.Item("transfer_sno").ToString
                    txtCertSlno.Text = lobjDataReader.Item("cert_sno").ToString
                    txtObjxSlno.Text = lobjDataReader.Item("objx_sno").ToString
                    txtInwardSlno.Text = lobjDataReader.Item("inward_sno").ToString
                    txtShareCapital.Text = lobjDataReader.Item("share_captial").ToString

                    txtShareQty.Text = lobjDataReader.Item("share_qty").ToString
                    txtSecurityType.Text = lobjDataReader.Item("share_type").ToString
                    txtPaidupValue.Text = lobjDataReader.Item("paid_up_value").ToString
                    txtAddress1.Text = lobjDataReader.Item("address1").ToString
                    txtAddress2.Text = lobjDataReader.Item("address2").ToString
                    txtAddress3.Text = lobjDataReader.Item("address3").ToString
                    txtCity.Text = lobjDataReader.Item("city").ToString
                    txtState.Text = lobjDataReader.Item("state").ToString
                    txtCountry.Text = lobjDataReader.Item("country").ToString
                    txtPincode.Text = lobjDataReader.Item("pincode").ToString
                    txtPanNo.Text = lobjDataReader.Item("pan_no").ToString
                    txtCinNo.Text = lobjDataReader.Item("cin_no").ToString
                    lsFolioPrefixFlag = lobjDataReader.Item("folio_prefix_flag").ToString
                    lsFolioPrefixSnoFlag = lobjDataReader.Item("folio_prefix_sno_flag").ToString
                    lsCompListed = lobjDataReader.Item("comp_listed").ToString
                    lsActiveFlag = lobjDataReader.Item("active_flag").ToString
                    lsElectronicsFlag = lobjDataReader.Item("electronics_flag").ToString

                    If lsFolioPrefixFlag.ToString = "Y" Then
                        RbdprefixYes.Checked = True
                    ElseIf lsFolioPrefixFlag.ToString = "N" Then
                        RbdprefixNo.Checked = True
                    End If

                    If lsFolioPrefixSnoFlag.ToString = "Y" Then
                        RbdprefixSnoYes.Checked = True
                    ElseIf lsFolioPrefixSnoFlag.ToString = "N" Then
                        RbdprefixSnoNo.Checked = True
                    End If

                    If lsCompListed.ToString = "Y" Then
                        RbdCompYes.Checked = True
                    ElseIf lsCompListed.ToString = "N" Then
                        RbdCompNo.Checked = True
                    End If

                    If lsActiveFlag.ToString = "Y" Then
                        Rbdactiveyes.Checked = True
                    ElseIf lsActiveFlag.ToString = "N" Then
                        RbdActiveNo.Checked = True
                    End If

                    If lsElectronicsFlag.ToString = "Y" Then
                        rb_electronics_yes.Checked = True
                    ElseIf lsElectronicsFlag.ToString = "N" Then
                        rb_electronics_no.Checked = True
                    End If
                End If
            End If

            lobjDataReader.Close()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnCompId As Long
        Dim lsCompCode As String
        Dim lsCompShortCode As String
        Dim lsCompName As String
        Dim lsIsinId As String
        Dim lsFolioNoFormat As Long 
        Dim lsFolioPrefixFlag As String
        Dim lsFolioPrefixSnoFlag As String
        Dim lsFolioPrefix As String
        Dim lsFolioPrefixField As String
        Dim lsUploadSno As Long
        Dim lsFolioSno As Long
        Dim lsTransferSno As Long
        Dim lsCertSno As Long
        Dim lsObjxSno As Long
        Dim lsInwardSno As Long
        Dim lsCompListed As String
        Dim lsActiveFlag As String
        Dim lsShareCapital As Long
        Dim lsShareQty As Long
        Dim lsPaidupValue As Long
        Dim lsSecurityType As String
        Dim lsAddress1 As String
        Dim lsAddress2 As String
        Dim lsAddress3 As String
        Dim lsElectronicsFlag As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsPanno As String
        Dim lscinno As String
        Dim lsAction As String

        Try
            lsCompCode = QuoteFilter(txtCompanyCode.Text)
            lsCompShortCode = QuoteFilter(txtCompShortCode.Text)
            lnCompId = Val(txtid.Text)
            lsCompName = QuoteFilter(txtCompanyName.Text)
            lsIsinId = QuoteFilter(txtIsinId.Text)
            lsFolioNoFormat = QuoteFilter(txtFolioNoFormat.Text)
            lsFolioPrefix = QuoteFilter(txtFolioPrefix.Text)
            lsFolioPrefixField = QuoteFilter(txtFolioPrefixField.Text)
            lsUploadSno = QuoteFilter(txtUploadSlno.Text)
            lsFolioSno = QuoteFilter(txtFolioSlno.Text)
            lsTransferSno = QuoteFilter(txtFolioTranSlno.Text)
            lsCertSno = QuoteFilter(txtCertSlno.Text)
            lsObjxSno = QuoteFilter(txtObjxSlno.Text)
            lsInwardSno = QuoteFilter(txtInwardSlno.Text)

            lsShareQty = QuoteFilter(txtShareQty.Text)
            lsPaidupValue = QuoteFilter(txtPaidupValue.Text)
            lsSecurityType = QuoteFilter(txtSecurityType.Text)
            lsAddress1 = QuoteFilter(txtAddress1.Text)
            lsAddress2 = QuoteFilter(txtAddress2.Text)
            lsAddress3 = QuoteFilter(txtAddress3.Text)
            lsCity = QuoteFilter(txtCity.Text)
            lsState = QuoteFilter(txtState.Text)
            lsCountry = QuoteFilter(txtCountry.Text)
            lsPincode = QuoteFilter(txtPincode.Text)
            lsPanno = QuoteFilter(txtPanNo.Text)
            lscinno = QuoteFilter(txtCinNo.Text)

            If RbdprefixYes.Checked = True Then
                lsFolioPrefixFlag = "Y"
            ElseIf RbdprefixNo.Checked = True Then
                lsFolioPrefixFlag = "N"
            End If

            If RbdprefixSnoYes.Checked = True Then
                lsFolioPrefixSnoFlag = "Y"
            ElseIf RbdprefixSnoNo.Checked = True Then
                lsFolioPrefixSnoFlag = "N"
            End If

            If RbdCompYes.Checked = True Then
                lsCompListed = "Y"
            ElseIf RbdCompNo.Checked = True Then
                lsCompListed = "N"
            End If

            If Rbdactiveyes.Checked = True Then
                lsActiveFlag = "Y"
            ElseIf RbdActiveNo.Checked = True Then
                lsActiveFlag = "N"
            End If

            If rb_electronics_yes.Checked = True Then
                lsElectronicsFlag = "Y"
            ElseIf rb_electronics_no.Checked = True Then
                lsElectronicsFlag = "N"
            End If

            lsShareCapital = QuoteFilter(txtShareCapital.Text)

            If lnCompId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_mst_tcompany", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_entity_gid", 1)
                cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                cmd.Parameters.AddWithValue("?in_comp_short_code", lsCompShortCode)
                cmd.Parameters.AddWithValue("?in_comp_name", lsCompName)
                cmd.Parameters.AddWithValue("?in_isin_id", lsIsinId)
                cmd.Parameters.AddWithValue("?in_folio_no_format", lsFolioNoFormat)
                cmd.Parameters.AddWithValue("?in_folio_prefix_flag", lsFolioPrefixFlag)
                cmd.Parameters.AddWithValue("?in_electronics_flag", lsElectronicsFlag)
                cmd.Parameters.AddWithValue("?in_folio_prefix_sno_flag", lsFolioPrefixSnoFlag)
                cmd.Parameters.AddWithValue("?in_folio_prefix", lsFolioPrefix)
                cmd.Parameters.AddWithValue("?in_folio_prefix_field", lsFolioPrefixField)
                cmd.Parameters.AddWithValue("?in_upload_sno", lsUploadSno)
                cmd.Parameters.AddWithValue("?in_folio_sno", lsFolioSno)
                cmd.Parameters.AddWithValue("?in_transfer_sno", lsTransferSno)
                cmd.Parameters.AddWithValue("?in_cert_sno", lsCertSno)
                cmd.Parameters.AddWithValue("?in_objx_sno", lsObjxSno)
                cmd.Parameters.AddWithValue("?in_inward_sno", lsInwardSno)
                cmd.Parameters.AddWithValue("?in_comp_listed", lsCompListed)
                cmd.Parameters.AddWithValue("?in_active_flag", lsActiveFlag)
                cmd.Parameters.AddWithValue("?in_share_captial", lsShareCapital)
                cmd.Parameters.AddWithValue("?in_security_type", lsSecurityType)
                cmd.Parameters.AddWithValue("?in_share_qty", lsShareQty)
                cmd.Parameters.AddWithValue("?in_paid_up_value", lsPaidupValue)
                cmd.Parameters.AddWithValue("?in_address1", lsAddress1)
                cmd.Parameters.AddWithValue("?in_address2", lsAddress2)
                cmd.Parameters.AddWithValue("?in_address3", lsAddress3)
                cmd.Parameters.AddWithValue("?in_city", lsCity)
                cmd.Parameters.AddWithValue("?in_state", lsState)
                cmd.Parameters.AddWithValue("?in_country", lsCountry)
                cmd.Parameters.AddWithValue("?in_pincode", lsPincode)
                cmd.Parameters.AddWithValue("?in_pan_no", lsPanno)
                cmd.Parameters.AddWithValue("?in_cin_no", lscinno)
                cmd.Parameters.AddWithValue("?in_action", lsAction)
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
                    Exit Sub
                End If
            End Using

            Call ClearControl()

            If MsgBox("Do you want to add another record ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
                btnNew.PerformClick()
            Else
                Call EnableSave(False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnCompId As Long
        Dim lsCompCode As String
        Dim lsCompShortCode As String
        Dim lsCompName As String
        Dim lsIsinId As String
        Dim lsFolioNoFormat As Long
        Dim lsFolioPrefixFlag As String
        Dim lsFolioPrefixSnoFlag As String
        Dim lsFolioPrefix As String
        Dim lsFolioPrefixField As String
        Dim lsUploadSno As Long
        Dim lsFolioSno As Long
        Dim lsTransferSno As Long
        Dim lsCertSno As Long
        Dim lsObjxSno As Long
        Dim lsInwardSno As Long
        Dim lsCompListed As String
        Dim lsActiveFlag As String
        Dim lsShareCapital As Long
        Dim lsShareQty As Long
        Dim lsPaidupValue As Long
        Dim lsSecurityType As String
        Dim lsAddress1 As String
        Dim lsAddress2 As String
        Dim lsAddress3 As String
        Dim lsElectronicsFlag As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsPanno As String
        Dim lsCinno As String
        Dim lsAction As String
        Try
            If txtid.Text = "" Then
                If MsgBox("Select record to delete?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                End If
            Else
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then

                    lsCompCode = QuoteFilter(txtCompanyCode.Text)
                    lsCompShortCode = QuoteFilter(txtCompShortCode.Text)
                    lnCompId = Val(txtid.Text)
                    lsCompName = QuoteFilter(txtCompanyName.Text)
                    lsIsinId = QuoteFilter(txtIsinId.Text)
                    lsFolioNoFormat = QuoteFilter(txtFolioNoFormat.Text)
                    lsFolioPrefix = QuoteFilter(txtFolioPrefix.Text)
                    lsFolioPrefixField = QuoteFilter(txtFolioPrefixField.Text)
                    lsUploadSno = QuoteFilter(txtUploadSlno.Text)
                    lsFolioSno = QuoteFilter(txtFolioSlno.Text)
                    lsTransferSno = QuoteFilter(txtFolioTranSlno.Text)
                    lsCertSno = QuoteFilter(txtCertSlno.Text)
                    lsObjxSno = QuoteFilter(txtObjxSlno.Text)
                    lsInwardSno = QuoteFilter(txtInwardSlno.Text)
                    lsShareCapital = QuoteFilter(txtShareCapital.Text)

                    lsShareQty = QuoteFilter(txtShareQty.Text)
                    lsPaidupValue = QuoteFilter(txtPaidupValue.Text)
                    lsSecurityType = QuoteFilter(txtSecurityType.Text)
                    lsAddress1 = QuoteFilter(txtAddress1.Text)
                    lsAddress2 = QuoteFilter(txtAddress2.Text)
                    lsAddress3 = QuoteFilter(txtAddress3.Text)
                    lsCity = QuoteFilter(txtCity.Text)
                    lsState = QuoteFilter(txtState.Text)
                    lsCountry = QuoteFilter(txtCountry.Text)
                    lsPincode = QuoteFilter(txtPincode.Text)
                    lsPanno = QuoteFilter(txtPanNo.Text)
                    lsCinno = QuoteFilter(txtCinNo.Text)

                    If RbdprefixYes.Checked = True Then
                        lsFolioPrefixFlag = "Y"
                    ElseIf RbdprefixNo.Checked = True Then
                        lsFolioPrefixFlag = "N"
                    End If

                    If RbdprefixSnoYes.Checked = True Then
                        lsFolioPrefixSnoFlag = "Y"
                    ElseIf RbdprefixSnoNo.Checked = True Then
                        lsFolioPrefixSnoFlag = "N"
                    End If

                    If RbdCompYes.Checked = True Then
                        lsCompListed = "Y"
                    ElseIf RbdCompNo.Checked = True Then
                        lsCompListed = "N"
                    End If

                    If Rbdactiveyes.Checked = True Then
                        lsActiveFlag = "Y"
                    ElseIf RbdActiveNo.Checked = True Then
                        lsActiveFlag = "N"
                    End If

                    If rb_electronics_yes.Checked = True Then
                        lsElectronicsFlag = "Y"
                    ElseIf rb_electronics_no.Checked = True Then
                        lsElectronicsFlag = "N"
                    End If

                    If lnCompId = 0 Then
                        lsAction = "INSERT"
                    Else
                        lsAction = "DELETE"
                    End If

                    Using cmd As New MySqlCommand("pr_sta_mst_tcompany", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                        cmd.Parameters.AddWithValue("?in_entity_gid", 1)
                        cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                        cmd.Parameters.AddWithValue("?in_comp_short_code", lsCompShortCode)
                        cmd.Parameters.AddWithValue("?in_comp_name", lsCompName)
                        cmd.Parameters.AddWithValue("?in_isin_id", lsIsinId)
                        cmd.Parameters.AddWithValue("?in_folio_no_format", lsFolioNoFormat)
                        cmd.Parameters.AddWithValue("?in_folio_prefix_flag", lsFolioPrefixFlag)
                        cmd.Parameters.AddWithValue("?in_electronics_flag", lsElectronicsFlag)
                        cmd.Parameters.AddWithValue("?in_folio_prefix_sno_flag", lsFolioPrefixSnoFlag)
                        cmd.Parameters.AddWithValue("?in_folio_prefix", lsFolioPrefix)
                        cmd.Parameters.AddWithValue("?in_folio_prefix_field", lsFolioPrefixField)
                        cmd.Parameters.AddWithValue("?in_upload_sno", lsUploadSno)
                        cmd.Parameters.AddWithValue("?in_folio_sno", lsFolioSno)
                        cmd.Parameters.AddWithValue("?in_transfer_sno", lsTransferSno)
                        cmd.Parameters.AddWithValue("?in_cert_sno", lsCertSno)
                        cmd.Parameters.AddWithValue("?in_objx_sno", lsObjxSno)
                        cmd.Parameters.AddWithValue("?in_inward_sno", lsInwardSno)
                        cmd.Parameters.AddWithValue("?in_comp_listed", lsCompListed)
                        cmd.Parameters.AddWithValue("?in_active_flag", lsActiveFlag)
                        cmd.Parameters.AddWithValue("?in_share_captial", lsShareCapital)
                        cmd.Parameters.AddWithValue("?in_security_type", lsSecurityType)
                        cmd.Parameters.AddWithValue("?in_share_qty", lsShareQty)
                        cmd.Parameters.AddWithValue("?in_paid_up_value", lsPaidupValue)
                        cmd.Parameters.AddWithValue("?in_address1", lsAddress1)
                        cmd.Parameters.AddWithValue("?in_address2", lsAddress2)
                        cmd.Parameters.AddWithValue("?in_address3", lsAddress3)
                        cmd.Parameters.AddWithValue("?in_city", lsCity)
                        cmd.Parameters.AddWithValue("?in_state", lsState)
                        cmd.Parameters.AddWithValue("?in_country", lsCountry)
                        cmd.Parameters.AddWithValue("?in_pincode", lsPincode)
                        cmd.Parameters.AddWithValue("?in_pan_no", lsPanno)
                        cmd.Parameters.AddWithValue("?in_cin_no", lscinno)
                        cmd.Parameters.AddWithValue("?in_action", lsAction)
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
                            Exit Sub
                        End If
                    End Using

                    Call ClearControl()
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
End Class