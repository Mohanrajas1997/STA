Imports System.IO

Public Class frmImportAll
    Private Sub frmImportAll_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call SetInitializeDefaultData()
        Call SetListViewProperty()

        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
    End Sub

    Private Sub SetInitializeDefaultData()
        Dim lsSql As String

        dtpDate.Value = Now

        ' load file type
        lsSql = ""
        lsSql &= " select * from sta_mst_tfile "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by file_desc "

        Call gpBindCombo(lsSql, "file_desc", "file_type", cboFileType, gOdbcConn)

    End Sub

    Private Sub cboFileType_LostFocus(sender As Object, e As EventArgs) Handles cboFileType.LostFocus
        Dim lsSql As String
        Dim lsTxt As String

        lsSql = ""
        lsSql &= " select excel_flag from sta_mst_tfile "
        lsSql &= " where true "

        If cboFileType.SelectedIndex <> -1 And cboFileType.Text <> "" Then
            lsSql &= " and file_type = " & Val(cboFileType.SelectedValue.ToString) & " "
        End If

        lsSql &= " and delete_flag = 'N' "

        lsTxt = gfExecuteScalar(lsSql, gOdbcConn)

        If lsTxt = "N" Then
            cboSheetName.Items.Clear()
            cboSheetName.Text = ""
            cboSheetName.Enabled = False
        Else
            cboSheetName.Enabled = True
        End If
    End Sub

    Private Sub cboFileSubType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFileType.SelectedIndexChanged

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

        If cboFileType.Text = "" Or cboFileType.SelectedIndex = -1 Then
            MessageBox.Show("Please select file sub type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboFileType.Focus()
            Exit Sub
        End If

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

            lsArr(0) = n.ToString
            lsArr(1) = cboFileType.Text
            lsArr(2) = lsFileName
            lsArr(3) = lsSheetName
            lsArr(4) = lsFile

            For i = 0 To .Items.Count - 1
                lobjItem = .Items(i)

                If lobjItem.SubItems(1).Text = cboFileType.Text _
                    And lobjItem.SubItems(3).Text = lsSheetName _
                    And lobjItem.SubItems(4).Text = lsFile Then
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

                        Select Case lsFileType.ToUpper
                            Case "FOLIO"
                                objFileReturn = objImp.Folio(lsFile, lsSheetName, False, lobjItem)
                            Case "CERTIFICATE"
                                objFileReturn = objImp.Certificate(lsFile, lsSheetName, False, lobjItem)
                            Case "CERTIFICATE DISTINCTIVE SERIES"
                                objFileReturn = objImp.CertificateDistSeries(lsFile, lsSheetName, False, lobjItem)
                            Case "PENDING - CDSL"
                                objFileReturn = objImp.PendingCDSL(lsFile, False, lobjItem)
                            Case "PENDING - NSDL"
                                objFileReturn = objImp.PendingNSDL(lsFile, False, lobjItem)
                            Case "FOLIO PAN UPDATION"
                                objFileReturn = objImp.FolioPan(lsFile, lsSheetName, False, lobjItem)
                            Case "FOLIO EMAIL ID UPDATION"
                                objFileReturn = objImp.FolioEmailId(lsFile, lsSheetName, False, lobjItem)
                            Case "FOLIO CONTACT NO UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioContactNo, "Contact No", "folio_contact_no", 128, False, lobjItem)
                            Case "CERTIFICATE STATUS UPDATION"
                                objFileReturn = objImp.CertificateStatusUpdate(lsFile, lsSheetName, False, lobjItem)
                            Case "FOLIO ADDRESS UPDATION"
                                objFileReturn = objImp.FolioAddr(lsFile, lsSheetName, False, lobjItem)
                            Case "BENPOST - NSDL"
                                objFileReturn = objImp.BenpostNSDL(lsFile, False, lobjItem)
                            Case "BENPOST - CDSL"
                                objFileReturn = objImp.BenpostCDSL(lsFile, False, lobjItem)
                            Case "HISTORY - TRAN"
                                objFileReturn = objImp.HistoryTran(lsFile, lsSheetName, False, lobjItem)
                            Case "HISTORY - NSDL/CDSL"
                                objFileReturn = objImp.HistoryNsdlCdsl(lsFile, lsSheetName, False, lobjItem)
                            Case "FOLIO MICR UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioMicrCode, "Micr Code", "bank_micr_code", 16, False, lobjItem)
                            Case "FOLIO BANK NAME UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioBankName, "Bank Name", "bank_name", 128, False, lobjItem)
                            Case "FOLIO BANK A/C NO UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioBankAccNo, "Bank A/C No", "bank_acc_no", 64, False, lobjItem)
                            Case "FOLIO BANK IFSC CODE UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioBankIfscCode, "Bank IFSC Code", "bank_ifsc_code", 32, False, lobjItem)
                            Case "FOLIO BANK BRANCH UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioBankBranch, "Bank Branch", "bank_branch", 128, False, lobjItem)
                            Case "FOLIO BANK BENEFICIARY UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioBankBeneficiary, "Bank Beneficiary", "bank_beneficiary", 128, False, lobjItem)
                            Case "FOLIO BANK A/C TYPE UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioBankAccType, "Bank A/C Type", "bank_acc_type", 1, False, lobjItem)
                            Case "FOLIO BANK ADDRESS UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioBankAddress, "Bank Address", "bank_branch_addr", 255, False, lobjItem)
                            Case "HISTORY - CONSOLIDATION/SPLIT"
                                objFileReturn = objImp.HistoryConsolidationSplit(lsFile, lsSheetName, False, lobjItem)
                            Case "FOLIO JOINT1 UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioJoint1, "Joint1", "holder2_name", 128, False, lobjItem)
                            Case "FOLIO JOINT2 UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioJoint1, "Joint2", "holder3_name", 128, False, lobjItem)
                            Case "FOLIO CUSTOMER FATHER/HUSBAND UPDATION"
                                objFileReturn = objImp.FolioFieldUpdation(lsFile, lsSheetName, gnFileFolioJoint1, "Holder1 FHName", "holder1_fh_name", 128, False, lobjItem)
                            Case "ANNUAL REPORT"
                                objFileReturn = objImp.FolioAnnualreport(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND FILE UPDATION"
                                objFileReturn = objImp.Dividendreport(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND SUCCESS FILE"
                                objFileReturn = objImp.DividendSuccess(lsFile, lsSheetName, False, lobjItem)
                            Case "HISTORY - PHYSICAL TRAN"
                                objFileReturn = objImp.HistoryPhysicalTran(lsFile, lsSheetName, False, lobjItem)
                            Case "HISTORY - DIVIDEND"
                                objFileReturn = objImp.HistoryDividenddt(lsFile, lsSheetName, False, lobjItem)
                            Case "HISTORY - ALLOTMENT"
                                objFileReturn = objImp.HistoryAllotment(lsFile, lsSheetName, False, lobjItem)
                            Case "LETTER DISPATCH"
                                objFileReturn = objImp.LetterDispatch(lsFile, lsSheetName, False, lobjItem)
                            Case "LETTER RESPONSE"
                                objFileReturn = objImp.LetterResponse(lsFile, lsSheetName, False, lobjItem)
                            Case "AML LIST"
                                objFileReturn = objImp.AMLList(lsFile, lsSheetName, False, lobjItem)
                            Case "INTER DEPOSITORY"
                                objFileReturn = objImp.InterDepository(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND MASTER"
                                objFileReturn = objImp.DividendMaster(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND STATUS"
                                objFileReturn = objImp.DividendStatus(lsFile, lsSheetName, False, lobjItem)
                            Case "COMPANY SHARE CAPTIAL"
                                objFileReturn = objImp.CompanyShareCaptial(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND ACCOUNT MASTER"
                                objFileReturn = objImp.DividendAccountMaster(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND SHARE CAPITAL"
                                objFileReturn = objImp.DividendShareCapital(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND SHARE CAPITAL CONTACT COMP"
                                objFileReturn = objImp.DividendShareCapital_ContactComp(lsFile, lsSheetName, False, lobjItem)
                            Case "DIVIDEND PAYSTATUS UPDATE"
                                objFileReturn = objImp.DividendPayStatusUpdate(lsFile, lsSheetName, False, lobjItem)
                            Case "LOSS OF SHARES"
                                objFileReturn = objImp.LossofShares(lsFile, lsSheetName, False, lobjItem)
                            Case "TRANSMISSION"
                                objFileReturn = objImp.Transmission(lsFile, lsSheetName, False, lobjItem)
                            Case "INWARD - OTHERS"
                                objFileReturn = objImp.InwardOthers(lsFile, lsSheetName, False, lobjItem)
                            Case "OUTWARD"
                                objFileReturn = objImp.Outward(lsFile, lsSheetName, False, lobjItem)
                        End Select

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

            'MessageBox.Show("File imported successfully !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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