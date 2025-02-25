Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Public Class frm_dmat_remat_conf
    Private doc As Document
    Dim lsSql As String
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As System.Data.DataTable

    Private Sub frm_dmat_remat_conf_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql, Mysql, My_sql As String

        Sql = ""
        Sql &= " select comp_gid,comp_name from sta_mst_tcompany "
        Sql &= " where delete_flag = 'N' "
        Sql &= " order by comp_name asc "

        Call gpBindCombo(Sql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        Mysql = ""
        Mysql &= " SELECT depository_code,depository_name FROM sta_mst_tdepository"
        Mysql &= " where delete_flag = 'N' "
        Mysql &= " order by depository_name "

        'Call gpBindCombo(Mysql, "depository_code", "depository_name", cbdepotype, gOdbcConn)

        Using cmd As MySqlCommand = New MySqlCommand(Mysql, gOdbcConn)
            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As System.Data.DataTable = New System.Data.DataTable
            dt.Load(rs)

            cbdepotype.ValueMember = "depository_code"
            cbdepotype.DisplayMember = "depository_name"
            cbdepotype.DataSource = dt
        End Using

        My_sql = ""
        My_sql &= " SELECT trantype_code,trantype_desc FROM sta_mst_ttrantype where trantype_code in ('DM','RM')"
        My_sql &= " and delete_flag = 'N' "

        Call gpBindCombo(My_sql, "trantype_desc", "trantype_code", cbotype, gOdbcConn)

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim lsCond As String = ""


        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbdepotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and e.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If
        If cbotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.tran_code = '" & cbotype.SelectedValue.ToString & "' "
        End If

        If dtp_datefrom.Checked = True Then lsCond &= " and a.approved_date between  '" & Format(dtp_datefrom.Value, "yyyy-MM-dd") & "' "
        If dtp_dateto.Checked = True Then lsCond &= " and  '" & Format(dtp_dateto.Value, "yyyy-MM-dd") & "' "


        lsSql = ""
        lsSql &= " SELECT a.folio_no as Folio,"
        lsSql &= " e.drn_no as Drn_no,"
        lsSql &= " a.inward_comp_no as Gnsa_inward_no,d.dist_from as DistNo_From,d.dist_to as DistNo_To,"
        lsSql &= " c.cert_no as Certificate_no,"
        lsSql &= " d.dist_count as shares,"
        lsSql &= " DATE_FORMAT(a.approved_date, '%d/%m/%Y') as Approved_date"
        lsSql &= " FROM sta_trn_tinward a"
        lsSql &= " inner join sta_trn_tcertentry b on a.inward_gid=b.inward_gid and b.delete_flag='N'"
        lsSql &= " inner join sta_trn_tcert c on b.cert_gid = c.cert_gid and c.delete_flag='N'"
        lsSql &= " inner join sta_trn_tcertdist d on d.cert_gid = b.cert_gid and d.delete_flag='N'"
        lsSql &= " inner join sta_trn_tdematpend e on e.inward_gid = a.inward_gid and e.delete_flag='N' and e.dematpend_flag in ( 'D','S')"
        lsSql &= " where true"
        lsSql &= lsCond
        lsSql &= " and a.dematpend_reject_code = '' and a.approved_date != '' and a.delete_flag='N'"

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New System.Data.DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        If dt.Rows.Count <= 0 Then
            MessageBox.Show("No Records Found")
            dgvList.DataSource = Nothing
        Else

            With dgvList
                .DataSource = dt
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i

                txtTotRec.Text = "Total Records : " & .RowCount.ToString
            End With
        End If

    End Sub

    Private Sub frmUploadSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        pnlSearch.Top = 6
        pnlSearch.Left = 6

        With dgvList
            .Top = pnlSearch.Top + pnlSearch.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlSearch.Top + pnlSearch.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvList.Top + dgvList.Height + 6
        pnlExport.Left = dgvList.Left
        pnlExport.Width = dgvList.Width
        btnExport.Left = Math.Abs(pnlExport.Width - btnExport.Width)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        'Try
        '    PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Dim ls_txt As String
        Dim dtxt As System.Data.DataTable
        Dim ds As New DataSet
        Dim lsCond As String = ""
        Dim lsCond1 As String = ""
        Dim lsCond2 As String = ""
        Dim lsCond3 As String = ""
        Dim lsCond4 As String = ""
        Dim comp_txt As String = ""
        Dim dep_txt As String = ""
        Dim sum_txt As String = ""

        Dim lnLineNo As Long = 0
        Dim lsTxt As String


        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        Else
            MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboCompany.Focus()
            Exit Sub
        End If

        If cbdepotype.Text <> "" And cbdepotype.SelectedIndex <> -1 Then
            lsCond &= " and e.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
        Else
            MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cbdepotype.Focus()
            Exit Sub
        End If

        If dtp_datefrom.Checked = True Then lsCond &= " and a.approved_date between  '" & Format(dtp_datefrom.Value, "yyyy-MM-dd") & "' "
        If dtp_dateto.Checked = True Then lsCond &= " and  '" & Format(dtp_dateto.Value, "yyyy-MM-dd") & "' "
        '''''''''''''''''''''''''''''
        If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond1 &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
        End If

        If cbdepotype.Text <> "" And cbdepotype.SelectedIndex <> -1 Then
            lsCond2 &= " and e.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "

        End If

        If cbotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
            lsCond &= " and a.tran_code = '" & cbotype.SelectedValue.ToString & "' "
        End If
        '''''''''''''''''''''''''''''''''''''''
        lsCond3 = Format(dtp_datefrom.Value, "yyyy-MM-dd")
        ''''''''''''''''''''''''''''''''''''''''''''''
        lsCond4 = Format(dtp_dateto.Value, "yyyy-MM-dd")
        ''''''''''''''''''''''''''''''''''''''''''''''
        ls_txt = ""
        ls_txt &= "SELECT comp_name FROM sta_mst_tcompany a WHERE true " & lsCond1 & " "
        cmd = New MySqlCommand(ls_txt, gOdbcConn)
        dtxt = New System.Data.DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dtxt)
        If dtxt.Rows.Count > 0 Then
            If Not IsDBNull(dtxt.Rows(0)(0).ToString) Then
                comp_txt = dtxt.Rows(0)(0).ToString
            End If
        End If
        '''''''''''''''''''''
        ls_txt = ""
        ls_txt &= "  SELECT depository_name FROM sta_mst_tdepository e where true " & lsCond2 & " "
        cmd = New MySqlCommand(ls_txt, gOdbcConn)
        dtxt = New System.Data.DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dtxt)
        If dtxt.Rows.Count > 0 Then
            If Not IsDBNull(dtxt.Rows(0)(0).ToString) Then
                dep_txt = dtxt.Rows(0)(0).ToString
            End If
        End If
        '''''''''''''''''''''''''
        ls_txt = ""
        ls_txt &= " SELECT sum(d.dist_count)as shares"
        ls_txt &= " FROM sta_trn_tinward a"
        ls_txt &= " inner join sta_trn_tcertentry b on a.inward_gid=b.inward_gid and b.delete_flag='N'"
        ls_txt &= " inner join sta_trn_tcert c on b.cert_gid = c.cert_gid and c.delete_flag='N'"
        ls_txt &= " inner join sta_trn_tcertdist d on d.cert_gid = b.cert_gid and d.delete_flag='N'"
        ls_txt &= " inner join sta_trn_tdematpend e on e.inward_gid = a.inward_gid and e.delete_flag='N' and e.dematpend_flag in ( 'D','S')"
        ls_txt &= " where true"
        ls_txt &= lsCond
        ls_txt &= " and dematpend_reject_code = '' and a.approved_date != '' and a.delete_flag='N'"
        cmd = New MySqlCommand(ls_txt, gOdbcConn)
        dtxt = New System.Data.DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dtxt)
        If dtxt.Rows.Count > 0 Then
            If Not IsDBNull(dtxt.Rows(0)(0).ToString) Then
                sum_txt = dtxt.Rows(0)(0).ToString
            End If
        End If
        '''''''''''''''''''''''''''''

        lsSql = ""
        lsSql &= " SELECT a.folio_no as Folio,"
        lsSql &= " e.drn_no as Drn_no,"
        lsSql &= " a.inward_no as Gnsa_inward_no,d.dist_from as DistNo_From,d.dist_to as DistNo_To,"
        lsSql &= " c.cert_no as Certificate_no,"
        lsSql &= " d.dist_count as shares,"
        lsSql &= " DATE_FORMAT(a.approved_date, '%d/%m/%Y') as Approved_date"
        lsSql &= " FROM sta_trn_tinward a"
        lsSql &= " inner join sta_trn_tcertentry b on a.inward_gid=b.inward_gid and b.delete_flag='N'"
        lsSql &= " inner join sta_trn_tcert c on b.cert_gid = c.cert_gid and c.delete_flag='N'"
        lsSql &= " inner join sta_trn_tcertdist d on d.cert_gid = b.cert_gid and d.delete_flag='N'"
        lsSql &= " inner join sta_trn_tdematpend e on e.inward_gid = a.inward_gid and e.delete_flag='N' and e.dematpend_flag in ( 'D','S')"
        lsSql &= " where true"
        lsSql &= lsCond
        lsSql &= " and dematpend_reject_code = '' and a.approved_date != '' and a.delete_flag='N'"

        cmd = New MySqlCommand(lsSql, gOdbcConn)
        dt = New System.Data.DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        Dim buf As String = Space(3)
        Dim fileloc As String = "C:\covering\1.txt"
        Dim txt As String = String.Empty
        txt += vbCr & vbLf
        For Each row As DataRow In dt.Rows
            For Each column As DataColumn In dt.Columns
                'Add the Data rows.
                'txt += row(column.ColumnName).ToString() & " | " & vbTab
                txt += row(column.ColumnName).ToString() & "  " & vbTab
            Next
            'Add new line.
            txt += vbCr & vbLf
        Next
        Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)

        If File.Exists(fileloc) Then

            Using sw As StreamWriter = New StreamWriter(fileloc)
                sw.WriteLine("Unit : " & comp_txt & "   -    DEMAT   " & vbTab & vbTab & vbTab & vbTab & "Date : " & todaysdate)
                sw.WriteLine(" ")
                sw.WriteLine("LIST OF CASES CONFIRMED TO " & dep_txt & "  FROM  " & lsCond3 & "  TO  " & lsCond4 & "            ")
                sw.WriteLine("===================================================================================================================================== ")
                sw.WriteLine(" Folio No " & vbTab & " Drnno " & vbTab & vbTab & buf & " Gnsainwdno " & vbTab & buf & " Dist From " & vbTab & buf & " Dist To " & buf & buf & "Cert no " & buf & " Shares " & buf & " Approved_date ")
                sw.WriteLine("===================================================================================================================================== ")
                For Each row As DataRow In dt.Rows
                    sw.WriteLine(row("Folio").ToString() & vbTab & row("Drn_no").ToString() & vbTab & vbTab & row("Gnsa_inward_no").ToString() & vbTab & buf & row("DistNo_From").ToString() & vbTab & buf & row("DistNo_To").ToString() & vbTab & buf & row("Certificate_no").ToString() & buf & vbTab & row("shares").ToString() & vbTab & buf & row("Approved_date").ToString())
                Next
                sw.WriteLine("===================================================================================================================================== ")
                sw.WriteLine(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & " Total : " & sum_txt)
                sw.WriteLine("===================================================================================================================================== ")
            End Using
        End If

        If System.IO.File.Exists(fileloc) = True Then
            Process.Start(fileloc)
        Else
            MsgBox("File Does Not Exist")
        End If

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

    Private Sub btnpdf_Click(sender As Object, e As EventArgs) Handles btnpdf.Click
        Dim ls_txt As String
        Dim dtxt As System.Data.DataTable
        Dim ds As New DataSet
        Dim lsCond As String = ""
        Dim lsCond1 As String = ""
        Dim lsCond2 As String = ""
        Dim lsCond3 As String = ""
        Dim lsCond4 As String = ""
        Dim comp_txt As String = ""
        Dim dep_txt As String = ""
        Dim sum_txt As String = ""
        Dim space As String = "    "

        Dim lnLineNo As Long = 0
        Dim lsTxt As String

        Try
            If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
                lsCond &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            Else
                MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboCompany.Focus()
                Exit Sub
            End If

            If cbdepotype.Text <> "" And cbdepotype.SelectedIndex <> -1 Then
                lsCond &= " and e.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "
            Else
                MessageBox.Show("Please select the Depository Type !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cbdepotype.Focus()
                Exit Sub
            End If

            If dtp_datefrom.Checked = True Then lsCond &= " and a.approved_date between  '" & Format(dtp_datefrom.Value, "yyyy-MM-dd") & "' "
            If dtp_dateto.Checked = True Then lsCond &= " and  '" & Format(dtp_dateto.Value, "yyyy-MM-dd") & "' "
            '''''''''''''''''''''''''''''
            If cboCompany.Text <> "" And cboCompany.SelectedIndex <> -1 Then
                lsCond1 &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString) & " "
            End If

            If cbdepotype.Text <> "" And cbdepotype.SelectedIndex <> -1 Then
                lsCond2 &= " and e.depository_code = '" & cbdepotype.SelectedValue.ToString & "' "

            End If

            If cbotype.Text <> "" And cboCompany.SelectedIndex <> -1 Then
                lsCond &= " and a.tran_code = '" & cbotype.SelectedValue.ToString & "' "
            End If
            '''''''''''''''''''''''''''''''''''''''
            lsCond3 = Format(dtp_datefrom.Value, "yyyy-MM-dd")
            ''''''''''''''''''''''''''''''''''''''''''''''
            lsCond4 = Format(dtp_dateto.Value, "yyyy-MM-dd")
            ''''''''''''''''''''''''''''''''''''''''''''''
            ls_txt = ""
            ls_txt &= "SELECT comp_name FROM sta_mst_tcompany a WHERE true " & lsCond1 & " "
            cmd = New MySqlCommand(ls_txt, gOdbcConn)
            dtxt = New System.Data.DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtxt)
            If dtxt.Rows.Count > 0 Then
                If Not IsDBNull(dtxt.Rows(0)(0).ToString) Then
                    comp_txt = dtxt.Rows(0)(0).ToString
                End If
            End If
            '''''''''''''''''''''
            ls_txt = ""
            ls_txt &= "  SELECT depository_name FROM sta_mst_tdepository e where true " & lsCond2 & " "
            cmd = New MySqlCommand(ls_txt, gOdbcConn)
            dtxt = New System.Data.DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtxt)
            If dtxt.Rows.Count > 0 Then
                If Not IsDBNull(dtxt.Rows(0)(0).ToString) Then
                    dep_txt = dtxt.Rows(0)(0).ToString
                End If
            End If
            '''''''''''''''''''''''''
            ls_txt = ""
            ls_txt &= " SELECT sum(d.dist_count)as shares"
            ls_txt &= " FROM sta_trn_tinward a"
            ls_txt &= " inner join sta_trn_tcertentry b on a.inward_gid=b.inward_gid and b.delete_flag='N'"
            ls_txt &= " inner join sta_trn_tcert c on b.cert_gid = c.cert_gid and c.delete_flag='N'"
            ls_txt &= " inner join sta_trn_tcertdist d on d.cert_gid = b.cert_gid and d.delete_flag='N'"
            ls_txt &= " inner join sta_trn_tdematpend e on e.inward_gid = a.inward_gid and e.delete_flag='N' and e.dematpend_flag in ( 'D','S')"
            ls_txt &= " where true"
            ls_txt &= lsCond
            ls_txt &= " and dematpend_reject_code = '' and a.approved_date != '' and a.delete_flag='N'"
            cmd = New MySqlCommand(ls_txt, gOdbcConn)
            dtxt = New System.Data.DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dtxt)
            If dtxt.Rows.Count > 0 Then
                If Not IsDBNull(dtxt.Rows(0)(0).ToString) Then
                    sum_txt = dtxt.Rows(0)(0).ToString
                End If
            End If
            '''''''''''''''''''''''''''''

            lsSql = ""
            lsSql &= " SELECT a.folio_no as Folio,"
            lsSql &= " e.drn_no as Drn_no,"
            lsSql &= " a.inward_no as Gnsa_inward_no,d.dist_from as DistNo_From,d.dist_to as DistNo_To,"
            lsSql &= " c.cert_no as Certificate_no,"
            lsSql &= " d.dist_count as shares,"
            lsSql &= " DATE_FORMAT(a.approved_date, '%d/%m/%Y') as Approved_date"
            lsSql &= " FROM sta_trn_tinward a"
            lsSql &= " inner join sta_trn_tcertentry b on a.inward_gid=b.inward_gid and b.delete_flag='N'"
            lsSql &= " inner join sta_trn_tcert c on b.cert_gid = c.cert_gid and c.delete_flag='N'"
            lsSql &= " inner join sta_trn_tcertdist d on d.cert_gid = b.cert_gid and d.delete_flag='N'"
            lsSql &= " inner join sta_trn_tdematpend e on e.inward_gid = a.inward_gid and e.delete_flag='N' and e.dematpend_flag in ( 'D','S')"
            lsSql &= " where true"
            lsSql &= lsCond
            lsSql &= " and dematpend_reject_code = '' and a.approved_date != '' and a.delete_flag='N'"

            cmd = New MySqlCommand(lsSql, gOdbcConn)
            dt = New System.Data.DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            'Dim buf As String = Space(3)
            Dim buf As String = vbTab
            doc = New Document()
            Dim lsPdfFile As String = "C:\covering\1.pdf"

            If File.Exists(lsPdfFile) Then File.Delete(lsPdfFile)

            PdfWriter.GetInstance(doc, New FileStream(lsPdfFile, FileMode.Create))
            doc.Open()

            If gOdbcConn.State = ConnectionState.Closed Then gOdbcConn.Open()

            Dim fileloc As String = "C:\covering\1.txt"


            Dim txt As String = String.Empty
            txt += vbCr & vbLf
            For Each row As DataRow In dt.Rows
                For Each column As DataColumn In dt.Columns
                    'Add the Data rows.
                    'txt += row(column.ColumnName).ToString() & " | " & vbTab
                    txt += row(column.ColumnName).ToString() & "  " & space
                Next
                'Add new line.
                txt += vbCr & vbLf
            Next
            Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)

            If File.Exists(fileloc) Then
                Using sw As StreamWriter = New StreamWriter(fileloc)
                    sw.WriteLine("Unit : " & comp_txt & "   -    DEMAT   " & space & space & space & space & "Date : " & todaysdate)
                    sw.WriteLine(" ")
                    sw.WriteLine("LIST OF CASES CONFIRMED TO " & dep_txt & "  FROM  " & lsCond3 & "  TO  " & lsCond4 & "            ")
                    sw.WriteLine("============================================================================================================ ")
                    sw.WriteLine(" Folio No " & space & " Drnno " & space & " Gnsainwdno " & space & " Dist From " & space & buf & " Dist To " & space & buf & "Cert no " & space & buf & " Shares " & space & buf & " Approved_date ")
                    sw.WriteLine("============================================================================================================ ")
                    If cbdepotype.Text = "CDSL" Then
                        For Each row As DataRow In dt.Rows
                            sw.WriteLine(row("Folio").ToString() & space & space & row("Drn_no").ToString() & space & row("Gnsa_inward_no").ToString() & space & space & row("DistNo_From").ToString().PadRight(10, " ") & space & row("DistNo_To").ToString().PadRight(10, " ") & space & row("Certificate_no").ToString().PadRight(8, " ") & space & row("shares").ToString().PadRight(8, " ") & space & row("Approved_date").ToString())
                        Next
                    End If
                    If cbdepotype.Text = "NSDL" Then
                        For Each row As DataRow In dt.Rows
                            sw.WriteLine(row("Folio").ToString() & space & row("Drn_no").ToString() & space & row("Gnsa_inward_no").ToString() & space & space & row("DistNo_From").ToString().PadRight(10, " ") & space & row("DistNo_To").ToString().PadRight(10, " ") & space & row("Certificate_no").ToString().PadRight(8, " ") & space & row("shares").ToString().PadRight(8, " ") & space & row("Approved_date").ToString())
                        Next
                    End If
                   
                    sw.WriteLine("============================================================================================================ ")
                    If cbdepotype.Text = "NSDL" Then
                        sw.WriteLine(space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & " Total : " & sum_txt)
                    End If
                    If cbdepotype.Text = "CDSL" Then
                        sw.WriteLine(space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & space & " Total : " & sum_txt)
                    End If
                    sw.WriteLine("============================================================================================================ ")
                End Using

            End If

            Call AppendPdf(doc, fileloc, 8)

            doc.Close()

            System.Diagnostics.Process.Start(lsPdfFile)
        Catch ex As Exception
            MsgBox("The process failed:" & ex.Message)
        End Try

    End Sub

    Private Sub AppendPdf(doc As Document, TextFileName As String, FontSize As Integer)

        If File.Exists(TextFileName) Then
            Dim rdr As New StreamReader(TextFileName)
            Dim lobjBaseFont As BaseFont
            lobjBaseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED)
            Dim lobjFont As New iTextSharp.text.Font(lobjBaseFont, FontSize, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

            doc.NewPage()
            doc.Add(New Paragraph(rdr.ReadToEnd(), lobjFont))

            rdr.Close()
        End If
    End Sub

    Private Sub AppendParagraphPdf(doc As Microsoft.Office.Interop.Word.Document, TextFileName As String)
        If File.Exists(TextFileName) Then
            Dim rdr As New StreamReader(TextFileName)

            Dim lobjBaseFont As BaseFont
            lobjBaseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED)
            Dim lobjFont As New iTextSharp.text.Font(lobjBaseFont, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

            doc.Add(New iTextSharp.text.Paragraph(rdr.ReadToEnd(), lobjFont))

            rdr.Close()
        End If
    End Sub

End Class