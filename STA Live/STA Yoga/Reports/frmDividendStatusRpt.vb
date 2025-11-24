Public Class frmDividendStatusRpt
    Dim mnFolioId As Long = 0

    Private Sub frmQueue_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' Financial Year

        lsSql = ""
        lsSql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by finyear_code asc "

        Call gpBindCombo(lsSql, "finyear_code", "finyear_gid", CboFinyear, gOdbcConn)

        With cboStatus
            .Items.Clear()
            .Items.Add("All")
            .Items.Add("Paid")
            .Items.Add("Unpaid")
        End With
    End Sub

    Private Sub frmQueue_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
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
    Private Sub cboFileName_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles cboFileName.GotFocus
        Dim lsSql As String = ""

        lsSql = ""
        lsSql &= " select file_gid,concat(file_name,' ',ifnull(sheet_name,'')) as file_name from sta_trn_tfile "
        lsSql &= " where 1 = 1"

        If dtpFrom.Checked = True Then
            lsSql &= " and insert_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpTo.Checked = True Then
            lsSql &= " AND insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "'"
        End If

        lsSql &= " and file_type in (" & gnDividendStatus & ") "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by file_gid desc"

        gpBindCombo(lsSql, "file_name", "file_gid", cboFileName, gOdbcConn)
    End Sub

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lsTranType As String = ""

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cboStatus.SelectedIndex <> -1 Then
            lsTranType = cboStatus.Text
        Else
            lsTranType = ""
        End If

        If lsTranType = "" Then
            MessageBox.Show("Please select the Status !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboStatus.Focus()
            Exit Sub
        End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If CboFinyear.Text <> "" And CboFinyear.SelectedIndex <> -1 Then
            lsCond &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        End If

        If dtpFrom.Checked = True Then lsCond &= " and f.insert_date >= '" & Format(CDate(dtpFrom.Value), "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and f.insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "
        If cboFileName.Text <> "" And cboFileName.SelectedIndex >= 0 Then lsCond &= " and f.file_gid = '" & Val(cboFileName.SelectedValue.ToString) & "' "
        If mnFolioId > 0 Then lsCond &= " and a.dividend_gid = " & mnFolioId & " "

        Select Case cboStatus.Text.ToUpper
            Case "PAID"
                lsCond &= " and a.dividend_status = '8' "
            Case "UNPAID"
                lsCond &= " and a.dividend_status = '512' "
        End Select

        If lsCond = "" Then lsCond &= " and 1 = 2 "


        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " c.finyear_code as 'FinYear Code',"
        lsSql &= " a.depository as 'Depository',"
        lsSql &= " a.folio_no as 'Folio No',"
        lsSql &= " a.folio_name as 'Folio Name',"

        lsSql &= " d.status_desc as 'Dividend Status',"
        lsSql &= " date_format(a.paid_date,'%d-%m-%Y') as 'Paid Date',"
    
        lsSql &= " a.remarks as 'Remarks',"
        lsSql &= " a.dividendstatus_gid as 'Dividend Status Gid',"
        lsSql &= " a.dividend_gid as 'Dividend Gid'"

        lsSql &= " from sta_trn_tdividendstatus as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid=b.comp_gid  and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfile as f on a.file_gid = f.file_gid and f.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tstatus as d on a.dividend_status=d.status_value and d.status_type='dividend' and d.delete_flag='N'"
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.dividendstatus_gid asc"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\IEPFReport.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub dgvList_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvList.KeyDown
        Dim lnFolioId As Long = 0
        Dim frm As Object

        With dgvList
            If .CurrentCell.RowIndex >= 0 Then
                lnFolioId = .CurrentRow.Cells("Dividend Status Gid").Value

                Select Case e.KeyCode
                    Case Keys.F1
                        frm = New frmFolioDetail(lnFolioId)
                        frm.ShowDialog()
                    Case Keys.F2
                        frm = New frmCertReport(lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                    Case Keys.F3
                        frm = New frmCertDistReport(0, lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                    Case Keys.F4
                        frm = New frmFolioTranReport(lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                End Select
            End If
        End With
    End Sub
End Class