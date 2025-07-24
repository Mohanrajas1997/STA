Imports MySql.Data.MySqlClient

Public Class frmOutwardEntry
    Dim msMode As String
    Dim mnInwardId As Long

    Public Sub New(Mode As String, Optional InwardId As Long = 0)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        msMode = Mode
        mnInwardId = InwardId

        Select Case msMode.ToUpper()
            Case "ADD"
                btnSave.Visible = True
                btnReject.Visible = True
            Case "VIEW"
                btnSave.Visible = False
                btnReject.Visible = False
        End Select
    End Sub

    Private Sub ListAll(ByVal InwardId As Long)
        Dim lsSql As String
        Dim lnUploadStatus As Integer
        Dim lobjDataReader As MySqlDataReader

        Try
            lsSql = ""
            lsSql &= " select "
            lsSql &= " i.inward_gid,i.inward_comp_no as 'inward_no',i.tran_code,"
            lsSql &= " i.update_completed,"
            lsSql &= " o.outward_gid,o.outward_date,o.outward_mode,"
            lsSql &= " o.courier_gid,o.awb_no,o.outward_remark,"
            lsSql &= " i.update_completed,i.upload_gid,u.upload_status "
            lsSql &= " from sta_trn_tinward as i "
            lsSql &= " left join sta_trn_toutward as o on o.inward_gid = i.inward_gid and o.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as u on i.upload_gid = u.upload_gid and u.delete_flag = 'N' "
            lsSql &= " where i.inward_gid = " & mnInwardId & " "
            lsSql &= " and i.delete_flag = 'N' "

            lobjDataReader = gfExecuteQry(lsSql, gOdbcConn)

            cboCourier.SelectedIndex = -1
            cboDocType.SelectedIndex = -1
            cboOutwardMode.SelectedIndex = -1

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("outward_gid").ToString
                        txtInwardNo.Text = .Item("inward_no").ToString
                        cboDocType.SelectedValue = .Item("tran_code").ToString
                        cboOutwardMode.SelectedValue = .Item("outward_mode").ToString

                        If IsDBNull(.Item("outward_date")) Then
                            dtpOutwardDate.Value = Now
                        Else
                            dtpOutwardDate.Value = .Item("outward_date")
                        End If

                        cboCourier.SelectedValue = Val(.Item("courier_gid").ToString)
                        txtAwbNo.Text = .Item("awb_no").ToString
                        txtRemark.Text = .Item("outward_remark").ToString
                        lnUploadStatus = Val(.Item("upload_status").ToString)

                        If .Item("update_completed").ToString = "Y" Then
                            btnReject.Enabled = False
                        End If

                        If lnUploadStatus > 0 And lnUploadStatus <> gnUploadStatusDone Then
                            btnReject.Enabled = False
                        End If
                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(cboDocType)
            Call gpAutoFillCombo(cboOutwardMode)
            Call gpAutoFillCombo(cboCourier)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmInwardEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmBankMater_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        'e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        ' received mode
        lsSql = ""
        lsSql &= " select receivedmode_code,receivedmode_desc from sta_mst_treceivedmode "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by receivedmode_desc asc "

        Call gpBindCombo(lsSql, "receivedmode_desc", "receivedmode_code", cboOutwardMode, gOdbcConn)

        ' doc type
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "

        Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

        ' courier
        lsSql = ""
        lsSql &= " select courier_gid,courier_name from sta_mst_tcourier "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by courier_name asc "

        Call gpBindCombo(lsSql, "courier_name", "courier_gid", cboCourier, gOdbcConn)

        dtpOutwardDate.Value = Now

        Call ClearControl()

        If mnInwardId > 0 Then
            Call ListAll(mnInwardId)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnOutwardId As Long
        Dim lsOutwardMode As String
        Dim ldOutwardDate As Date
        Dim lnCourierId As Long
        Dim lsAwbNo As String
        Dim lsRemark As String
        Dim lsAction As String

        Try
            lnOutwardId = Val(txtId.Text)

            ldOutwardDate = dtpOutwardDate.Value

            If cboCourier.SelectedIndex <> -1 Then
                lnCourierId = Val(cboCourier.SelectedValue.ToString)
            Else
                lnCourierId = 0
            End If

            If cboOutwardMode.SelectedIndex <> -1 Then
                lsOutwardMode = cboOutwardMode.SelectedValue.ToString
            Else
                lsOutwardMode = ""
            End If

            lsAwbNo = QuoteFilter(txtAwbNo.Text)

            lsRemark = QuoteFilter(txtRemark.Text)

            If lnOutwardId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_trn_toutward", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_outward_gid", lnOutwardId)
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_outward_date", Format(ldOutwardDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_outward_mode", lsOutwardMode)
                cmd.Parameters.AddWithValue("?in_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_awb_no", lsAwbNo)
                cmd.Parameters.AddWithValue("?in_outward_remark", lsRemark)
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
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using

            Call ClearControl()
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtInwardNo.Focus()
    End Sub

    Private Sub txtRemark_GotFocus(sender As Object, e As EventArgs) Handles txtRemark.GotFocus
        KeyPreview = False
    End Sub

    Private Sub txtRemark_LostFocus(sender As Object, e As EventArgs) Handles txtRemark.LostFocus
        KeyPreview = True
    End Sub

    Private Sub txtRemark_TextChanged(sender As Object, e As EventArgs) Handles txtRemark.TextChanged

    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim frm As New frmDocHistory(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Dim lsSql As String
        Dim ds As New DataSet
        Dim lnUploadStatus As Integer

        If MsgBox("Are you sure to reject?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        lsSql = ""
        lsSql &= " select "
        lsSql &= " u.upload_status "
        lsSql &= " from sta_trn_tinward as i "
        lsSql &= " inner join sta_trn_tqueue as q on i.queue_gid = q.queue_gid and q.delete_flag = 'N' "
        lsSql &= " left join sta_trn_tupload as u on i.upload_gid = u.upload_gid and u.delete_flag = 'N' "
        lsSql &= " where i.inward_gid = " & mnInwardId & " "
        lsSql &= " and i.delete_flag = 'N' "

        Call gpDataSet(lsSql, "queue", gOdbcConn, ds)

        With ds.Tables("queue")
            If .Rows.Count > 0 Then
                lnUploadStatus = Val(gfExecuteScalar(lsSql, gOdbcConn))

                If lnUploadStatus <> 0 And lnUploadStatus <> gnUploadStatusDone Then
                    MessageBox.Show("Document revoke is denied !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtRemark.Focus()
                    Exit Sub
                End If

                If txtRemark.Text = "" Then
                    MessageBox.Show("Remark cannot be empty !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtRemark.Focus()
                    Exit Sub
                End If

                Call UpdateQueue(mnInwardId, "D", txtRemark.Text, gnQueueReject)
            End If

            .Rows.Clear()
        End With

    End Sub

    Private Sub lnkAddAttachment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAttachment.LinkClicked
        Dim frm As New frmAttachmentAdd(mnInwardId, True)
        frm.ShowDialog()
    End Sub
End Class