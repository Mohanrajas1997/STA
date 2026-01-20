Public Class frmInwardDashboardReport

    Private Sub frmInwardDashboardReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFrom.Value = DateAdd(DateInterval.Day, -1, Now)
        dtpTo.Value = Now
    End Sub

    Private Sub frmInwardDashboardReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

        If dtpFrom.Checked = False Then
            MessageBox.Show("Please select the Inward From date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpFrom.Focus()
            Exit Sub
        End If

        If dtpTo.Checked = False Then
            MessageBox.Show("Please select the Inward To date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpTo.Focus()
            Exit Sub
        End If

        If dtpFrom.Checked = True Then lsCond &= " and a.received_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and a.received_date <= '" & Format(dtpTo.Value, "yyyy-MM-dd") & "' "

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= "  b.trantype_code as DocType,"
        lsSql &= "  '' as DocSubType,"
        lsSql &= "  concat(b.trantype_code,' / ',b.trantype_desc) as Type,"
        lsSql &= " fn_get_inwardopeningbal(a.tran_code,'','" & Format(dtpFrom.Value, "yyyy-MM-dd") & "') as `Opening Bal` ,"
        lsSql &= " count(a.tran_code) as Received,"
        lsSql &= " fn_get_inwardcompleted(a.tran_code,'','" & Format(dtpFrom.Value, "yyyy-MM-dd") & "','" & Format(dtpTo.Value, "yyyy-MM-dd") & "') as Processed,"
        lsSql &= " fn_get_inwardopeningbal(a.tran_code,'','" & Format(dtpFrom.Value, "yyyy-MM-dd") & "') + count(a.tran_code) - fn_get_inwardcompleted(a.tran_code,'','" & Format(dtpFrom.Value, "yyyy-MM-dd") & "','" & Format(dtpTo.Value, "yyyy-MM-dd") & "') as `Closing Bal`"
        'lsSql &= " SUM(fn_get_date_range_category(a.received_date) = '1-5 Days') AS '1-5 Days',"
        'lsSql &= " SUM(fn_get_date_range_category(a.received_date) = '6-10 Days') AS '6-10 Days',"
        'lsSql &= " SUM(fn_get_date_range_category(a.received_date) = '11-15 Days') AS '11-15 Days',"
        'lsSql &= " SUM(fn_get_date_range_category(a.received_date) = '16-21 Days') AS '16-21 Days',"
        'lsSql &= " SUM(fn_get_date_range_category(a.received_date) = '22-30 Days') AS '22-30 Days',"
        'lsSql &= " SUM(fn_get_date_range_category(a.received_date) = '>30 Days') AS '>30 Days'"
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as b on a.tran_code = b.trantype_code and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
        lsSql &= " left join sta_trn_tfolio as d on a.folio_gid = d.folio_gid and d.delete_flag = 'N' "
        lsSql &= " where a.tran_code in ('DM','LS','IS','TM','OT','CD') "
        lsSql &= " and a.delete_flag = 'N' "
        'lsSql &= " and a.received_date >= " & Format(dtpFrom.Value, "yyyy-MM-dd") & " and a.received_date <= " & Format(dtpTo.Value, "yyyy-MM-dd") & ""
        lsSql &= lsCond
        lsSql &= " group by a.tran_code "

        'lsSql &= "Union All"
        'lsSql &= " select "
        'lsSql &= "  b.trantype_code as DocType,"
        'lsSql &= "  b.docsubtype_code as DocSubType,"
        'lsSql &= " concat(b.trantype_code,' / ',b.docsubtype_desc) as Type,"
        'lsSql &= " fn_get_inwardopeningbal(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "') as `Opening Bal` ,"
        'lsSql &= " count(a.tran_code) as Received,"
        'lsSql &= " fn_get_inwardcompleted(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "','" & Format(dtpTo.Value, "yyyy-MM-dd") & "') as Processed,"
        'lsSql &= " fn_get_inwardopeningbal(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "') + count(a.tran_code) - fn_get_inwardcompleted(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "','" & Format(dtpTo.Value, "yyyy-MM-dd") & "') as `Closing Bal`"
        'lsSql &= " from sta_trn_tinward as a "
        'lsSql &= " inner join sta_mst_tdocsubtype as b on a.tran_code = b.trantype_code "
        'lsSql &= " and a.docsubtype_code = b.docsubtype_code and b.delete_flag = 'N' "
        'lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
        'lsSql &= " left join sta_trn_tfolio as d on a.folio_gid = d.folio_gid and d.delete_flag = 'N' "
        ''lsSql &= " left join sta_trn_tinwarddocsubtype as f on f.trantype_code = 'TM' and f.delete_flag = 'N' "
        'lsSql &= " where a.tran_code = 'TM'  "
        ''or a.docsubtype_code in('TM','ND','NC','IT','NA')
        ''lsSql &= " and a.received_date >= " & Format(dtpFrom.Value, "yyyy-MM-dd") & " and a.received_date <= " & Format(dtpTo.Value, "yyyy-MM-dd") & ""
        'lsSql &= " and a.delete_flag = 'N' "
        'lsSql &= lsCond
        'lsSql &= " group by a.tran_code,a.docsubtype_code "

        'lsSql &= "Union All"
        'lsSql &= " select "
        'lsSql &= "  b.trantype_code as DocType,"
        'lsSql &= "  b.docsubtype_code as DocSubType,"
        'lsSql &= " concat(b.trantype_code,' / ',b.docsubtype_desc) as Type,"
        'lsSql &= " fn_get_inwardopeningbal(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "') as `Opening Bal` ,"
        'lsSql &= " count(a.tran_code) as Received,"
        'lsSql &= " fn_get_inwardcompleted(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "','" & Format(dtpTo.Value, "yyyy-MM-dd") & "') as Processed,"
        'lsSql &= " fn_get_inwardopeningbal(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "') + count(a.tran_code) - fn_get_inwardcompleted(a.tran_code,b.docsubtype_code,'" & Format(dtpFrom.Value, "yyyy-MM-dd") & "','" & Format(dtpTo.Value, "yyyy-MM-dd") & "') as `Closing Bal`"
        'lsSql &= " from sta_trn_tinward as a "
        'lsSql &= " inner join sta_mst_tdocsubtype as b on a.tran_code = b.trantype_code "
        'lsSql &= " and a.docsubtype_code = b.docsubtype_code and b.delete_flag = 'N' "
        'lsSql &= " inner join sta_mst_tcompany as c on a.comp_gid = c.comp_gid and c.delete_flag = 'N' "
        'lsSql &= " left join sta_trn_tfolio as d on a.folio_gid = d.folio_gid and d.delete_flag = 'N' "
        ''lsSql &= " left join sta_trn_tinwarddocsubtype as f on f.trantype_code = 'OT' and f.delete_flag = 'N' "
        'lsSql &= " where a.tran_code = 'OT'  "
        ''or a.docsubtype_code in('DM','LS','IE','OT','NA')
        ''lsSql &= " and a.received_date >= " & Format(dtpFrom.Value, "yyyy-MM-dd") & " and a.received_date <= " & Format(dtpTo.Value, "yyyy-MM-dd") & ""
        'lsSql &= " and a.delete_flag = 'N' "
        'lsSql &= lsCond
        'lsSql &= " group by a.tran_code,a.docsubtype_code "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        'dgvList.Columns("DocType").Visible = False
        'dgvList.Columns("DocSubType").Visible = False

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
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

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub dgvList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellClick
        ' Ensure the clicked cell is valid and not a header row
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        Dim lsQry As String = ""

        ' Check if the clicked cell corresponds to the "Closing Bal" column
        If dgvList.Columns(e.ColumnIndex).Name = "Closing Bal" Then
            ' Check the value of the "Closing Bal" cell
            If dgvList.Rows(e.RowIndex).Cells("Closing Bal").Value Then
                lsQry = ""
                If dgvList.Rows(e.RowIndex).Cells("DocSubType").Value <> "" Then

                    lsQry &= " SELECT "
                    lsQry &= " a.received_date AS `Received Date`,"
                    lsQry &= " a.inward_comp_no AS `Inward No`,"
                    lsQry &= " c.comp_name AS `Company`,"
                    lsQry &= " d.folio_no AS `Folio No`,"
                    lsQry &= " d.holder1_name AS `Share Holder`,"
                    lsQry &= " b.trantype_code AS `Document Type`,"
                    lsQry &= " MAKE_SET(a.inward_status," & gsInwardStatusDesc & ") AS `Inward Status`,"
                    lsQry &= " MAKE_SET(a.queue_status," & gsQueueStatusDesc & ") AS `Queue Status`, "
                    lsQry &= " fn_get_date_range_category(a.received_date) = '1-5 Days' AS '1-5 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '6-10 Days' AS '6-10 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '11-15 Days' AS '11-15 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '16-21 Days' AS '16-21 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '22-30 Days' AS '22-30 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '>30 Days' AS '>30 Days' "
                    lsQry &= " FROM sta_trn_tinward AS a "
                    lsQry &= " INNER JOIN sta_mst_ttrantype AS b ON a.tran_code = b.trantype_code AND b.delete_flag = 'N' "
                    lsQry &= " INNER JOIN sta_mst_tcompany AS c ON a.comp_gid = c.comp_gid AND c.delete_flag = 'N' "
                    lsQry &= " INNER JOIN sta_mst_tdocsubtype AS e ON a.tran_code = e.trantype_code AND a.docsubtype_code = e.docsubtype_code AND e.delete_flag = 'N'"
                    lsQry &= " LEFT JOIN sta_trn_tfolio AS d ON a.folio_gid = d.folio_gid AND d.delete_flag = 'N' "
                    lsQry &= " WHERE a.tran_code = '" & dgvList.Rows(e.RowIndex).Cells("DocType").Value & "' "
                    lsQry &= " AND a.docsubtype_code = '" & dgvList.Rows(e.RowIndex).Cells("DocSubType").Value & "' "
                    lsQry &= " AND (a.inward_status = 1 or a.inward_status = 2 or a.inward_status = 16  or a.inward_status = 64) "
                    lsQry &= " and a.received_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' and a.received_date <= '" & Format(dtpTo.Value, "yyyy-MM-dd") & "' "
                    lsQry &= " and a.delete_flag = 'N' "
                    lsQry &= " UNION all "

                    lsQry &= " SELECT "
                    lsQry &= " a.received_date AS `Received Date`,"
                    lsQry &= " a.inward_comp_no AS `Inward No`,"
                    lsQry &= " c.comp_name AS `Company`,"
                    lsQry &= " d.folio_no AS `Folio No`,"
                    lsQry &= " d.holder1_name AS `Share Holder`,"
                    lsQry &= " b.trantype_code AS `Document Type`,"
                    lsQry &= " MAKE_SET(a.inward_status," & gsInwardStatusDesc & ") AS `Inward Status`,"
                    lsQry &= " MAKE_SET(a.queue_status," & gsQueueStatusDesc & ") AS `Queue Status`, "
                    lsQry &= " fn_get_date_range_category(a.received_date) = '1-5 Days' AS '1-5 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '6-10 Days' AS '6-10 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '11-15 Days' AS '11-15 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '16-21 Days' AS '16-21 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '22-30 Days' AS '22-30 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '>30 Days' AS '>30 Days' "
                    lsQry &= " FROM sta_trn_tinward AS a "
                    lsQry &= " INNER JOIN sta_mst_ttrantype AS b ON a.tran_code = b.trantype_code AND b.delete_flag = 'N' "
                    lsQry &= " INNER JOIN sta_mst_tcompany AS c ON a.comp_gid = c.comp_gid AND c.delete_flag = 'N' "
                    lsQry &= " INNER JOIN sta_mst_tdocsubtype AS e ON a.tran_code = e.trantype_code AND a.docsubtype_code = e.docsubtype_code AND e.delete_flag = 'N'"
                    lsQry &= " LEFT JOIN sta_trn_tfolio AS d ON a.folio_gid = d.folio_gid AND d.delete_flag = 'N' "
                    lsQry &= " WHERE a.tran_code = '" & dgvList.Rows(e.RowIndex).Cells("DocType").Value & "' "
                    lsQry &= " AND a.docsubtype_code = '" & dgvList.Rows(e.RowIndex).Cells("DocSubType").Value & "' "
                    lsQry &= " AND (a.inward_status = 1 OR a.inward_status = 2 OR a.inward_status = 16 OR a.inward_status = 64) "
                    lsQry &= " and a.received_date < '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "'"
                    lsQry &= " and a.delete_flag = 'N' "

                Else
                    lsQry &= " SELECT "
                    lsQry &= " a.received_date AS `Received Date`,"
                    lsQry &= " a.inward_comp_no AS `Inward No`,"
                    lsQry &= " c.comp_name AS `Company`,"
                    lsQry &= " d.folio_no AS `Folio No`,"
                    lsQry &= " d.holder1_name AS `Share Holder`,"
                    lsQry &= " b.trantype_code AS `Document Type`,"
                    lsQry &= " MAKE_SET(a.inward_status," & gsInwardStatusDesc & ") AS `Inward Status`,"
                    lsQry &= " MAKE_SET(a.queue_status," & gsQueueStatusDesc & ") AS `Queue Status`, "
                    lsQry &= " fn_get_date_range_category(a.received_date) = '1-5 Days' AS '1-5 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '6-10 Days' AS '6-10 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '11-15 Days' AS '11-15 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '16-21 Days' AS '16-21 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '22-30 Days' AS '22-30 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '>30 Days' AS '>30 Days' "
                    lsQry &= " FROM sta_trn_tinward AS a "
                    lsQry &= " INNER JOIN sta_mst_ttrantype AS b ON a.tran_code = b.trantype_code AND b.delete_flag = 'N' "
                    lsQry &= " INNER JOIN sta_mst_tcompany AS c ON a.comp_gid = c.comp_gid AND c.delete_flag = 'N' "
                    lsQry &= " LEFT JOIN sta_trn_tfolio AS d ON a.folio_gid = d.folio_gid AND d.delete_flag = 'N' "
                    lsQry &= " WHERE a.tran_code = '" & dgvList.Rows(e.RowIndex).Cells("DocType").Value & "' "
                    lsQry &= " AND (a.inward_status = 1 or a.inward_status = 2 or a.inward_status = 16  or a.inward_status = 64) "
                    lsQry &= " and a.received_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' and a.received_date <= '" & Format(dtpTo.Value, "yyyy-MM-dd") & "'"
                    lsQry &= " and a.delete_flag = 'N' "
                    lsQry &= " UNION all "

                    lsQry &= " SELECT "
                    lsQry &= " a.received_date AS `Received Date`,"
                    lsQry &= " a.inward_comp_no AS `Inward No`,"
                    lsQry &= " c.comp_name AS `Company`,"
                    lsQry &= " d.folio_no AS `Folio No`,"
                    lsQry &= " d.holder1_name AS `Share Holder`,"
                    lsQry &= " b.trantype_code AS `Document Type`,"
                    lsQry &= " MAKE_SET(a.inward_status," & gsInwardStatusDesc & ") AS `Inward Status`,"
                    lsQry &= " MAKE_SET(a.queue_status," & gsQueueStatusDesc & ") AS `Queue Status`, "
                    lsQry &= " fn_get_date_range_category(a.received_date) = '1-5 Days' AS '1-5 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '6-10 Days' AS '6-10 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '11-15 Days' AS '11-15 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '16-21 Days' AS '16-21 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '22-30 Days' AS '22-30 Days',"
                    lsQry &= " fn_get_date_range_category(a.received_date) = '>30 Days' AS '>30 Days' "
                    lsQry &= " FROM sta_trn_tinward AS a "
                    lsQry &= " INNER JOIN sta_mst_ttrantype AS b ON a.tran_code = b.trantype_code AND b.delete_flag = 'N' "
                    lsQry &= " INNER JOIN sta_mst_tcompany AS c ON a.comp_gid = c.comp_gid AND c.delete_flag = 'N' "
                    lsQry &= " LEFT JOIN sta_trn_tfolio AS d ON a.folio_gid = d.folio_gid AND d.delete_flag = 'N' "
                    lsQry &= " WHERE a.tran_code = '" & dgvList.Rows(e.RowIndex).Cells("DocType").Value & "' "
                    'lsQry &= " AND (a.inward_all_status & 1 = 0 ) AND (a.inward_all_status & 2 = 0 ) AND (a.inward_all_status & 64 = 0 ) "
                    lsQry &= " AND (a.inward_status = 1 OR a.inward_status = 2 OR a.inward_status = 16 OR a.inward_status = 64) "
                    lsQry &= " and a.received_date < '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "'"
                    lsQry &= " and a.delete_flag = 'N' "

                End If

                Dim objFrm As frmQuickView

                If e.RowIndex >= 0 Then
                    objFrm = New frmQuickView(gOdbcConn, lsQry)
                    objFrm.ShowDialog()
                End If
            End If
        End If


    End Sub
End Class