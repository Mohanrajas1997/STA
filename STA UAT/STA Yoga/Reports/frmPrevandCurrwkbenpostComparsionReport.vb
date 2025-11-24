Imports MySql.Data.MySqlClient

Public Class frmPrevandCurrwkbenpostComparsionReport

    Private Sub frmPrevandCurrwkbenpostComparsionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadData()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim lnCompId As Long = 0
        Dim lsBenpostDate As String = ""
        Dim lsDepository As String = ""

        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
            lsDepository = cbdepotype.SelectedValue.ToString()
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If

        lsBenpostDate = Format(dtpFrom.Value, "yyyy-MM-dd")

        cmd = New MySqlCommand("pr_sta_get_prevandcurrbenpostcomparison", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
        cmd.Parameters.AddWithValue("?in_benpost_date", CDate(lsBenpostDate))
        cmd.Parameters.AddWithValue("?in_depository_code", lsDepository)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvList
            .DataSource = dt

            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub frmPrevandCurrwkbenpostComparsionReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With dgvList
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvList.Top + dgvList.Height
        pnlExport.Left = dgvList.Left
        pnlExport.Width = dgvList.Width

        btnproccess.Left = Math.Abs(pnlExport.Width - btnExport.Width - btnproccess.Width - 6)
        btnExport.Left = Math.Abs(btnproccess.Left + btnproccess.Width + 6)
    End Sub

    Private Sub btnproccess_Click(sender As Object, e As EventArgs) Handles btnproccess.Click

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn
        Dim lbInsertFlag As Boolean = False
        Dim lbHeaderFlag As Boolean = False

        Dim lsBenpostDate As String = ""

        If dgvList.Rows.Count = 0 Then
            MessageBox.Show("No records found", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If


        For i As Integer = 0 To dgvList.Rows.Count - 1 Step +1

            lsBenpostDate = Format(dtpFrom.Value, "yyyy-MM-dd")

            Using cmd As New MySqlCommand("pr_sta_ins_benpost", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_gid", dgvList.Rows(i).Cells(0).Value.ToString())
                cmd.Parameters.AddWithValue("?in_depository_code", dgvList.Rows(i).Cells(1).Value.ToString())
                cmd.Parameters.AddWithValue("?in_isin_id", dgvList.Rows(i).Cells(2).Value.ToString())
                cmd.Parameters.AddWithValue("?in_dp_id", dgvList.Rows(i).Cells(3).Value.ToString())
                cmd.Parameters.AddWithValue("?in_client_id", dgvList.Rows(i).Cells(4).Value.ToString())
                cmd.Parameters.AddWithValue("?in_sebi_reg_no", dgvList.Rows(i).Cells(5).Value.ToString())
                cmd.Parameters.AddWithValue("?in_benpost_date", lsBenpostDate)
                cmd.Parameters.AddWithValue("?in_share_count", 0)
                cmd.Parameters.AddWithValue("?in_lockin", dgvList.Rows(i).Cells(8).Value.ToString())
                cmd.Parameters.AddWithValue("?in_pledge", dgvList.Rows(i).Cells(9).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_name", dgvList.Rows(i).Cells(10).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_fh_name", dgvList.Rows(i).Cells(11).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_pan", dgvList.Rows(i).Cells(12).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder2_name", dgvList.Rows(i).Cells(13).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder2_fh_name", dgvList.Rows(i).Cells(14).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder2_pan", dgvList.Rows(i).Cells(15).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder3_name", dgvList.Rows(i).Cells(16).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder3_fh_name", dgvList.Rows(i).Cells(17).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder3_pan", dgvList.Rows(i).Cells(18).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_addr1", dgvList.Rows(i).Cells(19).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_addr2", dgvList.Rows(i).Cells(20).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_addr3", dgvList.Rows(i).Cells(21).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_city", dgvList.Rows(i).Cells(22).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_state", dgvList.Rows(i).Cells(23).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_country", dgvList.Rows(i).Cells(24).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_pin", dgvList.Rows(i).Cells(25).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_contact_no", dgvList.Rows(i).Cells(26).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_fax_no", dgvList.Rows(i).Cells(27).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_email_id", dgvList.Rows(i).Cells(28).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder2_email_id", dgvList.Rows(i).Cells(29).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder3_email_id", dgvList.Rows(i).Cells(30).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_per_addr1", dgvList.Rows(i).Cells(31).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_per_addr2", dgvList.Rows(i).Cells(32).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_per_addr3", dgvList.Rows(i).Cells(33).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_per_city", dgvList.Rows(i).Cells(34).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_per_state", dgvList.Rows(i).Cells(35).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_per_country", dgvList.Rows(i).Cells(36).Value.ToString())
                cmd.Parameters.AddWithValue("?in_holder1_per_pin", dgvList.Rows(i).Cells(37).Value.ToString())
                cmd.Parameters.AddWithValue("?in_nominee_name", dgvList.Rows(i).Cells(38).Value.ToString())
                cmd.Parameters.AddWithValue("?in_nominee_part1", dgvList.Rows(i).Cells(39).Value.ToString())
                cmd.Parameters.AddWithValue("?in_nominee_part2", dgvList.Rows(i).Cells(40).Value.ToString())
                cmd.Parameters.AddWithValue("?in_nominee_part3", dgvList.Rows(i).Cells(41).Value.ToString())
                cmd.Parameters.AddWithValue("?in_nominee_part4", dgvList.Rows(i).Cells(42).Value.ToString())
                cmd.Parameters.AddWithValue("?in_nominee_part5", dgvList.Rows(i).Cells(43).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_acc_no", dgvList.Rows(i).Cells(44).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_acc_type", dgvList.Rows(i).Cells(45).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_micr_code", dgvList.Rows(i).Cells(46).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_ifsc_code", dgvList.Rows(i).Cells(47).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_name", dgvList.Rows(i).Cells(48).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_addr1", dgvList.Rows(i).Cells(49).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_addr2", dgvList.Rows(i).Cells(50).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_addr3", dgvList.Rows(i).Cells(51).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_city", dgvList.Rows(i).Cells(52).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_state", dgvList.Rows(i).Cells(53).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_country", dgvList.Rows(i).Cells(54).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bank_pin", dgvList.Rows(i).Cells(55).Value.ToString())
                cmd.Parameters.AddWithValue("?in_rbi_ref_no", dgvList.Rows(i).Cells(56).Value.ToString())
                cmd.Parameters.AddWithValue("?in_rbi_app_date", lsBenpostDate)
                cmd.Parameters.AddWithValue("?in_bene_type", dgvList.Rows(i).Cells(58).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bene_subtype", dgvList.Rows(i).Cells(59).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bene_acccat", dgvList.Rows(i).Cells(60).Value.ToString())
                cmd.Parameters.AddWithValue("?in_bene_occupation", dgvList.Rows(i).Cells(61).Value.ToString())
                cmd.Parameters.AddWithValue("?in_line_no", i)
                cmd.Parameters.AddWithValue("?in_errline_flag", True)

                'output Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsMsg = cmd.Parameters("?out_msg").Value.ToString()

            End Using
        Next

        MessageBox.Show("Records inserted Successfully...!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class