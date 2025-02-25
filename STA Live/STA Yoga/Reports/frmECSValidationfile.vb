Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.IO.FileStream
Public Class frmECSValidationfile
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

        ' Interim Code

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
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
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
            lsCond &= " and a.interim_code= '" & cbointerimcode.SelectedValue.ToString & "' "
        End If


        If txtFolioId.Text <> "" Then lsCond &= " and a.folioclient_id like '" & QuoteFilter(txtFolioId.Text) & "%' "

        If txtHolder1.Text <> "" Then lsCond &= " and a.bene_name like '" & QuoteFilter(txtHolder1.Text) & "%' "


        If mnFolioId > 0 Then lsCond &= " and a.ecsvalidation_gid = " & mnFolioId & " "



        

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
        lsSql &= " a.folioclient_dp_id as 'Dp Id',"
        lsSql &= " a.folioclient_id as 'Folio Client Id',"
        lsSql &= " a.folioclient_id_type as 'Folio Client Id Type',"
        lsSql &= " a.bene_name as 'Holder Name',"
        lsSql &= " a.dividend_amount as 'Dividend Amount',"
        lsSql &= " a.currency_code as 'Currency Code',"
        lsSql &= " a.currency_value as 'Currency Value',"
        lsSql &= " a.currency_amount as 'Currenct Amount',"
        lsSql &= " a.bank_name as 'Bank Name',"
        lsSql &= " a.bank_branch as 'Bank Branch',"
        lsSql &= " a.bank_acc_no as 'Bank Acc No',"
        lsSql &= " e.bankacctype_name as 'Bank Acc Type',"
        lsSql &= " a.bank_micr_code as 'Bank Micr Code',"
        lsSql &= " a.bank_ifsc_code as 'Bank IFSC Code',"
        lsSql &= " case  when a.ecsvalidation_status=0 then 'Ecs To be Validated'  when a.ecsvalidation_status=128 then 'Ecs Valid' "
        lsSql &= " when a.ecsvalidation_status=256 then 'Ecs Invalid' end as 'Status' ,"
        lsSql &= " a.ecsvalidation_gid as 'ECS Validation Id' "
        lsSql &= " from sta_trn_tecsvalidation as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfile as d on a.file_gid = d.file_gid and d.delete_flag='N' "
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and c.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tbankacctype as e on a.bank_acc_type=e.bankacctype_code and e.delete_flag='N'  "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.ecsvalidation_gid asc"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
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
                lnFolioId = .CurrentRow.Cells("ECS Validation Id").Value

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

    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        Call TxtFileGenerate()
    End Sub
    Private Sub TxtFileGenerate()
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnRecCount As Long
        Dim lnValidCount As Long
        Dim lnInvalidCount As Long
        Dim lsDematPendRejectCode As String = ""
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim lncompid As Long
        Dim lnfinyeargid As Long
        Dim lnFilegid As Long
        Dim lnInterimcode As String

        Dim ds As New DataSet
        Dim i As Integer
        Dim lnLineNo As Long = 0



        If cboCompany.SelectedIndex <> -1 Then
            lncompid = Val(cboCompany.SelectedValue.ToString)
        Else
            lncompid = 0
        End If

        If Cbofinyr.SelectedIndex <> -1 Then
            lnfinyeargid = Val(Cbofinyr.SelectedValue.ToString)
        Else
            lnfinyeargid = 0
        End If

        If cbointerimcode.SelectedIndex <> -1 Then
            lnInterimcode = cbointerimcode.SelectedValue.ToString
        Else
            lnInterimcode = ""
        End If

        If cboFileName.SelectedIndex <> -1 Then
            lnFilegid = cboFileName.SelectedValue.ToString
        Else
            lnFilegid = 0
        End If

        Using cmd As New MySqlCommand("pr_sta_get_ecsheader", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
            cmd.Parameters.AddWithValue("?in_finyear_gid", lnfinyeargid)
            cmd.Parameters.AddWithValue("?in_interim_code", lnInterimcode)
            cmd.Parameters.AddWithValue("?in_file_gid", lnFilegid)
            cmd.CommandTimeout = 0
            Dim adapter As New MySqlDataAdapter(cmd)
            adapter.Fill(ds, "Validation")
        End Using

        If ds.Tables("Validation").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\ECS"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("Validation").Rows(0).Item("finyear_code").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("Validation").Rows(0).Item("interim_code").ToString()
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("Validation").Rows(0).Item("comp_name").ToString


            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' header
            lsTxt = ""
            lsTxt &= AlignTxt("12", 2, 1)
            lsTxt &= AlignTxt("", 7, 1)
            lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("comp_name").ToString, 40, 1)
            lsTxt &= AlignTxt("", 14, 1)
            lsTxt &= AlignTxt("", 9, 1)
            lsTxt &= AlignTxt("", 9, 1)
            lsTxt &= AlignTxt("", 15, 1)
            lsTxt &= AlignTxt("", 3, 1)
            lsTxt &= AlignTxt(Format(ds.Tables("Validation").Rows(0).Item("max_dividend_amt"), StrDup(13, "0")), 13, 1)
            lsTxt &= AlignTxt(Format(ds.Tables("Validation").Rows(0).Item("dividend_amt"), StrDup(13, "0")), 13, 1)
            lsTxt &= AlignTxt(Format(ds.Tables("Validation").Rows(0).Item("settlement_date"), "ddMMyyyy"), 8, 1)
            lsTxt &= AlignTxt("", 10, 1)
            lsTxt &= AlignTxt("", 10, 1)
            lsTxt &= AlignTxt("", 3, 1)
            lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("user_no").ToString, 18, 1)
            lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("userref_no").ToString, 18, 1)
            lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("sponcer_code").ToString, 11, 1)
            lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("bank_acc_no").ToString, 35, 1)
            lsTxt &= AlignTxt(Format(ds.Tables("Validation").Rows(0).Item("total_item"), StrDup(9, "0")), 9, 1)
            lsTxt &= AlignTxt("", 2, 1)
            lsTxt &= AlignTxt("", 57, 1)
            Call Print(1, lsTxt)


            'Details

            lsSql = ""
            lsSql &= " select "
            lsSql &= " '23',"
            lsSql &= " case when bank_acc_type='S' then '10'  when bank_acc_type='C' then '11' else '13' end as acctype, "
            lsSql &= " bene_name, "
            lsSql &= " (dividend_amount*100) as dividendamount,"
            lsSql &= " bank_micr_code,"
            lsSql &= " bank_acc_no,"
            lsSql &= " concat(folioclient_dp_id,folioclient_id) as TranRef,"
            lsSql &= " 'ECS' "
            lsSql &= " from sta_trn_tecsvalidation "
            lsSql &= " where file_gid= " & lnFilegid & " "
            lsSql &= " and finyear_gid = " & lnfinyeargid & ""
            lsSql &= " and comp_gid = " & lncompid & ""
            lsSql &= " and interim_code = '" & lnInterimcode & "'"

            lsSql &= " and delete_flag = 'N' "
            lsSql &= " order by ecsvalidation_gid asc "

            Call gpDataSet(lsSql, "Details", gOdbcConn, ds)

            For i = 0 To ds.Tables("Details").Rows.Count - 1
                lnLineNo += 1

                lsTxt = vbNewLine
                lsTxt &= AlignTxt(ds.Tables("Details").Rows(i).Item("23").ToString, 2, 1)
                lsTxt &= AlignTxt("", 9, 1)
                lsTxt &= AlignTxt(ds.Tables("Details").Rows(i).Item("acctype").ToString, 2, 1)
                lsTxt &= AlignTxt("", 3, 1)
                lsTxt &= AlignTxt("", 15, 1)
                lsTxt &= AlignTxt(ds.Tables("Details").Rows(i).Item("bene_name").ToString, 40, 1)
                lsTxt &= AlignTxt("", 9, 1)
                lsTxt &= AlignTxt("", 7, 1)
                lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("comp_name").ToString, 20, 1)
                lsTxt &= AlignTxt("", 13, 1)
                lsTxt &= AlignTxt(Format(ds.Tables("Details").Rows(i).Item("dividendamount"), StrDup(13, "0")), 13, 1)
                lsTxt &= AlignTxt("", 10, 1)
                lsTxt &= AlignTxt("", 10, 1)
                lsTxt &= AlignTxt("", 1, 1)
                lsTxt &= AlignTxt("", 2, 1)
                lsTxt &= AlignTxt(ds.Tables("Details").Rows(i).Item("bank_micr_code").ToString, 11, 1)
                lsTxt &= AlignTxt(ds.Tables("Details").Rows(i).Item("bank_acc_no").ToString, 35, 1)
                lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("sponcer_code").ToString, 11, 1)
                lsTxt &= AlignTxt(ds.Tables("Validation").Rows(0).Item("user_no").ToString, 18, 1)
                lsTxt &= AlignTxt(ds.Tables("Details").Rows(i).Item("TranRef").ToString, 30, 1)
                lsTxt &= AlignTxt(ds.Tables("Details").Rows(i).Item("ECS").ToString, 3, 1)
                lsTxt &= AlignTxt("", 15, 1)
                lsTxt &= AlignTxt("", 20, 1)
                lsTxt &= AlignTxt("", 7, 1)
                Call Print(1, lsTxt)
            Next i
            ds.Tables("Details").Rows.Clear()

            Call FileClose(1)

            Call gpOpenFile(lsFilePath)

        End If




    End Sub
    Private Function AlignTxt(ByVal txt As String, ByVal Length As Integer, ByVal Alignment As Integer) As String
        Select Case Alignment
            Case 1
                Return LSet(txt, Length)
            Case 4
                Return CSet(txt, Length)
            Case 7
                Return RSet(txt, Length)
            Case Else
                Return txt
        End Select
    End Function
    Private Function CSet(ByVal txt As String, ByVal PaperChrWidth As Integer) As String
        Dim s As String                 ' Temporary String Variable
        Dim l As Integer                ' Length of the String
        If Len(txt) > PaperChrWidth Then
            CSet = Mid(txt, 1, PaperChrWidth)
        Else
            l = (PaperChrWidth - Len(txt)) / 2
            s = RSet(txt, l + Len(txt))
            CSet = Space(PaperChrWidth - Len(s))
            CSet = s + CSet
        End If
    End Function
End Class