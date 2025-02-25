Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

Public Class frmDebarrtEntry
    Dim fobjDTorderdtl As DataTable
    Dim msSql As String

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Call Clear_Control()
        EnableSave(True)
        txtOrderno.Focus()
        txtSlno.Text = 1
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status

        pnlOrderHeader.Enabled = Status
        pnlDbtDtl.Enabled = Status

    End Sub

    Private Sub Clear_Control()
        Call frmCtrClear(Me)

        txtorder_Id.Text = ""
        txtremark.Text = ""
        dtpOrderDate.Text = Date.Now.ToString("dd/MM/yyyy")
        txtorderdtl_id.Text = ""
        txtSlno.Text = ""
        txtpanno.Text = ""
        txtholdername.Text = ""

        dgvDbtDetail.Columns.Clear()
        dgvDbtDetail.DataSource = Nothing

    End Sub

    Private Sub frmDebarrtEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call EnableSave(False)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If txtorder_Id.Text = "" Then
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
            SearchDialog = New frmSearch(gOdbcConn, _
                         " select a.debarrtorder_gid as 'Order Id',a.debarrtorder_no as 'Order No',a.debarrtorder_date as 'Order Date',a.debarrtorder_remark as 'Remarks'" & _
                         " from sta_trn_tdebarrtorder a", _
                         " a.debarrtorder_gid,a.debarrtorder_no,a.debarrtorder_date,a.debarrtorder_remark,b.debarrtorderdtl_gid ", _
                         " a.delete_flag='N' and a.debarrt_flag='Y' ")
            SearchDialog.ShowDialog()

            If Val(gnSearchId) <> 0 Then
                Call ListAll("select a.debarrtorder_gid,a.debarrtorder_no,a.debarrtorder_date,a.debarrtorder_remark from sta_trn_tdebarrtorder as a" _
                             & " where a.debarrtorder_gid = " & Val(gnSearchId) & " " _
                             & " and a.delete_flag = 'N' and a.debarrt_flag = 'Y' ", gOdbcConn)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal gsqry As String, ByVal odbcConn As MySqlConnection)
        Dim lobjDataReader As Odbc.OdbcDataReader
        Dim objDGVButtonCol As DataGridViewButtonColumn
        Dim DTorderdtl As DataTable
        Dim ds As DataSet
        Dim objColumn As DataColumn
        Dim objRow As DataRow
        Dim i As Integer = 0

        Try
            ds = gfDataSet(gsqry, "list_all", gOdbcConn)

            With ds.Tables("list_all")
                If .Rows.Count > 0 Then

                    txtorder_Id.Text = .Rows(0).Item("debarrtorder_gid")
                    txtOrderno.Text = .Rows(0).Item("debarrtorder_no").ToString
                    dtpOrderDate.Value = .Rows(0).Item("debarrtorder_date")
                    txtremark.Text = .Rows(0).Item("debarrtorder_remark").ToString

                    fobjDTorderdtl = Nothing

                    'Load order Details
                    gsqry = ""
                    gsqry &= " select a.debarrtorder_gid,a.debarrtorder_no,a.debarrtorder_date,a.debarrtorder_remark,"
                    gsqry &= " b.debarrtorderdtl_gid,b.debarrtorderdtl_slno,b.pan_holdername,pan_no"
                    gsqry &= " from sta_trn_tdebarrtorder as a "
                    gsqry &= " inner join sta_trn_tdebarrtorderdtl b on b.debarrtorder_gid = a.debarrtorder_gid and b.delete_flag = 'N' and b.debarrt_flag='Y' "
                    gsqry &= " where a.debarrtorder_gid=" & txtorder_Id.Text
                    gsqry &= " and a.delete_flag='N' and a.debarrt_flag='Y' "

                    DTorderdtl = GetDataTable(gsqry)

                    If DTorderdtl.Rows.Count > 0 Then
                        fobjDTorderdtl = New DataTable

                        objColumn = New DataColumn("debarrtorderdtl_gid")
                        objColumn.ColumnName = "OrderDetail Id"
                        fobjDTorderdtl.Columns.Add(objColumn)

                        objColumn = New DataColumn("debarrtorderdtl_slno")
                        objColumn.ColumnName = "Sl No"
                        fobjDTorderdtl.Columns.Add(objColumn)

                        objColumn = New DataColumn("pan_holdername")
                        objColumn.ColumnName = "Holder Name"
                        fobjDTorderdtl.Columns.Add(objColumn)

                        objColumn = New DataColumn("pan_no")
                        objColumn.ColumnName = "Pan No"
                        fobjDTorderdtl.Columns.Add(objColumn)


                        For i = 0 To DTorderdtl.Rows.Count - 1
                            objRow = fobjDTorderdtl.NewRow

                            objRow.Item("OrderDetail Id") = DTorderdtl.Rows(i).Item("debarrtorderdtl_gid").ToString
                            objRow.Item("Sl No") = DTorderdtl.Rows(i).Item("debarrtorderdtl_slno").ToString
                            objRow.Item("Holder Name") = DTorderdtl.Rows(i).Item("pan_holdername").ToString
                            objRow.Item("Pan No") = DTorderdtl.Rows(i).Item("pan_no").ToString

                            fobjDTorderdtl.Rows.Add(objRow)
                        Next i

                        dgvDbtDetail.DataSource = fobjDTorderdtl

                        For i = 0 To dgvDbtDetail.Columns.Count - 1
                            dgvDbtDetail.Columns(i).ReadOnly = True
                            dgvDbtDetail.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                        Next i

                        dgvDbtDetail.Columns("OrderDetail Id").Visible = False

                        If dgvDbtDetail.Columns.Count = fobjDTorderdtl.Columns.Count Then
                            ' EDIT
                            objDGVButtonCol = New DataGridViewButtonColumn
                            objDGVButtonCol.Name = "Select"
                            objDGVButtonCol.Text = "Select"
                            objDGVButtonCol.UseColumnTextForButtonValue = True
                            dgvDbtDetail.Columns.Add(objDGVButtonCol)

                            ' Remove
                            objDGVButtonCol = New DataGridViewButtonColumn
                            objDGVButtonCol.Name = "Remove"
                            objDGVButtonCol.Text = "Remove"
                            objDGVButtonCol.UseColumnTextForButtonValue = True
                            dgvDbtDetail.Columns.Add(objDGVButtonCol)
                        End If
                    End If
                    dgvDbtDetail.AllowUserToAddRows = False
                End If
            End With

            Dim maxSlNo As Integer
            For Each row As DataGridViewRow In dgvDbtDetail.Rows
                If Not row.IsNewRow Then
                    Dim currentSlNo As Integer
                    If Integer.TryParse(row.Cells("Sl No").Value.ToString(), currentSlNo) Then
                        If currentSlNo > maxSlNo Then
                            maxSlNo = currentSlNo
                        End If
                    End If
                End If
            Next

            DbtDetail_Clear()
            txtSlno.Text = maxSlNo + 1

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Call Clear_Control()
        Call EnableSave(False)
    End Sub

    Private Sub dgvDbtDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDbtDetail.CellClick
        Dim objfrm As Object
        Dim i As Integer

        Select Case dgvDbtDetail.Columns(dgvDbtDetail.CurrentCell.ColumnIndex).Name
            Case "Select"
                txtorderdtl_id.Text = dgvDbtDetail.Rows(dgvDbtDetail.CurrentCell.RowIndex).Cells("OrderDetail Id").Value
                txtSlno.Text = dgvDbtDetail.Rows(dgvDbtDetail.CurrentCell.RowIndex).Cells("Sl No").Value
                txtholdername.Text = dgvDbtDetail.Rows(dgvDbtDetail.CurrentCell.RowIndex).Cells("Holder Name").Value
                txtpanno.Text = dgvDbtDetail.Rows(dgvDbtDetail.CurrentCell.RowIndex).Cells("Pan No").Value

            Case "Remove"
                If MsgBox("Are you sure to remove ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    Call dgvDbtDetail.Rows.RemoveAt(dgvDbtDetail.CurrentCell.RowIndex)

                    Dim maxSlNo As Integer
                    For Each row As DataGridViewRow In dgvDbtDetail.Rows
                        If Not row.IsNewRow Then
                            Dim currentSlNo As Integer
                            If Integer.TryParse(row.Cells("Sl No").Value.ToString(), currentSlNo) Then
                                If currentSlNo > maxSlNo Then
                                    maxSlNo = currentSlNo
                                End If
                            End If
                        End If
                    Next

                    DbtDetail_Clear()
                    txtSlno.Text = maxSlNo + 1
                    txtpanno.Focus()

                End If
        End Select
    End Sub

    Private Sub DbtDetail_Clear()
        txtorderdtl_id.Text = ""
        txtSlno.Text = ""
        txtholdername.Text = ""
        txtpanno.Text = ""
    End Sub

    Private Sub btnClearGrid_Click(sender As Object, e As EventArgs) Handles btnClearGrid.Click
        DbtDetail_Clear()
    End Sub

    Function IsValidPAN(pan As String) As Boolean
        ' Define the PAN pattern
        Dim panPattern As String = "^[A-Z]{5}[0-9]{4}[A-Z]{1}$"

        ' Create a regex object with the pattern
        Dim regex As New Regex(panPattern)

        ' Check if the PAN matches the pattern
        If regex.IsMatch(pan) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim objColumn As DataColumn
        Dim objRow As DataRow
        Dim objDGVButtonCol As DataGridViewButtonColumn

        Dim panvalidation As Boolean = False
        Dim lsorderId As String
        Dim lsorderdtlId As String
        Dim lsOrderno As String
        Dim lsSlno As String
        Dim lsHoldername As String
        Dim lspanno As String

        Dim rowFound As Boolean = False

        Try
            lsorderId = txtorder_Id.Text
            lsOrderno = txtOrderno.Text.Trim()

            lsorderdtlId = txtorderdtl_id.Text
            lsSlno = txtSlno.Text.Trim()
            lsHoldername = txtholdername.Text.Trim()
            lspanno = txtpanno.Text.Trim()

            If lsOrderno = "" Then
                MsgBox("Please enter the Order No !", MsgBoxStyle.Information, gsProjectName)
                txtOrderno.Focus()
                Exit Sub
            End If

            If lsSlno = "" Then
                MsgBox("Please enter the Sl No !", MsgBoxStyle.Information, gsProjectName)
                txtSlno.Focus()
                Exit Sub
            End If

            If lspanno = "" Then
                MsgBox("Please enter the Pan No !", MsgBoxStyle.Information, gsProjectName)
                txtpanno.Focus()
                Exit Sub
            End If

            'pan Check
            panvalidation = IsValidPAN(lspanno)
            If panvalidation = False Then
                MsgBox("Invalid Pan No !", MsgBoxStyle.Critical, gsProjectName)
                txtpanno.Focus()
                Exit Sub
            End If

            If lsHoldername = "" Then
                MsgBox("Please enter the holdername !", MsgBoxStyle.Information, gsProjectName)
                txtholdername.Focus()
                Exit Sub
            End If

            ' Check for duplicate PAN No and same Sl No
            For Each row As DataGridViewRow In dgvDbtDetail.Rows
                If Not row.IsNewRow Then
                    If row.Cells("Sl No").Value.ToString() = lsSlno Then
                        ' Update the existing row with the same Sl No
                        row.Cells("OrderDetail Id").Value = lsorderdtlId
                        row.Cells("Holder Name").Value = lsHoldername
                        row.Cells("Pan No").Value = lspanno
                        rowFound = True
                        Exit For
                    ElseIf row.Cells("Pan No").Value.ToString() = lspanno Then
                        ' PAN No is duplicate
                        MsgBox("Duplicate Pan No found!", MsgBoxStyle.Critical, gsProjectName)
                        txtpanno.Focus()
                        Exit Sub
                    End If
                End If
            Next

            ' Check for duplicate PAN No and same Sl No
            For Each row As DataGridViewRow In dgvDbtDetail.Rows
                If Not row.IsNewRow Then
                    If row.Cells("Sl No").Value.ToString() = lsSlno Then
                        ' Update the existing row with the same Sl No
                        row.Cells("OrderDetail Id").Value = lsorderdtlId
                        row.Cells("Holder Name").Value = lsHoldername
                        row.Cells("Pan No").Value = lspanno
                        rowFound = True
                        Exit For
                    ElseIf row.Cells("Pan No").Value.ToString() = lspanno Then
                        ' PAN No is duplicate
                        MsgBox("Duplicate Pan No found!", MsgBoxStyle.Critical, gsProjectName)
                        txtpanno.Focus()
                        Exit Sub
                    End If
                End If
            Next

            If Not rowFound Then
                ' Add new row if no existing Sl No found
                If dgvDbtDetail.Rows.Count = 0 Then
                    fobjDTorderdtl = New DataTable

                    objColumn = New DataColumn("debarrtorderdtl_gid")
                    objColumn.ColumnName = "OrderDetail Id"
                    fobjDTorderdtl.Columns.Add(objColumn)

                    objColumn = New DataColumn("debarrtorderdtl_slno")
                    objColumn.ColumnName = "Sl No"
                    fobjDTorderdtl.Columns.Add(objColumn)

                    objColumn = New DataColumn("pan_holdername")
                    objColumn.ColumnName = "Holder Name"
                    fobjDTorderdtl.Columns.Add(objColumn)

                    objColumn = New DataColumn("pan_no")
                    objColumn.ColumnName = "Pan No"
                    fobjDTorderdtl.Columns.Add(objColumn)

                End If

                objRow = fobjDTorderdtl.NewRow

                objRow.Item("OrderDetail Id") = lsorderdtlId
                objRow.Item("Sl No") = lsSlno
                objRow.Item("Holder Name") = lsHoldername
                objRow.Item("Pan No") = lspanno

                fobjDTorderdtl.Rows.Add(objRow)

                dgvDbtDetail.DataSource = fobjDTorderdtl

                For i = 0 To dgvDbtDetail.Columns.Count - 1
                    dgvDbtDetail.Columns(i).ReadOnly = True
                    dgvDbtDetail.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i

                dgvDbtDetail.Columns("OrderDetail Id").Visible = False

                If dgvDbtDetail.Columns.Count = fobjDTorderdtl.Columns.Count Then
                    ' EDIT
                    objDGVButtonCol = New DataGridViewButtonColumn
                    objDGVButtonCol.Name = "Select"
                    objDGVButtonCol.Text = "Select"
                    objDGVButtonCol.UseColumnTextForButtonValue = True
                    dgvDbtDetail.Columns.Add(objDGVButtonCol)

                    ' Remove
                    objDGVButtonCol = New DataGridViewButtonColumn
                    objDGVButtonCol.Name = "Remove"
                    objDGVButtonCol.Text = "Remove"
                    objDGVButtonCol.UseColumnTextForButtonValue = True
                    dgvDbtDetail.Columns.Add(objDGVButtonCol)
                End If
            End If

            Dim maxSlNo As Integer
            For Each row As DataGridViewRow In dgvDbtDetail.Rows
                If Not row.IsNewRow Then
                    Dim currentSlNo As Integer
                    If Integer.TryParse(row.Cells("Sl No").Value.ToString(), currentSlNo) Then
                        If currentSlNo > maxSlNo Then
                            maxSlNo = currentSlNo
                        End If
                    End If
                End If
            Next

            DbtDetail_Clear()
            txtSlno.Text = maxSlNo + 1
            txtpanno.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub txtSlno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSlno.KeyPress
        ' Check if the pressed key is not a digit
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            ' If it's not a digit or a control key (like backspace), cancel the input
            e.Handled = True
        End If
    End Sub

    Private Sub txtpanno_TextChanged(sender As Object, e As EventArgs) Handles txtpanno.TextChanged
        ' Get the current text in the TextBox
        Dim currentText As String = txtpanno.Text

        ' Convert the text to uppercase
        txtpanno.Text = currentText.ToUpper()

        ' Move the cursor to the end of the text
        txtpanno.SelectionStart = txtpanno.Text.Length
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim lsSql As String
        Dim i As Integer
        Dim lnResult As Long

        Dim lspanmasterId As String
        Dim lsorderId As String
        Dim lsOrderno As String
        Dim lsordercount As Integer
        Dim lsorderDate As String
        Dim lsremark As String
        Dim lsinsert_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")

        Try
            lsorderId = txtorder_Id.Text.Trim()
            lsOrderno = txtOrderno.Text.Trim()
            lsremark = txtremark.Text.Trim()
            lsorderDate = dtpOrderDate.Value.ToString("yyyy-MM-dd")

            If lsOrderno = "" Then
                MsgBox("Please enter the Order No !", MsgBoxStyle.Information, gsProjectName)
                txtOrderno.Focus()
                Exit Sub
            End If

            If lsorderId = "" Then
                'Check Duplicate order number
                gsQry = ""
                lsordercount = 0
                gsQry &= " select count(*) from sta_trn_tdebarrtorder "
                gsQry &= " where debarrtorder_no = '" & QuoteFilter(lsOrderno) & "' "
                gsQry &= " and delete_flag = 'N' "
                lsordercount = Val(gfExecuteScalar(gsQry, gOdbcConn))
                If lsordercount > 0 Then
                    MsgBox("Duplicate Order No !", MsgBoxStyle.Information, gsProjectName)
                    txtOrderno.Focus()
                    Exit Sub
                End If

                'Insert on Debarrtorder Table
                lsSql = ""
                lnResult = 0

                lsSql &= " insert into sta_trn_tdebarrtorder(debarrtorder_no,debarrtorder_date,debarrtorder_remark,debarrt_flag,insert_date,insert_by) "
                lsSql &= " values( "
                lsSql &= "'" & QuoteFilter(lsOrderno) & "', "
                lsSql &= "'" & QuoteFilter(lsorderDate) & "',"
                lsSql &= "'" & QuoteFilter(lsremark) & "',"
                lsSql &= "'Y',"
                lsSql &= "'" & QuoteFilter(lsinsert_date) & "',"
                lsSql &= "'" & QuoteFilter(gsLoginUserCode) & "')"
                lnResult = gfInsertQry(lsSql, gOdbcConn)

                If lnResult > 0 Then
                    'Assigning debarrtorder_gid
                    gsQry = ""
                    gsQry &= " select max(debarrtorder_gid) from sta_trn_tdebarrtorder "
                    lsorderId = Val(gfExecuteScalar(gsQry, gOdbcConn))
                End If

            Else
                'Check Duplicate order number
                gsQry = ""
                lsordercount = 0
                gsQry &= " select count(*) from sta_trn_tdebarrtorder "
                gsQry &= " where debarrtorder_no = '" & QuoteFilter(lsOrderno) & "' "
                gsQry &= " and delete_flag = 'N' and debarrtorder_gid <> '" & QuoteFilter(lsorderId) & "' "
                lsordercount = Val(gfExecuteScalar(gsQry, gOdbcConn))
                If lsordercount > 0 Then
                    MsgBox("Duplicate Order No !", MsgBoxStyle.Information, gsProjectName)
                    txtOrderno.Focus()
                    Exit Sub
                End If

                'Update on Debarrtorder Table
                lsSql = ""
                lnResult = 0

                lsSql &= " update sta_trn_tdebarrtorder set "
                lsSql &= " debarrtorder_no = '" & QuoteFilter(lsOrderno) & "',"
                lsSql &= " debarrtorder_date = '" & QuoteFilter(lsorderDate) & "',"
                lsSql &= " debarrtorder_remark = '" & QuoteFilter(lsremark) & "',"
                lsSql &= " update_date = sysdate(), "
                lsSql &= " update_by = '" & QuoteFilter(gsLoginUserCode) & "'"
                lsSql &= " where debarrtorder_gid = '" & QuoteFilter(lsorderId) & "' "
                lsSql &= " and delete_flag = 'N' and debarrt_flag = 'Y' "
                lnResult = gfInsertQry(lsSql, gOdbcConn)
            End If

            If lnResult > 0 Then
                'Delete on DebarrtorderDTL Table
                lsSql = ""
                lnResult = 0
                lsSql &= " delete from sta_trn_tdebarrtorderdtl "
                lsSql &= " where debarrtorder_gid = '" & QuoteFilter(lsorderId) & "' "
                lnResult = Val(gfExecuteScalar(lsSql, gOdbcConn))

                With dgvDbtDetail
                    For i = 0 To .Rows.Count - 1
                        'Get Panmaster_gid on Panmaster Table
                        gsQry = ""
                        gsQry &= " select panmaster_gid from sta_mst_tpanmaster where pan_no = "
                        gsQry &= " '" & .Rows(i).Cells("Pan No").Value.ToString & "' and delete_flag = 'N' "
                        lspanmasterId = Val(gfExecuteScalar(gsQry, gOdbcConn))

                        If lspanmasterId = 0 Then
                            'If Not Exist means Insert On Pan Master Table
                            gsQry = ""
                            lnResult = 0
                            gsQry &= " insert into sta_mst_tpanmaster(pan_holdername,pan_no,debarrt_flag,last_updated_datetime,insert_date,insert_by) "
                            gsQry &= " values ("
                            gsQry &= " '" & .Rows(i).Cells("Holder Name").Value & "',"
                            gsQry &= " '" & .Rows(i).Cells("Pan No").Value.ToString & "',"
                            gsQry &= " 'Y',"
                            gsQry &= "'" & QuoteFilter(lsinsert_date) & "',"
                            gsQry &= "'" & QuoteFilter(lsinsert_date) & "',"
                            gsQry &= "'" & QuoteFilter(gsLoginUserCode) & "')"
                            lnResult = gfInsertQry(gsQry, gOdbcConn)
                        Else
                            'If Exist means Update On Pan Master Table
                            lsSql = ""
                            lnResult = 0

                            lsSql &= " update sta_mst_tpanmaster set "
                            lsSql &= " pan_holdername = '" & .Rows(i).Cells("Holder Name").Value & "',"
                            lsSql &= " pan_no = '" & .Rows(i).Cells("Pan No").Value & "',"
                            lsSql &= " debarrt_flag = 'Y',"
                            lsSql &= " last_updated_datetime = sysdate(), "
                            lsSql &= " update_date = sysdate(), "
                            lsSql &= " update_by = '" & QuoteFilter(gsLoginUserCode) & "'"
                            lsSql &= " where panmaster_gid = '" & QuoteFilter(lspanmasterId) & "' "
                            lsSql &= " and delete_flag = 'N' "
                            lnResult = gfInsertQry(lsSql, gOdbcConn)
                        End If

                        'Insert on DebarrtorderDTL Table
                        gsQry = ""
                        lnResult = 0

                        gsQry &= " insert into sta_trn_tdebarrtorderdtl(debarrtorder_gid,debarrtorderdtl_slno,pan_holdername,pan_no,debarrt_flag,insert_date,insert_by) "
                        gsQry &= " values ("
                        gsQry &= " " & lsorderId & ","
                        gsQry &= " '" & .Rows(i).Cells("Sl No").Value.ToString & "',"
                        gsQry &= " '" & .Rows(i).Cells("Holder Name").Value & "',"
                        gsQry &= " '" & .Rows(i).Cells("Pan No").Value.ToString & "',"
                        gsQry &= " 'Y',"
                        gsQry &= "'" & QuoteFilter(lsinsert_date) & "',"
                        gsQry &= "'" & QuoteFilter(gsLoginUserCode) & "')"
                        lnResult = gfInsertQry(gsQry, gOdbcConn)

                        'Insert on DebarrtpanAudit Table
                        gsQry = ""
                        lnResult = 0

                        gsQry &= " insert into sta_trn_tdebarrtpanaudit(pan_no,pan_holdername,debarrt_flag,order_from,order_ref_gid,insert_date,insert_by) "
                        gsQry &= " values ("
                        gsQry &= " '" & .Rows(i).Cells("Pan No").Value.ToString & "',"
                        gsQry &= " '" & .Rows(i).Cells("Holder Name").Value & "',"
                        gsQry &= " 'Y',"
                        gsQry &= " 'D',"
                        gsQry &= " " & lsorderId & ","
                        gsQry &= "'" & QuoteFilter(lsinsert_date) & "',"
                        gsQry &= "'" & QuoteFilter(gsLoginUserCode) & "')"
                        lnResult = gfInsertQry(gsQry, gOdbcConn)

                    Next
                End With
            End If
            MessageBox.Show("Record saved Successfully..!", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call Clear_Control()

            If MsgBox("Do you want to add another record ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
                btnNew.PerformClick()
            Else
                Call EnableSave(False)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim lsSql As String
        Dim lnResult As Integer
        Dim lsorderId As Integer
        Dim lspannos As String

        Try
            lsorderId = Val(txtorder_Id.Text)

            If txtorder_Id.Text = "" Then
                If MsgBox("Select record to delete?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                End If
            Else
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then

                    'Delete on Debarrtorder Table
                    lsSql = ""
                    lnResult = 0
                    lsSql &= " delete from sta_trn_tdebarrtorder "
                    lsSql &= " where debarrtorder_gid = '" & QuoteFilter(lsorderId) & "' "
                    lnResult = Val(gfExecuteScalar(lsSql, gOdbcConn))

                    'Get pan_no on Panmaster Table
                    gsQry = ""
                    gsQry &= " select group_concat(pan_no) from sta_trn_tdebarrtorderdtl "
                    gsQry &= " where debarrtorder_gid = '" & QuoteFilter(lsorderId) & "' "
                    lspannos = gfExecuteScalar(gsQry, gOdbcConn)
                    lspannos = "'" & lspannos.Replace(",", "','") & "'"

                    'Delete on DebarrtorderDTL Table
                    lsSql = ""
                    lnResult = 0
                    lsSql &= " delete from sta_trn_tdebarrtorderdtl "
                    lsSql &= " where debarrtorder_gid = '" & QuoteFilter(lsorderId) & "' "
                    lnResult = Val(gfExecuteScalar(gsQry, gOdbcConn))

                    'Update on Panmsater Table
                    lsSql = ""
                    lnResult = 0
                    lsSql &= " update sta_mst_tpanmaster set "
                    lsSql &= " debarrt_flag = 'N' "
                    lsSql &= " where delete_flag = 'N' and pan_no in(" & lspannos & ") "
                    lnResult = Val(gfExecuteScalar(lsSql, gOdbcConn))

                    MsgBox("Record Deleted Successfully..!", MsgBoxStyle.Information, gsProjectName)
                    btnNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub
End Class