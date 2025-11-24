Imports MySql.Data.MySqlClient
Public Class frmAmlApplyRule
#Region "Local Variables"
    Dim mnCompId As Long
    Dim msCompName As String
    Dim mnInwardId As Long
    Dim mnFolioId As Long
    Dim mnQueueId As Long
    Dim msTranCode As String
    Dim mnChklstValid As Long
    Dim mnChklstDisc As Long
    Dim mnChkLstAllStatus As Long = 0
    Dim mnChkLstSelected As Long = 0
    Dim msGroupCode As String = ""
    Dim msDocRcvdDate As String = ""
#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub frmAddressChange_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lobjChkBoxColumn As DataGridViewCheckBoxColumn
        Dim i As Integer
        Dim n As Integer
        Dim lsDepositoryCode As String = ""
        Dim lsDematRejectCode As String = ""
        Dim lsNameChangeFlag As String = ""
        Dim lsNameChangeCode As String = ""
        Dim lnChkLstValid As Integer
        Dim lnChkLstDisc As Integer

        Dim lnCertEntryFlag As Integer = 0
        Dim lnTotShares As Long = 0
     

        ' load check list
        cmd = New MySqlCommand("pr_get_tamlapplyrule", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
    

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dgvChklst
            .DataSource = dt

            .Columns("amlrule_gid").Visible = False
            .Columns("Applyrule_Gid").Visible = False
            .Columns("ApplyRule Status").Visible = False
            .Columns("AmlRule Name").Width = 200


            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
            Next i

            n = .Columns.Count - 1

            lobjChkBoxColumn = New DataGridViewCheckBoxColumn
            lobjChkBoxColumn.HeaderText = "Apply Rule"
            lobjChkBoxColumn.Width = 50
            lobjChkBoxColumn.Name = "Apply Rule"
            lobjChkBoxColumn.Selected = False

            .Columns.Add(lobjChkBoxColumn)

            For i = 0 To .Rows.Count - 1

                If .Rows(i).Cells("ApplyRule Status").Value = "Y" Then
                    .Rows(i).Cells(n + 1).Value = True
                Else
                    .Rows(i).Cells(n + 1).Value = False
                End If

            Next i
            .Columns("Apply Rule").Width = 98
          
        End With

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer
        Dim n As Integer

        Dim lnResult As Long
        Dim lsTxt As String

        Dim lsCertId As String = ""
        Dim lsNameCode As String = ""
        Dim lsDematRejectCode As String = ""
        Dim lnDematPendId As Long = 0
        Dim lsApplyRuleGid As Long = 0
        Dim lsAmlRuleName As String
        Dim lsAmlRuleGid As Long = 0
        Dim lsApplyRuleStatus As String

        Try
            With dgvChklst
                n = .Columns.Count
                For i = 0 To .Rows.Count - 1

                    lsApplyRuleGid = .Rows(i).Cells("Applyrule_Gid").Value
                    lsAmlRuleName = .Rows(i).Cells("AmlRule Name").Value
                    lsAmlRuleGid = .Rows(i).Cells("amlrule_gid").Value

                    If .Rows(i).Cells("Apply Rule").Value = True Then
                        lsApplyRuleStatus = "Y"
                    Else
                        lsApplyRuleStatus = "N"
                    End If


                    Using cmd As New MySqlCommand("pr_sta_ins_tamlapplyrule", gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?In_applyrule_id", lsApplyRuleGid)
                        cmd.Parameters.AddWithValue("?In_rule_id", lsAmlRuleGid)
                        cmd.Parameters.AddWithValue("?In_active_status", lsApplyRuleStatus)
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

                        If lnResult = 0 Then
                            MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End Using
                Next i

                MessageBox.Show("Record Updated Sucessfully!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub
End Class