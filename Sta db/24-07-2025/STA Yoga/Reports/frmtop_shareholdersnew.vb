Imports MySql.Data.MySqlClient
Public Class frmtop_shareholdersnew

    Private Sub frmtop_shareholdersnew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        Dim lnCompId As Integer
        Dim lnsharelimit As Integer
        txttotal.Text = ""

        If cboCompany.Text = "" Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        Else
            lnCompId = Val(cboCompany.SelectedValue)
        End If

        If txtsharelist.Text <= 0 Then
            MessageBox.Show("Please enter Top Share Holders !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtsharelist.Focus()
            Exit Sub
        Else
            lnsharelimit = txtsharelist.Text
        End If

        cmd = New MySqlCommand("pr_sta_get_topshareholderlist", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
        cmd.Parameters.AddWithValue("?in_share_limit", lnsharelimit)
        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        dgvList.DataSource = dt
        If dt.Rows.Count > 0 Then
            txttotal.Text = lnsharelimit
        End If

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

    Private Sub txtsharelist_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsharelist.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class