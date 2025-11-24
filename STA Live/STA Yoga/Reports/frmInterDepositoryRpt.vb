Imports System.Data
Public Class frmInterDepositoryRpt
    Dim mnFolioId As Long = 0
    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)


        dtpFrom.Value = Now
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False



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
        Dim lsFrom As String = ""
        Dim lsTo As String = ""

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If
        If mnFolioId > 0 Then lsCond &= " and a.depo_gid = " & mnFolioId & " "

        lsFrom = Format(dtpFrom.Value, "yyyy-MM-dd")
        lsTo = Format(dtpTo.Value, "yyyy-MM-dd")

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'COMPANY NAME',"
        lsSql &= " date_format(a.depo_date,'%d-%m-%Y') as 'DATE',"
        lsSql &= " a.debit_nsdl as 'NSDL DEBIT',"
        lsSql &= " a.credit_nsdl as 'NSDL CREDIT',"
        lsSql &= " a.remat_nsdl as 'NSDL REMAT',"
        lsSql &= " a.confirmation_nsdl as 'NSDL DEMAT',"
        lsSql &= " a.nsdl_ca_addition as 'NSDL CA ADDITION',"
        lsSql &= " a.nsdl_ca_sub_tran as 'NSDL CA SUBTRACTION',"
        lsSql &= " a.nsdl_total as 'NSDL TOTAL',"
        lsSql &= " cast(a.nsdl_per as nchar) as 'NSDL %',"
        lsSql &= " a.debit_cdsl as 'CDSL DEBIT',"
        lsSql &= " a.credit_cdsl as 'CDSL CREDIT',"
        lsSql &= " a.remat_cdsl as 'CDSL REMAT',"
        lsSql &= " a.confirmation_cdsl as 'CDSL DEMAT',"
        lsSql &= " a.cdsl_ca_addition as 'CDSL CA ADDITION',"
        lsSql &= " a.cdsl_ca_sub_tran as 'CDSL CA SUBTRACTION',"
        lsSql &= " a.cdsl_total as 'CDSL TOTAL',"
        lsSql &= " cast(a.cdsl_per as nchar) as 'CDSL %',"
        lsSql &= "a.debit_physical as 'PHYSICAL REMAT',"
        lsSql &= "a.credit_physical as 'PHYSICAL DEMAT',"
        lsSql &= "a.physical_total as 'PHYSICAL TOTAL',"
        lsSql &= " cast(a.physical_per as nchar) as 'PHYSICAL %',"
        lsSql &= "a.share_captial as 'SHARE CAPITAL',"
        lsSql &= "a.demat_per as 'DEMAT %'"

        lsSql &= " from sta_trn_tinterdepository as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "

        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " and a.depo_date>='" & lsFrom & "' and a.depo_date<='" & lsTo & "'"
        lsSql &= " order by a.depo_gid"

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

    Public Sub New(FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        dgvList.DataSource = ""
    End Sub

End Class