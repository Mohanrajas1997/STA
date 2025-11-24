Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Net.Mail
Imports System.Net
Imports System.Net.WebRequestMethods
'Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.Drawing
Imports MySql.Data.MySqlClient
Imports DocumentFormat.OpenXml.Presentation
Imports ClosedXML.Excel



Public Class frmBenposBulkimport
    Public folderfiles As String
    Private Sub frmBenposBulkimport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        Call SetListViewProperty()
        'Call SetErrorListProperty()
        dtpDate.Value = Now
        ' load file type
        lsSql = ""
        lsSql &= " select * from sta_mst_tfile "
        lsSql &= " where delete_flag = 'N' and file_desc in ('Benpost - NSDL','Benpost - CDSL') "
        lsSql &= " order by file_desc "

        Call gpBindCombo(lsSql, "file_desc", "file_type", cboFileType, gOdbcConn)

    End Sub



    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        ' Create FolderBrowserDialog instance
        Using folderDialog As New FolderBrowserDialog()
            folderDialog.Description = "Select a folder"
            folderDialog.ShowNewFolderButton = True

            If folderDialog.ShowDialog() = DialogResult.OK Then
                Dim selectedFolder As String = folderDialog.SelectedPath
                txtFolderPath.Text = selectedFolder

                Dim txtFiles As String() = Directory.GetFiles(selectedFolder, "*.*")
                If txtFiles.Length = 0 Then
                    MessageBox.Show("There is No files found in the selected folder.", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                ' Clear previous items (optional)
                lsvFile.Items.Clear()
                Dim lsArr(3) As String
                Dim lobjItem As ListViewItem
                Dim lsFileName, lsSheetName As String
                Dim lsFile As String
                Dim n, i As Integer

                With lsvFile
                    For Each filePath In txtFiles
                        lsFile = filePath
                        lsFileName = Path.GetFileName(lsFile)
                        lsSheetName = Path.GetFileNameWithoutExtension(lsFile)

                        ' Check for duplicates
                        For i = 0 To .Items.Count - 1
                            lobjItem = .Items(i)
                            If lobjItem.SubItems(1).Text = cboFileType.Text Then
                                'AndAlso lobjItem.SubItems(3).Text = lsSheetName _
                                'AndAlso lobjItem.SubItems(4).Text = lsFile Then  
                                Continue For ' Already exists, skip
                            End If
                        Next

                        ' Build new item
                        n = .Items.Count + 1
                        lsArr(0) = n.ToString()
                        lsArr(1) = cboFileType.Text
                        lsArr(2) = lsFileName
                        lsArr(3) = lsFile
                        'lsArr(4) = lsFile

                        lobjItem = New ListViewItem(lsArr)
                        lobjItem.Checked = True
                        .Items.Add(lobjItem)
                    Next
                End With
                MessageBox.Show("Files loaded into list.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

    End Sub
    Private Sub SetListViewProperty()
        With lsvFile
            .Columns.Clear()
            .Columns.Add("SNo", 50)
            .Columns.Add("File Type", 150)
            .Columns.Add("File Name", 280)
            .Columns.Add("File path", 60)
            '.Columns.Add("File", 0)
            .View = View.Details

            .FullRowSelect = True
            .GridLines = True
            .CheckBoxes = True
        End With
        With lsvStatus
            .Columns.Clear()
            .Columns.Add("Status", 500)
            .View = View.Details
            .GridLines = False
        End With
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If MessageBox.Show("Are you sure to clear ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            lsvFile.Items.Clear()
            lsvStatus.Items.Clear()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        Dim lobjItem As ListViewItem
        Dim i As Integer
        Dim lsTxt As String
        Dim lsFileType As String
        Dim lsFile As String
        Dim lsFileName As String
        Dim lsSheetName As String
        Dim objImp As New clsImport
        Dim objFileReturn As New clsFileReturn
        Dim totalfiles As String
        Dim errorfiles As Int32
        Dim benType As Int32
        lsvStatus.Items.Clear()

        If cboFileType.Text.Trim() = "" Then
            MsgBox("Please select the File type !", MsgBoxStyle.Information, gsProjectName)
            cboFileType.Focus()
            Exit Sub
        End If

        If txtFolderPath.Text.Trim() = "" Then
            MessageBox.Show("Please select a folder", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnBrowse.Focus()
            Exit Sub
        End If

        'If lsvFile.Items.Count = 0 Then
        '    MessageBox.Show("There is Empty list", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    btnBrowse.Focus()
        '    Exit Sub
        'End If

        If MessageBox.Show("Are you sure to import ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            With lsvFile
                errorfiles = 0
                Dim checkedCount As Integer = 0
                For Each item As ListViewItem In lsvFile.CheckedItems
                    If item.Checked Then
                        checkedCount += 1
                    End If
                Next
                totalfiles = checkedCount ' lsvFile.Items.Count
                For i = 0 To .Items.Count - 1
                    lobjItem = .Items(i)
                    If lobjItem.Checked = True Then
                        lsFileType = lobjItem.SubItems(1).Text
                        lsvFile.Items(i).ForeColor = Color.Blue
                        lsFile = lobjItem.SubItems(3).Text
                        lsFileName = lobjItem.SubItems(2).Text

                        lsTxt = "Reading " & lsFileType
                        'add count of files
                        If i + 1 <> totalfiles Then
                            lobjItem = New ListViewItem("Processing.. " & totalfiles & " out of " & i + 1)
                            lobjItem.ForeColor = Color.Green
                            lsvStatus.Items.Add(lobjItem)
                            lsvStatus.TopItem = lobjItem
                        End If


                        Application.DoEvents()
                        lobjItem = New ListViewItem(Now().ToString() & " : " & lsTxt & "...")
                        lobjItem.ForeColor = Color.Blue
                        lsvStatus.Items.Add(lobjItem)
                        lsvStatus.TopItem = lobjItem

                        ' add file name
                        lobjItem = New ListViewItem("File : " & lsFileName)
                        lobjItem.ForeColor = Color.Blue
                        lsvStatus.Items.Add(lobjItem)
                        lsvStatus.TopItem = lobjItem

                        ' add sheet name
                        If lsFile <> "" Then
                            lobjItem = New ListViewItem("Path : " & lsFile)
                            lobjItem.ForeColor = Color.Blue
                            lsvStatus.Items.Add(lobjItem)
                            lsvStatus.TopItem = lobjItem
                        End If


                        lobjItem = New ListViewItem("Processing...")
                        lobjItem.ForeColor = Color.Red
                        lsvStatus.Items.Add(lobjItem)
                        lsvStatus.TopItem = lobjItem

                        Select Case lsFileType.ToUpper
                            Case "BENPOST - NSDL"
                                benType = gnFileBenpostNSDL
                                objFileReturn = objImp.BenpostNSDL(lsFile, False, lobjItem)
                            Case "BENPOST - CDSL"
                                benType = gnFileBenpostCDSL
                                objFileReturn = objImp.BenpostCDSL(lsFile, False, lobjItem)

                        End Select
                        If objFileReturn.Result = 1 Then
                            lobjItem.ForeColor = Color.Green
                            lsvFile.Items(i).ForeColor = Color.Green
                        Else
                            errorfiles = errorfiles + 1
                            lobjItem.ForeColor = Color.Red
                            lsvFile.Items(i).ForeColor = Color.Red
                        End If
                        createbenpostfilelist(benType, lsFileName, lsFile, objFileReturn.Msg)
                    End If
                Next i
                Application.DoEvents()

                'If i + 1 = totalfiles Then
                '    lobjItem = New ListViewItem("Processed files.. " & totalfiles)
                '    lobjItem.ForeColor = Color.Brown
                '    lsvStatus.Items.Add(lobjItem)
                '    lsvStatus.TopItem = lobjItem
                'End If

                'lsTxt = "Processed files.." & totalfiles & Environment.NewLine
                ''"  Out of " & totalfiles & " File(s), " & (totalfiles - errorfiles) & " File(s) imported successfully!"

                'lobjItem = New ListViewItem(Now() & " : " & lsTxt)
                'lobjItem.Text = Now() & " : " & lsTxt
                'lobjItem.Text += "  Out of " & totalfiles & " File(s) " & (totalfiles - errorfiles) & " File(s) imported successfully!"
                'lobjItem.ForeColor = Color.Green
                'lsvStatus.Items.Add(lobjItem)
                'lsvStatus.TopItem = lobjItem
                lsvStatus.Items.Clear()

                Dim successCount As Integer = totalfiles - errorfiles
                Dim message As String = "Out of " & totalfiles & " File(s): " & successCount & " file imported successfully, " & errorfiles & " failed."

                lobjItem = New ListViewItem(message)
                lobjItem.ForeColor = If(errorfiles = 0, Color.Green, Color.Red)
                lsvStatus.Items.Add(lobjItem)

            End With
            totalfiles = totalfiles - errorfiles
            'MessageBox.Show("File imported successfully !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show(totalfiles & "  File(s) imported successfully!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub createbenpostfilelist(ftype As Int32, fname As String, FileName As String, benremark As String)
        Dim lsLine As String = ""
        Dim sr As StreamReader
        Dim lsFldValues() As String
        Dim lsBenpostDate As String
        Dim lsIsinID As String = ""

        If ftype = 12 Then
            sr = FileIO.FileSystem.OpenTextFileReader(FileName)
            lsLine = sr.ReadLine()
            lsLine = lsLine.Trim()
            lsFldValues = Split(lsLine, "##")

            For j = 0 To lsFldValues.Length - 1
                lsFldValues(j) = QuoteFilter(lsFldValues(j))
            Next j
            lsIsinID = lsFldValues(1)
            lsBenpostDate = lsFldValues(2)
            If lsBenpostDate.Length = 8 And IsNumeric(lsBenpostDate) = True Then lsBenpostDate = Mid(lsBenpostDate, 1, 4) & "-" & Mid(lsBenpostDate, 5, 2) & "-" & Mid(lsBenpostDate, 7, 2)
            If IsDate(lsBenpostDate) Then lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd") Else lsBenpostDate = "0001-01-01"
        Else
            sr = FileIO.FileSystem.OpenTextFileReader(FileName)
            lsLine = sr.ReadLine()
            lsLine = lsLine.Trim()
            lsFldValues = Split(lsLine, "~")
            For j = 0 To lsFldValues.Length - 1
                lsFldValues(j) = QuoteFilter(lsFldValues(j))
            Next j
            lsIsinID = ""
            lsBenpostDate = lsFldValues(73)
            If lsBenpostDate.Length = 8 And IsNumeric(lsBenpostDate) = True Then lsBenpostDate = Mid(lsBenpostDate, 5, 4) & "-" & Mid(lsBenpostDate, 3, 2) & "-" & Mid(lsBenpostDate, 1, 2)
            If IsDate(lsBenpostDate) Then lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd") Else lsBenpostDate = "0001-01-01"

        End If

        Using cmd As New MySqlCommand("pr_sta_ins_tbenposlog", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_file_type", ftype)
            cmd.Parameters.AddWithValue("?in_file_name", fname)
            cmd.Parameters.AddWithValue("?in_isin_id", lsIsinID)
            cmd.Parameters.AddWithValue("?in_benpost_date", lsBenpostDate)
            cmd.Parameters.AddWithValue("?in_file_remark", benremark)
            cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
            cmd.Parameters.Add("?out_msg", MySqlDbType.Text).Direction = ParameterDirection.Output
            cmd.Parameters.Add("?out_result", MySqlDbType.Int32).Direction = ParameterDirection.Output
            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()
            Dim outMessage As String = cmd.Parameters("?out_msg").Value.ToString()
            Dim outResult As Integer = Convert.ToInt32(cmd.Parameters("?out_result").Value)
        End Using
    End Sub
    Private Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        For Each itm As ListViewItem In lsvFile.Items
            itm.Checked = chkAll.Checked
        Next
    End Sub
    Private Sub SetErrorListProperty()
        With lsvStatus
            .Columns.Clear()
            .Columns.Add("SNo", 20)
            .Columns.Add("File Name", 150)
            .Columns.Add("Error Msg", 270)
            .Columns.Add("Sheet Name", 100)
            .Columns.Add("File", 0)
            .View = View.Details

            .FullRowSelect = True
            .GridLines = True
        End With
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs)

        Try
            Dim lsSql As String = ""
            lsSql &= "SELECT " & vbCrLf
            lsSql &= " file_name AS 'File Name'," & vbCrLf
            lsSql &= " file_type AS 'File Type'," & vbCrLf
            lsSql &= " isin_id AS 'ISIN ID'," & vbCrLf
            lsSql &= " benpost_date AS 'Benpost Date'," & vbCrLf
            lsSql &= " file_remark AS 'File Remark'," & vbCrLf
            lsSql &= " insert_date AS 'Insert Date'," & vbCrLf
            lsSql &= " insert_by AS 'Inserted By'" & vbCrLf
            lsSql &= "FROM sta_trn_tbenposlog" & vbCrLf
            lsSql &= "WHERE benpost_date = " & dtpDate.Value & vbCrLf
            lsSql &= "ORDER BY 1 DESC;"

            '  ExportToExcel(lsSql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Sub ExportToExcel(query As String)
    '    'Dim connStr As String = "server=localhost;user id=root;password=yourpass;database=yourdb"
    '    'Dim conn As New MySqlConnection(connStr)
    '    Dim dt As New DataTable()

    '    Try
    '        Call ConOpenOdbc(ServerDetails)

    '        'adapter.Fill(dt)

    '        If dt.Rows.Count = 0 Then
    '            MessageBox.Show("No data found to export.")
    '            Return
    '        End If

    '        Using sfd As New SaveFileDialog()
    '            sfd.Title = "Save Excel File"
    '            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx"
    '            sfd.FileName = "exported_data.xlsx"

    '            If sfd.ShowDialog() = DialogResult.OK Then
    '                Using wb As New XLWorkbook()
    '                    wb.Worksheets.Add(dt, "Sheet1")
    '                    wb.SaveAs(sfd.FileName)
    '                    MessageBox.Show("File exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                End Using
    '            End If
    '        End Using

    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message)
    '    Finally
    '        conn.Close()
    '    End Try
    'End Sub


End Class