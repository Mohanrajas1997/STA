Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices

Public Class frmCAPhysicalToIEPF
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnFolioId As Long
    Dim mnQueueId As Long
    Dim mnCAiepfhdrId As Long
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

    Private Sub frmCAPhysicalToIEPF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsCurrQueue As String
        Dim lsSql As String

        If msGroupCode <> "V" And msGroupCode <> "C" Then
            dtpApprovalDate.MaxDate = DateTime.Today
            dtpExecDate.MinDate = DateTime.Today
        End If

        ' CA Type
        lsSql = ""
        lsSql &= " select ca_code,ca_type from sta_mst_tcatype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by ca_type "

        Call gpBindCombo(lsSql, "ca_type", "ca_code", cboCAtype, gOdbcConn)

        'Div FinYear
        lsSql = ""
        lsSql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        lsSql &= " where delete_flag = 'N' "

        Call gpBindCombo(lsSql, "finyear_code", "finyear_gid", cbodivFinyear, gOdbcConn)


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
                btnSubmit.Enabled = False
                btnReject.Enabled = False
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

        cmd = New MySqlCommand("pr_sta_get_caiepfentry", gOdbcConn)
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
                mnCAiepfhdrId = .Rows(0).Item("caiepfhdr_gid")

                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolder.Text = .Rows(0).Item("shareholder_name").ToString
                txtCompEmailId.Text = .Rows(0).Item("email_id").ToString
                txtCINno.Text = .Rows(0).Item("cin_no").ToString
                txtRtarefNo.Text = .Rows(0).Item("rta_internal_ref_no").ToString
                cboCAtype.SelectedIndex = -1
                cboCAtype.SelectedValue = .Rows(0).Item("catype").ToString

                Call gpAutoFillCombo(cboCAtype)
                cbodivFinyear.SelectedIndex = -1
                cbodivFinyear.SelectedValue = .Rows(0).Item("div_finyear").ToString
                Call gpAutoFillCombo(cbodivFinyear)

                dtpExecDate.Text = .Rows(0).Item("execution_date").ToString
                dtpApprovalDate.Text = .Rows(0).Item("board_approval_date").ToString

                txtSharesCount.Text = .Rows(0).Item("tot_nominal_amtof_shares").ToString
                txtCreditQty.Text = .Rows(0).Item("tot_credit_qty").ToString
                txtDebitQty.Text = .Rows(0).Item("tot_debit_qty").ToString
                txtCreditLockin.Text = .Rows(0).Item("tot_credit_lockin_qty").ToString
                txtDebitLockin.Text = .Rows(0).Item("tot_debit_lockin_qty").ToString

                lnChkLstValid = .Rows(0).Item("chklst_valid")
                lnChkLstDisc = .Rows(0).Item("chklst_disc")

            Else
                Call frmCtrClear(Me)
            End If


        End With

        If dt1.Rows.Count > 0 Then
            ' Add each row from dt1 to DataGridView1
            For Each row As DataRow In dt1.Rows
                dgvRecord2.Rows.Add(row("dp_id"), row("client_id"), row("folio_no"), row("dist_from"), row("dist_to"),
                                    row("credit_qty"), row("debit_qty"), row("holder1_name"), row("investor_category"),
                                    row("creditqty_lockin_reasoncode"), row("creditqty_lockin_releasedate"),
                                    row("debitqty_lockin_reasoncode"), row("debitqty_lockin_releasedate"),
                                    row("bo_address1"), row("bo_address2"), row("bo_address3"), row("bo_address_city"),
                                    row("bo_address_state"), row("bo_address_country"), row("bo_address_pincode"), "Remove")

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
                btnSubmit.Enabled = False
                btnReject.Enabled = False
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
                grpPhyysicalIEPFhdr.Enabled = False
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
                grpPhyysicalIEPFhdr.Enabled = False
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
        Dim colorFound As Boolean = False

        SharecountMissmatch()

        'NSDL Mandatory field validation
        If txtFolioNo.Text = "00999999" Then
            'CA Type cannot be blank
            If cboCAtype.Text.Trim() = "" Then
                MsgBox("Please select the CA Type !", MsgBoxStyle.Information, gsProjectName)
                cboCAtype.Focus()
                Exit Sub
            End If

            'CDSL Mandatory field validation
        ElseIf txtFolioNo.Text = "00888888" Then

        Else
            'Folio No Validation
            MessageBox.Show("Invalid Folio For Corporate Action Physical To IEPF !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnReject.Enabled = True
            btnReject.Focus()
            Exit Sub
        End If

        'Email ID cannot be empty
        If txtCompEmailId.Text.Trim() = "" Then
            MessageBox.Show("Email ID cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCompEmailId.Focus()
            Exit Sub
        End If

        'CINO cannot be empty
        If txtCINno.Text.Trim() = "" Then
            MessageBox.Show("Cin NO cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCINno.Focus()
            Exit Sub
        End If

        'RTA ref no cannot be empty
        If txtRtarefNo.Text.Trim() = "" Then
            MessageBox.Show("RTA Ref No cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRtarefNo.Focus()
            Exit Sub
        End If

        'Share Count cannot be empty
        If Val(txtSharesCount.Text) <= 0 Then
            MessageBox.Show("Share count cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSharesCount.Focus()
            Exit Sub
        End If

        'Please clear the error or remove the highlighted row
        For Each row As DataGridViewRow In dgvRecord2.Rows
            If row.DefaultCellStyle.BackColor = Color.Yellow OrElse row.DefaultCellStyle.BackColor = Color.Red Then
                colorFound = True
                Exit For
            End If
        Next

        If colorFound Then
            MessageBox.Show("Please clear the error or remove the highlighted row", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim lsCinno As String
        Dim lsEmailid As String
        Dim lsCAtype As String
        Dim lsApprovaldate As String = ""
        Dim lsExecutiondate As String = ""
        Dim lsDivfinyear As String
        Dim lsShares As String = ""
        Dim lsCreditqty As Long = 0
        Dim lsDebitqty As Long = 0
        Dim lsCreditLockin As Long = 0
        Dim lsDebitLockin As Long = 0
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

            lsRtaRefno = QuoteFilter(txtRtarefNo.Text)
            lsCinno = QuoteFilter(txtCINno.Text)
            lsEmailid = QuoteFilter(txtCompEmailId.Text)
            lsCAtype = cboCAtype.SelectedValue.ToString()
            lsApprovaldate = Format(CDate(dtpApprovalDate.Value), "yyyy-MM-dd")
            lsExecutiondate = Format(CDate(dtpExecDate.Value), "yyyy-MM-dd")
            lsDivfinyear = cbodivFinyear.SelectedValue.ToString()
            lsCreditqty = Val(txtCreditQty.Text)
            lsDebitqty = Val(txtDebitQty.Text)
            lsCreditLockin = Val(txtCreditLockin.Text)
            lsDebitLockin = Val(txtDebitLockin.Text)
            lsShares = Val(txtSharesCount.Text)
            lsRemark = QuoteFilter(txtRemark.Text)

            If dgvRecord2.Rows.Count <= 0 Then
                MessageBox.Show("Please Import the record !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnBrowse.Focus()
                Exit Sub
            End If

            'Delete a CA iepffoli against inward
            Using cmd As New MySqlCommand("pr_sta_dlt_caiepffolio", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()
            End Using

            'CA iepffolio 
            For Each row As DataGridViewRow In dgvRecord2.Rows
                If Not row.IsNewRow Then
                    Dim lsSql As String = ""
                    Dim lsBenCat As String = ""
                    Dim lnInveCat As String = ""
                    lnInveCat = row.Cells("investor_category").Value.ToString()

                    If lnInveCat <> "" Then
                        'Investor category Validation
                        lsSql = ""
                        lsSql &= " select 1 from sta_mst_tbencategory "
                        lsSql &= " where bencategory_name = '" & row.Cells("investor_category").Value.ToString() & "'"
                        lsSql &= " and delete_flag = 'N' "

                        lsBenCat = gfExecuteScalar(lsSql, gOdbcConn)

                        If lsBenCat = "" Then
                            MessageBox.Show("Invalid Investor Category Name : " & lnInveCat, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            'Delete a CA iepffoli against inward
                            Using cmd As New MySqlCommand("pr_sta_dlt_caiepffolio", gOdbcConn)
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                                cmd.CommandTimeout = 0

                                cmd.ExecuteNonQuery()
                            End Using
                            Exit Sub
                        End If
                    End If

                    'Dim lsLockinFlag As String = "N"
                    Dim lscreditlockinDate As String = ""
                    Dim lsdebitlockinDate As String = ""

                    If row.Cells("creditqty_lockin_releasedate").Value <> "" Then
                        lscreditlockinDate = row.Cells("creditqty_lockin_releasedate").Value.ToString()
                    End If

                    If row.Cells("debitqty_lockin_releasedate").Value <> "" Then
                        lsdebitlockinDate = row.Cells("debitqty_lockin_releasedate").Value.ToString()
                    End If

                    'lsreleaseDate = row.Cells("creditqty_lockin_releasedate").Value.ToString()
                    'lsreleaseDate = row.Cells("debitqty_lockin_releasedate").Value.ToString()

                    Using cmd As New MySqlCommand("pr_sta_ins_caiepffolio", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_caiepffolio_gid", 0)
                        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                        cmd.Parameters.AddWithValue("?in_caiepffolio_slno", m)
                        cmd.Parameters.AddWithValue("?in_dp_id", row.Cells("dp_id").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_client_id", row.Cells("clientid").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_folio_no", row.Cells("foliono").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_dist_from", row.Cells("dist_from").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_dist_to", row.Cells("dist_to").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_credit_qty", row.Cells("credit_qty").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_debit_qty", row.Cells("debit_qty").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_holder1_name", row.Cells("holder1_name").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_holder2_name", row.Cells("holder2_name").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_holder3_name", row.Cells("holder3_name").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_holder4_name", row.Cells("holder4_name").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_holder1_fhname", row.Cells("holder1_fhname").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_investor_category", row.Cells("investor_category").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_creditqty_lockin_reasoncode", row.Cells("creditqty_lockin_reasoncode").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_debitqty_lockin_reasoncode", row.Cells("debitqty_lockin_reasoncode").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_bo_address1", row.Cells("bo_correspondence_addr1").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_bo_address2", row.Cells("bo_correspondence_addr2").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_bo_address3", row.Cells("bo_correspondence_addr3").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_bo_address_city", row.Cells("bo_correspondence_city").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_bo_address_state", row.Cells("bo_correspondence_state").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_bo_address_country", row.Cells("bo_correspondence_country").Value.ToString())
                        cmd.Parameters.AddWithValue("?in_bo_address_pincode", row.Cells("bo_correspondence_pincode").Value.ToString())
                        If String.IsNullOrEmpty(lscreditlockinDate) Then
                            cmd.Parameters.AddWithValue("?in_creditqty_lockin_releasedate", DBNull.Value)
                        Else
                            cmd.Parameters.AddWithValue("?in_creditqty_lockin_releasedate", Format(CDate(row.Cells("creditqty_lockin_releasedate").Value.ToString()), "yyyy-MM-dd"))
                        End If
                        If String.IsNullOrEmpty(lsdebitlockinDate) Then
                            cmd.Parameters.AddWithValue("?in_debitqty_lockin_releasedate", DBNull.Value)
                        Else
                            cmd.Parameters.AddWithValue("?in_debitqty_lockin_releasedate", Format(CDate(row.Cells("debitqty_lockin_releasedate").Value.ToString()), "yyyy-MM-dd"))
                        End If
                        cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)
                        cmd.CommandTimeout = 0

                        cmd.ExecuteNonQuery()
                    End Using
                    m = m + 1
                End If
            Next


            'Insert/update on caiepfheader
            Using cmd As New MySqlCommand("pr_sta_ins_caiepfheader", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_caiepfhdr_gid", mnCAiepfhdrId)
                cmd.Parameters.AddWithValue("?in_comp_gid", mnCompId)
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_rta_internal_ref_no", lsRtaRefno)
                cmd.Parameters.AddWithValue("?in_catype", lsCAtype)
                cmd.Parameters.AddWithValue("?in_board_approval_date", lsApprovaldate)
                cmd.Parameters.AddWithValue("?in_execution_date", lsExecutiondate)
                cmd.Parameters.AddWithValue("?in_tot_credit_qty", lsCreditqty)
                cmd.Parameters.AddWithValue("?in_tot_credit_lockin_qty", lsCreditLockin)
                cmd.Parameters.AddWithValue("?in_tot_debit_qty", lsDebitqty)
                cmd.Parameters.AddWithValue("?in_tot_debit_lockin_qty", lsDebitLockin)
                cmd.Parameters.AddWithValue("?in_div_finyear", lsDivfinyear)
                cmd.Parameters.AddWithValue("?in_tot_nominal_amtof_shares", lsShares)
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
        Dim excelWorkbook As Excel.Workbook = Nothing
        Dim excelWorksheet As Excel.Worksheet = Nothing
        Dim excelRange As Excel.Range = Nothing
        Dim lssumofShares As Integer = 0
        Dim lssumofDebitqty As Integer = 0
        Dim lssumofpurcaseCost As Decimal = 0

        Try
            ' Initialize Excel application
            excelApp = New Excel.Application()
            excelWorkbook = excelApp.Workbooks.Open(filePath)

            ' Assuming you want to load data from the first sheet
            excelWorksheet = CType(excelWorkbook.Sheets(1), Excel.Worksheet)
            excelRange = excelWorksheet.UsedRange

            ' Clear existing data in DataGridView
            dgvRecord2.Rows.Clear()

            ' Load data from Excel to DataGridView
            For rowIndex As Integer = 2 To excelRange.Rows.Count
                Dim dpId As String = If(excelRange.Cells(rowIndex, 1).Value, String.Empty).ToString()
                Dim clientId As String = If(excelRange.Cells(rowIndex, 2).Value, String.Empty).ToString()
                Dim folioNo As String = If(excelRange.Cells(rowIndex, 3).Value, String.Empty).ToString()
                Dim distFrom As Integer = If(excelRange.Cells(rowIndex, 4).Value, String.Empty).ToString()
                Dim distTo As Integer = If(excelRange.Cells(rowIndex, 5).Value, String.Empty).ToString()
                Dim holder1Name As String = If(excelRange.Cells(rowIndex, 6).Value, String.Empty).ToString()
                Dim holder2Name As String = If(excelRange.Cells(rowIndex, 7).Value, String.Empty).ToString()
                Dim holder3Name As String = If(excelRange.Cells(rowIndex, 8).Value, String.Empty).ToString()
                Dim holder4Name As String = If(excelRange.Cells(rowIndex, 9).Value, String.Empty).ToString()
                Dim holder1_fhName As String = If(excelRange.Cells(rowIndex, 10).Value, String.Empty).ToString()
                Dim creditQty As Integer = If(excelRange.Cells(rowIndex, 11).Value, 0).ToString()
                Dim debitQty As Integer = If(excelRange.Cells(rowIndex, 12).Value, 0).ToString()
                lssumofShares += creditQty
                lssumofDebitqty += debitQty

                Dim creditlockinReasonCode As String = If(excelRange.Cells(rowIndex, 13).Value, String.Empty).ToString()
                Dim creditlockinReleaseDate As Nullable(Of Date)
                Dim cellValue As Object = excelRange.Cells(rowIndex, 14).Value
                If Not IsDBNull(cellValue) AndAlso cellValue IsNot Nothing Then
                    creditlockinReleaseDate = Convert.ToDateTime(cellValue)
                Else
                    creditlockinReleaseDate = Nothing
                End If
                Dim formattedDate As String = If(creditlockinReleaseDate.HasValue, creditlockinReleaseDate.Value.ToString("yyyy-MM-dd"), String.Empty)

                Dim debitlockinReasonCode As String = If(excelRange.Cells(rowIndex, 15).Value, String.Empty).ToString()
                Dim debitlockinReleaseDate As Nullable(Of Date)
                Dim cellValue1 As Object = excelRange.Cells(rowIndex, 16).Value
                If Not IsDBNull(cellValue) AndAlso cellValue1 IsNot Nothing Then
                    debitlockinReleaseDate = Convert.ToDateTime(cellValue1)
                Else
                    debitlockinReleaseDate = Nothing
                End If
                Dim formattedDate1 As String = If(debitlockinReleaseDate.HasValue, debitlockinReleaseDate.Value.ToString("yyyy-MM-dd"), String.Empty)

                Dim investorCategory As String = If(excelRange.Cells(rowIndex, 17).Value, String.Empty).ToString()
                Dim boAddress1 As String = If(excelRange.Cells(rowIndex, 18).Value, String.Empty).ToString()
                Dim boAddress2 As String = If(excelRange.Cells(rowIndex, 19).Value, String.Empty).ToString()
                Dim boAddress3 As String = If(excelRange.Cells(rowIndex, 20).Value, String.Empty).ToString()
                Dim boCity As String = If(excelRange.Cells(rowIndex, 21).Value, String.Empty).ToString()
                Dim boState As String = If(excelRange.Cells(rowIndex, 22).Value, String.Empty).ToString()
                Dim boCountry As String = If(excelRange.Cells(rowIndex, 23).Value, String.Empty).ToString()
                Dim boPincode As String = If(excelRange.Cells(rowIndex, 24).Value, String.Empty).ToString()

                dgvRecord2.Rows.Add(dpId, clientId, folioNo, distFrom, distTo, creditQty, debitQty, holder1Name, holder2Name, holder3Name, holder4Name, holder1_fhName,
                                    creditlockinReasonCode, creditlockinReleaseDate, debitlockinReasonCode, debitlockinReleaseDate,
                                    investorCategory, boAddress1, boAddress2, boAddress3, boCity, boState, boCountry, boPincode,
                                    "Remove")
            Next

            Dim ranges As New List(Of Tuple(Of Integer, Integer))()

            For Each row As DataGridViewRow In dgvRecord2.Rows
                'Dist From and Dist To highlighting part
                If row.Cells("dist_from").Value IsNot Nothing AndAlso row.Cells("dist_to").Value IsNot Nothing Then
                    Dim fromRange As Integer = Convert.ToInt32(row.Cells("dist_from").Value)
                    Dim toRange As Integer = Convert.ToInt32(row.Cells("dist_to").Value)
                    Dim creditQty As Integer = Convert.ToInt32(row.Cells("credit_qty").Value)
                    Dim shareCount As Integer = (toRange - fromRange) + 1

                    If shareCount <> creditQty Then
                        row.DefaultCellStyle.BackColor = Color.Red
                    End If

                    Dim range As New Tuple(Of Integer, Integer)(fromRange, toRange)

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

                'Credit and Debit Qty mismatch Highlighting part
                If row.Cells("credit_qty").Value IsNot Nothing AndAlso row.Cells("debit_qty").Value IsNot Nothing Then
                    Dim creditQty As Integer = Convert.ToInt32(row.Cells("credit_qty").Value)
                    Dim debitQty As Integer = Convert.ToInt32(row.Cells("debit_qty").Value)
                    If creditQty <> debitQty Then
                        ' Highlight the row yellow
                        row.DefaultCellStyle.BackColor = Color.Yellow
                    End If
                End If

            Next


            DisplayTotrecords.Text = dgvRecord2.RowCount.ToString()
            displayTotshare.Text = lssumofShares.ToString()
            txtCreditQty.Text = lssumofShares.ToString()
            txtDebitQty.Text = lssumofDebitqty.ToString()

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

    Private Sub btnGridclear_Click(sender As Object, e As EventArgs) Handles btnCleardgvrec2.Click
        dgvRecord2.DataSource = Nothing
        dgvRecord2.Rows.Clear()
        DisplayTotrecords.Text = 0
        displayTotshare.Text = 0

        lblDocStatus.Text = "Invalid"
        lblDocStatus.ForeColor = Color.Red

        btnSubmit.Enabled = False
        btnReject.Enabled = False
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

    Private Sub dgvCert_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRecord2.CellContentClick
        Dim lsTotShares As Integer = 0
        Dim lsTotCreditQty As Integer = 0
        Dim lsTotDebitQty As Integer = 0
        If msGroupCode = "M" Then
            Select Case dgvRecord2.Columns(dgvRecord2.CurrentCell.ColumnIndex).Name
                Case "action"
                    If MsgBox("Are you sure to remove ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                        lsTotShares = Convert.ToInt16(displayTotshare.Text)
                        lsTotCreditQty = Val(txtCreditQty.Text)
                        lsTotDebitQty = Val(txtDebitQty.Text)

                        displayTotshare.Text = (lsTotShares - dgvRecord2.Rows((dgvRecord2.CurrentCell.RowIndex)).Cells("credit_qty").Value)
                        txtCreditQty.Text = (lsTotCreditQty - dgvRecord2.Rows((dgvRecord2.CurrentCell.RowIndex)).Cells("credit_qty").Value)
                        txtDebitQty.Text = (lsTotDebitQty - dgvRecord2.Rows((dgvRecord2.CurrentCell.RowIndex)).Cells("debit_qty").Value)
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

    Private Sub SharecountMissmatch()
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
            CheckRowColors()
        End If
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
End Class