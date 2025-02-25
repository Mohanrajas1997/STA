Public Class frmDaywisebenpostreport

    Private Sub frmDaywisebenpostreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' Security type
        lsSql = ""
        lsSql &= " select securitytype_code,securitytype_name from sta_mst_tsecuritytype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by securitytype_name "

        Call gpBindCombo(lsSql, "securitytype_name", "securitytype_code", cboSecurityType, gOdbcConn)
    End Sub

    Private Sub frmDaywisebenpostreport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
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

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim lnCompId As Long = 0
        Dim lsBenpostDate As String = ""
        Dim dt As New DataTable

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If dtpBenpost.Checked = False Then
            MessageBox.Show("Please select the benpost date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpBenpost.Focus()
            Exit Sub
        End If

        lsBenpostDate = Format(dtpBenpost.Value, "yyyy-MM-dd")

        If txtISIN.Text <> "" Then
            lsCond &= " and b.isin_id = '" & txtISIN.Text & "' "
        End If

        If cboSecurityType.Text <> "" And cboSecurityType.SelectedIndex <> -1 Then
            lsCond &= " and b.share_type = '" & cboSecurityType.Text & "' "
        End If

        If lsCond = "" Then lsCond &= " and 1 = 1 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'COMPANY NAME',"
        lsSql &= " b.isin_id as 'ISIN',"
        lsSql &= " b.share_type as 'Security Type',"
        lsSql &= " date_format(a.date,'%d-%m-%Y') as 'DATE',"
        lsSql &= " a.nsdl_total as 'NSDL',"
        lsSql &= " a.cdsl_total as 'CDSL',"
        lsSql &= " a.physical_total as 'Physical',"
        lsSql &= " (a.nsdl_total + a.cdsl_total + a.physical_total) as 'Total',"
        lsSql &= " a.share_capital as 'Share Capital',"
        lsSql &= " (a.nsdl_total + a.cdsl_total + a.physical_total) - a.share_capital as 'Difference'"

        lsSql &= " from sta_windows.output as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.isin_no = b.isin_id and b.delete_flag = 'N' "

        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.date ='" & lsBenpostDate & "'"
        lsSql &= " order by a.company_name"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString

       
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call loaddata()
    End Sub


    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvList.DataBindingComplete
        For Each row As DataGridViewRow In dgvList.Rows
            Dim diffValue As Decimal

            ' Check if the "Difference" column exists and has a valid numeric value
            If Decimal.TryParse(row.Cells("Difference").Value.ToString(), diffValue) Then
                If diffValue = 0 Then
                    row.DefaultCellStyle.BackColor = Color.Green ' Set background color to Green (Green shade)
                    'row.DefaultCellStyle.ForeColor = Color.White ' Change text color for better visibility
                Else
                    row.DefaultCellStyle.BackColor = Color.Red ' Set background color to Red (Red shade)
                    'row.DefaultCellStyle.ForeColor = Color.White ' Change text color for better visibility
                End If

            End If
        Next
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        dgvList.DataSource = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class