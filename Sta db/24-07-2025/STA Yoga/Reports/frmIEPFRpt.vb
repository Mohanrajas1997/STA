Public Class frmIEPFRpt
    Dim mnFolioId As Long = 0

    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        With cboStatus
            .Items.Clear()
            .Items.Add("Movement")
            .Items.Add("Claim")
        End With
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
            MessageBox.Show("Please select the Tran Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboStatus.Focus()
            Exit Sub
        End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If mnFolioId > 0 Then lsCond &= " and a.dpid_gid = " & mnFolioId & " "

        Select Case cboStatus.Text.ToUpper
            Case "MOVEMENT"
                lsTranType = "M"
                lsCond &= " and b.iepf_tran_type = 'M' "
            Case "CLAIM"
                lsTranType = "C"
                lsCond &= " and b.iepf_tran_type = 'C' "
        End Select

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        If lsTranType = "M" Then
            lsSql = ""
            lsSql &= " select "
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " a.dpid as 'Dp Id',"
            lsSql &= " a.folio_no as 'Folio No',"
            lsSql &= " a.dpid_name as 'Name',"
            lsSql &= " date_format(b.tran_date,'%d-%m-%Y') as 'Transfer Date',"
            lsSql &= " a.shares_moved as 'No of Shares',"
            lsSql &= " a.addr1 as 'Addr1',"
            lsSql &= " a.addr2 as 'Addr2',"
            lsSql &= " a.addr3 as 'Addr3',"
            lsSql &= " a.city as 'City',"
            lsSql &= " a.pincode as 'Pincode',"
            lsSql &= " a.dpid_gid as 'DPID Gid' "
            lsSql &= " from sta_trn_tdpid as a "
            lsSql &= " inner join sta_trn_tiepftran as b on a.dpid_gid=b.dpid_gid and a.comp_gid=b.comp_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.dpid_gid asc"
        ElseIf lsTranType = "C" Then
            lsSql = ""
            lsSql &= " select "
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " a.dpid as 'Dp Id',"
            lsSql &= " a.folio_no as 'Folio No',"
            lsSql &= " a.dpid_name as 'Name',"
            lsSql &= " date_format(b.tran_date,'%d-%m-%Y') as 'Transfer Date',"
            lsSql &= " a.shares_moved as 'No of Shares Moved',"
            lsSql &= " a.shares_claimed as 'No of Shares Claimed',"
            lsSql &= " b.claimant_dpid as 'Claimant DP ID / Client ID',"
            lsSql &= " b.claimant_name as 'Claimant Name',"
            lsSql &= " b.srn_no as 'SRN NO',"
            lsSql &= " date_format(b.claim_date,'%d-%m-%Y') as 'Claim Date',"
            lsSql &= " a.addr1 as 'Addr1',"
            lsSql &= " a.addr2 as 'Addr2',"
            lsSql &= " a.addr3 as 'Addr3',"
            lsSql &= " a.city as 'City',"
            lsSql &= " a.pincode as 'Pincode',"
            lsSql &= " a.dpid_gid as 'DPID Gid' "
            lsSql &= " from sta_trn_tdpid as a "
            lsSql &= " inner join sta_trn_tiepftran as b on a.dpid_gid=b.dpid_gid and a.comp_gid=b.comp_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.dpid_gid asc"
        End If
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
            PrintDGridXML(dgvList, gsReportPath & "\IEPFReport.xls", "Report")
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
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvList.KeyDown
        Dim lnFolioId As Long = 0
        Dim frm As Object

        With dgvList
            If .CurrentCell.RowIndex >= 0 Then
                lnFolioId = .CurrentRow.Cells("DPID Gid").Value

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