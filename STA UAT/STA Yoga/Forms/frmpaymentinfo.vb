Imports MySql.Data.MySqlClient
Public Class frmpaymentinfo
    Inherits System.Windows.Forms.Form
    Private Sub EnableSave(ByVal Status As Boolean)
        pnlnew.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub
    Private Sub frmPaymentinfo_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", Cbocompany, gOdbcConn)

        ' Client Type
        lsSql = ""
        lsSql &= " select folioclient_type,folioclient_desc from sta_mst_tfolioclienttype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by folioclienttype_gid asc "

        Call gpBindCombo(lsSql, "folioclient_desc", "folioclient_type", cboclienttype, gOdbcConn)

        'Paymode

        lsSql = ""
        lsSql &= " select paymode_code,paymode_desc from sta_mst_tpaymode "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by paymode_gid asc "

        Call gpBindCombo(lsSql, "paymode_desc", "paymode_code", cbopaymode, gOdbcConn)


        'Currency

        lsSql = ""
        lsSql &= " select currency_code,currency_name from sta_mst_tcurrency "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by currency_gid asc "

        Call gpBindCombo(lsSql, "currency_name", "currency_code", CboCurrency, gOdbcConn)


        'Me.Rbdpaymode.Checked = True
        Me.Rbdyes.Checked = True


        Call EnableSave(False)
        btnNew.Focus()

        'If mnInwardId > 0 Then
        '    Call ListAll(mnInwardId)
        'End If
    End Sub
    Private Sub ClearControl()
        Call frmCtrClear(Me)
    End Sub

    Private Sub Rbdpaymode_CheckedChanged(sender As System.Object, e As System.EventArgs)

        'If Rbdpaymode.Checked = True Then
        '    cbopaymode.Enabled = True
        '    Txtbeneficiary.Enabled = False
        '    CboCurrency.Enabled = False

        '    cbopaymode.Text = ""
        '    Txtbeneficiary.Text = ""
        '    CboCurrency.Text = ""
        '    cbopaymode.SelectedValue = ""
        '    CboCurrency.SelectedValue = ""

        'End If

    End Sub

    Private Sub Rbdbeneficiary_CheckedChanged(sender As System.Object, e As System.EventArgs)
        'If Rbdbeneficiary.Checked = True Then
        '    cbopaymode.Enabled = False
        '    Txtbeneficiary.Enabled = True
        '    CboCurrency.Enabled = False
        '    cbopaymode.Text = ""
        '    CboCurrency.Text = ""

        '    cbopaymode.SelectedValue = ""
        '    CboCurrency.SelectedValue = ""

        'End If
    End Sub

    Private Sub RbdCurrency_CheckedChanged(sender As System.Object, e As System.EventArgs)
        'If RbdCurrency.Checked = True Then
        '    cbopaymode.Enabled = False
        '    Txtbeneficiary.Enabled = False
        '    CboCurrency.Enabled = True
        '    cbopaymode.Text = ""
        '    Txtbeneficiary.Text = ""
        '    CboCurrency.Text = ""

        '    cbopaymode.SelectedValue = ""
        '    CboCurrency.SelectedValue = ""
        'End If
    End Sub

    Private Sub btnsave_click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnpaymentgid As Long
        Dim lncompid As Long
        Dim lnclientid As String
        Dim lnclientdpid As String
        Dim lnclientidtype As String
        Dim lnpaymentinfotype As String = ""
        Dim lnbenename As String
        Dim lnpaymentmode As String
        Dim lncurrencycode As String
        Dim lnactiveflag As String = ""
        Dim lsAction As String
        Try
            If Cbocompany.SelectedIndex <> -1 Then
                lncompid = Val(Cbocompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            If cboclienttype.Text <> "" And cboclienttype.SelectedIndex <> -1 Then
                lnclientidtype = cboclienttype.SelectedValue.ToString
            Else
                lnclientidtype = ""
            End If

            If cbopaymode.Text <> "" And cbopaymode.SelectedIndex <> -1 Then
                lnpaymentmode = cbopaymode.SelectedValue.ToString
            Else
                lnpaymentmode = ""
            End If

            If CboCurrency.Text <> "" And CboCurrency.SelectedIndex <> -1 Then
                lncurrencycode = CboCurrency.SelectedValue.ToString
            Else
                lncurrencycode = ""
            End If

            'If Rbdpaymode.Checked = True Then
            '    lnpaymentinfotype = "P"
            'ElseIf Rbdbeneficiary.Checked = True Then
            '    lnpaymentinfotype = "B"
            'ElseIf RbdCurrency.Checked = True Then
            '    lnpaymentinfotype = "C"
            'End If

            If Rbdyes.Checked = True Then
                lnactiveflag = "Y"
            ElseIf RbdNo.Checked = True Then
                lnactiveflag = "N"
            End If
            lnclientid = QuoteFilter(Txtclientid.Text)
            lnclientdpid = QuoteFilter(TxtDpid.Text)
            lnbenename = QuoteFilter(Txtbeneficiary.Text)
            lnpaymentgid = Val(txtId.Text)

            If lnpaymentgid = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_mst_tpaymentinfo", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_paymentinfo_gid", lnpaymentgid)
                cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                cmd.Parameters.AddWithValue("?in_folioclient_id", lnclientid)
                cmd.Parameters.AddWithValue("?in_folioclient_dp_id", lnclientdpid)
                cmd.Parameters.AddWithValue("?in_folioclient_id_type", lnclientidtype)
                cmd.Parameters.AddWithValue("?in_paymentinfo_type", lnpaymentinfotype)
                cmd.Parameters.AddWithValue("?in_bene_name", lnbenename)
                cmd.Parameters.AddWithValue("?in_payment_mode", lnpaymentmode)
                cmd.Parameters.AddWithValue("?in_currency_code", lncurrencycode)
                cmd.Parameters.AddWithValue("?in_active_flag", lnactiveflag)
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
                End If

            End Using

            Call ClearControl()
            EnableSave(False)
            btnNew.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try


    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        EnableSave(True)
        Call ClearControl()
        Cbocompany.Focus()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        EnableSave(False)
        ClearControl()
        btnNew.Focus()
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click
        Try
            If txtId.Text = "" Then
                If MsgBox("Select Record to edit", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                    EnableSave(False)
                End If
            Else
                EnableSave(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind_Click(sender As System.Object, e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, _
                             " select a.paymentinfo_gid as 'Paymentinfo id'," & _
                             " b.comp_name as 'Company Name',a.folioclient_id as 'FolioClient Id',a.folioclient_dp_id as 'FolioClient Dp Id' from sta_mst_tpaymentinfo a , sta_mst_tcompany b ", _
                             " a.paymentinfo_gid,b.comp_name,a.folioclient_id,a.folioclient_dp_id ", _
                             " a.comp_gid=b.comp_gid and a.delete_flag='N' ")


            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tpaymentinfo " _
                    & " where paymentinfo_gid = " & gnSearchId & " " _
                    & " and delete_flag = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btn_Delete(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnpaymentgid As Long
        Dim lncompid As Long
        Dim lnclientid As String
        Dim lnclientdpid As String
        Dim lnclientidtype As String
        Dim lnpaymentinfotype As String
        Dim lnbenename As String
        Dim lnpaymentmode As String
        Dim lncurrencycode As String
        Dim lnactiveflag As String
        Dim lsAction As String
        Try
            If txtId.Text = "" Then
                If MsgBox("Select record to delete?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                End If
            Else
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then


                    If Cbocompany.SelectedIndex <> -1 Then
                        lncompid = Val(Cbocompany.SelectedValue.ToString)
                    Else
                        lncompid = 0
                    End If

                    If cboclienttype.SelectedIndex <> -1 Then
                        lnclientidtype = cboclienttype.SelectedValue.ToString
                    Else
                        lnclientidtype = ""
                    End If

                    If cbopaymode.SelectedIndex <> -1 Then
                        lnpaymentmode = cbopaymode.SelectedValue.ToString
                    Else
                        lnpaymentmode = ""
                    End If

                    If CboCurrency.SelectedIndex <> -1 Then
                        lncurrencycode = CboCurrency.SelectedValue.ToString
                    Else
                        lncurrencycode = ""
                    End If

                    'If Rbdpaymode.Checked = True Then
                    '    lnpaymentinfotype = "P"
                    'ElseIf Rbdbeneficiary.Checked = True Then
                    '    lnpaymentinfotype = "B"
                    'ElseIf RbdCurrency.Checked = True Then
                    '    lnpaymentinfotype = "C"
                    'End If

                    If Rbdyes.Checked = True Then
                        lnactiveflag = "Y"
                    ElseIf RbdNo.Checked = True Then
                        lnactiveflag = "N"
                    End If
                    lnclientid = QuoteFilter(Txtclientid.Text)
                    lnclientdpid = QuoteFilter(TxtDpid.Text)
                    lnbenename = QuoteFilter(Txtbeneficiary.Text)
                    lnpaymentgid = Val(txtId.Text)

                    If lnpaymentgid = 0 Then
                        lsAction = "INSERT"
                    Else
                        lsAction = "DELETE"
                    End If

                    Using cmd As New MySqlCommand("pr_sta_mst_tpaymentinfo", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_paymentinfo_gid", lnpaymentgid)
                        cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                        cmd.Parameters.AddWithValue("?in_folioclient_id", lnclientid)
                        cmd.Parameters.AddWithValue("?in_folioclient_dp_id", lnclientdpid)
                        cmd.Parameters.AddWithValue("?in_folioclient_id_type", lnclientidtype)
                        cmd.Parameters.AddWithValue("?in_paymentinfo_type", lnpaymentinfotype)
                        cmd.Parameters.AddWithValue("?in_bene_name", lnbenename)
                        cmd.Parameters.AddWithValue("?in_payment_mode", lnpaymentmode)
                        cmd.Parameters.AddWithValue("?in_currency_code", lncurrencycode)
                        cmd.Parameters.AddWithValue("?in_active_flag", lnactiveflag)
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
                            Me.Close()
                        Else
                            MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End Using

                    Call ClearControl()
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySql.Data.MySqlClient.MySqlConnection)
        Dim lobjDataReader As MySqlDataReader
        Dim Paymentinfotype As String
        Dim Activeflag As String
        Dim paymentmode As String
        Dim currencycode As String

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            Cbocompany.SelectedIndex = -1
            cboclienttype.SelectedIndex = -1
            cbopaymode.SelectedIndex = -1
            CboCurrency.SelectedIndex = -1

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("paymentinfo_gid").ToString
                        Cbocompany.SelectedValue = .Item("comp_gid").ToString
                        Txtclientid.Text = .Item("folioclient_id").ToString
                        TxtDpid.Text = .Item("folioclient_dp_id").ToString
                        cboclienttype.SelectedValue = .Item("folioclient_id_type").ToString
                        'Paymentinfotype = .Item("paymentinfo_type").ToString
                        Txtbeneficiary.Text = .Item("bene_name").ToString
                        cbopaymode.SelectedValue = .Item("payment_mode").ToString
                        paymentmode = .Item("payment_mode").ToString
                        CboCurrency.SelectedValue = .Item("currency_code").ToString
                        currencycode = .Item("currency_code").ToString
                        Activeflag = .Item("active_flag").ToString

                        'If Paymentinfotype.ToString = "P" Then
                        '    Rbdpaymode.Checked = True
                        'ElseIf Paymentinfotype.ToString = "B" Then
                        '    Rbdbeneficiary.Checked = True
                        'ElseIf Paymentinfotype.ToString = "C" Then
                        '    RbdCurrency.Checked = True
                        'End If

                        If Activeflag.ToString = "Y" Then
                            Rbdyes.Checked = True
                        ElseIf Activeflag.ToString = "N" Then
                            RbdNo.Checked = True
                        End If
                        
                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(Cbocompany)
            Call gpAutoFillCombo(cboclienttype)
            If paymentmode <> "" Then
                Call gpAutoFillCombo(cbopaymode)
            End If

            If currencycode <> "" Then
                Call gpAutoFillCombo(CboCurrency)
            End If


        Catch ex As Exception

        End Try
    End Sub

End Class