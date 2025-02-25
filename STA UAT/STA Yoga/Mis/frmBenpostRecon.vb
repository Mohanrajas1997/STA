Public Class frmBenpostRecon
    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpBenpost.Value = Now
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

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim lnBenpostNsdlCount As Double = 0
        Dim lnBenpostCdslCount As Double = 0

        Dim lnLocalNsdlCount As Double = 0
        Dim lnLocalCdslCount As Double = 0

        Dim lnDiffNsdlCount As Double = 0
        Dim lnDiffCdslCount As Double = 0

        Dim lnCompId As Long = 0
        Dim lsBenpostDate As String = ""
        Dim dt As New DataTable

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        lsBenpostDate = Format(dtpBenpost.Value, "yyyy-MM-dd")

        ' find benpost date
        lsSql = ""
        lsSql &= " select max(benpost_date) from sta_trn_tbenpost "
        lsSql &= " where comp_gid = " & lnCompId & " "
        lsSql &= " and benpost_date <= '" & lsBenpostDate & "' "
        lsSql &= " and delete_flag = 'N' "

        lsBenpostDate = gfExecuteScalar(lsSql, gOdbcConn)

        If IsDate(lsBenpostDate) = False Then
            MessageBox.Show("Invalid benpost date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpBenpost.Focus()
            Exit Sub
        Else
            lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd")
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

        lnBenpostNsdlCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

        ' benpost cdsl count
        lsSql = ""
        lsSql &= " select "
        lsSql &= " sum(share_count) as share_count "
        lsSql &= " from sta_trn_tbenpost "
        lsSql &= " where benpost_date = '" & lsBenpostDate & "' "
        lsSql &= " and comp_gid = " & lnCompId & " "
        lsSql &= " and depository_code = 'C' "
        lsSql &= " and delete_flag = 'N' "

        lnBenpostCdslCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

        ' local nsdl count
        lsSql = ""
        lsSql &= " select "
        lsSql &= " sum(tran_count*mult) as share_count "
        lsSql &= " from sta_mst_tdepository as a "
        lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
        lsSql &= " and b.comp_gid = " & lnCompId & " "
        lsSql &= " and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
        lsSql &= " and c.delete_flag = 'N' "
        lsSql &= " where c.tran_date <= '" & lsBenpostDate & "' "
        lsSql &= " and a.depository_code = 'N' "
        lsSql &= " and a.delete_flag = 'N' "

        lnLocalNsdlCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

        ' local cdsl count
        lsSql = ""
        lsSql &= " select "
        lsSql &= " sum(tran_count*mult) as share_count "
        lsSql &= " from sta_mst_tdepository as a "
        lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
        lsSql &= " and b.comp_gid = " & lnCompId & " "
        lsSql &= " and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
        lsSql &= " and c.delete_flag = 'N' "
        lsSql &= " where c.tran_date <= '" & lsBenpostDate & "' "
        lsSql &= " and a.depository_code = 'C' "
        lsSql &= " and a.delete_flag = 'N' "

        lnLocalCdslCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

        ' differenct
        lnDiffNsdlCount = lnBenpostNsdlCount - lnLocalNsdlCount
        lnDiffCdslCount = lnBenpostCdslCount - lnLocalCdslCount

        lsBenpostDate = Format(CDate(lsBenpostDate), "dd-MM-yyyy")

        With dt
            .Columns.Add("Date", GetType(System.String))
            .Columns.Add("Particulars", GetType(System.String))
            .Columns.Add("NSDL", GetType(System.Int64))
            .Columns.Add("CDSL", GetType(System.Int64))

            .Rows.Add(lsBenpostDate, "Benpost Count", lnBenpostNsdlCount, lnBenpostCdslCount)
            .Rows.Add(lsBenpostDate, "Local Count", lnLocalNsdlCount, lnLocalCdslCount)
            .Rows.Add(lsBenpostDate, "Difference", lnDiffNsdlCount, lnDiffCdslCount)
        End With

        With dgvList
            .DataSource = dt

            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call loaddata()
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