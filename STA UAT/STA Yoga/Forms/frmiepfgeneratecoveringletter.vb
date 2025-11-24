Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Diagnostics

Public Class frmiepfgeneratecoveringletter
    'Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=146.56.55.230;DataBase=sta;uid=root;pwd=Flexi@123;port=3306")
    Dim ds As DataSet
    Dim da As Odbc.OdbcDataAdapter
    Dim cmd As New Odbc.OdbcCommand
    Dim sql As String
    Private Sub frmiepfgeneratecoveringletter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iepf_covering_list()
        Call frmCtrClear(Me)
        Load_list("", "", "")
    End Sub

    Private Sub iepf_covering_list()
        Dim csSql As String
        'con.Open()

        csSql = ""
        csSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        csSql &= " where delete_flag = 'N' "
        csSql &= " order by comp_name asc "

        Using da As New MySqlDataAdapter(csSql, gOdbcConn)
            Dim ds As New DataSet()
            da.Fill(ds, "table")

            With cb_cmpy
                .DataSource = ds.Tables(0)
                .ValueMember = "comp_gid"
                .DisplayMember = "comp_name"
                .SelectedIndex = -1 ' optional: nothing selected by default
            End With
        End Using



        'con.Close()
    End Sub

    Private Sub dgv_covering_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_covering.CellContentClick
        Try
            If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

            Dim clickedColumnName As String = dgv_covering.Columns(e.ColumnIndex).Name
            Dim row As DataGridViewRow = dgv_covering.Rows(e.RowIndex)

            ' Common values from the row
            'Dim inwardCompNo As String = row.Cells("Inward Comp No").Value.ToString()
            Dim inwardCompNo As String = row.Cells("Comp Inward No").Value.ToString()
            Dim folioNo As String = row.Cells("Folio No").Value.ToString()
            Dim foliogid As Integer = row.Cells("folio_gid").Value.ToString()
            Dim inwardgid As Integer = row.Cells("inward_gid").Value.ToString()
            Dim compName As String = row.Cells("Company Name").Value.ToString()
            Dim shareHolderName As String = row.Cells("ShareHolder Name").Value.ToString()
            Dim inwardNo As String = row.Cells("Inward No").Value.ToString()
            Dim holder2name As String = row.Cells("holder2_name").Value.ToString()
            Dim holdern3ame As String = row.Cells("holder3_name").Value.ToString()
            Dim nomineename As String = row.Cells("nominee_name").Value.ToString()
            Dim sharevalue As String = row.Cells("share_value").Value.ToString()
            Dim shareprice As String = row.Cells("share_price").Value.ToString()
            Dim CompGid As Integer = row.Cells("comp_gid").Value
            Dim sharecount As Decimal = row.Cells("share_count").Value
            Dim ReqType As String = row.Cells("Request Type").Value
            Dim ClaimantName As String = row.Cells("req_claimant_name").Value
            Dim ClaimantAddr As String = row.Cells("claimant_addr").Value
            Dim ClaimantEmail As String = row.Cells("claimant_email").Value
            Dim HolderEmail As String = row.Cells("holder_email").Value
            Dim HolderAddr As String = row.Cells("holder_addr").Value
            Dim ShareAmount As Decimal = row.Cells("share_amount").Value
            Dim dpclientid As String = row.Cells("dp_client_id").Value
            Dim nomineeaddr As String = row.Cells("nominee_addr").Value

            Select Case clickedColumnName
                Case "GenerateLetter"
                    Dim status As String = row.Cells("Status").Value.ToString()
                    Dim req_type As String = row.Cells("Request Type").Value.ToString()
                    Dim reqgid As Integer = row.Cells("req_gid").Value()

                    Dim obj As New frmiepflettergenerate(reqgid, inwardNo, status, inwardgid, CompGid, sharecount, ReqType, ClaimantName,
                                                      shareHolderName, folioNo, inwardCompNo, compName, ClaimantAddr, ClaimantEmail,
                                                         HolderAddr, HolderEmail, ShareAmount, dpclientid, nomineename, nomineeaddr, foliogid)
                    obj.Show()

                Case "TransactionView"
                    Dim status As String = row.Cells("Status").Value.ToString()
                    Dim req_type As String = row.Cells("Request Type").Value.ToString()
                    Dim reqgid As Integer = row.Cells("req_gid").Value()

                    'Dim frm As New frmiepfmaker(folioNo, compName, shareHolderName, holder2name, holdern3ame, 0, "C", "Maker", nomineename, CompGid,
                    '                            inwardNo, shareprice, status, req_type, reqgid)

                    Dim frm As New frmiepfmaker(inwardgid, "C")
                    frm.HideButtonsForViewMode = True
                    frm.Show()

                Case "Dividend"

                    Dim obj As New frmDividend(foliogid, CompGid)
                    obj.Show()

                Case "SRNUpdate"
                    Dim srnNo As String = ""
                    Dim srnDate As Date? = Nothing

                    Using cmd As New MySqlCommand("pr_get_iepfclaimsrn", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("in_inward_gid", inwardgid)
                        cmd.Parameters.AddWithValue("in_folio_gid", foliogid)

                        Using rdr As MySqlDataReader = cmd.ExecuteReader()
                            If rdr.Read() Then
                                srnNo = If(rdr.IsDBNull(rdr.GetOrdinal("req_srn_no")), "", rdr("req_srn_no").ToString())
                                If Not rdr.IsDBNull(rdr.GetOrdinal("req_srn_date")) Then
                                    srnDate = Convert.ToDateTime(rdr("req_srn_date"))
                                End If
                            End If
                        End Using
                    End Using

                    Dim popup As New frmSRNpopup()
                    popup.InwardCompNo = inwardCompNo
                    popup.FolioGid = foliogid
                    popup.SRNNo = srnNo
                    popup.iepfsrndate = srnDate
                    popup.InwardGid = inwardgid

                    If popup.ShowDialog() = DialogResult.OK Then
                        MessageBox.Show(popup.UpdateResultMessage)
                        btn_search_Click(Nothing, EventArgs.Empty)
                    End If

            End Select

        Catch ex As Exception
            MessageBox.Show("Error processing button click: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        Try
            Dim folio_no As String = txt_folio.Text.Trim()
            Dim comp_name As String = cb_cmpy.Text.Trim()
            Dim inward_no As String = txt_inward.Text.Trim()

            'If Integer.TryParse(txt_inward.Text.Trim(), inward_no) = False Then
            '    inward_no = 0
            'End If

            Load_list(inward_no, folio_no, comp_name)
            'Using cmd As New MySqlCommand("pr_get_iepfcoveringletterlist", gOdbcConn)
            '    cmd.CommandType = CommandType.StoredProcedure
            '    cmd.Parameters.AddWithValue("in_inward_comp_no", inward_no)
            '    cmd.Parameters.AddWithValue("in_folio_no", folio_no)
            '    cmd.Parameters.AddWithValue("in_comp_name", comp_name)

            '    Using da As New MySqlDataAdapter(cmd)
            '        Dim dt As New DataTable()
            '        da.Fill(dt)

            '        If dt.Rows.Count = 0 Then
            '            MessageBox.Show("No records found", "STA")
            '            dgv_covering.DataSource = Nothing
            '            dgv_covering.Rows.Clear()
            '            dgv_covering.Columns.Clear()
            '            Exit Sub
            '        End If

            '        dgv_covering.Columns.Clear()
            '        dgv_covering.AutoGenerateColumns = True
            '        dgv_covering.DataSource = dt
            '        dgv_covering.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            '        dgv_covering.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            '        dgv_covering.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            '        dgv_covering.Columns("req_gid").Visible = False
            '        dgv_covering.Columns("holder2_name").Visible = False
            '        dgv_covering.Columns("holder3_name").Visible = False
            '        dgv_covering.Columns("nominee_name").Visible = False
            '        dgv_covering.Columns("share_value").Visible = False
            '        dgv_covering.Columns("share_price").Visible = False
            '        dgv_covering.Columns("inward_gid").Visible = False
            '        dgv_covering.Columns("comp_gid").Visible = False
            '        dgv_covering.Columns("share_count").Visible = False
            '        dgv_covering.Columns("req_claimant_name").Visible = False
            '        dgv_covering.Columns("claimant_addr").Visible = False
            '        dgv_covering.Columns("claimant_email").Visible = False
            '        dgv_covering.Columns("holder_addr").Visible = False
            '        dgv_covering.Columns("holder_email").Visible = False
            '        dgv_covering.Columns("share_amount").Visible = False
            '        dgv_covering.Columns("dp_client_id").Visible = False

            '        'dgv_covering.Columns("Inward Comp No").Width = 80
            '        dgv_covering.Columns("Comp Inward No").Width = 100
            '        dgv_covering.Columns("Folio No").Width = 70
            '        dgv_covering.Columns("Inward No").Width = 70
            '        dgv_covering.Columns("Request Type").Width = 80
            '        dgv_covering.Columns("Company Name").Width = 250
            '        dgv_covering.Columns("ShareHolder Name").Width = 170
            '        dgv_covering.Columns("SRN No").Width = 80
            '        dgv_covering.Columns("SRN Date").Width = 80
            '        dgv_covering.Columns("Status").Width = 70

            '        If Not dgv_covering.Columns.Contains("GenerateLetter") Then
            '            Dim btnCol As New DataGridViewButtonColumn()
            '            btnCol.Name = "GenerateLetter"
            '            btnCol.HeaderText = "Generate Letter"
            '            btnCol.Text = "Letter Generate"
            '            btnCol.UseColumnTextForButtonValue = True
            '            btnCol.Width = 110
            '            dgv_covering.Columns.Add(btnCol)
            '        End If

            '        If Not dgv_covering.Columns.Contains("Dividend") Then
            '            Dim btnCol As New DataGridViewButtonColumn()
            '            btnCol.Name = "Dividend"
            '            btnCol.HeaderText = "Dividend"
            '            btnCol.Text = "View"
            '            btnCol.UseColumnTextForButtonValue = True
            '            btnCol.Width = 80
            '            dgv_covering.Columns.Add(btnCol)
            '        End If

            '        If Not dgv_covering.Columns.Contains("SRNUpdate") Then
            '            Dim btnCol As New DataGridViewButtonColumn()
            '            btnCol.Name = "SRNUpdate"
            '            btnCol.HeaderText = "SRN No"
            '            btnCol.Text = "Update"
            '            btnCol.UseColumnTextForButtonValue = True
            '            btnCol.Width = 80
            '            dgv_covering.Columns.Add(btnCol)
            '        End If

            '        If Not dgv_covering.Columns.Contains("TransactionView") Then
            '            Dim btnCol As New DataGridViewButtonColumn()
            '            btnCol.Name = "TransactionView"
            '            btnCol.HeaderText = "Transaction View"
            '            btnCol.Text = "View"
            '            btnCol.UseColumnTextForButtonValue = True
            '            btnCol.Width = 80
            '            dgv_covering.Columns.Add(btnCol)
            '        End If

            '    End Using
            'End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching details: " & ex.Message)
        End Try
    End Sub

    Private Sub Load_list(inwardno As String, foliono As String, compname As String)
        Try
            'Dim folio_no As String = txt_folio.Text.Trim()
            'Dim comp_name As String = cb_cmpy.Text.Trim()
            'Dim inward_no As Integer
            'txt_folio.Text = ""
            'cb_cmpy.Text = ""
            'txt_inward.Text = ""


            'If Integer.TryParse(txt_inward.Text.Trim(), inwardno) = False Then
            '    inwardno = 0
            'End If

            Using cmd As New MySqlCommand("pr_get_iepfcoveringletterlist", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_inward_comp_no", inwardno)
                cmd.Parameters.AddWithValue("in_folio_no", foliono)
                cmd.Parameters.AddWithValue("in_comp_name", compname)

                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    If dt.Rows.Count = 0 Then
                        dgv_covering.DataSource = Nothing
                        dgv_covering.Rows.Clear()
                        dgv_covering.Columns.Clear()
                        Exit Sub
                    End If

                    dgv_covering.Columns.Clear()
                    dgv_covering.AutoGenerateColumns = True
                    dgv_covering.DataSource = dt
                    'dgv_covering.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    'dgv_covering.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

                    'Dim sameFont As New Font("Tahoma", 8.25, FontStyle.Bold)
                    'dgv_covering.ColumnHeadersDefaultCellStyle.Font = sameFont
                    'dgv_covering.DefaultCellStyle.Font = sameFont

                    dgv_covering.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

                    ' Hide unwanted columns
                    Dim colsToHide As String() = {"req_gid", "holder2_name", "holder3_name", "nominee_name",
                        "share_value", "share_price", "inward_gid", "comp_gid", "share_count",
                        "req_claimant_name", "claimant_addr", "claimant_email",
                        "holder_addr", "holder_email", "share_amount", "dp_client_id", "folio_gid", "nominee_addr", "inward_gid1"}

                    For Each col As String In colsToHide
                        If dgv_covering.Columns.Contains(col) Then
                            dgv_covering.Columns(col).Visible = False
                        End If
                    Next

                    ' Resize visible columns
                    dgv_covering.Columns("Comp Inward No").Width = 100
                    dgv_covering.Columns("Folio No").Width = 70
                    dgv_covering.Columns("Inward No").Width = 70
                    dgv_covering.Columns("Request Type").Width = 80
                    dgv_covering.Columns("Company Name").Width = 250
                    dgv_covering.Columns("ShareHolder Name").Width = 170
                    dgv_covering.Columns("SRN No").Width = 80
                    dgv_covering.Columns("SRN Date").Width = 80
                    dgv_covering.Columns("Status").Width = 70

                    ' Add button columns if not exists
                    If Not dgv_covering.Columns.Contains("GenerateLetter") Then
                        Dim btnCol As New DataGridViewButtonColumn()
                        btnCol.Name = "GenerateLetter"
                        btnCol.HeaderText = "Covering Letter"
                        btnCol.Text = "Generate"
                        btnCol.UseColumnTextForButtonValue = True
                        btnCol.Width = 110
                        dgv_covering.Columns.Add(btnCol)
                    End If

                    If Not dgv_covering.Columns.Contains("Dividend") Then
                        Dim btnCol As New DataGridViewButtonColumn()
                        btnCol.Name = "Dividend"
                        btnCol.HeaderText = "Dividend"
                        btnCol.Text = "View"
                        btnCol.UseColumnTextForButtonValue = True
                        btnCol.Width = 80
                        dgv_covering.Columns.Add(btnCol)
                    End If

                    If Not dgv_covering.Columns.Contains("SRNUpdate") Then
                        Dim btnCol As New DataGridViewButtonColumn()
                        btnCol.Name = "SRNUpdate"
                        btnCol.HeaderText = "SRN No"
                        btnCol.Text = "Update"
                        btnCol.UseColumnTextForButtonValue = True
                        btnCol.Width = 80
                        dgv_covering.Columns.Add(btnCol)
                    End If

                    If Not dgv_covering.Columns.Contains("TransactionView") Then
                        Dim btnCol As New DataGridViewButtonColumn()
                        btnCol.Name = "TransactionView"
                        btnCol.HeaderText = "Transaction View"
                        btnCol.Text = "View"
                        btnCol.UseColumnTextForButtonValue = True
                        btnCol.Width = 80
                        dgv_covering.Columns.Add(btnCol)
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching details: " & ex.Message)
        End Try
    End Sub
End Class