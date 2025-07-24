Public Class frmInterDepositoryReport
    Private Sub frmUploadSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        Call GridProperty()

        dtpFrom.Value = Now
        dtpTo.Value = Now
    End Sub

    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With msfGrid
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 42)

            pnlExport.Top = .Top + .Height + 6
            pnlExport.Left = .Left
            pnlExport.Width = .Width
        End With

        btnExport.Left = Math.Abs(pnlExport.Width - btnExport.Width)
    End Sub

    Private Sub GridProperty()
        Dim i As Integer
        Dim j As Integer

        Dim row As Integer
        Dim col As Integer

        With msfGrid
            row = 0
            col = 0

            .Cols = 15
            .Rows = 3
            .FixedRows = 2

            .FixedCols = 0
            col = 0
            .set_TextMatrix(row, col, "DATE")

            col += 1
            .set_TextMatrix(row, col, "NSDL")
            col += 1
            .set_TextMatrix(row, col, "NSDL")
            col += 1
            .set_TextMatrix(row, col, "NSDL")
            col += 1
            .set_TextMatrix(row, col, "NSDL")

            col += 1
            .set_TextMatrix(row, col, "CDSL")
            col += 1
            .set_TextMatrix(row, col, "CDSL")
            col += 1
            .set_TextMatrix(row, col, "CDSL")
            col += 1
            .set_TextMatrix(row, col, "CDSL")

            col += 1
            .set_TextMatrix(row, col, "PHYSICAL")
            col += 1
            .set_TextMatrix(row, col, "PHYSICAL")
            col += 1
            .set_TextMatrix(row, col, "PHYSICAL")
            col += 1
            .set_TextMatrix(row, col, "PHYSICAL")

            col += 1
            .set_TextMatrix(row, col, "CAPITAL")
            col += 1
            .set_TextMatrix(row, col, "DEMAT")

            ' 2nd row
            row += 1
            col = 0
            .set_TextMatrix(row, col, "DATE")

            col += 1
            .set_TextMatrix(row, col, "Debit")
            col += 1
            .set_TextMatrix(row, col, "Credit")
            col += 1
            .set_TextMatrix(row, col, "")
            col += 1
            .set_TextMatrix(row, col, "%")

            col += 1
            .set_TextMatrix(row, col, "Debit")
            col += 1
            .set_TextMatrix(row, col, "Credit")
            col += 1
            .set_TextMatrix(row, col, "")
            col += 1
            .set_TextMatrix(row, col, "%")

            col += 1
            .set_TextMatrix(row, col, "Debit")
            col += 1
            .set_TextMatrix(row, col, "Credit")
            col += 1
            .set_TextMatrix(row, col, "")
            col += 1
            .set_TextMatrix(row, col, "%")

            col += 1
            .set_TextMatrix(row, col, "CAPITAL")
            col += 1
            .set_TextMatrix(row, col, "%")

            .WordWrap = True

            .Row = 0

            For i = 0 To .Cols - 1
                .set_ColWidth(i, 1440 * 1)
                .set_ColAlignment(i, 7)
            Next i

            For i = 0 To .FixedRows - 1
                For j = 0 To .Cols - 1
                    .Row = i
                    .Col = j

                    .CellAlignment = 4
                Next j
            Next i

            .set_ColAlignment(0, 1)

            .Col = 0

            .RowHeightMin = 330 * 1
            .set_RowHeight(0, 330 * 2)

            .MergeCells = MSFlexGridLib.MergeCellsSettings.flexMergeFree

            .set_MergeRow(0, True)
            .set_MergeCol(0, True)
            .set_MergeCol(.Cols - 2, True)
        End With
    End Sub

    Private Sub LoadData()
        Dim i As Integer
        Dim n As Integer
        Dim lsDate As String
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim lnCompId As Long = 0

        Dim lsFrom As String = ""
        Dim lsTo As String = ""

        Dim lnCount As Double = 0
        Dim lnDrCount As Double = 0
        Dim lnCrCount As Double = 0
        Dim lnOpening As Double = 0
        Dim lnClosing As Double = 0
        Dim lnCaptial As Double = 0

        Dim lnRow As Integer = 0
        Dim lnCol As Integer = 0

        Dim dt As New DataTable

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        lsFrom = Format(dtpFrom.Value, "yyyy-MM-dd")
        lsTo = Format(dtpTo.Value, "yyyy-MM-dd")

        With msfGrid
            .Rows = .FixedRows
            lnRow = .FixedRows
            .Rows += 1
            lnCol = 0

            Application.DoEvents()

            ' find capital total
            lsSql = ""
            lsSql &= " select "
            lsSql &= " sum(tran_count*mult) as share_count "
            lsSql &= " from sta_trn_tfolio as b "
            lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
            lsSql &= " and c.delete_flag = 'N' "
            lsSql &= " where b.comp_gid = " & lnCompId & " "
            lsSql &= " and b.delete_flag = 'N' "

            lnCaptial = Val(gfExecuteScalar(lsSql, gOdbcConn))

            .set_TextMatrix(lnRow, lnCol, "Opening")

            Application.DoEvents()

            ' find nsdl opening balance
            lsSql = ""
            lsSql &= " select "
            lsSql &= " sum(tran_count*mult) as share_count "
            lsSql &= " from sta_mst_tdepository as a "
            lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
            lsSql &= " and b.comp_gid = " & lnCompId & " "
            lsSql &= " and b.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
            lsSql &= " and c.delete_flag = 'N' "
            lsSql &= " where c.tran_date < '" & lsFrom & "' "
            lsSql &= " and a.depository_code = 'N' "
            lsSql &= " and a.delete_flag = 'N' "

            lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            lnCol += 3
            .set_TextMatrix(lnRow, lnCol, lnCount)

            lnCol += 1
            .set_TextMatrix(lnRow, lnCol, Format((lnCount / lnCaptial) * 100, "0.00"))

            Application.DoEvents()

            ' find cdsl opening balance
            lsSql = ""
            lsSql &= " select "
            lsSql &= " sum(tran_count*mult) as share_count "
            lsSql &= " from sta_mst_tdepository as a "
            lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
            lsSql &= " and b.comp_gid = " & lnCompId & " "
            lsSql &= " and b.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
            lsSql &= " and c.delete_flag = 'N' "
            lsSql &= " where c.tran_date < '" & lsFrom & "' "
            lsSql &= " and a.depository_code = 'C' "
            lsSql &= " and a.delete_flag = 'N' "

            lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            lnCol += 3
            .set_TextMatrix(lnRow, lnCol, lnCount)

            lnCol += 1
            .set_TextMatrix(lnRow, lnCol, Format((lnCount / lnCaptial) * 100, "0.00"))

            Application.DoEvents()

            ' find physical opening balance
            lsSql = ""
            lsSql &= " select "
            lsSql &= " sum(tran_count*mult) as share_count "
            lsSql &= " from sta_trn_tfolio as b "
            lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
            lsSql &= " and c.delete_flag = 'N' "
            lsSql &= " where c.tran_date < '" & lsFrom & "' "
            lsSql &= " and b.comp_gid = " & lnCompId & " "
            lsSql &= " and b.folio_no <> '00999999' "
            lsSql &= " and b.folio_no <> '00888888' "
            lsSql &= " and b.delete_flag = 'N' "

            lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

            lnCol += 3
            .set_TextMatrix(lnRow, lnCol, lnCount)

            lnCol += 1
            .set_TextMatrix(lnRow, lnCol, Format((lnCount / lnCaptial) * 100, "0.00"))

            ' capital
            lnCol += 1
            .set_TextMatrix(lnRow, lnCol, lnCaptial)

            lnCol += 1
            .set_TextMatrix(lnRow, lnCol, Format(((lnCaptial - lnCount) / lnCaptial) * 100, "0.00"))

            n = DateDiff(DateInterval.Day, dtpFrom.Value, dtpTo.Value)

            For i = 0 To n
                .Rows += 1
                lnRow += 1

                lsDate = Format(DateAdd(DateInterval.Day, i, dtpFrom.Value), "yyyy-MM-dd")

                lnCol = 0
                .set_TextMatrix(lnRow, lnCol, Format(CDate(lsDate), "dd-MM-yyyy"))

                Application.DoEvents()

                ' nsdl debit
                lsSql = ""
                lsSql &= " select "
                lsSql &= " sum(c.tran_count) as share_count "
                lsSql &= " from sta_mst_tdepository as a "
                lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
                lsSql &= " and b.comp_gid = " & lnCompId & " "
                lsSql &= " and b.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
                lsSql &= " and c.delete_flag = 'N' "
                lsSql &= " where c.tran_date = '" & lsDate & "' "
                lsSql &= " and c.mult = -1 "
                lsSql &= " and a.depository_code = 'N' "
                lsSql &= " and a.delete_flag = 'N' "

                lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))
                lnDrCount = lnCount

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, lnCount)

                Application.DoEvents()

                ' nsdl credit
                lsSql = ""
                lsSql &= " select "
                lsSql &= " sum(c.tran_count) as share_count "
                lsSql &= " from sta_mst_tdepository as a "
                lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
                lsSql &= " and b.comp_gid = " & lnCompId & " "
                lsSql &= " and b.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
                lsSql &= " and c.delete_flag = 'N' "
                lsSql &= " where c.tran_date = '" & lsDate & "' "
                lsSql &= " and c.mult = 1 "
                lsSql &= " and a.depository_code = 'N' "
                lsSql &= " and a.delete_flag = 'N' "

                lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))
                lnCrCount = lnCount

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, lnCount)

                ' get opening balance
                lnCol += 1
                lnOpening = Val(.get_TextMatrix(lnRow - 1, lnCol))

                ' set closing balance
                lnClosing = lnOpening - lnDrCount + lnCrCount
                .set_TextMatrix(lnRow, lnCol, lnClosing)

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, Format((lnClosing / lnCaptial) * 100, "0.00"))

                Application.DoEvents()

                ' cdsl debit
                lsSql = ""
                lsSql &= " select "
                lsSql &= " sum(c.tran_count) as share_count "
                lsSql &= " from sta_mst_tdepository as a "
                lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
                lsSql &= " and b.comp_gid = " & lnCompId & " "
                lsSql &= " and b.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
                lsSql &= " and c.delete_flag = 'N' "
                lsSql &= " where c.tran_date = '" & lsDate & "' "
                lsSql &= " and c.mult = -1 "
                lsSql &= " and a.depository_code = 'C' "
                lsSql &= " and a.delete_flag = 'N' "

                lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))
                lnDrCount = lnCount

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, lnCount)

                Application.DoEvents()

                ' cdsl credit
                lsSql = ""
                lsSql &= " select "
                lsSql &= " sum(c.tran_count) as share_count "
                lsSql &= " from sta_mst_tdepository as a "
                lsSql &= " inner join sta_trn_tfolio as b on a.folio_no = b.folio_no "
                lsSql &= " and b.comp_gid = " & lnCompId & " "
                lsSql &= " and b.delete_flag = 'N' "
                lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
                lsSql &= " and c.delete_flag = 'N' "
                lsSql &= " where c.tran_date = '" & lsDate & "' "
                lsSql &= " and c.mult = 1 "
                lsSql &= " and a.depository_code = 'C' "
                lsSql &= " and a.delete_flag = 'N' "

                lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))
                lnCrCount = lnCount

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, lnCount)

                ' get opening balance
                lnCol += 1
                lnOpening = Val(.get_TextMatrix(lnRow - 1, lnCol))

                ' set closing balance
                lnClosing = lnOpening - lnDrCount + lnCrCount
                .set_TextMatrix(lnRow, lnCol, lnClosing)

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, Format((lnClosing / lnCaptial) * 100, "0.00"))

                Application.DoEvents()

                ' physical debit
                lsSql = ""
                lsSql &= " select "
                lsSql &= " sum(c.tran_count) as share_count "
                lsSql &= " from sta_trn_tfolio as b "
                lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
                lsSql &= " and c.delete_flag = 'N' "
                lsSql &= " where c.tran_date = '" & lsDate & "' "
                lsSql &= " and c.mult = -1 "
                lsSql &= " and b.comp_gid = " & lnCompId & " "
                lsSql &= " and b.folio_no <> '00999999' "
                lsSql &= " and b.folio_no <> '00888888' "
                lsSql &= " and b.delete_flag = 'N' "

                lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))
                lnDrCount = lnCount

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, lnCount)

                Application.DoEvents()

                ' physical credit
                lsSql = ""
                lsSql &= " select "
                lsSql &= " sum(c.tran_count) as share_count "
                lsSql &= " from sta_trn_tfolio as b "
                lsSql &= " inner join sta_trn_tfoliotran as c on b.folio_gid = c.folio_gid "
                lsSql &= " and c.delete_flag = 'N' "
                lsSql &= " where c.tran_date = '" & lsDate & "' "
                lsSql &= " and c.mult = 1 "
                lsSql &= " and b.comp_gid = " & lnCompId & " "
                lsSql &= " and b.folio_no <> '00999999' "
                lsSql &= " and b.folio_no <> '00888888' "
                lsSql &= " and b.delete_flag = 'N' "

                lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))
                lnCrCount = lnCount

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, lnCount)

                ' get opening balance
                lnCol += 1
                lnOpening = Val(.get_TextMatrix(lnRow - 1, lnCol))

                ' set closing balance
                lnClosing = lnOpening - lnDrCount + lnCrCount
                .set_TextMatrix(lnRow, lnCol, lnClosing)

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, Format((lnClosing / lnCaptial) * 100, "0.00"))

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, lnCaptial)

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, Format(((lnCaptial - lnClosing) / lnCaptial) * 100, "0.00"))

                .TopRow = lnRow
            Next i
        End With
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call loaddata()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintFGridXMLMerge(msfGrid, gsReportPath & "\Report.xls", "Report", True, True)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub
End Class