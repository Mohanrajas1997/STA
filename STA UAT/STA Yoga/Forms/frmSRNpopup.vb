Imports MySql.Data.MySqlClient

Public Class frmSRNpopup
    Public Property InwardGid As Long
    Public Property InwardCompNo As String
    Public Property FolioGid As String
    Public Property SRNNo As String
    Public Property iepfsrndate As Date?
    Public Property UpdateResultMessage As String

    Private Sub frmSRNpopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        srnno_txt.Text = SRNNo
        'srndate.Value = iepfsrndate
        If iepfsrndate.HasValue Then
            srndate.Value = iepfsrndate.Value
        Else
            srndate.Value = DateTime.Today  ' or any default you want
        End If
    End Sub

    Private Sub save_btn_Click(sender As Object, e As EventArgs) Handles save_btn.Click
        If String.IsNullOrWhiteSpace(srnno_txt.Text) Then
            MessageBox.Show("SRN No cannot be blank.")
            srnno_txt.Focus()
            Return
        End If

        SRNNo = srnno_txt.Text.Trim()
        iepfsrndate = srndate.Value

        ' Call update SP here
        Using cmdUpd As New MySqlCommand("pr_upd_iepfclaimsrn", gOdbcConn)
            cmdUpd.CommandType = CommandType.StoredProcedure
            cmdUpd.Parameters.AddWithValue("in_inward_gid", InwardGid)
            cmdUpd.Parameters.AddWithValue("in_folio_gid", FolioGid)
            cmdUpd.Parameters.AddWithValue("in_req_srn_no", SRNNo)
            cmdUpd.Parameters.AddWithValue("in_req_srn_date", iepfsrndate)

            Using rdrUpd As MySqlDataReader = cmdUpd.ExecuteReader()
                If rdrUpd.Read() Then
                    'MessageBox.Show(rdrUpd("result").ToString())
                    UpdateResultMessage = rdrUpd("result").ToString()
                End If
            End Using

        End Using

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cancel_btn_Click(sender As Object, e As EventArgs) Handles cancel_btn.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class