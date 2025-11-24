Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel
Imports System.Net.Mime.MediaTypeNames
Imports System.Runtime.InteropServices
Imports System.Data

Public Class frmPledgeReport

    Private Sub frmPledgeReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        Try
            ' company
            lsSql = ""
            lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
            lsSql &= " where delete_flag = 'N' "
            lsSql &= " order by comp_name asc "

            Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

            btnClear.PerformClick()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        Dim lsbenpostfrom As String = ""
        Dim lsbenpostto As String = ""
        Dim lnCompId As Long
        Dim lsTxt As String
        Dim lnResult As String

        Try
            lsbenpostfrom = Format(CDate(dtpBenpostFrom.Value), "yyyy-MM-dd")

            lsbenpostto = Format(CDate(dtpBenpostTo.Value), "yyyy-MM-dd")

            If cboCompany.Text <> "" Then
                lnCompId = Val(cboCompany.SelectedValue.ToString)
            End If

            cmd = New MySqlCommand("pr_sta_get_pledgeshares", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_benpost_from", lsbenpostfrom)
            cmd.Parameters.AddWithValue("?in_benpost_to", lsbenpostto)

            'Out put Para
            cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
            cmd.Parameters("?out_result").Direction = ParameterDirection.Output
            cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
            cmd.Parameters("?out_msg").Direction = ParameterDirection.Output
            cmd.CommandTimeout = 0

            cmd.ExecuteNonQuery()

            lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
            lsTxt = cmd.Parameters("?out_msg").Value.ToString()

            If lnResult = 0 Then
                MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            dgvReport.DataSource = dt

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPledgeReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvReport
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvReport.Top + dgvReport.Height + 6
        pnlExport.Width = Me.Width
        btnExport.Left = pnlExport.Width - btnExport.Width - 24
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try

            Dim tempfilepath As String
            Dim outfilepath As String
            Dim sharerange As String
            Dim pledgerange As String
            Dim dt As DataTable

            tempfilepath = "C:\STA EXE\pledgesharesTemplate.xlsx"
            outfilepath = "C:\STA EXE\PledgeReport"
            Dim Workbook As XLWorkbook = New XLWorkbook(tempfilepath)
            Dim Worksheet As IXLWorksheet = Workbook.Worksheet("Main Data")

            Dim NumberOfLastRow As Integer = 1
            Dim CellForNewData As IXLCell = Worksheet.Cell(NumberOfLastRow, 1)

            dt = dgvReport.DataSource

            'Export data to the pledge template
            CellForNewData.InsertTable(dt)

            sharerange = "E2:E" + (dt.Rows.Count + 1).ToString()
            sharerange = "E2:E" + (dt.Rows.Count + 1).ToString()

            pledgerange = "F2:F" + (dt.Rows.Count + 1).ToString()
            pledgerange = "F2:F" + (dt.Rows.Count + 1).ToString()

            Worksheet.Range(sharerange).DataType = XLCellValues.Number
            Worksheet.Range(sharerange).Style.NumberFormat.Format = "#,##0.00"

            Worksheet.Range(pledgerange).DataType = XLCellValues.Number
            Worksheet.Range(pledgerange).Style.NumberFormat.Format = "#,##0.00"

            ' If folder doesnot exists means create a directory folder
            If Not System.IO.Directory.Exists(outfilepath) Then
                System.IO.Directory.CreateDirectory(outfilepath)
            End If

            'saveas file
            Workbook.SaveAs(outfilepath + "\Pledgeshares.xlsx")

            System.Diagnostics.Process.Start(outfilepath + "\Pledgeshares.xlsx")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class