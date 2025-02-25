Imports MySql.Data.MySqlClient
Public Class frminwreport_period

    Private Sub frminwreport_period_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String
        'company 
        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' doc type
        Sql = ""
        Sql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by trantype_code asc "

        Call gpBindCombo(Sql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

        dtp_inwardfrom.Value = Now
        dtp_inwardfrom.Checked = False
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lsSql As String
        Dim lsCond As String = ""


        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            lsCond &= " and a.tran_code = '" & cboDocType.SelectedValue.ToString & "' "
        End If

        If dtp_inwardfrom.Checked = True Then lsCond &= " and b.outward_date >= '" & Format(dtp_inwardfrom.Value, "yyyy-MM-dd") & "' "
        If Dateinwardto.Checked = True Then lsCond &= " and b.outward_date <= '" & Format(Dateinwardto.Value, "yyyy-MM-dd") & "' "


        lsSql = ""
        lsSql &= " SELECT a.inward_no as Inw_No ,a.received_date as Inw_Date,a.tran_code as Inw_code,c.trantype_desc as Inw_Description,a.shareholder_name as Name_of_the_ShareHolder,b.outward_date as Reply_Date FROM sta_trn_tinward a "
        lsSql &= "left join sta_trn_toutward b on a.inward_gid = b.inward_gid "
        lsSql &= "left join sta_mst_ttrantype c on a.tran_code = c.trantype_code where true  "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "

        cmd = New MySqlCommand(lsSql, gOdbcConn)
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

    
    Private Sub Dateinwardto_ValueChanged(sender As Object, e As EventArgs) Handles Dateinwardto.ValueChanged

    End Sub
End Class