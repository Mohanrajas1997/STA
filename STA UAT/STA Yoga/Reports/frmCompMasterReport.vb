Public Class frmCompMasterReport

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Public Sub frmCtrClear(ByVal frmName As Object)
        Dim ctrl As Control
        Dim dtp As DateTimePicker

        For Each ctrl In frmName.Controls
            If ctrl.Tag <> "*" Then
                If TypeOf ctrl Is TextBox Then ctrl.Text = ""
                If TypeOf ctrl Is ComboBox Then
                    ctrl.Text = ""
                End If

                If TypeOf ctrl Is DateTimePicker Then
                    dtp = ctrl

                    If dtp.ShowCheckBox = True Then
                        dtp.Checked = False
                    End If
                End If

                If TypeOf ctrl Is Panel Then frmCtrClear(ctrl)
                If TypeOf ctrl Is GroupBox Then frmCtrClear(ctrl)
            End If
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvReport, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmCompMasterReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvReport
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvReport.Top + dgvReport.Height + 1
        pnlExport.Width = Me.Width
        btnExport.Left = pnlExport.Width - btnExport.Width - 32
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If
        If cboEntryMode.Text <> "" And cboEntryMode.SelectedIndex <> -1 Then
            lsCond &= " and a.entry_mode = '" & cboEntryMode.SelectedItem.ToString & "' "
        End If
        If txtISIN.Text <> "" Then lsCond &= " and a.isin_id like '" & QuoteFilter(txtISIN.Text) & "%' "
        If txtPanno.Text <> "" Then lsCond &= " and a.pan_no like '" & QuoteFilter(txtPanno.Text) & "%' "


        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql = ""
        lsSql &= " select distinct "
        lsSql &= " a.comp_code as 'Company Code',"
        lsSql &= " a.comp_name as 'Company Name',"
        lsSql &= " c.compgrp_name as 'Company Group Name',"
        lsSql &= " d.compsubgrp_name as 'Company Sub Group Name',"
        lsSql &= " a.isin_id as 'ISIN',"
        lsSql &= " a.comp_listed as 'Comp Listed',"
        lsSql &= " a.active_flag as 'Active Flag',"
        lsSql &= " a.cin_no as 'CIN No',"
        lsSql &= " a.pan_no as 'PAN No',"
        lsSql &= " a.share_type as 'Share Type',"
        lsSql &= " a.share_qty as 'Share Quantity',"
        lsSql &= " a.paid_up_value as 'Paid Up Value',"
        'lsSql &= " a.share_captial as 'Share Capital',"
        lsSql &= " (a.share_qty * a.paid_up_value) as 'Share Capital',"
        lsSql &= " a.contact_person as 'Contact Person',"
        lsSql &= " a.contact_no as 'Contact No',"
        lsSql &= " a.email_id as 'Email ID',"
        lsSql &= " ifnull(a.start_date,'') as 'Start Date',"
        lsSql &= " ifnull(a.maturity_date,'') as 'Maturity Date',"
        lsSql &= " a.electronics_flag as 'Electronic RTA',"
        lsSql &= " a.address1 as 'Address1',"
        lsSql &= " a.address2 as 'Address2',"
        lsSql &= " a.city as City,"
        lsSql &= " a.state as State,"
        lsSql &= " a.country as Country,"
        lsSql &= " a.pincode as Pincode,"
        lsSql &= " fn_get_verifyattachment(a.comp_gid,7) as 'STA AGREEMENT(Attach)',"
        lsSql &= " fn_get_verifyattachment(a.comp_gid,3) as 'FINAL MCF(Attach)',"
        lsSql &= " fn_get_verifyattachment(a.comp_gid,4)  as 'TPA(Attach)',"
        lsSql &= " fn_get_verifyattachment(a.comp_gid,5) as 'ISIN ACTIVATION(Attach)',"
        lsSql &= " fn_get_verifyattachment(a.comp_gid,6) as 'OTHERS(Attach)',"
        lsSql &= " a.folio_no_format as 'Folio No Format',"
        lsSql &= " a.folio_prefix_flag as 'Folio Prefix',"
        lsSql &= " a.folio_prefix_sno_flag as 'Folio Prefix Sno',"
        lsSql &= " a.folio_prefix as 'Folio Prefix',"
        lsSql &= " a.folio_prefix_field as 'Folio Prefix Field',"
        lsSql &= " a.folio_prefix_length as 'Folio Prefix Length',"
        lsSql &= " a.upload_sno as 'Upload Sno',"
        lsSql &= " a.folio_sno as 'Folio Sno',"
        lsSql &= " a.transfer_sno as 'Transfer Sno',"
        lsSql &= " a.cert_sno as 'Cert Sno',"
        lsSql &= " a.objx_sno as 'Objx Sno',"
        lsSql &= " a.cdsl_sno as 'CDSL Sno',"
        lsSql &= " a.nsdl_sno as 'NSDL Sno',"
        lsSql &= " a.inward_sno as 'Inward Sno',"
        lsSql &= " a.depository_code as 'Depository Type',"
        lsSql &= " a.entry_mode as 'Entry Mode',"
        'lsSql &= " a.isr_sno as 'ISR Sno',"
        lsSql &= " a.comp_gid, "
        lsSql &= " a.compgrp_gid, "
        lsSql &= " a.compsubgrp_gid "
        lsSql &= " from sta_mst_tcompany as a "
        lsSql &= " left join sta_trn_tcompanyattachment as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tcompanygroup as c on a.compgrp_gid = c.compgrp_gid and c.delete_flag = 'N' "
        lsSql &= " left join sta_mst_tcompanysubgroup as d on a.compsubgrp_gid = d.compsubgrp_gid and c.compgrp_gid = d.compgrp_gid and d.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "


        gpPopGridView(dgvReport, lsSql, gOdbcConn)

        For i = 0 To dgvReport.ColumnCount - 1
            dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtRecCount.Text = "Total Records : " & dgvReport.RowCount.ToString
    End Sub

    Private Sub frmCompMasterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
    End Sub
End Class