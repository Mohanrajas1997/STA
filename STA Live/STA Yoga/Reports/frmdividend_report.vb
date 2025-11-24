Imports MySql.Data.MySqlClient
Public Class frmdividend_report
    Private Sub frmdividend_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql, lsSql, ls_Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        lsSql = ""
        lsSql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by finyear_code asc "

        Call gpBindCombo(lsSql, "finyear_code", "finyear_gid", cbofinyear, gOdbcConn)

        ls_Sql = ""
        ls_Sql &= " select interim_gid,interim_name FROM sta.sta_mst_tinterim "
        ls_Sql &= " where delete_flag = 'N' "
        ls_Sql &= " order by interim_name asc "

        Call gpBindCombo(ls_Sql, "interim_name", "interim_gid", cbinterim, gOdbcConn)

        dtp_alphaindex.Value = Now
        dtp_alphaindex.Checked = False
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim lsSql As String
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        Dim lsCond As String = ""
        Dim lsdate As String = ""
        Dim lsdatephy As String = ""
       
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbofinyear.Text <> "" And cbofinyear.SelectedIndex <> -1 Then lsCond &= " and c.finyear_gid = " & Val(cbofinyear.SelectedValue.ToString) & " "
        If cbinterim.Text <> "" And cbinterim.SelectedIndex <> -1 Then lsCond &= " and f.interim_gid = " & Val(cbinterim.SelectedValue.ToString) & " "

        If dtp_alphaindex.Checked = True Then lsdate &= " and a.benpost_date = '" & Format(dtp_alphaindex.Value, "yyyy-MM-dd") & "' "
        lsdatephy = Format(dtp_alphaindex.Value, "yyyy-MM-dd")

        lsSql = ""
        lsSql &= " SELECT  @s:=@s+1 Sno,g.comp_code as company,"
        lsSql &= " e.finyear_code as Financial_Year_Code,f.interim_code as Interim_Code,"
        lsSql &= " null as DP_ID,"
        lsSql &= " a.folio_no as Folio_ClientId,"
        lsSql &= " 'Physical' as Folio_ClientId_Type,"
        lsSql &= " ifnull(if(d.bene_name = '',null,d.bene_name),a.holder1_name) as Holder_Name,"
        lsSql &= " fn_sta_get_folioshares(a.folio_gid,'" & lsdatephy & "') as Share_Count,"
        lsSql &= " (c.dividend_percentage/10) as Dividend_Per_Share,"
        lsSql &= " (fn_sta_get_folioshares(a.folio_gid,'" & lsdatephy & "')*c.dividend_percentage*10/100) as dividend_amount,"
        lsSql &= " ifnull(if(d.currency_code = '',null,d.currency_code),'INR') as Currency_Code,"
        lsSql &= " '1' as Currency_Value,"
        lsSql &= " (fn_sta_get_folioshares(a.folio_gid,'" & lsdatephy & "')*c.dividend_percentage*10/100) as Currency_Amount,"
        lsSql &= " if(ifnull(if(d.payment_mode = '',null,d.payment_mode),if(a.bank_acc_no != '' and a.bank_acc_type != '' and a.bank_micr_code != '','ECS','Warrant')) = 'Warrant' and "
        lsSql &= " (fn_sta_get_folioshares(a.folio_gid,'" & lsdatephy & "')*c.dividend_percentage*10/100) > 100000,'Demand Draft',"
        lsSql &= " ifnull(if(d.payment_mode = '',null,if(d.payment_mode='D','Demand Draft',if(d.payment_mode='F','Dollar Draft',d.payment_mode))), "
        lsSql &= " if(a.bank_acc_no != '' and a.bank_acc_type != '' and a.bank_micr_code != '','ECS','Warrant'))) as Payment_Mode,"
        lsSql &= " a.bank_name as Bank_Name,"
        lsSql &= " a.bank_branch as Bank_Branch,"
        lsSql &= " a.bank_acc_no as Bank_ACC_No,"
        lsSql &= " if(a.bank_acc_type = '13','Saving',if(a.bank_acc_type = '10','Saving',if(a.bank_acc_type = '11','Current',if(a.bank_acc_type = 'C','Current',if(a.bank_acc_type='S','Saving',a.bank_acc_type))))) as Bank_ACC_Type,"
        lsSql &= " a.bank_micr_code as Micr_Code,"
        lsSql &= " a.bank_ifsc_code as IFSC_Code,"
        lsSql &= " a.folio_addr1 as Addr1,"
        lsSql &= " a.folio_addr2 as Addr2,"
        lsSql &= " a.folio_addr3 as Addr3,"
        lsSql &= " null as Addr4,"
        lsSql &= " a.folio_city as City,"
        lsSql &= " a.folio_state as State,a.folio_country as Country,a.folio_pincode as Pincode,"
        lsSql &= " a.folio_mail_id as Email"
        lsSql &= " FROM sta_trn_tfolio a "
        lsSql &= " left join sta_mst_tdividendacc c on c.comp_gid = a.comp_gid and c.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tpaymentinfo d on d.comp_gid = a.comp_gid and a.folio_no = d.folioclient_id and d.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tfinyear e on e.finyear_gid = c.finyear_gid and e.delete_flag = 'N'"
        lsSql &= " left join sta_mst_tinterim f on f.interim_code = c.interim_code and f.delete_flag='N'"
        lsSql &= " left join sta_mst_tcompany g on g.comp_gid = a.comp_gid and g.delete_flag='N'"
        lsSql &= " ,(SELECT @s:= 0) AS s where true "
        lsSql &= lsCond
        lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsdatephy & "') > 0 and a.folio_no NOT IN ('00888888','00999999') and a.delete_flag = 'N' "
        lsSql &= " union"
        lsSql &= " SELECT @s:=@s+1 Sno,g.comp_code as company,"
        lsSql &= " e.finyear_code as Financial_Year_Code,f.interim_code as Interim_Code,"
        lsSql &= " a.dp_id as DP_ID,a.client_id as Folio_ClientId, "
        lsSql &= " case when a.depository_code = 'N' then 'NSDL' else 'CDSL' end as Folio_ClientId_Type,"
        lsSql &= " ifnull(if(d.bene_name = '',null,d.bene_name),a.holder1_name) as Holder_Name ,"
        lsSql &= " a.share_count as Share_Count,"
        lsSql &= " (c.dividend_percentage/10) as Dividend_Per_Share,"
        lsSql &= " (a.share_count*c.dividend_percentage*10/100) as dividend_amount,"
        lsSql &= " ifnull(if(d.currency_code = '',null,d.currency_code),'INR') as Currency_Code,"
        lsSql &= " '1' as Currency_Value,"
        lsSql &= " (a.share_count*c.dividend_percentage*10/100) as Currency_Amount,"
        lsSql &= " if(ifnull(if(d.payment_mode = '',null,d.payment_mode),if(a.bank_acc_no != '' and a.bank_acc_type != '' and a.bank_micr_code != '','ECS','Warrant')) = 'Warrant' and "
        lsSql &= " (a.share_count*c.dividend_percentage*10/100) > 100000,'Demand Draft', "
        lsSql &= " ifnull(if(d.payment_mode = '',null,if(d.payment_mode='D','Demand Draft',if(d.payment_mode='F','Dollar Draft',d.payment_mode))),"
        lsSql &= " if(a.bank_acc_no != '' and a.bank_acc_type != '' and a.bank_micr_code != '','ECS','Warrant'))) as Payment_Mode,"
        lsSql &= " a.bank_name as Bank_Name,"
        lsSql &= " a.bank_addr3 as Bank_Branch,"
        lsSql &= " a.bank_acc_no as Bank_ACC_No,"
        lsSql &= " if(a.bank_acc_type = '13','Saving',if(a.bank_acc_type = '10','Saving',if(a.bank_acc_type = '11','Current',if(a.bank_acc_type = 'C','Current',if(a.bank_acc_type='S','Saving',a.bank_acc_type))))) as Bank_ACC_Type,"
        lsSql &= " a.bank_micr_code as Micr_Code,"
        lsSql &= " a.bank_ifsc_code as IFSC_Code,"
        lsSql &= " a.holder1_addr1 as Addr1,"
        lsSql &= " a.holder1_addr2 as Addr2,a.holder1_addr3 as Addr3,'' as Addr4,"
        lsSql &= " a.holder1_city as City,"
        lsSql &= " a.holder1_state as State,a.holder1_country as Country,a.holder1_pin as Pincode,"
        lsSql &= " a.holder1_email_id as Email"
        lsSql &= " FROM sta_trn_tbenpost a"
        lsSql &= " left join sta_mst_tdividendacc c on c.comp_gid = a.comp_gid and c.delete_flag = 'N'"
        lsSql &= " left join sta_mst_tpaymentinfo d on d.comp_gid = a.comp_gid and a.client_id = d.folioclient_id and d.delete_flag = 'N'"
        lsSql &= " left join sta_mst_tfinyear e on e.finyear_gid = c.finyear_gid and e.delete_flag = 'N'"
        lsSql &= " left join sta_mst_tinterim f on f.interim_code = c.interim_code and f.delete_flag='N'"
        lsSql &= " left join sta_mst_tcompany g on g.comp_gid = a.comp_gid and g.delete_flag='N'"
        lsSql &= " ,(SELECT @s:= 0) AS s where true  "
        lsSql &= lsCond
        lsSql &= lsdate
        lsSql &= " and a.delete_flag = 'N'"

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        If (dt.Rows.Count <= 0) Then
            MessageBox.Show("Please Enter Valid Benpost Date")
        Else

            With dgvList
                .DataSource = dt
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i

                txtTotRec.Text = "Total Records : " & .RowCount.ToString
            End With
        End If
    End Sub

    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

End Class