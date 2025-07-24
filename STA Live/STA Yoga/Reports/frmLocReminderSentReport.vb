Imports MySql.Data.MySqlClient

Public Class frmLocReminderSentReport

    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};" + ServerDetails)
    Dim ds, dt, dt_t, dt_tt, dt_re, dt_st, dmtt, dmtt1 As DataSet
    Dim da As New MySqlDataAdapter
    Dim cmd As New MySqlCommand
    Dim sql As String

    Public Function QuoteFilter(ByVal txt As String) As String
        QuoteFilter = Trim(Replace(Replace(Replace(txt, "'", " "), """", """"""), "\", "\\"))
    End Function

    Private Sub frmLocReminderSendReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' doc type
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "

        Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGriddata()
    End Sub

    Private Sub LoadGriddata()
        'gOdbcConn.Open()
        Dim shsql As String
        Dim lsCond As String = ""
        lsCond = ""

        If dtpFrom.Checked = True Then lsCond &= " and b.received_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and b.received_date <= '" & Format(dtpTo.Value, "yyyy-MM-dd") & "' "
        If txtInwardNo.Text <> "" Then lsCond &= " and b.inward_comp_no = '" & QuoteFilter(txtInwardNo.Text) & "' "
        If txtFolioNo.Text <> "" Then lsCond &= " and b.folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%'"

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If txtCalcDays.Text <> "0" And txtCalcDays.Text <> "" Then
            lsCond &= " and datediff(g.reminder_sent_date,d.meeting_date) >= " & Val(txtCalcDays.Text) & " "
        End If

        If txtRemiday.Text <> "0" And txtRemiday.Text <> "" Then
            lsCond &= " and g.days = " & Val(txtRemiday.Text) & " "
        End If

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            lsCond &= " and b.tran_code = '" & cboDocType.SelectedValue.ToString & "' "
        Else
            lsCond &= " and b.tran_code in ('TR','TM','CO','LS','NC') "
        End If

        shsql = ""
        shsql &= " select distinct "
        shsql &= " e.comp_name as 'Company',"
        shsql &= " b.inward_comp_no as 'Inward No',"
        shsql &= " b.received_date as 'Inward Date',"
        'shsql &= " e.comp_gid,"
        shsql &= " b.folio_no as 'Folio No',"
        shsql &= " b.shareholder_name as 'Share Holder',"
        shsql &= " i.folio_shares as 'No Of Shares',"
        'shsql &= " b.tran_code,"
        'shsql &= " b.inward_status,"
        'shsql &= " b.chklst_disc,"
        'shsql &= " ifnull(b.dematpend_reject_code,0) as dematpend_reject_code,"
        shsql &= " c.trantype_desc as 'Document',"
        shsql &= " b.approved_date as 'LOC Issue Date',"
        shsql &= " g.reminder_sent_date as 'Reminder Sent Date',"
        shsql &= " g.awb_no as 'Awb No',"
        shsql &= " datediff(g.reminder_sent_date,d.meeting_date) as 'Days' ,"
        shsql &= " g.days as 'Rem Days' "
        'shsql &= " b.folio_gid,"
        'shsql &= " b.inward_gid, "
        'shsql &= " b.tran_folio_gid , ifnull(h.depository_code,'""') as depository_code,"
        'shsql &= " g.locrem_gid"
        shsql &= " from sta_trn_tinward as b "
        shsql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
        shsql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
        shsql &= " inner join sta_trn_tupload as d on b.comp_gid = b.comp_gid and b.upload_gid = d.upload_gid and d.upload_status = '2' and d.delete_flag = 'N' "
        shsql &= " inner join sta_trn_tfolio as i on b.comp_gid = i.comp_gid and i.folio_gid = if(b.tran_folio_gid > 0,b.tran_folio_gid,b.folio_gid) and i.delete_flag = 'N' and folio_shares > 0 "
        shsql &= " inner join sta_trn_tlocreminder as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
        shsql &= " left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
        shsql &= " where true "
        shsql &= lsCond
        shsql &= " and b.delete_flag = 'N' and b.reminder_flag = 'Y' and g.status = 'Sent' order by b.approved_date desc"
        'shsql &= " group by b.inward_gid desc"

        With cmd
            .Connection = gOdbcConn
            .CommandText = shsql

        End With
        With dgvList
            .Columns.Clear()
            'filling  data in the table
            ds = New DataSet
            da = New MySqlDataAdapter(shsql, gOdbcConn)
            da.Fill(ds, "tbl")

            dgvList.DataSource = ds.Tables(0)
            'dgvList.Columns("chklst_disc").Visible = False
            'dgvList.Columns("tran_code").Visible = False
            'dgvList.Columns("inward_gid").Visible = False
            'dgvList.Columns("comp_gid").Visible = False
            'dgvList.Columns("inward_status").Visible = False
            'dgvList.Columns("folio_gid").Visible = False
            'dgvList.Columns("tran_folio_gid").Visible = False
            'dgvList.Columns("dematpend_reject_code").Visible = False
            'dgvList.Columns("depository_code").Visible = False
            'dgvList.Columns("locrem_gid").Visible = False

        End With
        txtTotRec.Text = "Total Records : " & dgvList.RowCount
        'gOdbcConn.Close()
    End Sub

    Private Sub frmLocReminderSentReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

    Private Sub txtCalcDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCalcDays.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtRemiday_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRemiday.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class