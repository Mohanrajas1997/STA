Imports System.IO
Imports System.IO.FileStream


Public Class frmCAAllotmentUpload


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

    Private Sub btnNsdl_Click(sender As Object, e As EventArgs) Handles btnNsdl.Click
        Dim UploadId = 3262
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
            lsSql &= " a.rta_internal_refno,"
            lsSql &= " a.ca_type,"
            lsSql &= " a.allocation_allotment_desc,"
            lsSql &= " lpad(a.dist_from,18,0) as dist_from,"
            lsSql &= " lpad(a.dist_to,18,0) as dist_to,"
            lsSql &= " a.stamp_duty_flag,"
            lsSql &= " concat(lpad(a.total_issue_amt,16,0),'00') as total_issue_amt,"
            lsSql &= " concat(lpad(a.total_paidup_amt,16,0),'00') as total_paidup_amt,"
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
            lsSql &= " concat(lpad(e.dist_from,16,0),'00') as dist_from,"
            lsSql &= " concat(lpad(e.dist_to,16,0),'00') as dist_to,"
            lsSql &= " concat(lpad(cast(e.share_count as signed),15,0),'000') as allotment_qty,"
            lsSql &= " concat(lpad(cast(e.share_price as signed),12,0),'000000') as issue_price,"
            lsSql &= " concat(lpad((e.share_count * cast(e.share_price as signed)),16,0),'00') as issued_amt,"
            lsSql &= " concat(lpad(cast(e.purchase_cost as signed),12,0),'000000') as paidup_price,"
            lsSql &= " concat(lpad((e.share_count * cast(e.purchase_cost as signed)),16,0),'00') as paidup_amt"
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
                lsTxt &= AlignTxt("01SHRI001Allotment ESOP43C", 26, 1)
                lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("ca_type").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("allotment_date").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("allocation_allotment_desc").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("execution_date").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("share_count").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("share_count").ToString()
                lsTxt &= AlignTxt(Format(lnRecCount, "0000000"), 7, 1) ' Total number of detail records
                lsTxt &= ds.Tables("header").Rows(0).Item("total_issue_amt").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("total_paidup_amt").ToString()
                lsTxt &= ds.Tables("header").Rows(0).Item("stamp_duty_flag").ToString()
                lsTxt &= AlignTxt("000000", 6, 1)
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
                    lsTxt &= AlignTxt("", 8, 1)
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

    Private Sub btnCDSL_Click(sender As Object, e As EventArgs) Handles btnCDSL.Click
        Dim UploadId = 3266
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
            lsSql &= " concat(e.dp_id,e.client_id) as boid,"
            lsSql &= " concat(lpad(e.dist_from,16,0),'00') as dist_from,"
            lsSql &= " concat(lpad(e.dist_to,16,0),'00') as dist_to,"
            lsSql &= " concat(lpad(cast(e.share_count as signed),12,0),'.000') as current_qty,"
            lsSql &= " concat(lpad(cast(e.stamp_duty as signed),12,0),'.000') as stamp_duty"
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
                If ds.Tables("header").Rows(0).Item("share_count").ToString() = "Y" Then
                    lsTxt &= AlignTxt(Format(lnRecCount, "0000000001"), 10, 1) ' 
                Else
                    lsTxt &= AlignTxt(Format(lnRecCount, "0000000000"), 10, 1) ' 
                End If

                Call Print(1, lsTxt)

                ' Detail
                For j = 0 To ds.Tables("detail").Rows.Count - 1
                    lnLineNo += 1

                    lsTxt = vbNewLine
                    lsTxt &= AlignTxt("02", 2, 1)
                    lsTxt &= AlignTxt(Format(lnLineNo, "0000000"), 7, 1)
                    lsTxt &= ds.Tables("detail").Rows(j).Item("boid").ToString()
                    lsTxt &= ds.Tables("header").Rows(0).Item("rta_internal_refno").ToString()
                    lsTxt &= ds.Tables("header").Rows(0).Item("isin_id").ToString()
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    lsTxt &= AlignTxt("000000000000.000", 16, 1)
                    lsTxt &= AlignTxt("", 50, 1)
                    lsTxt &= AlignTxt("00000000", 8, 1)
                    lsTxt &= AlignTxt("D", 1, 1)
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
End Class