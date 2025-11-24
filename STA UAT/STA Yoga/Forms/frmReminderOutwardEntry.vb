Imports MySql.Data.MySqlClient

Public Class frmReminderOutwardEntry
    Dim msMode As String
    Dim mnLocreminderId As Long
    Dim mnInwardId As Long

    Public Sub New(Mode As String, Optional InwardId As Long = 0, Optional LocreminderId As Long = 0)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        msMode = Mode
        mnInwardId = InwardId
        mnLocreminderId = LocreminderId
    End Sub

    Private Sub ListAll(ByVal LocReminderId As Long)
        Dim lsSql As String
        Dim lnUploadStatus As Integer
        'Dim lobjDataReader As MySqlDataReader

        Try

            lsSql = ""
            lsSql &= " select "
            lsSql &= " i.inward_gid,i.inward_comp_no as 'inward_no',i.tran_code,"
            lsSql &= " i.update_completed,"
            lsSql &= " l.locrem_gid,ifnull(l.reminder_sent_date,'0000-00-00') as reminder_sent_date,l.reminder_mode,"
            lsSql &= " l.courier_gid,l.awb_no,l.remark,"
            lsSql &= " i.update_completed,i.upload_gid,u.upload_status "
            lsSql &= " from sta_trn_tinward as i "
            lsSql &= " left join sta_trn_toutward as o on o.inward_gid = i.inward_gid and o.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tupload as u on i.upload_gid = u.upload_gid and u.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tlocreminder as l on l.inward_gid = i.inward_gid and l.status = 'Yet to sent' and l.delete_flag = 'N' "
            lsSql &= " where l.locrem_gid = " & LocReminderId & " "
            lsSql &= " and i.delete_flag = 'N' "

            'lobjDataReader = gfExecuteQry(lsSql, gOdbcConn)

            cboCourier.SelectedIndex = -1
            cboDocType.SelectedIndex = -1
            cboOutwardMode.SelectedIndex = -1

            'With lobjDataReader
            '    If .HasRows Then
            '        If .Read Then
            '            txtId.Text = .Item("locrem_gid").ToString
            '            txtInwardNo.Text = .Item("inward_no").ToString
            '            cboDocType.SelectedValue = .Item("tran_code").ToString
            '            cboOutwardMode.SelectedValue = .Item("reminder_mode").ToString

            '            If IsDBNull(.Item("reminder_sent_date") Or .Item("reminder_sent_date") = "0000-00-00") Then
            '                dtpOutwardDate.Value = Now
            '            Else
            '                dtpOutwardDate.Value = .Item("reminder_sent_date")
            '            End If

            '            cboCourier.SelectedValue = Val(.Item("courier_gid").ToString)
            '            txtAwbNo.Text = .Item("awb_no").ToString
            '            txtRemark.Text = .Item("remark").ToString
            '            lnUploadStatus = Val(.Item("upload_status").ToString)

            '        End If
            '    End If

            '    .Close()
            'End With

            Using lobjDataReader As MySqlDataReader = gfExecuteQry(lsSql, gOdbcConn)
                If lobjDataReader.HasRows Then
                    If lobjDataReader.Read() Then
                        txtId.Text = lobjDataReader("locrem_gid").ToString()
                        txtInwardNo.Text = lobjDataReader("inward_no").ToString()
                        cboDocType.SelectedValue = lobjDataReader("tran_code").ToString()
                        cboOutwardMode.SelectedValue = lobjDataReader("reminder_mode").ToString()

                        Dim reminderDate As Object = lobjDataReader("reminder_sent_date")

                        If IsDBNull(reminderDate) OrElse reminderDate.ToString() = "0000-00-00" OrElse reminderDate.ToString() = "0000-00-00 00:00:00" Then
                            dtpOutwardDate.Value = Now
                        Else
                            dtpOutwardDate.Value = Convert.ToDateTime(reminderDate)
                        End If

                        cboCourier.SelectedValue = Val(lobjDataReader("courier_gid").ToString())
                        txtAwbNo.Text = lobjDataReader("awb_no").ToString()
                        txtRemark.Text = lobjDataReader("remark").ToString()
                        lnUploadStatus = Val(lobjDataReader("upload_status").ToString())
                    End If
                End If
            End Using


            Call gpAutoFillCombo(cboDocType)
            Call gpAutoFillCombo(cboOutwardMode)
            Call gpAutoFillCombo(cboCourier)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub frmReminderOutwardEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        If mnLocreminderId > 0 Then
            Call ListAll(mnLocreminderId)
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnLocreminderId As Long
        Dim lsOutwardMode As String
        Dim ldOutwardDate As Date
        Dim lnCourierId As Long
        Dim lsAwbNo As String
        Dim lsRemark As String
        Dim lsAction As String

        Try
            lnLocreminderId = Val(txtId.Text)

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

            If lnLocreminderId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_trn_tlocreminder", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_locrem_gid", lnLocreminderId)
                cmd.Parameters.AddWithValue("?in_inward_gid", mnInwardId)
                cmd.Parameters.AddWithValue("?in_reminder_sent_date", Format(ldOutwardDate, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("?in_reminder_mode", lsOutwardMode)
                cmd.Parameters.AddWithValue("?in_courier_gid", lnCourierId)
                cmd.Parameters.AddWithValue("?in_awb_no", lsAwbNo)
                cmd.Parameters.AddWithValue("?in_remark", lsRemark)
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

    Private Sub lnkAddAttachment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAttachment.LinkClicked
        Dim frm As New frmAttachmentAdd(mnInwardId, True)
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class