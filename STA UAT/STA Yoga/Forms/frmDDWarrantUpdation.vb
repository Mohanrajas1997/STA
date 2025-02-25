Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmDDWarrantUpdation
    Private Sub frmImportAll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call SetInitializeDefaultData()
        Call SetListViewProperty()

        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
    End Sub

    Private Sub SetInitializeDefaultData()
        Dim lsSql As String
        Dim lsTxt As String
        lsSql = ""
        lsSql &= " select excel_flag from sta_mst_tfile "
        lsSql &= " where true "

        'If cboFileType.SelectedIndex <> -1 And cboFileType.Text <> "" Then
        '    lsSql &= " and file_type = " & Val(cboFileType.SelectedValue.ToString) & " "
        'End If

        lsSql &= " and file_type='23' and  delete_flag = 'N' "

        lsTxt = gfExecuteScalar(lsSql, gOdbcConn)

        If lsTxt = "N" Then
            cboSheetName.Items.Clear()
            cboSheetName.Text = ""
            cboSheetName.Enabled = False
        Else
            cboSheetName.Enabled = True
        End If
    End Sub
    


    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        'User Selected Browse file 
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

        If cboSheetName.Enabled = True Then Call LoadSheet()

        cboSheetName.Focus()
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim lsFile As String
        Dim lsFileName As String
        Dim lsSheetName As String

        Dim n As Integer
        Dim lsArr(4) As String
        Dim lobjItem As ListViewItem
        Dim i As Integer
        Dim lsFileType As String = "DD/WarrantNo Update"

        

        If txtFileName.Text = "" Then
            MessageBox.Show("Please select the file !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnBrowse.Focus()
            Exit Sub
        Else
            If File.Exists(txtFileName.Text) = False Then
                MessageBox.Show("Please select valid file !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnBrowse.Focus()
                Exit Sub
            End If
        End If

        If (cboSheetName.Text = "" Or cboSheetName.SelectedIndex = -1) And cboSheetName.Enabled = True Then
            MessageBox.Show("Please select sheet name !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboSheetName.Focus()
            Exit Sub
        End If

        lsFile = txtFileName.Text
        n = lsFile.Split("\").GetUpperBound(0)
        lsFileName = lsFile.Split("\")(n)
        lsSheetName = cboSheetName.Text

        With lsvFile
            n = .Items.Count + 1

            'lsArr(0) = n.ToString

            'lsArr(1) = lsFileName
            'lsArr(2) = lsSheetName
            'lsArr(3) = lsFile

            lsArr(0) = n.ToString
            lsArr(1) = lsFileType
            lsArr(2) = lsFileName
            lsArr(3) = lsSheetName
            lsArr(4) = lsFile

            For i = 0 To .Items.Count - 1
                lobjItem = .Items(i)

                If lobjItem.SubItems(2).Text = lsSheetName _
                    And lobjItem.SubItems(3).Text = lsFile Then
                    MessageBox.Show("File already added !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Next i

            lobjItem = New ListViewItem(lsArr)
            lobjItem.Checked = True

            .Items.Add(lobjItem)
            .TopItem = lobjItem
        End With
    End Sub

    Private Sub SetListViewProperty()
        With lsvFile
            .Columns.Clear()
            .Columns.Add("SNo", 40)
            .Columns.Add("File Type", 150)
            .Columns.Add("File Name", 250)
            .Columns.Add("Sheet Name", 100)
            .Columns.Add("File", 0)
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

        If MessageBox.Show("Are you sure to import ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            With lsvFile
                For i = 0 To .Items.Count - 1
                    lobjItem = .Items(i)

                    If lobjItem.Checked = True Then
                        lsFileType = lobjItem.SubItems(1).Text

                        lsFile = lobjItem.SubItems(4).Text

                        lsFileName = lobjItem.SubItems(2).Text
                        lsSheetName = lobjItem.SubItems(3).Text

                        lsTxt = "Reading " & lsFileType

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
                        If lsSheetName <> "" Then
                            lobjItem = New ListViewItem("Sheet : " & lsSheetName)
                            lobjItem.ForeColor = Color.Blue
                            lsvStatus.Items.Add(lobjItem)
                            lsvStatus.TopItem = lobjItem
                        End If

                        lobjItem = New ListViewItem("Processing...")
                        lobjItem.ForeColor = Color.Red
                        lsvStatus.Items.Add(lobjItem)
                        lsvStatus.TopItem = lobjItem

                        objFileReturn = objImp.DDNOUpdation(lsFile, lsSheetName, False, lobjItem)

                        lsTxt = objFileReturn.Msg
                        lobjItem.Text = Now() & " : " & lsTxt

                        If objFileReturn.Result = 1 Then
                            lobjItem.ForeColor = Color.Green
                        Else
                            lobjItem.ForeColor = Color.Red
                        End If

                    End If

                    Application.DoEvents()
                Next i
            End With

            MessageBox.Show("File imported successfully !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If MessageBox.Show("Are you sure to clear ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            lsvFile.Items.Clear()
            lsvStatus.Items.Clear()
        End If
    End Sub

    Private Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        For Each itm As ListViewItem In lsvFile.Items
            itm.Checked = chkAll.Checked
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class