Imports MySql.Data.MySqlClient
Public Class Frm_divpaid_unpaid

    Private Sub Frm_divpaid_unpaid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        Sql = ""
        Sql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        Sql &= " where delete_flag = 'N' "

        Call gpBindCombo(Sql, "finyear_code", "finyear_gid", Cb_finyear, gOdbcConn)

        Sql = ""
        Sql &= " select interim_code,interim_name from sta_mst_tinterim "
        Sql &= " where delete_flag = 'N' "

        Call gpBindCombo(Sql, "interim_name", "interim_code", Cb_interim, gOdbcConn)

        cbstatus.Items.Clear()
        cbstatus.Items.Add("Paid")
        cbstatus.Items.Add("Unpaid")
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Dim lsSql As String
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If Cb_finyear.Text <> "" And Cb_finyear.SelectedIndex <> -1 Then
            lsCond &= " and a.finyear_gid = " & Val(Cb_finyear.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the Financial Year !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cb_finyear.Focus()
            Exit Sub
        End If

        If Cb_interim.Text <> "" And Cb_interim.SelectedIndex <> -1 Then
            lsCond &= " and a.interim_code = '" & Cb_interim.SelectedValue.ToString & "' "
        Else
            MessageBox.Show("Please select the interim !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cb_interim.Focus()
            Exit Sub
        End If

        If cbstatus.Text = "Paid" Then
            lsCond &= " and a.payment_status = '8' and a.curr_dividend_status = '8' "
        ElseIf cbstatus.Text = "Unpaid" Then
            lsCond &= " and a.payment_status != '8' and a.curr_dividend_status != '8' "
        ElseIf cbstatus.Text = "" Then
            MessageBox.Show("Please select the Status !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbstatus.Focus()
            Exit Sub
        End If

        lsSql = ""
        lsSql &= " SELECT @s:=@s+1 Sno,a.dp_id,a.folioclient_id, "
        lsSql &= " a.holder1_name,b.payment_no,a.share_count,"
        lsSql &= " a.currency_amount,a.paid_refno,DATE_FORMAT(a.paid_date, '%d/%m/%Y') as paid_date,a.payment_mode,"
        lsSql &= " a.holder1_addr1,a.holder1_addr2,a.holder1_addr3,a.holder1_addr4,a.holder1_city,a.holder1_state,a.holder1_country,a.holder1_pincode"
        lsSql &= " FROM sta_trn_tdividend  a"
        lsSql &= " left join sta_trn_tpayment b on a.dividend_gid = b.dividend_gid ,(SELECT @s:= 0) AS s where true"
        lsSql &= lsCond

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        If dt.Rows.Count <= 0 Then
            MessageBox.Show("No Records Found")
            dgvList.DataSource = Nothing

        Else

            With dgvList
                .DataSource = dt
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i

                txtTotRec.Text = "Total Records : " & .RowCount.ToString
            End With
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
End Class