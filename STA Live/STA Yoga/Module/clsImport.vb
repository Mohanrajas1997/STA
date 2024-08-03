Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports Microsoft.SqlServer

Public Class clsImport
    Public Function CertificateDistSeries(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(7) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsCertNo As String
        Dim lnShareCount As Long
        Dim lnDistFrom As Long
        Dim lnDistTo As Long
        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "CERTIFICATE NO"
        lsFldName(5) = "SHARES COUNT"
        lsFldName(6) = "DISTINCTIVE FROM"
        lsFldName(7) = "DISTINCTIVE TO"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 7
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 7
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileCertDistSeries)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsCertNo = Mid(QuoteFilter(.Item("CERTIFICATE NO").ToString), 1, 32)
                        lnShareCount = Val(QuoteFilter(.Item("SHARES COUNT").ToString))
                        lnDistFrom = Val(QuoteFilter(.Item("DISTINCTIVE FROM").ToString))
                        lnDistTo = Val(QuoteFilter(.Item("DISTINCTIVE TO").ToString))

                        Using cmd As New MySqlCommand("pr_sta_ins_certdist", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_cert_no", lsCertNo)
                            cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)
                            cmd.Parameters.AddWithValue("?in_certdist_from", lnDistFrom)
                            cmd.Parameters.AddWithValue("?in_certdist_to", lnDistTo)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function HistoryTran(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(20) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsTranDate As String
        Dim lsTranNo As String
        Dim lsTranType As String
        Dim lsStatus As String
        Dim lsFolioNo As String
        Dim lsFolioName As String
        Dim lsTranFolioNo As String
        Dim lsTranFolioName As String
        Dim lnCertNo As Long
        Dim lnTotShares As Long = 0
        Dim lnShareCount As Long
        Dim lnDistFrom As Long
        Dim lnDistTo As Long
        Dim lnTranCertNo As Long = 0
        Dim lnTranShareCount As Long = 0
        Dim lnTranDistFrom As Long = 0
        Dim lnTranDistTo As Long = 0
        Dim lsRemark As String
        Dim lsRefNo As String
        Dim lsInwardNo As String
        Dim lsInwardDate As String
        Dim lsOutwardNo As String
        Dim lsOutwardDate As String
        Dim lsClientId As String = ""
        Dim lsClientName As String = ""
        Dim lsDpId As String = ""
        Dim lsDpName As String = ""
        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "CO CODE"
        lsFldName(2) = "TRANSACTION DATE"
        lsFldName(3) = "TRANSACTION NO"
        lsFldName(4) = "TRANSACTION TYPE"
        lsFldName(5) = "STATUS"
        lsFldName(6) = "SELLER FOLIO"
        lsFldName(7) = "SELLER NAME"
        lsFldName(8) = "BUYER FOLIO"
        lsFldName(9) = "BUYER NAME"
        lsFldName(10) = "CERT NO#"
        lsFldName(11) = "CERT SHARES"
        lsFldName(12) = "DIST FROM"
        lsFldName(13) = "DIST TO"
        lsFldName(14) = "# SHARES"
        lsFldName(15) = "REJ REASON"
        lsFldName(16) = "REF NO"
        lsFldName(17) = "INW NO"
        lsFldName(18) = "INW DATE"
        lsFldName(19) = "OUTWARD NO"
        lsFldName(20) = "OUTWARD DATE"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 20
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 20
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileHistoryTran)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("CO CODE").ToString), 1, 16)
                        lsTranDate = QuoteFilter(.Item("TRANSACTION DATE").ToString)
                        lsTranNo = Mid(QuoteFilter(.Item("TRANSACTION NO").ToString), 1, 32)
                        lsTranType = Mid(QuoteFilter(.Item("TRANSACTION TYPE").ToString), 1, 32)
                        lsStatus = Mid(QuoteFilter(.Item("STATUS").ToString), 1, 32)

                        lsFolioNo = Mid(QuoteFilter(.Item("SELLER FOLIO").ToString), 1, 32)
                        lsFolioName = Mid(QuoteFilter(.Item("SELLER NAME").ToString), 1, 128)
                        lsTranFolioNo = Mid(QuoteFilter(.Item("BUYER FOLIO").ToString), 1, 32)
                        lsTranFolioName = Mid(QuoteFilter(.Item("BUYER NAME").ToString), 1, 128)
                        lnCertNo = Val(QuoteFilter(.Item("CERT NO#").ToString))
                        lnTotShares = Val(QuoteFilter(.Item("CERT SHARES").ToString))
                        lnDistFrom = Val(QuoteFilter(.Item("DIST FROM").ToString))
                        lnDistTo = Val(QuoteFilter(.Item("DIST TO").ToString))
                        lnShareCount = Val(QuoteFilter(.Item("# SHARES").ToString))
                        lsRemark = Mid(QuoteFilter(.Item("REJ REASON").ToString), 1, 255)
                        lsRefNo = Mid(QuoteFilter(.Item("REF NO").ToString), 1, 128)
                        lsInwardNo = Mid(QuoteFilter(.Item("INW NO").ToString), 1, 32)
                        lsInwardDate = QuoteFilter(.Item("INW DATE").ToString)
                        lsOutwardNo = Mid(QuoteFilter(.Item("OUTWARD NO").ToString), 1, 32)
                        lsOutwardDate = QuoteFilter(.Item("OUTWARD DATE").ToString)

                        If IsDate(lsTranDate) = False Then
                            lsTranDate = "0001-01-01"
                        Else
                            lsTranDate = Format(CDate(lsTranDate), "yyyy-MM-dd")
                        End If

                        If IsDate(lsInwardDate) = False Then
                            lsInwardDate = "0001-01-01"
                        Else
                            lsInwardDate = Format(CDate(lsInwardDate), "yyyy-MM-dd")
                        End If

                        If IsDate(lsOutwardDate) = False Then
                            lsOutwardDate = "0001-01-01"
                        Else
                            lsOutwardDate = Format(CDate(lsOutwardDate), "yyyy-MM-dd")
                        End If

                        Using cmd As New MySqlCommand("pr_sta_ins_history", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)

                            cmd.Parameters.AddWithValue("?in_tran_no", lsTranNo)
                            cmd.Parameters.AddWithValue("?in_tran_date", lsTranDate)
                            cmd.Parameters.AddWithValue("?in_tran_type", lsTranType)
                            cmd.Parameters.AddWithValue("?in_tran_status", lsStatus)

                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_folio_holder", lsFolioName)

                            cmd.Parameters.AddWithValue("?in_tran_folio_no", lsTranFolioNo)
                            cmd.Parameters.AddWithValue("?in_tran_folio_holder", lsTranFolioName)

                            cmd.Parameters.AddWithValue("?in_cert_no", lnCertNo)
                            cmd.Parameters.AddWithValue("?in_tot_shares", lnTotShares)
                            cmd.Parameters.AddWithValue("?in_certdist_from", lnDistFrom)
                            cmd.Parameters.AddWithValue("?in_certdist_to", lnDistTo)
                            cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)

                            cmd.Parameters.AddWithValue("?in_tran_cert_no", lnTranCertNo)
                            cmd.Parameters.AddWithValue("?in_tran_cert_tot_shares", 0)
                            cmd.Parameters.AddWithValue("?in_tran_certdist_from", lnTranDistFrom)
                            cmd.Parameters.AddWithValue("?in_tran_certdist_to", lnTranDistTo)
                            cmd.Parameters.AddWithValue("?in_tran_share_count", lnTranShareCount)

                            cmd.Parameters.AddWithValue("?in_tran_remark", lsRemark)
                            cmd.Parameters.AddWithValue("?in_tran_ref_no", lsRefNo)

                            cmd.Parameters.AddWithValue("?in_client_id", lsClientId)
                            cmd.Parameters.AddWithValue("?in_client_name", lsClientName)
                            cmd.Parameters.AddWithValue("?in_dp_id", lsDpId)
                            cmd.Parameters.AddWithValue("?in_dp_name", lsDpName)

                            cmd.Parameters.AddWithValue("?in_tran_inward_no", lsInwardNo)
                            cmd.Parameters.AddWithValue("?in_tran_inward_date", lsInwardDate)
                            cmd.Parameters.AddWithValue("?in_tran_outward_no", lsOutwardNo)
                            cmd.Parameters.AddWithValue("?in_tran_outward_date", lsOutwardDate)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function HistoryConsolidationSplit(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(15) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsTranDate As String
        Dim lsTranNo As String
        Dim lsTranType As String
        Dim lsStatus As String
        Dim lsFolioNo As String
        Dim lsFolioName As String
        Dim lsTranFolioNo As String
        Dim lsTranFolioName As String
        Dim lnCertNo As Long
        Dim lnTotShares As Long
        Dim lnShareCount As Long
        Dim lnDistFrom As Long
        Dim lnDistTo As Long
        Dim lnTranCertNo As Long = 0
        Dim lnTranCertTotShares As Long = 0
        Dim lnTranShareCount As Long = 0
        Dim lnTranDistFrom As Long = 0
        Dim lnTranDistTo As Long = 0
        Dim lsRemark As String
        Dim lsRefNo As String
        Dim lsInwardNo As String
        Dim lsInwardDate As String
        Dim lsOutwardNo As String
        Dim lsOutwardDate As String
        Dim lsClientId As String = ""
        Dim lsClientName As String = ""
        Dim lsDpId As String = ""
        Dim lsDpName As String = ""
        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "CO CODE"
        lsFldName(2) = "TRANSACTION DATE"
        lsFldName(3) = "TRANSACTION NO"
        lsFldName(4) = "TRANSACTION TYPE"
        lsFldName(5) = "FOLIO"
        lsFldName(6) = "OLD CERT NO"
        lsFldName(7) = "OLD CERT SHARES"
        lsFldName(8) = "OLD DIST FROM"
        lsFldName(9) = "OLD DIST TO"
        lsFldName(10) = "OLD # SHARES"
        lsFldName(11) = "NEW CERT NO"
        lsFldName(12) = "NEW CERT SHARES"
        lsFldName(13) = "NEW DIST FROM"
        lsFldName(14) = "NEW DIST TO"
        lsFldName(15) = "NEW # SHARES"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 15
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 15
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileHistoryConsSplit)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("CO CODE").ToString), 1, 16)
                        lsTranDate = QuoteFilter(.Item("TRANSACTION DATE").ToString)
                        lsTranNo = Mid(QuoteFilter(.Item("TRANSACTION NO").ToString), 1, 32)
                        lsTranType = Mid(QuoteFilter(.Item("TRANSACTION TYPE").ToString), 1, 32)
                        lsStatus = "Approved"

                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO").ToString), 1, 32)
                        lsFolioName = ""
                        lsTranFolioNo = ""
                        lsTranFolioName = ""

                        lnCertNo = Val(QuoteFilter(.Item("OLD CERT NO").ToString))
                        lnTotShares = Val(QuoteFilter(.Item("OLD CERT SHARES").ToString))
                        lnDistFrom = Val(QuoteFilter(.Item("OLD DIST FROM").ToString))
                        lnDistTo = Val(QuoteFilter(.Item("OLD DIST TO").ToString))
                        lnShareCount = Val(QuoteFilter(.Item("OLD # SHARES").ToString))

                        lnTranCertNo = Val(QuoteFilter(.Item("NEW CERT NO").ToString))
                        lnTranCertTotShares = Val(QuoteFilter(.Item("NEW CERT SHARES").ToString))
                        lnTranDistFrom = Val(QuoteFilter(.Item("NEW DIST FROM").ToString))
                        lnTranDistTo = Val(QuoteFilter(.Item("NEW DIST TO").ToString))
                        lnTranShareCount = Val(QuoteFilter(.Item("NEW # SHARES").ToString))

                        lsRemark = ""
                        lsRefNo = ""
                        lsInwardNo = ""
                        lsInwardDate = ""
                        lsOutwardNo = ""
                        lsOutwardDate = ""

                        If IsDate(lsTranDate) = False Then
                            lsTranDate = "0001-01-01"
                        Else
                            lsTranDate = Format(CDate(lsTranDate), "yyyy-MM-dd")
                        End If

                        If IsDate(lsInwardDate) = False Then
                            lsInwardDate = "0001-01-01"
                        Else
                            lsInwardDate = Format(CDate(lsInwardDate), "yyyy-MM-dd")
                        End If

                        If IsDate(lsOutwardDate) = False Then
                            lsOutwardDate = "0001-01-01"
                        Else
                            lsOutwardDate = Format(CDate(lsOutwardDate), "yyyy-MM-dd")
                        End If

                        Using cmd As New MySqlCommand("pr_sta_ins_history", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)

                            cmd.Parameters.AddWithValue("?in_tran_no", lsTranNo)
                            cmd.Parameters.AddWithValue("?in_tran_date", lsTranDate)
                            cmd.Parameters.AddWithValue("?in_tran_type", lsTranType)
                            cmd.Parameters.AddWithValue("?in_tran_status", lsStatus)

                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_folio_holder", lsFolioName)

                            cmd.Parameters.AddWithValue("?in_tran_folio_no", lsTranFolioNo)
                            cmd.Parameters.AddWithValue("?in_tran_folio_holder", lsTranFolioName)

                            cmd.Parameters.AddWithValue("?in_cert_no", lnCertNo)
                            cmd.Parameters.AddWithValue("?in_tot_shares", lnTotShares)
                            cmd.Parameters.AddWithValue("?in_certdist_from", lnDistFrom)
                            cmd.Parameters.AddWithValue("?in_certdist_to", lnDistTo)
                            cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)

                            cmd.Parameters.AddWithValue("?in_tran_cert_no", lnTranCertNo)
                            cmd.Parameters.AddWithValue("?in_tran_cert_tot_shares", lnTranCertTotShares)
                            cmd.Parameters.AddWithValue("?in_tran_certdist_from", lnTranDistFrom)
                            cmd.Parameters.AddWithValue("?in_tran_certdist_to", lnTranDistTo)
                            cmd.Parameters.AddWithValue("?in_tran_share_count", lnTranShareCount)

                            cmd.Parameters.AddWithValue("?in_tran_remark", lsRemark)
                            cmd.Parameters.AddWithValue("?in_tran_ref_no", lsRefNo)

                            cmd.Parameters.AddWithValue("?in_client_id", lsClientId)
                            cmd.Parameters.AddWithValue("?in_client_name", lsClientName)
                            cmd.Parameters.AddWithValue("?in_dp_id", lsDpId)
                            cmd.Parameters.AddWithValue("?in_dp_name", lsDpName)

                            cmd.Parameters.AddWithValue("?in_tran_inward_no", lsInwardNo)
                            cmd.Parameters.AddWithValue("?in_tran_inward_date", lsInwardDate)
                            cmd.Parameters.AddWithValue("?in_tran_outward_no", lsOutwardNo)
                            cmd.Parameters.AddWithValue("?in_tran_outward_date", lsOutwardDate)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function HistoryNsdlCdsl(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(20) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsTranDate As String
        Dim lsTranNo As String
        Dim lsTranType As String
        Dim lsStatus As String
        Dim lsFolioNo As String = ""
        Dim lsFolioName As String = ""
        Dim lsTranFolioNo As String = ""
        Dim lsTranFolioName As String = ""
        Dim lnCertNo As Long
        Dim lnTotShares As Long
        Dim lnShareCount As Long
        Dim lnDistFrom As Long
        Dim lnDistTo As Long
        Dim lnTranCertNo As Long = 0
        Dim lnTranShareCount As Long = 0
        Dim lnTranDistFrom As Long = 0
        Dim lnTranDistTo As Long = 0
        Dim lsRemark As String
        Dim lsRefNo As String
        Dim lsInwardNo As String
        Dim lsInwardDate As String
        Dim lsOutwardNo As String
        Dim lsOutwardDate As String
        Dim lsClientId As String = ""
        Dim lsClientName As String = ""
        Dim lsDpId As String = ""
        Dim lsDpName As String = ""
        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "CO CODE"
        lsFldName(2) = "TRANSACTION DATE"
        lsFldName(3) = "TRANSACTION NO"
        lsFldName(4) = "TRANSACTION TYPE"
        lsFldName(5) = "STATUS"
        lsFldName(6) = "CLIENT ID"
        lsFldName(7) = "CLIENT NAME"
        lsFldName(8) = "DP ID"
        lsFldName(9) = "DP NAME"
        lsFldName(10) = "CERT NO#"
        lsFldName(11) = "CERT SHARES"
        lsFldName(12) = "DIST FROM"
        lsFldName(13) = "DIST TO"
        lsFldName(14) = "# SHARES"
        lsFldName(15) = "REJ REASON"
        lsFldName(16) = "REF NO"
        lsFldName(17) = "INW NO"
        lsFldName(18) = "INW DATE"
        lsFldName(19) = "OUTWARD NO"
        lsFldName(20) = "OUTWARD DATE"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 20
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 20
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileHistoryNsdlCdsl)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("CO CODE").ToString), 1, 16)
                        lsTranDate = QuoteFilter(.Item("TRANSACTION DATE").ToString)
                        lsTranNo = Mid(QuoteFilter(.Item("TRANSACTION NO").ToString), 1, 32)
                        lsTranType = Mid(QuoteFilter(.Item("TRANSACTION TYPE").ToString), 1, 32)
                        lsStatus = Mid(QuoteFilter(.Item("STATUS").ToString), 1, 32)

                        lsClientId = Mid(QuoteFilter(.Item("CLIENT ID").ToString), 1, 32)
                        lsClientName = Mid(QuoteFilter(.Item("CLIENT NAME").ToString), 1, 128)
                        lsDpId = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 32)
                        lsDpName = Mid(QuoteFilter(.Item("DP NAME").ToString), 1, 128)
                        lnCertNo = Val(QuoteFilter(.Item("CERT NO#").ToString))
                        lnTotShares = Val(QuoteFilter(.Item("CERT SHARES").ToString))
                        lnDistFrom = Val(QuoteFilter(.Item("DIST FROM").ToString))
                        lnDistTo = Val(QuoteFilter(.Item("DIST TO").ToString))
                        lnShareCount = Val(QuoteFilter(.Item("# SHARES").ToString))
                        lsRemark = Mid(QuoteFilter(.Item("REJ REASON").ToString), 1, 255)
                        lsRefNo = Mid(QuoteFilter(.Item("REF NO").ToString), 1, 128)
                        lsInwardNo = Mid(QuoteFilter(.Item("INW NO").ToString), 1, 32)
                        lsInwardDate = QuoteFilter(.Item("INW DATE").ToString)
                        lsOutwardNo = Mid(QuoteFilter(.Item("OUTWARD NO").ToString), 1, 32)
                        lsOutwardDate = QuoteFilter(.Item("OUTWARD DATE").ToString)

                        If IsDate(lsTranDate) = False Then
                            lsTranDate = "0001-01-01"
                        Else
                            lsTranDate = Format(CDate(lsTranDate), "yyyy-MM-dd")
                        End If

                        If IsDate(lsInwardDate) = False Then
                            lsInwardDate = "0001-01-01"
                        Else
                            lsInwardDate = Format(CDate(lsInwardDate), "yyyy-MM-dd")
                        End If

                        If IsDate(lsOutwardDate) = False Then
                            lsOutwardDate = "0001-01-01"
                        Else
                            lsOutwardDate = Format(CDate(lsOutwardDate), "yyyy-MM-dd")
                        End If

                        Using cmd As New MySqlCommand("pr_sta_ins_history", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)

                            cmd.Parameters.AddWithValue("?in_tran_no", lsTranNo)
                            cmd.Parameters.AddWithValue("?in_tran_date", lsTranDate)
                            cmd.Parameters.AddWithValue("?in_tran_type", lsTranType)
                            cmd.Parameters.AddWithValue("?in_tran_status", lsStatus)

                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_folio_holder", lsFolioName)

                            cmd.Parameters.AddWithValue("?in_tran_folio_no", lsTranFolioNo)
                            cmd.Parameters.AddWithValue("?in_tran_folio_holder", lsTranFolioName)

                            cmd.Parameters.AddWithValue("?in_cert_no", lnCertNo)
                            cmd.Parameters.AddWithValue("?in_tot_shares", lnTotShares)
                            cmd.Parameters.AddWithValue("?in_certdist_from", lnDistFrom)
                            cmd.Parameters.AddWithValue("?in_certdist_to", lnDistTo)
                            cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)

                            cmd.Parameters.AddWithValue("?in_tran_cert_no", lnTranCertNo)
                            cmd.Parameters.AddWithValue("?in_tran_cert_tot_shares", 0)
                            cmd.Parameters.AddWithValue("?in_tran_certdist_from", lnTranDistFrom)
                            cmd.Parameters.AddWithValue("?in_tran_certdist_to", lnTranDistTo)
                            cmd.Parameters.AddWithValue("?in_tran_share_count", lnTranShareCount)

                            cmd.Parameters.AddWithValue("?in_tran_remark", lsRemark)
                            cmd.Parameters.AddWithValue("?in_tran_ref_no", lsRefNo)

                            cmd.Parameters.AddWithValue("?in_client_id", lsClientId)
                            cmd.Parameters.AddWithValue("?in_client_name", lsClientName)
                            cmd.Parameters.AddWithValue("?in_dp_id", lsDpId)
                            cmd.Parameters.AddWithValue("?in_dp_name", lsDpName)

                            cmd.Parameters.AddWithValue("?in_tran_inward_no", lsInwardNo)
                            cmd.Parameters.AddWithValue("?in_tran_inward_date", lsInwardDate)
                            cmd.Parameters.AddWithValue("?in_tran_outward_no", lsOutwardNo)
                            cmd.Parameters.AddWithValue("?in_tran_outward_date", lsOutwardDate)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function Certificate(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(12) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsCertNo As String
        Dim ldIssueDate As Date
        Dim ldExpiredDate As Date
        Dim ldLockinPeriodFrom As Date
        Dim ldLockinPeriodTo As Date
        Dim ldHoldDate As Date
        Dim ldHoldExpireDate As Date
        Dim lsStatus As String
        Dim lsRemark As String
        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "CERTIFICATE NO"
        lsFldName(5) = "ISSUE DATE"
        lsFldName(6) = "EXPIRED DATE"
        lsFldName(7) = "LOCKIN PERIOD FROM"
        lsFldName(8) = "LOCKIN PERIOD TO"
        lsFldName(9) = "HOLD DATE"
        lsFldName(10) = "HOLD EXPIRE DATE"
        lsFldName(11) = "STATUS"
        lsFldName(12) = "REMARK"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 12
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 12
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileCert)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        ldIssueDate = Nothing
                        ldExpiredDate = Nothing
                        ldLockinPeriodFrom = Nothing
                        ldLockinPeriodTo = Nothing
                        ldHoldDate = Nothing
                        ldHoldExpireDate = Nothing

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsCertNo = Mid(QuoteFilter(.Item("CERTIFICATE NO").ToString), 1, 32)

                        lsTxt = QuoteFilter(.Item("ISSUE DATE").ToString)
                        If IsDate(lsTxt) Then ldIssueDate = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("EXPIRED DATE").ToString)
                        If IsDate(lsTxt) Then ldExpiredDate = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("LOCKIN PERIOD FROM").ToString)
                        If IsDate(lsTxt) Then ldLockinPeriodFrom = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("LOCKIN PERIOD TO").ToString)
                        If IsDate(lsTxt) Then ldLockinPeriodTo = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("HOLD DATE").ToString)
                        If IsDate(lsTxt) Then ldHoldDate = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("HOLD EXPIRE DATE").ToString)
                        If IsDate(lsTxt) Then ldHoldExpireDate = CDate(lsTxt)

                        lsStatus = Mid(QuoteFilter(.Item("STATUS").ToString), 1, 32)
                        lsRemark = Mid(QuoteFilter(.Item("REMARK").ToString), 1, 255)

                        Using cmd As New MySqlCommand("pr_sta_ins_cert", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_cert_no", lsCertNo)
                            cmd.Parameters.AddWithValue("?in_issue_date", ldIssueDate)
                            cmd.Parameters.AddWithValue("?in_expired_date", ldExpiredDate)
                            cmd.Parameters.AddWithValue("?in_lockin_period_from", ldLockinPeriodFrom)
                            cmd.Parameters.AddWithValue("?in_lockin_period_to", ldLockinPeriodTo)
                            cmd.Parameters.AddWithValue("?in_hold_date", ldHoldDate)
                            cmd.Parameters.AddWithValue("?in_hold_release_date", ldHoldExpireDate)
                            cmd.Parameters.AddWithValue("?in_status", lsStatus)
                            cmd.Parameters.AddWithValue("?in_remark", lsRemark)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function ECSValidationFile(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        'Dim i As Integer
        'Dim lsFldName(30) As String
        'Dim lsFldFormat As String = ""
        'Dim ds As New DataSet
        'Dim c As Integer = 0
        'Dim d As Integer = 0

        'Dim lobjExcelDatatable As New DataTable
    End Function

    Public Function ECSBounceFileImport(FileName As String, SheetName As String, CompanyGID As Integer, FinYear As Integer, InterimCode As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(17) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompGid As Integer = Val(CompanyGID.ToString())
        Dim lsFinyearGid As Integer = Val(FinYear.ToString())
        Dim lsinterimCode As String = Mid(QuoteFilter(InterimCode.ToString), 1, 3)
        Dim lsFolioClientType As String
        Dim lsFolioClientId As String
        Dim lsFolioClientdpid As String
        Dim lsBeneName As String
        Dim lsDividendAmount As Double
        Dim lsCurrencyCode As String
        Dim lsCurrencyValue As Double
        Dim lsCurrencyAmount As Double
        Dim lsPaymentMode As String
        Dim lsBankName As String
        Dim lsBankBranch As String
        Dim lsBankAccno As String
        Dim lsBankMicrcode As String
        Dim lsBankIfsccode As String
        Dim lsPaymentRefno As String
        Dim lsPaymentgid As Long

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "DP ID"
        lsFldName(3) = "FOLIO/CLIENT ID"
        lsFldName(4) = "FOLIO/CLIENT ID TYPE"
        lsFldName(5) = "BENEFICIARY NAME"
        lsFldName(6) = "DIVIDEND AMOUNT"
        lsFldName(7) = "CURRENCY CODE"
        lsFldName(8) = "CURRENCY VALUE"
        lsFldName(9) = "CURRENCY AMOUNT"
        lsFldName(10) = "PAYMENT MODE"
        lsFldName(11) = "BANK NAME"
        lsFldName(12) = "BANK BRANCH"
        lsFldName(13) = "BANK A/C NO"
        lsFldName(14) = "MICR CODE"
        lsFldName(15) = "IFSC CODE"
        lsFldName(16) = "PAYMENT REF NO"
        lsFldName(17) = "PAYMENT GID"

        Try

            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 16
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 16
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileECSBounceFile)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        'lsCompGid = Val(QuoteFilter(.Item("COMPANY GID").ToString()))
                        'lsFinyearGid = Val(QuoteFilter(.Item("FINANCIAL YEAR GID").ToString()))
                        'lsinterimCode = Mid(QuoteFilter(.Item("INTERIM CODE").ToString), 1, 8)
                        lsFolioClientId = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID").ToString), 1, 32)
                        lsFolioClientType = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID TYPE").ToString), 1, 1)
                        lsFolioClientdpid = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 128)
                        lsBeneName = Mid(QuoteFilter(.Item("BENEFICIARY NAME").ToString), 1, 128)
                        lsDividendAmount = Val(QuoteFilter(.Item("DIVIDEND AMOUNT").ToString))
                        lsCurrencyCode = Mid(QuoteFilter(.Item("CURRENCY CODE").ToString), 1, 8)
                        lsCurrencyValue = Val(QuoteFilter(.Item("CURRENCY VALUE").ToString))
                        lsCurrencyAmount = Val(QuoteFilter(.Item("CURRENCY AMOUNT").ToString))
                        lsPaymentMode = Mid(QuoteFilter(.Item("PAYMENT MODE").ToString), 1, 128)
                        lsBankName = Mid(QuoteFilter(.Item("BANK NAME").ToString), 1, 128)
                        lsBankBranch = Mid(QuoteFilter(.Item("BANK BRANCH").ToString), 1, 128)
                        lsBankAccno = Mid(QuoteFilter(.Item("BANK A/C NO").ToString), 1, 32)
                        lsBankMicrcode = Mid(QuoteFilter(.Item("MICR CODE").ToString), 1, 16)
                        lsBankIfsccode = Mid(QuoteFilter(.Item("IFSC CODE").ToString), 1, 32)
                        lsPaymentRefno = Mid(QuoteFilter(.Item("PAYMENT REF NO").ToString), 1, 128)
                        lsPaymentgid = Val(QuoteFilter(.Item("PAYMENT GID").ToString))


                        Using cmd As New MySqlCommand("pr_sta_ins_tpaymentstatus", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_gid", lsCompGid)
                            cmd.Parameters.AddWithValue("?in_finyear_gid", lsFinyearGid)
                            cmd.Parameters.AddWithValue("?in_interim_code", lsinterimCode)
                            cmd.Parameters.AddWithValue("in_folioclient_id_type", lsFolioClientType)
                            cmd.Parameters.AddWithValue("in_folioclient_id", lsFolioClientId)
                            cmd.Parameters.AddWithValue("in_folioclient_dp_id", lsFolioClientdpid)
                            cmd.Parameters.AddWithValue("in_bene_name", lsBeneName)
                            cmd.Parameters.AddWithValue("in_dividend_amount", lsDividendAmount)
                            cmd.Parameters.AddWithValue("in_currency_code", lsCurrencyCode)
                            cmd.Parameters.AddWithValue("in_currency_value", lsCurrencyValue)
                            cmd.Parameters.AddWithValue("in_currency_amount", lsCurrencyAmount)
                            cmd.Parameters.AddWithValue("in_payment_mode", lsPaymentMode)
                            cmd.Parameters.AddWithValue("in_bank_name", lsBankName)
                            cmd.Parameters.AddWithValue("in_bank_branch", lsBankBranch)
                            cmd.Parameters.AddWithValue("in_bank_acc_no", lsBankAccno)
                            cmd.Parameters.AddWithValue("in_bank_micr_code", lsBankMicrcode)
                            cmd.Parameters.AddWithValue("in_bank_ifsc_code", lsBankIfsccode)
                            cmd.Parameters.AddWithValue("in_payment_ref_no", lsPaymentRefno)
                            cmd.Parameters.AddWithValue("in_payment_gid", lsPaymentgid)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try

    End Function

    Public Function DividendSuccess(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(13) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String
        Dim lsFinyearcode As String
        Dim lsinterimCode As String
        Dim lsFolioClientType As String
        Dim lsFolioClientId As String
        Dim lsFolioClientdpid As String
        Dim lsBeneName As String
        Dim lsDividendAmount As Double
        Dim lsPaymentMode As String
        Dim lsPaidRefNo As String
        Dim lsPaidDate As String
        Dim lsPaymentgid As Long

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "FINANCIAL YEAR"
        lsFldName(4) = "INTERIM CODE"
        lsFldName(5) = "FOLIO/CLIENT ID TYPE"
        lsFldName(6) = "DP ID"
        lsFldName(7) = "FOLIO/CLIENT ID"
        lsFldName(8) = "BENEFICIARY NAME"
        lsFldName(9) = "DIVIDEND AMOUNT"
        lsFldName(10) = "PAYMENT MODE"
        lsFldName(11) = "PAID REF NO"
        lsFldName(12) = "PAID DATE"
        lsFldName(13) = "PAYMENT GID"

        Try
            Call FormatSheet(FileName, SheetName)

            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 13
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 13
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileDividendsuccess)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 64)
                        lsFinyearcode = Mid(QuoteFilter(.Item("FINANCIAL YEAR").ToString), 1, 64)
                        lsinterimCode = Mid(QuoteFilter(.Item("INTERIM CODE").ToString), 1, 8)
                        lsFolioClientId = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID").ToString), 1, 128)
                        lsFolioClientType = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID TYPE").ToString), 1, 1)
                        lsFolioClientdpid = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 128)
                        lsBeneName = Mid(QuoteFilter(.Item("BENEFICIARY NAME").ToString), 1, 128)
                        lsDividendAmount = Val(QuoteFilter(.Item("DIVIDEND AMOUNT").ToString))
                        lsPaymentMode = Mid(QuoteFilter(.Item("PAYMENT MODE").ToString), 1, 128)
                        lsPaidRefNo = Mid(QuoteFilter(.Item("PAID REF NO").ToString), 1, 128)
                        lsPaidDate = QuoteFilter(.Item("Paid Date").ToString)
                        lsPaymentgid = Val(QuoteFilter(.Item("PAYMENT GID").ToString))

                        If IsDate(lsPaidDate) = False Then
                            lsPaidDate = "0001-01-01"
                        Else
                            lsPaidDate = Format(CDate(lsPaidDate), "yyyy-MM-dd")
                        End If


                        Using cmd As New MySqlCommand("pr_sta_ins_tpaymentpaid", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?in_finyear_code", lsFinyearcode)
                            cmd.Parameters.AddWithValue("?in_interim_code", lsinterimCode)
                            cmd.Parameters.AddWithValue("in_folioclient_id_type", lsFolioClientType)
                            cmd.Parameters.AddWithValue("in_folioclient_id", lsFolioClientId)
                            cmd.Parameters.AddWithValue("in_folioclient_dp_id", lsFolioClientdpid)
                            cmd.Parameters.AddWithValue("in_bene_name", lsBeneName)
                            cmd.Parameters.AddWithValue("in_dividend_amount", lsDividendAmount)
                            cmd.Parameters.AddWithValue("in_payment_mode", lsPaymentMode)
                            cmd.Parameters.AddWithValue("in_paid_refno", lsPaidRefNo)
                            cmd.Parameters.AddWithValue("in_paid_date", lsPaidDate)
                            cmd.Parameters.AddWithValue("in_payment_gid", lsPaymentgid)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function HistoryPhysicalTran(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(14) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String
        Dim lsDebitFolioNo As String
        Dim lsCreditFolioNo As String
        Dim lsTranType As String
        Dim lsTranDate As String
        Dim lsValueDate As String
        Dim lsCertNo As String
        Dim lsShareCount As Long = 0
        Dim lsTranRefNo As String
        Dim lsRefNo As String
        Dim lsMaker As String
        Dim lsChecker As String
        Dim lsRemark As String




        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "DEBIT FOLIO NO"
        lsFldName(4) = "CREDIT FOLIO NO"
        lsFldName(5) = "TRAN TYPE"
        lsFldName(6) = "TRAN DATE"
        lsFldName(7) = "VALUE DATE"
        lsFldName(8) = "CERT NO"
        lsFldName(9) = "SHARE COUNT"
        lsFldName(10) = "TRAN REF NO"
        lsFldName(11) = "REF NO"
        lsFldName(12) = "MAKER"
        lsFldName(13) = "CHECKER"
        lsFldName(14) = "REMARK"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 14
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 14
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileHistoryPhysicalFolio)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 32)
                        lsDebitFolioNo = Mid(QuoteFilter(.Item("DEBIT FOLIO NO").ToString), 1, 32)
                        lsCreditFolioNo = Mid(QuoteFilter(.Item("CREDIT FOLIO NO").ToString), 1, 32)
                        lsTranType = Mid(QuoteFilter(.Item("TRAN TYPE").ToString), 1, 32)
                        lsTranDate = QuoteFilter(.Item("TRAN DATE").ToString)
                        lsValueDate = QuoteFilter(.Item("VALUE DATE").ToString)
                        lsCertNo = QuoteFilter(.Item("CERT NO").ToString)
                        lsShareCount = Val(QuoteFilter(.Item("SHARE COUNT").ToString))
                        lsTranRefNo = Mid(QuoteFilter(.Item("TRAN REF NO").ToString), 1, 32)
                        lsRefNo = Mid(QuoteFilter(.Item("REF NO").ToString), 1, 32)
                        lsMaker = Mid(QuoteFilter(.Item("MAKER").ToString), 1, 32)
                        lsChecker = Mid(QuoteFilter(.Item("CHECKER").ToString), 1, 32)
                        lsRemark = QuoteFilter(.Item("REMARK").ToString)



                        If IsDate(lsTranDate) = False Then
                            lsTranDate = "0001-01-01"
                        Else
                            lsTranDate = Format(CDate(lsTranDate), "yyyy-MM-dd")
                        End If

                        If IsDate(lsValueDate) = False Then
                            lsValueDate = "0001-01-01"
                        Else
                            lsValueDate = Format(CDate(lsValueDate), "yyyy-MM-dd")
                        End If


                        Using cmd As New MySqlCommand("pr_sta_ins_tfoliotranhistory", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?in_debit_folio_no", lsDebitFolioNo)
                            cmd.Parameters.AddWithValue("?in_credit_folio_no", lsCreditFolioNo)
                            cmd.Parameters.AddWithValue("in_tran_type", lsTranType)
                            cmd.Parameters.AddWithValue("in_tran_date", lsTranDate)
                            cmd.Parameters.AddWithValue("in_value_date", lsValueDate)
                            cmd.Parameters.AddWithValue("in_cert_no", lsCertNo)
                            cmd.Parameters.AddWithValue("in_share_count", lsShareCount)
                            cmd.Parameters.AddWithValue("in_tran_ref_no", lsTranRefNo)
                            cmd.Parameters.AddWithValue("in_ref_no", lsRefNo)
                            cmd.Parameters.AddWithValue("in_maker", lsMaker)
                            cmd.Parameters.AddWithValue("in_checker", lsChecker)
                            cmd.Parameters.AddWithValue("?in_remarks", lsRemark)
                            cmd.Parameters.AddWithValue("?in_insert_by", gsLoginUserName)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function HistoryDividenddt(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(10) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String = ""
        Dim lsDpid As String = ""
        Dim lsFolioNo As String = ""
        Dim lsName As String = ""
        Dim lsFinacialYear As String = ""
        Dim lsWarrantNo As String = ""
        Dim lsChequeNo As String = ""
        Dim lsShares As String = ""
        Dim lsAmount As String
        Dim lsStatus As String = ""





        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "COMPANY"
        lsFldName(2) = "FINANCIAL YEAR"
        lsFldName(3) = "DP ID"
        lsFldName(4) = "FOLIO NO"
        lsFldName(5) = "NAME"
        lsFldName(6) = "WARRANT NO"
        lsFldName(7) = "CHEQUE NO"
        lsFldName(8) = "SHARES"
        lsFldName(9) = "AMOUNT"
        lsFldName(10) = "STATUS"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 10
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 10
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next



            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileHistoryDividend)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 32)
                        lsFinacialYear = Mid(QuoteFilter(.Item("Financial Year").ToString), 1, 255)
                        lsDpid = Mid(QuoteFilter(.Item("DP Id").ToString), 1, 255)
                        lsFolioNo = Mid(QuoteFilter(.Item("Folio No").ToString), 1, 255)
                        lsName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 255)
                        lsWarrantNo = Mid(QuoteFilter(.Item("Warrant No").ToString), 1, 255)
                        lsChequeNo = Mid(QuoteFilter(.Item("Cheque No").ToString), 1, 255)
                        lsShares = QuoteFilter(.Item("shares").ToString)
                        lsAmount = QuoteFilter(.Item("Amount").ToString)
                        lsStatus = Mid(QuoteFilter(.Item("Status").ToString), 1, 255)






                        Using cmd As New MySqlCommand("pr_web_set_dividendhistory", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?in_dp_id", lsDpid)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_name", lsName)
                            cmd.Parameters.AddWithValue("in_finacial_year", lsFinacialYear)
                            cmd.Parameters.AddWithValue("in_warrant_no", lsWarrantNo)
                            cmd.Parameters.AddWithValue("in_cheque_no", lsChequeNo)
                            cmd.Parameters.AddWithValue("in_shares", lsShares)
                            cmd.Parameters.AddWithValue("in_amount", lsAmount)
                            cmd.Parameters.AddWithValue("in_status", lsStatus)
                            cmd.Parameters.AddWithValue("?in_insert_by", gsLoginUserName)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function HistoryAllotment(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(7) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String = ""
        Dim lsFolioNo As String = ""
        Dim lsAllotmentNo As String = ""
        Dim lsAllotmentDate As String = ""
        Dim lsCertNo As String = ""
        Dim lsShareCount As Long = 0
        Dim lsRemarks As String = ""


        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "COMPANY"
        lsFldName(2) = "FOLIO NO"
        lsFldName(3) = "ALLOTMENT NO"
        lsFldName(4) = "ALLOTMENT DATE"
        lsFldName(5) = "CERT NO"
        lsFldName(6) = "SHARE COUNT"
        lsFldName(7) = "REMARKS"


        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 7
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 7
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFillHistoryAllotment)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 32)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 255)
                        lsAllotmentNo = Mid(QuoteFilter(.Item("ALLOTMENT NO").ToString), 1, 255)
                        lsAllotmentDate = Mid(QuoteFilter(.Item("ALLOTMENT DATE").ToString), 1, 255)
                        lsCertNo = Mid(QuoteFilter(.Item("CERT NO").ToString), 1, 255)
                        lsShareCount = Val(QuoteFilter(.Item("SHARE COUNT").ToString))
                        lsRemarks = QuoteFilter(.Item("REMARKS").ToString)


                        If IsDate(lsAllotmentDate) = False Then
                            lsAllotmentDate = "0001-01-01"
                        Else
                            lsAllotmentDate = Format(CDate(lsAllotmentDate), "yyyy-MM-dd")
                        End If




                        Using cmd As New MySqlCommand("pr_web_set_allotmenthis", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_allot_no", lsAllotmentNo)
                            cmd.Parameters.AddWithValue("in_allot_date", lsAllotmentDate)
                            cmd.Parameters.AddWithValue("in_share_count", lsShareCount)
                            cmd.Parameters.AddWithValue("in_cert_no", lsCertNo)
                            cmd.Parameters.AddWithValue("in_remarks", lsRemarks)

                            cmd.Parameters.AddWithValue("?in_insert_by", gsLoginUserName)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function LetterDispatch(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(8) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String
        Dim lsFolioNo As String
        Dim lsHolderName As String
        Dim lsDispatchType As String
        Dim lsDispatchDate As String
        Dim lsModeOfDispatch As String
        Dim lsWheatherRUD As String
        Dim lsResponseSlno As String


        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "UNIT NAME"
        lsFldName(2) = "FOLIO NO"
        lsFldName(3) = "NAME"
        lsFldName(4) = "TYPE"
        lsFldName(5) = "LETTER SENT ON"
        lsFldName(6) = "MODE OF DESPATCH"
        lsFldName(7) = "WHETHER RUD"
        lsFldName(8) = "RESPONSE SL NO"


        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 8
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 8
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next



            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileLetterDispatch)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("UNIT NAME").ToString), 1, 64)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsHolderName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 250)
                        lsDispatchType = Mid(QuoteFilter(.Item("TYPE").ToString), 1, 32)
                        lsDispatchDate = QuoteFilter(.Item("LETTER SENT ON").ToString)
                        lsModeOfDispatch = Mid(QuoteFilter(.Item("MODE OF DESPATCH").ToString), 1, 32)
                        lsWheatherRUD = Mid(QuoteFilter(.Item("WHETHER RUD").ToString), 1, 32)
                        lsResponseSlno = Mid(QuoteFilter(.Item("RESPONSE SL NO").ToString), 1, 32)


                        If IsDate(lsDispatchDate) = False Then
                            lsDispatchDate = ""
                        Else
                            lsDispatchDate = Format(CDate(lsDispatchDate), "yyyy-MM-dd")
                        End If


                        Using cmd As New MySqlCommand("pr_sta_ins_tletter_dispatch", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_holder_name", lsHolderName)
                            cmd.Parameters.AddWithValue("in_dispatch_type", lsDispatchType)
                            cmd.Parameters.AddWithValue("in_dispatch_date", lsDispatchDate)
                            cmd.Parameters.AddWithValue("in_mode_of_dispatch", lsModeOfDispatch)
                            cmd.Parameters.AddWithValue("in_wheather_rud", lsWheatherRUD)
                            cmd.Parameters.AddWithValue("in_response_slno", lsResponseSlno)
                            cmd.Parameters.AddWithValue("in_login_user", gsLoginUserName)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function LetterResponse(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(6) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String
        Dim lsFolioNo As String
        Dim lsHolderName As String
        Dim lsResponseType As String
        Dim lsReceivedDate As String
        Dim lsResponseSlno As String


        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "UNIT NAME"
        lsFldName(2) = "FOLIO NO"
        lsFldName(3) = "NAME"
        lsFldName(4) = "TYPE"
        lsFldName(5) = "RESPONSE RECEIVED ON"
        lsFldName(6) = "RESPONSE SL NO"


        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 6
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 6
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next



            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileLetterResponse)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("UNIT NAME").ToString), 1, 64)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsHolderName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 250)
                        lsResponseType = Mid(QuoteFilter(.Item("TYPE").ToString), 1, 32)
                        lsReceivedDate = QuoteFilter(.Item("RESPONSE RECEIVED ON").ToString)
                        lsResponseSlno = Mid(QuoteFilter(.Item("RESPONSE SL NO").ToString), 1, 32)


                        If IsDate(lsReceivedDate) = False Then
                            lsReceivedDate = ""
                        Else
                            lsReceivedDate = Format(CDate(lsReceivedDate), "yyyy-MM-dd")
                        End If


                        Using cmd As New MySqlCommand("pr_sta_ins_tletter_response", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_holder_name", lsHolderName)
                            cmd.Parameters.AddWithValue("in_response_type", lsResponseType)
                            cmd.Parameters.AddWithValue("in_response_received_date", lsReceivedDate)
                            cmd.Parameters.AddWithValue("in_response_slno", lsResponseSlno)
                            cmd.Parameters.AddWithValue("in_login_user", gsLoginUserName)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function AMLList(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(2) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsAMLName As String
        Dim lsAMLStatus As String



        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "NAME"
        lsFldName(2) = "STATUS"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 2
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 2
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next



            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnAMLList)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsAMLName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 255)
                        lsAMLStatus = Mid(QuoteFilter(.Item("STATUS").ToString), 1, 124)


                        Using cmd As New MySqlCommand("pr_sta_ins_tamllist", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_aml_name", lsAMLName)
                            cmd.Parameters.AddWithValue("?In_aml_status", lsAMLStatus)
                            cmd.Parameters.AddWithValue("In_login_user", gsLoginUserName)
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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function InterDepository(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(24) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String
        Dim lsInterDepositDate As String
        Dim lsNsdlDebit As Long
        Dim lsNsdlCredit As Long
        Dim lsNsdlRemat As Long
        Dim lsNsdlConfirmation As Long
        Dim lsNsdlTotal As Long
        Dim lsNsdlPercentage As Double
        Dim lsNsdlCAAdition As Long
        Dim lsNSdlCASubTran As Long
        Dim lsCdslDebit As Long
        Dim lsCdslCredit As Long
        Dim lsCdslRemat As Long
        Dim lsCdslConfirmation As Long
        Dim lsCdslTotal As Long
        Dim lsCdslPercentage As Double
        Dim lsCdslCAAdition As Long
        Dim lsCdslCASubTran As Long
        Dim lsPhysicalDebit As Long
        Dim lsPhysicalCredit As Long
        Dim lsPhysicalTotal As Long
        Dim lsPhysicalPercentage As Double
        Dim lsShareCaptial As Long
        Dim lsDematPercentage As Double


        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "COMPANY CODE"
        lsFldName(2) = "DATE"
        lsFldName(3) = "NSDL DEBIT"
        lsFldName(4) = "NSDL CREDIT"
        lsFldName(5) = "NSDL REMAT"
        lsFldName(6) = "NSDL DEMAT"
        lsFldName(7) = "NSDL CA ADDITION"
        lsFldName(8) = "NSDL CA SUB_TRANS"
        lsFldName(9) = "NSDL TOTAL"
        lsFldName(10) = "NSDL PERCENTAGE"
        lsFldName(11) = "CDSL DEBIT"
        lsFldName(12) = "CDSL CREDIT"
        lsFldName(13) = "CDSL REMAT"
        lsFldName(14) = "CDSL DEMAT"
        lsFldName(15) = "CDSL CA ADDITION"
        lsFldName(16) = "CDSL CA SUB_TRANS"
        lsFldName(17) = "CDSL TOTAL"
        lsFldName(18) = "CDSL PERCENTAGE"
        lsFldName(19) = "PHYSICAL REMAT"
        lsFldName(20) = "PHYSICAL DEMAT"
        lsFldName(21) = "PHYSICAL TOTAL"
        lsFldName(22) = "PHYSICAL PERCENTAGE"
        lsFldName(23) = "SHARE CAPITAL"
        lsFldName(24) = "DEMAT PERCENTAGE"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 24
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 24
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileInterDepository)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 64)
                        lsInterDepositDate = QuoteFilter(.Item("Date").ToString)
                        lsNsdlDebit = Val(QuoteFilter(.Item("NSDL DEBIT").ToString))
                        lsNsdlCredit = Val(QuoteFilter(.Item("NSDL CREDIT").ToString))
                        lsNsdlRemat = Val(QuoteFilter(.Item("NSDL REMAT").ToString))
                        lsNsdlConfirmation = Val(QuoteFilter(.Item("NSDL DEMAT").ToString))
                        lsNsdlCAAdition = Val(QuoteFilter(.Item("NSDL CA ADDITION").ToString))
                        lsNSdlCASubTran = Val(QuoteFilter(.Item("NSDL CA SUB_TRANS").ToString))
                        lsNsdlTotal = Val(QuoteFilter(.Item("NSDL TOTAL").ToString))
                        lsNsdlPercentage = Val(QuoteFilter(.Item("NSDL PERCENTAGE").ToString))
                        lsCdslDebit = Val(QuoteFilter(.Item("CDSL DEBIT").ToString))
                        lsCdslCredit = Val(QuoteFilter(.Item("CDSL CREDIT").ToString))
                        lsCdslRemat = Val(QuoteFilter(.Item("CDSL REMAT").ToString))
                        lsCdslConfirmation = Val(QuoteFilter(.Item("CDSL DEMAT").ToString))
                        lsCdslCAAdition = Val(QuoteFilter(.Item("CDSL CA ADDITION").ToString))
                        lsCdslCASubTran = Val(QuoteFilter(.Item("CDSL CA SUB_TRANS").ToString))
                        lsCdslTotal = Val(QuoteFilter(.Item("CDSL TOTAL").ToString))
                        lsCdslPercentage = Val(QuoteFilter(.Item("CDSL PERCENTAGE").ToString))
                        lsPhysicalDebit = Val(QuoteFilter(.Item("PHYSICAL REMAT").ToString))
                        lsPhysicalCredit = Val(QuoteFilter(.Item("PHYSICAL DEMAT").ToString))
                        lsPhysicalTotal = Val(QuoteFilter(.Item("PHYSICAL TOTAL").ToString))
                        lsPhysicalPercentage = Val(QuoteFilter(.Item("PHYSICAL PERCENTAGE").ToString))
                        lsShareCaptial = Val(QuoteFilter(.Item("SHARE CAPITAL").ToString))
                        lsDematPercentage = Val(QuoteFilter(.Item("DEMAT PERCENTAGE").ToString))


                        If IsDate(lsInterDepositDate) = False Then
                            lsInterDepositDate = "0001-01-01"
                        Else
                            lsInterDepositDate = Format(CDate(lsInterDepositDate), "yyyy-MM-dd")
                        End If


                        Using cmd As New MySqlCommand("pr_sta_ins_interdepository", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?in_depo_date", lsInterDepositDate)
                            cmd.Parameters.AddWithValue("?in_debit_nsdl", lsNsdlDebit)
                            cmd.Parameters.AddWithValue("in_credit_nsdl", lsNsdlCredit)
                            cmd.Parameters.AddWithValue("in_remat_nsdl", lsNsdlRemat)
                            cmd.Parameters.AddWithValue("in_confirmation_nsdl", lsNsdlConfirmation)
                            cmd.Parameters.AddWithValue("in_nsdl_ca_addition", lsNsdlCAAdition)
                            cmd.Parameters.AddWithValue("in_nsdl_ca_sub_tran", lsNSdlCASubTran)
                            cmd.Parameters.AddWithValue("in_nsdl_total", lsNsdlTotal)
                            cmd.Parameters.AddWithValue("in_nsdl_per", lsNsdlPercentage)
                            cmd.Parameters.AddWithValue("in_debit_cdsl", lsCdslDebit)
                            cmd.Parameters.AddWithValue("in_credit_cdsl", lsCdslCredit)
                            cmd.Parameters.AddWithValue("in_remat_cdsl", lsCdslRemat)
                            cmd.Parameters.AddWithValue("in_confirmation_cdsl", lsCdslConfirmation)
                            cmd.Parameters.AddWithValue("in_cdsl_ca_addition", lsCdslCAAdition)
                            cmd.Parameters.AddWithValue("in_cdsl_ca_sub_tran", lsCdslCASubTran)
                            cmd.Parameters.AddWithValue("?in_cdsl_total", lsCdslTotal)
                            cmd.Parameters.AddWithValue("?in_cdsl_per", lsCdslPercentage)
                            cmd.Parameters.AddWithValue("?in_debit_phy", lsPhysicalDebit)
                            cmd.Parameters.AddWithValue("?in_credit_phy", lsPhysicalCredit)
                            cmd.Parameters.AddWithValue("?in_physical_total", lsPhysicalTotal)
                            cmd.Parameters.AddWithValue("?in_physical_per", lsPhysicalPercentage)
                            cmd.Parameters.AddWithValue("?in_share_captial", lsShareCaptial)
                            cmd.Parameters.AddWithValue("?in_demat_per", lsDematPercentage)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function DividendMaster(ByVal FileName As String, ByVal SheetName As String, Optional ByVal ShowFlag As Boolean = True, Optional ByVal LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(42) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String
        Dim lsFinyearcode As String
        Dim lsDepository As String
        Dim lsBenpostDate As String
        Dim lsFolioNo As String
        Dim lsFolioName As String
        Dim lsShareCount As Long
        Dim lsDivRate As Double
        Dim lsPaymentType As String
        Dim lsPaymentDate As String
        Dim lsAmount As Double
        Dim lsWarrantNo As String
        Dim lsChqNo As String
        Dim lsJoint1 As String
        Dim lsJoint2 As String
        Dim lsHolder1PanNo As String
        Dim lsHolder1Emailid As String
        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsContactNo As String
        Dim lsBankName As String
        Dim lsBankAddr1 As String
        Dim lsBankAddr2 As String
        Dim lsBankAddr3 As String
        Dim lsBankCity As String
        Dim lsBankState As String
        Dim lsBankPincode As String
        Dim lsBankAcno As String
        Dim lsBankAccType As String
        Dim lsBankMicrCode As String
        Dim lsBankIfscCode As String
        Dim lsCategory As String
        Dim lsSubType As String
        Dim lsRemarks As String



        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FINANCIAL YEAR"
        lsFldName(4) = "DEPOSITORY"
        lsFldName(5) = "BENPOST DATE"
        lsFldName(6) = "FOLIO"
        lsFldName(7) = "NAME"
        lsFldName(8) = "SHARE COUNT"
        lsFldName(9) = "DIV RATE"
        lsFldName(10) = "PAYMENT TYPE"
        lsFldName(11) = "PAYMENT DATE"
        lsFldName(12) = "AMOUNT"
        lsFldName(13) = "WAR NO"
        lsFldName(14) = "CHEQUE NO"
        lsFldName(15) = "STATUS (UNPAID / PAID)"
        lsFldName(16) = "PAID DATE"
        lsFldName(17) = "JOINT 1"
        lsFldName(18) = "JOINT 2"
        lsFldName(19) = "HOLDER1 PAN"
        lsFldName(20) = "HOLDER1 EMAIL ID"
        lsFldName(21) = "ADDR1"
        lsFldName(22) = "ADDR2"
        lsFldName(23) = "ADDR3"
        lsFldName(24) = "CITY"
        lsFldName(25) = "STATE"
        lsFldName(26) = "COUNTRY"
        lsFldName(27) = "PINCODE"
        lsFldName(28) = "CONTACT NO"
        lsFldName(29) = "BANK NAME"
        lsFldName(30) = "BANK ADDR1"
        lsFldName(31) = "BANK ADDR2"
        lsFldName(32) = "BANK ADDR3"
        lsFldName(33) = "BANK CITY"
        lsFldName(34) = "BANK STATE"
        lsFldName(35) = "BANK PINCODE"
        lsFldName(36) = "BANK A/C NO"
        lsFldName(37) = "BANK A/C TYPE"
        lsFldName(38) = "BANK MICR CODE"
        lsFldName(39) = "BANK IFSC CODE"
        lsFldName(40) = "CATEGORY"
        lsFldName(41) = "SUB TYPE"
        lsFldName(42) = "REMARKS"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 42
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 42
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnDividendMaster)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFinyearcode = Mid(QuoteFilter(.Item("FINANCIAL YEAR").ToString), 1, 16)
                        lsDepository = Mid(QuoteFilter(.Item("DEPOSITORY").ToString), 1, 16)
                        lsBenpostDate = QuoteFilter(.Item("BENPOST DATE").ToString)
                        If IsDate(lsBenpostDate) = False Then
                            lsBenpostDate = "0001-01-01"
                        Else
                            lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd")
                        End If
                        lsFolioNo = Mid(QuoteFilter(.Item("Folio").ToString), 1, 64)
                        lsFolioName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 128)
                        lsShareCount = Val(QuoteFilter(.Item("SHARE COUNT").ToString))
                        lsDivRate = Val(QuoteFilter(.Item("DIV RATE").ToString))
                        lsPaymentType = Mid(QuoteFilter(.Item("PAYMENT TYPE").ToString), 1, 128)
                        lsPaymentDate = QuoteFilter(.Item("PAYMENT DATE").ToString)
                        If IsDate(lsPaymentDate) = False Then
                            lsPaymentDate = "0001-01-01"
                        Else
                            lsPaymentDate = Format(CDate(lsPaymentDate), "yyyy-MM-dd")
                        End If

                        lsAmount = Val(QuoteFilter(.Item("AMOUNT").ToString))
                        lsWarrantNo = Mid(QuoteFilter(.Item("WAR NO").ToString), 1, 128)
                        lsChqNo = Mid(QuoteFilter(.Item("CHEQUE NO").ToString), 1, 128)
                        lsJoint1 = Mid(QuoteFilter(.Item("JOINT 1").ToString), 1, 128)
                        lsJoint2 = Mid(QuoteFilter(.Item("JOINT 2").ToString), 1, 128)
                        lsHolder1PanNo = Mid(QuoteFilter(.Item("HOLDER1 PAN").ToString), 1, 32)
                        lsHolder1Emailid = Mid(QuoteFilter(.Item("HOLDER1 EMAIL ID").ToString), 1, 32)
                        lsAddr1 = Mid(QuoteFilter(.Item("ADDR1").ToString), 1, 128)
                        lsAddr2 = Mid(QuoteFilter(.Item("ADDR2").ToString), 1, 128)
                        lsAddr3 = Mid(QuoteFilter(.Item("ADDR3").ToString), 1, 128)
                        lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 64)
                        lsState = Mid(QuoteFilter(.Item("STATE").ToString), 1, 64)
                        lsCountry = Mid(QuoteFilter(.Item("COUNTRY").ToString), 1, 64)
                        lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 16)
                        lsContactNo = Mid(QuoteFilter(.Item("CONTACT NO").ToString), 1, 64)
                        lsBankName = Mid(QuoteFilter(.Item("BANK NAME").ToString), 1, 128)
                        lsBankAddr1 = Mid(QuoteFilter(.Item("BANK ADDR1").ToString), 1, 128)
                        lsBankAddr2 = Mid(QuoteFilter(.Item("BANK ADDR2").ToString), 1, 128)
                        lsBankAddr3 = Mid(QuoteFilter(.Item("BANK ADDR3").ToString), 1, 128)
                        lsBankCity = Mid(QuoteFilter(.Item("BANK CITY").ToString), 1, 64)
                        lsBankState = Mid(QuoteFilter(.Item("BANK STATE").ToString), 1, 64)
                        lsBankPincode = Mid(QuoteFilter(.Item("BANK PINCODE").ToString), 1, 64)
                        lsBankAcno = Mid(QuoteFilter(.Item("BANK A/C NO").ToString), 1, 64)
                        lsBankAccType = Mid(QuoteFilter(.Item("BANK A/C TYPE").ToString), 1, 64)
                        lsBankMicrCode = Mid(QuoteFilter(.Item("BANK MICR CODE").ToString), 1, 64)
                        lsBankIfscCode = Mid(QuoteFilter(.Item("BANK IFSC CODE").ToString), 1, 64)
                        lsCategory = Mid(QuoteFilter(.Item("CATEGORY").ToString), 1, 32)
                        lsSubType = Mid(QuoteFilter(.Item("SUB TYPE").ToString), 1, 64)
                        lsRemarks = QuoteFilter(.Item("REMARKS").ToString)


                        Using cmd As New MySqlCommand("pr_sta_ins_tdividendmaster", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?In_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?In_finyear_code", lsFinyearcode)
                            cmd.Parameters.AddWithValue("?In_depository", lsDepository)
                            cmd.Parameters.AddWithValue("?In_benpost_date", lsBenpostDate)
                            cmd.Parameters.AddWithValue("?In_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?In_folio_name", lsFolioName)
                            cmd.Parameters.AddWithValue("?In_share_count", lsShareCount)
                            cmd.Parameters.AddWithValue("?In_div_rate", lsDivRate)
                            cmd.Parameters.AddWithValue("?In_payment_type", lsPaymentType)
                            cmd.Parameters.AddWithValue("?In_payment_date", lsPaymentDate)
                            cmd.Parameters.AddWithValue("?In_amount", lsAmount)
                            cmd.Parameters.AddWithValue("?In_warrant_no", lsWarrantNo)
                            cmd.Parameters.AddWithValue("?In_cheque_no", lsChqNo)
                            cmd.Parameters.AddWithValue("?In_joint1", lsJoint1)
                            cmd.Parameters.AddWithValue("?In_joint2", lsJoint2)
                            cmd.Parameters.AddWithValue("?In_holder1_pan_no", lsHolder1PanNo)
                            cmd.Parameters.AddWithValue("?In_holder1_emailid", lsHolder1Emailid)
                            cmd.Parameters.AddWithValue("?In_addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("?In_addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("?In_addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("?In_city", lsCity)
                            cmd.Parameters.AddWithValue("?In_state", lsState)
                            cmd.Parameters.AddWithValue("?In_country", lsCountry)
                            cmd.Parameters.AddWithValue("?In_pincode", lsPincode)
                            cmd.Parameters.AddWithValue("?In_contact_no", lsContactNo)
                            cmd.Parameters.AddWithValue("?In_bank_name", lsBankName)
                            cmd.Parameters.AddWithValue("?In_bank_addr1", lsBankAddr1)
                            cmd.Parameters.AddWithValue("?In_bank_addr2", lsBankAddr2)
                            cmd.Parameters.AddWithValue("?In_bank_addr3", lsBankAddr3)
                            cmd.Parameters.AddWithValue("?In_bank_city", lsBankCity)
                            cmd.Parameters.AddWithValue("?In_bank_state", lsBankState)
                            cmd.Parameters.AddWithValue("?In_bank_pincode", lsBankPincode)
                            cmd.Parameters.AddWithValue("?In_bank_acno", lsBankAcno)
                            cmd.Parameters.AddWithValue("?In_bank_actype", lsBankAccType)
                            cmd.Parameters.AddWithValue("?In_bank_micrcode", lsBankMicrCode)
                            cmd.Parameters.AddWithValue("?In_bank_ifsccode", lsBankIfscCode)
                            cmd.Parameters.AddWithValue("?In_category", lsCategory)
                            cmd.Parameters.AddWithValue("?In_sub_type", lsSubType)
                            cmd.Parameters.AddWithValue("?In_remarks", lsRemarks)
                            cmd.Parameters.AddWithValue("?In_loginuser", gsLoginUserCode)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function DividendStatus(ByVal FileName As String, ByVal SheetName As String, Optional ByVal ShowFlag As Boolean = True, Optional ByVal LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(9) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompcode As String
        Dim lsFinyearcode As String
        Dim lsDepository As String

        Dim lsFolioNo As String
        Dim lsFolioName As String
        Dim lsStatus As String
        Dim lsPaidDate As String
        Dim lsRemarks As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FINANCIAL YEAR"
        lsFldName(4) = "DEPOSITORY"
        lsFldName(5) = "FOLIO"
        lsFldName(6) = "NAME"
        lsFldName(7) = "STATUS (UNPAID / PAID)"
        lsFldName(8) = "PAID DATE"
        lsFldName(9) = "REMARKS"


        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 9
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 9
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnDividendStatus)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompcode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFinyearcode = Mid(QuoteFilter(.Item("FINANCIAL YEAR").ToString), 1, 16)
                        lsDepository = Mid(QuoteFilter(.Item("DEPOSITORY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("Folio").ToString), 1, 64)
                        lsFolioName = Mid(QuoteFilter(.Item("NAME").ToString), 1, 128)
                        lsStatus = Mid(QuoteFilter(.Item("STATUS (UNPAID / PAID)").ToString), 1, 64)
                        lsPaidDate = QuoteFilter(.Item("PAID DATE").ToString)
                        If IsDate(lsPaidDate) = False Then
                            lsPaidDate = "0001-01-01"
                        Else
                            lsPaidDate = Format(CDate(lsPaidDate), "yyyy-MM-dd")
                        End If
                        lsRemarks = QuoteFilter(.Item("REMARKS").ToString)

                        Using cmd As New MySqlCommand("pr_sta_ins_tdividendstatus", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?In_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_comp_code", lsCompcode)
                            cmd.Parameters.AddWithValue("?In_finyear_code", lsFinyearcode)
                            cmd.Parameters.AddWithValue("?In_depository", lsDepository)
                            cmd.Parameters.AddWithValue("?In_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?In_folio_name", lsFolioName)
                            cmd.Parameters.AddWithValue("?In_status", lsStatus)
                            cmd.Parameters.AddWithValue("?In_Paid_date", lsPaidDate)
                            cmd.Parameters.AddWithValue("?In_remarks", lsRemarks)
                            cmd.Parameters.AddWithValue("?In_loginuser", gsLoginUserCode)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function CompanyShareCaptial(ByVal FileName As String, ByVal SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(4) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsSlno As String
        Dim lsCompanyName As String
        Dim lsShareCaptial As Double
        Dim lsIsinId As String



        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY NAME"
        lsFldName(3) = "SHARE CAPITAL"
        lsFldName(4) = "ISIN"


        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 4
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 4
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnCompanyShareCaptial)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()


                        lsIsinId = Mid(QuoteFilter(.Item("ISIN").ToString), 1, 16)
                        lsShareCaptial = Val(QuoteFilter(.Item("SHARE CAPITAL").ToString))



                        Using cmd As New MySqlCommand("pr_sta_ins_tcompanyshares", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_isin_id", lsIsinId)
                            cmd.Parameters.AddWithValue("?in_sharecaptial", lsShareCaptial)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function ECSValidFileImport(ByVal FileName As String, ByVal SheetName As String, ByVal CompanyGID As Integer, ByVal FinYear As Integer, ByVal InterimCode As String, Optional ByVal ShowFlag As Boolean = True, Optional ByVal LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(16) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompGid As Integer = Val(CompanyGID.ToString())
        Dim lsFinyearGid As Integer = Val(FinYear.ToString())
        Dim lsinterimCode As String = Mid(QuoteFilter(InterimCode.ToString), 1, 3)
        Dim lsFolioClientType As String
        Dim lsFolioClientId As String
        Dim lsFolioClientdpid As String
        Dim lsBeneName As String
        Dim lsDividendAmount As Double
        Dim lsCurrencyCode As String
        Dim lsCurrencyValue As Double
        Dim lsCurrencyAmount As Double
        Dim lsBankName As String
        Dim lsBankBranch As String
        Dim lsBankAccno As String
        Dim lsBankAccType As String
        Dim lsBankMicrcode As String
        Dim lsEcsValidationStatus As String
        Dim lsEcsValidationGid As String
        Dim lsEcsStatus As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "DP ID"
        lsFldName(3) = "FOLIO/CLIENT ID"
        lsFldName(4) = "FOLIO/CLIENT ID TYPE"
        lsFldName(5) = "BENEFICIARY NAME"
        lsFldName(6) = "DIVIDEND AMOUNT"
        lsFldName(7) = "CURRENCY CODE"
        lsFldName(8) = "CURRENCY VALUE"
        lsFldName(9) = "CURRENCY AMOUNT"
        lsFldName(10) = "BANK NAME"
        lsFldName(11) = "BANK BRANCH"
        lsFldName(12) = "BANK A/C NO"
        lsFldName(13) = "BANK A/C TYPE"
        lsFldName(14) = "MICR CODE"
        lsFldName(15) = "ECSVALIDATION STATUS"
        lsFldName(16) = "ECSVALIDATION GID"

        Try

            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 16
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 16
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileECSValidFile)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        'lsCompGid = Val(QuoteFilter(.Item("COMPANY GID").ToString()))
                        'lsFinyearGid = Val(QuoteFilter(.Item("FINANCIAL YEAR GID").ToString()))
                        'lsinterimCode = Mid(QuoteFilter(.Item("INTERIM CODE").ToString), 1, 8)
                        lsFolioClientId = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID").ToString), 1, 32)
                        lsFolioClientType = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID TYPE").ToString), 1, 1)
                        lsFolioClientdpid = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 128)
                        lsBeneName = Mid(QuoteFilter(.Item("BENEFICIARY NAME").ToString), 1, 128)
                        lsDividendAmount = Val(QuoteFilter(.Item("DIVIDEND AMOUNT").ToString))
                        lsCurrencyCode = Mid(QuoteFilter(.Item("CURRENCY CODE").ToString), 1, 8)
                        lsCurrencyValue = Val(QuoteFilter(.Item("CURRENCY VALUE").ToString))
                        lsCurrencyAmount = Val(QuoteFilter(.Item("CURRENCY AMOUNT").ToString))
                        lsBankAccType = Mid(QuoteFilter(.Item("BANK A/C TYPE").ToString), 1, 128)
                        lsBankName = Mid(QuoteFilter(.Item("BANK NAME").ToString), 1, 128)
                        lsBankBranch = Mid(QuoteFilter(.Item("BANK BRANCH").ToString), 1, 128)
                        lsBankAccno = Mid(QuoteFilter(.Item("BANK A/C NO").ToString), 1, 32)
                        lsBankMicrcode = Mid(QuoteFilter(.Item("MICR CODE").ToString), 1, 16)
                        lsEcsValidationStatus = Val(QuoteFilter(.Item("ECSVALIDATION STATUS").ToString))
                        lsEcsValidationGid = Val(QuoteFilter(.Item("ECSVALIDATION GID").ToString))
                        lsEcsStatus = "Ecs Valid"

                        Using cmd As New MySqlCommand("pr_sta_ins_tecsvalidationstatus", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_gid", lsCompGid)
                            cmd.Parameters.AddWithValue("?in_finyear_gid", lsFinyearGid)
                            cmd.Parameters.AddWithValue("?in_interim_code", lsinterimCode)
                            cmd.Parameters.AddWithValue("in_folioclient_id_type", lsFolioClientType)
                            cmd.Parameters.AddWithValue("in_folioclient_id", lsFolioClientId)
                            cmd.Parameters.AddWithValue("in_folioclient_dp_id", lsFolioClientdpid)
                            cmd.Parameters.AddWithValue("in_bene_name", lsBeneName)
                            cmd.Parameters.AddWithValue("in_dividend_amount", lsDividendAmount)
                            cmd.Parameters.AddWithValue("in_currency_code", lsCurrencyCode)
                            cmd.Parameters.AddWithValue("in_currency_value", lsCurrencyValue)
                            cmd.Parameters.AddWithValue("in_currency_amount", lsCurrencyAmount)
                            cmd.Parameters.AddWithValue("in_bank_acc_type", lsBankAccType)
                            cmd.Parameters.AddWithValue("in_bank_name", lsBankName)
                            cmd.Parameters.AddWithValue("in_bank_branch", lsBankBranch)
                            cmd.Parameters.AddWithValue("in_bank_acc_no", lsBankAccno)
                            cmd.Parameters.AddWithValue("in_bank_micr_code", lsBankMicrcode)
                            cmd.Parameters.AddWithValue("in_ecsvalidation_status", lsEcsValidationStatus)
                            cmd.Parameters.AddWithValue("in_ecsvalidation_gid", lsEcsValidationGid)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)
                            cmd.Parameters.AddWithValue("?in_status_type", lsEcsStatus)
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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try

    End Function

    Public Function ECSRejectFileImport(FileName As String, SheetName As String, CompanyGID As Integer, FinYear As Integer, InterimCode As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(16) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable


        Dim lsCompGid As Integer = Val(CompanyGID.ToString())
        Dim lsFinyearGid As Integer = Val(FinYear.ToString())
        Dim lsinterimCode As String = Mid(QuoteFilter(InterimCode.ToString), 1, 3)
        Dim lsFolioClientType As String
        Dim lsFolioClientId As String
        Dim lsFolioClientdpid As String
        Dim lsBeneName As String
        Dim lsDividendAmount As Double
        Dim lsCurrencyCode As String
        Dim lsCurrencyValue As Double
        Dim lsCurrencyAmount As Double
        Dim lsBankName As String
        Dim lsBankBranch As String
        Dim lsBankAccno As String
        Dim lsBankAccType As String
        Dim lsBankMicrcode As String
        Dim lsEcsValidationStatus As String
        Dim lsEcsValidationGid As String
        Dim lsEcsStatus As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "DP ID"
        lsFldName(3) = "FOLIO/CLIENT ID"
        lsFldName(4) = "FOLIO/CLIENT ID TYPE"
        lsFldName(5) = "BENEFICIARY NAME"
        lsFldName(6) = "DIVIDEND AMOUNT"
        lsFldName(7) = "CURRENCY CODE"
        lsFldName(8) = "CURRENCY VALUE"
        lsFldName(9) = "CURRENCY AMOUNT"
        lsFldName(10) = "BANK NAME"
        lsFldName(11) = "BANK BRANCH"
        lsFldName(12) = "BANK A/C NO"
        lsFldName(13) = "BANK A/C TYPE"
        lsFldName(14) = "MICR CODE"
        lsFldName(15) = "ECSVALIDATION STATUS"
        lsFldName(16) = "ECSVALIDATION GID"

        Try

            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 16
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 16
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileECSRejectFile)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        'lsCompGid = Val(QuoteFilter(.Item("COMPANY GID").ToString()))
                        'lsFinyearGid = Val(QuoteFilter(.Item("FINANCIAL YEAR GID").ToString()))
                        'lsinterimCode = Mid(QuoteFilter(.Item("INTERIM CODE").ToString), 1, 8)
                        lsFolioClientId = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID").ToString), 1, 32)
                        lsFolioClientType = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID TYPE").ToString), 1, 1)
                        lsFolioClientdpid = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 128)
                        lsBeneName = Mid(QuoteFilter(.Item("BENEFICIARY NAME").ToString), 1, 128)
                        lsDividendAmount = Val(QuoteFilter(.Item("DIVIDEND AMOUNT").ToString))
                        lsCurrencyCode = Mid(QuoteFilter(.Item("CURRENCY CODE").ToString), 1, 8)
                        lsCurrencyValue = Val(QuoteFilter(.Item("CURRENCY VALUE").ToString))
                        lsCurrencyAmount = Val(QuoteFilter(.Item("CURRENCY AMOUNT").ToString))
                        lsBankAccType = Mid(QuoteFilter(.Item("BANK A/C TYPE").ToString), 1, 128)
                        lsBankName = Mid(QuoteFilter(.Item("BANK NAME").ToString), 1, 128)
                        lsBankBranch = Mid(QuoteFilter(.Item("BANK BRANCH").ToString), 1, 128)
                        lsBankAccno = Mid(QuoteFilter(.Item("BANK A/C NO").ToString), 1, 32)
                        lsBankMicrcode = Mid(QuoteFilter(.Item("MICR CODE").ToString), 1, 16)
                        lsEcsValidationStatus = Val(QuoteFilter(.Item("ECSVALIDATION STATUS").ToString))
                        lsEcsValidationGid = Val(QuoteFilter(.Item("ECSVALIDATION GID").ToString))
                        lsEcsStatus = "Ecs Invalid"

                        Using cmd As New MySqlCommand("pr_sta_ins_tecsvalidationstatus", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_gid", lsCompGid)
                            cmd.Parameters.AddWithValue("?in_finyear_gid", lsFinyearGid)
                            cmd.Parameters.AddWithValue("?in_interim_code", lsinterimCode)
                            cmd.Parameters.AddWithValue("in_folioclient_id_type", lsFolioClientType)
                            cmd.Parameters.AddWithValue("in_folioclient_id", lsFolioClientId)
                            cmd.Parameters.AddWithValue("in_folioclient_dp_id", lsFolioClientdpid)
                            cmd.Parameters.AddWithValue("in_bene_name", lsBeneName)
                            cmd.Parameters.AddWithValue("in_dividend_amount", lsDividendAmount)
                            cmd.Parameters.AddWithValue("in_currency_code", lsCurrencyCode)
                            cmd.Parameters.AddWithValue("in_currency_value", lsCurrencyValue)
                            cmd.Parameters.AddWithValue("in_currency_amount", lsCurrencyAmount)
                            cmd.Parameters.AddWithValue("in_bank_acc_type", lsBankAccType)
                            cmd.Parameters.AddWithValue("in_bank_name", lsBankName)
                            cmd.Parameters.AddWithValue("in_bank_branch", lsBankBranch)
                            cmd.Parameters.AddWithValue("in_bank_acc_no", lsBankAccno)
                            cmd.Parameters.AddWithValue("in_bank_micr_code", lsBankMicrcode)
                            cmd.Parameters.AddWithValue("in_ecsvalidation_status", lsEcsValidationStatus)
                            cmd.Parameters.AddWithValue("in_ecsvalidation_gid", lsEcsValidationGid)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)
                            cmd.Parameters.AddWithValue("?in_status_type", lsEcsStatus)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try

    End Function

    Public Function DDNOUpdation(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(11) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable
        Dim lsCompCode As String
        Dim lsFinyear As String
        Dim lsInterimcode As String
        'Dim lsPaymentno As String
        Dim lsPaymentmode As String
        'Dim lsPaymentrefno A s String
        Dim lsDDNo As String
        Dim lsWarrantNo As String
        Dim lsDollarNo As String
        Dim lsupdateflag As String
        Dim lsPaymentgid As Long
        Dim lscurrencyvalue As Double
        Dim lscurrencyamount As Double

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "INTERIM CODE"
        lsFldName(4) = "FINANCIAL YEAR"
        lsFldName(5) = "PAYMENT TYPE"
        'lsFldName(6) = "PAYMENT NO"
        'lsFldName(7) = "PAYMENTREF NO"
        lsFldName(6) = "CHEQUE NO"
        lsFldName(7) = "DD NO"
        lsFldName(8) = "DOLLAR NO"
        lsFldName(9) = "CURRENCY VALUE"
        lsFldName(10) = "CURRENCY AMOUNT"
        lsFldName(11) = "PAYMENT GID"
        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 11
                lsFldFormat &= lsFldName(i) & "|"
            Next


            For i = 1 To 11
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next
            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileDDNoUpdate)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsFinyear = Mid(QuoteFilter(.Item("FINANCIAL YEAR").ToString), 1, 16)
                        lsInterimcode = Mid(QuoteFilter(.Item("INTERIM CODE").ToString), 1, 8)
                        lsPaymentmode = Mid(QuoteFilter(.Item("PAYMENT TYPE").ToString), 1, 128)
                        lsDDNo = Mid(QuoteFilter(.Item("DD NO").ToString), 1, 128)
                        lsDollarNo = Mid(QuoteFilter(.Item("Dollar No").ToString), 1, 128)
                        lsWarrantNo = Mid(QuoteFilter(.Item("Cheque No").ToString), 1, 128)
                        lscurrencyvalue = Val(QuoteFilter(.Item("CURRENCY VALUE").ToString))
                        lscurrencyamount = Val(QuoteFilter(.Item("CURRENCY AMOUNT").ToString))

                        lsPaymentgid = Val(QuoteFilter(.Item("Payment Gid").ToString))

                        lsupdateflag = "U"



                        Using cmd As New MySqlCommand("pr_sta_ins_tpaymentrefno", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_finyear_code", lsFinyear)
                            cmd.Parameters.AddWithValue("?in_interim_code", lsInterimcode)
                            cmd.Parameters.AddWithValue("?in_payment_mode", lsPaymentmode)
                            cmd.Parameters.AddWithValue("?in_Cheque_No", lsWarrantNo)
                            cmd.Parameters.AddWithValue("?in_DD_No", lsDDNo)
                            cmd.Parameters.AddWithValue("?in_Dollar_No", lsDollarNo)
                            cmd.Parameters.AddWithValue("?in_update_flag", lsupdateflag)
                            cmd.Parameters.AddWithValue("?in_payment_gid", lsPaymentgid)
                            cmd.Parameters.AddWithValue("?in_currency_value", lscurrencyvalue)
                            cmd.Parameters.AddWithValue("?in_currency_amount", lscurrencyamount)
                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try

    End Function

    Public Function Dividendreport(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim lsFldName(30) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFinyear As String
        Dim lsInterimcode As String
        Dim lsFolioclientid As String
        Dim lsFolioclientidtype As String
        Dim lsHoldername As String
        Dim lssharecount As Long
        Dim lsDividendpershare As Double
        Dim lsDividendamount As Double
        Dim lsCurrencycode As String
        Dim lsCurrencyvalue As Double
        Dim lsCurrencyamount As Double
        Dim lsPaymentmode As String
        Dim lsBankName As String
        Dim lsBankBranch As String
        Dim lsBankACNo As String
        Dim lsBankACType As String
        Dim lsMicrcode As String
        Dim lsIFSCCode As String
        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsAddr4 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsDpid As String
        Dim lsEmail As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "FINANCIAL YEAR CODE"
        lsFldName(4) = "INTERIM CODE"
        lsFldName(5) = "DP ID"
        lsFldName(6) = "FOLIO/CLIENT ID"
        lsFldName(7) = "FOLIO/CLIENT ID TYPE"
        lsFldName(8) = "HOLDER NAME"
        lsFldName(9) = "SHARE COUNT"
        lsFldName(10) = "DIVIDEND PER SHARE"
        lsFldName(11) = "DIVIDEND AMOUNT"
        lsFldName(12) = "CURRENCY CODE"
        lsFldName(13) = "CURRENCY VALUE"
        lsFldName(14) = "CURRENCY AMOUNT"
        lsFldName(15) = "PAYMENT MODE"
        lsFldName(16) = "BANK NAME"
        lsFldName(17) = "BANK BRANCH"
        lsFldName(18) = "BANK A/C NO"
        lsFldName(19) = "BANK A/C TYPE"
        lsFldName(20) = "MICR CODE"
        lsFldName(21) = "IFSC CODE"
        lsFldName(22) = "ADDR1"
        lsFldName(23) = "ADDR2"
        lsFldName(24) = "ADDR3"
        lsFldName(25) = "ADDR4"
        lsFldName(26) = "CITY"
        lsFldName(27) = "STATE"
        lsFldName(28) = "COUNTRY"
        lsFldName(29) = "PINCODE"
        lsFldName(30) = "EMAIL"

        Try
            Call FormatSheet(FileName, SheetName)

            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 30
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 30
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next



            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileDividendfile)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsFinyear = Mid(QuoteFilter(.Item("FINANCIAL YEAR CODE").ToString), 1, 16)
                        lsInterimcode = Mid(QuoteFilter(.Item("INTERIM CODE").ToString), 1, 8)
                        lsFolioclientid = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID").ToString), 1, 32)
                        lsFolioclientidtype = Mid(QuoteFilter(.Item("FOLIO/CLIENT ID TYPE").ToString), 1, 1)
                        lsHoldername = Mid(QuoteFilter(.Item("HOLDER NAME").ToString), 1, 128)
                        lssharecount = Val(QuoteFilter(.Item("SHARE COUNT").ToString()))
                        lsDividendpershare = Val(QuoteFilter(.Item("DIVIDEND PER SHARE").ToString))
                        lsDividendamount = Val(QuoteFilter(.Item("DIVIDEND AMOUNT").ToString))
                        lsCurrencycode = Mid(QuoteFilter(.Item("CURRENCY CODE").ToString), 1, 8)
                        lsCurrencyvalue = Val(QuoteFilter(.Item("CURRENCY VALUE").ToString))
                        lsCurrencyamount = Val(QuoteFilter(.Item("CURRENCY AMOUNT").ToString))
                        lsPaymentmode = Mid(QuoteFilter(.Item("PAYMENT MODE").ToString), 1, 128)
                        lsBankName = Mid(QuoteFilter(.Item("BANK NAME").ToString), 1, 128)
                        lsBankBranch = Mid(QuoteFilter(.Item("BANK BRANCH").ToString), 1, 128)
                        lsBankACNo = Mid(QuoteFilter(.Item("BANK A/C NO").ToString), 1, 32)
                        lsBankACType = Mid(QuoteFilter(.Item("BANK A/C TYPE").ToString), 1, 16)
                        lsMicrcode = Mid(QuoteFilter(.Item("MICR CODE").ToString), 1, 16)
                        lsIFSCCode = Mid(QuoteFilter(.Item("IFSC CODE").ToString), 1, 32)
                        lsAddr1 = Mid(QuoteFilter(.Item("ADDR1").ToString), 1, 128)
                        lsAddr2 = Mid(QuoteFilter(.Item("ADDR2").ToString), 1, 128)
                        lsAddr3 = Mid(QuoteFilter(.Item("ADDR3").ToString), 1, 128)
                        lsAddr4 = Mid(QuoteFilter(.Item("ADDR4").ToString), 1, 128)
                        lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 64)
                        lsState = Mid(QuoteFilter(.Item("STATE").ToString), 1, 64)
                        lsCountry = Mid(QuoteFilter(.Item("COUNTRY").ToString), 1, 64)
                        lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 16)
                        lsDpid = Mid(QuoteFilter(.Item("DP ID").ToString), 1, 16)
                        lsEmail = Mid(QuoteFilter(.Item("EMAIL").ToString), 1, 128)




                        Using cmd As New MySqlCommand("pr_sta_ins_dividend", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_finyear_code", lsFinyear)
                            cmd.Parameters.AddWithValue("?in_interim_code", lsInterimcode)
                            cmd.Parameters.AddWithValue("in_folioclient_id", lsFolioclientid)
                            cmd.Parameters.AddWithValue("in_folioclient_id_type", lsFolioclientidtype)
                            cmd.Parameters.AddWithValue("in_holder1_name", lsHoldername)
                            cmd.Parameters.AddWithValue("in_share_count", lssharecount)
                            cmd.Parameters.AddWithValue("in_dividend_per_share", lsDividendpershare)
                            cmd.Parameters.AddWithValue("in_dividend_amount", lsDividendamount)
                            cmd.Parameters.AddWithValue("in_currency_code", lsCurrencycode)
                            cmd.Parameters.AddWithValue("in_currency_value", lsCurrencyvalue)
                            cmd.Parameters.AddWithValue("in_currency_amount", lsCurrencyamount)
                            cmd.Parameters.AddWithValue("in_payment_mode", lsPaymentmode)
                            cmd.Parameters.AddWithValue("in_bank_name", lsBankName)
                            cmd.Parameters.AddWithValue("in_bank_branch", lsBankBranch)
                            cmd.Parameters.AddWithValue("in_bank_acc_no", lsBankACNo)
                            cmd.Parameters.AddWithValue("in_bank_acc_type", lsBankACType)
                            cmd.Parameters.AddWithValue("in_bank_micr_code", lsMicrcode)
                            cmd.Parameters.AddWithValue("in_bank_ifsc_code", lsIFSCCode)
                            cmd.Parameters.AddWithValue("in_holder1_addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("in_holder1_addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("in_holder1_addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("in_holder1_addr4", lsAddr4)
                            cmd.Parameters.AddWithValue("in_holder1_city", lsCity)
                            cmd.Parameters.AddWithValue("in_holder1_state", lsState)
                            cmd.Parameters.AddWithValue("in_holder1_country", lsCountry)
                            cmd.Parameters.AddWithValue("in_holder1_pincode", lsPincode)
                            cmd.Parameters.AddWithValue("in_dp_id", lsDpid)
                            cmd.Parameters.AddWithValue("in_email", lsEmail)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try


    End Function

    Public Function FolioAnnualreport(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)

        Dim i As Integer
        Dim lsFldName(27) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFinyear As String
        Dim lsFolioSlNo As String
        Dim lsFolioNo As String
        Dim lsHolder1Name As String
        Dim lsHolder2Name As String
        Dim lsHolder3Name As String
        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsAddr4 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsEmailid1 As String
        Dim lsEmailid2 As String
        Dim lsSharecount As Long
        Dim lsDispatchmode As String
        Dim lsMemberOrProxy As String
        Dim lsProxy1name As String
        Dim lsProxy2name As String
        Dim lsproxy3name As String
        Dim lsTokenNo As String
        Dim lsReturnFlag As String
        Dim lsReturnreason As String
        Dim lsdateofagm As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SL NO"
        lsFldName(2) = "FOLIO NO"
        lsFldName(3) = "HOLDER1"
        lsFldName(4) = "HOLDER2"
        lsFldName(5) = "HOLDER3"
        lsFldName(6) = "ADDR1"
        lsFldName(7) = "ADDR2"
        lsFldName(8) = "ADDR3"
        lsFldName(9) = "ADDR4"
        lsFldName(10) = "CITY"
        lsFldName(11) = "STATE"
        lsFldName(12) = "COUNTRY"
        lsFldName(13) = "PINCODE"
        lsFldName(14) = "EMAIL ID1"
        lsFldName(15) = "EMAIL ID2"
        lsFldName(16) = "SHARE COUNT"
        lsFldName(17) = "DISPATCH MODE"
        lsFldName(18) = "MEMBER/PROXY"
        lsFldName(19) = "PROXY1 NAME"
        lsFldName(20) = "PROXY2 NAME"
        lsFldName(21) = "PROXY3 NAME"
        lsFldName(22) = "TOKEN NO"
        lsFldName(23) = "RETURNTAG"
        lsFldName(24) = "RETURN REASON"
        lsFldName(25) = "FINANCIAL YEAR"
        lsFldName(26) = "DATE OF AGM"
        lsFldName(27) = "COMPANY CODE"



        Try

            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 27
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 27
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Call FormatSheet(lsFileName, SheetName)

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileAnnualRpt)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsFinyear = Mid(QuoteFilter(.Item("FINANCIAL YEAR").ToString), 1, 16)
                        lsFolioSlNo = Mid(QuoteFilter(.Item("SL NO").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsHolder1Name = Mid(QuoteFilter(.Item("HOLDER1").ToString), 1, 128)
                        lsHolder2Name = Mid(QuoteFilter(.Item("HOLDER2").ToString), 1, 128)
                        lsHolder3Name = Mid(QuoteFilter(.Item("HOLDER3").ToString), 1, 128)
                        lsAddr1 = Mid(QuoteFilter(.Item("ADDR1").ToString), 1, 128)
                        lsAddr2 = Mid(QuoteFilter(.Item("ADDR2").ToString), 1, 128)
                        lsAddr3 = Mid(QuoteFilter(.Item("ADDR3").ToString), 1, 128)
                        lsAddr4 = Mid(QuoteFilter(.Item("ADDR4").ToString), 1, 128)
                        lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 64)
                        lsState = Mid(QuoteFilter(.Item("STATE").ToString), 1, 64)
                        lsCountry = Mid(QuoteFilter(.Item("COUNTRY").ToString), 1, 64)
                        lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 16)
                        lsEmailid1 = Mid(QuoteFilter(.Item("EMAIL ID1").ToString), 1, 128)
                        lsEmailid2 = Mid(QuoteFilter(.Item("EMAIL ID2").ToString), 1, 128)
                        lsSharecount = Val(QuoteFilter(.Item("SHARE COUNT").ToString()))
                        lsDispatchmode = Mid(QuoteFilter(.Item("DISPATCH MODE").ToString), 1, 128)
                        lsMemberOrProxy = Mid(QuoteFilter(.Item("MEMBER/PROXY").ToString), 1, 64)
                        lsProxy1name = Mid(QuoteFilter(.Item("PROXY1 NAME").ToString), 1, 128)
                        lsProxy2name = Mid(QuoteFilter(.Item("PROXY2 NAME").ToString), 1, 128)
                        lsproxy3name = Mid(QuoteFilter(.Item("PROXY3 NAME").ToString), 1, 128)
                        lsTokenNo = Mid(QuoteFilter(.Item("TOKEN NO").ToString), 1, 64)
                        lsReturnFlag = Mid(QuoteFilter(.Item("RETURNTAG").ToString), 1, 64)
                        lsReturnreason = Mid(QuoteFilter(.Item("RETURN REASON").ToString), 1, 128)
                        lsdateofagm = QuoteFilter(.Item("DATE OF AGM").ToString)

                        If IsDate(lsdateofagm) = False Then
                            lsdateofagm = "0001-01-01"
                        Else
                            lsdateofagm = Format(CDate(lsdateofagm), "yyyy-MM-dd")
                        End If


                        Using cmd As New MySqlCommand("pr_sta_ins_annualrpt", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_finyear_value", lsFinyear)
                            cmd.Parameters.AddWithValue("?in_annual_folio_slno", lsFolioSlNo)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("in_holder1_name", lsHolder1Name)
                            cmd.Parameters.AddWithValue("in_holder2_name", lsHolder2Name)
                            cmd.Parameters.AddWithValue("in_holder3_name", lsHolder3Name)
                            cmd.Parameters.AddWithValue("in_addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("in_addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("in_addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("in_addr4", lsAddr4)
                            cmd.Parameters.AddWithValue("in_city", lsCity)
                            cmd.Parameters.AddWithValue("in_state", lsState)
                            cmd.Parameters.AddWithValue("in_country", lsCountry)
                            cmd.Parameters.AddWithValue("in_pincode", lsPincode)
                            cmd.Parameters.AddWithValue("in_email_id1", lsEmailid1)
                            cmd.Parameters.AddWithValue("in_email_id2", lsEmailid2)
                            cmd.Parameters.AddWithValue("in_share_count", lsSharecount)
                            cmd.Parameters.AddWithValue("in_dispatch_mode", lsDispatchmode)
                            cmd.Parameters.AddWithValue("in_member_proxy", lsMemberOrProxy)
                            cmd.Parameters.AddWithValue("in_proxy1_name", lsProxy1name)
                            cmd.Parameters.AddWithValue("in_proxy2_name", lsProxy2name)
                            cmd.Parameters.AddWithValue("in_proxy3_name", lsproxy3name)
                            cmd.Parameters.AddWithValue("in_token_no", lsTokenNo)
                            cmd.Parameters.AddWithValue("in_return_tag", lsReturnFlag)
                            cmd.Parameters.AddWithValue("in_return_reason", lsReturnreason)
                            cmd.Parameters.AddWithValue("in_dateofagm", lsdateofagm)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try


    End Function

    Public Function Folio(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(39) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsHolder1Name As String
        Dim lsHolder1FHName As String
        Dim lsHolder1PanNo As String
        Dim lsHolder2Name As String
        Dim lsHolder2FHName As String
        Dim lsHolder2PanNo As String
        Dim lsHolder3Name As String
        Dim lsHolder3FHName As String
        Dim lsHolder3PanNo As String
        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsMailId As String
        Dim lsContactNo As String
        Dim lsNomineeRegNo As String
        Dim lsNomineeName As String
        Dim lsNomineeAddr1 As String
        Dim lsNomineeAddr2 As String
        Dim lsNomineeAddr3 As String
        Dim lsNomineeCity As String
        Dim lsNomineeState As String
        Dim lsNomineeCountry As String
        Dim lsNomineePincode As String
        Dim lsBankName As String
        Dim lsBankBranchName As String
        Dim lsBankBranchAddr As String
        Dim lsBankAccNo As String
        Dim lsBankIFSCCode As String
        Dim lsBankAccType As String
        Dim lsBankBeneficiary As String
        Dim lsRepatritionFlag As String
        Dim lsCategory As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "HOLDER1 NAME"
        lsFldName(5) = "HOLDER1 FH NAME"
        lsFldName(6) = "HOLDER1 PAN NO"
        lsFldName(7) = "HOLDER2 NAME"
        lsFldName(8) = "HOLDER2 FH NAME"
        lsFldName(9) = "HOLDER2 PAN NO"
        lsFldName(10) = "HOLDER3 NAME"
        lsFldName(11) = "HOLDER3 FH NAME"
        lsFldName(12) = "HOLDER3 PAN NO"
        lsFldName(13) = "ADDRESS1"
        lsFldName(14) = "ADDRESS2"
        lsFldName(15) = "ADDRESS3"
        lsFldName(16) = "CITY"
        lsFldName(17) = "STATE"
        lsFldName(18) = "COUNTRY"
        lsFldName(19) = "PINCODE"
        lsFldName(20) = "MAIL ID"
        lsFldName(21) = "CONTACT NO"
        lsFldName(22) = "NOMINEE REG NO"
        lsFldName(23) = "NOMINEE NAME"
        lsFldName(24) = "NOMINEE ADDRESS1"
        lsFldName(25) = "NOMINEE ADDRESS2"
        lsFldName(26) = "NOMINEE ADDRESS3"
        lsFldName(27) = "NOMINEE CITY"
        lsFldName(28) = "NOMINEE STATE"
        lsFldName(29) = "NOMINEE COUNTRY"
        lsFldName(30) = "NOMINEE PINCODE"
        lsFldName(31) = "BANK NAME"
        lsFldName(32) = "BANK BRANCH NAME"
        lsFldName(33) = "BANK BRANCH ADDRESS"
        lsFldName(34) = "BANK A/C NO"
        lsFldName(35) = "BANK IFSC CODE"
        lsFldName(36) = "BANK A/C TYPE"
        lsFldName(37) = "BANK BENEFICIARY"
        lsFldName(38) = "REPATRITION FLAG"
        lsFldName(39) = "CATEGORY"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 39
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 39
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileFolio)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsHolder1Name = Mid(QuoteFilter(.Item("HOLDER1 NAME").ToString), 1, 128)
                        lsHolder1FHName = Mid(QuoteFilter(.Item("HOLDER1 FH NAME").ToString), 1, 128)
                        lsHolder1PanNo = Mid(QuoteFilter(.Item("HOLDER1 PAN NO").ToString), 1, 16)
                        lsHolder2Name = Mid(QuoteFilter(.Item("HOLDER2 NAME").ToString), 1, 128)
                        lsHolder2FHName = Mid(QuoteFilter(.Item("HOLDER2 FH NAME").ToString), 1, 128)
                        lsHolder2PanNo = Mid(QuoteFilter(.Item("HOLDER2 PAN NO").ToString), 1, 16)
                        lsHolder3Name = Mid(QuoteFilter(.Item("HOLDER3 NAME").ToString), 1, 128)
                        lsHolder3FHName = Mid(QuoteFilter(.Item("HOLDER3 FH NAME").ToString), 1, 128)
                        lsHolder3PanNo = Mid(QuoteFilter(.Item("HOLDER3 PAN NO").ToString), 1, 16)
                        lsAddr1 = Mid(QuoteFilter(.Item("ADDRESS1").ToString), 1, 64)
                        lsAddr2 = Mid(QuoteFilter(.Item("ADDRESS2").ToString), 1, 64)
                        lsAddr3 = Mid(QuoteFilter(.Item("ADDRESS3").ToString), 1, 64)
                        lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 64)
                        lsState = Mid(QuoteFilter(.Item("STATE").ToString), 1, 64)
                        lsCountry = Mid(QuoteFilter(.Item("COUNTRY").ToString), 1, 64)
                        lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 16)
                        lsMailId = Mid(QuoteFilter(.Item("MAIL ID").ToString), 1, 128)
                        lsContactNo = Mid(QuoteFilter(.Item("CONTACT NO").ToString), 1, 128)
                        lsNomineeRegNo = Mid(QuoteFilter(.Item("NOMINEE REG NO").ToString), 1, 32)
                        lsNomineeName = Mid(QuoteFilter(.Item("NOMINEE NAME").ToString), 1, 128)
                        lsNomineeAddr1 = Mid(QuoteFilter(.Item("NOMINEE ADDRESS1").ToString), 1, 64)
                        lsNomineeAddr2 = Mid(QuoteFilter(.Item("NOMINEE ADDRESS2").ToString), 1, 64)
                        lsNomineeAddr3 = Mid(QuoteFilter(.Item("NOMINEE ADDRESS3").ToString), 1, 64)
                        lsNomineeCity = Mid(QuoteFilter(.Item("NOMINEE CITY").ToString), 1, 64)
                        lsNomineeState = Mid(QuoteFilter(.Item("NOMINEE STATE").ToString), 1, 64)
                        lsNomineeCountry = Mid(QuoteFilter(.Item("NOMINEE COUNTRY").ToString), 1, 64)
                        lsNomineePincode = Mid(QuoteFilter(.Item("NOMINEE PINCODE").ToString), 1, 16)
                        lsBankName = Mid(QuoteFilter(.Item("BANK NAME").ToString), 1, 128)
                        lsBankBranchName = Mid(QuoteFilter(.Item("BANK BRANCH NAME").ToString), 1, 128)
                        lsBankBranchAddr = Mid(QuoteFilter(.Item("BANK BRANCH ADDRESS").ToString), 1, 255)
                        lsBankAccNo = Mid(QuoteFilter(.Item("BANK A/C NO").ToString), 1, 64)
                        lsBankIFSCCode = Mid(QuoteFilter(.Item("BANK IFSC CODE").ToString), 1, 32)
                        lsBankAccType = Mid(QuoteFilter(.Item("BANK A/C TYPE").ToString), 1, 16)
                        lsBankBeneficiary = Mid(QuoteFilter(.Item("BANK BENEFICIARY").ToString), 1, 128)
                        lsRepatritionFlag = Mid(QuoteFilter(.Item("REPATRITION FLAG").ToString), 1, 1)
                        lsCategory = Mid(QuoteFilter(.Item("CATEGORY").ToString), 1, 32)

                        Using cmd As New MySqlCommand("pr_sta_ins_folio", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("in_holder1_name", lsHolder1Name)
                            cmd.Parameters.AddWithValue("in_holder1_fh_name", lsHolder1FHName)
                            cmd.Parameters.AddWithValue("in_holder1_pan_no", lsHolder1PanNo)
                            cmd.Parameters.AddWithValue("in_holder2_name", lsHolder2Name)
                            cmd.Parameters.AddWithValue("in_holder2_fh_name", lsHolder2FHName)
                            cmd.Parameters.AddWithValue("in_holder2_pan_no", lsHolder2PanNo)
                            cmd.Parameters.AddWithValue("in_holder3_name", lsHolder3Name)
                            cmd.Parameters.AddWithValue("in_holder3_fh_name", lsHolder3FHName)
                            cmd.Parameters.AddWithValue("in_holder3_pan_no", lsHolder3PanNo)
                            cmd.Parameters.AddWithValue("in_folio_addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("in_folio_addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("in_folio_addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("in_folio_city", lsCity)
                            cmd.Parameters.AddWithValue("in_folio_state", lsState)
                            cmd.Parameters.AddWithValue("in_folio_country", lsCountry)
                            cmd.Parameters.AddWithValue("in_folio_pincode", lsPincode)
                            cmd.Parameters.AddWithValue("in_folio_mail_id", lsMailId)
                            cmd.Parameters.AddWithValue("in_folio_contact_no", lsContactNo)
                            cmd.Parameters.AddWithValue("in_nominee_reg_no", lsNomineeRegNo)
                            cmd.Parameters.AddWithValue("in_nominee_name", lsNomineeName)
                            cmd.Parameters.AddWithValue("in_nominee_addr1", lsNomineeAddr1)
                            cmd.Parameters.AddWithValue("in_nominee_addr2", lsNomineeAddr2)
                            cmd.Parameters.AddWithValue("in_nominee_addr3", lsNomineeAddr3)
                            cmd.Parameters.AddWithValue("in_nominee_city", lsNomineeCity)
                            cmd.Parameters.AddWithValue("in_nominee_state", lsNomineeState)
                            cmd.Parameters.AddWithValue("in_nominee_country", lsNomineeCountry)
                            cmd.Parameters.AddWithValue("in_nominee_pincode", lsNomineePincode)
                            cmd.Parameters.AddWithValue("in_bank_name", lsBankName)
                            cmd.Parameters.AddWithValue("in_bank_branch", lsBankBranchName)
                            cmd.Parameters.AddWithValue("in_bank_branch_addr", lsBankBranchAddr)
                            cmd.Parameters.AddWithValue("in_bank_acc_no", lsBankAccNo)
                            cmd.Parameters.AddWithValue("in_bank_ifsc_code", lsBankIFSCCode)
                            cmd.Parameters.AddWithValue("in_bank_acc_type", lsBankAccType)
                            cmd.Parameters.AddWithValue("in_bank_beneficiary", lsBankBeneficiary)
                            cmd.Parameters.AddWithValue("in_repatrition_flag", lsRepatritionFlag)
                            cmd.Parameters.AddWithValue("in_category", lsCategory)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function FolioAddr(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(10) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsAddr1 As String
        Dim lsAddr2 As String
        Dim lsAddr3 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "ADDRESS1"
        lsFldName(5) = "ADDRESS2"
        lsFldName(6) = "ADDRESS3"
        lsFldName(7) = "CITY"
        lsFldName(8) = "STATE"
        lsFldName(9) = "COUNTRY"
        lsFldName(10) = "PINCODE"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 10
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 10
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileFolioAddr)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsAddr1 = Mid(QuoteFilter(.Item("ADDRESS1").ToString), 1, 64)
                        lsAddr2 = Mid(QuoteFilter(.Item("ADDRESS2").ToString), 1, 64)
                        lsAddr3 = Mid(QuoteFilter(.Item("ADDRESS3").ToString), 1, 64)
                        lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 64)
                        lsState = Mid(QuoteFilter(.Item("STATE").ToString), 1, 64)
                        lsCountry = Mid(QuoteFilter(.Item("COUNTRY").ToString), 1, 64)
                        lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 16)

                        Using cmd As New MySqlCommand("pr_sta_upd_folioaddrfile", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("in_folio_addr1", lsAddr1)
                            cmd.Parameters.AddWithValue("in_folio_addr2", lsAddr2)
                            cmd.Parameters.AddWithValue("in_folio_addr3", lsAddr3)
                            cmd.Parameters.AddWithValue("in_folio_city", lsCity)
                            cmd.Parameters.AddWithValue("in_folio_state", lsState)
                            cmd.Parameters.AddWithValue("in_folio_country", lsCountry)
                            cmd.Parameters.AddWithValue("in_folio_pincode", lsPincode)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Private Function QuoteFilter(ByVal txt As String) As String
        QuoteFilter = Trim(Replace(Replace(Replace(txt, "'", " "), """", """"""), "\", "\\"))
    End Function

    Public Function PendingCDSL(FileName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lfobj As New FileIO.FileSystem
        Dim ds As New DataSet
        Dim lsLine As String = ""
        Dim sr As StreamReader

        Dim lsReqDate As String
        Dim lsIsinID As String
        Dim lsDpId As String
        Dim lsClientId As String
        Dim lsCustName As String
        Dim lsJoint1Name As String = ""
        Dim lsJoint2Name As String = ""
        Dim lsDrnNo As String
        Dim lnShareCount As Long
        Dim lsDematPendFlag As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn
        Dim lbInsertFlag As Boolean = False

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", "")
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFilePendCDSL)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            sr = FileIO.FileSystem.OpenTextFileReader(FileName)

            While Not sr.EndOfStream
                lsLine = sr.ReadLine()
                lbInsertFlag = True
                i += 1

                lsReqDate = ""
                lsIsinID = ""
                lsDpId = ""
                lsClientId = ""
                lsCustName = ""
                lsDrnNo = ""
                lnShareCount = 0
                lsDematPendFlag = ""

                If lsLine.Split("~").Length >= 37 Then
                    lsIsinID = Mid(QuoteFilter(lsLine.Split("~")(14).ToString()), 1, 16)
                    lsDpId = Mid(QuoteFilter(lsLine.Split("~")(1).ToString()), 1, 16)
                    lsClientId = Mid(QuoteFilter(lsLine.Split("~")(21).ToString()), 1, 32)
                    lsCustName = Mid(QuoteFilter(lsLine.Split("~")(26).ToString()), 1, 128)
                    lsJoint1Name = Mid(QuoteFilter(lsLine.Split("~")(27).ToString()), 1, 128)
                    lsJoint2Name = Mid(QuoteFilter(lsLine.Split("~")(28).ToString()), 1, 128)
                    lsDrnNo = Mid(QuoteFilter(lsLine.Split("~")(2).ToString()), 1, 16)
                    lnShareCount = Val(QuoteFilter(lsLine.Split("~")(5).ToString()))
                    lsDematPendFlag = Mid(QuoteFilter(lsLine.Split("~")(12).ToString()), 1, 1)
                    lsReqDate = QuoteFilter(lsLine.Split("~")(18).ToString())
                    If IsDate(lsReqDate) Then lsReqDate = Format(CDate(lsReqDate), "yyyy-MM-dd") Else lsReqDate = "0001-01-01"
                End If

                Using cmd As New MySqlCommand("pr_sta_ins_dematpending", gOdbcConn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                    cmd.Parameters.AddWithValue("?in_depository_code", gsCDSLCode)
                    cmd.Parameters.AddWithValue("?in_req_date", lsReqDate)
                    cmd.Parameters.AddWithValue("?in_isin_id", lsIsinID)
                    cmd.Parameters.AddWithValue("?in_dp_id", lsDpId)
                    cmd.Parameters.AddWithValue("?in_client_id", lsClientId)
                    cmd.Parameters.AddWithValue("?in_cust_name", lsCustName)
                    cmd.Parameters.AddWithValue("?in_joint1_name", lsJoint1Name)
                    cmd.Parameters.AddWithValue("?in_joint2_name", lsJoint2Name)
                    cmd.Parameters.AddWithValue("?in_drn_no", lsDrnNo)
                    cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)
                    cmd.Parameters.AddWithValue("?in_dematpend_flag", lsDematPendFlag)
                    cmd.Parameters.AddWithValue("?in_line_no", i)
                    cmd.Parameters.AddWithValue("?in_errline_flag", True)

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

                Application.DoEvents()
            End While

            sr.Close()

            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function PendingNSDL(FileName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lfobj As New FileIO.FileSystem
        Dim ds As New DataSet
        Dim lsLine As String = ""
        Dim sr As StreamReader

        Dim lsType As String
        Dim lsReqDate As String
        Dim lsIsinID As String = ""
        Dim lsDpId As String
        Dim lsClientId As String
        Dim lsCustName As String
        Dim lsJoint1Name As String = ""
        Dim lsJoint2Name As String = ""
        Dim lsDrnNo As String
        Dim lnShareCount As Long
        Dim lsDematPendFlag As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn
        Dim lbInsertFlag As Boolean = False
        Dim lbHeaderFlag As Boolean = False

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", "")
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFilePendNSDL)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            sr = FileIO.FileSystem.OpenTextFileReader(FileName)

            While Not sr.EndOfStream
                lsLine = sr.ReadLine()
                lsLine = lsLine.Trim()

                lbInsertFlag = True
                i += 1

                lsType = ""
                lsReqDate = ""
                lsDpId = ""
                lsClientId = ""
                lsCustName = ""
                lsDrnNo = ""
                lnShareCount = 0
                lsDematPendFlag = ""

                lsType = QuoteFilter(Mid(lsLine, 1, 2))

                If lsLine.Length = 467 And lbHeaderFlag = False And lsType = "01" Then
                    lsIsinID = QuoteFilter(Mid(lsLine, 3, 12))

                    If lsIsinID <> "" Then lbHeaderFlag = True
                End If

                If lsLine.Length = 588 And lsType = "02" And lbHeaderFlag = True Then
                    lsReqDate = QuoteFilter(Mid(lsLine, 445, 8))
                    If lsReqDate.Length = 8 And IsNumeric(lsReqDate) = True Then lsReqDate = Mid(lsReqDate, 1, 4) & "-" & Mid(lsReqDate, 5, 2) & "-" & Mid(lsReqDate, 7, 2)
                    If IsDate(lsReqDate) Then lsReqDate = Format(CDate(lsReqDate), "yyyy-MM-dd") Else lsReqDate = "0001-01-01"

                    lsDpId = QuoteFilter(Mid(lsLine, 10, 8))
                    lsClientId = QuoteFilter(Mid(lsLine, 33, 8))
                    lsCustName = QuoteFilter(Mid(lsLine, 41, 128))
                    lsJoint1Name = QuoteFilter(Mid(lsLine, 291, 90))
                    lsDrnNo = QuoteFilter(Mid(lsLine, 19, 14))
                    lnShareCount = Val(QuoteFilter(Mid(lsLine, 392, 15)))
                    lsDematPendFlag = QuoteFilter(Mid(lsLine, 18, 1))

                    Using cmd As New MySqlCommand("pr_sta_ins_dematpending", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                        cmd.Parameters.AddWithValue("?in_depository_code", gsNSDLCode)
                        cmd.Parameters.AddWithValue("?in_req_date", lsReqDate)
                        cmd.Parameters.AddWithValue("?in_isin_id", lsIsinID)
                        cmd.Parameters.AddWithValue("?in_dp_id", lsDpId)
                        cmd.Parameters.AddWithValue("?in_client_id", lsClientId)
                        cmd.Parameters.AddWithValue("?in_cust_name", lsCustName)
                        cmd.Parameters.AddWithValue("?in_joint1_name", lsJoint1Name)
                        cmd.Parameters.AddWithValue("?in_joint2_name", lsJoint2Name)
                        cmd.Parameters.AddWithValue("?in_drn_no", lsDrnNo)
                        cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)
                        cmd.Parameters.AddWithValue("?in_dematpend_flag", lsDematPendFlag)
                        cmd.Parameters.AddWithValue("?in_line_no", i)
                        cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End If

                Application.DoEvents()
            End While

            sr.Close()

            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function BenpostNSDL(FileName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim j As Integer
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lfobj As New FileIO.FileSystem
        Dim ds As New DataSet
        Dim lsLine As String = ""
        Dim sr As StreamReader

        Dim lsFldValues() As String

        Dim lsType As String = ""
        Dim lsLineNo As String = ""
        Dim lsIsinID As String = ""
        Dim lsDpId As String = ""
        Dim lsClientId As String = ""
        Dim lsSebiRegNo As String = ""

        Dim lsBenpostDate As String = ""
        Dim lnShareCount As Long
        Dim lnLockin As Long
        Dim lnpledge As Long

        Dim lsHolder1Name As String
        Dim lsHolder1FHName As String
        Dim lsHolder1PanNo As String
        Dim lsHolder2Name As String
        Dim lsHolder2FHName As String
        Dim lsHolder2PanNo As String
        Dim lsHolder3Name As String
        Dim lsHolder3FHName As String
        Dim lsHolder3PanNo As String

        Dim lsHolder1Addr1 As String
        Dim lsHolder1Addr2 As String
        Dim lsHolder1Addr3 As String
        Dim lsHolder1City As String
        Dim lsHolder1State As String
        Dim lsHolder1Country As String
        Dim lsHolder1Pincode As String
        Dim lsHolder1ContactNo As String
        Dim lsHolder1FaxNo As String
        Dim lsHolder1EmailId As String
        Dim lsHolder2EmailId As String
        Dim lsHolder3EmailId As String

        Dim lsHolder1PerAddr1 As String
        Dim lsHolder1PerAddr2 As String
        Dim lsHolder1PerAddr3 As String
        Dim lsHolder1PerCity As String
        Dim lsHolder1PerState As String
        Dim lsHolder1PerCountry As String
        Dim lsHolder1PerPincode As String

        Dim lsNomineeName As String
        Dim lsNomineePart1 As String
        Dim lsNomineePart2 As String
        Dim lsNomineePart3 As String
        Dim lsNomineePart4 As String
        Dim lsNomineePart5 As String

        Dim lsBankAccNo As String
        Dim lsBankAccType As String
        Dim lsBankMicrCode As String
        Dim lsBankIfscCode As String

        Dim lsBankName As String
        Dim lsBankAddr1 As String
        Dim lsBankAddr2 As String
        Dim lsBankAddr3 As String
        Dim lsBankCity As String
        Dim lsBankState As String
        Dim lsBankCountry As String
        Dim lsBankPincode As String

        Dim lsRBIRefNo As String
        Dim lsRBIAppDate As String

        Dim lsBeneType As String
        Dim lsBeneSubType As String
        Dim lsBeneAcccat As String
        Dim lsBeneOccupation As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn
        Dim lbInsertFlag As Boolean = False
        Dim lbHeaderFlag As Boolean = False
        Dim lsbool_flag As Boolean = True

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", "")
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileBenpostNSDL)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            sr = FileIO.FileSystem.OpenTextFileReader(FileName)

            While Not sr.EndOfStream
                lsLine = sr.ReadLine()
                lsLine = lsLine.Trim()

                lsFldValues = Split(lsLine, "##")

                For j = 0 To lsFldValues.Length - 1
                    lsFldValues(j) = QuoteFilter(lsFldValues(j))
                Next j

                lbInsertFlag = True
                i += 1

                If LsvItem Is Nothing Then
                    frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                Else
                    LsvItem.Text = "Reading " & i.ToString & " line..."
                End If

                lsType = ""
                lsDpId = ""
                lsClientId = ""
                lsSebiRegNo = ""
                lnShareCount = 0
                lnLockin = 0
                lnpledge = 0
                lsHolder1Name = ""
                lsHolder1FHName = ""
                lsHolder1PanNo = ""
                lsHolder2Name = ""
                lsHolder2FHName = ""
                lsHolder2PanNo = ""
                lsHolder3Name = ""
                lsHolder3FHName = ""
                lsHolder3PanNo = ""
                lsHolder1Addr1 = ""
                lsHolder1Addr2 = ""
                lsHolder1Addr3 = ""
                lsHolder1City = ""
                lsHolder1State = ""
                lsHolder1Country = ""
                lsHolder1Pincode = ""
                lsHolder1ContactNo = ""
                lsHolder1FaxNo = ""
                lsHolder1EmailId = ""
                lsHolder2EmailId = ""
                lsHolder3EmailId = ""
                lsHolder1PerAddr1 = ""
                lsHolder1PerAddr2 = ""
                lsHolder1PerAddr3 = ""
                lsHolder1PerCity = ""
                lsHolder1PerState = ""
                lsHolder1PerCountry = ""
                lsHolder1PerPincode = ""
                lsNomineeName = ""
                lsNomineePart1 = ""
                lsNomineePart2 = ""
                lsNomineePart3 = ""
                lsNomineePart4 = ""
                lsNomineePart5 = ""
                lsBankAccNo = ""
                lsBankAccType = ""
                lsBankMicrCode = ""
                lsBankIfscCode = ""
                lsBankName = ""
                lsBankAddr1 = ""
                lsBankAddr2 = ""
                lsBankAddr3 = ""
                lsBankCity = ""
                lsBankState = ""
                lsBankCountry = ""
                lsBankPincode = ""
                lsRBIRefNo = ""
                lsRBIAppDate = ""
                lsBeneType = ""
                lsBeneSubType = ""
                lsBeneAcccat = ""
                lsBeneOccupation = ""

                If (lsFldValues.Length = 20 Or lsFldValues.Length = 24) And lbHeaderFlag = False And lsFldValues(0) = "01" Then
                    lsIsinID = lsFldValues(1)
                    lsBenpostDate = lsFldValues(2)
                    If lsBenpostDate.Length = 8 And IsNumeric(lsBenpostDate) = True Then lsBenpostDate = Mid(lsBenpostDate, 1, 4) & "-" & Mid(lsBenpostDate, 5, 2) & "-" & Mid(lsBenpostDate, 7, 2)
                    If IsDate(lsBenpostDate) Then lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd") Else lsBenpostDate = "0001-01-01"
                    If lsIsinID <> "" Then lbHeaderFlag = True
                End If

                If (lsFldValues.Length = 72 Or lsFldValues.Length = 76 Or lsFldValues.Length = 82 Or lsFldValues.Length = 84) And lsFldValues(0) = "02" And lbHeaderFlag = True Then
                    lsDpId = Mid(lsFldValues(2), 1, 32)
                    lsClientId = Mid(lsFldValues(3), 1, 32)
                    lsSebiRegNo = Mid(lsFldValues(44), 1, 32)

                    lnShareCount = 0
                    lnShareCount += Val(lsFldValues(47))
                    lnShareCount += Val(lsFldValues(48))
                    lnShareCount += Val(lsFldValues(49))
                    lnShareCount += Val(lsFldValues(50))
                    lnShareCount += Val(lsFldValues(51))
                    lnShareCount += Val(lsFldValues(52))
                    lnShareCount += Val(lsFldValues(53))
                    lnShareCount += Val(lsFldValues(54))
                    lnShareCount += Val(lsFldValues(55))
                    lnShareCount += Val(lsFldValues(56))
                    lnShareCount += Val(lsFldValues(57))
                    lnShareCount += Val(lsFldValues(58))

                    lnLockin = Val(lsFldValues(48))
                    lnpledge = Val(lsFldValues(49))

                    lsHolder1Name = Mid(lsFldValues(8), 1, 128)
                    lsHolder1FHName = Mid(lsFldValues(9), 1, 128)
                    lsHolder1PanNo = Mid(lsFldValues(23), 1, 16)
                    lsHolder2Name = Mid(lsFldValues(17), 1, 128)
                    lsHolder2FHName = Mid(lsFldValues(18), 1, 128)
                    lsHolder2PanNo = Mid(lsFldValues(24), 1, 16)
                    lsHolder3Name = Mid(lsFldValues(19), 1, 128)
                    lsHolder3FHName = Mid(lsFldValues(20), 1, 128)
                    lsHolder3PanNo = Mid(lsFldValues(25), 1, 16)
                    lsHolder1Addr1 = Mid(lsFldValues(10), 1, 128)
                    lsHolder1Addr2 = Mid(lsFldValues(11), 1, 128)
                    lsHolder1Addr3 = Mid(lsFldValues(12), 1, 128)
                    lsHolder1City = Mid(lsFldValues(13), 1, 128)
                    lsHolder1State = ""
                    lsHolder1Country = ""
                    lsHolder1Pincode = Mid(lsFldValues(14), 1, 128)
                    lsHolder1ContactNo = Mid(lsFldValues(15), 1, 128)
                    lsHolder1FaxNo = Mid(lsFldValues(16), 1, 128)
                    lsHolder1EmailId = Mid(lsFldValues(66), 1, 128)
                    lsHolder2EmailId = Mid(lsFldValues(67), 1, 128)
                    lsHolder3EmailId = Mid(lsFldValues(68), 1, 128)
                    lsHolder1PerAddr1 = ""
                    lsHolder1PerAddr2 = ""
                    lsHolder1PerAddr3 = ""
                    lsHolder1PerCity = ""
                    lsHolder1PerState = ""
                    lsHolder1PerCountry = ""
                    lsHolder1PerPincode = ""
                    lsNomineeName = Mid(lsFldValues(27), 1, 128)
                    lsNomineePart1 = Mid(lsFldValues(28), 1, 128)
                    lsNomineePart2 = Mid(lsFldValues(29), 1, 128)
                    lsNomineePart3 = Mid(lsFldValues(30), 1, 128)
                    lsNomineePart4 = Mid(lsFldValues(31), 1, 128)
                    lsNomineePart5 = Mid(lsFldValues(32), 1, 128)
                    lsBankAccNo = Mid(lsFldValues(35), 1, 128)
                    lsBankAccType = Mid(lsFldValues(61), 1, 128)
                    lsBankMicrCode = Mid(lsFldValues(59), 1, 16)
                    lsBankIfscCode = Mid(lsFldValues(60), 1, 32)
                    lsBankName = Mid(lsFldValues(36), 1, 128)
                    lsBankAddr1 = Mid(lsFldValues(37), 1, 128)
                    lsBankAddr2 = Mid(lsFldValues(38), 1, 128)
                    lsBankAddr3 = Mid(lsFldValues(39), 1, 128)
                    lsBankCity = Mid(lsFldValues(40), 1, 128)
                    lsBankState = ""
                    lsBankCountry = ""
                    lsBankPincode = Mid(lsFldValues(41), 1, 16)
                    lsRBIRefNo = Mid(lsFldValues(42), 1, 32)

                    lsRBIAppDate = lsFldValues(43)
                    If lsRBIAppDate.Length = 8 And IsNumeric(lsRBIAppDate) = True Then lsRBIAppDate = Mid(lsRBIAppDate, 1, 4) & "-" & Mid(lsRBIAppDate, 5, 2) & "-" & Mid(lsRBIAppDate, 7, 2)
                    If IsDate(lsRBIAppDate) Then lsRBIAppDate = Format(CDate(lsRBIAppDate), "yyyy-MM-dd") Else lsRBIAppDate = "0001-01-01"

                    lsBeneType = Mid(lsFldValues(4), 1, 16)
                    lsBeneSubType = Mid(lsFldValues(5), 1, 16)
                    lsBeneAcccat = Mid(lsFldValues(6), 1, 16)
                    lsBeneOccupation = Mid(lsFldValues(7), 1, 16)

                    If lsbool_flag = True Then
                        Using cmd As New MySqlCommand("pr_sta_del_currentbenpost", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_depository_code", gsNSDLCode)
                            cmd.Parameters.AddWithValue("?in_isin_id", lsIsinID)
                            cmd.Parameters.AddWithValue("?in_benpost_date", lsBenpostDate)

                            cmd.CommandTimeout = 0

                            cmd.ExecuteNonQuery()
                        End Using
                        lsbool_flag = False
                    End If

                    Using cmd As New MySqlCommand("pr_sta_ins_benpost", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                        cmd.Parameters.AddWithValue("?in_depository_code", gsNSDLCode)
                        cmd.Parameters.AddWithValue("?in_isin_id", lsIsinID)
                        cmd.Parameters.AddWithValue("?in_dp_id", lsDpId)
                        cmd.Parameters.AddWithValue("?in_client_id", lsClientId)
                        cmd.Parameters.AddWithValue("?in_sebi_reg_no", lsSebiRegNo)
                        cmd.Parameters.AddWithValue("?in_benpost_date", lsBenpostDate)
                        cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)
                        cmd.Parameters.AddWithValue("?in_lockin", lnLockin)
                        cmd.Parameters.AddWithValue("?in_pledge", lnpledge)
                        cmd.Parameters.AddWithValue("?in_holder1_name", lsHolder1Name)
                        cmd.Parameters.AddWithValue("?in_holder1_fh_name", lsHolder1FHName)
                        cmd.Parameters.AddWithValue("?in_holder1_pan", lsHolder1PanNo)
                        cmd.Parameters.AddWithValue("?in_holder2_name", lsHolder2Name)
                        cmd.Parameters.AddWithValue("?in_holder2_fh_name", lsHolder2FHName)
                        cmd.Parameters.AddWithValue("?in_holder2_pan", lsHolder2PanNo)
                        cmd.Parameters.AddWithValue("?in_holder3_name", lsHolder3Name)
                        cmd.Parameters.AddWithValue("?in_holder3_fh_name", lsHolder3FHName)
                        cmd.Parameters.AddWithValue("?in_holder3_pan", lsHolder3PanNo)
                        cmd.Parameters.AddWithValue("?in_holder1_addr1", lsHolder1Addr1)
                        cmd.Parameters.AddWithValue("?in_holder1_addr2", lsHolder1Addr2)
                        cmd.Parameters.AddWithValue("?in_holder1_addr3", lsHolder1Addr3)
                        cmd.Parameters.AddWithValue("?in_holder1_city", lsHolder1City)
                        cmd.Parameters.AddWithValue("?in_holder1_state", lsHolder1State)
                        cmd.Parameters.AddWithValue("?in_holder1_country", lsHolder1Country)
                        cmd.Parameters.AddWithValue("?in_holder1_pin", lsHolder1Pincode)
                        cmd.Parameters.AddWithValue("?in_holder1_contact_no", lsHolder1ContactNo)
                        cmd.Parameters.AddWithValue("?in_holder1_fax_no", lsHolder1FaxNo)
                        cmd.Parameters.AddWithValue("?in_holder1_email_id", lsHolder1EmailId)
                        cmd.Parameters.AddWithValue("?in_holder2_email_id", lsHolder2EmailId)
                        cmd.Parameters.AddWithValue("?in_holder3_email_id", lsHolder3EmailId)
                        cmd.Parameters.AddWithValue("?in_holder1_per_addr1", lsHolder1PerAddr1)
                        cmd.Parameters.AddWithValue("?in_holder1_per_addr2", lsHolder1PerAddr2)
                        cmd.Parameters.AddWithValue("?in_holder1_per_addr3", lsHolder1PerAddr3)
                        cmd.Parameters.AddWithValue("?in_holder1_per_city", lsHolder1PerCity)
                        cmd.Parameters.AddWithValue("?in_holder1_per_state", lsHolder1PerState)
                        cmd.Parameters.AddWithValue("?in_holder1_per_country", lsHolder1PerCountry)
                        cmd.Parameters.AddWithValue("?in_holder1_per_pin", lsHolder1PerPincode)
                        cmd.Parameters.AddWithValue("?in_nominee_name", lsNomineeName)
                        cmd.Parameters.AddWithValue("?in_nominee_part1", lsNomineePart1)
                        cmd.Parameters.AddWithValue("?in_nominee_part2", lsNomineePart2)
                        cmd.Parameters.AddWithValue("?in_nominee_part3", lsNomineePart3)
                        cmd.Parameters.AddWithValue("?in_nominee_part4", lsNomineePart4)
                        cmd.Parameters.AddWithValue("?in_nominee_part5", lsNomineePart5)
                        cmd.Parameters.AddWithValue("?in_bank_acc_no", lsBankAccNo)
                        cmd.Parameters.AddWithValue("?in_bank_acc_type", lsBankAccType)
                        cmd.Parameters.AddWithValue("?in_bank_micr_code", lsBankMicrCode)
                        cmd.Parameters.AddWithValue("?in_bank_ifsc_code", lsBankIfscCode)
                        cmd.Parameters.AddWithValue("?in_bank_name", lsBankName)
                        cmd.Parameters.AddWithValue("?in_bank_addr1", lsBankAddr1)
                        cmd.Parameters.AddWithValue("?in_bank_addr2", lsBankAddr2)
                        cmd.Parameters.AddWithValue("?in_bank_addr3", lsBankAddr3)
                        cmd.Parameters.AddWithValue("?in_bank_city", lsBankCity)
                        cmd.Parameters.AddWithValue("?in_bank_state", lsBankState)
                        cmd.Parameters.AddWithValue("?in_bank_country", lsBankCountry)
                        cmd.Parameters.AddWithValue("?in_bank_pin", lsBankPincode)
                        cmd.Parameters.AddWithValue("?in_rbi_ref_no", lsRBIRefNo)
                        cmd.Parameters.AddWithValue("?in_rbi_app_date", lsRBIAppDate)
                        cmd.Parameters.AddWithValue("?in_bene_type", lsBeneType)
                        cmd.Parameters.AddWithValue("?in_bene_subtype", lsBeneSubType)
                        cmd.Parameters.AddWithValue("?in_bene_acccat", lsBeneAcccat)
                        cmd.Parameters.AddWithValue("?in_bene_occupation", lsBeneOccupation)
                        cmd.Parameters.AddWithValue("?in_line_no", i)
                        cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                        'If lnResult = 0 Then MsgBox("vijay")
                    End Using
                End If

                Application.DoEvents()
            End While

            sr.Close()

            frmMain.lblStatus.Text = ""

            If lbHeaderFlag = True Then i -= 1

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function BenpostCDSL(FileName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim j As Integer
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lfobj As New FileIO.FileSystem
        Dim ds As New DataSet
        Dim lsLine As String = ""
        Dim sr As StreamReader

        Dim lsFldValues() As String

        Dim lsType As String = ""
        Dim lsLineNo As String = ""
        Dim lsIsinID As String = ""
        Dim lsDpId As String = ""
        Dim lsClientId As String = ""
        Dim lsSebiRegNo As String = ""

        Dim lsBenpostDate As String = ""
        Dim lnShareCount As Long
        Dim lnLockIn As Long
        Dim lnpledge As Long

        Dim lsHolder1Name As String
        Dim lsHolder1FHName As String
        Dim lsHolder1PanNo As String
        Dim lsHolder2Name As String
        Dim lsHolder2FHName As String
        Dim lsHolder2PanNo As String
        Dim lsHolder3Name As String
        Dim lsHolder3FHName As String
        Dim lsHolder3PanNo As String

        Dim lsHolder1Addr1 As String
        Dim lsHolder1Addr2 As String
        Dim lsHolder1Addr3 As String
        Dim lsHolder1City As String
        Dim lsHolder1State As String
        Dim lsHolder1Country As String
        Dim lsHolder1Pincode As String
        Dim lsHolder1ContactNo As String
        Dim lsHolder1FaxNo As String
        Dim lsHolder1EmailId As String
        Dim lsHolder2EmailId As String
        Dim lsHolder3EmailId As String

        Dim lsHolder1PerAddr1 As String
        Dim lsHolder1PerAddr2 As String
        Dim lsHolder1PerAddr3 As String
        Dim lsHolder1PerCity As String
        Dim lsHolder1PerState As String
        Dim lsHolder1PerCountry As String
        Dim lsHolder1PerPincode As String

        Dim lsNomineeName As String
        Dim lsNomineePart1 As String
        Dim lsNomineePart2 As String
        Dim lsNomineePart3 As String
        Dim lsNomineePart4 As String
        Dim lsNomineePart5 As String

        Dim lsBankAccNo As String
        Dim lsBankAccType As String
        Dim lsBankMicrCode As String
        Dim lsBankIfscCode As String

        Dim lsBankName As String
        Dim lsBankAddr1 As String
        Dim lsBankAddr2 As String
        Dim lsBankAddr3 As String
        Dim lsBankCity As String
        Dim lsBankState As String
        Dim lsBankCountry As String
        Dim lsBankPincode As String

        Dim lsRBIRefNo As String
        Dim lsRBIAppDate As String

        Dim lsBeneType As String
        Dim lsBeneSubType As String
        Dim lsBeneAcccat As String
        Dim lsBeneOccupation As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn
        Dim lbInsertFlag As Boolean = False
        Dim lsbool_flag As Boolean = True


        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", "")
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileBenpostCDSL)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            sr = FileIO.FileSystem.OpenTextFileReader(FileName)

            While Not sr.EndOfStream
                lsLine = sr.ReadLine()
                lsLine = lsLine.Trim()

                lsFldValues = Split(lsLine, "~")

                For j = 0 To lsFldValues.Length - 1
                    lsFldValues(j) = QuoteFilter(lsFldValues(j))
                Next j

                lbInsertFlag = True
                i += 1

                If LsvItem Is Nothing Then
                    frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                Else
                    LsvItem.Text = "Reading " & i.ToString & " line..."
                End If

                Application.DoEvents()

                lsType = ""
                lsDpId = ""
                lsClientId = ""
                lsSebiRegNo = ""
                lnShareCount = 0
                lnLockIn = 0
                lnpledge = 0
                lsHolder1Name = ""
                lsHolder1FHName = ""
                lsHolder1PanNo = ""
                lsHolder2Name = ""
                lsHolder2FHName = ""
                lsHolder2PanNo = ""
                lsHolder3Name = ""
                lsHolder3FHName = ""
                lsHolder3PanNo = ""
                lsHolder1Addr1 = ""
                lsHolder1Addr2 = ""
                lsHolder1Addr3 = ""
                lsHolder1City = ""
                lsHolder1State = ""
                lsHolder1Country = ""
                lsHolder1Pincode = ""
                lsHolder1ContactNo = ""
                lsHolder1FaxNo = ""
                lsHolder1EmailId = ""
                lsHolder2EmailId = ""
                lsHolder3EmailId = ""
                lsHolder1PerAddr1 = ""
                lsHolder1PerAddr2 = ""
                lsHolder1PerAddr3 = ""
                lsHolder1PerCity = ""
                lsHolder1PerState = ""
                lsHolder1PerCountry = ""
                lsHolder1PerPincode = ""
                lsNomineeName = ""
                lsNomineePart1 = ""
                lsNomineePart2 = ""
                lsNomineePart3 = ""
                lsNomineePart4 = ""
                lsNomineePart5 = ""
                lsBankAccNo = ""
                lsBankAccType = ""
                lsBankMicrCode = ""
                lsBankIfscCode = ""
                lsBankName = ""
                lsBankAddr1 = ""
                lsBankAddr2 = ""
                lsBankAddr3 = ""
                lsBankCity = ""
                lsBankState = ""
                lsBankCountry = ""
                lsBankPincode = ""
                lsRBIRefNo = ""
                lsRBIAppDate = ""
                lsBeneType = ""
                lsBeneSubType = ""
                lsBeneAcccat = ""
                lsBeneOccupation = ""

                lsBenpostDate = lsFldValues(73)
                If lsBenpostDate.Length = 8 And IsNumeric(lsBenpostDate) = True Then lsBenpostDate = Mid(lsBenpostDate, 5, 4) & "-" & Mid(lsBenpostDate, 3, 2) & "-" & Mid(lsBenpostDate, 1, 2)
                If IsDate(lsBenpostDate) Then lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd") Else lsBenpostDate = "0001-01-01"

                'actual value is 105 is on there i have added 100 and 102 reason:- uploaded failed scenario -- cmt line added by Mohan
                'Or lsFldValues.Length = 100 Or lsFldValues.Length = 102 Or lsFldValues.Length = 105
                If lsFldValues.Length = 91 Or lsFldValues.Length = 92 Or lsFldValues.Length = 100 Or lsFldValues.Length = 102 Or lsFldValues.Length = 105 Then
                    lsIsinID = lsFldValues(0)

                    lsDpId = Mid(lsFldValues(1), 1, 8)
                    lsClientId = Mid(lsFldValues(1), 9, 32)
                    lsSebiRegNo = Mid(lsFldValues(23), 1, 32)
                    lnShareCount = Val(lsFldValues(64))
                    lnLockIn = Val(lsFldValues(65))
                    lnpledge = Val(lsFldValues(66))
                    lsHolder1Name = Mid(lsFldValues(2), 1, 128)
                    lsHolder1FHName = Mid(lsFldValues(7), 1, 128)
                    lsHolder1PanNo = Mid(lsFldValues(16), 1, 17)
                    lsHolder2Name = Mid(lsFldValues(3), 1, 128)
                    lsHolder2FHName = ""
                    lsHolder2PanNo = Mid(lsFldValues(17), 1, 18)
                    lsHolder3Name = Mid(lsFldValues(4), 1, 128)
                    lsHolder3FHName = ""
                    lsHolder3PanNo = Mid(lsFldValues(18), 1, 19)
                    lsHolder1Addr1 = Mid(lsFldValues(32), 1, 128)
                    lsHolder1Addr2 = Mid(lsFldValues(33), 1, 128)
                    lsHolder1Addr3 = Mid(lsFldValues(34), 1, 128)
                    lsHolder1City = Mid(lsFldValues(35), 1, 128)
                    lsHolder1State = Mid(lsFldValues(36), 1, 128)
                    lsHolder1Country = Mid(lsFldValues(37), 1, 128)
                    lsHolder1Pincode = Mid(lsFldValues(38), 1, 16)
                    lsHolder1ContactNo = Mid(lsFldValues(46), 1, 128)
                    lsHolder1FaxNo = Mid(lsFldValues(48), 1, 128)
                    lsHolder1EmailId = Mid(QuoteFilter(lsFldValues(49)), 1, 128)
                    lsHolder2EmailId = ""
                    lsHolder3EmailId = ""
                    lsHolder1PerAddr1 = Mid(lsFldValues(39), 1, 128)
                    lsHolder1PerAddr2 = Mid(lsFldValues(40), 1, 128)
                    lsHolder1PerAddr3 = Mid(lsFldValues(41), 1, 128)
                    lsHolder1PerCity = Mid(lsFldValues(42), 1, 128)
                    lsHolder1PerState = Mid(lsFldValues(43), 1, 128)
                    lsHolder1PerCountry = Mid(lsFldValues(44), 1, 128)
                    lsHolder1PerPincode = Mid(lsFldValues(45), 1, 128)
                    lsNomineeName = Mid(lsFldValues(6), 1, 128)
                    lsNomineePart1 = ""
                    lsNomineePart2 = ""
                    lsNomineePart3 = ""
                    lsNomineePart4 = ""
                    lsNomineePart5 = ""
                    lsBankAccNo = Mid(lsFldValues(63), 1, 128)
                    lsBankAccType = Mid(lsFldValues(62), 1, 128)
                    lsBankMicrCode = Mid(lsFldValues(51), 1, 16)
                    lsBankIfscCode = Mid(lsFldValues(52), 1, 32)
                    lsBankName = Mid(lsFldValues(53), 1, 128)
                    lsBankAddr1 = Mid(lsFldValues(54), 1, 128)
                    lsBankAddr2 = Mid(lsFldValues(55), 1, 128)
                    lsBankAddr3 = Mid(lsFldValues(56), 1, 128)
                    lsBankCity = Mid(lsFldValues(57), 1, 128)
                    lsBankState = Mid(lsFldValues(58), 1, 128)
                    lsBankCountry = Mid(lsFldValues(59), 1, 128)
                    lsBankPincode = Mid(lsFldValues(60), 1, 16)
                    lsRBIRefNo = Mid(lsFldValues(28), 1, 32)

                    lsRBIAppDate = lsFldValues(29)
                    If lsRBIAppDate.Length = 8 And IsNumeric(lsRBIAppDate) = True Then lsRBIAppDate = Mid(lsRBIAppDate, 1, 4) & "-" & Mid(lsRBIAppDate, 5, 2) & "-" & Mid(lsRBIAppDate, 7, 2)
                    If IsDate(lsRBIAppDate) Then lsRBIAppDate = Format(CDate(lsRBIAppDate), "yyyy-MM-dd") Else lsRBIAppDate = "0001-01-01"

                    lsBeneType = Mid(lsFldValues(13), 1, 16)
                    lsBeneSubType = Mid(lsFldValues(14), 1, 16)
                    lsBeneAcccat = Mid(lsFldValues(11), 1, 16)
                    lsBeneOccupation = Mid(lsFldValues(15), 1, 16)

                    If lsbool_flag = True Then
                        Using cmd As New MySqlCommand("pr_sta_del_currentbenpost", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_depository_code", gsCDSLCode)
                            cmd.Parameters.AddWithValue("?in_isin_id", lsIsinID)
                            cmd.Parameters.AddWithValue("?in_benpost_date", lsBenpostDate)

                            cmd.CommandTimeout = 0

                            cmd.ExecuteNonQuery()
                        End Using
                        lsbool_flag = False
                    End If

                    Using cmd As New MySqlCommand("pr_sta_ins_benpost", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                        cmd.Parameters.AddWithValue("?in_depository_code", gsCDSLCode)
                        cmd.Parameters.AddWithValue("?in_isin_id", lsIsinID)
                        cmd.Parameters.AddWithValue("?in_dp_id", lsDpId)
                        cmd.Parameters.AddWithValue("?in_client_id", lsClientId)
                        cmd.Parameters.AddWithValue("?in_sebi_reg_no", lsSebiRegNo)
                        cmd.Parameters.AddWithValue("?in_benpost_date", lsBenpostDate)
                        cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)
                        cmd.Parameters.AddWithValue("?in_lockin", lnLockIn)
                        cmd.Parameters.AddWithValue("?in_pledge", lnpledge)
                        cmd.Parameters.AddWithValue("?in_holder1_name", lsHolder1Name)
                        cmd.Parameters.AddWithValue("?in_holder1_fh_name", lsHolder1FHName)
                        cmd.Parameters.AddWithValue("?in_holder1_pan", lsHolder1PanNo)
                        cmd.Parameters.AddWithValue("?in_holder2_name", lsHolder2Name)
                        cmd.Parameters.AddWithValue("?in_holder2_fh_name", lsHolder2FHName)
                        cmd.Parameters.AddWithValue("?in_holder2_pan", lsHolder2PanNo)
                        cmd.Parameters.AddWithValue("?in_holder3_name", lsHolder3Name)
                        cmd.Parameters.AddWithValue("?in_holder3_fh_name", lsHolder3FHName)
                        cmd.Parameters.AddWithValue("?in_holder3_pan", lsHolder3PanNo)
                        cmd.Parameters.AddWithValue("?in_holder1_addr1", lsHolder1Addr1)
                        cmd.Parameters.AddWithValue("?in_holder1_addr2", lsHolder1Addr2)
                        cmd.Parameters.AddWithValue("?in_holder1_addr3", lsHolder1Addr3)
                        cmd.Parameters.AddWithValue("?in_holder1_city", lsHolder1City)
                        cmd.Parameters.AddWithValue("?in_holder1_state", lsHolder1State)
                        cmd.Parameters.AddWithValue("?in_holder1_country", lsHolder1Country)
                        cmd.Parameters.AddWithValue("?in_holder1_pin", lsHolder1Pincode)
                        cmd.Parameters.AddWithValue("?in_holder1_contact_no", lsHolder1ContactNo)
                        cmd.Parameters.AddWithValue("?in_holder1_fax_no", lsHolder1FaxNo)
                        cmd.Parameters.AddWithValue("?in_holder1_email_id", lsHolder1EmailId)
                        cmd.Parameters.AddWithValue("?in_holder2_email_id", lsHolder2EmailId)
                        cmd.Parameters.AddWithValue("?in_holder3_email_id", lsHolder3EmailId)
                        cmd.Parameters.AddWithValue("?in_holder1_per_addr1", lsHolder1PerAddr1)
                        cmd.Parameters.AddWithValue("?in_holder1_per_addr2", lsHolder1PerAddr2)
                        cmd.Parameters.AddWithValue("?in_holder1_per_addr3", lsHolder1PerAddr3)
                        cmd.Parameters.AddWithValue("?in_holder1_per_city", lsHolder1PerCity)
                        cmd.Parameters.AddWithValue("?in_holder1_per_state", lsHolder1PerState)
                        cmd.Parameters.AddWithValue("?in_holder1_per_country", lsHolder1PerCountry)
                        cmd.Parameters.AddWithValue("?in_holder1_per_pin", lsHolder1PerPincode)
                        cmd.Parameters.AddWithValue("?in_nominee_name", lsNomineeName)
                        cmd.Parameters.AddWithValue("?in_nominee_part1", lsNomineePart1)
                        cmd.Parameters.AddWithValue("?in_nominee_part2", lsNomineePart2)
                        cmd.Parameters.AddWithValue("?in_nominee_part3", lsNomineePart3)
                        cmd.Parameters.AddWithValue("?in_nominee_part4", lsNomineePart4)
                        cmd.Parameters.AddWithValue("?in_nominee_part5", lsNomineePart5)
                        cmd.Parameters.AddWithValue("?in_bank_acc_no", lsBankAccNo)
                        cmd.Parameters.AddWithValue("?in_bank_acc_type", lsBankAccType)
                        cmd.Parameters.AddWithValue("?in_bank_micr_code", lsBankMicrCode)
                        cmd.Parameters.AddWithValue("?in_bank_ifsc_code", lsBankIfscCode)
                        cmd.Parameters.AddWithValue("?in_bank_name", lsBankName)
                        cmd.Parameters.AddWithValue("?in_bank_addr1", lsBankAddr1)
                        cmd.Parameters.AddWithValue("?in_bank_addr2", lsBankAddr2)
                        cmd.Parameters.AddWithValue("?in_bank_addr3", lsBankAddr3)
                        cmd.Parameters.AddWithValue("?in_bank_city", lsBankCity)
                        cmd.Parameters.AddWithValue("?in_bank_state", lsBankState)
                        cmd.Parameters.AddWithValue("?in_bank_country", lsBankCountry)
                        cmd.Parameters.AddWithValue("?in_bank_pin", lsBankPincode)
                        cmd.Parameters.AddWithValue("?in_rbi_ref_no", lsRBIRefNo)
                        cmd.Parameters.AddWithValue("?in_rbi_app_date", lsRBIAppDate)
                        cmd.Parameters.AddWithValue("?in_bene_type", lsBeneType)
                        cmd.Parameters.AddWithValue("?in_bene_subtype", lsBeneSubType)
                        cmd.Parameters.AddWithValue("?in_bene_acccat", lsBeneAcccat)
                        cmd.Parameters.AddWithValue("?in_bene_occupation", lsBeneOccupation)
                        cmd.Parameters.AddWithValue("?in_line_no", i)
                        cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End If

                Application.DoEvents()
            End While

            sr.Close()

            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function FolioPan(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(6) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsHolder1PanNo As String
        Dim lsHolder2PanNo As String
        Dim lsHolder3PanNo As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "HOLDER1 PAN NO"
        lsFldName(5) = "HOLDER2 PAN NO"
        lsFldName(6) = "HOLDER3 PAN NO"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 6
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 6
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileFolioPan)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsHolder1PanNo = Mid(QuoteFilter(.Item("HOLDER1 PAN NO").ToString), 1, 16)
                        lsHolder2PanNo = Mid(QuoteFilter(.Item("HOLDER2 PAN NO").ToString), 1, 16)
                        lsHolder3PanNo = Mid(QuoteFilter(.Item("HOLDER3 PAN NO").ToString), 1, 16)

                        Using cmd As New MySqlCommand("pr_sta_upd_foliopanfile", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_holder1_pan_no", lsHolder1PanNo)
                            cmd.Parameters.AddWithValue("?in_holder2_pan_no", lsHolder2PanNo)
                            cmd.Parameters.AddWithValue("?in_holder3_pan_no", lsHolder3PanNo)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function FolioEmailId(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(4) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsEmailId As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "EMAIL ID"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 4
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 4
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileFolioEmailId)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsEmailId = Mid(QuoteFilter(.Item("EMAIL ID").ToString), 1, 128)

                        Using cmd As New MySqlCommand("pr_sta_upd_folioemailidfile", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_email_id", lsEmailId)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function FolioContactNo(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(4) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsContactNo As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "CONTACT NO"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 4
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 4
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileFolioContactNo)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsContactNo = Mid(QuoteFilter(.Item("CONTACT NO").ToString), 1, 128)

                        Using cmd As New MySqlCommand("pr_sta_upd_foliocontactnofile", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_contact_no", lsContactNo)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function CertificateStatusUpdate(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(8) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsCertNo As String
        Dim ldIssueDate As Date
        Dim lnShareCount As Long
        Dim lsStatus As String
        Dim lsRemark As String
        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "CERTIFICATE NO"
        lsFldName(5) = "ISSUE DATE"
        lsFldName(6) = "SHARE COUNT"
        lsFldName(7) = "STATUS"
        lsFldName(8) = "REMARK"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 8
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 8
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next
            Call FormatSheet(lsFileName, SheetName)
            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileCertStatusUpdate)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        ldIssueDate = Nothing
                        lnShareCount = 0

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsCertNo = Mid(QuoteFilter(.Item("CERTIFICATE NO").ToString), 1, 32)
                        lnShareCount = Val(QuoteFilter(.Item("SHARE COUNT").ToString))

                        lsTxt = QuoteFilter(.Item("ISSUE DATE").ToString)
                        If IsDate(lsTxt) Then
                            ldIssueDate = CDate(lsTxt)
                        Else
                            ldIssueDate = DateSerial(1, 1, 1)
                        End If

                        lsStatus = Mid(QuoteFilter(.Item("STATUS").ToString), 1, 32)
                        lsRemark = Mid(QuoteFilter(.Item("REMARK").ToString), 1, 255)

                        Using cmd As New MySqlCommand("pr_sta_upd_certstatus", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_cert_no", lsCertNo)
                            cmd.Parameters.AddWithValue("?in_issue_date", ldIssueDate)
                            cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)
                            cmd.Parameters.AddWithValue("?in_status", lsStatus)
                            cmd.Parameters.AddWithValue("?in_remark", lsRemark)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function CertificateUpdate(FileName As String, SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(12) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsCertNo As String
        Dim ldIssueDate As Date
        Dim ldExpiredDate As Date
        Dim ldLockinPeriodFrom As Date
        Dim ldLockinPeriodTo As Date
        Dim ldHoldDate As Date
        Dim ldHoldExpireDate As Date
        Dim lsStatus As String
        Dim lsRemark As String
        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = "CERTIFICATE NO"
        lsFldName(5) = "EXPIRED DATE"
        lsFldName(6) = "ISSUE DATE"
        lsFldName(7) = "LOCKIN PERIOD FROM"
        lsFldName(8) = "LOCKIN PERIOD TO"
        lsFldName(9) = "HOLD DATE"
        lsFldName(10) = "HOLD EXPIRE DATE"
        lsFldName(11) = "STATUS"
        lsFldName(12) = "REMARK"

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 12
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 12
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnFileCert)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        ldIssueDate = Nothing
                        ldExpiredDate = Nothing
                        ldLockinPeriodFrom = Nothing
                        ldLockinPeriodTo = Nothing
                        ldHoldDate = Nothing
                        ldHoldExpireDate = Nothing

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsCertNo = Mid(QuoteFilter(.Item("CERTIFICATE NO").ToString), 1, 32)

                        lsTxt = QuoteFilter(.Item("ISSUE DATE").ToString)
                        If IsDate(lsTxt) Then ldIssueDate = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("EXPIRED DATE").ToString)
                        If IsDate(lsTxt) Then ldExpiredDate = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("LOCKIN PERIOD FROM").ToString)
                        If IsDate(lsTxt) Then ldLockinPeriodFrom = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("LOCKIN PERIOD TO").ToString)
                        If IsDate(lsTxt) Then ldLockinPeriodTo = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("HOLD DATE").ToString)
                        If IsDate(lsTxt) Then ldHoldDate = CDate(lsTxt)

                        lsTxt = QuoteFilter(.Item("HOLD EXPIRE DATE").ToString)
                        If IsDate(lsTxt) Then ldHoldExpireDate = CDate(lsTxt)

                        lsStatus = Mid(QuoteFilter(.Item("STATUS").ToString), 1, 32)
                        lsRemark = Mid(QuoteFilter(.Item("REMARK").ToString), 1, 255)

                        Using cmd As New MySqlCommand("pr_sta_upd_cert", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_cert_no", lsCertNo)
                            cmd.Parameters.AddWithValue("?in_issue_date", ldIssueDate)
                            cmd.Parameters.AddWithValue("?in_expired_date", ldExpiredDate)
                            cmd.Parameters.AddWithValue("?in_lockin_period_from", ldLockinPeriodFrom)
                            cmd.Parameters.AddWithValue("?in_lockin_period_to", ldLockinPeriodTo)
                            cmd.Parameters.AddWithValue("?in_hold_date", ldHoldDate)
                            cmd.Parameters.AddWithValue("?in_hold_release_date", ldHoldExpireDate)
                            cmd.Parameters.AddWithValue("?in_status", lsStatus)
                            cmd.Parameters.AddWithValue("?in_remark", lsRemark)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function FolioFieldUpdation(FileName As String, SheetName As String, FileType As Integer, XLDispFldName As String, TbFldName As String, TbFldLen As Integer,
                                        Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing) As clsFileReturn
        Dim i As Integer
        Dim lsFldName(4) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsCompCode As String
        Dim lsFolioNo As String
        Dim lsFldValue As String

        Dim lsMsg As String

        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String

        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SNO"
        lsFldName(2) = "COMPANY"
        lsFldName(3) = "FOLIO NO"
        lsFldName(4) = XLDispFldName.ToUpper()

        Try
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 4
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 4
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", FileType)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            With lobjExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompCode = Mid(QuoteFilter(.Item("COMPANY").ToString), 1, 16)
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsFldValue = Mid(QuoteFilter(.Item(XLDispFldName).ToString), 1, TbFldLen)

                        Using cmd As New MySqlCommand("pr_sta_upd_foliofieldfile", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompCode)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_field_name", TbFldName)
                            cmd.Parameters.AddWithValue("?in_field_value", lsFldValue)

                            cmd.Parameters.AddWithValue("?in_line_no", i + 1)
                            cmd.Parameters.AddWithValue("?in_errline_flag", True)

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
                End While
            End With

            Call FileClose(1)
            frmMain.lblStatus.Text = ""

            lsTxt = "Out of " & i & " record(s) " & c & " record(s) imported successfully !"
            Call gpUpdateFileRemark(lnFileId, lsTxt)

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn
        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function DividendAccountMaster(ByVal FileName As String, ByVal SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim j As String

        Dim lsFldName(5) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsSlno As String
        Dim lsCompanyCode As String
        Dim lsFinyearCode As String
        Dim lsAccNo As String
        Dim lsBankCode As String
        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "FINYEAR CODE"
        lsFldName(4) = "ACC NO"
        lsFldName(5) = "BANK CODE"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 5
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 5
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnDividendAccountMst)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            Dim lsTxtFile As String = gsAsciiPath & "\Error.txt"

            With lobjExcelDatatable
                Dim message As String
                message = String.Empty
                i = 0
                j = "0"
                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompanyCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsFinyearCode = Mid(QuoteFilter(.Item("FINYEAR CODE").ToString), 1, 32)
                        lsAccNo = Mid(QuoteFilter(.Item("ACC NO").ToString), 1, 32)
                        lsBankCode = Mid(QuoteFilter(.Item("BANK CODE").ToString), 1, 8)

                        Using cmd As New MySqlCommand("pr_sta_ins_tdividend_acc_mst", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?In_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_comp_code", lsCompanyCode)
                            cmd.Parameters.AddWithValue("?In_finyear_code", lsFinyearCode)
                            cmd.Parameters.AddWithValue("?In_acc_no", lsAccNo)
                            cmd.Parameters.AddWithValue("?In_bank_code", lsBankCode)
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
                    j += 1
                    If lnResult = 0 Then
                        Using sw As New StreamWriter(lsTxtFile)
                            message += "Line: " + j + " ErrorMsg: " + lsMsg + Environment.NewLine
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
            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn

        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function DividendShareCapital(ByVal FileName As String, ByVal SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim J As String
        Dim lsFldName(36) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsSlno As String
        Dim lsCompanyCode As String
        Dim lsFinyearCode As String
        Dim lsFolioDpid As String
        Dim lsAccno As String
        Dim lsShareHolder As String
        Dim lsShareCount As Integer
        Dim lsDividendRate As Double
        Dim lsDividendAmt As Double
        Dim lsTdsPercent As Double
        Dim lsTdsAmt As Double
        Dim lsNetAmt As Double
        Dim lsForeignCurr As Double
        Dim lsDividendDate As Date
        Dim lsWarrantNo As String
        Dim lsDividendPayMode As String
        Dim lsDividendPayRefno As String
        Dim lsJoint1 As String
        Dim lsJoint2 As String
        Dim lsPanNo As String
        Dim lsEmailId As String
        Dim lsAddress1 As String
        Dim lsAddress2 As String
        Dim lsAddress3 As String
        Dim lsCity As String
        Dim lsState As String
        Dim lsCountry As String
        Dim lsPincode As String
        Dim lsBankName As String
        Dim lsBankBranch As String
        Dim lsBankAccNo As String
        Dim lsBankAccType As String
        Dim lsMicrCode As String
        Dim lsIFSCcode As String
        Dim lsCategory As String
        Dim lsRemarks As String

        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn


        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "FINYEAR CODE"
        lsFldName(4) = "ACC NO"
        lsFldName(5) = "FOLIO DPID"
        lsFldName(6) = "SHARE HOLDER"
        lsFldName(7) = "SHARE COUNT"
        lsFldName(8) = "DIVIDEND RATE"
        lsFldName(9) = "DIVIDEND AMOUNT"
        lsFldName(10) = "TDS PERCENTAGE"
        lsFldName(11) = "TDS AMOUNT"
        lsFldName(12) = "NET AMOUNT"
        lsFldName(13) = "FOREGIN CURRENCY"
        lsFldName(14) = "DIVIDEND DATE"
        lsFldName(15) = "WARRANT NO"
        lsFldName(16) = "DIVIDEND PAY MODE"
        lsFldName(17) = "DIVIDEND PAY REF NO"
        lsFldName(18) = "JOINT1"
        lsFldName(19) = "JOINT2"
        lsFldName(20) = "PAN NO"
        lsFldName(21) = "EMAIL ID"
        lsFldName(22) = "ADDR1"
        lsFldName(23) = "ADDR2"
        lsFldName(24) = "ADDR3"
        lsFldName(25) = "CITY"
        lsFldName(26) = "STATE"
        lsFldName(27) = "COUNTRY"
        lsFldName(28) = "PINCODE"
        lsFldName(29) = "BANK NAME"
        lsFldName(30) = "BANK BRANCH"
        lsFldName(31) = "BANK ACC NO"
        lsFldName(32) = "BANK ACC TYPE"
        lsFldName(33) = "BANK MICR CODE"
        lsFldName(34) = "BANK IFSC CODE"
        lsFldName(35) = "CATEGORY"
        lsFldName(36) = "REMARKS"


        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 36
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 36
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnDividendShareCapital)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            Dim lsTxtFile As String = gsAsciiPath & "\Error.txt"

            With lobjExcelDatatable
                Dim message As String
                message = String.Empty
                i = 0
                j = "0"

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompanyCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsFinyearCode = Mid(QuoteFilter(.Item("FINYEAR CODE").ToString), 1, 32)
                        lsAccno = Mid(QuoteFilter(.Item("ACC NO").ToString), 1, 32)
                        lsFolioDpid = Mid(QuoteFilter(.Item("FOLIO DPID").ToString), 1, 32)
                        lsShareHolder = Mid(QuoteFilter(.Item("SHARE HOLDER").ToString), 1, 128)
                        lsShareCount = Mid(QuoteFilter(.Item("SHARE COUNT").ToString), 1, 10)
                        lsDividendRate = Mid(QuoteFilter(.Item("DIVIDEND RATE").ToString), 1, 16)
                        lsDividendAmt = Mid(QuoteFilter(.Item("DIVIDEND AMOUNT").ToString), 1, 16)
                        lsTdsPercent = Mid(QuoteFilter(.Item("TDS PERCENTAGE").ToString), 1, 16)
                        lsTdsAmt = Mid(QuoteFilter(.Item("TDS AMOUNT").ToString), 1, 16)
                        lsNetAmt = Mid(QuoteFilter(.Item("NET AMOUNT").ToString), 1, 16)
                        lsForeignCurr = Mid(QuoteFilter(.Item("FOREGIN CURRENCY").ToString), 1, 16)
                        lsDividendDate = QuoteFilter(.Item("DIVIDEND DATE").ToString)
                        If IsDate(lsDividendDate) = False Then
                            lsDividendDate = "0001-01-01"
                        Else
                            lsDividendDate = Format(CDate(lsDividendDate), "yyyy-MM-dd")
                        End If
                        lsWarrantNo = Mid(QuoteFilter(.Item("WARRANT NO").ToString), 1, 32)
                        lsDividendPayMode = Mid(QuoteFilter(.Item("DIVIDEND PAY MODE").ToString), 1, 32)
                        lsDividendPayRefno = Mid(QuoteFilter(.Item("DIVIDEND PAY REF NO").ToString), 1, 32)
                        lsJoint1 = Mid(QuoteFilter(.Item("JOINT1").ToString), 1, 128)
                        lsJoint2 = Mid(QuoteFilter(.Item("JOINT2").ToString), 1, 128)
                        lsPanNo = Mid(QuoteFilter(.Item("PAN NO").ToString), 1, 128)
                        lsEmailId = Mid(QuoteFilter(.Item("EMAIL ID").ToString), 1, 128)
                        lsAddress1 = Mid(QuoteFilter(.Item("ADDR1").ToString), 1, 128)
                        lsAddress2 = Mid(QuoteFilter(.Item("ADDR2").ToString), 1, 128)
                        lsAddress3 = Mid(QuoteFilter(.Item("ADDR3").ToString), 1, 128)
                        lsCity = Mid(QuoteFilter(.Item("CITY").ToString), 1, 128)
                        lsState = Mid(QuoteFilter(.Item("STATE").ToString), 1, 128)
                        lsCountry = Mid(QuoteFilter(.Item("COUNTRY").ToString), 1, 128)
                        lsPincode = Mid(QuoteFilter(.Item("PINCODE").ToString), 1, 128)
                        lsBankName = Mid(QuoteFilter(.Item("BANK NAME").ToString), 1, 128)
                        lsBankBranch = Mid(QuoteFilter(.Item("BANK BRANCH").ToString), 1, 128)
                        lsBankAccNo = Mid(QuoteFilter(.Item("BANK ACC NO").ToString), 1, 32)
                        lsBankAccType = Mid(QuoteFilter(.Item("BANK ACC TYPE").ToString), 1, 128)
                        lsMicrCode = Mid(QuoteFilter(.Item("BANK MICR CODE").ToString), 1, 128)
                        lsIFSCcode = Mid(QuoteFilter(.Item("BANK IFSC CODE").ToString), 1, 128)
                        lsCategory = Mid(QuoteFilter(.Item("CATEGORY").ToString), 1, 128)
                        lsRemarks = Mid(QuoteFilter(.Item("REMARKS").ToString), 1, 128)

                        Using cmd As New MySqlCommand("pr_sta_ins_tdividend_div", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?In_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?In_comp_code", lsCompanyCode)
                            cmd.Parameters.AddWithValue("?In_finyear_code", lsFinyearCode)
                            cmd.Parameters.AddWithValue("?In_accno", lsAccno)
                            cmd.Parameters.AddWithValue("?In_folio_dpid", lsFolioDpid)
                            cmd.Parameters.AddWithValue("?In_share_holder", lsShareHolder)
                            cmd.Parameters.AddWithValue("?In_share_count", lsShareCount)
                            cmd.Parameters.AddWithValue("?In_div_rate", lsDividendRate)
                            cmd.Parameters.AddWithValue("?In_div_amount", lsDividendAmt)
                            cmd.Parameters.AddWithValue("?In_tds_percent", lsTdsPercent)
                            cmd.Parameters.AddWithValue("?In_tds_amount", lsTdsAmt)
                            cmd.Parameters.AddWithValue("?In_net_amount", lsNetAmt)
                            cmd.Parameters.AddWithValue("?In_curr_amount", lsForeignCurr)
                            cmd.Parameters.AddWithValue("?In_div_date", lsDividendDate)
                            cmd.Parameters.AddWithValue("?In_warrant_no", lsWarrantNo)
                            cmd.Parameters.AddWithValue("?In_div_paymode", lsDividendPayMode)
                            cmd.Parameters.AddWithValue("?In_div_payrefno", lsDividendPayRefno)
                            cmd.Parameters.AddWithValue("?In_joint1", lsJoint1)
                            cmd.Parameters.AddWithValue("?In_joint2", lsJoint2)
                            cmd.Parameters.AddWithValue("?In_holder1_pan", lsPanNo)
                            cmd.Parameters.AddWithValue("?In_holder1_email", lsEmailId)
                            cmd.Parameters.AddWithValue("?In_holder1_addr1", lsAddress1)
                            cmd.Parameters.AddWithValue("?In_holder1_addr2", lsAddress2)
                            cmd.Parameters.AddWithValue("?In_holder1_addr3", lsAddress3)
                            cmd.Parameters.AddWithValue("?In_holder1_city", lsCity)
                            cmd.Parameters.AddWithValue("?In_holder1_state", lsState)
                            cmd.Parameters.AddWithValue("?In_holder1_country", lsCountry)
                            cmd.Parameters.AddWithValue("?In_holder1_pincode", lsPincode)
                            cmd.Parameters.AddWithValue("?In_holder1_bank_name", lsBankName)
                            cmd.Parameters.AddWithValue("?In_holder1_bank_branch", lsBankBranch)
                            cmd.Parameters.AddWithValue("?In_holder1_acc_no", lsBankAccNo)
                            cmd.Parameters.AddWithValue("?In_holder1_acc_type", lsBankAccType)
                            cmd.Parameters.AddWithValue("?In_holder1_micr_code", lsMicrCode)
                            cmd.Parameters.AddWithValue("?In_holder1_ifsc_code", lsIFSCcode)
                            cmd.Parameters.AddWithValue("?In_holder1_category", lsCategory)
                            cmd.Parameters.AddWithValue("?In_div_remark", lsRemarks)

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
                    j += 1
                    If lnResult = 0 Then
                        Using sw As New StreamWriter(lsTxtFile)
                            Message += "Line: " + j + " ErrorMsg: " + lsMsg + Environment.NewLine
                            sw.WriteLine(Message)
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

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function DividendPayStatusUpdate(ByVal FileName As String, ByVal SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim J As String
        Dim lsFldName(16) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsSlno As String
        Dim lsCompanyCode As String
        Dim lsFinyearCode As String
        Dim lsAccNo As String
        Dim lsFolioDpid As String
        Dim lsShareHolder As String
        Dim lsShareCount As Integer
        Dim lsDividendAmt As Double
        Dim lsDividendStatus As String
        Dim lsIssueDate As String
        Dim lsIssuePayMode As String
        Dim lsIssuePayRefno As String
        Dim lsPaidDate As String
        Dim lsPaidModeStatus As String
        Dim lsPaidPayRefno As String
        Dim lsRejectReason As String

        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "FINYEAR CODE"
        lsFldName(4) = "ACC NO"
        lsFldName(5) = "FOLIO DPID"
        lsFldName(6) = "SHARE HOLDER"
        lsFldName(7) = "SHARE COUNT"
        lsFldName(8) = "DIVIDEND AMOUNT"
        lsFldName(9) = "DIVIDEND STATUS"
        lsFldName(10) = "ISSUE DATE"
        lsFldName(11) = "ISSUE PAYMODE"
        lsFldName(12) = "ISSUE PAY REFNO"
        lsFldName(13) = "PAID DATE"
        lsFldName(14) = "PAID MODE STATUS"
        lsFldName(15) = "PAID PAY REFNO"
        lsFldName(16) = "REJECT REASON"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 16
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 16
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnDividendPaystatusUpdate)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            Dim lsTxtFile As String = gsAsciiPath & "\Error.txt"

            With lobjExcelDatatable
                Dim message As String
                Dim lbInsertFlag As Boolean
                Dim lsErrMsg As String

                message = String.Empty
                i = 0
                J = "0"

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        lbInsertFlag = True
                        lsErrMsg = ""

                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompanyCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsFinyearCode = Mid(QuoteFilter(.Item("FINYEAR CODE").ToString), 1, 32)
                        lsAccNo = Mid(QuoteFilter(.Item("ACC NO").ToString), 1, 32)
                        lsFolioDpid = Mid(QuoteFilter(.Item("FOLIO DPID").ToString), 1, 32)
                        lsShareHolder = Mid(QuoteFilter(.Item("SHARE HOLDER").ToString), 1, 128)
                        lsShareCount = Mid(QuoteFilter(.Item("SHARE COUNT").ToString), 1, 10)
                        lsDividendAmt = Mid(QuoteFilter(.Item("DIVIDEND AMOUNT").ToString), 1, 16)
                        lsDividendStatus = Mid(QuoteFilter(.Item("DIVIDEND STATUS").ToString), 1, 1)
                        If lsDividendStatus = "U" Or lsDividendStatus = "P" Or lsDividendStatus = "L" Then
                            lsDividendStatus = lsDividendStatus
                        Else
                            lsDividendStatus = "I"
                        End If

                        lsIssueDate = QuoteFilter(.Item("ISSUE DATE").ToString)

                        If IsDate(lsIssueDate) = False Then
                            lbInsertFlag = False
                            lsErrMsg = "Issue Date : " & lsIssueDate & ","
                        Else
                            lsIssueDate = Format(CDate(lsIssueDate), "yyyy-MM-dd")
                        End If

                        lsIssuePayMode = Mid(QuoteFilter(.Item("ISSUE PAYMODE").ToString), 1, 16)
                        lsIssuePayRefno = Mid(QuoteFilter(.Item("ISSUE PAY REFNO").ToString), 1, 32)
                        lsPaidDate = QuoteFilter(.Item("PAID DATE").ToString)

                        If IsDate(lsPaidDate) = False Then
                            lbInsertFlag = False
                            lsErrMsg = "Paid Date : " & lsPaidDate & ","
                        Else
                            lsPaidDate = Format(CDate(lsPaidDate), "yyyy-MM-dd")
                        End If

                        lsPaidModeStatus = Mid(QuoteFilter(.Item("PAID MODE STATUS").ToString), 1, 1)
                        lsPaidPayRefno = Mid(QuoteFilter(.Item("PAID PAY REFNO").ToString), 1, 32)
                        lsRejectReason = Mid(QuoteFilter(.Item("REJECT REASON").ToString), 1, 255)

                        If lbInsertFlag = True Then
                            Using cmd As New MySqlCommand("pr_sta_ins_tdivpayment_statusupdate", gOdbcConn)
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.AddWithValue("?In_file_gid", lnFileId)
                                cmd.Parameters.AddWithValue("?In_comp_code", lsCompanyCode)
                                cmd.Parameters.AddWithValue("?In_finyear_code", lsFinyearCode)
                                cmd.Parameters.AddWithValue("?In_acc_no", lsAccNo)
                                cmd.Parameters.AddWithValue("?In_folio_dpid", lsFolioDpid)
                                cmd.Parameters.AddWithValue("?In_share_holder", lsShareHolder)
                                cmd.Parameters.AddWithValue("?In_share_count", lsShareCount)
                                cmd.Parameters.AddWithValue("?In_div_amount", lsDividendAmt)
                                cmd.Parameters.AddWithValue("?In_div_status", lsDividendStatus)
                                cmd.Parameters.AddWithValue("?In_issue_date", lsIssueDate)
                                cmd.Parameters.AddWithValue("?In_issue_paymode", lsIssuePayMode)
                                cmd.Parameters.AddWithValue("?In_issue_payrefno", lsIssuePayRefno)
                                cmd.Parameters.AddWithValue("?In_paid_date", lsPaidDate)
                                cmd.Parameters.AddWithValue("?In_paid_status", lsPaidModeStatus)
                                cmd.Parameters.AddWithValue("?In_paid_payrefno", lsPaidPayRefno)
                                cmd.Parameters.AddWithValue("?In_reject_remark", lsRejectReason)

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
                        Else
                            lsMsg = lsErrMsg
                            lnResult = 0
                        End If
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

            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn


        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function LossofShares(ByVal FileName As String, ByVal SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim j As String

        Dim lsFldName(7) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsSlno As String
        Dim lsCompanyCode As String
        Dim lsDocumentType As String
        Dim lsReceivedDate As String
        Dim lsFolioNo As String
        Dim lsShareCount As Integer
        Dim lsRemarks As String
        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "DOCUMENT TYPE CODE"
        lsFldName(4) = "RECEIVED DATE"
        lsFldName(5) = "FOLIO NO"
        lsFldName(6) = "SHARE COUNT"
        lsFldName(7) = "REMARKS"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 5
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 5
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnDividendAccountMst)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            Dim lsTxtFile As String = gsAsciiPath & "\Error.txt"

            With lobjExcelDatatable
                Dim message As String
                message = String.Empty
                i = 0
                j = "0"
                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompanyCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsDocumentType = Mid(QuoteFilter(.Item("DOCUMENT TYPE CODE").ToString), 1, 16)
                        lsReceivedDate = QuoteFilter(.Item("RECEIVED DATE").ToString)
                        If IsDate(lsReceivedDate) = False Then
                            lsReceivedDate = "0001-01-01"
                        Else
                            lsReceivedDate = Format(CDate(lsReceivedDate), "yyyy-MM-dd")
                        End If
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsShareCount = Convert.ToInt32(Mid(QuoteFilter(.Item("SHARE COUNT").ToString), 1, 10))
                        lsRemarks = Mid(QuoteFilter(.Item("REMARKS").ToString), 1, 255)

                        Using cmd As New MySqlCommand("pr_sta_bulk_tinward_ls", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_inward_gid", 0)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompanyCode)
                            cmd.Parameters.AddWithValue("?in_inward_no", 0)
                            cmd.Parameters.AddWithValue("?in_received_date", lsReceivedDate)
                            cmd.Parameters.AddWithValue("?in_tran_code", lsDocumentType)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_shareholder_name", "")
                            cmd.Parameters.AddWithValue("?in_shareholder_pan_no", "")
                            cmd.Parameters.AddWithValue("?in_share_count", lsShareCount)
                            cmd.Parameters.AddWithValue("?in_inward_remark", lsRemarks)
                            cmd.Parameters.AddWithValue("?in_action", "INSERT")
                            cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
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
                    j += 1
                    If lnResult = 0 Then
                        Using sw As New StreamWriter(lsTxtFile)
                            message += "Line: " + j + " ErrorMsg: " + lsMsg + Environment.NewLine
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
            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn

        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Public Function Transmission(ByVal FileName As String, ByVal SheetName As String, Optional ShowFlag As Boolean = True, Optional LsvItem As ListViewItem = Nothing)
        Dim i As Integer
        Dim j As String

        Dim lsFldName(9) As String
        Dim lsFldFormat As String = ""
        Dim ds As New DataSet
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lobjExcelDatatable As New DataTable

        Dim lsSlno As String
        Dim lsCompanyCode As String
        Dim lsDocumentType As String
        Dim lsReceivedDate As String
        Dim lsFolioNo As String
        Dim lsNewFolioNo As String
        Dim lsCertNo As Integer
        Dim lsShareCount As Integer
        Dim lsRemarks As String
        Dim lsMsg As String
        Dim lsFileName As String = ""
        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lnResult As Long
        Dim lobjFileReturn As New clsFileReturn

        lsFldName(1) = "SL NO"
        lsFldName(2) = "COMPANY CODE"
        lsFldName(3) = "DOCUMENT TYPE CODE"
        lsFldName(4) = "RECEIVED DATE"
        lsFldName(5) = "FOLIO NO"
        lsFldName(6) = "NEW FOLIO NO"
        lsFldName(7) = "CERTIFICATE NO"
        lsFldName(8) = "SHARE COUNT"
        lsFldName(9) = "REMARKS"

        Try
            Call FormatSheet(FileName, SheetName)
            lsFileName = QuoteFilter(FileName.Substring(FileName.LastIndexOf("\") + 1))

            '---------------------------------
            lobjExcelDatatable = gpExcelDataset("select * from [" & SheetName & "$]", FileName)

            For i = 1 To 5
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 5
                If lsFldName(i).Trim <> lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    lsMsg = "Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & lobjExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat

                    If ShowFlag Then MsgBox(lsMsg, vbOKOnly + vbExclamation, gsProjectName)

                    lobjFileReturn.Result = 0
                    lobjFileReturn.Msg = lsMsg

                    Return lobjFileReturn
                End If
            Next

            Using cmd As New MySqlCommand("pr_sta_ins_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                cmd.Parameters("?in_file_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_sheet_name", QuoteFilter(SheetName))
                cmd.Parameters("?in_sheet_name").Direction = ParameterDirection.Input
                cmd.Parameters.AddWithValue("?in_file_type", gnDividendAccountMst)
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

                    Return lobjFileReturn
                End If

                lnFileId = Val(cmd.Parameters("?out_file_gid").Value.ToString())
            End Using

            Dim lsTxtFile As String = gsAsciiPath & "\Error.txt"

            With lobjExcelDatatable
                Dim message As String
                message = String.Empty
                i = 0
                j = "0"
                While i <= .Rows.Count - 1
                    With .Rows(i)
                        If LsvItem Is Nothing Then
                            frmMain.lblStatus.Text = "Reading " & i.ToString & " line..."
                        Else
                            LsvItem.Text = "Reading " & i.ToString & " line..."
                        End If

                        Application.DoEvents()

                        lsCompanyCode = Mid(QuoteFilter(.Item("COMPANY CODE").ToString), 1, 16)
                        lsDocumentType = Mid(QuoteFilter(.Item("DOCUMENT TYPE CODE").ToString), 1, 16)
                        lsReceivedDate = QuoteFilter(.Item("RECEIVED DATE").ToString)
                        If IsDate(lsReceivedDate) = False Then
                            lsReceivedDate = "0001-01-01"
                        Else
                            lsReceivedDate = Format(CDate(lsReceivedDate), "yyyy-MM-dd")
                        End If
                        lsFolioNo = Mid(QuoteFilter(.Item("FOLIO NO").ToString), 1, 32)
                        lsNewFolioNo = Mid(QuoteFilter(.Item("NEW FOLIO NO").ToString), 1, 32)
                        lsShareCount = Convert.ToInt32(Mid(QuoteFilter(.Item("SHARE COUNT").ToString), 1, 10))
                        lsCertNo = Convert.ToInt32(Mid(QuoteFilter(.Item("CERTIFICATE NO").ToString), 1, 10))
                        lsRemarks = Mid(QuoteFilter(.Item("REMARKS").ToString), 1, 255)

                        Using cmd As New MySqlCommand("pr_sta_bulk_tinward_tm", gOdbcConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                            cmd.Parameters.AddWithValue("?in_inward_gid", 0)
                            cmd.Parameters.AddWithValue("?in_comp_code", lsCompanyCode)
                            cmd.Parameters.AddWithValue("?in_inward_no", 0)
                            cmd.Parameters.AddWithValue("?in_received_date", lsReceivedDate)
                            cmd.Parameters.AddWithValue("?in_tran_code", lsDocumentType)
                            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                            cmd.Parameters.AddWithValue("?in_newfolio_no", lsNewFolioNo)
                            cmd.Parameters.AddWithValue("?in_cert_no", lsCertNo)
                            cmd.Parameters.AddWithValue("?in_shareholder_name", "")
                            cmd.Parameters.AddWithValue("?in_shareholder_pan_no", "")
                            cmd.Parameters.AddWithValue("?in_share_count", lsShareCount)
                            cmd.Parameters.AddWithValue("?in_inward_remark", lsRemarks)
                            cmd.Parameters.AddWithValue("?in_action", "INSERT")
                            cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
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
                    j += 1
                    If lnResult = 0 Then
                        Using sw As New StreamWriter(lsTxtFile)
                            message += "Line: " + j + " ErrorMsg: " + lsMsg + Environment.NewLine
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
            If ShowFlag Then MsgBox(lsTxt, MsgBoxStyle.Information, gsProjectName)

            lobjFileReturn.Result = 1
            lobjFileReturn.Msg = lsTxt

            Return lobjFileReturn

        Catch ex As Exception
            If ShowFlag Then MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, gsProjectName)

            lobjFileReturn.Result = 0
            lobjFileReturn.Msg = ex.Message.ToString

            Return lobjFileReturn
        End Try
    End Function

    Private Sub FormatSheet(ByVal ExcelFileName As String, ByVal SheetName As String)
        Dim objApplication As New Excel.Application
        Dim objBooks As Excel.Workbooks
        Dim objWorkBook As Excel.Workbook
        Dim objWorkSheet As Excel.Worksheet
        Dim objRange As Excel.Range
        Dim a() As Short = {1, 2}
        Dim b() As Short = {1, 1}
        Dim i As Integer

        Try
            If File.Exists(ExcelFileName) = False Then Exit Sub

            objBooks = objApplication.Workbooks
            objWorkBook = objBooks.Open(ExcelFileName, False, False)
            objWorkSheet = objWorkBook.Sheets(SheetName)

            objApplication.Visible = True

            'For i = 1 To 256
            '    If objWorkSheet.Cells(1, i).Value <> "" Then
            '        Select Case objWorkSheet.Cells(2, i).NumberFormat
            '            Case "@", "General", "0", "0.00"
            '                objWorkSheet.Columns(i).cells.NumberFormat = "@"

            '                objRange = objWorkSheet.Columns(i)
            '                objRange.TextToColumns(objRange, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote, , , , , , , , a, , )
            '        End Select
            '    Else
            '        Exit For
            '    End If
            'Next i

            For i = 1 To 256
                If objWorkSheet.Cells(1, i).Value <> "" Then
                    objRange = objWorkSheet.Columns(i)

                    If IsDate(objWorkSheet.Cells(2, i).Value) Then
                        objRange.TextToColumns(objRange, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote, , , , , , , , b, , )
                    Else
                        objRange.TextToColumns(objRange, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote, , , , , , , , a, , )
                    End If
                Else
                    Exit For
                End If
            Next i

            objWorkBook.Save()
            objWorkBook.Close()
            objBooks.Close()

            objApplication.Quit()

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(objApplication)

            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error...")
        End Try
    End Sub
End Class
