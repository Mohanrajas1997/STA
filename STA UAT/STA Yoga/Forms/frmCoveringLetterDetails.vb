Imports MySql.Data.MySqlClient

Public Class frmCoveringLetterDetails
    Private _lnInwardId As Long
    Private _lnInwardNo As String
    Private _lnFolioNo As String

    Public Sub New(inwardId As Long, inwardNo As String, folioNo As String)
        ' This call is required by the designer.
        InitializeComponent()
        _lnInwardId = inwardId
        _lnInwardNo = inwardNo
        _lnFolioNo = folioNo
    End Sub
    Private Sub frmCoveringLetterDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub
    Private Sub LoadGrid()
        Dim lsSql As String
        Dim dt As New DataTable()

        Try
            lsSql = "SELECT a.inward_comp_no AS `Inward No`, " &
        "a.folio_no AS `Folio No`, " &
        "b.file_path AS `Path`, " &
        "c.template_name AS `Template Name`, " &
        "b.file_name AS `File Name` ," &
        "b.insert_date AS `Date` " &
        "FROM sta_trn_tinward a " &
        "JOIN sta_trn_ttemplate_files b ON a.inward_gid = b.inward_gid " &
        "JOIN sta_mst_ttemplate c ON b.template_gid = c.template_gid " &
        "WHERE a.inward_gid = " & _lnInwardId & " " &
        "ORDER BY b.insert_date DESC"


            DataGridView1.Columns.Clear()
            ' Create a MySQL Data Adapter
            Using cmd As New MySqlCommand(lsSql, gOdbcConn)
                Using da As New MySqlDataAdapter(cmd)
                    ' Fill the DataTable
                    da.Fill(dt)
                End Using
            End Using

            ' Bind DataTable to DataGridView
            DataGridView1.DataSource = dt
            ' Set equal width for columns
            For Each column As DataGridViewColumn In DataGridView1.Columns
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft

            Next
            ' Set font for all rows
            DataGridView1.DefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold)

            ' Set font for column headers
            DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold)

            ' Set font for row headers
            DataGridView1.RowHeadersDefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold)
            DataGridView1.Columns("Path").Visible = False

            ' Add a Link Button Column (if required)
            Dim lobjViewLinkButton As New DataGridViewLinkColumn()
            lobjViewLinkButton.HeaderText = "View"
            lobjViewLinkButton.Name = "View" ' Ensure column has a name
            lobjViewLinkButton.Text = "View"
            lobjViewLinkButton.UseColumnTextForLinkValue = True
            DataGridView1.Columns.Add(lobjViewLinkButton)

        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            ' Check if the clicked column is the "View" button column
            If e.ColumnIndex = DataGridView1.Columns("View").Index AndAlso e.RowIndex >= 0 Then
                ' Get the file path from the clicked row
                Dim filePath As String = DataGridView1.Rows(e.RowIndex).Cells("Path").Value.ToString()

                ' Check if the file exists
                If System.IO.File.Exists(filePath) Then
                    ' Open the file using the default application
                    Process.Start(filePath)
                Else
                    MessageBox.Show("File not found: " & filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class