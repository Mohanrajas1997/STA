Imports MySql.Data.MySqlClient
Public Class frmcategoryreport

    Private Sub frmcategoryreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql, Sql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' doc type
        Sql = ""
        Sql &= " select category_gid as id,category_name as category_name from sta_mst_tcategory "
        Sql &= " where delete_flag = 'N' "
        Sql &= " union"
        Sql &= " select bencategory_type as id,bencategory_name as category_name from sta_mst_tbencategory "
        Sql &= " where delete_flag = 'N' "
        Sql &= " group by category_name "

        Call gpBindCombo(Sql, "id", "category_name", cbocatgeory, gOdbcConn)

        Datecategory.Value = Now
        Datecategory.Checked = False
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt, dtt, dben As DataTable
        Dim lsSql, ls_Sql, ls_ben As String
        Dim lsCond As String = ""
        Dim lsCond2 As String = ""
        Dim lsDate As String = ""
        Dim ben_date As String = ""


        lsDate = Format(Datecategory.Value, "yyyy-MM-dd")

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If
        'If Datecategory.Checked = True Then lsCond &= " and c.tran_date <= '" & Format(Datecategory.Value, "yyyy-MM-dd") & "' "

        If cbocatgeory.Text <> "" And cbocatgeory.SelectedIndex <> -1 Then
            lsCond &= " and b.category_name = '" & cbocatgeory.SelectedValue.ToString & "' "
        End If

        ''''''''''''''''
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond2 &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If
        If Datecategory.Checked = True Then lsCond2 &= " and a.benpost_date = '" & Format(Datecategory.Value, "yyyy-MM-dd") & "' "

        If cbocatgeory.Text <> "" And cbocatgeory.SelectedIndex <> -1 Then
            lsCond2 &= " and b.bencategory_name = '" & cbocatgeory.SelectedValue.ToString & "' "
        End If

        ''''''''''''''''''''

        ls_ben = ""
        ls_ben &= "SELECT benpost_date FROM sta_trn_tbenpost where true and benpost_date = '" & Format(Datecategory.Value, "yyyy-MM-dd") & "' and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' ;"
        cmd = New MySqlCommand(ls_ben, gOdbcConn)
        dben = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dben)
        If dben.Rows.Count > 0 Then
            If Not IsDBNull(dben.Rows(0)(0).ToString) Then
                ben_date = dben.Rows(0)(0).ToString
            End If
        End If
        '''''''''''''''''''''


        If Not cbocatgeory.Text <> "" Then

            lsSql = ""
            lsSql &= "  select Category_Name,sum(tally) as Cnt,sum(shares_sum) as Sum from"
            lsSql &= " (select b.category_name as category_name,count(*) as tally,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as shares_sum "
            lsSql &= " from sta_trn_tfolio as a"
            lsSql &= " left join sta_mst_tcategory as b on a.category_gid = b.category_gid and b.delete_flag = 'N'"
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999') and a.delete_flag = 'N' "
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') > 0"
            lsSql &= " group by a.category_gid,b.category_name "
            lsSql &= " union all "
            lsSql &= " select b.bencategory_name as category_name,count(*) as tally,sum(a.share_count) as shares_sum from sta_trn_tbenpost as a"
            lsSql &= " left join sta_mst_tbencategory as b"
            lsSql &= " on a.bene_type = b.bencategory_type"
            lsSql &= " and a.bene_subtype = b.bencategory_subtype"
            lsSql &= " and a.depository_code = b.depository_code"
            lsSql &= " and b.delete_flag = 'N'"
            lsSql &= " where true "
            lsSql &= lsCond2
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by category_name) as t group by category_name"

            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            If ben_date = "" Then
                MessageBox.Show("Please Enter Valid Benpost Date")
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

        ElseIf cbocatgeory.Text <> "" Then
            ls_Sql = ""
            ls_Sql &= " select a.folio_no as folio,null as dp_id, a.holder1_name as name,fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') as shares,b.category_name as category from sta_trn_tfolio as a "
            ls_Sql &= " left join sta_mst_tcategory b on a.category_gid=b.category_gid where true "
            ls_Sql &= lsCond
            ls_Sql &= " and a.folio_no NOT IN ('00888888','00999999') "
            ls_Sql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') > 0"
            ls_Sql &= " and a.delete_flag = 'N'"
            ls_Sql &= " union all"
            ls_Sql &= " select a.client_id,a.dp_id,a.holder1_name as name,a.share_count as shares,b.bencategory_name as category from sta_trn_tbenpost as a"
            ls_Sql &= " inner join sta_mst_tbencategory as b"
            ls_Sql &= " on a.bene_type = b.bencategory_type"
            ls_Sql &= " and a.bene_subtype = b.bencategory_subtype"
            ls_Sql &= " and a.depository_code = b.depository_code"
            ls_Sql &= " and b.delete_flag = 'N'"
            ls_Sql &= " where true "
            ls_Sql &= lsCond2
            cmd = New MySqlCommand(ls_Sql, gOdbcConn)
            dtt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt)

            If (dtt.Rows.Count <= 0) Then
                MessageBox.Show("Category is Empty")
                dgvList.DataSource = Nothing
            ElseIf ben_date = "" Then
                MessageBox.Show("Please Enter Valid Benpost Date")
                dgvList.DataSource = Nothing
            Else
                With dgvList
                    .DataSource = dtt
                    For i = 0 To .ColumnCount - 1
                        .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    Next i
                    txtTotRec.Text = "Total Records : " & .RowCount.ToString
                End With
            End If
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