Public Class frmCertificatePrePrintReport
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub frmCertificatePrePrintReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lsTranType As String = ""

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If txtppNo.Text <> "" Then lsCond &= " and b.pp_no = '" & Val(txtppNo.Text.ToString) & "' "

        If cboPPstatus.Text <> "" Then
            Select Case cboPPstatus.Text.ToUpper
                Case "NOT UTILISED"
                    lsCond &= " and b.pp_status = 'B' "
                Case "UTILISED"
                    lsCond &= " and b.pp_status = 'P' "
                Case "CANCELLED"
                    lsCond &= " and b.pp_status = 'C' "
            End Select
        End If

        lsSql = ""
        lsSql &= " select "
        lsSql &= " c.comp_name as 'Company',"
        lsSql &= " b.pp_no as 'Pre Print No.',"
        lsSql &= " CASE WHEN b.pp_status = 'B' THEN 'Not Utilised' WHEN b.pp_status = 'P' THEN 'Utilised' WHEN b.pp_status = 'C' THEN 'Cancelled' end  as 'Status',"
        lsSql &= " g.folio_no as 'Folio No',"
        lsSql &= " date_format(b.issue_date,'%d-%m-%Y') as 'Issue Date',"
        lsSql &= " g.holder1_name as 'Name',"
        lsSql &= " b.cert_gid as 'New Certificate No',"
        lsSql &= " group_concat(DISTINCT f.cert_no) as 'old Certificate No',"
        'lsSql &= " group_concat(concat(cast(i.dist_from As nchar),'-',cast(i.dist_to as nchar))) as 'Distinctive Nos',"
        lsSql &= " h.trantype_desc as 'Inward Type',"
        lsSql &= " d.inward_no as 'Auto Inward No',"
        lsSql &= " d.inward_comp_no as 'Comp Inward No',"
        lsSql &= " b.pp_remark as 'Remarks',"
        lsSql &= " b.pp_sign as 'User Name',"
        lsSql &= " fn_sta_get_certdist(b.cert_gid) as 'Distinctive Nos'"

        lsSql &= " from sta_trn_tcertpphead as a "
        lsSql &= " inner join sta_trn_tcertpp as b on a.certpphead_gid = b.certpphead_gid and a.comp_gid=b.comp_gid  and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid=c.comp_gid  and c.delete_flag = 'N' "
        lsSql &= " left join sta_trn_tinward as d on b.inward_gid = d.inward_gid and b.comp_gid=d.comp_gid and d.delete_flag = 'N' "
        lsSql &= " left join sta_trn_tcertentry as e on d.inward_gid = e.inward_gid and e.delete_flag = 'N' "
        lsSql &= " left join sta_trn_tcert as f on e.cert_gid = f.cert_gid and f.delete_flag = 'N' "
        'lsSql &= " left join sta_trn_tcertdist as i on f.cert_gid = i.cert_gid and i.delete_flag = 'N' "
        lsSql &= " left join sta_trn_tfolio as g on d.folio_gid = g.folio_gid and g.delete_flag = 'N' "
        lsSql &= " left join sta_mst_ttrantype as h on d.tran_code = h.trantype_code and h.delete_flag = 'N' "


        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " group by b.pp_no,b.pp_status,b.issue_date,b.pp_remark,b.pp_sign,b.inward_gid,b.cert_gid,h.trantype_desc,g.folio_no,g.holder1_name,d.inward_comp_no "
        lsSql &= " order by b.pp_no asc "

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        dgvList.AutoResizeColumns()

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Certificate PP Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class