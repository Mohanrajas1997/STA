Public Class frmQueue
    Dim msGroupCode As String

    Private Sub frmQueue_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Call btnSearch.PerformClick()
    End Sub

    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' doc type
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "

        Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)
    End Sub

    Private Sub frmQueue_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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
        Dim lsTxt As String
        Dim n As Integer

        If cboDocType.Text <> "" And cboDocType.SelectedIndex <> -1 Then
            lsCond &= " and b.tran_code = '" & cboDocType.SelectedValue.ToString & "' "
        End If

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and b.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If txtInwardNo.Text <> "" Then lsCond &= " and b.inward_no = " & Val(txtInwardNo.Text) & " "

        If txtCompInwadNo.Text <> "" Then lsCond &= " and b.inward_comp_no = '" & txtCompInwadNo.Text.Trim & "'"

        If msGroupCode = "DU" And lsCond = "" Then lsCond &= " and 1 = 2 "

        If gsLoginUserCode.ToLower() <> "admin" And msGroupCode = "C" Then lsCond &= " and a.queue_from_user <> '" & gsLoginUserCode & "' "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " b.inward_comp_no as 'Comp Inward No',"
        lsSql &= " b.inward_no as 'Inward No',"
        lsSql &= " e.comp_name as 'Company',"
        lsSql &= " b.folio_no as 'Folio No',"
        lsSql &= " b.shareholder_name as 'Share Holder',"
        lsSql &= " b.tran_code,"
        lsSql &= " c.trantype_desc as 'Document',"
        lsSql &= " date_format(a.queue_date,'%d-%m-%Y') as 'Queue Date',"
        lsSql &= " d.group_name as 'Queue From',"
        'lsSql &= " a.queue_from_user as 'Last Action By',"
        lsSql &= " fn_sta_get_behalfof(a.queue_from_user,a.queue_date) as 'Last Action By',"
        lsSql &= " a.inward_gid,"
        lsSql &= " a.queue_gid "
        lsSql &= " from sta_trn_tqueue as a "
        lsSql &= " inner join sta_trn_tinward as b on b.inward_gid = a.inward_gid and b.queue_gid = a.queue_gid and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tgroup as d on d.group_code = a.queue_from and c.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N' "
        lsSql &= " where a.queue_to = '" & Mid(msGroupCode, 1, 1) & "' "

        If msGroupCode = "DU" Then
            lsSql &= " and a.action_status = " & gnQueueSuccess
        Else
            lsSql &= " and a.action_status = 0 "
        End If

        If Mid(msGroupCode, 1, 1) <> "D" Then
            lsSql &= " and b.upload_gid = 0 "
        End If

        lsSql &= lsCond
        lsSql &= " and a.delete_flag = 'N' "
        lsSql &= " order by a.queue_gid desc"

        With dgvList
            .Columns.Clear()

            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(i).ReadOnly = True
            Next i

            .Columns("tran_code").Visible = False
            .Columns("inward_gid").Visible = False
            .Columns("queue_gid").Visible = False

            n = .Columns.Count

            If msGroupCode <> "U" Then
                lsTxt = "Action"
            Else
                lsTxt = "Reject"
            End If

            lobjbtnColumn = New DataGridViewButtonColumn
            lobjbtnColumn.HeaderText = lsTxt
            lobjbtnColumn.Width = 100
            lobjbtnColumn.Name = lsTxt
            lobjbtnColumn.Text = lsTxt

            .Columns.Add(lobjbtnColumn)

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n).Value = lsTxt
            Next i

            txtTotRec.Text = "Total Records : " & .RowCount.ToString
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim lnInwardId As Long
        Dim lnQueueId As Long
        Dim lsTranCode As String
        Dim objFrm As Form

        With dgvList
            If e.RowIndex >= 0 Then
                If e.ColumnIndex = .Columns.Count - 1 Then
                    lnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value
                    lnQueueId = dgvList.Rows(e.RowIndex).Cells("queue_gid").Value
                    lsTranCode = dgvList.Rows(e.RowIndex).Cells("tran_code").Value

                    If Mid(msGroupCode, 1, 1) = "D" Then
                        objFrm = New frmOutwardEntry("ADD", lnInwardId)
                        objFrm.ShowDialog()
                    ElseIf msGroupCode = "N" Then
                        'objFrm = New frmInwardEntry("UPDATE", lnInwardId)
                        objFrm = New frmInwardEntryNew("UPDATE", lnInwardId)
                        objFrm.ShowDialog()
                    Else
                        Select Case lsTranCode
                            Case "CA"
                                objFrm = New frmAddressChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "NC"
                                objFrm = New frmNameCorrection(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "CP"
                                objFrm = New frmPanChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "CC"
                                objFrm = New frmCategoryChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "NU"
                                objFrm = New frmNomineeChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "CB"
                                objFrm = New frmBankDetailsChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "TR"
                                objFrm = New frmCertificateTransfer(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "TM"
                                objFrm = New frmCertificateTransmission_new(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Certificate Transmission"
                                objFrm.ShowDialog()
                            Case "TP"
                                objFrm = New frmCertificateTransmission_new(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Certificate Transposition"
                                objFrm.ShowDialog()
                            Case "OL"
                                objFrm = New frmCertificateTransmission_new(msGroupCode, lnInwardId, lnQueueId, lsTranCode, False)
                                objFrm.Text = "Certificate Old Transfer"
                                objFrm.ShowDialog()
                            Case "FC"
                                objFrm = New frmCertificateTransmission_new(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Folio Consolidation"
                                objFrm.ShowDialog()
                            Case "ST"
                                ' Stop Transfer
                                objFrm = New frmCertificateHold(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Certificate Stop Transfer"
                                objFrm.ShowDialog()
                            Case "RT"   ' release for transfer
                                objFrm = New frmCertificateHold(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Certificate Release Stop Transfer"
                                objFrm.ShowDialog()
                            Case "CL"
                                ' Certificate Lock
                                objFrm = New frmCertificateLock(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "RL"
                                ' Release Lock
                                objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Release Lock"
                                objFrm.ShowDialog()
                            Case "CO"
                                ' Consolidation
                                objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Certificate Consolidation"
                                objFrm.ShowDialog()
                            Case "LS"
                                ' Consolidation
                                objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Lose of Certificate"
                                objFrm.ShowDialog()
                            Case "DP"
                                ' Duplicate
                                objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Duplicate Certificate"
                                objFrm.ShowDialog()
                            Case "SP"
                                ' Split
                                objFrm = New frmCertificateSplit(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "DM"
                                ' Demat
                                objFrm = New frmCertificateDemat(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "DT"
                                ' Depository Transfer
                                objFrm = New frmDepositoryTransfer(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "CS"
                                ' Depository Certificate Split
                                objFrm = New frmDepCertificateSplit(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "DS"
                                ' Depository Certificate Distinctive Series Split
                                objFrm = New frmDepCertificateDistSplit(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.ShowDialog()
                            Case "DC"
                                ' Depository Certificate Consolidation
                                objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Depository Certificate Consolidation"
                                objFrm.ShowDialog()
                            Case "RM"
                                ' remat 
                                objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Remat"
                                objFrm.ShowDialog()
                            Case "SG"
                                ' Signature
                                objFrm = New frmSignature(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "Signature"
                                objFrm.ShowDialog()
                            Case "IS"
                                'ISR1
                                objFrm = New frmISR1(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "ISR1"
                                objFrm.ShowDialog()
                            Case "AM"
                                'CA Allotment
                                objFrm = New frmCAAllotment(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "CA Allotment"
                                objFrm.ShowDialog()
                            Case "PI"
                                'CA Physical to IEPF
                                objFrm = New frmCAPhysicalToIEPF(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "CA Physical To IEPF"
                                objFrm.ShowDialog()
                            Case "IE"
                                'IEPF Maker
                                objFrm = New frmiepfmaker(lnInwardId, msGroupCode)
                                objFrm.Text = "IEPF Claim"
                                objFrm.ShowDialog()
                            Case "LT"
                                ' LS Come Transmission
                                objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                                objFrm.Text = "LS Cum Transmission"
                                objFrm.ShowDialog()
                        End Select
                    End If
                End If

                Call btnSearch.PerformClick()
            End If
        End With
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        Dim lnInwardId As Long
        Dim lnQueueId As Long
        Dim lsTranCode As String
        Dim objFrm As Form

        If e.RowIndex >= 0 Then
            lnInwardId = dgvList.Rows(e.RowIndex).Cells("inward_gid").Value
            lnQueueId = dgvList.Rows(e.RowIndex).Cells("queue_gid").Value
            lsTranCode = dgvList.Rows(e.RowIndex).Cells("tran_code").Value

            If msGroupCode <> "D" Then
                Select Case lsTranCode
                    Case "CA"
                        objFrm = New frmAddressChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "NC"
                        objFrm = New frmNameCorrection(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "CP"
                        objFrm = New frmPanChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "CC"
                        objFrm = New frmCategoryChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "NU"
                        objFrm = New frmNomineeChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "CB"
                        objFrm = New frmBankDetailsChange(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "TR"
                        objFrm = New frmCertificateTransfer(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "TM"
                        objFrm = New frmCertificateTransmission_new(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Certificate Transmission"
                        objFrm.ShowDialog()
                    Case "TP"
                        objFrm = New frmCertificateTransmission_new(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Certificate Transposition"
                        objFrm.ShowDialog()
                    Case "FC"
                        objFrm = New frmCertificateTransmission_new(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Folio Consolidation"
                        objFrm.ShowDialog()
                    Case "ST"
                        ' Stop Transfer
                        objFrm = New frmCertificateHold(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Certificate Stop Transfer"
                        objFrm.ShowDialog()
                    Case "RT"   ' release for transfer
                        objFrm = New frmCertificateHold(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Certificate Release Stop Transfer"
                        objFrm.ShowDialog()
                    Case "CL"
                        ' Certificate Lock
                        objFrm = New frmCertificateLock(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "RL"
                        ' Release Lock
                        objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Release Lock"
                        objFrm.ShowDialog()
                    Case "CO"
                        ' Consolidation
                        objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Certificate Consolidation"
                        objFrm.ShowDialog()
                    Case "LS"
                        ' Consolidation
                        objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Lose of Certificate"
                        objFrm.ShowDialog()
                    Case "SP"
                        ' Split
                        objFrm = New frmCertificateSplit(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "DM"
                        ' Demat
                        objFrm = New frmCertificateDemat(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.ShowDialog()
                    Case "RM"
                        ' remat 
                        objFrm = New frmCertificateTran(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Remat"
                        objFrm.ShowDialog()
                    Case "SG"
                        ' Signature
                        objFrm = New frmSignature(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "Signature"
                        objFrm.ShowDialog()
                    Case "IS"
                        ' ISR
                        objFrm = New frmISR1(msGroupCode, lnInwardId, lnQueueId, lsTranCode)
                        objFrm.Text = "ISR"
                End Select
            Else
                objFrm = New frmOutwardEntry("ADD", lnInwardId)
                objFrm.ShowDialog()
            End If
        End If
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
End Class