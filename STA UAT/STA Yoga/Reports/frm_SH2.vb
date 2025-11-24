Imports MySql.Data.MySqlClient
Public Class frm_SH2


    Private Sub frm_SH2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
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

        If dtp_datefrom.Checked = True Then lsCond &= " and a.received_date between  '" & Format(dtp_datefrom.Value, "yyyy-MM-dd") & "' "
        If dtp_dateto.Checked = True Then lsCond &= " and  '" & Format(dtp_dateto.Value, "yyyy-MM-dd") & "' "

        lsSql = ""
        lsSql &= " SELECT @s:=@s+1 Sno,"
        lsSql &= " a.folio_no,"
        lsSql &= " a.shareholder_name,"
        lsSql &= " DATE_FORMAT(a.approved_date, '%d/%m/%Y') as app_date,"
        lsSql &= " 'equity',"
        lsSql &= " DATE_FORMAT(c.issue_date, '%d/%m/%Y') as issdate, "
        lsSql &= " c.cert_no,c.share_count,"
        lsSql &= " e.dist_from,e.dist_to,"
        lsSql &= " DATE_FORMAT(a.approved_date, '%d/%m/%Y') as adate, "
        lsSql &= " f.trantype_desc, "
        lsSql &= " a.tran_cert_no,c.share_count as scount, DATE_FORMAT(a.approved_date, '%d/%m/%Y') as tdb2,'' as tdb3"
        lsSql &= " FROM sta_trn_tinward as a"
        lsSql &= " left join sta_trn_tcert c on a.folio_gid = c.folio_gid and c.delete_flag='N'"
        lsSql &= " left join sta_trn_tcertdist e on e.cert_gid = c.cert_gid"
        lsSql &= " left join sta_mst_ttrantype f on f.trantype_code = a.tran_code"
        lsSql &= " ,(SELECT @s:= 0) AS s where true and c.cert_status = '2'"
        lsSql &= lsCond
        lsSql &= " and a.tran_code in ('LS','CO','SP','RM') and approved_date!= ''"

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
            PrintExportExcel(dgvList, gsReportPath & "\Report.xls", "Report")
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