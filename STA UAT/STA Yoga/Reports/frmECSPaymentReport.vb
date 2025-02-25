Public Class frmECSPaymentReport
    Dim mnFolioId As Long = 0

    Private Sub frmECSPaymentReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        cboCompany.Focus()
    End Sub

    Private Sub frmECSPaymentReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
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


        If mnFolioId > 0 Then lsCond &= " and a.payment_gid = " & mnFolioId & " "

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

        lsSql &= " a.payment_no as 'Payment No',"
        lsSql &= " a.folioclient_dp_id as 'Dp Id',"
        lsSql &= " a.folioclient_id as 'Folio Client Id',"
        lsSql &= " a.folioclient_id_type as 'Folio Client Id Type',"
        lsSql &= " a.holder1_name as 'Holder1 Name',"
        lsSql &= " a.bene_name as 'Beneficiary Name',"
        lsSql &= " a.dividend_acc_no as 'Dividend Acc Amount',"
        lsSql &= " a.dividend_amount as 'Dividend Amount',"
        lsSql &= " a.currency_code as 'Currency Code',"
        lsSql &= " a.currency_value as 'Currency Value',"
        lsSql &= " a.currency_amount as 'Currenct Amount',"
        lsSql &= " a.payment_mode as 'Payment Mode',"
        'lsSql &= " a.payment_status as 'Payment Status',"
        lsSql &= " case  when a.payment_status=16 then 'Bounce' end as 'Status', "
        lsSql &= " a.bank_name as 'Bank Name',"
        lsSql &= " a.bank_branch as 'Bank Branch',"
        lsSql &= " a.bank_acc_no as 'Bank Acc No',"
        lsSql &= " a.bank_acc_type as 'Bank Acc Type',"
        lsSql &= " a.bank_micr_code as 'Bank Micr Code',"
        lsSql &= " a.bank_ifsc_code as 'Bank Ifsc Code',"
        lsSql &= " a.payment_ref_no as 'Payment Ref.No',"

        lsSql &= " a.holder1_addr1 as 'Holder Address1',"
        lsSql &= " a.holder1_addr2 as 'Holder Address2',"
        lsSql &= " a.holder1_addr3 as 'Holder Address3',"
        lsSql &= " a.holder1_city as 'Holder City',"
        lsSql &= " a.holder1_state as 'Holder State',"
        lsSql &= " a.holder1_country as 'Holder Country',"
        lsSql &= " a.holder1_pincode as 'Pin Code',"
        lsSql &= " a.despatch_status as 'Despatch Status',"
        lsSql &= " a.despatch_date as 'Despatch Date',"
        lsSql &= " a.despatch_by as 'Despatch By',"

        lsSql &= " a.despatch_entry_date as 'Despatch Entry Date',"
        lsSql &= " a.despatch_entry_by as 'Despatch Entry By',"
        lsSql &= " a.return_date as 'Return Date',"
        lsSql &= " a.return_received_by as 'Return Received By',"
        lsSql &= " a.return_reason as 'Return Reason',"
        lsSql &= " a.return_entry_date as 'Return Entry Date',"
        lsSql &= " a.return_entry_by as 'Return Entry By',"
        lsSql &= " a.repayment_gid as 'Repayment GID',"
        lsSql &= " a.paymentbatch_gid as 'Payment Batch GID',"
        lsSql &= " a.paymentupload_gid as 'Payment Upload GID',"

        lsSql &= " a.payment_gid as 'Payment Report Id' "
        lsSql &= " from sta_trn_tpayment as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and a.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.payment_gid asc "

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
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

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
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


    Private Sub dgvList_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvList.KeyDown
        Dim lnFolioId As Long = 0
        Dim frm As Object

        With dgvList
            If .CurrentCell.RowIndex >= 0 Then
                lnFolioId = .CurrentRow.Cells("Payment Report Id").Value

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