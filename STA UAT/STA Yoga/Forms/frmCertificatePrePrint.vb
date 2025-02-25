Imports MySql.Data.MySqlClient

Public Class frmCertificatePrePrint
    Private Sub frmCertificatePrePrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "
        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)


        Call ClearControl()
        Call EnableSave(False)
        btnNew.Focus()
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        cboCompany.Focus()
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlnew.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        EnableSave(True)
        Call ClearControl()
        cboCompany.Focus()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        EnableSave(False)
        ClearControl()
        btnNew.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If (txtppfrom.Text = "") Then
            MessageBox.Show("Please enter the pre print from !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtppfrom.Focus()
            Exit Sub
        End If

        If (txtppto.Text = "") Then
            MessageBox.Show("Please enter the pre print from !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtppfrom.Focus()
            Exit Sub
        End If

        'If (txtppfrom.Text > txtppto.Text) Then
        '    MessageBox.Show("pre print to should be greater than pre print from !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    txtppfrom.Focus()
        '    Exit Sub
        'End If

        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnPPheadGid As Long
        Dim lnCompId As Long
        Dim lsPPfrom As Integer
        Dim lsPPto As Integer
        Dim lsRemarks As String
        Dim lsAction As String

        Try
            lnPPheadGid = Val(txtId.Text)

            lnCompId = QuoteFilter(cboCompany.SelectedValue)
            lsPPfrom = QuoteFilter(txtppfrom.Text)
            lsPPto = QuoteFilter(txtppto.Text)
            lsRemarks = QuoteFilter(Txtremark.Text)

            If lnPPheadGid = 0 Then
                lsAction = "INSERT"
                cboCompany.Enabled = True

            Else
                lsAction = "UPDATE"
                cboCompany.Enabled = False
            End If

            Using cmd As New MySqlCommand("pr_sta_ins_certificatepphead", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_certpphead_gid", lnPPheadGid)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_pp_from", lsPPfrom)
                cmd.Parameters.AddWithValue("?in_pp_to", lsPPto)
                cmd.Parameters.AddWithValue("?in_pphead_remark", lsRemarks)
                cmd.Parameters.AddWithValue("?in_entry_by", gsLoginUserCode)
                cmd.Parameters.AddWithValue("?in_action", lsAction)

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
            EnableSave(False)
            btnNew.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try

    End Sub

    Private Sub txtppfrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtppfrom.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtppto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtppto.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn,
                " select certpphead_gid as 'Certificate PPhead Gid'," &
                " comp_gid as 'Company Name',pp_from as 'Pre print From',pp_to as 'Pre print To'," &
                "pphead_remark as 'Remarks' from sta_trn_tcertpphead ",
                " certpphead_gid,comp_gid,pp_from,pp_to,pphead_remark ", " delete_flag='N' ")

            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_trn_tcertpphead " _
                    & " where certpphead_gid = " & gnSearchId & " " _
                    & " and delete_flag = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySql.Data.MySqlClient.MySqlConnection)
        Dim lobjDataReader As MySqlDataReader

        Dim ppfrom As Integer
        Dim ppto As Integer
        Dim lnCompgid As String
        Dim remarks As String

        Try

            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            cboCompany.SelectedIndex = -1

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("certpphead_gid").ToString
                        cboCompany.SelectedValue = .Item("comp_gid").ToString
                        lnCompgid = .Item("comp_gid").ToString
                        ppfrom = .Item("pp_from").ToString
                        ppto = .Item("pp_to").ToString
                        remarks = .Item("pphead_remark").ToString
                        txtppfrom.Text = .Item("pp_from").ToString
                        txtppto.Text = .Item("pp_to").ToString
                        Txtremark.Text = .Item("pphead_remark").ToString
                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(cboCompany)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
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
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim lnResult As Integer
            Dim lsTxt As String
            Dim lnPPheadGid As Long
            Dim lnCompId As Long
            Dim ppfrom As Integer
            Dim ppto As Integer

            lnPPheadGid = Val(txtId.Text)
            ppfrom = Val(txtppfrom.Text)
            ppto = Val(txtppto.Text)

            If txtId.Text = "" Then
                If MsgBox("Select Record to edit", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                    EnableSave(False)
                End If
            Else
                If MsgBox("Are You sure to delete ?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then

                    lnCompId = QuoteFilter(cboCompany.SelectedValue)

                    Using cmd As New MySqlCommand("pr_sta_ins_certificatepphead", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_certpphead_gid", lnPPheadGid)
                        cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                        cmd.Parameters.AddWithValue("?in_pp_from", ppfrom)
                        cmd.Parameters.AddWithValue("?in_pp_to", ppto)
                        cmd.Parameters.AddWithValue("?in_pphead_remark", "")
                        cmd.Parameters.AddWithValue("?in_entry_by", gsLoginUserCode)
                        cmd.Parameters.AddWithValue("?in_action", "DELETE")

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
                    EnableSave(False)
                    btnNew.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class