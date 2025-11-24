Public Class frmDividendReset
    Private Sub frmDividendReset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String


        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        ' Financial Year

        lsSql = ""
        lsSql &= " select finyear_gid,finyear_code from sta_mst_tfinyear "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by finyear_code asc "

        Call gpBindCombo(lsSql, "finyear_code", "finyear_gid", CboFinyear, gOdbcConn)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ResetDividend()
        Dim lsSql As String
        Dim lnResult As Integer

        If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If (CboFinyear.Text = "" Or CboFinyear.SelectedIndex = -1) Then
            MessageBox.Show("Please select the finyear !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CboFinyear.Focus()
            Exit Sub
        End If

        'Conformation msg 
        If MsgBox("Are you sure you want to Reset?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gsProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        lsSql = ""

        'Dividend Account Master Reset 

        lsSql &= " UPDATE div_mst_tacc"
        lsSql &= " SET delete_flag = 'R'"
        lsSql &= " WHERE"
        lsSql &= " comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & " "
        lsSql &= " and delete_flag = 'N' ;"

        'Dividend Reset

        lsSql &= " UPDATE div_trn_tdividend"
        lsSql &= " SET delete_flag = 'R'"
        lsSql &= " WHERE "
        lsSql &= " comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & " "
        lsSql &= " and delete_flag = 'N' ;"

        'Dividend Log Reset

        lsSql &= " UPDATE div_trn_tdividendlog as a"
        lsSql &= " INNER JOIN div_trn_tdividend as b"
        lsSql &= " ON a.div_gid = b.div_gid"
        lsSql &= " and b.delete_flag = 'R'"
        lsSql &= " SET a.delete_flag = 'R'"
        lsSql &= " WHERE"
        lsSql &= " b.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        lsSql &= " and b.finyear_gid = " & Val(CboFinyear.SelectedValue.ToString) & " "
        lsSql &= " and a.delete_flag = 'N' ;"

        lnResult = gfInsertQry(lsSql, gOdbcConn)

        If lnResult = 0 Then
            MsgBox("No Records !", MsgBoxStyle.Information, gsProjectName)
            Exit Sub

        End If

        MsgBox("Reset successfully !", MsgBoxStyle.Information, gsProjectName)

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Call ResetDividend()
    End Sub
End Class