Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmQueryMaster
    Dim fsSQL As String
    Dim llqryID As String
    Dim fnResult As Integer

    Private Sub frmQueryMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EnableSave(False)
        btnNew.Focus()
        Me.rtfqrydesc.ScrollBars = RichTextBoxScrollBars.Vertical
    End Sub
    Private Sub EnableSave(ByVal status As Boolean)
        pnlButtons.Visible = Not status
        pnlmain.Enabled = status
        pnlsave.Visible = status
    End Sub
    Private Sub ctrclear()
        txtqrycode.Text = ""
        txtid.Text = ""
        rtfqrydesc.Text = ""
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        EnableSave(True)
        ctrclear()
        txtqrycode.Focus()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If txtid.Text = "" Then
                If MsgBox("Select record to edit?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    Call btnFind_Click(sender, e)
                End If
            Else
                EnableSave(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch
        Try
            SearchDialog = New frmSearch(gOdbcConn, _
                             "select qry_gid 'Query Id',qry_code 'Query Code',qry_desc 'Query Description' " & _
                             "from sta_mst_tqry ", _
                             "qry_gid,qry_code,qry_desc ", _
                             "1=1 and delete_flag='N' ")
            SearchDialog.ShowDialog()
            If gnSearchId <> 0 Then
                Call listAll("select * from sta_mst_tqry where delete_flag='N' and qry_gid=" & gnSearchId & " ", gOdbcConn)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
    Private Sub listAll(ByVal gsQry As String, ByVal odbcconn As MySqlConnection)
        Dim ds As New DataSet
        Try
            ds = gfDataSet(gsQry, "sta_mst_tqry", gOdbcConn)
            If ds.Tables(0).Rows.Count > 0 Then
                txtid.Text = Val(ds.Tables("sta_mst_tqry").Rows(0).Item("qry_gid").ToString)
                txtqrycode.Text = ds.Tables("sta_mst_tqry").Rows(0).Item("qry_code")
                rtfqrydesc.Text = ds.Tables("sta_mst_tqry").Rows(0).Item("qry_desc")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If txtid.Text = "" Then
                If MsgBox("Select record to delete?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    Call btnFind_Click(sender, e)
                End If
            Else
                EnableSave(True)
                If MsgBox("Do you want to delete this record?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    gsQry = "update sta_mst_tqry set "
                    gsQry &= " delete_flag='Y' "
                    gsQry &= " where "
                    gsQry &= " qry_gid=" & txtid.Text

                    fnResult = gfInsertQry(gsQry, gOdbcConn)

                    If fnResult > 0 Then
                        MsgBox("Record Deleted successfully !!", MsgBoxStyle.Information, gsProjectName)
                    End If

                    ctrclear()
                    EnableSave(False)
                Else
                    ctrclear()
                    EnableSave(False)
                End If
                btnNew.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you want to close?", MsgBoxStyle.YesNo, gsProjectName) Then
            Me.Close()
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        EnableSave(False)
        ctrclear()
        btnNew.Focus()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If txtqrycode.Text = "" Then
                MsgBox("Please Enter Query Code!", MsgBoxStyle.Information, gsProjectName)
                Exit Sub
            End If

            If rtfqrydesc.Text = "" Then
                MsgBox("Please Enter Query Description!", MsgBoxStyle.Information, gsProjectName)
                Exit Sub
            End If

            'Duplicate Checking
            gsQry = "select qry_gid from sta_mst_tqry "
            gsQry &= "where qry_code='" & txtqrycode.Text & "' "


            If txtid.Text.Trim <> "" Then gsQry &= " and qry_gid <>" & txtid.Text
            gsQry &= " and delete_flag='N'"

            llqryID = Val(gfExecuteScalar(gsQry, gOdbcConn))
            If llqryID <> 0 Then
                MsgBox("Query Code Aldready Exists", MsgBoxStyle.Information, gsProjectName)
                txtqrycode.Focus()
                Exit Sub
            End If

            If txtid.Text = "" Then
                fsSQL = ""
                fsSQL &= "insert into sta_mst_tqry( "
                fsSQL &= "qry_code,qry_desc)values( "
                fsSQL &= "'" & QuoteFilter(txtqrycode.Text) & "', "
                fsSQL &= "'" & QuoteFilter(rtfqrydesc.Text) & "')"

                fnResult = gfInsertQry(fsSQL, gOdbcConn)

                If fnResult > 0 Then
                    MsgBox("Saved successfully!!", MsgBoxStyle.Information, gsProjectName)
                End If

                If MsgBox("Do you Want To Add another record ?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    Call btnNew_Click(sender, e)
                Else
                    ctrclear()
                    EnableSave(False)
                    btnNew.Focus()
                End If

            Else
                'Updation
                fsSQL = ""
                fsSQL &= " update sta_mst_tqry set "
                fsSQL &= " qry_code='" & QuoteFilter(txtqrycode.Text) & "', "
                fsSQL &= " qry_desc='" & QuoteFilter(rtfqrydesc.Text) & "' "
                fsSQL &= " where "
                fsSQL &= " qry_gid= " & txtid.Text
                fsSQL &= " and delete_flag = 'N' "

                fnResult = gfInsertQry(fsSQL, gOdbcConn)

                If fnResult > 0 Then
                    MsgBox("Updated successfully!!", MsgBoxStyle.Information, gsProjectName)
                End If
            End If
            ctrclear()
            EnableSave(False)
            btnNew.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
End Class