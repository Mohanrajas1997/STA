Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmCompanyAttachmentView
    Dim mnInwardId As Long

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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
            lsSql &= " a.attachment_gid "
            lsSql &= " from sta_trn_tcompanyattachment as a "
            lsSql &= " left join sta_mst_tattachmenttype as b on b.attachmenttype_gid = a.comp_attachmenttype_gid and b.delete_flag = 'N' "
            lsSql &= " where a.comp_gid = " & mnInwardId & " "
            lsSql &= " and a.delete_flag = 'N' "

            dgvList.Columns.Clear()

            Call gfInsertQry("set @a := 0", gOdbcConn)
            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            n = dgvList.Columns.Count
            dgvList.Columns("comp_attachment_gid").Visible = False

            ' view button
            lobjViewLinkButton = New DataGridViewLinkColumn

            With lobjViewLinkButton
                .HeaderText = "View"
                .Text = "View"
            End With

            dgvList.Columns.Add(lobjViewLinkButton)

            With dgvList
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells(n).Value = "View"
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

    Private Sub frmAttachmentAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim n As Integer
        Dim lnAttachmentId As Long
        Dim lsFileName As String
        Dim lsSrcFile As String
        Dim lsDestFile As String

        Try

            lnAttachmentId = Val(dgvList.Rows(e.RowIndex).Cells("comp_attachment_gid").Value.ToString)
            lsFileName = dgvList.Rows(e.RowIndex).Cells("File Name").Value.ToString
            lsSrcFile = gsCompAttachmentPath & "\" & lnAttachmentId.ToString & ".sta"
            lsDestFile = gsReportPath & "\" & lsFileName

            n = dgvList.Columns.Count

            Select Case e.ColumnIndex
                Case n - 1
                    File.Copy(lsSrcFile, lsDestFile, True)

                    Call gpOpenFile(lsDestFile)
            End Select
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