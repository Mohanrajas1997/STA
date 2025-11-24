Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Word
Imports Excel

Public Class frmCAAllotment
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnFolioId As Long
    Dim mnQueueId As Long
    Dim msTranCode As String
    Dim mnChklstValid As Long
    Dim mnChklstDisc As Long
    Dim mnChkLstAllStatus As Long = 0
    Dim mnChkLstSelected As Long = 0
    Dim msGroupCode As String = ""
    Dim msDocRcvdDate As String = ""
#End Region

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmAddressChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim lsCurrQueue As String
        Dim lsSql As String
        If msGroupCode <> "V" And msGroupCode <> "C" Then
            dtpAllotmentDate.MaxDate = DateTime.Today
            dtpExecDate.MinDate = DateTime.Today
        End If

        ' CA Type
        lsSql = ""
        lsSql &= " select ca_code,ca_type from sta_mst_tcatype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by ca_type "

        Call gpBindCombo(lsSql, "ca_type", "ca_code", cboCAtype, gOdbcConn)

        ' CA Desc
        lsSql = ""
        lsSql &= " select ca_desc_code,ca_desc from sta_mst_tcaallotmentdesc "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by ca_desc "

        Call gpBindCombo(lsSql, "ca_desc", "ca_desc_code", cboAllotmentdesc, gOdbcConn)

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
                btnReject.Text = "Send Back"
                'btnSubmit.Enabled = False
                'btnReject.Enabled = False
            Case "C"
                btnReject.Text = "Send Back"
        End Select

        LoadData(mnInwardId)
    End Sub

    Public Sub LoadData(inwardid As Integer)
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim ds As System.Data.DataSet
        Dim dt As System.Data.DataTable
        Dim dt1 As System.Data.DataTable
        Dim lobjChkBoxColumn As DataGridViewCheckBoxColumn
        'Dim removeButtonColumn As DataGridViewButtonColumn
        Dim i As Integer
        Dim n As Integer

        Dim lnChkLstValid As Integer
        Dim lnChkLstDisc As Integer


        cmd = New MySqlCommand("pr_sta_get_caallotmententry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_queue_gid", mnQueueId)

        cmd.CommandTimeout = 0

        dt = New System.Data.DataTable
        dt1 = New System.Data.DataTable
        ds = New System.Data.DataSet
        da = New MySqlDataAdapter(cmd)
        da.Fill(ds)
        dt = ds.Tables(0)
        dt1 = ds.Tables(1)

        With dt
            If .Rows.Count > 0 Then
                mnCompId = .Rows(0).Item("comp_gid")
                msCompName = .Rows(0).Item("comp_name").ToString
                txtIsinId.Text = .Rows(0).Item("isin_id").ToString
                If msGroupCode <> "M" Then
                    txtInputIsinid.Text = txtIsinId.Text
                End If

                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolder.Text = .Rows(0).Item("shareholder_name").ToString

                txtSharesCount.Text = .Rows(0).Item("share_count").ToString
                txtRtarefno.Text = .Rows(0).Item("rta_internal_refno").ToString
                txtCAno.Text = .Rows(0).Item("credit_allotment_no").ToString
                'cboCAtype.Text = .Rows(0).Item("ca_type").ToString
                cboCAtype.SelectedIndex = -1
                cboCAtype.SelectedValue = .Rows(0).Item("ca_type").ToString
                Call gpAutoFillCombo(cboCAtype)

                dtpAllotmentDate.Text = .Rows(0).Item("allotment_date").ToString
                dtpExecDate.Text = .Rows(0).Item("execution_date").ToString

                'cboAllotmentdesc.Text = .Rows(0).Item("allocation_allotment_desc").ToString
                cboAllotmentdesc.SelectedIndex = -1
                cboAllotmentdesc.SelectedValue = .Rows(0).Item("allocation_allotment_desc").ToString
                Call gpAutoFillCombo(cboAllotmentdesc)

                txtDistfrom.Text = .Rows(0).Item("dist_from").ToString
                txtDistto.Text = .Rows(0).Item("dist_to").ToString

                If .Rows(0).Item("stamp_duty_flag").ToString = "Y" Then
                    cboStampDuty.Text = "Yes"
                Else
                    cboStampDuty.Text = "No"
                End If

                txtIssueamt.Text = .Rows(0).Item("total_issue_amt").ToString
                txtPaidupamt.Text = .Rows(0).Item("total_paidup_amt").ToString

                lnChkLstValid = .Rows(0).Item("chklst_valid")
                lnChkLstDisc = .Rows(0).Item("chklst_disc")

            Else
                Call frmCtrClear(Me)
            End If

            If txtFolioNo.Text = "00999999" Then
                cboCAtype.Enabled = True
                cboAllotmentdesc.Enabled = True
            Else
                cboCAtype.Enabled = False
                cboAllotmentdesc.Enabled = False
            End If
        End With

        If dt1.Rows.Count > 0 Then
            ' Add each row from dt1 to DataGridView1
            For Each row As DataRow In dt1.Rows
                dgvRecord2.Rows.Add(row("dp_id"), row("client_id"), row("dist_from"), row("dist_to"),
                                       row("share_count"), row("face_value"), row("offerprice_premium"),
                                       row("lockin_flag"), row("lockin_reason_code"), row("lockin_releasedate"),
                                       row("share_price"), row("purchase_cost"), row("stamp_duty"), "Remove")

            Next

            DisplayTotrecords.Text = dgvRecord2.Rows.Count
            displayTotshare.Text = txtSharesCount.Text
        Else
            dgvRecord2.DataSource = Nothing
        End If

        da.Dispose()
        dt.Dispose()
        dt1.Dispose()
        cmd.Dispose()

        ' load check list
        cmd = New MySqlCommand("pr_sta_get_checklist", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_tran_code", msTranCode)
        cmd.Parameters.AddWithValue("?in_auto_flag", "")

        cmd.CommandTimeout = 0

        dt = New System.Data.DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvChklst
            .DataSource = dt

            .Columns("chklst_gid").Visible = False
            .Columns("chklst_value").Visible = False
            .Columns("Check List").Width = 320

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

            mnChklstValid = lnChkLstValid

            If mnChkLstAllStatus = lnChkLstValid And Val(txtSharesCount.Text) = Val(displayTotshare.Text) Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
                btnSubmit.Enabled = True
                btnReject.Enabled = True
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
                'btnSubmit.Enabled = False
                'btnReject.Enabled = False
            End If

            If msGroupCode = "I" Then
                btnSubmit.Enabled = True
                btnReject.Enabled = True
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()

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
                dgvRecord2.ReadOnly = True
                dgvChklst.ReadOnly = True
                grpInwardDtl.Enabled = False
                btnCleardgvrec2.Enabled = False
                btnBrowse.Enabled = False
                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False
                txtRemark.Enabled = False
            Case Else
                dgvRecord2.ReadOnly = True
                dgvChklst.ReadOnly = True
                grpInwardDtl.Enabled = False
                btnCleardgvrec2.Enabled = False
                btnBrowse.Enabled = False
        End Select
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim lnResult As Long
        Dim i As Integer
        Dim n As Integer
        Dim lnChklstValue As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnNamelstValue As Long = 0
        Dim lnNamelstDis As Long = 0

        SharecountMissmatch()


        'NSDL Mandatory field validation
        If txtFolioNo.Text = "00999999" Then
            'CA Type cannot be blank
            If cboCAtype.Text.Trim() = "" Then
                MsgBox("Please select the CA Type !", MsgBoxStyle.Information, gsProjectName)
                cboCAtype.Focus()
                Exit Sub
            End If
            'Allotment desc cannot be blank
            If cboAllotmentdesc.Text.Trim() = "" Then
                MsgBox("Please select the Allotment Desc !", MsgBoxStyle.Information, gsProjectName)
                cboAllotmentdesc.Focus()
                Exit Sub
            End If
            'CDSL Mandatory field validation
        ElseIf txtFolioNo.Text = "00888888" Then
            'CA No cannot be blank
            If txtCAno.Text.Trim() = "" Then
                MessageBox.Show("CA No cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCAno.Focus()
                Exit Sub
            End If
        Else
            'Folio No Validation
            MessageBox.Show("Invalid Folio For Corporate Action allotment !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnReject.Enabled = True
            btnReject.Focus()
            Exit Sub
        End If

        'Isin Id Validation
        If txtInputIsinid.Text.Trim() <> txtIsinId.Text.Trim() Then
            MessageBox.Show("The entered ISIN does not match the selected company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtInputIsinid.Focus()
            Exit Sub
        End If

        'RTA ref no cannot be empty
        If txtRtarefno.Text.Trim() = "" Then
            MessageBox.Show("RTA Ref No cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRtarefno.Focus()
            Exit Sub
        End If

        'Stamp duty validation
        If cboStampDuty.Text.Trim() = "" Then
            MsgBox("Please select the Stampduty !", MsgBoxStyle.Information, gsProjectName)
            cboStampDuty.Focus()
            Exit Sub
        End If

        'Share Count cannot be empty
        If Val(txtSharesCount.Text) <= 0 Then
            MessageBox.Show("Share count cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSharesCount.Focus()
            Exit Sub
        End If

        'Dist From cannot be empty
        If Val(txtDistfrom.Text) <= 0 Then
            MessageBox.Show("Dist From cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDistfrom.Focus()
            Exit Sub
        End If

        'Dist To cannot be empty
        If Val(txtDistto.Text) <= 0 Then
            MessageBox.Show("Dist To cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDistto.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then


            If msGroupCode = "M" Then
                Call UpdateInformation(gnQueueSuccess)
            Else
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueSuccess)

                If lnResult = 1 Then Me.Close()
            End If
        End If
    End Sub

    Private Sub UpdateInformation(ActionStatus As Integer)
        Dim i As Integer
        Dim n As Integer = 1
        Dim m As Integer = 1

        Dim lsRtaRefno As String
        Dim lsCAno As String
        Dim lsCAtype As String
        Dim lsAllotmentdesc As String = ""
        Dim lsAllotmentdate As String = ""
        Dim lsExecutiondate As String = ""
        Dim lsStampduty As String = "N"
        Dim lsShares As String = ""
        Dim lsDistfrom As Long = 0
        Dim lsDistto As Long = 0
        Dim lsIssuedAmt As Decimal = 0
        Dim lsPaidupAmt As Decimal = 0
        Dim lsRemark As String = ""

        Dim lnChklstValid As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnChklstValue As Long = 0

        Dim lnResult As Long
        Dim lsTxt As String

        Try

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

            lsRtaRefno = QuoteFilter(txtRtarefno.Text)
            lsCAno = QuoteFilter(txtCAno.Text)
            lsCAtype = cboCAtype.SelectedValue.ToString()
            lsAllotmentdate = Format(CDate(dtpAllotmentDate.Value), "yyyy-MM-dd")
            lsAllotmentdesc = cboAllotmentdesc.SelectedValue.ToString()
            lsExecutiondate = Format(CDate(dtpExecDate.Value), "yyyy-MM-dd")
            lsDistfrom = Val(txtDistfrom.Text)
            lsDistto = Val(txtDistto.Text)
            lsShares = Val(txtSharesCount.Text)
            If cboStampDuty.Text = "Yes" Then
                lsStampduty = "Y"
            End If
            lsIssuedAmt = Val(txtIssueamt.Text)
            lsPaidupAmt = Val(txtPaidupamt.Text)

            lsRemark = QuoteFilter(txtRemark.Text)

            If dgvRecord2.Rows.Count <= 0 Then
                MessageBox.Show("Please Import the record !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnBrowse.Focus()
                Exit Sub
            End If

            'Delete a CA entry angainst inward
            Using cmd As New MySqlCommand("pr_sta_dlt_caentry", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()
            End Using

            'CA Entry 
            For Each row As DataGridViewRow In dgvRecord2.Rows
                If Not row.IsNewRow Then
                    Dim lsLockinFlag As String = "N"
                    Dim lsreleaseDate As String = ""

                    If row.Cells("lock_in_flag").Value.ToString() = "Y" Then
                        lsLockinFlag = "Y"
                    End If

                    If row.Cells("lock_in_realease_date").Value.ToString() <> "" Then
                        lsreleaseDate = row.Cells("lock_in_realease_date").Value.ToString()
                    End If


                    'lsreleaseDate = row.Cells("lock_in_realease_date").Value.ToString()

                    'Validate distinct series Implemented on 03-09-2025
                    Using cmd As New MySqlCommand("pr_sta_validate_distseries", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                        cmd.Parameters.AddWithValue("?in_share_count", Convert.ToInt32(row.Cells("share_count").Value))
                        cmd.Parameters.AddWithValue("?in_dist_from", Convert.ToInt32(row.Cells("dist_from").Value))
                        cmd.Parameters.AddWithValue("?in_dist_to", Convert.ToInt32(row.Cells("dist_to").Value))
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
                            Using cmd1 As New MySqlCommand("pr_sta_ins_caentry", gOdbcConn)
                                cmd1.CommandType = CommandType.StoredProcedure
                                cmd1.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                                cmd1.Parameters.AddWithValue("?in_caentry_gid", 0)
                                cmd1.Parameters.AddWithValue("?in_caentry_slno", m)
                                cmd1.Parameters.AddWithValue("?in_dp_id", row.Cells("dp_id").Value.ToString())
                                cmd1.Parameters.AddWithValue("?in_client_id", row.Cells("client_id").Value.ToString())
                                cmd1.Parameters.AddWithValue("?in_dist_from", Convert.ToInt32(row.Cells("dist_from").Value))
                                cmd1.Parameters.AddWithValue("?in_dist_to", Convert.ToInt32(row.Cells("dist_to").Value))
                                cmd1.Parameters.AddWithValue("?in_share_count", Convert.ToInt32(row.Cells("share_count").Value))
                                'cmd.Parameters.AddWithValue("?in_face_value", Convert.ToInt32(row.Cells("face_value").Value))
                                'cmd.Parameters.AddWithValue("?in_offerprice_premium", Convert.ToInt32(row.Cells("offerprice_premium").Value))
                                'Changes done on 16-12-2024 Integer to Decimal
                                cmd1.Parameters.AddWithValue("?in_face_value", row.Cells("face_value").Value)
                                cmd1.Parameters.AddWithValue("?in_offerprice_premium", row.Cells("offerprice_premium").Value)
                                cmd1.Parameters.AddWithValue("?in_lockin_flag", lsLockinFlag)
                                cmd1.Parameters.AddWithValue("?in_lockin_reason_code", row.Cells("lock_in_reason_code").Value.ToString())
                                If String.IsNullOrEmpty(lsreleaseDate) Then
                                    cmd1.Parameters.AddWithValue("?in_lockin_releasedate", DBNull.Value)
                                Else
                                    cmd1.Parameters.AddWithValue("?in_lockin_releasedate", Format(CDate(row.Cells("lock_in_realease_date").Value.ToString()), "yyyy-MM-dd"))
                                End If
                                'Format(CDate(row.Cells("lock_in_realease_date").Value.ToString()), "yyyy-MM-dd"))
                                cmd1.Parameters.AddWithValue("?in_share_price", row.Cells("share_price").Value)
                                cmd1.Parameters.AddWithValue("?in_purchase_cost", row.Cells("purchase_cost").Value)
                                cmd1.Parameters.AddWithValue("?in_stamp_duty", row.Cells("stamp_duty").Value)
                                cmd1.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
                                cmd1.CommandTimeout = 0

                                cmd1.ExecuteNonQuery()
                            End Using
                            m = m + 1
                        Else
                            MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    End Using

                End If
            Next


            'Update on inward table
            Using cmd As New MySqlCommand("pr_sta_upd_inwardcaentry", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_rta_internal_refno", lsRtaRefno)
                cmd.Parameters.AddWithValue("?in_credit_allotment_no", lsCAno)
                cmd.Parameters.AddWithValue("?in_ca_type", lsCAtype)
                cmd.Parameters.AddWithValue("?in_allotment_date", lsAllotmentdate)
                cmd.Parameters.AddWithValue("?in_allocation_allotment_desc", lsAllotmentdesc)
                cmd.Parameters.AddWithValue("?in_execution_date", lsExecutiondate)
                cmd.Parameters.AddWithValue("?in_dist_from", lsDistfrom)
                cmd.Parameters.AddWithValue("?in_dist_to", lsDistto)
                cmd.Parameters.AddWithValue("?in_share_count", lsShares)
                cmd.Parameters.AddWithValue("?in_stamp_duty_flag", lsStampduty)
                cmd.Parameters.AddWithValue("?in_total_issue_amt", lsIssuedAmt)
                cmd.Parameters.AddWithValue("?in_total_paidup_amt", lsPaidupAmt)
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
                    Me.Close()
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
        Dim lnResult As Long

        If txtRemark.Text = "" Then
            MessageBox.Show("Remark cannot be empty !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRemark.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then
            If msGroupCode <> "I" Then
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueReject)
            Else
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueReprocess)
            End If

            If lnResult = 1 Then Me.Close()
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

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        ' User selected Browse file 
        With ofdInput
            .Filter = "All Files|*.*"

            .Title = "Select Input File"
            ' Restore the directory to the previously selected directory
            .RestoreDirectory = True
            ' Show the file dialog
            .ShowDialog()

            ' If a valid file is selected, set the file name in the text box and load sheet names
            If .FileName <> "" AndAlso .FileName <> "OpenFileDialog1" Then

                'Share count validation
                If Val(txtSharesCount.Text) <= 0 Then
                    MessageBox.Show("Shares cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtSharesCount.Focus()
                    Exit Sub
                End If
                'Dist from validation
                If Val(txtDistfrom.Text) <= 0 Then
                    MessageBox.Show("Dist From cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtDistfrom.Focus()
                    Exit Sub
                End If
                'Dist to validation
                If Val(txtDistto.Text) <= 0 Then
                    MessageBox.Show("Dist To cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtDistto.Focus()
                    Exit Sub
                End If
                'Dist from and to validation
                If Val(txtDistto.Text) < Val(txtDistfrom.Text) Then
                    MessageBox.Show("Dist To should be greater than Dist From !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtDistto.Focus()
                    Exit Sub
                End If

                lblProgressbar.Visible = True
                txtFileName.Text = .FileName

                LoadDataToGrid(.FileName)
                lblProgressbar.Visible = False
            End If
            ' Reset the file dialog's file name
            .FileName = ""
        End With
        If mnChkLstAllStatus = mnChklstValid And Val(txtSharesCount.Text) = Val(displayTotshare.Text) Then
            lblDocStatus.Text = "Valid"
            lblDocStatus.ForeColor = Color.DarkGreen
        Else
            lblDocStatus.Text = "Invalid"
            lblDocStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub LoadDataToGrid(filePath As String)
        Dim excelApp As Excel.Application = Nothing
        Dim excelWorkbook As Workbook = Nothing
        Dim excelWorksheet As Worksheet = Nothing
        Dim excelRange As Excel.Range = Nothing
        Dim lssumofShares As Integer = 0
        Dim lssumofpurcaseCost As Decimal = 0

        Try
            ' Initialize Excel application
            excelApp = New Excel.Application()
            excelWorkbook = excelApp.Workbooks.Open(filePath)

            ' Assuming you want to load data from the first sheet
            excelWorksheet = CType(excelWorkbook.Sheets(1), Worksheet)
            excelRange = excelWorksheet.UsedRange

            ' Clear existing data in DataGridView
            dgvRecord2.Rows.Clear()

            ' Load data from Excel to DataGridView
            For rowIndex As Integer = 2 To excelRange.Rows.Count
                Dim dpId As String = If(excelRange.Cells(rowIndex, 1).Value, String.Empty).ToString()
                Dim clientId As String = If(excelRange.Cells(rowIndex, 2).Value, String.Empty).ToString()
                Dim distFrom As Integer = If(excelRange.Cells(rowIndex, 3).Value, String.Empty).ToString()
                Dim distTo As Integer = If(excelRange.Cells(rowIndex, 4).Value, String.Empty).ToString()
                Dim shareCount As Integer = If(excelRange.Cells(rowIndex, 5).Value, String.Empty).ToString()
                lssumofShares += shareCount
                'Changes done on 16-12-2024 Integer to Decimal
                Dim faceValue As Decimal = If(excelRange.Cells(rowIndex, 6).Value, String.Empty).ToString()
                Dim offerpriPremium As String = If(excelRange.Cells(rowIndex, 7).Value, String.Empty).ToString()
                Dim lockinFlag As String = If(excelRange.Cells(rowIndex, 8).Value, String.Empty).ToString()
                Dim lockinReasonCode As String = If(excelRange.Cells(rowIndex, 9).Value, String.Empty).ToString()

                'Dim lockinReleaseDate As Date
                'Dim cellValue As Object = excelRange.Cells(rowIndex, 10).Value
                'If Not IsDBNull(cellValue) AndAlso cellValue IsNot Nothing Then
                '    lockinReleaseDate = Convert.ToDateTime(cellValue)
                'Else
                '    lockinReleaseDate = Date.MinValue
                'End If
                'Dim formattedDate As String = If(lockinReleaseDate <> Date.MinValue, lockinReleaseDate.ToString("yyyy-MM-dd"), String.Empty)

                Dim lockinReleaseDate As Nullable(Of Date)
                Dim cellValue As Object = excelRange.Cells(rowIndex, 10).Value
                If Not IsDBNull(cellValue) AndAlso cellValue IsNot Nothing Then
                    lockinReleaseDate = Convert.ToDateTime(cellValue)
                Else
                    lockinReleaseDate = Nothing
                End If
                Dim formattedDate As String = If(lockinReleaseDate.HasValue, lockinReleaseDate.Value.ToString("yyyy-MM-dd"), String.Empty)

                Dim sharePrice As Decimal = faceValue + offerpriPremium
                Dim purcaseCost As Decimal = shareCount * sharePrice
                lssumofpurcaseCost += purcaseCost
                'Dim stampDuty As Decimal = purcaseCost * 0.005 / 100

                Dim stampDuty As Decimal = 0

                If cboStampDuty.Text = "Yes" Then
                    'NSDL Stamp duty Calc
                    If txtFolioNo.Text = "00999999" Then
                        stampDuty = purcaseCost * 0.005 / 100
                        'CDSL Stamp duty Calc
                    ElseIf txtFolioNo.Text = "00888888" Then
                        stampDuty = offerpriPremium
                    End If
                End If

                dgvRecord2.Rows.Add(dpId, clientId, distFrom, distTo, shareCount, faceValue,
                                 offerpriPremium, lockinFlag, lockinReasonCode, formattedDate,
                                 sharePrice, purcaseCost, stampDuty, "Remove")
            Next

            Dim ranges As New List(Of Tuple(Of Integer, Integer))()
            Dim lsDistfrom As Integer = Val(txtDistfrom.Text)
            Dim lsDistto As Integer = Val(txtDistto.Text)

            For Each row As DataGridViewRow In dgvRecord2.Rows
                If row.Cells("dist_from").Value IsNot Nothing AndAlso row.Cells("dist_to").Value IsNot Nothing Then
                    Dim fromRange As Integer = Convert.ToInt32(row.Cells("dist_from").Value)
                    Dim toRange As Integer = Convert.ToInt32(row.Cells("dist_to").Value)

                    Dim range As New Tuple(Of Integer, Integer)(fromRange, toRange)

                    ' Highlight row red if current fromRange and current toRange are not within the lsDistfrom and lsDistto
                    If Not (fromRange >= lsDistfrom AndAlso toRange <= lsDistto) Then
                        row.DefaultCellStyle.BackColor = Color.Red
                    End If

                    ' Check if range overlaps or is contained within any existing range
                    For Each existingRange As Tuple(Of Integer, Integer) In ranges
                        If (fromRange >= existingRange.Item1 AndAlso fromRange <= existingRange.Item2) OrElse _
                           (toRange >= existingRange.Item1 AndAlso toRange <= existingRange.Item2) OrElse _
                           (fromRange <= existingRange.Item1 AndAlso toRange >= existingRange.Item2) Then
                            ' Highlight the row yellow
                            row.DefaultCellStyle.BackColor = Color.Yellow
                            Exit For ' Exit the loop to avoid multiple checks for the same row
                        End If
                    Next

                    ranges.Add(range)
                End If
            Next

            DisplayTotrecords.Text = dgvRecord2.RowCount.ToString()
            displayTotshare.Text = lssumofShares.ToString()
            txtIssueamt.Text = lssumofpurcaseCost.ToString()
            txtPaidupamt.Text = lssumofpurcaseCost.ToString()

            CheckRowColors()

        Catch ex As Exception
            lblProgressbar.Visible = False
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            ' Clean up
            If excelRange IsNot Nothing Then Marshal.ReleaseComObject(excelRange)
            If excelWorksheet IsNot Nothing Then Marshal.ReleaseComObject(excelWorksheet)
            If excelWorkbook IsNot Nothing Then
                excelWorkbook.Close(False)
                Marshal.ReleaseComObject(excelWorkbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                Marshal.ReleaseComObject(excelApp)
            End If

            excelRange = Nothing
            excelWorksheet = Nothing
            excelWorkbook = Nothing
            excelApp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub CheckRowColors()
        Dim redCount As Integer = 0
        Dim yellowCount As Integer = 0
        Dim lssharecount As Integer = 0
        Dim lssumsharecount As Integer = 0

        lssharecount = Val(txtSharesCount.Text)
        lssumsharecount = Val(displayTotshare.Text)

        For Each row As DataGridViewRow In dgvRecord2.Rows
            If row.DefaultCellStyle.BackColor = Color.Red Then
                redCount += 1
            ElseIf row.DefaultCellStyle.BackColor = Color.Yellow Then
                yellowCount += 1
            End If
        Next

        If redCount > 0 Or yellowCount > 0 Or lssumsharecount <> lssharecount Then
            btnSubmit.Enabled = False
            btnReject.Enabled = False
        Else
            btnSubmit.Enabled = True
            btnReject.Enabled = True
        End If

        'share count and sum of shares validation
        If lssharecount <> lssumsharecount Then
            MessageBox.Show("Share count Missmatch !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSharesCount.Focus()
            Exit Sub
        End If

    End Sub

    Private Sub dgvCert_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRecord2.CellContentClick
        Dim lsTotShares As Integer = 0
        Dim lsTotPurchcost As Decimal = 0
        If msGroupCode = "M" Then
            Select Case dgvRecord2.Columns(dgvRecord2.CurrentCell.ColumnIndex).Name
                Case "action"
                    If MsgBox("Are you sure to remove ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                        lsTotShares = Convert.ToInt16(displayTotshare.Text)
                        lsTotPurchcost = Val(txtIssueamt.Text)

                        displayTotshare.Text = (lsTotShares - dgvRecord2.Rows((dgvRecord2.CurrentCell.RowIndex)).Cells("share_count").Value)
                        txtIssueamt.Text = (lsTotPurchcost - dgvRecord2.Rows((dgvRecord2.CurrentCell.RowIndex)).Cells("purchase_cost").Value)
                        txtPaidupamt.Text = (lsTotPurchcost - dgvRecord2.Rows((dgvRecord2.CurrentCell.RowIndex)).Cells("purchase_cost").Value)
                        Call dgvRecord2.Rows.RemoveAt(dgvRecord2.CurrentCell.RowIndex)

                        DisplayTotrecords.Text = dgvRecord2.RowCount

                        SharecountMissmatch()

                        If Val(txtSharesCount.Text) = Val(displayTotshare.Text) Then
                            lblDocStatus.Text = "Valid"
                            lblDocStatus.ForeColor = Color.DarkGreen
                        Else
                            lblDocStatus.Text = "Invalid"
                            lblDocStatus.ForeColor = Color.Red
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub btnGridclear_Click(sender As Object, e As EventArgs) Handles btnCleardgvrec2.Click
        dgvRecord2.DataSource = Nothing
        dgvRecord2.Rows.Clear()
        DisplayTotrecords.Text = 0
        displayTotshare.Text = 0
        txtIssueamt.Text = 0
        txtPaidupamt.Text = 0
    End Sub

    Private Sub txtSharesCount_Validated(sender As Object, e As EventArgs) Handles txtSharesCount.Validated
        If Val(displayTotshare.Text) > 0 Then
            If Val(txtSharesCount.Text) = Val(displayTotshare.Text) And mnChkLstAllStatus = mnChklstValid Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
                btnSubmit.Enabled = True
                btnReject.Enabled = True
                Exit Sub
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
                btnSubmit.Enabled = False
                btnReject.Enabled = False
            End If
        End If
    End Sub

    Private Sub SharecountMissmatch()
        If Val(displayTotshare.Text) > 0 Then
            'If Val(txtSharesCount.Text) <> Val(displayTotshare.Text) Then
            '    MessageBox.Show("Share count Missmatch !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    lblDocStatus.Text = "Invalid"
            '    lblDocStatus.ForeColor = Color.Red
            '    btnSubmit.Enabled = False
            '    txtSharesCount.Focus()
            '    Exit Sub
            'Else
            '    lblDocStatus.Text = "Valid"
            '    lblDocStatus.ForeColor = Color.DarkGreen
            '    btnSubmit.Enabled = True
            'End If
            If Val(txtSharesCount.Text) = Val(displayTotshare.Text) And mnChkLstAllStatus = mnChklstValid Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
                btnSubmit.Enabled = True
                btnReject.Enabled = True
                Exit Sub
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
                btnSubmit.Enabled = False
                btnReject.Enabled = False
            End If
            CheckRowColors()
        End If
    End Sub

    Private Sub RefreshShareCount()
        Dim i As Integer
        Dim lnShareCount As Long = 0

        With dgvRecord2
            For i = 0 To .RowCount - 1
                If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                    lnShareCount += .Rows(i).Cells("share_count").Value
                End If
            Next i

            displayTotshare.Text = lnShareCount.ToString()

            If mnChkLstAllStatus = mnChklstValid And Val(displayTotshare.Text) = Val(txtSharesCount.Text) Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
            End If
        End With
    End Sub

    Private Sub dgvChklst_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChklst.CellContentClick
        Dim i As Integer
        Dim lnChklstValue As Long
        Dim lnChklstValid As Long
        Dim lnChklstDisc As Long

        With dgvChklst
            If e.RowIndex >= 0 And dgvChklst.ReadOnly = False Then
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

                        mnChklstValid = lnChklstValid

                        If mnChkLstAllStatus = lnChklstValid And Val(txtSharesCount.Text) = Val(displayTotshare.Text) And Val(txtSharesCount.Text) > 0 Then
                            lblDocStatus.Text = "Valid"
                            lblDocStatus.ForeColor = Color.DarkGreen

                            btnSubmit.Enabled = True
                            btnReject.Enabled = True
                        Else
                            lblDocStatus.Text = "Invalid"
                            lblDocStatus.ForeColor = Color.Red

                            btnSubmit.Enabled = False
                            btnReject.Enabled = False
                        End If
                End Select

            End If
        End With
    End Sub

    Private Sub txtDistfrom_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDistfrom.Validating
        Call GetShareCount()
    End Sub

    Private Sub txtDistto_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDistto.Validating
        Call GetShareCount()
    End Sub

    Private Sub GetShareCount()
        Dim v_sharecount As Integer = 0
        txtSharesCount.Text = 0
        'If Val(txtDistfrom.Text) <> 0 & Val(txtDistto.Text) <> 0 Then
        v_sharecount = Val(txtDistto.Text) - Val(txtDistfrom.Text) + 1
        txtSharesCount.Text = v_sharecount
        'End If
    End Sub

    Private Sub txtInputIsinid_Leave(sender As Object, e As EventArgs) Handles txtInputIsinid.Leave
        If txtInputIsinid.Text.Trim() <> txtIsinId.Text.Trim() Then
            MessageBox.Show("The entered ISIN does not match the selected company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub
End Class