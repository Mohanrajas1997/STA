Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmSignatureAdd
    Private Sub SetListViewProperty()
        With lsvFile
            .Columns.Clear()
            .Columns.Add("SNo", 40)
            .Columns.Add("File Name", 225)
            .Columns.Add("Status", 75)
            .Columns.Add("Remark", 225)
            .View = View.Details

            .FullRowSelect = True
            .GridLines = True
            .CheckBoxes = True
        End With
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
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

        Call SetListViewProperty()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim lsArr(3) As String
        Dim lobjItem As ListViewItem
        Dim lsFileName As String
        Dim n As Integer

        With FolderBrowserDialog1
            .ShowDialog()
            txtFolder.Text = .SelectedPath

            If .SelectedPath <> "" Then
                lsvFile.Items.Clear()

                For Each f As String In Directory.GetFiles(.SelectedPath)
                    With lsvFile
                        n = .Items.Count + 1
                        lsFileName = f.Split("\")(f.Split("\").Length - 1)

                        lsArr(0) = n.ToString
                        lsArr(1) = lsFileName
                        lsArr(2) = ""
                        lsArr(3) = ""

                        lobjItem = New ListViewItem(lsArr)
                        lobjItem.Checked = True

                        .Items.Add(lobjItem)
                    End With
                Next
            End If
        End With
    End Sub

    Private Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        For Each itm As ListViewItem In lsvFile.Items
            itm.Checked = chkAll.Checked
        Next
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim lobjItem As ListViewItem
        Dim i As Integer
        Dim lsTxt As String = ""
        Dim lsStatus As String
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

        If MessageBox.Show("Are you sure to add ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            With lsvFile
                For i = 0 To .Items.Count - 1
                    lobjItem = .Items(i)
                    .TopItem = lobjItem

                    If lobjItem.Checked = True Then
                        lsFileName = lobjItem.SubItems(1).Text
                        lsSrcFile = txtFolder.Text & "\" & lsFileName

                        If lsFileName.Contains(".") Then
                            lsFolioNo = Mid(lsFileName, 1, lsFileName.Length - (lsFileName.Split(".")(lsFileName.Split(".").Length - 1).Length) - 1)

                            If lsFolioNo.Contains("_") Then
                                lsFolioNo = lsFolioNo.Split("_")(0)
                            End If
                        Else
                            lsFolioNo = ""
                        End If

                        If File.Exists(lsSrcFile) Then
                            lsStatus = "Success"

                            Try
                                Using cmd As New MySqlCommand("pr_sta_trn_tsignature", gOdbcConn)
                                    cmd.CommandType = CommandType.StoredProcedure
                                    cmd.Parameters.AddWithValue("?in_signature_gid", 0)
                                    cmd.Parameters("?in_signature_gid").Direction = ParameterDirection.InputOutput
                                    cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                                    cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)
                                    cmd.Parameters.AddWithValue("?in_folio_gid", 0)
                                    cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                                    cmd.Parameters.AddWithValue("?in_dup_flag", True)
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
                                        If Directory.Exists(gsSignaturePath) = False Then Directory.CreateDirectory(gsSignaturePath)
                                        If Directory.Exists(gsSignaturePath & "\Old") = False Then Directory.CreateDirectory(gsSignaturePath & "\Old")

                                        lsDestFile = gsSignaturePath & "\" & lnSignatureId.ToString & ".sig"

                                        If File.Exists(lsDestFile) Then
                                            If chkOverWrite.Checked = True Then
                                                File.Copy(lsDestFile, gsSignaturePath & "\Old\" & lnSignatureId.ToString & "_" & Now.ToString() & ".sig")
                                                File.Delete(lsDestFile)

                                                Call File.Copy(lsSrcFile, lsDestFile)
                                            End If
                                        Else
                                            Call File.Copy(lsSrcFile, lsDestFile)
                                        End If
                                    Else
                                        lsStatus = "Failed"

                                        If chkOverWrite.Checked = True Then
                                            lnSignatureId = Val(cmd.Parameters("?in_signature_gid").Value.ToString())

                                            If Directory.Exists(gsSignaturePath) = False Then Directory.CreateDirectory(gsSignaturePath)
                                            If Directory.Exists(gsSignaturePath & "\Old") = False Then Directory.CreateDirectory(gsSignaturePath & "\Old")

                                            If lnSignatureId > 0 Then
                                                lsDestFile = gsSignaturePath & "\" & lnSignatureId.ToString & ".sig"

                                                File.Copy(lsDestFile, gsSignaturePath & "\Old\" & lnSignatureId.ToString & "_" & Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "") & ".sig")
                                                File.Delete(lsDestFile)

                                                Call File.Copy(lsSrcFile, lsDestFile)

                                                lsStatus = "Success"
                                                lsTxt = "File updated successfully,"
                                            End If
                                        End If
                                    End If
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                lsStatus = "Failed"
                            End Try
                        Else
                            lsStatus = "Failed"
                        End If

                        lobjItem.SubItems(2).Text = lsStatus
                        lobjItem.SubItems(3).Text = lsTxt

                        lsTxt = ""

                        Select Case lsStatus
                            Case "Success"
                                lobjItem.ForeColor = Color.Blue
                            Case "Failed"
                                lobjItem.ForeColor = Color.Red
                        End Select

                        Application.DoEvents()
                    End If
                Next i
            End With
        End If
    End Sub
End Class