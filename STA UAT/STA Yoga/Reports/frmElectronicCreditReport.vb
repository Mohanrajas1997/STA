Public Class frmElectronicCreditReport

    Private Sub frmElectronicCreditReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
    End Sub

    Private Sub frmElectronicCreditReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""

        lsCond = ""

        If dtpConfDate.Checked = False Then
            MessageBox.Show("Please select the Confirm Date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpConfDate.Focus()
            Exit Sub
        End If

        If dtpConfDate.Checked = True Then lsCond &= " and d.status_update_date = '" & Format(dtpConfDate.Value, "yyyy-MM-dd") & "' "

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If lsCond = "" Then lsCond = " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " d.status_update_date as 'Confirm Date',"
        lsSql &= " c.comp_name as 'Company',"
        lsSql &= " c.isin_id as 'ISIN No',"
        lsSql &= " b.trantype_desc as 'Doc Type',"
        lsSql &= " case when e.depository_code = 'N' then 'NSDL'  when e.depository_code = 'C' then 'CDSL' else '' end as 'Depository',"
        lsSql &= " sum(e.share_count) as 'No of shares'"
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as b on a.tran_code = b.trantype_code and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tupload as d on a.upload_gid = d.upload_gid and d.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tdematpend as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as f on a.folio_gid = f.folio_gid and f.delete_flag = 'N'  "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' and a.tran_code = 'DM' and a.chklst_disc = 0"
        lsSql &= " group by e.depository_code,c.comp_name"
        lsSql &= " UNION ALL "
        lsSql &= " select "
        lsSql &= " d.status_update_date as 'Confirm Date',"
        lsSql &= " c.comp_name as 'Company',"
        lsSql &= " c.isin_id as 'ISIN No',"
        lsSql &= " b.trantype_desc as 'Doc Type',"
        lsSql &= " case when f.folio_no = '00888888' then 'CDSL' when f.folio_no = '00999999' then 'NSDL' else '' end as 'Depository',"
        lsSql &= " sum(e.share_count) as 'No of shares'"
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as b on a.tran_code = b.trantype_code and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tupload as d on a.upload_gid = d.upload_gid and d.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tcaentry as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as f on a.folio_gid = f.folio_gid and f.delete_flag = 'N'  "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' and a.tran_code = 'AM' and a.chklst_disc = 0"
        lsSql &= " group by f.folio_no,c.comp_name"
        lsSql &= " UNION ALL "
        lsSql &= " select "
        lsSql &= " d.status_update_date as 'Confirm Date',"
        lsSql &= " c.comp_name as 'Company',"
        lsSql &= " c.isin_id as 'ISIN No',"
        lsSql &= " b.trantype_desc as 'Doc Type',"
        lsSql &= " case when f.folio_no = '00888888' then 'CDSL' when f.folio_no = '00999999' then 'NSDL' else '' end as 'Depository',"
        lsSql &= " sum(e.tot_nominal_amtof_shares) as 'No of shares'"
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as b on a.tran_code = b.trantype_code and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tupload as d on a.upload_gid = d.upload_gid and d.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tcaiepfheader as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as f on a.folio_gid = f.folio_gid and f.delete_flag = 'N'  "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' and a.tran_code = 'PI' and a.chklst_disc = 0"
        lsSql &= " group by f.folio_no,c.comp_name"

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        dgvList.DataSource = Nothing
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\ElectronicCredit_RPT.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class