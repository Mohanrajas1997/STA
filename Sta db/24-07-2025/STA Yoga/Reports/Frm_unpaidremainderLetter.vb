Imports MySql.Data.MySqlClient
Imports System.IO
Public Class Frm_unpaidremainderLetter

    Private Sub Frm_unpaidremainderLetter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    End Sub

    Private Sub Bulkmail_Click(sender As Object, e As EventArgs) Handles Bulkmail.Click
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
            MessageBox.Show("Please select the Interim !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cb_interim.Focus()
            Exit Sub
        End If

        If dtp_wardate.Checked = False Then
            MessageBox.Show("Please select the warrant date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtp_wardate.Focus()
            Exit Sub
        End If
        If dtp_iepf.Checked = False Then
            MessageBox.Show("Please select the IEPF date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtp_iepf.Focus()
            Exit Sub
        End If
        If dtplastdate.Checked = False Then
            MessageBox.Show("Please select the Last submission date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtplastdate.Focus()
            Exit Sub
        End If

        lsSql = ""
        lsSql &= " SELECT a.dividend_gid,a.file_gid,a.comp_gid,a.finyear_gid,a.interim_code,a.dp_id,a.folioclient_id,a.folioclient_id_type,a.holder1_name,a.share_count,a.dividend_per_share,"
        lsSql &= " a.currency_code,a.currency_value,a.currency_amount,a.dividend_amount,a.dividend_status,a.curr_dividend_status,a.payment_mode,a.payment_status,a.paid_refno,"
        lsSql &= " a.bene_name,a.bank_name,a.bank_branch,a.bank_acc_no,a.bank_acc_type,a.bank_micr_code,a.bank_ifsc_code,a.holder1_addr1,a.holder1_addr2,a.holder1_addr3,a.holder1_addr4,a.holder1_city,"
        lsSql &= " a.holder1_state,a.holder1_country,a.holder1_pincode,a.email_id,a.insert_date,"
        lsSql &= " a.insert_by,"
        lsSql &= " a.update_by,a.delete_flag"
        lsSql &= " FROM sta_trn_tdividend a"
        lsSql &= " where true"
        lsSql &= lsCond
        lsSql &= " and a.payment_status != '8' and a.curr_dividend_status != '8' "

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)
        If dt.Rows.Count <= 0 Then
            MessageBox.Show("No Records Found")
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Dim lsSql As String
        Dim ls_Sql As String
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
            MessageBox.Show("Please select the Interim !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cb_interim.Focus()
            Exit Sub
        End If

        If dtp_wardate.Checked = False Then
            MessageBox.Show("Please select the warrant date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtp_wardate.Focus()
            Exit Sub
        End If
        If dtp_iepf.Checked = False Then
            MessageBox.Show("Please select the IEPF date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtp_iepf.Focus()
            Exit Sub
        End If
        If dtplastdate.Checked = False Then
            MessageBox.Show("Please select the Last submission date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtplastdate.Focus()
            Exit Sub
        End If


        lsSql = ""
        lsSql &= " SELECT a.dividend_gid,a.file_gid,a.comp_gid,a.finyear_gid,a.interim_code,a.dp_id,a.folioclient_id,a.folioclient_id_type,a.holder1_name,a.share_count,a.dividend_per_share,"
        lsSql &= " a.currency_code,a.currency_value,a.currency_amount,a.dividend_amount,a.dividend_status,a.curr_dividend_status,a.payment_mode,a.payment_status,a.paid_refno,"
        lsSql &= " a.bene_name,a.bank_name,a.bank_branch,a.bank_acc_no,a.bank_acc_type,a.bank_micr_code,a.bank_ifsc_code,a.holder1_addr1,a.holder1_addr2,a.holder1_addr3,a.holder1_addr4,a.holder1_city,"
        lsSql &= " a.holder1_state,a.holder1_country,a.holder1_pincode,a.email_id,a.insert_date,"
        lsSql &= " a.insert_by,"
        lsSql &= " a.update_by,a.delete_flag"
        lsSql &= " FROM sta_trn_tdividend a"
        lsSql &= " where true"
        lsSql &= lsCond
        lsSql &= " and a.payment_status != '8' and a.curr_dividend_status != '8' "

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)
        dgvList1.DataSource = dt

        For Each rw1 As DataGridViewRow In dgvList1.Rows
            Dim insert_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")

            ls_Sql = ""
            ls_Sql &= " INSERT INTO sta_trn_tdividend_unpaid"
            ls_Sql &= " (dividend_gid,file_gid,comp_gid,finyear_gid,interim_code,dp_id,folioclient_id,folioclient_id_type,"
            ls_Sql &= " holder1_name,share_count,dividend_per_share,currency_code,currency_value,currency_amount,dividend_amount,"
            ls_Sql &= " dividend_status,curr_dividend_status,payment_mode,payment_status,paid_refno,bene_name,bank_name,bank_branch,"
            ls_Sql &= " bank_acc_no, bank_acc_type, bank_micr_code, bank_ifsc_code, holder1_addr1, "
            ls_Sql &= " holder1_addr2,holder1_addr3,holder1_addr4,holder1_city,holder1_state,holder1_country,holder1_pincode,email_id,"
            ls_Sql &= " insert_date,insert_by,update_by,delete_flag)"
            ls_Sql &= " values ('" & rw1.Cells(0).Value & "','" & rw1.Cells(1).Value & "','" & rw1.Cells(2).Value & "','" & rw1.Cells(3).Value & "',"
            ls_Sql &= " '" & rw1.Cells(4).Value & "','" & rw1.Cells(5).Value & "','" & rw1.Cells(6).Value & "','" & rw1.Cells(7).Value & "','" & rw1.Cells(8).Value & "',"
            ls_Sql &= " '" & rw1.Cells(9).Value & "','" & rw1.Cells(10).Value & "','" & rw1.Cells(11).Value & "','" & rw1.Cells(12).Value & "',"
            ls_Sql &= " '" & rw1.Cells(13).Value & "','" & rw1.Cells(14).Value & "','" & rw1.Cells(15).Value & "','" & rw1.Cells(16).Value & "',"
            ls_Sql &= " '" & rw1.Cells(17).Value & "',"
            ls_Sql &= " '" & rw1.Cells(18).Value & "',"
            ls_Sql &= " '" & rw1.Cells(19).Value & "',"
            ls_Sql &= " '" & rw1.Cells(20).Value & "',"
            ls_Sql &= " '" & rw1.Cells(21).Value & "','" & rw1.Cells(22).Value & "','" & rw1.Cells(23).Value & "','" & rw1.Cells(24).Value & "','" & rw1.Cells(25).Value & "',"
            ls_Sql &= " '" & rw1.Cells(26).Value & "','" & rw1.Cells(27).Value & "','" & rw1.Cells(28).Value & "','" & rw1.Cells(29).Value & "','" & rw1.Cells(30).Value & "',"
            ls_Sql &= " '" & rw1.Cells(31).Value & "','" & rw1.Cells(32).Value & "','" & rw1.Cells(33).Value & "','" & rw1.Cells(34).Value & "',"
            ls_Sql &= " '" & rw1.Cells(35).Value & "','" & insert_date & "',"
            ls_Sql &= " '" & rw1.Cells(37).Value & "',"
            ls_Sql &= " '" & rw1.Cells(38).Value & "','" & rw1.Cells(39).Value & "' ) "

            cmd = New MySqlCommand(ls_Sql, gOdbcConn)
            cmd.ExecuteNonQuery()
        Next
        'generate letter in text file
        Dim lsdate As String = ""
        Dim dtplast As String = ""
        Dim dtpiepf As String = ""
        Dim numtowrd As String = ""
        If dtp_wardate.Checked = True Then lsdate &= "" & Format(dtp_wardate.Value, "dd-MM-yyyy") & " "
        If dtplastdate.Checked = True Then dtplast &= " " & Format(dtplastdate.Value, "dd-MMMM-yyyy") & " "
        If dtp_iepf.Checked = True Then dtpiepf &= " " & Format(dtp_iepf.Value, "dd-MMMM-yyyy") & " "

        lsSql = ""
        lsSql &= " SELECT b.comp_code,b.comp_name,a.dividend_gid,a.file_gid,a.comp_gid,a.finyear_gid,a.interim_code,a.dp_id,a.folioclient_id,a.folioclient_id_type,a.holder1_name,a.share_count,a.dividend_per_share,"
        lsSql &= " a.currency_code,a.currency_value,a.currency_amount,a.dividend_amount,a.dividend_status,a.curr_dividend_status,a.payment_mode,a.payment_status,a.paid_refno,"
        lsSql &= " a.bene_name,a.bank_name,a.bank_branch,a.bank_acc_no,a.bank_acc_type,a.bank_micr_code,a.bank_ifsc_code,a.holder1_addr1,a.holder1_addr2,a.holder1_addr3,a.holder1_addr4,a.holder1_city,"
        lsSql &= " a.holder1_state,a.holder1_country,a.holder1_pincode,a.email_id,a.insert_date,"
        lsSql &= " a.insert_by,"
        lsSql &= " a.update_by,a.delete_flag"
        lsSql &= " FROM sta_trn_tdividend_unpaid a"
        lsSql &= " left join sta_mst_tcompany b on a.comp_gid = b.comp_gid"
        lsSql &= " where true"
        lsSql &= lsCond

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)


        Dim fileloc As String = "C:\covering\1.txt"
        Dim txt As String = String.Empty
        txt += vbCr & vbLf

        Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
        
        If File.Exists(fileloc) Then

            Using sw As StreamWriter = New StreamWriter(fileloc)
                For Each row As DataRow In dt.Rows
                    numtowrd = NumberToString(CDbl(row("dividend_amount").ToString()))

                    sw.WriteLine(vbTab & vbTab & vbTab & vbTab & vbTab & "Reminder Letter")
                    sw.WriteLine("")
                    sw.WriteLine("Ref : " & row("comp_code").ToString() & "\DIV\" & Cb_finyear.Text & "\" & Cb_interim.Text & "\IEPF\" & vbTab & vbTab & vbTab & vbTab & vbTab & "Date : " & todaysdate)
                    sw.WriteLine("")
                    sw.WriteLine("       " & row("holder1_name").ToString() & "")
                    sw.WriteLine("       " & row("holder1_addr1").ToString() & "")
                    sw.WriteLine("       " & row("holder1_addr2").ToString() & "")
                    sw.WriteLine("       " & row("holder1_addr3").ToString() & "")
                    sw.WriteLine("       " & row("holder1_addr4").ToString() & "")
                    sw.WriteLine("       " & row("holder1_city").ToString() & "")
                    sw.WriteLine("       " & row("holder1_state").ToString() & "")
                    sw.WriteLine("       " & row("holder1_pincode").ToString() & "")
                    sw.WriteLine("")
                    sw.WriteLine("Dear " & row("holder1_name").ToString() & ",")
                    sw.WriteLine("")
                    sw.WriteLine(vbTab & vbTab & "  Sub : Payment of Dividend for the year : " & Cb_finyear.Text & " and Interim : " & Cb_interim.Text)
                    sw.WriteLine(vbTab & vbTab & "  Ref : Folio/Client id : " & row("folioclient_id").ToString() & "/" & row("dp_id").ToString())
                    sw.WriteLine("")
                    sw.WriteLine("While verifying our records, we understand that the dividend warrant bearing No : " & row("dividend_gid").ToString() & " and warrant Dated ")
                    sw.WriteLine(lsdate & " issued in your favour for Rs." & row("dividend_amount").ToString() & " /- ( " & numtowrd & ") still remains unpaid ")
                    sw.WriteLine("in our books.")
                    sw.WriteLine("")
                    sw.WriteLine("In this connection, we wish to state that as per the provisions of section 124 of the Companies Act, 2013")
                    sw.WriteLine("and its applicable subsequent amendments if any, the said dividend amount which has remained unpaid will")
                    sw.WriteLine("have to be transferred to the Investors Education and Protection Fund  on expiry of 7 years from the")
                    sw.WriteLine("date of issue.")
                    sw.WriteLine("")
                    sw.WriteLine("In case you are in possession of the un-encashed Dividend warrant cited above, the same would have become")
                    sw.WriteLine("out-dated by now.  So the same can be surrendered to us to enable us to send a fresh Demand Draft in lieu")
                    sw.WriteLine("of the same or else you may sign and return the enclosed Indemnity Bond / declaration to enable us forward")
                    sw.WriteLine("a fresh Demand Draft to your address cited above.")
                    sw.WriteLine("")
                    sw.WriteLine("We also request you to furnish your full bank account particulars as detailed below to enable us to transfer ")
                    sw.WriteLine("the present as well as the future dividends through ECS mode.")
                    sw.WriteLine("")
                    sw.WriteLine(" Name of the Bank                  : " & row("bank_name").ToString())
                    sw.WriteLine(" Complete Bank Address             : " & row("bank_branch").ToString() & "")
                    sw.WriteLine(" Type of account  & Account Number : " & row("bank_acc_type").ToString())
                    sw.WriteLine(" IFSC Code Number                  : " & row("bank_ifsc_code").ToString())
                    sw.WriteLine(" MICR Code Number                  : " & row("bank_micr_code").ToString())
                    sw.WriteLine(" Cancelled Cheque leaf             : ")
                    sw.WriteLine(" Mobile No. & E-mail ID            : " & row("email_id").ToString())
                    sw.WriteLine("")
                    sw.WriteLine("We request you to respond to this letter immediately so as to make your reply reach us on or before ")
                    sw.WriteLine(dtplast & "to enable us to respond effectively or else we will be transferring your un-encashed amount to ")
                    sw.WriteLine("the credit of the Investor Education and Protection Fund on the due date " & dtpiepf)
                    sw.WriteLine("")
                    sw.WriteLine("****In case there are any changes in your mailing address printed above,please inform us accordingly along ")
                    sw.WriteLine("with your address proof duly self-attested")
                    sw.WriteLine("")
                    sw.WriteLine("Thanking you.,")
                    sw.WriteLine("For " & row("comp_name").ToString())
                    sw.WriteLine("")
                    sw.WriteLine("Company Secretary ")
                    sw.WriteLine("Encl : as above")
                    sw.WriteLine("")
                    sw.WriteLine("________________________________________________________________________________________________")
                    sw.WriteLine("")
                    sw.WriteLine("")
                    sw.WriteLine("")
                    sw.WriteLine("")
                Next
            End Using
        End If

        If System.IO.File.Exists(fileloc) = True Then
            Process.Start(fileloc)
        Else
            MsgBox("File Does Not Exist")
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
    Private Function GroupToWords(ByVal num As Integer) As _
    String
        Static one_to_nineteen() As String = {"zero", "One", _
            "Two", "Three", "Four", "Five", "Six", "Seven", _
            "Eight", "Nine", "Ten", "Eleven", "Twelve", _
            "Thirteen", "Fourteen", "Fifteen", "Sixteen", _
            "Seventeen", "Eightteen", "Nineteen"}
        Static multiples_of_ten() As String = {"Twenty", _
            "Thirty", "Forty", "Fifty", "Sixty", "Seventy", _
            "Eighty", "Ninety"}

        ' If the number is 0, return an empty string.
        If num = 0 Then Return ""

        ' Handle the hundreds digit.
        Dim digit As Integer
        Dim result As String = ""
        If num > 99 Then
            digit = num \ 100
            num = num Mod 100
            result = one_to_nineteen(digit) & " Hundred"
        End If

        ' If num = 0, we have hundreds only.
        If num = 0 Then Return result.Trim()

        ' See if the rest is less than 20.
        If num < 20 Then
            ' Look up the correct name.
            result &= " " & one_to_nineteen(num)
        Else
            ' Handle the tens digit.
            digit = num \ 10
            num = num Mod 10
            result &= " " & multiples_of_ten(digit - 2)

            ' Handle the final digit.
            If num > 0 Then
                result &= " " & one_to_nineteen(num)
            End If
        End If

        Return result.Trim()
    End Function
    Private Function NumberToString(ByVal num As Double) As _
    String
        ' Remove any fractional part.
        num = Int(num)

        ' If the number is 0, return zero.
        If num = 0 Then Return "zero"

        Static groups() As String = {"", "Thousand", "million", _
            "billion", "trillion", "quadrillion", "?", "??", _
            "???", "????"}
        Dim result As String = ""

        ' Process the groups, smallest first.
        Dim quotient As Double
        Dim remainder As Integer
        Dim group_num As Integer = 0
        Do While num > 0
            ' Get the next group of three digits.
            quotient = Int(num / 1000)
            remainder = CInt(num - quotient * 1000)
            num = quotient

            ' Convert the group into words.
            result = GroupToWords(remainder) & _
                " " & groups(group_num) & ", " & _
                result

            ' Get ready for the next group.
            group_num += 1
        Loop

        ' Remove the trailing ", ".
        If result.EndsWith(", ") Then
            result = result.Substring(0, result.Length - 2)
        End If

        Return result.Trim()
    End Function

    
End Class