Imports MySql.Data.MySqlClient

Public Class frmBenpostComparisonReport
    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpFrom.Value = Now

        lsSql = ""
        lsSql &= " SELECT depository_code,depository_name FROM sta_mst_tdepository"
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by depository_name "

        Using cmd As MySqlCommand = New MySqlCommand(lsSql, gOdbcConn)
            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As System.Data.DataTable = New System.Data.DataTable
            dt.Load(rs)

            cbdepotype.ValueMember = "depository_code"
            cbdepotype.DisplayMember = "depository_name"
            cbdepotype.DataSource = dt
        End Using

        Call gpBindCombo(lsSql, "depository_name", "depository_code", cbdepotype, gOdbcConn)

    End Sub

    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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

    Private Sub LoadDataOld()
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim lnCompId As Long = 0
        Dim lsBenpostDate As String = ""

        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If

        lsBenpostDate = Format(dtpFrom.Value, "yyyy-MM-dd")

        ' find benpost date
        lsSql = ""
        lsSql &= " select max(benpost_date) from sta_trn_tbenpost "
        lsSql &= " where comp_gid = " & lnCompId & " "
        lsSql &= " and benpost_date <= '" & lsBenpostDate & "' "
        lsSql &= " and delete_flag = 'N' "

        lsBenpostDate = gfExecuteScalar(lsSql, gOdbcConn)

        If IsDate(lsBenpostDate) = False Then
            MessageBox.Show("Invalid benpost date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpFrom.Focus()
            Exit Sub
        Else
            lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd")
        End If

        If cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If

        ' benpost nsdl count
        lsSql = ""
        lsSql &= " select "
        lsSql &= " sum(share_count) as share_count "
        lsSql &= " from sta_trn_tbenpost "
        lsSql &= " where benpost_date = '" & lsBenpostDate & "' "
        lsSql &= " and comp_gid = " & lnCompId & " "
        lsSql &= " and depository_code = 'N' "
        lsSql &= " and delete_flag = 'N' "

        cmd = New MySqlCommand("pr_sta_get_benpostcomparison", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
        cmd.Parameters.AddWithValue("?in_benpost_date", CDate(lsBenpostDate))

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvList
            .DataSource = dt

            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim lnCompId As Long = 0
        Dim lsBenpostDate As String = ""
        Dim lsBenpostFrom As String = ""
        Dim lsBenpostTo As String = ""
        Dim lsDepository As String = ""

        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
            lsDepository = cbdepotype.SelectedValue.ToString()
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If

        lsBenpostDate = Format(dtpFrom.Value, "yyyy-MM-dd")

        lsBenpostFrom = lsBenpostDate
        lsBenpostTo = Format(dtpTo.Value, "yyyy-MM-dd")

        ' find benpost date
        lsSql = ""
        lsSql &= " select max(benpost_date) from sta_trn_tbenpost "
        lsSql &= " where comp_gid = " & lnCompId & " "
        lsSql &= " and benpost_date <= '" & lsBenpostDate & "' "
        lsSql &= " and delete_flag = 'N' "

        lsBenpostDate = gfExecuteScalar(lsSql, gOdbcConn)

        If IsDate(lsBenpostDate) = False Then
            MessageBox.Show("Invalid benpost date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpFrom.Focus()
            Exit Sub
        Else
            lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd")
            lsBenpostFrom = lsBenpostDate
        End If

        If chkFromTo.Checked And lsDepository <> "A" Then
            cmd = New MySqlCommand("pr_sta_get_benpostcomparisonfromto_new", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_benpost_from", CDate(lsBenpostFrom))
            cmd.Parameters.AddWithValue("?in_benpost_to", CDate(lsBenpostTo))
            cmd.Parameters.AddWithValue("?in_depository_code", lsDepository)
        ElseIf lsDepository <> "A" Then
            cmd = New MySqlCommand("pr_sta_get_benpostcomparison_nsdlcdsl_new", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_benpost_date", CDate(lsBenpostDate))
            cmd.Parameters.AddWithValue("?in_depository_code", lsDepository)
        End If

        If chkFromTo.Checked And lsDepository = "A" Then
            cmd = New MySqlCommand("pr_sta_get_benpostcomparisonfromto_all", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_benpost_from", CDate(lsBenpostFrom))
            cmd.Parameters.AddWithValue("?in_benpost_to", CDate(lsBenpostTo))
        ElseIf lsDepository = "A" Then
            cmd = New MySqlCommand("pr_sta_get_benpostcomparison_nsdlcdsl_all", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_benpost_date", CDate(lsBenpostDate))
        End If

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvList
            .DataSource = dt

            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call LoadData()
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

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub chkFromTo_CheckedChanged(sender As Object, e As EventArgs) Handles chkFromTo.CheckedChanged
        If chkFromTo.Checked Then
            lblFrom.Text = "Benpost From"
            lblTo.Visible = True
            dtpTo.Visible = True
        Else
            lblFrom.Text = "Benpost Date"
            lblTo.Visible = False
            dtpTo.Visible = False
        End If
    End Sub
End Class