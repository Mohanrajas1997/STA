Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmCertificateList
    Dim mnFolioId As Long

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
            lsSql &= " if(cert_sub_no = 1,cast(cert_no as nchar),concat(cast(cert_no as nchar),'-',cast(cert_sub_no as nchar))) as 'Certificate No',"
            lsSql &= " issue_date as 'Issue Date',"
            lsSql &= " share_count as 'Share Count',"
            lsSql &= " lockin_period_from as 'Lockin From',"
            lsSql &= " lockin_period_to as 'Lockin To',"
            lsSql &= " hold_date as 'Hold Date',"
            lsSql &= " hold_release_date as 'Hold Release Date',"
            lsSql &= " make_set(cert_status," & gsCertStatusDesc & ") as 'Status',"
            lsSql &= " cert_gid "
            lsSql &= " from sta_trn_tcert "
            lsSql &= " where folio_gid = " & mnFolioId & " "
            lsSql &= " and delete_flag = 'N' "
            lsSql &= " order by cert_no "

            With dgvList
                .Columns.Clear()

                Call gfInsertQry("set @a := 0", gOdbcConn)
                Call gpPopGridView(dgvList, lsSql, gOdbcConn)

                n = .Columns.Count
                .Columns("cert_gid").Visible = False

                ' view button
                lobjViewLinkButton = New DataGridViewLinkColumn

                With lobjViewLinkButton
                    .HeaderText = "View"
                    .Text = "View"
                End With

                .Columns.Add(lobjViewLinkButton)

                For i = 0 To .RowCount - 1
                    .Rows(i).Cells(n).Value = "View"
                Next i

                txtTot.Text = "Total Certificates : " & .Rows.Count.ToString
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New(FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub

    Private Sub frmAttachmentAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadGrid()
    End Sub

    Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        Dim n As Integer
        Dim lnCertId As Long
        Dim frm As frmCertificateView

        Try
            lnCertId = Val(dgvList.Rows(e.RowIndex).Cells("cert_gid").Value.ToString)

            n = dgvList.Columns.Count

            Select Case e.ColumnIndex
                Case n - 1
                    frm = New frmCertificateView(lnCertId)
                    frm.ShowDialog()
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