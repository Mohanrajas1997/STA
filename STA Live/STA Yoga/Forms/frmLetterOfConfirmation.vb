Imports Microsoft.Office.Interop.Word
Imports System
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports MySql.Data.MySqlClient

Public Class frmLetterOfConfirmation
    Dim objWord As Application
    Dim objDoc As Microsoft.Office.Interop.Word.Document

    ' Dim tlb As Table
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=192.168.0.182;DataBase=sta;uid=production;pwd=gnsalive;port=3306")
    Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=" & DbIP & ";DataBase=" & Db & ";uid=" & DbUId & ";pwd=" & DbPwd & ";port=" & DbPort)
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=146.56.55.230;DataBase=sta;uid=root;pwd=Flexi@123;port=3306")
    Dim ds, dt, dt_t, dt_tt, dt_re, dt_st, dmtt, dmtt1 As DataSet
    Dim da As Odbc.OdbcDataAdapter
    Dim cmd As New Odbc.OdbcCommand
    Dim sql As String

    Public Function QuoteFilter(ByVal txt As String) As String
        QuoteFilter = Trim(Replace(Replace(Replace(txt, "'", " "), """", """"""), "\", "\\"))
    End Function

    Private Sub covering_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim lsSql As String
        Dim csSql As String
        con.Open()
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' and trantype_code in ('TM','LS','CO','SP')"
        lsSql &= " order by trantype_code asc "
        ds = New DataSet
        da = New Odbc.OdbcDataAdapter(lsSql, con)
        da.Fill(ds, "table")

        cbo_doc.DataSource = ds.Tables(0)
        cbo_doc.ValueMember = "trantype_code"
        cbo_doc.DisplayMember = "trantype_desc"


        csSql = ""
        csSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        csSql &= " where delete_flag = 'N' "
        csSql &= " order by comp_name asc "
        ds = New DataSet
        da = New Odbc.OdbcDataAdapter(csSql, con)
        da.Fill(ds, "table")

        cb_cmpy.ValueMember = "comp_gid"
        cb_cmpy.DisplayMember = "comp_name"
        cb_cmpy.DataSource = ds.Tables(0)

        con.Close()
        dtp_from.Value = Now
        dtp_to.Value = Now
        dtp_owd.Value = Now

        dtp_from.Checked = False
        dtp_to.Checked = False
        dtp_owd.Checked = False

    End Sub

    Private Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        con.Open()
        Dim shsql As String

        Dim lsCond As String = ""
        lsCond = ""

        If dtp_from.Checked = True Then lsCond &= " and b.received_date >= '" & Format(dtp_from.Value, "yyyy-MM-dd") & "' "
        If dtp_to.Checked = True Then lsCond &= " and b.received_date <= '" & Format(dtp_to.Value, "yyyy-MM-dd") & "' "
        If dtp_owd.Checked = True Then lsCond &= " and g.outward_date <= '" & Format(dtp_to.Value, "yyyy-MM-dd") & "' "
        If txt_inward.Text <> "" Then lsCond &= " and b.inward_comp_no = '" & QuoteFilter(txt_inward.Text) & "' "
        If txt_folio.Text <> "" Then lsCond &= " and b.folio_no like '" & QuoteFilter(txt_folio.Text) & "%'"

        If cb_cmpy.Text <> "" And cb_cmpy.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cb_cmpy.SelectedValue.ToString) & " "
        End If

        If cbo_doc.Text <> "" And cbo_doc.SelectedIndex <> -1 Then
            lsCond &= " and b.tran_code = '" & cbo_doc.SelectedValue.ToString & "' "
            'If cbo_doc.Text = "LS-Loss of Shares" Or cbo_doc.Text = "TM-Transmission" Or cbo_doc.Text = "CO-Consolidation of Shares" Or cbo_doc.Text = "SP-Split of Shares" Then
            '    lsCond &= " and b.inward_status = '4' "
            '    'and b.queue_status = '32' "
            'End If
        Else
            lsCond &= " and b.tran_code in ('TM','LS','CO','SP') "
        End If

        shsql = ""
        shsql &= " select "
        shsql &= " b.inward_comp_no as 'Inward No',"
        shsql &= " e.comp_gid,"
        shsql &= " e.comp_name as 'Company',"
        shsql &= " b.folio_no as 'Folio No',"
        shsql &= " b.shareholder_name as 'Share Holder',"
        shsql &= " b.tran_code,"
        shsql &= " b.inward_status,"
        shsql &= " b.chklst_disc,"
        shsql &= " ifnull(b.dematpend_reject_code,0) as dematpend_reject_code,"
        shsql &= " c.trantype_desc as 'Document',"
        shsql &= " b.folio_gid,"
        shsql &= " make_set(b.inward_status,'Received','Inprocess','Completed','Reject','Reprocess','Despatch','Inex') as 'Inward Status',"
        shsql &= " make_set(b.queue_status,'Inward','Maker','Checker','Authorizer','Upload','Despatch','Inex') as 'Queue Status',"
        shsql &= " b.inward_gid, "
        shsql &= " b.tran_folio_gid , ifnull(h.depository_code,'""') as depository_code"
        shsql &= " from sta_trn_tinward as b "
        shsql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
        shsql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
        shsql &= " inner join sta_trn_tupload as d on b.comp_gid = b.comp_gid and b.upload_gid = d.upload_gid and d.upload_status = '2' and d.delete_flag = 'N' "
        shsql &= " left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
        shsql &= " left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
        shsql &= " where true "
        shsql &= lsCond
        shsql &= " and b.delete_flag = 'N' order by d.meeting_date desc"
        'shsql &= " group by b.inward_gid desc"

        With cmd
            .Connection = con
            .CommandText = shsql

        End With
        With dgv_covering
            .Columns.Clear()
            'filling  data in the table
            ds = New DataSet
            da = New Odbc.OdbcDataAdapter(shsql, con)
            da.Fill(ds, "tbl")
            dgv_covering.DataSource = ds.Tables(0)
            dgv_covering.Columns("chklst_disc").Visible = False
            dgv_covering.Columns("tran_code").Visible = False
            dgv_covering.Columns("inward_gid").Visible = False
            dgv_covering.Columns("comp_gid").Visible = False
            dgv_covering.Columns("inward_status").Visible = False
            dgv_covering.Columns("folio_gid").Visible = False
            dgv_covering.Columns("tran_folio_gid").Visible = False
            dgv_covering.Columns("dematpend_reject_code").Visible = False
            dgv_covering.Columns("depository_code").Visible = False


            Dim btn_cover As New DataGridViewButtonColumn()
            dgv_covering.Columns.Add(btn_cover)
            btn_cover.HeaderText = "Letter Of Confirmation"
            btn_cover.Width = "140"
            btn_cover.Text = "Generate"
            btn_cover.Name = "btn_cover"
            btn_cover.UseColumnTextForButtonValue = True
        End With
        txttotalrecord.Text = dgv_covering.RowCount
        con.Close()
    End Sub

    Private Sub dgv_covering_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_covering.CellContentClick

        If e.ColumnIndex = 16 Then
            Dim fileReader As String
            Dim inputFile As String = "c:\STA EXE\LOC_Template.rtf"
            Dim shsql As String
            Dim ds1 As DataSet

            Dim mnInwardId As Long
            Dim mnComp_gid As Long
            fileReader = ""
            mnInwardId = dgv_covering.Rows(e.RowIndex).Cells("inward_gid").Value
            mnComp_gid = dgv_covering.Rows(e.RowIndex).Cells("comp_gid").Value

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
            shsql &= "j.share_count as cert_shares,"
            shsql &= "i.folio_no,"
            shsql &= "fn_sta_get_certno(i.folio_gid," & mnInwardId & "," & mnComp_gid & ") as cert_no,"
            'shsql &= "j.cert_no,"
            shsql &= "group_concat(concat(k.dist_from," + "' TO '" + ",k.dist_to)) as dist_fromto ,"
            'shsql &= "concat(k.dist_from," + "' TO '" + ",k.dist_to) as dist_fromto ,"
            shsql &= "b.inward_comp_no,"
            shsql &= "e.comp_name,"
            shsql &= "b.tran_code, "
            shsql &= "date_format(d.meeting_date,'%d-%m-%Y') as meeting_date ,"
            shsql &= "e.address1 ,"
            shsql &= "e.city ,"
            shsql &= "e.pincode "
            'If cbo_doc.Text = "TM-Transmission" Then
            shsql &= "from sta_trn_tinward as b "
            shsql &= "inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
            shsql &= "inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
            shsql &= "left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
            shsql &= "left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
            shsql &= "inner join sta_trn_tfolio as i on i.folio_gid = if(b.tran_folio_gid > 0,b.tran_folio_gid,b.folio_gid) "
            shsql &= "and b.comp_gid = i.comp_gid and i.delete_flag = 'N' "
            shsql &= "inner join sta_trn_tupload as d on b.comp_gid = b.comp_gid and b.upload_gid = d.upload_gid and d.upload_status = '2' "
            shsql &= "and d.delete_flag = 'N' "
            shsql &= " inner join sta_trn_tcertentry as l on b.inward_gid = l.inward_gid and l.delete_flag = 'N' "
            shsql &= " inner join sta_trn_tcert as j on l.cert_gid = j.cert_gid and j.delete_flag = 'N' "
            shsql &= " inner join sta_trn_tcertdist as k on j.cert_gid = k.cert_gid and k.delete_flag = 'N' "
            shsql &= "where true and b.comp_gid = " & mnComp_gid & "  and b.inward_gid = " & mnInwardId & " and b.delete_flag = 'N' "
            shsql &= "group by b.inward_gid;"
            'Else
            '    shsql &= "from sta_trn_tinward as b "
            '    shsql &= "inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
            '    shsql &= "inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
            '    shsql &= "left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
            '    shsql &= "left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
            '    shsql &= "left join sta_trn_tfolio as i on b.tran_folio_gid = i.folio_gid and i.delete_flag = 'N' "
            '    shsql &= "and b.comp_gid = i.comp_gid and i.delete_flag = 'N' "
            '    shsql &= "inner join sta_trn_tupload as d on b.comp_gid = b.comp_gid and b.upload_gid = d.upload_gid and d.upload_status = '2' "
            '    shsql &= "and d.delete_flag = 'N' "
            '    shsql &= "left join sta_trn_tcert as j on b.tran_cert_no = j.cert_no and  i.folio_gid = j.folio_gid and i.comp_gid = j.comp_gid and j.delete_flag = 'N' "
            '    shsql &= "left join sta_trn_tcertdist as k on j.cert_gid = k.cert_gid and k.delete_flag = 'N' "
            '    shsql &= "where true and b.comp_gid = " & mnComp_gid & "  and b.inward_gid = " & mnInwardId & " and b.delete_flag = 'N' "
            '    shsql &= "group by b.inward_gid;"
            'End If

            ds1 = New DataSet
            Call gpDataSet(shsql, "loc", gOdbcConn, ds1)

            With ds1.Tables("loc")
                If .Rows.Count > 0 Then
                    fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
                    fileReader = fileReader.Replace("<<date>>", Date.Today.ToString("dd-MM-yyyy"))
                    fileReader = fileReader.Replace("<<holder1_name>>", .Rows(0).Item("shareholder_name").ToString)
                    fileReader = fileReader.Replace("<<holder2_name>>", .Rows(0).Item("holder2_name").ToString)
                    fileReader = fileReader.Replace("<<holder3_name>>", .Rows(0).Item("holder3_name").ToString)
                    fileReader = fileReader.Replace("<<holder1_pan_no>>", .Rows(0).Item("holder1_pan_no").ToString)
                    fileReader = fileReader.Replace("<<holder2_pan_no>>", .Rows(0).Item("holder2_pan_no").ToString)
                    fileReader = fileReader.Replace("<<holder3_pan_no>>", .Rows(0).Item("holder3_pan_no").ToString)
                    fileReader = fileReader.Replace("<<folio_addr1>>", .Rows(0).Item("folio_addr1").ToString)
                    fileReader = fileReader.Replace("<<folio_addr2>>", .Rows(0).Item("folio_addr2").ToString)
                    fileReader = fileReader.Replace("<<folio_city>>", .Rows(0).Item("folio_city").ToString)
                    fileReader = fileReader.Replace("<<folio_pincode>>", .Rows(0).Item("folio_pincode").ToString)
                    fileReader = fileReader.Replace("<<folio_state>>", .Rows(0).Item("folio_state").ToString)
                    fileReader = fileReader.Replace("<<folio_contact_no>>", .Rows(0).Item("folio_contact_no").ToString)
                    fileReader = fileReader.Replace("<<company_name>>", .Rows(0).Item("comp_name").ToString)
                    fileReader = fileReader.Replace("<<company_addr1>>", .Rows(0).Item("address1").ToString)
                    fileReader = fileReader.Replace("<<company_city>>", .Rows(0).Item("city").ToString)
                    fileReader = fileReader.Replace("<<company_pincode>>", .Rows(0).Item("pincode").ToString)
                    fileReader = fileReader.Replace("<<folio_shares>>", .Rows(0).Item("folio_shares").ToString)
                    fileReader = fileReader.Replace("<<folio_no>>", .Rows(0).Item("folio_no").ToString)
                    fileReader = fileReader.Replace("<<certificate_no>>", .Rows(0).Item("cert_no").ToString)
                    fileReader = fileReader.Replace("<<dist_from>>  TO <<dist_to>>", .Rows(0).Item("dist_fromto").ToString)
                    fileReader = fileReader.Replace("<<tran_code>>", .Rows(0).Item("tran_code").ToString)
                    fileReader = fileReader.Replace("<<completion_date>>", .Rows(0).Item("meeting_date").ToString)
                    fileReader = fileReader.Replace("<<inward_no>>", .Rows(0).Item("inward_comp_no").ToString)
                End If
            End With

            Dim outputFile As String = "C:\letterofconfirmation"

            ' If folder doesnot exists means create a directory folder
            If Not System.IO.Directory.Exists(outputFile) Then
                System.IO.Directory.CreateDirectory(outputFile)
            End If

            outputFile = Path.ChangeExtension(outputFile + "\LetterOfConfirmation.rtf", ".rtf")

            ' Read our HTML file a string.
            Dim htmlString As String = File.ReadAllText(inputFile)

            ' Open the result for demonstration purposes.
            If Not String.IsNullOrEmpty(outputFile) Then
                File.WriteAllText(outputFile, fileReader)
                System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
            End If
            ds1.Clear()
        End If

    End Sub

    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With dgv_covering
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgv_covering.Top + dgv_covering.Height + 6
        pnlExport.Left = dgv_covering.Left
        pnlExport.Width = dgv_covering.Width
        btnExport.Left = Math.Abs(pnlExport.Width - btnExport.Width)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgv_covering, gsReportPath & "\Report.xls", "Report")
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

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        dtp_from.Checked = False
        dtp_to.Checked = False
        txt_inward.Text = ""
        txt_folio.Text = ""
        cb_cmpy.Text = ""
        cbo_doc.Text = ""
        dtp_owd.Checked = False
        txttotalrecord.Text = ""
    End Sub
End Class

