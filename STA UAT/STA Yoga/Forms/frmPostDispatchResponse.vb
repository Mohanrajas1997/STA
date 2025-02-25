Imports MySql.Data.MySqlClient
Public Class frmPostDispatchResponse
    Private Sub frmQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' Company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", CboCompany, gOdbcConn)

        dtpFrom.Value = Now
        dtpTo.Value = Now


    End Sub

    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click
        Dim lnResult As Integer
        Dim lsTxt As String

        Dim lncompid As Long
        Dim lnfromdate As String
        Dim lnTodate As String


        Try
            If (CboCompany.Text = "" Or CboCompany.SelectedIndex = -1) Then
                MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                CboCompany.Focus()
                Exit Sub
            End If

            If CboCompany.SelectedIndex <> -1 Then
                lncompid = Val(CboCompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            lnfromdate = Format(dtpFrom.Value, "yyyy-MM-dd")
            lnTodate = Format(dtpTo.Value, "yyyy-MM-dd")


            Using cmd As New MySqlCommand("pr_set_letter_dispatch_response", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                cmd.Parameters.AddWithValue("?in_response_from_date", lnfromdate)
                cmd.Parameters.AddWithValue("?in_response_to_date", lnTodate)

                'Out put Para

                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)

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