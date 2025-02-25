Public Class frmDividendMasterRpt
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
        lsSql &= " date_format(a.benpost_date,'%d-%m-%Y') as 'Benpost Date',"
        lsSql &= " a.folio_no as 'Folio No',"
        lsSql &= " a.folio_name as 'Folio Name',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " a.div_rate as 'Div Rate',"
        lsSql &= " a.payment_type as 'Payment Type',"
        lsSql &= " date_format(a.payment_date,'%d-%m-%Y') as 'Payment Date',"
        lsSql &= " a.amount as 'Amount',"
        lsSql &= " a.warrant_no as 'Warrant No',"
        lsSql &= " a.cheque_no as 'Cheque No',"
        lsSql &= " d.status_desc as 'Dividend Status',"
        lsSql &= " date_format(a.paid_date,'%d-%m-%Y') as 'Paid Date',"
        'lsSql &= " a.joint1 as 'Joint 1',"
        'lsSql &= " a.joint2 as 'Joint 2',"
        lsSql &= " a.holder1_pan_no as 'Holder1 Pan No',"
        lsSql &= " a.holder1_emailid as 'Holder1 EmailId',"
        lsSql &= " a.addr1 as 'Addr1',"
        lsSql &= " a.addr2 as 'Addr2',"
        lsSql &= " a.addr3 as 'Addr3',"
        lsSql &= " a.city as 'City',"
        'lsSql &= " a.state as 'State',"
        'lsSql &= " a.country as 'Country',"
        lsSql &= " a.pincode as 'Pincode',"
        lsSql &= " a.contact_no as 'Contact No',"
        lsSql &= " a.bank_name as 'Bank Name',"
        lsSql &= " a.bank_addr1 as 'Bank Addr1',"
        lsSql &= " a.bank_addr2 as 'Bank Addr2',"
        lsSql &= " a.bank_addr3 as 'Bank Addr3',"
        lsSql &= " a.bank_city as 'Bank City',"
        lsSql &= " a.bank_state as 'Bank States',"
        lsSql &= " a.bank_pincode as 'Bank Pincode',"
        lsSql &= " a.bank_acno as 'Bank Acno',"
        'lsSql &= " a.bank_actype as 'Bank A/C Type',"
        lsSql &= " a.bank_micrcode as 'Bank Micr Code',"
        lsSql &= " a.bank_ifsccode as 'Bank Ifsc Code',"
        lsSql &= " a.category as 'Category',"
        lsSql &= " a.sub_type as 'Sub Type',"
        lsSql &= " a.remarks as 'Remarks',"
        lsSql &= " a.dividendstatus_gid as 'Dividend Status Gid',"
        lsSql &= " a.dividend_gid as 'Dividend Gid' "
        lsSql &= " from sta_trn_tdividendmaster as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid=b.comp_gid  and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and c.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tstatus as d on a.dividend_status=d.status_value and d.status_type='dividend' and d.delete_flag='N'"
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.dividend_gid asc"

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
                lnFolioId = .CurrentRow.Cells("Dividend Gid").Value

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