Public Class frmUploadReport
    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' status
        lsSql = ""
        lsSql &= " select status_value,status_desc from sta_mst_tstatus "
        lsSql &= " where status_type = 'Upload' "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by status_desc asc "

        Call gpBindCombo(lsSql, "status_desc", "status_value", cboStatus, gOdbcConn)

        ' upload type
        lsSql = ""
        lsSql &= " select upload_type,upload_desc from sta_mst_tupload "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by upload_desc asc "

        Call gpBindCombo(lsSql, "upload_desc", "upload_type", cboUploadType, gOdbcConn)

        dtpFrom.Value = DateAdd(DateInterval.Day, -7, Now)
        dtpTo.Value = Now
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

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If dtpFrom.Checked = True Then lsCond &= " and a.upload_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        If dtpTo.Checked = True Then lsCond &= " and a.upload_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

        If Val(txtUploadNo.Text) > 0 Then lsCond &= " and a.upload_no = " & Val(txtUploadNo.Text) & " "
        If txtUploadId.Text <> "" Then lsCond &= " and a.upload_gid in (" & txtUploadId.Text & ") "

        If cboStatus.Text <> "" And cboStatus.SelectedIndex <> -1 Then
            lsCond &= " and a.upload_status = " & Val(cboStatus.SelectedValue.ToString) & " "
        End If

        If cboUploadType.Text <> "" And cboUploadType.SelectedValue <> -1 Then
            lsCond &= " and a.upload_type = " & Val(cboUploadType.SelectedValue.ToString) & " "
        End If

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name as 'Company',"
        lsSql &= " date_format(a.upload_date,'%d-%m-%Y') as 'Upload Date',"
        lsSql &= " a.upload_no as 'Upload No',"
        lsSql &= " make_set(a.upload_type," & gsUploadTypeDesc & ") as 'Upload Type',"
        lsSql &= " make_set(a.upload_status," & gsUploadStatusDesc & ") as 'Status',"
        lsSql &= " a.transfer_count as 'Transfer',"
        lsSql &= " a.cert_count as 'Certificate',"
        lsSql &= " a.objx_count as 'Objection',"
        lsSql &= " a.transfer_start_sno as 'Transfer Start No',"
        lsSql &= " a.transfer_end_sno as 'Transfer End No',"
        lsSql &= " a.cert_start_sno as 'Certificate Start No',"
        lsSql &= " a.cert_end_sno as 'Certificate End No',"
        lsSql &= " a.objx_start_sno as 'Objection Start No',"
        lsSql &= " a.objx_end_sno as 'Objection End No',"
        lsSql &= " a.cdsl_sno as 'Cdsl No',"
        lsSql &= " a.nsdl_sno as 'Nsdl No',"
        lsSql &= " a.upload_by as 'Upload By',"
        lsSql &= " a.upload_type as 'Upload Type Value',"
        lsSql &= " a.upload_status as 'Upload Status Value',"
        lsSql &= " a.status_update_date as 'Status Update Date',"
        lsSql &= " a.update_by as 'Status Update By',"
        lsSql &= " a.upload_date as 'Insert Date',"
        lsSql &= " a.update_date as 'Update Date',"
        lsSql &= " a.upload_gid as 'Upload Id',"
        lsSql &= " a.comp_gid as 'Comp Id' "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.upload_gid desc "

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub
End Class