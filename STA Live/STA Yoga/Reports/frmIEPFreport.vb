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
        ElseIf RbdIEPF2.Checked = True Then
            Call LoadIEPF2Grid()
        ElseIf RbdIEPF4.Checked = True Then
            Call LoadIEPF4Grid()
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
        'lsSql = "select distinct c.* from ("
        lsSql &= " select replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 1), ' ', -1),' ','')  AS 'Investor First Name',"
        lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2), ' ', -1),' ','')  AS 'Investor Middle Name',"
        lsSql &= " replace(SUBSTRING(fn_remove_extra_spaces(a.shar_holder),LENGTH(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2))+1),' ','')  AS 'Investor Last Name',"
        lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        lsSql &= " 'NA'"
        lsSql &= " else"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        lsSql &= " end"
        lsSql &= " AS 'Father/Husband First Name',"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS 'Father/Husband Middle Name',"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS 'Father/Husband Last Name',"
        lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        'lsSql &= " a.holder1_country as Country,"
        'lsSql &= " a.holder1_state as State,"
        'lsSql &= " '' as District,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'country') as Country,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'state') as State,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'district') as District,"
        lsSql &= " a.holder1_pincode as Pincode,"
        lsSql &= " a.folio_dpid as 'Folio Number',"
        lsSql &= " '' as 'DP Id-Client Id-Account Number',"
        lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        lsSql &= " a.net_amount as 'Amount Transferred',"
        lsSql &= " 'Date should be provided by STA Team' as 'Date of event',"
        lsSql &= " a.holder1_pan as 'PAN',"
        lsSql &= " '' as 'Date of Birth(DD-MON-YYYY)',"
        lsSql &= " '' as 'Aadhar Number',"
        lsSql &= " b.nominee_name as 'Nominee Name',"
        lsSql &= " b.nominee_name as 'Join Holder Name',"
        lsSql &= " '' as 'Remarks',"
        lsSql &= " 'No' as 'Is the Investment (amount / shares )under any litigation.'"
        lsSql &= " from div_trn_tdividend as a"
        lsSql &= " left join sta_trn_tfolio as b on a.folio_dpid = b.folio_no"
        lsSql &= " and b.delete_flag = 'N'"
        lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null)  group by a.div_gid"

        lsSql &= " union all"

        lsSql &= " select replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 1), ' ', -1),' ','')  AS 'Investor First Name',"
        lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2), ' ', -1),' ','')  AS 'Investor Middle Name',"
        lsSql &= " replace(SUBSTRING(fn_remove_extra_spaces(a.shar_holder),LENGTH(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2))+1),' ','')  AS 'Investor Last Name',"
        lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        lsSql &= " 'NA'"
        lsSql &= " else "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        lsSql &= " end"
        lsSql &= " AS 'Father/Husband First Name', "
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS 'Father/Husband Middle Name',"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS 'Father/Husband Last Name',"
        lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        'lsSql &= " a.holder1_country as Country,"
        'lsSql &= " a.holder1_state as State,"
        'lsSql &= " '' as District,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'country') as Country,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'state') as State,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'district') as District,"
        lsSql &= " a.holder1_pincode as Pincode,"
        lsSql &= " a.folio_dpid as 'Folio Number',"
        lsSql &= " '' as 'DP Id-Client Id-Account Number',"
        lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        'lsSql &= " a.div_amount as 'Amount Transferred',"
        lsSql &= " a.net_amount as 'Amount Transferred',"
        lsSql &= " 'Date should be provided by STA Team' as 'Date of event',"
        lsSql &= " a.holder1_pan as 'PAN',"
        lsSql &= " '' as 'Date of Birth(DD-MON-YYYY)',"
        lsSql &= " '' as 'Aadhar Number',"
        lsSql &= " b.nominee_name as 'Nominee Name',"
        lsSql &= " b.nominee_name as 'Join Holder Name',"
        lsSql &= " '' as 'Remarks',"
        lsSql &= " 'No' as 'Is the Investment (amount / shares )under any litigation.'"
        lsSql &= " from div_trn_tdividend as a"
        lsSql &= " left join sta_trn_tbenpost as b on a.folio_dpid = concat(b.dp_id,b.client_id)"
        lsSql &= " and b.delete_flag = 'N'"
        lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) "
        lsSql &= " and b.benpost_date = '" & lnbenpost_date & "' group by a.div_gid"
        'lsSql &= " ) as c"


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

        Dim finyearList As String() = finyearvalues.Split(","c)
        Dim finyearAliasQuery As String = ""
        Dim i As Integer = 1

        ' Loop through each selected financial year and create an alias like FY -1, FY -2, etc.
        For Each finyear As String In finyearList
            finyearAliasQuery &= " WHEN c.finyear_gid = " & finyear & " THEN 'FY -" & i & "'"
            i += 1
        Next

        lsSql = ""
        lsSql &= "select max(benpost_date) from sta_trn_tbenpost where comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lnbenpost_date = Val(gfExecuteScalar(lsSql, gOdbcConn))

        lsSql = ""
        lsSql &= " select replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 1), ' ', -1),' ','') AS 'Investor First Name',"
        lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2), ' ', -1),' ','') AS 'Investor Middle Name',"
        lsSql &= " replace(SUBSTRING(fn_remove_extra_spaces(a.shar_holder),LENGTH(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2))+1),' ','') AS 'Investor Last Name',"
        lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        lsSql &= " 'NA'"
        lsSql &= " else"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        lsSql &= " end"
        lsSql &= " AS 'Father/Husband First Name',"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS 'Father/Husband Middle Name',"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS 'Father/Husband Last Name',"
        lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'country') as Country,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'state') as State,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'district') as District,"
        lsSql &= " a.holder1_pincode as Pincode,"
        lsSql &= " a.folio_dpid as 'Folio Number',"
        lsSql &= " '' as 'DP ID - Client ID Account Number',"
        lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        lsSql &= " a.net_amount as 'Amount Transferred',"
        lsSql &= " 'Date should be provided by STA Team' as 'Proposed Date of transfer to IEPF(DD-MON-YYYY)',"
        lsSql &= " b.holder1_pan_no as 'PAN',"
        lsSql &= " '' as 'Date of Birth(DD-MON-YYYY)',"
        lsSql &= " '' as 'Aadhar Number',"
        lsSql &= " b.nominee_name as 'Nominee Name',"
        lsSql &= " b.holder2_name as 'Join Holder Name',"
        lsSql &= " '' as 'Remarks',"
        lsSql &= " 'No' as 'Is the Investment (amount / shares )under any litigation.',"
        lsSql &= " 'No' as ' Is the shares transfer from unpaid suspense account (Yes/No)',"
        'lsSql &= " c.finyear_code as 'Financial Year'"
        ' Add case for financial year aliasing
        lsSql &= " CASE" & finyearAliasQuery
        lsSql &= " ELSE c.finyear_code END AS 'Financial Year'"
        lsSql &= " from div_trn_tdividend as a"
        lsSql &= " left join sta_trn_tfolio as b on a.folio_dpid = b.folio_no"
        lsSql &= " and b.delete_flag = 'N'"
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid "
        lsSql &= " and c.delete_flag = 'N'"
        lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.finyear_gid in (" & finyearvalues & ")"
        lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) group by a.div_gid,a.finyear_gid"

        'lsSql &= " union all"

        'lsSql &= " select replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 1), ' ', -1),' ','') AS 'Investor First Name', "
        'lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2), ' ', -1),' ','') AS 'Investor Middle Name',"
        'lsSql &= " replace(SUBSTRING(fn_remove_extra_spaces(a.shar_holder),LENGTH(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2))+1),' ','') AS 'Investor Last Name',"
        'lsSql &= " case when SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' or null then"
        'lsSql &= " 'NA'"
        'lsSql &= " else "
        'lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1)"
        'lsSql &= " end"
        'lsSql &= " AS 'Father/Husband First Name', "
        'lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS 'Father/Husband Middle Name',"
        'lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS 'Father/Husband Last Name',"
        'lsSql &= " concat(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) as Address,"
        'lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'country') as Country,"
        'lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'state') as State,"
        'lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'district') as District,"
        'lsSql &= " a.holder1_pincode as Pincode,"
        'lsSql &= " a.folio_dpid as 'Folio Number',"
        'lsSql &= " '' as 'DP ID - Client ID Account Number',"
        'lsSql &= " 'Amount for unclaimed and unpaid dividend' as 'Investment Type',"
        ''lsSql &= " a.div_amount as 'Amount Transferred',"
        'lsSql &= " a.net_amount as 'Amount Transferred',"
        'lsSql &= " 'Date should be provided by STA Team' as 'Proposed Date of transfer to IEPF(DD-MON-YYYY)',"
        'lsSql &= " b.holder1_pan as 'PAN',"
        'lsSql &= " '' as 'Date of Birth(DD-MON-YYYY)',"
        'lsSql &= " '' as 'Aadhar Number',"
        'lsSql &= " b.nominee_name as 'Nominee Name',"
        'lsSql &= " b.holder2_name as 'Join Holder Name',"
        'lsSql &= " '' as 'Remarks',"
        'lsSql &= " 'No' as 'Is the Investment (amount / shares )under any litigation.',"
        'lsSql &= " 'No' as ' Is the shares transfer from unpaid suspense account (Yes/No)',"
        ''lsSql &= " c.finyear_code as 'Financial Year'"
        '' Add case for financial year aliasing
        'lsSql &= " CASE" & finyearAliasQuery
        'lsSql &= " ELSE c.finyear_code END AS 'Financial Year'"
        'lsSql &= " from div_trn_tdividend as a"
        'lsSql &= " left join sta_trn_tbenpost as b on a.folio_dpid = concat(b.dp_id,b.client_id)"
        'lsSql &= " and b.delete_flag = 'N'"
        'lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid "
        'lsSql &= " and c.delete_flag = 'N'"
        'lsSql &= " where true  and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        'lsSql &= " and a.finyear_gid in (" & finyearvalues & ")"
        'lsSql &= " and ( a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' or a.paid_date is null) "
        'lsSql &= " and b.benpost_date = '" & lnbenpost_date & "' group by a.div_gid,a.finyear_gid"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub LoadIEPF4Grid()
        Dim lsSql As String
        Dim finyearvalues As String = ""
        Dim lnbenpost_date As Date

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If RbdIEPF4.Checked = True And Finyear.CheckedItems.Count = 0 Then
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
        lnbenpost_date = gfExecuteScalar(lsSql, gOdbcConn)

        lsSql = ""
        lsSql &= " select "
        lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 1), ' ', -1),' ','') AS 'Investor First Name',"
        lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2), ' ', -1),' ','') AS 'Investor Middle Name',"
        lsSql &= " replace(SUBSTRING(fn_remove_extra_spaces(a.shar_holder),LENGTH(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2))+1),' ','') AS 'Investor Last Name',"
        lsSql &= " CASE WHEN SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' OR b.holder1_fh_name IS NULL THEN 'NA' ELSE SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) END AS 'Father/Husband First Name',"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS 'Father/Husband Middle Name',"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS 'Father/Husband Last Name',"
        lsSql &= " CONCAT(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) AS Address,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'country') AS Country,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'state') AS State,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'district') AS District,"
        lsSql &= " a.holder1_pincode AS Pincode,"
        lsSql &= " a.folio_dpid AS 'Folio Number',"
        lsSql &= " '' AS 'DP ID - Client ID Account Number',"
        lsSql &= " ifnull(b.folio_shares,0) AS 'Number of Shares',"
        lsSql &= " (ifnull(b.folio_shares,0) * 10) AS 'Nominal Value of Shares',"
        lsSql &= " 'Date should be provided by STA Team' AS 'Date of Transfer',"
        lsSql &= " b.holder1_pan_no AS 'PAN',"
        lsSql &= " '' AS 'Date of Birth(DD-MON-YYYY)',"
        lsSql &= " '' AS 'Aadhar Number',"
        lsSql &= " b.nominee_name AS 'Nominee Name',"
        lsSql &= " b.holder2_name AS 'Join Holder Name',"
        lsSql &= " '' AS 'Remarks',"
        lsSql &= " 'No' AS 'Is the Investment (amount / shares) under any litigation.',"
        lsSql &= " 'No' AS 'Is the shares transfer from unpaid suspense account (Yes/No)'"
        lsSql &= " FROM div_trn_tdividend a"
        lsSql &= " LEFT JOIN sta_trn_tfolio b ON a.folio_dpid = b.folio_no AND b.delete_flag = 'N'"
        lsSql &= " WHERE a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " AND a.finyear_gid IN (" & finyearvalues & ")"
        lsSql &= " AND (a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' OR a.paid_date IS NULL) "
        lsSql &= " AND a.folio_dpid not in (select distinct CONCAT(b.dp_id, b.client_id) from div_trn_tdividend a"
        lsSql &= " inner join sta_trn_tbenpost b on a.folio_dpid = CONCAT(b.dp_id, b.client_id) "
        lsSql &= " WHERE a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " AND a.finyear_gid IN (" & finyearvalues & ")"
        lsSql &= " AND (a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' OR a.paid_date IS NULL)"
        lsSql &= " AND b.benpost_date = '" & Format(lnbenpost_date, "yyyy-MM-dd") & "') "
        lsSql &= " GROUP BY a.folio_dpid"
        lsSql &= " HAVING COUNT(distinct a.finyear_gid) = " & Finyear.CheckedItems.Count & ""

        lsSql &= " UNION ALL"

        lsSql &= " select "
        lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 1), ' ', -1),' ','') AS 'Investor First Name',"
        lsSql &= " replace(SUBSTRING_INDEX(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2), ' ', -1),' ','') AS 'Investor Middle Name',"
        lsSql &= " replace(SUBSTRING(fn_remove_extra_spaces(a.shar_holder),LENGTH(SUBSTRING_INDEX(fn_remove_extra_spaces(a.shar_holder), ' ', 2))+1),' ','') AS 'Investor Last Name',"
        lsSql &= " CASE WHEN SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) = ' ' OR b.holder1_fh_name IS NULL THEN 'NA' ELSE SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 1), ' ', -1) END AS 'Father/Husband First Name',"
        lsSql &= " SUBSTRING_INDEX(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2), ' ', -1) AS 'Father/Husband Middle Name',"
        lsSql &= " SUBSTRING(b.holder1_fh_name,LENGTH(SUBSTRING_INDEX(b.holder1_fh_name, ' ', 2))+1) AS 'Father/Husband Last Name',"
        lsSql &= " CONCAT(a.holder1_addr1,',',a.holder1_addr2,',',a.holder1_addr3) AS Address,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'country') AS Country,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'state') AS State,"
        lsSql &= " fn_get_pincodedtl(a.holder1_pincode,'district') AS District,"
        lsSql &= " a.holder1_pincode AS Pincode,"
        lsSql &= " a.folio_dpid AS 'Folio Number',"
        lsSql &= " '' AS 'DP ID - Client ID Account Number',"
        lsSql &= " b.share_count AS 'Number of Shares',"
        lsSql &= " (b.share_count * 10) AS 'Nominal Value of Shares',"
        lsSql &= " 'Date should be provided by STA Team' AS 'Date of Transfer',"
        lsSql &= " b.holder1_pan AS 'PAN',"
        lsSql &= " '' AS 'Date of Birth(DD-MON-YYYY)',"
        lsSql &= " '' AS 'Aadhar Number',"
        lsSql &= " b.nominee_name AS 'Nominee Name',"
        lsSql &= " b.holder2_name AS 'Join Holder Name',"
        lsSql &= " '' AS 'Remarks',"
        lsSql &= " 'No' AS 'Is the Investment (amount / shares) under any litigation.',"
        lsSql &= " 'No' AS 'Is the shares transfer from unpaid suspense account (Yes/No)'"
        lsSql &= " FROM div_trn_tdividend a"
        lsSql &= " inner JOIN sta_trn_tbenpost b ON a.folio_dpid = CONCAT(b.dp_id, b.client_id) AND b.delete_flag = 'N'"
        lsSql &= " WHERE a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " AND a.finyear_gid IN (" & finyearvalues & ")"
        lsSql &= " AND (a.paid_date > '" & Format(dtpPaidDate.Value, "yyyy-MM-dd") & "' OR a.paid_date IS NULL)"
        lsSql &= " AND b.benpost_date = '" & Format(lnbenpost_date, "yyyy-MM-dd") & "'"
        lsSql &= " GROUP BY a.folio_dpid"
        lsSql &= " HAVING COUNT(distinct a.finyear_gid) = " & Finyear.CheckedItems.Count & ""


        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
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

    Private Sub RbdIEPF4_Click(sender As Object, e As EventArgs) Handles RbdIEPF4.Click
        If RbdIEPF4.Checked = True Then
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        ' Loop through each item in the CheckedListBox.
        For i As Integer = 0 To Finyear.Items.Count - 1
            ' Uncheck each item.
            Finyear.SetItemChecked(i, False)
        Next
        dgvList.DataSource = ""
    End Sub
End Class