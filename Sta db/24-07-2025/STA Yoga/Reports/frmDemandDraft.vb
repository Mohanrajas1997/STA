Imports System.Data
Public Class frmDemandDraft
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

        'With cbopayment
        '    .Items.Clear()
        '    .Items.Add("W")
        '    .Items.Add("E")
        '    .Items.Add("D")
        'End With

        'dtpFrom.Value = Now
        'dtpTo.Value = Now

        'dtpFrom.Checked = False
        'dtpTo.Checked = False

        cboCompany.Focus()

    End Sub
    'Private Sub cboFileName_GotFocus(sender As Object, e As EventArgs)
    '    Dim lsSql As String = ""

    '    lsSql = ""
    '    lsSql &= " select file_gid,concat(file_name,' ',ifnull(sheet_name,'')) as file_name from sta_trn_tfile "
    '    lsSql &= " where 1 = 1"

    '    If dtpFrom.Checked = True Then
    '        lsSql &= " and insert_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "'"
    '    End If

    '    If dtpTo.Checked = True Then
    '        lsSql &= " AND insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "'"
    '    End If

    '    lsSql &= " and file_type in (" & gnFileDividendfile & ") "
    '    lsSql &= " and delete_flag = 'N' "
    '    lsSql &= " order by file_gid desc"

    '    gpBindCombo(lsSql, "file_name", "file_gid", cboFileName, gOdbcConn)
    'End Sub
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

        'If (cboFileName.Text = "" Or cboFileName.SelectedIndex = -1) And mnFolioId = 0 Then
        '    MessageBox.Show("Please select the File Name !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    cboFileName.Focus()
        '    Exit Sub
        'End If


        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        'If cboFileName.Text <> "" And cboFileName.SelectedIndex <> -1 Then
        '    lsCond &= " and a.file_gid= " & Val(cboFileName.SelectedValue.ToString) & ""
        'End If

        If (cbointerimcode.Text = "" Or cbointerimcode.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the Interim Code !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbointerimcode.Focus()
            Exit Sub
        End If
        If (Cbofinyr.Text = "" Or Cbofinyr.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please Select the Financial Year !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cbofinyr.Focus()
            Exit Sub
        End If

        If Cbofinyr.Text <> "" And Cbofinyr.SelectedIndex <> -1 Then
            lsCond &= " and a.finyear_gid= " & Val(Cbofinyr.SelectedValue.ToString) & " "
        End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cbointerimcode.Text <> "" And cbointerimcode.SelectedIndex <> -1 Then
            lsCond &= " and a.Interim_code = '" & cbointerimcode.SelectedValue.ToString & "' "
        End If


        If txtFolioId.Text <> "" Then lsCond &= " and a.folioclient_id like '" & QuoteFilter(txtFolioId.Text) & "%' "

        If txtHolder1.Text <> "" Then lsCond &= " and a.holder1_name like '" & QuoteFilter(txtHolder1.Text) & "%' "


        If mnFolioId > 0 Then lsCond &= " and a.Payment_gid = " & mnFolioId & " "

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
        lsSql &= " a.folioclient_id as 'FOLIO/DPCL ID',"
        lsSql &= " '' as 'DEMAND DRAFT NO',"
        lsSql &= " a.bene_name as 'BENEFICIARY NAME',"
        lsSql &= " a.dividend_amount as 'DRAFT AMOUNT',"
        lsSql &= " '' as 'BENEFICIARY BANK NAME',"
        lsSql &= " '' as 'BENEFICIARY ACC NO',"
        lsSql &= " '' as 'LOCATION NAME',"
        lsSql &= " a.holder1_addr1 as 'BENE. ADDRESS LINE 1',"
        lsSql &= " a.holder1_addr2 as 'BENE. ADDRESS LINE 2',"
        lsSql &= " a.holder1_addr3 as 'BENE. ADDRESS LINE 3',"
        lsSql &= " concat(a.holder1_city,' ',a.holder1_state) as 'BENE. ADDRESS LINE 4',"
        lsSql &= " a.holder1_pincode as 'BENE. ADDRESS LINE 5',"
        lsSql &= " concat(b.comp_code,'-',c.finyear_code,'',a.interim_code) as 'PAYMENT DETAILS 1',"
        lsSql &= " '' as 'PAYMENT DETAILS 2',"
        lsSql &= " '' as 'PAYMENT DETAILS 3',"
        lsSql &= " '' as 'PAYMENT DETAILS 4',"
        lsSql &= " '' as 'PAYMENT DETAILS 5',"
        lsSql &= " '' as 'PAYMENT DETAILS 6',"
        lsSql &= " '' as 'PAYMENT DETAILS 7',"
        lsSql &= " a.Payment_gid as 'GNSA Ref Id' "
        lsSql &= " from sta_trn_tpayment as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and a.payment_mode='D' and b.delete_flag = 'N'  "
        'lsSql &= " inner join sta_trn_tfile as d on a.file_gid = d.file_gid and d.delete_flag='N' "
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and a.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.Payment_gid asc "

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
                lnFolioId = .CurrentRow.Cells("GNSA Ref Id").Value

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