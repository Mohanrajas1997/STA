Public Class frmDividendShareCapitalRpt
    Private Sub frmDividendShareCapitalRpt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        Sql = ""
        Sql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by finyear_code asc "

        Call gpBindCombo(Sql, "finyear_code", "finyear_gid", CboFinyear, gOdbcConn)

        Sql = ""
        Sql &= " select paymode_gid,paymode_desc FROM sta_mst_tpaymode "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by paymode_desc asc "

        Call gpBindCombo(Sql, "paymode_desc", "paymode_gid", CboIssPaymode, gOdbcConn)
    End Sub

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        'If (CboFinyear.Text = "" Or CboFinyear.SelectedIndex = -1) Then
        '    MessageBox.Show("Please select the finyear !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    CboFinyear.Focus()
        '    Exit Sub
        'End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If CboFinyear.Text <> "" And CboFinyear.SelectedIndex <> -1 Then
            lsCond &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        End If

        If txtFoliodpid.Text <> "" Then
            lsCond &= " and a.folio_dpid = '" & QuoteFilter(txtFoliodpid.Text.ToString) & "'"
        End If

        If dtpIssuedatefrm.Checked = True Then lsCond &= " and a.issue_date >= '" & Format(dtpIssuedatefrm.Value, "yyyy-MM-dd") & "' "
        If dtpissueDateTo.Checked = True Then lsCond &= " and a.issue_date <= '" & Format(dtpissueDateTo.Value, "yyyy-MM-dd") & "' "

        If dtpPaidDatefrm.Checked = True Then lsCond &= " and a.paid_date >= '" & Format(dtpPaidDatefrm.Value, "yyyy-MM-dd") & "' "
        If dtpPaidDateto.Checked = True Then lsCond &= " and a.paid_date <= '" & Format(dtpPaidDateto.Value, "yyyy-MM-dd") & "' "

        Select Case CboIssPaymode.Text.ToUpper
            Case "WARRANT"
                lsCond &= " and a.div_pay_mode = 'W' "
            Case "DEMAND DRAFT"
                lsCond &= " and a.div_pay_mode = 'D' "
            Case "ECS"
                lsCond &= " and a.div_pay_mode = 'E' "
            Case "DOLLAR DRAFT"
                lsCond &= " and a.div_pay_mode = 'F' "
        End Select

        Select Case CboDivstatus.Text.ToUpper
            Case "UNPAID"
                lsCond &= " and a.div_status = 'U' "
            Case "PAID"
                lsCond &= " and a.div_status = 'P' "
            Case "LATER"
                lsCond &= " and a.div_status = 'L' "
        End Select

        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql = ""
        lsSql &= " select distinct"
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " c.finyear_code as 'FinYear Code',"
        lsSql &= " a.folio_dpid as 'Folio Dpid',"
        lsSql &= " a.shar_holder as 'Share Holder',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " a.div_rate as 'Dividend Rate',"
        lsSql &= " a.div_amount as 'Dividend Amount',"
        lsSql &= " a.tds_per as 'TDS Percentage',"
        lsSql &= " a.tds_amount as 'TDS Amount',"
        lsSql &= " a.net_amount as 'Net Amount',"
        lsSql &= " a.warrant_no as 'Warrant No',"
        'lsSql &= " a.curr_amount as 'Foregin Currency',"
        lsSql &= " a.div_date as 'Dividend Date',"
        lsSql &= " d.paymode_desc as 'Dividend Paymode',"
        lsSql &= " a.div_ref_no as 'Dividend PayRefNo',"
        lsSql &= " a.div_status as 'Dividend Status',"
        lsSql &= " a.paid_date as 'Paid Date',"
        'lsSql &= " d.paymode_desc as 'Paid Pay Mode',"
        'lsSql &= " a.paid_ref_no as 'Paid Ref No',"
        'lsSql &= " a.joint1_name as 'Join 1',"
        'lsSql &= " a.joint2_name as 'Joint 2',"
        lsSql &= " a.holder1_pan as 'Pan No',"
        lsSql &= " a.holder1_email as 'Email Id',"
        lsSql &= " a.holder1_addr1 as 'Address 1',"
        lsSql &= " a.holder1_addr2 as 'Address 2',"
        lsSql &= " a.holder1_addr3 as 'Address 3',"
        lsSql &= " a.holder1_city as 'City',"
        'lsSql &= " a.holder1_state as 'State',"
        'lsSql &= " a.holder1_country as 'Country',"
        lsSql &= " a.holder1_pincode as 'Pincode',"
        lsSql &= " a.holder1_bank_name as 'Bank Name',"
        'lsSql &= " a.holder1_bank_branch as 'Bank Branch',"
        lsSql &= " a.holder1_acc_no as 'Bank Acc No',"
        'lsSql &= " a.holder1_acc_type as 'Bank Acc Type',"
        lsSql &= " a.holder1_micr_code as 'Micr Code',"
        lsSql &= " a.holder1_ifsc_code as 'IFSC Code',"
        lsSql &= " a.holder1_category as 'Category',"
        lsSql &= " a.reject_reason as 'Rejection reason',"
        lsSql &= " a.div_remark as 'Dividend Remark'"

        'lsSql &= " a.issue_date as 'Issue Date',"
        'lsSql &= " a.issue_pay_mode as 'Issue Pay Mode',"
        'lsSql &= " a.issue_ref_no as 'Issue Ref No',"

        'lsSql &= " a.div_gid as 'Dividend Gid'"
        lsSql &= " from div_trn_tdividend as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid  and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tpaymode as d on a.div_pay_mode = d.paymode_code  and d.delete_flag='N'"

        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by c.finyear_code asc"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub frmQueue_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Dividend Payment Status.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class