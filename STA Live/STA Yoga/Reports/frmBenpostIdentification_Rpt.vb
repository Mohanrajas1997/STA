Public Class frmBenpostIdentification_Rpt
    Inherits System.Windows.Forms.Form



#Region "Local Declaration"
#End Region

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click


        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvReport, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboFileName_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles cboFileName.GotFocus
        Dim lsSql As String = ""

        lsSql = ""
        lsSql &= " select file_gid,concat(file_name,' ',ifnull(sheet_name,'')) as file_name from sta_trn_tfile "
        lsSql &= " where 1 = 1"



        lsSql &= " and file_type in (" & gnFileBenpostCDSL & "," & gnFileBenpostNSDL & ") "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by file_gid desc"

        gpBindCombo(lsSql, "file_name", "file_gid", cboFileName, gOdbcConn)
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim i As Integer
        Dim lnResult As Long
        Try
            lsCond = ""
            If dtpBenpostFrom.Checked = True Then lsCond &= " and b.benpost_date >= '" & Format(CDate(dtpBenpostFrom.Value), "yyyy-MM-dd") & "' "
            If dtpBenpostTo.Checked = True Then lsCond &= " and b.benpost_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpBenpostTo.Value), "yyyy-MM-dd") & "' "

            If cboFileName.Text <> "" And cboFileName.SelectedIndex >= 0 Then lsCond &= " and b.file_gid = '" & Val(cboFileName.SelectedValue.ToString) & "' "


            If lsCond = "" Then lsCond &= " and 1 = 1 "

            'lsFld = ""
            'lsFld &= " comp_gid as 'Company Gid',"
            'lsFld &= " comp_code as 'Company Code',"
            'lsFld &= " comp_short_code as 'Company Short Code',"
            'lsFld &= " comp_name as 'Company Name',"
            'lsFld &= " isin_id as 'Isin Id'"

            lsSql = ""
            lsSql &= " truncate table sta_tmp_benpost_identification "
            lnResult = Val(gfExecuteScalar(lsSql, gOdbcConn))

            lsSql = ""
            lsSql &= " insert into sta_tmp_benpost_identification(comp_gid,depository_code) "
            lsSql &= " select distinct b.comp_gid ,b.depository_code "
            lsSql &= " from sta_trn_tbenpost as b where b.delete_flag='N' "
            lsSql &= lsCond
            lnResult = gfInsertQry(lsSql, gOdbcConn)

            lsSql = ""
            If cboDeposit.Text = "ALL" Or cboDeposit.Text = "" Then
                lsSql &= " select "
                'lsSql &= " comp_gid as 'Company Gid',"
                lsSql &= " isin_id as 'ISIN Id',"
                'lsSql &= " comp_code as 'Company Code',"
                'lsSql &= " comp_short_code as 'Company Short Code',"
                lsSql &= " comp_name as 'Company Name',"
                lsSql &= " depository_code as 'Depository Type',"
                lsSql &= " ifnull(start_date,'') as 'Start Date',"
                lsSql &= " case when active_flag = 'Y' then 'Active' else 'Inactive' end as 'Active Status',"
                lsSql &= " case when sum(cdsl)>0 then 'Received' when depository_code = 'NSDL' then ' - '  else fn_chk_cdslnsdlfolio_avilable(comp_gid,'CDSL') end as 'CDSL Status',"
                lsSql &= " case when sum(nsdl)>0 then 'Received' when depository_code = 'CDSL' then ' - ' else fn_chk_cdslnsdlfolio_avilable(comp_gid,'NSDL') end as 'NSDL Status'"
                lsSql &= " from ("
                lsSql &= " select distinct a.comp_gid,a.comp_code,a.comp_short_code,a.comp_name,"
                lsSql &= " a.isin_id,ifnull(b.comp_gid,0) as cdsl,0 as nsdl,a.active_flag,a.depository_code,a.start_date"
                lsSql &= " from sta_mst_tcompany as a left join sta_tmp_benpost_identification as b"
                lsSql &= " on a.comp_gid=b.comp_gid and b.depository_code='C' "
                lsSql &= " where a.delete_flag='N'"
                lsSql &= " union"
                lsSql &= " select distinct a.comp_gid,a.comp_code,a.comp_short_code,a.comp_name,a.isin_id,"
                lsSql &= " 0 as cdsl,ifnull(b.comp_gid,0) as nsdl,a.active_flag,a.depository_code,a.start_date"
                lsSql &= " from sta_mst_tcompany as a inner join"
                lsSql &= " sta_tmp_benpost_identification as b"
                lsSql &= " on a.comp_gid=b.comp_gid and b.depository_code='N' "
                lsSql &= " where a.delete_flag='N') as d"
                lsSql &= " group by comp_gid,comp_code,comp_short_code,comp_name,isin_id order by start_date;"
            ElseIf cboDeposit.Text = "CDSL" Or cboDeposit.Text = "NSDL" Then
                lsSql &= " select distinct a.isin_id as 'ISIN Id',a.comp_name as 'Company Name',"
                lsSql &= " " & "'" & cboDeposit.Text & "' as 'Depository Type'"
                lsSql &= " from sta_mst_tcompany as a inner join sta_tmp_benpost_identification as b"
                lsSql &= " on a.comp_gid=b.comp_gid and b.depository_code = '" & cboDeposit.SelectedValue & "'"
                lsSql &= " and ifnull(b.comp_gid,0) > 0 where a.delete_flag='N';"
                'ElseIf cboDeposit.Text = "NSDL" Then
                '    lsSql &= " select "
                '    lsSql &= " comp_gid as 'Company Gid',"
                '    lsSql &= " comp_code as 'Company Code',"
                '    lsSql &= " comp_short_code as 'Company Short Code',"
                '    lsSql &= " comp_name as 'Company Name',"
                '    lsSql &= " isin_id as 'ISIN Id',"
                '    lsSql &= " 'NSDL' as 'Type'"
                '    lsSql &= " from ("
                '    lsSql &= " select distinct a.comp_gid,a.comp_code,a.comp_short_code,a.comp_name,a.isin_id,"
                '    lsSql &= " ifnull(b.comp_gid,0) as nsdl"
                '    lsSql &= " from sta_mst_tcompany as a left join"
                '    lsSql &= " sta_tmp_benpost_identification as b"
                '    lsSql &= " on a.comp_gid=b.comp_gid and b.depository_code='N' "
                '    lsSql &= " and ifnull(b.comp_gid,0) > 0 and a.delete_flag='N') as d"
                '    lsSql &= " group by comp_gid,comp_code,comp_short_code,comp_name,isin_id;"
            End If

            'lsSql &= " from sta_mst_tcompany "
            'lsSql &= " where delete_flag='N' and comp_gid not in(select d.comp_gid from sta_trn_tbenpost as d "
            'lsSql &= " inner join sta_trn_tfile as f on d.file_gid = f.file_gid and f.delete_flag = 'N' "
            'lsSql &= " where true "
            'lsSql &= lsCond
            'lsSql &= " and d.delete_flag = 'N' "
            'lsSql &= ")"


            Call gpPopGridView(dgvReport, lsSql, gOdbcConn)

            For i = 0 To dgvReport.Columns.Count - 1
                dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lsSql As String

        Try
            ' depository
            lsSql = ""
            lsSql &= " select depository_code,depository_name from sta_mst_tdepository "
            lsSql &= " where delete_flag = 'N' "
            lsSql &= " order by depository_name "

            Call gpBindCombo(lsSql, "depository_name", "depository_code", cboDeposit, gOdbcConn)

            Call frmDtpCtrClear(Me)

            btnClear.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvReport
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvReport.Top + dgvReport.Height + 6
        pnlExport.Width = Me.Width
        btnExport.Left = pnlExport.Width - btnExport.Width - 24
    End Sub
End Class