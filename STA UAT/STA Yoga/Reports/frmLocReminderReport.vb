Imports System.IO
Imports MySql.Data.MySqlClient

Public Class frmLocReminderReport
    Dim objWord As Application
    Dim objDoc As Microsoft.Office.Interop.Word.Document

    ' Dim tlb As Table
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=192.168.0.182;DataBase=sta;uid=production;pwd=gnsalive;port=3306")
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=" & DbIP & ";DataBase=" & Db & ";uid=" & DbUId & ";pwd=" & DbPwd & ";port=" & DbPort)
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};" + ServerDetails)
    Dim ds, dt, dt_t, dt_tt, dt_re, dt_st, dmtt, dmtt1 As DataSet
    Dim da As New MySqlDataAdapter
    Dim cmd As New MySqlCommand
    Dim sql As String

    Public Function QuoteFilter(ByVal txt As String) As String
        QuoteFilter = Trim(Replace(Replace(Replace(txt, "'", " "), """", """"""), "\", "\\"))
    End Function

    Private Sub frmLocReminderReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub frmLocReminderReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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
            lsCond &= " and datediff(now(),d.meeting_date) >= " & Val(txtCalcDays.Text) & " "
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
        shsql &= " e.comp_gid,"
        shsql &= " b.folio_no as 'Folio No',"
        shsql &= " b.shareholder_name as 'Share Holder',"
        shsql &= " i.folio_shares as 'No Of Shares',"
        shsql &= " b.tran_code,"
        shsql &= " b.inward_status,"
        shsql &= " b.chklst_disc,"
        shsql &= " ifnull(b.dematpend_reject_code,0) as dematpend_reject_code,"
        shsql &= " c.trantype_desc as 'Document',"
        shsql &= " b.approved_date as 'LOC Issue Date',"
        'shsql &= " '' as 'Demat',"
        'shsql &= " '' as 'Escrow Date',"
        shsql &= " datediff(now(),d.meeting_date) as 'Days' ,"
        shsql &= " g.days as 'Rem Days',"
        'shsql &= " g.remark as 'Remarks',"
        shsql &= " b.folio_gid,"
        'shsql &= " make_set(b.inward_status,'Received','Inprocess','Completed','Reject','Reprocess','Despatch','Inex') as 'Inward Status',"
        'shsql &= " make_set(b.queue_status,'Inward','Maker','Checker','Authorizer','Upload','Despatch','Inex') as 'Queue Status',"
        shsql &= " b.inward_gid, "
        shsql &= " b.tran_folio_gid , ifnull(h.depository_code,'""') as depository_code,"
        shsql &= " g.locrem_gid"
        shsql &= " from sta_trn_tinward as b "
        shsql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
        shsql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
        shsql &= " inner join sta_trn_tupload as d on b.comp_gid = b.comp_gid and b.upload_gid = d.upload_gid and d.upload_status = '2' and d.delete_flag = 'N' "
        shsql &= " inner join sta_trn_tfolio as i on b.comp_gid = i.comp_gid and i.folio_gid = if(b.tran_folio_gid > 0,b.tran_folio_gid,b.folio_gid) and i.delete_flag = 'N' and folio_shares > 0 "
        'shsql &= " left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
        shsql &= " left join sta_trn_tlocreminder as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
        shsql &= " left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
        shsql &= " where true "
        shsql &= lsCond
        shsql &= " and e.comp_listed = 'Y' and b.delete_flag = 'N' and b.reminder_flag = 'Y' and g.status != 'Sent' order by b.approved_date desc"
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
            dgvList.Columns("chklst_disc").Visible = False
            dgvList.Columns("tran_code").Visible = False
            dgvList.Columns("inward_gid").Visible = False
            dgvList.Columns("comp_gid").Visible = False
            dgvList.Columns("inward_status").Visible = False
            dgvList.Columns("folio_gid").Visible = False
            dgvList.Columns("tran_folio_gid").Visible = False
            dgvList.Columns("dematpend_reject_code").Visible = False
            dgvList.Columns("depository_code").Visible = False
            dgvList.Columns("locrem_gid").Visible = False

            Dim btn_Loc As New DataGridViewButtonColumn()
            dgvList.Columns.Add(btn_Loc)
            btn_Loc.HeaderText = "LOC Reminder"
            btn_Loc.Width = 120
            btn_Loc.Text = "Generate"
            btn_Loc.Name = "btn_Loc"
            btn_Loc.UseColumnTextForButtonValue = True

            Dim btn_outward As New DataGridViewButtonColumn()
            dgvList.Columns.Add(btn_outward)
            btn_outward.HeaderText = "Action"
            btn_outward.Width = 100
            btn_outward.Text = "Outward"
            btn_outward.Name = "btn_outward"
            btn_outward.UseColumnTextForButtonValue = True

            AddHandler dgvList.CellPainting, AddressOf dgvList_CellPainting

        End With
        txtTotRec.Text = "Total Records : " & dgvList.RowCount
        'gOdbcConn.Close()
    End Sub

    Private Sub dgvList_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim dgv As DataGridView = DirectCast(sender, DataGridView)
            Dim columnName As String = dgv.Columns(e.ColumnIndex).Name

            If columnName = "btn_Loc" OrElse columnName = "btn_outward" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim rect As Rectangle = e.CellBounds
                rect.Inflate(-2, -2)

                ' Set button-specific colors
                Dim backColor As Color = If(columnName = "btn_Loc", Color.ForestGreen, Color.Red)
                Dim foreColor As Color = Color.White
                Dim buttonText As String = If(columnName = "btn_Loc", "Generate", "Outward")

                ' Draw button background
                Using brush As New SolidBrush(backColor)
                    e.Graphics.FillRectangle(brush, rect)
                End Using

                ' Draw centered text
                TextRenderer.DrawText(e.Graphics, buttonText, dgv.Font, rect, foreColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim objFrm As Form
        Dim mnInwardId As Long
        Dim mnComp_gid As Long
        Dim mnLocreminder_gid As Long
        Dim lsSql As String = ""

        mnLocreminder_gid = dgvList.Rows(e.RowIndex).Cells("locrem_gid").Value

        If dgvList.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() = "Outward" Then
            objFrm = New frmReminderOutwardEntry("UPDATE", mnInwardId, mnLocreminder_gid)
            objFrm.ShowDialog()
            Call LoadGriddata()
        ElseIf dgvList.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() = "Generate" Then
            Dim fileReader As String
            Dim days As String
            Dim calcdays As String
            Dim inputFile As String = ""
            days = dgvList.Rows(e.RowIndex).Cells("Rem Days").Value
            calcdays = dgvList.Rows(e.RowIndex).Cells("Days").Value

            If days = 45 Then
                inputFile = "c:\STA EXE\LOC_Reminder1_Template.rtf"
                If Val(calcdays) < Val(days) Then
                    MessageBox.Show("After 45 days only you can Generate the Reminder1 Letter !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            ElseIf days = 90 Then
                inputFile = "c:\STA EXE\LOC_Reminder2_Template.rtf"
                If Val(calcdays) < Val(days) Then
                    MessageBox.Show("After 90 days only you can Generate the Reminder2 Letter !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                inputFile = "c:\STA EXE\RTA_LetterToCompany_After120Days.rtf"
                If Val(calcdays) < Val(days) Then
                    MessageBox.Show("After 120 days only you can Generate the RTA Letter !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

            Dim shsql As String
            Dim ds1 As DataSet


            fileReader = ""
            mnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value
            mnComp_gid = dgvList.Rows(e.RowIndex).Cells("comp_gid").Value
            If days = 45 Or days = 90 Then
                shsql = ""
                shsql &= "select  distinct i.holder1_name as shareholder_name,"
                shsql &= "i.holder2_name,"
                shsql &= "i.holder3_name,"
                shsql &= "i.holder1_pan_no,"
                shsql &= "i.holder2_pan_no,"
                shsql &= "i.holder3_pan_no,"
                shsql &= "concat(i.folio_addr1,',',i.folio_addr2,',',i.folio_addr3) as folio_address,"
                shsql &= "concat(i.folio_city,',',i.folio_state,',',i.folio_pincode) as city_state_pincode,"
                shsql &= "i.folio_contact_no,"
                shsql &= "i.folio_shares,"
                shsql &= "i.folio_no,"
                shsql &= "b.inward_comp_no,"
                shsql &= "e.comp_name,"
                shsql &= "b.tran_code, "
                shsql &= "date_format(g.outward_date,'%d-%m-%Y') as outward_date , "
                shsql &= "g.awb_no as outward_awbno , "
                shsql &= "date_format(d.meeting_date,'%d-%m-%Y') as meeting_date ,"
                shsql &= "date_format(j.reminder_sent_date,'%d-%m-%Y') as reminder_sent_date ,"
                shsql &= "date_format(DATE_ADD(d.meeting_date, INTERVAL 45 DAY),'%d-%m-%Y') as reminder1_date,"
                shsql &= "date_format(DATE_ADD(d.meeting_date, INTERVAL 90 DAY),'%d-%m-%Y') as reminder2_date,"
                shsql &= "date_format(DATE_ADD(d.meeting_date, INTERVAL 120 DAY),'%d-%m-%Y') as reminder3_date "
                shsql &= "from sta_trn_tinward as b "
                shsql &= "inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
                shsql &= "inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
                shsql &= "left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
                shsql &= "left join sta_trn_tlocreminder as j on j.inward_gid = b.inward_gid and j.delete_flag = 'N' "
                shsql &= "left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
                shsql &= "inner join sta_trn_tfolio as i on i.folio_gid = if(b.tran_folio_gid > 0,b.tran_folio_gid,b.folio_gid) "
                shsql &= "and b.comp_gid = i.comp_gid and i.delete_flag = 'N' "
                shsql &= "inner join sta_trn_tupload as d on b.comp_gid = b.comp_gid and b.upload_gid = d.upload_gid and d.upload_status = '2' "
                shsql &= "and d.delete_flag = 'N' "
                shsql &= "where true and b.comp_gid = " & mnComp_gid & "  and b.inward_gid = " & mnInwardId & " and j.locrem_gid = " & mnLocreminder_gid & " and b.reminder_flag = 'Y' "
                shsql &= "and j.status = 'Yet to sent' and b.delete_flag = 'N' "
                shsql &= "group by b.inward_gid;"

                ds1 = New DataSet
                Call gpDataSet(shsql, "loc", gOdbcConn, ds1)

                With ds1.Tables("loc")
                    If .Rows.Count > 0 Then
                        fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
                        fileReader = fileReader.Replace("<<date>>", .Rows(0).Item("reminder_sent_date").ToString)
                        fileReader = fileReader.Replace("<Name of the Shareholder>", .Rows(0).Item("shareholder_name").ToString)
                        fileReader = fileReader.Replace("<Address>", .Rows(0).Item("folio_address").ToString)
                        fileReader = fileReader.Replace("<Pincode>", .Rows(0).Item("city_state_pincode").ToString)
                        fileReader = fileReader.Replace("<XXXXXXXXXX>", .Rows(0).Item("folio_contact_no").ToString)
                        fileReader = fileReader.Replace("<Company Name>", .Rows(0).Item("comp_name").ToString)
                        fileReader = fileReader.Replace("<Shares Count>", .Rows(0).Item("folio_shares").ToString)
                        fileReader = fileReader.Replace("<Folio No.>", .Rows(0).Item("folio_no").ToString)
                        fileReader = fileReader.Replace("<Issued Date>", .Rows(0).Item("meeting_date").ToString)
                        fileReader = fileReader.Replace("<Reminder1 Date>", .Rows(0).Item("reminder1_date").ToString)
                        fileReader = fileReader.Replace("<Reminder2 Date>", .Rows(0).Item("reminder2_date").ToString)
                        fileReader = fileReader.Replace("<Reminder3 Date>", .Rows(0).Item("reminder3_date").ToString)
                        fileReader = fileReader.Replace("<inward_no>", .Rows(0).Item("inward_comp_no").ToString)
                        fileReader = fileReader.Replace("<Outward Date>", .Rows(0).Item("outward_date").ToString)
                        fileReader = fileReader.Replace("<OutwardAwbno>", .Rows(0).Item("outward_awbno").ToString)
                        'Else
                        '    MessageBox.Show("Please do the Outward & Generate..!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Exit Sub
                    End If

                    Dim outputFile As String = "C:\LocReminder"

                    ' If folder doesnot exists means create a directory folder
                    If Not System.IO.Directory.Exists(outputFile) Then
                        System.IO.Directory.CreateDirectory(outputFile)
                    End If

                    outputFile = Path.ChangeExtension(outputFile + "\LocReminder.rtf", ".rtf")

                    ' Read our HTML file a string.
                    Dim htmlString As String = File.ReadAllText(inputFile)

                    ' Open the result for demonstration purposes.
                    If Not String.IsNullOrEmpty(outputFile) Then
                        File.WriteAllText(outputFile, fileReader)
                        System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
                    End If

                    'UPDATE The status as "Sent"
                    'lsSql = ""
                    'lsSql &= "update sta_trn_tlocreminder "
                    'lsSql &= "set status = 'Sent' "
                    'lsSql &= "where locrem_gid = " & mnLocreminder_gid & " and status = 'Yet to Sent' and delete_flag = 'N';"

                    'gfInsertQry(lsSql, gOdbcConn)

                    ds1.Clear()
                End With
            Else
                shsql = ""
                shsql &= "select  distinct i.holder1_name as shareholder_name,"
                shsql &= "i.holder2_name,"
                shsql &= "i.holder3_name,"
                shsql &= "i.holder1_pan_no,"
                shsql &= "i.holder2_pan_no,"
                shsql &= "i.holder3_pan_no,"
                shsql &= "i.folio_addr1,"
                shsql &= "i.folio_addr2,"
                shsql &= "i.folio_addr3,"
                shsql &= "i.folio_city,"
                shsql &= "i.folio_pincode,"
                shsql &= "i.folio_state,"
                shsql &= "i.folio_contact_no,"
                shsql &= "i.folio_shares,"
                shsql &= "i.folio_no,"
                shsql &= "fn_sta_get_certno(i.folio_gid," & mnInwardId & "," & mnComp_gid & ") as cert_no,"
                shsql &= "group_concat(concat(k.dist_from," + "' TO '" + ",k.dist_to)) as dist_fromto ,"
                shsql &= "group_concat(k.dist_from) as dist_from ,"
                shsql &= "group_concat(k.dist_to) as dist_to ,"
                shsql &= "b.inward_comp_no,"
                shsql &= "e.comp_name,"
                shsql &= "e.depository_name,"
                shsql &= "concat(e.dp_id,e.client_id) as dp_client_id, "
                shsql &= "e.client_id,"
                shsql &= "b.tran_code, "
                shsql &= "fn_sta_get_escrowfoliono(e.comp_gid) as escrow_foliono, "
                shsql &= "date_format(d.meeting_date,'%d-%m-%Y') as meeting_date , "
                shsql &= "date_format(DATE_ADD(d.meeting_date, INTERVAL 120 DAY),'%d-%m-%Y') as reminder3_sent_date , "
                shsql &= "date_format(g.outward_date,'%d-%m-%Y') as outward_date , "
                shsql &= "g.awb_no as outward_awbno , "
                shsql &= "concat(e.address1,',',e.address2,',',e.address3) as comp_address , "
                shsql &= "e.city , "
                shsql &= "e.pincode "
                shsql &= "from sta_trn_tinward as b "
                shsql &= "inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
                shsql &= "inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
                shsql &= "left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
                shsql &= "left join sta_trn_tlocreminder as f on f.inward_gid = b.inward_gid and f.delete_flag = 'N' "
                shsql &= "left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
                shsql &= "inner join sta_trn_tfolio as i on i.folio_gid = if(b.tran_folio_gid > 0,b.tran_folio_gid,b.folio_gid) "
                shsql &= "and b.comp_gid = i.comp_gid and i.delete_flag = 'N' "
                shsql &= "inner join sta_trn_tupload as d on b.comp_gid = b.comp_gid and b.upload_gid = d.upload_gid and d.upload_status = '2' "
                shsql &= "and d.delete_flag = 'N' "
                shsql &= " inner join sta_trn_tcertentry as l on b.inward_gid = l.inward_gid and l.delete_flag = 'N' "
                shsql &= " inner join sta_trn_tcert as j on l.cert_gid = j.cert_gid and j.delete_flag = 'N' "
                shsql &= " inner join sta_trn_tcertdist as k on j.cert_gid = k.cert_gid and k.delete_flag = 'N' "
                shsql &= "where true and b.comp_gid = " & mnComp_gid & "  and b.inward_gid = " & mnInwardId & " and f.locrem_gid = " & mnLocreminder_gid & " and b.reminder_flag = 'Y' "
                shsql &= "and f.status = 'Yet to sent' and b.delete_flag = 'N' "
                shsql &= "group by b.inward_gid;"

                ds1 = New DataSet
                Call gpDataSet(shsql, "loc", gOdbcConn, ds1)
                'RTA Letter generation
                With ds1.Tables("loc")
                    If .Rows.Count > 0 Then
                        fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
                        fileReader = fileReader.Replace("<Reminder3Date>", .Rows(0).Item("reminder3_sent_date").ToString)
                        fileReader = fileReader.Replace("<Company Name>", .Rows(0).Item("comp_name").ToString)
                        fileReader = fileReader.Replace("<Comp Address>", .Rows(0).Item("comp_address").ToString)
                        fileReader = fileReader.Replace("<City>", .Rows(0).Item("city").ToString)
                        fileReader = fileReader.Replace("<Pin code>", .Rows(0).Item("pincode").ToString)
                        fileReader = fileReader.Replace("<Folio No>", .Rows(0).Item("folio_no").ToString)
                        fileReader = fileReader.Replace("<ShareCount>", .Rows(0).Item("folio_shares").ToString)
                        fileReader = fileReader.Replace("<DistFrom>", .Rows(0).Item("dist_from").ToString)
                        fileReader = fileReader.Replace("<DistTo>", .Rows(0).Item("dist_to").ToString)
                        fileReader = fileReader.Replace("<CertNo>", .Rows(0).Item("cert_no").ToString)
                        fileReader = fileReader.Replace("<LocIssueDate>", .Rows(0).Item("meeting_date").ToString)
                        fileReader = fileReader.Replace("<DpClientId>", .Rows(0).Item("dp_client_id").ToString)
                        fileReader = fileReader.Replace("<DepositoryName>", .Rows(0).Item("depository_name").ToString)
                        fileReader = fileReader.Replace("<EscrowAcountFolioNo>", .Rows(0).Item("escrow_foliono").ToString)
                        'Else
                        '    MessageBox.Show("Please do the Outward & Generate..!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Exit Sub
                    End If

                    Dim outputFile As String = "C:\LocReminder"

                    ' If folder doesnot exists means create a directory folder
                    If Not System.IO.Directory.Exists(outputFile) Then
                        System.IO.Directory.CreateDirectory(outputFile)
                    End If

                    outputFile = Path.ChangeExtension(outputFile + "\LocReminder.rtf", ".rtf")

                    ' Read our HTML file a string.
                    Dim htmlString As String = File.ReadAllText(inputFile)

                    ' Open the result for demonstration purposes.
                    If Not String.IsNullOrEmpty(outputFile) Then
                        File.WriteAllText(outputFile, fileReader)
                        System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
                    End If

                    ds1.Clear()
                End With

            End If
        End If

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtRemiday_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRemiday.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCalcDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCalcDays.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvList.DataBindingComplete
        For Each row As DataGridViewRow In dgvList.Rows
            Try
                Dim remDays As Integer = Val(row.Cells("Rem Days").Value)
                Dim days As Integer = Val(row.Cells("Days").Value)

                If days >= remDays Then
                    row.DefaultCellStyle.BackColor = Color.Lavender
                    row.DefaultCellStyle.ForeColor = Color.Black
                End If
            Catch ex As Exception
                ' Optional: log or ignore rows that can't be parsed
            End Try
        Next
    End Sub
End Class