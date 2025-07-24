Imports MySql.Data.MySqlClient
Public Class frmanul_report

    Private Sub frmanul_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        cbtype.Items.Clear()
        cbtype.Items.Add("SHH")
        cbtype.Items.Add("SHT")

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim lsSql As String
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt, dben, dtt As DataTable

        Dim lsCond As String = ""
        Dim ls_Sql As String = ""
        Dim ls_ben As String = ""
        Dim ben_date As String = ""
        Dim lsCond_date As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If


        If dtp_anualrpt.Checked = True Then lsCond_date = Format(dtp_anualrpt.Value, "yyyy-MM-dd")

        ''''''''''''''''''''
        ls_ben = ""
        ls_ben &= "SELECT benpost_date FROM sta_trn_tbenpost as a where true " & lsCond & " and benpost_date = '" & lsCond_date & "' ;"
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
        If dtp_tranfrom.Checked = False And dtp_tranto.Checked = False Then
            lsSql = ""
            lsSql &= " SELECT"
            lsSql &= " folio_no as Folio,"
            lsSql &= " holder1_name as Name,"
            lsSql &= " holder1_fh_name as Fname,"
            lsSql &= " folio_addr1 as Add1,"
            lsSql &= " folio_addr2 as Add2,"
            lsSql &= " folio_addr3 as Add3,"
            lsSql &= " folio_city as Place,folio_pincode as Pin,"
            lsSql &= " '1' as Type_share,fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "') as shares,"
            lsSql &= " '10' as Amount FROM sta_trn_tfolio a"
            lsSql &= " where true " & lsCond & " and fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "') > 0"
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999') and a.delete_flag = 'N' "
            lsSql &= " union"
            lsSql &= " select"
            lsSql &= " CONCAT(a.dp_id,a.client_id) as Folio,"
            lsSql &= " holder1_name as Name,"
            lsSql &= " holder1_fh_name as Fname,"
            lsSql &= " holder1_addr1 as Add1,"
            lsSql &= " holder1_addr2 as Add2,"
            lsSql &= " holder1_addr2 as Add3,"
            lsSql &= " holder1_city as Place,"
            lsSql &= " holder1_pin as Pin,"
            lsSql &= " '1' as Type_share,share_count as Shares,"
            lsSql &= " '10' as Amount FROM sta_trn_tbenpost a"
            lsSql &= " where true " & lsCond & " and benpost_date='" & lsCond_date & "';"

            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            If ben_date = "" Then
                MessageBox.Show("Please Enter Valid Benpost Date")
            Else

                With dgvList
                    .DataSource = dt
                    For i = 0 To .ColumnCount - 1
                        .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    Next i

                    txtTotRec.Text = "Total Records : " & .RowCount.ToString
                End With
            End If


        ElseIf dtp_tranfrom.Checked = True And dtp_tranto.Checked = True Then
            ls_Sql = ""
            ls_Sql &= " SELECT DATE_FORMAT(a.approved_date, '%d/%m/%Y') as Date_,"
            ls_Sql &= " '1' as type_,"
            ls_Sql &= " a.share_count as Shares,"
            ls_Sql &= " a.cons_amount as Amount,"
            ls_Sql &= " c.folio_no as Fromfolio,"
            ls_Sql &= " c.holder1_name as Fromname,"
            ls_Sql &= " b.folio_no as Tofolio,"
            ls_Sql &= " b.holder1_name as Toname"
            ls_Sql &= " FROM sta_trn_tinward a"
            ls_Sql &= " inner join sta_trn_tfolio b on b.folio_gid = a.tran_folio_gid"
            ls_Sql &= " inner join sta_trn_tfolio c on c.folio_gid = a.folio_gid"
            ls_Sql &= " where true"
            ls_Sql &= lsCond
            ls_Sql &= " and b.folio_no NOT IN ('00888888','00999999')"
            ls_Sql &= " and c.folio_no NOT IN ('00888888','00999999')"
            ls_Sql &= " and a.approved_date != '' and a.share_count != '0'"
            ls_Sql &= " and a.approved_date between '" & Format(dtp_tranfrom.Value, "yyyy-MM-dd") & "'  and '" & Format(dtp_tranto.Value, "yyyy-MM-dd") & "';"


            cmd = New MySqlCommand(ls_Sql, gOdbcConn)
            dtt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt)

            If dtt.Rows.Count <= 0 Then
                MessageBox.Show("Please Enter Valid Tran Date")
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

    Private Sub cbtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbtype.SelectedIndexChanged
       
        Select Case cbtype.SelectedItem.ToString
           
            Case "SHH"
                dtp_anualrpt.Visible = True
                bendate.Visible = True
                dtp_tranfrom.Visible = False
                trandatefrom.Visible = False
                dtp_tranto.Visible = False
                trandateto.Visible = False
            Case Else
                dtp_tranfrom.Visible = True
                trandatefrom.Visible = True
                dtp_tranto.Visible = True
                trandateto.Visible = True
                dtp_anualrpt.Visible = False
                bendate.Visible = False
        End Select
    End Sub

    
    
   
    Private Sub bendate_Click(sender As Object, e As EventArgs) Handles bendate.Click

    End Sub
End Class