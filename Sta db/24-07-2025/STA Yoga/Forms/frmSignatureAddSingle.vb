Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmSignatureAddSingle
    Dim mnFolioId As Long = 0

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmSignatureAddSingle_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmSignatureAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboComp, gOdbcConn)

        KeyPreview = True
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        With OpenFileDialog1
            .Filter = "All Files (*.*)|*.*"

            .FileName = ""
            .ShowDialog()

            txtFileName.Text = .FileName
        End With
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim lsTxt As String
        Dim lnResult As Long
        Dim lsFileName As String
        Dim lsSrcFile As String
        Dim lsDestFile As String
        Dim lsFolioNo As String
        Dim lnCompId As Long
        Dim lnSignatureId As Long

        If cboComp.Text = "" Or cboComp.SelectedIndex = -1 Then
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboComp.Focus()
            Exit Sub
        Else
            lnCompId = Val(cboComp.SelectedValue.ToString())
        End If

        If File.Exists(txtFileName.Text) = False Then
            MessageBox.Show("Please select the file !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnBrowse.PerformClick()
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to add ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            lsSrcFile = txtFileName.Text
            lsFileName = lsSrcFile.Split("\")(lsSrcFile.Split("\").Length - 1)
            lsFolioNo = txtFolioNo.Text

            If File.Exists(lsSrcFile) Then
                Using cmd As New MySqlCommand("pr_sta_trn_tsignature", gOdbcConn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("?in_signature_gid", 0)
                    cmd.Parameters("?in_signature_gid").Direction = ParameterDirection.InputOutput
                    cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                    cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                    cmd.Parameters.AddWithValue("?in_folio_gid", 0)
                    cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                    cmd.Parameters.AddWithValue("?in_dup_flag", False)
                    cmd.Parameters.AddWithValue("?in_action", "INSERT")
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
                        lnSignatureId = Val(cmd.Parameters("?in_signature_gid").Value.ToString())
                        If Directory.Exists(gsAttachmentPath) = False Then Directory.CreateDirectory(gsSignaturePath)
                        lsDestFile = gsSignaturePath & "\" & lnSignatureId.ToString & ".sig"

                        If File.Exists(lsDestFile) Then
                            File.Copy(lsDestFile, gsAttachmentPath & "\" & lnSignatureId.ToString & "_" & Now.ToString() & ".sig")
                            File.Delete(lsDestFile)
                        End If

                        Call File.Copy(lsSrcFile, lsDestFile)
                    End If

                    If lnResult = 1 Then
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Call frmCtrClear(Me)
                        txtFolioNo.Focus()
                    Else
                        MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End If
        End If
    End Sub

    Private Sub txtFolioNo_TextChanged(sender As Object, e As EventArgs) Handles txtFolioNo.TextChanged

    End Sub

    Private Sub txtFolioNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFolioNo.Validating
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lnCompId As Long = 0
        Dim lsFolioNo As String

        If txtShareHolderName.Text = "" Then
            If cboComp.SelectedIndex <> -1 And cboComp.Text <> "" Then lnCompId = Val(cboComp.SelectedValue.ToString)
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
                mnFolioId = dt.Rows(0).Item("folio_gid")
            Else
                mnFolioId = 0
            End If
        End If
    End Sub
End Class