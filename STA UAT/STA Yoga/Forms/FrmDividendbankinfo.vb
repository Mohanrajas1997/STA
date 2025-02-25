Imports MySql.Data.MySqlClient
Public Class FrmDividendbankinfo
    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' Company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cbocompany, gOdbcConn)

        ' Financial Year
        lsSql = ""
        lsSql &= " select finyear_code ,finyear_gid from sta_mst_tfinyear "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by finyear_code asc "

        Call gpBindCombo(lsSql, "finyear_code", "finyear_gid", cbofinyear, gOdbcConn)

        ' interim code
        lsSql = ""
        lsSql &= " select interim_name,interim_code from sta_mst_tinterim "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by interim_code asc "

        Call gpBindCombo(lsSql, "interim_name", "interim_code", cbointcode, gOdbcConn)
        EnableSave(False)

    End Sub
    Private Sub EnableSave(ByVal Status As Boolean)
        pnlnew.Visible = Not Status
        pnldiv.Enabled = Status
        pnlsave.Visible = Status
    End Sub
    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        EnableSave(True)
        Call frmCtrClear(Me)
        cbocompany.Focus()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click

        Dim lnResult As Integer
        Dim lsTxt As String

        Dim lndividendaccgid As Long
        Dim lncompid As Long
        Dim lnfinyeargid As Long
        Dim lninterimcode As String
        Dim lnbankaccno As String
        Dim lnbankname As String
        Dim lnbranchname As String
        Dim lnifsccode As String
        Dim lnmicrcode As String
        Dim lnaccopendate As Date
        Dim lnsettlementdate As Date
        Dim lnsponcercode As String
        Dim lndividendamount As Double
        Dim lndividendpercentage As Double
        Dim lnuserno As String
        Dim lsAction As String
        Try
            If cbocompany.SelectedIndex <> -1 Then
                lncompid = Val(cbocompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            If cbofinyear.SelectedIndex <> -1 Then
                lnfinyeargid = Val(cbofinyear.SelectedValue.ToString)
            Else
                lnfinyeargid = 0
            End If

            If cbointcode.SelectedIndex <> -1 Then
                lninterimcode = cbointcode.SelectedValue.ToString
            Else
                lninterimcode = ""
            End If

          
            lnbankaccno = QuoteFilter(txtbankaccno.Text)
            lnbankname = QuoteFilter(txtbankname.Text)
            lnbranchname = QuoteFilter(txtbankbranch.Text)
            lnifsccode = QuoteFilter(txtbankifsc.Text)
            lnmicrcode = QuoteFilter(txtmicr.Text)
            lnaccopendate = dtnopen.Value
            lnsettlementdate = dtnsettle.Value
            lnsponcercode = QuoteFilter(txtsponsor.Text)
            lndividendamount = Val(txtdivamt.Text)
            lndividendpercentage = Val(txtdivpercent.Text)
            lnuserno = QuoteFilter(txtuserno.Text)
            lndividendaccgid = Val(txtId.Text)

            If lndividendaccgid = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_mst_tdividendacc", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_dividendacc_gid", lndividendaccgid)
                cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                cmd.Parameters.AddWithValue("?in_finyear_gid", lnfinyeargid)
                cmd.Parameters.AddWithValue("?in_interim_code", lninterimcode)
                cmd.Parameters.AddWithValue("?in_bank_acc_no", lnbankaccno)
                cmd.Parameters.AddWithValue("?in_bank_name", lnbankname)
                cmd.Parameters.AddWithValue("?in_bank_branch", lnbranchname)
                cmd.Parameters.AddWithValue("?in_bank_ifsc_code", lnifsccode)
                cmd.Parameters.AddWithValue("?in_bank_micr_code", lnmicrcode)
                cmd.Parameters.AddWithValue("?in_acc_open_date", Format(lnaccopendate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_settlement_date", Format(lnsettlementdate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_sponcer_code", lnsponcercode)
                cmd.Parameters.AddWithValue("?in_dividend_amount", lndividendamount)
                cmd.Parameters.AddWithValue("?in_dividend_percentage", lndividendpercentage)
                cmd.Parameters.AddWithValue("?in_user_no", lnuserno)
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

            Call frmCtrClear(Me)
            EnableSave(False)
            btnnew.Focus()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try


    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        EnableSave(False)
        Call frmCtrClear(Me)
        btnnew.Focus()
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
                             " select a.dividendacc_gid as 'Dividend Acc Gid'," & _
                             " b.comp_name as 'Company Name',c.finyear_code as 'Financial Year',d.interim_name as 'Interim Name',a.bank_acc_no as 'Bank Acc No' from sta_mst_tdividendacc a,sta_mst_tcompany b,sta_mst_tfinyear c,sta_mst_tinterim d ", _
                             " a.dividendacc_gid,b.comp_name,c.finyear_code,d.interim_name,a.bank_acc_no ", _
                             " a.comp_gid=b.comp_gid and a.finyear_gid=c.finyear_gid and a.interim_code=d.interim_code and a.delete_flag='N' Order by a.dividendacc_gid asc ")


            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tdividendacc " _
                    & " where dividendacc_gid = " & gnSearchId & " " _
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

            cbocompany.SelectedIndex = -1
            cbofinyear.SelectedIndex = -1
            cbointcode.SelectedIndex = -1


            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("dividendacc_gid").ToString
                        cbocompany.SelectedValue = .Item("comp_gid").ToString
                        cbofinyear.SelectedValue = .Item("finyear_gid").ToString
                        cbointcode.SelectedValue = .Item("interim_code").ToString
                        txtbankaccno.Text = .Item("bank_acc_no").ToString
                        txtbankname.Text = .Item("bank_name").ToString
                        txtbankbranch.Text = .Item("bank_branch").ToString
                        txtbankifsc.Text = .Item("bank_ifsc_code").ToString
                        txtmicr.Text = .Item("bank_micr_code").ToString
                        dtnopen.Value = .Item("acc_open_date")
                        dtnsettle.Value = .Item("settlement_date")
                        txtsponsor.Text = .Item("sponcer_code").ToString
                        txtdivamt.Text = .Item("dividend_amount").ToString
                        txtdivpercent.Text = .Item("dividend_percentage").ToString
                        txtuserno.Text = .Item("user_no").ToString


                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(cbocompany)
            Call gpAutoFillCombo(cbofinyear)
            Call gpAutoFillCombo(cbointcode)


        Catch ex As Exception

        End Try
    End Sub

   

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnclear.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btndelete_Click_1(sender As System.Object, e As System.EventArgs) Handles btndelete.Click
        Dim lnResult As Integer
        Dim lsTxt As String

        Dim lndividendaccgid As Long
        Dim lncompid As Long
        Dim lnfinyeargid As Long
        Dim lninterimcode As String
        Dim lnbankaccno As String
        Dim lnbankname As String
        Dim lnbranchname As String
        Dim lnifsccode As String
        Dim lnmicrcode As String
        Dim lnaccopendate As Date
        Dim lnsettlementdate As Date
        Dim lnsponcercode As String
        Dim lndividendamount As Double
        Dim lndividendpercentage As Double
        Dim lsAction As String
        Try
            If txtId.Text = "" Then
                If MsgBox("Select record to delete?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                End If
            Else
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then


                    If cbocompany.SelectedIndex <> -1 Then
                        lncompid = Val(cbocompany.SelectedValue.ToString)
                    Else
                        lncompid = 0
                    End If

                    If cbofinyear.SelectedIndex <> -1 Then
                        lnfinyeargid = Val(cbofinyear.SelectedValue.ToString)
                    Else
                        lnfinyeargid = 0
                    End If

                    If cbointcode.SelectedIndex <> -1 Then
                        lninterimcode = cbointcode.SelectedValue.ToString
                    Else
                        lninterimcode = ""
                    End If


                    lnbankaccno = QuoteFilter(txtbankaccno.Text)
                    lnbankname = QuoteFilter(txtbankname.Text)
                    lnbranchname = QuoteFilter(txtbankbranch.Text)
                    lnifsccode = QuoteFilter(txtbankifsc.Text)
                    lnmicrcode = QuoteFilter(txtmicr.Text)
                    lnaccopendate = dtnopen.Value
                    lnsettlementdate = dtnsettle.Value
                    lnsponcercode = QuoteFilter(txtsponsor.Text)
                    lndividendamount = Val(txtdivamt.Text)
                    lndividendpercentage = Val(txtdivpercent.Text)
                    lndividendaccgid = Val(txtId.Text)

                    If lndividendaccgid = 0 Then
                        lsAction = "INSERT"
                    Else
                        lsAction = "DELETE"
                    End If

                    Using cmd As New MySqlCommand("pr_sta_mst_tdividendacc", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_dividendacc_gid", lndividendaccgid)
                        cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                        cmd.Parameters.AddWithValue("?in_finyear_gid", lnfinyeargid)
                        cmd.Parameters.AddWithValue("?in_interim_code", lninterimcode)
                        cmd.Parameters.AddWithValue("?in_bank_acc_no", lnbankaccno)
                        cmd.Parameters.AddWithValue("?in_bank_name", lnbankname)
                        cmd.Parameters.AddWithValue("?in_bank_branch", lnbranchname)
                        cmd.Parameters.AddWithValue("?in_bank_ifsc_code", lnifsccode)
                        cmd.Parameters.AddWithValue("?in_bank_micr_code", lnmicrcode)
                        cmd.Parameters.AddWithValue("?in_acc_open_date", Format(lnaccopendate, "yyyy-MM-dd"))
                        cmd.Parameters.AddWithValue("?in_settlement_date", Format(lnsettlementdate, "yyyy-MM-dd"))
                        cmd.Parameters.AddWithValue("?in_sponcer_code", lnsponcercode)
                        cmd.Parameters.AddWithValue("?in_dividend_amount", lndividendamount)
                        cmd.Parameters.AddWithValue("?in_dividend_percentage", lndividendpercentage)
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

                    Call frmCtrClear(Me)
                    EnableSave(False)
                    btnnew.Focus()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
End Class