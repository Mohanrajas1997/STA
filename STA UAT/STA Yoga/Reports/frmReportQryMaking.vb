Public Class frmReportQryMaking
    Dim dtTables As DataTable
    Dim i As Integer

    Private Sub LoadCtrls()
        Try
            Dim i As Integer

            gsQry = " show tables like 'sta_%'"
            dtTables = GetDataTable(gsqry)

            For i = 0 To dtTables.Rows.Count - 1
                lstTables.Items.Add(dtTables.Rows(i).Item(0).ToString)
            Next i

            lstSelectedTables.Items.Clear()
            lstConditions.Items.Clear()

            lstTables.Sorted = True
            lstSelectedTables.Sorted = True
            lstConditions.Sorted = False

            lstRptFields.Sorted = True
            lstRptSelectedFields.Sorted = True

            With cboCondition
                .Items.Clear()
                .Items.Add("Like")
                .Items.Add("Not Like")
                .Items.Add("=")
                .Items.Add(">")
                .Items.Add(">=")
                .Items.Add("<")
                .Items.Add("<=")
                .Items.Add("<>")
                .Text = "="
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmReportQryMaking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadCtrls()
    End Sub

    Private Sub AddTable()
        Try
            Dim i As Integer
            If Not IsNothing(lstTables.SelectedItem) Then
                lstRptSelectedFields.Items.Clear()
                For i = 0 To lstSelectedTables.Items.Count - 1
                    If lstSelectedTables.Items(i).ToString = lstTables.SelectedItem Then
                        Exit For
                    End If
                Next
                If i = lstSelectedTables.Items.Count Then
                    lstSelectedTables.Items.Add(lstTables.SelectedItem)
                    lstTables.Items.RemoveAt(lstTables.SelectedIndex)
                End If
            End If

            If lstSelectedTables.Items.Count > 0 Then
                Dim dtFields As DataTable
                Dim j As Integer

                lstRptFields.Items.Clear()
                lstRptSelectedFields.Items.Clear()

                For i = 0 To lstSelectedTables.Items.Count - 1
                    gsqry = " desc " & lstSelectedTables.Items(i).ToString

                    dtFields = GetDataTable(gsqry)
                    For j = 0 To dtFields.Rows.Count - 1
                        lstRptFields.Items.Add(lstSelectedTables.Items(i).ToString & "." & dtFields.Rows(j).Item("field").ToString)
                    Next j

                    dtFields.Rows.Clear()
                Next
            Else
                lstRptFields.Items.Clear()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnTblSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTblSelect.Click
        Call AddTable()
    End Sub

    Private Sub lstTables_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTables.DoubleClick
        Call AddTable()
    End Sub

    Private Sub RemoveTable()
        Dim dtFields As DataTable
        Dim i As Integer
        Dim j As Integer

        Try
            If Not IsNothing(lstSelectedTables.SelectedItem) Then
                lstRptSelectedFields.Items.Clear()
                lstTables.Items.Add(lstSelectedTables.SelectedItem)
                lstSelectedTables.Items.RemoveAt(lstSelectedTables.SelectedIndex)
            End If

            If lstSelectedTables.Items.Count > 0 Then
                lstRptFields.Items.Clear()
                lstRptSelectedFields.Items.Clear()

                For i = 0 To lstSelectedTables.Items.Count - 1
                    gsQry = " desc " & lstSelectedTables.Items(i).ToString

                    dtFields = GetDataTable(gsQry)
                    For j = 0 To dtFields.Rows.Count - 1
                        lstRptFields.Items.Add(lstSelectedTables.Items(i).ToString & "." & dtFields.Rows(j).Item("field").ToString)
                    Next j

                    dtFields.Rows.Clear()
                Next
            Else
                lstRptFields.Items.Clear()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnTblDeselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTblDeselect.Click
        Call RemoveTable()
    End Sub

    Private Sub lstSelectedTables_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSelectedTables.DoubleClick
        Call RemoveTable()
    End Sub

    Private Sub btnCondAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCondAdd.Click
        Dim lsCond As String
        Dim lbFieldFlag As Boolean = False
        Dim i As Integer

        Try
            If cboCondField.Text = "" Then
                MsgBox("Please select the Condition field ..", MsgBoxStyle.Information, gsProjectName)
                cboCondField.Focus()
                Exit Sub
            End If

            If cboCondition.Text = "" Then
                MsgBox("Please select the Condition ..", MsgBoxStyle.Information, gsProjectName)
                cboCondition.Focus()
                Exit Sub
            End If

            If cboCondValue.Text = "" Then
                MsgBox("Please select the Condition Value ..", MsgBoxStyle.Information, gsProjectName)
                cboCondValue.Focus()
                Exit Sub
            End If

            lsCond = cboCondField.Text

            For i = 0 To cboCondField.Items.Count - 1
                If cboCondValue.Text = cboCondField.Items(i).ToString Then
                    lbFieldFlag = True
                    Exit For
                End If
            Next

            If lbFieldFlag = True Then
                lsCond &= " " & cboCondition.Text & " " & cboCondValue.Text
            Else
                If cboCondition.Text.ToUpper = "LIKE" Or cboCondition.Text.ToUpper = "NOT LIKE" Then
                    lsCond &= " " & cboCondition.Text & " '" & cboCondValue.Text & "%'"
                Else
                    lsCond &= " " & cboCondition.Text & " '" & cboCondValue.Text & "'"
                End If
            End If

            lstConditions.Items.Add(lsCond)

            cboCondField.Text = ""
            cboCondition.Text = "="
            cboCondValue.Text = ""

            lsCond = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnCondRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCondRemove.Click
        Try
            If Not IsNothing(lstConditions.SelectedItem) Then
                lstConditions.Items.RemoveAt(lstConditions.SelectedIndex)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim objfrm As frmQuickView
        Dim lsQry As String
        Dim i As Integer

        lsQry = " select "

        If lstRptSelectedFields.Items.Count > 0 Then
            'Report Fields
            For i = 0 To lstRptSelectedFields.Items.Count - 1
                lsQry &= lstRptSelectedFields.Items(i).ToString & ","
            Next

            lsQry = lsQry.TrimEnd(",")
            lsQry &= " from "

            'Report Tables
            For i = 0 To lstSelectedTables.Items.Count - 1
                lsQry &= lstSelectedTables.Items(i).ToString & ","
            Next
            lsQry = lsQry.TrimEnd(",")

            'Report Conditions
            If lstConditions.Items.Count > 0 Then
                lsQry &= " where "
                For i = 0 To lstConditions.Items.Count - 1
                    If i = lstConditions.Items.Count - 1 Then
                        lsQry &= lstConditions.Items(i).ToString
                    Else
                        lsQry &= lstConditions.Items(i).ToString & " and "
                    End If
                Next
            End If

            objfrm = New frmQuickView(gOdbcConn, lsQry)
            objfrm.ShowDialog()
        Else
            MsgBox("Please select the Report Fields.. ", MsgBoxStyle.Information, gsProjectName)
            lstRptFields.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub lstRptFields_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRptFields.GotFocus
        If lstSelectedTables.Items.Count > 0 Then
            Dim dtFields As DataTable
            Dim i As Integer
            Dim j As Integer

            lstRptFields.Items.Clear()

            For i = 0 To lstSelectedTables.Items.Count - 1
                gsqry = " desc " & lstSelectedTables.Items(i).ToString

                dtFields = GetDataTable(gsqry)
                For j = 0 To dtFields.Rows.Count - 1
                    lstRptFields.Items.Add(lstSelectedTables.Items(i).ToString & "." & dtFields.Rows(j).Item("field").ToString)
                Next j

                dtFields.Rows.Clear()
            Next
        Else
            'MsgBox("Please select Tables for Report Fields..", MsgBoxStyle.Information, gsProjectName)
            'lstTables.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub AddRptField()
        Try
            Dim i As Integer
            If Not IsNothing(lstRptFields.SelectedItem) Then
                For i = 0 To lstRptSelectedFields.Items.Count - 1
                    If lstRptSelectedFields.Items(i).ToString = lstRptFields.SelectedItem Then
                        Exit For
                    End If
                Next

                If i = lstRptSelectedFields.Items.Count Then
                    lstRptSelectedFields.Items.Add(lstRptFields.SelectedItem)
                    lstRptFields.Items.RemoveAt(lstRptFields.SelectedIndex)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRptFieldSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptFieldSelect.Click
        Call AddRptField()
    End Sub

    Private Sub lstRptFields_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRptFields.DoubleClick
        Call AddRptField()
    End Sub

    Private Sub RemoveRptField()
        Try
            If Not IsNothing(lstRptSelectedFields.SelectedItem) Then
                lstRptFields.Items.Add(lstRptSelectedFields.SelectedItem)
                lstRptSelectedFields.Items.RemoveAt(lstRptSelectedFields.SelectedIndex)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRptFieldDeselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptFieldDeselect.Click
        Call RemoveRptField()
    End Sub

    Private Sub lstRptSelectedFields_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRptSelectedFields.DoubleClick
        Call RemoveRptField()
    End Sub

    Private Sub cboCondField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCondField.GotFocus
        Dim dtFields As DataTable
        Dim i As Integer
        Dim j As Integer

        Try
            cboCondField.Items.Clear()
            For i = 0 To lstSelectedTables.Items.Count - 1
                gsQry = " desc " & lstSelectedTables.Items(i).ToString

                dtFields = GetDataTable(gsQry)
                For j = 0 To dtFields.Rows.Count - 1
                    cboCondField.Items.Add(lstSelectedTables.Items(i).ToString & "." & dtFields.Rows(j).Item("field").ToString)
                Next j

                dtFields.Rows.Clear()
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboCondValue_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCondValue.GotFocus
        Dim dtFields As DataTable
        Dim i As Integer
        Dim j As Integer

        Try
            cboCondValue.Items.Clear()
            For i = 0 To lstSelectedTables.Items.Count - 1
                gsQry = " desc " & lstSelectedTables.Items(i).ToString

                dtFields = GetDataTable(gsQry)
                For j = 0 To dtFields.Rows.Count - 1
                    cboCondValue.Items.Add(lstSelectedTables.Items(i).ToString & "." & dtFields.Rows(j).Item("field").ToString)
                Next j

                dtFields.Rows.Clear()
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lstRptFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstRptFields.SelectedIndexChanged

    End Sub
End Class