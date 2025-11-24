Imports MySql.Data.MySqlClient

Public Class frmSearchEngine
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim lnCompId As Long = 0
        Dim lsFolioNo As String = ""
        Dim lnCertNo As Long = 0

        Dim i As Integer = 0
        Dim n As Integer = 0
        Dim lnResult As Long = 0

        Dim dgvRow As DataGridViewRow = Nothing

        Try
            If cboCompany.Text = "" Or cboCompany.SelectedIndex = -1 Then
                MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboCompany.Focus()
                Exit Sub
            End If

            lnCompId = Val(cboCompany.SelectedValue.ToString)
            lsFolioNo = QuoteFilter(txtFolioNo.Text)
            lnCertNo = Val(txtCertNo.Text)

            If lsFolioNo = "" And lnCertNo = 0 Then
                MessageBox.Show("Please input folio/certificate no !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtFolioNo.Focus()
                Exit Sub
            End If

            dgvDematPend.DataSource = Nothing
            dgvCertDist.DataSource = Nothing
            dgvFolio.DataSource = Nothing
            dgvFolioTran.DataSource = Nothing
            dgvCert.DataSource = Nothing
            dgvInward.DataSource = Nothing

            ' folio
            lsFld = ""
            lsFld &= " b.comp_name as 'Company',"
            lsFld &= " a.folio_no as 'Folio No',"
            lsFld &= " a.holder1_name as 'Holder1',"
            lsFld &= " a.folio_sno as 'Folio SNo',"
            lsFld &= " a.folio_shares as 'Share Count',"
            lsFld &= " a.holder1_fh_name as 'Holder1 F/H Name',"
            lsFld &= " a.holder1_pan_no as 'Holder1 Pan No',"
            lsFld &= " a.holder2_name as 'Holder2',"
            lsFld &= " a.holder2_fh_name as 'Holder2 F/H Name',"
            lsFld &= " a.holder2_pan_no as 'Holder2 Pan No',"
            lsFld &= " a.holder3_name as 'Holder3',"
            lsFld &= " a.holder3_fh_name as 'Holder3 F/H Name',"
            lsFld &= " a.holder3_pan_no as 'Holder3 Pan No',"
            lsFld &= " a.folio_addr1 as 'Addr1',"
            lsFld &= " a.folio_addr2 as 'Addr2',"
            lsFld &= " a.folio_addr3 as 'Addr3',"
            lsFld &= " a.folio_city as 'City',"
            lsFld &= " a.folio_state as 'State',"
            lsFld &= " a.folio_country as 'Country',"
            lsFld &= " a.folio_pincode as 'Pincode',"
            lsFld &= " a.folio_mail_id as 'Email Id',"
            lsFld &= " a.folio_contact_no as 'Contact No',"
            lsFld &= " a.nominee_reg_no as 'Nominee Reg No',"
            lsFld &= " a.nominee_name as 'Nominee Name',"
            lsFld &= " a.nominee_addr1 as 'Nominee Addr1',"
            lsFld &= " a.nominee_addr2 as 'Nominee Addr2',"
            lsFld &= " a.nominee_addr3 as 'Nominee Addr3',"
            lsFld &= " a.nominee_city as 'Nominee City',"
            lsFld &= " a.nominee_state as 'Nominee State',"
            lsFld &= " a.nominee_country as 'Nominee Country',"
            lsFld &= " a.nominee_pincode as 'Nominee Pincode',"
            lsFld &= " a.bank_name as 'Bank Name',"
            lsFld &= " a.bank_acc_no as 'Bank A/C No',"
            lsFld &= " a.bank_ifsc_code as 'IFSC Code',"
            lsFld &= " a.bank_branch as 'Bank Branch',"
            lsFld &= " a.bank_beneficiary as 'Bank Beneficiary',"
            lsFld &= " a.bank_acc_type as 'Bank A/C Type',"
            lsFld &= " a.bank_branch_addr as 'Bank Branch',"
            lsFld &= " a.witness_name as 'Witness Name',"
            lsFld &= " a.witness_addr as 'Witness Addr',"
            lsFld &= " a.repatrition_flag as 'Repatrition Flag',"
            lsFld &= " c.category_name as 'Category',"
            lsFld &= " a.insert_date as 'Insert Date',"
            lsFld &= " a.insert_by as 'Insert By',"
            lsFld &= " a.signature_gid as 'Signature Id',"
            lsFld &= " a.comp_gid as 'Comp Id',"
            lsFld &= " a.folio_gid as 'Folio Id' "

            lsCond = ""
            lsCond &= " and a.comp_gid = " & lnCompId & " "

            If lsFolioNo <> "" Then lsCond &= " and a.folio_no = '" & lsFolioNo & "' "
            If lnCertNo > 0 Then
                lsCond &= " and a.folio_gid in (select folio_gid from sta_trn_tcert "
                lsCond &= " where comp_gid = " & lnCompId & " and cert_no = " & lnCertNo & " and delete_flag = 'N')"
            End If

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from sta_trn_tfolio as a "
            lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcategory as c on a.category_gid = c.category_gid and a.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.folio_sno asc"

            Call gpPopGridView(dgvFolio, lsSql, gOdbcConn)

            ' certificate
            lsFld = ""
            lsFld &= " c.comp_name as 'Company',"
            lsFld &= " a.cert_no as 'Cert No',"
            lsFld &= " a.share_count as 'Share Count',"
            lsFld &= " make_set(a.cert_status," & gsCertStatusDesc & ") as 'Status',"
            lsFld &= " b.folio_no as 'Folio No',"
            lsFld &= " b.holder1_name as 'Holder',"
            lsFld &= " a.issue_date as 'Issue Date',"
            lsFld &= " a.lockin_period_from as 'Lockin From',"
            lsFld &= " a.lockin_period_to as 'Lockin To',"
            lsFld &= " a.hold_date as 'Hold Date',"
            lsFld &= " a.hold_release_date as 'Hold Release Date',"
            lsFld &= " a.expired_date as 'Expired Date',"
            lsFld &= " a.cert_remark as 'Remark',"
            lsFld &= " b.folio_sno as 'Folio SNo',"
            lsFld &= " b.holder1_fh_name as 'Holder F/H Name',"
            lsFld &= " b.holder1_pan_no as 'Holder Pan No',"
            lsFld &= " a.cert_status as 'Status Value',"
            lsFld &= " a.comp_gid as 'Comp Id',"
            lsFld &= " a.folio_gid as 'Folio Id',"
            lsFld &= " a.cert_gid as 'Cert Id',"
            lsFld &= " a.file_gid as 'File Id' "

            lsCond = ""
            lsCond &= " and a.comp_gid = " & lnCompId & " "

            If lsFolioNo <> "" Then lsCond &= " and b.folio_no = '" & lsFolioNo & "' "
            If lnCertNo > 0 Then
                lsCond &= " and a.cert_no = " & lnCertNo & " "
            End If

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from sta_trn_tcert as a "
            lsSql &= " inner join sta_trn_tfolio as b on a.folio_gid = b.folio_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and b.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.comp_gid,a.cert_no asc"

            Call gpPopGridView(dgvCert, lsSql, gOdbcConn)

            ' certificate distinctive series
            lsFld = ""
            lsFld &= " c.comp_name as 'Company',"
            lsFld &= " d.dist_from as 'Dist From',"
            lsFld &= " d.dist_to as 'Dist To',"
            lsFld &= " d.dist_count as 'Dist Share Count',"
            lsFld &= " b.folio_no as 'Folio No',"
            lsFld &= " a.cert_no as 'Cert No',"
            lsFld &= " a.share_count as 'Cert Share Count',"
            lsFld &= " b.holder1_name as 'Holder',"
            lsFld &= " make_set(a.cert_status," & gsCertStatusDesc & ") as 'Status',"
            lsFld &= " a.issue_date as 'Issue Date',"
            lsFld &= " a.lockin_period_from as 'Lockin From',"
            lsFld &= " a.lockin_period_to as 'Lockin To',"
            lsFld &= " a.hold_date as 'Hold Date',"
            lsFld &= " a.hold_release_date as 'Hold Release Date',"
            lsFld &= " a.expired_date as 'Expired Date',"
            lsFld &= " a.cert_remark as 'Remark',"
            lsFld &= " b.folio_sno as 'Folio SNo',"
            lsFld &= " b.holder1_fh_name as 'Holder F/H Name',"
            lsFld &= " b.holder1_pan_no as 'Holder Pan No',"
            lsFld &= " a.cert_status as 'Status Value',"
            lsFld &= " a.comp_gid as 'Comp Id',"
            lsFld &= " a.folio_gid as 'Folio Id',"
            lsFld &= " a.cert_gid as 'Cert Id',"
            lsFld &= " d.certdist_gid as 'Cert Dist Id',"
            lsFld &= " d.file_gid as 'File Id' "

            lsCond = ""
            lsCond &= " and a.comp_gid = " & lnCompId & " "

            If lsFolioNo <> "" Then lsCond &= " and b.folio_no = '" & lsFolioNo & "' "
            If lnCertNo > 0 Then
                lsCond &= " and a.cert_no = " & lnCertNo & " "
            End If

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from sta_trn_tcertdist as d "
            lsSql &= " inner join sta_trn_tcert as a on d.cert_gid = a.cert_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tfolio as b on a.folio_gid = b.folio_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and b.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and d.delete_flag = 'N' "
            lsSql &= " order by a.comp_gid,d.dist_from asc"

            Call gpPopGridView(dgvCertDist, lsSql, gOdbcConn)

            ' demat pending
            lsFld = ""
            lsFld &= " c.comp_name as 'Company',"
            lsFld &= " p.depository_name as 'Depository',"
            lsFld &= " d.req_date as 'Req Date',"
            lsFld &= " d.drn_no as 'DRN No',"
            lsFld &= " o.folio_no as 'Folio No',"
            lsFld &= " d.cust_name as 'Customer Name',"
            lsFld &= " d.share_count as 'Share Count',"
            lsFld &= " d.client_id as 'Client Id',"
            lsFld &= " d.dp_id as 'DP Id',"
            lsFld &= " d.dematpend_flag as 'Pending Flag',"
            lsFld &= " i.inward_no as 'Inward No',"
            lsFld &= " i.received_date as 'Received Date',"
            lsFld &= " d.dematpend_gid as 'Demat Pending Id',"
            lsFld &= " d.inward_gid as 'Inward Id',"
            lsFld &= " f.insert_date as 'Import Date',f.file_name as 'File Name',f.sheet_name as 'Sheet Name',"
            lsFld &= " f.insert_by as 'Import By',"
            lsFld &= " f.file_type as 'File Type',"
            lsFld &= " f.file_gid as 'File Id' "

            lsCond = ""
            lsCond &= " and d.comp_gid = " & lnCompId & " "

            If lsFolioNo <> "" Then lsCond &= " and i.folio_no = '" & lsFolioNo & "' "
            If lnCertNo > 0 Then
                lsCond &= " and i.folio_gid in (select folio_gid from sta_trn_tcert "
                lsCond &= " where comp_gid = " & lnCompId & " and cert_no = " & lnCertNo & " and delete_flag = 'N')"
            End If

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from sta_trn_tdematpend as d "
            lsSql &= " inner join sta_trn_tfile as f on d.file_gid = f.file_gid and f.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on d.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tdepository as p on d.depository_code = p.depository_code and p.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tinward as i on d.inward_gid = i.inward_gid and i.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tfolio as o on i.folio_gid = o.folio_gid and o.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and d.delete_flag = 'N' "
            lsSql &= " order by d.dematpend_gid desc"

            Call gpPopGridView(dgvDematPend, lsSql, gOdbcConn)

            ' folio tran
            lsFld = ""
            lsFld &= " c.comp_name as 'Company',"
            lsFld &= " a.tran_date as 'Tran Date',"
            lsFld &= " a.tran_desc as 'Tran Desc',"
            lsFld &= " a.tran_count as 'Tran Count',"
            lsFld &= " a.tran_mode as 'Tran Mode',"
            lsFld &= " a.mult as 'Mult',"
            lsFld &= " a.tran_remark as 'Tran Remark',"
            lsFld &= " b.folio_no as 'Folio No',"
            lsFld &= " b.holder1_name as 'Customer Name',"
            lsFld &= " e.folio_no as 'Ref Folio No',"
            lsFld &= " e.holder1_name as 'Ref Customer Name',"
            lsFld &= " d.inward_no as 'Inward No',"
            lsFld &= " d.received_date as 'Received Date',"
            lsFld &= " d.tran_code as 'Doc Tran Code',"
            lsFld &= " a.foliotran_gid as 'Folio Tran Id',"
            lsFld &= " a.inward_gid as 'Inward Id',"
            lsFld &= " a.folio_gid as 'Folio Id',"
            lsFld &= " a.ref_folio_gid as 'Ref Folio Id' "

            lsCond = ""
            lsCond &= " and b.comp_gid = " & lnCompId & " "

            If lsFolioNo <> "" Then lsCond &= " and b.folio_no = '" & lsFolioNo & "' "
            If lnCertNo > 0 Then
                lsCond &= " and a.folio_gid in (select folio_gid from sta_trn_tcert "
                lsCond &= " where comp_gid = " & lnCompId & " and cert_no = " & lnCertNo & " and delete_flag = 'N')"
            End If

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from sta_trn_tfoliotran as a "
            lsSql &= " inner join sta_trn_tfolio as b on a.folio_gid = b.folio_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on b.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinward as d on a.inward_gid = d.inward_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as e on a.ref_folio_gid = e.folio_gid and e.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.foliotran_gid"

            gpPopGridView(dgvFolioTran, lsSql, gOdbcConn)

            ' Inward
            lsFld = ""
            lsFld &= " c.comp_name as 'Company',"
            lsFld &= " a.received_date as 'Rececived Date',"
            lsFld &= " a.inward_no as 'Inward No',"
            lsFld &= " a.folio_no as 'Folio No',"
            lsFld &= " a.shareholder_name as 'Share Holder',"
            lsFld &= " a.tran_code as 'Tran Code',"
            lsFld &= " b.trantype_desc as 'Document',"
            lsFld &= " make_set(a.inward_status," & gsInwardStatusDesc & ") as 'Inward Status',"
            lsFld &= " make_set(a.queue_status," & gsQueueStatusDesc & ") as 'Queue Status',"
            lsFld &= " make_set(e.upload_status," & gsUploadStatusDesc & ") as 'Upload Status',"
            lsFld &= " a.received_mode as 'Received Mode',"
            lsFld &= " d.courier_name as 'Courier Name',"
            lsFld &= " a.awb_no as 'Awb No',"
            lsFld &= " a.approved_date as 'Approved Date',"
            lsFld &= " a.inward_valid as 'Inward Valid',"
            lsFld &= " a.inward_completed as 'Inward Completed',"
            lsFld &= " a.update_completed as 'Update Completed',"
            lsFld &= " a.share_count as 'Transfer Share Count',"
            lsFld &= " a.share_value as 'Transfer Share Value',"
            lsFld &= " a.stamp_duty as 'Transfer Stamp Duty',"
            lsFld &= " a.execution_date as 'Transfer Execution Date',"
            lsFld &= " a.witness_name as 'Transfer Witness Name',"
            lsFld &= " a.witness_addr as 'Transfer Witness Address',"
            lsFld &= " a.tran_folio_gid as 'Tran Folio Id',"
            lsFld &= " a.tran_cert_no as 'Tran Cert No',"
            lsFld &= " a.tran_cert_gid as 'Tran Cert Id',"
            lsFld &= " a.transfer_no as 'Transfer No',"
            lsFld &= " a.objx_no as 'Objx No',"
            lsFld &= " a.queue_status as 'Queue Status Value',"
            lsFld &= " a.queue_all_status as 'Queue All Status',"
            lsFld &= " e.upload_status as 'Upload Status Value',"
            lsFld &= " a.split_formula as 'Split Formula',"
            lsFld &= " a.chklst_valid as 'Chklst Valid',"
            lsFld &= " a.chklst_disc as 'Chklst Disc',"
            lsFld &= " a.lockin_period_from as 'Lockin Period From',"
            lsFld &= " a.lockin_period_to as 'Lockin Period To',"
            lsFld &= " a.insert_date as 'Inward Insert Date',"
            lsFld &= " a.insert_by as 'Inward Insert By',"
            lsFld &= " a.update_date as 'Inward Update Date',"
            lsFld &= " a.update_by as 'Inward Update By',"
            lsFld &= " f.outward_date as 'Outward Date',"
            lsFld &= " f.outward_mode as 'Outward Mode',"
            lsFld &= " g.courier_name as 'Outward Courier Name',"
            lsFld &= " f.awb_no as 'Outward Awb No',"
            lsFld &= " f.outward_remark as 'Outward Remark',"
            lsFld &= " f.insert_date as 'Outward Insert Date',"
            lsFld &= " f.insert_by as 'Outward Insert By',"
            lsFld &= " f.update_date as 'Outward Update Date',"
            lsFld &= " f.update_by as 'Outward Update By',"
            lsFld &= " a.inward_gid as 'Inward Id', "
            lsFld &= " a.queue_gid as 'Queue Id',"
            lsFld &= " a.outward_gid as 'Outward Id',"
            lsFld &= " a.folio_gid as 'Folio Id',"
            lsFld &= " a.dematpend_gid as 'Demat Pend Id',"
            lsFld &= " a.reason_gid as 'Reason Id',"
            lsFld &= " a.upload_gid as 'Upload Id',"
            lsFld &= " a.courier_gid as 'Inward Courier Id',"
            lsFld &= " f.courier_gid as 'Outward Courier Id' "

            lsCond = ""
            lsCond &= " and a.comp_gid = " & lnCompId & " "

            If lsFolioNo <> "" Then lsCond &= " and a.folio_no = '" & lsFolioNo & "' "
            If lnCertNo > 0 Then
                lsCond &= " and a.folio_gid in (select folio_gid from sta_trn_tcert "
                lsCond &= " where comp_gid = " & lnCompId & " and cert_no = " & lnCertNo & " and delete_flag = 'N')"
            End If

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on c.comp_gid = a.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as d on d.courier_gid = a.courier_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as e on a.upload_gid = e.upload_gid and e.delete_flag = 'N' "
            lsSql &= " left join sta_trn_toutward as f on a.inward_gid = f.inward_gid and f.delete_flag = 'N' "
            lsSql &= " left join sta_mst_tcourier as g on f.courier_gid = g.courier_gid and g.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.inward_gid asc"

            Call gpPopGridView(dgvInward, lsSql, gOdbcConn)

            ' Count
            lblCertCount.Text = dgvCert.Rows.Count
            lblCertDistCount.Text = dgvCertDist.Rows.Count
            lblInwardCount.Text = dgvInward.Rows.Count
            lblTranCount.Text = dgvFolioTran.Rows.Count
            lblDematPendCount.Text = dgvDematPend.Rows.Count

            ' Sorting Remove
            With dgvCert
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvDematPend
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvCertDist
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvFolioTran
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvInward
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtCertNo.Text = ""
        txtFolioNo.Text = ""

        dgvDematPend.DataSource = Nothing
        dgvCertDist.DataSource = Nothing
        dgvFolio.DataSource = Nothing
        dgvFolioTran.DataSource = Nothing
        dgvCert.DataSource = Nothing
        dgvInward.DataSource = Nothing
        txtFolioNo.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmSearchEngine_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        cboCompany.Focus()
    End Sub

    Private Sub frmSearchEngine_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        If txtFolioNo.Text.Trim <> "" Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub frmSearchEngine_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim llHeight As Long
        Dim llWidth As Long
        Dim llTop As Long

        llHeight = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - 16)
        llHeight = (llHeight - 48 - (lblFolio.Height * 4) - 6 * 4) \ 4

        llWidth = Me.Width - 30

        llTop = pnlMain.Top + pnlMain.Height + 6

        lblFolio.Top = llTop
        lblFolio.Left = pnlMain.Left
        llTop = llTop + lblFolio.Height + 6

        dgvFolio.Top = llTop
        dgvFolio.Height = llHeight
        dgvFolio.Width = llWidth
        dgvFolio.Left = pnlMain.Left
        llTop = llTop + llHeight + 6

        lblCert.Top = llTop
        lblCert.Left = pnlMain.Left

        lblCertCount.Top = lblCert.Top
        lblCertCount.Left = lblCert.Left + lblCert.Width + 12

        llTop = llTop + lblFolio.Height + 6

        dgvCert.Top = llTop
        dgvCert.Height = llHeight
        dgvCert.Left = pnlMain.Left
        dgvCert.Width = llWidth

        llTop = llTop + llHeight + 6

        lblCertDist.Top = llTop
        lblCertDist.Left = pnlMain.Left

        lblCertDistCount.Top = lblCertDist.Top
        lblCertDistCount.Left = lblCertDist.Left + lblCertDist.Width + 12

        llTop = llTop + lblFolio.Height + 6
        llWidth = (llWidth - 4) \ 2

        dgvCertDist.Top = llTop
        dgvCertDist.Height = llHeight
        dgvCertDist.Left = pnlMain.Left
        dgvCertDist.Width = llWidth

        lblTran.Top = lblCertDistCount.Top
        lblTran.Left = dgvCertDist.Left + dgvCertDist.Width + 2

        lblTranCount.Top = lblCertDistCount.Top
        lblTranCount.Left = lblTran.Left + lblTran.Width + 12

        dgvFolioTran.Top = llTop
        dgvFolioTran.Height = llHeight
        dgvFolioTran.Left = dgvCertDist.Left + dgvCertDist.Width + 2
        dgvFolioTran.Width = dgvCertDist.Width

        llTop = llTop + llHeight + 6

        'llWidth = (llWidth - 6) \ 3

        lblInward.Top = llTop
        lblInward.Left = pnlMain.Left

        lblInwardCount.Top = lblInward.Top
        lblInwardCount.Left = lblInward.Left + lblInward.Width + 12

        lblDematPend.Top = lblInward.Top
        lblDematPend.Left = pnlMain.Left + llWidth + 2

        lblDematPendCount.Top = lblDematPend.Top
        lblDematPendCount.Left = lblDematPend.Left + lblDematPend.Width + 12

        llTop = llTop + lblFolio.Height + 6

        dgvInward.Top = llTop
        dgvInward.Height = llHeight
        dgvInward.Left = pnlMain.Left
        dgvInward.Width = llWidth

        dgvDematPend.Top = llTop
        dgvDematPend.Height = llHeight
        dgvDematPend.Left = dgvInward.Left + dgvInward.Width + 2
        dgvDematPend.Width = dgvInward.Width
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGViewXML(dgvFolio, gsReportPath & "\Report.xls", "Folio", "", True)
            PrintDGViewXML(dgvCert, gsReportPath & "\Report.xls", "Certificate", "", False)
            PrintDGViewXML(dgvCertDist, gsReportPath & "\Report.xls", "Certificate Distinctive Series", "", False)
            PrintDGViewXML(dgvDematPend, gsReportPath & "\Report.xls", "Demat Pending", "", False)
            PrintDGViewXML(dgvFolioTran, gsReportPath & "\Report.xls", "Folio Tran", "", False)
            PrintDGViewXML(dgvInward, gsReportPath & "\Report.xls", "Inward", "", False)

            Call gpOpenFile(gsReportPath & "\Report.xls")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class