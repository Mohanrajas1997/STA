Imports System.Data
Public Class frmDividendReport
    Dim mnFolioId As Long = 0
    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        lsSql &= " order by finyear_gid asc "

        Call gpBindCombo(lsSql, "finyear_code", "finyear_gid", Cbofinyr, gOdbcConn)

        lsSql = ""
        lsSql &= " select interim_code,interim_name from sta_mst_tinterim "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by interim_gid asc "

        Call gpBindCombo(lsSql, "interim_name", "interim_code", cbointerimcode, gOdbcConn)

        'With cbointerimcode
        '    .Items.Clear()
        '    .Items.Add("I")
        '    .Items.Add("II")
        '    .Items.Add("III")
        '    .Items.Add("F")

        'End With

        With Cbofolioidtype
            .Items.Clear()
            .Items.Add("P")
            .Items.Add("N")
            .Items.Add("C")
        End With

        'Paymode

        lsSql = ""
        lsSql &= " select paymode_code,paymode_desc from sta_mst_tpaymode "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by paymode_gid asc "

        Call gpBindCombo(lsSql, "paymode_desc", "paymode_code", cbopayment, gOdbcConn)

        'With cbopayment
        '    .Items.Clear()
        '    .Items.Add("W")
        '    .Items.Add("E")
        '    .Items.Add("D")
        'End With

        dtpFrom.Value = Now
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False

        cboFileName.Focus()

    End Sub
    Private Sub cboFileName_GotFocus(sender As Object, e As EventArgs) Handles cboFileName.GotFocus
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

        lsSql &= " and file_type in (" & gnFileDividendfile & ") "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by file_gid desc"

        gpBindCombo(lsSql, "file_name", "file_gid", cboFileName, gOdbcConn)
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

        If (cboFileName.Text = "" Or cboFileName.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the File Name !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboFileName.Focus()
            Exit Sub
        End If


        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cboFileName.Text <> "" And cboFileName.SelectedIndex <> -1 Then
            lsCond &= " and a.file_gid= " & Val(cboFileName.SelectedValue.ToString) & ""
        End If

        If Cbofinyr.Text <> "" And Cbofinyr.SelectedIndex <> -1 Then
            lsCond &= " and a.finyear_gid= " & Val(Cbofinyr.SelectedValue.ToString) & " "
        End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cbointerimcode.Text <> "" And cbointerimcode.SelectedIndex <> -1 Then
            lsCond &= " and a.interim_code = '" & cbointerimcode.SelectedValue.ToString & "' "
        End If

        If cbopayment.Text <> "" And cbopayment.SelectedIndex <> -1 Then
            lsCond &= " and a.payment_mode = '" & cbopayment.SelectedValue.ToString & "' "
        End If

        If txtFolioId.Text <> "" Then lsCond &= " and a.folioclient_id like '" & QuoteFilter(txtFolioId.Text) & "%' "

        If txtHolder1.Text <> "" Then lsCond &= " and a.holder1_name like '" & QuoteFilter(txtHolder1.Text) & "%' "


        If mnFolioId > 0 Then lsCond &= " and a.dividend_gid = " & mnFolioId & " "

        'Select Case cbointerimcode.Text.ToUpper
        '    Case "I"
        '        lsCond &= " and a.interim_code ='I' "
        '    Case "II"
        '        lsCond &= " and a.interim_code ='II' "
        '    Case "III"
        '        lsCond &= " and a.interim_code = 'III' "
        '    Case "F"
        '        lsCond &= " and a.interim_code = 'F' "
        'End Select

        Select Case Cbofolioidtype.Text.ToUpper
            Case "P"
                lsCond &= " and a.folioclient_id_type = 'P' "
            Case "N"
                lsCond &= " and a.folioclient_id_type = 'N' "
            Case "C"
                lsCond &= " and a.folioclient_id_type = 'C' "
        End Select

        'Select Case cbopayment.Text.ToUpper
        '    Case "W"
        '        lsCond &= " and a.payment_mode = 'W' "
        '    Case "E"
        '        lsCond &= " and a.payment_mode = 'E' "
        '    Case "D"
        '        lsCond &= " and a.payment_mode = 'D' "
        'End Select

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " c.finyear_code as 'Financial Year',"
        lsSql &= " a.interim_code as 'Interim Code',"
        lsSql &= " a.dp_id as 'Dp Id',"
        lsSql &= " a.folioclient_id as 'Folio Client Id',"
        lsSql &= " a.folioclient_id_type as 'Folio Client Id Type',"
        lsSql &= " a.holder1_name as 'Holder Name',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " a.dividend_per_share as 'Dividend Per Share',"
        lsSql &= " a.dividend_amount as 'Dividend Amount',"
        lsSql &= " case  when a.curr_dividend_status=1 then 'Payment To Be Generated'  when a.curr_dividend_status=2 then 'Payment Generated' "
        lsSql &= " when a.curr_dividend_status=16 then 'Bounce' end as 'Curr_Status' ,"
        lsSql &= " a.currency_code as 'Currency Code',"
        lsSql &= " a.currency_value as 'Currency Value',"
        lsSql &= " a.currency_amount as 'Currenct Amount',"
        lsSql &= " a.payment_mode as 'Payment Mode',"
        lsSql &= " a.payment_status as 'Payment Status',"
        lsSql &= " a.bank_name as 'Bank Name',"
        lsSql &= " a.bank_branch as 'Bank Branch',"
        lsSql &= " a.bank_acc_no as 'Bank Acc No',"
        lsSql &= " a.bank_acc_type as 'Bank Acc Type',"
        lsSql &= " a.bank_micr_code as 'Bank Micr Code',"
        lsSql &= " a.bank_ifsc_code as 'Bank Ifsc Code',"
        lsSql &= " a.holder1_addr1 as 'Addr1',"
        lsSql &= " a.holder1_addr2 as 'Addr2',"
        lsSql &= " a.holder1_addr3 as 'Addr3',"
        lsSql &= " a.holder1_addr4 as 'Addr4',"
        lsSql &= " a.holder1_city as 'City',"
        lsSql &= " a.holder1_state as 'State',"
        lsSql &= " a.holder1_country as 'Country', "
        lsSql &= " a.holder1_pincode as 'Pincode',  "
        lsSql &= " a.email_id as 'Email',"
        lsSql &= " a.dividend_gid as 'Dividend Report Id' "
        lsSql &= " from sta_trn_tdividend as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfile as d on a.file_gid = d.file_gid and d.delete_flag='N' "
        lsSql &= " left join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and a.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.dividend_gid"

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

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvList.KeyDown
        Dim lnFolioId As Long = 0
        Dim frm As Object

        With dgvList
            If .CurrentCell.RowIndex >= 0 Then
                lnFolioId = .CurrentRow.Cells("Dividend Report Id").Value

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