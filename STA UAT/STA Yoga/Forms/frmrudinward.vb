Imports MySql.Data.MySqlClient
Public Class frmrudinward
    Dim msMode As String
    Dim mnInwardId As Long
    Dim mbGenerateInwardNo As Boolean = True

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String


        ' doc type
        With cboDocType
            .Items.Clear()
            .Items.Add("Return Undelivered")
            .Items.Add("Correspondence")
        End With

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

        'Received By
        lsSql = ""
        lsSql &= " select user_code,user_name from soft_mst_tuser "
        lsSql &= " where delete_flag='N' "
        lsSql &= " Order by user_name asc "

        Call gpBindCombo(lsSql, "user_name", "user_code", CboRecvby, gOdbcConn)

        dtpRcvdDate.Value = Now
        txtInwardNo.Enabled = False
        Call ClearControl()

        'If mnInwardId > 0 Then
        '    Call ListAll(mnInwardId)
        'End If
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboDocType.SelectedIndexChanged
        cboSubType.Items.Clear()
        cboSubType.Text = String.Empty
        Dim doctype As Integer
        doctype = cboDocType.SelectedIndex
        If doctype = 0 Then
            With cboSubType
                .Items.Clear()
                .Items.Add("Allotment")
                .Items.Add("Transfers")
                .Items.Add("New Share Certificate")
                .Items.Add("Demat Rejection")
                .Items.Add("Alteration")
            End With
        Else
            With cboSubType
                .Items.Clear()
                .Items.Add("PA-POWER OF ATTORNEY")
                .Items.Add("MN-MANDATE INSTRUCTIONS")
                .Items.Add("DR-REVALIDATION OF DIVIDEND WARRANT(S)")
                .Items.Add("EN-CALL MONEY ENDORSEMENT ON SHARE CERTIFICATE(S)")
                .Items.Add("RE-RECTIFICATION OF SHARE CERTIFICATE(S)")
                .Items.Add("LN-MARKING LIEN ON SHARES")
                .Items.Add("NC-NAME CORRECTION")
                .Items.Add("DW-ISSUE OF DUPLICATE DIVIDEND WARRANT")
                .Items.Add("VS-VERIFICATION OF SIGNATURE")
                .Items.Add("DD-DIVIDEND WARRANT CORRECTION")
                .Items.Add("FR-FINANCIAL RESULTS")
                .Items.Add("MU-MUTILATED SHARE CERTIFICATE")
                .Items.Add("RA-MARKET PRICE")
                .Items.Add("CI-COMPANY DETAILS")
                .Items.Add("EC-ECS INSTRUCTION")
                .Items.Add("SD-SHARE HOLDING DETAILS")
                .Items.Add("AS-AUTHORISED SIGNATURE")
                .Items.Add("DI-DIVIDEND DETAILS")
                .Items.Add("MS-CHANGE OF SIGNATURE")
                .Items.Add("EM-REGISTRATION OF EMAIL ID")
                .Items.Add("RV-NON RECEIPT OF REFUND ORDER AFTER REVALIDATION")
                .Items.Add("CT-NON RECEIPT OF SHARE CERTIFICATE(S) AFTER TRANSFER")
                .Items.Add("CE-NON RECEIPT OF SHARE CERTIFICATE(S) AFTER ENDORSEMENT")
                .Items.Add("CS-NON RECEIPT OF CERTIFICATE(S) AFTER SPLIT/CONSOLIDATION")
                .Items.Add("DW-NON RECEIPT OF DIVIDEND WARRANT(S)")
                .Items.Add("DV-NON RECEIPT OF DIVIDEND WARRANT(S) AFTER REVALIDATION")
                .Items.Add("NA-NON RECEIPT OF ANNUAL REPORTS")
                .Items.Add("CM-NON RECEIPT OF SHARE CERTIFICATE DULY REPLACED")
                .Items.Add("CD-NON RECEIPT OF DUPLICATE CERTIFICATE")
                .Items.Add("EC-ECS QUERIES")
                .Items.Add("DT-NON RECEIPT OF DEMAT REJECTION")
                .Items.Add("BR-REQUEST FOR REMATERIALISATION")
                .Items.Add("DC-DEMAT CREDIT PENDING")
            End With
        End If

    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnInwardId As Long
        Dim lnInwardNo As Long
        Dim lnCompId As Long
        Dim lsDocType As String
        Dim ldRcvdDate As Date
        Dim lnCourierId As Long
        Dim lsAwbNo As String
        Dim lsCertificateno As String
        Dim lsSubType As String
        Dim lsFolioNo As String
        Dim lsShareCount As Long
        Dim lsReceivedby As String
        Dim lsReason As String
        Dim lsAction As String

        Try
            If cboCompany.SelectedIndex <> -1 Then
                lnCompId = Val(cboCompany.SelectedValue.ToString)
            Else
                lnCompId = 0
            End If

            lnInwardNo = Val(QuoteFilter(txtInwardNo.Text))

            'If mnInwardId = 0 Then
            '    If mbGenerateInwardNo = True Then
            '        If lnInwardNo > 0 Then
            '            MessageBox.Show("Inward no is should be generated automatically !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '            cboDocType.Focus()
            '            Exit Sub
            '        End If
            '    Else
            '        If lnInwardNo = 0 Then
            '            MessageBox.Show("Inward no cannot be zero !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '            txtInwardNo.Focus()
            '            Exit Sub
            '        End If
            '    End If
            'End If

            If cboDocType.SelectedIndex <> -1 Then
                lsDocType = cboDocType.Text
            Else
                lsDocType = ""
            End If

            ldRcvdDate = dtpRcvdDate.Value

            If cboCourier.SelectedIndex <> -1 Then
                lnCourierId = Val(cboCourier.SelectedValue.ToString)
            Else
                lnCourierId = 0
            End If

            lsAwbNo = QuoteFilter(txtAwbNo.Text)

            If cboSubType.SelectedIndex <> -1 Then
                lsSubType = cboSubType.Text
            Else
                lsSubType = ""
            End If

            If CboRecvby.SelectedIndex <> -1 Then
                lsReceivedby = CboRecvby.SelectedValue.ToString
            Else
                lsReceivedby = ""
            End If

            lsFolioNo = QuoteFilter(txtFolioNo.Text)
            lsCertificateno = QuoteFilter(txtCertificateNo.Text)
            lsShareCount = Val(txtSharecount.Text)
            lsReason = QuoteFilter(txtReason.Text)


            lnInwardId = Val(txtId.Text)

            If lnInwardId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_ins_correspondence", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_corres_gid", lnInwardId)
                cmd.Parameters.AddWithValue("?in_corres_slno", lnInwardNo)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_certificate_no", lsCertificateno)
                cmd.Parameters.AddWithValue("?in_share_count", lsShareCount)
                cmd.Parameters.AddWithValue("?in_document_type", lsDocType)
                cmd.Parameters.AddWithValue("?in_corres_subtype", lsSubType)
                cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                cmd.Parameters.AddWithValue("?in_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_awb_no", lsAwbNo)
                cmd.Parameters.AddWithValue("?in_return_date", Format(ldRcvdDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_return_received_by", lsReceivedby)
                cmd.Parameters.AddWithValue("?in_return_reason", lsReason)
                cmd.Parameters.AddWithValue("?in_entry_by", gsLoginUserCode)
                cmd.Parameters.AddWithValue("?in_action", lsAction)

                'Out put Para
                cmd.Parameters.Add("?out_corres_slno", MySqlDbType.Int32)
                cmd.Parameters("?out_corres_slno").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()
                lnInwardNo = Val(cmd.Parameters("?out_corres_slno").Value.ToString())

                If lnResult = 1 Then
                    If lnInwardId = 0 Then
                        MessageBox.Show("New Inward No : " & lnInwardNo, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    'If mnInwardId > 0 Then Me.Close()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using

            Call ClearControl()
            cboCompany.SelectedItem = Nothing
            cboCourier.SelectedItem = Nothing
            CboRecvby.SelectedItem = Nothing


            If mbGenerateInwardNo = True Then
                cboDocType.Focus()
            Else
                txtInwardNo.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtInwardNo.Focus()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub
End Class