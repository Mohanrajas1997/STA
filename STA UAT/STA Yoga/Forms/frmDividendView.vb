Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmDividend

    Public Property FolioGid As String
    Public Property CompGid As Integer

    Public Sub New(FolioGid As String, compgid As Integer)
        InitializeComponent()
        Me.FolioGid = FolioGid
        Me.CompGid = compgid
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmDividend_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        ' Dim lobjViewLinkButton As DataGridViewLinkColumn
        Dim folio_no As String = Me.FolioGid

        Try
            dgvList.Columns.Clear()

            'Dim sameFont As New Font("Tahoma", 8.25, FontStyle.Bold)
            'dgvList.ColumnHeadersDefaultCellStyle.Font = sameFont
            'dgvList.DefaultCellStyle.Font = sameFont

            Using cmd As New MySqlCommand("pr_get_dividentdetails", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_folio_gid", FolioGid)
                cmd.Parameters.AddWithValue("in_comp_gid", CompGid)

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvList.DataSource = dt

                ' --- Auto-adjust column widths based on header and content ---
                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                For Each col As DataGridViewColumn In dgvList.Columns
                    col.Width = col.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, True)
                    If col.Width > 250 Then col.Width = 250 ' optional max width
                Next
            End Using
            'dgvList.Columns("net_amount").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New(InwardId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FolioGid = InwardId
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

            lnAttachmentId = Val(dgvList.Rows(e.RowIndex).Cells("attachment_gid").Value.ToString)
            lsFileName = dgvList.Rows(e.RowIndex).Cells("File Name").Value.ToString
            lsSrcFile = gsAttachmentPath & "\" & lnAttachmentId.ToString & ".sta"
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

    Private Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class