
Imports MySql.Data.MySqlClient
Imports System.Data.OleDb
Imports System.IO
Imports System.Text
Imports System

Public Class frmshp

    Dim Promoter_Tot1 As Integer = 0
    Dim Promoter_sum1 As Double = 0.0
    Dim Promoter_sum2 As Double = 0.0
    Dim Promoter_sum3 As Double = 0.0
    Dim Promoter_sum4 As Double = 0.0

    Dim Total As Integer = 0
    Dim Tot_sum1 As Double = 0.0
    Dim Tot_sum2 As Double = 0.0
    Dim Tot_sum3 As Double = 0.0
    Dim Tot_sum4 As Double = 0.0

    Dim dt As New Data.DataTable
    Dim row() As String
    Dim lsnewHeaderTxt() As String
    Dim headerText As String = ""

    Dim lnCompId As Long = 0
    Dim lsBenpostDate As String = ""

    Dim cmd1 As MySqlCommand
    Dim da1 As MySqlDataAdapter
    'Dim dt As DataTable()
    Dim ds1 As DataSet
    Dim lsCond As String = ""


    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, _
           ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, _
           ByVal nShowCmd As Integer) As Integer
    Const SW_SHOWNORMAL As Short = 1

    Private Sub frmshp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim lsSql As String
       
        With dgvList
            For i = 0 To .ColumnCount - 1
                headerText &= dgvList.Columns(i).HeaderText() + "|"
            Next i

        End With

        lsnewHeaderTxt = Split(headerText, "|")


        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpBenpost.Value = Now
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        dgvList.DataSource = Nothing
        dgvList1.DataSource = Nothing
        dgvList2.DataSource = Nothing
        dgvList3.DataSource = Nothing
        dgvList4.DataSource = Nothing
        dgvList5.DataSource = Nothing
        Call frmCtrClear(Me)
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub sumclear()
        Promoter_Tot1 = 0
        Promoter_sum1 = 0.0
        Promoter_sum2 = 0.0
        Promoter_sum3 = 0.0
    End Sub

    Private Sub frmshp_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim llHeight As Long
        Dim llWidth As Long
        Dim llTop As Long

        llHeight = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - 16)
        llHeight = (llHeight - 48 - (lblPromoter.Height * 4) - 6 * 4) \ 4

        llWidth = Me.Width - 30

        llTop = pnlMain.Top + pnlMain.Height + 6

        lblPromoter.Top = llTop
        lblPromoter.Left = pnlMain.Left
        llTop = llTop + lblPromoter.Height + 6

        dgvList.Top = llTop
        dgvList.Height = llHeight
        dgvList.Width = llWidth
        dgvList.Left = pnlMain.Left
        llTop = llTop + llHeight + 6

        lblFrpromoter.Top = llTop
        lblFrpromoter.Left = pnlMain.Left



        llTop = llTop + lblPromoter.Height + 6

        dgvList1.Top = llTop
        dgvList1.Height = llHeight
        dgvList1.Left = pnlMain.Left
        dgvList1.Width = llWidth

        llTop = llTop + llHeight + 6

        lblInstitutions.Top = llTop
        lblInstitutions.Left = pnlMain.Left



        'lblCertDistCount.Top = lblCertDist.Top
        'lblCertDistCount.Left = lblCertDist.Left + lblCertDist.Width + 12

        llTop = llTop + lblPromoter.Height + 6
        llWidth = (llWidth - 4) \ 2

        'llTop = llTop + lblPromoter.Height + 6

        dgvList2.Top = llTop
        dgvList2.Height = llHeight
        dgvList2.Left = pnlMain.Left
        dgvList2.Width = llWidth

        'llTop = llTop + llHeight + 

        lblNoninstitutions.Top = lblInstitutions.Top
        lblNoninstitutions.Left = dgvList2.Left + dgvList2.Width + 2

        'lblTranCount.Top = lblCertDistCount.Top
        ' lblTranCount.Left = lblTran.Left + lblTran.Width + 12

        'llTop = llTop + lblPromoter.Height + 6

        dgvList3.Top = llTop
        dgvList3.Height = llHeight
        dgvList3.Left = dgvList2.Left + dgvList2.Width + 2
        dgvList3.Width = dgvList2.Width


        llTop = llTop + llHeight + 6





        lbdrshares.Top = llTop
        lbdrshares.Left = pnlMain.Left

        lblemptrust.Top = lbdrshares.Top
        lblemptrust.Left = pnlMain.Left + llWidth + 2

        llTop = llTop + lblPromoter.Height + 6

        dgvList4.Top = llTop
        dgvList4.Height = llHeight
        dgvList4.Left = pnlMain.Left
        dgvList4.Width = llWidth


        dgvList5.Top = llTop
        dgvList5.Height = llHeight
        dgvList5.Left = dgvList4.Left + dgvList4.Width + 2
        dgvList5.Width = dgvList4.Width

        llTop = llTop + lblPromoter.Height + 6
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call LoadData()
        ' Sorting Remove
        With dgvList
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i
        End With

        With dgvList1
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i
        End With

        With dgvList2
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i
        End With

        With dgvList3
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i
        End With

        With dgvList4
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i
        End With

        With dgvList5
            For i = 0 To .ColumnCount - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i
        End With

    End Sub



    Private Sub LoadData()
        Dim lsSql As String

        Dim squery As String

      




        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable = New DataTable()
        Dim ds As DataSet
        Dim ds1 As DataSet = New DataSet

        dgvList.DataSource = Nothing
        dgvList1.DataSource = Nothing
        dgvList2.DataSource = Nothing
        dgvList3.DataSource = Nothing
        dgvList4.DataSource = Nothing
        dgvList5.DataSource = Nothing

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        lsBenpostDate = Format(dtpBenpost.Value, "yyyy-MM-dd")

        squery = ""
        squery &= " select comp_gid from sta_mst_tcompany where true and ifnull(isin_id,'') <> ''  and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' ;"

        cmd1 = New MySqlCommand(squery, gOdbcConn)
        Dim dataAdapter As MySqlDataAdapter = New MySqlDataAdapter(cmd1)
        Dim datatable As DataTable = New DataTable()
        dataAdapter.Fill(datatable)

        If datatable.Rows.Count > 0 Then

            lsSql = ""
            lsSql &= "SELECT benpost_date FROM sta_trn_tbenpost where true and benpost_date = '" & Format(dtpBenpost.Value, "yyyy-MM-dd") & "' and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' ;"
            cmd = New MySqlCommand(lsSql, gOdbcConn)
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)(0).ToString) Then
                    lsBenpostDate = dt.Rows(0)(0).ToString
                End If

            Else
                MessageBox.Show("Invalid benpost date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpBenpost.Focus()
                Exit Sub
            End If

        End If
        ' ''find benpost date
        'lsSql = ""
        'lsSql &= " select max(benpost_date) from sta_trn_tbenpost "
        'lsSql &= " where comp_gid = " & lnCompId & " "
        'lsSql &= " and benpost_date <= '" & lsBenpostDate & "' "
        'lsSql &= " and delete_flag = 'N' "

        'lsBenpostDate = gfExecuteScalar(lsSql, gOdbcConn)

        If IsDate(lsBenpostDate) = False Then
            MessageBox.Show("Invalid benpost date !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpBenpost.Focus()
            Exit Sub
        Else
            lsBenpostDate = Format(CDate(lsBenpostDate), "yyyy-MM-dd")
        End If

        cmd = New MySqlCommand("pr_sta_getshareholdingpattern", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
        cmd.Parameters.AddWithValue("?in_benpost_date", CDate(lsBenpostDate))
        cmd.CommandTimeout = 0

        ds = New DataSet
        da = New MySqlDataAdapter(cmd)
        da.Fill(ds)


        'Dim Promoter_Tot1 As Integer = 0
        'Dim Promoter_sum1 As Double = 0.0
        'Dim Promoter_sum2 As Double = 0.0
        'Dim Promoter_sum3 As Double = 0.0

        For K As Integer = 0 To ds.Tables.Count - 1

            Dim Tot1 As Integer = 0
            Dim sum1 As Double = 0.0
            Dim sum2 As Double = 0.0
            Dim sum3 As Double = 0.0
            Dim sum4 As Double = 0.0



            If ds.Tables(K).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(K).Rows.Count - 1

                    Tot1 += Convert.ToInt32(ds.Tables(K).Rows(i)("Shareholder_count").ToString())
                    sum1 += Convert.ToInt32(ds.Tables(K).Rows(i)("paided_sharecount").ToString())
                    sum2 += Convert.ToInt32(ds.Tables(K).Rows(i)("total_sharecount").ToString())
                    sum3 += Convert.ToDouble(ds.Tables(K).Rows(i)("calculation").ToString())
                    sum4 += Convert.ToDouble(ds.Tables(K).Rows(i)("equity_sharescount").ToString())
                Next
            End If

            If K = 0 Then

                Promoter_Tot1 = Convert.ToInt32(Tot1.ToString)
                Promoter_sum1 = Convert.ToDouble(sum1.ToString)
                Promoter_sum2 = Convert.ToDouble(sum2.ToString)
                Promoter_sum3 = Convert.ToDouble(sum3.ToString)
                Promoter_sum4 = Convert.ToDouble(sum4.ToString)

                ds.Tables(K).Rows.Add(New Object() {"", "Sub-Total (A)(1)", Tot1, sum1, sum2, FormatNumber(CDbl(sum3), 3), sum1, sum2, FormatNumber(CDbl(sum3), 3), FormatNumber(CDbl(sum3), 3), sum4})



                With dgvList
                    .DataSource = ds.Tables(K)

                    'For i = 0 To .ColumnCount - 1
                    '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    'Next i

                End With

            End If

            If K = 1 Then

                Promoter_Tot1 += Convert.ToInt32(Tot1.ToString)
                Promoter_sum1 += Convert.ToDouble(sum1.ToString)
                Promoter_sum2 += Convert.ToDouble(sum2.ToString)
                Promoter_sum3 += Convert.ToDouble(sum3.ToString)
                Promoter_sum4 += Convert.ToDouble(sum4.ToString)

                Total = Convert.ToInt32(Promoter_Tot1.ToString)
                Tot_sum1 = Convert.ToDouble(Promoter_sum1.ToString)
                Tot_sum2 = Convert.ToDouble(Promoter_sum2.ToString)
                Tot_sum3 = Convert.ToDouble(Promoter_sum3.ToString)
                Tot_sum4 = Convert.ToDouble(Promoter_sum4.ToString)


                ds.Tables(K).Rows.Add(New Object() {"", "Sub-Total (A)(2)", Tot1, sum1, sum2, FormatNumber(CDbl(sum3), 3), sum1, sum2, FormatNumber(CDbl(sum3), 3), FormatNumber(CDbl(sum3), 3), sum4})
                ds.Tables(K).Rows.Add(New Object() {"", "Total Shareholding of Promoter and Promoter Group (A)=(A)(1)+(A)(2) ", Promoter_Tot1, Promoter_sum1, Promoter_sum2, FormatNumber(CDbl(Promoter_sum3), 3), Promoter_sum1, Promoter_sum2, FormatNumber(CDbl(Promoter_sum3), 3), FormatNumber(CDbl(Promoter_sum3), 3), Promoter_sum4})
                'dt.Rows.Add(New Object() {"", 0, "Promoter & Promoter Group ", Promoter_Tot1, Promoter_sum1, Promoter_sum2, Promoter_sum3})
                'row = New String() {"", "Promoter & Promoter Group ", Promoter_Tot1, Promoter_sum1, Promoter_sum2, Promoter_sum3}
                'dgvList5.Rows.Add(row)

                With dgvList1
                    .DataSource = ds.Tables(K)

                    'For i = 0 To .ColumnCount - 1
                    '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    'Next i

                End With

                sumclear()
            End If

            If K = 2 Then
                Promoter_Tot1 = Convert.ToInt32(Tot1.ToString)
                Promoter_sum1 = Convert.ToDouble(sum1.ToString)
                Promoter_sum2 = Convert.ToDouble(sum2.ToString)
                Promoter_sum3 = Convert.ToDouble(sum3.ToString)
                Promoter_sum4 = Convert.ToDouble(sum4.ToString)

                ds.Tables(K).Rows.Add(New Object() {"", "Sub-Total (B)(1)", Tot1, sum1, sum2, FormatNumber(CDbl(sum3), 3), sum1, sum2, FormatNumber(CDbl(sum3), 3), FormatNumber(CDbl(sum3), 3), sum4})

                With dgvList2
                    .DataSource = ds.Tables(K)

                    'For i = 0 To .ColumnCount - 1
                    '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    'Next i

                End With

            End If

            If K = 3 Then

                Promoter_Tot1 += Convert.ToInt32(Tot1.ToString)
                Promoter_sum1 += Convert.ToDouble(sum1.ToString)
                Promoter_sum2 += Convert.ToDouble(sum2.ToString)
                Promoter_sum3 += Convert.ToDouble(sum3.ToString)
                Promoter_sum4 += Convert.ToDouble(sum4.ToString)


                Total += Convert.ToInt32(Promoter_Tot1.ToString)
                Tot_sum1 += Convert.ToDouble(Promoter_sum1.ToString)
                Tot_sum2 += Convert.ToDouble(Promoter_sum2.ToString)
                Tot_sum3 += Convert.ToDouble(Promoter_sum3.ToString)
                Tot_sum4 += Convert.ToDouble(Promoter_sum4.ToString)

                ds.Tables(K).Rows.Add(New Object() {"", "Sub-Total (B)(2)", Tot1, sum1, sum2, FormatNumber(CDbl(sum3), 3), sum1, sum2, FormatNumber(CDbl(sum3), 3), FormatNumber(CDbl(sum3), 3), sum4})
                ds.Tables(K).Rows.Add(New Object() {"", "Total Shareholding of Public Share holder Group (B)=(B)(1)+(B)(2) ", Promoter_Tot1, Promoter_sum1, Promoter_sum2, FormatNumber(CDbl(Promoter_sum3), 3), Promoter_sum1, Promoter_sum2, FormatNumber(CDbl(Promoter_sum3), 3), FormatNumber(CDbl(Promoter_sum3), 3), Promoter_sum4})
                'dt.Rows.Add(New Object() {"", 0, "Public", Promoter_Tot1, Promoter_sum1, Promoter_sum2, Promoter_sum3})
                'row = New String() {"", "Public ", Promoter_Tot1, Promoter_sum1, Promoter_sum2, Promoter_sum3}
                'dgvList5.Rows.Add(row)
                With dgvList3
                    .DataSource = ds.Tables(K)

                    'For i = 0 To .ColumnCount - 1
                    '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    'Next i

                End With
                sumclear()
            End If

            If K = 4 Then

                Promoter_Tot1 = Convert.ToInt32(Tot1.ToString)
                Promoter_sum1 = Convert.ToDouble(sum1.ToString)
                Promoter_sum2 = Convert.ToDouble(sum2.ToString)
                Promoter_sum3 = Convert.ToDouble(sum3.ToString)
                Promoter_sum4 = Convert.ToDouble(sum4.ToString)

                Total += Convert.ToInt32(Promoter_Tot1.ToString)
                Tot_sum1 += Convert.ToDouble(Promoter_sum1.ToString)
                Tot_sum2 += Convert.ToDouble(Promoter_sum2.ToString)
                Tot_sum3 += Convert.ToDouble(Promoter_sum3.ToString)
                Tot_sum4 += Convert.ToDouble(Promoter_sum4.ToString)

                ds.Tables(K).Rows.Add(New Object() {"", "Sub-Total (C1)", Tot1, sum1, sum2, FormatNumber(CDbl(sum3), 3), sum1, sum2, FormatNumber(CDbl(sum3), 3), FormatNumber(CDbl(sum3), 3), sum4})
                ds.Tables(K).Rows.Add(New Object() {"", "Total (A+B+C)", Total, Tot_sum1, Tot_sum2, FormatNumber(CDbl(Tot_sum3), 3), Tot_sum1, Tot_sum2, FormatNumber(CDbl(Tot_sum3), 3), FormatNumber(CDbl(Tot_sum3), 3), Tot_sum4})

                With dgvList4
                    .DataSource = ds.Tables(K)

                    'For i = 0 To .ColumnCount - 1
                    '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    'Next i

                End With
                sumclear()
            End If

            If K = 5 Then

                Promoter_Tot1 += Convert.ToInt32(Tot1.ToString)
                Promoter_sum1 += Convert.ToDouble(sum1.ToString)
                Promoter_sum2 += Convert.ToDouble(sum2.ToString)
                Promoter_sum3 += Convert.ToDouble(sum3.ToString)
                Promoter_sum4 += Convert.ToDouble(sum4.ToString)

                ds.Tables(K).Rows.Add(New Object() {"", "Total (C2)", Tot1, sum1, sum2, FormatNumber(CDbl(sum3), 3), sum1, sum2, FormatNumber(CDbl(sum3), 3), FormatNumber(CDbl(sum3), 3), sum4})
                ' ds.Tables(K).Rows.Add(New Object() {"", 0, "Total Shareholding of Promoter and Promoter Group (C)=(C)(1)+(C)(2) ", Promoter_Tot1, Promoter_sum1, Promoter_sum2, Promoter_sum3})
                'dt.Rows.Add(New Object() {"", 0, "Non-Public(Shares Underlying DR)", Promoter_Tot1, Promoter_sum1, Promoter_sum2, Promoter_sum3})
                With dgvList5
                    .DataSource = ds.Tables(K)

                    'For i = 0 To .ColumnCount - 1
                    '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    'Next i

                End With
                sumclear()
            End If

        Next K
        LoadShareholdingdetails()
    End Sub

    Private Sub LoadShareholdingdetails()
       

        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        lsBenpostDate = Format(dtpBenpost.Value, "yyyy-MM-dd")


        cmd1 = New MySqlCommand("pr_sta_getshareholdingpatterndetail", gOdbcConn)
        cmd1.CommandType = CommandType.StoredProcedure
        cmd1.Parameters.AddWithValue("?in_comp_gid", lnCompId)
        cmd1.Parameters.AddWithValue("?in_benpost_date", CDate(lsBenpostDate))
        cmd1.CommandTimeout = 0

        ds1 = New DataSet
        da1 = New MySqlDataAdapter(cmd1)
        da1.Fill(ds1)

        'For i As Integer = 0 To ds1.Tables.Count - 1


        '    With ds1.Tables(i)
        '        .Columns(0).Caption = "SNO"
        '        .Columns(1).Caption = "Name"
        '        .Columns(2).Caption = "PAN"
        '        .Columns(3).Caption = "No. of fully paid up equity shares held"
        '        .Columns(4).Caption = "Total nos. shares"
        '        .Columns(5).Caption = "Shareholding as a % of total no. of shares (calculated as per SCRR, 1957)"
        '        .Columns(6).Caption = "Number of Voting Rights held in each class of securities"
        '        .Columns(7).Caption = " Voting Rights "
        '        .Columns(8).Caption = " Voting Rights "
        '        .Columns(9).Caption = "Shareholding , as a % assuming full conversion of convertible securities (as a percentage of diluted share capital)"
        '        .Columns(10).Caption = "Number of equity shares held in dematerialized form"
        '    End With


        'Next i

    End Sub






    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim objdstable As New Data.DataTable
            Dim objdstable1 As New Data.DataTable
            Dim objdstable2 As New Data.DataTable
            Dim objdstable3 As New Data.DataTable
            Dim objdstable4 As New Data.DataTable
            Dim objdstable5 As New Data.DataTable

            'Dim TotColumn As Integer
            objdstable = ds1.Tables(0)
            objdstable1 = ds1.Tables(1)
            objdstable2 = ds1.Tables(2)
            objdstable3 = ds1.Tables(3)
            objdstable4 = ds1.Tables(4)
            objdstable5 = ds1.Tables(5)

            PrintExportExcel1(dgvList, dgvList1, dgvList2, dgvList3, dgvList4, gsReportPath & "\Report.xls", "Report", "", True)
            PrintExportExcel2(dgvList5, gsReportPath & "\Report.xls", "Summary", "", False)
            PrintExportExcel3(objdstable, gsReportPath & "\Report.xls", "Indian Promoters", "", False)
            PrintExportExcel3(objdstable1, gsReportPath & "\Report.xls", "Foreign Promoters", "", False)
            PrintExportExcel3(objdstable2, gsReportPath & "\Report.xls", "Public Institutions", "", False)
            PrintExportExcel3(objdstable3, gsReportPath & "\Report.xls", "Public Non Institutions", "", False)
            PrintExportExcel3(objdstable4, gsReportPath & "\Report.xls", "Non Public Share Holders(DR)", "", False)
            PrintExportExcel3(objdstable5, gsReportPath & "\Report.xls", "Share Holder'S for 1 % and ABOVE", "", False)

            Call gpOpenFile(gsReportPath & "\Report.xls")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub PrintDGridExcel(ByVal dgrid As DataGridView, ByVal FileName As String, Optional ByVal SheetName As String = "Report", Optional ByVal StoreasNum As String = "", Optional ByVal HeaderTxt As String = "")
        Dim objDataTable As New Data.DataTable
        Dim TotCol As Integer, Col As Integer, Row As Integer
        Dim objStreamReader As StreamReader
        Dim lsTextContent As String
        Dim NumericCols() As String
        Dim lsHeaderTxt() As String
        Dim lsnewHeaderTxt() As String
        Dim i As Integer

        Dim headerText As String = ""
        With dgrid
            For i = 0 To .ColumnCount - 1
                headerText &= dgrid.Columns(i).HeaderText() + "|"
            Next i

        End With


        NumericCols = Split(StoreasNum, "|")
        HeaderTxt = HeaderTxt.Replace(Chr(10), "")
        lsHeaderTxt = Split(HeaderTxt, Chr(13))
        lsnewHeaderTxt = Split(headerText, "|")
        Try

            objDataTable = dgrid.DataSource
            TotCol = objDataTable.Columns.Count
            If File.Exists(FileName) Then File.Delete(FileName)

            If File.Exists(FileName) = False Then
                FileOpen(1, FileName, OpenMode.Output)

                PrintLine(1, "<?xml version=""1.0"" encoding=""utf-8""?>")
                PrintLine(1, "<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:x=""urn:schemas-microsoft-com:office:excel"" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:html=""http://www.w3.org/TR/REC-html40"">")

                PrintLine(1, "<Styles>")
                PrintLine(1, "<Style ss:ID=""s1"">")
                PrintLine(1, "<Font x:Family=""Swiss"" ss:Bold=""1""/>")
                PrintLine(1, "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "<Style ss:ID=""s2"">")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "<Style ss:ID=""s3"">")
                PrintLine(1, "<Font x:Family=""Swiss"" ss:Bold=""1""/>")
                PrintLine(1, "</Style>")
                PrintLine(1, "</Styles>")
            Else
                objStreamReader = File.OpenText(FileName)
                lsTextContent = objStreamReader.ReadToEnd()
                lsTextContent = Replace(lsTextContent.ToString, "</Workbook>", "")
                objStreamReader.Close()
                FileOpen(1, FileName, OpenMode.Output)
                PrintLine(1, lsTextContent.ToString)
            End If

            PrintLine(1, "<Worksheet ss:Name=""" & SheetName & """>")
            PrintLine(1, "<Table>")

            If HeaderTxt <> "" Then
                For i = 0 To lsHeaderTxt.Length - 1
                    PrintLine(1, "<Row>")
                    PrintLine(1, "<Cell ss:MergeAcross=" & Chr(34) & TotCol - 1 & Chr(34) & " ss:StyleID=""s3""><Data ss:Type=""String"">" & lsHeaderTxt(i) & "</Data></Cell>")
                    PrintLine(1, "</Row>")
                Next i

                PrintLine(1, "<Row>")
                PrintLine(1, "<Cell ss:StyleID=""s3""><Data ss:Type=""String""> </Data></Cell>")
                PrintLine(1, "</Row>")
            End If

            PrintLine(1, "<Row>")
            For Col = 0 To TotCol
                ' PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & objDataTable.Columns(Col - 1).Caption.ToString & "</Data></Cell>")
                PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & lsnewHeaderTxt(Col) & "</Data></Cell>")
            Next
            PrintLine(1, "</Row>")

            For Row = 0 To objDataTable.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol
                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & objDataTable.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & objDataTable.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            PrintLine(1, "</Table>")
            PrintLine(1, "</Worksheet>")
            PrintLine(1, "</Workbook>")

            FileClose(1)
            Call ShellExecute(frmMain.Handle.ToInt32, "open", FileName, "", "0", SW_SHOWNORMAL)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub PrintExportExcel(ByVal dgrid As DataGridView, ByVal dgrid1 As DataGridView, ByVal dgrid2 As DataGridView, ByVal dgrid3 As DataGridView, ByVal dgrid4 As DataGridView, ByVal FileName As String, Optional ByVal SheetName As String = "Report", Optional ByVal StoreasNum As String = "", Optional ByVal HeaderTxt As String = "")
        Dim objDataTable As New Data.DataTable
        Dim objDataTable1 As New Data.DataTable
        Dim objDataTable2 As New Data.DataTable
        Dim objDataTable3 As New Data.DataTable
        Dim objDataTable4 As New Data.DataTable


        Dim TotCol As Integer, Col As Integer, Row As Integer, TotCol1 As Integer, TotCol2 As Integer, TotCol3 As Integer, TotCol4 As Integer, TotCol5 As Integer
        Dim objStreamReader As StreamReader
        Dim lsTextContent As String
        Dim NumericCols() As String
        Dim lsHeaderTxt() As String
        Dim lsnewHeaderTxt() As String
        Dim i As Integer

        Dim headerText As String = ""
        With dgrid
            For i = 0 To .ColumnCount - 1
                headerText &= dgrid.Columns(i).HeaderText() + "|"
            Next i

        End With


        NumericCols = Split(StoreasNum, "|")
        HeaderTxt = HeaderTxt.Replace(Chr(10), "")
        lsHeaderTxt = Split(HeaderTxt, Chr(13))
        lsnewHeaderTxt = Split(headerText, "|")
        Try

            objDataTable = dgrid.DataSource
            objDataTable1 = dgrid1.DataSource
            objDataTable2 = dgrid2.DataSource
            objDataTable3 = dgrid3.DataSource
            objDataTable4 = dgrid4.DataSource
            '  objDataTable5 = dgrid5.DataSource

            TotCol = objDataTable.Columns.Count
            TotCol1 = objDataTable1.Columns.Count
            TotCol2 = objDataTable2.Columns.Count
            TotCol3 = objDataTable3.Columns.Count
            TotCol4 = objDataTable4.Columns.Count


            If File.Exists(FileName) Then File.Delete(FileName)

            If File.Exists(FileName) = False Then
                FileOpen(1, FileName, OpenMode.Output)

                PrintLine(1, "<?xml version=""1.0"" encoding=""utf-8""?>")
                PrintLine(1, "<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:x=""urn:schemas-microsoft-com:office:excel"" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:html=""http://www.w3.org/TR/REC-html40"">")

                PrintLine(1, "<Styles>")
                PrintLine(1, "<Style ss:ID=""s1"">")
                PrintLine(1, "<Font x:Family=""Swiss"" ss:Bold=""1""/>")
                PrintLine(1, "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "<Style ss:ID=""s2"">")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "<Style ss:ID=""s3"">")
                PrintLine(1, "<Font x:Family=""Swiss"" ss:Bold=""1""/>")
                PrintLine(1, "</Style>")
                PrintLine(1, "</Styles>")
            Else
                objStreamReader = File.OpenText(FileName)
                lsTextContent = objStreamReader.ReadToEnd()
                lsTextContent = Replace(lsTextContent.ToString, "</Workbook>", "")
                objStreamReader.Close()
                FileOpen(1, FileName, OpenMode.Output)
                PrintLine(1, lsTextContent.ToString)
            End If

            PrintLine(1, "<Worksheet ss:Name=""" & SheetName & """>")
            PrintLine(1, "<Table>")

            If HeaderTxt <> "" Then
                For i = 0 To lsHeaderTxt.Length - 1
                    PrintLine(1, "<Row>")
                    PrintLine(1, "<Cell ss:MergeAcross=" & Chr(34) & TotCol - 1 & Chr(34) & " ss:StyleID=""s3""><Data ss:Type=""String"">" & lsHeaderTxt(i) & "</Data></Cell>")
                    PrintLine(1, "</Row>")
                Next i

                PrintLine(1, "<Row>")
                PrintLine(1, "<Cell ss:StyleID=""s3""><Data ss:Type=""String""> </Data></Cell>")
                PrintLine(1, "</Row>")
            End If

            PrintLine(1, "<Row>")
            For Col = 0 To TotCol
                ' PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & objDataTable.Columns(Col - 1).Caption.ToString & "</Data></Cell>")
                PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & lsnewHeaderTxt(Col) & "</Data></Cell>")
            Next
            PrintLine(1, "</Row>")

            For Row = 0 To objDataTable.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol
                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & objDataTable.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & objDataTable.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            For Row = 0 To objDataTable1.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol1
                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & objDataTable1.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & objDataTable1.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            For Row = 0 To objDataTable2.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol2
                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & objDataTable2.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & objDataTable2.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            For Row = 0 To objDataTable3.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol3
                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & objDataTable3.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & objDataTable3.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            For Row = 0 To objDataTable4.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol4
                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & objDataTable4.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & objDataTable4.Rows(Row).Item(Col - 1).ToString & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            PrintLine(1, "</Table>")
            PrintLine(1, "</Worksheet>")
            PrintLine(1, "</Workbook>")

            FileClose(1)
            Call ShellExecute(frmMain.Handle.ToInt32, "open", FileName, "", "0", SW_SHOWNORMAL)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub PrintExportExcel1(ByVal dgrid As DataGridView, ByVal dgrid1 As DataGridView, ByVal dgrid2 As DataGridView, ByVal dgrid3 As DataGridView, ByVal dgrid4 As DataGridView, ByVal FileName As String, Optional ByVal SheetName As String = "Report", Optional ByVal StoreasNum As String = "", Optional ByVal DeleteFlag As Boolean = True, Optional ByVal ShowFlag As Boolean = False)
        Dim objDataTable As New Data.DataTable
        Dim objDataTable1 As New Data.DataTable
        Dim objDataTable2 As New Data.DataTable
        Dim objDataTable3 As New Data.DataTable
        Dim objDataTable4 As New Data.DataTable

        Dim TotCol As Integer, Col As Integer, Row As Integer, TotCol1 As Integer, TotCol2 As Integer, TotCol3 As Integer, TotCol4 As Integer
        Dim objStreamReader As StreamReader
        Dim lsTxt As String
        Dim lsTextContent As String
        ' Dim lsnewHeaderTxt() As String
        Dim NumericCols() As String
        NumericCols = Split(StoreasNum, "|")



        'Dim headerText As String = ""
        'With dgrid
        '    For i = 0 To .ColumnCount - 1
        '        headerText &= dgrid.Columns(i).HeaderText() + "|"
        '    Next i

        'End With

        'lsnewHeaderTxt = Split(headerText, "|")
        Try

            objDataTable = dgrid.DataSource
            objDataTable1 = dgrid1.DataSource
            objDataTable2 = dgrid2.DataSource
            objDataTable3 = dgrid3.DataSource
            objDataTable4 = dgrid4.DataSource
            '  objDataTable5 = dgrid5.DataSource

            TotCol = objDataTable.Columns.Count
            TotCol1 = objDataTable1.Columns.Count
            TotCol2 = objDataTable2.Columns.Count
            TotCol3 = objDataTable3.Columns.Count
            TotCol4 = objDataTable4.Columns.Count

            If DeleteFlag = True Then
                If File.Exists(FileName) Then File.Delete(FileName)
            End If
            If File.Exists(FileName) = False Then

                FileOpen(1, FileName, OpenMode.Output)

                PrintLine(1, "<?xml version=""1.0"" encoding=""utf-8""?>")
                PrintLine(1, "<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:x=""urn:schemas-microsoft-com:office:excel"" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:html=""http://www.w3.org/TR/REC-html40"">")

                PrintLine(1, "<Styles>")
                PrintLine(1, "<Style ss:ID=""s1"">")
                PrintLine(1, "<Font x:Family=""Swiss"" ss:Bold=""1""/>")
                PrintLine(1, "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "<Style ss:ID=""s2"">")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "</Styles>")
            Else
                objStreamReader = File.OpenText(FileName)
                lsTextContent = objStreamReader.ReadToEnd()
                lsTextContent = Replace(lsTextContent.ToString, "</Workbook>", "")
                objStreamReader.Close()
                FileOpen(1, FileName, OpenMode.Output)
                PrintLine(1, lsTextContent.ToString)
            End If

            PrintLine(1, "<Worksheet ss:Name=""" & SheetName & """>")
            PrintLine(1, "<Table>")

            PrintLine(1, "<Row>")
            For Col = 0 To TotCol
                ' PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & objDataTable.Columns(Col - 1).Caption.ToString & "</Data></Cell>")
                PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & lsnewHeaderTxt(Col) & "</Data></Cell>")
            Next
            PrintLine(1, "</Row>")

            PrintLine(1, "<Row>")
            PrintLine(1, "<Cell ss:StyleID=""s1"" ><Data ss:Type=""String"">Table II - Statement showing shareholding pattern of the Promoter and Promoter Group</Data></Cell>")
            PrintLine(1, "</Row>")

            PrintLine(1, "<Row>")
            PrintLine(1, "<Cell ss:StyleID=""s1"" ><Data ss:Type=""String"" >Indian </Data></Cell>")
            PrintLine(1, "</Row>")

            For Row = 0 To objDataTable.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol
                    lsTxt = Replace(objDataTable.Rows(Row).Item(Col - 1).ToString, ">", " >")
                    lsTxt = Replace(lsTxt, "<", "< ")

                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & lsTxt & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & lsTxt & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            PrintLine(1, "<Row>")

            ' PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & objDataTable.Columns(Col - 1).Caption.ToString & "</Data></Cell>")
            PrintLine(1, "<Cell ss:StyleID=""s1"" ><Data ss:Type=""String"" >Foreign </Data></Cell>")

            PrintLine(1, "</Row>")
            For Row = 0 To objDataTable1.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol1
                    lsTxt = Replace(objDataTable1.Rows(Row).Item(Col - 1).ToString, ">", " >")
                    lsTxt = Replace(lsTxt, "<", "< ")

                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & lsTxt & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & lsTxt & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            PrintLine(1, "<Row>")

            PrintLine(1, "<Cell ss:StyleID=""s1"" ><Data ss:Type=""String"" > Table III - Statement showing shareholding pattern of the Public shareholder </Data></Cell>")

            PrintLine(1, "</Row>")

            PrintLine(1, "<Row>")
            PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String""> Institutions </Data></Cell>")
            PrintLine(1, "</Row>")

            For Row = 0 To objDataTable2.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol2
                    lsTxt = Replace(objDataTable2.Rows(Row).Item(Col - 1).ToString, ">", " >")
                    lsTxt = Replace(lsTxt, "<", "< ")

                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & lsTxt & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & lsTxt & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            PrintLine(1, "<Row>")

            PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">Non Institutions </Data></Cell>")
            PrintLine(1, "</Row>")

            For Row = 0 To objDataTable3.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol3
                    lsTxt = Replace(objDataTable3.Rows(Row).Item(Col - 1).ToString, ">", " >")
                    lsTxt = Replace(lsTxt, "<", "< ")

                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & lsTxt & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & lsTxt & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            PrintLine(1, "<Row>")

            PrintLine(1, "<Cell ss:StyleID=""s1"" ><Data ss:Type=""String"">Table IV-Statement showing shareholding pattern of the Non Promoter- Non Public shareholder </Data></Cell>")
            PrintLine(1, "</Row>")

            For Row = 0 To objDataTable4.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol4
                    lsTxt = Replace(objDataTable4.Rows(Row).Item(Col - 1).ToString, ">", " >")
                    lsTxt = Replace(lsTxt, "<", "< ")

                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & lsTxt & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & lsTxt & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next

            PrintLine(1, "</Table>")
            PrintLine(1, "</Worksheet>")
            PrintLine(1, "</Workbook>")

            FileClose(1)

            If ShowFlag = True Then
                Call ShellExecute(frmMain.Handle.ToInt32, "open", FileName, "", "0", SW_SHOWNORMAL)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub PrintExportExcel2(ByVal dgrid As DataGridView, ByVal FileName As String, Optional ByVal SheetName As String = "Report", Optional ByVal StoreasNum As String = "", Optional ByVal DeleteFlag As Boolean = True, Optional ByVal ShowFlag As Boolean = False)
        Dim objDataTable As New Data.DataTable


        Dim TotCol As Integer, Col As Integer, Row As Integer, TotCol1 As Integer, TotCol2 As Integer, TotCol3 As Integer, TotCol4 As Integer
        Dim objStreamReader As StreamReader
        Dim lsTxt As String
        Dim lsTextContent As String
        'Dim lsnewHeaderTxt() As String
        Dim NumericCols() As String
        NumericCols = Split(StoreasNum, "|")



        'Dim headerText As String = ""
        'With dgrid
        '    For i = 0 To .ColumnCount - 1
        '        headerText &= dgrid.Columns(i).HeaderText() + "|"
        '    Next i

        'End With

        'lsnewHeaderTxt = Split(headerText, "|")
        Try

            objDataTable = dgrid.DataSource

            '  objDataTable5 = dgrid5.DataSource

            TotCol = objDataTable.Columns.Count

            If DeleteFlag = True Then
                If File.Exists(FileName) Then File.Delete(FileName)
            End If
            If File.Exists(FileName) = False Then

                FileOpen(1, FileName, OpenMode.Output)

                PrintLine(1, "<?xml version=""1.0"" encoding=""utf-8""?>")
                PrintLine(1, "<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:x=""urn:schemas-microsoft-com:office:excel"" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:html=""http://www.w3.org/TR/REC-html40"">")

                PrintLine(1, "<Styles>")
                PrintLine(1, "<Style ss:ID=""s1"">")
                PrintLine(1, "<Font x:Family=""Swiss"" ss:Bold=""1""/>")
                PrintLine(1, "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "<Style ss:ID=""s2"">")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "</Styles>")
            Else
                objStreamReader = File.OpenText(FileName)
                lsTextContent = objStreamReader.ReadToEnd()
                lsTextContent = Replace(lsTextContent.ToString, "</Workbook>", "")
                objStreamReader.Close()
                FileOpen(1, FileName, OpenMode.Output)
                PrintLine(1, lsTextContent.ToString)
            End If

            PrintLine(1, "<Worksheet ss:Name=""" & SheetName & """>")
            PrintLine(1, "<Table>")

            PrintLine(1, "<Row>")
            For Col = 0 To TotCol
                ' PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & objDataTable.Columns(Col - 1).Caption.ToString & "</Data></Cell>")
                PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & lsnewHeaderTxt(Col) & "</Data></Cell>")
            Next
            PrintLine(1, "</Row>")

            For Row = 0 To objDataTable.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol
                    lsTxt = Replace(objDataTable.Rows(Row).Item(Col - 1).ToString, ">", " >")
                    lsTxt = Replace(lsTxt, "<", "< ")

                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & lsTxt & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & lsTxt & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next


            PrintLine(1, "</Table>")
            PrintLine(1, "</Worksheet>")
            PrintLine(1, "</Workbook>")

            FileClose(1)

            If ShowFlag = True Then
                Call ShellExecute(frmMain.Handle.ToInt32, "open", FileName, "", "0", SW_SHOWNORMAL)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub PrintExportExcel3(ByVal datatable As DataTable, ByVal FileName As String, Optional ByVal SheetName As String = "Report", Optional ByVal StoreasNum As String = "", Optional ByVal DeleteFlag As Boolean = True, Optional ByVal ShowFlag As Boolean = False)

        'Dim objDataTable As New Data.DataTable


        Dim TotCol As Integer, Col As Integer, Row As Integer, TotCol1 As Integer, TotCol2 As Integer, TotCol3 As Integer, TotCol4 As Integer
        Dim objStreamReader As StreamReader
        Dim lsTxt As String
        Dim lsTextContent As String
        Dim NumericCols() As String
        NumericCols = Split(StoreasNum, "|")






        'Dim headerText As String = ""
        'With dgrid
        '    For i = 0 To .ColumnCount - 1
        '        headerText &= dgrid.Columns(i).HeaderText() + "|"
        '    Next i

        'End With

        'lsnewHeaderTxt = Split(headerText, "|")
        Try

             

            TotCol = datatable.Columns.Count

            If DeleteFlag = True Then
                If File.Exists(FileName) Then File.Delete(FileName)
            End If
            If File.Exists(FileName) = False Then

                FileOpen(1, FileName, OpenMode.Output)

                PrintLine(1, "<?xml version=""1.0"" encoding=""utf-8""?>")
                PrintLine(1, "<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:x=""urn:schemas-microsoft-com:office:excel"" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:html=""http://www.w3.org/TR/REC-html40"">")

                PrintLine(1, "<Styles>")
                PrintLine(1, "<Style ss:ID=""s1"">")
                PrintLine(1, "<Font x:Family=""Swiss"" ss:Bold=""1""/>")
                PrintLine(1, "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "<Style ss:ID=""s2"">")
                PrintLine(1, "<Borders>")
                PrintLine(1, "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />")
                PrintLine(1, "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> ")
                PrintLine(1, "</Borders>")
                PrintLine(1, "</Style>")
                PrintLine(1, "</Styles>")
            Else
                objStreamReader = File.OpenText(FileName)
                lsTextContent = objStreamReader.ReadToEnd()
                lsTextContent = Replace(lsTextContent.ToString, "</Workbook>", "")
                objStreamReader.Close()
                FileOpen(1, FileName, OpenMode.Output)
                PrintLine(1, lsTextContent.ToString)
            End If

            PrintLine(1, "<Worksheet ss:Name=""" & SheetName & """>")
            PrintLine(1, "<Table>")

            PrintLine(1, "<Row>")
            For Col = 1 To TotCol
                PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & datatable.Columns(Col - 1).Caption.ToString & "</Data></Cell>")
                'PrintLine(1, "<Cell ss:StyleID=""s1""><Data ss:Type=""String"">" & lsnewHeaderTxt(Col) & "</Data></Cell>")
            Next
            PrintLine(1, "</Row>")

            For Row = 0 To datatable.Rows.Count - 1
                PrintLine(1, "<Row>")
                For Col = 1 To TotCol
                    lsTxt = Replace(datatable.Rows(Row).Item(Col - 1).ToString, ">", " >")
                    lsTxt = Replace(lsTxt, "<", "< ")

                    If IsNumericFldCol(Col, NumericCols) = False Then
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""String"" x:Ticked=""1"">" & lsTxt & "</Data></Cell>")
                    Else
                        PrintLine(1, "<Cell ss:StyleID=""s2""><Data ss:Type=""Number"">" & lsTxt & "</Data></Cell>")
                    End If
                Next
                PrintLine(1, "</Row>")
            Next


            PrintLine(1, "</Table>")
            PrintLine(1, "</Worksheet>")
            PrintLine(1, "</Workbook>")

            FileClose(1)

            If ShowFlag = True Then
                Call ShellExecute(frmMain.Handle.ToInt32, "open", FileName, "", "0", SW_SHOWNORMAL)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



End Class