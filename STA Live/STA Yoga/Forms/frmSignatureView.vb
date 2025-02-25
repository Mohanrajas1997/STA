Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmSignatureView
    Dim mnInwardId As Long

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadGrid()
        Dim i As Integer
        Dim n As Integer
        Dim lsSql As String
        Dim lobjViewLinkButton As DataGridViewLinkColumn
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        Try
           

            cmd = New MySqlCommand("pr_sta_get_signatureview", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_folio_gid", mnInwardId)
            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            'lsSql = ""
            'lsSql &= " select "
            'lsSql &= " @a := @a + 1 as 'SNo',"
            'lsSql &= " insert_date as 'Date',"
            'lsSql &= " file_name as 'File Name',"
            'lsSql &= " insert_by as 'Uploaded By',"
            'lsSql &= " case when delete_flag='Y' then 'Old' else 'Current' end as 'Signature Status',"
            'lsSql &= " signature_gid "
            'lsSql &= " from sta_trn_tsignature "
            'lsSql &= " where folio_gid = " & mnInwardId & " "
            'lsSql &= " order by insert_date desc"

            dgvList.Columns.Clear()

            'Call gfInsertQry("set @a := 0", gOdbcConn)
            'Call gpPopGridView(dgvList, lsSql, gOdbcConn)
            dgvList.DataSource = dt
            n = dgvList.Columns.Count
            dgvList.Columns("signature_gid").Visible = False

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
        Dim lnSignatureId As Long
        Dim lsFileName As String
        Dim lsSrcFile As String
        Dim lsDestFile As String

        Try

            lnSignatureId = Val(dgvList.Rows(e.RowIndex).Cells("signature_gid").Value.ToString)
            lsFileName = dgvList.Rows(e.RowIndex).Cells("File Name").Value.ToString
            lsSrcFile = gsSignaturePath & "\" & lnSignatureId.ToString & ".sig"
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