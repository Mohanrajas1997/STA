Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmPendingBenpostReport

    Dim lsTxtFile As String = gsAsciiPath & "\Error.txt"
    Dim message As String = ""

    Private Sub frmPendingBenpostReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpFrom.Value = Now

        lsSql = ""
        lsSql &= " SELECT depository_code,depository_name FROM sta_mst_tdepository"
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by depository_name "

        Using cmd As MySqlCommand = New MySqlCommand(lsSql, gOdbcConn)
            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As System.Data.DataTable = New System.Data.DataTable
            dt.Load(rs)

            cbdepotype.ValueMember = "depository_code"
            cbdepotype.DisplayMember = "depository_name"
            cbdepotype.DataSource = dt
        End Using

        Call gpBindCombo(lsSql, "depository_name", "depository_code", cbdepotype, gOdbcConn)
    End Sub

    Private Sub LoadData(lnCompId, next_friday, increament_days, lsDepository)
        Dim lsMsg As String
        Dim lnResult As Long

        Dim lsCond As String = ""
        Dim j As String

        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        increament_days = Format(increament_days, "yyyy-MM-dd")

        cmd = New MySqlCommand("pr_sta_get_pendingbenpost", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
        cmd.Parameters.AddWithValue("?in_benpost_from", next_friday) 'CDate(lsBenpostfromDate))
        cmd.Parameters.AddWithValue("?in_benpost_to", increament_days) 'CDate(lsBenposttoDate))
        cmd.Parameters.AddWithValue("?in_depository_code", lsDepository)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                Using cmd1 As New MySqlCommand("pr_sta_ins_benpost", gOdbcConn)
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.AddWithValue("?in_file_gid", dt.Rows(i).Item(0))
                    cmd1.Parameters.AddWithValue("?in_depository_code", dt.Rows(i).Item(1))
                    cmd1.Parameters.AddWithValue("?in_isin_id", dt.Rows(i).Item(2))
                    cmd1.Parameters.AddWithValue("?in_dp_id", dt.Rows(i).Item(3))
                    cmd1.Parameters.AddWithValue("?in_client_id", dt.Rows(i).Item(4))
                    cmd1.Parameters.AddWithValue("?in_sebi_reg_no", dt.Rows(i).Item(5))
                    cmd1.Parameters.AddWithValue("?in_benpost_date", increament_days)
                    cmd1.Parameters.AddWithValue("?in_share_count", 0)
                    cmd1.Parameters.AddWithValue("?in_lockin", dt.Rows(i).Item(8))
                    cmd1.Parameters.AddWithValue("?in_pledge", dt.Rows(i).Item(9))
                    cmd1.Parameters.AddWithValue("?in_holder1_name", dt.Rows(i).Item(10))
                    cmd1.Parameters.AddWithValue("?in_holder1_fh_name", dt.Rows(i).Item(11))
                    cmd1.Parameters.AddWithValue("?in_holder1_pan", dt.Rows(i).Item(12))
                    cmd1.Parameters.AddWithValue("?in_holder2_name", dt.Rows(i).Item(13))
                    cmd1.Parameters.AddWithValue("?in_holder2_fh_name", dt.Rows(i).Item(14))
                    cmd1.Parameters.AddWithValue("?in_holder2_pan", dt.Rows(i).Item(15))
                    cmd1.Parameters.AddWithValue("?in_holder3_name", dt.Rows(i).Item(16))
                    cmd1.Parameters.AddWithValue("?in_holder3_fh_name", dt.Rows(i).Item(17))
                    cmd1.Parameters.AddWithValue("?in_holder3_pan", dt.Rows(i).Item(18))
                    cmd1.Parameters.AddWithValue("?in_holder1_addr1", dt.Rows(i).Item(19))
                    cmd1.Parameters.AddWithValue("?in_holder1_addr2", dt.Rows(i).Item(20))
                    cmd1.Parameters.AddWithValue("?in_holder1_addr3", dt.Rows(i).Item(21))
                    cmd1.Parameters.AddWithValue("?in_holder1_city", dt.Rows(i).Item(22))
                    cmd1.Parameters.AddWithValue("?in_holder1_state", dt.Rows(i).Item(23))
                    cmd1.Parameters.AddWithValue("?in_holder1_country", dt.Rows(i).Item(24))
                    cmd1.Parameters.AddWithValue("?in_holder1_pin", dt.Rows(i).Item(25))
                    cmd1.Parameters.AddWithValue("?in_holder1_contact_no", dt.Rows(i).Item(26))
                    cmd1.Parameters.AddWithValue("?in_holder1_fax_no", dt.Rows(i).Item(27))
                    cmd1.Parameters.AddWithValue("?in_holder1_email_id", dt.Rows(i).Item(28))
                    cmd1.Parameters.AddWithValue("?in_holder2_email_id", dt.Rows(i).Item(29))
                    cmd1.Parameters.AddWithValue("?in_holder3_email_id", dt.Rows(i).Item(30))
                    cmd1.Parameters.AddWithValue("?in_holder1_per_addr1", dt.Rows(i).Item(31))
                    cmd1.Parameters.AddWithValue("?in_holder1_per_addr2", dt.Rows(i).Item(32))
                    cmd1.Parameters.AddWithValue("?in_holder1_per_addr3", dt.Rows(i).Item(33))
                    cmd1.Parameters.AddWithValue("?in_holder1_per_city", dt.Rows(i).Item(34))
                    cmd1.Parameters.AddWithValue("?in_holder1_per_state", dt.Rows(i).Item(35))
                    cmd1.Parameters.AddWithValue("?in_holder1_per_country", dt.Rows(i).Item(36))
                    cmd1.Parameters.AddWithValue("?in_holder1_per_pin", dt.Rows(i).Item(37))
                    cmd1.Parameters.AddWithValue("?in_nominee_name", dt.Rows(i).Item(38))
                    cmd1.Parameters.AddWithValue("?in_nominee_part1", dt.Rows(i).Item(39))
                    cmd1.Parameters.AddWithValue("?in_nominee_part2", dt.Rows(i).Item(40))
                    cmd1.Parameters.AddWithValue("?in_nominee_part3", dt.Rows(i).Item(41))
                    cmd1.Parameters.AddWithValue("?in_nominee_part4", dt.Rows(i).Item(42))
                    cmd1.Parameters.AddWithValue("?in_nominee_part5", dt.Rows(i).Item(43))
                    cmd1.Parameters.AddWithValue("?in_bank_acc_no", dt.Rows(i).Item(44))
                    cmd1.Parameters.AddWithValue("?in_bank_acc_type", dt.Rows(i).Item(45))
                    cmd1.Parameters.AddWithValue("?in_bank_micr_code", dt.Rows(i).Item(46))
                    cmd1.Parameters.AddWithValue("?in_bank_ifsc_code", dt.Rows(i).Item(47))
                    cmd1.Parameters.AddWithValue("?in_bank_name", dt.Rows(i).Item(48))
                    cmd1.Parameters.AddWithValue("?in_bank_addr1", dt.Rows(i).Item(49))
                    cmd1.Parameters.AddWithValue("?in_bank_addr2", dt.Rows(i).Item(50))
                    cmd1.Parameters.AddWithValue("?in_bank_addr3", dt.Rows(i).Item(51))
                    cmd1.Parameters.AddWithValue("?in_bank_city", dt.Rows(i).Item(52))
                    cmd1.Parameters.AddWithValue("?in_bank_state", dt.Rows(i).Item(53))
                    cmd1.Parameters.AddWithValue("?in_bank_country", dt.Rows(i).Item(54))
                    cmd1.Parameters.AddWithValue("?in_bank_pin", dt.Rows(i).Item(55))
                    cmd1.Parameters.AddWithValue("?in_rbi_ref_no", dt.Rows(i).Item(56))
                    cmd1.Parameters.AddWithValue("?in_rbi_app_date", increament_days)
                    cmd1.Parameters.AddWithValue("?in_bene_type", dt.Rows(i).Item(58))
                    cmd1.Parameters.AddWithValue("?in_bene_subtype", dt.Rows(i).Item(59))
                    cmd1.Parameters.AddWithValue("?in_bene_acccat", dt.Rows(i).Item(60))
                    cmd1.Parameters.AddWithValue("?in_bene_occupation", dt.Rows(i).Item(61))
                    cmd1.Parameters.AddWithValue("?in_line_no", i)
                    cmd1.Parameters.AddWithValue("?in_errline_flag", True)

                    'output Para
                    cmd1.Parameters.Add("?out_result", MySqlDbType.Int32)
                    cmd1.Parameters("?out_result").Direction = ParameterDirection.Output
                    cmd1.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                    cmd1.Parameters("?out_msg").Direction = ParameterDirection.Output

                    cmd1.CommandTimeout = 0
                    cmd1.ExecuteNonQuery()

                    lnResult = Val(cmd1.Parameters("?out_result").Value.ToString())
                    lsMsg = cmd1.Parameters("?out_msg").Value.ToString()

                End Using
            Next

        Else
            Using sw As New StreamWriter(lsTxtFile)
                message += next_friday + " To " + increament_days + " No Record Found !" + Environment.NewLine
                sw.WriteLine(message)
                sw.Close()
            End Using

        End If

    End Sub

    Private Sub btnproccess_Click(sender As Object, e As EventArgs) Handles btnproccess.Click

        Dim from_date As Date
        Dim to_date As Date
        Dim Tdate = dtpFrom.Value

        Dim next_friday As String = ""
        Dim inc_days As String = ""
        Dim increment_days As Date

        Dim lnCompId As Long = 0
        Dim lsDepository As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsDepository = cbdepotype.SelectedValue.ToString()
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If

        from_date = dtpFrom.Value.Date.ToString()
        to_date = dtpTodate.Value.Date.ToString()

        If from_date > to_date Then
            MessageBox.Show("From date should be less than To date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpFrom.Focus()
            Exit Sub
        End If


        Do While Tdate.DayOfWeek <> DayOfWeek.Friday
            Tdate = Tdate.AddDays(1)
        Loop

        next_friday = Format(Tdate, "yyyy-MM-dd")
        increment_days = Tdate.AddDays(7).Date.ToString()

        Call LoadData(lnCompId, next_friday, increment_days, lsDepository)

        While (increment_days <= to_date)

            next_friday = Format(increment_days, "yyyy-MM-dd")
            increment_days = increment_days.AddDays(7)

            Call LoadData(lnCompId, next_friday, increment_days, lsDepository)

        End While

        MessageBox.Show("Process Completed ", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Process.Start(lsTxtFile)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub clear()
        cboCompany.Text = ""
        cbdepotype.Text = ""
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call clear()
    End Sub

End Class