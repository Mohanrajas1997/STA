Imports MySql.Data.MySqlClient

Public Class frmFolioPanUpdate
    Dim mnFolioId As Long

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If MessageBox.Show("Are you sure to submit ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If _
                    txtPanNo1.Text = txtPanNo1.Tag And _
                    txtPanNo2.Text = txtPanNo2.Tag And _
                    txtPanNo3.Text = txtPanNo3.Tag Then
                MessageBox.Show("No change in pan !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Call UpdateInformation()
        End If
    End Sub

    Private Sub UpdateInformation()
        Dim lnResult As Long
        Dim lsTxt As String

        Dim lsPanNo1 As String = ""
        Dim lsPanNo2 As String = ""
        Dim lsPanNo3 As String = ""

        Try
            lsPanNo1 = QuoteFilter(txtPanNo1.Text)
            lsPanNo2 = QuoteFilter(txtPanNo2.Text)
            lsPanNo3 = QuoteFilter(txtPanNo3.Text)

            Using cmd As New MySqlCommand("pr_sta_upd_foliopan", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_folio_gid", mnFolioId)
                cmd.Parameters.AddWithValue("?in_holder1_pan_no", lsPanNo1)
                cmd.Parameters.AddWithValue("?in_holder2_pan_no", lsPanNo2)
                cmd.Parameters.AddWithValue("?in_holder3_pan_no", lsPanNo3)

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
                    Me.Close()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmFolioCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadFolio(mnFolioId)
    End Sub

    Private Sub LoadFolio(FolioId)
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_folio_gid", FolioId)
        cmd.Parameters.AddWithValue("?in_comp_gid", 0)
        cmd.Parameters.AddWithValue("?in_folio_no", "")

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                txtHolder1.Text = .Rows(0).Item("holder1_name").ToString
                txtFHName1.Text = .Rows(0).Item("holder1_fh_name").ToString
                txtPanNo1.Text = .Rows(0).Item("holder1_pan_no").ToString

                txtHolder2.Text = .Rows(0).Item("holder2_name").ToString
                txtFHName2.Text = .Rows(0).Item("holder2_fh_name").ToString
                txtPanNo2.Text = .Rows(0).Item("holder2_pan_no").ToString

                txtHolder3.Text = .Rows(0).Item("holder3_name").ToString
                txtFHName3.Text = .Rows(0).Item("holder3_fh_name").ToString
                txtPanNo3.Text = .Rows(0).Item("holder3_pan_no").ToString

                ' store existing data
                txtHolder1.Tag = .Rows(0).Item("holder1_name").ToString
                txtFHName1.Tag = .Rows(0).Item("holder1_fh_name").ToString
                txtPanNo1.Tag = .Rows(0).Item("holder1_pan_no").ToString

                txtHolder2.Tag = .Rows(0).Item("holder2_name").ToString
                txtFHName2.Tag = .Rows(0).Item("holder2_fh_name").ToString
                txtPanNo2.Tag = .Rows(0).Item("holder2_pan_no").ToString

                txtHolder3.Tag = .Rows(0).Item("holder3_name").ToString
                txtFHName3.Tag = .Rows(0).Item("holder3_fh_name").ToString
                txtPanNo3.Tag = .Rows(0).Item("holder3_pan_no").ToString

                txtAddr1.Text = .Rows(0).Item("folio_addr1").ToString
                txtAddr2.Text = .Rows(0).Item("folio_addr2").ToString
                txtAddr3.Text = .Rows(0).Item("folio_addr3").ToString
                txtCity.Text = .Rows(0).Item("folio_city").ToString
                txtState.Text = .Rows(0).Item("folio_state").ToString
                txtCountry.Text = .Rows(0).Item("folio_country").ToString
                txtPincode.Text = .Rows(0).Item("folio_pincode").ToString
                txtPhoneNo.Text = .Rows(0).Item("folio_contact_no").ToString
                txtEmailId.Text = .Rows(0).Item("folio_mail_id").ToString

                grpFolio.Text = "Folio : " & .Rows(0).Item("folio_no").ToString & "  -  Company : " & .Rows(0).Item("comp_name").ToString
            Else
                Call frmCtrClear(Me)
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()
    End Sub

    Public Sub New(FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub
End Class