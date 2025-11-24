Public Class frmCertDistReport
    Dim mnCertId As Long = 0
    Dim mnFolioId As Long = 0

    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' status
        lsSql = ""
        lsSql &= " select status_value,status_desc from sta_mst_tstatus "
        lsSql &= " where status_type = 'Certificate' "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by status_desc asc "

        Call gpBindCombo(lsSql, "status_desc", "status_value", cboStatus, gOdbcConn)

        For Each ctrl In pnlSearch.Controls
            If TypeOf ctrl Is DateTimePicker Then
                ctrl.Value = Now
                ctrl.Checked = False
            End If
        Next

        If mnCertId > 0 Or mnFolioId > 0 Then btnRefresh.PerformClick()
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

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            If mnCertId = 0 And mnFolioId = 0 Then
                MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboCompany.Focus()
                Exit Sub
            End If
        End If

            If txtCertNo.Text <> "" Then lsCond &= " and a.cert_no = '" & Val(txtCertNo.Text) & "' "
            If txtFolioNo.Text <> "" Then lsCond &= " and b.folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%' "

            If Val(txtDistFrom.Text) > 0 And Val(txtDistTo.Text) = 0 Then lsCond &= " and " & Val(txtDistFrom.Text) & " between d.dist_from and d.dist_to "
            If Val(txtDistTo.Text) > 0 And Val(txtDistFrom.Text) = 0 Then lsCond &= " and " & Val(txtDistTo.Text) & " between d.dist_from and d.dist_to "

            If Val(txtDistTo.Text) > 0 And Val(txtDistFrom.Text) > 0 Then
            lsCond &= " and ((d.dist_from >= " & Val(txtDistFrom.Text) & " "
            lsCond &= " and d.dist_to <= " & Val(txtDistTo.Text) & ") "
            lsCond &= " or " & Val(txtDistTo.Text) & " between d.dist_from and d.dist_to "
            lsCond &= " or " & Val(txtDistTo.Text) & " between d.dist_from and d.dist_to) "
            End If

            If txtDistShares.Text <> "" Then lsCond &= " and d.dist_count = '" & Val(txtCertShares.Text) & "' "

            If txtHolderName.Text <> "" Then lsCond &= " and b.holder1_name like '" & QuoteFilter(txtHolderName.Text) & "%' "
            If txtPanNo.Text <> "" Then lsCond &= " and b.holder2_pan_no like '" & QuoteFilter(txtPanNo.Text) & "%' "
            If txtCertShares.Text <> "" Then lsCond &= " and a.share_count = '" & Val(txtCertShares.Text) & "' "
            If txtRemark.Text <> "" Then lsCond &= " and a.cert_remark like '" & QuoteFilter(txtRemark.Text) & "%' "

            If dtpIssueFrom.Checked = True Then lsCond &= " and a.issue_date >= '" & Format(dtpIssueFrom.Value, "yyyy-MM-dd") & "' "
            If dtpIssueTo.Checked = True Then lsCond &= " and a.issue_date <= '" & Format(dtpIssueTo.Value, "yyyy-MM-dd") & "' "

            If dtpLockinFrom.Checked = True Then lsCond &= " and '" & Format(dtpLockinFrom.Value, "yyyy-MM-dd") & "' between a.lockin_period_from and a.lockin_period_to "
            If dtpLockinTo.Checked = True Then lsCond &= " and '" & Format(dtpLockinTo.Value, "yyyy-MM-dd") & "' between a.lockin_period_from and a.lockin_period_to "

            If dtpHoldFrom.Checked = True Then lsCond &= " and a.hold_date >= '" & Format(dtpHoldFrom.Value, "yyyy-MM-dd") & "' "
            If dtpHoldTo.Checked = True Then lsCond &= " and a.hold_date <= '" & Format(dtpHoldTo.Value, "yyyy-MM-dd") & "' "

            If dtpReleaseFrom.Checked = True Then lsCond &= " and a.hold_release_date >= '" & Format(dtpReleaseFrom.Value, "yyyy-MM-dd") & "' "
            If dtpReleaseTo.Checked = True Then lsCond &= " and a.hold_release_date <= '" & Format(dtpReleaseTo.Value, "yyyy-MM-dd") & "' "

            If cboStatus.Text <> "" And cboStatus.SelectedIndex <> -1 Then
                lsCond &= " and a.cert_status = " & Val(cboStatus.SelectedValue.ToString) & " "
            End If

            If mnCertId > 0 Then lsCond &= " and d.cert_gid = " & mnCertId & " "
            If mnFolioId > 0 Then lsCond &= " and a.folio_gid = " & mnFolioId & " "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " d.dist_from as 'Dist From',"
            lsSql &= " d.dist_to as 'Dist To',"
            lsSql &= " d.dist_count as 'Dist Share Count',"
            lsSql &= " b.folio_no as 'Folio No',"
        lsSql &= " if(a.cert_sub_no = 1,cast(a.cert_no as nchar),concat(cast(a.cert_no as nchar),'-',cast(a.cert_sub_no as nchar))) as 'Cert No',"
            lsSql &= " a.share_count as 'Cert Share Count',"
            lsSql &= " b.holder1_name as 'Holder',"
            lsSql &= " make_set(a.cert_status," & gsCertStatusDesc & ") as 'Status',"
            lsSql &= " a.issue_date as 'Issue Date',"
            lsSql &= " a.lockin_period_from as 'Lockin From',"
            lsSql &= " a.lockin_period_to as 'Lockin To',"
            lsSql &= " a.hold_date as 'Hold Date',"
            lsSql &= " a.hold_release_date as 'Hold Release Date',"
            lsSql &= " a.expired_date as 'Expired Date',"
            lsSql &= " a.cert_remark as 'Remark',"
            lsSql &= " b.folio_sno as 'Folio SNo',"
            lsSql &= " b.holder1_fh_name as 'Holder F/H Name',"
            lsSql &= " b.holder1_pan_no as 'Holder Pan No',"
            lsSql &= " a.cert_status as 'Status Value',"
            lsSql &= " a.comp_gid as 'Comp Id',"
            lsSql &= " a.folio_gid as 'Folio Id',"
            lsSql &= " a.cert_gid as 'Cert Id',"
            lsSql &= " d.certdist_gid as 'Cert Dist Id',"
            lsSql &= " d.file_gid as 'File Id' "
            lsSql &= " from sta_trn_tcertdist as d "
            lsSql &= " inner join sta_trn_tcert as a on d.cert_gid = a.cert_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tfolio as b on a.folio_gid = b.folio_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and b.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and d.delete_flag = 'N' "
            lsSql &= " order by a.comp_gid,d.dist_from asc"

            gpPopGridView(dgvList, lsSql, gOdbcConn)

            For i = 0 To dgvList.ColumnCount - 1
                dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
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

    Public Sub New(CertId As Long, FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnCertId = CertId
        mnFolioId = FolioId
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles dtpLockinTo.ValueChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles dtpLockinFrom.ValueChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles txtPanNo.TextChanged

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtHolderName.TextChanged

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub
End Class