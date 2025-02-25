Imports MySql.Data.MySqlClient
Public Class frmShareholdingpatterninfo
    Inherits System.Windows.Forms.Form

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlnew.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub
    Private Sub ClearControl()
        Call frmCtrClear(Me)
    End Sub

    Private Sub frmShareholdingpatterninfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", Cbocompany, gOdbcConn)

        ' Client Type
        lsSql = ""
        lsSql &= " select folioclient_type,folioclient_desc from sta_mst_tfolioclienttype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by folioclienttype_gid asc "

        Call gpBindCombo(lsSql, "folioclient_desc", "folioclient_type", cboclienttype, gOdbcConn)

        'Category 
        lsSql = ""
        lsSql &= " select 1 as category_id, 'Indian Promoter' as category_name Union "
        lsSql &= " select 2 as category_id, 'Foriegn Promoter' as category_name "


        Call gpBindCombo(lsSql, "category_name", "category_id", cboCategory, gOdbcConn)




        'lsSql = ""
        'lsSql &= " select 1 as Categorytype_id,'Indian Promoter' as CategoryName Union"
        'lsSql &= " select 2 as Categorytype_id,'Foriegn Promoter' as CategoryName Union"
        'lsSql &= " select 3 as Categorytype_id,'Public Institutions' as CategoryName Union"
        'lsSql &= " select 4 as Categorytype_id,'Public Non Institutions' as CategoryName Union"
        'lsSql &= " select 5 as Categorytype_id,'Non Public' as CategoryName "

        'Call gpBindCombo(lsSql, "CategoryName", "Categorytype_id", cboCategoryType, gOdbcConn)

        Call EnableSave(False)
        btnNew.Focus()

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        EnableSave(True)
        Call ClearControl()
        Cbocompany.Focus()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
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

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, _
       " select a.promoter_gid as 'Promoter Gid'," & _
       " b.comp_name as 'Company Name',a.promo_category_id as 'Category ID',a.promo_category_name as 'Category Name',a.promo_categorytype_id as 'CategoryTypeID',a.promo_categorytype_name as 'CategoryTypeName',a.folioclient_dp_id as 'DP ID',a.folioclient_id as 'Client ID'   from sta_mst_tpromoterdetailinfo a , sta_mst_tcompany b ", _
       "  a.promoter_gid,b.comp_name,a.promo_category_id,a.promo_category_name,a.promo_categorytype_id,a.promo_categorytype_name,a.folioclient_dp_id,a.folioclient_id ", _
       " a.comp_gid=b.comp_gid and a.delete_flag='N'  ")


            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tpromoterdetailinfo " _
                    & " where promoter_gid = " & gnSearchId & " " _
                    & " and delete_flag = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnpromotergid As Long
        Dim lncompid As Long
        Dim lnclientid As String
        Dim lnclientdpid As String
        Dim lnclientidtype As String
        Dim lnPAN As String
        Dim lnDIN As String
        Dim lnCategory As String
        Dim lnBenCategory As String
        Dim lsAction As String
        Dim lnpcategoryid As Integer, lnpcategorytypeid As Integer
        Dim lnpcategoryname As String, lnpcategorytypename As String

        Try
            If txtId.Text = "" Then
                If MsgBox("Select record to delete?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                End If
            Else
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then


                    If Cbocompany.SelectedIndex <> -1 Then
                        lncompid = Val(Cbocompany.SelectedValue.ToString)
                    Else
                        lncompid = 0
                    End If

                    If cboclienttype.SelectedIndex <> -1 Then
                        lnclientidtype = cboclienttype.SelectedValue.ToString
                    Else
                        lnclientidtype = ""
                    End If

                     
                    If cboCategory.SelectedIndex <> -1 Then
                        lnpcategoryid = Val(cboCategory.SelectedValue.ToString)
                        lnpcategoryname = cboCategory.Text
                    Else
                        lnpcategoryid = 0
                        lnpcategoryname = ""
                    End If
                   

                    If cboCategoryType.SelectedIndex <> -1 Then
                        lnpcategorytypeid = Val(cboCategoryType.SelectedValue.ToString)
                        lnpcategorytypename = cboCategoryType.Text
                    Else
                        lnpcategorytypeid = 0
                        lnpcategorytypename = ""
                    End If

                    'If Rbdpaymode.Checked = True Then
                    '    lnpaymentinfotype = "P"
                    'ElseIf Rbdbeneficiary.Checked = True Then
                    '    lnpaymentinfotype = "B"
                    'ElseIf RbdCurrency.Checked = True Then
                    '    lnpaymentinfotype = "C"
                    'End If

                    'If Rbdyes.Checked = True Then
                    '    lnactiveflag = "Y"
                    'ElseIf RbdNo.Checked = True Then
                    '    lnactiveflag = "N"
                    'End If
                    lnclientid = QuoteFilter(Txtclientid.Text)
                    lnclientdpid = QuoteFilter(TxtDpid.Text)
                    lnPAN = QuoteFilter(TxtPanNumber.Text)
                    lnDIN = QuoteFilter(TxtDINnumber.Text)
                    lnpromotergid = Val(txtId.Text)

                    If lnpromotergid = 0 Then
                        lsAction = "INSERT"
                    Else
                        lsAction = "DELETE"
                    End If

                    Using cmd As New MySqlCommand("pr_sta_mst_tpromoterdetailinfo", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_promoter_gid", lnpromotergid)
                        cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                        cmd.Parameters.AddWithValue("?in_folioclient_id", lnclientid)
                        cmd.Parameters.AddWithValue("?in_folioclient_dp_id", lnclientdpid)
                        cmd.Parameters.AddWithValue("?in_folioclient_id_type", lnclientidtype)
                        cmd.Parameters.AddWithValue("?in_promo_category_id", lnpcategoryid)
                        cmd.Parameters.AddWithValue("?in_promo_category_name", lnpcategoryname)
                        cmd.Parameters.AddWithValue("?in_promo_categorytype_id", lnpcategorytypeid)
                        cmd.Parameters.AddWithValue("?in_promo_categorytype_name", lnpcategorytypename)
                        cmd.Parameters.AddWithValue("?in_pan_number", lnPAN)
                        cmd.Parameters.AddWithValue("?in_din_number", lnDIN)
                        'cmd.Parameters.AddWithValue("?in_categorytype_id", lncategorytypeid)
                        cmd.Parameters.AddWithValue("?in_action", lsAction)
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
                        End If

                    End Using

                    Call ClearControl()
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnpromotergid As Long
        Dim lncompid As Long
        Dim lnclientid As String
        Dim lnclientdpid As String
        Dim lnclientidtype As String
        Dim lnCategory As String
        Dim lnBenCategory As String
        Dim lncategorytypeid As Integer = 0
        Dim lsAction As String
        Dim lnPAN As String
        Dim lnDIN As String
        Dim lnpcategoryid As Integer, lnpcategorytypeid As Integer
        Dim lnpcategoryname As String, lnpcategorytypename As String

        Try
            If Cbocompany.SelectedIndex <> -1 Then
                lncompid = Val(Cbocompany.SelectedValue.ToString)
            Else
                lncompid = 0
            End If

            If cboclienttype.Text <> "" And cboclienttype.SelectedIndex <> -1 Then
                lnclientidtype = cboclienttype.SelectedValue.ToString
            Else
                lnclientidtype = ""
            End If


            If cboCategory.SelectedIndex <> -1 Then
                lnpcategoryid = Val(cboCategory.SelectedValue.ToString)
                lnpcategoryname = cboCategory.Text
            Else
                lnpcategoryid = 0
                lnpcategoryname = ""
            End If


            If cboCategoryType.SelectedIndex <> -1 Then
                lnpcategorytypeid = Val(cboCategoryType.SelectedValue.ToString)
                lnpcategorytypename = cboCategoryType.Text
            Else
                lnpcategorytypeid = 0
                lnpcategorytypename = ""
                cboCategory.SelectedIndex = -1
                MessageBox.Show("Select Category & Category Type", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'If Rbdpaymode.Checked = True Then
            '    lnpaymentinfotype = "P"
            'ElseIf Rbdbeneficiary.Checked = True Then
            '    lnpaymentinfotype = "B"
            'ElseIf RbdCurrency.Checked = True Then
            '    lnpaymentinfotype = "C"
            'End If

            'If Rbdyes.Checked = True Then
            '    lnactiveflag = "Y"
            'ElseIf RbdNo.Checked = True Then
            '    lnactiveflag = "N"
            'End If
            lnclientid = QuoteFilter(Txtclientid.Text)
            lnclientdpid = QuoteFilter(TxtDpid.Text)
            lnPAN = QuoteFilter(TxtPanNumber.Text)
            lnDIN = QuoteFilter(TxtDINnumber.Text)
            lnpromotergid = Val(txtId.Text)

            If lnpromotergid = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_mst_tpromoterdetailinfo", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_promoter_gid", lnpromotergid)
                cmd.Parameters.AddWithValue("?in_comp_gid", lncompid)
                cmd.Parameters.AddWithValue("?in_folioclient_id", lnclientid)
                cmd.Parameters.AddWithValue("?in_folioclient_dp_id", lnclientdpid)
                cmd.Parameters.AddWithValue("?in_folioclient_id_type", lnclientidtype)
                cmd.Parameters.AddWithValue("?in_promo_category_id", lnpcategoryid)
                cmd.Parameters.AddWithValue("?in_promo_category_name", lnpcategoryname)
                cmd.Parameters.AddWithValue("?in_promo_categorytype_id", lnpcategorytypeid)
                cmd.Parameters.AddWithValue("?in_promo_categorytype_name", lnpcategorytypename)
                cmd.Parameters.AddWithValue("?in_pan_number", lnPAN)
                cmd.Parameters.AddWithValue("?in_din_number", lnDIN)
                'cmd.Parameters.AddWithValue("?in_categorytype_id", lncategorytypeid)
                cmd.Parameters.AddWithValue("?in_action", lsAction)
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

            End Using

            Call ClearControl()
            EnableSave(False)
            btnNew.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try


    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        EnableSave(False)
        ClearControl()
        btnNew.Focus()
    End Sub


    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySql.Data.MySqlClient.MySqlConnection)
        Dim lobjDataReader As MySqlDataReader
         

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            Cbocompany.SelectedIndex = -1
            cboclienttype.SelectedIndex = -1
            cboCategory.SelectedIndex = -1


            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("promoter_gid").ToString
                        Cbocompany.SelectedValue = .Item("comp_gid").ToString
                        Txtclientid.Text = .Item("folioclient_id").ToString
                        TxtDpid.Text = .Item("folioclient_dp_id").ToString
                        cboclienttype.SelectedValue = .Item("folioclient_id_type").ToString
                        'Paymentinfotype = .Item("paymentinfo_type").ToString
                        TxtPanNumber.Text = .Item("pan_number").ToString
                        cboCategory.SelectedValue = .Item("promo_category_id").ToString
                        cboCategoryType.SelectedValue = .Item("promo_categorytype_id").ToString
                        TxtDINnumber.Text = .Item("din_number").ToString

                        

                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(Cbocompany)
            Call gpAutoFillCombo(cboclienttype)
            'If paymentmode <> "" Then
            '    Call gpAutoFillCombo(cboCategory)
            'End If

            'If currencycode <> "" Then
            '    Call gpAutoFillCombo(CboBenCategory)
            'End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboCategory_LostFocus(sender As Object, e As EventArgs) Handles cboCategory.LostFocus

        Dim lsSql As String = ""
        If cboCategory.SelectedValue = 1 Then

            lsSql = ""
            lsSql &= " select 1 as Categorytype_id,'Individuals/Hindu undivided Family' as CategoryName Union"
            lsSql &= " select 2 as Categorytype_id,'Central Government/State Government(s)' as CategoryName Union"
            lsSql &= " select 3 as Categorytype_id,'Financial Institutions/ Banks' as CategoryName Union"
            lsSql &= " select 4 as Categorytype_id,'Any other (specify)' as CategoryName "


            Call gpBindCombo(lsSql, "CategoryName", "Categorytype_id", cboCategoryType, gOdbcConn)

        End If
        If cboCategory.SelectedValue = 2 Then

            lsSql = ""
            lsSql &= " select 1 as Categorytype_id,'Individuals  (Non-resident Individual/ Foreign Individuals)' as CategoryName Union"
            lsSql &= " select 2 as Categorytype_id,'Government' as CategoryName Union"
            lsSql &= " select 3 as Categorytype_id,'Institutions' as CategoryName Union"
            lsSql &= " select 4 as Categorytype_id,'Foreign Portfolio Investor' as CategoryName Union"
            lsSql &= " select 5 as Categorytype_id,'Any other (specify)' as CategoryName "

            Call gpBindCombo(lsSql, "CategoryName", "Categorytype_id", cboCategoryType, gOdbcConn)

        End If

    End Sub

    Private Sub cboCategory_LostFocus(sender As Object, Optional p2 As Object = Nothing, Optional e As EventArgs = Nothing)
        Throw New NotImplementedException
    End Sub

End Class