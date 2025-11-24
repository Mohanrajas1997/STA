Public Class frmDividendAccountMasterRpt
    Private Sub frmDividendAccountMasterRpt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        Sql = ""
        Sql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by finyear_code asc "

        Call gpBindCombo(Sql, "finyear_code", "finyear_gid", CboFinyear, gOdbcConn)

        Sql = ""
        Sql &= " select bank_gid,bank_code FROM sta_mst_tbank "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by bank_code asc "

        Call gpBindCombo(Sql, "bank_code", "bank_gid", CboBankCode, gOdbcConn)
    End Sub
    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        'If (CboFinyear.Text = "" Or CboFinyear.SelectedIndex = -1) Then
        '    MessageBox.Show("Please select the finyear !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    CboFinyear.Focus()
        '    Exit Sub
        'End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If CboFinyear.Text <> "" And CboFinyear.SelectedIndex <> -1 Then
            lsCond &= " and a.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & ""
        End If

        If CboBankCode.Text <> "" And CboBankCode.SelectedIndex <> -1 Then
            lsCond &= " and a.bank_gid = " & Val(CboBankCode.SelectedValue.ToString) & ""
        End If

        If txtAccno.Text <> "" Then
            lsCond &= " and a.acc_no = " & Val(txtAccno.Text.ToString) & ""
        End If

        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " c.finyear_code as 'FinYear Code',"
        lsSql &= " a.acc_no as 'Account Number',"
        lsSql &= " d.bank_code as 'Bank Code',"
        lsSql &= " a.div_date as 'Div Date',"
        lsSql &= " a.div_rate as 'Div Rate',"
        lsSql &= " a.acc_gid as 'Account Gid'"
        lsSql &= " from div_mst_tacc as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid  and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tfinyear as c on a.finyear_gid = c.finyear_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tbank as d on a.bank_gid = d.bank_gid  and d.delete_flag='N'"
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.acc_gid asc"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
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

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Dividend Account Master.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class