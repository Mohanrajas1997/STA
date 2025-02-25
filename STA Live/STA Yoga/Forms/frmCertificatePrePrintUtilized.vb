Imports System.IO
Imports MySql.Data.MySqlClient

Public Class frmCertificatePrePrintUtilized

#Region "Local Declaration"
    Dim lnres As Integer
    Dim fsSql As String
    Dim lnResult As Long

    Dim fsFilePath As String = ""
    Dim fsFileName As String
    Dim fExcelDatatable As New DataTable
    Dim lobjRow As DataRow
#End Region

    Private Sub frmCertificatePrePrintUtility_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "
        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        'User Selected Browse file 
        With OpenFileDialog1

            .Filter = "Excel Files|*.xls;*.xlsx|Text Files|*.*|DBF Files|*.dbf|Text Files|*.txt|Word Files|*.doc"
            .Title = "Select Files to Import"
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try

            If cboCompany.Text = "" Then
                MsgBox("Please Select Company", MsgBoxStyle.Information, gsProjectName)
                cboCompany.Focus()
                Exit Sub
            End If

            If txtFileName.Text = "" Then
                MsgBox("Select File Name", MsgBoxStyle.Information, gsProjectName)
                txtFileName.Focus()
                Exit Sub
            End If

            Panel1.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            fsFilePath = txtFileName.Text.Trim
            fsFileName = fsFilePath.Substring(fsFilePath.LastIndexOf("\") + 1)

            If txtFileName.Text <> "" Then
                If cboSheetName.Text <> "" Then

                    Call FormatSheet(txtFileName.Text, cboSheetName.Text)

                    ImportPPUtilizedFile()

                Else
                    MsgBox("Select Sheet Name", MsgBoxStyle.Information, gsProjectName)
                    cboSheetName.Focus()
                    Exit Sub
                End If
            Else
                MsgBox("Select File to Import!", MsgBoxStyle.Information, gsProjectName)
                Exit Sub
            End If

            Panel1.Enabled = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)

            btnImport.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Function ImportPPUtilizedFile()

        Dim i As Integer
        Dim J As String
        Dim c As Integer
        Dim d As Integer
        Dim lbInsertFlag As Boolean = False
        Dim lsDiscRemark As String = ""
        Dim lsFldFormat As String = ""
        Dim lsFldName(7) As String
        Dim lsTxt As String
        Dim lsMsg As String

        Dim lsSlno As String = ""
        Dim lsCompanyCode As String = ""
        Dim lsPrePrintNo As Integer
        Dim lsInwardNo As String = ""
        Dim lsCompInwardNo As String = ""
        Dim lsRemarks As String = ""
        Dim lsSign As String = ""
        Dim lsFileName As String = ""
        Dim lnFileId As Long = 0
        Dim lnEntityId As Integer = 0
        Dim lsentity_gid As Integer = 0
        Dim lsSheetName As String = ""


        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "PREPRINT NO"
        lsFldName(4) = "INWARD NO"
        lsFldName(5) = "COMP INWARD NO"
        lsFldName(6) = "REMARKS"
        lsFldName(7) = "SIGN"

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFileName = QuoteFilter(fsFileName)

        Try
            fExcelDatatable = gpExcelDataset("select * from [" & cboSheetName.Text & "$]", fsFilePath)


            For i = 1 To 7
                lsFldFormat &= lsFldName(i) & "|"
            Next
            For i = 1 To 7
                If lsFldName(i).Trim.ToUpper <> fExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    MsgBox("Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & fExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat, vbOKOnly + vbExclamation, gsProjectName)
                    Exit Function
                End If
            Next


            'File Name Duplicate
            fsSql = ""
            fsSql &= " select file_gid from sta_trn_tfile"
            fsSql &= " where 1=1 "
            fsSql &= " and file_name = '" & lsFileName & "'"
            fsSql &= " and sheet_name='" & cboSheetName.Text & "'"
            fsSql &= " and file_type = " & gnCertificatePPUtilized & " "
            fsSql &= " and delete_flag ='N'"

            lnFileId = Val(gfExecuteScalar(fsSql, gOdbcConn))
            If lnFileId > 0 Then
                MsgBox("File Already Imported !", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gsProjectName)
                txtFileName.Focus()
                Exit Function
            End If

            btnImport.Enabled = False

            lsSheetName = cboSheetName.Text
            Call ConOpenOdbc(ServerDetails)
            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters.AddWithValue("?in_sheet_name", lsSheetName)
                cmd.Parameters.AddWithValue("?in_file_type", gnCertificatePPUtilized)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                ''Out put Para
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
                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            Dim lsTxtFile As String = gsAsciiPath & "\Error.txt"

            With fExcelDatatable
                Dim message As String
                message = String.Empty
                i = 0
                J = "0"
                While i <= .Rows.Count - 1
                    With .Rows(i)
                        Me.Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        lbInsertFlag = True
                        lsDiscRemark = ""

                        lsCompanyCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsPrePrintNo = Mid(QuoteFilter(.Item("PREPRINT NO").ToString), 1, 16)
                        lsInwardNo = Mid(QuoteFilter(.Item("INWARD NO").ToString), 1, 16)
                        lsCompInwardNo = Mid(QuoteFilter(.Item("COMP INWARD NO").ToString), 1, 16)
                        lsRemarks = Mid(QuoteFilter(.Item("REMARKS").ToString), 1, 255)
                        lsSign = Mid(QuoteFilter(.Item("SIGN").ToString), 1, 255)

                        Call ConOpenOdbc(ServerDetails)
                        Using cmd = New MySqlCommand("pr_sta_ins_tcertificatepp_utilized", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?In_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_comp_gid", cboCompany.SelectedValue)
                            cmd.Parameters.AddWithValue("?In_preprint_no", lsPrePrintNo)
                            cmd.Parameters.AddWithValue("?In_inward_no", lsInwardNo)
                            cmd.Parameters.AddWithValue("?In_comp_inward_no", lsCompInwardNo)
                            cmd.Parameters.AddWithValue("?In_remarks", lsRemarks)
                            cmd.Parameters.AddWithValue("?In_sign", lsSign)

                            cmd.Parameters.AddWithValue("?In_loginuser", gsLoginUserCode)
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
                        End Using
                    End With

                    i += 1
                    J += 1
                    If lnResult = 0 Then
                        Using sw As New StreamWriter(lsTxtFile)
                            message += "Line: " + J + " ErrorMsg: " + lsMsg + Environment.NewLine
                            sw.WriteLine(message)
                            sw.Close()
                        End Using
                    End If
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If i <> c Then
                Process.Start(lsTxtFile)
            End If

            MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try

    End Function
End Class