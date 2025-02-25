Imports MySql.Data.MySqlClient
Public Class frmInterDepositoryEnt
    Dim msMode As String
    Dim mnInwardId As Long
    Dim mbGenerateInwardNo As Boolean = True

    Private Sub frmInterDepository_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "
        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpRcvdDate.Value = Now

        Call ClearControl()

        'If mnInwardId > 0 Then
        '    Call ListAll(mnInwardId)
        'End If
    End Sub

    Private Sub txtNsdlTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNsdlTotal.Leave
        Dim NsdlPercentage As Double
        Dim lnNsdlTotal As Long
        Dim lnShareCaptial As Long
        Dim CdslPercentage As Double
        Dim lnCdslTotal As Long

        lnCdslTotal = Val(txtCdslTotal.Text)
        CdslPercentage = Val((lnCdslTotal / lnShareCaptial) * 100)

        lnNsdlTotal = Val(txtNsdlTotal.Text)
        lnShareCaptial = Val(txtShareCaptial.Text)
        NsdlPercentage = Val((lnNsdlTotal / lnShareCaptial) * 100)
        txtNsdlPercentage.Text = Math.Round(Val(NsdlPercentage), 2)

        txtDematPercentage.Text = Math.Round(Val(NsdlPercentage), 2) + Math.Round(Val(CdslPercentage), 2)

    End Sub
    Private Sub txtCdslTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCdslTotal.Leave
        Dim CdslPercentage As Double
        Dim lnCdslTotal As Long
        Dim lnShareCaptial As Long
        Dim NsdlPercentage As Double
        Dim lnNsdlTotal As Long

        lnCdslTotal = Val(txtCdslTotal.Text)
        lnShareCaptial = Val(txtShareCaptial.Text)
        CdslPercentage = Val((lnCdslTotal / lnShareCaptial) * 100)
        txtCdslPercentage.Text = Math.Round(Val(CdslPercentage), 2)
        lnNsdlTotal = Val(txtNsdlTotal.Text)
        NsdlPercentage = Val((lnNsdlTotal / lnShareCaptial) * 100)

        txtDematPercentage.Text = Math.Round(Val(NsdlPercentage), 2) + Math.Round(Val(CdslPercentage), 2)

    End Sub
    Private Sub txtShareCaptial_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShareCaptial.Leave
        Dim CdslPercentage As Double
        Dim lnCdslTotal As Long
        Dim lnShareCaptial As Long
        Dim NsdlPercentage As Double
        Dim lnNsdlTotal As Long
        Dim PhysicalPercentage As Double
        Dim lnPhysicalTotal As Long

        lnNsdlTotal = Val(txtNsdlTotal.Text)
        lnCdslTotal = Val(txtCdslTotal.Text)
        lnShareCaptial = Val(txtShareCaptial.Text)
        lnPhysicalTotal = Val(txtPhysicalTotal.Text)

        NsdlPercentage = Val((lnNsdlTotal / lnShareCaptial) * 100)
        txtNsdlPercentage.Text = Math.Round(Val(NsdlPercentage), 2)

        CdslPercentage = Val((lnCdslTotal / lnShareCaptial) * 100)
        txtCdslPercentage.Text = Math.Round(Val(CdslPercentage), 2)

        PhysicalPercentage = Val((lnPhysicalTotal / lnShareCaptial) * 100)
        txtPhysicalpercentage.Text = Math.Round(Val(PhysicalPercentage), 2)

        txtDematPercentage.Text = Math.Round(Val(NsdlPercentage), 2) + Math.Round(Val(CdslPercentage), 2)



    End Sub
    Private Sub txtPhysicalTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhysicalTotal.Leave
        Dim PhysicalPercentage As Double
        Dim lnPhysicalTotal As Long
        Dim lnShareCaptial As Long
        lnPhysicalTotal = Val(txtPhysicalTotal.Text)
        lnShareCaptial = Val(txtShareCaptial.Text)
        PhysicalPercentage = Val((lnPhysicalTotal / lnShareCaptial) * 100)
        txtPhysicalpercentage.Text = Math.Round(Val(PhysicalPercentage), 2)
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnInwardId As Long
        Dim lnCompId As Long
        Dim lnNsdlDebit As Long
        Dim lnNsdlCredit As Long
        Dim lnNsdlRemat As Long
        Dim lnNsdlConfirmation As Long
        Dim lnNsdlTotal As Long
        Dim lnNsdlPercentage As Long
        Dim lnCdslDebit As Long
        Dim lnCdslCredit As Long
        Dim lnCdslRemat As Long
        Dim lnCdslConfirmation As Long
        Dim lnCdslTotal As Long
        Dim lnCdslPercentage As Long
        Dim lnPhysicalDebit As Long
        Dim lnPhysicalCredit As Long
        Dim lnPhysicalTotal As Long
        Dim lnPhysicalPercentage As Long
        Dim lnShareTotal As Long
        Dim lnDematPercentage As Long

        Dim lnFilegid As Long
        Dim ldRcvdDate As Date
        Dim lsAction As String

        Try
            If cboCompany.SelectedIndex <> -1 Then
                lnCompId = Val(cboCompany.SelectedValue.ToString)
            Else
                lnCompId = 0
            End If

            ldRcvdDate = dtpRcvdDate.Value

            lnNsdlDebit = Val(QuoteFilter(txtNsdlDebit.Text))
            lnNsdlCredit = Val(QuoteFilter(txtNsdlCredit.Text))
            lnNsdlRemat = Val(QuoteFilter(txtNsdlRemat.Text))
            lnNsdlConfirmation = Val(QuoteFilter(txtNsdlPercentage.Text))
            lnNsdlTotal = Val(QuoteFilter(txtNsdlTotal.Text))
            lnNsdlPercentage = Val(QuoteFilter(txtNsdlPercentage.Text))

            lnCdslDebit = Val(QuoteFilter(txtCdslDebit.Text))
            lnCdslCredit = Val(QuoteFilter(txtCdslCredit.Text))
            lnCdslRemat = Val(QuoteFilter(txtCdslRemat.Text))
            lnCdslConfirmation = Val(QuoteFilter(txtCdsConfirmation.Text))
            lnCdslTotal = Val(QuoteFilter(txtCdslTotal.Text))
            lnCdslPercentage = Val(QuoteFilter(txtCdslPercentage.Text))

            lnPhysicalDebit = Val(QuoteFilter(txtPhysicalDebit.Text))
            lnPhysicalCredit = Val(QuoteFilter(txtPhysicalCredit.Text))
            lnPhysicalTotal = Val(QuoteFilter(txtPhysicalTotal.Text))
            lnPhysicalPercentage = Val(QuoteFilter(txtPhysicalpercentage.Text))

            lnShareTotal = Val(QuoteFilter(txtShareCaptial.Text))
            lnDematPercentage = Val(QuoteFilter(txtDematPercentage.Text))


            lnInwardId = Val(txtId.Text)

            lnFilegid = 0
            If lnInwardId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_ins_tinterdepository_entry", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_gid", lnFilegid)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_depo_date", Format(ldRcvdDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_debit_nsdl", lnNsdlDebit)
                cmd.Parameters.AddWithValue("?in_credit_nsdl", lnNsdlCredit)
                cmd.Parameters.AddWithValue("?in_remat_nsdl", lnNsdlRemat)
                cmd.Parameters.AddWithValue("?in_confirmation_nsdl", lnNsdlConfirmation)
                cmd.Parameters.AddWithValue("?in_nsdl_total", lnNsdlTotal)
                cmd.Parameters.AddWithValue("?in_nsdl_per", lnNsdlPercentage)
                cmd.Parameters.AddWithValue("?in_debit_cdsl", lnCdslDebit)
                cmd.Parameters.AddWithValue("?in_credit_cdsl", lnCdslCredit)
                cmd.Parameters.AddWithValue("?in_remat_cdsl", lnCdslRemat)
                cmd.Parameters.AddWithValue("?in_confirmation_cdsl", lnCdslConfirmation)
                cmd.Parameters.AddWithValue("?in_cdsl_total", lnCdslTotal)
                cmd.Parameters.AddWithValue("?in_cdsl_per", lnCdslPercentage)
                cmd.Parameters.AddWithValue("?in_debit_phy", lnPhysicalDebit)
                cmd.Parameters.AddWithValue("?in_credit_phy", lnPhysicalCredit)
                cmd.Parameters.AddWithValue("?in_physical_total", lnPhysicalTotal)
                cmd.Parameters.AddWithValue("?in_physical_per", lnPhysicalPercentage)

                cmd.Parameters.AddWithValue("?in_share_captial", lnShareTotal)
                cmd.Parameters.AddWithValue("?in_demat_per", lnDematPercentage)
                cmd.Parameters.AddWithValue("?in_line_no", 1)
                cmd.Parameters.AddWithValue("?in_errline_flag", True)

                'Out put Para     
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()


                If lnResult = 1 Then
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'If lnInwardId = 0 Then
                    '    MessageBox.Show("New Inward No : " & lnInwardNo, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Else
                    '    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If

                    'If mnInwardId > 0 Then Me.Close()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using

            Call ClearControl()
            cboCompany.SelectedItem = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
    Private Sub ClearControl()
        Call frmCtrClear(Me)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

End Class