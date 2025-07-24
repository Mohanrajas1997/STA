Public Class frmAnnualReport
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

        With cboStatus
            .Items.Clear()
            .Items.Add("MEMBER")
            .Items.Add("PROXY")
           
        End With

        With CboRtTag
            .Items.Clear()
            .Items.Add("YES")
            .Items.Add("NO")
        End With

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

        lsSql &= " and file_type in (" & gnFileAnnualRpt & ") "
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

        If txtFolioNo.Text <> "" Then lsCond &= " and a.annualrpt_folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%' "

        If txtHolder1.Text <> "" Then lsCond &= " and a.holder1_name like '" & QuoteFilter(txtHolder1.Text) & "%' "
        If txtHolder2.Text <> "" Then lsCond &= " and a.holder2_name like '" & QuoteFilter(txtHolder2.Text) & "%' "
        If txtHolder3.Text <> "" Then lsCond &= " and a.holder3_name like '" & QuoteFilter(txtHolder3.Text) & "%' "

        If mnFolioId > 0 Then lsCond &= " and a.annualrpt_gid = " & mnFolioId & " "

        Select Case cboStatus.Text.ToUpper
            Case "MEMBER"
                lsCond &= " and a.member_proxy ='MEMBER' "
            Case "PROXY"
                lsCond &= " and a.member_proxy ='PROXY' "
        End Select

        Select Case CboRtTag.Text.ToUpper
            Case "YES"
                lsCond &= " and a.return_tag = 'Y' "
            Case "NO"
                lsCond &= " and a.return_tag = 'N' "
        End Select

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " a.annaulrpt_slip_no as 'SL No',"
        lsSql &= " a.annualrpt_folio_no as 'Folio No',"
        lsSql &= " a.share_count as 'Share Count',"
        lsSql &= " a.holder1_name as 'Holder1',"
        lsSql &= " a.holder2_name as 'Holder2',"
        lsSql &= " a.holder3_name as 'Holder3',"
        lsSql &= " a.addr1 as 'Addr1',"
        lsSql &= " a.addr2 as 'Addr2',"
        lsSql &= " a.addr3 as 'Addr3',"
        lsSql &= " a.addr4 as 'Addr4',"
        lsSql &= " a.city as 'City',"
        lsSql &= " a.state as 'State',"
        lsSql &= " a.country as 'Country',"
        lsSql &= " a.pincode as 'Pincode',"
        lsSql &= " a.email_id1 as 'Email Id1',"
        lsSql &= " a.email_id2 as 'Email Id2',"
        lsSql &= " a.dispatch_mode as 'Dispatch Mode',"
        lsSql &= " a.member_proxy as 'Member/Proxy',"
        lsSql &= " a.proxy1_name as 'Proxy1 Name',"
        lsSql &= " a.proxy2_name as 'Proxy2 Name',"
        lsSql &= " a.proxy3_name as 'proxy3 Name',"
        lsSql &= " a.token_no as 'Token No',"
        lsSql &= " a.return_tag as 'Return Tag',"
        lsSql &= " a.return_reason as 'Return Reason',"
        lsSql &= " c.finyear_code as 'Financial Year',"
        lsSql &= " a.date_of_agm as 'Date of Agm', "
        lsSql &= " a.attend_flag as 'Attend Flag',  "
        lsSql &= " a.attend_date as 'Attend Date',"
        lsSql &= " a.attend_member_mode as 'Attend Member Mode',"
        lsSql &= " a.attend_member_name as 'Attend Member Name',"
        lsSql &= " a.annualrpt_gid as 'Annaual Report Id' "
        lsSql &= " from sta_trn_tannualrpt as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfile as d on a.file_gid = d.file_gid and d.delete_flag='N' "
        lsSql &= " left join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and a.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.annaulrpt_slip_no asc"

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
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvList.KeyDown
        Dim lnFolioId As Long = 0
        Dim frm As Object

        With dgvList
            If .CurrentCell.RowIndex >= 0 Then
                lnFolioId = .CurrentRow.Cells("Annaual Report Id").Value

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