Imports MySql.Data.MySqlClient
Public Class frmRudOutward
    Dim msMode As String
    Dim mnInwardId As Long
    Dim mbGenerateInwardNo As Boolean = True
    Private Sub frmRudOutward_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String
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

        Call ClearControl()

    End Sub
    Private Sub btnSearchInward_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchInward.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, _
                             " select a.Corres_gid as 'Correspondence Gid', " & _
                             " a.corres_slno as 'Inward No',b.comp_name as 'Company Name',a.folio_no as 'Folio No',a.holder_name as 'Holder Name',a.certificate_no as 'Certificate No',a.awb_no as 'AWB No',a.document_type as 'Document Type',a.corres_sub_type as 'Sub Type' from sta_trn_tcorrespondence a,sta_mst_tcompany b ", _
                             " a.corres_gid,a.corres_slno,b.comp_name,a.folio_no,a.holder_name,a.certificate_no,a.awb_no,a.document_type,a.corres_sub_type ", _
                             " a.comp_gid=b.comp_gid and a.corres_status=1 and a.delete_flag='N'  ")

            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_trn_tcorrespondence " _
                    & " where Corres_gid = " & gnSearchId & " " _
                    & " and delete_flag = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySql.Data.MySqlClient.MySqlConnection)
        Dim lobjDataReader As MySqlDataReader


        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            cboCompany.SelectedIndex = -1
            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("Corres_gid").ToString
                        txtInwardNo.Text = .Item("corres_slno").ToString
                        cboCompany.SelectedValue = .Item("comp_gid").ToString
                        Txtdoctype.Text = .Item("document_type").ToString
                        Txtsubtype.Text = .Item("corres_sub_type").ToString
                        txtCertificateNo.Text = .Item("certificate_no").ToString
                        txtFolioNo.Text = .Item("folio_no").ToString
                    End If
                End If
                .Close()
            End With
            Call gpAutoFillCombo(cboCompany)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
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
        Dim lsReceivedby As String
        Dim lsAddress As String
        Dim lsReason As String
        Dim lsAction As String


        Try

            If cboCompany.SelectedIndex <> -1 Then
                lnCompId = Val(cboCompany.SelectedValue.ToString)
            Else
                lnCompId = 0
            End If

            lnInwardId = Val(txtId.Text)
            lnInwardNo = Val(QuoteFilter(txtInwardNo.Text))
            lsDocType = QuoteFilter(Txtdoctype.Text)
            ldRcvdDate = dtpRcvdDate.Value

            If cboCourier.SelectedIndex <> -1 Then
                lnCourierId = Val(cboCourier.SelectedValue.ToString)
            Else
                lnCourierId = 0
            End If

            lsAwbNo = QuoteFilter(Txtawbno.Text)
            lsCertificateno = QuoteFilter(txtCertificateNo.Text)
            lsSubType = QuoteFilter(Txtsubtype.Text)
            lsFolioNo = QuoteFilter(txtFolioNo.Text)

            If CboRecvby.SelectedIndex <> -1 Then
                lsReceivedby = CboRecvby.SelectedValue.ToString
            Else
                lsReceivedby = ""
            End If
            lsAddress = QuoteFilter(Txtaddress.Text)
            lsReason = QuoteFilter(txtReason.Text)

            lsAction = "UPDATE"

            Using cmd As New MySqlCommand("pr_sta_ins_ruddispatch", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_corres_gid", lnInwardId)
                cmd.Parameters.AddWithValue("?in_corres_slno", lnInwardNo)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_certificate_no", lsCertificateno)
                cmd.Parameters.AddWithValue("?in_document_type", lsDocType)
                cmd.Parameters.AddWithValue("?in_corres_subtype", lsSubType)
                cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                cmd.Parameters.AddWithValue("?in_dispatch_date", Format(ldRcvdDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_dispatch_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_dispatch_awb_no", lsAwbNo)
                cmd.Parameters.AddWithValue("?in_dispatch_address", lsAddress)
                cmd.Parameters.AddWithValue("?in_dispatched_by", lsReceivedby)
                cmd.Parameters.AddWithValue("?in_dispatch_remark", lsReason)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
                cmd.Parameters.AddWithValue("?in_action", lsAction)

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
                    'If lnInwardId = 0 Then
                    '    MessageBox.Show("New Inward No : " & lnInwardNo, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)

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
                cboCourier.Focus()
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
        Txtaddress.Clear()
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