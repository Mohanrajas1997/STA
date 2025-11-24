Imports MySql.Data.MySqlClient

Public Class frmCompanySubGroupMaster

    Private Sub frmCompanySubGroupMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lsSql As String
        ' Company Grp Name
        lsSql = ""
        lsSql &= " select compgrp_gid,compgrp_name from sta_mst_tcompanygroup "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by compgrp_name asc "

        Call gpBindCombo(lsSql, "compgrp_name", "compgrp_gid", cboCompanyGrp, gOdbcConn)

        Call EnableSave(False)
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtCompSubGrpCode.Focus()
    End Sub

    Public Sub frmCtrClear(ByVal frmName As Object)
        Dim ctrl As Control
        Dim dtp As DateTimePicker

        For Each ctrl In frmName.Controls
            If ctrl.Tag <> "*" Then
                If TypeOf ctrl Is TextBox Then ctrl.Text = ""
                If TypeOf ctrl Is ComboBox Then
                    ctrl.Text = ""
                End If

                If TypeOf ctrl Is DateTimePicker Then
                    dtp = ctrl

                    If dtp.ShowCheckBox = True Then
                        dtp.Checked = False
                    End If
                End If

                If TypeOf ctrl Is Panel Then frmCtrClear(ctrl)
                If TypeOf ctrl Is GroupBox Then frmCtrClear(ctrl)
            End If
        Next
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Call ClearControl()
        Call EnableSave(False)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
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
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, "select compsubgrp_gid as 'Comp Sub Group Gid',compgrp_gid as 'Comp Group Id'," &
            "compsubgrp_code as 'Company Sub Group Code',compsubgrp_name as 'Company Sub Group Name' FROM sta_mst_tcompanysubgroup ",
            "compsubgrp_gid,compgrp_gid,compsubgrp_code,compsubgrp_name",
            " 1 = 1 and delete_flag = 'N' ")
            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tcompanysubgroup " _
                    & "where compsubgrp_gid = " & gnSearchId & " " _
                    & "and delete_flag = 'N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySqlConnection)
        Dim lobjDataReader As MySqlDataReader

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            If lobjDataReader.HasRows Then
                If lobjDataReader.Read Then
                    txtId.Text = lobjDataReader.Item("compsubgrp_gid").ToString

                    cboCompanyGrp.SelectedIndex = -1
                    cboCompanyGrp.SelectedValue = lobjDataReader.Item("compgrp_gid").ToString
                    Call gpAutoFillCombo(cboCompanyGrp)

                    txtCompSubGrpCode.Text = lobjDataReader.Item("compsubgrp_code").ToString
                    txtCompSubGrpName.Text = lobjDataReader.Item("compsubgrp_name").ToString
                End If
            End If
            txtCompSubGrpCode.Enabled = False
            lobjDataReader.Close()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnCompSubGrpId As Long
        Dim lnCompGrpId As Long
        Dim lsCompSubGrpCode As String
        Dim lsCompSubGrpName As String
        Dim lsAction As String = ""

        Try
            lnCompGrpId = Val(cboCompanyGrp.SelectedValue)
            lnCompSubGrpId = Val(txtId.Text)
            lsCompSubGrpCode = QuoteFilter(txtCompSubGrpCode.Text)
            lsCompSubGrpName = QuoteFilter(txtCompSubGrpName.Text)

            If lnCompSubGrpId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_mst_companysubgroup", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_compsubgrp_gid", lnCompSubGrpId)
                cmd.Parameters.AddWithValue("?in_compgrp_gid", lnCompGrpId)
                cmd.Parameters.AddWithValue("?in_compsubgrp_code", lsCompSubGrpCode)
                cmd.Parameters.AddWithValue("?in_compsubgrp_name", lsCompSubGrpName)
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
                    Exit Sub
                End If
            End Using

            Call ClearControl()

            If MsgBox("Do you want to add another record ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
                btnNew.PerformClick()
            Else
                Call EnableSave(False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Call ClearControl()
        Call EnableSave(True)
        txtCompSubGrpCode.Enabled = True
        cboCompanyGrp.Focus()
    End Sub

End Class