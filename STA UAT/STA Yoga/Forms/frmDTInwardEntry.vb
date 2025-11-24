Imports MySql.Data.MySqlClient

Public Class frmDTInwardEntry
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

            cboCompany.SelectedIndex = -1

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("inward_gid").ToString
                        txtInwardNo.Text = .Item("inward_no").ToString
                        txtShareCount.Text = .Item("share_count").ToString
                        cboCompany.SelectedValue = .Item("comp_gid").ToString
                        txtFolioNo.Text = .Item("folio_no").ToString
                        txtShareHolderName.Text = .Item("shareholder_name").ToString
                        txtRemark.Text = .Item("inward_remark").ToString
                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(cboCompany)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
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

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

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
        Dim lnCompId As Long
        Dim lsRcvdMode As String = ""
        Dim lnCourierId As Long = 0
        Dim lsAwbNo As String = ""
        Dim lsTranCode As String = "DT"
        Dim lsFolioNo As String
        Dim lsShareHolderName As String
        Dim lsShareHolderAddr As String = ""
        Dim lsShareHolderPanNo As String = ""
        Dim lsShareHolderContactNo As String = ""
        Dim lsShareHolderEmailId As String = ""
        Dim lnShareCount As Long = 0
        Dim lsRemark As String
        Dim lsAction As String
        Dim lnCompInwardNo As String

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
                        cboCompany.Focus()
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

            lnShareCount = Val(txtShareCount.Text)

            If lnShareCount <= 0 Then
                MessageBox.Show("Invalid share count !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtShareCount.Focus()
                Exit Sub
            End If

            lsFolioNo = QuoteFilter(txtFolioNo.Text)
            lsShareHolderName = QuoteFilter(txtShareHolderName.Text)
            lsRemark = QuoteFilter(txtRemark.Text)

            lnInwardId = Val(txtId.Text)

            If lnInwardId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_trn_tinward", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", lnInwardId)
                cmd.Parameters.AddWithValue("?in_entity_gid", gnEntityId)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_inward_no", lnInwardNo)
                cmd.Parameters.AddWithValue("?in_received_date", Format(Now(), "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_received_mode", "I")
                cmd.Parameters.AddWithValue("?in_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_awb_no", "-")
                cmd.Parameters.AddWithValue("?in_tran_code", "DT")
                cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                cmd.Parameters.AddWithValue("?in_shareholder_name", lsShareHolderName)
                cmd.Parameters.AddWithValue("?in_shareholder_addr", lsShareHolderAddr)
                cmd.Parameters.AddWithValue("?in_shareholder_pan_no", lsShareHolderPanNo)
                cmd.Parameters.AddWithValue("?in_shareholder_contact_no", "-")
                cmd.Parameters.AddWithValue("?in_shareholder_email_id", "-")
                cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)
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
                    Else
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    If mnInwardId > 0 Then Me.Close()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using

            Call ClearControl()

            If mbGenerateInwardNo = True Then
                cboCompany.Focus()
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
End Class