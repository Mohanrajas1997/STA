Imports System.Data.OleDb
Imports MySql.Data.MySqlClient

Public Class frmIEPFreport

    Private Sub frmIEPFreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        'Company
        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        'Finyear
        Sql = ""
        Sql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by finyear_code asc "

        Call gpBindCombo(Sql, "finyear_code", "finyear_gid", CboFinyear, gOdbcConn)

        Finyear.Visible = False

    End Sub

    Public Function GetMyTable() As DataTable
        ' Create new DataTable instance.
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        Dim lsSql As String
        lsSql = ""
        lsSql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by finyear_code asc "

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        Return dt
    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If RbdIEPF1.Checked = True Then
            Call LoadIEPF1Grid()
        Else
            Call LoadIEPF2Grid()
        End If
    End Sub

    Private Sub LoadIEPF1Grid()
        Dim lsSql As String
        Dim lnbenpost_date As String

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If RbdIEPF1.Checked = True Then
            If (CboFinyear.Text = "" Or CboFinyear.SelectedIndex = -1) Then
                MessageBox.Show("Please select the finyear !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                CboFinyear.Focus()
                Exit Sub
            End If
        End If

        If (dtpPaidDate.Checked = False) Then
            MessageBox.Show("Please select the paid date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpPaidDate.Focus()
            Exit Sub
        End If
        lsSql = ""
        lsSql &= "select max(benpost_date) from sta_trn_tbenpost where comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lnbenpost_date = Val(gfExecuteScalar(lsSql, gOdbcConn))

        lsSql = ""
        lsSql &= " select SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 1), ' ', -1) AS first_name,"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 2), ' ', -1) AS middle_name,"
        lsSql &= " SUBSTRING(a.shar_holder,LENGTH(SUBSTRING_INDEX(a.shar_holder, ' ', 2))+1) AS last_name,"
        lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        lsSql &= " 'NA'"
        lsSql &= " else"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        lsSql &= " end"
        lsSql &= " AS father_firstname,"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS father_middlename,"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS father_lastname,"
        lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        lsSql &= " a.holder1_country,"
        lsSql &= " a.holder1_state,"
        lsSql &= " '' as district,"
        lsSql &= " a.holder1_pincode,"
        lsSql &= " a.folio_dpid as 'folio number',"
        lsSql &= " '' as 'dp id - client id Account Number',"
        lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        lsSql &= " a.div_amount as 'Amount Transferred',"
        lsSql &= " 'Date should be provided by STA Team' as 'Date of event',"
        lsSql &= " a.holder1_pan as 'pan',"
        lsSql &= " '' as 'DOB',"
        lsSql &= " '' as 'Aadhar Number',"
        lsSql &= " b.nominee_name as 'Join Holder Name',"
        lsSql &= " '' as 'Remarks',"
        lsSql &= " 'No' as 'Is the Investment (Amount/Shares/Under any litigation)'"
        lsSql &= " from div_trn_tdividend as a"
        lsSql &= " left join sta_trn_tfolio as b on a.folio_dpid = b.folio_no"
        lsSql &= " and b.delete_flag = 'N'"
        lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) "
        lsSql &= " union all"
        lsSql &= " select SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 1), ' ', -1) AS first_name, "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 2), ' ', -1) AS middle_name,"
        lsSql &= " SUBSTRING(a.shar_holder,LENGTH(SUBSTRING_INDEX(a.shar_holder, ' ', 2))+1) AS last_name,"
        lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        lsSql &= " 'NA'"
        lsSql &= " else "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        lsSql &= " end"
        lsSql &= " AS father_firstname, "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS father_middlename,"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS father_lastname,"
        lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        lsSql &= " a.holder1_country,"
        lsSql &= " a.holder1_state,"
        lsSql &= " '' as district,"
        lsSql &= " a.holder1_pincode,"
        lsSql &= " a.folio_dpid as 'folio number',"
        lsSql &= " '' as 'dp id - client id Account Number',"
        lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        lsSql &= " a.div_amount as 'Amount Transferred',"
        lsSql &= " 'Date should be provided by STA Team' as 'Date of event',"
        lsSql &= " a.holder1_pan as 'pan',"
        lsSql &= " '' as 'DOB',"
        lsSql &= " '' as 'Aadhar Number',"
        lsSql &= " b.nominee_name as 'Join Holder Name',"
        lsSql &= " '' as 'Remarks',"
        lsSql &= " 'No' as 'Is the Investment (Amount/Shares/Under any litigation)'"
        lsSql &= " from div_trn_tdividend as a"
        lsSql &= " left join sta_trn_tbenpost as b on a.folio_dpid = concat(b.dp_id,b.client_id)"
        lsSql &= " and b.delete_flag = 'N'"
        lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) "
        lsSql &= " and b.benpost_date = '" & lnbenpost_date & "'"


        gpPopGridView(dgvList, lsSql, gOdbcConn)


        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub LoadIEPF2Grid()
        Dim lsSql As String
        Dim finyearvalues As String = ""
        Dim lnbenpost_date As String

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If RbdIEPF2.Checked = True And Finyear.CheckedItems.Count = 0 Then
            MessageBox.Show("Please select the Financial Year !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finyear.Focus()
            Exit Sub
        Else
            Dim message As String = ""
            For Each item As Object In Finyear.CheckedItems
                Dim row As DataRowView = TryCast(item, DataRowView)
                message += row("finyear_gid") & ","
            Next
            finyearvalues = message.TrimEnd(",")
        End If

        If (dtpPaidDate.Checked = False) Then
            MessageBox.Show("Please select the paid date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpPaidDate.Focus()
            Exit Sub
        End If
        lsSql = ""
        lsSql &= "select max(benpost_date) from sta_trn_tbenpost where comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lnbenpost_date = Val(gfExecuteScalar(lsSql, gOdbcConn))

        lsSql = ""
        lsSql &= " select SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 1), ' ', -1) AS first_name,"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 2), ' ', -1) AS middle_name,"
        lsSql &= " SUBSTRING(a.shar_holder,LENGTH(SUBSTRING_INDEX(a.shar_holder, ' ', 2))+1) AS last_name,"
        lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        lsSql &= " 'NA'"
        lsSql &= " else"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        lsSql &= " end"
        lsSql &= " AS father_firstname,"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS father_middlename,"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS father_lastname,"
        lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        lsSql &= " a.holder1_country,"
        lsSql &= " a.holder1_state,"
        lsSql &= " '' as district,"
        lsSql &= " a.holder1_pincode,"
        lsSql &= " a.folio_dpid as 'folio number',"
        lsSql &= " '' as 'dp id - client id Account Number',"
        lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        lsSql &= " a.div_amount as 'Amount Transferred',"
        lsSql &= " 'Date should be provided by STA Team' as 'Date of event',"
        lsSql &= " a.holder1_pan as 'pan',"
        lsSql &= " '' as 'DOB',"
        lsSql &= " '' as 'Aadhar Number',"
        lsSql &= " b.nominee_name as 'Join Holder Name',"
        lsSql &= " '' as 'Remarks',"
        lsSql &= " 'No' as 'Is the Investment (Amount/Shares/Under any litigation)'"
        lsSql &= " from div_trn_tdividend as a"
        lsSql &= " inner join sta_trn_tfolio as b on a.folio_dpid = b.folio_no"
        lsSql &= " and b.delete_flag = 'N'"
        lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid in (" & finyearvalues & ")"
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) "
        lsSql &= " union all"
        lsSql &= " select SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 1), ' ', -1) AS first_name, "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(a.shar_holder, ' ', 2), ' ', -1) AS middle_name,"
        lsSql &= " SUBSTRING(a.shar_holder,LENGTH(SUBSTRING_INDEX(a.shar_holder, ' ', 2))+1) AS last_name,"
        lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        lsSql &= " 'NA'"
        lsSql &= " else "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        lsSql &= " end"
        lsSql &= " AS father_firstname, "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS father_middlename,"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS father_lastname,"
        lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        lsSql &= " a.holder1_country,"
        lsSql &= " a.holder1_state,"
        lsSql &= " '' as district,"
        lsSql &= " a.holder1_pincode,"
        lsSql &= " a.folio_dpid as 'folio number',"
        lsSql &= " '' as 'dp id - client id Account Number',"
        lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        lsSql &= " a.div_amount as 'Amount Transferred',"
        lsSql &= " 'Date should be provided by STA Team' as 'Date of event',"
        lsSql &= " a.holder1_pan as 'pan',"
        lsSql &= " '' as 'DOB',"
        lsSql &= " '' as 'Aadhar Number',"
        lsSql &= " b.nominee_name as 'Join Holder Name',"
        lsSql &= " '' as 'Remarks',"
        lsSql &= " 'No' as 'Is the Investment (Amount/Shares/Under any litigation)'"
        lsSql &= " from div_trn_tdividend as a"
        lsSql &= " inner join sta_trn_tbenpost as b on a.folio_dpid = concat(b.dp_id,b.client_id)"
        lsSql &= " and b.delete_flag = 'N'"
        lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid in (" & finyearvalues & ")"
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) "
        lsSql &= " and b.benpost_date = '" & lnbenpost_date & "'"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub RbdIEPF2_Click(sender As Object, e As EventArgs) Handles RbdIEPF2.Click
        If RbdIEPF2.Checked = True Then
            Finyear.Visible = True
            CboFinyear.Visible = False
            'List of finyear
            Dim dt1 As DataTable = GetMyTable()
            If dt1.Rows.Count > 0 Then
                'Assign DataTable as DataSource.
                Finyear.DataSource = dt1
                Finyear.DisplayMember = "finyear_code"
                Finyear.ValueMember = "finyear_gid"
            End If
            Finyear.CheckOnClick = True
        Else
            Finyear.Visible = False
            CboFinyear.Visible = True
        End If
    End Sub

    Private Sub RbdIEPF1_Click(sender As Object, e As EventArgs) Handles RbdIEPF1.Click
        If RbdIEPF1.Checked = True Then
            Finyear.Visible = False
            CboFinyear.Visible = True

        Else
            Finyear.Visible = True
            CboFinyear.Visible = False

            'List of finyear
            Dim dt1 As DataTable = GetMyTable()

            If dt1.Rows.Count > 0 Then
                For i As Integer = 0 To dt1.Rows.Count - 1
                    Finyear.Items.Add(CStr(dt1.Rows(i).Item("finyear_code")), False)
                Next
            End If
            Finyear.CheckOnClick = True
        End If
    End Sub

    Private Sub frmIEPFreport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With dgvList
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvList.Top + dgvList.Height + 6
        pnlExport.Left = dgvList.Left
        pnlExport.Width = dgvList.Width
        btnExport.Left = Math.Abs(pnlExport.Width - btnExport.Width)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class