Imports System.IO
Imports MySql.Data.MySqlClient
Public Class frmRevokeIEPF
#Region "Local Declaration"
    Dim lnImportFlag As Integer
    Dim fsSql As String
    Dim lnResult As Long

    Dim fsFilePath As String = ""
    Dim fsFileName As String
    Dim fExcelDatatable As New DataTable
    Dim lobjRow As DataRow
#End Region
    Private Sub frmIEPFUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' Company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", CboCompany, gOdbcConn)

    End Sub
    Private Sub cboFileName_GotFocus(sender As Object, e As EventArgs) Handles cboFileName.GotFocus
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select iepfinward_gid,file_name as file_name from sta_trn_tiepfinward "
        lsSql &= " where iepfinward_date >= '" & Format(dtpDate.Value, "yyyy-MM-dd") & "' "
        lsSql &= " and iepfinward_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpDate.Value), "yyyy-MM-dd") & "' "

        If CboCompany.SelectedIndex = -1 Or CboCompany.Text = "" Then
            lsSql &= " and 1 = 2 "
        Else
            lsSql &= " and comp_gid = " & CboCompany.SelectedValue & " "
        End If

        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by iepfinward_gid desc "

        gpBindCombo(lsSql, "file_name", "iepfinward_gid", cboFileName, gOdbcConn)
    End Sub
    Private Sub cboFileName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFileName.TextChanged
        If cboFileName.SelectedIndex = -1 And cboFileName.Text <> "" Then gpAutoFillCombo(cboFileName)
    End Sub
    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click

        Dim lnFileId As Long
        Dim lsTxt As String
        Dim lnResult As Integer
        Dim lncompid As Long

        Try

            If CboCompany.SelectedIndex <> -1 Then
                lncompid = Val(CboCompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            If lncompid = 0 Then
                MsgBox("Select Company Name", MsgBoxStyle.Information, gsProjectName)
                CboCompany.Focus()
                Exit Sub
            End If

            If cboFileName.SelectedIndex = -1 Or cboFileName.Text = "" Then
                MessageBox.Show("Please select the file !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboFileName.Focus()
                Exit Sub
            End If

            lnFileId = Val(cboFileName.SelectedValue)

            If MessageBox.Show("Are you sure to delete ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Using cmd As New MySqlCommand("pr_sta_revoke_tiepfinward_tran", gOdbcConn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("?In_comp_gid", lncompid)
                    cmd.Parameters.AddWithValue("?In_iepfinward_gid", lnFileId)
                    cmd.Parameters.AddWithValue("?In_action_by", gsLoginUserCode)

                    'Out put Para
                    cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                    cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                    cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                    cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                    cmd.ExecuteNonQuery()

                    lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                    lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, IIf(lnResult = 1, MessageBoxIcon.Information, MessageBoxIcon.Exclamation))
                End Using
            End If

        Catch ex As Exception
       
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            MyBase.Close()
        End If
    End Sub


End Class