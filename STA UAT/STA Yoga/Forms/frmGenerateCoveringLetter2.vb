Imports Microsoft.Office.Interop.Word
Imports Microsoft.Office.Interop

Public Class frmGenerateCoveringLetter2
    Dim objWord As Application
    Dim objDoc As Microsoft.Office.Interop.Word.Document

    ' Dim tlb As Table
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=192.168.0.182;DataBase=sta;uid=production;pwd=gnsalive;port=3306")
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=" & DbIP & ";DataBase=" & Db & ";uid=" & DbUId & ";pwd=" & DbPwd & ";port=" & DbPort)
    Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=146.56.55.230;DataBase=sta;uid=root;pwd=Flexi@123;port=3306")
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
        End If

        shsql = ""
        shsql &= " select "
        shsql &= " b.inward_comp_no as 'Inward No',"
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
        shsql &= " left join sta_trn_toutward as g on g.inward_gid = b.inward_gid and g.delete_flag = 'N' "
        shsql &= " left join sta_trn_tdematpend as h on h.inward_gid = b.inward_gid and h.delete_flag = 'N' "
        shsql &= " where true "
        shsql &= lsCond
        shsql &= " and b.delete_flag = 'N' "
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
            dgv_covering.Columns("inward_status").Visible = False
            dgv_covering.Columns("folio_gid").Visible = False
            dgv_covering.Columns("tran_folio_gid").Visible = False
            dgv_covering.Columns("dematpend_reject_code").Visible = False
            dgv_covering.Columns("depository_code").Visible = False


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
            objWord = CreateObject("Word.Application")
            objWord.Visible = True
            objDoc = objWord.Documents.Add
            objDoc.Activate()
            objWord.Selection.Font.Name = "Tahoma"
            objWord.Selection.Font.Size = 9.5

            objWord.Selection.ParagraphFormat.LeftIndent = 0 ' InchesToPoints(0)
            objWord.Selection.ParagraphFormat.RightIndent = 0 ' InchesToPoints(0)
            objWord.Selection.ParagraphFormat.SpaceBefore = 0
            objWord.Selection.ParagraphFormat.SpaceBeforeAuto = False
            objWord.Selection.ParagraphFormat.SpaceAfter = 0
            objWord.Selection.ParagraphFormat.SpaceAfterAuto = False
            objWord.Selection.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle ' wdLineSpaceSingle
            objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft ' wdAlignParagraphLeft
            objWord.Selection.ParagraphFormat.WidowControl = True
            objWord.Selection.ParagraphFormat.KeepWithNext = False
            objWord.Selection.ParagraphFormat.KeepTogether = False
            objWord.Selection.ParagraphFormat.PageBreakBefore = False
            objWord.Selection.ParagraphFormat.NoLineNumber = False
            objWord.Selection.ParagraphFormat.Hyphenation = True
            objWord.Selection.ParagraphFormat.FirstLineIndent = 0 ' InchesToPoints(0)
            objWord.Selection.ParagraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText ' wdOutlineLevelBodyText
            objWord.Selection.ParagraphFormat.CharacterUnitLeftIndent = 0
            objWord.Selection.ParagraphFormat.CharacterUnitRightIndent = 0
            objWord.Selection.ParagraphFormat.CharacterUnitFirstLineIndent = 0
            objWord.Selection.ParagraphFormat.LineUnitBefore = 0
            objWord.Selection.ParagraphFormat.LineUnitAfter = 0
            objWord.Selection.ParagraphFormat.MirrorIndents = False
            objWord.Selection.ParagraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone ' wdTightNone

            Dim objPara As Microsoft.Office.Interop.Word.Paragraph
            'Dim objPara As Word.Paragraph = Nothing
            'objPara = objWord.Documents.Add()
            objPara = objDoc.Content.Paragraphs.Add
            'objPara = objDoc.Content.Paragraphs.Add()
            objPara.LineSpacing = 10.0
            Dim celsql, trsql, tr_sql, trr_sql, sql_rej, sql_st, Sql_dmt As String

            'field variable name
            Dim folio_gid_item As String
            Dim comp_name As String
            Dim comp_code As String
            Dim received_date As String
            Dim holder1_name As String
            Dim folio_addr1 As String
            Dim folio_addr2 As String
            Dim folio_addr3 As String
            Dim folio_city As String
            Dim folio_state As String
            Dim folio_pincode As String
            Dim folio_shares As String
            Dim tran_code, dematpend_gid As String
            Dim share_count As String
            Dim folio_no As String
            Dim to_folio_no As String
            Dim dp_id As String
            Dim depository_code As String = ""
            Dim inward_no As String
            Dim approved_date As String
            Dim inward_gid As String
            Dim objx_no As String
            Dim transfer_no As String
            Dim trantype_desc As String
            Dim new_addr1 As String  'address new
            Dim new_addr2 As String
            Dim new_addr3 As String
            Dim new_city As String
            Dim new_state As String
            Dim new_pincode As String
            Dim old_addr1 As String
            Dim old_addr2 As String
            Dim old_addr3 As String
            Dim old_city As String
            Dim old_state As String
            Dim old_pincode As String
            Dim adrs_inward As String
            Dim new_pan1 As String     'Pan new
            Dim new_pan2 As String
            Dim new_pan3 As String
            Dim old_pan1 As String
            Dim old_pan2 As String
            Dim old_pan3 As String
            Dim bank_beneficiary As String    'bank new
            Dim bank_name As String
            Dim bank_branch As String
            Dim bank_addr As String
            Dim bank_ifsc_code As String
            Dim bank_acc_type As String
            Dim bank_acc_no As String
            Dim old_bbeneficiary As String
            Dim old_bank_name As String
            Dim old_bank_branch As String
            Dim old_bank_addr As String
            Dim old_bank_ifsc As String
            Dim old_bank_acc As String
            Dim old_bank_acc_no As String
            Dim category_name As String
            Dim old_category, update_completed As String
            Dim nominee_reg_no As String = ""
            Dim nominee_name As String = ""
            Dim dfrom As String = ""
            Dim dto As String = ""
            Dim c_no As String = ""
            Dim dist_count As String = ""
            Dim Sno As String = ""
            Dim dfrom2 As String = ""
            Dim dto2 As String = ""
            Dim c_no2 As String = ""
            Dim Sno2 As String = ""
            Dim dist_count2 As String = ""
            Dim shareholder_pan_no, holdername, folioo, faddr1, faddr2, faddr3, fcity, fstate, fpincode, fshares As String  'second table
            Dim chklst_desc As String = ""
            Dim hold_release_date As String = ""
            Dim lockin_period_to As String = ""
            Dim reason_desc As String = ""
            Dim tran_cert_no As String = ""
            Dim newname As String = ""
            Dim oldname As String = ""
            Dim dematreject_code, dematreject_desc, drn_no, client_id As String

            Dim lnInwardId As Long
            lnInwardId = dgv_covering.Rows(e.RowIndex).Cells("inward_gid").Value
            Dim TranCode As String
            TranCode = dgv_covering.Rows(e.RowIndex).Cells("tran_code").Value
            Dim chk_lst As String
            chk_lst = dgv_covering.Rows(e.RowIndex).Cells("chklst_disc").Value
            Dim inward_status As String
            inward_status = dgv_covering.Rows(e.RowIndex).Cells("inward_status").Value
            Dim folio_gid As Long
            folio_gid = dgv_covering.Rows(e.RowIndex).Cells("folio_gid").Value
            Dim tran_folio_gid As Integer
            tran_folio_gid = dgv_covering.Rows(e.RowIndex).Cells("tran_folio_gid").Value
            Dim dep_code As String = ""
            dep_code = dgv_covering.Rows(e.RowIndex).Cells("depository_code").Value

            celsql = ""
            celsql &= " SELECT "
            celsql &= "case when g.depository_code = 'N' then 'NSDL' else 'CDSL' end as depository_code,g.*,"
            celsql &= "h.folio_addr1 as new_addr1,"
            celsql &= "h.folio_addr2 as new_addr2,"
            celsql &= "h.folio_addr3 as new_addr3,"
            celsql &= "h.folio_city as new_city,"
            celsql &= "h.folio_state as new_state,"
            celsql &= "h.folio_pincode as new_pincode,"
            celsql &= "h.inward_gid as adrs_inward,"
            celsql &= "h.holder1_pan_no as new_pan1,"
            celsql &= "h.holder2_pan_no as new_pan2,"
            celsql &= "h.holder3_pan_no as new_pan3,"
            celsql &= "i.folio_addr1 as old_addr1,"
            celsql &= "i.folio_addr2 as old_addr2,"
            celsql &= "i.folio_addr3 as old_addr3,"
            celsql &= "i.folio_city as old_city,"
            celsql &= "i.folio_state as old_state,"
            celsql &= "i.folio_pincode as old_pincode,"
            celsql &= "i.bank_beneficiary as old_bbeneficiary,"
            celsql &= "i.bank_name as old_bank_name,"
            celsql &= "i.bank_branch as old_bank_branch,"
            celsql &= "i.bank_addr as old_bank_addr,"
            celsql &= "i.bank_ifsc_code as old_bank_ifsc,"
            celsql &= "i.bank_acc_type as old_bank_acc,"
            celsql &= "i.bank_acc_no as old_bank_acc_no,"
            celsql &= "i.holder1_pan_no as old_pan1, i.holder2_pan_no as old_pan2, i.holder3_pan_no as old_pan3,j.category_name,k.category_name as old_category,"
            celsql &= "a.folio_gid,a.bank_beneficiary,a.bank_name,a.bank_branch,a.bank_branch_addr,a.bank_ifsc_code,a.bank_acc_type,a.bank_acc_no, c.comp_name, c.comp_code, b.received_date, a.holder1_name, a.folio_addr1, a.folio_addr2, a.folio_addr3, a.folio_city, a.folio_state, a.folio_pincode,"
            celsql &= "a.nominee_reg_no,a.nominee_name,b.update_completed,a.folio_shares,b.transfer_no,b.shareholder_name as oldname, b.approved_date, b.tran_code, b.share_count, b.folio_no, a.folio_no as to_folio_no,g.dp_id, g.depository_code, b.inward_comp_no as inward_no, b.inward_gid, b.objx_no,b.shareholder_pan_no, d.trantype_desc"
            celsql &= " FROM sta_trn_tfolio a "
            celsql &= "LEFT JOIN sta_trn_tinward b on a.folio_gid = b.folio_gid and b.delete_flag = 'N' "
            celsql &= "LEFT JOIN sta_mst_tcompany c ON a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            celsql &= "LEFT JOIN sta_mst_ttrantype d ON b.tran_code = d.trantype_code and d.delete_flag = 'N' "
            celsql &= "LEFT JOIN sta_trn_tdematpend g on g.inward_gid = b.inward_gid and g.delete_flag = 'N'"
            celsql &= "LEFT JOIN sta_trn_tfolioentry h on h.inward_gid = b.inward_gid and h.delete_flag = 'N'"
            celsql &= "LEFT JOIN sta_mst_tcategory j on j.category_gid = a.category_gid and j.delete_flag = 'N'"
            celsql &= "LEFT join sta_trn_tfolioentryold i on i.folioentry_gid = h.folioentry_gid and i.delete_flag = 'N' "
            celsql &= "LEFT JOIN sta_mst_tcategory k on k.category_gid = i.category_gid and k.delete_flag = 'N' where b.inward_gid = " & lnInwardId & ""
            ds = New DataSet
            da = New Odbc.OdbcDataAdapter(celsql, con)
            da.Fill(ds, celsql)

            trsql = "SELECT * from sta_trn_tfolio where folio_gid = '" & tran_folio_gid & "' and delete_flag = 'N'"
            dt = New DataSet
            da = New Odbc.OdbcDataAdapter(trsql, con)
            da.Fill(dt, trsql)

            tr_sql = "SELECT @s:=@s+1 Sno,a.*,e.*,f.* FROM sta_trn_tfolio a LEFT JOIN sta_trn_tcert e ON a.folio_gid = e.folio_gid and e.delete_flag = 'N' LEFT JOIN sta_trn_tcertdist f ON e.cert_gid = f.cert_gid and f.delete_flag = 'N' ,(SELECT @s:= 0) AS s where a.folio_gid=" & folio_gid & " and a.delete_flag = 'N'"
            dt_t = New DataSet
            da = New Odbc.OdbcDataAdapter(tr_sql, con)
            da.Fill(dt_t, tr_sql)


            trr_sql = " SELECT @s:=@s+1 Sno,a.*,b.*,c.*,d.* FROM sta_trn_tcertentry a "
            trr_sql &= " left join sta_trn_tcert b ON b.cert_gid = a.cert_gid and b.delete_flag = 'N' "
            trr_sql &= " left join sta_trn_tcertdist c ON c.cert_gid = b.cert_gid and c.delete_flag = 'N' "
            trr_sql &= " left join sta_trn_tinward d on d.inward_gid = a.inward_gid and d.delete_flag = 'N',(SELECT @s:= 0) AS s where a.inward_gid = '" & lnInwardId & "' and a.delete_flag = 'N'"
            dt_tt = New DataSet
            da = New Odbc.OdbcDataAdapter(trr_sql, con)
            da.Fill(dt_tt, trr_sql)

            ''chklist reason
            sql_rej = "SELECT chklst_desc FROM sta_mst_tchecklist where chklst_value & " & chk_lst & " > 0 and tran_code = '" & TranCode & "' and delete_flag = 'N'"
            dt_re = New DataSet
            da = New Odbc.OdbcDataAdapter(sql_rej, con)
            da.Fill(dt_re, sql_rej)


            ''release stoptransfer/loss of share
            sql_st = ""
            sql_st &= " select @s:=@s+1 Sno,a.*,b.*,c.* from sta_trn_tinward a"
            sql_st &= " left join sta_trn_tcert b on b.folio_gid = a.folio_gid and b.delete_flag = 'N'"
            sql_st &= " left join sta_mst_treason c on c.reason_gid = a.reason_gid and c.delete_flag = 'N',"
            sql_st &= " (SELECT @s:= 0) AS s where a.inward_gid = '" & lnInwardId & "' and a.delete_flag = 'N'"
            dt_st = New DataSet
            da = New Odbc.OdbcDataAdapter(sql_st, con)
            da.Fill(dt_st, sql_st)

            'demat rejection code
            Dim dematpend_reject_code As String
            Dim strArr() As String = Nothing
            dematpend_reject_code = dgv_covering.Rows(e.RowIndex).Cells("dematpend_reject_code").Value
            strArr = dematpend_reject_code.Split(",")
            Dim dtmer As New System.Data.DataTable()
            Dim dtddd As New System.Data.DataTable()
            For Each s As String In strArr

                Sql_dmt = "SELECT dematreject_code,dematreject_desc FROM sta_mst_tdematreject where dematreject_code  = '" & s & "' and depository_code = '" & dep_code & "' and delete_flag='N'"
                dmtt = New DataSet
                da = New Odbc.OdbcDataAdapter(Sql_dmt, con)
                da.Fill(dtddd)
                dtmer.Merge(dtddd)
                dtddd.Rows.Clear()
            Next


            For i As Integer = 0 To ds.Tables(celsql).Rows.Count() - 1 Step +1
                i = +i
                folio_gid_item = ds.Tables(celsql).Rows(i).Item("folio_gid").ToString
                comp_name = ds.Tables(celsql).Rows(i).Item("comp_name").ToString
                comp_code = ds.Tables(celsql).Rows(i).Item("comp_code").ToString
                received_date = ds.Tables(celsql).Rows(i).Item("received_date").ToString
                approved_date = ds.Tables(celsql).Rows(i).Item("approved_date").ToString
                holder1_name = ds.Tables(celsql).Rows(i).Item("holder1_name").ToString
                folio_addr1 = ds.Tables(celsql).Rows(i).Item("folio_addr1").ToString
                folio_addr2 = ds.Tables(celsql).Rows(i).Item("folio_addr2").ToString
                folio_addr3 = ds.Tables(celsql).Rows(i).Item("folio_addr3").ToString
                folio_city = ds.Tables(celsql).Rows(i).Item("folio_city").ToString
                folio_state = ds.Tables(celsql).Rows(i).Item("folio_state").ToString
                folio_pincode = ds.Tables(celsql).Rows(i).Item("folio_pincode").ToString
                folio_shares = ds.Tables(celsql).Rows(i).Item("folio_shares").ToString
                bank_beneficiary = ds.Tables(celsql).Rows(i).Item("bank_beneficiary").ToString
                bank_name = ds.Tables(celsql).Rows(i).Item("bank_name").ToString
                bank_branch = ds.Tables(celsql).Rows(i).Item("bank_branch").ToString
                bank_addr = ds.Tables(celsql).Rows(i).Item("bank_branch_addr").ToString
                bank_ifsc_code = ds.Tables(celsql).Rows(i).Item("bank_ifsc_code").ToString
                bank_acc_type = ds.Tables(celsql).Rows(i).Item("bank_acc_type").ToString
                bank_acc_no = ds.Tables(celsql).Rows(i).Item("bank_acc_no").ToString
                tran_code = ds.Tables(celsql).Rows(i).Item("tran_code").ToString
                share_count = ds.Tables(celsql).Rows(i).Item("share_count").ToString
                folio_no = ds.Tables(celsql).Rows(i).Item("folio_no").ToString
                to_folio_no = ds.Tables(celsql).Rows(i).Item("to_folio_no").ToString
                dp_id = ds.Tables(celsql).Rows(i).Item("dp_id").ToString
                depository_code = ds.Tables(celsql).Rows(i).Item("depository_code").ToString
                inward_no = ds.Tables(celsql).Rows(i).Item("inward_no").ToString
                shareholder_pan_no = ds.Tables(celsql).Rows(i).Item("shareholder_pan_no").ToString
                inward_gid = ds.Tables(celsql).Rows(i).Item("inward_gid").ToString
                objx_no = ds.Tables(celsql).Rows(i).Item("objx_no").ToString
                transfer_no = ds.Tables(celsql).Rows(i).Item("transfer_no").ToString
                trantype_desc = ds.Tables(celsql).Rows(i).Item("trantype_desc").ToString
                new_addr1 = ds.Tables(celsql).Rows(i).Item("new_addr1").ToString
                new_addr2 = ds.Tables(celsql).Rows(i).Item("new_addr2").ToString
                new_addr3 = ds.Tables(celsql).Rows(i).Item("new_addr3").ToString
                new_city = ds.Tables(celsql).Rows(i).Item("new_city").ToString
                new_state = ds.Tables(celsql).Rows(i).Item("new_state").ToString
                new_pincode = ds.Tables(celsql).Rows(i).Item("new_pincode").ToString
                old_addr1 = ds.Tables(celsql).Rows(i).Item("old_addr1").ToString
                old_addr2 = ds.Tables(celsql).Rows(i).Item("old_addr2").ToString
                old_addr3 = ds.Tables(celsql).Rows(i).Item("old_addr3").ToString
                old_city = ds.Tables(celsql).Rows(i).Item("old_city").ToString
                old_state = ds.Tables(celsql).Rows(i).Item("old_state").ToString
                old_pincode = ds.Tables(celsql).Rows(i).Item("old_pincode").ToString
                new_pan1 = ds.Tables(celsql).Rows(i).Item("new_pan1").ToString
                new_pan2 = ds.Tables(celsql).Rows(i).Item("new_pan2").ToString
                new_pan3 = ds.Tables(celsql).Rows(i).Item("new_pan3").ToString
                old_pan1 = ds.Tables(celsql).Rows(i).Item("old_pan1").ToString
                old_pan2 = ds.Tables(celsql).Rows(i).Item("old_pan2").ToString
                old_pan3 = ds.Tables(celsql).Rows(i).Item("old_pan3").ToString
                adrs_inward = ds.Tables(celsql).Rows(i).Item("adrs_inward").ToString
                old_bbeneficiary = ds.Tables(celsql).Rows(i).Item("old_bbeneficiary").ToString
                old_bank_name = ds.Tables(celsql).Rows(i).Item("old_bank_name").ToString
                old_bank_branch = ds.Tables(celsql).Rows(i).Item("old_bank_branch").ToString
                old_bank_addr = ds.Tables(celsql).Rows(i).Item("old_bank_addr").ToString
                old_bank_ifsc = ds.Tables(celsql).Rows(i).Item("old_bank_ifsc").ToString
                old_bank_acc = ds.Tables(celsql).Rows(i).Item("old_bank_acc").ToString
                old_bank_acc_no = ds.Tables(celsql).Rows(i).Item("old_bank_acc_no").ToString
                category_name = ds.Tables(celsql).Rows(i).Item("category_name").ToString
                old_category = ds.Tables(celsql).Rows(i).Item("old_category").ToString
                nominee_reg_no = ds.Tables(celsql).Rows(i).Item("nominee_reg_no").ToString
                nominee_name = ds.Tables(celsql).Rows(i).Item("nominee_name").ToString
                dematpend_gid = ds.Tables(celsql).Rows(i).Item("dematpend_gid").ToString
                drn_no = ds.Tables(celsql).Rows(i).Item("drn_no").ToString
                client_id = ds.Tables(celsql).Rows(i).Item("client_id").ToString
                newname = ds.Tables(celsql).Rows(i).Item("holder1_name").ToString
                oldname = ds.Tables(celsql).Rows(i).Item("oldname").ToString
                update_completed = ds.Tables(celsql).Rows(i).Item("update_completed").ToString

                If dt.Tables.Count > 0 AndAlso dt.Tables(trsql).Rows.Count > 0 Then
                    holdername = dt.Tables(trsql).Rows(i).Item("holder1_name").ToString
                    folioo = dt.Tables(trsql).Rows(i).Item("folio_no").ToString
                    faddr1 = dt.Tables(trsql).Rows(i).Item("folio_addr1").ToString
                    faddr2 = dt.Tables(trsql).Rows(i).Item("folio_addr2").ToString
                    faddr3 = dt.Tables(trsql).Rows(i).Item("folio_addr3").ToString
                    fcity = dt.Tables(trsql).Rows(i).Item("folio_city").ToString
                    fstate = dt.Tables(trsql).Rows(i).Item("folio_state").ToString
                    fpincode = dt.Tables(trsql).Rows(i).Item("folio_pincode").ToString
                    fshares = dt.Tables(trsql).Rows(i).Item("folio_shares").ToString
                End If

                If (TranCode = "CA" And update_completed = "N") Then

                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True


                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holder1_name & ",")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("            With reference to your letter regarding Change of Address, Please note that your signature getting mismatch with our record.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("            Hence kindly get and send us a Notary or Bank Attestation for your signature with self attested address proof to enable us to update your address in master. ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        'Add a new paragraph that the table will replace
                        pg = .Paragraphs.Add(.Paragraphs(17).Range)
                        tbl = .Tables.Add(pg.Range, 1, 2)
                    End With

                    tbl.Columns(1).Cells(1).Range.Text = "New Address :"
                    tbl.Columns(2).Cells(1).Range.Text = "Old Address :"

                    For k As Integer = 0 To 6 Step +1
                        k = +k
                        tbl.Rows.Add()
                        If (k = 0) Then
                            tbl.Cell(k + 2, 1).Range.Text = holder1_name
                            tbl.Cell(k + 2, 2).Range.Text = holder1_name
                        End If
                        If (k = 1) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_addr1
                            tbl.Cell(k + 2, 2).Range.Text = old_addr1
                        End If
                        If (k = 2) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_addr2
                            tbl.Cell(k + 2, 2).Range.Text = old_addr2
                        End If
                        If (k = 3) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_addr3
                            tbl.Cell(k + 2, 2).Range.Text = old_addr3
                        End If
                        If (k = 4) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_city
                            tbl.Cell(k + 2, 2).Range.Text = old_city
                        End If
                        If (k = 5) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_state
                            tbl.Cell(k + 2, 2).Range.Text = old_state
                        End If
                        If (k = 6) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_pincode
                            tbl.Cell(k + 2, 2).Range.Text = old_pincode
                        End If
                    Next
                    tbl.Borders.Enable = False
                    tbl.Rows.Item(1).Range.Font.Bold = True
                    objWord.Selection.TypeParagraph()

                    'objWord.Selection.TypeText("New Address : " & vbTab & vbTab & vbTab & vbTab & "                                           Old Address : ")
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(holder1_name & vbTab & vbTab & vbTab & "                                                       " & holder1_name)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_addr1 & vbTab & vbTab & vbTab & "                                                        " & old_addr1)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_addr2 & vbTab & vbTab & vbTab & vbTab & "                                                " & old_addr2)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_addr3 & vbTab & vbTab & vbTab & vbTab & "                                                " & old_addr3)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_city & vbTab & vbTab & vbTab & vbTab & "                                                 " & old_city)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_state & vbTab & vbTab & vbTab & vbTab & "                                                " & old_state)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_pincode & vbTab & vbTab & vbTab & vbTab & vbTab & "                                      " & old_pincode)
                    'objWord.Selection.TypeParagraph()


                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()

                ElseIf (TranCode = "CA" And update_completed = "Y") Then
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & "  GNSA INFOTECH PRIVATE LIMITED                          ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  NELSON CHAMBERS                 ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  4TH FLOOR,F-BLOCK,      ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  NO 115, NELSON MANICKAM ROAD, ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & " AMINTHAKARAI, CHENNAI-600029,       ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  044-42962025 / 044-42962050. ")
                    'objWord.Selection.TypeText(" ")
                    'objWord.Selection.TypeText("___________________________________________________________________________")
                    'objWord.Selection.TypeText(" ")
                    'objWord.Selection.TypeText("Unit: " & comp_name)
                    'objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holder1_name & ",")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("            With reference to your letter regarding Change of Address, Please note we have updated your new below mentioned address in our master. We will forward all our future correspondence to your new address.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()

                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        'Add a new paragraph that the table will replace
                        pg = .Paragraphs.Add(.Paragraphs(16).Range)
                        tbl = .Tables.Add(pg.Range, 1, 2)
                    End With

                    tbl.Columns(1).Cells(1).Range.Text = "New Address :"
                    tbl.Columns(2).Cells(1).Range.Text = "Old Address :"

                    For k As Integer = 0 To 6 Step +1
                        k = +k
                        tbl.Rows.Add()
                        If (k = 0) Then
                            tbl.Cell(k + 2, 1).Range.Text = holder1_name
                            tbl.Cell(k + 2, 2).Range.Text = holder1_name
                        End If
                        If (k = 1) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_addr1
                            tbl.Cell(k + 2, 2).Range.Text = old_addr1
                        End If
                        If (k = 2) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_addr2
                            tbl.Cell(k + 2, 2).Range.Text = old_addr2
                        End If
                        If (k = 3) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_addr3
                            tbl.Cell(k + 2, 2).Range.Text = old_addr3
                        End If
                        If (k = 4) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_city
                            tbl.Cell(k + 2, 2).Range.Text = old_city
                        End If
                        If (k = 5) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_state
                            tbl.Cell(k + 2, 2).Range.Text = old_state
                        End If
                        If (k = 6) Then
                            tbl.Cell(k + 2, 1).Range.Text = folio_pincode
                            tbl.Cell(k + 2, 2).Range.Text = old_pincode
                        End If
                    Next
                    tbl.Borders.Enable = False
                    tbl.Rows.Item(1).Range.Font.Bold = True
                    objWord.Selection.TypeParagraph()

                    'objWord.Selection.TypeText("New Address : " & vbTab & vbTab & vbTab & vbTab & "                                           Old Address : ")
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(holder1_name & vbTab & vbTab & vbTab & "                               " & holder1_name)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_addr1 & vbTab & vbTab & vbTab & "                               " & old_addr1)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_addr2 & vbTab & vbTab & vbTab & vbTab & "                                           " & old_addr2)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_addr3 & vbTab & vbTab & vbTab & vbTab & "                                           " & old_addr3)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_city & vbTab & vbTab & vbTab & vbTab & "                                             " & old_city)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_state & vbTab & vbTab & vbTab & vbTab & "                                              " & old_state)
                    'objWord.Selection.TypeParagraph()
                    'objWord.Selection.TypeText(folio_pincode & vbTab & vbTab & vbTab & vbTab & vbTab & "                                           " & old_pincode)
                    'objWord.Selection.TypeParagraph()

                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()
                End If


                If (TranCode = "CP" OrElse TranCode = "CB" OrElse TranCode = "CC" OrElse TranCode = "CR") Then

                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(holder1_name)
                    objWord.Selection.TypeParagraph()
                    If (old_addr1 = "") Then
                        objWord.Selection.TypeText(folio_addr1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr3)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_city)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_state)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_pincode)
                        objWord.Selection.TypeParagraph()

                    Else
                        objWord.Selection.TypeText(old_addr1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(old_addr2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(old_addr3)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(old_city)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(old_state)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(old_pincode)
                        objWord.Selection.TypeParagraph()
                    End If

                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holder1_name & ",")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & "This is with reference to your letter regarding the above. Please note that we have updated the below change in our records.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    If (tran_code = "CA") Then
                        objWord.Selection.TypeText("New Address : " & vbTab & vbTab & vbTab & "                                     Old Address : ")
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(holder1_name & vbTab & vbTab & vbTab & "                                     " & holder1_name)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr1 & vbTab & vbTab & vbTab & "                                      " & old_addr1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr2 & vbTab & vbTab & vbTab & "                                      " & old_addr2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr3 & vbTab & vbTab & vbTab & "                                      " & old_addr3)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_city & vbTab & vbTab & vbTab & "                                       " & old_city)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_state & vbTab & vbTab & vbTab & "                                      " & old_state)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_pincode & vbTab & vbTab & vbTab & "                                    " & old_pincode)
                        objWord.Selection.TypeParagraph()



                    ElseIf (tran_code = "CP") Then
                        objWord.Selection.TypeText("New Pan card No : " & "                               " & "Old Pan card No : ")
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(new_pan1 & "                                           " & old_pan1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(new_pan2 & "                                           " & old_pan2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(new_pan3 & "                                           " & old_pan3)
                        objWord.Selection.TypeParagraph()
                    ElseIf (tran_code = "CB") Then
                        Dim tbl As Table
                        Dim pg As Microsoft.Office.Interop.Word.Paragraph
                        With objDoc
                            'Add a new paragraph that the table will replace
                            pg = .Paragraphs.Add(.Paragraphs(23).Range)
                            tbl = .Tables.Add(pg.Range, 1, 2)
                        End With

                        tbl.Columns(1).Cells(1).Range.Text = "New Bank details :"
                        tbl.Columns(2).Cells(1).Range.Text = "Old Bank details :"

                        For k As Integer = 0 To 6 Step +1
                            k = +k
                            tbl.Rows.Add()
                            If (k = 0) Then
                                tbl.Cell(k + 2, 1).Range.Text = "Name : " + bank_beneficiary
                                tbl.Cell(k + 2, 2).Range.Text = "Name : " + old_bbeneficiary
                            End If
                            If (k = 1) Then
                                tbl.Cell(k + 2, 1).Range.Text = "Bank : " + bank_name
                                tbl.Cell(k + 2, 2).Range.Text = "Bank : " + old_bank_name
                            End If
                            If (k = 2) Then
                                tbl.Cell(k + 2, 1).Range.Text = "Branch : " + bank_branch
                                tbl.Cell(k + 2, 2).Range.Text = "Branch : " + old_bank_branch
                            End If
                            If (k = 3) Then
                                tbl.Cell(k + 2, 1).Range.Text = "Address : " + folio_addr3
                                tbl.Cell(k + 2, 2).Range.Text = "Address : " + old_addr3
                            End If
                            If (k = 4) Then
                                tbl.Cell(k + 2, 1).Range.Text = "Bank Addr : " + bank_addr
                                tbl.Cell(k + 2, 2).Range.Text = "Bank Addr : " + old_bank_addr
                            End If
                            If (k = 5) Then
                                tbl.Cell(k + 2, 1).Range.Text = "IFSC : " + bank_ifsc_code
                                tbl.Cell(k + 2, 2).Range.Text = "IFSC : " + old_bank_ifsc
                            End If
                            If (k = 6) Then
                                tbl.Cell(k + 2, 1).Range.Text = "Bank Acc No : " + bank_acc_no
                                tbl.Cell(k + 2, 2).Range.Text = "Bank Acc No : " + old_bank_acc_no
                            End If
                        Next
                        tbl.Borders.Enable = False
                        tbl.Rows.Item(1).Range.Font.Bold = True
                        objWord.Selection.TypeParagraph()

                        'objWord.Selection.TypeText("New Bank details : " & "                            " & "Old Bank details : ")
                        'objWord.Selection.TypeParagraph()
                        'objWord.Selection.TypeText(bank_beneficiary & "                                 " & old_bbeneficiary)
                        'objWord.Selection.TypeParagraph()
                        'objWord.Selection.TypeText(bank_name & "                                        " & old_bank_name)
                        'objWord.Selection.TypeParagraph()
                        'objWord.Selection.TypeText(bank_branch & "                                      " & old_bank_branch)
                        'objWord.Selection.TypeParagraph()
                        'objWord.Selection.TypeText(bank_addr & "                                        " & old_bank_addr)
                        'objWord.Selection.TypeParagraph()
                        'objWord.Selection.TypeText(bank_ifsc_code & "                                   " & old_bank_ifsc)
                        'objWord.Selection.TypeParagraph()
                        'objWord.Selection.TypeText("Acc No: " & bank_acc_no & "                         " & old_bank_acc_no)
                        objWord.Selection.TypeParagraph()
                    ElseIf (tran_code = "CC") Then
                        objWord.Selection.TypeText("New Category details : " & "                     " & "Old Category details : ")
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(category_name & "                                 " & old_category)
                        objWord.Selection.TypeParagraph()
                        'ElseIf (tran_code = "NC") Then
                        '    objWord.Selection.TypeText("New name: " & "                     " & "Old Name : ")
                        '    objWord.Selection.TypeParagraph()
                        '    objWord.Selection.TypeText(newname & "                     " & oldname)
                        '    objWord.Selection.TypeParagraph()
                    End If
                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()
                End If
                'Registration of Nomination
                If (TranCode = "NU") Then
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(holder1_name)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr1)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr2)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr3)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_city)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_state)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_pincode)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holder1_name & ",")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & "This is with reference to your letter regarding registration of Nomination. We append below the details of Registration and request you to quote the registration number in all your future correspondence.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    'table
                    Dim tbl As Table 'table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        pg = .Paragraphs.Add(.Paragraphs(16).Range)
                        tbl = .Tables.Add(pg.Range, 2, 5)
                    End With
                    tbl.Columns(1).Cells(1).Range.Text = "Registration No."
                    tbl.Columns(2).Cells(1).Range.Text = "Date of Registration"
                    tbl.Columns(3).Cells(1).Range.Text = "Folio No."
                    tbl.Columns(4).Cells(1).Range.Text = "Name of the Nominee"
                    tbl.Columns(5).Cells(1).Range.Text = "Nominee PAN No."
                    'Data Row
                    tbl.Columns(1).Cells(2).Range.Text = nominee_reg_no
                    tbl.Columns(2).Cells(2).Range.Text = If(String.IsNullOrEmpty(received_date), "", CDate(received_date))
                    tbl.Columns(3).Cells(2).Range.Text = folio_no
                    tbl.Columns(4).Cells(2).Range.Text = nominee_name
                    tbl.Columns(5).Cells(2).Range.Text = shareholder_pan_no
                    tbl.Borders.Enable = True
                    tbl.Rows.Item(1).Range.Font.Bold = True
                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()
                End If

                ''tm tr rejection
                If ((TranCode = "TR" AndAlso inward_status = "64") OrElse (TranCode = "TR" AndAlso inward_status = "8") OrElse (TranCode = "TM" AndAlso inward_status = "64") OrElse (TranCode = "TM" AndAlso inward_status = "8") OrElse (TranCode = "TP" AndAlso inward_status = "64") OrElse (TranCode = "TP" AndAlso inward_status = "8") OrElse (TranCode = "FC" AndAlso inward_status = "64") OrElse (TranCode = "FC" AndAlso inward_status = "8") OrElse (TranCode = "NC" AndAlso inward_status = "64") OrElse (TranCode = "NC" AndAlso inward_status = "8") OrElse (TranCode = "CO" AndAlso inward_status = "64") OrElse (TranCode = "SP" AndAlso inward_status = "64")) Then
                    'ElseIf (TranCode = "TR" AndAlso inward_status = "64") OrElse (TranCode = "TR" AndAlso inward_status = "8") OrElse (TranCode = "TR" AndAlso inward_status = "4") OrElse (update_completed = "N") Then
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    If Not (tran_folio_gid = "0") Then
                        objWord.Selection.TypeText(holdername)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(faddr1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(faddr2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(faddr3)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(fcity)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(fstate)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(fpincode)
                    Else
                        objWord.Selection.TypeText(holder1_name)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr3)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_city)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_state)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_pincode)
                        objWord.Selection.TypeParagraph()
                    End If
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc & " Rejection")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holdername & ",")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & "With reference to the above, we enclose herewith the following Share Certificates(s) duly Rejected")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()

                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        'Add a new paragraph that the table will replace
                        pg = .Paragraphs.Add(.Paragraphs(16).Range)
                        tbl = .Tables.Add(pg.Range, 1, 7)
                    End With

                    tbl.Columns(1).Cells(1).Range.Text = "Sl. No."
                    tbl.Columns(2).Cells(1).Range.Text = "Tr No."
                    tbl.Columns(3).Cells(1).Range.Text = "   Tr Date"
                    tbl.Columns(4).Cells(1).Range.Text = "   Cert.No."
                    tbl.Columns(5).Cells(1).Range.Text = "Dist. From"
                    tbl.Columns(6).Cells(1).Range.Text = "Dist. To"
                    tbl.Columns(7).Cells(1).Range.Text = "Shares"


                    If dt_t.Tables.Count > 0 AndAlso dt_t.Tables(tr_sql).Rows.Count > 0 Then
                        For j As Integer = 0 To dt_t.Tables(tr_sql).Rows.Count() - 1 Step +1
                            j = +j
                            dfrom = dt_t.Tables(tr_sql).Rows(j).Item("dist_from").ToString
                            dto = dt_t.Tables(tr_sql).Rows(j).Item("dist_to").ToString
                            c_no = dt_t.Tables(tr_sql).Rows(j).Item("cert_no").ToString
                            dist_count = dt_t.Tables(tr_sql).Rows(j).Item("dist_count").ToString
                            Sno = dt_t.Tables(tr_sql).Rows(j).Item("Sno").ToString

                            If Not c_no = "" Then
                                objWord.Selection.TypeText(Sno & vbTab & vbTab & transfer_no & vbTab & vbTab & If(String.IsNullOrEmpty(approved_date), "              ", CDate(approved_date)) & vbTab & c_no & vbTab & "   " & dfrom & vbTab & "  " & dto & vbTab & " " & dist_count & vbTab)
                                objWord.Selection.TypeParagraph()

                            ElseIf dt_tt.Tables.Count > 0 AndAlso dt_tt.Tables(trr_sql).Rows.Count > 0 Then
                                For k As Integer = 0 To dt_tt.Tables(trr_sql).Rows.Count() - 1 Step +1
                                    k = +k

                                    dfrom2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_from").ToString
                                    dto2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_to").ToString
                                    c_no2 = dt_tt.Tables(trr_sql).Rows(k).Item("cert_no").ToString
                                    dist_count2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_count").ToString
                                    Sno2 = dt_tt.Tables(trr_sql).Rows(j).Item("Sno").ToString

                                    objWord.Selection.TypeText(Sno2 & vbTab & vbTab & transfer_no & vbTab & vbTab & If(String.IsNullOrEmpty(approved_date), "              ", CDate(approved_date)) & vbTab & c_no2 & vbTab & "   " & dfrom2 & vbTab & "  " & dto2 & vbTab & " " & dist_count2 & vbTab)
                                    objWord.Selection.TypeParagraph()
                                Next
                            End If
                        Next
                    End If
                    tbl.Borders.Enable = False
                    tbl.Rows.Item(1).Range.Font.Bold = True
                    If dt_re.Tables.Count > 0 AndAlso dt_re.Tables(sql_rej).Rows.Count > 0 Then
                        objWord.Selection.TypeText(" Reason for Rejection & Rectification ")
                        objWord.Selection.TypeParagraph()
                        For a As Integer = 0 To dt_re.Tables(sql_rej).Rows.Count() - 1 Step +1
                            a = +a
                            chklst_desc = dt_re.Tables(sql_rej).Rows(a).Item("chklst_desc").ToString
                            objWord.Selection.TypeText("      *" & If(String.IsNullOrEmpty(chklst_desc), "", chklst_desc))
                            objWord.Selection.TypeParagraph()
                        Next
                    End If

                    objWord.Selection.TypeText("Kindly acknowledge receipt of the same.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" Encl : as above")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()

                ElseIf (TranCode = "TR" OrElse TranCode = "TM" OrElse TranCode = "TP" OrElse TranCode = "FC" OrElse TranCode = "NC") Then
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    If Not (tran_folio_gid = "0") Then
                        objWord.Selection.TypeText("To,")
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(vbNewLine)
                        objWord.Selection.TypeText(holdername)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(faddr1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(faddr2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(faddr3)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(fcity)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(fstate)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(fpincode)
                    Else
                        objWord.Selection.TypeText("To,")
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(vbNewLine)
                        objWord.Selection.TypeText(holder1_name)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr1)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr2)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_addr3)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_city)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_state)
                        objWord.Selection.TypeParagraph()
                        objWord.Selection.TypeText(folio_pincode)
                        objWord.Selection.TypeParagraph()
                    End If
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & to_folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holdername & ",")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & "With reference to the above, we enclose herewith the following Share Certificates(s) duly transferred in your favour")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()

                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        'Add a new paragraph that the table will replace
                        pg = .Paragraphs.Add(.Paragraphs(24).Range)
                        tbl = .Tables.Add(pg.Range, 1, 7)
                    End With


                    tbl.Columns(1).Cells(1).Range.Text = "Sl. No."
                    tbl.Columns(2).Cells(1).Range.Text = "Tr No."
                    tbl.Columns(3).Cells(1).Range.Text = "Tr Date"
                    tbl.Columns(4).Cells(1).Range.Text = "Cert.No."
                    tbl.Columns(5).Cells(1).Range.Text = "Dist. From"
                    tbl.Columns(6).Cells(1).Range.Text = "Dist. To"
                    tbl.Columns(7).Cells(1).Range.Text = "Shares"

                    If TranCode = "TM" Then
                        If dt_tt.Tables.Count > 0 AndAlso dt_tt.Tables(trr_sql).Rows.Count > 0 Then
                            For j As Integer = 0 To dt_tt.Tables(trr_sql).Rows.Count() - 1 Step +1
                                j = +j
                                dfrom = dt_tt.Tables(trr_sql).Rows(j).Item("dist_from").ToString
                                dto = dt_tt.Tables(trr_sql).Rows(j).Item("dist_to").ToString
                                c_no = dt_tt.Tables(trr_sql).Rows(j).Item("cert_no").ToString
                                dist_count = dt_tt.Tables(trr_sql).Rows(j).Item("dist_count").ToString
                                Sno = dt_tt.Tables(trr_sql).Rows(j).Item("Sno").ToString
                                If Not c_no = "" Then

                                    tbl.Rows.Add()
                                    tbl.Cell(j + 2, 1).Range.Text = Sno
                                    tbl.Cell(j + 2, 2).Range.Text = transfer_no
                                    tbl.Cell(j + 2, 3).Range.Text = If(String.IsNullOrEmpty(approved_date), "                   ", CDate(approved_date))
                                    tbl.Cell(j + 2, 4).Range.Text = c_no
                                    tbl.Cell(j + 2, 5).Range.Text = dfrom
                                    tbl.Cell(j + 2, 6).Range.Text = dto
                                    tbl.Cell(j + 2, 7).Range.Text = dist_count

                                    'objWord.Selection.TypeText(Sno & vbTab & vbTab & transfer_no & vbTab & vbTab & If(String.IsNullOrEmpty(approved_date), "                   ", CDate(approved_date)) & vbTab & c_no & vbTab & "   " & dfrom & vbTab & "  " & dto & vbTab & " " & dist_count & vbTab)
                                    objWord.Selection.TypeParagraph()

                                ElseIf dt_t.Tables.Count > 0 AndAlso dt_t.Tables(tr_sql).Rows.Count > 0 Then
                                    For k As Integer = 0 To dt_t.Tables(tr_sql).Rows.Count() - 1 Step +1
                                        k = +k

                                        'dfrom2 = dt_t.Tables(tr_sql).Rows(k).Item("dist_from").ToString
                                        'dto2 = dt_t.Tables(tr_sql).Rows(k).Item("dist_to").ToString
                                        'c_no2 = dt_t.Tables(tr_sql).Rows(k).Item("cert_no").ToString
                                        'dist_count2 = dt_t.Tables(tr_sql).Rows(k).Item("dist_count").ToString
                                        'Sno2 = dt_t.Tables(tr_sql).Rows(k).Item("Sno").ToString

                                        'objWord.Selection.TypeText(Sno2 & vbTab & vbTab & transfer_no & vbTab & vbTab & If(String.IsNullOrEmpty(approved_date), "                   ", CDate(approved_date)) & vbTab & c_no2 & vbTab & "   " & dfrom2 & vbTab & "  " & dto2 & vbTab & " " & dist_count2 & vbTab)

                                        tbl.Rows.Add()
                                        tbl.Cell(k + 2, 1).Range.Text = Sno2
                                        tbl.Cell(k + 2, 2).Range.Text = transfer_no
                                        tbl.Cell(k + 2, 3).Range.Text = If(String.IsNullOrEmpty(approved_date), "                   ", CDate(approved_date))
                                        tbl.Cell(k + 2, 4).Range.Text = c_no2
                                        tbl.Cell(k + 2, 5).Range.Text = dfrom2
                                        tbl.Cell(k + 2, 6).Range.Text = dto2
                                        tbl.Cell(k + 2, 7).Range.Text = dist_count2

                                        objWord.Selection.TypeParagraph()
                                    Next
                                End If
                            Next
                        End If

                    Else

                        If dt_t.Tables.Count > 0 AndAlso dt_t.Tables(tr_sql).Rows.Count > 0 Then
                            For j As Integer = 0 To dt_t.Tables(tr_sql).Rows.Count() - 1 Step +1
                                j = +j
                                dfrom = dt_t.Tables(tr_sql).Rows(j).Item("dist_from").ToString
                                dto = dt_t.Tables(tr_sql).Rows(j).Item("dist_to").ToString
                                c_no = dt_t.Tables(tr_sql).Rows(j).Item("cert_no").ToString
                                dist_count = dt_t.Tables(tr_sql).Rows(j).Item("dist_count").ToString
                                Sno = dt_t.Tables(tr_sql).Rows(j).Item("Sno").ToString
                                If Not c_no = "" Then
                                    objWord.Selection.TypeText(Sno & vbTab & vbTab & transfer_no & vbTab & vbTab & If(String.IsNullOrEmpty(approved_date), "                   ", CDate(approved_date)) & vbTab & c_no & vbTab & "   " & dfrom & vbTab & "  " & dto & vbTab & " " & dist_count & vbTab)
                                    objWord.Selection.TypeParagraph()

                                ElseIf dt_tt.Tables.Count > 0 AndAlso dt_tt.Tables(trr_sql).Rows.Count > 0 Then
                                    For k As Integer = 0 To dt_tt.Tables(trr_sql).Rows.Count() - 1 Step +1
                                        k = +k
                                        dfrom2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_from").ToString
                                        dto2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_to").ToString
                                        c_no2 = dt_tt.Tables(trr_sql).Rows(k).Item("cert_no").ToString
                                        dist_count2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_count").ToString
                                        Sno2 = dt_tt.Tables(trr_sql).Rows(k).Item("Sno").ToString
                                        objWord.Selection.TypeText(Sno2 & vbTab & vbTab & transfer_no & vbTab & vbTab & If(String.IsNullOrEmpty(approved_date), "                   ", CDate(approved_date)) & vbTab & c_no2 & vbTab & "   " & dfrom2 & vbTab & "  " & dto2 & vbTab & " " & dist_count2 & vbTab)
                                        objWord.Selection.TypeParagraph()
                                    Next
                                End If
                            Next
                        End If
                    End If




                    tbl.Borders.Enable = False
                    tbl.Rows.Item(1).Range.Font.Bold = True
                    objWord.Selection.TypeText("Kindly acknowledge receipt of the same.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)

                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Encl : as above")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()
                End If
                'stop transfer/loss of share
                If (TranCode = "RT" OrElse TranCode = "RL") Then
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref : \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(holder1_name)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr1)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr2)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr3)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_city)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_state)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_pincode)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holder1_name & ",")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & "This is with reference to your letter regarding the above. Please note that we have updated the " & trantype_desc & " of the following certificates in our records")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("")
                    objWord.Selection.TypeParagraph()
                    'table
                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        'Add a new paragraph that the table will replace
                        pg = .Paragraphs.Add(.Paragraphs(15).Range)
                        'Add a table in place of the new paragraph
                        tbl = .Tables.Add(pg.Range, 1, 4)
                    End With
                    tbl.Columns(1).Cells(1).Range.Text = "Sl. No."
                    tbl.Columns(2).Cells(1).Range.Text = "Cert. No."
                    tbl.Columns(3).Cells(1).Range.Text = "Release Date"
                    tbl.Columns(4).Cells(1).Range.Text = "Reason for Release"

                    If dt_st.Tables.Count > 0 AndAlso dt_st.Tables(sql_st).Rows.Count > 0 Then
                        For j As Integer = 0 To dt_st.Tables(sql_st).Rows.Count() - 1 Step +1
                            j = +j

                            c_no = dt_st.Tables(sql_st).Rows(j).Item("cert_no").ToString
                            Sno = dt_st.Tables(sql_st).Rows(j).Item("Sno").ToString
                            hold_release_date = dt_st.Tables(sql_st).Rows(j).Item("hold_release_date").ToString
                            received_date = dt_st.Tables(sql_st).Rows(j).Item("received_date").ToString
                            lockin_period_to = dt_st.Tables(sql_st).Rows(j).Item("lockin_period_to").ToString
                            reason_desc = dt_st.Tables(sql_st).Rows(j).Item("reason_desc").ToString

                            If Not hold_release_date = "" Then
                                objWord.Selection.TypeText(Sno & vbTab & vbTab & vbTab & c_no & vbTab & vbTab & vbTab & If(String.IsNullOrEmpty(hold_release_date), "                   ", CDate(hold_release_date)) & vbTab & vbTab & reason_desc)
                                objWord.Selection.TypeParagraph()
                            Else
                                objWord.Selection.TypeText(Sno & vbTab & vbTab & vbTab & c_no & vbTab & vbTab & vbTab & If(String.IsNullOrEmpty(received_date), "                   ", CDate(received_date)) & vbTab & vbTab & reason_desc)
                                objWord.Selection.TypeParagraph()
                            End If
                        Next
                    End If
                    tbl.Borders.Enable = False
                    tbl.Rows.Item(1).Range.Font.Bold = True

                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()
                End If
                If (TranCode = "ST" AndAlso inward_status = "64") OrElse (TranCode = "LS" AndAlso inward_status = "64") OrElse (TranCode = "LS" AndAlso chklst_desc = "0") Then  'rejection

                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(holder1_name)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr1)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr2)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr3)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_city)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_state)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_pincode)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holder1_name & ",")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & "This is with reference to your letter regarding " & trantype_desc & " . Please note that we cannot hold a valid transfer of the said shares unless the following is complied with respect to the lost shares.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("On receipt of the above, we shall proceed further.")
                    objWord.Selection.TypeParagraph()
                    'table
                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        'Add a new paragraph that the table will replace
                        pg = .Paragraphs.Add(.Paragraphs(16).Range)
                        'Add a table in place of the new paragraph
                        tbl = .Tables.Add(pg.Range, 6, 2)
                    End With
                    tbl.Columns(1).Cells(1).Range.Text = "Sl. No."
                    tbl.Columns(2).Cells(1).Range.Text = "Document Type"
                    'Data Row
                    tbl.Columns(1).Cells(2).Range.Text = "1"
                    tbl.Columns(2).Cells(2).Range.Text = "Request Letter with Folio, Company Name, Certificate No and distinctive Nos."
                    tbl.Columns(1).Cells(3).Range.Text = "2"
                    tbl.Columns(2).Cells(3).Range.Text = "Original FIR  / FIR copy duly attested by notary public"
                    tbl.Columns(1).Cells(4).Range.Text = "3"
                    tbl.Columns(2).Cells(4).Range.Text = "PAN copy of the shareholder"
                    tbl.Columns(1).Cells(5).Range.Text = "4"
                    tbl.Columns(2).Cells(5).Range.Text = "Original Transfer Deed, if applicable"
                    tbl.Columns(1).Cells(6).Range.Text = "5"
                    tbl.Columns(2).Cells(6).Range.Text = "Broker Contract Note, if applicable"

                    tbl.Borders.Enable = True
                    tbl.Rows.Item(1).Range.Font.Bold = True
                    'tbl.Rows.HorizontalPosition = 150 'In points
                    'tbl.Rows.VerticalPosition = 200
                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()

                ElseIf (TranCode = "SP" OrElse TranCode = "LS" OrElse TranCode = "CO" OrElse TranCode = "ST") Then
                    'Test
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True
                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(holder1_name)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr1)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr2)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_addr3)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_city)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_state)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(folio_pincode)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear " & holder1_name & ",")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & "This is with reference to your letter regarding the above. Please note that we have Update the " & trantype_desc & "  of the following certificates in our records")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    'table
                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        pg = .Paragraphs.Add(.Paragraphs(22).Range)
                        tbl = .Tables.Add(pg.Range, 1, 8)
                    End With
                    tbl.Columns(1).Cells(1).Range.Text = "Folio No"
                    tbl.Columns(2).Cells(1).Range.Text = "Date"
                    tbl.Columns(3).Cells(1).Range.Text = "New Cert. No"
                    tbl.Columns(4).Cells(1).Range.Text = "Dist From"
                    tbl.Columns(5).Cells(1).Range.Text = "Dist To"
                    tbl.Columns(6).Cells(1).Range.Text = "No of Shares"
                    tbl.Columns(7).Cells(1).Range.Text = "Old Cert"
                    tbl.Columns(8).Cells(1).Range.Text = "Reason "

                    If dt_tt.Tables.Count > 0 AndAlso dt_tt.Tables(trr_sql).Rows.Count > 0 Then
                        For k As Integer = 0 To dt_tt.Tables(trr_sql).Rows.Count() - 1 Step +1
                            k = +k
                            tran_cert_no = dt_tt.Tables(trr_sql).Rows(k).Item("tran_cert_no").ToString
                            folio_no = dt_tt.Tables(trr_sql).Rows(k).Item("folio_no").ToString
                            dfrom2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_from").ToString
                            dto2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_to").ToString
                            c_no2 = dt_tt.Tables(trr_sql).Rows(k).Item("cert_no").ToString
                            dist_count2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_count").ToString
                            Sno2 = dt_tt.Tables(trr_sql).Rows(k).Item("Sno").ToString

                            'objWord.Selection.TypeText(folio_no & "    " & If(String.IsNullOrEmpty(approved_date), "            ", CDate(approved_date)) & vbTab & "  " & tran_cert_no & vbTab & "    " & dfrom2 & vbTab & dto2 & vbTab & " " & dist_count2 & vbTab & "       " & c_no2 & vbTab)

                            tbl.Rows.Add()
                            tbl.Cell(k + 2, 1).Range.Text = folio_no
                            tbl.Cell(k + 2, 2).Range.Text = If(String.IsNullOrEmpty(approved_date), "            ", CDate(approved_date))
                            tbl.Cell(k + 2, 3).Range.Text = tran_cert_no
                            tbl.Cell(k + 2, 4).Range.Text = dfrom2
                            tbl.Cell(k + 2, 5).Range.Text = dto2
                            tbl.Cell(k + 2, 6).Range.Text = dist_count2
                            tbl.Cell(k + 2, 7).Range.Text = c_no2

                            objWord.Selection.TypeParagraph()
                        Next
                    End If
                    tbl.Borders.Enable = False
                    tbl.Rows.Item(1).Range.Font.Bold = True

                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()
                End If

                'dmat/remat
                If (TranCode = "DM" OrElse TranCode = "RM") Then

                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    objWord.Selection.Font.Bold = True

                    objWord.Selection.TypeText("GNSA INFOTECH PRIVATE LIMITED" & vbNewLine)
                    objWord.Selection.TypeText("NELSON CHAMBERS, 4TH FLOOR,F-BLOCK" & vbNewLine)
                    objWord.Selection.TypeText("NO 115, NELSON MANICKAM ROAD" & vbNewLine)
                    objWord.Selection.TypeText("AMINTHAKARAI,CHENNAI-600029" & vbNewLine)
                    objWord.Selection.TypeText("044-42962025 / 044-42962050" & vbNewLine)
                    objWord.Selection.TypeText("Email: sta@gnsaindia.com" & vbNewLine)

                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & "  GNSA INFOTECH PRIVATE LIMITED                          ")

                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  NELSON CHAMBERS                 ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  4TH FLOOR,F-BLOCK,      ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  NO 115, NELSON MANICKAM ROAD, ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & " AMINTHAKARAI, CHENNAI-600029,       ")
                    'objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "  044-42962025 / 044-42962050.")
                    objWord.Selection.TypeText(" ")

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeText("Unit: " & comp_name)
                    objWord.Selection.TypeParagraph()

                    objWord.Selection.Font.Bold = False
                    objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft

                    objDoc.Paragraphs(1).Range.Bold = True

                    'objDoc.Paragraphs(1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    'objWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter


                    objWord.Selection.TypeText("Ref: \GNSA\" & comp_code & "\" & tran_code & "\" & inward_no)
                    objWord.Selection.TypeText("                                                                " & "Date : " & Format(Date.Today, "dd-MM-yyyy"))
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("XXXXXXXXXXX")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs.SpaceAfter = 0
                    objWord.Selection.TypeText("XXXXXXXXXXX")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs.SpaceAfter = 0
                    objWord.Selection.TypeText("XXXXXXXXXXX")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs.SpaceAfter = 0
                    objWord.Selection.TypeText("XXXXXXXXXXX")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs.SpaceAfter = 0
                    objWord.Selection.TypeText("XXXXXXXXXXX")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Ref: " & folio_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Sub: " & trantype_desc & " of Shares")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dear DEPOSITORY PARTICIPANT,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbTab & " With reference to the above, please note that your request has been rejected and the details are mentioned below:")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("")
                    objWord.Selection.TypeParagraph()
                    'table
                    Dim tbl As Table
                    Dim pg As Microsoft.Office.Interop.Word.Paragraph
                    With objDoc
                        'Add a new paragraph that the table will replace
                        pg = .Paragraphs.Add(.Paragraphs(18).Range)
                        'pg = .Paragraphs.Add(objWord.Selection.Range)
                        'Add a table in place of the new paragraph
                        tbl = .Tables.Add(pg.Range, 1, 6)
                    End With

                    tbl.Columns(1).Cells(1).Range.Text = "Sl. No."
                    tbl.Columns(2).Cells(1).Range.Text = "Folio No"
                    tbl.Columns(3).Cells(1).Range.Text = "Cert No "
                    tbl.Columns(4).Cells(1).Range.Text = "Dist From"
                    tbl.Columns(5).Cells(1).Range.Text = "Dist To"
                    tbl.Columns(6).Cells(1).Range.Text = "Shares"

                    If dt_tt.Tables.Count > 0 AndAlso dt_tt.Tables(trr_sql).Rows.Count > 0 Then
                        For k As Integer = 0 To dt_tt.Tables(trr_sql).Rows.Count() - 1 Step +1
                            'k = +k
                            dfrom2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_from").ToString
                            dto2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_to").ToString
                            c_no2 = dt_tt.Tables(trr_sql).Rows(k).Item("cert_no").ToString
                            dist_count2 = dt_tt.Tables(trr_sql).Rows(k).Item("dist_count").ToString
                            Sno2 = dt_tt.Tables(trr_sql).Rows(k).Item("Sno").ToString

                            If Not dematpend_gid = "" Then
                                'For x As Integer = 1 To dt_tt.Tables(trr_sql).Rows.Count() - 1 Step +1
                                '    x = +x
                                '    Dim rng As Range = objDoc.Range(0, 0)
                                '    Dim tlb As Table = objDoc.Tables.Add(Range:=rng, NumRows:=1, NumColumns:=x)

                                '    tlb.Cell(1, k).Range.Text = Sno2
                                '    tlb.Cell(2, k).Range.Text = folio_no
                                'Next
                                'objWord.Selection.TypeText(Sno2 & vbTab & vbTab & folio_no & vbTab & c_no2 & vbTab & vbTab & dfrom2 & vbTab & "  " & dto2 & vbTab & "     " & dist_count2)
                                objWord.Selection.TypeParagraph()

                                tbl.Rows.Add()
                                tbl.Cell(k + 2, 1).Range.Text = Sno2
                                tbl.Cell(k + 2, 2).Range.Text = folio_no
                                tbl.Cell(k + 2, 3).Range.Text = c_no2
                                tbl.Cell(k + 2, 4).Range.Text = dfrom2
                                tbl.Cell(k + 2, 5).Range.Text = dto2
                                tbl.Cell(k + 2, 6).Range.Text = dist_count2


                            ElseIf dt_t.Tables.Count > 0 AndAlso dt_t.Tables(tr_sql).Rows.Count > 0 Then

                                'For j As Integer = 0 To dt_t.Tables(tr_sql).Rows.Count() - 1 Step +1
                                '    j = +j
                                '    dfrom = dt_t.Tables(tr_sql).Rows(j).Item("dist_from").ToString
                                '    dto = dt_t.Tables(tr_sql).Rows(j).Item("dist_to").ToString
                                '    c_no = dt_t.Tables(tr_sql).Rows(j).Item("cert_no").ToString
                                '    dist_count = dt_t.Tables(tr_sql).Rows(j).Item("dist_count").ToString
                                '    Sno = dt_t.Tables(tr_sql).Rows(j).Item("Sno").ToString

                                '    objWord.Selection.TypeText(Sno & vbTab & vbTab & folio_no & vbTab & c_no & vbTab & vbTab & dfrom & vbTab & "  " & dto & vbTab & "     " & dist_count)

                                '    objWord.Selection.TypeParagraph()

                                'Next

                                tbl.Rows.Add()
                                tbl.Cell(k + 2, 1).Range.Text = Sno
                                tbl.Cell(k + 2, 2).Range.Text = folio_no
                                tbl.Cell(k + 2, 3).Range.Text = c_no
                                tbl.Cell(k + 2, 4).Range.Text = dfrom
                                tbl.Cell(k + 2, 5).Range.Text = dto
                                tbl.Cell(k + 2, 6).Range.Text = dist_count
                                objWord.Selection.TypeParagraph()

                            End If
                        Next
                    End If

                    tbl.Borders.Enable = False
                    tbl.Rows.Item(1).Range.Font.Bold = True
                    objWord.Selection.TypeText("____________________________________________________________________________")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("DRN. No.     " & vbTab & ":" & vbTab & drn_no)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Dp. ID       " & vbTab & ":" & vbTab & dp_id)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Client Id    " & vbTab & ":" & vbTab & client_id)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Depository   " & vbTab & ":" & vbTab & depository_code)
                    objWord.Selection.TypeParagraph()
                    If (chk_lst > 0) Then
                        objWord.Selection.TypeText("DRF Qty      " & vbTab & ":" & vbTab & share_count)
                    ElseIf (chk_lst = 0) Then
                        objWord.Selection.TypeText("DRF Qty      " & vbTab & ":" & vbTab & share_count)

                    End If
                    'objWord.Selection.TypeText("Accepted Qty " & vbTab & ":" & vbTab & folio_shares)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Rejected Qty " & vbTab & ":" & vbTab & folio_shares)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.Font.Bold = True
                    objWord.Selection.Font.Underline = True
                    objWord.Selection.TypeText(" Reason & Rectification : ")
                    objWord.Selection.Font.Bold = False
                    objWord.Selection.Font.Underline = False
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()

                    If dtmer.Rows.Count > 0 Then
                        For j As Integer = 0 To dtmer.Rows.Count() - 1 Step +1

                            j = +j
                            dematreject_code = dtmer.Rows(j).Item("dematreject_code").ToString
                            dematreject_desc = dtmer.Rows(j).Item("dematreject_desc").ToString
                            objWord.Selection.Font.Bold = True

                            objWord.Selection.TypeText(vbTab & vbTab & If(String.IsNullOrEmpty(dematreject_code), "", dematreject_code) & vbTab & If(String.IsNullOrEmpty(dematreject_desc), "", dematreject_desc))
                            objWord.Selection.Font.Bold = False

                            objWord.Selection.TypeParagraph()
                            If dematreject_code = 30 Then
                                objWord.Selection.TypeText("Kindly provide to us Govt. Gazzette Copy (Duly Notarized by a Notary Public).")
                                objWord.Selection.TypeParagraph()
                            ElseIf dematreject_code = 22 Then
                                objWord.Selection.TypeText("Kindly arrange to forward a new demat request duly attested by Bank (or) Notary Public for Signature Mismatch.")
                                objWord.Selection.TypeParagraph()
                            ElseIf dematreject_code = 21 Then
                                objWord.Selection.TypeText("Kindly arrange a new demat request attested by bank official with seal / signature along with ISR-2 form to be signed by the shareholder and forward to us for taking necessary action at our end. Please also provide self - Attested pancard & aadharcard for further action at our end")
                                objWord.Selection.TypeParagraph()
                            End If
                        Next
                    End If

                    objWord.Selection.TypeText("___________________________________________________________________________")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Kindly acknowledge receipt of the same")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Thanking You,")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(" ")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Yours faithfully,")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("For GNSA INFOTECH PRIVATE LIMITED,")
                    objWord.Selection.TypeParagraph()
                    'objDoc.Paragraphs(28).Range.Bold = True
                    objWord.Selection.TypeText("Sd/-")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("Authorised Signatory")
                    objWord.Selection.TypeText(vbNewLine)
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText("       •	This is a computer generated letter and does not require signature.")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.TypeText(vbNewLine)

                    objWord.Selection.Font.Bold = True
                    objWord.Selection.TypeText("Encl : ISR - 2")
                    objWord.Selection.TypeParagraph()
                    objWord.Selection.Font.Bold = False


                End If
            Next
        End If
        con.Close()
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
        Call frmCtrClear(Me)
    End Sub
End Class

