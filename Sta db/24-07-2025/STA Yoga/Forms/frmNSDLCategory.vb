Imports MySql.Data.MySqlClient
Public Class frmNSDLCategory
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

        Try
            lobjDataReader = gfExecuteQry(SqlStr, gOdbcConn)

            If lobjDataReader.HasRows Then
                If lobjDataReader.Read Then
                    txtId.Text = lobjDataReader.Item("category_id").ToString
                    txtCategoryName.Text = lobjDataReader.Item("category_name").ToString
                    CboCategoryCode.SelectedValue = lobjDataReader.Item("category_code").ToString

                End If
                Call gpAutoFillCombo(CboCategoryCode)
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

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load


        CboCategoryCode.Items.Clear()
        CboCategoryCode.Items.Add("P")
        CboCategoryCode.Items.Add("PI")
        CboCategoryCode.Items.Add("PN")

        Call EnableSave(False)
    End Sub
    Private Sub ClearControl()
        Call frmCtrClear(Me)
        CboCategoryCode.Focus()
    End Sub
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs)
        Call frmCtrClear(Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Call ClearControl()
        Call EnableSave(True)
        CboCategoryCode.Focus()
        

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
            SearchDialog = New frmSearch(gOdbcConn, "select a.category_id as 'Category Id'," & _
            "a.category_code as 'Category Code',a.category_name as 'Category Name' FROM sta_mst_tnsdlcategory as a ", _
            "a.category_id,a.category_code,a.category_name", " 1 = 1 and a.delete_flag = 'N'")
            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from sta_mst_tnsdlcategory " _
                    & "where category_id = " & gnSearchId & " " _
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
                    Using cmd As New MySqlCommand("pr_sta_mst_tnsdlcategory", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_category_id", Val(txtId.Text))
                        cmd.Parameters.AddWithValue("?in_category_code", "")
                        cmd.Parameters.AddWithValue("?in_category_name", "")
                        cmd.Parameters.AddWithValue("?In_action", "DELETE")
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
        Dim lnCategoryName As String
        Dim lsCategoryCode As String
       
        Dim lsLengthStart As Long = 0
        Dim lsLengthEnd As Long = 0

        Try

            lnAmlAplId = Val(txtId.Text)

            lnCategoryName = QuoteFilter(txtCategoryName.Text)

            If CboCategoryCode.SelectedIndex <> -1 Then
                lsCategoryCode = CboCategoryCode.Text
            Else
                lsCategoryCode = ""
            End If

            If lnCategoryName = "" Then
                MessageBox.Show("Please enter the Category name!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCategoryName.Focus()
                Exit Sub
            End If
            If lsCategoryCode = "" Then
                MessageBox.Show("Please select the Category Code!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                CboCategoryCode.Focus()
                Exit Sub
            End If

            If lnAmlAplId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand("pr_sta_mst_tnsdlcategory", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_category_id", lnAmlAplId)
                cmd.Parameters.AddWithValue("?in_category_code", lsCategoryCode)
                cmd.Parameters.AddWithValue("?in_category_name", lnCategoryName)
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

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call ClearControl()
        Call EnableSave(False)
    End Sub
   
End Class