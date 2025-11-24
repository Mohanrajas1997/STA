Public Class frmInwardDocSubtypeReport
    Dim mnUploadId As Long

    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ' queue
        lsSql = ""
        lsSql &= " select group_status,group_code,group_name from sta_mst_tgroup "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by group_name asc "

        Call gpBindCombo(lsSql, "group_name", "group_status", cboQueue, gOdbcConn)

        ' updated
        With cboUpdated
            .Items.Clear()
            .Items.Add("Yes")
            .Items.Add("No")
        End With

        ' completed
        With cboCompleted
            .Items.Clear()
            .Items.Add("Yes")
            .Items.Add("No")
        End With

        ' valid
        With cboValid
            .Items.Clear()
            .Items.Add("Yes")
            .Items.Add("No")
        End With

        ' status
        With cboStatus
            .Items.Clear()
            '.Items.Add("Despatched")
            .Items.Add("Valid")
            .Items.Add("Valid Pending")
            .Items.Add("Valid Completed")
            .Items.Add("Discrepant")
            .Items.Add("Discrepant Pending")
            .Items.Add("Discrepant Completed")
            .Items.Add("Signature Available")
            .Items.Add("Signature Not Available")
            .Items.Add("PAN Available")
            .Items.Add("PAN Not Available")
        End With

        With cboReportType
            .Items.Clear()
            .Items.Add("Inward")
            .Items.Add("Inward Detail")
            .Items.Add("Inward Certificate")
            .Items.Add("Inward Certificate Dist")
            .Items.Add("Inward Certificate Split")
        End With

        dtpFrom.Value = Now
        dtpTo.Value = Now

        dtpFrom.Checked = False
        dtpTo.Checked = False
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
        Dim lsDocSubTypeCode As String = ""

        lsCond = ""

        If dtpFrom.Checked = True Then lsCond &= " and a.received_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and a.received_date <= '" & Format(dtpTo.Value, "yyyy-MM-dd") & "' "

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            lsCond &= " and a.tran_code = '" & cboDocType.SelectedValue.ToString & "' "
        End If

        If txtHolderName.Text <> "" Then lsCond &= " and a.shareholder_name like '" & QuoteFilter(txtHolderName.Text) & "%'"
        If txtFolioNo.Text <> "" Then lsCond &= " and a.folio_no like '" & QuoteFilter(txtFolioNo.Text) & "%'"

        If txtToHolderName.Text <> "" Then lsCond &= " and l.holder1_name like '" & QuoteFilter(txtToHolderName.Text) & "%'"
        If txtToFolioNo.Text <> "" Then lsCond &= " and l.folio_no like '" & QuoteFilter(txtToFolioNo.Text) & "%'"

        If txtInwardNo.Text <> "" Then lsCond &= " and a.inward_comp_no = '" & QuoteFilter(txtInwardNo.Text) & "' "

        ' Get codes
        Dim selectedCodes As String = String.Join(","c, DocSubType.CheckedItems.Cast(Of DataRowView)().Select(Function(row) "'" & row("docsubtype_code").ToString() & "'"))

        ' Assign to variables
        lsDocSubTypeCode = selectedCodes

        If lsDocSubTypeCode <> "" Then
            lsCond &= " and (q.docsubtype_code in (" & lsDocSubTypeCode & ") "
            lsCond &= " or a.docsubtype_code in (" & lsDocSubTypeCode & ")) "
        End If

        If cboQueue.Text <> "" And cboQueue.SelectedIndex <> -1 Then
            lsCond &= " and a.queue_status = " & Val(cboQueue.SelectedValue.ToString) & " "
        End If

        Select Case cboUpdated.Text.ToUpper
            Case "YES"
                lsCond &= " and a.update_completed = 'Y' "
            Case "NO"
                lsCond &= " and a.update_completed = 'N' "
        End Select

        Select Case cboCompleted.Text.ToUpper
            Case "YES"
                lsCond &= " and a.inward_completed = 'Y' "
            Case "NO"
                lsCond &= " and a.inward_completed = 'N' "
        End Select

        Select Case cboValid.Text.ToUpper
            Case "YES"
                lsCond &= " and a.chklst_disc = 0 "
            Case "NO"
                lsCond &= " and a.chklst_disc > 0 "
        End Select

        Select Case cboStatus.Text.ToUpper
            Case "VALID"
                lsCond &= " and a.chklst_disc = 0 "
            Case "VALID PENDING"
                lsCond &= " and a.chklst_disc = 0 "
                lsCond &= " and a.inward_completed = 'N' "
            Case "VALID COMPLETED"
                lsCond &= " and a.chklst_disc = 0 "
                lsCond &= " and a.inward_completed = 'Y' "
            Case "DISCREPANT"
                lsCond &= " and a.chklst_disc > 0 "
            Case "DISCREPANT PENDING"
                lsCond &= " and a.chklst_disc > 0 "
                lsCond &= " and a.inward_completed = 'N' "
            Case "DISCREPANT COMPLETED"
                lsCond &= " and a.chklst_disc > 0 "
                lsCond &= " and a.inward_completed = 'Y' "
            Case "SIGNATURE AVAILABLE"
                lsCond &= " and o.signature_gid > 0 "
            Case "SIGNATURE NOT AVAILABLE"
                lsCond &= " and o.signature_gid = 0 "
            Case "PAN AVAILABLE"
                lsCond &= " and o.holder1_pan_no <> '' "
                lsCond &= " and (o.holder2_name = '' or (o.holder2_name <> '' and o.holder2_pan_no <> '')) "
                lsCond &= " and (o.holder3_name = '' or (o.holder3_name <> '' and o.holder3_pan_no <> '')) "
            Case "PAN NOT AVAILABLE"
                lsCond &= " and not (o.holder1_pan_no <> '' "
                lsCond &= " and (o.holder2_name = '' or (o.holder2_name <> '' and o.holder2_pan_no <> '')) "
                lsCond &= " and (o.holder3_name = '' or (o.holder3_name <> '' and o.holder3_pan_no <> ''))) "
        End Select

        If mnUploadId > 0 Then lsCond &= " and a.upload_gid = " & mnUploadId & " "

        If lsCond = "" Then lsCond = " and 1 = 2 "
        If cboReportType.Text.ToUpper() = "INWARD DETAIL" Then
            lsSql = ""
            lsSql &= " select Distinct"
            lsSql &= " a.received_date as 'Received Date',"
            lsSql &= " e.meeting_date as 'Meeting Date',"
            lsSql &= " e.status_update_date as 'Confirm Date',"
            lsSql &= " a.inward_no as 'Auto Inward No',"
            lsSql &= " a.inward_comp_no as 'Inward No',"
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " c.isin_id as 'ISIN No',"
            lsSql &= " a.folio_no as 'Folio No',"
            lsSql &= " a.shareholder_name as 'Share Holder',"
            lsSql &= " a.inward_share_count as 'Inward share count',"
            lsSql &= " a.market_price as 'Market Price',"
            lsSql &= " a.market_value as 'Market Value',"
            lsSql &= " a.threshold_level as 'Threshold Level',"
            lsSql &= " a.tran_code as 'Tran Code',"
            lsSql &= " b.trantype_desc as 'Document Type',"
            'lsSql &= " p.docsubtype_desc as 'Document SubType',"
            lsSql &= " fn_get_docsubtypedesc(a.inward_gid,a.tran_code) as 'Document SubType',"
            lsSql &= " l.folio_no as 'To Folio No',"
            lsSql &= " l.holder1_name as 'To Share Holder',"
            lsSql &= " if(a.chklst_disc = 0,'Valid','Disc') as 'Doc Status',"
            lsSql &= " concat(if(a.chklst_disc = 0,'Valid ','Disc '),make_set(a.inward_status," & gsInwardStatusDesc & ")) as 'Status',"
            lsSql &= " make_set(a.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
            lsSql &= " make_set(a.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
            lsSql &= " make_set(e.upload_status," & gsUploadStatusDesc & ") as 'Upload Status',"
            lsSql &= " a.received_mode as 'Received Mode',"
            lsSql &= " d.courier_name as 'Courier Name',"
            lsSql &= " a.awb_no as 'Awb No',"
            lsSql &= " a.approved_date as 'Approved Date',"
            lsSql &= " a.inward_valid as 'Inward Valid',"
            lsSql &= " a.inward_completed as 'Inward Completed',"
            lsSql &= " a.update_completed as 'Update Completed',"
            lsSql &= " a.share_count as 'Transfer Share Count',"
            lsSql &= " a.share_value as 'Transfer Share Value',"
            lsSql &= " a.stamp_duty as 'Transfer Stamp Duty',"
            lsSql &= " a.execution_date as 'Transfer Execution Date',"
            lsSql &= " a.witness_name as 'Transfer Witness Name',"
            lsSql &= " a.witness_addr as 'Transfer Witness Address',"
            lsSql &= " a.tran_folio_gid as 'Tran Folio Id',"
            lsSql &= " a.tran_cert_no as 'Tran Cert No',"
            lsSql &= " a.tran_cert_gid as 'Tran Cert Id',"
            lsSql &= " a.transfer_no as 'Transfer No',"
            lsSql &= " a.objx_no as 'Objx No',"
            lsSql &= " a.queue_status as 'Queue Status Value',"
            lsSql &= " a.queue_all_status as 'Queue All Status',"
            lsSql &= " e.upload_status as 'Upload Status Value',"
            lsSql &= " a.split_formula as 'Split Formula',"
            lsSql &= " a.chklst_valid as 'Chklst Valid',"
            lsSql &= " a.chklst_disc as 'Chklst Disc',"
            lsSql &= " a.lockin_period_from as 'Lockin Period From',"
            lsSql &= " a.lockin_period_to as 'Lockin Period To',"
            lsSql &= " a.insert_date as 'Inward Insert Date',"
            lsSql &= " a.insert_by as 'Inward Insert By',"
            lsSql &= " a.update_date as 'Inward Update Date',"
            lsSql &= " a.update_by as 'Inward Update By',"
            lsSql &= " f.outward_date as 'Outward Date',"
            lsSql &= " f.outward_mode as 'Outward Mode',"
            lsSql &= " g.courier_name as 'Outward Courier Name',"
            lsSql &= " f.awb_no as 'Outward Awb No',"
            lsSql &= " f.outward_remark as 'Outward Remark',"
            lsSql &= " f.insert_date as 'Outward Insert Date',"
            lsSql &= " f.insert_by as 'Outward Insert By',"
            lsSql &= " f.update_date as 'Outward Update Date',"
            lsSql &= " f.update_by as 'Outward Update By',"
            lsSql &= " k.req_date as 'DRN Req Date',"
            lsSql &= " k.drn_no as 'DRN No',"
            lsSql &= " k.depository_code as 'DRN Despository Code',"
            lsSql &= " k.isin_id as 'DRN ISIN ID',"
            lsSql &= " k.dp_id as 'DRN DP ID',"
            lsSql &= " k.cust_name as 'DRN Cust Name',"
            lsSql &= " k.share_count as 'DRN Share Count',"
            lsSql &= " k.dematpend_flag as 'DRN Demat Pend Flag',"
            lsSql &= " m.holder1_name as 'Proposed Holder1',"
            lsSql &= " m.holder1_fh_name as 'Proposed F/H Name1',"
            lsSql &= " m.holder1_pan_no as 'Proposed Pan1',"
            lsSql &= " m.holder2_name as 'Proposed Holder2',"
            lsSql &= " m.holder2_fh_name as 'Proposed F/H Name2',"
            lsSql &= " m.holder2_pan_no as 'Proposed Pan2',"
            lsSql &= " m.holder3_name as 'Proposed Holder3',"
            lsSql &= " m.holder3_fh_name as 'Proposed F/H Name3',"
            lsSql &= " m.holder3_pan_no as 'Proposed Pan3',"
            lsSql &= " m.folio_addr1 as 'Proposed Address1',"
            lsSql &= " m.folio_addr2 as 'Proposed Address2',"
            lsSql &= " m.folio_addr3 as 'Proposed Address3',"
            lsSql &= " m.folio_city as 'Proposed City',"
            lsSql &= " m.folio_state as 'Proposed State',"
            lsSql &= " m.folio_country as 'Proposed Country',"
            lsSql &= " m.folio_pincode as 'Proposed Pincode',"
            lsSql &= " m.folio_mail_id as 'Proposed Mail ID',"
            lsSql &= " m.folio_contact_no as 'Proposed Contact No',"
            lsSql &= " m.nominee_reg_no as 'Proposed Nominee Reg No',"
            lsSql &= " m.nominee_name as 'Proposed Nominee Name',"
            lsSql &= " m.nominee_addr1 as 'Proposed Nominee Addr1',"
            lsSql &= " m.nominee_addr2 as 'Proposed Nominee Addr2',"
            lsSql &= " m.nominee_addr3 as 'Proposed Nominee Addr3',"
            lsSql &= " m.nominee_city as 'Proposed Nominee City',"
            lsSql &= " m.nominee_state as 'Proposed Nominee State',"
            lsSql &= " m.nominee_country as 'Proposed Nominee Country',"
            lsSql &= " m.nominee_pincode as 'Proposed Nominee Pincode',"
            lsSql &= " m.bank_name as 'Proposed Bank Name',"
            lsSql &= " m.bank_acc_no as 'Proposed Bank A/C No',"
            lsSql &= " m.bank_ifsc_code as 'Proposed Bank Ifsc Code',"
            lsSql &= " m.bank_branch as 'Proposed Bank Branch',"
            lsSql &= " m.bank_beneficiary as 'Proposed Bank Beneficiary',"
            lsSql &= " m.bank_acc_type as 'Proposed Bank A/C Type',"
            lsSql &= " m.bank_addr as 'Proposed Bank Addr',"
            lsSql &= " n.holder1_name as 'Old Holder1',"
            lsSql &= " n.holder1_fh_name as 'Old F/H Name1',"
            lsSql &= " n.holder1_pan_no as 'Old Pan1',"
            lsSql &= " n.holder2_name as 'Old Holder2',"
            lsSql &= " n.holder2_fh_name as 'Old F/H Name2',"
            lsSql &= " n.holder2_pan_no as 'Old Pan2',"
            lsSql &= " n.holder3_name as 'Old Holder3',"
            lsSql &= " n.holder3_fh_name as 'Old F/H Name3',"
            lsSql &= " n.holder3_pan_no as 'Old Pan3',"
            lsSql &= " n.folio_addr1 as 'Old Address1',"
            lsSql &= " n.folio_addr2 as 'Old Address2',"
            lsSql &= " n.folio_addr3 as 'Old Address3',"
            lsSql &= " n.folio_city as 'Old City',"
            lsSql &= " n.folio_state as 'Old State',"
            lsSql &= " n.folio_country as 'Old Country',"
            lsSql &= " n.folio_pincode as 'Old Pincode',"
            lsSql &= " n.folio_mail_id as 'Old Mail ID',"
            lsSql &= " n.folio_contact_no as 'Old Contact No',"
            lsSql &= " n.nominee_reg_no as 'Old Nominee Reg No',"
            lsSql &= " n.nominee_name as 'Old Nominee Name',"
            lsSql &= " n.nominee_addr1 as 'Old Nominee Addr1',"
            lsSql &= " n.nominee_addr2 as 'Old Nominee Addr2',"
            lsSql &= " n.nominee_addr3 as 'Old Nominee Addr3',"
            lsSql &= " n.nominee_city as 'Old Nominee City',"
            lsSql &= " n.nominee_state as 'Old Nominee State',"
            lsSql &= " n.nominee_country as 'Old Nominee Country',"
            lsSql &= " n.nominee_pincode as 'Old Nominee Pincode',"
            lsSql &= " a.nominee_assign_flag as 'Nominee Assign Flag',"
            lsSql &= " a.isr_sno as 'ISR Sno',"
            lsSql &= " case when a.stamp_duty_flag = 'Y' then 'Yes' else 'No' end as 'Stamp Duty Flag',"
            lsSql &= " n.bank_name as 'Old Bank Name',"
            lsSql &= " n.bank_acc_no as 'Old Bank A/C No',"
            lsSql &= " n.bank_ifsc_code as 'Old Bank Ifsc Code',"
            lsSql &= " n.bank_branch as 'Old Bank Branch',"
            lsSql &= " n.bank_beneficiary as 'Old Bank Beneficiary',"
            lsSql &= " n.bank_acc_type as 'Old Bank A/C Type',"
            lsSql &= " n.bank_addr as 'Old Bank Addr',"
            lsSql &= " a.inward_gid as 'Inward Id', "
            lsSql &= " a.queue_gid as 'Queue Id',"
            lsSql &= " a.outward_gid as 'Outward Id',"
            lsSql &= " a.folio_gid as 'Folio Id',"
            lsSql &= " a.dematpend_gid as 'Demat Pend Id',"
            lsSql &= " a.reason_gid as 'Reason Id',"
            lsSql &= " a.upload_gid as 'Upload Id',"
            lsSql &= " a.courier_gid as 'Inward Courier Id',"
            lsSql &= " f.courier_gid as 'Outward Courier Id',"
            lsSql &= " o.signature_gid as 'Signature Id' "
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on c.comp_gid = a.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as d on d.courier_gid = a.courier_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as e on a.upload_gid = e.upload_gid and e.delete_flag = 'N' "
            lsSql &= " left join sta_trn_toutward as f on a.inward_gid = f.inward_gid and f.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as g on f.courier_gid = g.courier_gid and g.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tdematpend as k on a.inward_gid = k.inward_gid and k.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as l on a.tran_folio_gid = l.folio_gid and l.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolioentry as m on a.inward_gid = m.inward_gid and m.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolioentryold as n on m.folioentry_gid = n.folioentry_gid and n.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as o on a.folio_gid = o.folio_gid and o.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tdocsubtype as p on p.docsubtype_code = a.docsubtype_code and p.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinwarddocsubtype as q on a.tran_code = q.trantype_code "
            lsSql &= " and a.inward_gid = q.inward_gid  and q.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.inward_gid asc"
        ElseIf cboReportType.Text.ToUpper() = "INWARD CERTIFICATE" Then
            lsSql = ""
            lsSql &= " select Distinct"
            lsSql &= " a.received_date as 'Received Date',"
            lsSql &= " e.meeting_date as 'Meeting Date',"
            lsSql &= " e.status_update_date as 'Confirm Date',"
            lsSql &= " a.inward_no as 'Auto Inward No',"
            lsSql &= " a.inward_comp_no as 'Inward No',"
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " c.isin_id as 'ISIN No',"
            lsSql &= " a.folio_no as 'Folio No',"
            lsSql &= " i.cert_no as 'Cert No',"
            lsSql &= " i.share_count as 'Share Count',"
            lsSql &= " fn_sta_get_certdist(i.cert_gid) as 'Dist Series',"
            lsSql &= " a.shareholder_name as 'Share Holder',"
            lsSql &= " a.inward_share_count as 'Inward share count',"
            lsSql &= " a.market_price as 'Market Price',"
            lsSql &= " a.market_value as 'Market Value',"
            lsSql &= " a.threshold_level as 'Threshold Level',"
            lsSql &= " a.tran_code as 'Tran Code',"
            lsSql &= " b.trantype_desc as 'Document Type',"
            'lsSql &= " p.docsubtype_desc as 'Document SubType',"
            lsSql &= " fn_get_docsubtypedesc(a.inward_gid,a.tran_code) as 'Document SubType',"
            lsSql &= " l.folio_no as 'To Folio No',"
            lsSql &= " l.holder1_name as 'To Share Holder',"
            lsSql &= " if(a.chklst_disc = 0,'Valid','Disc') as 'Doc Status',"
            lsSql &= " concat(if(a.chklst_disc = 0,'Valid ','Disc '),make_set(a.inward_status," & gsInwardStatusDesc & ")) as 'Status',"
            lsSql &= " make_set(a.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
            lsSql &= " make_set(a.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
            lsSql &= " make_set(e.upload_status," & gsUploadStatusDesc & ") as 'Upload Status',"
            lsSql &= " a.received_mode as 'Received Mode',"
            lsSql &= " d.courier_name as 'Courier Name',"
            lsSql &= " a.awb_no as 'Awb No',"
            lsSql &= " a.approved_date as 'Approved Date',"
            lsSql &= " a.inward_valid as 'Inward Valid',"
            lsSql &= " a.inward_completed as 'Inward Completed',"
            lsSql &= " a.update_completed as 'Update Completed',"
            lsSql &= " a.share_count as 'Transfer Share Count',"
            lsSql &= " a.share_value as 'Transfer Share Value',"
            lsSql &= " a.stamp_duty as 'Transfer Stamp Duty',"
            lsSql &= " a.execution_date as 'Transfer Execution Date',"
            lsSql &= " a.witness_name as 'Transfer Witness Name',"
            lsSql &= " a.witness_addr as 'Transfer Witness Address',"
            lsSql &= " a.tran_folio_gid as 'Tran Folio Id',"
            lsSql &= " a.tran_cert_no as 'Tran Cert No',"
            lsSql &= " a.tran_cert_gid as 'Tran Cert Id',"
            lsSql &= " a.transfer_no as 'Transfer No',"
            lsSql &= " a.objx_no as 'Objx No',"
            lsSql &= " a.queue_status as 'Queue Status Value',"
            lsSql &= " a.queue_all_status as 'Queue All Status',"
            lsSql &= " e.upload_status as 'Upload Status Value',"
            lsSql &= " a.split_formula as 'Split Formula',"
            lsSql &= " a.chklst_valid as 'Chklst Valid',"
            lsSql &= " a.chklst_disc as 'Chklst Disc',"
            lsSql &= " a.lockin_period_from as 'Lockin Period From',"
            lsSql &= " a.lockin_period_to as 'Lockin Period To',"
            lsSql &= " a.insert_date as 'Inward Insert Date',"
            lsSql &= " a.insert_by as 'Inward Insert By',"
            lsSql &= " a.update_date as 'Inward Update Date',"
            lsSql &= " a.update_by as 'Inward Update By',"
            lsSql &= " f.outward_date as 'Outward Date',"
            lsSql &= " f.outward_mode as 'Outward Mode',"
            lsSql &= " g.courier_name as 'Outward Courier Name',"
            lsSql &= " f.awb_no as 'Outward Awb No',"
            lsSql &= " f.outward_remark as 'Outward Remark',"
            lsSql &= " f.insert_date as 'Outward Insert Date',"
            lsSql &= " f.insert_by as 'Outward Insert By',"
            lsSql &= " f.update_date as 'Outward Update Date',"
            lsSql &= " f.update_by as 'Outward Update By',"
            lsSql &= " k.req_date as 'DRN Req Date',"
            lsSql &= " k.drn_no as 'DRN No',"
            lsSql &= " k.depository_code as 'DRN Despository Code',"
            lsSql &= " k.isin_id as 'DRN ISIN ID',"
            lsSql &= " k.dp_id as 'DRN DP ID',"
            lsSql &= " k.cust_name as 'DRN Cust Name',"
            lsSql &= " k.share_count as 'DRN Share Count',"
            lsSql &= " k.dematpend_flag as 'DRN Demat Pend Flag',"
            lsSql &= " a.nominee_assign_flag as 'Nominee Assign Flag',"
            lsSql &= " a.isr_sno as 'ISR Sno',"
            lsSql &= " case when a.stamp_duty_flag = 'Y' then 'Yes' else 'No' end as 'Stamp Duty Flag',"
            lsSql &= " a.inward_gid as 'Inward Id', "
            lsSql &= " a.queue_gid as 'Queue Id',"
            lsSql &= " a.outward_gid as 'Outward Id',"
            lsSql &= " a.folio_gid as 'Folio Id',"
            lsSql &= " i.cert_gid as 'Cert Id',"
            lsSql &= " o.signature_gid as 'Signature Id',"
            lsSql &= " h.certentry_gid as 'Cert Entry Id',"
            lsSql &= " a.dematpend_gid as 'Demat Pend Id',"
            lsSql &= " a.reason_gid as 'Reason Id',"
            lsSql &= " a.upload_gid as 'Upload Id',"
            lsSql &= " a.courier_gid as 'Inward Courier Id',"
            lsSql &= " f.courier_gid as 'Outward Courier Id' "
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on c.comp_gid = a.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as d on d.courier_gid = a.courier_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as e on a.upload_gid = e.upload_gid and e.delete_flag = 'N' "
            lsSql &= " left join sta_trn_toutward as f on a.inward_gid = f.inward_gid and f.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as g on f.courier_gid = g.courier_gid and g.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcertentry as h on a.inward_gid = h.inward_gid and h.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcert as i on h.cert_gid = i.cert_gid and i.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tdematpend as k on a.inward_gid = k.inward_gid and k.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as l on a.tran_folio_gid = l.folio_gid and l.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as o on a.folio_gid = o.folio_gid and o.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tdocsubtype as p on p.docsubtype_code = a.docsubtype_code and p.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinwarddocsubtype as q on a.tran_code = q.trantype_code "
            lsSql &= " and a.inward_gid = q.inward_gid  and q.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.inward_gid,i.cert_no asc"
        ElseIf cboReportType.Text.ToUpper = "INWARD CERTIFICATE DIST" Then
            lsSql = ""
            lsSql &= " select Distinct"
            lsSql &= " a.received_date as 'Received Date',"
            lsSql &= " e.meeting_date as 'Meeting Date',"
            lsSql &= " e.status_update_date as 'Confirm Date',"
            lsSql &= " a.inward_no as 'Auto Inward No',"
            lsSql &= " a.inward_comp_no as 'Inward No',"
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " c.isin_id as 'ISIN No',"
            lsSql &= " a.folio_no as 'Folio No',"
            lsSql &= " i.cert_no as 'Cert No',"
            lsSql &= " concat(cast(j.dist_from as nchar),'-',cast(j.dist_to as nchar)) as 'Dist Series',"
            lsSql &= " j.dist_count as 'Dist Count',"
            lsSql &= " a.shareholder_name as 'Share Holder',"
            lsSql &= " a.inward_share_count as 'Inward share count',"
            lsSql &= " a.market_price as 'Market Price',"
            lsSql &= " a.market_value as 'Market Value',"
            lsSql &= " a.threshold_level as 'Threshold Level',"
            lsSql &= " a.tran_code as 'Tran Code',"
            lsSql &= " b.trantype_desc as 'Document Type',"
            'lsSql &= " p.docsubtype_desc as 'Document SubType',"
            lsSql &= " fn_get_docsubtypedesc(a.inward_gid,a.tran_code) as 'Document SubType',"
            lsSql &= " l.folio_no as 'To Folio No',"
            lsSql &= " l.holder1_name as 'To Share Holder',"
            lsSql &= " if(a.chklst_disc = 0,'Valid','Disc') as 'Doc Status',"
            lsSql &= " concat(if(a.chklst_disc = 0,'Valid ','Disc '),make_set(a.inward_status," & gsInwardStatusDesc & ")) as 'Status',"
            lsSql &= " make_set(a.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
            lsSql &= " make_set(a.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
            lsSql &= " make_set(e.upload_status," & gsUploadStatusDesc & ") as 'Upload Status',"
            lsSql &= " a.received_mode as 'Received Mode',"
            lsSql &= " d.courier_name as 'Courier Name',"
            lsSql &= " a.awb_no as 'Awb No',"
            lsSql &= " a.approved_date as 'Approved Date',"
            lsSql &= " a.inward_valid as 'Inward Valid',"
            lsSql &= " a.inward_completed as 'Inward Completed',"
            lsSql &= " a.update_completed as 'Update Completed',"
            lsSql &= " a.share_count as 'Transfer Share Count',"
            lsSql &= " a.share_value as 'Transfer Share Value',"
            lsSql &= " a.stamp_duty as 'Transfer Stamp Duty',"
            lsSql &= " a.execution_date as 'Transfer Execution Date',"
            lsSql &= " a.witness_name as 'Transfer Witness Name',"
            lsSql &= " a.witness_addr as 'Transfer Witness Address',"
            lsSql &= " a.tran_folio_gid as 'Tran Folio Id',"
            lsSql &= " a.tran_cert_no as 'Tran Cert No',"
            lsSql &= " a.tran_cert_gid as 'Tran Cert Id',"
            lsSql &= " a.transfer_no as 'Transfer No',"
            lsSql &= " a.objx_no as 'Objx No',"
            lsSql &= " a.queue_status as 'Queue Status Value',"
            lsSql &= " a.queue_all_status as 'Queue All Status',"
            lsSql &= " e.upload_status as 'Upload Status Value',"
            lsSql &= " a.split_formula as 'Split Formula',"
            lsSql &= " a.chklst_valid as 'Chklst Valid',"
            lsSql &= " a.chklst_disc as 'Chklst Disc',"
            lsSql &= " a.lockin_period_from as 'Lockin Period From',"
            lsSql &= " a.lockin_period_to as 'Lockin Period To',"
            lsSql &= " a.insert_date as 'Inward Insert Date',"
            lsSql &= " a.insert_by as 'Inward Insert By',"
            lsSql &= " a.update_date as 'Inward Update Date',"
            lsSql &= " a.update_by as 'Inward Update By',"
            lsSql &= " f.outward_date as 'Outward Date',"
            lsSql &= " f.outward_mode as 'Outward Mode',"
            lsSql &= " g.courier_name as 'Outward Courier Name',"
            lsSql &= " f.awb_no as 'Outward Awb No',"
            lsSql &= " f.outward_remark as 'Outward Remark',"
            lsSql &= " f.insert_date as 'Outward Insert Date',"
            lsSql &= " f.insert_by as 'Outward Insert By',"
            lsSql &= " f.update_date as 'Outward Update Date',"
            lsSql &= " f.update_by as 'Outward Update By',"
            lsSql &= " k.req_date as 'DRN Req Date',"
            lsSql &= " k.drn_no as 'DRN No',"
            lsSql &= " k.depository_code as 'DRN Despository Code',"
            lsSql &= " k.isin_id as 'DRN ISIN ID',"
            lsSql &= " k.dp_id as 'DRN DP ID',"
            lsSql &= " k.cust_name as 'DRN Cust Name',"
            lsSql &= " k.share_count as 'DRN Share Count',"
            lsSql &= " k.dematpend_flag as 'DRN Demat Pend Flag',"
            lsSql &= " e.upload_no as 'Upload No',"
            lsSql &= " a.nominee_assign_flag as 'Nominee Assign Flag',"
            lsSql &= " a.isr_sno as 'ISR Sno',"
            lsSql &= " case when a.stamp_duty_flag = 'Y' then 'Yes' else 'No' end as 'Stamp Duty Flag',"
            lsSql &= " a.inward_gid as 'Inward Id',"
            lsSql &= " a.queue_gid as 'Queue Id',"
            lsSql &= " a.outward_gid as 'Outward Id',"
            lsSql &= " a.folio_gid as 'Folio Id',"
            lsSql &= " i.cert_gid as 'Cert Id',"
            lsSql &= " h.certentry_gid as 'Cert Entry Id',"
            lsSql &= " a.dematpend_gid as 'Demat Pend Id',"
            lsSql &= " a.reason_gid as 'Reason Id',"
            lsSql &= " a.upload_gid as 'Upload Id',"
            lsSql &= " a.courier_gid as 'Inward Courier Id',"
            lsSql &= " f.courier_gid as 'Outward Courier Id',"
            lsSql &= " o.signature_gid as 'Signature Id' "
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on c.comp_gid = a.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as d on d.courier_gid = a.courier_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as e on a.upload_gid = e.upload_gid and e.delete_flag = 'N' "
            lsSql &= " left join sta_trn_toutward as f on a.inward_gid = f.inward_gid and f.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as g on f.courier_gid = g.courier_gid and g.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcertentry as h on a.inward_gid = h.inward_gid and h.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcert as i on h.cert_gid = i.cert_gid and i.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcertdist as j on i.cert_gid = j.cert_gid and j.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tdematpend as k on a.inward_gid = k.inward_gid and k.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as l on a.tran_folio_gid = l.folio_gid and l.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as o on a.folio_gid = o.folio_gid and o.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tdocsubtype as p on p.docsubtype_code = a.docsubtype_code and p.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinwarddocsubtype as q on a.tran_code = q.trantype_code "
            lsSql &= " and a.inward_gid = q.inward_gid  and q.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.inward_gid,i.cert_no,j.dist_from asc"
        ElseIf cboReportType.Text.ToUpper = "INWARD CERTIFICATE SPLIT" Then
            lsSql = ""
            lsSql &= " select Distinct"
            lsSql &= " a.received_date as 'Received Date',"
            lsSql &= " e.meeting_date as 'Meeting Date',"
            lsSql &= " e.status_update_date as 'Confirm Date',"
            lsSql &= " a.inward_no as 'Auto Inward No',"
            lsSql &= " a.inward_comp_no as 'Inward No',"
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " c.isin_id as 'ISIN No',"
            lsSql &= " a.folio_no as 'Folio No',"
            lsSql &= " i.cert_no as 'Cert No',"
            lsSql &= " i.share_count as 'Share Count',"
            lsSql &= " fn_sta_get_certdist(i.cert_gid) as 'Dist Series',"
            lsSql &= " a.shareholder_name as 'Share Holder',"
            lsSql &= " a.inward_share_count as 'Inward share count',"
            lsSql &= " a.market_price as 'Market Price',"
            lsSql &= " a.market_value as 'Market Value',"
            lsSql &= " a.threshold_level as 'Threshold Level',"
            lsSql &= " a.tran_code as 'Tran Code',"
            lsSql &= " b.trantype_desc as 'Document Type',"
            'lsSql &= " p.docsubtype_desc as 'Document SubType',"
            lsSql &= " fn_get_docsubtypedesc(a.inward_gid,a.tran_code) as 'Document SubType',"
            lsSql &= " l.folio_no as 'To Folio No',"
            lsSql &= " l.holder1_name as 'To Share Holder',"
            lsSql &= " if(a.chklst_disc = 0,'Valid','Disc') as 'Doc Status',"
            lsSql &= " concat(if(a.chklst_disc = 0,'Valid ','Disc '),make_set(a.inward_status," & gsInwardStatusDesc & ")) as 'Status',"
            lsSql &= " make_set(a.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
            lsSql &= " make_set(a.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
            lsSql &= " make_set(e.upload_status," & gsUploadStatusDesc & ") as 'Upload Status',"
            lsSql &= " a.received_mode as 'Received Mode',"
            lsSql &= " d.courier_name as 'Courier Name',"
            lsSql &= " a.awb_no as 'Awb No',"
            lsSql &= " a.approved_date as 'Approved Date',"
            lsSql &= " a.inward_valid as 'Inward Valid',"
            lsSql &= " a.inward_completed as 'Inward Completed',"
            lsSql &= " a.update_completed as 'Update Completed',"
            lsSql &= " a.share_count as 'Transfer Share Count',"
            lsSql &= " a.share_value as 'Transfer Share Value',"
            lsSql &= " a.stamp_duty as 'Transfer Stamp Duty',"
            lsSql &= " a.execution_date as 'Transfer Execution Date',"
            lsSql &= " a.witness_name as 'Transfer Witness Name',"
            lsSql &= " a.witness_addr as 'Transfer Witness Address',"
            lsSql &= " a.tran_folio_gid as 'Tran Folio Id',"
            lsSql &= " a.tran_cert_no as 'Tran Cert No',"
            lsSql &= " a.tran_cert_gid as 'Tran Cert Id',"
            lsSql &= " a.transfer_no as 'Transfer No',"
            lsSql &= " a.objx_no as 'Objx No',"
            lsSql &= " a.queue_status as 'Queue Status Value',"
            lsSql &= " a.queue_all_status as 'Queue All Status',"
            lsSql &= " e.upload_status as 'Upload Status Value',"
            lsSql &= " a.split_formula as 'Split Formula',"
            lsSql &= " a.chklst_valid as 'Chklst Valid',"
            lsSql &= " a.chklst_disc as 'Chklst Disc',"
            lsSql &= " a.lockin_period_from as 'Lockin Period From',"
            lsSql &= " a.lockin_period_to as 'Lockin Period To',"
            lsSql &= " a.insert_date as 'Inward Insert Date',"
            lsSql &= " a.insert_by as 'Inward Insert By',"
            lsSql &= " a.update_date as 'Inward Update Date',"
            lsSql &= " a.update_by as 'Inward Update By',"
            lsSql &= " f.outward_date as 'Outward Date',"
            lsSql &= " f.outward_mode as 'Outward Mode',"
            lsSql &= " g.courier_name as 'Outward Courier Name',"
            lsSql &= " f.awb_no as 'Outward Awb No',"
            lsSql &= " f.outward_remark as 'Outward Remark',"
            lsSql &= " f.insert_date as 'Outward Insert Date',"
            lsSql &= " f.insert_by as 'Outward Insert By',"
            lsSql &= " f.update_date as 'Outward Update Date',"
            lsSql &= " f.update_by as 'Outward Update By',"
            lsSql &= " a.nominee_assign_flag as 'Nominee Assign Flag',"
            lsSql &= " a.isr_sno as 'ISR Sno',"
            lsSql &= " case when a.stamp_duty_flag = 'Y' then 'Yes' else 'No' end as 'Stamp Duty Flag',"
            lsSql &= " a.inward_gid as 'Inward Id', "
            lsSql &= " a.queue_gid as 'Queue Id',"
            lsSql &= " a.outward_gid as 'Outward Id',"
            lsSql &= " a.folio_gid as 'Folio Id',"
            lsSql &= " i.cert_gid as 'Cert Id',"
            lsSql &= " h.certsplitentry_gid as 'Cert Entry Id',"
            lsSql &= " a.dematpend_gid as 'Demat Pend Id',"
            lsSql &= " a.reason_gid as 'Reason Id',"
            lsSql &= " a.upload_gid as 'Upload Id',"
            lsSql &= " a.courier_gid as 'Inward Courier Id',"
            lsSql &= " f.courier_gid as 'Outward Courier Id',"
            lsSql &= " o.signature_gid as 'Signature Id' "
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on c.comp_gid = a.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as d on d.courier_gid = a.courier_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as e on a.upload_gid = e.upload_gid and e.delete_flag = 'N' "
            lsSql &= " left join sta_trn_toutward as f on a.inward_gid = f.inward_gid and f.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as g on f.courier_gid = g.courier_gid and g.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcertsplitentry as h on a.inward_gid = h.inward_gid and h.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcert as i on h.new_cert_gid = i.cert_gid and i.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as l on a.tran_folio_gid = l.folio_gid and l.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as o on a.folio_gid = o.folio_gid and o.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tdocsubtype as p on p.docsubtype_code = a.docsubtype_code and p.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinwarddocsubtype as q on a.tran_code = q.trantype_code "
            lsSql &= " and a.inward_gid = q.inward_gid  and q.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.inward_gid,i.cert_no asc"
        Else
            lsSql = ""
            lsSql &= " select Distinct"
            lsSql &= " a.received_date as 'Received Date',"
            lsSql &= " e.meeting_date as 'Meeting Date',"
            lsSql &= " e.status_update_date as 'Confirm Date',"
            lsSql &= " a.inward_no as 'Auto Inward No',"
            lsSql &= " a.inward_comp_no as 'Inward No',"
            lsSql &= " c.comp_name as 'Company',"
            lsSql &= " c.isin_id as 'ISIN No',"
            lsSql &= " a.folio_no as 'Folio No',"
            lsSql &= " a.shareholder_name as 'Share Holder',"
            lsSql &= " a.inward_share_count as 'Inward share count',"
            lsSql &= " a.market_price as 'Market Price',"
            lsSql &= " a.market_value as 'Market Value',"
            lsSql &= " a.threshold_level as 'Threshold Level',"
            lsSql &= " a.tran_code as 'Tran Code',"
            lsSql &= " b.trantype_desc as 'Document Type',"
            'lsSql &= " p.docsubtype_desc as 'Document SubType',"
            lsSql &= " fn_get_docsubtypedesc(a.inward_gid,a.tran_code) as 'Document SubType',"
            lsSql &= " l.folio_no as 'To Folio No',"
            lsSql &= " l.holder1_name as 'To Share Holder',"
            lsSql &= " if(a.chklst_disc = 0,'Valid','Disc') as 'Doc Status',"
            lsSql &= " concat(if(a.chklst_disc = 0,'Valid ','Disc '),make_set(a.inward_status," & gsInwardStatusDesc & ")) as 'Status',"
            lsSql &= " make_set(a.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
            lsSql &= " make_set(a.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
            lsSql &= " make_set(e.upload_status," & gsUploadStatusDesc & ") as 'Upload Status',"
            lsSql &= " a.received_mode as 'Received Mode',"
            lsSql &= " d.courier_name as 'Courier Name',"
            lsSql &= " a.awb_no as 'Awb No',"
            lsSql &= " a.approved_date as 'Approved Date',"
            lsSql &= " a.inward_valid as 'Inward Valid',"
            lsSql &= " a.inward_completed as 'Inward Completed',"
            lsSql &= " a.update_completed as 'Update Completed',"
            lsSql &= " a.share_count as 'Transfer Share Count',"
            lsSql &= " a.share_value as 'Transfer Share Value',"
            lsSql &= " a.stamp_duty as 'Transfer Stamp Duty',"
            lsSql &= " a.execution_date as 'Transfer Execution Date',"
            lsSql &= " a.witness_name as 'Transfer Witness Name',"
            lsSql &= " a.witness_addr as 'Transfer Witness Address',"
            lsSql &= " a.tran_folio_gid as 'Tran Folio Id',"
            lsSql &= " a.tran_cert_no as 'Tran Cert No',"
            lsSql &= " a.tran_cert_gid as 'Tran Cert Id',"
            lsSql &= " a.transfer_no as 'Transfer No',"
            lsSql &= " a.objx_no as 'Objx No',"
            lsSql &= " a.queue_status as 'Queue Status Value',"
            lsSql &= " a.queue_all_status as 'Queue All Status',"
            lsSql &= " e.upload_status as 'Upload Status Value',"
            lsSql &= " a.split_formula as 'Split Formula',"
            lsSql &= " a.chklst_valid as 'Chklst Valid',"
            lsSql &= " a.chklst_disc as 'Chklst Disc',"
            lsSql &= " a.lockin_period_from as 'Lockin Period From',"
            lsSql &= " a.lockin_period_to as 'Lockin Period To',"
            lsSql &= " a.insert_date as 'Inward Insert Date',"
            lsSql &= " a.insert_by as 'Inward Insert By',"
            lsSql &= " a.update_date as 'Inward Update Date',"
            lsSql &= " a.update_by as 'Inward Update By',"
            lsSql &= " f.outward_date as 'Outward Date',"
            lsSql &= " f.outward_mode as 'Outward Mode',"
            lsSql &= " g.courier_name as 'Outward Courier Name',"
            lsSql &= " f.awb_no as 'Outward Awb No',"
            lsSql &= " f.outward_remark as 'Outward Remark',"
            lsSql &= " f.insert_date as 'Outward Insert Date',"
            lsSql &= " f.insert_by as 'Outward Insert By',"
            lsSql &= " f.update_date as 'Outward Update Date',"
            lsSql &= " f.update_by as 'Outward Update By',"
            lsSql &= " a.nominee_assign_flag as 'Nominee Assign Flag',"
            lsSql &= " a.isr_sno as 'ISR Sno',"
            lsSql &= " case when a.stamp_duty_flag = 'Y' then 'Yes' else 'No' end as 'Stamp Duty Flag',"
            lsSql &= " a.inward_gid as 'Inward Id', "
            lsSql &= " a.queue_gid as 'Queue Id',"
            lsSql &= " a.outward_gid as 'Outward Id',"
            lsSql &= " a.folio_gid as 'Folio Id',"
            lsSql &= " a.dematpend_gid as 'Demat Pend Id',"
            lsSql &= " a.reason_gid as 'Reason Id',"
            lsSql &= " a.upload_gid as 'Upload Id',"
            lsSql &= " a.courier_gid as 'Inward Courier Id',"
            lsSql &= " f.courier_gid as 'Outward Courier Id',"
            lsSql &= " o.signature_gid as 'Signature Id' "
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcompany as c on c.comp_gid = a.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as d on d.courier_gid = a.courier_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as e on a.upload_gid = e.upload_gid and e.delete_flag = 'N' "
            lsSql &= " left join sta_trn_toutward as f on a.inward_gid = f.inward_gid and f.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as g on f.courier_gid = g.courier_gid and g.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as l on a.tran_folio_gid = l.folio_gid and l.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as o on a.folio_gid = o.folio_gid and o.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tdocsubtype as p on p.docsubtype_code = a.docsubtype_code and p.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinwarddocsubtype as q on a.tran_code = q.trantype_code "
            lsSql &= " and a.inward_gid = q.inward_gid  and q.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.inward_gid asc"
        End If

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        Dim lnInwardId As Long
        Dim objFrm As frmDocHistory

        If e.RowIndex >= 0 Then
            lnInwardId = dgvList.Rows(e.RowIndex).Cells("Inward Id").Value
            objFrm = New frmDocHistory(lnInwardId)
            objFrm.ShowDialog()
        End If
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

    Public Sub New(UploadId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnUploadId = UploadId
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        DocSubType.DataSource = Nothing
    End Sub

    Public Function GetDocsubtypeDT() As DataTable
        '' Create new DataTable instance.
        Dim dt As DataTable

        Dim lsSql As String
        If cboDocType.SelectedValue <> Nothing Then
            lsSql = ""
            lsSql &= " select docsubtype_code,docsubtype_desc from sta_mst_tdocsubtype "
            lsSql &= " where delete_flag = 'N' "
            lsSql &= " and trantype_code = '" & cboDocType.SelectedValue.ToString & "' "
            lsSql &= " order by docsubtype_code asc "

            dt = GetDataTable(lsSql)
            Return dt

        End If
    End Function

  
    Private Sub cboDocType_Leave(sender As Object, e As EventArgs) Handles cboDocType.Leave
        Dim dt1 As DataTable = GetDocsubtypeDT()

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            'Assign DataTable as DataSource.
            DocSubType.DataSource = dt1
            DocSubType.DisplayMember = "docsubtype_desc"
            DocSubType.ValueMember = "docsubtype_code"
        Else
            DocSubType.DataSource = Nothing
        End If
        DocSubType.CheckOnClick = True
    End Sub
End Class