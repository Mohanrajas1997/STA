Imports System.IO
Imports MySql.Data.MySqlClient

Public Class frmIEPFUpload

#Region "Local Declaration"
    Dim lnImportFlag As Integer
    Dim fsSql As String
    Dim lnResult As Long

    Dim fsFilePath As String = ""
    Dim fsFileName As String
    Dim fExcelDatatable As New DataTable
    Dim lobjRow As DataRow
#End Region

    Private Sub frmIEPFUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' Company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", CboCompany, gOdbcConn)

        CboTransferType.Items.Clear()
        CboTransferType.Items.Add("Movement")
        CboTransferType.Items.Add("Claim")
    End Sub

    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click

        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lsSheetName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lsErrFileName As String = ""
        Dim lncompid As Long
        Dim lsTranType As String = ""
        Dim lsTranType_single As String = ""

        Dim i As Integer

        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0
        Dim ShowFlag As Boolean = True
        Dim lobjExcelDatatable As New DataTable

        Dim lsDPId As String = ""
        Dim lsFolio As String = ""
        Dim lsFolioName As String = ""
        Dim lsDateofTransfer As String = ""
        Dim lsShares As Long
        Dim lsAddr1 As String = ""
        Dim lsAddr2 As String = ""
        Dim lsAddr3 As String = ""
        Dim lsCity As String = ""
        Dim lsPincode As String = ""

        Dim lsClaimDpId As String = ""
        Dim lsClaimntName As String = ""
        Dim lsSRNNo As String = ""
        Dim lsClaimDate As String = ""

        Dim lsTotalShare As Long


        Dim lsInwarddate As Date

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        Try

            If CboCompany.SelectedIndex <> -1 Then
                lncompid = Val(CboCompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            If CboTransferType.SelectedIndex <> -1 Then
                lsTranType = CboTransferType.Text
            Else
                lsTranType = ""
            End If

            If lncompid = 0 Then
                MsgBox("Select Company Name", MsgBoxStyle.Information, gsProjectName)
                CboCompany.Focus()
                Exit Sub
            End If
            If lsTranType = "" Then
                MsgBox("Select Tran Type", MsgBoxStyle.Information, gsProjectName)
                CboTransferType.Focus()
                Exit Sub
            Else
                lsTranType_single = lsTranType.Substring(0, 1)
            End If

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

            If lsTranType_single = "M" Then

                Dim lsFldName(11) As String

                lsFldName(1) = "SL NO"
                lsFldName(2) = "DP ID"
                lsFldName(3) = "FOLIO"
                lsFldName(4) = "NAME"
                lsFldName(5) = "DATE OF TRANSFER"
                lsFldName(6) = "SHARES"
                lsFldName(7) = "ADDR1"
                lsFldName(8) = "ADDR2"
                lsFldName(9) = "ADDR3"
                lsFldName(10) = "CITY"
                lsFldName(11) = "PINCODE"

                For i = 1 To 11
                    lsFldFormat &= lsFldName(i) & "|"
                Next

                For i = 1 To 11
                    If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                        lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                        & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                        & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                        If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                        Exit Sub
                    End If
                Next

            ElseIf lsTranType_single = "C" Then

                Dim lsFldName(10) As String

                lsFldName(1) = "SL NO"
                lsFldName(2) = "DP ID"
                lsFldName(3) = "FOLIO"
                lsFldName(4) = "NAME"
                lsFldName(5) = "DATE OF TRANSFER"
                lsFldName(6) = "SHARES"
                lsFldName(7) = "CLAIMANT DP ID / CLIENT ID"
                lsFldName(8) = "CLAIMANT NAME"
                lsFldName(9) = "SRN NO"
                lsFldName(10) = "CLAIM DATE"

                For i = 1 To 10
                    lsFldFormat &= lsFldName(i) & "|"
                Next

                For i = 1 To 10
                    If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                        lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                        & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                        & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                        If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                        Exit Sub
                    End If
                Next

            End If



            lsInwarddate = dtpDate.Value

            Using cmd As New MySqlCommand("pr_sta_ins_tiepfinward", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?In_iepfinward_date", Format(lsInwarddate, "yyyy-MM-dd"))
                cmd.Parameters("?In_iepfinward_date").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?In_comp_gid", lncompid)
                cmd.Parameters("?In_comp_gid").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?In_iepf_tran_type", lsTranType_single)
                cmd.Parameters("?In_iepf_tran_type").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?In_file_name", lsFileName)
                cmd.Parameters("?In_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?In_login_user", gsLoginUserCode)
                cmd.Parameters("?In_login_user").Direction = ParameterDirection.Input

                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_iepfinward_gid", MySqlDbType.Int64)
                cmd.Parameters("?out_iepfinward_gid").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())

                If (lnResult = 0) Then
                    lobjFileReturn.Msg = cmd.Parameters("?out_msg").Value.ToString()
                    If ShowFlag Then MsgBox(lobjFileReturn.Msg)
                End If
                lnFileId = Val(cmd.Parameters("?out_iepfinward_gid").Value.ToString())
            End Using

            btnGenerate.Enabled = False


            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)


                        Application.DoEvents()
                        If lsTranType_single = "M" Then
                            lsDPId = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 64)
                            lsFolio = Mid(QuoteFilter(.Item("FOLIO").ToString), 1, 124)
                            lsFolioName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 128)
                            lsDateofTransfer = QuoteFilter(.Item("DATE OF TRANSFER").ToString)
                            lsShares = Val(QuoteFilter(.Item("SHARES").ToString))
                            lsAddr1 = QuoteFilter(.Item("ADDR1").ToString)
                            lsAddr2 = QuoteFilter(.Item("ADDR2").ToString)
                            lsAddr3 = QuoteFilter(.Item("ADDR3").ToString)
                            lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 124)
                            lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 12)

                            If IsDate(lsDateofTransfer) = False Then
                                lsDateofTransfer = "0001-01-01"
                            Else
                                lsDateofTransfer = Format(CDate(lsDateofTransfer), "yyyy-MM-dd")
                            End If
                        ElseIf lsTranType_single = "C" Then
                            lsDPId = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 64)
                            lsFolio = Mid(QuoteFilter(.Item("FOLIO").ToString), 1, 124)
                            lsFolioName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 128)
                            lsDateofTransfer = QuoteFilter(.Item("DATE OF TRANSFER").ToString)
                            lsShares = Val(QuoteFilter(.Item("SHARES").ToString))
                            lsClaimDpId = Mid(QuoteFilter(.Item("CLAIMANT DP ID / CLIENT ID").ToString), 1, 128)
                            lsClaimntName = Mid(QuoteFilter(.Item("CLAIMANT NAME").ToString), 1, 128)
                            lsSRNNo = Mid(QuoteFilter(.Item("SRN NO").ToString), 1, 128)
                            lsClaimDate = QuoteFilter(.Item("CLAIM DATE").ToString)

                            If IsDate(lsClaimDate) = False Then
                                lsClaimDate = "0001-01-01"
                            Else
                                lsClaimDate = Format(CDate(lsClaimDate), "yyyy-MM-dd")
                            End If

                            If IsDate(lsDateofTransfer) = False Then
                                lsDateofTransfer = "0001-01-01"
                            Else
                                lsDateofTransfer = Format(CDate(lsDateofTransfer), "yyyy-MM-dd")
                            End If

                        End If
                        lsTotalShare = lsTotalShare + lsShares
                        Using cmd As New MySqlCommand("pr_sta_ins_tiepfinward_tran", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?In_iepfinward_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_comp_gid", lncompid)
                            cmd.Parameters.AddWithValue("?In_dpid", lsDPId)
                            cmd.Parameters.AddWithValue("?In_folio_no", lsFolio)
                            cmd.Parameters.AddWithValue("?In_dpid_name", lsFolioName)
                            cmd.Parameters.AddWithValue("?In_tran_date", lsDateofTransfer)
                            cmd.Parameters.AddWithValue("?In_no_of_shares", lsShares)
                            cmd.Parameters.AddWithValue("?In_iepf_tran_type", lsTranType_single)
                            cmd.Parameters.AddWithValue("?In_srn_no", lsSRNNo)
                            cmd.Parameters.AddWithValue("?In_claim_date", lsClaimDate)
                            cmd.Parameters.AddWithValue("?In_claimant_dpid", lsClaimDpId)
                            cmd.Parameters.AddWithValue("?In_claimant_name", lsClaimntName)
                            cmd.Parameters.AddWithValue("?In_addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("?In_addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("?In_addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("?In_city", lsCity)
                            cmd.Parameters.AddWithValue("?In_pincode", lsPincode)
                            cmd.Parameters.AddWithValue("?In_TotalShare", lsTotalShare)
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


            'fsSql = ""
            'fsSql &= " delete from  sta_trn_taddresslabel "
            'fsSql &= " where insert_by = '" & gsLoginUserCode & "' "

            'lnResult = Val(gfExecuteScalar(fsSql, gOdbcConn))


            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            btnGenerate.Enabled = True
            Application.DoEvents()
            Me.Cursor = Cursors.Default



        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gsProjectName)
            btnGenerate.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

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