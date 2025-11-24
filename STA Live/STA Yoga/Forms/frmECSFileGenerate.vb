Imports MySql.Data.MySqlClient
Public Class frmECSFileGenerate
    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' Company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", CboCompany, gOdbcConn)

        ' Financial Year
        lsSql = ""
        lsSql &= " select finyear_code ,finyear_gid from sta_mst_tfinyear "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by finyear_code asc "

        Call gpBindCombo(lsSql, "finyear_code", "finyear_gid", CboFinyear, gOdbcConn)

        ' interim code
        lsSql = ""
        lsSql &= " select interim_name,interim_code from sta_mst_tinterim "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by interim_code asc "

        Call gpBindCombo(lsSql, "interim_name", "interim_code", CboInterim, gOdbcConn)


    End Sub

    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click
        Dim lnResult As Integer
        Dim lsTxt As String

        Dim lncompid As Long
        Dim lnFinyearid As Long
        Dim lnInterim As String
        Try
            If CboCompany.SelectedIndex <> -1 Then
                lncompid = Val(CboCompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            If CboFinyear.SelectedIndex <> -1 Then
                lnFinyearid = Val(CboFinyear.SelectedValue.ToString)
            Else
                lnFinyearid = 0
            End If

            If CboInterim.SelectedIndex <> -1 Then
                lnInterim = CboInterim.SelectedValue.ToString
            Else
                lnInterim = ""
            End If


            Using cmd As New MySqlCommand("pr_sta_ins_tecsgeneration", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                cmd.Parameters.AddWithValue("?in_finyear_gid", lnFinyearid)
                cmd.Parameters.AddWithValue("?in_interim_code", lnInterim)
                cmd.Parameters.AddWithValue("?in_insert_by", gsLoginUserCode)

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
                End If

            End Using

            Call frmCtrClear(Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class