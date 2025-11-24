Imports MySql.Data.MySqlClient
Public Class frmdis_schedule

    Private Sub frmdis_schedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        'Cb_sharefrom.Items.Add(" >= 0")
        'Cb_sharefrom.Items.Add(" >= 5001")


    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt, dt1, dtt, dtt3, dtt4, dtt5, dtt6, dtt7, dtt8, dtsum As DataTable
        'Dim dtsum As DataTable()
        Dim holder_count As String = ""
        Dim sumcount As String = ""
        Dim ben_date As String = ""
        Dim lsSql As String
        Dim ls_Sql As String
        Dim ls_Sql_s As String
        Dim lsDate As String = ""

        Dim lsCond As String = ""
        Dim datephy As String = ""
        Dim lsCond2 As String = ""

        lsDate = Format(dtp_dis_schd.Value, "yyyy-MM-dd")
        lsCond = ""
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If dtp_dis_schd.Checked = True Then lsCond &= " and benpost_date = '" & Format(dtp_dis_schd.Value, "yyyy-MM-dd") & "' "

        lsCond2 = ""
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond2 &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If dtp_dis_schd.Checked = True Then datephy &= " and b.tran_date <= '" & Format(dtp_dis_schd.Value, "yyyy-MM-dd") & "' "

        'holder count
        ls_Sql = ""
        ls_Sql &= "SELECT(SELECT count(benpost_gid) FROM sta_trn_tbenpost where true " & lsCond & " )+"
        ls_Sql &= "(SELECT count(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as cunt FROM sta_trn_tfolio a "
        ls_Sql &= " where true " & lsCond2 & " and a.folio_no NOT IN ('00888888','00999999') and (fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) > 0 )"
        ls_Sql &= " as totalholdercount;"
        cmd = New MySqlCommand(ls_Sql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)(0).ToString) Then
                holder_count = dt.Rows(0)(0).ToString
            End If
        End If

        'sum of shares
        ls_Sql_s = ""
        ls_Sql_s &= "SELECT(SELECT sum(share_count) FROM sta_trn_tbenpost where true " & lsCond & " )+"
        ls_Sql_s &= "(SELECT sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) FROM sta_trn_tfolio a where true " & lsCond2 & " and a.folio_no NOT IN ('00888888','00999999'))"
        ls_Sql_s &= " as sumcount;"
        cmd = New MySqlCommand(ls_Sql_s, gOdbcConn)
        dt1 = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            If Not IsDBNull(dt1.Rows(0)(0).ToString) Then
                sumcount = dt1.Rows(0)(0).ToString
            End If
        End If

        If sumcount = "" Then
            MessageBox.Show("Please Enter Valid Benpost Date")
            dgvList.DataSource = Nothing
        Else

            '>= 1 and <= 500
            lsSql = ""
            lsSql &= " select '1 to 500' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount' from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count > 0"
            lsSql &= " and share_count <= 500"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') between 1 and 500"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            '>= 501 and <= 1000
            lsSql = ""
            lsSql &= " select '501 to 1000' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount' from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count >= 501"
            lsSql &= " and share_count <= 1000"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') between 501 and 1000"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dtt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt)



            '>= 1001 and <= 2000
            lsSql = ""
            lsSql &= " select '1001 to 2000' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount'    from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count >= 1001"
            lsSql &= " and share_count <= 2000"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') between 1001 and 2000"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dtt3 = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt3)

            '>= 2001 and <= 3000
            lsSql = ""
            lsSql &= " select '2001 to 3000' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount' from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count >= 2001"
            lsSql &= " and share_count <= 3000"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') between 2001 and 3000"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dtt4 = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt4)

            '>= 3001 and <= 4000
            lsSql = ""
            lsSql &= " select '3001 to 4000' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount'  from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count >= 3001"
            lsSql &= " and share_count <= 4000"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') between 3001 and 4000"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dtt5 = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt5)

            '>= 4001 and <= 5000
            lsSql = ""
            lsSql &= " select '4001 to 5000' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount'  from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count >= 4001"
            lsSql &= " and share_count <= 5000"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') between 4001 and 5000"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dtt6 = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt6)

            '>= 5001 and <= 10000
            lsSql = ""
            lsSql &= " select '5001 to 10000' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount' from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count >= 5001"
            lsSql &= " and share_count <= 10000"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') between 5001 and 10000"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dtt7 = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt7)

            '>= 10001 
            lsSql = ""
            lsSql &= " select 'Above 10000' as Category, sum(a.share_count) as 'Share Holders'  ,round((sum(a.share_count)/" & holder_count & ")*100,2) as 'Percentage of shareholders',"
            lsSql &= " sum(a.sum_count) as 'Share Amount',round((sum(a.sum_count)/" & sumcount & ")*100,2)  as 'Percentage of Share Amount'  from"
            lsSql &= " ("
            lsSql &= " SELECT count(share_count) as share_count,sum(share_count) as sum_count FROM sta_trn_tbenpost"
            lsSql &= " where True"
            lsSql &= lsCond
            lsSql &= " and share_count >= 10001"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " union all"
            lsSql &= " SELECT count(*) as share_count,sum(fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "')) as sum_count"
            lsSql &= " FROM sta_trn_tfolio a"
            lsSql &= " where true"
            lsSql &= lsCond2
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999')"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsDate & "') and a.folio_shares >= 10001"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " ) as a"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dtt8 = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt8)

            dt.Merge(dtt)
            dt.Merge(dtt3)
            dt.Merge(dtt4)
            dt.Merge(dtt5)
            dt.Merge(dtt6)
            dt.Merge(dtt7)
            dt.Merge(dtt8)

            Dim H_total As String = 0
            Dim H_Per As String = 0
            Dim S_total As String = 0
            Dim S_per As String = 0


            For i As Integer = 0 To dt.Rows.Count - 1
                H_total += Convert.ToInt32(dt.Rows(i)(1).ToString())
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                H_Per += Convert.ToDouble(dt.Rows(i)(2).ToString())
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                S_total += Convert.ToInt32(dt.Rows(i)(3).ToString())
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                S_per += Convert.ToDouble(dt.Rows(i)(4).ToString())
            Next

            dt.Rows.Add(New Object() {"Sub Total", H_total, H_Per, S_total, S_per})
            dgvList.DataSource = dt

        End If
    End Sub
    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With dgvList
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 48)
        End With


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