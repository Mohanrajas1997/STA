Imports System.IO
Imports MySql.Data.MySqlClient
Public Class frmAMLResult

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim lsAmlname As String
        Dim amllist As New DataSet
        Dim amlapplyrule As New DataSet
        Dim amlapplyrule_ds As New DataSet
        Dim lsAmlrule_gid As Long = 0
        Dim lsMatcingtype As String
        Dim lsLengthStart As Long = 0
        Dim lsLengthEnd As Long = 0
        Dim lsRemoveIntial As String
        Dim lsRemoveSplChar As String
        Dim lsRemoveSpace As String
        Dim lsAmlnamerule As String

        Dim lnResult As Long
        Dim lsTxt As String

        Try

       
        Using cmd As New MySqlCommand("pr_get_tamllist_result", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            Using sda As New MySqlDataAdapter(cmd)
                sda.Fill(amllist)
            End Using
        End Using

        For i = 0 To amllist.Tables("Table").Rows.Count - 1

            Using cmd As New MySqlCommand("pr_get_tamlruleapply_result", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                Using sda As New MySqlDataAdapter(cmd)
                    sda.Fill(amlapplyrule)
                End Using
            End Using

            lsAmlname = amllist.Tables("Table").Rows(i).Item("aml_name").ToString

            For j = 0 To amlapplyrule.Tables("Table").Rows.Count - 1

                lsAmlrule_gid = amlapplyrule.Tables("Table").Rows(j).Item("amlrule_gid").ToString
                lsMatcingtype = amlapplyrule.Tables("Table").Rows(j).Item("matching_type").ToString
                lsLengthStart = amlapplyrule.Tables("Table").Rows(j).Item("length_start").ToString
                lsLengthEnd = amlapplyrule.Tables("Table").Rows(j).Item("length_end").ToString
                lsRemoveIntial = amlapplyrule.Tables("Table").Rows(j).Item("remove_initial").ToString
                lsRemoveSplChar = amlapplyrule.Tables("Table").Rows(j).Item("remove_spl_char").ToString
                lsRemoveSpace = amlapplyrule.Tables("Table").Rows(j).Item("remove_space").ToString

                Using cmd As New MySqlCommand("pr_set_tamlruleapply_result", gOdbcConn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("?In_matchingtype", lsMatcingtype)
                    cmd.Parameters.AddWithValue("?In_lengthstart", lsLengthStart)
                    cmd.Parameters.AddWithValue("?In_lengthend", lsLengthEnd)
                    cmd.Parameters.AddWithValue("?In_removeinitial", lsRemoveIntial)
                    cmd.Parameters.AddWithValue("?In_removesplchar", lsRemoveSplChar)
                    cmd.Parameters.AddWithValue("?In_removespace", lsRemoveSpace)
                    cmd.Parameters.AddWithValue("?In_amlname", lsAmlname)
                    Using sda As New MySqlDataAdapter(cmd)
                        sda.Fill(amlapplyrule_ds)
                    End Using
                End Using

                lsAmlnamerule = amlapplyrule_ds.Tables("Table").Rows(0).Item("v_amlname_list").ToString

                If lsMatcingtype = "Shuffle" Then
                    Call PermuteString(lsAmlnamerule, lsAmlrule_gid, lsMatcingtype, lsLengthStart, lsLengthEnd, lsRemoveIntial, lsRemoveSplChar, lsRemoveSpace)
                Else
                    Using cmd As New MySqlCommand("pr_set_tamlprocess_result", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?In_amlrule_gid", lsAmlrule_gid)
                        cmd.Parameters.AddWithValue("?In_matchingtype", lsMatcingtype)
                        cmd.Parameters.AddWithValue("?In_lengthstart", lsLengthStart)
                        cmd.Parameters.AddWithValue("?In_lengthend", lsLengthEnd)
                        cmd.Parameters.AddWithValue("?In_removeinitial", lsRemoveIntial)
                        cmd.Parameters.AddWithValue("?In_removesplchar", lsRemoveSplChar)
                        cmd.Parameters.AddWithValue("?In_removespace", lsRemoveSpace)
                        cmd.Parameters.AddWithValue("?In_amlname", lsAmlnamerule)
                        cmd.Parameters.AddWithValue("?In_login_name", gsLoginUserCode)

                        'Out put Para
                        cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                        cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                        cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                        cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                        cmd.CommandTimeout = 0
                        cmd.ExecuteNonQuery()

                        lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                        lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                        If lnResult = 0 Then
                            MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    End Using
                End If
            Next
        Next

            MessageBox.Show("Record Updated Sucessfully.!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try

    End Sub
    Public Function PermuteString(ByVal Ztring As String, ByVal amlrulegid As String, ByVal matchingtype As String, ByVal lengthstart As String, ByVal lengthend As String,
                                  ByVal removeinitaial As String, ByVal removesplchar As String, ByVal removespace As String, Optional ByVal Base As String = "") As String

        Dim TmpStrArray() As String, I As Long
        Dim Amlname As String

        Dim lnResult As Long
        Dim lsTxt As String
        Try

    
        ' If there's only 1 element, then
        If InStr(1, Ztring, " ", vbTextCompare) = 0 Then
            PermuteString = Base & " " & Ztring & vbCrLf
            Amlname = PermuteString

            Using cmd As New MySqlCommand("pr_set_tamlprocess_result", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?In_amlrule_gid", amlrulegid)
                cmd.Parameters.AddWithValue("?In_matchingtype", matchingtype)
                cmd.Parameters.AddWithValue("?In_lengthstart", lengthstart)
                cmd.Parameters.AddWithValue("?In_lengthend", lengthend)
                cmd.Parameters.AddWithValue("?In_removeinitial", removeinitaial)
                cmd.Parameters.AddWithValue("?In_removesplchar", removesplchar)
                cmd.Parameters.AddWithValue("?In_removespace", removespace)
                cmd.Parameters.AddWithValue("?In_amlname", Amlname)
                cmd.Parameters.AddWithValue("?In_login_name", gsLoginUserCode)

                'Out put Para
                cmd.Parameters.Add("?out_result", MySqlDbType.Int32)
                cmd.Parameters("?out_result").Direction = ParameterDirection.Output
                cmd.Parameters.Add("?out_msg", MySqlDbType.VarChar)
                cmd.Parameters("?out_msg").Direction = ParameterDirection.Output

                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()

                lnResult = Val(cmd.Parameters("?out_result").Value.ToString())
                lsTxt = cmd.Parameters("?out_msg").Value.ToString()

                If lnResult = 0 Then
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End Using
            Exit Function
        End If

        ' If more than 1 element: split elements in one array of elements
        TmpStrArray = Split(Ztring, " ", , vbTextCompare)


        If Base = "" Then
            ' Loop trough each element and do callbacks to permute again
            For I = LBound(TmpStrArray) To UBound(TmpStrArray)
                PermuteString = PermuteString & _
                PermuteString(ReturnAllBut(TmpStrArray, I), amlrulegid, matchingtype, lengthstart, lengthend, removeinitaial, removesplchar, removespace, TmpStrArray(I))
            Next
        Else
            ' Loop trough each element and do callbacks to permute again
            For I = LBound(TmpStrArray) To UBound(TmpStrArray)
                PermuteString = PermuteString & " " & _
                PermuteString(ReturnAllBut(TmpStrArray, I), amlrulegid, matchingtype, lengthstart, lengthend, removeinitaial, removesplchar, removespace, Base & " " & TmpStrArray(I))
            Next
        End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Function


    ' Return all items in a array but 1
    Public Function ReturnAllBut(ByRef Arrai() As String, _
           ByVal But As Long) _
           As String
        Dim I As Long
        For I = LBound(Arrai) To UBound(Arrai)
            If I <> But Then
                ReturnAllBut = ReturnAllBut & Arrai(I) & " "
            End If
        Next
        ReturnAllBut = RTrim(ReturnAllBut)
    End Function


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

    End Sub
End Class