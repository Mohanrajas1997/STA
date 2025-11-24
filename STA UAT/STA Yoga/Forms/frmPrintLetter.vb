Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports Microsoft.SqlServer
Imports System.Net.Mail
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class frmPrintLetter
    Private inwardNo As String
    Private folioNo As String
    Private inwardId As String
    Private doctype As String
    Private shcount As String
    Private inwdate As String
    Private tofoliono As String
    Private companyname As String
    Private selectedReasonIds As New HashSet(Of Integer)

    Dim con As Odbc.OdbcConnection = New Odbc.OdbcConnection("Driver={Mysql odbc 3.51 Driver};Server=146.56.55.230;DataBase=sta;uid=root;pwd=Flexi@123;port=3306")
    Dim cmd As New Odbc.OdbcCommand
    Dim sql As String
    Dim reasonSubReasonMap As New Dictionary(Of String, List(Of String))
    Private reasonDictionary As New Dictionary(Of String, Integer)()
    Private allReasonItems As New List(Of KeyValuePair(Of Integer, String))

    Public Sub New(ByVal inwardno As String, ByVal foliono As String, ByVal inwardid As String, ByVal doctype As String, ByVal shcount As String, ByVal inwdate As String, ByVal tofoliono As String, ByVal companyname As String)
        InitializeComponent()
        Me.inwardNo = inwardno
        Me.folioNo = foliono
        Me.inwardId = inwardid
        Me.doctype = doctype
        Me.shcount = shcount
        Me.inwdate = inwdate
        Me.tofoliono = tofoliono
        Me.companyname = companyname
    End Sub

    Private Sub frmPrintLetter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTemplateData()
        TextBox1.Enabled = False
        Button1.Enabled = False
    End Sub

    Private Sub LoadTemplateData()
        Dim dt As New DataTable()

        Using cmd As New MySqlCommand("pr_sta_get_all_templates", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim adapter As New MySqlDataAdapter(cmd)
            adapter.Fill(dt)
        End Using

        ' Bind DataTable to ComboBox
        Dim newRow As DataRow = dt.NewRow()
        newRow("template_id") = DBNull.Value ' Use DBNull to avoid conversion errors
        newRow("template_name") = "-- Select Template Name --"
        dt.Rows.InsertAt(newRow, 0) ' Insert at first position

        ' Bind DataTable to ComboBox
        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "template_name" ' What the user sees
        ComboBox1.ValueMember = "template_id"    ' What we retrieve as SelectedValue
        ComboBox1.SelectedIndex = 0 ' Set default selection
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex <> -1 AndAlso ComboBox1.SelectedValue IsNot Nothing Then
            Dim selectedTemplateId As Integer
            If Integer.TryParse(ComboBox1.SelectedValue.ToString(), selectedTemplateId) Then

                Dim mhsql As String
                mhsql = "SELECT COUNT(*) AS cnt FROM sta_trn_ttemplate_files " &
                        "WHERE inward_gid = '" & inwardId & "' AND template_gid = '" & selectedTemplateId & "'"
                Using cmd As New MySqlCommand(mhsql, gOdbcConn)
                    Dim cnt As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If cnt = 0 Then
                        LoadReasonData(selectedTemplateId)
                    Else
                        MessageBox.Show("Inward already exists for this template")
                    End If
                End Using
            End If
        End If
    End Sub

    Private Sub LoadReasonData(templateId As Integer)
        CheckedListBox1.Items.Clear()
        CheckedListBox2.Items.Clear()
        allReasonItems.Clear() ' Clear previous data

        Using cmd As New MySqlCommand("pr_sta_get_reasons", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_template_id", templateId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                Dim reasonItem As New KeyValuePair(Of Integer, String)(reader("reason_id"), reader("reason_name").ToString())
                allReasonItems.Add(reasonItem)
            End While
            reader.Close()
        End Using

        ' Load all items initially
        LoadFilteredReasons("")

        CheckedListBox1.DisplayMember = "Value"
        CheckedListBox1.ValueMember = "Key"
    End Sub

    Private Sub LoadFilteredReasons(filter As String)
        CheckedListBox1.Items.Clear()

        For Each item In allReasonItems
            If item.Value.ToLower().Contains(filter.ToLower()) Then
                Dim isChecked As Boolean = selectedReasonIds.Contains(item.Key)
                CheckedListBox1.Items.Add(item, isChecked)
            End If
        Next
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadFilteredReasons(txtSearch.Text)
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        Me.BeginInvoke(New Action(Sub() UpdateSelectedItems()))
    End Sub

    'Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
    '    ' Ensure item exists and is a KeyValuePair
    '    If CheckedListBox1.Items(e.Index) IsNot Nothing Then
    '        Dim selectedReason As KeyValuePair(Of Integer, String) = CType(CheckedListBox1.Items(e.Index), KeyValuePair(Of Integer, String))
    '        Dim selectedReasonId As Integer = selectedReason.Key ' Get the reason_id

    '        If e.NewValue = CheckState.Checked Then
    '            LoadSubReasons(selectedReasonId)
    '        ElseIf e.NewValue = CheckState.Unchecked Then
    '            RemoveSubReasons(selectedReasonId)
    '        End If
    '    End If
    'End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Dim selectedItem = CType(CheckedListBox1.Items(e.Index), KeyValuePair(Of Integer, String))
        Dim selectedReasonId As Integer = selectedItem.Key
        Dim reasonText As String = selectedItem.Value ' Capture before BeginInvoke

        Me.BeginInvoke(New Action(Sub()
                                      If CheckedListBox1.GetItemChecked(e.Index) Then
                                          selectedReasonIds.Add(selectedReasonId)
                                          LoadSubReasons(selectedReasonId, reasonText)
                                      Else
                                          selectedReasonIds.Remove(selectedReasonId)
                                          RemoveSubReasons(selectedReasonId)
                                      End If
                                  End Sub))
    End Sub

    Private Sub LoadSubReasons(reasonId As Integer, reasonText As String)
        Using cmd As New MySqlCommand("pr_sta_get_subreasons", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_reason_id", reasonId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                Dim subReasonName As String = reader("subreason_name").ToString()

                If Not CheckedListBox2.Items.Contains(subReasonName) Then
                    CheckedListBox2.Items.Add(subReasonName)
                End If

                If Not reasonSubReasonMap.ContainsKey(reasonText) Then
                    reasonSubReasonMap(reasonText) = New List(Of String)
                End If

                If Not reasonSubReasonMap(reasonText).Contains(subReasonName) Then
                    reasonSubReasonMap(reasonText).Add(subReasonName)
                End If
            End While
            reader.Close()
        End Using
    End Sub

    Private Sub RemoveSubReasons(reasonId As Integer)
        ' Create a temporary list to store items to remove
        Dim itemsToRemove As New List(Of String)

        Using cmd As New MySqlCommand("pr_sta_get_subreasons", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_reason_id", reasonId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                Dim subReasonName As String = reader("subreason_name").ToString()
                itemsToRemove.Add(subReasonName)
            End While
            reader.Close()
        End Using

        ' Remove matching sub-reasons from CheckedListBox2
        For Each item As String In itemsToRemove
            CheckedListBox2.Items.Remove(item)
        Next
    End Sub

    Private Sub CheckedListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox2.SelectedIndexChanged

    End Sub

    Private Sub UpdateSelectedItems()
        Dim selectedItems As New List(Of String)

        For Each item As Object In CheckedListBox1.CheckedItems
            selectedItems.Add(item.ToString())
        Next
    End Sub

    Private Function GetSelectedReasonsAndSubReasons() As String
        Dim output As New List(Of String)

        For Each item As Object In CheckedListBox1.CheckedItems
            Dim reasonKVP As KeyValuePair(Of Integer, String) = CType(item, KeyValuePair(Of Integer, String))
            Dim reasonText As String = reasonKVP.Value

            ' Add reason to output
            output.Add("[" & reasonKVP.Key & ", " & reasonText & "] :")

            Dim selectedSubReasons As New List(Of String)
            For Each subReason As Object In CheckedListBox2.CheckedItems
                If reasonSubReasonMap.ContainsKey(reasonText) AndAlso reasonSubReasonMap(reasonText).Contains(subReason.ToString()) Then
                    selectedSubReasons.Add(subReason.ToString())
                End If
            Next

            If selectedSubReasons.Count > 0 Then
                output.Add(" - " & String.Join(vbCrLf & " - ", selectedSubReasons))
            Else
                output.Add("<no sub reasons>")
            End If

            output.Add("") ' Spacer
        Next

        Return String.Join(vbCrLf, output)
    End Function

    Private Function RemoveRtfLineIfEmpty(fileReader As String, placeholder As String, value As String) As String
        If String.IsNullOrWhiteSpace(value) Then
            ' Remove the entire line including the placeholder and its line break
            Dim pattern As String = "^[ \t]*<<\s*" & placeholder & "\s*>>[ \t]*(\r\n|\r|\n)?"
            Return Regex.Replace(fileReader, pattern, "", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Else
            Return fileReader.Replace("<<" & placeholder & ">>", value)
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim selectedData As String = GetSelectedReasonsAndSubReasons()
        Dim selectedValue = ""

        ' Split the selectedData into individual entries
        Dim entries As String() = selectedData.Split({vbCrLf & vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim cleanedList As New List(Of String)
        Dim selectedTemplateId As Integer
        Dim templateName As String = ComboBox1.Text
        Dim selectedItems As New List(Of String)
        Dim fileReader As String
        Dim basePath As String = "C:\STA EXE\CoveringletterTemplate\"
        Dim defaultTemplate As String = Path.Combine(basePath, "Covering_Letter_Template.rtf")
        Dim serialNumber As Integer = 1  ' Declare outside the loop that calls match
        'Dim inputFile As String = "c:\STA EXE\Covering_Letter_Template.rtf"
        'Convert template name into a filename dynamically
        Dim templateFileName As String = ComboBox1.Text.ToLower().Replace(" ", "_") & ".rtf"
        Dim templateFilePath As String = Path.Combine(basePath, templateFileName)

        ' If the dynamically generated file exists, use it; otherwise, fall back to the default template
        Dim inputFile As String = If(File.Exists(templateFilePath), templateFilePath, defaultTemplate)

        ' Validate if the file exists before opening
        If Not File.Exists(inputFile) Then
            MessageBox.Show("Template file not found: " & inputFile, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        'Process.Start(New ProcessStartInfo(inputFile) With {.UseShellExecute = True})
        ' Check if input file exists
        If Not File.Exists(inputFile) Then
            MessageBox.Show("Input file not found: " & inputFile, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        fileReader = ""
        Dim shsql As String
        Dim ds1 As DataSet
        ' Loop through each block of main reason + sub-reasons
        For Each entry As String In entries
            Dim lines As String() = entry.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
            If lines.Length = 0 Then Continue For

            ' Extract main reason (removing ID and brackets)
            Dim match As Match = Regex.Match(lines(0), "^\[\d+,\s*(.+?)\]")

            If match.Success Then
                Dim rawReason As String = match.Groups(1).Value.Trim()

                ' Extract just the reason text if input is like "[4, Reason...]"
                If rawReason.StartsWith("[") AndAlso rawReason.Contains(",") Then
                    Dim parts() As String = rawReason.Trim("["c, "]"c).Split(","c)
                    If parts.Length > 1 Then
                        rawReason = String.Join(",", parts.Skip(1)).Trim()
                    End If
                End If

                Dim subReasons As New List(Of String)

                ' Sub-reason parsing
                For i As Integer = 1 To lines.Length - 1
                    Dim subReason As String = lines(i).Trim()
                    If Not subReason.Equals("<no sub reasons>", StringComparison.OrdinalIgnoreCase) Then
                        If subReason.StartsWith("-") Then
                            subReason = subReason.Substring(1).Trim()
                        End If

                        ' Use RTF bullet
                        subReasons.Add("    \bullet\tab " & subReason)
                    End If
                Next

                ' Format main reason
                Dim formattedReason As String = serialNumber.ToString() & ". " & rawReason

                ' Add to cleanedList in RTF format
                If subReasons.Count > 0 Then
                    cleanedList.Add("\b " & formattedReason & " \b0" & "\par\par" & String.Join("\par", subReasons))
                Else
                    cleanedList.Add("\b " & formattedReason & " \b0" & "\par")
                End If

                serialNumber += 1
            End If

        Next
        Dim finalOutput As String = String.Join(vbCrLf, cleanedList)
        ' Ensure fileReader is not empty before replacing
        If Not String.IsNullOrEmpty(fileReader) Then
            fileReader = fileReader.Replace("<<reason1>>", finalOutput.Replace(vbCrLf, "\par "))
        End If

        If RadioButton2.Checked Then
            Dim cmd As MySqlCommand
            Dim da As MySqlDataAdapter
            Dim dt As DataTable
            Dim parsedDate As DateTime
            cmd = New MySqlCommand("pr_sta_get_foliocertentrylist", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_inward_gid", inwardId)

            'Out put Para
            cmd.Parameters.Add("?out_certentry_flag", MySqlDbType.Int32)
            cmd.Parameters("?out_certentry_flag").Direction = ParameterDirection.Output

            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            ' 'SQL Query Execution
            shsql = ""
            shsql &= "SELECT b.comp_name, b.comp_code, b.address1, b.address2, b.address3, b.city, b.state, b.country, a.holder1_name, COALESCE(a.holder2_name, '') AS holder2_name, COALESCE(a.holder3_name, '') AS holder3_name, a.folio_no, a.folio_contact_no, "
            shsql &= "COALESCE(e.address, '') AS address, COALESCE(f.salzer_controls_folio_no, '') AS salzer_controls_folio_no, "
            shsql &= "a.folio_addr1, a.folio_addr2, a.folio_addr3, a.folio_city, a.folio_state, COALESCE(d.share_count, 0) AS share_count, "
            shsql &= "COALESCE(f.salzer_electronic_folio_no, '') AS salzer_electronic_folio_no, COALESCE(g.req_dt, '0000-00-00') AS req_dt, "
            shsql &= "COALESCE(g.depo_id, '') AS depo_id, COALESCE(g.client_id, '') AS client_id, "
            shsql &= "COALESCE(h.WNO, '') AS WNO, COALESCE(h.CHQ, '') AS CHQ, COALESCE(h.AMOUNT, '') AS AMOUNT, COALESCE(h.DATE, '') AS date, COALESCE(h.FOLIO, '') AS folio, "
            shsql &= "COALESCE(i.SHARES, '') AS SHARES, COALESCE(i.FOLIO, '') AS oldfolio, COALESCE(i.GMLFOLIO, '') AS newfolio, "
            shsql &= "a.folio_country, a.folio_pincode, DATE_FORMAT(MAX(c.insert_date), '%d-%m-%Y') AS latest_insert_date "
            shsql &= "FROM sta_trn_tfolio AS a "
            shsql &= "Inner JOIN sta_mst_tcompany AS b ON a.comp_gid = b.comp_gid AND b.delete_flag = 'N' "
            shsql &= "Inner JOIN sta_trn_tinward AS c ON a.folio_no = c.folio_no and a.comp_gid = c.comp_gid "
            shsql &= "LEFT JOIN sel_member_master AS d ON a.folio_no = d.folio_no "
            shsql &= "LEFT JOIN sta_mst_ttemplateaddress AS e ON a.folio_no = e.folio_no "
            shsql &= "AND e.id = (SELECT MAX(id) FROM sta_mst_ttemplateaddress WHERE folio_no = a.folio_no) "
            shsql &= "LEFT JOIN scl_merger_master AS f ON a.folio_no = f.salzer_electronic_folio_no "
            shsql &= "LEFT JOIN sel_dp_master AS g ON a.folio_no = g.folio_no "
            shsql &= "LEFT JOIN ganga_paid AS h ON a.folio_no = h.FOLIO "
            shsql &= "LEFT JOIN ganga_to_gandhimathi AS i ON a.folio_no = i.GMLFOLIO "
            shsql &= "WHERE a.folio_no = '" & folioNo & "';"


            ds1 = New DataSet
            Call gpDataSet(shsql, "loc", gOdbcConn, ds1)

            With ds1.Tables("loc")
                If .Rows.Count > 0 Then
                    fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
                    fileReader = fileReader.Replace("<<date>>", Date.Today.ToString("dd-MM-yyyy"))
                    fileReader = fileReader.Replace("<<company_name>>", .Rows(0).Item("comp_name").ToString)
                    fileReader = fileReader.Replace("<<company_code>>", .Rows(0).Item("comp_code").ToString)
                    fileReader = fileReader.Replace("<<caddr_1>>", .Rows(0).Item("address1").ToString)
                    fileReader = fileReader.Replace("<<city>>", .Rows(0).Item("city").ToString)
                    fileReader = fileReader.Replace("<<state>>", .Rows(0).Item("state").ToString)
                    fileReader = fileReader.Replace("<<country>>", .Rows(0).Item("country").ToString)
                    'fileReader = fileReader.Replace("<<pincode>>", .Rows(0).Item("pincode").ToString)
                    fileReader = fileReader.Replace("<<folio_no>>", .Rows(0).Item("folio_no").ToString)
                    fileReader = fileReader.Replace("<<holder_name>>", .Rows(0).Item("holder1_name").ToString)
                    Dim holder2Name As String = .Rows(0).Item("holder2_name").ToString().Trim()
                    Dim formattedHolder2Name As String = If(String.IsNullOrEmpty(holder2Name), "", "Jointly With : " & holder2Name)
                    Dim holder3Name As String = .Rows(0).Item("holder3_name").ToString().Trim()
                    ' Check if both values are empty
                    If String.IsNullOrEmpty(holder2Name) AndAlso String.IsNullOrEmpty(holder3Name) Then
                        ' Use regex to remove the entire line that contains the placeholders
                        Dim pattern As String = ".*<<holder2_name>>\s*<<holder3_name>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        ' Replace placeholders with actual values
                        fileReader = fileReader.Replace("<<holder2_name>>", formattedHolder2Name)
                        fileReader = fileReader.Replace("<<holder3_name>>", holder3Name)
                    End If
                    fileReader = fileReader.Replace("<<holder3_name>>", .Rows(0).Item("holder3_name").ToString)
                    fileReader = fileReader.Replace("<<addr_1>>", .Rows(0).Item("address").ToString().Replace(",", "\par "))
                    Dim addr2 As String = ""
                    Dim addr3 As String = ""
                    Dim pin1 As String = ""
                    Dim contact1 As String = ""
                    If .Columns.Contains("addr_2") AndAlso Not IsDBNull(.Rows(0)("addr_2")) Then
                        addr2 = .Rows(0).Item("addr_2").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(addr2) Then
                        Dim patternAddr2 As String = ".*<<addr_2>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr2, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_2>>", addr2)
                    End If

                    If .Columns.Contains("addr_3") AndAlso Not IsDBNull(.Rows(0)("addr_3")) Then
                        addr3 = .Rows(0).Item("addr_3").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(addr3) Then
                        Dim patternAddr3 As String = ".*<<addr_3>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr3, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_3>>", addr3)
                    End If


                    If .Columns.Contains("pincode") AndAlso Not IsDBNull(.Rows(0)("pincode")) Then
                        pin1 = .Rows(0).Item("pincode").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(pin1) Then
                        Dim patternAddr4 As String = ".*<<pincode>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr4, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<pincode>>", pin1)
                    End If


                    If .Columns.Contains("mobile_no") AndAlso Not IsDBNull(.Rows(0)("mobile_no")) Then
                        contact1 = .Rows(0).Item("mobile_no").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(contact1) Then
                        Dim patternAddr5 As String = ".*<<mobile_no>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr5, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<mobile_no>>", contact1)
                    End If
                    fileReader = fileReader.Replace("<<inward_date>>", .Rows(0).Item("latest_insert_date").ToString)
                    fileReader = fileReader.Replace("<<old_share_count>>", .Rows(0).Item("share_count").ToString)
                    fileReader = fileReader.Replace("<<old_control_folio>>", .Rows(0).Item("salzer_controls_folio_no").ToString)
                    fileReader = fileReader.Replace("<<new_share_count_electronics>>", .Rows(0).Item("salzer_electronic_folio_no").ToString)
                    fileReader = fileReader.Replace("<<demat_year>>", .Rows(0).Item("req_dt").ToString)
                    fileReader = fileReader.Replace("<<dp_id>>", .Rows(0).Item("depo_id").ToString)
                    fileReader = fileReader.Replace("<<client_id>>", .Rows(0).Item("client_id").ToString)
                    fileReader = fileReader.Replace("<<warrent_no>>", .Rows(0).Item("WNO").ToString)
                    fileReader = fileReader.Replace("<<cheque_no>>", .Rows(0).Item("CHQ").ToString)
                    fileReader = fileReader.Replace("<<amount>>", .Rows(0).Item("AMOUNT").ToString)
                    fileReader = fileReader.Replace("<<date>>", .Rows(0).Item("date").ToString)
                    fileReader = fileReader.Replace("<<shares>>", .Rows(0).Item("SHARES").ToString)
                    fileReader = fileReader.Replace("<<old_folio_no>>", .Rows(0).Item("oldfolio").ToString)
                    fileReader = fileReader.Replace("<<new_folio_no>>", .Rows(0).Item("newfolio").ToString)
                    fileReader = fileReader.Replace("<<template_name>>", templateName)
                    fileReader = fileReader.Replace("<<document_type>>", doctype)
                    If DateTime.TryParse(inwdate, parsedDate) Then
                        fileReader = fileReader.Replace("<<inw_date>>", parsedDate.ToString("dd-MM-yyyy"))
                    Else
                        fileReader = fileReader.Replace("<<inw_date>>", "Invalid Date")
                    End If
                    fileReader = fileReader.Replace("<<sh_count>>", shcount)
                    fileReader = fileReader.Replace("<<inw_no>>", inwardNo)
                    fileReader = fileReader.Replace("<<reason1>>", finalOutput.Replace(vbCrLf, "\par "))
                    Dim replacements As New StringBuilder(fileReader)
                    Dim folioPlaceholder As String = "<<folio_no>>"

                    If fileReader.Contains(folioPlaceholder) Then
                        Dim startIndex As Integer = fileReader.IndexOf(folioPlaceholder)
                        If startIndex <> -1 Then
                            folioNo = fileReader.Substring(startIndex + folioPlaceholder.Length).Trim()
                        End If
                    End If

                    ' Process DataTable rows dynamically
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim rowNum As Integer = i + 1 ' Row numbers start from 1

                        ' Extract values from DataTable (dt)
                        Dim certGid As String = dt.Rows(i)("cert_gid").ToString().Trim()
                        Dim certNo As String = dt.Rows(i)("Certificate No").ToString().Trim()
                        Dim issuedDate As String = ""
                        If Not IsDBNull(dt.Rows(i)("Issued Date")) AndAlso Not String.IsNullOrWhiteSpace(dt.Rows(i)("Issued Date").ToString()) Then
                            issuedDate = Convert.ToDateTime(dt.Rows(i)("Issued Date")).ToString("dd-MM-yyyy")
                        End If
                        Dim shareCount As String = dt.Rows(i)("Share Count").ToString().Trim()

                        ' Extract dist_from and dist_to from "Dist Series" column
                        Dim distSeries As String = dt.Rows(i)("Dist Series").ToString().Trim()
                        Dim distFrom As String = ""
                        Dim distTo As String = ""

                        ' If "Dist Series" follows a pattern like "123-456", split it
                        If distSeries.Contains("-") Then
                            Dim distParts() As String = distSeries.Split("-"c)
                            distFrom = distParts(0).Trim()
                            distTo = If(distParts.Length > 1, distParts(1).Trim(), "")
                        End If

                        ' Check if all values are empty
                        If String.IsNullOrEmpty(certGid) AndAlso String.IsNullOrEmpty(issuedDate) AndAlso
                           String.IsNullOrEmpty(shareCount) AndAlso String.IsNullOrEmpty(distFrom) AndAlso
                           String.IsNullOrEmpty(distTo) Then

                            ' Remove the entire row from the table
                            Dim rowText As String = "<<row" & rowNum & "_date>>"
                            If replacements.ToString().Contains(rowText) Then
                                Dim startIndex As Integer = replacements.ToString().IndexOf(rowText)
                                Dim endIndex As Integer = replacements.ToString().IndexOf(">>", startIndex) + 2
                                replacements.Remove(startIndex, endIndex - startIndex)
                            End If
                        Else
                            ' Replace placeholders with actual values dynamically
                            replacements.Replace("<<row" & rowNum & "_folio_no>>", If(String.IsNullOrEmpty(folioNo), " --- ", tofoliono))
                            replacements.Replace("<<row" & rowNum & "_cert_no>>", certNo)
                            replacements.Replace("<<row" & rowNum & "_date>>", Convert.ToDateTime(inwdate).ToString("dd-MM-yyyy"))
                            replacements.Replace("<<row" & rowNum & "_share_count>>", shareCount)
                            replacements.Replace("<<row" & rowNum & "_dist_from>>", distFrom)
                            replacements.Replace("<<row" & rowNum & "_dist_to>>", distTo)
                        End If
                    Next

                    ' Remove placeholders for extra rows if they are not present in the DataTable
                    Dim maxRowPlaceholders As Integer = 10 ' Adjust this number as needed
                    For i As Integer = dt.Rows.Count + 1 To maxRowPlaceholders
                        replacements.Replace("<<row" & i & "_folio_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_cert_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_date>>", " --- ")
                        replacements.Replace("<<row" & i & "_share_count>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_from>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_to>>", " --- ")
                    Next

                    fileReader = replacements.ToString() ' Convert back to string


                End If
            End With

        Else
            Dim cmd As MySqlCommand
            Dim da As MySqlDataAdapter
            Dim dt As DataTable
            Dim parsedDate As DateTime
            cmd = New MySqlCommand("pr_sta_get_foliocertentrylist", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_inward_gid", inwardId)

            'Out put Para
            cmd.Parameters.Add("?out_certentry_flag", MySqlDbType.Int32)
            cmd.Parameters("?out_certentry_flag").Direction = ParameterDirection.Output

            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            ' SQL Query Execution
            shsql = ""
            shsql &= "SELECT b.comp_name, b.comp_code, b.address1, b.address2, b.address3, b.city, b.state, b.country, b.pincode, a.holder1_name, COALESCE(a.holder2_name, '') AS holder2_name, COALESCE(a.holder3_name, '') AS holder3_name, a.folio_no, a.folio_contact_no, "
            shsql &= "COALESCE(f.salzer_controls_folio_no, '') AS salzer_controls_folio_no, "
            shsql &= "a.folio_addr1, a.folio_addr2, a.folio_addr3, a.folio_city, a.folio_state, COALESCE(d.share_count, 0) AS share_count, "
            shsql &= "COALESCE(f.salzer_electronic_folio_no, '') AS salzer_electronic_folio_no, COALESCE(g.req_dt, '0000-00-00') AS req_dt, "
            shsql &= "COALESCE(g.depo_id, '') AS depo_id, COALESCE(g.client_id, '') AS client_id, "
            shsql &= "COALESCE(h.WNO, '') AS WNO, COALESCE(h.CHQ, '') AS CHQ, COALESCE(h.AMOUNT, '') AS AMOUNT, COALESCE(h.DATE, '') AS date, COALESCE(h.FOLIO, '') AS folio, "
            shsql &= "COALESCE(i.SHARES, '') AS SHARES, COALESCE(i.FOLIO, '') AS oldfolio, COALESCE(i.GMLFOLIO, '') AS newfolio, "
            shsql &= "a.folio_country, a.folio_pincode, DATE_FORMAT(MAX(c.insert_date), '%d-%m-%Y') AS latest_insert_date "
            shsql &= "FROM sta_trn_tfolio AS a "
            shsql &= "Inner JOIN sta_mst_tcompany AS b ON a.comp_gid = b.comp_gid AND b.delete_flag = 'N' "
            shsql &= "Inner JOIN sta_trn_tinward AS c ON a.folio_no = c.folio_no and a.comp_gid = c.comp_gid "
            shsql &= "LEFT JOIN sel_member_master AS d ON a.folio_no = d.folio_no "
            shsql &= "LEFT JOIN sta_mst_ttemplateaddress AS e ON a.folio_no = e.folio_no "
            shsql &= "LEFT JOIN scl_merger_master AS f ON a.folio_no = f.salzer_electronic_folio_no "
            shsql &= "LEFT JOIN sel_dp_master AS g ON a.folio_no = g.folio_no "
            shsql &= "LEFT JOIN ganga_paid AS h ON a.folio_no = h.FOLIO "
            shsql &= "LEFT JOIN ganga_to_gandhimathi AS i ON a.folio_no = i.GMLFOLIO "
            shsql &= "WHERE a.folio_no = '" & folioNo & "';"


            ds1 = New DataSet
            Call gpDataSet(shsql, "loc", gOdbcConn, ds1)

            With ds1.Tables("loc")
                If .Rows.Count > 0 Then
                    fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
                    fileReader = fileReader.Replace("<<date>>", Date.Today.ToString("dd-MM-yyyy"))
                    fileReader = fileReader.Replace("<<company_name>>", .Rows(0).Item("comp_name").ToString)
                    fileReader = fileReader.Replace("<<company_code>>", .Rows(0).Item("comp_code").ToString)
                    fileReader = fileReader.Replace("<<caddr_1>>", .Rows(0).Item("address1").ToString)
                    fileReader = fileReader.Replace("<<city>>", .Rows(0).Item("city").ToString)
                    fileReader = fileReader.Replace("<<state>>", .Rows(0).Item("state").ToString)
                    fileReader = fileReader.Replace("<<country>>", .Rows(0).Item("country").ToString)
                    fileReader = fileReader.Replace("<<pincode>>", .Rows(0).Item("folio_pincode").ToString)
                    fileReader = fileReader.Replace("<<folio_no>>", .Rows(0).Item("folio_no").ToString)
                    fileReader = fileReader.Replace("<<holder_name>>", .Rows(0).Item("holder1_name").ToString)
                    Dim holder2Name As String = .Rows(0).Item("holder2_name").ToString().Trim()
                    Dim formattedHolder2Name As String = If(String.IsNullOrEmpty(holder2Name), "", "Jointly With : " & holder2Name)
                    Dim holder3Name As String = .Rows(0).Item("holder3_name").ToString().Trim()

                    If String.IsNullOrEmpty(holder2Name) AndAlso String.IsNullOrEmpty(holder3Name) Then
                        Dim pattern As String = ".*<<holder2_name>>\s*<<holder3_name>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<holder2_name>>", formattedHolder2Name)
                        fileReader = fileReader.Replace("<<holder3_name>>", holder3Name)
                    End If
                    Dim addr1 As String = .Rows(0).Item("folio_addr1").ToString().Trim()
                    Dim addr2 As String = .Rows(0).Item("folio_addr2").ToString().Trim()
                    Dim addr3 As String = .Rows(0).Item("folio_addr3").ToString().Trim()
                    Dim city1 As String = .Rows(0).Item("folio_city").ToString().Trim()
                    Dim state1 As String = .Rows(0).Item("folio_state").ToString().Trim()
                    Dim country1 As String = .Rows(0).Item("folio_country").ToString().Trim()
                    Dim pin1 As String = .Rows(0).Item("folio_pincode").ToString().Trim()
                    Dim contact1 As String = .Rows(0).Item("folio_contact_no").ToString().Trim()
                    If String.IsNullOrEmpty(addr1) Then
                        Dim pattern1 As String = ".*<<addr_1>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern1, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_1>>", addr1)
                    End If
                    If String.IsNullOrEmpty(addr2) Then
                        Dim pattern2 As String = ".*<<addr_2>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern2, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_2>>", addr2)
                    End If
                    If String.IsNullOrEmpty(addr3) Then
                        Dim pattern3 As String = ".*<<addr_3>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern3, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_3>>", addr3)
                    End If
                    If String.IsNullOrEmpty(city1) Then
                        Dim pattern4 As String = ".*<<city>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern4, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<city>>", city1)
                    End If
                    If String.IsNullOrEmpty(state1) Then
                        Dim pattern5 As String = ".*<<state>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern5, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<state>>", state1)
                    End If
                    If String.IsNullOrEmpty(country1) Then
                        Dim pattern6 As String = ".*<<country>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern6, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<country>>", country1)
                    End If
                    If String.IsNullOrEmpty(pin1) Then
                        Dim pattern7 As String = ".*<<pincode>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern7, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<pincode>>", pin1)
                    End If
                    If String.IsNullOrEmpty(contact1) Then
                        Dim pattern8 As String = ".*<<mobile_no>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern8, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<mobile_no>>", contact1)
                    End If
                    fileReader = fileReader.Replace("<<inward_date>>", .Rows(0).Item("latest_insert_date").ToString)
                    fileReader = fileReader.Replace("<<old_share_count>>", .Rows(0).Item("share_count").ToString)
                    fileReader = fileReader.Replace("<<old_control_folio>>", .Rows(0).Item("salzer_controls_folio_no").ToString)
                    fileReader = fileReader.Replace("<<new_share_count_electronics>>", .Rows(0).Item("salzer_electronic_folio_no").ToString)
                    fileReader = fileReader.Replace("<<demat_year>>", .Rows(0).Item("req_dt").ToString)
                    fileReader = fileReader.Replace("<<dp_id>>", .Rows(0).Item("depo_id").ToString)
                    fileReader = fileReader.Replace("<<client_id>>", .Rows(0).Item("client_id").ToString)
                    fileReader = fileReader.Replace("<<warrent_no>>", .Rows(0).Item("WNO").ToString)
                    fileReader = fileReader.Replace("<<cheque_no>>", .Rows(0).Item("CHQ").ToString)
                    fileReader = fileReader.Replace("<<amount>>", .Rows(0).Item("AMOUNT").ToString)
                    fileReader = fileReader.Replace("<<date>>", .Rows(0).Item("date").ToString)
                    fileReader = fileReader.Replace("<<shares>>", .Rows(0).Item("SHARES").ToString)
                    fileReader = fileReader.Replace("<<old_foilo_no>>", .Rows(0).Item("oldfolio").ToString)
                    fileReader = fileReader.Replace("<<new_folio_no>>", .Rows(0).Item("newfolio").ToString)
                    fileReader = fileReader.Replace("<<document_type>>", doctype)
                    If DateTime.TryParse(inwdate, parsedDate) Then
                        fileReader = fileReader.Replace("<<inw_date>>", parsedDate.ToString("dd-MM-yyyy"))
                    Else
                        fileReader = fileReader.Replace("<<inw_date>>", "Invalid Date")
                    End If
                    fileReader = fileReader.Replace("<<sh_count>>", shcount)
                    fileReader = fileReader.Replace("<<template_name>>", templateName)
                    fileReader = fileReader.Replace("<<inw_no>>", inwardNo)
                    fileReader = fileReader.Replace("<<reason1>>", finalOutput.Replace(vbCrLf, "\par "))

                    Dim replacements As New StringBuilder(fileReader)
                    Dim folioPlaceholder As String = "<<folio_no>>"

                    If fileReader.Contains(folioPlaceholder) Then
                        Dim startIndex As Integer = fileReader.IndexOf(folioPlaceholder)
                        If startIndex <> -1 Then
                            folioNo = fileReader.Substring(startIndex + folioPlaceholder.Length).Trim()
                        End If
                    End If

                    ' Process DataTable rows dynamically
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim rowNum As Integer = i + 1 ' Row numbers start from 1

                        ' Extract values from DataTable (dt)
                        Dim certGid As String = dt.Rows(i)("cert_gid").ToString().Trim()
                        Dim certNo As String = dt.Rows(i)("Certificate No").ToString().Trim()
                        Dim issuedDate As String = ""
                        If Not IsDBNull(dt.Rows(i)("Issued Date")) AndAlso Not String.IsNullOrWhiteSpace(dt.Rows(i)("Issued Date").ToString()) Then
                            issuedDate = Convert.ToDateTime(dt.Rows(i)("Issued Date")).ToString("dd-MM-yyyy")
                        End If
                        Dim shareCount As String = dt.Rows(i)("Share Count").ToString().Trim()

                        ' Extract dist_from and dist_to from "Dist Series" column
                        Dim distSeries As String = dt.Rows(i)("Dist Series").ToString().Trim()
                        Dim distFrom As String = ""
                        Dim distTo As String = ""

                        ' If "Dist Series" follows a pattern like "123-456", split it
                        If distSeries.Contains("-") Then
                            Dim distParts() As String = distSeries.Split("-"c)
                            distFrom = distParts(0).Trim()
                            distTo = If(distParts.Length > 1, distParts(1).Trim(), "")
                        End If

                        ' Check if all values are empty
                        If String.IsNullOrEmpty(certGid) AndAlso String.IsNullOrEmpty(issuedDate) AndAlso
                           String.IsNullOrEmpty(shareCount) AndAlso String.IsNullOrEmpty(distFrom) AndAlso
                           String.IsNullOrEmpty(distTo) Then

                            ' Remove the entire row from the table
                            Dim rowText As String = "<<row" & rowNum & "_date>>"
                            If replacements.ToString().Contains(rowText) Then
                                Dim startIndex As Integer = replacements.ToString().IndexOf(rowText)
                                Dim endIndex As Integer = replacements.ToString().IndexOf(">>", startIndex) + 2
                                replacements.Remove(startIndex, endIndex - startIndex)
                            End If
                        Else
                            ' Replace placeholders with actual values dynamically
                            replacements.Replace("<<row" & rowNum & "_folio_no>>", If(String.IsNullOrEmpty(folioNo), " --- ", tofoliono))
                            replacements.Replace("<<row" & rowNum & "_cert_no>>", certNo)
                            replacements.Replace("<<row" & rowNum & "_date>>", Convert.ToDateTime(inwdate).ToString("dd-MM-yyyy"))
                            replacements.Replace("<<row" & rowNum & "_share_count>>", shareCount)
                            replacements.Replace("<<row" & rowNum & "_dist_from>>", distFrom)
                            replacements.Replace("<<row" & rowNum & "_dist_to>>", distTo)
                        End If
                    Next

                    ' Remove placeholders for extra rows if they are not present in the DataTable
                    Dim maxRowPlaceholders As Integer = 10 ' Adjust this number as needed
                    For i As Integer = dt.Rows.Count + 1 To maxRowPlaceholders
                        replacements.Replace("<<row" & i & "_folio_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_cert_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_date>>", " --- ")
                        replacements.Replace("<<row" & i & "_share_count>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_from>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_to>>", " --- ")
                    Next

                    fileReader = replacements.ToString() ' Convert back to string


                End If
            End With
        End If

        If Integer.TryParse(ComboBox1.SelectedValue.ToString(), selectedTemplateId) Then
            Dim outputFolder As String = "C:\letterofconfirmation\template" & selectedTemplateId

            ' Ensure output folder exists
            If Not Directory.Exists(outputFolder) Then
                Directory.CreateDirectory(outputFolder)
            End If

            Dim nextFileGid As Integer = 1
            Dim lsSql As String = "SELECT MAX(file_gid) FROM sta_trn_ttemplate_files"

            Using cmd As New MySqlCommand(lsSql, gOdbcConn)
                If gOdbcConn.State = ConnectionState.Closed Then
                    gOdbcConn.Open()
                End If
                Dim result = cmd.ExecuteScalar()
                If Not IsDBNull(result) AndAlso result IsNot Nothing Then
                    nextFileGid = Convert.ToInt32(result) + 1
                End If
            End Using

            Dim currentDateTime As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim outputFile As String = Path.Combine(outputFolder, _
    "CoveringLetterTemplate-" & inwardNo & "-" & folioNo & "-" & nextFileGid & "-" & currentDateTime & ".rtf")
            File.WriteAllText(outputFile, fileReader)
            Call AddAttachment(outputFile, inwardId, selectedTemplateId, selectedItems)


            ' Ensure output file exists before opening
            If File.Exists(outputFile) Then
                'MessageBox.Show("File successfully created at: " & outputFile, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Dim startInfo As New ProcessStartInfo()
                startInfo.FileName = outputFile
                startInfo.UseShellExecute = True
                Process.Start(startInfo)
            Else
                MessageBox.Show("File not found after writing: " & outputFile, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            ds1.Clear()
        End If
    End Sub

    ' Function to determine which main reason a sub-reason belongs to
    Function GetMainReasonForSubReason(subReason As String, mainReasons As List(Of String)) As String
        ' Logic: Find the closest matching main reason for the sub-reason
        For Each mainReason As String In mainReasons
            If subReason.ToLower().Contains(mainReason.ToLower()) Then
                Return mainReason
            End If
        Next
        Return "" ' If no match found
    End Function

    Private Sub AddAttachment(FileName As String, InwardId As Integer, selectedTemplateId As Integer, selectedItems As List(Of String))
        Dim lnResult As Long
        Dim lsTxt As String
        Dim lsFileName As String
        Dim lnAttachmentId As Long
        Dim lnAttachmentTypeId As Long = 0
        Dim lsSrcFile As String
        Dim lsDestFile As String
        Dim file_name As String
        Dim insert_date As Date
        Dim update_date As Date
        Dim insert_by As String
        Dim update_by As String
        Dim delete_flag As String
        Try

            If File.Exists(FileName) Then
                lsFileName = Path.GetFileName(FileName) ' Extracts the filename correctly
            Else
                lsFileName = ""
                MessageBox.Show("File Not Available! " & FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            lsSrcFile = FileName.ToString()
            lnAttachmentTypeId = 1

            Using cmd As New MySqlCommand("pr_sta_trn_tattachment", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_attachment_gid", 0)
                cmd.Parameters("?in_attachment_gid").Direction = ParameterDirection.InputOutput
                cmd.Parameters.AddWithValue("?in_inward_gid", InwardId)
                cmd.Parameters.AddWithValue("?in_attachmenttype_gid", lnAttachmentTypeId)
                cmd.Parameters.AddWithValue("?in_file_name", lsFileName)
                'cmd.Parameters.AddWithValue("?in_file_name", lsSrcFile)
                cmd.Parameters.AddWithValue("?in_action", "INSERT")
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                If lnResult = 1 Then
                    lnAttachmentId = Val(cmd.Parameters("?in_attachment_gid").Value.ToString())
                    If Directory.Exists(gsAttachmentPath) = False Then Directory.CreateDirectory(gsAttachmentPath)
                    lsDestFile = gsAttachmentPath & "\" & lnAttachmentId.ToString & ".sta"
                    Call File.Copy(lsSrcFile, lsDestFile)
                End If
                Call InsTemplateFiles(InwardId, selectedTemplateId, lsFileName, file_name, lsSrcFile, insert_date, insert_by, update_date, update_by, delete_flag, selectedItems)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If (RadioButton1.Checked) Then
            TextBox1.Enabled = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If (RadioButton2.Checked) Then
            TextBox1.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button1.Enabled = True
        Dim address As String = TextBox1.Text
        Dim selectedData As String = GetSelectedReasonsAndSubReasons()
        Dim template_id As Integer
        Dim folio_no As String
        Dim insert_date As Date
        Dim insert_by As String
        Dim update_date As Date
        Dim update_by As String
        Dim delete_flag As String

        ' Split the selectedData into individual entries
        Dim entries As String() = selectedData.Split({vbCrLf & vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim cleanedList As New List(Of String)

        If ComboBox1.SelectedIndex = -1 OrElse ComboBox1.Text = "-- Select Template Name --" Then
            MessageBox.Show("Please select a template.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'ElseIf CheckedListBox1.CheckedItems.Count = 0 Then
            '    MessageBox.Show("Please select at least one reason in the list.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Exit Sub
        End If

        Dim fileReader As String
        Dim selectedTemplateId As Integer
        Dim templateName As String = ComboBox1.Text
        Dim selectedItems As New List(Of String)
        Dim basePath As String = "C:\STA EXE\CoveringletterTemplate\"
        Dim defaultTemplate As String = Path.Combine(basePath, "Covering_Letter_Template.rtf")
        Dim serialNumber As Integer = 1  ' Declare outside the loop that calls match
        'Dim inputFile As String = "c:\STA EXE\Covering_Letter_Template.rtf"
        'Convert template name into a filename dynamically
        Dim templateFileName As String = ComboBox1.Text.ToLower().Replace(" ", "_") & ".rtf"
        Dim templateFilePath As String = Path.Combine(basePath, templateFileName)

        ' If the dynamically generated file exists, use it; otherwise, fall back to the default template
        Dim inputFile As String = If(File.Exists(templateFilePath), templateFilePath, defaultTemplate)

        ' Validate if the file exists before opening
        If Not File.Exists(inputFile) Then
            MessageBox.Show("Template file not found: " & inputFile, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        ' Check if input file exists
        If Not File.Exists(inputFile) Then
            MessageBox.Show("Input file not found: " & inputFile, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        fileReader = ""
        Dim shsql As String
        Dim ds1 As DataSet
        ' Loop through each block of main reason + sub-reasons
        For Each entry As String In entries
            Dim lines As String() = entry.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
            If lines.Length = 0 Then Continue For

            ' Extract main reason (removing ID and brackets)
            Dim match As Match = Regex.Match(lines(0), "^\[\d+,\s*(.+?)\]")

            If match.Success Then
                Dim rawReason As String = match.Groups(1).Value.Trim()

                ' Extract just the reason text if input is like "[4, Reason...]"
                If rawReason.StartsWith("[") AndAlso rawReason.Contains(",") Then
                    Dim parts() As String = rawReason.Trim("["c, "]"c).Split(","c)
                    If parts.Length > 1 Then
                        rawReason = String.Join(",", parts.Skip(1)).Trim()
                    End If
                End If

                Dim subReasons As New List(Of String)

                ' Sub-reason parsing
                For i As Integer = 1 To lines.Length - 1
                    Dim subReason As String = lines(i).Trim()
                    If Not subReason.Equals("<no sub reasons>", StringComparison.OrdinalIgnoreCase) Then
                        If subReason.StartsWith("-") Then
                            subReason = subReason.Substring(1).Trim()
                        End If

                        ' Use RTF bullet
                        subReasons.Add("    \bullet\tab " & subReason)
                    End If
                Next

                ' Format main reason
                Dim formattedReason As String = serialNumber.ToString() & ". " & rawReason

                ' Add to cleanedList in RTF format
                If subReasons.Count > 0 Then
                    cleanedList.Add("\b " & formattedReason & " \b0" & "\par\par" & String.Join("\par", subReasons))
                Else
                    cleanedList.Add("\b " & formattedReason & " \b0" & "\par")
                End If

                serialNumber += 1
            End If

        Next
        Dim finalOutput As String = String.Join(vbCrLf, cleanedList)
        ' Ensure fileReader is not empty before replacing
        If Not String.IsNullOrEmpty(fileReader) Then
            fileReader = fileReader.Replace("<<reason1>>", finalOutput.Replace(vbCrLf, "\par "))
        End If

        Call insAddress(ComboBox1.SelectedValue.ToString(), folioNo, address, insert_date, insert_by, update_date, update_by, delete_flag)

        If RadioButton2.Checked Then
            Dim cmd As MySqlCommand
            Dim da As MySqlDataAdapter
            Dim dt As DataTable
            Dim parsedDate As DateTime
            cmd = New MySqlCommand("pr_sta_get_foliocertentrylist", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_inward_gid", inwardId)

            'Out put Para
            cmd.Parameters.Add("?out_certentry_flag", MySqlDbType.Int32)
            cmd.Parameters("?out_certentry_flag").Direction = ParameterDirection.Output

            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            ' 'SQL Query Execution
            shsql = ""
            shsql &= "SELECT b.comp_name, b.comp_code, b.address1, b.address2, b.address3, b.city, b.state, b.country, a.holder1_name, COALESCE(a.holder2_name, '') AS holder2_name, COALESCE(a.holder3_name, '') AS holder3_name, a.folio_no, a.folio_contact_no, "
            shsql &= "COALESCE(e.address, '') AS address, COALESCE(f.salzer_controls_folio_no, '') AS salzer_controls_folio_no, "
            shsql &= "a.folio_addr1, a.folio_addr2, a.folio_addr3, a.folio_city, a.folio_state, COALESCE(d.share_count, 0) AS share_count, "
            shsql &= "COALESCE(f.salzer_electronic_folio_no, '') AS salzer_electronic_folio_no, COALESCE(g.req_dt, '0000-00-00') AS req_dt, "
            shsql &= "COALESCE(g.depo_id, '') AS depo_id, COALESCE(g.client_id, '') AS client_id, "
            shsql &= "COALESCE(h.WNO, '') AS WNO, COALESCE(h.CHQ, '') AS CHQ, COALESCE(h.AMOUNT, '') AS AMOUNT, COALESCE(h.DATE, '') AS date, COALESCE(h.FOLIO, '') AS folio, "
            shsql &= "COALESCE(i.SHARES, '') AS SHARES, COALESCE(i.FOLIO, '') AS oldfolio, COALESCE(i.GMLFOLIO, '') AS newfolio, "
            shsql &= "a.folio_country, DATE_FORMAT(MAX(c.insert_date), '%d-%m-%Y') AS latest_insert_date "
            shsql &= "FROM sta_trn_tfolio AS a "
            shsql &= "Inner JOIN sta_mst_tcompany AS b ON a.comp_gid = b.comp_gid AND b.delete_flag = 'N' "
            shsql &= "Inner JOIN sta_trn_tinward AS c ON a.folio_no = c.folio_no and a.comp_gid = c.comp_gid "
            shsql &= "LEFT JOIN sel_member_master AS d ON a.folio_no = d.folio_no "
            shsql &= "LEFT JOIN sta_mst_ttemplateaddress AS e ON a.folio_no = e.folio_no "
            shsql &= "AND e.id = (SELECT MAX(id) FROM sta_mst_ttemplateaddress WHERE folio_no = a.folio_no) "
            shsql &= "LEFT JOIN scl_merger_master AS f ON a.folio_no = f.salzer_electronic_folio_no "
            shsql &= "LEFT JOIN sel_dp_master AS g ON a.folio_no = g.folio_no "
            shsql &= "LEFT JOIN ganga_paid AS h ON a.folio_no = h.FOLIO "
            shsql &= "LEFT JOIN ganga_to_gandhimathi AS i ON a.folio_no = i.GMLFOLIO "
            shsql &= "WHERE a.folio_no = '" & folioNo & "';"


            ds1 = New DataSet
            Call gpDataSet(shsql, "loc", gOdbcConn, ds1)

            With ds1.Tables("loc")
                If .Rows.Count > 0 Then
                    fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
                    fileReader = fileReader.Replace("<<date>>", Date.Today.ToString("dd-MM-yyyy"))
                    fileReader = fileReader.Replace("<<company_name>>", .Rows(0).Item("comp_name").ToString)
                    fileReader = fileReader.Replace("<<company_code>>", .Rows(0).Item("comp_code").ToString)
                    fileReader = fileReader.Replace("<<caddr_1>>", .Rows(0).Item("address1").ToString)
                    fileReader = fileReader.Replace("<<city>>", .Rows(0).Item("city").ToString)
                    fileReader = fileReader.Replace("<<state>>", .Rows(0).Item("state").ToString)
                    fileReader = fileReader.Replace("<<country>>", .Rows(0).Item("country").ToString)
                    'fileReader = fileReader.Replace("<<pincode>>", .Rows(0).Item("pincode").ToString)
                    fileReader = fileReader.Replace("<<folio_no>>", .Rows(0).Item("folio_no").ToString)
                    fileReader = fileReader.Replace("<<holder_name>>", .Rows(0).Item("holder1_name").ToString)
                    Dim holder2Name As String = .Rows(0).Item("holder2_name").ToString().Trim()
                    Dim formattedHolder2Name As String = If(String.IsNullOrEmpty(holder2Name), "", "Jointly With : " & holder2Name)
                    Dim holder3Name As String = .Rows(0).Item("holder3_name").ToString().Trim()
                    ' Check if both values are empty
                    If String.IsNullOrEmpty(holder2Name) AndAlso String.IsNullOrEmpty(holder3Name) Then
                        ' Use regex to remove the entire line that contains the placeholders
                        Dim pattern As String = ".*<<holder2_name>>\s*<<holder3_name>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        ' Replace placeholders with actual values
                        fileReader = fileReader.Replace("<<holder2_name>>", formattedHolder2Name)
                        fileReader = fileReader.Replace("<<holder3_name>>", holder3Name)
                    End If
                    fileReader = fileReader.Replace("<<addr_1>>", .Rows(0).Item("address").ToString().Replace(",", "\par "))
                    Dim addr2 As String = ""
                    Dim addr3 As String = ""
                    Dim pin1 As String = ""
                    Dim contact1 As String = ""
                    If .Columns.Contains("addr_2") AndAlso Not IsDBNull(.Rows(0)("addr_2")) Then
                        addr2 = .Rows(0).Item("addr_2").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(addr2) Then
                        Dim patternAddr2 As String = ".*<<addr_2>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr2, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_2>>", addr2)
                    End If

                    If .Columns.Contains("addr_3") AndAlso Not IsDBNull(.Rows(0)("addr_3")) Then
                        addr3 = .Rows(0).Item("addr_3").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(addr3) Then
                        Dim patternAddr3 As String = ".*<<addr_3>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr3, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_3>>", addr3)
                    End If


                    If .Columns.Contains("pincode") AndAlso Not IsDBNull(.Rows(0)("pincode")) Then
                        pin1 = .Rows(0).Item("pincode").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(pin1) Then
                        Dim patternAddr4 As String = ".*<<pincode>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr4, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<pincode>>", pin1)
                    End If


                    If .Columns.Contains("mobile_no") AndAlso Not IsDBNull(.Rows(0)("mobile_no")) Then
                        contact1 = .Rows(0).Item("mobile_no").ToString().Trim()
                    End If

                    If String.IsNullOrEmpty(contact1) Then
                        Dim patternAddr5 As String = ".*<<mobile_no>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, patternAddr5, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<mobile_no>>", contact1)
                    End If

                    fileReader = fileReader.Replace("<<inward_date>>", .Rows(0).Item("latest_insert_date").ToString)
                    fileReader = fileReader.Replace("<<old_share_count>>", .Rows(0).Item("share_count").ToString)
                    fileReader = fileReader.Replace("<<old_control_folio>>", .Rows(0).Item("salzer_controls_folio_no").ToString)
                    fileReader = fileReader.Replace("<<new_share_count_electronics>>", .Rows(0).Item("salzer_electronic_folio_no").ToString)
                    fileReader = fileReader.Replace("<<demat_year>>", .Rows(0).Item("req_dt").ToString)
                    fileReader = fileReader.Replace("<<dp_id>>", .Rows(0).Item("depo_id").ToString)
                    fileReader = fileReader.Replace("<<client_id>>", .Rows(0).Item("client_id").ToString)
                    fileReader = fileReader.Replace("<<warrent_no>>", .Rows(0).Item("WNO").ToString)
                    fileReader = fileReader.Replace("<<cheque_no>>", .Rows(0).Item("CHQ").ToString)
                    fileReader = fileReader.Replace("<<amount>>", .Rows(0).Item("AMOUNT").ToString)
                    fileReader = fileReader.Replace("<<date>>", .Rows(0).Item("date").ToString)
                    fileReader = fileReader.Replace("<<shares>>", .Rows(0).Item("SHARES").ToString)
                    fileReader = fileReader.Replace("<<old_folio_no>>", .Rows(0).Item("oldfolio").ToString)
                    fileReader = fileReader.Replace("<<new_folio_no>>", .Rows(0).Item("newfolio").ToString)
                    fileReader = fileReader.Replace("<<document_type>>", doctype)
                    If DateTime.TryParse(inwdate, parsedDate) Then
                        fileReader = fileReader.Replace("<<inw_date>>", parsedDate.ToString("dd-MM-yyyy"))
                    Else
                        fileReader = fileReader.Replace("<<inw_date>>", "Invalid Date")
                    End If
                    fileReader = fileReader.Replace("<<sh_count>>", shcount)
                    fileReader = fileReader.Replace("<<template_name>>", templateName)
                    fileReader = fileReader.Replace("<<inw_no>>", inwardNo)
                    fileReader = fileReader.Replace("<<reason1>>", finalOutput.Replace(vbCrLf, "\par "))
                    Debug.WriteLine(fileReader)
                    Dim replacements As New StringBuilder(fileReader)

                    Dim folioPlaceholder As String = "<<folio_no>>"

                    If fileReader.Contains(folioPlaceholder) Then
                        Dim startIndex As Integer = fileReader.IndexOf(folioPlaceholder)
                        If startIndex <> -1 Then
                            folioNo = fileReader.Substring(startIndex + folioPlaceholder.Length).Trim()
                        End If
                    End If

                    ' Process DataTable rows dynamically
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim rowNum As Integer = i + 1 ' Row numbers start from 1

                        ' Extract values from DataTable (dt)
                        Dim certGid As String = dt.Rows(i)("cert_gid").ToString().Trim()
                        Dim certNo As String = dt.Rows(i)("Certificate No").ToString().Trim()
                        Dim issuedDate As String = ""
                        If Not IsDBNull(dt.Rows(i)("Issued Date")) AndAlso Not String.IsNullOrWhiteSpace(dt.Rows(i)("Issued Date").ToString()) Then
                            issuedDate = Convert.ToDateTime(dt.Rows(i)("Issued Date")).ToString("dd-MM-yyyy")
                        End If
                        Dim shareCount As String = dt.Rows(i)("Share Count").ToString().Trim()

                        ' Extract dist_from and dist_to from "Dist Series" column
                        Dim distSeries As String = dt.Rows(i)("Dist Series").ToString().Trim()
                        Dim distFrom As String = ""
                        Dim distTo As String = ""

                        ' If "Dist Series" follows a pattern like "123-456", split it
                        If distSeries.Contains("-") Then
                            Dim distParts() As String = distSeries.Split("-"c)
                            distFrom = distParts(0).Trim()
                            distTo = If(distParts.Length > 1, distParts(1).Trim(), "")
                        End If

                        ' Check if all values are empty
                        If String.IsNullOrEmpty(certGid) AndAlso String.IsNullOrEmpty(issuedDate) AndAlso
                           String.IsNullOrEmpty(shareCount) AndAlso String.IsNullOrEmpty(distFrom) AndAlso
                           String.IsNullOrEmpty(distTo) Then

                            ' Remove the entire row from the table
                            Dim rowText As String = "<<row" & rowNum & "_date>>"
                            If replacements.ToString().Contains(rowText) Then
                                Dim startIndex As Integer = replacements.ToString().IndexOf(rowText)
                                Dim endIndex As Integer = replacements.ToString().IndexOf(">>", startIndex) + 2
                                replacements.Remove(startIndex, endIndex - startIndex)
                            End If
                        Else
                            ' Replace placeholders with actual values dynamically
                            replacements.Replace("<<row" & rowNum & "_folio_no>>", If(String.IsNullOrEmpty(folioNo), " --- ", tofoliono))
                            replacements.Replace("<<row" & rowNum & "_cert_no>>", certNo)
                            replacements.Replace("<<row" & rowNum & "_date>>", Convert.ToDateTime(inwdate).ToString("dd-MM-yyyy"))
                            replacements.Replace("<<row" & rowNum & "_share_count>>", shareCount)
                            replacements.Replace("<<row" & rowNum & "_dist_from>>", distFrom)
                            replacements.Replace("<<row" & rowNum & "_dist_to>>", distTo)
                        End If
                    Next

                    ' Remove placeholders for extra rows if they are not present in the DataTable
                    Dim maxRowPlaceholders As Integer = 10 ' Adjust this number as needed
                    For i As Integer = dt.Rows.Count + 1 To maxRowPlaceholders
                        replacements.Replace("<<row" & i & "_folio_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_cert_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_date>>", " --- ")
                        replacements.Replace("<<row" & i & "_share_count>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_from>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_to>>", " --- ")
                    Next

                    fileReader = replacements.ToString() ' Convert back to string


                End If
            End With

        Else
            Dim cmd As MySqlCommand
            Dim da As MySqlDataAdapter
            Dim dt As DataTable
            Dim parsedDate As DateTime
            cmd = New MySqlCommand("pr_sta_get_foliocertentrylist", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_inward_gid", inwardId)

            'Out put Para
            cmd.Parameters.Add("?out_certentry_flag", MySqlDbType.Int32)
            cmd.Parameters("?out_certentry_flag").Direction = ParameterDirection.Output

            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            ' SQL Query Execution
            shsql = ""
            shsql &= "SELECT b.comp_name, b.comp_code, b.address1, b.address2, b.address3, b.city, b.state, b.country, b.pincode, a.holder1_name, COALESCE(a.holder2_name, '') AS holder2_name, COALESCE(a.holder3_name, '') AS holder3_name, a.folio_no, a.folio_contact_no, "
            shsql &= "COALESCE(f.salzer_controls_folio_no, '') AS salzer_controls_folio_no, "
            shsql &= "a.folio_addr1, a.folio_addr2, a.folio_addr3, a.folio_city, a.folio_state, COALESCE(d.share_count, 0) AS share_count, "
            shsql &= "COALESCE(f.salzer_electronic_folio_no, '') AS salzer_electronic_folio_no, COALESCE(g.req_dt, '0000-00-00') AS req_dt, "
            shsql &= "COALESCE(g.depo_id, '') AS depo_id, COALESCE(g.client_id, '') AS client_id, "
            shsql &= "COALESCE(h.WNO, '') AS WNO, COALESCE(h.CHQ, '') AS CHQ, COALESCE(h.AMOUNT, '') AS AMOUNT, COALESCE(h.DATE, '') AS date, COALESCE(h.FOLIO, '') AS folio, "
            shsql &= "COALESCE(i.SHARES, '') AS SHARES, COALESCE(i.FOLIO, '') AS oldfolio, COALESCE(i.GMLFOLIO, '') AS newfolio, "
            shsql &= "a.folio_country, a.folio_pincode, DATE_FORMAT(MAX(c.insert_date), '%d-%m-%Y') AS latest_insert_date "
            shsql &= "FROM sta_trn_tfolio AS a "
            shsql &= "Inner JOIN sta_mst_tcompany AS b ON a.comp_gid = b.comp_gid AND b.delete_flag = 'N' "
            shsql &= "Inner JOIN sta_trn_tinward AS c ON a.folio_no = c.folio_no and a.comp_gid = c.comp_gid "
            shsql &= "LEFT JOIN sel_member_master AS d ON a.folio_no = d.folio_no "
            shsql &= "LEFT JOIN sta_mst_ttemplateaddress AS e ON a.folio_no = e.folio_no "
            shsql &= "LEFT JOIN scl_merger_master AS f ON a.folio_no = f.salzer_electronic_folio_no "
            shsql &= "LEFT JOIN sel_dp_master AS g ON a.folio_no = g.folio_no "
            shsql &= "LEFT JOIN ganga_paid AS h ON a.folio_no = h.FOLIO "
            shsql &= "LEFT JOIN ganga_to_gandhimathi AS i ON a.folio_no = i.GMLFOLIO "
            shsql &= "WHERE a.folio_no = '" & folioNo & "';"


            ds1 = New DataSet
            Call gpDataSet(shsql, "loc", gOdbcConn, ds1)

            With ds1.Tables("loc")
                If .Rows.Count > 0 Then
                    fileReader = My.Computer.FileSystem.ReadAllText(inputFile)
                    fileReader = fileReader.Replace("<<date>>", Date.Today.ToString("dd-MM-yyyy"))
                    fileReader = fileReader.Replace("<<company_name>>", .Rows(0).Item("comp_name").ToString)
                    fileReader = fileReader.Replace("<<company_code>>", .Rows(0).Item("comp_code").ToString)
                    fileReader = fileReader.Replace("<<caddr_1>>", .Rows(0).Item("address1").ToString)
                    fileReader = fileReader.Replace("<<city>>", .Rows(0).Item("city").ToString)
                    fileReader = fileReader.Replace("<<state>>", .Rows(0).Item("state").ToString)
                    fileReader = fileReader.Replace("<<country>>", .Rows(0).Item("country").ToString)
                    fileReader = fileReader.Replace("<<pincode>>", .Rows(0).Item("folio_pincode").ToString)
                    fileReader = fileReader.Replace("<<folio_no>>", .Rows(0).Item("folio_no").ToString)
                    fileReader = fileReader.Replace("<<holder_name>>", .Rows(0).Item("holder1_name").ToString)
                    Dim holder2Name As String = .Rows(0).Item("holder2_name").ToString().Trim()
                    Dim formattedHolder2Name As String = If(String.IsNullOrEmpty(holder2Name), "", "Jointly With : " & holder2Name)
                    Dim holder3Name As String = .Rows(0).Item("holder3_name").ToString().Trim()

                    ' Check if both values are empty
                    If String.IsNullOrEmpty(holder2Name) AndAlso String.IsNullOrEmpty(holder3Name) Then
                        ' Use regex to remove the entire line that contains the placeholders
                        Dim pattern As String = ".*<<holder2_name>>\s*<<holder3_name>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern, "", System.Text.RegularExpressions.RegexOptions.Multiline)
                    Else
                        ' Replace placeholders with actual values
                        fileReader = fileReader.Replace("<<holder2_name>>", formattedHolder2Name)
                        fileReader = fileReader.Replace("<<holder3_name>>", holder3Name)
                    End If

                    Dim addr1 As String = .Rows(0).Item("folio_addr1").ToString().Trim()
                    Dim addr2 As String = .Rows(0).Item("folio_addr2").ToString().Trim()
                    Dim addr3 As String = .Rows(0).Item("folio_addr3").ToString().Trim()
                    Dim city1 As String = .Rows(0).Item("folio_city").ToString().Trim()
                    Dim state1 As String = .Rows(0).Item("folio_state").ToString().Trim()
                    Dim country1 As String = .Rows(0).Item("folio_country").ToString().Trim()
                    Dim pin1 As String = .Rows(0).Item("folio_pincode").ToString().Trim()
                    Dim contact1 As String = .Rows(0).Item("folio_contact_no").ToString().Trim()
                    If String.IsNullOrEmpty(addr1) Then
                        Dim pattern1 As String = ".*<<addr_1>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern1, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_1>>", addr1)
                    End If
                    If String.IsNullOrEmpty(addr2) Then
                        Dim pattern2 As String = ".*<<addr_2>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern2, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_2>>", addr2)
                    End If
                    If String.IsNullOrEmpty(addr3) Then
                        Dim pattern3 As String = ".*<<addr_3>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern3, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<addr_3>>", addr3)
                    End If
                    If String.IsNullOrEmpty(city1) Then
                        Dim pattern4 As String = ".*<<city>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern4, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<city>>", city1)
                    End If
                    If String.IsNullOrEmpty(state1) Then
                        Dim pattern5 As String = ".*<<state>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern5, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<state>>", state1)
                    End If
                    If String.IsNullOrEmpty(country1) Then
                        Dim pattern6 As String = ".*<<country>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern6, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<country>>", country1)
                    End If
                    If String.IsNullOrEmpty(pin1) Then
                        Dim pattern7 As String = ".*<<pincode>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern7, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<pincode>>", pin1)
                    End If
                    If String.IsNullOrEmpty(contact1) Then
                        Dim pattern8 As String = ".*<<mobile_no>>.*\r?\n?"
                        fileReader = System.Text.RegularExpressions.Regex.Replace(fileReader, pattern8, "", RegexOptions.Multiline)
                    Else
                        fileReader = fileReader.Replace("<<mobile_no>>", contact1)
                    End If
                    fileReader = fileReader.Replace("<<inward_date>>", .Rows(0).Item("latest_insert_date").ToString)
                    fileReader = fileReader.Replace("<<old_share_count>>", .Rows(0).Item("share_count").ToString)
                    fileReader = fileReader.Replace("<<old_control_folio>>", .Rows(0).Item("salzer_controls_folio_no").ToString)
                    fileReader = fileReader.Replace("<<new_share_count_electronics>>", .Rows(0).Item("salzer_electronic_folio_no").ToString)
                    fileReader = fileReader.Replace("<<demat_year>>", .Rows(0).Item("req_dt").ToString)
                    fileReader = fileReader.Replace("<<dp_id>>", .Rows(0).Item("depo_id").ToString)
                    fileReader = fileReader.Replace("<<client_id>>", .Rows(0).Item("client_id").ToString)
                    fileReader = fileReader.Replace("<<warrent_no>>", .Rows(0).Item("WNO").ToString)
                    fileReader = fileReader.Replace("<<cheque_no>>", .Rows(0).Item("CHQ").ToString)
                    fileReader = fileReader.Replace("<<amount>>", .Rows(0).Item("AMOUNT").ToString)
                    fileReader = fileReader.Replace("<<date>>", .Rows(0).Item("date").ToString)
                    fileReader = fileReader.Replace("<<shares>>", .Rows(0).Item("SHARES").ToString)
                    fileReader = fileReader.Replace("<<old_folio_no>>", .Rows(0).Item("oldfolio").ToString)
                    fileReader = fileReader.Replace("<<new_folio_no>>", .Rows(0).Item("newfolio").ToString)
                    fileReader = fileReader.Replace("<<document_type>>", doctype)
                    If DateTime.TryParse(inwdate, parsedDate) Then
                        fileReader = fileReader.Replace("<<inw_date>>", parsedDate.ToString("dd-MM-yyyy"))
                    Else
                        fileReader = fileReader.Replace("<<inw_date>>", "Invalid Date")
                    End If
                    fileReader = fileReader.Replace("<<sh_count>>", shcount)
                    fileReader = fileReader.Replace("<<template_name>>", templateName)
                    fileReader = fileReader.Replace("<<inw_no>>", inwardNo)
                    fileReader = fileReader.Replace("<<reason1>>", finalOutput.Replace(vbCrLf, "\par "))

                    Dim replacements As New StringBuilder(fileReader)
                    Dim folioPlaceholder As String = "<<folio_no>>"

                    If fileReader.Contains(folioPlaceholder) Then
                        Dim startIndex As Integer = fileReader.IndexOf(folioPlaceholder)
                        If startIndex <> -1 Then
                            folioNo = fileReader.Substring(startIndex + folioPlaceholder.Length).Trim()
                        End If
                    End If

                    ' Process DataTable rows dynamically
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim rowNum As Integer = i + 1 ' Row numbers start from 1

                        ' Extract values from DataTable (dt)
                        Dim certGid As String = dt.Rows(i)("cert_gid").ToString().Trim()
                        Dim certNo As String = dt.Rows(i)("Certificate No").ToString().Trim()
                        Dim issuedDate As String = ""
                        If Not IsDBNull(dt.Rows(i)("Issued Date")) AndAlso Not String.IsNullOrWhiteSpace(dt.Rows(i)("Issued Date").ToString()) Then
                            issuedDate = Convert.ToDateTime(dt.Rows(i)("Issued Date")).ToString("dd-MM-yyyy")
                        End If
                        Dim shareCount As String = dt.Rows(i)("Share Count").ToString().Trim()

                        ' Extract dist_from and dist_to from "Dist Series" column
                        Dim distSeries As String = dt.Rows(i)("Dist Series").ToString().Trim()
                        Dim distFrom As String = ""
                        Dim distTo As String = ""

                        ' If "Dist Series" follows a pattern like "123-456", split it
                        If distSeries.Contains("-") Then
                            Dim distParts() As String = distSeries.Split("-"c)
                            distFrom = distParts(0).Trim()
                            distTo = If(distParts.Length > 1, distParts(1).Trim(), "")
                        End If

                        ' Check if all values are empty
                        If String.IsNullOrEmpty(certGid) AndAlso String.IsNullOrEmpty(issuedDate) AndAlso
                           String.IsNullOrEmpty(shareCount) AndAlso String.IsNullOrEmpty(distFrom) AndAlso
                           String.IsNullOrEmpty(distTo) Then

                            ' Remove the entire row from the table
                            Dim rowText As String = "<<row" & rowNum & "_date>>"
                            If replacements.ToString().Contains(rowText) Then
                                Dim startIndex As Integer = replacements.ToString().IndexOf(rowText)
                                Dim endIndex As Integer = replacements.ToString().IndexOf(">>", startIndex) + 2
                                replacements.Remove(startIndex, endIndex - startIndex)
                            End If
                        Else
                            ' Replace placeholders with actual values dynamically
                            replacements.Replace("<<row" & rowNum & "_folio_no>>", If(String.IsNullOrEmpty(folioNo), " --- ", tofoliono))
                            replacements.Replace("<<row" & rowNum & "_cert_no>>", certNo)
                            replacements.Replace("<<row" & rowNum & "_date>>", Convert.ToDateTime(inwdate).ToString("dd-MM-yyyy"))
                            replacements.Replace("<<row" & rowNum & "_share_count>>", shareCount)
                            replacements.Replace("<<row" & rowNum & "_dist_from>>", distFrom)
                            replacements.Replace("<<row" & rowNum & "_dist_to>>", distTo)
                        End If
                    Next

                    ' Remove placeholders for extra rows if they are not present in the DataTable
                    Dim maxRowPlaceholders As Integer = 10 ' Adjust this number as needed
                    For i As Integer = dt.Rows.Count + 1 To maxRowPlaceholders
                        replacements.Replace("<<row" & i & "_folio_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_cert_no>>", " --- ")
                        replacements.Replace("<<row" & i & "_date>>", " --- ")
                        replacements.Replace("<<row" & i & "_share_count>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_from>>", " --- ")
                        replacements.Replace("<<row" & i & "_dist_to>>", " --- ")
                    Next

                    fileReader = replacements.ToString() ' Convert back to string
                End If
            End With
        End If

        If Integer.TryParse(ComboBox1.SelectedValue.ToString(), selectedTemplateId) Then
            Dim nextFileGid As Integer = 1
            Dim lsSql As String = "SELECT MAX(file_gid) FROM sta_trn_ttemplate_files"

            Using cmd As New MySqlCommand(lsSql, gOdbcConn)
                If gOdbcConn.State = ConnectionState.Closed Then
                    gOdbcConn.Open()
                End If
                Dim result = cmd.ExecuteScalar()
                If Not IsDBNull(result) AndAlso result IsNot Nothing Then
                    nextFileGid = Convert.ToInt32(result) + 1
                End If
            End Using

            Dim currentDateTime As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim tempFilePath As String = Path.Combine(Path.GetTempPath(), _
    "CoveringLetterTemplate-" & nextFileGid & "-" & currentDateTime & ".rtf")

            ' Write content to a temporary file in memory
            File.WriteAllText(tempFilePath, fileReader)

            ' Open the file for viewing
            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = tempFilePath
            startInfo.UseShellExecute = True
            Process.Start(startInfo)

            ds1.Clear()
        End If

    End Sub

    Private Sub insAddress(template_id As Integer, folio_no As String, address As String, insert_date As Date, insert_by As String, update_date As Date,
                           update_by As String, delete_flag As String)
        Dim inssql As String
        Dim insResult As Integer
        inssql = ""
        inssql &= " insert into sta_mst_ttemplateaddress (template_id, folio_no, address, insert_date, insert_by, update_date, update_by, delete_flag) values ("
        inssql &= " '" & template_id & "',"
        inssql &= " '" & folioNo & "',"
        inssql &= " '" & address & "',"
        inssql &= " sysdate(),"
        inssql &= " '" & gsLoginUserCode & "',"
        inssql &= " sysdate(),"
        inssql &= " '" & gsLoginUserCode & "',"
        inssql &= " 'N')"
        insResult = gfInsertQry(inssql, gOdbcConn)
    End Sub

    Private Sub InsTemplateFiles(inwardId As Integer, selectedTemplateId As Integer, lsFileName As String, file_name As String, lsSrcFile As String, insert_date As Date,
                              insert_by As String, update_date As Date, update_by As String, delete_flag As String, selectedItems As List(Of String))
        Dim lsSql As String
        Dim lsDestFile As String
        'Dim lsFileName As String
        Dim lnResult As Integer
        'Dim selectedTemplateId As Integer
        'Dim selectedItems As New List(Of String)
        Dim reasons As String = If(selectedItems.Count > 0, String.Join(vbCrLf, selectedItems), "None")
        ' Use the passed selectedItems
        Dim formattedReasons As String = If(selectedItems.Count > 0, String.Join(", ", selectedItems), "None")
        lsSql = ""
        lsSql &= " insert into sta_trn_ttemplate_files (inward_gid, template_gid, org_file_name, file_name, file_path, insert_date, insert_by, update_date, update_by, delete_flag) values ("
        lsSql &= " '" & inwardId & "',"
        lsSql &= " '" & selectedTemplateId & "',"
        lsSql &= " '" & lsFileName & "',"
        lsSql &= " '" & lsFileName & "',"
        lsSql &= " '" & lsSrcFile.Replace("\", "\\") & "',"
        lsSql &= " sysdate(),"
        lsSql &= " '" & gsLoginUserCode & "',"
        lsSql &= " sysdate(),"
        lsSql &= " '" & gsLoginUserCode & "',"
        lsSql &= " 'N')"
        lnResult = gfInsertQry(lsSql, gOdbcConn)
        MsgBox("Covering Letter Genereted Successfully !", MsgBoxStyle.Information, gsProjectName)
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

End Class