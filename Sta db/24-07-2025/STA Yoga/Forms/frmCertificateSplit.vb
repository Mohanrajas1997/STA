Imports MySql.Data.MySqlClient

Public Class frmCertificateSplit
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnQueueId As Long
    Dim msTranCode As String
    Dim mnChklstValid As Long
    Dim mnChklstDisc As Long
    Dim mnChkLstAllStatus As Long = 0
    Dim mnChkLstSelected As Long = 0
    Dim msGroupCode As String = ""

    Dim fobjDTSplit As New DataTable
    Dim fobjDTSplitDist As New DataTable
    Dim fobjRow As DataRow
#End Region

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmAddressChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsCurrQueue As String
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select b.queue_to from sta_trn_tinward as a "
        lsSql &= " inner join sta_trn_tqueue as b on b.queue_gid = a.queue_gid and b.delete_flag = 'N' "
        lsSql &= " where a.inward_gid = " & mnInwardId & " "
        lsSql &= " and a.delete_flag = 'N' "

        lsCurrQueue = gfExecuteScalar(lsSql, gOdbcConn)

        Select Case lsCurrQueue
            Case "I"
                btnSubmit.Text = "Inex"
                btnReject.Text = "Reprocess"
            Case "M"
                btnReject.Text = "Send Back"

                'btnReject.Text = "Inex"
                'btnReject.Visible = False
                'btnSubmit.Left = btnReject.Left
            Case "C"
                btnReject.Text = "Send Back"
            Case "U"
                btnSubmit.Visible = False
                btnReject.Text = "Send Back"
        End Select

        Call LoadData()
    End Sub

    Public Sub New(GroupCode As String, InwardId As Long, QueueId As Long, TranCode As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        msGroupCode = GroupCode
        mnInwardId = InwardId
        mnQueueId = QueueId
        msTranCode = TranCode

        Select Case GroupCode
            Case "M"
            Case "V"
                dgvCert.ReadOnly = True
                dgvSplit.ReadOnly = True

                dgvSplitDist.ReadOnly = True
                dgvChklst.ReadOnly = True

                btnSubmit.Visible = False
                btnReject.Visible = False
                btnView.Visible = False
                lnkAddAttachment.Visible = False

                txtRemark.Enabled = False
            Case Else
                dgvCert.ReadOnly = True
                dgvSplit.ReadOnly = True

                dgvSplitDist.ReadOnly = True
                dgvChklst.ReadOnly = True

                txtSplit.Enabled = False

                btnSplit.Enabled = False
                btnClear.Enabled = False

                lnkAddAttachment.Visible = False
        End Select
    End Sub

    Private Sub LoadData()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lobjChkBoxColumn As DataGridViewCheckBoxColumn
        Dim i As Integer
        Dim n As Integer
        Dim lnChkLstValid As Integer
        Dim lnChkLstDisc As Integer

        Dim lnFolioId As Long
        Dim lnCertEntryFlag As Integer = 0
        Dim lnTotShares As Long = 0

        cmd = New MySqlCommand("pr_sta_get_certtranentry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
        cmd.Parameters.AddWithValue("?in_queue_gid", mnQueueId)

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                mnCompId = .Rows(0).Item("comp_gid")
                msCompName = .Rows(0).Item("comp_name").ToString

                lnFolioId = .Rows(0).Item("folio_gid")

                txtInwardNo.Text = .Rows(0).Item("inward_no").ToString
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolder.Text = .Rows(0).Item("shareholder_name").ToString
                txtPanNo.Text = .Rows(0).Item("shareholder_pan_no").ToString

                txtSplit.Text = .Rows(0).Item("split_formula").ToString

                lnChkLstValid = .Rows(0).Item("chklst_valid")
                lnChkLstDisc = .Rows(0).Item("chklst_disc")
            Else
                Call frmCtrClear(Me)
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()

        ' load certificate
        cmd = New MySqlCommand("pr_sta_get_foliocertentrylist", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)

        'Out put Para
        cmd.Parameters.Add("?out_certentry_flag", MySqlDbType.Int32)
        cmd.Parameters("?out_certentry_flag").Direction = ParameterDirection.Output

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        lnCertEntryFlag = Val(cmd.Parameters("?out_certentry_flag").Value.ToString())

        With dgvCert
            .DataSource = dt

            .Columns("cert_gid").Visible = False
            .Columns("cert_status").Visible = False

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Select"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Select"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            lnTotShares = 0

            For i = 0 To .Rows.Count - 1
                lnTotShares += .Rows(i).Cells("Share Count").Value

                If (lnCertEntryFlag = 1) Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If
            Next i
        End With

        lblTotal.Text = lnTotShares.ToString()
        Call RefreshShareCount()

        ' load split
        cmd = New MySqlCommand("pr_sta_get_certsplitentry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)

        cmd.CommandTimeout = 0

        fobjDTSplit.Rows.Clear()

        da = New MySqlDataAdapter(cmd)
        da.Fill(fobjDTSplit)

        With dgvSplit
            .DataSource = fobjDTSplit

            .Columns("new_cert_gid").Visible = False
            .Columns("certsplitentry_gid").Visible = False
        End With

        ' load split dist
        cmd = New MySqlCommand("pr_sta_get_certsplitdistentry", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)

        cmd.CommandTimeout = 0

        fobjDTSplitDist.Rows.Clear()

        da = New MySqlDataAdapter(cmd)
        da.Fill(fobjDTSplitDist)

        With dgvSplitDist
            .DataSource = fobjDTSplitDist

            .Columns("Cert Share").Visible = False
            .Columns("Old Cert No").Visible = False
            .Columns("certsplitentry_gid").Visible = False
            .Columns("certsplitdistentry_gid").Visible = False
        End With

        ' load check list
        cmd = New MySqlCommand("pr_sta_get_checklist", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_tran_code", msTranCode)
        cmd.Parameters.AddWithValue("?in_auto_flag", "")

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvChklst
            .DataSource = dt

            .Columns("chklst_gid").Visible = False
            .Columns("chklst_value").Visible = False
            .Columns("Check List").Width = 210

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Ok"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Ok"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Not Ok"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Not Ok"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            For i = 0 To .Rows.Count - 1
                mnChkLstAllStatus = mnChkLstAllStatus Or .Rows(i).Cells("chklst_value").Value

                If (lnChkLstValid And .Rows(i).Cells("chklst_value").Value) > 0 Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If

                If (lnChkLstDisc And .Rows(i).Cells("chklst_value").Value) > 0 Then
                    .Rows(i).Cells(n + 2).Value = True
                Else
                    .Rows(i).Cells(n + 2).Value = False
                End If
            Next i

            If mnChkLstAllStatus = lnChkLstValid Then
                lblDocStatus.Text = "Valid"
                lblDocStatus.ForeColor = Color.DarkGreen
            Else
                lblDocStatus.Text = "Invalid"
                lblDocStatus.ForeColor = Color.Red
            End If
        End With
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim lnResult As Long

        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            If msGroupCode = "M" Then
                lnResult = UpdateInformation(gnQueueSuccess)
            Else
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueSuccess)
            End If

            If lnResult = 1 Then Me.Close()
        End If
    End Sub

    Private Function UpdateInformation(ActionStatus As Integer) As Long
        Dim i As Integer
        Dim n As Integer

        Dim lnChklstValid As Long = 0
        Dim lnChklstDisc As Long = 0
        Dim lnChklstValue As Long = 0

        Dim lnResult As Long = 0
        Dim lsTxt As String
        Dim lsMsg As String

        Dim lsCertId As String = ""
        Dim lnNewFolioId As Long = 0

        Dim lsSplitFormula As String = ""
        Dim lsRemark As String = ""
        Dim lnRowSNo As Integer = 0
        Dim lnShareCount As Long = 0
        Dim lnSplitCount As Long = 0

        Dim lnDistFrom As Long = 0
        Dim lnDistTo As Long = 0

        Try
            lsSplitFormula = QuoteFilter(txtSplit.Text)
            lsRemark = QuoteFilter(txtRemark.Text)

            ' get certificate id
            lsCertId = ""

            With dgvCert
                For i = 0 To .RowCount - 1
                    If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                        lsCertId &= .Rows(i).Cells("cert_gid").Value.ToString & ","
                        lnShareCount += Val(.Rows(i).Cells("Share Count").Value.ToString)
                    End If
                Next i
            End With

            With dgvSplit
                For i = 0 To .RowCount - 1
                    lnSplitCount += Val(.Rows(i).Cells("Share Count").Value.ToString)
                Next i
            End With

            If lsCertId = "" Then
                MessageBox.Show("Please select the certificate !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Return lnResult
            End If

            If lnShareCount <> lnSplitCount Or lnShareCount = 0 Then
                MessageBox.Show("Split share count is not tallied !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvCert.Focus()
                Return lnResult
            End If

            ' check list value
            With dgvChklst
                n = .Columns.Count - 1

                For i = 0 To .Rows.Count - 1
                    lnChklstValue = .Rows(i).Cells("chklst_value").Value
                    mnChkLstAllStatus = mnChkLstAllStatus Or lnChklstValue

                    If .Rows(i).Cells(n - 1).Value = True Then
                        lnChklstValid = lnChklstValid Or lnChklstValue
                    End If

                    If .Rows(i).Cells(n).Value = True Then
                        lnChklstDisc = lnChklstDisc Or lnChklstValue
                    End If
                Next i

                If mnChkLstAllStatus <> (lnChklstValid + lnChklstDisc) Then
                    MessageBox.Show("Please complete the check list !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return lnResult
                End If
            End With

            Using cmd As New MySqlCommand("pr_sta_set_certsplitentry", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_cert_gid", lsCertId)

                cmd.Parameters.AddWithValue("?in_chklst_valid", lnChklstValid)
                cmd.Parameters.AddWithValue("?in_chklst_disc", lnChklstDisc)
                cmd.Parameters.AddWithValue("?in_split_formula", lsSplitFormula)
                cmd.Parameters.AddWithValue("?in_remark", lsRemark)
                cmd.Parameters.AddWithValue("?in_action_status", ActionStatus)
                cmd.Parameters.AddWithValue("?in_action_by", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0

                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsMsg = cmd.Parameters("?out_msg").Value.ToString()
            End Using

            ' insert in cert split entry table
            With dgvSplit
                For i = 0 To .RowCount - 1
                    lnRowSNo = Val(.Rows(i).Cells("SNo").Value.ToString)
                    lnShareCount = Val(.Rows(i).Cells("Share Count").Value.ToString)

                    Using cmd As New MySqlCommand("pr_sta_ins_certsplitentry", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                        cmd.Parameters.AddWithValue("?in_row_sno", lnRowSNo)
                        cmd.Parameters.AddWithValue("?in_share_count", lnShareCount)

                        'Out put Para
                        cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                        cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                        cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                        cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                        cmd.CommandTimeout = 0

                        cmd.ExecuteNonQuery()

                        lsTxt = cmd.Parameters("?out_msg").Value.ToString()
                    End Using
                Next i
            End With

            ' insert in cert split dist entry table
            With dgvSplitDist
                For i = 0 To .RowCount - 1
                    lnRowSNo = Val(.Rows(i).Cells("Cert SNo").Value.ToString)
                    lnDistFrom = Val(.Rows(i).Cells("Dist From").Value.ToString)
                    lnDistTo = Val(.Rows(i).Cells("Dist To").Value.ToString)
                    lnShareCount = Val(.Rows(i).Cells("Dist Share").Value.ToString)

                    Using cmd As New MySqlCommand("pr_sta_ins_certsplitdistentry", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                        cmd.Parameters.AddWithValue("?in_row_sno", lnRowSNo)
                        cmd.Parameters.AddWithValue("?in_dist_from", lnDistFrom)
                        cmd.Parameters.AddWithValue("?in_dist_to", lnDistTo)
                        cmd.Parameters.AddWithValue("?in_dist_count", lnShareCount)

                        'Out put Para
                        cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                        cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                        cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                        cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                        cmd.CommandTimeout = 0

                        cmd.ExecuteNonQuery()
                        lsTxt = cmd.Parameters("?out_msg").Value.ToString()
                    End Using
                Next i
            End With

            If lnResult = 1 Then
                MessageBox.Show(lsMsg, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show(lsMsg, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Return lnResult
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return lnResult
        End Try
    End Function

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Dim lnResult As Long

        If txtRemark.Text = "" Then
            MessageBox.Show("Remark cannot be empty !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRemark.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to confirm action ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            If msGroupCode <> "I" Then
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueReject)
            Else
                lnResult = UpdateQueue(mnInwardId, msGroupCode, txtRemark.Text, gnQueueReprocess)
            End If
        End If

        If lnResult = 1 Then Me.Close()
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim frm As New frmDocHistory(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub lnkAddAttachment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAttachment.LinkClicked
        Dim frm As New frmAttachmentAdd(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub dgvChklst_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChklst.CellContentClick
        Dim i As Integer
        Dim lnChklstValue As Long
        Dim lnChklstValid As Long
        Dim lnChklstDisc As Long

        With dgvChklst
            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1, .Columns.Count - 2
                        .Rows(e.RowIndex).Cells(.Columns.Count - 1).Value = False
                        .Rows(e.RowIndex).Cells(.Columns.Count - 2).Value = False

                        .EndEdit()

                        For i = 0 To .Rows.Count - 1
                            lnChklstValue = .Rows(i).Cells("chklst_value").Value
                            mnChkLstAllStatus = mnChkLstAllStatus Or lnChklstValue

                            If .Rows(i).Cells(.Columns.Count - 2).Value = True Then
                                lnChklstValid = lnChklstValid Or lnChklstValue
                            Else
                                lnChklstDisc = lnChklstDisc Or lnChklstValue
                            End If
                        Next i

                        If mnChkLstAllStatus = lnChklstValid Then
                            lblDocStatus.Text = "Valid"
                            lblDocStatus.ForeColor = Color.DarkGreen
                        Else
                            lblDocStatus.Text = "Invalid"
                            lblDocStatus.ForeColor = Color.Red
                        End If
                End Select

            End If
        End With
    End Sub

    Private Sub dgvCert_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCert.CellContentClick
        Dim lnCertId As Long = 0
        Dim lsTxt As String = ""

        With dgvCert
            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1
                        .EndEdit()

                        lnCertId = .Rows(e.RowIndex).Cells("cert_gid").Value

                        If .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
                            lsTxt = GetInwardCertificate(mnInwardId, lnCertId)

                            If lsTxt <> "" Then
                                .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
                                MessageBox.Show("Certificate already mapped with Inward : " & lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If

                        Call RefreshShareCount()
                End Select
            End If
        End With
    End Sub

    Private Sub RefreshShareCount()
        Dim i As Integer
        Dim lnShareCount As Long = 0

        With dgvCert
            For i = 0 To .RowCount - 1
                If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                    lnShareCount += .Rows(i).Cells("Share Count").Value
                End If
            Next i

            lblShareCount.Text = lnShareCount.ToString()
        End With
    End Sub

    Private Sub btnSplit_Click(sender As Object, e As EventArgs) Handles btnSplit.Click
        Dim lnSelectShareCount As Long
        Dim lnSplitShareCount As Long
        Dim lsSplit As String = ""
        Dim lsCertDistList As String = ""
        Dim i As Integer

        Dim listDist As New List(Of clsDist)
        Dim listCompressDist As New List(Of clsDist)
        Dim lobjDist As clsDist
        Dim lobjTempDist As New clsDist
        Dim listNewDist As New List(Of clsDist)

        Dim lsTxt As String = ""
        Dim lnShareCount As Long
        Dim lnCertNo As Long
        Dim m As Long
        Dim n As Long

        Dim lnDistFrom As Long
        Dim lnDistTo As Long
        Dim lnDistCount As Long

        Dim lnNewCertNo As Long
        Dim lnNewDistFrom As Long
        Dim lnNewDistTo As Long
        Dim lnNewDistCount As Long
        Dim lbFlag As Boolean

        lsSplit = txtSplit.Text
        lsSplit = lsSplit.Replace("+", "")

        lnSelectShareCount = Val(lblShareCount.Text)
        lnSplitShareCount = evaluate(txtSplit.Text)

        If IsNumeric(lsSplit) = False Or lnSelectShareCount <> lnSplitShareCount Then
            MessageBox.Show("Discrepancy in Split !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSplit.Focus()
            Exit Sub
        End If

        If dgvSplit.RowCount > 0 Then
            MessageBox.Show("Please clear the list before spliting !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSplit.Focus()
            Exit Sub

        End If

        lsSplit = txtSplit.Text
        lsCertDistList = ""

        ' get certificate dist list
        With dgvCert
            For i = 0 To .RowCount - 1
                If .Rows(i).Cells(.ColumnCount - 1).Value = True Then
                    lsTxt = .Rows(i).Cells("Certificate No").Value
                    If lsCertDistList = "" Then
                        lsCertDistList = lsTxt & "-" & (.Rows(i).Cells("Dist Series").Value).ToString.Replace(",", "," & lsTxt & "-")
                    Else
                        lsCertDistList &= "," & lsTxt & "-" & (.Rows(i).Cells("Dist Series").Value).ToString.Replace(",", "," & lsTxt & "-")
                    End If
                End If
            Next i
        End With

        If lsCertDistList <> "" Then
            For i = 0 To lsCertDistList.Split(",").Length - 1
                lsTxt = lsCertDistList.Split(",")(i)

                If lsTxt.Split("-").Length = 3 Then
                    lnCertNo = Val(lsTxt.Split("-")(0))
                    m = Val(lsTxt.Split("-")(1))
                    n = Val(lsTxt.Split("-")(2))
                Else
                    lnCertNo = Val(lsTxt.Split("-")(0))
                    m = Val(lsTxt.Split("-")(1))
                    n = m
                End If

                lobjDist = New clsDist(lnCertNo, m, n, lsTxt, 0)
                listDist.Add(lobjDist)
            Next i

            listDist.Sort(Function(x As clsDist, y As clsDist)
                              Return x.DistFrom.CompareTo(y.DistFrom)
                          End Function)

            i = 0
            ' compress list
            For Each lobjDist In listDist
                If i = 0 Then
                    lobjTempDist = lobjDist
                Else
                    'If lobjTempDist.DistTo + 1 = lobjDist.DistFrom And lobjTempDist.CertNo = lobjDist.CertNo Then
                    If lobjTempDist.DistTo + 1 = lobjDist.DistFrom Then
                        lobjTempDist.DistTo = lobjDist.DistTo
                    Else
                        listCompressDist.Add(lobjTempDist)
                        lobjTempDist = lobjDist
                    End If
                End If

                i += 1
            Next

            If i > 0 Then
                listCompressDist.Add(lobjTempDist)
            End If
        End If

        fobjDTSplit.Rows.Clear()

        For i = 0 To lsSplit.Split("+").Length - 1
            fobjRow = fobjDTSplit.NewRow

            fobjRow.Item("SNo") = (i + 1).ToString
            fobjRow.Item("Share Count") = lsSplit.Split("+")(i)
            fobjDTSplit.Rows.Add(fobjRow)
        Next i

        dgvSplit.DataSource = fobjDTSplit

        i = 0

        lnNewDistFrom = 0
        lnNewDistTo = 0
        lnNewDistCount = 0

        lnShareCount = Val(lblShareCount.Text)
        lbFlag = False

        ' compress list
        For Each lobjDist In listCompressDist
            lnCertNo = lobjDist.CertNo
            lnDistFrom = lobjDist.DistFrom
            lnDistTo = lobjDist.DistTo

            lnDistCount = lnDistTo - lnDistFrom + 1
            lbFlag = True

            Do
                If lnNewDistCount = 0 Then
                    lnNewCertNo = dgvSplit.Rows(i).Cells("SNo").Value
                    lnNewDistCount = dgvSplit.Rows(i).Cells("Share Count").Value

                    i += 1
                End If

                If lnNewDistCount > lnDistCount Then
                    lnNewDistFrom = lnDistFrom
                    lnNewDistTo = lnDistTo

                    lnNewDistCount = lnNewDistCount - lnDistCount
                    lsTxt = lnNewDistFrom.ToString & "-" & lnNewDistTo.ToString

                    lobjDist = New clsDist(lnNewCertNo, lnNewDistFrom, lnNewDistTo, lsTxt, lnCertNo)
                    listNewDist.Add(lobjDist)

                    lbFlag = False
                ElseIf lnNewDistCount <= lnDistCount Then
                    lnNewDistFrom = lnDistFrom
                    lnNewDistTo = lnDistFrom + lnNewDistCount - 1

                    lsTxt = lnNewDistFrom.ToString & "-" & lnNewDistTo.ToString

                    lobjDist = New clsDist(lnNewCertNo, lnNewDistFrom, lnNewDistTo, lsTxt, lnCertNo)
                    listNewDist.Add(lobjDist)

                    lnDistFrom = lnDistFrom + lnNewDistCount
                    lnDistCount = lnDistTo - lnDistFrom + 1

                    If lnDistCount = 0 Then lbFlag = False
                    lnNewDistCount = 0
                End If
            Loop Until lbFlag = False
        Next

        fobjDTSplitDist.Rows.Clear()

        ' compress list
        For Each lobjDist In listNewDist
            fobjRow = fobjDTSplitDist.NewRow

            fobjRow.Item("Cert SNo") = lobjDist.CertNo
            fobjRow.Item("Dist Share") = lobjDist.DistTo - lobjDist.DistFrom + 1
            fobjRow.Item("Dist From") = lobjDist.DistFrom
            fobjRow.Item("Dist To") = lobjDist.DistTo
            fobjDTSplitDist.Rows.Add(fobjRow)

            dgvSplitDist.DataSource = fobjDTSplitDist
        Next
    End Sub

    Private Class clsDist
        Public CertNo As Long
        Public OldCertNo As Long
        Public DistFrom As Long
        Public DistTo As Long
        Public DistTxt As String

        Public Sub New()
        End Sub

        Public Sub New(_CertNo As Long, _DistFrom As Long, _DistTo As Long, _DistTxt As String, _OldCertNo As Long)
            CertNo = _CertNo
            DistFrom = _DistFrom
            DistTo = _DistTo
            DistTxt = _DistTxt
            OldCertNo = _OldCertNo
        End Sub
    End Class

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        fobjDTSplit.Rows.Clear()
        dgvSplit.DataSource = fobjDTSplit

        fobjDTSplitDist.Rows.Clear()
        dgvSplitDist.DataSource = fobjDTSplitDist
    End Sub

    Private Sub dgvCert_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCert.CellContentDoubleClick
        Dim lnCertId As Long
        Dim frm As frmCertificateView

        Try
            lnCertId = Val(dgvCert.Rows(e.RowIndex).Cells("cert_gid").Value.ToString)

            frm = New frmCertificateView(lnCertId)
            frm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCertNo_TextChanged(sender As Object, e As EventArgs) Handles txtCertNo.TextChanged
        Dim i As Integer

        With dgvCert
            If txtCertNo.Text <> "" Then
                .ClearSelection()

                For i = 0 To .Rows.Count - 1
                    If InStr(.Rows(i).Cells("Certificate No").Value, txtCertNo.Text) > 0 Then
                        .FirstDisplayedScrollingRowIndex = i
                        .Rows(i).Selected = True
                        Exit For
                    End If
                Next i
            End If
        End With
    End Sub
End Class