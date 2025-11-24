Public Class frmUploadQueue
    Dim msGroupCode As String

    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' upload
        lsSql = ""
        lsSql &= " select upload_type,upload_desc from sta_mst_tupload "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by upload_desc asc "

        Call gpBindCombo(lsSql, "upload_desc", "upload_type", cboUpload, gOdbcConn)
    End Sub

    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With dgvList
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvList.Top + dgvList.Height + 6
        pnlExport.Left = dgvList.Left
        pnlExport.Width = dgvList.Width
        btnExport.Left = Math.Abs(pnlExport.Width - btnExport.Width)
    End Sub

    Private Sub LoadGrid()
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lobjbtnColumn As DataGridViewButtonColumn
        Dim n As Integer
        Dim lsUploadType As String
        Dim lnUploadType As Long

        Dim lnArrUploadType(9) As Integer
        Dim lsArrUploadType(9) As String
        Dim lsArrCode(9) As String
        Dim lnStart As Integer
        Dim lnEnd As Integer

        lnArrUploadType(0) = gnUploadTransferRegister
        lnArrUploadType(1) = gnUploadCertRegister
        lnArrUploadType(2) = gnUploadObjxRegister
        lnArrUploadType(3) = gnUploadCDSLUpload
        lnArrUploadType(4) = gnUploadNSDLUpload
        lnArrUploadType(5) = gnAllotmentNSDL
        lnArrUploadType(6) = gnAllotmentCDSL
        lnArrUploadType(7) = gnPhyToiepfNSDL
        lnArrUploadType(8) = gnPhyToiepfCDSL

        lsArrUploadType(0) = "Transfer Register"
        lsArrUploadType(1) = "Certificate Register"
        lsArrUploadType(2) = "Objection Register"
        lsArrUploadType(3) = "CDSL Upload"
        lsArrUploadType(4) = "NSDL Upload"
        lsArrUploadType(5) = "Allotment NSDL"
        lsArrUploadType(6) = "Allotment CDSL"
        lsArrUploadType(7) = "PhyToIEPF NSDL"
        lsArrUploadType(8) = "PhyToIEPF CDSL"

        lsArrCode(0) = " and c.transfer_flag = 'Y' and a.queue_to = '" & msGroupCode & "' "
        lsArrCode(1) = " and c.cert_flag = 'Y' and a.queue_to = '" & msGroupCode & "' "

        lsArrCode(2) = " and a.queue_to = 'D' and c.objx_flag = 'Y' "
        lsArrCode(2) &= " and b.chklst_disc > 0 "

        lsArrCode(3) = " and c.demat_flag = 'Y' and (a.queue_to = '" & msGroupCode & "' "
        'CDSL Demat reject start 
        lsArrCode(3) &= " or (a.queue_to = 'D' and b.inward_all_status & " & gnInwardInex & " > 0) "
        'CDSL Demat reject end 
        lsArrCode(3) &= " )and f.depository_code = 'C' "

        lsArrCode(4) = " and c.demat_flag = 'Y' "
        lsArrCode(4) &= " and (a.queue_to = '" & msGroupCode & "' "
        lsArrCode(4) &= " or (a.queue_to = 'D' and b.inward_all_status & " & gnInwardInex & " > 0)) "
        lsArrCode(4) &= " and f.depository_code = 'N' "

        lsArrCode(5) = " and a.queue_to = '" & msGroupCode & "' "
        lsArrCode(5) &= " and g.folio_no = '00999999' and c.trantype_code = 'AM' "

        lsArrCode(6) = " and a.queue_to = '" & msGroupCode & "' "
        lsArrCode(6) &= " and g.folio_no = '00888888' and c.trantype_code = 'AM' "

        lsArrCode(7) = " and a.queue_to = '" & msGroupCode & "' "
        lsArrCode(7) &= " and g.folio_no = '00999999'  and c.trantype_code = 'PI' "

        lsArrCode(8) = " and a.queue_to = '" & msGroupCode & "' "
        lsArrCode(8) &= " and g.folio_no = '00888888' and c.trantype_code = 'PI' "

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cboUpload.Text = "" Or cboUpload.SelectedValue = -1 Then
            lnUploadType = 0
        Else
            lsUploadType = cboUpload.Text
            lnUploadType = Val(cboUpload.SelectedValue.ToString)
        End If

        Select Case lnUploadType
            Case gnUploadTransferRegister
                lnStart = 0
                lnEnd = 0
            Case gnUploadCertRegister
                lnStart = 1
                lnEnd = 1
            Case gnUploadObjxRegister
                lnStart = 2
                lnEnd = 2
            Case gnUploadCDSLUpload
                lnStart = 3
                lnEnd = 3
            Case gnUploadNSDLUpload
                lnStart = 4
                lnEnd = 4
            Case gnAllotmentNSDL
                lnStart = 5
                lnEnd = 5
            Case gnAllotmentCDSL
                lnStart = 6
                lnEnd = 6
            Case gnPhyToiepfNSDL
                lnStart = 7
                lnEnd = 7
            Case gnPhyToiepfCDSL
                lnStart = 8
                lnEnd = 8
            Case Else
                lnStart = 0
                lnEnd = 8
        End Select

        'Select Case lnUploadType
        '    Case gnUploadTransferRegister
        '        lsCond &= " and c.transfer_flag = 'Y' and a.queue_to = '" & msGroupCode & "' "
        '    Case gnUploadCertRegister
        '        lsCond &= " and c.cert_flag = 'Y' and a.queue_to = '" & msGroupCode & "' "
        '    Case gnUploadObjxRegister
        '        lsCond &= " and ((a.queue_to = 'D' and c.objx_flag = 'Y') "
        '        lsCond &= " or (a.queue_to = 'D' and b.inward_all_status & " & gnInwardInex & " > 0)) "
        '    Case gnUploadNSDLUpload
        '        lsCond &= " and c.demat_flag = 'Y' and a.queue_to in ('" & msGroupCode & "','D') "
        '        lsCond &= " and f.depository_code = 'N' "
        '    Case gnUploadCDSLUpload
        '        lsCond &= " and c.demat_flag = 'Y' "
        '        lsCond &= " and (a.queue_to = '" & msGroupCode & "' "
        '        lsCond &= " or (a.queue_to = 'D' and b.inward_all_status & " & gnInwardInex & " > 0)) "
        '        lsCond &= " and f.depository_code = 'C' "
        'End Select

        lsSql = ""
        For i = lnStart To lnEnd
            If lsSql <> "" Then
                lsSql &= " union "
            End If

            lsSql &= " select "
            lsSql &= " b.comp_gid,"
            lsSql &= " e.comp_name as 'Company',"
            lsSql &= " '" & lsArrUploadType(i) & "' as 'Upload Type',"
            lsSql &= " " & lnArrUploadType(i) & " as upload_type,"
            lsSql &= " count(*) as 'Document' "
            lsSql &= " from sta_trn_tqueue as a "
            lsSql &= " inner join sta_trn_tinward as b on b.inward_gid = a.inward_gid and b.queue_gid = a.queue_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tgroup as d on d.group_code = a.queue_from and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tdematpend as f on b.dematpend_gid = f.dematpend_gid and f.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as g on b.folio_gid = g.folio_gid and g.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsArrCode(i)
            lsSql &= " and a.action_status = 0 "
            lsSql &= " and b.upload_gid = 0 "
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " group by e.comp_name,b.comp_gid "
        Next i

        dgvList.Columns.Clear()

        gpPopGridView(dgvList, lsSql, gOdbcConn)

        With dgvList
            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = "Upload"
            lobjbtnColumn.Name = "Upload"
            lobjbtnColumn.Text = "Upload"

            .Columns.Add(lobjbtnColumn)

            n = .Columns.Count - 1
            .Columns(n).Width = .Columns(n).Width * 1.2

            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n).Value = "Generate Upload"
            Next i

            .Columns("Company").Width = 250
            .Columns("Upload Type").Width = 150

            .Columns("comp_gid").Visible = False
            .Columns("upload_type").Visible = False

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call ValidateDematUpload()
        Call LoadGrid()
    End Sub

    Private Sub ValidateDematUpload()
        Dim ds As New DataSet
        Dim lnInwardId As Long

        Dim lsSql As String
        Dim lsTxt As String
        Dim i As Long
        Dim lnProcessDays As Long

        lsSql = ""
        lsSql &= " select "
        lsSql &= " i.inward_gid,"
        lsSql &= " q.queue_to,"
        lsSql &= " q.action_status,"
        lsSql &= " i.chklst_disc,"
        lsSql &= " i.upload_gid,"
        lsSql &= " datediff(curdate(),i.received_date) as process_days "
        lsSql &= " from sta_trn_tinward as i "
        lsSql &= " inner join sta_trn_tqueue as q on i.queue_gid = q.queue_gid and q.delete_flag = 'N' "
        lsSql &= " where i.tran_code = 'DM' "
        lsSql &= " and i.upload_gid = 0 "
        lsSql &= " and q.queue_to = 'U' "
        lsSql &= " and q.action_status = 0 "
        lsSql &= " and i.chklst_disc = 0 "
        lsSql &= " and i.delete_flag = 'N' "

        Call gpDataSet(lsSql, "queue", gOdbcConn, ds)

        With ds.Tables("queue")
            lsTxt = "Auto Reject : Trying to process received date more than " & gnDematProcessDays & " day(s) document"

            For i = 0 To .Rows.Count - 1
                lnInwardId = .Rows(i).Item("inward_gid")
                lnProcessDays = .Rows(i).Item("process_days")

                If lnProcessDays > gnDematProcessDays Or lnProcessDays < 0 Then
                    Call UpdateQueue(lnInwardId, "U", lsTxt, gnQueueReject)
                End If
            Next i

            .Rows.Clear()
        End With
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New(Groupcode As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        msGroupCode = Groupcode
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim lnCompId As Long
        Dim lsCompName As String
        Dim lnUploadType As Integer
        Dim lsUploadType As String
        Dim frm As frmUpload

        With dgvList
            If e.RowIndex >= 0 And e.ColumnIndex = .ColumnCount - 1 Then
                lnCompId = .Rows(e.RowIndex).Cells("comp_gid").Value
                lsCompName = .Rows(e.RowIndex).Cells("Company").Value
                lnUploadType = .Rows(e.RowIndex).Cells("upload_type").Value
                lsUploadType = .Rows(e.RowIndex).Cells("Upload Type").Value

                frm = New frmUpload(msGroupCode, lnCompId, lsCompName, lnUploadType, lsUploadType)
                frm.ShowDialog()

                Call btnSearch.PerformClick()
            End If
        End With
    End Sub
End Class