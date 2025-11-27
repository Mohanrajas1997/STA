Imports System.IO
Imports System.IO.FileStream

Public Class clsUpload
    Dim mnPageNo As Integer
    Dim mnPageWidth As Integer
    Const mnPageRows As Integer = 60
    Dim mnLine As Integer
    Dim msTxt As String

    Dim msColTRHead1(9) As String
    Dim msColTRHead2(9) As String
    Dim mnColTRAlign(9) As Integer
    Dim mnColTRWidth(9) As Integer

    Dim msColCRHead1(11) As String
    Dim msColCRHead2(11) As String
    Dim mnColCRAlign(11) As Integer
    Dim mnColCRWidth(11) As Integer

    Dim msCompName As String = ""
    Dim mnUploadType As Integer = 0

    Public Sub NSDLUpload(UploadId As Long)
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnRecCount As Long
        Dim lnValidCount As Long
        Dim lnInvalidCount As Long
        Dim lsDematPendRejectCode As String = ""
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim lnLineNo As Long = 0

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.nsdl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " c.nsdl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnUploadNSDLUpload & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\NSDL"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("nsdl_sno").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' total record count
            lsSql = ""
            lsSql &= " select count(*) from sta_trn_tinward "
            lsSql &= " where upload_gid = " & UploadId & " "
            lsSql &= " and delete_flag = 'N' "

            lnRecCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            ' valid count
            'lsSql = ""
            'lsSql &= " select count(*) from sta_trn_tinward "
            'lsSql &= " where upload_gid = " & UploadId & " "
            'lsSql &= " and chklst_disc = 0 "
            'lsSql &= " and delete_flag = 'N' "

            lsSql = ""
            lsSql &= " select sum(c.share_count) from sta_trn_tinward as a "
            lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcert as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.chklst_disc = 0 "
            lsSql &= " and a.delete_flag = 'N' "

            lnValidCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            ' reject count
            'lsSql = ""
            'lsSql &= " select count(*) from sta_trn_tinward "
            'lsSql &= " where upload_gid = " & UploadId & " "
            'lsSql &= " and chklst_disc > 0 "
            'lsSql &= " and delete_flag = 'N' "

            lsSql = ""
            lsSql &= " select sum(c.share_count) from sta_trn_tinward as a "
            lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcert as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.chklst_disc > 0 "
            lsSql &= " and a.delete_flag = 'N' "

            lnInvalidCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            ' header
            lsTxt = ""
            lsTxt &= AlignTxt(Format(ds.Tables("upload").Rows(0).Item("nsdl_sno"), "00000000"), 8, 1)
            lsTxt &= AlignTxt("01", 2, 1)
            lsTxt &= AlignTxt("", 6, 1)
            lsTxt &= AlignTxt(ds.Tables("upload").Rows(0).Item("nsdl_dp_id").ToString, 8, 1)
            lsTxt &= AlignTxt("04", 2, 1)
            lsTxt &= AlignTxt(Format(lnValidCount, StrDup(15, "0")), 15, 1)
            lsTxt &= AlignTxt("000", 3, 1)
            lsTxt &= AlignTxt(Format(lnInvalidCount, StrDup(15, "0")), 15, 1)
            lsTxt &= AlignTxt("000", 3, 1)
            lsTxt &= AlignTxt(Format(0, StrDup(18, "0")), 18, 1)
            lsTxt &= AlignTxt(Format(0, StrDup(18, "0")), 18, 1)
            lsTxt &= AlignTxt(Format(0, StrDup(18, "0")), 18, 1)
            lsTxt &= AlignTxt(Format(0, StrDup(18, "0")), 18, 1)
            lsTxt &= AlignTxt(Format(lnRecCount, StrDup(7, "0")), 7, 1)
            lsTxt &= Format(Now, "yyyyMMdd")
            lsTxt &= AlignTxt(ds.Tables("upload").Rows(0).Item("upload_by").ToString, 12, 1)

            Call Print(1, lsTxt)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.inward_comp_no as 'inward_no',"
            'lsSql &= " a.inward_no,"
            lsSql &= " a.received_date,"
            lsSql &= " a.folio_no,"
            lsSql &= " a.shareholder_name,"
            lsSql &= " d.isin_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.drn_no,"
            lsSql &= " e.dp_id,"
            lsSql &= " a.chklst_disc,"
            lsSql &= " a.dematpend_reject_code,"
            lsSql &= " count(*) as cnt_ranges,"
            lsSql &= " sum(if(a.chklst_disc = 0,c.dist_count,0)) as tot_valid_shares,"
            lsSql &= " sum(if(a.chklst_disc > 0,c.dist_count,0)) as tot_rejected_shares "
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tdematpend as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.inward_comp_no,"
            lsSql &= " a.received_date,"
            lsSql &= " a.folio_no,"
            lsSql &= " a.shareholder_name,"
            lsSql &= " d.isin_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.drn_no,"
            lsSql &= " e.dp_id,"
            lsSql &= " a.dematpend_reject_code"

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            For i = 0 To ds.Tables("header").Rows.Count - 1
                lnLineNo += 1
                lsDematPendRejectCode = ds.Tables("header").Rows(i).Item("dematpend_reject_code").ToString

                lsTxt = vbNewLine
                lsTxt &= AlignTxt(Format(ds.Tables("upload").Rows(0).Item("nsdl_sno"), "00000000"), 8, 1)
                lsTxt &= AlignTxt("02", 2, 1)
                lsTxt &= AlignTxt(Format(lnLineNo, "0000000"), 7, 1)
                lsTxt &= AlignTxt("801", 3, 1)
                lsTxt &= AlignTxt("A", 1, 1)
                lsTxt &= AlignTxt(ds.Tables("header").Rows(i).Item("dp_id").ToString, 8, 1)
                lsTxt &= AlignTxt(ds.Tables("header").Rows(i).Item("drn_no").ToString, 14, 1)
                lsTxt &= AlignTxt(Format(ds.Tables("header").Rows(i).Item("received_date"), "yyyyMMdd"), 8, 1)
                lsTxt &= AlignTxt(Format(ds.Tables("header").Rows(i).Item("tot_valid_shares"), StrDup(15, "0")), 15, 1)
                lsTxt &= AlignTxt("000", 3, 1)
                lsTxt &= AlignTxt(Format(ds.Tables("header").Rows(i).Item("tot_rejected_shares"), StrDup(15, "0")), 15, 1)
                lsTxt &= AlignTxt("000", 3, 1)
                lsTxt &= AlignTxt("00", 2, 1)
                lsTxt &= AlignTxt("", 8, 1)
                lsTxt &= AlignTxt(ds.Tables("header").Rows(i).Item("inward_no").ToString, 35, 7)

                If lsDematPendRejectCode = "" Then
                    lsTxt &= AlignTxt("0000", 4, 1)
                    lsTxt &= AlignTxt("0000", 4, 1)
                    lsTxt &= AlignTxt("0000", 4, 1)
                    lsTxt &= AlignTxt("0000", 4, 1)
                Else
                    For k = 0 To 3
                        If (k + 1) <= lsDematPendRejectCode.Split(",").Length Then
                            lsTxt &= AlignTxt(Format(Val(lsDematPendRejectCode.Split(",")(k)), "0000"), 4, 1)
                        Else
                            lsTxt &= AlignTxt("0000", 4, 1)
                        End If
                    Next k
                End If

                lsTxt &= AlignTxt(ds.Tables("header").Rows(i).Item("folio_no").ToString, 50, 7)
                lsTxt &= AlignTxt(ds.Tables("header").Rows(i).Item("shareholder_name").ToString, 50, 7)

                lsTxt &= AlignTxt("", 1, 1)
                lsTxt &= AlignTxt("", 1, 1)
                lsTxt &= AlignTxt("", 1, 1)

                Call Print(1, lsTxt)

                lsSql = ""
                lsSql &= " select "
                lsSql &= " c.dist_from,"
                lsSql &= " c.dist_to,"
                lsSql &= " c.dist_count, "
                lsSql &= " d.folio_no,"
                lsSql &= " e.cert_no,"
                lsSql &= " a.namechangeref_flag,"
                lsSql &= " a.namechageref_code"
                lsSql &= " from sta_trn_tinward as a "
                lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tfolio as d on a.folio_gid=d.folio_gid and d.delete_flag='N'"
                lsSql &= " inner join sta_trn_tcert as e on b.cert_gid=e.cert_gid and e.delete_flag='N'"
                lsSql &= " where a.inward_gid = " & ds.Tables("header").Rows(i).Item("inward_gid").ToString & " "
                lsSql &= " and a.chklst_disc = 0 "
                lsSql &= " and a.delete_flag = 'N' "
                lsSql &= " order by c.dist_from "

                Call gpDataSet(lsSql, "range", gOdbcConn, ds)

                For j = 0 To ds.Tables("range").Rows.Count - 1
                    lnLineNo += 1

                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt(Format(ds.Tables("upload").Rows(0).Item("nsdl_sno"), "00000000"), 8, 1)
                    lsTxt &= AlignTxt("03", 2, 1)
                    lsTxt &= AlignTxt(Format(lnLineNo, "0000000"), 7, 1)
                    lsTxt &= AlignTxt("801", 3, 1)
                    lsTxt &= AlignTxt("A", 1, 1)
                    lsTxt &= AlignTxt(ds.Tables("header").Rows(i).Item("dp_id").ToString, 8, 1)
                    lsTxt &= AlignTxt(ds.Tables("header").Rows(i).Item("drn_no").ToString, 14, 1)
                    lsTxt &= AlignTxt(Format(ds.Tables("range").Rows(j).Item("dist_from"), StrDup(18, "0")), 18, 1)
                    lsTxt &= AlignTxt(Format(ds.Tables("range").Rows(j).Item("dist_to"), StrDup(18, "0")), 18, 1)
                    lsTxt &= AlignTxt(Format(ds.Tables("range").Rows(j).Item("dist_count"), StrDup(18, "0")), 18, 1)

                    lsTxt &= AlignTxt(ds.Tables("range").Rows(j).Item("folio_no").ToString, 20, 1)
                    lsTxt &= AlignTxt(ds.Tables("range").Rows(j).Item("cert_no").ToString, 18, 1)

                    If ds.Tables("range").Rows(j).Item("namechangeref_flag").ToString = "Y" Then
                        lsTxt &= AlignTxt(ds.Tables("range").Rows(j).Item("namechageref_code").ToString, 2, 1)
                        If ds.Tables("range").Rows(j).Item("namechageref_code").ToString = "99" Then
                            lsTxt &= AlignTxt(ds.Tables("range").Rows(j).Item("namechageref_code").ToString, 50, 1)
                        Else
                            lsTxt &= AlignTxt("", 50, 1)
                        End If
                    Else
                        lsTxt &= AlignTxt("", 2, 1)
                        lsTxt &= AlignTxt("", 50, 1)
                    End If

                    lsTxt &= AlignTxt("", 13, 1)

                    Call Print(1, lsTxt)
                Next j

                ds.Tables("range").Rows.Clear()
            Next i

            ds.Tables("header").Rows.Clear()

            Call FileClose(1)

            Call gpOpenFile(lsFilePath)
        End If

        ds.Tables("upload").Rows.Clear()
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

    Public Sub CDSLUpload(UploadId As Long)
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnCount As Long

        Dim lsFilePath As String
        Dim lsFileName As String
        'Dim lsCompListedFlag As String
        Dim lsDematPendRejectCode As String
        Dim lsDematPendRejectDesc As String
        Dim lsrejcode As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.cdsl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " b.comp_listed,"
        lsSql &= " c.cdsl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnUploadCDSLUpload & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\CDSL"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("cdsl_sno").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            lsSql = ""
            lsSql &= " select count(*) from sta_trn_tinward "
            lsSql &= " where upload_gid = " & UploadId & " "
            lsSql &= " and delete_flag = 'N' "

            lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            If ds.Tables("upload").Rows(0).Item("comp_listed").ToString = "Y" Then
                lsSql = ""
                lsSql &= " select count(*) from sta_trn_tinward as a "
                lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
                lsSql &= " where a.upload_gid = " & UploadId & " "
                lsSql &= " and a.delete_flag = 'N' "

                lnCount += Val(gfExecuteScalar(lsSql, gOdbcConn))
            End If

            ' header
            lsTxt = ""
            lsTxt &= ds.Tables("upload").Rows(0).Item("cdsl_dp_id").ToString
            lsTxt &= "RTAADM"
            lsTxt &= Format(lnCount, "000000")
            lsTxt &= ds.Tables("upload").Rows(0).Item("upload_filename_extension").ToString
            lsTxt &= Format(Now, "ddMMyyyy")

            Call Print(1, lsTxt)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.received_date,"
            lsSql &= " d.isin_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.drn_no,"
            lsSql &= " a.inward_comp_no as dp_id,"
            'lsSql &= " e.dp_id,"
            lsSql &= " count(*) as cnt_ranges,"
            lsSql &= " sum(dist_count) as tot_shares, "
            lsSql &= " a.namechangeref_flag,"
            lsSql &= " a.namechageref_code,"
            lsSql &= " a.chklst_disc,"
            lsSql &= " a.dematpend_reject_code"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tdematpend as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.received_date,"
            lsSql &= " d.isin_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.drn_no,"
            lsSql &= " e.dp_id "

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            For i = 0 To ds.Tables("header").Rows.Count - 1
                lsDematPendRejectDesc = ""
                lsDematPendRejectCode = ""
                lsDematPendRejectCode = ds.Tables("header").Rows(i).Item("dematpend_reject_code").ToString
                If lsDematPendRejectCode <> "" Then
                    lsSql = ""
                    lsSql &= " select group_concat(dematreject_desc) from sta_mst_tdematreject "
                    lsSql &= " where dematreject_code in (" & lsDematPendRejectCode & ")"
                    lsSql &= " and depository_code = 'C' and delete_flag = 'N' "

                    lsDematPendRejectDesc = gfExecuteScalar(lsSql, gOdbcConn)
                End If

                lsTxt = Chr(10)
                lsTxt &= "<Tp>2</Tp>"
                lsTxt &= "<Bnfcry>" & ds.Tables("header").Rows(i).Item("client_id").ToString & "</Bnfcry>"
                lsTxt &= "<ISIN>" & ds.Tables("header").Rows(i).Item("isin_id").ToString & "</ISIN>"
                lsTxt &= "<Drn>" & ds.Tables("header").Rows(i).Item("drn_no").ToString & "</Drn>"
                lsTxt &= "<Qty>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Qty>"
                lsTxt &= "<Ref>" & ds.Tables("header").Rows(i).Item("dp_id").ToString & "</Ref>"
                lsTxt &= "<Rcvdt>" & Format(ds.Tables("header").Rows(i).Item("received_date"), "ddMMyyyy") & "</Rcvdt>"
                'Changes done by Mohan on 24th may 2023 
                'Rejection tags
                If ds.Tables("header").Rows(i).Item("chklst_disc").ToString = 0 Then
                    lsTxt &= "<Veraccpt>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Veraccpt>"
                    lsTxt &= "<Verrjct></Verrjct>"
                    lsTxt &= "<Accpt>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Accpt>"
                    lsTxt &= "<Rjct></Rjct>"
                    lsTxt &= "<Rej>"
                    lsTxt &= "<Flg></Flg>"
                    lsTxt &= "<Cd></Cd>"
                    lsTxt &= "<Prtqty></Prtqty>"
                    lsTxt &= "<Rmk></Rmk>"
                    lsTxt &= "</Rej>"
                Else
                    lsTxt &= "<Veraccpt>0</Veraccpt>"
                    lsTxt &= "<Verrjct>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Verrjct>"
                    lsTxt &= "<Accpt>0</Accpt>"
                    'lsTxt &= "<Rjct>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Rjct>"
                    lsTxt &= "<Rjct></Rjct>"
                    lsTxt &= "<Rej>"
                    lsTxt &= "<Flg>V</Flg>"
                    If lsDematPendRejectCode = "" Then
                        lsTxt &= "<Cd></Cd>"
                    Else
                        For k = 0 To 5
                            If (k + 1) <= lsDematPendRejectCode.Split(",").Length Then
                                lsrejcode = Val(lsDematPendRejectCode.Split(",")(k))
                                lsTxt &= "<Cd>" & lsrejcode & "</Cd>"
                            End If
                        Next k
                    End If
                    lsTxt &= "<Prtqty>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Prtqty>"
                    lsTxt &= "<Rmk>" & lsDematPendRejectDesc & "</Rmk>"
                    lsTxt &= "</Rej>"
                End If


                If ds.Tables("upload").Rows(0).Item("comp_listed").ToString = "Y" Then
                    lsTxt &= "<Ranges>" & ds.Tables("header").Rows(i).Item("cnt_ranges").ToString & "</Ranges>"

                    If ds.Tables("header").Rows(i).Item("namechangeref_flag").ToString = "Y" Then
                        lsTxt &= "<DocTyp>" & ds.Tables("header").Rows(i).Item("namechageref_code").ToString & "</DocTyp>"
                    Else
                        lsTxt &= "<DocTyp>01</DocTyp>"
                    End If
                End If


                Call Print(1, lsTxt)

                If ds.Tables("upload").Rows(0).Item("comp_listed").ToString = "Y" Then
                    lsSql = ""
                    lsSql &= " select "
                    lsSql &= " c.dist_from,"
                    lsSql &= " c.dist_to, "
                    lsSql &= " d.folio_no,"
                    lsSql &= " e.cert_no,"
                    lsSql &= " a.chklst_disc"
                    lsSql &= " from sta_trn_tinward as a "
                    lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
                    lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
                    lsSql &= " inner join sta_trn_tfolio as d on a.folio_gid=d.folio_gid and d.delete_flag='N' "
                    lsSql &= " inner join  sta_trn_tcert as e on b.cert_gid=e.cert_gid and e.delete_flag='N' "
                    lsSql &= " where a.inward_gid = " & ds.Tables("header").Rows(i).Item("inward_gid").ToString & " "
                    lsSql &= " and a.delete_flag = 'N' "
                    lsSql &= " order by c.dist_from "

                    Call gpDataSet(lsSql, "range", gOdbcConn, ds)

                    For j = 0 To ds.Tables("range").Rows.Count - 1
                        If ds.Tables("range").Rows(j).Item("chklst_disc").ToString = 0 Then
                            lsTxt = Chr(10)
                            lsTxt &= "<Rngs>" & (j + 1).ToString & "</Rngs>"
                            lsTxt &= "<DNFrm>" & ds.Tables("range").Rows(j).Item("dist_from").ToString & "</DNFrm>"
                            lsTxt &= "<DNTo>" & ds.Tables("range").Rows(j).Item("dist_to").ToString & "</DNTo>"
                            'Murali Changes on 09-03-2020
                            lsTxt &= "<Folio>" & ds.Tables("range").Rows(j).Item("folio_no").ToString & "</Folio>"
                            lsTxt &= "<CertFrm>" & ds.Tables("range").Rows(j).Item("cert_no").ToString & "</CertFrm>"
                            lsTxt &= "<CertTo>" & ds.Tables("range").Rows(j).Item("cert_no").ToString & "</CertTo>"
                            lsTxt &= "<RngStts>A</RngStts>"

                            Call Print(1, lsTxt)
                            'Mohan Changes on 20-05-2025
                        Else
                            lsTxt = Chr(10)
                            lsTxt &= "<Rngs>" & (j + 1).ToString & "</Rngs>"
                            lsTxt &= "<DNFrm>" & ds.Tables("range").Rows(j).Item("dist_from").ToString & "</DNFrm>"
                            lsTxt &= "<DNTo>" & ds.Tables("range").Rows(j).Item("dist_to").ToString & "</DNTo>"
                            lsTxt &= "<Folio>" & ds.Tables("range").Rows(j).Item("folio_no").ToString & "</Folio>"
                            lsTxt &= "<CertFrm>" & ds.Tables("range").Rows(j).Item("dist_from").ToString & "</CertFrm>"
                            lsTxt &= "<CertTo>" & ds.Tables("range").Rows(j).Item("dist_to").ToString & "</CertTo>"
                            lsTxt &= "<RngStts>R</RngStts>"

                            Call Print(1, lsTxt)
                        End If
                    Next j

                    ds.Tables("range").Rows.Clear()
                End If
            Next i

            ds.Tables("header").Rows.Clear()

            Call Print(1, Chr(10))

            Call FileClose(1)

            Call gpOpenFile(lsFilePath)
        End If

        ds.Tables("upload").Rows.Clear()
    End Sub

    Public Sub CDSLUploadold(UploadId As Long)
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnCount As Long

        Dim lsFilePath As String
        Dim lsFileName As String
        Dim lsCompListedFlag As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.cdsl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " b.comp_listed,"
        lsSql &= " c.cdsl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnUploadCDSLUpload & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\CDSL"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("cdsl_sno").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            lsSql = ""
            lsSql &= " select count(*) from sta_trn_tinward "
            lsSql &= " where upload_gid = " & UploadId & " "
            lsSql &= " and delete_flag = 'N' "

            lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            If ds.Tables("upload").Rows(0).Item("comp_listed").ToString = "Y" Then
                lsSql = ""
                lsSql &= " select count(*) from sta_trn_tinward as a "
                lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
                lsSql &= " where a.upload_gid = " & UploadId & " "
                lsSql &= " and a.delete_flag = 'N' "

                lnCount += Val(gfExecuteScalar(lsSql, gOdbcConn))
            End If

            ' header
            lsTxt = ""
            lsTxt &= ds.Tables("upload").Rows(0).Item("cdsl_dp_id").ToString
            lsTxt &= "RTAADM"
            lsTxt &= Format(lnCount, "000000")
            lsTxt &= ds.Tables("upload").Rows(0).Item("upload_filename_extension").ToString
            lsTxt &= Format(Now, "ddMMyyyy")

            Call Print(1, lsTxt)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.received_date,"
            lsSql &= " d.isin_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.drn_no,"
            lsSql &= " a.inward_comp_no as dp_id,"
            'lsSql &= " e.dp_id,"
            lsSql &= " count(*) as cnt_ranges,"
            lsSql &= " sum(dist_count) as tot_shares, "
            lsSql &= " a.namechangeref_flag,"
            lsSql &= " a.namechageref_code"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tdematpend as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.received_date,"
            lsSql &= " d.isin_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.drn_no,"
            lsSql &= " e.dp_id "

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            For i = 0 To ds.Tables("header").Rows.Count - 1
                lsTxt = Chr(10)
                lsTxt &= "<Tp>2</Tp>"
                lsTxt &= "<Bnfcry>" & ds.Tables("header").Rows(i).Item("client_id").ToString & "</Bnfcry>"
                lsTxt &= "<ISIN>" & ds.Tables("header").Rows(i).Item("isin_id").ToString & "</ISIN>"
                lsTxt &= "<Drn>" & ds.Tables("header").Rows(i).Item("drn_no").ToString & "</Drn>"
                lsTxt &= "<Qty>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Qty>"
                lsTxt &= "<Ref>" & ds.Tables("header").Rows(i).Item("dp_id").ToString & "</Ref>"
                lsTxt &= "<Rcvdt>" & Format(ds.Tables("header").Rows(i).Item("received_date"), "ddMMyyyy") & "</Rcvdt>"
                lsTxt &= "<Veraccpt>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Veraccpt>"
                lsTxt &= "<Verrjct></Verrjct>"
                lsTxt &= "<Accpt>" & ds.Tables("header").Rows(i).Item("tot_shares").ToString & "</Accpt>"
                lsTxt &= "<Rjct></Rjct>"
                'lsTxt &= "<Rej>"
                'lsTxt &= "<Flg></Flg>"
                'lsTxt &= "<Cd></Cd>"
                'lsTxt &= "<Prtqty></Prtqty>"
                'lsTxt &= "<Rmk></Rmk>"
                'lsTxt &= "</Rej>"

                If ds.Tables("upload").Rows(0).Item("comp_listed").ToString = "Y" Then
                    lsTxt &= "<Ranges>" & ds.Tables("header").Rows(i).Item("cnt_ranges").ToString & "</Ranges>"

                    If ds.Tables("header").Rows(i).Item("namechangeref_flag").ToString = "Y" Then
                        lsTxt &= "<DocTyp>" & ds.Tables("header").Rows(i).Item("namechageref_code").ToString & "</DocTyp>"
                    Else
                        lsTxt &= "<DocTyp>01</DocTyp>"
                    End If
                End If


                Call Print(1, lsTxt)

                If ds.Tables("upload").Rows(0).Item("comp_listed").ToString = "Y" Then
                    lsSql = ""
                    lsSql &= " select "
                    lsSql &= " c.dist_from,"
                    lsSql &= " c.dist_to, "
                    lsSql &= " d.folio_no,"
                    lsSql &= " e.cert_no"
                    lsSql &= " from sta_trn_tinward as a "
                    lsSql &= " inner join sta_trn_tcertentry as b on a.inward_gid = b.inward_gid and b.delete_flag = 'N' "
                    lsSql &= " inner join sta_trn_tcertdist as c on b.cert_gid = c.cert_gid and c.delete_flag = 'N' "
                    lsSql &= " inner join sta_trn_tfolio as d on a.folio_gid=d.folio_gid and d.delete_flag='N' "
                    lsSql &= " inner join  sta_trn_tcert as e on b.cert_gid=e.cert_gid and e.delete_flag='N' "
                    lsSql &= " where a.inward_gid = " & ds.Tables("header").Rows(i).Item("inward_gid").ToString & " "
                    lsSql &= " and a.delete_flag = 'N' "
                    lsSql &= " order by c.dist_from "

                    Call gpDataSet(lsSql, "range", gOdbcConn, ds)

                    For j = 0 To ds.Tables("range").Rows.Count - 1
                        lsTxt = Chr(10)
                        lsTxt &= "<Rngs>" & (j + 1).ToString & "</Rngs>"
                        lsTxt &= "<DNFrm>" & ds.Tables("range").Rows(j).Item("dist_from").ToString & "</DNFrm>"
                        lsTxt &= "<DNTo>" & ds.Tables("range").Rows(j).Item("dist_to").ToString & "</DNTo>"
                        'Murali Changes on 09-03-2020
                        lsTxt &= "<Folio>" & ds.Tables("range").Rows(j).Item("folio_no").ToString & "</Folio>"
                        lsTxt &= "<CertFrm>" & ds.Tables("range").Rows(j).Item("cert_no").ToString & "</CertFrm>"
                        lsTxt &= "<CertTo>" & ds.Tables("range").Rows(j).Item("cert_no").ToString & "</CertTo>"
                        lsTxt &= "<RngStts>A</RngStts>"

                        Call Print(1, lsTxt)
                    Next j

                    ds.Tables("range").Rows.Clear()
                End If
            Next i

            ds.Tables("header").Rows.Clear()

            Call Print(1, Chr(10))

            Call FileClose(1)

            Call gpOpenFile(lsFilePath)
        End If

        ds.Tables("upload").Rows.Clear()
    End Sub

    Public Sub CDSLAllotment(UploadId As Long)
        'Dim UploadId = 3266
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnRecCount As Long
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim lnLineNo As Long = 0

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.cdsl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " c.cdsl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnAllotmentCDSL & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\CDSLCAallotment"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.inward_comp_no as 'inward_no',"
            lsSql &= " a.received_date,"
            lsSql &= " a.folio_no,"
            lsSql &= " a.shareholder_name,"
            lsSql &= " d.isin_id,"
            lsSql &= " d.comp_listed,"
            lsSql &= " a.ca_type,"
            lsSql &= " date_format(a.allotment_date,'%Y%m%d') as allotment_date,"
            lsSql &= " date_format(a.execution_date,'%Y%m%d') as execution_date,"
            lsSql &= " a.rta_internal_refno,"
            lsSql &= " a.ca_type,"
            lsSql &= " a.allocation_allotment_desc,"
            lsSql &= " lpad(a.dist_from,18,0) as dist_from,"
            lsSql &= " lpad(a.dist_to,18,0) as dist_to,"
            lsSql &= " a.stamp_duty_flag,"
            lsSql &= " concat(lpad(a.total_issue_amt,16,0),'00') as total_issue_amt,"
            lsSql &= " concat(lpad(a.total_paidup_amt,16,0),'00') as total_paidup_amt,"
            lsSql &= " concat(lpad(a.share_count,12,0),'.000') as share_count,"
            lsSql &= " lpad(a.share_count,18,0) as dn_qty"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            ' detail
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caentry_slno,"
            lsSql &= " e.caentry_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.lockin_flag,"
            lsSql &= " e.lockin_reason_code,"
            lsSql &= " ifnull(date_format(ifnull(e.lockin_releasedate,0),'%Y%m%d'),0) as lockin_releasedate,"
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            lsSql &= " concat(lpad(e.dist_from,16,0),'00') as dist_from,"
            lsSql &= " concat(lpad(e.dist_to,16,0),'00') as dist_to,"
            lsSql &= " concat(lpad(cast(e.share_count as signed),12,0),'.000') as current_qty,"
            'lsSql &= " concat(lpad(cast(e.stamp_duty as signed),12,0),'.000') as stamp_duty"
            lsSql &= " lpad(e.stamp_duty,16,0) as stamp_duty"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaentry as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "detail", gOdbcConn, ds)

            lnRecCount = ds.Tables("detail").Rows.Count

            If ds.Tables("header").Rows.Count > 0 Then
                ' header
                lsTxt = ""
                lsTxt &= AlignTxt("01", 2, 1)
                lsTxt &= AlignTxt(Format(lnRecCount, "0000000000"), 10, 1) ' Total number of detail records
                lsTxt &= ds.Tables("header").Rows(0).Item("share_count").ToString()
                lsTxt &= AlignTxt("000000000000.000", 16, 1)
                lsTxt &= AlignTxt("000000000000.000", 16, 1)
                lsTxt &= AlignTxt(Format(lnRecCount, "0000000000"), 10, 1) ' Total number of detail records
                If ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y" Then
                    lsTxt &= AlignTxt(Format(lnRecCount, "0000000000"), 10, 1) ' 
                Else
                    lsTxt &= AlignTxt(Format(0, "0000000000"), 10, 1) ' 
                End If
                Call Print(1, lsTxt)

                ' Detail
                For j = 0 To ds.Tables("detail").Rows.Count - 1
                    lnLineNo += 1

                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("02", 2, 1)
                    'lsTxt &= AlignTxt(Format(lnLineNo, "0000000"), 7, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("boid").ToString()
                    lsTxt &= ds.Tables("header").Rows(0).Item("rta_internal_refno").ToString()
                    lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    If ds.Tables("detail").Rows(j).Item("lockin_reason_code").ToString = "" Then
                        lsTxt &= AlignTxt("00", 2, 1)
                    Else
                        lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("lockin_reason_code").ToString, 2, 1)
                    End If

                    If ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString = "0" Then
                        lsTxt &= AlignTxt("", 50, 1)
                        lsTxt &= AlignTxt("00000000", 8, 1)
                    Else
                        lsTxt &= AlignTxt("", 50, 1)
                        AlignTxt(ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString, 8, 1)

                    End If

                    lsTxt &= AlignTxt("D", 1, 1)
                    lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                    lsTxt &= ds.Tables("detail").Rows(j).Item("current_qty").ToString()
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    lsTxt &= AlignTxt("00", 2, 1)
                    lsTxt &= AlignTxt("", 50, 1)
                    lsTxt &= AlignTxt("00000000", 8, 1)
                    lsTxt &= AlignTxt("C", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("stamp_duty").ToString()

                    Call Print(1, lsTxt)
                Next j

                If (ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y") Then
                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("03", 2, 1)
                    lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                    lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("dist_from").ToString(), 18, 0)
                    lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("dist_to").ToString(), 18, 0)
                    lsTxt &= ds.Tables("header").Rows(0).Item("dn_qty").ToString()
                    lsTxt &= AlignTxt("C", 1, 1)

                    Call Print(1, lsTxt)
                End If

                Call FileClose(1)

                Call gpOpenFile(lsFilePath)

                ds.Tables("header").Rows.Clear()
                ds.Tables("detail").Rows.Clear()

            End If
        End If

        ds.Tables("upload").Rows.Clear()
    End Sub

    Public Sub NSDLAllotment(UploadId As Long)
        'Dim UploadId = 3262
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnRecCount As Long
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim lnLineNo As Long = 0

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.nsdl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " c.nsdl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnAllotmentNSDL & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\NSDLCAallotment"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.inward_comp_no as 'inward_no',"
            lsSql &= " a.received_date,"
            lsSql &= " a.folio_no,"
            lsSql &= " a.shareholder_name,"
            lsSql &= " d.isin_id,"
            lsSql &= " d.comp_listed,"
            lsSql &= " a.ca_type,"
            lsSql &= " date_format(a.allotment_date,'%Y%m%d') as allotment_date,"
            lsSql &= " date_format(a.execution_date,'%Y%m%d') as execution_date,"
            lsSql &= " rpad(a.rta_internal_refno,'16',' ') as rta_internal_refno,"
            lsSql &= " a.ca_type,"
            lsSql &= " a.allocation_allotment_desc,"
            lsSql &= " lpad(a.dist_from,18,0) as dist_from,"
            lsSql &= " lpad(a.dist_to,18,0) as dist_to,"
            lsSql &= " a.stamp_duty_flag,"
            lsSql &= " LPAD(REPLACE(a.total_issue_amt, '.', ''), 18, '0') as total_issue_amt,"
            lsSql &= " LPAD(REPLACE(a.total_paidup_amt, '.', ''), 18, '0') as total_paidup_amt,"
            'lsSql &= " concat(lpad(FLOOR(a.total_issue_amt),16,0),'00') as total_issue_amt,"
            'lsSql &= " concat(lpad(FLOOR(a.total_paidup_amt),16,0),'00') as total_paidup_amt,"
            lsSql &= " concat(lpad(a.share_count,15,0),'000') as share_count,"
            lsSql &= " lpad(a.share_count,18,0) as share_count1"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            ' detail
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caentry_slno,"
            lsSql &= " e.caentry_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.lockin_flag,"
            lsSql &= " ifnull(date_format(e.lockin_releasedate,'%Y%m%d'),0) as lockin_releasedate,"
            lsSql &= " concat(lpad(e.dist_from,16,0),'00') as dist_from,"
            lsSql &= " concat(lpad(e.dist_to,16,0),'00') as dist_to,"
            lsSql &= " concat(lpad(cast(e.share_count as signed),15,0),'000') as allotment_qty,"
            'lsSql &= " concat(lpad(cast(e.share_price as signed),12,0),'000000') as issue_price,"

            lsSql &= " concat(lpad(replace((e.share_count * e.share_price),'.',''),14,0),'000000') as issue_price,"
            lsSql &= " concat(lpad(replace((e.share_count * e.share_price),'.',''),18,0),'00') as issued_amt,"
            lsSql &= " concat(lpad(replace((e.share_count * e.share_price),'.',''),14,0),'000000') as paidup_price,"
            lsSql &= " lpad(replace((e.share_count * e.share_price),'.',''),18,0) as paidup_amt"

            'lsSql &= " concat(lpad((e.share_count * cast(e.share_price as signed)),12,0),'000000') as issue_price,"
            'lsSql &= " concat(lpad((e.share_count * cast(e.share_price as signed)),16,0),'00') as issued_amt,"
            'lsSql &= " concat(lpad((e.share_count * cast(e.share_price as signed)),12,0),'000000') as paidup_price,"
            'lsSql &= " concat(lpad((e.share_count * cast(e.share_price as signed)),16,0),'00') as paidup_amt"

            'lsSql &= " concat(lpad(cast(e.purchase_cost as signed),12,0),'000000') as paidup_price,"
            'lsSql &= " concat(lpad((e.share_count * cast(e.purchase_cost as signed)),16,0),'00') as paidup_amt"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaentry as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "detail", gOdbcConn, ds)

            lnRecCount = ds.Tables("detail").Rows.Count

            If ds.Tables("header").Rows.Count > 0 Then
                ' header
                lsTxt = ""
                lsTxt &= AlignTxt("01", 2, 1)
                lsTxt &= AlignTxt("SHRI001", 7, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("rta_internal_refno").ToString()
                lsTxt &= AlignTxt("C", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("ca_type").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("allotment_date").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("allocation_allotment_desc").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("execution_date").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("share_count").ToString()
                If ds.Tables("detail").Rows(0).Item("lockin_flag").ToString() = "Y" Then
                    lsTxt &= ds.Tables("header").Rows(0).Item("share_count").ToString()
                Else
                    lsTxt &= AlignTxt("000000000000000000", 18, 1)
                End If
                lsTxt &= AlignTxt(Format(lnRecCount, "0000000"), 7, 1) ' Total number of detail records
                lsTxt &= ds.Tables("header").Rows(0).Item("total_issue_amt").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("total_paidup_amt").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("stamp_duty_flag").ToString()
                If ds.Tables("header").Rows(0).Item("stamp_duty_flag").ToString() = "Y" Then
                    lsTxt &= AlignTxt("01", 2, 1)
                Else
                    lsTxt &= AlignTxt("00", 2, 1)
                End If
                lsTxt &= AlignTxt("0000", 4, 1)
                lsTxt &= AlignTxt("", 2, 1)

                Call Print(1, lsTxt)

                For j = 0 To ds.Tables("detail").Rows.Count - 1
                    lnLineNo += 1

                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("02", 2, 1)
                    lsTxt &= AlignTxt(Format(lnLineNo, "0000000"), 7, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("dp_id").ToString, 8, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("client_id").ToString, 8, 1)
                    lsTxt &= AlignTxt("00", 2, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("allotment_qty").ToString, 18, 1)
                    lsTxt &= AlignTxt("00", 2, 1)
                    If ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString = "0" Then
                        lsTxt &= AlignTxt("", 8, 1)
                    Else
                        AlignTxt(ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString, 8, 1)
                    End If
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("issue_price").ToString, 18, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("issued_amt").ToString, 18, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("paidup_price").ToString, 18, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("paidup_amt").ToString, 18, 1)

                    lsTxt &= AlignTxt("", 12, 1)

                    Call Print(1, lsTxt)
                Next j

                If (ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y") Then
                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("03", 2, 1)
                    lsTxt &= AlignTxt("0000001", 7, 1)
                    lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("dist_from").ToString(), 18, 0)
                    lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("dist_to").ToString(), 18, 0)
                    lsTxt &= ds.Tables("header").Rows(0).Item("share_count1").ToString()
                    lsTxt &= AlignTxt("00", 2, 1)
                    lsTxt &= ds.Tables("header").Rows(0).Item("ca_type").ToString()

                    Call Print(1, lsTxt)
                End If

                Call FileClose(1)

                Call gpOpenFile(lsFilePath)

                ds.Tables("header").Rows.Clear()
                ds.Tables("detail").Rows.Clear()

            End If
        End If

        ds.Tables("upload").Rows.Clear()
    End Sub

    Public Sub CDSLPhysToIEPF_old(UploadId As Long)
        'Dim UploadId = 3266
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnRecCount As Long
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim lnLineNo As Long = 0

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.cdsl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " c.cdsl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnPhyToiepfCDSL & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\CDSLPhysToIEPF"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.inward_comp_no as 'inward_no',"
            lsSql &= " a.received_date,"
            lsSql &= " a.folio_no,"
            lsSql &= " a.shareholder_name,"
            lsSql &= " d.isin_id,"
            lsSql &= " d.comp_listed,"
            lsSql &= " d.email_id,"
            lsSql &= " d.cin_no,"
            lsSql &= " d.isin_id,"
            lsSql &= " h.rta_internal_ref_no,"
            lsSql &= " h.catype,"
            lsSql &= " date_format(h.board_approval_date,'%d%m%Y') as board_approval_date,"
            lsSql &= " date_format(h.execution_date,'%d%m%Y') as execution_date,"
            lsSql &= " concat(lpad(h.tot_credit_qty,16,0),'000') as tot_credit_qty,"
            lsSql &= " floor(h.tot_credit_qty) as credit_qty,"
            lsSql &= " concat(lpad(h.tot_credit_lockin_qty,16,0),'000') as tot_credit_lockin_qty,"
            lsSql &= " floor(h.tot_credit_lockin_qty) as credit_lockin_qty,"
            lsSql &= " concat(lpad(h.tot_debit_qty,16,0),'000') as tot_debit_qty,"
            lsSql &= " floor(h.tot_debit_qty) as debit_qty,"
            lsSql &= " concat(lpad(h.tot_debit_lockin_qty,16,0),'000') as tot_debit_lockin_qty,"
            lsSql &= " floor(h.tot_debit_lockin_qty) as debit_lockin_qty,"
            lsSql &= " lpad(f.finyear_code,9,'') as finyear_code,"
            lsSql &= " rpad(floor(h.tot_nominal_amtof_shares),5,0) as tot_nominal_amtof_shares"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepfheader as h on h.inward_gid = a.inward_gid and h.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tfinyear as f on f.finyear_gid = h.div_finyear and f.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            ' detail
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caiepffolio_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.folio_no,"
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            'lsSql &= " lpad(e.dist_from,18,0) as dist_from,"
            'lsSql &= " lpad(e.dist_to,18,0) as dist_to,"
            lsSql &= " lpad((e.dist_to - e.dist_from )+ 1,18,0) as dn_qty,"
            lsSql &= " concat(lpad(e.credit_qty,16,0),'000') as credit_qty,"
            lsSql &= " concat(lpad(e.debit_qty,16,0),'000') as debit_qty,"
            lsSql &= " e.holder1_name,"
            lsSql &= " e.investor_category,"
            lsSql &= " e.creditqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.creditqty_lockin_releasedate,0),'%d%m%Y'),0) as creditqty_lockin_releasedate,"
            lsSql &= " e.debitqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.debitqty_lockin_releasedate,0),'%d%m%Y'),0) as debitqty_lockin_releasedate,"
            lsSql &= " e.bo_address1,"
            lsSql &= " e.bo_address2,"
            lsSql &= " e.bo_address3,"
            lsSql &= " e.bo_address_city,"
            lsSql &= " e.bo_address_state,"
            lsSql &= " e.bo_address_country,"
            lsSql &= " e.bo_address_pincode"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepffolio as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by e.dp_id, e.client_id, e.folio_no"

            Call gpDataSet(lsSql, "detail", gOdbcConn, ds)

            lnRecCount = ds.Tables("detail").Rows.Count

            ' Record03
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caiepffolio_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.folio_no,"
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            lsSql &= " lpad(e.dist_from,18,0) as dist_from,"
            lsSql &= " lpad(e.dist_to,18,0) as dist_to,"
            lsSql &= " lpad((e.dist_to - e.dist_from )+ 1,18,0) as dn_qty,"
            lsSql &= " concat(lpad(e.credit_qty,16,0),'000') as credit_qty,"
            lsSql &= " concat(lpad(e.debit_qty,16,0),'000') as debit_qty,"
            lsSql &= " e.holder1_name,"
            lsSql &= " e.investor_category,"
            lsSql &= " e.creditqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.creditqty_lockin_releasedate,0),'%d%m%Y'),0) as creditqty_lockin_releasedate,"
            lsSql &= " e.debitqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.debitqty_lockin_releasedate,0),'%d%m%Y'),0) as debitqty_lockin_releasedate,"
            lsSql &= " e.bo_address1,"
            lsSql &= " e.bo_address2,"
            lsSql &= " e.bo_address3,"
            lsSql &= " e.bo_address_city,"
            lsSql &= " e.bo_address_state,"
            lsSql &= " e.bo_address_country,"
            lsSql &= " e.bo_address_pincode"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepffolio as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            'lsSql &= " group by e.dp_id, e.client_id, e.folio_no"

            Call gpDataSet(lsSql, "record03", gOdbcConn, ds)


            If ds.Tables("header").Rows.Count > 0 Then
                ' header
                lsTxt = ""
                lsTxt &= AlignTxt("01", 2, 1)
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= AlignTxt("000185", 6, 1)
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("rta_internal_ref_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("catype").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("board_approval_date").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("execution_date").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("debit_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("debit_lockin_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_lockin_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= AlignTxt(lnRecCount, 5, 1) ' Total number of detail records
                lsTxt &= lnRecCount ' Total number of detail records
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("cin_no").ToString(), 21, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("cin_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("email_id").ToString(), 50, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("email_id").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("finyear_code").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= ds.Tables("header").Rows(0).Item("tot_nominal_amtof_shares").ToString()
                lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("tot_nominal_amtof_shares").ToString(), 5, 1)

                'If ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y" Then
                '    lsTxt &= AlignTxt(Format(lnRecCount, "0000000000"), 10, 1) ' 
                'Else
                '    lsTxt &= AlignTxt(Format(0, "0000000000"), 10, 1) ' 
                'End If
                Call Print(1, lsTxt)

                ' Detail
                For j = 0 To ds.Tables("detail").Rows.Count - 1
                    lnLineNo += 1

                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("02", 2, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt(Format(lnLineNo, "00000"), 5, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("boid").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("folio_no").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("debit_qty").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("0000000000000000000", 19, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("credit_qty").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("0000000000000000000", 19, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("holder1_name").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("investor_category").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address1").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address2").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address3").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_city").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_state").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_country").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_pincode").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    'If ds.Tables("detail").Rows(j).Item("lockin_reason_code").ToString = "" Then
                    '    lsTxt &= AlignTxt("00", 2, 1)
                    'Else
                    '    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("lockin_reason_code").ToString, 2, 1)
                    'End If

                    'If ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString = "0" Then
                    '    lsTxt &= AlignTxt("", 50, 1)
                    '    lsTxt &= AlignTxt("00000000", 8, 1)
                    'Else
                    '    lsTxt &= AlignTxt("", 50, 1)
                    '    AlignTxt(ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString, 8, 1)
                    'End If

                    Call Print(1, lsTxt)
                Next j

                If (ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y") Then
                    lnLineNo = 0
                    For m = 0 To ds.Tables("record03").Rows.Count - 1
                        lnLineNo += 1

                        lsTxt = vbNewLine
                        lsTxt &= AlignTxt("03", 2, 1)
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= AlignTxt(Format(lnLineNo, "00000"), 5, 1)
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= AlignTxt("C", 1, 1)
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= Format(ds.Tables("record03").Rows(m).Item("dist_from").ToString(), "000000000000000000")
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= Format(ds.Tables("record03").Rows(m).Item("dist_to").ToString(), "000000000000000000")
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= Format(ds.Tables("record03").Rows(m).Item("dn_qty").ToString(), "000000000000000000")

                        Call Print(1, lsTxt)
                    Next m
                End If

                Call FileClose(1)

                Call gpOpenFile(lsFilePath)

                ds.Tables("header").Rows.Clear()
                ds.Tables("detail").Rows.Clear()
                ds.Tables("record03").Rows.Clear()

            End If
        End If

        ds.Tables("upload").Rows.Clear()
    End Sub

    Public Sub CDSLPhysToIEPF(UploadId As Long)
        'Dim UploadId = 3266
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnRecCount As Long
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim lnLineNo As Long = 0

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.cdsl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " c.cdsl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnPhyToiepfCDSL & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\CDSLPhysToIEPF"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.inward_comp_no as 'inward_no',"
            lsSql &= " a.received_date,"
            lsSql &= " a.folio_no,"
            lsSql &= " a.shareholder_name,"
            lsSql &= " d.isin_id,"
            lsSql &= " d.comp_listed,"
            lsSql &= " d.email_id,"
            lsSql &= " d.cin_no,"
            lsSql &= " d.isin_id,"
            lsSql &= " h.rta_internal_ref_no,"
            lsSql &= " h.catype,"
            lsSql &= " date_format(h.board_approval_date,'%d%m%Y') as board_approval_date,"
            lsSql &= " date_format(h.execution_date,'%d%m%Y') as execution_date,"
            lsSql &= " concat(lpad(h.tot_credit_qty,16,0),'000') as tot_credit_qty,"
            lsSql &= " floor(h.tot_credit_qty) as credit_qty,"
            lsSql &= " concat(lpad(h.tot_credit_lockin_qty,16,0),'000') as tot_credit_lockin_qty,"
            lsSql &= " floor(h.tot_credit_lockin_qty) as credit_lockin_qty,"
            lsSql &= " concat(lpad(h.tot_debit_qty,16,0),'000') as tot_debit_qty,"
            lsSql &= " floor(h.tot_debit_qty) as debit_qty,"
            lsSql &= " concat(lpad(h.tot_debit_lockin_qty,16,0),'000') as tot_debit_lockin_qty,"
            lsSql &= " floor(h.tot_debit_lockin_qty) as debit_lockin_qty,"
            lsSql &= " lpad(f.finyear_code,9,'') as finyear_code,"
            lsSql &= " rpad(floor(h.tot_nominal_amtof_shares),5,0) as tot_nominal_amtof_shares"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepfheader as h on h.inward_gid = a.inward_gid and h.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tfinyear as f on f.finyear_gid = h.div_finyear and f.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            ' detail
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caiepffolio_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.folio_no,"
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            'lsSql &= " lpad(e.dist_from,18,0) as dist_from,"
            'lsSql &= " lpad(e.dist_to,18,0) as dist_to,"
            lsSql &= " lpad((e.dist_to - e.dist_from )+ 1,18,0) as dn_qty,"
            'lsSql &= " concat(lpad(e.credit_qty,16,0),'000') as credit_qty,"
            'lsSql &= " concat(lpad(e.debit_qty,16,0),'000') as debit_qty,"
            lsSql &= " e.credit_qty as credit_qty,"
            lsSql &= " e.debit_qty as debit_qty,"
            lsSql &= " e.holder1_name,"
            lsSql &= " e.holder2_name,"
            lsSql &= " e.holder3_name,"
            lsSql &= " e.holder4_name,"
            lsSql &= " e.holder1_fhname,"
            lsSql &= " e.investor_category,"
            lsSql &= " e.creditqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.creditqty_lockin_releasedate,0),'%d%m%Y'),0) as creditqty_lockin_releasedate,"
            lsSql &= " e.debitqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.debitqty_lockin_releasedate,0),'%d%m%Y'),0) as debitqty_lockin_releasedate,"
            lsSql &= " e.bo_address1,"
            lsSql &= " e.bo_address2,"
            lsSql &= " e.bo_address3,"
            lsSql &= " e.bo_address_city,"
            lsSql &= " e.bo_address_state,"
            lsSql &= " e.bo_address_country,"
            lsSql &= " e.bo_address_pincode"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepffolio as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by e.dp_id, e.client_id, e.folio_no"

            Call gpDataSet(lsSql, "detail", gOdbcConn, ds)

            lnRecCount = ds.Tables("detail").Rows.Count

            ' Record03
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caiepffolio_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.folio_no,"
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            lsSql &= " lpad(e.dist_from,18,0) as dist_from,"
            lsSql &= " lpad(e.dist_to,18,0) as dist_to,"
            lsSql &= " lpad((e.dist_to - e.dist_from )+ 1,18,0) as dn_qty,"
            lsSql &= " concat(lpad(e.credit_qty,16,0),'000') as credit_qty,"
            lsSql &= " concat(lpad(e.debit_qty,16,0),'000') as debit_qty,"
            lsSql &= " e.holder1_name,"
            lsSql &= " e.investor_category,"
            lsSql &= " e.creditqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.creditqty_lockin_releasedate,0),'%d%m%Y'),0) as creditqty_lockin_releasedate,"
            lsSql &= " e.debitqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.debitqty_lockin_releasedate,0),'%d%m%Y'),0) as debitqty_lockin_releasedate,"
            lsSql &= " e.bo_address1,"
            lsSql &= " e.bo_address2,"
            lsSql &= " e.bo_address3,"
            lsSql &= " e.bo_address_city,"
            lsSql &= " e.bo_address_state,"
            lsSql &= " e.bo_address_country,"
            lsSql &= " e.bo_address_pincode"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepffolio as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            'lsSql &= " group by e.dp_id, e.client_id, e.folio_no"

            Call gpDataSet(lsSql, "record03", gOdbcConn, ds)


            If ds.Tables("header").Rows.Count > 0 Then
                ' header
                lsTxt = ""
                lsTxt &= AlignTxt("01", 2, 1)
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= AlignTxt("000185", 6, 1)
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("rta_internal_ref_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("catype").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("board_approval_date").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("execution_date").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("debit_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("debit_lockin_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_lockin_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= AlignTxt(lnRecCount, 5, 1) ' Total number of detail records
                lsTxt &= lnRecCount ' Total number of detail records
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_qty").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("cin_no").ToString(), 21, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("cin_no").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("email_id").ToString(), 50, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("email_id").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("finyear_code").ToString()
                lsTxt &= AlignTxt("~", 1, 1)
                'lsTxt &= ds.Tables("header").Rows(0).Item("tot_nominal_amtof_shares").ToString()
                lsTxt &= AlignTxt(ds.Tables("header").Rows(0).Item("tot_nominal_amtof_shares").ToString(), 5, 1)

                'If ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y" Then
                '    lsTxt &= AlignTxt(Format(lnRecCount, "0000000000"), 10, 1) ' 
                'Else
                '    lsTxt &= AlignTxt(Format(0, "0000000000"), 10, 1) ' 
                'End If
                Call Print(1, lsTxt)

                ' Detail
                For j = 0 To ds.Tables("detail").Rows.Count - 1
                    lnLineNo += 1

                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("02", 2, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    'lsTxt &= AlignTxt(Format(lnLineNo, "00000"), 5, 1)
                    lsTxt &= lnLineNo
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("boid").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("folio_no").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("debit_qty").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    'lsTxt &= AlignTxt("0000000000000000000", 19, 1)
                    'lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("credit_qty").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    'lsTxt &= AlignTxt("0000000000000000000", 19, 1)
                    'lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("holder1_name").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("holder2_name").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("holder3_name").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("holder4_name").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("holder1_fhname").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("investor_category").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address1").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address2").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address3").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_city").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_state").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_country").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("bo_address_pincode").ToString()
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    lsTxt &= AlignTxt("~", 1, 1)
                    'If ds.Tables("detail").Rows(j).Item("lockin_reason_code").ToString = "" Then
                    '    lsTxt &= AlignTxt("00", 2, 1)
                    'Else
                    '    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("lockin_reason_code").ToString, 2, 1)
                    'End If

                    'If ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString = "0" Then
                    '    lsTxt &= AlignTxt("", 50, 1)
                    '    lsTxt &= AlignTxt("00000000", 8, 1)
                    'Else
                    '    lsTxt &= AlignTxt("", 50, 1)
                    '    AlignTxt(ds.Tables("detail").Rows(j).Item("lockin_releasedate").ToString, 8, 1)
                    'End If

                    Call Print(1, lsTxt)
                Next j

                If (ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y") Then
                    lnLineNo = 0
                    For m = 0 To ds.Tables("record03").Rows.Count - 1
                        lnLineNo += 1

                        lsTxt = vbNewLine
                        lsTxt &= AlignTxt("03", 2, 1)
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= AlignTxt(Format(lnLineNo, "00000"), 5, 1)
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= AlignTxt("C", 1, 1)
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= ds.Tables("record03").Rows(m).Item("dist_from").ToString()
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= ds.Tables("record03").Rows(m).Item("dist_to").ToString()
                        lsTxt &= AlignTxt("~", 1, 1)
                        lsTxt &= ds.Tables("record03").Rows(m).Item("dn_qty").ToString()

                        Call Print(1, lsTxt)
                    Next m
                End If

                Call FileClose(1)

                Call gpOpenFile(lsFilePath)

                ds.Tables("header").Rows.Clear()
                ds.Tables("detail").Rows.Clear()
                ds.Tables("record03").Rows.Clear()

            End If
        End If

        ds.Tables("upload").Rows.Clear()
    End Sub

    Public Sub NSDLPhysToIEPF(UploadId As Long)
        'Dim UploadId = 3262
        Dim lsSql As String
        Dim lsTxt As String
        Dim lnRecCount As Long
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim ds As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim lnLineNo As Long = 0

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.nsdl_sno,"
        lsSql &= " b.comp_name,"
        lsSql &= " c.nsdl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnPhyToiepfNSDL & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\NSDLPhysToIEPF"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()

            Call FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

            ' header
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.inward_gid,"
            lsSql &= " a.inward_comp_no as 'inward_no',"
            lsSql &= " a.received_date,"
            lsSql &= " a.folio_no,"
            lsSql &= " a.shareholder_name,"
            lsSql &= " d.isin_id,"
            lsSql &= " d.comp_listed,"
            lsSql &= " d.email_id,"
            lsSql &= " d.cin_no,"
            lsSql &= " d.isin_id,"
            lsSql &= " h.rta_internal_ref_no,"
            lsSql &= " h.catype,"
            lsSql &= " date_format(h.board_approval_date,'%Y%m%d') as board_approval_date,"
            lsSql &= " date_format(h.execution_date,'%Y%m%d') as execution_date,"
            lsSql &= " concat(lpad(h.tot_credit_qty,16,0),'000') as tot_credit_qty,"
            lsSql &= " concat(lpad(floor(h.tot_credit_qty),15,0),'000')  as credit_qty,"
            lsSql &= " concat(lpad(h.tot_credit_lockin_qty,16,0),'000') as tot_credit_lockin_qty,"
            lsSql &= " concat(lpad(floor(h.tot_credit_lockin_qty),15,0),'000') as credit_lockin_qty,"
            lsSql &= " concat(lpad(h.tot_debit_qty,16,0),'000') as tot_debit_qty,"
            lsSql &= " concat(lpad(floor(h.tot_debit_qty),15,0),'000')  as debit_qty,"
            lsSql &= " concat(lpad(h.tot_debit_lockin_qty,16,0),'000') as tot_debit_lockin_qty,"
            lsSql &= " concat(lpad(floor(h.tot_debit_lockin_qty),15,0),'000') as debit_lockin_qty,"
            lsSql &= " replace(lpad(f.finyear_code,9,''),'-20','-') as finyear_code,"
            lsSql &= " rpad(floor(h.tot_nominal_amtof_shares),5,0) as tot_nominal_amtof_shares"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepfheader as h on h.inward_gid = a.inward_gid and h.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tfinyear as f on f.finyear_gid = h.div_finyear and f.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "header", gOdbcConn, ds)

            ' detail
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caiepffolio_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.folio_no,"
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            'lsSql &= " lpad(e.dist_from,18,0) as dist_from,"
            'lsSql &= " lpad(e.dist_to,18,0) as dist_to,"
            'lsSql &= " lpad((e.dist_to - e.dist_from )+ 1,18,0) as dn_qty,"
            'lsSql &= " concat(lpad(e.credit_qty,16,0),'000') as credit_qty,"
            'lsSql &= " concat(lpad(e.debit_qty,16,0),'000') as debit_qty,"
            lsSql &= "lpad(cast(sum((e.dist_to - e.dist_from )+ 1)as char(255)),18,0) as dn_qty,"
            lsSql &= "concat(lpad(cast(sum(e.credit_qty) as char(255)),15,0),'000') as credit_qty,"
            lsSql &= "concat(lpad(cast(sum(e.debit_qty) as char(255)),15,0),'000') as debit_qty,"
            lsSql &= " e.holder1_name,"
            lsSql &= " e.investor_category,"
            lsSql &= " e.creditqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.creditqty_lockin_releasedate,0),'%Y%m%d'),0) as creditqty_lockin_releasedate,"
            lsSql &= " e.debitqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.debitqty_lockin_releasedate,0),'%Y%m%d'),0) as debitqty_lockin_releasedate,"
            lsSql &= " e.bo_address1,"
            lsSql &= " e.bo_address2,"
            lsSql &= " e.bo_address3,"
            lsSql &= " e.bo_address_city,"
            lsSql &= " e.bo_address_state,"
            lsSql &= " e.bo_address_country,"
            lsSql &= " e.bo_address_pincode"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepffolio as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by e.dp_id, e.client_id, e.folio_no"

            Call gpDataSet(lsSql, "detail", gOdbcConn, ds)

            lnRecCount = ds.Tables("detail").Rows.Count


            ' Record 03
            lsSql = ""
            lsSql &= " select "
            lsSql &= " e.inward_gid,"
            lsSql &= " e.caiepffolio_slno,"
            lsSql &= " e.dp_id,"
            lsSql &= " e.client_id,"
            lsSql &= " e.folio_no,"
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            lsSql &= " lpad(e.dist_from,18,0) as dist_from,"
            lsSql &= " lpad(e.dist_to,18,0) as dist_to,"
            lsSql &= " lpad((e.dist_to - e.dist_from )+ 1,18,0) as dn_qty,"
            lsSql &= " concat(lpad(e.credit_qty,16,0),'000') as credit_qty,"
            lsSql &= " concat(lpad(e.debit_qty,16,0),'000') as debit_qty,"
            'lsSql &= "lpad(cast(sum((e.dist_to - e.dist_from )+ 1)as char(255)),18,0) as dn_qty,"
            'lsSql &= "concat(lpad(cast(sum(e.credit_qty) as char(255)),16,0),'000') as credit_qty,"
            'lsSql &= "concat(lpad(cast(sum(e.debit_qty) as char(255)),16,0),'000') as debit_qty,"
            lsSql &= " e.holder1_name,"
            lsSql &= " e.investor_category,"
            lsSql &= " e.creditqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.creditqty_lockin_releasedate,0),'%Y%m%d'),0) as creditqty_lockin_releasedate,"
            lsSql &= " e.debitqty_lockin_reasoncode,"
            lsSql &= " ifnull(date_format(ifnull(e.debitqty_lockin_releasedate,0),'%Y%m%d'),0) as debitqty_lockin_releasedate,"
            lsSql &= " e.bo_address1,"
            lsSql &= " e.bo_address2,"
            lsSql &= " e.bo_address3,"
            lsSql &= " e.bo_address_city,"
            lsSql &= " e.bo_address_state,"
            lsSql &= " e.bo_address_country,"
            lsSql &= " e.bo_address_pincode"
            lsSql &= " from sta_trn_tinward as a "
            lsSql &= " inner join sta_mst_tcompany as d on a.comp_gid = d.comp_gid and a.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tcaiepffolio as e on a.inward_gid = e.inward_gid and e.delete_flag = 'N' "
            lsSql &= " where a.upload_gid = " & UploadId & " "
            lsSql &= " and a.delete_flag = 'N' "
            'lsSql &= " group by e.dp_id, e.client_id, e.folio_no"

            Call gpDataSet(lsSql, "record03", gOdbcConn, ds)

            If ds.Tables("header").Rows.Count > 0 Then
                ' header
                lsTxt = ""
                lsTxt &= AlignTxt("01", 2, 1)
                lsTxt &= AlignTxt("SHRI001", 7, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("rta_internal_ref_no").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                lsTxt &= AlignTxt("", 12, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("catype").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("board_approval_date").ToString()
                lsTxt &= "0078"
                lsTxt &= ds.Tables("header").Rows(0).Item("execution_date").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_qty").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("debit_qty").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("credit_lockin_qty").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("debit_lockin_qty").ToString()
                lsTxt &= AlignTxt(Format(lnRecCount, "0000000"), 7, 1) ' Total number of detail records
                lsTxt &= ds.Tables("header").Rows(0).Item("cin_no").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("finyear_code").ToString()

                Call Print(1, lsTxt)

                For j = 0 To ds.Tables("detail").Rows.Count - 1
                    lnLineNo += 1

                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("02", 2, 1)
                    lsTxt &= AlignTxt(Format(lnLineNo, "0000000"), 7, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("dp_id").ToString, 8, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("client_id").ToString(), 8, 1)
                    lsTxt &= AlignTxt("00", 2, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("folio_no").ToString(), 16, 1)
                    lsTxt &= AlignTxt("00", 2, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("credit_qty").ToString(), 18, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("debit_qty").ToString(), 18, 1)
                    lsTxt &= AlignTxt("00", 2, 1)
                    If ds.Tables("detail").Rows(j).Item("creditqty_lockin_releasedate").ToString() = "0" Then
                        lsTxt &= AlignTxt("", 8, 1)
                    Else
                        AlignTxt(ds.Tables("detail").Rows(j).Item("creditqty_lockin_releasedate").ToString(), 8, 1)
                    End If
                    lsTxt &= AlignTxt("00", 2, 1)
                    If ds.Tables("detail").Rows(j).Item("debitqty_lockin_releasedate").ToString() = "0" Then
                        lsTxt &= AlignTxt("", 8, 1)
                    Else
                        AlignTxt(ds.Tables("detail").Rows(j).Item("debitqty_lockin_releasedate").ToString(), 8, 1)
                    End If
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("holder1_name").ToString(), 164, 1)
                    lsTxt &= AlignTxt("", 164, 1)
                    lsTxt &= AlignTxt("", 164, 1)
                    lsTxt &= AlignTxt("", 142, 1)
                    lsTxt &= AlignTxt("", 142, 1)
                    lsTxt &= AlignTxt("", 50, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 11, 1)
                    lsTxt &= AlignTxt("", 2, 1)
                    lsTxt &= AlignTxt("", 2, 1)
                    lsTxt &= AlignTxt("", 4, 1)
                    lsTxt &= AlignTxt("", 2, 1)
                    lsTxt &= AlignTxt("", 4, 1)
                    lsTxt &= AlignTxt("", 4, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 2, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 11, 1)
                    lsTxt &= AlignTxt("", 24, 1)
                    lsTxt &= AlignTxt("", 2, 1)
                    lsTxt &= AlignTxt("", 6, 1)
                    lsTxt &= AlignTxt("", 8, 1)
                    lsTxt &= AlignTxt("", 8, 1)
                    lsTxt &= AlignTxt("", 30, 1)
                    lsTxt &= AlignTxt("", 11, 1)
                    lsTxt &= AlignTxt("", 2, 1)
                    lsTxt &= AlignTxt("", 3, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address1").ToString(), 50, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address2").ToString(), 50, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address3").ToString(), 50, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_city").ToString(), 30, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_state").ToString(), 30, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_country").ToString(), 30, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_pincode").ToString(), 10, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address1").ToString(), 50, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address2").ToString(), 50, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address3").ToString(), 50, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_city").ToString(), 30, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_state").ToString(), 30, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_country").ToString(), 30, 1)
                    lsTxt &= AlignTxt(ds.Tables("detail").Rows(j).Item("bo_address_pincode").ToString(), 10, 1)
                    lsTxt &= AlignTxt("", 17, 1)
                    lsTxt &= AlignTxt("", 17, 1)
                    lsTxt &= AlignTxt("", 17, 1)
                    lsTxt &= AlignTxt("", 50, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 12, 1)
                    lsTxt &= AlignTxt("", 12, 1)
                    lsTxt &= AlignTxt("", 100, 1)
                    lsTxt &= AlignTxt("", 40, 1)
                    lsTxt &= AlignTxt("", 40, 1)
                    lsTxt &= AlignTxt("", 40, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 10, 1)
                    lsTxt &= AlignTxt("", 6, 1)
                    lsTxt &= AlignTxt("", 5, 1)
                    lsTxt &= AlignTxt("", 20, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 8, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 22, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 12, 1)
                    lsTxt &= AlignTxt("", 12, 1)
                    lsTxt &= AlignTxt("", 12, 1)
                    lsTxt &= AlignTxt("", 10, 1)
                    lsTxt &= AlignTxt("", 12, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 25, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 1, 1)
                    lsTxt &= AlignTxt("", 1, 1)

                    Call Print(1, lsTxt)
                Next j

                If (ds.Tables("header").Rows(0).Item("comp_listed").ToString() = "Y") Then
                    lnLineNo = 0
                    For m = 0 To ds.Tables("record03").Rows.Count - 1
                        lnLineNo += 1

                        lsTxt = vbNewLine
                        lsTxt &= AlignTxt("03", 2, 1)
                        lsTxt &= Format(lnLineNo, "0000000")
                        lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                        lsTxt &= AlignTxt("C", 1, 1)
                        lsTxt &= ds.Tables("record03").Rows(m).Item("dist_from").ToString()
                        lsTxt &= ds.Tables("record03").Rows(m).Item("dist_to").ToString()
                        lsTxt &= ds.Tables("record03").Rows(m).Item("dn_qty").ToString()
                        lsTxt &= AlignTxt("98", 2, 1)
                        lsTxt &= ds.Tables("header").Rows(0).Item("catype").ToString()
                        'lsTxt &= AlignTxt("0034", 4, 1)

                        Call Print(1, lsTxt)
                    Next m
                End If

                Call FileClose(1)

                Call gpOpenFile(lsFilePath)

                ds.Tables("header").Rows.Clear()
                ds.Tables("detail").Rows.Clear()
                ds.Tables("record03").Rows.Clear()

            End If
        End If

        ds.Tables("upload").Rows.Clear()
    End Sub

    Private Sub PageFooterTR()
        Call PrintLine(1, StrDup(mnPageWidth, "-"))

        mnLine = 0
    End Sub

    Private Sub PageFooterCR()
        Call PrintLine(1, StrDup(mnPageWidth, "-"))

        mnLine = 0
    End Sub

    Private Sub PageHeaderTR()
        Dim i As Integer
        Dim lnWidth As Integer

        mnPageNo = mnPageNo + 1
        lnWidth = mnPageWidth \ 3
        mnLine = 0

        msTxt = ""
        'msTxt &= Chr(12)
        msTxt &= AlignTxt("Unit : " & msCompName, lnWidth, 1)
        msTxt &= AlignTxt("REGISTER", lnWidth, 4)
        msTxt &= AlignTxt("PAGE : " & mnPageNo.ToString, lnWidth, 7)

        Call PrintLine(1, msTxt)
        mnLine += 1

        Call PrintLine(1, StrDup(mnPageWidth, "-"))
        mnLine += 1

        ' Header 1
        msTxt = ""

        For i = 0 To msColTRHead1.Length - 1
            msTxt &= AlignTxt(msColTRHead1(i), mnColTRWidth(i), 4)
        Next i

        Call PrintLine(1, msTxt)
        mnLine += 1

        ' Header 2
        msTxt = ""

        For i = 0 To msColTRHead2.Length - 1
            msTxt &= AlignTxt(msColTRHead2(i), mnColTRWidth(i), 4)
        Next i

        Call PrintLine(1, msTxt)
        mnLine += 1

        Call PrintLine(1, StrDup(mnPageWidth, "-"))
        mnLine += 1
    End Sub

    Private Sub PageHeaderOR()
        Dim i As Integer
        Dim lnWidth As Integer

        mnPageNo = mnPageNo + 1
        lnWidth = mnPageWidth \ 3
        mnLine = 0

        msTxt = ""
        'msTxt &= Chr(12)
        msTxt &= AlignTxt("Unit : " & msCompName, lnWidth, 1)
        msTxt &= AlignTxt("OBJECTION REGISTER", lnWidth, 4)
        msTxt &= AlignTxt("PAGE : " & mnPageNo.ToString, lnWidth, 7)

        Call PrintLine(1, msTxt)
        mnLine += 1

        Call PrintLine(1, StrDup(mnPageWidth, "-"))
        mnLine += 1

        ' Header 1
        msTxt = ""

        For i = 0 To msColTRHead1.Length - 1
            msTxt &= AlignTxt(msColTRHead1(i), mnColTRWidth(i), 4)
        Next i

        Call PrintLine(1, msTxt)
        mnLine += 1

        ' Header 2
        msTxt = ""

        For i = 0 To msColTRHead2.Length - 1
            msTxt &= AlignTxt(msColTRHead2(i), mnColTRWidth(i), 4)
        Next i

        Call PrintLine(1, msTxt)
        mnLine += 1

        Call PrintLine(1, StrDup(mnPageWidth, "-"))
        mnLine += 1
    End Sub

    Private Sub PageHeaderCR(ByVal lsMeetingdate As String)
        Dim i As Integer
        Dim lnWidth As Integer

        mnPageNo = mnPageNo + 1
        lnWidth = mnPageWidth \ 3
        mnLine = 0

        msTxt = ""
        'msTxt &= Chr(12)
        msTxt &= AlignTxt("Unit : " & msCompName, lnWidth, 1)
        msTxt &= AlignTxt("CERTIFICATE REGISTER - " & lsMeetingdate, lnWidth, 4)
        msTxt &= AlignTxt("PAGE : " & mnPageNo.ToString, lnWidth, 7)

        Call PrintLine(1, msTxt)
        mnLine += 1

        Call PrintLine(1, StrDup(mnPageWidth, "-"))
        mnLine += 1

        ' Header 1
        msTxt = ""

        For i = 0 To msColCRHead1.Length - 1
            msTxt &= AlignTxt(msColCRHead1(i), mnColCRWidth(i), 4)
        Next i

        Call PrintLine(1, msTxt)
        mnLine += 1

        ' Header 2
        msTxt = ""

        For i = 0 To msColCRHead2.Length - 1
            msTxt &= AlignTxt(msColCRHead2(i), mnColCRWidth(i), 4)
        Next i

        Call PrintLine(1, msTxt)
        mnLine += 1

        Call PrintLine(1, StrDup(mnPageWidth, "-"))
        mnLine += 1
    End Sub

    Private Sub LoadTR(UploadId As Long)
        Dim listTR As New List(Of clsTR)
        Dim lobjTR As clsTR
        Dim lsSql As String
        Dim ds As New DataSet
        Dim i As Long
        Dim j As Long
        Dim k As Long

        Dim lnLineNo As Long

        Dim lsMeetingDate As String = ""

        Dim lsTransferNo As String
        Dim lsTransferDate As String
        Dim lsTransferFooterdate As String
        Dim lsTransferrorName As String
        Dim lsTransfereeName As String
        Dim lsShareCount As String
        Dim lsCertNo As String
        Dim lsDistSeries As String
        Dim lsRemark As String
        Dim lnTotTransfer As Long = 0
        Dim lnTotCert As Long = 0
        Dim lnTotShares As Long = 0
        Dim lnTranFrom As Long = 0
        Dim lnTranTo As Long = 0
        Dim lsTranCode As String = ""
        Dim lsTranSubCode As String = ""
        Dim lsFilePath As String
        Dim lsFileName As String

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name,"
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.nsdl_sno,"
        lsSql &= " a.meeting_date,"
        lsSql &= " c.nsdl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnUploadTransferRegister & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            msCompName = ds.Tables("upload").Rows(0).Item("comp_name").ToString
            lsMeetingDate = ds.Tables("upload").Rows(0).Item("meeting_date").ToString

            If lsMeetingDate <> "" Then lsMeetingDate = Format(CDate(lsMeetingDate), "dd-MM-yyyy")

            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\Transfer Register"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_no").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            If ds.Tables("upload").Rows(0).Item("upload_filename").ToString() <> "" Then
                lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()
            Else
                lsFileName = lsFilePath & "\TransferRegister.txt"
            End If
        Else
            If Directory.Exists(gsReportPath) = False Then Call Directory.CreateDirectory(gsReportPath)
            lsFileName = gsReportPath & "TransferRegister.txt"
        End If

        ds.Tables("upload").Rows.Clear()

        ' file creation
        If File.Exists(lsFileName) Then File.Delete(lsFileName)

        FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

        ' inward
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.inward_gid,"
        lsSql &= " a.transfer_no,"
        lsSql &= " ifnull(a.execution_date,a.received_date) as received_date,"
        lsSql &= " a.inward_comp_no as 'inward_no',"
        'lsSql &= " a.inward_no,"
        lsSql &= " a.tran_code,"
        lsSql &= " concat('-',a.docsubtype_code)  as tran_subcode,"
        lsSql &= " fn_get_docsubtypecode(a.inward_gid) as tran_subcode,"
        lsSql &= " b.folio_no as from_folio_no,"
        lsSql &= " b.holder1_name as from_holder1,"
        lsSql &= " b.holder2_name as from_holder2,"
        lsSql &= " b.holder3_name as from_holder3,"
        lsSql &= " c.folio_no as to_folio_no,"
        lsSql &= " c.holder1_name as to_holder1,"
        lsSql &= " c.holder2_name as to_holder2,"
        lsSql &= " c.holder3_name as to_holder3,"
        lsSql &= " a.cons_amount,"
        lsSql &= " a.stamp_duty "
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as t on t.trantype_code = a.tran_code and t.transfer_flag = 'Y' and t.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as b on b.folio_gid = a.folio_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as c on c.folio_gid = a.tran_folio_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tupload as d on d.upload_gid = a.upload_gid and d.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.transfer_no > 0 "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.transfer_no "

        Call gpDataSet(lsSql, "inward", gOdbcConn, ds)

        For i = 0 To ds.Tables("inward").Rows.Count - 1
            lnLineNo = 1

            ' certificate 
            lsSql = ""
            lsSql &= " select "
            lsSql &= " b.cert_gid,"
            lsSql &= " b.cert_no,"
            lsSql &= " b.share_count "
            lsSql &= " from sta_trn_tcertentry as a "
            lsSql &= " inner join sta_trn_tcert as b on b.cert_gid = a.cert_gid and b.delete_flag = 'N' "
            lsSql &= " where a.inward_gid = " & ds.Tables("inward").Rows(i).Item("inward_gid").ToString & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "cert", gOdbcConn, ds)

            For j = 0 To ds.Tables("cert").Rows.Count - 1
                lsCertNo = ds.Tables("cert").Rows(j).Item("cert_no").ToString & " "
                lsShareCount = ds.Tables("cert").Rows(j).Item("share_count").ToString & " "
                lnTotShares += ds.Tables("cert").Rows(j).Item("share_count")
                lnTotCert += 1

                ' certificate distinctive series
                lsSql = ""
                lsSql &= " select "
                lsSql &= " a.dist_from,"
                lsSql &= " a.dist_to,"
                lsSql &= " a.dist_count "
                lsSql &= " from sta_trn_tcertdist as a "
                lsSql &= " where a.cert_gid = " & ds.Tables("cert").Rows(j).Item("cert_gid").ToString & " "
                lsSql &= " and a.delete_flag = 'N' "

                Call gpDataSet(lsSql, "dist", gOdbcConn, ds)

                For k = 0 To ds.Tables("dist").Rows.Count - 1
                    lsTransferNo = ""
                    lsTransferDate = ""
                    lsTransferrorName = ""
                    lsTransfereeName = ""
                    lsTranCode = ""
                    lsRemark = ""

                    lsDistSeries = ""
                    lsDistSeries &= ds.Tables("dist").Rows(k).Item("dist_from").ToString
                    lsDistSeries &= "/"
                    lsDistSeries &= ds.Tables("dist").Rows(k).Item("dist_to").ToString

                    Select Case lnLineNo
                        Case 1
                            lsTransferNo = ds.Tables("inward").Rows(i).Item("transfer_no").ToString
                            lsTransferDate = Format(ds.Tables("inward").Rows(i).Item("received_date"), "dd-MM-yyyy")
                            lsTransferFooterdate = Format(ds.Tables("inward").Rows(i).Item("received_date"), "dd-MM-yyyy")

                            If lsMeetingDate <> "" Then lsTransferDate = lsMeetingDate
                            If lsMeetingDate <> "" Then lsTransferFooterdate = lsMeetingDate

                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_folio_no").ToString
                            lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_folio_no").ToString
                            lsTranCode = ds.Tables("inward").Rows(i).Item("tran_code").ToString
                            lsTranSubCode = ds.Tables("inward").Rows(i).Item("tran_subcode").ToString

                            If ds.Tables("inward").Rows(i).Item("cons_amount") > 0 Then
                                lsRemark = Format(ds.Tables("inward").Rows(i).Item("cons_amount"), "0.00")
                            End If
                        Case 2
                            lsTransferNo = "Inw.No :"
                            lsTransferDate = ds.Tables("inward").Rows(i).Item("inward_no").ToString
                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder1").ToString
                            lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder1").ToString

                            If ds.Tables("inward").Rows(i).Item("stamp_duty") > 0 Then
                                lsRemark = Format(ds.Tables("inward").Rows(i).Item("stamp_duty"), "0.00")
                            End If
                        Case 3
                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder2").ToString
                            lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder2").ToString
                        Case 4
                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder3").ToString
                            lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder3").ToString
                        Case Else
                    End Select

                    lobjTR = New clsTR(lsTransferNo, lsTransferDate, lsTransferrorName, lsTransfereeName, lsTranCode, lsShareCount, lsCertNo, lsDistSeries, lsRemark)
                    listTR.Add(lobjTR)

                    lsCertNo = ""
                    lsShareCount = ""

                    lnLineNo += 1
                Next k

                ds.Tables("dist").Rows.Clear()

                If j = ds.Tables("cert").Rows.Count - 1 Then
                    If lnLineNo = 2 Then
                        lsTransferNo = "Inw.No :"
                        lsTransferDate = ds.Tables("inward").Rows(i).Item("inward_no").ToString
                        lsDistSeries = ""
                        lsCertNo = ""
                        lsShareCount = ""
                        lsTranCode = ""
                        lsRemark = ""

                        lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder1").ToString
                        lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder1").ToString

                        If ds.Tables("inward").Rows(i).Item("stamp_duty") > 0 Then
                            lsRemark = Format(ds.Tables("inward").Rows(i).Item("stamp_duty"), "0.00")
                        End If

                        lobjTR = New clsTR(lsTransferNo, lsTransferDate, lsTransferrorName, lsTransfereeName, lsTranCode, lsShareCount, lsCertNo, lsDistSeries, lsRemark)
                        listTR.Add(lobjTR)

                        lnLineNo += 1
                    End If

                    If lnLineNo = 3 And _
                        (ds.Tables("inward").Rows(i).Item("from_holder2").ToString <> "" Or ds.Tables("inward").Rows(i).Item("to_holder2").ToString <> "") Then
                        lsTransferNo = ""
                        lsTransferDate = ""
                        lsDistSeries = ""
                        lsCertNo = ""
                        lsShareCount = ""
                        lsTranCode = ""
                        lsRemark = ""

                        lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder2").ToString
                        lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder2").ToString

                        lobjTR = New clsTR(lsTransferNo, lsTransferDate, lsTransferrorName, lsTransfereeName, lsTranCode, lsShareCount, lsCertNo, lsDistSeries, lsRemark)
                        listTR.Add(lobjTR)

                        lnLineNo += 1
                    End If

                    If lnLineNo = 4 And _
                        (ds.Tables("inward").Rows(i).Item("from_holder3").ToString <> "" Or ds.Tables("inward").Rows(i).Item("to_holder3").ToString <> "") Then
                        lsTransferNo = ""
                        lsTransferDate = ""
                        lsDistSeries = ""
                        lsCertNo = ""
                        lsShareCount = ""
                        lsTranCode = ""
                        lsRemark = ""

                        lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder3").ToString
                        lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder3").ToString

                        lobjTR = New clsTR(lsTransferNo, lsTransferDate, lsTransferrorName, lsTransfereeName, lsTranCode, lsShareCount, lsCertNo, lsDistSeries, lsRemark)
                        listTR.Add(lobjTR)

                        lnLineNo += 1
                    End If
                End If
            Next j

            ds.Tables("cert").Rows.Clear()

            lobjTR = New clsTR()
            listTR.Add(lobjTR)

            lnLineNo += 1
        Next i

        ds.Tables("inward").Rows.Clear()

        ' print to file
        For Each lobjTR In listTR
            If mnLine = 0 Then
                Call PageHeaderTR()
            End If

            msTxt = ""
            msTxt &= AlignTxt(lobjTR.TransferNo, mnColTRWidth(0), mnColTRAlign(0))
            msTxt &= AlignTxt(lobjTR.TransferDate, mnColTRWidth(1), mnColTRAlign(1))
            msTxt &= AlignTxt(lobjTR.TransferrorName, mnColTRWidth(2), mnColTRAlign(2))
            msTxt &= AlignTxt(lobjTR.TransfereeName, mnColTRWidth(3), mnColTRAlign(3))
            msTxt &= AlignTxt(lobjTR.ShareCount, mnColTRWidth(4), mnColTRAlign(4))
            msTxt &= AlignTxt(lobjTR.CertNo, mnColTRWidth(5), mnColTRAlign(5))
            msTxt &= AlignTxt(lobjTR.DistSeries, mnColTRWidth(6), mnColTRAlign(6))
            msTxt &= AlignTxt(lobjTR.Remark, mnColTRWidth(7), mnColTRAlign(7))
            msTxt &= AlignTxt(lobjTR.TranCode, mnColTRWidth(8), mnColTRAlign(8))
            If lobjTR.TranCode <> "" And lsTranSubCode <> "" Then
                msTxt &= AlignTxt(lsTranSubCode, mnColTRWidth(9), mnColTRAlign(9))
            End If

            PrintLine(1, msTxt)
            mnLine += 1

            If mnLine >= mnPageRows Then
                Call PageFooterTR()
            End If
        Next

        If mnLine > 0 Then
            Call PageFooterTR()
        End If

        ' summary
        lsSql = ""
        lsSql &= " select "
        lsSql &= " count(*) as tot_transfer,"
        lsSql &= " max(a.transfer_no) as max_transfer_no,"
        lsSql &= " min(a.transfer_no) as min_transfer_no "
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as t on t.trantype_code = a.tran_code and t.transfer_flag = 'Y' and t.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as b on b.folio_gid = a.folio_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as c on c.folio_gid = a.tran_folio_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tupload as d on d.upload_gid = a.upload_gid and d.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.transfer_no > 0 "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.transfer_no "

        Call gpDataSet(lsSql, "summary", gOdbcConn, ds)

        If ds.Tables("summary").Rows.Count > 0 Then
            PrintLine(1, "TOTAL NO. OF TRANSFER / TRANSMISSION : " & ds.Tables("summary").Rows(0).Item("tot_transfer").ToString)
            PrintLine(1, "TOTAL NO. OF CERTIFICATES : " & lnTotCert)

            PrintLine(1, "")

            msTxt = ""
            msTxt &= "TRANSFER NO. "
            msTxt &= ds.Tables("summary").Rows(0).Item("min_transfer_no").ToString
            msTxt &= " TO "
            msTxt &= ds.Tables("summary").Rows(0).Item("max_transfer_no").ToString
            msTxt &= " AGGREGATING TO "
            msTxt &= lnTotShares.ToString
            msTxt &= " SHARES APPROVED ON THIS DAY OF " & lsTransferFooterdate

            PrintLine(1, msTxt)
            PrintLine(1, "")
        End If

        ds.Tables("summary").Rows.Clear()

        Call FileClose(1)

        Call gpOpenFile(lsFileName)
    End Sub

    Private Sub LoadOR(UploadId As Long)
        Dim listLine As New List(Of clsLine)
        Dim lobjLine As clsLine
        Dim lsSql As String
        Dim ds As New DataSet
        Dim i As Long
        Dim j As Long
        Dim k As Long
        Dim l As Integer

        Dim lnLineNo As Long

        Dim lsTransferNo As String
        Dim lsTransferDate As String
        Dim lsMeetingDate As String
        Dim lsTransferrorName As String
        Dim lsTransfereeName As String
        Dim lsTranCode As String
        Dim lsShareCount As String
        Dim lsCertNo As String
        Dim lsDistSeries As String
        Dim lsRemark As String
        Dim lsDisc As String
        Dim lnTotShares As Long = 0
        Dim lsFilePath As String
        Dim lsFileName As String
        Dim lsAddrFld(6) As String
        Dim n As Integer = 0
        Dim lsTxt As String = ""

        lsAddrFld(0) = "to_addr1"
        lsAddrFld(1) = "to_addr2"
        lsAddrFld(2) = "to_addr3"
        lsAddrFld(3) = "to_city"
        lsAddrFld(4) = "to_state"
        lsAddrFld(5) = "to_country"
        lsAddrFld(6) = "to_pincode"

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name,"
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.nsdl_sno,"
        lsSql &= " c.nsdl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnUploadObjxRegister & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            msCompName = ds.Tables("upload").Rows(0).Item("comp_name").ToString

            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\Objection Register"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_no").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            If ds.Tables("upload").Rows(0).Item("upload_filename").ToString() <> "" Then
                lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()
            Else
                lsFileName = lsFilePath & "\ObjxRegister.txt"
            End If
        Else
            If Directory.Exists(gsReportPath) = False Then Call Directory.CreateDirectory(gsReportPath)
            lsFileName = gsReportPath & "ObjxRegister.txt"
        End If

        ds.Tables("upload").Rows.Clear()

        ' file creation

        If File.Exists(lsFileName) Then File.Delete(lsFileName)

        FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

        ' inward
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.inward_gid,"
        lsSql &= " a.objx_no,"
        lsSql &= " d.meeting_date as received_date,"
        'lsSql &= " a.received_date,"
        lsSql &= " a.inward_comp_no as 'inward_no',"
        'lsSql &= " a.inward_no,"
        lsSql &= " b.folio_no as from_folio_no,"
        lsSql &= " b.holder1_name as from_holder1,"
        lsSql &= " b.holder2_name as from_holder2,"
        lsSql &= " b.holder3_name as from_holder3,"
        lsSql &= " c.folio_no as to_folio_no,"
        lsSql &= " c.holder1_name as to_holder1,"
        lsSql &= " c.holder2_name as to_holder2,"
        lsSql &= " c.holder3_name as to_holder3,"
        lsSql &= " c.folio_addr1 as to_addr1,"
        lsSql &= " c.folio_addr2 as to_addr2,"
        lsSql &= " c.folio_addr3 as to_addr3,"
        lsSql &= " c.folio_city as to_city,"
        lsSql &= " c.folio_state as to_state,"
        lsSql &= " c.folio_country as to_country,"
        lsSql &= " c.folio_pincode as to_pincode,"
        lsSql &= " a.cons_amount,"
        lsSql &= " a.stamp_duty,"
        lsSql &= " a.chklst_disc,"
        lsSql &= " a.tran_code "
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as t on t.trantype_code = a.tran_code and t.objx_flag = 'Y' and t.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as b on b.folio_gid = a.folio_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as c on c.folio_gid = a.tran_folio_gid and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tupload as d on d.upload_gid = a.upload_gid and d.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.objx_no > 0 "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.objx_no "

        Call gpDataSet(lsSql, "inward", gOdbcConn, ds)

        For i = 0 To ds.Tables("inward").Rows.Count - 1
            lnLineNo = 1
            n = 0

            ' disc reascon
            lsSql = ""
            lsSql &= " select group_concat(chklst_desc) from sta_mst_tchecklist "
            lsSql &= " where chklst_value & " & ds.Tables("inward").Rows(i).Item("chklst_disc").ToString & " > 0 "
            lsSql &= " and tran_code = '" & ds.Tables("inward").Rows(i).Item("tran_code").ToString & "' "
            lsSql &= " and delete_flag = 'N' "

            lsDisc = gfExecuteScalar(lsSql, gOdbcConn)

            ' certificate 
            lsSql = ""
            lsSql &= " select "
            lsSql &= " b.cert_gid,"
            lsSql &= " b.cert_no,"
            lsSql &= " b.share_count "
            lsSql &= " from sta_trn_tcertentry as a "
            lsSql &= " inner join sta_trn_tcert as b on b.cert_gid = a.cert_gid and b.delete_flag = 'N' "
            lsSql &= " where a.inward_gid = " & ds.Tables("inward").Rows(i).Item("inward_gid").ToString & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "cert", gOdbcConn, ds)

            For j = 0 To ds.Tables("cert").Rows.Count - 1
                lsCertNo = ds.Tables("cert").Rows(j).Item("cert_no").ToString & " "
                lsShareCount = ds.Tables("cert").Rows(j).Item("share_count").ToString & " "
                lnTotShares += ds.Tables("cert").Rows(j).Item("share_count")

                ' certificate distinctive series
                lsSql = ""
                lsSql &= " select "
                lsSql &= " a.dist_from,"
                lsSql &= " a.dist_to,"
                lsSql &= " a.dist_count "
                lsSql &= " from sta_trn_tcertdist as a "
                lsSql &= " where a.cert_gid = " & ds.Tables("cert").Rows(j).Item("cert_gid").ToString & " "
                lsSql &= " and a.delete_flag = 'N' "

                Call gpDataSet(lsSql, "dist", gOdbcConn, ds)

                For k = 0 To ds.Tables("dist").Rows.Count - 1
                    lsTransferNo = ""
                    lsTransferDate = ""
                    lsTransferrorName = ""
                    lsTransfereeName = ""
                    lsTranCode = ""
                    lsRemark = ""

                    lsDistSeries = ""
                    lsDistSeries &= ds.Tables("dist").Rows(k).Item("dist_from").ToString
                    lsDistSeries &= "/"
                    lsDistSeries &= ds.Tables("dist").Rows(k).Item("dist_to").ToString

                    Select Case lnLineNo
                        Case 1
                            lsTransferNo = ds.Tables("inward").Rows(i).Item("objx_no").ToString
                            lsMeetingDate = Format(ds.Tables("inward").Rows(i).Item("received_date"), "dd-MM-yyyy")
                            lsTransferDate = Format(ds.Tables("inward").Rows(i).Item("received_date"), "dd-MM-yyyy")
                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_folio_no").ToString
                            lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_folio_no").ToString
                            lsTranCode = ds.Tables("inward").Rows(i).Item("tran_code").ToString

                            If ds.Tables("inward").Rows(i).Item("cons_amount") > 0 Then
                                lsRemark = Format(ds.Tables("inward").Rows(i).Item("cons_amount"), "0.00")
                            End If
                        Case 2
                            lsTransferNo = "Inw.No :"
                            lsTransferDate = ds.Tables("inward").Rows(i).Item("inward_no").ToString
                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder1").ToString
                            lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder1").ToString

                            If ds.Tables("inward").Rows(i).Item("stamp_duty") > 0 Then
                                lsRemark = Format(ds.Tables("inward").Rows(i).Item("stamp_duty"), "0.00")
                            End If
                        Case 3
                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder2").ToString
                            lsTxt = ""

                            For l = n To 6
                                lsTxt = ds.Tables("inward").Rows(i).Item(lsAddrFld(l)).ToString

                                If lsTxt <> "" Then
                                    n += 1
                                    Exit For
                                End If
                            Next l

                            lsTransfereeName = lsTxt
                        Case 4
                            lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder3").ToString

                            lsTxt = ""

                            For l = n To 6
                                lsTxt = ds.Tables("inward").Rows(i).Item(lsAddrFld(l)).ToString

                                If lsTxt <> "" Then
                                    n += 1
                                    Exit For
                                End If
                            Next l

                            lsTransfereeName = lsTxt
                        Case Else
                            lsTxt = ""

                            For l = n To 6
                                lsTxt = ds.Tables("inward").Rows(i).Item(lsAddrFld(l)).ToString

                                If lsTxt <> "" Then
                                    n += 1
                                    Exit For
                                End If
                            Next l

                            lsTransfereeName = lsTxt
                    End Select

                    msTxt = ""
                    msTxt &= AlignTxt(lsTransferNo, mnColTRWidth(0), mnColTRAlign(0))
                    msTxt &= AlignTxt(lsTransferDate, mnColTRWidth(1), mnColTRAlign(1))
                    msTxt &= AlignTxt(lsTransferrorName, mnColTRWidth(2), mnColTRAlign(2))
                    msTxt &= AlignTxt(lsTransfereeName, mnColTRWidth(3), mnColTRAlign(3))
                    msTxt &= AlignTxt(lsShareCount, mnColTRWidth(4), mnColTRAlign(4))
                    msTxt &= AlignTxt(lsCertNo, mnColTRWidth(5), mnColTRAlign(5))
                    msTxt &= AlignTxt(lsDistSeries, mnColTRWidth(6), mnColTRAlign(6))
                    msTxt &= AlignTxt(lsRemark, mnColTRWidth(7), mnColTRAlign(7))
                    msTxt &= AlignTxt(lsTranCode, mnColTRWidth(8), mnColTRAlign(8))

                    lobjLine = New clsLine(msTxt)
                    listLine.Add(lobjLine)

                    lsCertNo = ""
                    lsShareCount = ""

                    lnLineNo += 1
                Next k

                ds.Tables("dist").Rows.Clear()

                If j = ds.Tables("cert").Rows.Count - 1 Then
                    If lnLineNo = 1 Then
                        lsTransferNo = ds.Tables("inward").Rows(i).Item("objx_no").ToString
                        lsTransferDate = Format(ds.Tables("inward").Rows(i).Item("received_date"), "dd-MM-yyyy")
                        lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_folio_no").ToString
                        lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_folio_no").ToString
                        lsDistSeries = ""
                        lsRemark = ""

                        If ds.Tables("inward").Rows(i).Item("cons_amount") > 0 Then
                            lsRemark = Format(ds.Tables("inward").Rows(i).Item("cons_amount"), "0.00")
                        End If

                        msTxt = ""
                        msTxt &= AlignTxt(lsTransferNo, mnColTRWidth(0), mnColTRAlign(0))
                        msTxt &= AlignTxt(lsTransferDate, mnColTRWidth(1), mnColTRAlign(1))
                        msTxt &= AlignTxt(lsTransferrorName, mnColTRWidth(2), mnColTRAlign(2))
                        msTxt &= AlignTxt(lsTransfereeName, mnColTRWidth(3), mnColTRAlign(3))
                        msTxt &= AlignTxt(lsShareCount, mnColTRWidth(4), mnColTRAlign(4))
                        msTxt &= AlignTxt(lsCertNo, mnColTRWidth(5), mnColTRAlign(5))
                        msTxt &= AlignTxt(lsDistSeries, mnColTRWidth(6), mnColTRAlign(6))
                        msTxt &= AlignTxt(lsRemark, mnColTRWidth(7), mnColTRAlign(7))

                        lobjLine = New clsLine(msTxt)
                        listLine.Add(lobjLine)

                        lsCertNo = ""
                        lsShareCount = ""

                        lnLineNo += 1
                    End If

                    If lnLineNo = 2 Then
                        lsTransferNo = "Inw.No :"
                        lsTransferDate = ds.Tables("inward").Rows(i).Item("inward_no").ToString
                        lsDistSeries = ""
                        lsCertNo = ""
                        lsShareCount = ""
                        lsRemark = ""

                        lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder1").ToString
                        lsTransfereeName = ds.Tables("inward").Rows(i).Item("to_holder1").ToString

                        If ds.Tables("inward").Rows(i).Item("stamp_duty") > 0 Then
                            lsRemark = Format(ds.Tables("inward").Rows(i).Item("stamp_duty"), "0.00")
                        End If

                        msTxt = ""
                        msTxt &= AlignTxt(lsTransferNo, mnColTRWidth(0), mnColTRAlign(0))
                        msTxt &= AlignTxt(lsTransferDate, mnColTRWidth(1), mnColTRAlign(1))
                        msTxt &= AlignTxt(lsTransferrorName, mnColTRWidth(2), mnColTRAlign(2))
                        msTxt &= AlignTxt(lsTransfereeName, mnColTRWidth(3), mnColTRAlign(3))
                        msTxt &= AlignTxt(lsShareCount, mnColTRWidth(4), mnColTRAlign(4))
                        msTxt &= AlignTxt(lsCertNo, mnColTRWidth(5), mnColTRAlign(5))
                        msTxt &= AlignTxt(lsDistSeries, mnColTRWidth(6), mnColTRAlign(6))
                        msTxt &= AlignTxt(lsRemark, mnColTRWidth(7), mnColTRAlign(7))

                        lobjLine = New clsLine(msTxt)
                        listLine.Add(lobjLine)

                        lnLineNo += 1
                    End If

                    If lnLineNo = 3 And _
                        (ds.Tables("inward").Rows(i).Item("from_holder2").ToString <> "" Or ds.Tables("inward").Rows(i).Item("to_holder2").ToString <> "") Then
                        lsTransferNo = ""
                        lsTransferDate = ""
                        lsDistSeries = ""
                        lsCertNo = ""
                        lsShareCount = ""
                        lsRemark = ""

                        lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder2").ToString

                        lsTxt = ""

                        For l = n To 6
                            lsTxt = ds.Tables("inward").Rows(i).Item(lsAddrFld(l)).ToString

                            If lsTxt <> "" Then
                                n += 1
                                Exit For
                            End If
                        Next l

                        lsTransfereeName = lsTxt

                        msTxt = ""
                        msTxt &= AlignTxt(lsTransferNo, mnColTRWidth(0), mnColTRAlign(0))
                        msTxt &= AlignTxt(lsTransferDate, mnColTRWidth(1), mnColTRAlign(1))
                        msTxt &= AlignTxt(lsTransferrorName, mnColTRWidth(2), mnColTRAlign(2))
                        msTxt &= AlignTxt(lsTransfereeName, mnColTRWidth(3), mnColTRAlign(3))
                        msTxt &= AlignTxt(lsShareCount, mnColTRWidth(4), mnColTRAlign(4))
                        msTxt &= AlignTxt(lsCertNo, mnColTRWidth(5), mnColTRAlign(5))
                        msTxt &= AlignTxt(lsDistSeries, mnColTRWidth(6), mnColTRAlign(6))
                        msTxt &= AlignTxt(lsRemark, mnColTRWidth(7), mnColTRAlign(7))

                        lobjLine = New clsLine(msTxt)
                        listLine.Add(lobjLine)
                    End If

                    lnLineNo += 1

                    If lnLineNo = 4 And _
                        (ds.Tables("inward").Rows(i).Item("from_holder3").ToString <> "" Or ds.Tables("inward").Rows(i).Item("to_holder3").ToString <> "") Then
                        lsTransferNo = ""
                        lsTransferDate = ""
                        lsDistSeries = ""
                        lsCertNo = ""
                        lsShareCount = ""
                        lsRemark = ""

                        lsTransferrorName = ds.Tables("inward").Rows(i).Item("from_holder3").ToString

                        lsTxt = ""

                        For l = n To 6
                            lsTxt = ds.Tables("inward").Rows(i).Item(lsAddrFld(l)).ToString

                            If lsTxt <> "" Then
                                n += 1
                                Exit For
                            End If
                        Next l

                        lsTransfereeName = lsTxt

                        msTxt = ""
                        msTxt &= AlignTxt(lsTransferNo, mnColTRWidth(0), mnColTRAlign(0))
                        msTxt &= AlignTxt(lsTransferDate, mnColTRWidth(1), mnColTRAlign(1))
                        msTxt &= AlignTxt(lsTransferrorName, mnColTRWidth(2), mnColTRAlign(2))
                        msTxt &= AlignTxt(lsTransfereeName, mnColTRWidth(3), mnColTRAlign(3))
                        msTxt &= AlignTxt(lsShareCount, mnColTRWidth(4), mnColTRAlign(4))
                        msTxt &= AlignTxt(lsCertNo, mnColTRWidth(5), mnColTRAlign(5))
                        msTxt &= AlignTxt(lsDistSeries, mnColTRWidth(6), mnColTRAlign(6))
                        msTxt &= AlignTxt(lsRemark, mnColTRWidth(7), mnColTRAlign(7))

                        lobjLine = New clsLine(msTxt)
                        listLine.Add(lobjLine)
                    End If

                    lnLineNo += 1

                    If lnLineNo > 4 Then
                        lsTransferrorName = ""
                        lsTransferNo = ""
                        lsTransferDate = ""
                        lsDistSeries = ""
                        lsCertNo = ""
                        lsShareCount = ""
                        lsRemark = ""
                        lsTxt = ""

                        For l = n To 6
                            lsTxt = ds.Tables("inward").Rows(i).Item(lsAddrFld(l)).ToString

                            If lsTxt <> "" Then
                                lsTransfereeName = lsTxt

                                msTxt = ""
                                msTxt &= AlignTxt(lsTransferNo, mnColTRWidth(0), mnColTRAlign(0))
                                msTxt &= AlignTxt(lsTransferDate, mnColTRWidth(1), mnColTRAlign(1))
                                msTxt &= AlignTxt(lsTransferrorName, mnColTRWidth(2), mnColTRAlign(2))
                                msTxt &= AlignTxt(lsTransfereeName, mnColTRWidth(3), mnColTRAlign(3))
                                msTxt &= AlignTxt(lsShareCount, mnColTRWidth(4), mnColTRAlign(4))
                                msTxt &= AlignTxt(lsCertNo, mnColTRWidth(5), mnColTRAlign(5))
                                msTxt &= AlignTxt(lsDistSeries, mnColTRWidth(6), mnColTRAlign(6))
                                msTxt &= AlignTxt(lsRemark, mnColTRWidth(7), mnColTRAlign(7))

                                lobjLine = New clsLine(msTxt)
                                listLine.Add(lobjLine)

                                n += 1
                            End If
                        Next l
                    End If
                End If
            Next j

            ds.Tables("cert").Rows.Clear()

            ' add blank line
            lobjLine = New clsLine("")
            listLine.Add(lobjLine)

            ' add rejected reason
            msTxt = AlignTxt("Rejected Reason : " & lsDisc, mnPageWidth, 1)

            lobjLine = New clsLine(msTxt)
            listLine.Add(lobjLine)

            ' add blank line
            lobjLine = New clsLine("")
            listLine.Add(lobjLine)
        Next i

        ds.Tables("inward").Rows.Clear()

        ' print to file
        For Each lobjLine In listLine
            If mnLine = 0 Then
                Call PageHeaderOR()
            End If

            PrintLine(1, lobjLine.LineTxt)
            mnLine += 1

            If mnLine >= mnPageRows Then
                Call PageFooterTR()
            End If
        Next

        If mnLine > 0 Then
            Call PageFooterTR()
        End If

        PrintLine(1, "Total Rejected Shares : " & lnTotShares.ToString & " ON " & lsMeetingDate)

        Call FileClose(1)

        Call gpOpenFile(lsFileName)
    End Sub

    Public Sub CRUpload(UploadId As Long)
        Call InitializeCR()
        Call LoadCR(UploadId)
    End Sub

    Public Sub TRUpload(UploadId As Long)
        Call InitializeTR()
        Call LoadTR(UploadId)
    End Sub

    Public Sub ORUpload(UploadId As Long)
        Call InitializeTR()
        msColTRHead1(0) = "OBJX"
        Call LoadOR(UploadId)
    End Sub

    Private Sub LoadCR(UploadId As Long)
        Dim listCR As New List(Of clsCR)
        Dim lobjCR As clsCR
        Dim lsSql As String
        Dim ds As New DataSet
        Dim i As Long
        Dim j1 As Long
        Dim j2 As Long
        Dim k1 As Long
        Dim k2 As Long
        Dim lnLoopCount As Long

        Dim lnLineNo As Long

        Dim lsFolioNo As String
        Dim lsHolderName As String
        Dim lsHolder2Name As String = ""
        Dim lsHolder3Name As String = ""
        Dim lsLieuCertNo As String
        Dim lsLieuShareCount As String
        Dim lsLieuDistFrom As String
        Dim lsLieuDistTo As String
        Dim lsNewCertNo As String
        Dim lsNewShareCount As String = ""
        Dim lsNewDistFrom As String
        Dim lsNewDistTo As String
        Dim lnTotCert As Long = 0
        Dim lsRemark As String
        Dim lsMeetingdate As String

        Dim lsTranCode As String
        Dim lsFilePath As String
        Dim lsFileName As String

        ' upload
        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.comp_name,"
        lsSql &= " a.upload_no,"
        lsSql &= " a.upload_filename,"
        lsSql &= " a.upload_filename_extension,"
        lsSql &= " a.upload_by,"
        lsSql &= " a.nsdl_sno,"
        lsSql &= " c.nsdl_dp_id "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tentity as c on b.entity_gid = c.entity_gid and c.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.upload_type = " & gnUploadCertRegister & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        If ds.Tables("upload").Rows.Count > 0 Then
            msCompName = ds.Tables("upload").Rows(0).Item("comp_name").ToString

            lsFilePath = gsUploadPath
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\Certificate Register"
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("comp_name").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            lsFilePath = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_no").ToString
            If Directory.Exists(lsFilePath) = False Then Call Directory.CreateDirectory(lsFilePath)

            If ds.Tables("upload").Rows(0).Item("upload_filename").ToString() <> "" Then
                lsFileName = lsFilePath & "\" & ds.Tables("upload").Rows(0).Item("upload_filename").ToString()
            Else
                lsFileName = lsFilePath & "\CertRegister.txt"
            End If
        Else
            If Directory.Exists(gsReportPath) = False Then Call Directory.CreateDirectory(gsReportPath)
            lsFileName = gsReportPath & "CertRegister.txt"
        End If

        ds.Tables("upload").Rows.Clear()

        ' file creation

        If File.Exists(lsFileName) Then File.Delete(lsFileName)

        FileOpen(1, lsFileName, OpenMode.Output, OpenAccess.Write)

        ' inward
        lsSql = ""
        lsSql &= " select "
        lsSql &= " a.inward_gid,"
        lsSql &= " a.transfer_no,"
        lsSql &= " d.meeting_date as received_date,"
        lsSql &= " a.inward_comp_no as 'inward_no',"
        'lsSql &= " a.inward_no,"
        lsSql &= " b.folio_no,"
        lsSql &= " b.holder1_name,"
        lsSql &= " b.holder2_name,"
        lsSql &= " b.holder3_name,"
        lsSql &= " a.tran_code,"
        lsSql &= " a.tran_cert_no "
        lsSql &= " from sta_trn_tinward as a "
        lsSql &= " inner join sta_mst_ttrantype as t on t.trantype_code = a.tran_code and t.cert_flag = 'Y' and t.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tfolio as b on b.folio_gid = a.folio_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tupload as d on d.upload_gid = a.upload_gid and d.delete_flag = 'N' "
        lsSql &= " where a.upload_gid = " & UploadId & " "
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.tran_cert_no "

        Call gpDataSet(lsSql, "inward", gOdbcConn, ds)

        For i = 0 To ds.Tables("inward").Rows.Count - 1
            lnLineNo = 1
            lsTranCode = ds.Tables("inward").Rows(i).Item("tran_code").ToString
            lsFolioNo = ds.Tables("inward").Rows(i).Item("folio_no").ToString
            lsHolderName = ds.Tables("inward").Rows(i).Item("holder1_name").ToString
            lsHolder2Name = ds.Tables("inward").Rows(i).Item("holder2_name").ToString
            lsHolder3Name = ds.Tables("inward").Rows(i).Item("holder3_name").ToString
            lsNewCertNo = " " & ds.Tables("inward").Rows(i).Item("tran_cert_no").ToString
            lsRemark = ds.Tables("inward").Rows(i).Item("tran_code").ToString
            lsMeetingdate = Format(ds.Tables("inward").Rows(i).Item("received_date"), "dd-MM-yyyy")
            ' lieu certificate 
            lsSql = ""
            lsSql &= " select "
            lsSql &= " b.cert_gid,"
            lsSql &= " b.cert_no,"
            lsSql &= " b.share_count "
            lsSql &= " from sta_trn_tcertentry as a "
            lsSql &= " inner join sta_trn_tcert as b on b.cert_gid = a.cert_gid and b.delete_flag = 'N' "
            lsSql &= " where a.inward_gid = " & ds.Tables("inward").Rows(i).Item("inward_gid").ToString & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "lieucert", gOdbcConn, ds)
            j1 = 0

            ' total certificate
            ' lieu certificate 
            lsSql = ""
            lsSql &= " select "
            lsSql &= " sum(b.share_count) as tot "
            lsSql &= " from sta_trn_tcertentry as a "
            lsSql &= " inner join sta_trn_tcert as b on b.cert_gid = a.cert_gid and b.delete_flag = 'N' "
            lsSql &= " where a.inward_gid = " & ds.Tables("inward").Rows(i).Item("inward_gid").ToString & " "
            lsSql &= " and a.delete_flag = 'N' "

            lnTotCert = Val(gfExecuteScalar(lsSql, gOdbcConn))

            Select Case lsTranCode
                Case "CO"
                    lsNewShareCount = lnTotCert.ToString & " "
            End Select

            ' new certificate 
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.certsplitentry_gid,"
            lsSql &= " a.new_cert_no,"
            lsSql &= " a.share_count "
            lsSql &= " from sta_trn_tcertsplitentry as a "
            lsSql &= " where a.inward_gid = " & ds.Tables("inward").Rows(i).Item("inward_gid").ToString & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "newcert", gOdbcConn, ds)
            j2 = 0

            ' lieu certificate distinctive series
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.dist_from,"
            lsSql &= " a.dist_to,"
            lsSql &= " a.dist_count "
            lsSql &= " from sta_trn_tcertdist as a "
            lsSql &= " where 1 = 2 "

            Call gpDataSet(lsSql, "lieudist", gOdbcConn, ds)
            k1 = 0

            ' lieu certificate distinctive series
            lsSql = ""
            lsSql &= " select "
            lsSql &= " a.dist_from,"
            lsSql &= " a.dist_to,"
            lsSql &= " a.dist_count "
            lsSql &= " from sta_trn_tcertsplitdistentry as a "
            lsSql &= " where 1 = 2 "

            Call gpDataSet(lsSql, "newdist", gOdbcConn, ds)
            k2 = 0

            lnLoopCount = 0

            While (ds.Tables("lieucert").Rows.Count > j1) Or (ds.Tables("newcert").Rows.Count > j2)
                lsLieuCertNo = ""
                lsLieuShareCount = ""
                lsLieuDistFrom = ""
                lsLieuDistTo = ""

                lsNewDistFrom = ""
                lsNewDistTo = ""

                lnLoopCount += 1

                If j1 < ds.Tables("lieucert").Rows.Count And k1 = 0 Then
                    lsLieuCertNo = ds.Tables("lieucert").Rows(j1).Item("cert_no").ToString & " "
                    lsLieuShareCount = ds.Tables("lieucert").Rows(j1).Item("share_count").ToString & " "

                    Select Case lsTranCode
                        Case "LS"
                            lsNewShareCount = lsLieuShareCount
                    End Select

                    ' lieu certificate distinctive series
                    lsSql = ""
                    lsSql &= " select "
                    lsSql &= " a.dist_from,"
                    lsSql &= " a.dist_to,"
                    lsSql &= " a.dist_count "
                    lsSql &= " from sta_trn_tcertdist as a "
                    lsSql &= " where a.cert_gid = " & ds.Tables("lieucert").Rows(j1).Item("cert_gid").ToString & " "
                    lsSql &= " and a.delete_flag = 'N' "

                    Call gpDataSet(lsSql, "lieudist", gOdbcConn, ds)
                End If

                If j2 < ds.Tables("newcert").Rows.Count And k2 = 0 Then
                    lsNewCertNo = " " & ds.Tables("newcert").Rows(j2).Item("new_cert_no").ToString & " "
                    lsNewShareCount = ds.Tables("newcert").Rows(j2).Item("share_count").ToString & " "

                    ' lieu certificate distinctive series
                    lsSql = ""
                    lsSql &= " select "
                    lsSql &= " a.dist_from,"
                    lsSql &= " a.dist_to,"
                    lsSql &= " a.dist_count "
                    lsSql &= " from sta_trn_tcertsplitdistentry as a "
                    lsSql &= " where a.certsplitentry_gid = " & ds.Tables("newcert").Rows(j2).Item("certsplitentry_gid").ToString & " "
                    lsSql &= " and a.delete_flag = 'N' "

                    Call gpDataSet(lsSql, "newdist", gOdbcConn, ds)
                End If

                If k1 < ds.Tables("lieudist").Rows.Count Then
                    lsLieuDistFrom = ds.Tables("lieudist").Rows(k1).Item("dist_from").ToString
                    lsLieuDistTo = ds.Tables("lieudist").Rows(k1).Item("dist_to").ToString

                    If lsTranCode <> "SP" Then
                        lsNewDistFrom = lsLieuDistFrom
                        lsNewDistTo = lsLieuDistTo
                    End If

                    k1 += 1

                    If k1 >= ds.Tables("lieudist").Rows.Count Then
                        ds.Tables("lieudist").Rows.Clear()
                        k1 = 0
                        j1 += 1
                    End If
                Else
                    ds.Tables("lieudist").Rows.Clear()
                    k1 = 0
                End If

                If k2 < ds.Tables("newdist").Rows.Count Then
                    lsNewDistFrom = ds.Tables("newdist").Rows(k2).Item("dist_from").ToString
                    lsNewDistTo = ds.Tables("newdist").Rows(k2).Item("dist_to").ToString

                    k2 += 1

                    If k2 >= ds.Tables("newdist").Rows.Count Then
                        ds.Tables("newdist").Rows.Clear()
                        k2 = 0
                        j2 += 1
                    End If
                Else
                    ds.Tables("newdist").Rows.Clear()
                    k2 = 0
                End If

                Select Case lnLoopCount
                    Case 2
                        If lsHolder2Name <> "" Then lsHolderName = lsHolder2Name
                    Case 3
                        If lsHolder3Name <> "" Then lsHolderName = lsHolder3Name
                End Select

                lobjCR = New clsCR(lsFolioNo, lsHolderName, lsLieuCertNo, lsLieuShareCount, lsLieuDistFrom, lsLieuDistTo, _
                                   lsNewCertNo, lsNewShareCount, lsNewDistFrom, lsNewDistTo, lsRemark)
                listCR.Add(lobjCR)

                lsFolioNo = ""
                lsHolderName = ""
                lsNewCertNo = ""
                lsNewShareCount = ""
                lsRemark = ""
            End While

            If lnLoopCount = 1 And lsHolder2Name <> "" Then
                lsFolioNo = ""
                lsHolderName = lsHolder2Name
                lsNewCertNo = ""
                lsNewShareCount = ""
                lsRemark = ""
                lsLieuCertNo = ""
                lsLieuShareCount = ""
                lsLieuDistFrom = ""
                lsLieuDistTo = ""

                lsNewDistFrom = ""
                lsNewDistTo = ""

                lobjCR = New clsCR(lsFolioNo, lsHolderName, lsLieuCertNo, lsLieuShareCount, lsLieuDistFrom, lsLieuDistTo, _
                                   lsNewCertNo, lsNewShareCount, lsNewDistFrom, lsNewDistTo, lsRemark)
                listCR.Add(lobjCR)

                lnLoopCount += 1
            End If

            If lnLoopCount = 2 And lsHolder3Name <> "" Then
                lsFolioNo = ""
                lsHolderName = lsHolder3Name
                lsNewCertNo = ""
                lsNewShareCount = ""
                lsRemark = ""
                lsLieuCertNo = ""
                lsLieuShareCount = ""
                lsLieuDistFrom = ""
                lsLieuDistTo = ""

                lsNewDistFrom = ""
                lsNewDistTo = ""

                lobjCR = New clsCR(lsFolioNo, lsHolderName, lsLieuCertNo, lsLieuShareCount, lsLieuDistFrom, lsLieuDistTo, _
                                   lsNewCertNo, lsNewShareCount, lsNewDistFrom, lsNewDistTo, lsRemark)
                listCR.Add(lobjCR)
            End If

            ds.Tables("lieucert").Rows.Clear()
            j1 = 0

            ds.Tables("newcert").Rows.Clear()
            j2 = 0

            lobjCR = New clsCR()
            listCR.Add(lobjCR)
        Next i

        ds.Tables("inward").Rows.Clear()

        mnLine = 0

        ' print to file
        For Each lobjCR In listCR
            If mnLine = 0 Then
                Call PageHeaderCR(lsMeetingdate)
            End If

            msTxt = ""
            msTxt &= AlignTxt(lobjCR.FolioNo, mnColCRWidth(0), mnColCRAlign(0))
            msTxt &= AlignTxt(lobjCR.HolderName, mnColCRWidth(1), mnColCRAlign(1))
            msTxt &= AlignTxt(lobjCR.LieuCertNo, mnColCRWidth(2), mnColCRAlign(2))
            msTxt &= AlignTxt(lobjCR.LieuShareCount, mnColCRWidth(3), mnColCRAlign(3))
            msTxt &= AlignTxt(lobjCR.LieuDistFrom, mnColCRWidth(4), mnColCRAlign(4))
            msTxt &= AlignTxt(lobjCR.LieuDistTo, mnColCRWidth(5), mnColCRAlign(5))
            msTxt &= AlignTxt(lobjCR.NewCertNo, mnColCRWidth(6), mnColCRAlign(6))
            msTxt &= AlignTxt(lobjCR.NewShareCount, mnColCRWidth(7), mnColCRAlign(7))
            msTxt &= AlignTxt(lobjCR.NewDistFrom, mnColCRWidth(8), mnColCRAlign(8))
            msTxt &= AlignTxt(lobjCR.NewDistTo, mnColCRWidth(9), mnColCRAlign(9))
            msTxt &= AlignTxt(lobjCR.Remark, mnColCRWidth(10), mnColCRAlign(10))

            PrintLine(1, msTxt)
            mnLine += 1

            If mnLine >= mnPageRows Then
                Call PageFooterCR()
            End If
        Next

        If mnLine > 0 Then
            Call PageFooterCR()
        End If

        Call FileClose(1)

        Call gpOpenFile(lsFileName)
    End Sub

    Private Sub InitializeTR()
        Dim i As Integer

        msColTRHead1(0) = "TRANS"
        msColTRHead1(1) = "DATE"
        msColTRHead1(2) = "TRANSFERROR DETAILS"
        msColTRHead1(3) = "TRANSFEREE DETAILS"
        msColTRHead1(4) = "NO.OF"
        msColTRHead1(5) = "CERT"
        msColTRHead1(6) = "DIST.NOS."
        msColTRHead1(7) = "CONSE.AMOUNT"
        msColTRHead1(8) = ""

        msColTRHead2(0) = "NO."
        msColTRHead2(1) = ""
        msColTRHead2(2) = "FOLIO/NAME(S)"
        msColTRHead2(3) = "FOLIO/NAME(S)"
        msColTRHead2(4) = "SHARES"
        msColTRHead2(5) = "NO"
        msColTRHead2(6) = "FROM/TO"
        msColTRHead2(7) = "STAMP DUTY"
        msColTRHead2(8) = ""

        mnColTRAlign(0) = 1
        mnColTRAlign(1) = 1
        mnColTRAlign(2) = 1
        mnColTRAlign(3) = 1
        mnColTRAlign(4) = 7
        mnColTRAlign(5) = 7
        mnColTRAlign(6) = 1
        mnColTRAlign(7) = 7
        mnColTRAlign(8) = 7

        mnColTRWidth(0) = 7
        mnColTRWidth(1) = 12
        mnColTRWidth(2) = 36
        mnColTRWidth(3) = 36
        mnColTRWidth(4) = 8
        mnColTRWidth(5) = 8
        mnColTRWidth(6) = 20
        mnColTRWidth(7) = 15
        mnColTRWidth(8) = 3

        mnPageNo = 0
        mnPageWidth = 0

        For i = 0 To mnColTRWidth.Length - 1
            mnPageWidth += mnColTRWidth(i)
        Next
    End Sub

    Private Sub InitializeCR()
        Dim i As Integer

        msColCRHead1(0) = "FOLIO"
        msColCRHead1(1) = "NAME OF THE HOLDER"
        msColCRHead1(2) = "IN LIEU"
        msColCRHead1(3) = "SHARES"
        msColCRHead1(4) = "DIST.FROM"
        msColCRHead1(5) = "DIST.TO"
        msColCRHead1(6) = "NEW"
        msColCRHead1(7) = "SHARES"
        msColCRHead1(8) = "DIST.FROM"
        msColCRHead1(9) = "DIST.TO"
        msColCRHead1(10) = ""

        msColCRHead2(0) = ""
        msColCRHead2(1) = ""
        msColCRHead2(2) = "OF CERT.NO"
        msColCRHead2(3) = ""
        msColCRHead2(4) = ""
        msColCRHead2(5) = ""
        msColCRHead2(6) = "CERT.NO"
        msColCRHead2(7) = ""
        msColCRHead2(8) = ""
        msColCRHead2(9) = ""
        msColCRHead2(10) = ""

        mnColCRAlign(0) = 1
        mnColCRAlign(1) = 1
        mnColCRAlign(2) = 1
        mnColCRAlign(3) = 7
        mnColCRAlign(4) = 7
        mnColCRAlign(5) = 7
        mnColCRAlign(6) = 1
        mnColCRAlign(7) = 7
        mnColCRAlign(8) = 7
        mnColCRAlign(9) = 7
        mnColCRAlign(10) = 7

        mnColCRWidth(0) = 12
        mnColCRWidth(1) = 36
        mnColCRWidth(2) = 12
        mnColCRWidth(3) = 8
        mnColCRWidth(4) = 10
        mnColCRWidth(5) = 10
        mnColCRWidth(6) = 12
        mnColCRWidth(7) = 8
        mnColCRWidth(8) = 10
        mnColCRWidth(9) = 10
        mnColCRWidth(10) = 3

        mnPageNo = 0
        mnPageWidth = 0

        For i = 0 To mnColCRWidth.Length - 1
            mnPageWidth += mnColCRWidth(i)
        Next
    End Sub

    Private Class clsCR
        Public FolioNo As String
        Public HolderName As String
        Public LieuCertNo As String
        Public LieuShareCount As String
        Public LieuDistFrom As String
        Public LieuDistTo As String
        Public NewCertNo As String
        Public NewShareCount As String
        Public NewDistFrom As String
        Public NewDistTo As String
        Public Remark As String

        Public Sub New()

        End Sub

        Public Sub New(_FolioNo As String, _
                       _HolderName As String, _
                       _LieuCertNo As String, _
                       _LieuShareCount As String, _
                       _LieuDistFrom As String, _
                       _LieuDistTo As String, _
                       _NewCertNo As String, _
                       _NewShareCount As String, _
                       _NewDistFrom As String, _
                       _NewDistTo As String, _
                       _Remark As String)

            FolioNo = _FolioNo
            HolderName = _HolderName
            LieuCertNo = _LieuCertNo
            LieuShareCount = _LieuShareCount
            LieuDistFrom = _LieuDistFrom
            LieuDistTo = _LieuDistTo
            NewCertNo = _NewCertNo
            NewShareCount = _NewShareCount
            NewDistFrom = _NewDistFrom
            NewDistTo = _NewDistTo
            Remark = _Remark
        End Sub
    End Class

    Private Class clsLine
        Public LineTxt As String

        Public Sub New()

        End Sub

        Public Sub New(_LineTxt As String)
            LineTxt = _LineTxt
        End Sub
    End Class

    Private Class clsTR
        Public TransferNo As String
        Public TransferDate As String
        Public TransferrorName As String
        Public TransfereeName As String
        Public TranCode As String
        Public ShareCount As String
        Public CertNo As String
        Public DistSeries As String
        Public Remark As String

        Public Sub New()

        End Sub

        Public Sub New(_TransferNo As String, _
                       _TransferDate As String, _
                       _TransferrorName As String, _
                       _TransfereeName As String, _
                       _TranCode As String, _
                       _ShareCount As String, _
                       _CertNo As String, _
                       _DistSeries As String, _
                       _Remark As String)

            TransferNo = _TransferNo
            TransferDate = _TransferDate
            TransferrorName = _TransferrorName
            TransfereeName = _TransfereeName
            TranCode = _TranCode
            ShareCount = _ShareCount
            CertNo = _CertNo
            DistSeries = _DistSeries
            Remark = _Remark
        End Sub
    End Class

    Public Sub New()
    End Sub

    Public Sub ShowUpload(UploadId As Long)
        Dim lsSql As String
        Dim lnUploadType As Integer

        lsSql = ""
        lsSql &= " select upload_type from sta_trn_tupload "
        lsSql &= " where upload_gid = " & UploadId & " "
        lsSql &= " and upload_status in (" & gnUploadStatusDone & "," & gnUploadStatusSuccess & ")"
        lsSql &= " and delete_flag = 'N' "

        lnUploadType = Val(gfExecuteScalar(lsSql, gOdbcConn))

        Select Case lnUploadType
            Case gnUploadTransferRegister
                Call TRUpload(UploadId)
            Case gnUploadCertRegister
                Call CRUpload(UploadId)
            Case gnUploadObjxRegister
                Call ORUpload(UploadId)
            Case gnUploadCDSLUpload
                Call CDSLUpload(UploadId)
            Case gnUploadNSDLUpload
                Call NSDLUpload(UploadId)
            Case gnAllotmentCDSL
                Call CDSLAllotment(UploadId)
            Case gnAllotmentNSDL
                Call NSDLAllotment(UploadId)
            Case gnPhyToiepfCDSL
                Call CDSLPhysToIEPF(UploadId)
            Case gnPhyToiepfNSDL
                Call NSDLPhysToIEPF(UploadId)
        End Select
    End Sub



End Class
