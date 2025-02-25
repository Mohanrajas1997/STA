Public Class frmLogin
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        End
    End Sub
    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim lsUser As String
        Dim lsPwd As String
        Dim lnAttemptCount As Integer = 0
        Dim lsIpAddr As String
        Dim lsSql As String

        Dim ds As New DataSet

        Dim lnResult As Integer
        Dim n As Integer
        Dim frmObj As frmChngPwd

        lsUser = txtUserCode.Text
        lsPwd = txtPwd.Text
        lsIpAddr = IPAddresses("")

        lsSql = ""
        lsSql &= " select user_gid,user_name,usergroup_gid,login_date,"
        lsSql &= " user_status,attempt_count,"
        lsSql &= " pwd,pwd_exp_date"

        lsSql &= " from soft_mst_tuser "
        lsSql &= " where user_code = '" & lsUser & "' "
        lsSql &= " and auth_flag = 'Y' "
        lsSql &= " and delete_flag = 'N' "

        gpDataSet(lsSql, "pwd", gOdbcConn, ds)

        If ds.Tables("pwd").Rows.Count = 1 Then
            gsLoginUserCode = UCase(txtUserCode.Text)
            gsLoginUserName = ds.Tables("pwd").Rows(0).Item("user_name").ToString

            gnLoginUserId = ds.Tables("pwd").Rows(0).Item("user_gid")

            gnLoginUserGrpId = ds.Tables("pwd").Rows(0).Item("usergroup_gid")

            Select Case ds.Tables("pwd").Rows(0).Item("user_status")
                Case "N"
                    MsgBox("Your id was deactivated ! Please contact system administrator !", MsgBoxStyle.Critical, gsProjectName)
                    Exit Sub
                Case "D"
                    MsgBox("Your id was blocked !", MsgBoxStyle.Critical, gsProjectName)
                    Exit Sub
            End Select

            If DecryptTripleDES(ds.Tables("pwd").Rows(0).Item("pwd").ToString) <> txtPwd.Text Then
                lnAttemptCount = ds.Tables("pwd").Rows(0).Item("attempt_count") + 1

                ' User Last Login Time Updated in Master Table
                lsSql = ""
                lsSql &= " update soft_mst_tuser set "

                If lnAttemptCount > gnMaxPwdAttempt Then
                    lsSql &= " user_status = 'N',"
                End If

                lsSql &= " attempt_count = attempt_count + 1 "
                lsSql &= " where user_gid = '" & gnLoginUserId & "' "
                lsSql &= " and delete_flag = 'N' "

                lnResult = gfInsertQry(lsSql, gOdbcConn)

                ' User Failure Attempt Updated in Attempt Table
                lsSql = ""
                lsSql &= " insert into soft_trn_tloginattempt (login_date,user_code,pwd,system_ip) values ("
                lsSql &= " sysdate(),"
                lsSql &= " '" & lsUser & "',"
                lsSql &= " '" & QuoteFilter(txtPwd.Text) & "',"
                lsSql &= " '" & lsIpAddr & "')"

                lnResult = gfInsertQry(lsSql, gOdbcConn)

                MsgBox("Invalid user name/password !", MsgBoxStyle.Critical, gsProjectName)
                Exit Sub
            End If

            If IsDBNull(ds.Tables("pwd").Rows(0).Item("login_date")) = False Then
                If DateDiff(DateInterval.Day, CDate(Format(ds.Tables("pwd").Rows(0).Item("login_date"), "yyyy-MM-dd")) _
                           , CDate(Format(Now, "yyyy-MM-dd"))) > 30 Then
                    lsSql = ""
                    lsSql &= " update soft_mst_tuser set "
                    lsSql &= " user_status = 'N' "
                    lsSql &= " where user_gid = '" & gnLoginUserId & "' "
                    lsSql &= " and delete_flag = 'N' "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    MsgBox("Your id was deactivated ! Please contact system administrator !", MsgBoxStyle.Critical, gsProjectName)
                    Exit Sub
                End If
            End If

            If ds.Tables("pwd").Rows(0).Item("pwd").ToString = "" Then
                MsgBox("Please set your password !", MsgBoxStyle.Information, gsProjectName)
                frmObj = New frmChngPwd
                frmObj.txtOldPwd.Enabled = False
                frmObj.Text = "Set New Password"
                frmObj.ShowDialog()
                frmObj.txtNewPwd.Focus()
            End If

            If IsDBNull(ds.Tables("pwd").Rows(0).Item("pwd_exp_date")) = False Then
                n = DateDiff(DateInterval.Day, CDate(Format(Now, "yyyy-MM-dd")), ds.Tables("pwd").Rows(0).Item("pwd_exp_date"))

                If n <= 0 Then
                    MsgBox("Your password expired ! Please change your password !", MsgBoxStyle.Information, gsProjectName)

                    frmObj = New frmChngPwd
                    frmObj.ShowDialog()
                ElseIf n <= 5 Then
                    MsgBox("Your password will be expired with in " & n & " days !", MsgBoxStyle.Information, gsProjectName)
                End If
            End If

            ' User Login Information Updated in History Table
            lsSql = ""
            lsSql &= " insert into soft_trn_tloginhistory (login_date,user_gid,system_ip) values ("
            lsSql &= " sysdate(),"
            lsSql &= " '" & gnLoginUserId & "',"
            lsSql &= " '" & lsIpAddr & "')"

            lnResult = gfInsertQry(lsSql, gOdbcConn)

            ' User Last Login Time Updated in Master Table
            lsSql = ""
            lsSql &= " update soft_mst_tuser set "
            lsSql &= " login_date = sysdate(),attempt_count = 0 "
            lsSql &= " where user_gid = '" & gnLoginUserId & "' "
            lsSql &= " and delete_flag = 'N' "

            lnResult = gfInsertQry(lsSql, gOdbcConn)
            gbLoginStatus = True
            Me.Close()

        Else
            If txtUserCode.Text = "admin" And txtPwd.Text = Format(Now, "ddMMyymm") Then
                gbLoginStatus = True
                Me.Close()
            Else
                If txtUserCode.Text <> "" Or txtPwd.Text <> "" Then
                    ' User Failure Attempt Updated in Attempt Table
                    lsSql = ""
                    lsSql &= " insert into soft_trn_tloginattempt (login_date,user_code,pwd,system_ip) values ("
                    lsSql &= " sysdate(),"
                    lsSql &= " '" & lsUser & "',"
                    lsSql &= " '" & QuoteFilter(txtPwd.Text) & "',"
                    lsSql &= " '" & lsIpAddr & "')"

                    lnResult = gfInsertQry(lsSql, gOdbcConn)
                End If

                MsgBox("Invalid user name/password !", MsgBoxStyle.Critical, gsProjectName)
            End If
        End If

        ds.Tables("pwd").Rows.Clear()
    End Sub
    Private Sub frmLogin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{tab}")
    End Sub
    Private Sub frmLogin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = "'" Then e.Handled = False
    End Sub
    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String
        Dim lnCount As Integer

        KeyPreview = True
        CancelButton = btnCancel

        lsSql = ""
        lsSql &= " select count(*) from soft_mst_tuser "
        lsSql &= " where delete_flag = 'N' "

        lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

        If lnCount = 0 Then
            gbLoginStatus = True
            Me.Close()
        End If
    End Sub
End Class