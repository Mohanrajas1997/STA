Imports MySql.Data.MySqlClient
Public Class frmBeneficiaryDetails
    Dim msMode As String
    Dim mnInwardId As Long
    Dim mbGenerateInwardNo As Boolean = True

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String
        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "
        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

      
        Call ClearControl()
        Call EnableSave(False)
        btnNew.Focus()

    End Sub


    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnBeneGid As Long
        Dim lnCompId As Long
        Dim lsPanNo As String
        Dim lsFolioGid As Long
        Dim lsBeneName As String
        Dim lsAction As String

        Try
            If cboCompany.SelectedIndex <> -1 Then
                lnCompId = Val(cboCompany.SelectedValue.ToString)
            Else
                lnCompId = 0
            End If

            If CboFolio_No.SelectedIndex <> -1 Then
                lsFolioGid = Val(CboFolio_No.SelectedValue.ToString)
            Else
                lsFolioGid = 0
            End If

            lsBeneName = QuoteFilter(txtBeneName.Text)
            lsPanNo = QuoteFilter(TxtPanNo.Text)

            lnBeneGid = Val(txtId.Text)

            If lnBeneGid = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_ins_beneficiarydetails", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_bene_gid", lnBeneGid)
                cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
                cmd.Parameters.AddWithValue("?in_folio_gid", lsFolioGid)
                cmd.Parameters.AddWithValue("?in_beneficiary_name", lsBeneName)
                cmd.Parameters.AddWithValue("?in_pan_no", lsPanNo)
                cmd.Parameters.AddWithValue("?in_entry_by", gsLoginUserCode)
                cmd.Parameters.AddWithValue("?in_action", lsAction)

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
            EnableSave(False)
            btnNew.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
    Private Sub ClearControl()
        Call frmCtrClear(Me)
        cboCompany.Focus()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        Call frmCtrClear(Me)
    End Sub

 
    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        EnableSave(True)
        Call ClearControl()
        cboCompany.Focus()
    End Sub
    Private Sub EnableSave(ByVal Status As Boolean)
        pnlnew.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCompany.SelectedIndexChanged
        Dim lnCompId As Long

        If cboCompany.SelectedIndex <> -1 Then
            lnCompId = Val(cboCompany.SelectedValue.ToString)
        End If

        If lnCompId > 0 Then
            If Val(txtId.Text) = 0 Then
                Dim lsSql As String
                ' folio
                lsSql = ""
                lsSql &= " select folio_gid,folio_no from sta_trn_tfolio "
                lsSql &= " where comp_gid = " & lnCompId & " "
                lsSql &= " and delete_flag = 'N' "
                lsSql &= " order by folio_no asc "
                Call gpBindCombo(lsSql, "folio_no", "folio_gid", CboFolio_No, gOdbcConn)
            End If
        End If
    End Sub

    Private Sub btnFind_Click(sender As System.Object, e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, _
       " select a.bene_gid as 'Beneficiary Gid'," & _
       " b.comp_name as 'Company Name',c.folio_no as 'Folio no',a.bene_name as 'Beneficiary Name',a.pan_no as 'Pan No' from sta_trn_tfoliobeneficiary a , sta_mst_tcompany b, sta_trn_tfolio c ", _
       "  a.bene_gid,b.comp_name,c.folio_no,a.bene_name,a.pan_no ", _
       " a.comp_gid=b.comp_gid and a.folio_gid=c.folio_gid and c.delete_flag='N' and a.delete_flag='N'  ")


            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_trn_tfoliobeneficiary " _
                    & " where bene_gid = " & gnSearchId & " " _
                    & " and delete_flag = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySql.Data.MySqlClient.MySqlConnection)
        Dim lobjDataReader As MySqlDataReader

        Dim foliogid As String
        Dim lnCompgid As String

        Try
         
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            cboCompany.SelectedIndex = -1
            CboFolio_No.SelectedIndex = -1



            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("bene_gid").ToString
                        cboCompany.SelectedValue = .Item("comp_gid").ToString
                        lnCompgid = .Item("comp_gid").ToString
                        foliogid = .Item("folio_gid").ToString
                        ' CboFolio_No.SelectedValue = .Item("folio_gid").ToString
                        txtBeneName.Text = .Item("bene_name").ToString
                        TxtPanNo.Text = .Item("pan_no").ToString
                    End If
                End If

                .Close()
            End With

            Dim lsSql As String
            ' company
            lsSql = ""
            lsSql &= " select folio_gid,folio_no from sta_trn_tfolio "
            lsSql &= " where comp_gid = " & lnCompgid & " "
            lsSql &= " and delete_flag = 'N' "
            lsSql &= " order by folio_no asc "
            Call gpBindCombo(lsSql, "folio_no", "folio_gid", CboFolio_No, gOdbcConn)

            CboFolio_No.SelectedValue = foliogid.ToString

            Call gpAutoFillCombo(cboCompany)
            Call gpAutoFillCombo(CboFolio_No)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click
        Try
            If txtId.Text = "" Then
                If MsgBox("Select Record to edit", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                    EnableSave(False)
                End If
            Else
                EnableSave(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click_1(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        EnableSave(False)
        ClearControl()
        btnNew.Focus()
    End Sub

End Class