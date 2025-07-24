Imports MySql.Data.MySqlClient

Public Class frmDpMaster

    Private Sub frmDpMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' Depository type
        lsSql = ""
        lsSql &= " select depository_code,depository_name from sta_mst_tdepository "
        lsSql &= " where depository_code != 'A' and delete_flag = 'N' "
        lsSql &= " order by depository_name "

        Call gpBindCombo(lsSql, "depository_name", "depository_name", cboDepositoryType, gOdbcConn)

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
        txtDpid.Focus()
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtDpid.Focus()
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
            SearchDialog = New frmSearch(gOdbcConn, "select dp_gid as 'DP Gid',dp_id as 'Dp ID',dp_name as 'Dp Name',depository_code as 'Depository Code', " &
            "dp_address1 as 'Address1',dp_address2 as 'Address2',dp_address3 as 'Address3',dp_contact_no as 'Contact No',dp_email_id as 'Email ID', " &
            "dp_city as 'City',dp_state as 'State',dp_country as 'Country',dp_pincode as Pincode,dp_profile_url as 'Profile Url' FROM sta_mst_tdepositoryparticipant ",
            "dp_gid,dp_id,dp_name,depository_code,dp_address1,dp_address2,dp_address3,dp_contact_no,dp_email_id, " &
            "dp_city,dp_state,dp_country,dp_pincode,dp_profile_url ",
            " 1 = 1 and delete_flag = 'N' ")
            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tdepositoryparticipant " _
                    & "where dp_gid = " & gnSearchId & " " _
                    & "and delete_flag = 'N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySqlConnection)
        Dim lobjDataReader As MySqlDataReader

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            If lobjDataReader.HasRows Then
                If lobjDataReader.Read Then
                    txtid.Text = lobjDataReader.Item("dp_gid").ToString
                    txtDpid.Text = lobjDataReader.Item("dp_id").ToString
                    txtDpName.Text = lobjDataReader.Item("dp_name").ToString
                    cboDepositoryType.Text = lobjDataReader.Item("depository_code").ToString
                    txtAddress1.Text = lobjDataReader.Item("dp_address1").ToString
                    txtAddress2.Text = lobjDataReader.Item("dp_address2").ToString
                    txtAddress3.Text = lobjDataReader.Item("dp_address3").ToString
                    txtContactNo.Text = lobjDataReader.Item("dp_contact_no").ToString
                    txtEmailid.Text = lobjDataReader.Item("dp_email_id").ToString
                    txtCity.Text = lobjDataReader.Item("dp_city").ToString
                    txtState.Text = lobjDataReader.Item("dp_state").ToString
                    txtCountry.Text = lobjDataReader.Item("dp_country").ToString
                    txtPincode.Text = lobjDataReader.Item("dp_pincode").ToString
                    txtProfileUrl.Text = lobjDataReader.Item("dp_profile_url").ToString
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
        Dim lnDpGid As Long
        Dim lsDpID As String
        Dim lsDpName As String
        Dim lsDepositorycode As String
        Dim lsAddress1 As String
        Dim lsAddress2 As String
        Dim lsAddress3 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsContactNo As String
        Dim lsEmailID As String
        Dim lsProfileUrl As String
        Dim lsAction As String

        Try
            lnDpGid = Val(txtid.Text)
            lsDpID = QuoteFilter(txtDpid.Text)
            lsDpName = QuoteFilter(txtDpName.Text)
            lsDepositorycode = QuoteFilter(cboDepositoryType.Text)
            lsAddress1 = QuoteFilter(txtAddress1.Text)
            lsAddress2 = QuoteFilter(txtAddress2.Text)
            lsAddress3 = QuoteFilter(txtAddress3.Text)
            lsContactNo = QuoteFilter(txtContactNo.Text)
            lsEmailID = QuoteFilter(txtEmailid.Text)
            lsCity = QuoteFilter(txtCity.Text)
            lsState = QuoteFilter(txtState.Text)
            lsCountry = QuoteFilter(txtCountry.Text)
            lsPincode = QuoteFilter(txtPincode.Text)
            lsProfileUrl = QuoteFilter(txtProfileUrl.Text)

            If lnDpGid = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_mst_tdepositoryparticipant", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_dp_gid", lnDpGid)
                cmd.Parameters.AddWithValue("?in_dp_id", lsDpID)
                cmd.Parameters.AddWithValue("?in_dp_name", lsDpName)
                cmd.Parameters.AddWithValue("?in_depository_code", lsDepositorycode)
                cmd.Parameters.AddWithValue("?in_dp_address1", lsAddress1)
                cmd.Parameters.AddWithValue("?in_dp_address2", lsAddress2)
                cmd.Parameters.AddWithValue("?in_dp_address3", lsAddress3)
                cmd.Parameters.AddWithValue("?in_dp_city", lsCity)
                cmd.Parameters.AddWithValue("?in_dp_state", lsState)
                cmd.Parameters.AddWithValue("?in_dp_country", lsCountry)
                cmd.Parameters.AddWithValue("?in_dp_pincode", lsPincode)
                cmd.Parameters.AddWithValue("?in_dp_contact_no", lsContactNo)
                cmd.Parameters.AddWithValue("?in_dp_email_id", lsEmailID)
                cmd.Parameters.AddWithValue("?in_dp_profile_url", lsProfileUrl)
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
        Dim lnDpGid As Long
        Dim lsDpID As String
        Dim lsDpName As String
        Dim lsDepositorycode As String
        Dim lsAddress1 As String
        Dim lsAddress2 As String
        Dim lsAddress3 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsContactNo As String
        Dim lsEmailID As String
        Dim lsProfileUrl As String

        Dim lsAction As String
        Try
            If txtid.Text = "" Then
                If MsgBox("Select record to delete?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                End If
            Else
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then

                    lnDpGid = Val(txtid.Text)
                    lsDpID = QuoteFilter(txtDpid.Text)
                    lsDpName = QuoteFilter(txtDpName.Text)
                    lsDepositorycode = QuoteFilter(cboDepositoryType.Text)
                    lsAddress1 = QuoteFilter(txtAddress1.Text)
                    lsAddress2 = QuoteFilter(txtAddress2.Text)
                    lsAddress3 = QuoteFilter(txtAddress3.Text)
                    lsContactNo = QuoteFilter(txtContactNo.Text)
                    lsEmailID = QuoteFilter(txtEmailid.Text)
                    lsCity = QuoteFilter(txtCity.Text)
                    lsState = QuoteFilter(txtState.Text)
                    lsCountry = QuoteFilter(txtCountry.Text)
                    lsPincode = QuoteFilter(txtPincode.Text)
                    lsProfileUrl = QuoteFilter(txtProfileUrl.Text)

                    If lnDpGid = 0 Then
                        lsAction = "INSERT"
                    Else
                        lsAction = "DELETE"
                    End If

                    Using cmd As New MySqlCommand("pr_sta_mst_tdepositoryparticipant", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_dp_gid", lnDpGid)
                        cmd.Parameters.AddWithValue("?in_dp_id", lsDpID)
                        cmd.Parameters.AddWithValue("?in_dp_name", lsDpName)
                        cmd.Parameters.AddWithValue("?in_depository_code", lsDepositorycode)
                        cmd.Parameters.AddWithValue("?in_dp_address1", lsAddress1)
                        cmd.Parameters.AddWithValue("?in_dp_address2", lsAddress2)
                        cmd.Parameters.AddWithValue("?in_dp_address3", lsAddress3)
                        cmd.Parameters.AddWithValue("?in_dp_city", lsCity)
                        cmd.Parameters.AddWithValue("?in_dp_state", lsState)
                        cmd.Parameters.AddWithValue("?in_dp_country", lsCountry)
                        cmd.Parameters.AddWithValue("?in_dp_pincode", lsPincode)
                        cmd.Parameters.AddWithValue("?in_dp_contact_no", lsContactNo)
                        cmd.Parameters.AddWithValue("?in_dp_email_id", lsEmailID)
                        cmd.Parameters.AddWithValue("?in_dp_profile_url", lsProfileUrl)
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