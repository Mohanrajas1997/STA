Imports MySql.Data.MySqlClient

Public Class frmDeleteFile
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmDeleteFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select file_type,file_desc from sta_mst_tfile "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by file_desc "

        Call gpBindCombo(lsSql, "file_desc", "file_type", cboFileType, gOdbcConn)

        dtpImportDate.Value = DateSerial(2000, 1, 1)
        dtpImportDate.Value = Now
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub cboFileName_GotFocus(sender As Object, e As EventArgs) Handles cboFileName.GotFocus
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select file_gid,concat(file_name,' ',ifnull(sheet_name,'')) as file_name from sta_trn_tfile "
        lsSql &= " where insert_date >= '" & Format(dtpImportDate.Value, "yyyy-MM-dd") & "' "
        lsSql &= " and insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpImportDate.Value), "yyyy-MM-dd") & "' "

        If cboFileType.SelectedIndex = -1 Or cboFileType.Text = "" Then
            lsSql &= " and 1 = 2 "
        Else
            lsSql &= " and file_type = " & cboFileType.SelectedValue & " "
        End If

        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by file_gid desc "

        gpBindCombo(lsSql, "file_name", "file_gid", cboFileName, gOdbcConn)
    End Sub

    Private Sub cboFileName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFileName.TextChanged
        If cboFileName.SelectedIndex = -1 And cboFileName.Text <> "" Then gpAutoFillCombo(cboFileName)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnFileId As Long
        Dim lnFileType As Integer
        Dim lsTxt As String
        Dim lnResult As Integer

        If cboFileType.SelectedIndex = -1 Or cboFileType.Text = "" Then
            MessageBox.Show("Please select file type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboFileType.Focus()
            Exit Sub
        End If

        If cboFileName.SelectedIndex = -1 Or cboFileName.Text = "" Then
            MessageBox.Show("Please select the file !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboFileName.Focus()
            Exit Sub
        End If

        lnFileId = Val(cboFileName.SelectedValue)
        lnFileType = Val(cboFileType.SelectedValue)

        If MessageBox.Show("Are you sure to delete ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Using cmd As New MySqlCommand("pr_sta_del_file", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_file_gid", lnFileId)
                cmd.Parameters.AddWithValue("?in_file_type", lnFileType)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_file_gid", MySqlDbType.Int64)
                cmd.Parameters("?out_file_gid").Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, IIf(lnResult = 1, MessageBoxIcon.Information, MessageBoxIcon.Exclamation))
            End Using
        End If
    End Sub

    Private Sub cboFileName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFileName.SelectedIndexChanged

    End Sub
End Class