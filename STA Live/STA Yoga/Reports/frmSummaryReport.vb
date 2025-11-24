Public Class frmSummaryReport
    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' report
        With cboReport
            .Items.Clear()
            .Items.Add("Folio")
            .Items.Add("Folio Zero Shares")
            .Items.Add("Certificate")
            .Items.Add("Certificate Hold")
            .Items.Add("Certificate Lockin Period")
            .Items.Add("Certificate Inactive")
            .Items.Add("Depository Folio")
            .Items.Add("Depository Certificate")
            .Items.Add("Monthly Report")
        End With
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

    Private Sub LoadFolio()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " count(*) as 'Folio Count',"
        lsSql &= " sum(a.folio_shares) as 'Total Shares' "
        lsSql &= " from sta_trn_tfolio as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " group by b.comp_name "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadFolioZeroShares()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsCond &= " and a.folio_shares <= 0 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.folio_no as 'Folio No',"
        lsSql &= " a.holder1_name as 'Holder',"
        lsSql &= " a.folio_shares as 'Shares' "
        lsSql &= " from sta_trn_tfolio as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250
            .Columns("Holder").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadDepositoryFolio()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.folio_no as 'Folio No',"
        lsSql &= " a.holder1_name as 'Holder1',"
        lsSql &= " a.folio_shares as 'Total Shares' "
        lsSql &= " from sta_trn_tfolio as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tdepository as c on a.folio_no = c.folio_no and c.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadDepositoryCertificate()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsCond &= " and a.cert_status <> " & gnCertInactive & " "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.cert_no as 'Certificate No',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " make_set(a.cert_status," & gsCertStatusDesc & ") as 'Status',"
        lsSql &= " c.folio_no as 'Folio No',"
        lsSql &= " c.holder1_name as 'Holder',"
        lsSql &= " a.issue_date as 'Issue Date',"
        lsSql &= " a.lockin_period_from as 'Lockin From',"
        lsSql &= " a.lockin_period_to as 'Lockin To',"
        lsSql &= " a.hold_date as 'Hold Date',"
        lsSql &= " a.hold_release_date as 'Hold Release Date',"
        lsSql &= " a.cert_remark as 'Remark' "
        lsSql &= " from sta_trn_tcert as a "
        lsSql &= " inner join sta_trn_tfolio as c on a.folio_gid = c.folio_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tdepository as d on c.folio_no = d.folio_no and d.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250
            .Columns("Folio No").Width = 150
            .Columns("Holder").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadCertificate()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " count(distinct folio_gid) as 'Folio Count',"
        lsSql &= " count(*) as 'Total Certificates',"
        lsSql &= " sum(if(a.cert_status <> " & gnCertInactive & ",1,0)) as 'Valid Certificates',"
        lsSql &= " sum(if(a.cert_status = " & gnCertActive & ",1,0)) as 'Active Certificates',"
        lsSql &= " sum(if(a.cert_status = " & gnCertHold & ",1,0)) as 'Hold Certificates',"
        lsSql &= " sum(if(a.cert_status = " & gnCertInactive & ",1,0)) as 'Invalid Certificates',"
        lsSql &= " sum(a.share_count) as 'Total Shares',"
        lsSql &= " sum(if(a.cert_status <> " & gnCertInactive & ",a.share_count,0)) as 'Valid Shares',"
        lsSql &= " sum(if(a.cert_status = " & gnCertActive & ",a.share_count,0)) as 'Active Shares',"
        lsSql &= " sum(if(a.cert_status = " & gnCertHold & ",a.share_count,0)) as 'Hold Shares',"
        lsSql &= " sum(if(a.cert_status = " & gnCertInactive & ",a.share_count,0)) as 'Invalid Shares' "
        lsSql &= " from sta_trn_tcert as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " group by b.comp_name "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadCertificateHold()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsCond &= " and a.cert_status = " & gnCertHold & " "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.cert_no as 'Certificate No',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " make_set(a.cert_status," & gsCertStatusDesc & ") as 'Status',"
        lsSql &= " c.folio_no as 'Folio No',"
        lsSql &= " c.holder1_name as 'Holder',"
        lsSql &= " a.issue_date as 'Issue Date',"
        lsSql &= " a.lockin_period_from as 'Lockin From',"
        lsSql &= " a.lockin_period_to as 'Lockin To',"
        lsSql &= " a.hold_date as 'Hold Date',"
        lsSql &= " a.hold_release_date as 'Hold Release Date',"
        lsSql &= " a.cert_remark as 'Remark' "
        lsSql &= " from sta_trn_tcert as a "
        lsSql &= " inner join sta_trn_tfolio as c on a.folio_gid = c.folio_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250
            .Columns("Folio No").Width = 150
            .Columns("Holder").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadCertificateLockinPeriod()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsCond &= " and curdate() between a.lockin_period_from and a.lockin_period_to "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.cert_no as 'Certificate No',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " make_set(a.cert_status," & gsCertStatusDesc & ") as 'Status',"
        lsSql &= " c.folio_no as 'Folio No',"
        lsSql &= " c.holder1_name as 'Holder',"
        lsSql &= " a.issue_date as 'Issue Date',"
        lsSql &= " a.lockin_period_from as 'Lockin From',"
        lsSql &= " a.lockin_period_to as 'Lockin To',"
        lsSql &= " a.hold_date as 'Hold Date',"
        lsSql &= " a.hold_release_date as 'Hold Release Date',"
        lsSql &= " a.cert_remark as 'Remark' "
        lsSql &= " from sta_trn_tcert as a "
        lsSql &= " inner join sta_trn_tfolio as c on a.folio_gid = c.folio_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250
            .Columns("Folio No").Width = 150
            .Columns("Holder").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadCertificateInactive()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsCond &= " and a.cert_status = " & gnCertInactive & " "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.cert_no as 'Certificate No',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " make_set(a.cert_status," & gsCertStatusDesc & ") as 'Status',"
        lsSql &= " c.folio_no as 'Folio No',"
        lsSql &= " c.holder1_name as 'Holder',"
        lsSql &= " a.issue_date as 'Issue Date',"
        lsSql &= " a.lockin_period_from as 'Lockin From',"
        lsSql &= " a.lockin_period_to as 'Lockin To',"
        lsSql &= " a.hold_date as 'Hold Date',"
        lsSql &= " a.hold_release_date as 'Hold Release Date',"
        lsSql &= " a.cert_remark as 'Remark' "
        lsSql &= " from sta_trn_tcert as a "
        lsSql &= " inner join sta_trn_tfolio as c on a.folio_gid = c.folio_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250
            .Columns("Folio No").Width = 150
            .Columns("Holder").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadMonthlyReport()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.holder1_name as 'Mode',"
        lsSql &= " 1 as 'Share Holder',"
        lsSql &= " a.folio_shares as 'Total Shares' "
        lsSql &= " from sta_trn_tfolio as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tdepository as c on a.folio_no = c.folio_no and c.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        lsSql &= " union all "

        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " 'Physical' as 'Mode',"
        lsSql &= " count(*) as 'Share Holder',"
        lsSql &= " sum(a.folio_shares) as 'Total Shares' "
        lsSql &= " from sta_trn_tfolio as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tdepository as c on a.folio_no = c.folio_no and c.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and c.folio_no is null "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " group by b.comp_name "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .Columns("Company").Width = 250

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Select Case cboReport.Text.ToUpper
            Case "FOLIO"
                Call LoadFolio()
            Case "FOLIO ZERO SHARES"
                Call LoadFolioZeroShares()
            Case "CERTIFICATE"
                Call LoadCertificate()
            Case "CERTIFICATE HOLD"
                Call LoadCertificateHold()
            Case "CERTIFICATE INACTIVE"
                Call LoadCertificateInactive()
            Case "CERTIFICATE LOCKIN PERIOD"
                Call LoadCertificateLockinPeriod()
            Case "DEPOSITORY FOLIO"
                Call LoadDepositoryFolio()
            Case "DEPOSITORY CERTIFICATE"
                Call LoadDepositoryCertificate()
            Case "MONTHLY REPORT"
                Call LoadMonthlyReport()
        End Select
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