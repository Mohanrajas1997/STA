Imports MySql.Data.MySqlClient

Public Class frmInwardMaster
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Call ClearControl()
        Call EnableSave(True)
        txtInwardNo.Focus()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If txtId.Text = "" Then
                If MsgBox("Select Record to edit", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                    EnableSave(False)
                End If
            Else
                EnableSave(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, "select a.inward_gid as 'Inward Id',a.inward_no as 'Inward No',a.received_date as 'Rcvd Date' " & _
            "FROM sta_trn_tinward as a ", _
            "a.inward_gid,a.inward_no,a.received_date", " 1 = 1 and a.delete_flag = 'N'")
            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_trn_tinward " _
                    & "where inward_gid = " & gnSearchId & " " _
                    & "and delete_flag = 'N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySqlConnection)
        Dim lobjDataReader As MySqlDataReader

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnResult As Long
        Dim lsTxt As String

        Try
            If txtId.Text.Trim = "" Then
                MsgBox("Select the Record", MsgBoxStyle.Information, gsProjectName)
                'Calling Find Button to select record
                Call btnFind_Click(sender, e)
            Else
                If MsgBox("Are you sure want to delete this record?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gsProjectName) = MsgBoxResult.Yes Then
                    Using cmd As New MySqlCommand("pr_sta_mst_tcity", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_city_gid", Val(txtId.Text))
                        cmd.Parameters.AddWithValue("?in_city_code", "")
                        cmd.Parameters.AddWithValue("?in_city_name", "")
                        cmd.Parameters.AddWithValue("?in_state_gid", 0)
                        cmd.Parameters.AddWithValue("?in_action", "DELETE")
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

                    Call EnableSave(False)
                    Call ClearControl()
                Else
                    btnNew.Focus()
                End If
            End If
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
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpRcvdDate.Value = Now

        Call EnableSave(False)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnInwardId As Long
        Dim lnCompId As Long
        Dim lsInwardNo As String
        Dim lsRcvdMode As String
        Dim ldRcvdDate As Date
        Dim lnCourierId As Long
        Dim lsAwbNo As String
        Dim lsTranCode As String
        Dim lsFolioNo As String
        Dim lsShareHolderName As String
        Dim lsShareHolderAddr As String = ""
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

            lsInwardNo = QuoteFilter(txtInwardNo.Text)

            If cboRcvdMode.SelectedIndex <> -1 Then
                lsRcvdMode = cboRcvdMode.SelectedValue.ToString
            Else
                lsRcvdMode = ""
            End If

            ldRcvdDate = dtpRcvdDate.Value

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

            lsFolioNo = QuoteFilter(txtFolioNo.Text)
            lsShareHolderName = QuoteFilter(txtShareHolderName.Text)
            lsShareHolderContactNo = QuoteFilter(txtContactNo.Text)
            lsShareHolderEmailId = QuoteFilter(txtMailId.Text)
            lsRemark = QuoteFilter(txtRemark.Text)

            If lnInwardId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_trn_tinward", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", lnInwardId)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_inward_no", lsInwardNo)
                cmd.Parameters.AddWithValue("?in_received_date", Format(ldRcvdDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_received_mode", lsRcvdMode)
                cmd.Parameters.AddWithValue("?in_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_awb_no", lsAwbNo)
                cmd.Parameters.AddWithValue("?in_tran_code", lsTranCode)
                cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                cmd.Parameters.AddWithValue("?in_shareholder_name", lsShareHolderName)
                cmd.Parameters.AddWithValue("?in_shareholder_addr", lsShareHolderAddr)
                cmd.Parameters.AddWithValue("?in_shareholder_contact_no", lsShareHolderContactNo)
                cmd.Parameters.AddWithValue("?in_shareholder_email_id", lsShareHolderEmailId)
                cmd.Parameters.AddWithValue("?in_inward_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_action", lsAction)
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

            Call ClearControl()

            If MsgBox("Do you want to add another record ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
                btnNew.PerformClick()
            Else
                Call EnableSave(False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtInwardNo.Focus()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call ClearControl()
        Call EnableSave(False)
    End Sub

    Private Sub txtRemark_GotFocus(sender As Object, e As EventArgs) Handles txtRemark.GotFocus
        KeyPreview = False
    End Sub

    Private Sub txtRemark_LostFocus(sender As Object, e As EventArgs) Handles txtRemark.LostFocus
        KeyPreview = True
    End Sub

    Private Sub txtRemark_TextChanged(sender As Object, e As EventArgs) Handles txtRemark.TextChanged

    End Sub
End Class