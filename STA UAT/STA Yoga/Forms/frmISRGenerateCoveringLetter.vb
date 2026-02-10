Imports Microsoft.Office.Interop.Word
Imports Microsoft.Office.Interop
Imports System.IO

Public Class frmISRGenerateCoveringLetter
    Dim objWord As Application
    Dim objDoc As Microsoft.Office.Interop.Word.Document

    ' Dim tlb As Table
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=192.168.0.182;DataBase=sta;uid=production;pwd=gnsalive;port=3306")
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=146.56.55.230;DataBase=sta;uid=root;pwd=Flexi@123;port=3306")
    Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=" & DbIP & ";DataBase=" & Db & ";uid=" & DbUId & ";pwd=" & DbPwd & ";port=" & DbPort)
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
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "
        ds = New DataSet
        da = New Odbc.OdbcDataAdapter(lsSql, con)
        da.Fill(ds, "table")


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
        If txt_folio.Text <> "" Then lsCond &= " and b.folio_no = '" & QuoteFilter(txt_folio.Text) & "'"
        If cb_cmpy.Text <> "" And cb_cmpy.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cb_cmpy.SelectedValue.ToString) & " "
        End If

        shsql = ""
        shsql &= " select "
        shsql &= " b.inward_comp_no as 'Inward No',"
        shsql &= " e.comp_name as 'Company',"
        shsql &= " b.comp_gid as 'Comp_gid',"
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
        shsql &= " b.tran_folio_gid "
        shsql &= " from sta_trn_tinward as b "
        shsql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
        shsql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
        shsql &= " inner join sta_trn_tqueue as q on q.inward_gid = b.inward_gid "
        shsql &= " and q.queue_from = 'C' and q.queue_to = 'D' and q.delete_flag = 'N' "
        shsql &= " left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
        shsql &= " where true and b.chklst_disc = 0 and b.tran_code = 'IS'"
        shsql &= lsCond
        shsql &= " and b.delete_flag = 'N' "
        shsql &= " order by b.inward_gid desc"

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


            Dim btn_cover As New DataGridViewButtonColumn()
            dgv_covering.Columns.Add(btn_cover)
            btn_cover.HeaderText = "Covering Letter"
            btn_cover.Text = "Generate"
            btn_cover.Name = "btn_cover"
            btn_cover.UseColumnTextForButtonValue = True
        End With
        con.Close()
    End Sub

    Private Sub dgv_covering_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_covering.CellContentClick
        con.Open()
        Dim colName As String = dgv_covering.Columns(e.ColumnIndex).Name
        If colName = "btn_cover" Then
            Dim fileReader As String
            Dim inputFile As String = "c:\STA EXE\ISR_Divident_Letter_Format.rtf"
            Dim shsql As String
            Dim ds1 As DataSet
            Dim ds2 As DataSet

            Dim lsCompgid As Long
            Dim lsFolioNo As String
            fileReader = ""
            lsFolioNo = dgv_covering.Rows(e.RowIndex).Cells("Folio No").Value
            lsCompgid = dgv_covering.Rows(e.RowIndex).Cells("comp_gid").Value

            'Company Details
            shsql = ""
            shsql &= "select "
            shsql &= "comp_name,"
            shsql &= "concat(address1,',',address2,',',address3) as 'comp_address',"
            shsql &= "city,"
            shsql &= "pincode "
            shsql &= "from sta_mst_tcompany "
            shsql &= "where comp_gid = " & lsCompgid & " and delete_flag = 'N'; "

            ds1 = New DataSet
            Call gpDataSet(shsql, "Comp", gOdbcConn, ds1)

            'Divident Details
            shsql = ""
            shsql &= "select distinct "
            shsql &= "a.folio_dpid as 'Folio No',"
            shsql &= "e.holder1_name as 'Share Holder',"
            shsql &= "ifnull(a.div_amount,0) as 'Dividend Amount',"
            shsql &= "a.warrant_no as 'Warrant No',"
            shsql &= "ifnull(a.div_date,'')  as 'Dividend Date',"
            shsql &= "c.finyear_code as 'FinYear Code',"
            shsql &= "da.acc_no as 'Divident Account No',"
            shsql &= "ifnull(a.div_remark,'Unclaimed Dividend') as 'Remarks',"
            shsql &= "e.bank_acc_no  as holder1_acc_no,"
            shsql &= "e.bank_name as holder1_bank_name ,"
            shsql &= "e.bank_branch as holder1_bank_branch ,"
            shsql &= "e.bank_ifsc_code as holder1_ifsc_code ,"
            shsql &= "e.bank_micr_code as holder1_micr_code "
            shsql &= "from div_trn_tdividend as a "
            shsql &= "inner join div_mst_tacc as da on a.acc_gid = da.acc_gid and da.delete_flag = 'N' "
            shsql &= "inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N'  "
            shsql &= "inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and c.delete_flag = 'N'  "
            shsql &= "inner join sta_mst_tpaymode as d on a.div_pay_mode = d.paymode_code  and d.delete_flag='N' "
            shsql &= "inner join sta_trn_tfolio as e on e.folio_no = a.folio_dpid  and e.comp_gid = b.comp_gid and e.delete_flag='N' "
            shsql &= "where a.comp_gid = " & lsCompgid & "  and a.folio_dpid = '" & lsFolioNo & "' and a.div_status = 'U' and b.delete_flag = 'N' "
            shsql &= "order by c.finyear_code asc;"

            ds2 = New DataSet
            Call gpDataSet(shsql, "ISR", gOdbcConn, ds2)

            fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
            fileReader = fileReader.Replace("<CurrentDate>", Date.Today.ToString("dd-MM-yyyy"))

            With ds1.Tables("Comp")
                If .Rows.Count > 0 Then
                    fileReader = fileReader.Replace("<Company_Name>", .Rows(0).Item("comp_name").ToString)
                    fileReader = fileReader.Replace("<Comp_address>", .Rows(0).Item("comp_address").ToString)
                    fileReader = fileReader.Replace("<City>", .Rows(0).Item("city").ToString)
                    fileReader = fileReader.Replace("<Pincode>", .Rows(0).Item("pincode").ToString)
                End If
            End With

            With ds2.Tables("ISR")
                If .Rows.Count > 0 Then
                    fileReader = fileReader.Replace("<FolioNo>", .Rows(0).Item("Folio No").ToString)
                    fileReader = fileReader.Replace("<HolderName>", .Rows(0).Item("Share Holder").ToString)
                    fileReader = fileReader.Replace("<AccountHolder>", .Rows(0).Item("Share Holder").ToString)
                    fileReader = fileReader.Replace("<BankName>", .Rows(0).Item("holder1_bank_name").ToString)
                    fileReader = fileReader.Replace("<BankBranch>", .Rows(0).Item("holder1_bank_branch").ToString)
                    fileReader = fileReader.Replace("<IFSC_Code>", .Rows(0).Item("holder1_ifsc_code").ToString)
                    fileReader = fileReader.Replace("<MicrNumber>", .Rows(0).Item("holder1_micr_code").ToString)
                    fileReader = fileReader.Replace("<AccountNumber>", .Rows(0).Item("holder1_acc_no").ToString)
                End If
            End With

            Dim dividendRTF As String = BuildDividendRTF(ds2)
            fileReader = fileReader.Replace("<DividendDetails>", dividendRTF)

            Dim outputFile As String = "C:\ISR_DividentLetter"

            ' If folder doesnot exists means create a directory folder
            If Not System.IO.Directory.Exists(outputFile) Then
                System.IO.Directory.CreateDirectory(outputFile)
            End If

            outputFile = Path.ChangeExtension(outputFile + "\ISR_DividentLetter.rtf", ".rtf")

            ' Read our HTML file a string.
            Dim htmlString As String = File.ReadAllText(inputFile)

            ' Open the result for demonstration purposes.
            If Not String.IsNullOrEmpty(outputFile) Then
                File.WriteAllText(outputFile, fileReader)
                System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
            End If
            ds1.Clear()
        End If

        con.Close()
    End Sub

    Private Function BuildDividendRTF(ds As DataSet) As String
        Dim sb As New System.Text.StringBuilder()

        '============ HEADER ROW ====================
        sb.AppendLine("\trowd\trgaph108\trleft0")

        Dim cellDef As String =
            "\clbrdrt\brdrs\brdrw10" &
            "\clbrdrl\brdrs\brdrw10" &
            "\clbrdrb\brdrs\brdrw10" &
            "\clbrdrr\brdrs\brdrw10"

        sb.AppendLine(cellDef & "\cellx1200")
        sb.AppendLine(cellDef & "\cellx3600")
        sb.AppendLine(cellDef & "\cellx5000")
        sb.AppendLine(cellDef & "\cellx6300")
        sb.AppendLine(cellDef & "\cellx7600")
        sb.AppendLine(cellDef & "\cellx8800")
        sb.AppendLine(cellDef & "\cellx10300")
        'sb.AppendLine(cellDef & "\cellx12500")

        sb.AppendLine("\intbl\b " &
                      "Folio No\cell " &
                      "Shareholder Name\cell " &
                      "Net Amount\cell " &
                      "Warrant No\cell " &
                      "Div Date\cell " &
                      "Fin Year\cell " &
                      "Account No\cell\b0\row")
        '"Dividend Bank Account\cell " &
        '"Remarks\cell\b0\row")

        '============ DATA ROWS =====================
        For Each dr As DataRow In ds.Tables("ISR").Rows
            sb.AppendLine("\trowd\trgaph108\trleft0")

            sb.AppendLine(cellDef & "\cellx1200")
            sb.AppendLine(cellDef & "\cellx3600")
            sb.AppendLine(cellDef & "\cellx5000")
            sb.AppendLine(cellDef & "\cellx6300")
            sb.AppendLine(cellDef & "\cellx7600")
            sb.AppendLine(cellDef & "\cellx8800")
            sb.AppendLine(cellDef & "\cellx10300")
            'sb.AppendLine(cellDef & "\cellx12500")

            sb.AppendLine("\intbl " &
                dr("Folio No") & "\cell " &
                dr("Share Holder") & "\cell " &
                dr("Dividend Amount") & "\cell " &
                dr("Warrant No") & "\cell " &
                If(IsDBNull(dr("Dividend Date")), "", CDate(dr("Dividend Date")).ToString("dd-MM-yyyy")) & "\cell " &
                dr("FinYear Code") & "\cell " &
                dr("Divident Account No") & "\cell\row")
            'dr("Divident Account No") & "\cell " &

        Next

        Return sb.ToString()
    End Function

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
        Call frmCtrClear(Me)
    End Sub
End Class

