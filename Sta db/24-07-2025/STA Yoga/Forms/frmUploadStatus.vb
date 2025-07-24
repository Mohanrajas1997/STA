Imports MySql.Data.MySqlClient

Public Class frmUploadStatus
    Dim mnUploadId As Long
    Dim mnUploadStatus As Integer

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmUploadStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadData()

        dtpApprovedDate.Value = Now
    End Sub

    Public Sub New(UploadId As Long, UploadStatus As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnUploadId = UploadId
        mnUploadStatus = UploadStatus

        Select Case UploadStatus
            Case gnUploadStatusSuccess
                btnUpdate.Text = "Success"
            Case gnUploadStatusFailure
                btnUpdate.Text = "Failure"
        End Select
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String


        lsSql = ""
        lsSql &= " select a.*,b.comp_name "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " left join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= " and a.upload_gid = " & mnUploadId & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        With ds.Tables("upload")
            If .Rows.Count = 1 Then
                txtUploadNo.Text = .Rows(0).Item("upload_no").ToString
                txtUploadDate.Text = Format(.Rows(0).Item("upload_date"), "dd-MM-yyyy")
                txtCompName.Text = .Rows(0).Item("comp_name").ToString

                txtTranCount.Text = .Rows(0).Item("transfer_count").ToString
                txtTranFrom.Text = .Rows(0).Item("transfer_start_sno").ToString
                txtTranTo.Text = .Rows(0).Item("transfer_end_sno").ToString

                txtCertCount.Text = .Rows(0).Item("cert_count").ToString
                txtCertFrom.Text = .Rows(0).Item("cert_start_sno").ToString
                txtCertTo.Text = .Rows(0).Item("cert_end_sno").ToString

                txtObjxCount.Text = .Rows(0).Item("objx_count").ToString
                txtObjxFrom.Text = .Rows(0).Item("objx_start_sno").ToString
                txtObjxTo.Text = .Rows(0).Item("objx_end_sno").ToString

                txtRemark.Text = .Rows(0).Item("update_remark").ToString
            End If
        End With
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim lnResult As Long

        If MessageBox.Show("Are you sure to update ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            lnResult = UpdateUpload(mnUploadId, Format(dtpApprovedDate.Value, "yyyy-MM-dd"), mnUploadStatus)

            If lnResult = 1 Then
                Me.Close()
            End If
        End If
    End Sub

    Private Function UpdateUpload(UploadId As Long, ApprovedDate As String, UploadStatus As Integer) As Long
        Dim lnResult As Long = 0
        Dim lsTxt As String
        Dim lsRemark As String

        Try
            lsRemark = QuoteFilter(txtRemark.Text)


            Using cmd As New MySqlCommand("pr_sta_set_uploadstatus", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_upload_gid", UploadId)
                cmd.Parameters.AddWithValue("?in_approved_date", ApprovedDate)
                cmd.Parameters.AddWithValue("?in_upload_status", UploadStatus)
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
                End If

                Return lnResult
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return lnResult
        End Try
    End Function

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim frm As frmUploadView

        frm = New frmUploadView(mnUploadId, txtCompName.Text)
        frm.ShowDialog()
    End Sub
End Class