Imports MySql.Data.MySqlClient

Public Class frmFolioView
    Dim mnFolioId As Long

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadData()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_folio_gid", mnFolioId)
        cmd.Parameters.AddWithValue("?in_comp_gid", 0)
        cmd.Parameters.AddWithValue("?in_folio_no", "")

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                txtCompName.Text = .Rows(0).Item("comp_name").ToString
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString

                txtCategory.Text = .Rows(0).Item("category_name").ToString

                If .Rows(0).Item("repatrition_flag").ToString() = "Y" Then
                    rdoYes.Checked = True
                Else
                    rdoNo.Checked = True
                End If

                txtHolder1.Text = .Rows(0).Item("holder1_name").ToString
                txtFHName1.Text = .Rows(0).Item("holder1_fh_name").ToString
                txtPan1.Text = .Rows(0).Item("holder1_pan_no").ToString

                txtHolder2.Text = .Rows(0).Item("holder2_name").ToString
                txtFHName2.Text = .Rows(0).Item("holder2_fh_name").ToString
                txtPan2.Text = .Rows(0).Item("holder2_pan_no").ToString

                txtHolder3.Text = .Rows(0).Item("holder3_name").ToString
                txtFHName3.Text = .Rows(0).Item("holder3_fh_name").ToString
                txtPan3.Text = .Rows(0).Item("holder3_pan_no").ToString

                txtAddr1.Text = .Rows(0).Item("folio_addr1").ToString
                txtAddr2.Text = .Rows(0).Item("folio_addr2").ToString
                txtAddr3.Text = .Rows(0).Item("folio_addr3").ToString
                txtCity.Text = .Rows(0).Item("folio_city").ToString
                txtState.Text = .Rows(0).Item("folio_state").ToString
                txtCountry.Text = .Rows(0).Item("folio_country").ToString
                txtPincode.Text = .Rows(0).Item("folio_pincode").ToString
                txtPhoneNo.Text = .Rows(0).Item("folio_contact_no").ToString
                txtEmailId.Text = .Rows(0).Item("folio_mail_id").ToString

                txtNomineeRegNo.Text = .Rows(0).Item("nominee_reg_no").ToString
                txtNomineeName.Text = .Rows(0).Item("nominee_name").ToString
                txtNomineeAddr1.Text = .Rows(0).Item("nominee_addr1").ToString
                txtNomineeAddr2.Text = .Rows(0).Item("nominee_addr2").ToString
                txtNomineeAddr3.Text = .Rows(0).Item("nominee_addr3").ToString
                txtNomineeCity.Text = .Rows(0).Item("nominee_city").ToString
                txtNomineeState.Text = .Rows(0).Item("nominee_state").ToString
                txtNomineeCountry.Text = .Rows(0).Item("nominee_country").ToString
                txtNomineePincode.Text = .Rows(0).Item("nominee_pincode").ToString

                txtBankName.Text = .Rows(0).Item("bank_name").ToString
                txtAccNo.Text = .Rows(0).Item("bank_acc_no").ToString
                txtIfscCode.Text = .Rows(0).Item("bank_ifsc_code").ToString
                txtMicrCode.Text = .Rows(0).Item("bank_micr_code").ToString
                txtBranchName.Text = .Rows(0).Item("bank_branch").ToString
                txtBeneficiaryName.Text = .Rows(0).Item("bank_beneficiary").ToString
                txtAccType.Text = .Rows(0).Item("bank_acc_type").ToString
                txtBranchAddr.Text = .Rows(0).Item("bank_branch_addr").ToString
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

    Private Sub frmFolioView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
End Class