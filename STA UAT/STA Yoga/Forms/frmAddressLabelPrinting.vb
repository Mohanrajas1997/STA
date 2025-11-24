Imports System.IO
Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop

Public Class frmAddressLabelPrinting
#Region "Local Declaration"
    Dim lnImportFlag As Integer
    Dim fsSql As String
    Dim lnResult As Long

    Dim fsFilePath As String = ""
    Dim fsFileName As String
    Dim fExcelDatatable As New DataTable
    Dim lobjRow As DataRow
#End Region
    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click

        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lsSheetName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lsErrFileName As String = ""

        Dim i As Integer
        Dim lsFldName(8) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0
        Dim ShowFlag As Boolean = True
        Dim lobjExcelDatatable As New DataTable

        Dim lsAddSlno As String
        Dim lsFolio As String
        Dim lsFolioName As String
        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsCity As String
        Dim lsPincode As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn
        lsFldName(1) = "SL"
        lsFldName(2) = "FOLIO"
        lsFldName(3) = "NAME"
        lsFldName(4) = "ADDR1"
        lsFldName(5) = "ADDR2"
        lsFldName(6) = "ADDR3"
        lsFldName(7) = "CITY"
        lsFldName(8) = "PINCODE"
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

            For i = 1 To 8
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 8
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
                cmd.Parameters.AddWithValue("?in_file_type", gnAddressLabelPrinting)
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


            'lsErrFileName = gsReportPath & "err.txt"
            'If File.Exists(lsErrFileName) = True Then File.Delete(lsErrFileName)

            'Call FileOpen(1, lsErrFileName, OpenMode.Output)
            'Call PrintLine(1, "SNo;Error Desc")

            btnGenerate.Enabled = False

            fsSql = ""
            fsSql &= " delete from  sta_trn_taddresslabel "
            fsSql &= " where insert_by = '" & gsLoginUserCode & "' "

            lnResult = Val(gfExecuteScalar(fsSql, gOdbcConn))

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)


                        Application.DoEvents()

                        lsAddSlno = Mid(QuoteFilter(.Item("SL").ToString), 1, 124)
                        lsFolio = Mid(QuoteFilter(.Item("FOLIO").ToString), 1, 124)
                        lsFolioName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 255)
                        lsAddr1 = QuoteFilter(.Item("ADDR1").ToString)
                        lsAddr2 = QuoteFilter(.Item("ADDR2").ToString)
                        lsAddr3 = QuoteFilter(.Item("ADDR3").ToString)
                        lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 124)
                        lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 12)

                        Using cmd As New MySqlCommand("pr_sta_ins_taddresslabeling", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?In_File_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_Addr_Slno", lsAddSlno)
                            cmd.Parameters.AddWithValue("?In_Addr_folio", lsFolio)
                            cmd.Parameters.AddWithValue("?In_Addr_name", lsFolioName)
                            cmd.Parameters.AddWithValue("?In_Addr_Addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("?In_Addr_Addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("?In_Addr_Addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("?In_Addr_City", lsCity)
                            cmd.Parameters.AddWithValue("?In_Addr_Pincode", lsPincode)
                            cmd.Parameters.AddWithValue("?In_login_user", gsLoginUserCode)
                            cmd.Parameters.AddWithValue("?In_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?In_errline_flag", True)

                            'output Para
                            cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                            cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                            cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                            cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                            cmd.CommandTimeout = 0

                            cmd.ExecuteNonQuery()

                            lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                            lsMsg = cmd.Parameters("?out_msg").Value.ToString()

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

            Call frmAddressLabelPrinting()

        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gsProjectName)
            btnGenerate.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub
    Private Sub frmAddressLabelPrinting()
        Dim ds As New DataSet
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


        Try
            Using cmd As New MySqlCommand("pr_sta_get_taddresslabeling", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?In_LoginUser", gsLoginUserCode)
                Using sda As New MySqlDataAdapter(cmd)
                    sda.Fill(ds)
                End Using
            End Using
            lsTempFileName = gsUploadPath
            If Directory.Exists(lsTempFileName) = False Then Call Directory.CreateDirectory(lsTempFileName)
            lsTempFileName = lsTempFileName & "\ADDRESSLABEL"
            If Directory.Exists(lsTempFileName) = False Then Call Directory.CreateDirectory(lsTempFileName)

            lsFileName = lsTempFileName & "\" & "AddressLabel_" & Format(Now, "ddMMyyy") & ".txt"

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)
            j = 0
            For i = 0 To ds.Tables("Table").Rows.Count - 1

                sl_folio &= AlignTxt(ds.Tables("Table").Rows(i).Item("Slno_folio").ToString, 42, 1)
                Folio_Name &= AlignTxt(ds.Tables("Table").Rows(i).Item("addr_name").ToString, 42, 1)
                Addr1 &= AlignTxt(ds.Tables("Table").Rows(i).Item("addr_addr1").ToString, 42, 1)
                Addr2 &= AlignTxt(ds.Tables("Table").Rows(i).Item("addr_addr2").ToString, 42, 1)
                Addr3 &= AlignTxt(ds.Tables("Table").Rows(i).Item("addr_addr3").ToString, 42, 1)
                City_Pincode &= AlignTxt(ds.Tables("Table").Rows(i).Item("City_Pincode").ToString, 42, 1)
                j = j + 1
                If j > 1 Then
                    lsTxt = vbNewLine
                    lsTxt &= vbNewLine
                    lsTxt &= sl_folio
                    lsTxt &= vbNewLine
                    lsTxt &= Folio_Name
                    lsTxt &= vbNewLine
                    lsTxt &= Addr1
                    lsTxt &= vbNewLine
                    lsTxt &= Addr2
                    lsTxt &= vbNewLine
                    lsTxt &= Addr3
                    lsTxt &= vbNewLine
                    lsTxt &= City_Pincode
                    lsTxt &= vbNewLine
                    lsTxt &= vbNewLine
                    lsTxt &= vbNewLine
                    lsTxt &= vbNewLine
                    lsTxt &= vbNewLine

                    j = 0

                    sl_folio = ""
                    Folio_Name = ""
                    Addr1 = ""
                    Addr2 = ""
                    Addr3 = ""
                    City_Pincode = ""

                    Call Print(1, lsTxt)
                End If
            Next i

            If j = 1 Then
                lsTxt = vbNewLine
                lsTxt &= vbNewLine
                lsTxt &= sl_folio
                lsTxt &= vbNewLine
                lsTxt &= Folio_Name
                lsTxt &= vbNewLine
                lsTxt &= Addr1
                lsTxt &= vbNewLine
                lsTxt &= Addr2
                lsTxt &= vbNewLine
                lsTxt &= Addr3
                lsTxt &= vbNewLine
                lsTxt &= City_Pincode
                lsTxt &= vbNewLine
                lsTxt &= vbNewLine
                lsTxt &= vbNewLine
                lsTxt &= vbNewLine
                lsTxt &= vbNewLine
                Call Print(1, lsTxt)
            End If

            ds.Tables("Table").Rows.Clear()

            Call FileClose(1)
            Call gpOpenFile(lsTempFileName)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gsProjectName)
        End Try

    End Sub
    ' Aligns the given text in specified format
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
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            MyBase.Close()
        End If
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
End Class