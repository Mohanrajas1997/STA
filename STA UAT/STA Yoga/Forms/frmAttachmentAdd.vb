Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmAttachmentAdd
    Dim mnInwardId As Long
    Dim mbAddOnlyFlag As Boolean = False

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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
        Dim lnResult As Long
        Dim lsTxt As String
        Dim lsFileName As String
        Dim lnAttachmentId As Long
        Dim lnAttachmentTypeId As Long = 0
        Dim lsSrcFile As String
        Dim lsDestFile As String

        Try
            If cboAttachmentType.Text = "" Or cboAttachmentType.SelectedIndex = -1 Then
                MessageBox.Show("Please select attachment type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboAttachmentType.Focus()
                Exit Sub
            End If

            If File.Exists(txtFileName.Text) Then
                lsFileName = txtFileName.Text.Split("\")(txtFileName.Text.Split("\").Length - 1)
            Else
                MessageBox.Show("Please select the file !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnBrowse.PerformClick()
                Exit Sub
            End If

            lsSrcFile = txtFileName.Text
            lnAttachmentTypeId = Val(cboAttachmentType.SelectedValue.ToString)

            Using cmd As New MySqlCommand("pr_sta_trn_tattachment", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_attachment_gid", 0)
                cmd.Parameters("?in_attachment_gid").Direction = ParameterDirection.InputOutput
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_attachmenttype_gid", lnAttachmentTypeId)
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
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
                    lnAttachmentId = Val(cmd.Parameters("?in_attachment_gid").Value.ToString())
                    If Directory.Exists(gsAttachmentPath) = False Then Directory.CreateDirectory(gsAttachmentPath)
                    lsDestFile = gsAttachmentPath & "\" & lnAttachmentId.ToString & ".sta"

                    Call File.Copy(lsSrcFile, lsDestFile)

                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Call LoadGrid()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGrid()
        Dim i As Integer
        Dim n As Integer
        Dim lsSql As String
        Dim lobjViewLinkButton As DataGridViewLinkColumn

        Try

            lsSql = ""
            lsSql &= " select "
            lsSql &= " @a := @a + 1 as 'SNo',"
            lsSql &= " a.insert_date as 'Date',"
            lsSql &= " b.attachmenttype_name as 'Type',"
            lsSql &= " a.file_name as 'File Name',"
            lsSql &= " a.attachment_gid,"
            lsSql &= " a.insert_by "
            lsSql &= " from sta_trn_tattachment as a "
            lsSql &= " left join sta_mst_tattachmenttype as b on b.attachmenttype_gid = a.attachmenttype_gid and b.delete_flag = 'N' "
            lsSql &= " where a.inward_gid = " & mnInwardId & " "
            lsSql &= " and a.delete_flag = 'N' "

            dgvList.Columns.Clear()

            Call gfInsertQry("set @a = 0", gOdbcConn)
            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            n = dgvList.Columns.Count
            dgvList.Columns("attachment_gid").Visible = False

            ' view button
            lobjViewLinkButton = New DataGridViewLinkColumn

            With lobjViewLinkButton
                .HeaderText = "View"
                .Text = "View"
            End With

            dgvList.Columns.Add(lobjViewLinkButton)

            ' view button
            lobjViewLinkButton = New DataGridViewLinkColumn

            With lobjViewLinkButton
                .HeaderText = "Remove"
                .Text = "Remove"
            End With

            dgvList.Columns.Add(lobjViewLinkButton)

            With dgvList
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells(n).Value = "View"
                    .Rows(i).Cells(n + 1).Value = "Remove"

                    If mbAddOnlyFlag = True Then
                        If .Rows(i).Cells("insert_by").Value <> gsLoginUserCode Then
                            .Rows(i).Cells(n + 1).Value = "Denied"
                        End If
                    End If
                Next i
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New(InwardId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnInwardId = InwardId
    End Sub

    Public Sub New(InwardId As Long, AddOnlyFlag As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnInwardId = InwardId
        mbAddOnlyFlag = AddOnlyFlag
    End Sub

    Private Sub frmAttachmentAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select * from sta_mst_tattachmenttype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by attachmenttype_name asc "

        Call gpBindCombo(lsSql, "attachmenttype_name", "attachmenttype_gid", cboAttachmentType, gOdbcConn)

        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim n As Integer
        Dim lnAttachmentId As Long
        Dim lsFileName As String
        Dim lsSrcFile As String
        Dim lsDestFile As String

        Try

            lnAttachmentId = Val(dgvList.Rows(e.RowIndex).Cells("attachment_gid").Value.ToString)
            lsFileName = dgvList.Rows(e.RowIndex).Cells("File Name").Value.ToString
            lsSrcFile = gsAttachmentPath & "\" & lnAttachmentId.ToString & ".sta"
            lsDestFile = gsReportPath & "\" & lsFileName

            n = dgvList.Columns.Count

            Select Case e.ColumnIndex
                Case n - 2
                    File.Copy(lsSrcFile, lsDestFile, True)

                    Call gpOpenFile(lsDestFile)
                Case n - 1
                    If dgvList.Rows(e.RowIndex).Cells(n - 1).Value = "Remove" Then
                        If MessageBox.Show("Are you sure to remove ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                            Call DeleteFile(lnAttachmentId)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteFile(AttachmentId As Long)
        Dim lnResult As Long
        Dim lsTxt As String

        Try
            Using cmd As New MySqlCommand("pr_sta_trn_tattachment", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_attachment_gid", AttachmentId)
                cmd.Parameters("?in_attachment_gid").Direction = ParameterDirection.InputOutput
                cmd.Parameters.AddWithValue("?in_inward_gid", 0)
                cmd.Parameters.AddWithValue("?in_attachmenttype_gid", 0)
                cmd.Parameters.AddWithValue("?in_file_name", "")
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

                    Call LoadGrid()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class