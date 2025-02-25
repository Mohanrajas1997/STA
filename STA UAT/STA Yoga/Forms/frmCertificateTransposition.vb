Imports MySql.Data.MySqlClient

Public Class frmCertificateTransposition
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnQueueId As Long
    Dim msTranCode As String
    Dim mnChklstValid As Long
    Dim mnChklstDisc As Long
    Dim mnChkLstAllStatus As Long = 0
    Dim mnChkLstSelected As Long = 0
    Dim msGroupCode As String = ""
#End Region

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmAddressChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsCurrQueue As String
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select b.queue_to from sta_trn_tinward as a "
        lsSql &= " inner join sta_trn_tqueue as b on b.queue_gid = a.queue_gid and b.delete_flag = 'N' "
        lsSql &= " where a.inward_gid = " & mnInwardId & " "
        lsSql &= " and a.delete_flag = 'N' "

        lsCurrQueue = gfExecuteScalar(lsSql, gOdbcConn)

        Select Case lsCurrQueue
            Case "I"
                btnSubmit.Text = "Inex"
                btnReject.Text = "Reprocess"
            Case "M"
                btnReject.Text = "Inex"
                btnReject.Visible = False
                btnSubmit.Left = btnReject.Left
            Case "C"
                btnReject.Text = "Send Back"
        End Select

        Call LoadData()
    End Sub

    Public Sub New(GroupCode As String, InwardId As Long, QueueId As Long, TranCode As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        msGroupCode = GroupCode
        mnInwardId = InwardId
        mnQueueId = QueueId
        msTranCode = TranCode

        Select Case GroupCode
            Case "M"
            Case "V"
                dgvChklst.Enabled = False
                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False

                txtRemark.Enabled = False
            Case Else
                dgvCert.Enabled = False
                dgvChklst.Enabled = False

                grpHeader.Enabled = False
                lnkAddAttachment.Visible = False
        End Select
    End Sub

    Private Sub LoadData()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lobjChkBoxColumn As DataGridViewCheckBoxColumn
        Dim i As Integer
        Dim n As Integer
        Dim lnChkLstValid As Integer
        Dim lnChkLstDisc As Integer

        Dim lnFolioId As Long
        Dim lnCertId As Long

        cmd = New MySqlCommand("pr_sta_get_certtranspositionentry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_queue_gid", mnQueueId)

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                mnCompId = .Rows(0).Item("comp_gid")
                msCompName = .Rows(0).Item("comp_name").ToString

                lnFolioId = .Rows(0).Item("folio_gid")
                lnCertId = Val(.Rows(0).Item("cert_gid").ToString)
                txtCertId.Text = .Rows(0).Item("cert_gid").ToString

                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolder.Text = .Rows(0).Item("shareholder_name").ToString

                txtCurrHolder1.Text = .Rows(0).Item("holder1_name").ToString
                txtCurrFHName1.Text = .Rows(0).Item("holder1_fh_name").ToString
                txtCurrPanNo1.Text = .Rows(0).Item("holder1_pan_no").ToString

                txtCurrHolder2.Text = .Rows(0).Item("holder2_name").ToString
                txtCurrFHName2.Text = .Rows(0).Item("holder2_fh_name").ToString
                txtCurrPanNo2.Text = .Rows(0).Item("holder2_pan_no").ToString

                txtCurrHolder3.Text = .Rows(0).Item("holder3_name").ToString
                txtCurrFHName3.Text = .Rows(0).Item("holder3_fh_name").ToString
                txtCurrPanNo3.Text = .Rows(0).Item("holder3_pan_no").ToString

                txtNewHolder1.Text = .Rows(0).Item("new_holder1_name").ToString
                txtNewFHName1.Text = .Rows(0).Item("new_holder1_fh_name").ToString
                txtNewPanNo1.Text = .Rows(0).Item("new_holder1_pan_no").ToString

                txtNewHolder2.Text = .Rows(0).Item("new_holder2_name").ToString
                txtNewFHName2.Text = .Rows(0).Item("new_holder2_fh_name").ToString
                txtNewPanNo2.Text = .Rows(0).Item("new_holder2_pan_no").ToString

                txtNewHolder3.Text = .Rows(0).Item("new_holder3_name").ToString
                txtNewFHName3.Text = .Rows(0).Item("new_holder3_fh_name").ToString
                txtNewPanNo3.Text = .Rows(0).Item("new_holder3_pan_no").ToString

                txtRemark.Text = .Rows(0).Item("folioentry_remark").ToString

                lnChkLstValid = .Rows(0).Item("chklst_valid")
                lnChkLstDisc = .Rows(0).Item("chklst_disc")
            Else
                Call frmCtrClear(Me)
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()

        ' load certificate
        cmd = New MySqlCommand("pr_sta_get_foliocert", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_folio_gid", lnFolioId)
        cmd.Parameters.AddWithValue("?in_cert_gid", Val(txtCertId.Text))

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvCert
            .DataSource = dt

            .Columns("cert_gid").Visible = False
            .Columns("cert_status").Visible = False

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Select"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Select"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            For i = 0 To .Rows.Count - 1
                If (lnCertId = .Rows(i).Cells("cert_gid").Value) Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If
            Next i
        End With

        ' load check list
        cmd = New MySqlCommand("pr_sta_get_checklist", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_tran_code", msTranCode)
        cmd.Parameters.AddWithValue("?in_auto_flag", "")

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvChklst
            .DataSource = dt

            .Columns("chklst_gid").Visible = False
            .Columns("chklst_value").Visible = False
            .Columns("Check List").Width = 210

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Ok"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Ok"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Not Ok"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Not Ok"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            For i = 0 To .Rows.Count - 1
                mnChkLstAllStatus = mnChkLstAllStatus Or .Rows(i).Cells("chklst_value").Value

                If (lnChkLstValid And .Rows(i).Cells("chklst_value").Value) > 0 Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If

                If (lnChkLstDisc And .Rows(i).Cells("chklst_value").Value) > 0 Then
                    .Rows(i).Cells(n + 2).Value = True
                Else
                    .Rows(i).Cells(n + 2).Value = False
                End If
            Next i

            If mnChkLstAllStatus = lnChkLstValid Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
            End If
        End With
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            If msGroupCode = "M" Then
                Call UpdateInformation(gnQueueSuccess)
            Else
                Call UpdateQueue(mnInwardId, txtRemark.Text, gnQueueSuccess)
            End If
        End If
    End Sub

    Private Sub UpdateInformation(ActionStatus As Integer)
        Dim i As Integer
        Dim n As Integer

        Dim lnChklstValid As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnChklstValue As Long = 0

        Dim lnResult As Long
        Dim lsTxt As String

        Dim lnCertId As Long = 0
        Dim lnNewFolioId As Long = 0

        Dim lsHolder1 As String = ""
        Dim lsFHName1 As String = ""
        Dim lsPanNo1 As String = ""
        Dim lsHolder2 As String = ""
        Dim lsFHName2 As String = ""
        Dim lsPanNo2 As String = ""
        Dim lsHolder3 As String = ""
        Dim lsFHName3 As String = ""
        Dim lsPanNo3 As String = ""

        Dim lsAddr1 As String = ""
        Dim lsAddr2 As String = ""
        Dim lsAddr3 As String = ""
        Dim lsCity As String = ""
        Dim lsState As String = ""
        Dim lsCountry As String = ""
        Dim lsPincode As String = ""
        Dim lsRemark As String = ""

        Dim lsExecDate As String = ""
        Dim lsWitnessName As String = ""

        Dim lnShares As Long = 0
        Dim lnRate As Double = 0
        Dim lnCons As Double = 0
        Dim lnStampDuty As Double = 0

        Try
            lsHolder1 = QuoteFilter(txtNewHolder1.Text)
            lsFHName1 = QuoteFilter(txtNewFHName1.Text)
            lsPanNo1 = QuoteFilter(txtNewPanNo1.Text)
            lsHolder2 = QuoteFilter(txtNewHolder2.Text)
            lsFHName2 = QuoteFilter(txtNewFHName2.Text)
            lsPanNo2 = QuoteFilter(txtNewPanNo2.Text)
            lsHolder3 = QuoteFilter(txtNewHolder3.Text)
            lsFHName3 = QuoteFilter(txtNewFHName3.Text)
            lsPanNo3 = QuoteFilter(txtNewPanNo3.Text)

            lsRemark = QuoteFilter(txtRemark.Text)

            ' get certificate id
            lnCertId = 0

            With dgvCert
                For i = 0 To .RowCount - 1
                    If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                        lnCertId = .Rows(i).Cells("cert_gid").Value
                    End If
                Next i
            End With

            If lnCertId = 0 Then
                MessageBox.Show("Please select the certificate !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Exit Sub
            End If

            If _
                txtCurrHolder1.Text = txtNewHolder1.Text And _
                txtCurrFHName1.Text = txtNewFHName1.Text And _
                txtCurrPanNo1.Text = txtNewPanNo1.Text And _
                txtCurrHolder2.Text = txtNewHolder2.Text And _
                txtCurrFHName2.Text = txtNewFHName2.Text And _
                txtCurrPanNo2.Text = txtNewPanNo2.Text And _
                txtCurrHolder3.Text = txtNewHolder3.Text And _
                txtCurrFHName3.Text = txtNewFHName3.Text And _
                txtCurrPanNo3.Text = txtNewPanNo3.Text Then
                MessageBox.Show("Proposed information is same as current information !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            ' check list value
            With dgvChklst
                n = .Columns.Count - 1

                For i = 0 To .Rows.Count - 1
                    lnChklstValue = .Rows(i).Cells("chklst_value").Value
                    mnChkLstAllStatus = mnChkLstAllStatus Or lnChklstValue

                    If .Rows(i).Cells(n - 1).Value = True Then
                        lnChklstValid = lnChklstValid Or lnChklstValue
                    End If

                    If .Rows(i).Cells(n).Value = True Then
                        lnChklstDisc = lnChklstDisc Or lnChklstValue
                    End If
                Next i

                If mnChkLstAllStatus <> (lnChklstValid + lnChklstDisc) Then
                    MessageBox.Show("Please complete the check list !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End With

            Using cmd As New MySqlCommand("pr_sta_set_certentrytransfer", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_cert_gid", lnCertId)
                cmd.Parameters.AddWithValue("?in_new_folio_gid", lnNewFolioId)

                cmd.Parameters.AddWithValue("?in_holder1_name", lsHolder1)
                cmd.Parameters.AddWithValue("?in_holder1_fh_name", lsFHName1)
                cmd.Parameters.AddWithValue("?in_holder1_pan_no", lsPanNo1)
                cmd.Parameters.AddWithValue("?in_holder2_name", lsHolder2)
                cmd.Parameters.AddWithValue("?in_holder2_fh_name", lsFHName2)
                cmd.Parameters.AddWithValue("?in_holder2_pan_no", lsPanNo2)
                cmd.Parameters.AddWithValue("?in_holder3_name", lsHolder3)
                cmd.Parameters.AddWithValue("?in_holder3_fh_name", lsFHName3)
                cmd.Parameters.AddWithValue("?in_holder3_pan_no", lsPanNo3)

                cmd.Parameters.AddWithValue("?in_folio_addr1", lsAddr1)
                cmd.Parameters.AddWithValue("?in_folio_addr2", lsAddr2)
                cmd.Parameters.AddWithValue("?in_folio_addr3", lsAddr3)
                cmd.Parameters.AddWithValue("?in_folio_city", lsCity)
                cmd.Parameters.AddWithValue("?in_folio_state", lsState)
                cmd.Parameters.AddWithValue("?in_folio_country", lsCountry)
                cmd.Parameters.AddWithValue("?in_folio_pincode", lsPincode)

                cmd.Parameters.AddWithValue("?in_exec_date", CDate(lsExecDate))
                cmd.Parameters.AddWithValue("?in_witness_name", lsWitnessName)
                cmd.Parameters.AddWithValue("?in_witness_addr", lsWitnessAddr)
                cmd.Parameters.AddWithValue("?in_share_count", lnShares)
                cmd.Parameters.AddWithValue("?in_share_value", lnRate)
                cmd.Parameters.AddWithValue("?in_cons_amount", lnCons)
                cmd.Parameters.AddWithValue("?in_stamp_duty", lnStampDuty)

                cmd.Parameters.AddWithValue("?in_chklst_valid", lnChklstValid)
                cmd.Parameters.AddWithValue("?in_chklst_disc", lnChklstDisc)
                cmd.Parameters.AddWithValue("?in_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_action_status", ActionStatus)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                If lnResult = 1 Then
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        If txtRemark.Text = "" Then
            MessageBox.Show("Remark cannot be empty !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRemark.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            If msGroupCode <> "I" Then
                Call UpdateQueue(mnInwardId, txtRemark.Text, gnQueueReject)
            Else
                Call UpdateQueue(mnInwardId, txtRemark.Text, gnQueueReprocess)
            End If
        End If
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim frm As New frmDocHistory(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub lnkAddAttachment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAttachment.LinkClicked
        Dim frm As New frmAttachmentAdd(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub dgvChklst_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChklst.CellContentClick
        Dim i As Integer
        Dim lnChklstValue As Long
        Dim lnChklstValid As Long
        Dim lnChklstDisc As Long

        With dgvChklst
            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1, .Columns.Count - 2
                        .Rows(e.RowIndex).Cells(.Columns.Count - 1).Value = False
                        .Rows(e.RowIndex).Cells(.Columns.Count - 2).Value = False

                        .EndEdit()

                        For i = 0 To .Rows.Count - 1
                            lnChklstValue = .Rows(i).Cells("chklst_value").Value
                            mnChkLstAllStatus = mnChkLstAllStatus Or lnChklstValue

                            If .Rows(i).Cells(.Columns.Count - 2).Value = True Then
                                lnChklstValid = lnChklstValid Or lnChklstValue
                            Else
                                lnChklstDisc = lnChklstDisc Or lnChklstValue
                            End If
                        Next i

                        If mnChkLstAllStatus = lnChklstValid Then
                            lblDocStatus.Text = "Valid"
                            lblDocStatus.ForeColor = Color.DarkGreen
                        Else
                            lblDocStatus.Text = "Invalid"
                            lblDocStatus.ForeColor = Color.Red
                        End If
                End Select

            End If
        End With
    End Sub

    Private Sub dgvCert_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCert.CellContentClick
        Dim i As Integer

        With dgvCert
            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1

                        .EndEdit()
                End Select

                For i = 0 To .RowCount - 1
                    If i <> e.RowIndex Then
                        .Rows(i).Cells(.Columns.Count - 1).Value = False
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub btnSwapOneAndTwo_Click(sender As Object, e As EventArgs) Handles btnSwapOneAndTwo.Click
        Dim lsHolderName As String = ""
        Dim lsFHName As String = ""
        Dim lsPanNo As String = ""

        If txtNewHolder1.Text = "" Or txtNewHolder2.Text = "" Then
            MessageBox.Show("Blank data !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        lsHolderName = txtNewHolder1.Text
        lsFHName = txtNewFHName1.Text
        lsPanNo = txtNewPanNo1.Text

        txtNewHolder1.Text = txtNewHolder2.Text
        txtNewFHName1.Text = txtNewFHName2.Text
        txtNewPanNo1.Text = txtNewPanNo2.Text

        txtNewHolder2.Text = lsHolderName
        txtNewFHName2.Text = lsFHName
        txtNewPanNo2.Text = lsPanNo
    End Sub

    Private Sub btnSwapTwoAndThree_Click(sender As Object, e As EventArgs) Handles btnSwapTwoAndThree.Click
        Dim lsHolderName As String = ""
        Dim lsFHName As String = ""
        Dim lsPanNo As String = ""

        If txtNewHolder2.Text = "" Or txtNewHolder3.Text = "" Then
            MessageBox.Show("Blank data !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        lsHolderName = txtNewHolder2.Text
        lsFHName = txtNewFHName2.Text
        lsPanNo = txtNewPanNo2.Text

        txtNewHolder2.Text = txtNewHolder3.Text
        txtNewFHName2.Text = txtNewFHName3.Text
        txtNewPanNo2.Text = txtNewPanNo3.Text

        txtNewHolder3.Text = lsHolderName
        txtNewFHName3.Text = lsFHName
        txtNewPanNo3.Text = lsPanNo
    End Sub
End Class