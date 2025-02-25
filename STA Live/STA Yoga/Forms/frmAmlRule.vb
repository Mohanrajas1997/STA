Imports MySql.Data.MySqlClient
Public Class frmAmlRule
    Dim msMode As String
    Dim mnAmlId As Long
    Dim mbGenerateInwardNo As Boolean = True
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub
    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySqlConnection)
        Dim lobjDataReader As MySqlDataReader
        Dim lsRmkIntial As String
        Dim lsRmkSplChar As String
        Dim lsRmkSpace As String
        Dim lsActiveStatus As String

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            If lobjDataReader.HasRows Then
                If lobjDataReader.Read Then
                    txtId.Text = lobjDataReader.Item("amlrule_gid").ToString
                    txtRuleName.Text = lobjDataReader.Item("amlrule_name").ToString
                    CboMatchingType.SelectedValue = lobjDataReader.Item("matching_type").ToString
                    TxtlenStart.Text = lobjDataReader.Item("length_start").ToString
                    TxtlenEnd.Text = lobjDataReader.Item("length_end").ToString
                    lsRmkIntial = lobjDataReader.Item("remove_initial").ToString
                    lsRmkSplChar = lobjDataReader.Item("remove_spl_char").ToString
                    lsRmkSpace = lobjDataReader.Item("remove_space").ToString
                    lsActiveStatus = lobjDataReader.Item("active_status").ToString

                    If lsRmkIntial = "Y" Then
                        ChkRmvIntial.Checked = True
                    Else
                        ChkRmvIntial.Checked = False
                    End If

                    If lsRmkSplChar = "Y" Then
                        ChkRmvChr.Checked = True
                    Else
                        ChkRmvChr.Checked = False
                    End If

                    If lsRmkSpace = "Y" Then
                        ChkRmvSpace.Checked = True
                    Else
                        ChkRmvSpace.Checked = False
                    End If
                    If lsActiveStatus = "Y" Then
                        ChkYes.Checked = True
                    Else
                        ChkYes.Checked = False
                    End If

                End If
                Call gpAutoFillCombo(CboMatchingType)
            End If
            lobjDataReader.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub frmInwardEntry_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmBankMater_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      

        CboMatchingType.Items.Clear()
        CboMatchingType.Items.Add("Exact")
        CboMatchingType.Items.Add("Substr(m,n)")
        CboMatchingType.Items.Add("Left(n)")
        CboMatchingType.Items.Add("Right(n)")
        CboMatchingType.Items.Add("Like")
        CboMatchingType.Items.Add("Shuffle")

        ChkRmvChr.Checked = False
        ChkRmvIntial.Checked = False
        ChkRmvSpace.Checked = False

        TxtlenStart.Enabled = True
        TxtlenEnd.Enabled = True

        Call EnableSave(False)
    End Sub
    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtRuleName.Focus()
    End Sub
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs)
        Call frmCtrClear(Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Call ClearControl()
        Call EnableSave(True)
        txtRuleName.Focus()
        ChkRmvChr.Checked = False
        ChkRmvIntial.Checked = False
        ChkRmvSpace.Checked = False

        TxtlenStart.Enabled = False
        TxtlenEnd.Enabled = False

    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
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

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, "select a.amlrule_gid as 'Rule Id'," & _
            "a.amlrule_name as 'Rule Name',a.matching_type as 'Matching Type',a.remove_initial as 'Remove Initial',a.remove_spl_char as 'Remove Spl Char', " & _
            "a.remove_space as 'Remove Space',a.active_status as 'Active Status' FROM sta_mst_tamlrule as a ", _
            "a.amlrule_gid,a.amlrule_name,a.matching_type,a.remove_initial,a.remove_spl_char,a.remove_space,a.active_status", " 1 = 1 and a.delete_flag = 'N'")
            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tamlrule " _
                    & "where amlrule_gid = " & gnSearchId & " " _
                    & "and delete_flag = 'N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnResult As Long
        Dim lsTxt As String

        Try
            If txtId.Text.Trim = "" Then
                MsgBox("Select the Record", MsgBoxStyle.Information, gsProjectName)
                'Calling Find Button to select record
                Call btnFind_Click(sender, e)
            Else
                If MsgBox("Are you sure want to delete this record?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gsProjectName) = MsgBoxResult.Yes Then
                    Using cmd As New MySqlCommand("pr_sta_ins_tamlrule", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?In_amlrule_gid", Val(txtId.Text))
                        cmd.Parameters.AddWithValue("?In_amlrule_name", "")
                        cmd.Parameters.AddWithValue("?In_matching_type", "")
                        cmd.Parameters.AddWithValue("?In_length_start", 0)
                        cmd.Parameters.AddWithValue("?In_length_end", 0)
                        cmd.Parameters.AddWithValue("?In_remove_initial", "")
                        cmd.Parameters.AddWithValue("?In_remove_spl_char", "")
                        cmd.Parameters.AddWithValue("?In_remove_space", "")
                        cmd.Parameters.AddWithValue("?In_active_status", "")
                        cmd.Parameters.AddWithValue("?In_action", "DELETE")
                        cmd.Parameters.AddWithValue("?In_login_user", gsLoginUserCode)

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

                    Call EnableSave(False)
                    Call ClearControl()
                Else
                    btnNew.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lsAction As String
        Dim lnAmlAplId As Long
        Dim lnAmlAplyName As String
        Dim lsMatchingType As String
        Dim lsRmvIntial As String
        Dim lsRmvSplChar As String
        Dim lsRmvSpace As String
        Dim lsActiveStatus As String
        Dim lsLengthStart As Long = 0
        Dim lsLengthEnd As Long = 0

        Try
          
            lnAmlAplId = Val(txtId.Text)
            lsLengthStart = Val(TxtlenStart.Text)
            lsLengthEnd = Val(TxtlenEnd.Text)

            lnAmlAplyName = QuoteFilter(txtRuleName.Text)

            If CboMatchingType.SelectedIndex <> -1 Then
                lsMatchingType = CboMatchingType.Text
            Else
                lsMatchingType = ""
            End If
            ' lsMatchingType = CboMatchingType.Text
            If lsMatchingType = "Substr(m,n)" Then
                If lsLengthStart = 0 Or lsLengthEnd = 0 Then
                    MessageBox.Show("Please enter the Starting Length and Ending Length!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TxtlenStart.Focus()
                    Exit Sub
                End If
            End If

            If lsMatchingType = "Left(n)" Then
                If lsLengthStart = 0 Then
                    MessageBox.Show("Please enter the Length!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TxtlenStart.Focus()
                    Exit Sub
                End If
            ElseIf lsMatchingType = "Right(n)" Then
                If lsLengthStart = 0 Then
                    MessageBox.Show("Please enter the Length!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TxtlenStart.Focus()
                    Exit Sub
                End If
            End If

            If ChkRmvIntial.CheckState = 1 Then
                lsRmvIntial = "Y"
            Else
                lsRmvIntial = "N"
            End If

            If ChkRmvChr.CheckState = 1 Then
                lsRmvSplChar = "Y"
            Else
                lsRmvSplChar = "N"
            End If

            If ChkRmvSpace.CheckState = 1 Then
                lsRmvSpace = "Y"
            Else
                lsRmvSpace = "N"
            End If

            If ChkYes.CheckState = 1 Then
                lsActiveStatus = "Y"
            Else
                lsActiveStatus = "N"
            End If



            If lnAmlAplyName = "" Then
                MessageBox.Show("Please enter the Aml Rule name!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtRuleName.Focus()
                Exit Sub
            End If
            If lsMatchingType = "" Then
                MessageBox.Show("Please select the Matching Type!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                CboMatchingType.Focus()
                Exit Sub
            End If

            If lnAmlAplId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_ins_tamlrule", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?In_amlrule_gid", lnAmlAplId)
                cmd.Parameters.AddWithValue("?In_amlrule_name", lnAmlAplyName)
                cmd.Parameters.AddWithValue("?In_matching_type", lsMatchingType)
                cmd.Parameters.AddWithValue("?In_length_start", lsLengthStart)
                cmd.Parameters.AddWithValue("?In_length_end", lsLengthEnd)
                cmd.Parameters.AddWithValue("?In_remove_initial", lsRmvIntial)
                cmd.Parameters.AddWithValue("?In_remove_spl_char", lsRmvSplChar)
                cmd.Parameters.AddWithValue("?In_remove_space", lsRmvSpace)
                cmd.Parameters.AddWithValue("?In_active_status", lsActiveStatus)
                cmd.Parameters.AddWithValue("?In_action", lsAction)
                cmd.Parameters.AddWithValue("?In_login_user", gsLoginUserCode)

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

                ChkRmvChr.Checked = False
                ChkRmvIntial.Checked = False
                ChkRmvSpace.Checked = False
            Else
                Call EnableSave(False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call ClearControl()
        Call EnableSave(False)
    End Sub

    Private Sub CboMatchingType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboMatchingType.SelectedIndexChanged
        Dim lsMatchingType As String
        lsMatchingType = CboMatchingType.Text
        TxtlenStart.Enabled = False
        TxtlenEnd.Enabled = False
        TxtlenStart.Text = 0
        TxtlenEnd.Text = 0
        If lsMatchingType = "Substr(m,n)" Then
            TxtlenStart.Enabled = True
            TxtlenEnd.Enabled = True
        ElseIf lsMatchingType = "Left(n)" Then
            TxtlenStart.Enabled = True
            TxtlenEnd.Enabled = False
        ElseIf lsMatchingType = "Right(n)" Then
            TxtlenStart.Enabled = True
            TxtlenEnd.Enabled = False
        End If
    End Sub
End Class