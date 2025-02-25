Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmEvotingCdsl
    Dim fsFilePath As String = ""
    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' Company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", CboCompany, gOdbcConn)


    End Sub

    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lsShareCount As Long

        Dim lncompid As Long
        Dim lnnsdlid As Long
        Dim lnBatchno As Long
        Dim lnShrid As String
        Dim ldCutoffDate As Date
        Dim lnEvenNo As Long
        Dim lnHeaderGid As Integer

        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lsSheetName As String = ""
        Dim lnFileId As Long
        Dim lsErrFileName As String = ""

        Dim i As Integer
        Dim lsFldName(23) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0
        Dim ShowFlag As Boolean = True
        Dim lobjExcelDatatable As New DataTable
        Dim lsTotalShares As Long


        Dim lsDepositoryType As String
        Dim lsFolio As String
        Dim lsPanNo As String
        Dim lsAadharNo As String
        Dim lsSubStatus As Long
        Dim lsShares As Long
        Dim lsHolderName As String
        Dim lsJTName1 As String
        Dim lsJTName2 As String
        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsPlace As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPin As String
        Dim lsDob As String
        Dim lsBankAccNo As String
        Dim lsMobileNo As String
        Dim lsEmailId As String
        Dim lsBlock As String
        Dim lsBlockAreas As String




        Dim lobjFileReturn As New clsFileReturn
        lsFldName(1) = "SLNO"
        lsFldName(2) = "DEPOSITORY"
        lsFldName(3) = "FOLIO"
        lsFldName(4) = "PANNO"
        lsFldName(5) = "AADHAR"
        lsFldName(6) = "SUB STATUS"
        lsFldName(7) = "SHARES"
        lsFldName(8) = "NAME"
        lsFldName(9) = "JTNAME1"
        lsFldName(10) = "JTNAME2"
        lsFldName(11) = "ADD1"
        lsFldName(12) = "ADD2"
        lsFldName(13) = "ADD3"
        lsFldName(14) = "PLACE"
        lsFldName(15) = "STATE"
        lsFldName(16) = "COUNTRY"
        lsFldName(17) = "PIN"
        lsFldName(18) = "DOB"
        lsFldName(19) = "BANKACCNO"
        lsFldName(20) = "MOBILE"
        lsFldName(21) = "EMAIL"
        lsFldName(22) = "BLOCK"
        lsFldName(23) = "BLOCKREAS"

        Try

            If txtFileName.Text = "" Then
                MsgBox("Select File Name", MsgBoxStyle.Information, gsProjectName)
                txtFileName.Focus()
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            lsSheetName = cboSheetName.Text
            fsFilePath = txtFileName.Text.Trim

            Call FormatSheet(fsFilePath, cboSheetName.Text)

            lsFileName = QuoteFilter(fsFilePath.Substring(fsFilePath.LastIndexOf("\") + 1))
            lobjExcelDatatable = gpExcelDataset("select * from [" & cboSheetName.Text & "$]", fsFilePath)

            For i = 1 To 23
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 23
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    Exit Sub
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(lsSheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnCDSLEvoting)
                cmd.Parameters("?in_file_type").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
                cmd.Parameters("?in_action_by").Direction = ParameterDirection.Input
                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_file_gid", MySqlDbType.Int64)
                cmd.Parameters("?out_file_gid").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())

                If (lnResult = 0) Then
                    lobjFileReturn.Msg = cmd.Parameters("?out_msg").Value.ToString()
                    If ShowFlag Then MsgBox(lobjFileReturn.Msg)
                    Exit Sub
                End If
                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using


            If CboCompany.SelectedIndex <> -1 Then
                lncompid = Val(CboCompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            lnEvenNo = Val(TxtEvenNo.Text)

            Using cmd As New MySqlCommand("pr_sta_ins_tevoting_cdsl_header", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                cmd.Parameters.AddWithValue("?in_cdsl_header_gid", lnnsdlid)
                cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                cmd.Parameters.AddWithValue("?in_even_no", lnEvenNo)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
                'Out put Para
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_share_count", MySqlDbType.Double)
                cmd.Parameters("?out_share_count").Direction = ParameterDirection.Output
                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnHeaderGid = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()
                lsShareCount = Val(cmd.Parameters("?out_share_count").Value.ToString())

                'If lnHeaderGid > 0 Then
                '    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                'Else

                If lnHeaderGid = 0 Then
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)

                        Application.DoEvents()

                        lsDepositoryType = Mid(QuoteFilter(.Item("DEPOSITORY").ToString), 1, 12)
                        lsFolio = Mid(QuoteFilter(.Item("FOLIO").ToString), 1, 64)
                        lsPanNo = Mid(QuoteFilter(.Item("PANNO").ToString), 1, 10)
                        lsAadharNo = Mid(QuoteFilter(.Item("AADHAR").ToString), 1, 12)
                        lsSubStatus = Val(QuoteFilter(.Item("SUB STATUS").ToString))
                        lsShares = Val(QuoteFilter(.Item("SHARES").ToString))
                        lsHolderName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 165)
                        lsJTName1 = Mid(QuoteFilter(.Item("JTNAME1").ToString), 1, 165)
                        lsJTName2 = Mid(QuoteFilter(.Item("JTNAME2").ToString), 1, 165)
                        lsAddr1 = Mid(QuoteFilter_Addr(.Item("ADD1").ToString), 1, 36)
                        lsAddr2 = Mid(QuoteFilter_Addr(.Item("ADD2").ToString), 1, 36)
                        lsAddr3 = Mid(QuoteFilter_Addr(.Item("ADD3").ToString), 1, 36)
                        lsPlace = Mid(QuoteFilter_Addr(.Item("PLACE").ToString), 1, 25)
                        lsState = Mid(QuoteFilter(.Item("STATE").ToString), 1, 25)
                        lsCountry = Mid(QuoteFilter(.Item("COUNTRY").ToString), 1, 25)
                        lsPin = Mid(QuoteFilter(.Item("PIN").ToString), 1, 10)
                        lsBankAccNo = Mid(QuoteFilter(.Item("BANKACCNO").ToString), 1, 20)
                        lsMobileNo = Mid(QuoteFilter(.Item("MOBILE").ToString), 1, 10)
                        lsEmailId = Mid(QuoteFilter(.Item("EMAIL").ToString), 1, 100)
                        lsBlock = Mid(QuoteFilter(.Item("BLOCK").ToString), 1, 64)
                        lsBlockAreas = Mid(QuoteFilter(.Item("BLOCKREAS").ToString), 1, 64)

                        Using cmd As New MySqlCommand("pr_sta_ins_tevoting_cdsldetails", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                            cmd.Parameters.AddWithValue("?in_cdsl_header_gid", lnHeaderGid)
                            cmd.Parameters.AddWithValue("?in_depository_type", lsDepositoryType)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolio)
                            cmd.Parameters.AddWithValue("?in_pan_no", lsPanNo)
                            cmd.Parameters.AddWithValue("?in_aadhar_no", lsAadharNo)
                            cmd.Parameters.AddWithValue("?in_sub_status", lsSubStatus)
                            cmd.Parameters.AddWithValue("?in_share", lsShares)
                            cmd.Parameters.AddWithValue("?in_holder_name", lsHolderName)
                            cmd.Parameters.AddWithValue("?in_jtname1", lsJTName1)
                            cmd.Parameters.AddWithValue("?in_jtname2", lsJTName2)
                            cmd.Parameters.AddWithValue("?in_addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("?in_addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("?in_addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("?in_place", lsPlace)
                            cmd.Parameters.AddWithValue("?in_state", lsState)
                            cmd.Parameters.AddWithValue("?in_country", lsCountry)
                            cmd.Parameters.AddWithValue("?in_pin", lsPin)
                            cmd.Parameters.AddWithValue("?in_bank_acc_no", lsBankAccNo)
                            cmd.Parameters.AddWithValue("?in_mobile_no", lsMobileNo)
                            cmd.Parameters.AddWithValue("?in_email_id", lsEmailId)
                            cmd.Parameters.AddWithValue("?in_block", lsBlock)
                            cmd.Parameters.AddWithValue("?in_blockreas", lsBlockAreas)
                            cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
                            cmd.Parameters.AddWithValue("?In_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?In_errline_flag", True)

                            'output Para
                            cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                            cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                            cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                            cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
                            cmd.Parameters.Add("?out_total_shares", MySqlDbType.Int32)
                            cmd.Parameters("?out_total_shares").Direction = ParameterDirection.Output
                            cmd.CommandTimeout = 0

                            cmd.ExecuteNonQuery()

                            lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                            lsMsg = cmd.Parameters("?out_msg").Value.ToString()
                            lsTotalShares = Val(cmd.Parameters("?out_total_shares").Value.ToString())

                            If lnResult = 1 Then c += 1
                            frmMain.lblStatus.Text = "Imported Records Count : " & c
                            Application.DoEvents()
                        End Using
                    End With

                    i += 1
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            btnGenerate.Enabled = True
            Application.DoEvents()
            Me.Cursor = Cursors.Default

            If lsTotalShares <> lsShareCount Then
                MessageBox.Show("Share Captial Total Mismatch!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If i <> c Then
                lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully ! Total Record Mismatch"
                MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Call NSDLFileGenerate(lnFileId, lncompid)

            Call frmCtrClear(Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowse.Click
        With ofdInput
            If cboSheetName.Enabled = True Then
                .Filter = "Excel Files|*.xls|All Files|*.*"
            Else
                .Filter = "All Files|*.*"
            End If

            .Title = "Select Input File"
            .RestoreDirectory = True
            .ShowDialog()
            If .FileName <> "" And .FileName <> "OpenFileDialog1" Then
                txtFileName.Text = .FileName
            End If
            .FileName = ""
        End With
        If (InStr(1, LCase(Trim(txtFileName.Text)), ".xls")) > 0 Then
            cboSheetName.Enabled = True

            Call LoadSheet()

            cboSheetName.Focus()
        Else
            cboSheetName.Enabled = False
        End If
    End Sub

    Private Sub LoadSheet()
        Dim objXls As New Excel.Application
        Dim objBook As Excel.Workbook

        If Trim(txtFileName.Text) <> "" Then
            If File.Exists(txtFileName.Text) Then
                objBook = objXls.Workbooks.Open(txtFileName.Text)
                cboSheetName.Items.Clear()
                For i As Integer = 1 To objXls.ActiveWorkbook.Worksheets.Count
                    cboSheetName.Items.Add(objXls.ActiveWorkbook.Worksheets(i).Name)
                Next i
                objXls.Workbooks.Close()

            End If
        End If

        objXls.Workbooks.Close()

        GC.Collect()
        GC.WaitForPendingFinalizers()

        objXls.Quit()

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(objXls)
        objXls = Nothing
    End Sub

    Private Sub NSDLFileGenerate(lnFile_Id As Long, lncomp_Id As Long)
        Dim nsdl_ds As New DataSet
        Dim nsdlheader_dt As New DataTable
        Dim nsdldetail_dt As New DataTable
        Dim lsTempFileName As String
        Dim lsFileName As String
        Dim lsTxt As String
        Dim i As Integer
        Dim j As Integer

        Dim sl_folio As String = ""
        Dim Folio_Name As String = ""
        Dim Addr1 As String = ""
        Dim Addr2 As String = ""
        Dim Addr3 As String = ""
        Dim City_Pincode As String = ""
        Dim In_File_Gid As Integer = 0
        Dim In_Comp_Gid As Integer = 0

        Dim lsSql As String
        Dim nsdl_header_ds As New DataSet
        Dim nsdl_detail_ds As New DataSet
        Dim lnLineNo As Integer = 0

        Dim test As String

        Try

            lsTempFileName = gsUploadPath
            If Directory.Exists(lsTempFileName) = False Then Call Directory.CreateDirectory(lsTempFileName)
            lsTempFileName = lsTempFileName & "\E-Voting"
            If Directory.Exists(lsTempFileName) = False Then Call Directory.CreateDirectory(lsTempFileName)

            lsFileName = lsTempFileName & "\" & "CDSL_" & Format(Now, "ddMMyyy") & ".txt"
            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' header
            lsSql = ""
            lsSql &= " select cdsl_header_gid,file_gid,comp_gid,comp_isin_id, "
            lsSql &= " number_of_records,even_no"
            lsSql &= " from sta_trn_tevoting_cdslheader "
            lsSql &= " where file_gid = " & lnFile_Id & " "
            lsSql &= " and comp_gid=" & lncomp_Id & ""
            lsSql &= " and delete_flag = 'N' "

            Call gpDataSet(lsSql, "Header", gOdbcConn, nsdl_header_ds)

            If nsdl_header_ds.Tables("Header").Rows.Count > 0 Then
                nsdlheader_dt = nsdl_header_ds.Tables("Header")
            End If

            lsTxt = ""
            For i = 0 To nsdlheader_dt.Rows.Count - 1

                lsTxt &= "00"
                lsTxt &= AlignTxt("=", 1, 1)
                lsTxt &= nsdlheader_dt.Rows(i).Item("number_of_records").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdlheader_dt.Rows(i).Item("comp_isin_id").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdlheader_dt.Rows(i).Item("even_no").ToString()
                Call Print(1, lsTxt)
            Next

            'Details

            lsSql = ""
            lsSql &= " select cdsldetail_gid,file_gid,comp_gid,cdslheader_gid,depository_type,folio_no,pan_no,aadhar_no,sub_status,shares,holder_name,jtname1, "
            lsSql &= " jtname2,addr1,addr2,addr3,place,state,country,pin,ifnull(dob,'') as dob,bank_acc_no,mobile_no,email_id,blocks,blockreas"
            lsSql &= " from sta_trn_tevoting_cdsldetails "
            lsSql &= " where file_gid = " & lnFile_Id & " "
            lsSql &= " and comp_gid=" & lncomp_Id & ""
            lsSql &= " and delete_flag = 'N' "

            Call gpDataSet(lsSql, "Details", gOdbcConn, nsdl_detail_ds)

            If nsdl_detail_ds.Tables("Details").Rows.Count > 0 Then
                nsdldetail_dt = nsdl_detail_ds.Tables("Details")
            End If

            For j = 0 To nsdldetail_dt.Rows.Count - 1
                lnLineNo += 1
                lsTxt = vbNewLine
                lsTxt &= "01"
                lsTxt &= AlignTxt("=", 1, 1)
                lsTxt &= lnLineNo
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("depository_type").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("folio_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("pan_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("aadhar_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("sub_status").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("shares").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("holder_name").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("jtname1").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("jtname2").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("addr1").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("addr2").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("addr3").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("place").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("state").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("country").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("pin").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("dob").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("bank_acc_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("mobile_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("email_id").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("blocks").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= nsdldetail_dt.Rows(j).Item("blockreas").ToString()

                Call Print(1, lsTxt)
            Next
         

            nsdlheader_dt.Rows.Clear()
            nsdldetail_dt.Rows.Clear()

            Call FileClose(1)
            Call gpOpenFile(lsTempFileName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gsProjectName)
        End Try
    End Sub
    Private Function AlignTxt(ByVal txt As String, ByVal Length As Integer, ByVal Alignment As Integer) As String
        Select Case Alignment
            Case 1
                Return LSet(txt, Length)
            Case 4
                Return CSet(txt, Length)
            Case 7
                Return RSet(txt, Length)
            Case Else
                Return txt
        End Select
    End Function
    ' Center Align the Given Text
    Private Function CSet(ByVal txt As String, ByVal PaperChrWidth As Integer) As String
        Dim s As String                 ' Temporary String Variable
        Dim l As Integer                ' Length of the String
        If Len(txt) > PaperChrWidth Then
            CSet = Mid(txt, 1, PaperChrWidth)
        Else
            l = (PaperChrWidth - Len(txt)) / 2
            s = RSet(txt, l + Len(txt))
            CSet = Space(PaperChrWidth - Len(s))
            CSet = s + CSet
        End If
    End Function

End Class