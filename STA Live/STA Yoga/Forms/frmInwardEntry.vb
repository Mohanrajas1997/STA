Imports MySql.Data.MySqlClient

Public Class frmInwardEntry
    Dim msMode As String
    Dim mnInwardId As Long
    Dim mbGenerateInwardNo As Boolean = True

    Public Sub New(Mode As String, Optional InwardId As Long = 0, Optional GenerateInwardNoFlag As Boolean = True)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        msMode = Mode
        mnInwardId = InwardId
        mbGenerateInwardNo = GenerateInwardNoFlag

        If mbGenerateInwardNo = True Then
            txtInwardNo.Enabled = False
        Else
            txtInwardNo.Enabled = True
        End If

        Select Case msMode.ToUpper()
            Case "ADD"
                btnSave.Visible = True
                btnClear.Visible = True
            Case "UPDATE"
                btnSave.Visible = True
                btnClear.Visible = True
            Case "VIEW"
                btnSave.Visible = False
                btnClear.Visible = False
        End Select
    End Sub

    Private Sub ListAll(ByVal InwardId As Long)
        Dim lsSql As String
        Dim lobjDataReader As MySqlDataReader

        Try
            lsSql = ""
            lsSql &= " select * from sta_trn_tinward "
            lsSql &= " where inward_gid = " & mnInwardId & " "
            lsSql &= " and delete_flag = 'N' "

            lobjDataReader = gfExecuteQry(lsSql, gOdbcConn)

            cboCourier.SelectedIndex = -1
            cboDocType.SelectedIndex = -1
            cboCompany.SelectedIndex = -1
            cboRcvdMode.SelectedIndex = -1

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("inward_gid").ToString
                        txtInwardNo.Text = .Item("inward_no").ToString
                        cboDocType.SelectedValue = .Item("tran_code").ToString
                        cboRcvdMode.SelectedValue = .Item("received_mode").ToString
                        dtpRcvdDate.Value = .Item("received_date")
                        cboCourier.SelectedValue = .Item("courier_gid").ToString
                        txtAwbNo.Text = .Item("awb_no").ToString
                        cboCompany.SelectedValue = .Item("comp_gid").ToString
                        txtFolioNo.Text = .Item("folio_no").ToString
                        txtShareHolderName.Text = .Item("shareholder_name").ToString
                        txtPanNo.Text = .Item("shareholder_pan_no").ToString
                        txtContactNo.Text = .Item("shareholder_contact_no").ToString
                        txtMailId.Text = .Item("shareholder_email_id").ToString
                        txtRemark.Text = .Item("inward_remark").ToString
                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(cboDocType)
            Call gpAutoFillCombo(cboRcvdMode)
            Call gpAutoFillCombo(cboCompany)
            Call gpAutoFillCombo(cboCourier)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmInwardEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmBankMater_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        'e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        cboDocSubType.Enabled = False
        lnkAddAttachment.Enabled = False

        ' received mode
        lsSql = ""
        lsSql &= " select receivedmode_code,receivedmode_desc from sta_mst_treceivedmode "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by receivedmode_desc asc "

        Call gpBindCombo(lsSql, "receivedmode_desc", "receivedmode_code", cboRcvdMode, gOdbcConn)

        ' doc type
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "

        Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

        ' courier
        lsSql = ""
        lsSql &= " select courier_gid,courier_name from sta_mst_tcourier "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by courier_name asc "

        Call gpBindCombo(lsSql, "courier_name", "courier_gid", cboCourier, gOdbcConn)

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpRcvdDate.Value = Now

        Call ClearControl()

        If mnInwardId > 0 Then
            Call ListAll(mnInwardId)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnInwardId As Long
        Dim lnInwardNo As Long
        Dim lnCompInwardNo As String
        Dim lnCompId As Long
        Dim lsRcvdMode As String
        Dim ldRcvdDate As Date
        Dim lnCourierId As Long
        Dim lsAwbNo As String
        Dim lsTranCode As String
        Dim lsDocSubTypeCode As String
        Dim lsFolioNo As String
        Dim lsShareHolderName As String
        Dim lsShareHolderAddr As String = ""
        Dim lsShareHolderPanNo As String = ""
        Dim lsShareHolderContactNo As String
        Dim lsShareHolderEmailId As String
        Dim lsRemark As String
        Dim lsAction As String

        Try
            If cboCompany.SelectedIndex <> -1 Then
                lnCompId = Val(cboCompany.SelectedValue.ToString)
            Else
                lnCompId = 0
            End If

            lnInwardNo = Val(QuoteFilter(txtInwardNo.Text))

            If mnInwardId = 0 Then
                If mbGenerateInwardNo = True Then
                    If lnInwardNo > 0 Then
                        MessageBox.Show("Inward no is should be generated automatically !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cboDocType.Focus()
                        Exit Sub
                    End If
                Else
                    If lnInwardNo = 0 Then
                        MessageBox.Show("Inward no cannot be zero !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtInwardNo.Focus()
                        Exit Sub
                    End If
                End If
            End If

            If cboRcvdMode.SelectedIndex <> -1 Then
                lsRcvdMode = cboRcvdMode.SelectedValue.ToString
            Else
                lsRcvdMode = ""
            End If

            ldRcvdDate = dtpRcvdDate.Value

            If cboDocType.Text = "OT-Others" Or cboDocType.Text = "TM-Transmission" Then
                If cboDocSubType.Text = "" Then
                    MessageBox.Show("Document SubType cannot be blank !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cboDocSubType.Focus()
                    Exit Sub
                End If
            End If

            If cboCourier.SelectedIndex <> -1 Then
                lnCourierId = Val(cboCourier.SelectedValue.ToString)
            Else
                lnCourierId = 0
            End If

            lsAwbNo = QuoteFilter(txtAwbNo.Text)

            If cboDocType.SelectedIndex <> -1 Then
                lsTranCode = cboDocType.SelectedValue.ToString
            Else
                lsTranCode = ""
            End If

            If cboDocSubType.SelectedIndex <> -1 Then
                lsDocSubTypeCode = cboDocSubType.SelectedValue.ToString
            Else
                lsDocSubTypeCode = ""
            End If

            lsFolioNo = QuoteFilter(txtFolioNo.Text)
            lsShareHolderName = QuoteFilter(txtShareHolderName.Text)
            lsShareHolderPanNo = QuoteFilter(txtPanNo.Text)
            lsShareHolderContactNo = QuoteFilter(txtContactNo.Text)
            lsShareHolderEmailId = QuoteFilter(txtMailId.Text)
            lsRemark = QuoteFilter(txtRemark.Text)

            lnInwardId = Val(txtId.Text)

            'Mohan Changes on 28-05-2024
            'And cboDocType.Text <> "TM-Transmission" And cboDocType.Text <> "IS-ISR" And cboDocType.Text <> "CP-PAN Update" 
            If cboDocType.Text <> "OT-Others" Then
                Using cmd As New MySqlCommand("pr_sta_validate_debarrtpan", gOdbcConn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("?in_folio_gid", 0)
                    cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                    cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                    cmd.Parameters.AddWithValue("?in_tran_type", cboDocType.Text)

                    'Out put Para
                    cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                    cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                    cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                    cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                    cmd.CommandTimeout = 0

                    cmd.ExecuteNonQuery()

                    lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                    lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                    If lnResult = 0 Then
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtFolioNo.Focus()
                        Exit Sub
                    End If
                End Using
            End If

            If lnInwardId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_trn_tinwardnew", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", lnInwardId)
                cmd.Parameters.AddWithValue("?in_entity_gid", gnEntityId)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_inward_no", lnInwardNo)
                cmd.Parameters.AddWithValue("?in_received_date", Format(ldRcvdDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_received_mode", lsRcvdMode)
                cmd.Parameters.AddWithValue("?in_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_awb_no", lsAwbNo)
                cmd.Parameters.AddWithValue("?in_tran_code", lsTranCode)
                cmd.Parameters.AddWithValue("?in_docsubtype_code", lsDocSubTypeCode)
                cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                cmd.Parameters.AddWithValue("?in_shareholder_name", lsShareHolderName)
                cmd.Parameters.AddWithValue("?in_shareholder_addr", lsShareHolderAddr)
                cmd.Parameters.AddWithValue("?in_shareholder_pan_no", lsShareHolderPanNo)
                cmd.Parameters.AddWithValue("?in_shareholder_contact_no", lsShareHolderContactNo)
                cmd.Parameters.AddWithValue("?in_shareholder_email_id", lsShareHolderEmailId)
                cmd.Parameters.AddWithValue("?in_share_count", 0)
                cmd.Parameters.AddWithValue("?in_inward_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_action", lsAction)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_comp_inward_no", MySqlDbType.VarChar)
                cmd.Parameters("?out_comp_inward_no").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_inward_no", MySqlDbType.Int32)
                cmd.Parameters("?out_inward_no").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()
                lnInwardNo = Val(cmd.Parameters("?out_inward_no").Value.ToString())
                lnCompInwardNo = cmd.Parameters("?out_comp_inward_no").Value.ToString()

                If lnResult = 1 Then
                    If lnInwardId = 0 Then
                        MessageBox.Show("Inward No :" & lnInwardNo & ", New Inward No : " & lnCompInwardNo, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mnInwardId = lnInwardNo
                        If cboDocType.Text = "OT-Others" Or cboDocType.Text = "TM-Transmission" Then
                            lnkAddAttachment.Enabled = True
                        Else
                            lnkAddAttachment.Enabled = False
                        End If
                    Else
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        lnkAddAttachment.Enabled = False
                    End If

                    'If mnInwardId > 0 Then Me.Close()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using

            If cboDocType.Text <> "OT-Others" Or cboDocType.Text <> "TM-Transmission" Then
                Call ClearControl()
            End If

            If mbGenerateInwardNo = True Then
                cboDocType.Focus()
            Else
                txtInwardNo.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtInwardNo.Focus()
    End Sub

    Private Sub txtRemark_GotFocus(sender As Object, e As EventArgs) Handles txtRemark.GotFocus
        KeyPreview = False
    End Sub

    Private Sub txtRemark_LostFocus(sender As Object, e As EventArgs) Handles txtRemark.LostFocus
        KeyPreview = True
    End Sub

    Private Sub txtRemark_TextChanged(sender As Object, e As EventArgs) Handles txtRemark.TextChanged

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        mnInwardId = 0
        cboDocSubType.Enabled = False
        lnkAddAttachment.Enabled = False
    End Sub

    Private Sub txtFolioNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFolioNo.Validating
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lnCompId As Long = 0
        Dim lsFolioNo As String

        If btnSave.Visible = True And txtShareHolderName.Text = "" Then
            If cboCompany.SelectedIndex <> -1 And cboCompany.Text <> "" Then lnCompId = Val(cboCompany.SelectedValue.ToString)
            lsFolioNo = QuoteFilter(txtFolioNo.Text)

            If lnCompId = 0 Then lsFolioNo = ""

            cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_folio_gid", 0)
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)

            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                txtShareHolderName.Text = dt.Rows(0).Item("holder1_name").ToString
                txtPanNo.Text = dt.Rows(0).Item("holder1_pan_no").ToString
            End If
        End If
    End Sub

    Private Sub LoadFolio(FolioId As Long)
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_folio_gid", FolioId)
        cmd.Parameters.AddWithValue("?in_comp_gid", 0)
        cmd.Parameters.AddWithValue("?in_folio_no", "")

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolderName.Text = .Rows(0).Item("holder1_name").ToString
                txtPanNo.Text = .Rows(0).Item("holder1_pan_no").ToString
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()
    End Sub

    Private Sub btnSearchFolio_Click(sender As Object, e As EventArgs) Handles btnSearchFolio.Click
        Dim frm As frmFolioSearch
        Dim lnCompId As Long = 0

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        End If

        frm = New frmFolioSearch(lnCompId)
        frm.ShowDialog()

        If gnSearchId <> 0 Then
            Call LoadFolio(gnSearchId)
        End If
    End Sub

    Private Sub txtFolioNo_TextChanged(sender As Object, e As EventArgs) Handles txtFolioNo.TextChanged

    End Sub

    Private Sub cboDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDocType.SelectedIndexChanged
        Dim lsSql As String

        If cboDocType.Text = "OT-Others" Or cboDocType.Text = "TM-Transmission" Then
            cboDocSubType.Enabled = True

            ' doc sub type
            lsSql = ""
            lsSql &= " select docsubtype_code,docsubtype_desc from sta_mst_tdocsubtype "
            lsSql &= " where delete_flag = 'N' and trantype_code = '" & cboDocType.SelectedValue.ToString & "' "
            lsSql &= " order by docsubtype_code asc "

            Call gpBindCombo(lsSql, "docsubtype_desc", "docsubtype_code", cboDocSubType, gOdbcConn)

            cboDocSubType.Focus()
        Else
            cboDocSubType.Text = ""
            cboDocSubType.Enabled = False
        End If

    End Sub

    Private Sub lnkAddAttachment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAttachment.LinkClicked
        Dim frm As New frmAttachmentAdd(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs)
        Dim frm As New frmDocHistory(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class