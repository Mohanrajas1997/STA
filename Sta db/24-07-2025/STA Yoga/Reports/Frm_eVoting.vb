Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Public Class Frm_eVoting
    Private Sub eVoting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql, Mysql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        Mysql = ""
        Mysql &= " SELECT depository_code,depository_name FROM sta_mst_tdepository"
        Mysql &= " where delete_flag = 'N' "

        Call gpBindCombo(Mysql, "depository_name", "depository_code", cbdepotype, gOdbcConn)


    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Dim lsSql, ls_Sql, squery, lsBenpostDate As String
        Dim cmd, cmd1 As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt, dtt, dtin As DataTable
        Dim lsCond As String = ""
        Dim ls_Cond As String = ""
        Dim lsCond_date As String = ""
        Dim lsCond_phy As String = ""
        Dim holder_count As String = ""
        Dim isin_id As String = ""
        Dim batch_no As String = ""

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond_phy &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If

        If dtp_bendate.Checked = False Then
            MessageBox.Show("Please select the date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtp_bendate.Focus()
            Exit Sub
        End If
        If dtp_bendate.Checked = True Then lsCond &= " and benpost_date = '" & Format(dtp_bendate.Value, "yyyy-MM-dd") & "' "
        If dtp_bendate.Checked = True Then lsCond_date = Format(dtp_bendate.Value, "yyyy-MM-dd")

        If Batchno.Text = "" And Evenno.Text = "" Then
            MessageBox.Show("Please fill values of Evenno and batchno!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Evenno.Focus()
            Exit Sub
        End If

        squery = ""
        squery &= " select comp_gid from sta_mst_tcompany where true and ifnull(isin_id,'') <> ''  and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' ;"

        cmd1 = New MySqlCommand(squery, gOdbcConn)
        Dim dataAdapter As MySqlDataAdapter = New MySqlDataAdapter(cmd1)
        Dim datatable As DataTable = New DataTable()
        dataAdapter.Fill(datatable)

        If datatable.Rows.Count > 0 Then

            lsSql = ""
            lsSql &= "SELECT benpost_date FROM sta_trn_tbenpost where true and benpost_date = '" & Format(dtp_bendate.Value, "yyyy-MM-dd") & "' and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' ;"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)(0).ToString) Then
                    lsBenpostDate = dt.Rows(0)(0).ToString
                End If
            Else
                MessageBox.Show("Invalid benpost date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtp_bendate.Focus()
                Exit Sub
            End If
        End If


        If cbdepotype.SelectedValue.ToString = "C" Then

            lsSql = ""
            lsSql &= " SELECT '01=',@s:=@s+1 Sno,'D' as depository_code,concat(a.dp_id,a.client_id) as folio,"
            lsSql &= " ifnull(if(a.holder1_pan = '',null,a.holder1_pan),a.client_id) as pan1,"
            lsSql &= " '' as Aadhar,"
            lsSql &= " ifnull(if(cast(a.bene_subtype as unsigned) = '',null,cast(a.bene_subtype as unsigned)),1) as Sub_Status,"
            lsSql &= " a.share_count as shares,"
            lsSql &= " REPLACE(a.holder1_name, '  ',' ') as name,"
            lsSql &= " REPLACE(a.holder2_name, '  ',' ') as jname1,"
            lsSql &= " REPLACE(a.holder3_name, '  ',' ') as jname2,"
            lsSql &= " REPLACE(a.holder1_per_addr1, '  ',' ') as address1,"
            lsSql &= " REPLACE(a.holder1_per_addr2, '  ',' ') as address2,"
            lsSql &= " REPLACE(a.holder1_per_addr3, '  ',' ') as address3,"
            lsSql &= " REPLACE(a.holder1_per_city, '  ',' ') as city,REPLACE(a.holder1_state, '  ',' ') as state,a.holder1_country as country,a.holder1_per_pin as pincode,"
            lsSql &= " '' as dob,"
            lsSql &= " ifnull(if(a.bank_acc_no = '',null,a.bank_acc_no),concat(a.dp_id,a.client_id))as bank,"
            lsSql &= " a.holder1_contact_no as mobile,a.holder1_email_id as email,'' as bflag,'' as breason"
            lsSql &= " FROM sta.sta_trn_tbenpost a,(SELECT @s:= 0) AS s where true"
            lsSql &= lsCond
            lsSql &= " union"
            lsSql &= " SELECT '01=',@s:=@s+1 Sno,'P' as depository_code, a.folio_no as folio,"
            lsSql &= " ifnull(if(a.holder1_pan_no = '',null,a.holder1_pan_no),a.folio_no)as pan1,"
            lsSql &= " '' as Aadhar,"
            lsSql &= " ifnull(if(a.category_gid = '',null,a.category_gid),1) as Sub_Status,"
            lsSql &= " fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "') as shares,"
            lsSql &= " REPLACE(a.holder1_name, '  ',' ') as name,"
            lsSql &= " REPLACE(a.holder2_name, '  ',' ') as jname1,"
            lsSql &= " REPLACE(a.holder3_name, '  ',' ') as jname2,"
            lsSql &= " REPLACE(a.folio_addr1, '  ',' ') as address1,"
            lsSql &= " REPLACE(a.folio_addr2, '  ',' ') as address2,"
            lsSql &= " REPLACE(a.folio_addr3, '  ',' ') as address3,"
            lsSql &= " REPLACE(a.folio_city, '  ',' ') as city,REPLACE(a.folio_state, '  ',' ') as state,a.folio_country as country,a.folio_pincode as pincode,"
            lsSql &= " '' as dob,"
            lsSql &= " ifnull(if(a.bank_acc_no = '',null,a.bank_acc_no),a.folio_no)as bank,"
            lsSql &= " a.folio_contact_no as mobile,a.folio_mail_id as email,'' as bflag,'' as breason"
            lsSql &= " FROM sta_trn_tfolio a ,(SELECT @s:= 0) AS s where true"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "') > 0"
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999') and a.delete_flag = 'N'"
            lsSql &= lsCond_phy


            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            'comp isin number
            ls_Sql &= " SELECT isin_id FROM sta_mst_tcompany where true and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            cmd = New MySqlCommand(ls_Sql, gOdbcConn)
            dtin = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtin)
            If dtin.Rows.Count > 0 Then
                If Not IsDBNull(dtin.Rows(0)(0).ToString) Then
                    isin_id = dtin.Rows(0)(0).ToString
                End If
            End If

            'holder count
            ls_Sql = ""
            ls_Sql &= "SELECT(SELECT count(a.benpost_gid) FROM sta_trn_tbenpost a where true " & lsCond_phy & " and benpost_date = '" & lsCond_date & "' )+"
            ls_Sql &= "(SELECT count(fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "')) as cunt FROM sta_trn_tfolio a "
            ls_Sql &= " where true " & lsCond_phy & " and a.folio_no NOT IN ('00888888','00999999') and (fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "')) > 0 )"
            ls_Sql &= " as totalholdercount;"
            cmd = New MySqlCommand(ls_Sql, gOdbcConn)
            dtt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt)
            If dtt.Rows.Count > 0 Then
                If Not IsDBNull(dtt.Rows(0)(0).ToString) Then
                    holder_count = dtt.Rows(0)(0).ToString
                End If
            End If

            'text file
            Dim fileloc As String = "C:\covering\1.txt"
            Dim txt As String = String.Empty
            For Each row As DataRow In dt.Rows
                For Each column As DataColumn In dt.Columns
                    'txt += row(column.ColumnName).ToString() & "~"
                Next
                'Add new line.
                txt += vbCr & vbLf
            Next

            If File.Exists(fileloc) Then
                Using sw As StreamWriter = New StreamWriter(fileloc)
                    sw.WriteLine("00=" & holder_count & "~" & isin_id & "~" & Val(Evenno.Text).ToString)
                    For Each row As DataRow In dt.Rows
                        Dim name As String = Regex.Replace(row("name").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim address1 As String = Regex.Replace(row("address1").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim address2 As String = Regex.Replace(row("address2").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim address3 As String = Regex.Replace(row("address3").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim city As String = Regex.Replace(row("city").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim state As String = Regex.Replace(row("state").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim country As String = Regex.Replace(row("country").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim pincode As String = Regex.Replace(row("pincode").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")

                        sw.WriteLine("01=" & row("Sno").ToString() & "~" & row("depository_code").ToString() & "~" & row("folio").ToString() & "~EV" & row("pan1").ToString() & "~" & row("Aadhar").ToString() & "~" & row("Sub_Status").ToString() & "~" & row("shares").ToString() & "~" & row("name").ToString() & "~" & row("jname1").ToString() & "~" & row("jname2").ToString() & "~" & address1 & "~" & address2 & "~" & address3 & "~" & city & "~" & state & "~" & country & "~" & pincode & "~" & row("dob").ToString() & "~" & row("bank").ToString() & "~" & row("mobile").ToString() & "~" & row("email").ToString() & "~" & row("bflag").ToString() & "~" & row("breason").ToString() & vbTab)
                    Next
                End Using
            End If

            If System.IO.File.Exists(fileloc) = True Then
                Process.Start(fileloc)
            Else
                MsgBox("File Does Not Exist")
            End If
            'nsdl
        ElseIf cbdepotype.SelectedValue.ToString = "N" Then
            Dim lss_Sql As String = ""
            batch_no = Val(Batchno.Text).ToString
            lsSql = ""
            lsSql &= " SELECT "
            lsSql &= batch_no
            lsSql &= " ,'12',a.depository_code as depository_code,concat(a.dp_id,a.client_id) as folio,"
            lsSql &= " REPLACE(a.holder1_name, '  ',' ') as name,"
            lsSql &= " a.holder1_pan as pan1,"
            lsSql &= " REPLACE(a.holder2_name, '  ',' ') as jname1,a.holder2_pan as pan2,"
            lsSql &= " REPLACE(a.holder3_name, '  ',' ') as jname2,a.holder3_pan as pan3,"
            lsSql &= " REPLACE(a.holder1_per_addr1, '  ',' ') as address1,"
            lsSql &= " REPLACE(a.holder1_per_addr2, '  ',' ') as address2,REPLACE(a.holder1_per_addr3, '  ',' ') as address3,"
            lsSql &= " REPLACE(a.holder1_per_city, '  ',' ') as city,a.holder1_per_pin as pincode,a.holder1_contact_no as mobile,a.holder1_email_id as email,"
            lsSql &= " convert(rpad(lpad(a.share_count,15,0),18,0),char(20)) as shares"
            lsSql &= " FROM sta.sta_trn_tbenpost a where true"
            lsSql &= lsCond
            lsSql &= " union"
            lsSql &= " SELECT "
            lsSql &= batch_no
            lsSql &= " ,'12','P' as depository_code, a.folio_no as folio,REPLACE(a.holder1_name, '  ',' ') as name,"
            lsSql &= " a.holder1_pan_no as pan1,REPLACE(a.holder2_name, '  ',' ') as jname1,a.holder2_pan_no as pan2,REPLACE(a.holder3_name, '  ',' ') as jname2,a.holder3_pan_no as pan3,"
            lsSql &= " REPLACE(a.folio_addr1, '  ',' ') as address1,"
            lsSql &= " REPLACE(a.folio_addr2, '  ',' ') as address2,"
            lsSql &= " REPLACE(a.folio_addr3, '  ',' ') as address3,"
            lsSql &= " REPLACE(a.folio_city, '  ',' ') as city,REPLACE(a.folio_pincode, '  ',' ') as pincode,a.folio_contact_no as mobile,a.folio_mail_id as email,"
            lsSql &= " convert(rpad(lpad( fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "'),15,0),18,0),char(20)) as shares"
            lsSql &= " FROM sta_trn_tfolio a  where true"
            lsSql &= " and fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "') > 0"
            lsSql &= " and a.folio_no NOT IN ('00888888','00999999') and a.delete_flag = 'N'"
            lsSql &= lsCond_phy


            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            'ISIN number
            lss_Sql &= " SELECT isin_id FROM sta_mst_tcompany where true and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            cmd = New MySqlCommand(lss_Sql, gOdbcConn)
            dtin = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtin)
            If dtin.Rows.Count > 0 Then
                If Not IsDBNull(dtin.Rows(0)(0).ToString) Then
                    isin_id = dtin.Rows(0)(0).ToString
                End If
            End If
            'holder count
            ls_Sql = ""
            ls_Sql &= "SELECT(SELECT count(a.benpost_gid) FROM sta_trn_tbenpost a where true " & lsCond_phy & " and benpost_date = '" & lsCond_date & "' )+"
            ls_Sql &= "(SELECT count(fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "')) as cunt FROM sta_trn_tfolio a "
            ls_Sql &= " where true " & lsCond_phy & " and a.folio_no NOT IN ('00888888','00999999') and (fn_sta_get_folioshares(a.folio_gid,'" & lsCond_date & "')) > 0 )"
            ls_Sql &= " as totalholdercount;"
            cmd = New MySqlCommand(ls_Sql, gOdbcConn)
            dtt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtt)
            If dtt.Rows.Count > 0 Then
                If Not IsDBNull(dtt.Rows(0)(0).ToString) Then
                    holder_count = dtt.Rows(0)(0).ToString
                End If
            End If

            Dim cleanString As String = Regex.Replace(lsCond_date, "[-]", "")
            Dim fileloc As String = "C:\covering\1.txt"
            Dim txt As String = String.Empty
            For Each row As DataRow In dt.Rows
                For Each column As DataColumn In dt.Columns
                    'txt += row(column.ColumnName).ToString() & "^"
                Next
            Next
            If File.Exists(fileloc) Then
                Using sw As StreamWriter = New StreamWriter(fileloc)
                    sw.WriteLine("{}")
                    sw.WriteLine(batch_no & "^11^IN200703^" & isin_id & "^" & cleanString & "^" & holder_count & "^" & Val(Evenno.Text).ToString)
                    For Each row As DataRow In dt.Rows
                        Dim name As String = Regex.Replace(row("name").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim address1 As String = Regex.Replace(row("address1").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim address2 As String = Regex.Replace(row("address2").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim address3 As String = Regex.Replace(row("address3").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim city As String = Regex.Replace(row("city").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        Dim pincode As String = Regex.Replace(row("pincode").ToString(), "[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")
                        sw.WriteLine(batch_no & "^" & row("12").ToString() & "^" & row("depository_code").ToString() & "^" & row("folio").ToString() & "^" & name & "^" & row("pan1").ToString() & "^" & row("jname1").ToString() & "^" & row("pan2").ToString() & "^" & row("jname2").ToString() & "^" & row("pan3").ToString() & "^" & address1 & "^" & address2 & "^" & address3 & "^" & city & "^" & pincode & "^" & row("mobile").ToString() & "^" & row("email").ToString() & "^" & row("shares").ToString() & vbTab)
                    Next
                    sw.WriteLine("{}")
                End Using
            End If

            If System.IO.File.Exists(fileloc) = True Then
                Process.Start(fileloc)
            Else
                MsgBox("File Does Not Exist")
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
        dgvList.DataSource = Nothing
    End Sub

    Private Sub Evenno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Evenno.KeyPress
        If Not cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            MessageBox.Show("Please select the Depository Type before enter Even / Evsn No !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Call frmCtrClear(Me)
            Exit Sub
        End If
        If cbdepotype.SelectedValue.ToString = "N" Then
            If Evenno.Text.Length >= 12 Then
                If e.KeyChar <> ControlChars.Back Then
                    e.Handled = True
                End If
            End If
        ElseIf Evenno.Text.Length >= 9 Then
            If e.KeyChar <> ControlChars.Back Then
                e.Handled = True
            End If
        End If

    End Sub
   

    Private Sub Evenno_TextChanged(sender As Object, e As EventArgs) Handles Evenno.TextChanged
        Dim tt As New ToolTip With {.IsBalloon = True}
        For Each ch As Char In Evenno.Text
            If Not Char.IsDigit(ch) Then
                Evenno.Clear()
                tt.Show("Please Enter Valid 9 Numbers Only", Evenno, New Point(0, -40), 4000)
            End If
        Next

    End Sub

    Private Sub Batchno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Batchno.KeyPress
       
        If Batchno.Text.Length >= 7 Then
            If e.KeyChar <> ControlChars.Back Then
                e.Handled = True
            End If
        End If
    End Sub


    Private Sub Batchno_TextChanged(sender As Object, e As EventArgs) Handles Batchno.TextChanged
        Dim tt As New ToolTip With {.IsBalloon = True}
        For Each ch As Char In Batchno.Text
            If Not Char.IsDigit(ch) Then
                Batchno.Clear()
                tt.Show("Please Enter Valid 7 Numbers Only", Batchno, New Point(0, -40), 4000)
            End If
        Next
    End Sub

    Private Sub cbdepotype_LostFocus(sender As Object, e As EventArgs) Handles cbdepotype.LostFocus

        If cbdepotype.SelectedValue.ToString = "C" Then
            'Call frmCtrClear(Me)
            Evenno.Text = ""
            Batchno.Text = ""
        ElseIf cbdepotype.SelectedValue.ToString = "N" Then
            Evenno.Text = ""
            Batchno.Text = ""
        End If
    End Sub
   
End Class