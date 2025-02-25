Public Class frmAmlresultRpt
    Dim mnFolioId As Long = 0
    Private Sub frmQueue_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select comp_gid,comp_name  from sta_mst_tcompany "
        lsSql &= " where delete_flag='N'"
        gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompName, gOdbcConn)

        lsSql = ""
        lsSql &= " select amlrule_gid,amlrule_name  from sta_mst_tamlrule "
        lsSql &= " where delete_flag='N'"
        gpBindCombo(lsSql, "amlrule_name", "amlrule_gid", CmbRuleName, gOdbcConn)


    End Sub
    
    Private Sub frmQueue_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
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

        If (cboCompName.Text = "" Or cboCompName.SelectedIndex = -1) And mnFolioId = 0 Then
            MessageBox.Show("Please select the Company Name !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompName.Focus()
            Exit Sub
        End If
        If cboCompName.Text <> "" And cboCompName.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid= " & Val(cboCompName.SelectedValue.ToString) & ""
        End If
        If CmbRuleName.Text <> "" And CmbRuleName.SelectedIndex <> -1 Then
            lsCond &= " and d.amlrule_gid= " & Val(CmbRuleName.SelectedValue.ToString) & ""
        End If


        If mnFolioId > 0 Then lsCond &= " and a.amlresult_gid = " & mnFolioId & " "


        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_code as 'Company Code',"
        lsSql &= " b.comp_name as 'Company Name',"
        lsSql &= " c.folio_no as 'Folio No',"
        lsSql &= " a.aml_name as 'AML Name',"
        lsSql &= " a.amlresult_gid as 'Aml Id' "
        lsSql &= " from sta_trn_amlresult as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid=b.comp_gid and b.delete_flag='N' "
        lsSql &= " inner join  sta_trn_tfolio as c on a.folio_gid=c.folio_gid and c.delete_flag='N'"
        lsSql &= " inner join sta_mst_tamlrule as d on a.amlrule_gid=d.amlrule_gid and d.delete_flag='N'"
        lsSql &= " where true "
        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.amlresult_gid"

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        For i = 0 To dgvList.ColumnCount - 1
            dgvList.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        txtTotRec.Text = "Total Records : " & dgvList.RowCount.ToString
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        Call LoadGrid()
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        dgvList.DataSource = ""
    End Sub

    Private Sub dgvList_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvList.KeyDown
        Dim lnFolioId As Long = 0
        Dim frm As Object

        With dgvList
            If .CurrentCell.RowIndex >= 0 Then
                lnFolioId = .CurrentRow.Cells("Aml Id").Value

                Select Case e.KeyCode
                    Case Keys.F1
                        frm = New frmFolioDetail(lnFolioId)
                        frm.ShowDialog()
                    Case Keys.F2
                        frm = New frmCertReport(lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                    Case Keys.F3
                        frm = New frmCertDistReport(0, lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                    Case Keys.F4
                        frm = New frmFolioTranReport(lnFolioId)
                        frm.MdiParent = frmMain
                        frm.Show()
                End Select
            End If
        End With
    End Sub
End Class