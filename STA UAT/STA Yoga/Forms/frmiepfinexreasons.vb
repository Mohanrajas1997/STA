Imports MySql.Data.MySqlClient

Public Class frmiepfinexreasons
#Region "Local Variables"
    Dim GetReqType As String = ""
    Dim GetStatus As String = ""
    Dim GetReqGid As Integer = 0
#End Region

    Private dt As DataTable

    Public Sub New(ReqType As String, Status As String, ReqGid As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        GetReqType = ReqType
        GetStatus = Status
        GetReqGid = ReqGid

    End Sub

    Private Sub frmiepfinexreasons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Get_mstinexreasonlist(GetReqType)

        If GetStatus = "Inex" Then
            save_btn.Visible = False
            CheckInexedReasons(GetReqGid)
            dgv_inexreason.Columns("chkSelect").ReadOnly = True


            inex_reason_txt.ReadOnly = True
            Dim inex_reason As String = ""

            inex_reason = GetAdditionalRemarks(GetReqGid)
            inex_reason_txt.Text = inex_reason
        Else
            save_btn.Visible = True
            inex_reason_txt.ReadOnly = False
        End If


    End Sub

    Private Sub Get_mstinexreasonlist(req_type As String)
        Try
            Using cmd As New MySqlCommand("pr_get_mstiepfinexreasons", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_req_type", req_type)

                Using da As New MySqlDataAdapter(cmd)
                    'Dim dt As New DataTable()
                    dt = New DataTable()
                    da.Fill(dt)

                    If Not dt.Columns.Contains("isSelected") Then
                        dt.Columns.Add("isSelected", GetType(Boolean))
                    End If

                    dgv_inexreason.Columns.Clear()
                    dgv_inexreason.AutoGenerateColumns = False
                    dgv_inexreason.AllowUserToAddRows = False
                    'dgv_inexreason.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

                    'Dim sameFont As New Font("Tahoma", 8.25, FontStyle.Bold)
                    'dgv_inexreason.ColumnHeadersDefaultCellStyle.Font = sameFont
                    'dgv_inexreason.DefaultCellStyle.Font = sameFont

                    Dim colId As New DataGridViewTextBoxColumn()
                    colId.HeaderText = "Reason ID"
                    colId.DataPropertyName = "mst_reason_gid"
                    colId.Name = "mst_reason_gid"
                    colId.Visible = False
                    dgv_inexreason.Columns.Add(colId)

                    Dim colDescp As New DataGridViewTextBoxColumn()
                    colDescp.HeaderText = "Inex Reason"
                    colDescp.DataPropertyName = "descp"
                    colDescp.Name = "descp"
                    colDescp.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                    dgv_inexreason.Columns.Add(colDescp)

                    Dim colCheck As New DataGridViewCheckBoxColumn()
                    colCheck.HeaderText = "Select"
                    colCheck.DataPropertyName = "isSelected"
                    colCheck.Name = "chkSelect"
                    colCheck.Width = 50
                    'colCheck.TrueValue = True
                    'colCheck.FalseValue = False
                    dgv_inexreason.Columns.Add(colCheck)

                    dgv_inexreason.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching details: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckInexedReasons(req_gid As Long)
        Try
            Dim inexedIds As New List(Of String)

            Using cmd As New MySqlCommand("pr_get_iepfinexreasonslist", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_req_gid", req_gid)

                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    For Each row As DataRow In dt.Rows
                        inexedIds.Add(row("inexreason_mstinexreason_gid").ToString())
                    Next
                End Using
            End Using

            ' Now tick checkboxes in dgv
            For Each gridRow As DataGridViewRow In dgv_inexreason.Rows
                Dim reasonId As String = gridRow.Cells("mst_reason_gid").Value.ToString()
                If inexedIds.Contains(reasonId) Then
                    gridRow.Cells("chkSelect").Value = True
                Else
                    gridRow.Cells("chkSelect").Value = False
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error checking inexed reasons: " & ex.Message)
        End Try
    End Sub

    'Private Sub save_btn_Click_old(sender As Object, e As EventArgs) Handles save_btn.Click
    '    Dim result As DialogResult = MessageBox.Show("Are you sure want to inex?", "STA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

    '    If result = DialogResult.Yes Then
    '        Dim selectedReasons As New List(Of String)()

    '        For Each row As DataGridViewRow In dgv_inexreason.Rows
    '            If row.Cells("chkSelect").Value IsNot Nothing AndAlso CBool(row.Cells("chkSelect").Value) = True Then
    '                Dim reasonId As String = row.Cells("mst_reason_gid").Value.ToString()
    '                selectedReasons.Add(reasonId)
    '            End If
    '        Next

    '        If dgv_inexreason.Rows.Count > 0 Then
    '            If selectedReasons.Count = 0 Then
    '                MessageBox.Show("Please select at least one Inex Reason before saving", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '            End If
    '        End If


    '        Dim reasonIdsCsv As String = String.Join(",", selectedReasons)

    '        Dim makerForm As frmiepfmaker = CType(Application.OpenForms("frmiepfmaker"), frmiepfmaker)
    '        If makerForm IsNot Nothing Then
    '            makerForm.ProcessInex(reasonIdsCsv)
    '            Me.Close()
    '        Else
    '            MessageBox.Show("Save cancelled", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    End If
    'End Sub

    Private Sub save_btn_Click(sender As Object, e As EventArgs) Handles save_btn.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to INEX?", "STA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Dim inex_reason As String = inex_reason_txt.Text()

            If result <> DialogResult.Yes Then Exit Sub

            Dim selectedReasons As New List(Of String)()

            ' Loop all rows and collect checked ones
            For Each row As DataGridViewRow In dgv_inexreason.Rows
                If Not row.IsNewRow Then
                    Dim isChecked As Boolean = False
                    If row.Cells("chkSelect").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("chkSelect").Value) Then
                        isChecked = CBool(row.Cells("chkSelect").Value)
                    End If

                    If isChecked Then
                        Dim reasonId As String = ""
                        If row.Cells("mst_reason_gid").Value IsNot Nothing Then
                            reasonId = row.Cells("mst_reason_gid").Value.ToString()
                        End If
                        If reasonId <> "" Then selectedReasons.Add(reasonId)
                    End If
                End If
            Next

            ' Validation
            If dgv_inexreason.Rows.Count > 0 AndAlso selectedReasons.Count = 0 AndAlso inex_reason = "" Then
                MessageBox.Show("Please select at least one Inex Reason or give the inex reason before saving.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Convert list to CSV string
            Dim reasonIdsCsv As String = String.Join(",", selectedReasons)

            ' Send data to maker form
            Dim makerForm As frmiepfmaker = TryCast(Application.OpenForms("frmiepfmaker"), frmiepfmaker)
            If makerForm IsNot Nothing Then
                makerForm.ProcessInex(reasonIdsCsv, inex_reason)
                Me.Close()
            Else
                MessageBox.Show("Maker form not found or closed.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error while saving: " & ex.Message, "STA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub reason_search_box_TextChanged(sender As Object, e As EventArgs) Handles reason_search_box.TextChanged
        Try
            ' if data not loaded yet, skip
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Sub

            ' create DataView for filtering
            Dim dv As New DataView(dt)
            Dim filterText As String = reason_search_box.Text.Trim().Replace("'", "''")

            If filterText <> "" Then
                dv.RowFilter = "descp LIKE '%" & filterText & "%'"
            Else
                dv.RowFilter = ""
            End If

            ' rebind filtered data
            dgv_inexreason.DataSource = dv

        Catch ex As Exception
            MessageBox.Show("Error while filtering: " & ex.Message)
        End Try
    End Sub

    Public Function GetAdditionalRemarks(req_gid As Long) As String
        Dim inex_reason As String = String.Empty
        Try
            Using cmd As New MySqlCommand("pr_get_iepfadditionalremarks", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_req_gid", req_gid)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        inex_reason = reader("inex_reason").ToString()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching remarks: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return inex_reason
    End Function
End Class