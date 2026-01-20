Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices
Public Class frmiepfmaker
#Region "Local Variables"
    Dim GetFolioNo As String = ""
    Dim GetCompName As String = ""
    Dim GetName As String = ""
    Dim GetName2 As String = ""
    Dim GetName3 As String = ""
    Dim GetInwardID As Long
    Dim GetFolioGid As Long
    Dim GetGroupCode As String
    Dim GetQueueFrom As String
    Dim GetNomineeName As String
    Dim GetCompGid As Integer
    Dim GetInwardNo As Long
    Dim GetSharePrice As Decimal
#End Region
    Private isHandlingValueChange As Boolean = False
    Private selectedType As String = ""
    Private isChecklistReadonly As Boolean = False
    Private inputsPendingSave As Boolean = False
    Private selectedStatus As String = ""
    Private currentChecklistRowIndex As Integer = -1
    Private isReadOnlyMode As Boolean = False
    Private HideFlag As String = "N"
    Public Property HideButtonsForViewMode As Boolean = False
    Public Property coveringstatus As String = ""
    Public Property RequestType As String = ""
    Public Property coveringReqGid As Integer = 0
    Private Property ReqGid As Integer = 0
    Private removedChecklistRows As New List(Of DataGridViewRow)
    'Private removedChecklistRows As New List(Of (Row As DataGridViewRow, Index As Integer))

    Public Sub New(inward_gid As Integer, GroupCode As String)
        InitializeComponent()
        LoadClaimDetails(inward_gid, GroupCode)
    End Sub

    Private Sub frmRequestType_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        claimant.Checked = False
        shareholder.Checked = False
        nominee.Checked = False
        claimant_name_txt.ReadOnly = True
        claimant_addr_txt.ReadOnly = True
        claimant_email_txt.ReadOnly = True

        If GetGroupCode = "C" Then
            GetQueueFrom = "Maker"
        ElseIf GetGroupCode = "V" Then
            GetQueueFrom = "Checker"
            'GetQueueFrom = "Maker"
            HideButtonsForViewMode = True
        End If

        ' Inward Case
        If GetGroupCode = "M" And GetQueueFrom <> "Checker" Then
            If GetFolioNo <> "" Then
                foliono_txt.Text = GetFolioNo
                cmp_txt.Text = GetCompName
                name_txt.Text = GetName
                name1_txt.Text = GetName2
                name2_txt.Text = GetName3
                nominee_txt.Text = GetNomineeName

                If nominee_txt.Text = "" Then
                    nominee.Enabled = False
                Else
                    nominee.Enabled = True
                End If

                'sharevalue_txt.Text = GetShareValue
                cert_details()
            End If
        End If

        ' Maker To Checker / Inex / View Case
        If (GetGroupCode = "C" Or GetGroupCode = "I" Or GetGroupCode = "V") And (GetQueueFrom = "Maker" Or GetQueueFrom = "Checker") Then
            Dim isReadOnly As Boolean = True
            submit.Visible = False
            inex.Visible = False

            If GetGroupCode = "I" Then
                HideButtonsForViewMode = True
                coveringstatus = "Inex"
                Que_inex_btn.Visible = True
                reprocess_btn.Visible = True
            Else
                Que_inex_btn.Visible = False
                reprocess_btn.Visible = False
            End If

            If HideButtonsForViewMode Then
                HideFlag = "Y"
                approve_btn.Visible = False
                reject_btn.Visible = False
                remark_txt.ReadOnly = True
            Else
                approve_btn.Visible = True
                reject_btn.Visible = True
            End If

            foliono_txt.Text = GetFolioNo
            cmp_txt.Text = GetCompName
            name_txt.Text = GetName
            name1_txt.Text = GetName2
            name2_txt.Text = GetName3
            nominee_txt.Text = GetNomineeName

            If nominee_txt.Text = "" Then
                nominee.Enabled = False
            Else
                nominee.Enabled = True
            End If

            'sharevalue_txt.Text = GetShareValue

            'If coveringstatus = "Inex" Then
            '    status_value_lab.Text = "Invalid"
            '    status_value_lab.ForeColor = Color.Red
            '    inexreasonview_btn.Visible = True

            'Else
            '    inexreasonview_btn.Visible = False
            '    status_value_lab.Text = "Valid"
            '    status_value_lab.ForeColor = Color.DarkGreen
            'End If

            cert_details()

            Dim dtClaims As DataTable = get_iepfclaims(GetFolioGid, GetInwardID)

            If dtClaims.Rows.Count > 0 Then
                ReqGid = Convert.ToInt32(dtClaims.Rows(0)("req_gid"))

                pre_rmk_lab.Visible = True
                pre_rmk_txt.Visible = True
                pre_rmk_txt.Text = ""
                Get_Remarks(GetInwardID, GetGroupCode, GetQueueFrom, HideFlag)
                Get_Remarks(GetInwardID, GetGroupCode, GetQueueFrom, "N")

                Dim reqType As String = dtClaims.Rows(0)("req_type").ToString().ToLower()
                Dim claimantname As String = dtClaims.Rows(0)("req_claimant_name").ToString()
                Dim claimantaddr As String = dtClaims.Rows(0)("req_claimant_addr").ToString()
                Dim claimantemail As String = dtClaims.Rows(0)("req_claimant_email").ToString()
                Dim reqstatus As String = dtClaims.Rows(0)("req_queue_status").ToString()

                coveringstatus = reqstatus
                RequestType = reqType
                coveringReqGid = ReqGid

                If coveringstatus = "Inex" Then
                    inexreasonview_btn.Visible = True
                    status_value_lab.Text = "Invalid"
                    status_value_lab.ForeColor = Color.Red
                Else
                    inexreasonview_btn.Visible = False
                    status_value_lab.Text = "Valid"
                    status_value_lab.ForeColor = Color.DarkGreen
                End If

                Select Case reqType
                    Case "claimant"
                        claimant.Checked = True
                        shareholder.Enabled = False
                        nominee.Enabled = False
                        claimant_name_txt.Text = claimantname
                        claimant_addr_txt.Text = claimantaddr
                        claimant_email_txt.Text = claimantemail

                        claimant_name_txt.ReadOnly = True
                        claimant_addr_txt.ReadOnly = True
                        claimant_email_txt.ReadOnly = True
                    Case "shareholder"
                        shareholder.Checked = True
                        claimant.Enabled = False
                        nominee.Enabled = False
                    Case "nominee"
                        nominee.Checked = True
                        claimant.Enabled = False
                        shareholder.Enabled = False
                End Select

                dgvChecklist.Visible = True
                dgvChecklist.DataSource = Nothing
                dgvChecklist.Rows.Clear()
                dgvChecklist.ReadOnly = True

                LoadChecklistChecker(selectedType, "")

                For Each claimRow As DataRow In dtClaims.Rows
                    Dim spChecklistGID As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim spStatus As String = claimRow("checklist_status").ToString()
                    Dim Inp_Flag As String = claimRow("input_flag").ToString()
                    Dim chklst_gid As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim checkliststatus As String = claimRow("checklist_status").ToString()

                    For i As Integer = 0 To dgvChecklist.Rows.Count - 1
                        Dim dgvRow As DataGridViewRow = dgvChecklist.Rows(i)
                        If dgvRow.IsNewRow Then Continue For

                        Dim gridChecklistGID As Integer = Convert.ToInt32(dgvRow.Cells("ChecklistGID").Value)

                        If gridChecklistGID = spChecklistGID Then

                            dgvRow.Cells("Ok").Value = (spStatus = "O")
                            dgvRow.Cells("NotOk").Value = (spStatus = "N")

                            If Inp_Flag = "Y" Then
                                Dim status As String = If(checkliststatus = "O", "OK", "NOTOK")
                                LoadChecklistInput(chklst_gid, chklst_inputs, status)
                                Dim dtInputs As DataTable = Get_InputValues(ReqGid)

                                ' Bind values to controls
                                For Each ctrl As Control In chklst_inputs.Controls
                                    Dim ctrlGid As Integer = Convert.ToInt32(ctrl.Tag)

                                    ' Match with master checklist gid
                                    Dim drs() As DataRow = dtInputs.Select("chklstinput_mstinput_gid  = " & ctrlGid)
                                    If drs.Length > 0 Then
                                        ' If you have multiple rows for the same master gid, loop them
                                        For Each dr As DataRow In drs
                                            If TypeOf ctrl Is TextBox Then
                                                'CType(ctrl, TextBox).Text = dr("chklstinput_inputvalue").ToString()
                                                Dim txt As TextBox = CType(ctrl, TextBox)
                                                txt.Text = dr("chklstinput_inputvalue").ToString()
                                                txt.ReadOnly = True
                                            ElseIf TypeOf ctrl Is ComboBox Then
                                                CType(ctrl, ComboBox).SelectedValue = dr("chklstinput_inputvalue").ToString()
                                            End If
                                        Next
                                    End If
                                Next
                                currentChecklistRowIndex = i
                                selectedStatus = status
                                inputsPendingSave = False
                            End If

                            Dim childCount As Integer = 0
                            If dgvRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(dgvRow.Cells("ChildCount").Value) Then
                                childCount = Convert.ToInt32(dgvRow.Cells("ChildCount").Value)
                            End If

                            If childCount > 0 Then
                                Dim status As String = If(spStatus = "O", "OK", "NOTOK")
                                LoadChildChecklist(spChecklistGID, i + 1, status)
                                dgvRow.Cells("ChildLoaded").Value = True
                            End If

                            Exit For
                        End If
                    Next
                Next

                '' Collect all GIDs returned by SP
                Dim spGids As New HashSet(Of Integer)(
                    dtClaims.AsEnumerable().Select(Function(r) Convert.ToInt32(r("checklist_gid")))
                )

                ' Hide rows not in SP result
                For Each dgvRow As DataGridViewRow In dgvChecklist.Rows
                    If dgvRow.IsNewRow Then Continue For

                    Dim gridChecklistGID As Integer = Convert.ToInt32(dgvRow.Cells("ChecklistGID").Value)

                    ' If grid GID not in SP result → hide row
                    dgvRow.Visible = spGids.Contains(gridChecklistGID)
                Next

            End If
        End If

        ' Checker To Maker Case
        If GetGroupCode = "M" And (GetQueueFrom = "Checker" Or GetQueueFrom = "Inex") Then

            Dim isReadOnly As Boolean = True
            submit.Enabled = True

            If nominee_txt.Text = "" Then
                nominee.Enabled = False
            Else
                nominee.Enabled = True
            End If

            foliono_txt.Text = GetFolioNo
            cmp_txt.Text = GetCompName
            name_txt.Text = GetName
            name1_txt.Text = GetName2
            name2_txt.Text = GetName3
            nominee_txt.Text = GetNomineeName
            'sharevalue_txt.Text = GetShareValue
            status_value_lab.Text = "Valid"
            status_value_lab.ForeColor = Color.DarkGreen

            If GetQueueFrom = "Inex" Then
                status_value_lab.Text = "Invalid"
                status_value_lab.ForeColor = Color.Red
                submit.Enabled = False
                inex.Enabled = True
            Else
                status_value_lab.Text = "Valid"
                status_value_lab.ForeColor = Color.DarkGreen
                submit.Enabled = True
                inex.Enabled = False
            End If

            cert_details()

            Dim dtClaims As DataTable = get_iepfclaims(GetFolioGid, GetInwardID)

            If dtClaims.Rows.Count > 0 Then
                ReqGid = Convert.ToInt32(dtClaims.Rows(0)("req_gid"))

                pre_rmk_lab.Visible = True
                pre_rmk_txt.Visible = True
                pre_rmk_txt.Text = ""
                Get_Remarks(GetInwardID, GetGroupCode, GetQueueFrom, HideFlag)

                Dim reqType As String = dtClaims.Rows(0)("req_type").ToString().ToLower()
                Dim claimantname As String = dtClaims.Rows(0)("req_claimant_name").ToString()
                Dim claimantaddr As String = dtClaims.Rows(0)("req_claimant_addr").ToString()
                Dim claimantemail As String = dtClaims.Rows(0)("req_claimant_email").ToString()
                Select Case reqType
                    Case "claimant"
                        claimant.Checked = True
                        'shareholder.Enabled = False
                        'nominee.Enabled = False

                        claimant_name_txt.Text = claimantname
                        claimant_addr_txt.Text = claimantaddr
                        claimant_email_txt.Text = claimantemail
                    Case "shareholder"
                        shareholder.Checked = True
                        'claimant.Enabled = False
                        'nominee.Enabled = False
                    Case "nominee"
                        nominee.Checked = True
                        ' claimant.Enabled = False
                        'shareholder.Enabled = False
                End Select

                dgvChecklist.Visible = True
                dgvChecklist.DataSource = Nothing
                dgvChecklist.Rows.Clear()
                dgvChecklist.ReadOnly = True

                LoadChecklist(selectedType, "")

                For Each claimRow As DataRow In dtClaims.Rows
                    Dim spChecklistGID As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim spStatus As String = claimRow("checklist_status").ToString()

                    Dim Inp_Flag As String = claimRow("input_flag").ToString()
                    Dim chklst_gid As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim checkliststatus As String = claimRow("checklist_status").ToString()
                    'Dim ExpectedValue As String = claimRow("expected_value").ToString()
                    'Dim ExpectedOKCount As Integer = claimRow("expected_okcount")
                    'Dim ExpectedNotOKCount As Integer = claimRow("expected_notokcount")

                    For i As Integer = 0 To dgvChecklist.Rows.Count - 1
                        Dim dgvRow As DataGridViewRow = dgvChecklist.Rows(i)
                        If dgvRow.IsNewRow Then Continue For

                        Dim gridChecklistGID As Integer = Convert.ToInt32(dgvRow.Cells("ChecklistGID").Value)

                        If gridChecklistGID = spChecklistGID Then

                            dgvRow.Cells("Ok").Value = (spStatus = "O")
                            dgvRow.Cells("NotOk").Value = (spStatus = "N")
                            'dgvRow.Cells("ArrivedValue").Value = ExpectedValue
                            'dgvRow.Cells("ArrivedOkCount").Value = ExpectedOKCount
                            'dgvRow.Cells("ArrivedNotOkCount").Value = ExpectedNotOKCount

                            If Inp_Flag = "Y" Then
                                Dim status As String = If(checkliststatus = "O", "OK", "NOTOK")
                                LoadChecklistInput(chklst_gid, chklst_inputs, status)
                                Dim dtInputs As DataTable = Get_InputValues(ReqGid)

                                ' Bind values to controls
                                For Each ctrl As Control In chklst_inputs.Controls
                                    Dim ctrlGid As Integer = Convert.ToInt32(ctrl.Tag)

                                    ' Match with master checklist gid
                                    Dim drs() As DataRow = dtInputs.Select("chklstinput_mstinput_gid  = " & ctrlGid)
                                    If drs.Length > 0 Then
                                        ' If you have multiple rows for the same master gid, loop them
                                        For Each dr As DataRow In drs
                                            If TypeOf ctrl Is TextBox Then
                                                'CType(ctrl, TextBox).Text = dr("chklstinput_inputvalue").ToString()
                                                Dim txt As TextBox = CType(ctrl, TextBox)
                                                txt.Text = dr("chklstinput_inputvalue").ToString()
                                            ElseIf TypeOf ctrl Is ComboBox Then
                                                CType(ctrl, ComboBox).SelectedValue = dr("chklstinput_inputvalue").ToString()
                                            End If
                                        Next
                                    End If
                                Next
                                currentChecklistRowIndex = i
                                selectedStatus = status
                                inputsPendingSave = False
                            End If

                            Dim childCount As Integer = 0
                            If dgvRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(dgvRow.Cells("ChildCount").Value) Then
                                childCount = Convert.ToInt32(dgvRow.Cells("ChildCount").Value)
                            End If

                            If childCount > 0 Then
                                Dim status As String = If(spStatus = "O", "OK", "NOTOK")
                                LoadChildChecklist(spChecklistGID, i + 1, status)
                                dgvRow.Cells("ChildLoaded").Value = True
                            End If

                            Exit For
                        End If
                    Next
                Next
            End If
        End If

    End Sub

    Private Sub shareholder_CheckedChanged(sender As Object, e As EventArgs) Handles shareholder.CheckedChanged
        Dim spreqtype As String = ""

        If shareholder.Checked = True Then
            selectedType = "shareholder"
            claimant_name_txt.ReadOnly = True
            claimant_addr_txt.ReadOnly = True
            claimant_email_txt.ReadOnly = True

            dgvChecklist.Visible = True
            dgvChecklist.DataSource = Nothing
            dgvChecklist.Rows.Clear()
            chklst_inputs.Controls.Clear()
            LoadChecklist(selectedType, "")

            Dim dtClaims As DataTable = get_iepfclaims(GetFolioGid, GetInwardID)
            If dtClaims.Rows.Count > 0 Then
                spreqtype = dtClaims.Rows(0)("req_type").ToString()
            End If

            If spreqtype = "ShareHolder" Then
                For Each claimRow As DataRow In dtClaims.Rows
                    'Dim spreqtype As String = claimRow("req_type").ToString()
                    Dim spChecklistGID As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim spStatus As String = claimRow("checklist_status").ToString()

                    Dim Inp_Flag As String = claimRow("input_flag").ToString()
                    Dim chklst_gid As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim checkliststatus As String = claimRow("checklist_status").ToString()

                    'If spreqtype = "shareholder" Then
                    For i As Integer = 0 To dgvChecklist.Rows.Count - 1
                        Dim dgvRow As DataGridViewRow = dgvChecklist.Rows(i)
                        If dgvRow.IsNewRow Then Continue For

                        Dim gridChecklistGID As Integer = Convert.ToInt32(dgvRow.Cells("ChecklistGID").Value)

                        If gridChecklistGID = spChecklistGID Then

                            dgvRow.Cells("Ok").Value = (spStatus = "O")
                            dgvRow.Cells("NotOk").Value = (spStatus = "N")

                            If Inp_Flag = "Y" Then
                                Dim status As String = If(checkliststatus = "O", "OK", "NOTOK")
                                LoadChecklistInput(chklst_gid, chklst_inputs, status)
                                Dim dtInputs As DataTable = Get_InputValues(ReqGid)

                                ' Bind values to controls
                                For Each ctrl As Control In chklst_inputs.Controls
                                    Dim ctrlGid As Integer = Convert.ToInt32(ctrl.Tag)

                                    ' Match with master checklist gid
                                    Dim drs() As DataRow = dtInputs.Select("chklstinput_mstinput_gid  = " & ctrlGid)
                                    If drs.Length > 0 Then
                                        ' If you have multiple rows for the same master gid, loop them
                                        For Each dr As DataRow In drs
                                            If TypeOf ctrl Is TextBox Then
                                                'CType(ctrl, TextBox).Text = dr("chklstinput_inputvalue").ToString()
                                                Dim txt As TextBox = CType(ctrl, TextBox)
                                                txt.Text = dr("chklstinput_inputvalue").ToString()
                                            ElseIf TypeOf ctrl Is ComboBox Then
                                                CType(ctrl, ComboBox).SelectedValue = dr("chklstinput_inputvalue").ToString()
                                            End If
                                        Next
                                    End If
                                Next
                                currentChecklistRowIndex = i
                                selectedStatus = status
                                inputsPendingSave = False
                            End If

                            Dim childCount As Integer = 0
                            If dgvRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(dgvRow.Cells("ChildCount").Value) Then
                                childCount = Convert.ToInt32(dgvRow.Cells("ChildCount").Value)
                            End If

                            If childCount > 0 Then
                                Dim status As String = If(spStatus = "O", "OK", "NOTOK")
                                LoadChildChecklist(spChecklistGID, i + 1, status)
                                dgvRow.Cells("ChildLoaded").Value = True
                            End If

                            Exit For
                        End If
                    Next
                    ' End If
                Next
            End If


        End If

    End Sub

    Private Sub claimant_CheckedChanged(sender As Object, e As EventArgs) Handles claimant.CheckedChanged
        Dim spreqtype As String = ""

        If claimant.Checked = True Then
            claimant_email_txt.ReadOnly = False
            claimant_name_txt.ReadOnly = False
            claimant_addr_txt.ReadOnly = False

            selectedType = "claimant"
            dgvChecklist.Visible = True
            dgvChecklist.DataSource = Nothing
            dgvChecklist.Rows.Clear()
            chklst_inputs.Controls.Clear()
            LoadChecklist(selectedType, "")
            Dim dtClaims As DataTable = get_iepfclaims(GetFolioGid, GetInwardID)
            If dtClaims.Rows.Count > 0 Then
                spreqtype = dtClaims.Rows(0)("req_type").ToString()
            End If

            If spreqtype = "Claimant" Then
                For Each claimRow As DataRow In dtClaims.Rows
                    'Dim spreqtype As String = claimRow("req_type").ToString()
                    Dim spChecklistGID As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim spStatus As String = claimRow("checklist_status").ToString()

                    Dim Inp_Flag As String = claimRow("input_flag").ToString()
                    Dim chklst_gid As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                    Dim checkliststatus As String = claimRow("checklist_status").ToString()

                    'If spreqtype = "shareholder" Then
                    For i As Integer = 0 To dgvChecklist.Rows.Count - 1
                        Dim dgvRow As DataGridViewRow = dgvChecklist.Rows(i)
                        If dgvRow.IsNewRow Then Continue For

                        Dim gridChecklistGID As Integer = Convert.ToInt32(dgvRow.Cells("ChecklistGID").Value)

                        If gridChecklistGID = spChecklistGID Then

                            dgvRow.Cells("Ok").Value = (spStatus = "O")
                            dgvRow.Cells("NotOk").Value = (spStatus = "N")

                            If Inp_Flag = "Y" Then
                                Dim status As String = If(checkliststatus = "O", "OK", "NOTOK")
                                LoadChecklistInput(chklst_gid, chklst_inputs, status)
                                Dim dtInputs As DataTable = Get_InputValues(ReqGid)

                                ' Bind values to controls
                                For Each ctrl As Control In chklst_inputs.Controls
                                    Dim ctrlGid As Integer = Convert.ToInt32(ctrl.Tag)

                                    ' Match with master checklist gid
                                    Dim drs() As DataRow = dtInputs.Select("chklstinput_mstinput_gid  = " & ctrlGid)
                                    If drs.Length > 0 Then
                                        ' If you have multiple rows for the same master gid, loop them
                                        For Each dr As DataRow In drs
                                            If TypeOf ctrl Is TextBox Then
                                                'CType(ctrl, TextBox).Text = dr("chklstinput_inputvalue").ToString()
                                                Dim txt As TextBox = CType(ctrl, TextBox)
                                                txt.Text = dr("chklstinput_inputvalue").ToString()
                                            ElseIf TypeOf ctrl Is ComboBox Then
                                                CType(ctrl, ComboBox).SelectedValue = dr("chklstinput_inputvalue").ToString()
                                            End If
                                        Next
                                    End If
                                Next
                                currentChecklistRowIndex = i
                                selectedStatus = status
                                inputsPendingSave = False
                            End If

                            Dim childCount As Integer = 0
                            If dgvRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(dgvRow.Cells("ChildCount").Value) Then
                                childCount = Convert.ToInt32(dgvRow.Cells("ChildCount").Value)
                            End If

                            If childCount > 0 Then
                                Dim status As String = If(spStatus = "O", "OK", "NOTOK")
                                LoadChildChecklist(spChecklistGID, i + 1, status)
                                dgvRow.Cells("ChildLoaded").Value = True
                            End If

                            Exit For
                        End If
                    Next
                    ' End If
                Next
            End If
        End If

    End Sub

    Private Sub nominee_CheckedChanged(sender As Object, e As EventArgs) Handles nominee.CheckedChanged
        Dim spreqtype As String = ""

        If nominee.Checked = True Then

            claimant_name_txt.ReadOnly = True
            claimant_addr_txt.ReadOnly = True
            claimant_email_txt.ReadOnly = True

            If nominee_txt.Text = "" Then
                MessageBox.Show("Folio against Nominee not available", "STA")
                dgvChecklist.Visible = True
                dgvChecklist.DataSource = Nothing
                dgvChecklist.Rows.Clear()
            Else
                selectedType = "nominee"

                claimant_name_txt.ReadOnly = True
                claimant_addr_txt.ReadOnly = True
                claimant_email_txt.ReadOnly = True
                dgvChecklist.Visible = True
                dgvChecklist.DataSource = Nothing
                dgvChecklist.Rows.Clear()
                chklst_inputs.Controls.Clear()
                LoadChecklist(selectedType, "")
                Dim dtClaims As DataTable = get_iepfclaims(GetFolioGid, GetInwardID)
                If dtClaims.Rows.Count > 0 Then
                    spreqtype = dtClaims.Rows(0)("req_type").ToString()
                End If

                If spreqtype = "Nominee" Then
                    For Each claimRow As DataRow In dtClaims.Rows
                        'Dim spreqtype As String = claimRow("req_type").ToString()
                        Dim spChecklistGID As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                        Dim spStatus As String = claimRow("checklist_status").ToString()

                        Dim Inp_Flag As String = claimRow("input_flag").ToString()
                        Dim chklst_gid As Integer = Convert.ToInt32(claimRow("checklist_gid"))
                        Dim checkliststatus As String = claimRow("checklist_status").ToString()

                        'If spreqtype = "shareholder" Then
                        For i As Integer = 0 To dgvChecklist.Rows.Count - 1
                            Dim dgvRow As DataGridViewRow = dgvChecklist.Rows(i)
                            If dgvRow.IsNewRow Then Continue For

                            Dim gridChecklistGID As Integer = Convert.ToInt32(dgvRow.Cells("ChecklistGID").Value)

                            If gridChecklistGID = spChecklistGID Then

                                dgvRow.Cells("Ok").Value = (spStatus = "O")
                                dgvRow.Cells("NotOk").Value = (spStatus = "N")

                                If Inp_Flag = "Y" Then
                                    Dim status As String = If(checkliststatus = "O", "OK", "NOTOK")
                                    LoadChecklistInput(chklst_gid, chklst_inputs, status)
                                    Dim dtInputs As DataTable = Get_InputValues(ReqGid)

                                    ' Bind values to controls
                                    For Each ctrl As Control In chklst_inputs.Controls
                                        Dim ctrlGid As Integer = Convert.ToInt32(ctrl.Tag)

                                        ' Match with master checklist gid
                                        Dim drs() As DataRow = dtInputs.Select("chklstinput_mstinput_gid  = " & ctrlGid)
                                        If drs.Length > 0 Then
                                            ' If you have multiple rows for the same master gid, loop them
                                            For Each dr As DataRow In drs
                                                If TypeOf ctrl Is TextBox Then
                                                    'CType(ctrl, TextBox).Text = dr("chklstinput_inputvalue").ToString()
                                                    Dim txt As TextBox = CType(ctrl, TextBox)
                                                    txt.Text = dr("chklstinput_inputvalue").ToString()
                                                ElseIf TypeOf ctrl Is ComboBox Then
                                                    CType(ctrl, ComboBox).SelectedValue = dr("chklstinput_inputvalue").ToString()
                                                End If
                                            Next
                                        End If
                                    Next
                                    currentChecklistRowIndex = i
                                    selectedStatus = status
                                    inputsPendingSave = False
                                End If

                                Dim childCount As Integer = 0
                                If dgvRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(dgvRow.Cells("ChildCount").Value) Then
                                    childCount = Convert.ToInt32(dgvRow.Cells("ChildCount").Value)
                                End If

                                If childCount > 0 Then
                                    Dim status As String = If(spStatus = "O", "OK", "NOTOK")
                                    LoadChildChecklist(spChecklistGID, i + 1, status)
                                    dgvRow.Cells("ChildLoaded").Value = True
                                End If

                                Exit For
                            End If
                        Next
                        ' End If
                    Next
                End If
            End If

        End If

    End Sub

    Private Sub dgvChecklist_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvChecklist.CurrentCellDirtyStateChanged
        If dgvChecklist.IsCurrentCellDirty Then
            dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub dgvChecklist_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChecklist.CellContentClick
        ' --- Prevent invalid index ---
        ' ----- Begin Newly Added ------
        If e.RowIndex < 0 Then Exit Sub
        If GetGroupCode = "C" AndAlso GetQueueFrom = "Maker" Then
            Exit Sub
        End If

        Dim isOkCol As Boolean = (e.ColumnIndex = dgvChecklist.Columns("OK").Index)
        Dim isNotOkCol As Boolean = (e.ColumnIndex = dgvChecklist.Columns("NotOK").Index)

        If Not (isOkCol Or isNotOkCol) Then Exit Sub

        ' Get current row
        Dim currentRow As DataGridViewRow = dgvChecklist.Rows(e.RowIndex)

        ' Safely get current values
        Dim okValue As Boolean = False
        Dim notOkValue As Boolean = False

        If currentRow.Cells("OK").Value IsNot Nothing Then
            okValue = Convert.ToBoolean(currentRow.Cells("OK").Value)
        End If

        If currentRow.Cells("NotOK").Value IsNot Nothing Then
            notOkValue = Convert.ToBoolean(currentRow.Cells("NotOK").Value)
        End If

        ' --- Apply new logic ---
        If isOkCol Then
            ' Toggle OK only for this row
            If okValue = True Then
                ' Already checked → uncheck
                currentRow.Cells("OK").Value = False
            Else
                ' Check OK, uncheck NotOK
                currentRow.Cells("OK").Value = True
                currentRow.Cells("NotOK").Value = False
            End If

        ElseIf isNotOkCol Then
            ' Toggle NotOK only for this row
            If notOkValue = True Then
                ' Already checked → uncheck
                currentRow.Cells("NotOK").Value = False
            Else
                ' Check NotOK, uncheck OK
                currentRow.Cells("NotOK").Value = True
                currentRow.Cells("OK").Value = False
            End If
        End If

        ' Commit and refresh the change
        dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)
        dgvChecklist.EndEdit()
        dgvChecklist.RefreshEdit()

        ' ---- End ----

        

        Dim childCount As Integer = 0
        Try

            If currentRow.Cells("OK").Value IsNot Nothing Then
                okValue = Convert.ToBoolean(currentRow.Cells("OK").Value)
            End If

            If currentRow.Cells("NotOK").Value IsNot Nothing Then
                notOkValue = Convert.ToBoolean(currentRow.Cells("NotOK").Value)
            End If

            ' Now use the right logic
            If isOkCol Then
                ' Toggle OK
                currentRow.Cells("OK").Value = Not okValue
                currentRow.Cells("NotOK").Value = False

            ElseIf isNotOkCol Then
                ' Toggle NotOK
                currentRow.Cells("NotOK").Value = Not notOkValue
                currentRow.Cells("OK").Value = False
            End If

            Dim get_chklstgid As Integer = 0
            If currentRow.Cells("ChecklistGID").Value IsNot Nothing AndAlso IsNumeric(currentRow.Cells("ChecklistGID").Value) Then
                get_chklstgid = Convert.ToInt32(currentRow.Cells("ChecklistGID").Value)
            End If

            ' Define which GIDs should be hidden when trigger is OK
            Dim triggerGid As Integer = 2
            Dim hideGids As Integer() = {5, 6, 7}

            If isOkCol Then
                currentRow.Cells("OK").Value = True
                currentRow.Cells("NotOK").Value = False

                ' If trigger row is "Original Share Certificate?"
                If get_chklstgid = triggerGid Then
                    ' Remove rows from grid and store in temporary list
                    For i As Integer = dgvChecklist.Rows.Count - 1 To 0 Step -1
                        Dim row As DataGridViewRow = dgvChecklist.Rows(i)
                        If row.Cells("ChecklistGID").Value IsNot Nothing Then
                            Dim rowGid As Integer = Convert.ToInt32(row.Cells("ChecklistGID").Value)
                            If hideGids.Contains(rowGid) Then
                                removedChecklistRows.Add(row)
                                dgvChecklist.Rows.RemoveAt(i)
                            End If
                        End If
                    Next
                End If

            ElseIf isNotOkCol Then
                currentRow.Cells("OK").Value = False
                currentRow.Cells("NotOK").Value = True

                ' Restore removed rows if NOT OK
                If get_chklstgid = triggerGid Then
                    For Each row As DataGridViewRow In removedChecklistRows
                        dgvChecklist.Rows.Add(row)
                    Next
                    removedChecklistRows.Clear()
                End If
            End If

            If Not (isOkCol Or isNotOkCol) Then Exit Sub

            Dim above5LFlag As String = If(currentRow.Cells("Above5LFlag").Value IsNot Nothing, currentRow.Cells("Above5LFlag").Value.ToString(), "")
            Dim jointHolderFlag As String = If(currentRow.Cells("JointFlag").Value IsNot Nothing, currentRow.Cells("JointFlag").Value.ToString(), "")

            ' Prevent clicking NotOK if condition is met
            If isNotOkCol AndAlso (above5LFlag = "Y" OrElse jointHolderFlag = "Y") Then
                MessageBox.Show("Cannot mark Not OK because OK is already checked.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvChecklist.CancelEdit()  ' Cancel any attempted toggle
                currentRow.Cells("NotOK").Value = False
                currentRow.Cells("OK").Value = True

                dgvChecklist.RefreshEdit()
                Exit Sub
            End If

            dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)

            ' Prevent switching if inputs are pending
            If inputsPendingSave AndAlso e.RowIndex <> currentChecklistRowIndex Then
                MessageBox.Show("Please save the current inputs before making other selections.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)

            ' Get InputFlag
            Dim inputFlag As String = ""
            If currentRow.Cells("InputFlag").Value IsNot Nothing Then
                inputFlag = currentRow.Cells("InputFlag").Value.ToString().Trim().ToUpper()
            End If

            ' Get current row's GID (not ParentGID)
            Dim currentGid As Integer = 0
            If currentRow.Cells("ChecklistGID").Value IsNot Nothing AndAlso IsNumeric(currentRow.Cells("ChecklistGID").Value) Then
                currentGid = Convert.ToInt32(currentRow.Cells("ChecklistGID").Value)
            End If

            ' Case: Input required AND clicked OK → load inputs, clear children
            If inputFlag = "Y" AndAlso isOkCol = True Then
                ' Clear any existing children
                Dim rowIndex As Integer = e.RowIndex + 1
                While rowIndex < dgvChecklist.Rows.Count
                    Dim childRow = dgvChecklist.Rows(rowIndex)
                    Dim childParentGid As Integer = 0
                    If childRow.Cells("ParentGID").Value IsNot Nothing AndAlso IsNumeric(childRow.Cells("ParentGID").Value) Then
                        childParentGid = Convert.ToInt32(childRow.Cells("ParentGID").Value)
                    End If

                    If childParentGid = currentGid Then
                        dgvChecklist.Rows.RemoveAt(rowIndex)
                    Else
                        Exit While
                    End If
                End While
                currentRow.Cells("ChildLoaded").Value = False

                ' Clear old inputs before loading fresh
                chklst_inputs.Controls.Clear()

                ' Load inputs
                UpdateStatus()
                Dim status As String = If(isOkCol, "OK", "NOTOK")
                Dim inputLoaded As Boolean = LoadChecklistInput(currentGid, chklst_inputs, status)

                currentChecklistRowIndex = e.RowIndex
                selectedStatus = status
                inputsPendingSave = False
                'Exit Sub
            End If

            ' Continue normal flow when no input required
            currentRow.Cells("OK").Value = isOkCol
            currentRow.Cells("NotOK").Value = isNotOkCol

            ' Get ChildCount and ChildLoaded
            'Dim childCount As Integer = 0
            If currentRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(currentRow.Cells("ChildCount").Value) Then
                childCount = Convert.ToInt32(currentRow.Cells("ChildCount").Value)
            End If

            Dim childLoaded As Boolean = False
            Dim cellVal = currentRow.Cells("ChildLoaded").Value

            If cellVal IsNot Nothing AndAlso Not IsDBNull(cellVal) Then
                Dim strVal As String = cellVal.ToString().Trim().ToUpper()

                ' Accept multiple formats safely
                If strVal = "TRUE" OrElse strVal = "1" Then
                    childLoaded = True
                ElseIf strVal = "FALSE" OrElse strVal = "0" OrElse strVal = "" Then
                    childLoaded = False
                End If
            End If

            ' Remove existing children
            If childLoaded Then
                Dim rowIndex As Integer = e.RowIndex + 1
                While rowIndex < dgvChecklist.Rows.Count
                    Dim childRow = dgvChecklist.Rows(rowIndex)
                    Dim childParentGid As Integer = 0
                    If childRow.Cells("ParentGID").Value IsNot Nothing AndAlso IsNumeric(childRow.Cells("ParentGID").Value) Then
                        childParentGid = Convert.ToInt32(childRow.Cells("ParentGID").Value)
                    End If

                    If childParentGid = currentGid Then
                        dgvChecklist.Rows.RemoveAt(rowIndex)
                    Else
                        Exit While
                    End If
                End While
                currentRow.Cells("ChildLoaded").Value = False
            End If

            ' Load new children if applicable
            If childCount > 0 Then
                Dim status As String = If(isOkCol, "OK", "NOTOK")
                LoadChildChecklist(currentGid, e.RowIndex + 1, status)
                currentRow.Cells("ChildLoaded").Value = True
            End If

            Try
                ' Commit the latest checkbox change

                dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)

                ' Get clicked row info
                Dim clickedChecklistGid As Integer = If(IsNumeric(currentRow.Cells("ChecklistGID").Value), Convert.ToInt32(currentRow.Cells("ChecklistGID").Value), -1)
                Dim clickedParentGid As Integer = If(IsNumeric(currentRow.Cells("ParentGID").Value), Convert.ToInt32(currentRow.Cells("ParentGID").Value), -1)


                ' Determine which parent row to update
                Dim parentToUpdateGid As Integer = If(clickedParentGid <> -1, clickedParentGid, clickedChecklistGid)
                If parentToUpdateGid = -1 Then Exit Try

                ' Find parent row in grid
                Dim parentRow As DataGridViewRow = dgvChecklist.Rows _
                    .Cast(Of DataGridViewRow)() _
                    .FirstOrDefault(Function(r) IsNumeric(r.Cells("ChecklistGID").Value) AndAlso Convert.ToInt32(r.Cells("ChecklistGID").Value) = parentToUpdateGid)
                If parentRow Is Nothing Then Exit Try

                ' Read parent info (use parentRow for expected counts!)
                Dim parentChildCount As Integer = If(IsNumeric(parentRow.Cells("ChildCount").Value), Convert.ToInt32(parentRow.Cells("ChildCount").Value), 0)
                Dim parentExpectedOkCount As Integer = If(IsNumeric(parentRow.Cells("ExpectedOkCount").Value), Convert.ToInt32(parentRow.Cells("ExpectedOkCount").Value), 0)
                Dim parentExpectedNotOkCount As Integer = If(IsNumeric(parentRow.Cells("ExpectedNotOkCount").Value), Convert.ToInt32(parentRow.Cells("ExpectedNotOkCount").Value), 0)

                ' Was the click on a child row?
                Dim isChildClick As Boolean = (clickedParentGid <> -1)

                If parentChildCount > 0 Then
                    ' Parent has children → always clear ArrivedValue
                    parentRow.Cells("ArrivedValue").Value = ""
                    parentRow.Cells("ArrivedOkCount").Value = 0
                    parentRow.Cells("ArrivedNotOkCount").Value = 0
                    ' Only update Arrived counts when the user changed a child (not when user clicks the parent row)
                    If isChildClick Then
                        Dim childOkCount As Integer = 0
                        Dim childNotOkCount As Integer = 0

                        For Each r As DataGridViewRow In dgvChecklist.Rows
                            If IsNumeric(r.Cells("ParentGID").Value) AndAlso Convert.ToInt32(r.Cells("ParentGID").Value) = parentToUpdateGid Then
                                Dim childOk As Boolean = If(r.Cells("OK").Value IsNot Nothing, Convert.ToBoolean(r.Cells("OK").Value), False)
                                Dim childNOTOk As Boolean = If(r.Cells("NOTOK").Value IsNot Nothing, Convert.ToBoolean(r.Cells("NOTOK").Value), False)
                                Dim expectedvalue As String = If(r.Cells("ExpectedValue").Value IsNot Nothing, r.Cells("ExpectedValue").Value.ToString().ToUpper(), "NOTOK")

                                Dim expectedOk As Boolean = False
                                Dim expectedNOTOk As Boolean = False

                                r.Cells("ArrivedOkCount").Value = 0
                                r.Cells("ArrivedNotOkCount").Value = 0

                                If expectedvalue = "OK" Then
                                    expectedOk = (expectedvalue = "OK")
                                Else
                                    expectedNOTOk = (expectedvalue = "NOTOK")
                                End If

                                If parentExpectedOkCount > 0 AndAlso childOk = expectedOk Then
                                    ' Parent expecting OKs -> count child OKs into ArrivedOkCount
                                    If childOk Then childOkCount += 1
                                ElseIf parentExpectedNotOkCount > 0 AndAlso childOk = expectedOk Then
                                    ' Parent expecting NOT OKs -> child OKs represent resolved NOT OKs
                                    If childOk Then
                                        childNotOkCount += 1
                                    End If
                                End If
                            End If
                        Next

                        parentRow.Cells("ArrivedOkCount").Value = childOkCount
                        parentRow.Cells("ArrivedNotOkCount").Value = childNotOkCount
                    End If

                Else
                    ' Parent has NO children → update ArrivedValue from the clicked row
                    Dim okState As Boolean = If(currentRow.Cells("OK").Value IsNot Nothing, Convert.ToBoolean(currentRow.Cells("OK").Value), False)
                    parentRow.Cells("ArrivedValue").Value = If(okState, "OK", "NOTOK")
                    parentRow.Cells("ArrivedOkCount").Value = 0
                    parentRow.Cells("ArrivedNotOkCount").Value = 0
                End If

            Catch ex As Exception
                MessageBox.Show("Error updating ArrivedValue/ArrivedCount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


            UpdateStatus()

        Catch ex As Exception
            MessageBox.Show("Error processing checklist click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub dgvChecklist_CurrentCellDirtyStateChanged_old(sender As Object, e As EventArgs) Handles dgvChecklist.CurrentCellDirtyStateChanged
    '    If dgvChecklist.IsCurrentCellDirty Then
    '        dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)
    '    End If
    'End Sub

    'Private Sub dgvChecklist_CellValueChanged_old(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChecklist.CellValueChanged
    '    If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
    '    If GetGroupCode = "C" AndAlso GetQueueFrom = "Maker" Then Exit Sub

    '    ' Prevent recursive re-entry
    '    If isHandlingValueChange Then Exit Sub
    '    isHandlingValueChange = True

    '    Try
    '        Dim currentRow = dgvChecklist.Rows(e.RowIndex)
    '        Dim isOkCol As Boolean = (e.ColumnIndex = dgvChecklist.Columns("OK").Index)
    '        Dim isNotOkCol As Boolean = (e.ColumnIndex = dgvChecklist.Columns("NotOK").Index)

    '        Dim get_chklstgid As Integer = 0
    '        If currentRow.Cells("ChecklistGID").Value IsNot Nothing AndAlso IsNumeric(currentRow.Cells("ChecklistGID").Value) Then
    '            get_chklstgid = Convert.ToInt32(currentRow.Cells("ChecklistGID").Value)
    '        End If

    '        ' Define trigger and hide list
    '        Dim triggerGid As Integer = 2
    '        Dim hideGids As Integer() = {5, 6, 7}

    '        ' --- Handle OK column change ---
    '        ' --- Handle OK column change ---
    '        'If isOkCol Then
    '        '    ' Uncheck NotOK only
    '        '    If currentRow.Cells("OK").Value IsNot Nothing AndAlso Convert.ToBoolean(currentRow.Cells("OK").Value) Then
    '        '        currentRow.Cells("NotOK").Value = False
    '        '    End If

    '        '    ' Trigger hide logic
    '        '    If get_chklstgid = triggerGid Then
    '        '        For i As Integer = dgvChecklist.Rows.Count - 1 To 0 Step -1
    '        '            Dim row As DataGridViewRow = dgvChecklist.Rows(i)
    '        '            If row.Cells("ChecklistGID").Value IsNot Nothing AndAlso IsNumeric(row.Cells("ChecklistGID").Value) Then
    '        '                Dim rowGid As Integer = Convert.ToInt32(row.Cells("ChecklistGID").Value)
    '        '                If hideGids.Contains(rowGid) Then
    '        '                    removedChecklistRows.Add(row)
    '        '                    dgvChecklist.Rows.RemoveAt(i)
    '        '                End If
    '        '            End If
    '        '        Next
    '        '    End If
    '        'ElseIf isNotOkCol Then
    '        '    ' Uncheck OK only
    '        '    If currentRow.Cells("NotOK").Value IsNot Nothing AndAlso Convert.ToBoolean(currentRow.Cells("NotOK").Value) Then
    '        '        currentRow.Cells("OK").Value = False
    '        '    End If

    '        '    ' Restore hidden rows if trigger
    '        '    If get_chklstgid = triggerGid AndAlso removedChecklistRows.Count > 0 Then
    '        '        For Each row As DataGridViewRow In removedChecklistRows
    '        '            dgvChecklist.Rows.Add(row)
    '        '        Next
    '        '        removedChecklistRows.Clear()
    '        '    End If
    '        'End If

    '        If isOkCol Then
    '            currentRow.Cells("OK").Value = True
    '            currentRow.Cells("NotOK").Value = False ' If trigger row is "Original Share Certificate?" 
    '            If get_chklstgid = triggerGid Then ' Remove rows from grid and store in temporary list 
    '                For i As Integer = dgvChecklist.Rows.Count - 1 To 0 Step -1
    '                    Dim row As DataGridViewRow = dgvChecklist.Rows(i)
    '                    If row.Cells("ChecklistGID").Value IsNot Nothing Then
    '                        Dim rowGid As Integer = Convert.ToInt32(row.Cells("ChecklistGID").Value)
    '                        If hideGids.Contains(rowGid) Then
    '                            removedChecklistRows.Add(row)
    '                            dgvChecklist.Rows.RemoveAt(i)
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        ElseIf isNotOkCol Then
    '            currentRow.Cells("OK").Value = False
    '            currentRow.Cells("NotOK").Value = True
    '            ' Restore removed rows if NOT OK 
    '            If get_chklstgid = triggerGid Then
    '                For Each row As DataGridViewRow In removedChecklistRows
    '                    dgvChecklist.Rows.Add(row)
    '                Next
    '                removedChecklistRows.Clear()
    '            End If
    '        End If


    '        ' If neither OK nor NotOK column changed, we continue only for other logic that depends on selection
    '        If Not (isOkCol Or isNotOkCol) Then
    '            isHandlingValueChange = False
    '            Exit Sub
    '        End If

    '        ' --- Prevent clicking NotOK if condition is met ---
    '        Dim above5LFlag As String = If(currentRow.Cells("Above5LFlag").Value IsNot Nothing, currentRow.Cells("Above5LFlag").Value.ToString(), "")
    '        Dim jointHolderFlag As String = If(currentRow.Cells("JointFlag").Value IsNot Nothing, currentRow.Cells("JointFlag").Value.ToString(), "")

    '        If isNotOkCol AndAlso (above5LFlag = "Y" OrElse jointHolderFlag = "Y") Then
    '            MessageBox.Show("Cannot mark Not OK because OK is already checked.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            ' revert NotOK and re-check OK (safe because guarded by flag)
    '            currentRow.Cells("NotOK").Value = False
    '            currentRow.Cells("OK").Value = True

    '            dgvChecklist.RefreshEdit()
    '            isHandlingValueChange = False
    '            Exit Sub
    '        End If

    '        ' Commit ensures current cell value is available (should already be committed by CurrentCellDirtyStateChanged)
    '        dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)

    '        ' --- Prevent switching if inputs are pending ---
    '        If inputsPendingSave AndAlso e.RowIndex <> currentChecklistRowIndex Then
    '            MessageBox.Show("Please save the current inputs before making other selections.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            isHandlingValueChange = False
    '            Exit Sub
    '        End If

    '        dgvChecklist.CommitEdit(DataGridViewDataErrorContexts.Commit)

    '        ' --- InputFlag & GID retrieval ---
    '        Dim inputFlag As String = ""
    '        If currentRow.Cells("InputFlag").Value IsNot Nothing Then
    '            inputFlag = currentRow.Cells("InputFlag").Value.ToString().Trim().ToUpper()
    '        End If

    '        Dim currentGid As Integer = 0
    '        If currentRow.Cells("ChecklistGID").Value IsNot Nothing AndAlso IsNumeric(currentRow.Cells("ChecklistGID").Value) Then
    '            currentGid = Convert.ToInt32(currentRow.Cells("ChecklistGID").Value)
    '        End If

    '        ' --- If input required AND clicked OK => load inputs and clear children ---
    '        If inputFlag = "Y" AndAlso isOkCol Then
    '            ' Remove child rows for this parent (if any)
    '            Dim rowIndex As Integer = e.RowIndex + 1
    '            While rowIndex < dgvChecklist.Rows.Count
    '                Dim childRow = dgvChecklist.Rows(rowIndex)
    '                Dim childParentGid As Integer = 0
    '                If childRow.Cells("ParentGID").Value IsNot Nothing AndAlso IsNumeric(childRow.Cells("ParentGID").Value) Then
    '                    childParentGid = Convert.ToInt32(childRow.Cells("ParentGID").Value)
    '                End If

    '                If childParentGid = currentGid Then
    '                    dgvChecklist.Rows.RemoveAt(rowIndex)
    '                Else
    '                    Exit While
    '                End If
    '            End While
    '            currentRow.Cells("ChildLoaded").Value = False

    '            ' Clear and load inputs
    '            chklst_inputs.Controls.Clear()
    '            UpdateStatus()
    '            Dim status As String = "OK"
    '            Dim inputLoaded As Boolean = LoadChecklistInput(currentGid, chklst_inputs, status)

    '            currentChecklistRowIndex = e.RowIndex
    '            selectedStatus = status
    '            inputsPendingSave = False
    '            ' continue — do not exit, you might want arrived counts updated below
    '        End If

    '        ' --- If input required AND clicked NotOK => load children and clear inputs ---
    '        If inputFlag = "Y" AndAlso isNotOkCol Then
    '            ' Clear input controls
    '            chklst_inputs.Controls.Clear()

    '            ' Remove existing children (prevent duplication)
    '            Dim rowIndex As Integer = e.RowIndex + 1
    '            While rowIndex < dgvChecklist.Rows.Count
    '                Dim childRow = dgvChecklist.Rows(rowIndex)
    '                Dim childParentGid As Integer = 0
    '                If childRow.Cells("ParentGID").Value IsNot Nothing AndAlso IsNumeric(childRow.Cells("ParentGID").Value) Then
    '                    childParentGid = Convert.ToInt32(childRow.Cells("ParentGID").Value)
    '                End If

    '                If childParentGid = currentGid Then
    '                    dgvChecklist.Rows.RemoveAt(rowIndex)
    '                Else
    '                    Exit While
    '                End If
    '            End While
    '            currentRow.Cells("ChildLoaded").Value = False

    '            ' Load children if available
    '            Dim childCount As Integer = 0
    '            If currentRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(currentRow.Cells("ChildCount").Value) Then
    '                childCount = Convert.ToInt32(currentRow.Cells("ChildCount").Value)
    '            End If

    '            If childCount > 0 Then
    '                Dim status As String = "NOTOK"
    '                LoadChildChecklist(currentGid, e.RowIndex + 1, status)
    '                currentRow.Cells("ChildLoaded").Value = True
    '            End If

    '            UpdateStatus()
    '            isHandlingValueChange = False
    '            Exit Sub
    '        End If

    '        ' --- Get ChildCount and ChildLoaded ---
    '        Dim parentChildCount As Integer = 0
    '        If currentRow.Cells("ChildCount").Value IsNot Nothing AndAlso IsNumeric(currentRow.Cells("ChildCount").Value) Then
    '            parentChildCount = Convert.ToInt32(currentRow.Cells("ChildCount").Value)
    '        End If

    '        Dim childLoaded As Boolean = False
    '        Dim cellVal = currentRow.Cells("ChildLoaded").Value
    '        If cellVal IsNot Nothing AndAlso Not IsDBNull(cellVal) Then
    '            Dim strVal As String = cellVal.ToString().Trim().ToUpper()
    '            If strVal = "TRUE" OrElse strVal = "1" Then
    '                childLoaded = True
    '            Else
    '                childLoaded = False
    '            End If
    '        End If

    '        ' Remove existing children if childLoaded (user toggled to hide)
    '        If childLoaded Then
    '            Dim rowIndex As Integer = e.RowIndex + 1
    '            While rowIndex < dgvChecklist.Rows.Count
    '                Dim childRow = dgvChecklist.Rows(rowIndex)
    '                Dim childParentGid As Integer = 0
    '                If childRow.Cells("ParentGID").Value IsNot Nothing AndAlso IsNumeric(childRow.Cells("ParentGID").Value) Then
    '                    childParentGid = Convert.ToInt32(childRow.Cells("ParentGID").Value)
    '                End If

    '                If childParentGid = currentGid Then
    '                    dgvChecklist.Rows.RemoveAt(rowIndex)
    '                Else
    '                    Exit While
    '                End If
    '            End While
    '            currentRow.Cells("ChildLoaded").Value = False
    '        End If

    '        ' Load new children if applicable (when parent has children and current change demands child expansion)
    '        If parentChildCount > 0 Then
    '            Dim status As String = If(isOkCol, "OK", "NOTOK")
    '            LoadChildChecklist(currentGid, e.RowIndex + 1, status)
    '            currentRow.Cells("ChildLoaded").Value = True
    '        End If

    '        ' --- Update ArrivedValue / Arrived counts (parent row updates) ---
    '        Try
    '            Dim clickedChecklistGid As Integer = If(IsNumeric(currentRow.Cells("ChecklistGID").Value), Convert.ToInt32(currentRow.Cells("ChecklistGID").Value), -1)
    '            Dim clickedParentGid As Integer = If(IsNumeric(currentRow.Cells("ParentGID").Value), Convert.ToInt32(currentRow.Cells("ParentGID").Value), -1)
    '            Dim parentToUpdateGid As Integer = If(clickedParentGid <> -1, clickedParentGid, clickedChecklistGid)
    '            If parentToUpdateGid <> -1 Then
    '                Dim parentRow As DataGridViewRow = dgvChecklist.Rows.Cast(Of DataGridViewRow)() _
    '                    .FirstOrDefault(Function(r) IsNumeric(r.Cells("ChecklistGID").Value) AndAlso Convert.ToInt32(r.Cells("ChecklistGID").Value) = parentToUpdateGid)
    '                If parentRow IsNot Nothing Then
    '                    Dim pChildCount As Integer = If(IsNumeric(parentRow.Cells("ChildCount").Value), Convert.ToInt32(parentRow.Cells("ChildCount").Value), 0)
    '                    Dim parentExpectedOkCount As Integer = If(IsNumeric(parentRow.Cells("ExpectedOkCount").Value), Convert.ToInt32(parentRow.Cells("ExpectedOkCount").Value), 0)
    '                    Dim parentExpectedNotOkCount As Integer = If(IsNumeric(parentRow.Cells("ExpectedNotOkCount").Value), Convert.ToInt32(parentRow.Cells("ExpectedNotOkCount").Value), 0)

    '                    Dim isChildClick As Boolean = (clickedParentGid <> -1)

    '                    If pChildCount > 0 Then
    '                        parentRow.Cells("ArrivedValue").Value = ""
    '                        parentRow.Cells("ArrivedOkCount").Value = 0
    '                        parentRow.Cells("ArrivedNotOkCount").Value = 0

    '                        If isChildClick Then
    '                            Dim childOkCount As Integer = 0
    '                            Dim childNotOkCount As Integer = 0

    '                            For Each r As DataGridViewRow In dgvChecklist.Rows
    '                                If IsNumeric(r.Cells("ParentGID").Value) AndAlso Convert.ToInt32(r.Cells("ParentGID").Value) = parentToUpdateGid Then
    '                                    Dim childOk As Boolean = If(r.Cells("OK").Value IsNot Nothing, Convert.ToBoolean(r.Cells("OK").Value), False)
    '                                    Dim expectedvalue As String = If(r.Cells("ExpectedValue").Value IsNot Nothing, r.Cells("ExpectedValue").Value.ToString().ToUpper(), "NOTOK")

    '                                    If parentExpectedOkCount > 0 AndAlso expectedvalue = "OK" AndAlso childOk Then
    '                                        childOkCount += 1
    '                                    ElseIf parentExpectedNotOkCount > 0 AndAlso expectedvalue = "NOTOK" AndAlso childOk Then
    '                                        childNotOkCount += 1
    '                                    End If
    '                                End If
    '                            Next

    '                            parentRow.Cells("ArrivedOkCount").Value = childOkCount
    '                            parentRow.Cells("ArrivedNotOkCount").Value = childNotOkCount
    '                        End If
    '                    Else
    '                        ' Parent has no children -> set ArrivedValue from this clicked row
    '                        Dim okState As Boolean = If(currentRow.Cells("OK").Value IsNot Nothing, Convert.ToBoolean(currentRow.Cells("OK").Value), False)
    '                        parentRow.Cells("ArrivedValue").Value = If(okState, "OK", "NOTOK")
    '                        parentRow.Cells("ArrivedOkCount").Value = 0
    '                        parentRow.Cells("ArrivedNotOkCount").Value = 0
    '                    End If
    '                End If
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show("Error updating ArrivedValue/ArrivedCount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try

    '        UpdateStatus()

    '    Catch ex As Exception
    '        MessageBox.Show("Error processing checklist click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        isHandlingValueChange = False
    '    End Try
    'End Sub

    Private Function UpdateStatus_old() As Boolean
        Dim allValid As Boolean = False

        Try
            For Each row As DataGridViewRow In dgvChecklist.Rows
                ' Skip new or empty rows
                If row.IsNewRow Then Continue For

                ' Read parent info
                Dim gid As Integer = If(IsNumeric(row.Cells("ChecklistGID").Value), Convert.ToInt32(row.Cells("ChecklistGID").Value), 0)
                Dim childCount As Integer = If(IsNumeric(row.Cells("ChildCount").Value), Convert.ToInt32(row.Cells("ChildCount").Value), 0)
                Dim expectedValue As String = If(row.Cells("ExpectedValue").Value IsNot Nothing, row.Cells("ExpectedValue").Value.ToString().Trim().ToUpper(), "")
                Dim arrivedValue As String = If(row.Cells("ArrivedValue").Value IsNot Nothing, row.Cells("ArrivedValue").Value.ToString().Trim().ToUpper(), "")
                Dim expectedOkCount As Integer = If(IsNumeric(row.Cells("ExpectedOkCount").Value), Convert.ToInt32(row.Cells("ExpectedOkCount").Value), 0)
                Dim expectedNotOkCount As Integer = If(IsNumeric(row.Cells("ExpectedNotOkCount").Value), Convert.ToInt32(row.Cells("ExpectedNotOkCount").Value), 0)
                Dim arrivedOkCount As Integer = If(IsNumeric(row.Cells("ArrivedOkCount").Value), Convert.ToInt32(row.Cells("ArrivedOkCount").Value), -1)
                Dim arrivedNotOkCount As Integer = If(IsNumeric(row.Cells("ArrivedNotOkCount").Value), Convert.ToInt32(row.Cells("ArrivedNotOkCount").Value), -1)
                Dim isOkClicked As Boolean = False
                Dim isNotOkClicked As Boolean = False

                If Convert.ToBoolean(row.Cells("OK").Value) = True Then
                    isOkClicked = True
                End If

                If Convert.ToBoolean(row.Cells("NotOK").Value) = True Then
                    isNotOkClicked = True
                End If
                'Dim isRowValid As Boolean = True

                If childCount = 0 Then
                    ' Parent has NO children → ExpectedValue must match ArrivedValue
                    If expectedValue <> "" AndAlso expectedValue <> arrivedValue Then
                        allValid = False
                    Else
                        allValid = True
                    End If
                Else
                    ' Parent has children → check OK/NotOK counts
                    If isOkClicked Then
                        'If expectedOkCount < arrivedOkCount Then
                        '    allValid = False
                        'End If
                        If arrivedOkCount < expectedOkCount Then
                            allValid = False
                        Else
                            allValid = True
                        End If
                    ElseIf isNotOkClicked Then
                        'If expectedNotOkCount < arrivedNotOkCount Then
                        '    allValid = False
                        'End If
                        If arrivedNotOkCount < expectedNotOkCount Then
                            allValid = False
                        Else
                            allValid = True
                        End If
                    Else
                        If expectedValue = "" And (expectedNotOkCount + expectedOkCount) <= 0 Then
                        Else
                            allValid = False
                        End If
                    End If

                End If
            Next

            ' Update overall status label
            If allValid Then
                status_value_lab.Text = "Valid"
                status_value_lab.ForeColor = Color.DarkGreen
                submit.Enabled = True
                inex.Enabled = False
            Else
                status_value_lab.Text = "Invalid"
                status_value_lab.ForeColor = Color.Red
                submit.Enabled = False
                inex.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show("Error in UpdateStatus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            allValid = False
        End Try

        Return allValid
    End Function

    Private Function UpdateStatus() As Boolean
        Dim allValid As Boolean = False

        Try
            For Each row As DataGridViewRow In dgvChecklist.Rows
                ' Skip new or empty rows
                If row.IsNewRow Then Continue For

                Dim parentFlag As String = ""
                Dim expected_okcount As Integer = 0

                Dim isOkClicked As Boolean = False
                Dim isNotOkClicked As Boolean = False

                If Convert.ToBoolean(row.Cells("OK").Value) = True Then
                    isOkClicked = True
                End If
                If Convert.ToBoolean(row.Cells("NotOK").Value) = True Then
                    isNotOkClicked = True
                End If

                If row.Cells("ParentFlag").Value IsNot Nothing Then
                    parentFlag = row.Cells("ParentFlag").Value.ToString().Trim().ToUpper()
                End If
                'If row.Cells("ExpectedOkCount").Value IsNot Nothing Then
                '    expected_okcount = If(IsNumeric(row.Cells("ExpectedOkCount").Value), Convert.ToInt32(row.Cells("ExpectedOkCount").Value), 0)
                'End If

                ' --- Read parent info ---
                Dim gid As Integer = If(IsNumeric(row.Cells("ChecklistGID").Value), Convert.ToInt32(row.Cells("ChecklistGID").Value), 0)
                Dim childCount As Integer = If(IsNumeric(row.Cells("ChildCount").Value), Convert.ToInt32(row.Cells("ChildCount").Value), 0)
                Dim expectedValue As String = If(row.Cells("ExpectedValue").Value IsNot Nothing, row.Cells("ExpectedValue").Value.ToString().Trim().ToUpper(), "")
                Dim arrivedValue As String = If(row.Cells("ArrivedValue").Value IsNot Nothing, row.Cells("ArrivedValue").Value.ToString().Trim().ToUpper(), "")
                Dim expectedOkCount As Integer = If(IsNumeric(row.Cells("ExpectedOkCount").Value), Convert.ToInt32(row.Cells("ExpectedOkCount").Value), 0)
                Dim expectedNotOkCount As Integer = If(IsNumeric(row.Cells("ExpectedNotOkCount").Value), Convert.ToInt32(row.Cells("ExpectedNotOkCount").Value), 0)
                Dim arrivedOkCount As Integer = If(IsNumeric(row.Cells("ArrivedOkCount").Value), Convert.ToInt32(row.Cells("ArrivedOkCount").Value), 0)
                Dim arrivedNotOkCount As Integer = If(IsNumeric(row.Cells("ArrivedNotOkCount").Value), Convert.ToInt32(row.Cells("ArrivedNotOkCount").Value), 0)

                'If parentFlag <> "P" AndAlso expectedOkCount = 0 Then
                'Continue For ' Skip non-parent rows
                'ElseIf parentFlag = "C" AndAlso isNotOkClicked = True AndAlso expected_okcount > 0 Then
                '    Continue For
                'ElseIf parentFlag = "C" AndAlso isOkClicked = True AndAlso expectedOkCount > 0 Then
                '    Continue For

                If parentFlag = "C" Then
                    Continue For
                ElseIf parentFlag = "P" AndAlso gid = 47 Or gid = 15 Then
                    Continue For
                End If

                ' --- Parent Validation Logic ---
                If isOkClicked = False AndAlso isNotOkClicked = False Then
                    allValid = False
                ElseIf childCount = 0 Then
                    ' No children → ExpectedValue must match ArrivedValue
                    If expectedValue <> "" AndAlso expectedValue <> arrivedValue Then
                        allValid = False
                    Else
                        allValid = True
                    End If
                Else
                    ' Has children → Validate based on OK/NotOK counts
                    If isOkClicked Then
                        If arrivedOkCount < expectedOkCount Then
                            allValid = False
                        Else
                            allValid = True
                        End If
                    ElseIf isNotOkClicked Then
                        If arrivedNotOkCount < expectedNotOkCount Then
                            allValid = False
                            'ElseIf arrivedOkCount < expectedOkCount Then
                            '   allValid = False
                        Else
                            allValid = True
                        End If

                    Else
                        ' No selection made but expected counts exist
                        If expectedValue <> "" OrElse (expectedNotOkCount + expectedOkCount) > 0 Then
                        Else
                            allValid = False
                        End If
                    End If
                End If
                If Not allValid Then
                    Exit For
                End If
            Next

            ' --- Update overall status label ---
            If allValid Then
                status_value_lab.Text = "Valid"
                status_value_lab.ForeColor = Color.DarkGreen
                submit.Enabled = True
                inex.Enabled = False
            Else
                status_value_lab.Text = "Invalid"
                status_value_lab.ForeColor = Color.Red
                submit.Enabled = False
                inex.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show("Error in UpdateStatus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            allValid = False
        End Try

        Return allValid
    End Function

    Private Sub cert_details()
        Dim folioNo As String = foliono_txt.Text.Trim()
        Try
            Using cmd As New MySqlCommand("pr_get_avblechk_iepf", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure

                ' Add parameters
                cmd.Parameters.AddWithValue("in_folio_no", folioNo)
                cmd.Parameters.AddWithValue("in_inward_no", "")
                cmd.Parameters.AddWithValue("in_comp_gid", GetCompGid)
                cmd.Parameters.AddWithValue("in_action", "details")

                ' Fill into DataTable
                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgv_cert_details.AutoGenerateColumns = False
                    'dgv_cert_details.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    'Dim sameFont As New Font("Tahoma", 8.25, FontStyle.Bold)
                    'dgv_cert_details.ColumnHeadersDefaultCellStyle.Font = sameFont
                    'dgv_cert_details.DefaultCellStyle.Font = sameFont


                    dgv_cert_details.Columns("cert_no").DataPropertyName = "cert_no"
                    dgv_cert_details.Columns("issued_date").DataPropertyName = "issued_date"
                    'dgv_cert_details.Columns("lock_from").DataPropertyName = "lock_from"
                    'dgv_cert_details.Columns("lock_to").DataPropertyName = "lock_to"
                    dgv_cert_details.Columns("share_count").DataPropertyName = "share_count"
                    With dgv_cert_details.Columns("dist_series")
                        .DataPropertyName = "dist_series"
                        .Width = 200
                    End With
                    ' Bind to DataGridView
                    dgv_cert_details.DataSource = dt

                    Dim total As Integer = 0
                    For Each row As DataRow In dt.Rows
                        If Not IsDBNull(row("share_count")) Then
                            total += Convert.ToInt32(row("share_count"))
                        End If
                    Next

                    totalshare_txt.Text = total.ToString()
                    sharevalue_txt.Text = total * GetSharePrice
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching details: " & ex.Message)
        End Try
    End Sub

    Private Function IsValidEmail(ByVal email As String) As Boolean
        If String.IsNullOrWhiteSpace(email) Then Return False

        ' Basic Regex pattern for email validation
        Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Return System.Text.RegularExpressions.Regex.IsMatch(email.Trim(), pattern)
    End Function

    Private Sub submit_Click(sender As Object, e As EventArgs) Handles submit.Click
        Dim result As Long
        Dim Remark As String = remark_txt.Text
        Dim groupcode As String = GetGroupCode
        Dim SpMessage As String
        Dim req_type As String = ""
        Dim cert_flag As String = ""
        Dim chklst_status As String = ""
        Dim queue_status As String = "Checker"
        Dim action As String = ""
        Dim inputflag As String = ""
        Dim claimnatname As String = claimant_name_txt.Text
        Dim claimantaddr As String = claimant_addr_txt.Text
        Dim claimantemail As String = claimant_email_txt.Text

        For Each txt As TextBox In chklst_inputs.Controls.OfType(Of TextBox)()
            If String.IsNullOrWhiteSpace(txt.Text) Then
                MessageBox.Show("Please fill all required input fields before submitting", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txt.Focus()
                Exit Sub
            End If
        Next

        ' Assigning Values
        If claimant.Checked Then
            req_type = "Claimant"
        ElseIf shareholder.Checked Then
            req_type = "ShareHolder"
        Else
            req_type = "Nominee"
        End If

        If req_type = "Claimant" Then
            If claimnatname = "" Or claimantaddr = "" Or claimantemail = "" Then
                MessageBox.Show("Please fill all required claimant details", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If claimnatname.Length < 2 Then
                MessageBox.Show("Claimant name must be at least 3 characters.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                claimant_name_txt.Focus()
                Exit Sub
            End If

            If claimantaddr.Length < 20 Then
                MessageBox.Show("Claimant address must be at least 20 characters.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                claimant_addr_txt.Focus()
                Exit Sub
            End If

            If Not IsValidEmail(claimantemail) Then
                MessageBox.Show("Please enter a valid email address.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                claimant_email_txt.Focus()
                Exit Sub
            End If
        End If

        If status_value_lab.Text = "Valid" Then
            chklst_status = "Valid"
        Else
            chklst_status = "Invalid"
        End If

        If ReqGid = 0 Then
            action = "insert"
        Else
            action = "update"
        End If

        'Call SP header
        SpMessage = iud_iepfclaims(ReqGid, req_type, GetFolioGid, cert_flag, chklst_status, queue_status, gsLoginUserCode, GetInwardID, action, "", "")

        'Message Part
        If SpMessage.Trim.ToLower() = "success" Then
            result = UpdateQueue(GetInwardID, GetGroupCode, Remark, gnQueueSuccess)

            Dim frmQueue As New frmQueue(groupcode)
            frmQueue.Refresh()
            Me.Hide()
        Else
            MessageBox.Show(SpMessage, "STA - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub dgvChecklist_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvChecklist.CellBeginEdit
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvChecklist.Columns("NotOK").Index Then
            Dim row As DataGridViewRow = dgvChecklist.Rows(e.RowIndex)

            ' Check if OK is already True
            Dim isOK As Boolean = False
            If row.Cells("OK").Value IsNot Nothing Then
                Boolean.TryParse(row.Cells("OK").Value.ToString(), isOK)
            End If

            ' Cancel editing if OK is True
            If isOK Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub LoadChecklist(reqtype As String, certflag As String)
        dgvChecklist.Columns.Clear()
        dgvChecklist.Rows.Clear()

        dgvChecklist.ReadOnly = False
        dgvChecklist.AllowUserToAddRows = False
        dgvChecklist.EditMode = DataGridViewEditMode.EditOnEnter
        dgvChecklist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgvChecklist.RowHeadersVisible = False

        ' Checklist text
        Dim colItem As New DataGridViewTextBoxColumn With {
            .HeaderText = "Checklist",
            .Name = "Checklist",
            .ReadOnly = True,
            .Width = 475
        }
        dgvChecklist.Columns.Add(colItem)

        ' OK checkbox
        Dim colOk As New DataGridViewCheckBoxColumn With {
            .HeaderText = "OK",
            .Name = "OK",
            .Width = 50
        }
        dgvChecklist.Columns.Add(colOk)

        ' Not OK checkbox
        Dim colNotOk As New DataGridViewCheckBoxColumn With {
            .HeaderText = "Not OK",
            .Name = "NotOK",
            .Width = 50
        }
        dgvChecklist.Columns.Add(colNotOk)

        ' Input column (optional, but required in CellContentClick logic)
        Dim colInput As New DataGridViewTextBoxColumn With {
            .HeaderText = "Input",
            .Name = "Input",
            .Width = 100,
            .Visible = False
        }
        dgvChecklist.Columns.Add(colInput)

        ' Header style and wrapping
        'dgvChecklist.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
        'Dim sameFont As New Font("Tahoma", 8.25, FontStyle.Bold)
        'dgvChecklist.ColumnHeadersDefaultCellStyle.Font = sameFont
        'dgvChecklist.DefaultCellStyle.Font = sameFont
        dgvChecklist.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvChecklist.Columns("Checklist").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "Child Count", .Name = "ChildCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ParentGID", .Name = "ParentGID", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewCheckBoxColumn With {.HeaderText = "Child Loaded", .Name = "ChildLoaded", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "InputFlag", .Name = "InputFlag", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ChecklistGID", .Name = "ChecklistGID", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "AllowNotOkAsValid", .Name = "AllowNotOkAsValid", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "Above5LFlag", .Name = "Above5LFlag", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "JointFlag", .Name = "JointFlag", .Visible = False})
        'dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "MandatoryFlag", .Name = "MandatoryFlag", .Visible = False})
        'dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "RuleType", .Name = "RuleType", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ExpectedValue", .Name = "ExpectedValue", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ExpectedOkCount", .Name = "ExpectedOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ExpectedNotOkCount", .Name = "ExpectedNotOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ArrivedValue", .Name = "ArrivedValue", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ArrivedOkCount", .Name = "ArrivedOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ArrivedNotOkCount", .Name = "ArrivedNotOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ParentFlag", .Name = "ParentFlag", .Visible = False})

        'dgvChecklist.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
        'dgvChecklist.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvChecklist.Columns("Checklist").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        Try
            Dim sharecount As Long = totalshare_txt.Text
            Dim sharevalue As Decimal = sharevalue_txt.Text
            ' Load all data first
            Dim dt As New DataTable()
            Using cmd As New MySqlCommand("pr_get_iepfchecklist", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_folio_gid", GetFolioGid)
                cmd.Parameters.AddWithValue("in_inward_gid", GetInwardID)
                cmd.Parameters.AddWithValue("in_comp_gid", GetCompGid)
                cmd.Parameters.AddWithValue("in_reqtype", reqtype)
                cmd.Parameters.AddWithValue("in_cert_flag", certflag)
                cmd.Parameters.AddWithValue("in_share_value", sharevalue)

                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            ' Now loop rows from DataTable (no open reader)
            For Each row As DataRow In dt.Rows
                Dim item As String = row("checklist").ToString()
                Dim childCount As Integer = Convert.ToInt32(row("childcount"))
                Dim gid As Integer = Convert.ToInt32(row("parent_gid"))
                Dim parentFlag As String = row("parent_flag").ToString().Trim().ToUpper()
                Dim inputFlag As String = row("inputflag").ToString().Trim().ToUpper()
                Dim checklistGID As Integer = Convert.ToInt32(row("checklist_gid"))
                Dim allowNotOk As String = If(IsDBNull(row("notokasvalid_flag")), "N", row("notokasvalid_flag").ToString().Trim().ToUpper())
                'Dim mandatoryflag As String = If(IsDBNull(row("mandatory_flag")), "N", row("mandatory_flag").ToString().Trim().ToUpper())
                'Dim ruletype As String = If(IsDBNull(row("rule_type")), "N", row("rule_type").ToString().Trim().ToUpper())
                Dim expectedvalue As String = row("expected_value").ToString()
                Dim expectedokcount As Integer = Convert.ToInt32(row("expected_okcount"))
                Dim expectednotokcount As Integer = Convert.ToInt32(row("expected_notokcount"))

                Dim above5LFlag As String = If(IsDBNull(row("iepfchklst_above5L_flag")), "N", row("iepfchklst_above5L_flag").ToString().Trim().ToUpper())
                Dim jointHolderFlag As String = If(IsDBNull(row("iepfchklst_jointholder_flag")), "N", row("iepfchklst_jointholder_flag").ToString().Trim().ToUpper())

                Dim markOK As Boolean = (above5LFlag = "Y" OrElse jointHolderFlag = "Y")
                Dim childLoaded As Boolean = markOK
                ' Add the row
                Dim rowIndex As Integer = dgvChecklist.Rows.Add(item, markOK, False, "", childCount, gid, False, inputFlag, checklistGID, allowNotOk, expectedvalue, expectedokcount, expectednotokcount, parentFlag)
                Dim rows As DataGridViewRow = dgvChecklist.Rows(rowIndex)
                rows.Cells("Above5LFlag").Value = above5LFlag
                rows.Cells("JointFlag").Value = jointHolderFlag
                rows.Cells("ExpectedValue").Value = expectedvalue
                rows.Cells("ExpectedOkCount").Value = expectedokcount
                rows.Cells("ExpectedNotOkCount").Value = expectednotokcount
                rows.Cells("ParentFlag").Value = parentFlag
                rows.Cells("ArrivedValue").Value = ""
                rows.Cells("ArrivedOkCount").Value = ""
                rows.Cells("ArrivedNotOkCount").Value = ""

                If markOK Then
                    Dim notOkCell As DataGridViewCheckBoxCell = CType(rows.Cells("NotOK"), DataGridViewCheckBoxCell)
                    notOkCell.Value = False
                    notOkCell.ReadOnly = True
                End If

                Dim currentRowIndex As Integer = dgvChecklist.Rows.Count - 1

                If GetGroupCode = "M" AndAlso GetQueueFrom <> "Checker" Then
                    If childLoaded AndAlso childCount > 0 Then
                        LoadChildChecklist(gid, currentRowIndex + 1, "OK")
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error loading checklist: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LoadChecklistChecker(reqtype As String, certflag As String)
        dgvChecklist.Columns.Clear()
        dgvChecklist.Rows.Clear()

        dgvChecklist.ReadOnly = False
        dgvChecklist.AllowUserToAddRows = False
        dgvChecklist.EditMode = DataGridViewEditMode.EditOnEnter
        dgvChecklist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgvChecklist.RowHeadersVisible = False

        ' Checklist text
        Dim colItem As New DataGridViewTextBoxColumn With {
            .HeaderText = "Checklist",
            .Name = "Checklist",
            .ReadOnly = True,
            .Width = 475
        }
        dgvChecklist.Columns.Add(colItem)

        ' OK checkbox
        Dim colOk As New DataGridViewCheckBoxColumn With {
            .HeaderText = "OK",
            .Name = "OK",
            .ReadOnly = True,
            .Width = 50
        }
        dgvChecklist.Columns.Add(colOk)

        ' Not OK checkbox
        Dim colNotOk As New DataGridViewCheckBoxColumn With {
            .HeaderText = "Not OK",
            .Name = "NotOK",
            .ReadOnly = True,
            .Width = 50
        }
        dgvChecklist.Columns.Add(colNotOk)

        ' Input column (optional, but required in CellContentClick logic)
        Dim colInput As New DataGridViewTextBoxColumn With {
            .HeaderText = "Input",
            .Name = "Input",
            .Width = 100,
            .Visible = False
        }
        dgvChecklist.Columns.Add(colInput)

        ' Hidden data columns
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {
            .HeaderText = "Child Count",
            .Name = "ChildCount",
            .Visible = False
        })

        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {
            .HeaderText = "ParentGID",
            .Name = "ParentGID",
            .Visible = False
        })

        Dim colChildLoaded As New DataGridViewCheckBoxColumn With {
            .HeaderText = "Child Loaded",
            .Name = "ChildLoaded",
            .Visible = False,
            .ValueType = GetType(Boolean),
            .FalseValue = False,
            .TrueValue = True
        }
        dgvChecklist.Columns.Add(colChildLoaded)

        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {
            .HeaderText = "InputFlag",
            .Name = "InputFlag",
            .Visible = False
        })

        Dim colChecklistGID As New DataGridViewTextBoxColumn With {
            .HeaderText = "ChecklistGID",
            .Name = "ChecklistGID",
            .Visible = False
        }
        dgvChecklist.Columns.Add(colChecklistGID)

        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ExpectedValue", .Name = "ExpectedValue", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ExpectedOkCount", .Name = "ExpectedOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ExpectedNotOkCount", .Name = "ExpectedNotOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ArrivedValue", .Name = "ArrivedValue", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ArrivedOkCount", .Name = "ArrivedOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ArrivedNotOkCount", .Name = "ArrivedNotOkCount", .Visible = False})
        dgvChecklist.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "ParentFlag", .Name = "ParentFlag", .Visible = False})
        ' Header style and wrapping
        'dgvChecklist.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
        'Dim sameFont As New Font("Tahoma", 8.25, FontStyle.Bold)
        'dgvChecklist.ColumnHeadersDefaultCellStyle.Font = sameFont
        'dgvChecklist.DefaultCellStyle.Font = sameFont
        dgvChecklist.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvChecklist.Columns("Checklist").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        ' Data loading
        Try

            Dim sharevalue As Decimal = sharevalue_txt.Text
            Using cmd As New MySqlCommand("pr_get_iepfchecklist", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_folio_gid", GetFolioGid)
                cmd.Parameters.AddWithValue("in_inward_gid", GetInwardID)
                cmd.Parameters.AddWithValue("in_comp_gid", GetCompGid)
                cmd.Parameters.AddWithValue("in_reqtype", reqtype)
                cmd.Parameters.AddWithValue("in_cert_flag", certflag)
                cmd.Parameters.AddWithValue("in_share_value", sharevalue)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Try
                            Dim item As String = reader("checklist").ToString()
                            Dim childCount As Integer = Convert.ToInt32(reader("childcount"))
                            Dim gid As Integer = Convert.ToInt32(reader("parent_gid"))
                            Dim inputFlag As String = reader("inputflag").ToString().Trim().ToUpper()
                            Dim checklistGID As Integer = Convert.ToInt32(reader("checklist_gid"))
                            ' Must match the order of columns added
                            dgvChecklist.Rows.Add(item, False, False, "", childCount, gid, False, inputFlag, checklistGID)
                        Catch exInner As Exception
                            MessageBox.Show("Error reading checklist row: " & exInner.Message, "Row Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End Try
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading checklist: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadChildChecklist(parentgid As Integer, insertAtIndex As Integer, status As String)
        Try

            Using cmd As New MySqlCommand("pr_get_iepfchecklistchild", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_parent_gid", parentgid)
                cmd.Parameters.AddWithValue("in_status", status)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        ' Validate required columns
                        If reader.HasRows Then
                            Dim checklistText As String = "   " & reader("checklist").ToString().Trim()
                            Dim inputFlag As String = reader("input_flag").ToString().Trim().ToUpper()
                            Dim parentFlag As String = reader("parent_flag").ToString().Trim().ToUpper()
                            Dim childCount As Integer = Convert.ToInt32(reader("child_count"))
                            Dim checklistGID As Integer = Convert.ToInt32(reader("checklist_gid"))
                            Dim childexpectedvalue As String = reader("child_expected_value").ToString().Trim()
                            Dim childexpectedokcount As String = reader("child_expected_okcount").ToString().Trim()
                            Dim childexpectednotokcount As String = reader("child_expected_notokcount").ToString().Trim()

                            Dim newRow As New DataGridViewRow()
                            newRow.CreateCells(dgvChecklist)
                            newRow.Cells(dgvChecklist.Columns("ChecklistGID").Index).Value = checklistGID
                            newRow.Cells(dgvChecklist.Columns("Checklist").Index).Value = checklistText
                            newRow.Cells(dgvChecklist.Columns("OK").Index).Value = False
                            newRow.Cells(dgvChecklist.Columns("NotOK").Index).Value = False
                            newRow.Cells(dgvChecklist.Columns("Input").Index).Value = ""
                            newRow.Cells(dgvChecklist.Columns("ChildCount").Index).Value = childCount
                            newRow.Cells(dgvChecklist.Columns("ParentGID").Index).Value = parentgid
                            newRow.Cells(dgvChecklist.Columns("InputFlag").Index).Value = inputFlag
                            newRow.Cells(dgvChecklist.Columns("ParentFlag").Index).Value = parentFlag
                            newRow.Cells(dgvChecklist.Columns("ChildLoaded").Index).Value = False
                            newRow.Cells(dgvChecklist.Columns("ExpectedValue").Index).Value = childexpectedvalue
                            newRow.Cells(dgvChecklist.Columns("ExpectedOkCount").Index).Value = childexpectedokcount
                            newRow.Cells(dgvChecklist.Columns("ExpectedNotOkCount").Index).Value = childexpectednotokcount

                            dgvChecklist.Rows.Insert(insertAtIndex, newRow)
                            insertAtIndex += 1
                        End If
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading child checklist: " & ex.Message, "Child Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function LoadChecklistInput(parentgid As Integer, grpBox As GroupBox, checklistStatus As String) As Boolean
        Dim inputAdded As Boolean = False
        Try
            ' First, remove any existing inputs for this gid
            Dim toRemove = grpBox.Controls.Cast(Of Control)().
                Where(Function(c) c.Name.StartsWith("txt_" & parentgid & "_") OrElse
                                  c.Name.StartsWith("lbl_" & parentgid & "_") OrElse
                                  c.Name = "desc_" & parentgid).
                ToList()

            For Each ctrl In toRemove
                grpBox.Controls.Remove(ctrl)
                ctrl.Dispose()
            Next

            Using cmd As New MySqlCommand("pr_get_iepfchecklistinput", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_chklst_gid", parentgid)
                cmd.Parameters.AddWithValue("in_chklst_status", checklistStatus)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    Dim topOffset As Integer = 20
                    Dim descriptionShown As Boolean = False

                    While reader.Read()
                        ' Show checklist description once at the top
                        If Not descriptionShown Then
                            Dim checklistDesc As String = reader("iepfchklst_description").ToString().Trim()

                            If Not String.IsNullOrEmpty(checklistDesc) Then
                                Dim descLabel As New Label With {
                                    .Text = checklistDesc,
                                    .AutoSize = True,
                                    .Top = topOffset,
                                    .Left = 10,
                                    .Font = New Font("Tahoma", 8.25, FontStyle.Bold),
                                    .Name = "desc_" & parentgid,
                                    .MaximumSize = New Size(grpBox.Width - 20, 0)
                                }

                                grpBox.Controls.Add(descLabel)
                                topOffset += descLabel.Height + 10
                            End If

                            descriptionShown = True
                        End If

                        ' Load each input
                        Dim labelText As String = reader("inputname").ToString()
                        Dim inputFlag As String = reader("inputflag").ToString().Trim().ToUpper()
                        Dim inputGid As Integer = Convert.ToInt32(reader("input_gid"))
                        Dim charLimit As Integer = 0

                        If Not IsDBNull(reader("iepfchklstinput_char_value")) Then
                            Integer.TryParse(reader("iepfchklstinput_char_value").ToString(), charLimit)
                        End If

                        If inputFlag = "Y" Or inputFlag = "N" Then
                            inputAdded = True

                            Dim lbl As New Label With {
                                .Text = labelText,
                                .AutoSize = True,
                                .Top = topOffset,
                                .Left = 10,
                                .Name = "lbl_" & parentgid & "_" & labelText.Replace(" ", "_")
                            }

                            Dim txt As New TextBox With {
                                .Name = "txt_" & parentgid & "_" & labelText.Replace(" ", "_"),
                                .Width = 200,
                                .Top = topOffset - 4,
                                .Left = 150,
                                .Tag = inputGid
                            }

                            If charLimit > 0 Then
                                ' Restrict length
                                txt.MaxLength = charLimit

                                ' Tooltip
                                Dim tt As New ToolTip()
                                tt.SetToolTip(txt, "Exactly " & charLimit & " alphanumeric characters required")

                                ' Restrict to ALPHANUMERIC only
                                AddHandler txt.KeyPress,
                                Sub(senderObj, eArgs)
                                    If Not Char.IsControl(eArgs.KeyChar) AndAlso Not Char.IsLetterOrDigit(eArgs.KeyChar) Then
                                        eArgs.Handled = True
                                    End If
                                End Sub

                                ' Validate compulsory length when focus leaves
                                AddHandler txt.Validating,
                                    Sub(senderObj, eArgs)
                                        Dim tb As TextBox = DirectCast(senderObj, TextBox)
                                        If tb.TextLength <> charLimit Then
                                            MessageBox.Show("This field requires exactly " & charLimit & " alphanumeric characters.",
                                                            "Validation Error",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Warning)
                                            eArgs.Cancel = True
                                        End If
                                    End Sub
                            End If

                            grpBox.Controls.Add(lbl)
                            grpBox.Controls.Add(txt)

                            topOffset += 30
                        End If
                    End While
                End Using
            End Using

            If Not inputAdded Then
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading input fields: " & ex.Message, "Checklist Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return inputAdded
    End Function

    Private Sub approve_btn_Click(sender As Object, e As EventArgs) Handles approve_btn.Click

        Dim result As Long
        Dim Remark As String = remark_txt.Text
        Dim groupcode As String = GetGroupCode
        Dim SpMessage As String
        Dim queue_status As String = "Approved"
        Dim req_type As String = ""

        If claimant.Checked Then
            req_type = "Claimant"
        ElseIf shareholder.Checked Then
            req_type = "ShareHolder"
        Else
            req_type = "Nominee"
        End If

        'Call SP header
        SpMessage = iud_iepfclaims(ReqGid, req_type, GetFolioGid, "", "", queue_status, gsLoginUserCode, GetInwardID, "update", "", "")

        'Message Part
        If SpMessage.Trim.ToLower() = "success" Then
            result = UpdateQueue(GetInwardID, GetGroupCode, Remark, gnQueueSuccess)

            Dim frmQueue As New frmQueue(groupcode)
            frmQueue.Refresh()
            Me.Hide()
        Else
            MessageBox.Show(SpMessage, "STA - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub reject_btn_Click(sender As Object, e As EventArgs) Handles reject_btn.Click

        Dim result As Long
        Dim Remark As String = remark_txt.Text
        Dim groupcode As String = GetGroupCode
        Dim SpMessage As String
        Dim queue_status As String = "Rejected"
        Dim req_type As String = ""

        If claimant.Checked Then
            req_type = "Claimant"
        ElseIf shareholder.Checked Then
            req_type = "ShareHolder"
        Else
            req_type = "Nominee"
        End If

        If String.IsNullOrWhiteSpace(Remark) Then
            MessageBox.Show("Remark is Missing", "STA")
            Exit Sub
        End If

        'Call SP header
        SpMessage = iud_iepfclaims(ReqGid, req_type, GetFolioGid, "", "", queue_status, gsLoginUserCode, GetInwardID, "update", "", "")

        'Message Part
        If SpMessage.Trim.ToLower() = "success" Then
            result = UpdateQueue(GetInwardID, GetGroupCode, Remark, 0)

            Dim frmQueue As New frmQueue(groupcode)
            frmQueue.Refresh()
            Me.Hide()
        Else
            MessageBox.Show(SpMessage, "STA - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub inex_Click(sender As Object, e As EventArgs) Handles inex.Click

        Dim frm As New frmiepfinexreasons(selectedType, "", ReqGid)
        frm.Show()

    End Sub

    Public Sub ProcessInex(selectedReasonIds As String, inexreason As String)
        Dim result As Long
        Dim Remark As String = remark_txt.Text
        Dim groupcode As String = GetGroupCode
        Dim SpMessage As String
        Dim req_type As String = ""
        Dim cert_flag As String = ""
        Dim chklst_status As String = "I"
        Dim queue_status As String = "Inex"
        Dim action As String = ""
        Dim claimnatname As String = claimant_name_txt.Text
        Dim claimantaddr As String = claimant_addr_txt.Text
        Dim claimantemail As String = claimant_email_txt.Text

        For Each txt As TextBox In chklst_inputs.Controls.OfType(Of TextBox)()
            If String.IsNullOrWhiteSpace(txt.Text) Then
                MessageBox.Show("Please fill all required input fields before submitting", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txt.Focus()
                Exit Sub
            End If
        Next

        ' Assigning Values
        If claimant.Checked Then
            req_type = "Claimant"
        ElseIf shareholder.Checked Then
            req_type = "ShareHolder"
        Else
            req_type = "Nominee"
        End If

        If req_type = "Claimant" Then
            If claimnatname = "" Or claimantaddr = "" Or claimantemail = "" Then
                MessageBox.Show("Please fill all required claimant details", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If claimnatname.Length < 2 Then
                MessageBox.Show("Claimant name must be at least 3 characters.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                claimant_name_txt.Focus()
                Exit Sub
            End If

            If claimantaddr.Length < 20 Then
                MessageBox.Show("Claimant address must be at least 20 characters.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                claimant_addr_txt.Focus()
                Exit Sub
            End If

            If Not IsValidEmail(claimantemail) Then
                MessageBox.Show("Please enter a valid email address.", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                claimant_email_txt.Focus()
                Exit Sub
            End If
        End If

        If status_value_lab.Text = "Valid" Then
            chklst_status = "Valid"
        Else
            chklst_status = "Invalid"
        End If

        If ReqGid = 0 Then
            action = "insert"
        Else
            action = "update"
        End If

        'Call SP header
        SpMessage = iud_iepfclaims(ReqGid, req_type, GetFolioGid, cert_flag, chklst_status, queue_status, gsLoginUserCode,
                                    GetInwardID, action, selectedReasonIds, inexreason)
        'Message Part
        If SpMessage.Trim.ToLower() = "success" Then
            result = UpdateQueue(GetInwardID, GetGroupCode, Remark, gnQueueReject)

            Dim frmQueue As New frmQueue(groupcode)
            frmQueue.Refresh()
            Me.Hide()
        Else
            MessageBox.Show(SpMessage, "STA - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function iud_iepfclaims(req_gid As Integer, req_type As String, folio_gid As Long, cert_flag As String, chklst_status As String,
                                    queue_status As String, actionBy As String, inward_gid As Long, action As String, reason_gid As String, inex_reason As String) As String
        Try
            Dim claimantname As String = ""
            Dim claimantaddr As String = ""
            Dim claimantemail As String = ""

            ' Prepare lists for CSVs
            Dim gidList As New List(Of String)
            Dim flagList As New List(Of String)
            Dim inputflagList As New List(Of String)
            Dim inputValueList As New List(Of String)
            Dim inputGidList As New List(Of String)

            ' Loop through each checklist row
            For Each row As DataGridViewRow In dgvChecklist.Rows
                If row.IsNewRow Then Continue For

                Dim checklistGid As Integer = Convert.ToInt32(row.Cells("ChecklistGID").Value)
                'Dim checklistStatus As String = If(Convert.ToBoolean(row.Cells("Ok").Value), "Ok", "Not Ok")
                Dim chklst_input_flag As String = Convert.ToString(row.Cells("InputFlag").Value)

                Dim checklistStatus As String = ""
                If row.Cells("Ok").Value Is Nothing OrElse IsDBNull(row.Cells("Ok").Value) OrElse row.Cells("Ok").Value.ToString().Trim() = "" Then
                    MessageBox.Show("Please select a value for checklist row (ChecklistGID: " & checklistGid & ")", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return "Checklist value missing"
                Else
                    If Convert.ToBoolean(row.Cells("Ok").Value) = True Then
                        checklistStatus = "Ok"
                    Else
                        checklistStatus = "Not Ok"
                    End If
                End If

                gidList.Add(checklistGid.ToString())
                flagList.Add(checklistStatus)
                inputflagList.Add(chklst_input_flag)

                ' Collect input_gid and values for this checklist
                Dim gidsForThisChecklist As New List(Of String)
                Dim valuesForThisChecklist As New List(Of String)

                For Each ctrl As Control In chklst_inputs.Controls
                    If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txt_" & checklistGid & "_") Then
                        ' Tag contains input_gid
                        If ctrl.Tag IsNot Nothing AndAlso IsNumeric(ctrl.Tag) Then
                            gidsForThisChecklist.Add(ctrl.Tag.ToString())
                            valuesForThisChecklist.Add(CType(ctrl, TextBox).Text.Trim())
                        End If
                    End If
                Next

                ' If no inputs, still add placeholder
                If gidsForThisChecklist.Count = 0 Then
                    gidsForThisChecklist.Add("0")
                    valuesForThisChecklist.Add("")
                End If

                ' Join multiple input_gids and values
                inputGidList.Add(String.Join("|", gidsForThisChecklist))
                inputValueList.Add(String.Join("|", valuesForThisChecklist))
            Next

            ' Create CSVs
            Dim gidCSV As String = String.Join(",", gidList)
            Dim flagCSV As String = String.Join(",", flagList)
            Dim inputFlagCSV As String = String.Join(",", inputflagList)
            Dim inputGidCSV As String = String.Join(";", inputGidList)
            Dim inputValueCSV As String = String.Join(";", inputValueList)

            claimantname = claimant_name_txt.Text
            claimantaddr = claimant_addr_txt.Text
            claimantemail = claimant_email_txt.Text

            ' Execute SP
            Using cmd As New MySqlCommand("pr_iud_iepfclaims", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("in_req_gid", req_gid)
                cmd.Parameters.AddWithValue("in_req_type", req_type)
                cmd.Parameters.AddWithValue("in_req_folio_gid", folio_gid)
                cmd.Parameters.AddWithValue("in_req_inward_gid", inward_gid)
                cmd.Parameters.AddWithValue("in_req_phycert_yesno", "")
                cmd.Parameters.AddWithValue("in_req_check_list_status", chklst_status)
                cmd.Parameters.AddWithValue("in_req_queue_status", queue_status)
                cmd.Parameters.AddWithValue("in_req_insert_by", actionBy)
                cmd.Parameters.AddWithValue("in_req_update_by", actionBy)
                cmd.Parameters.AddWithValue("in_mstchklst_gid", gidCSV)
                cmd.Parameters.AddWithValue("in_chklst_flag", flagCSV)
                cmd.Parameters.AddWithValue("in_action", action)
                cmd.Parameters.AddWithValue("in_input_flag", inputFlagCSV)
                cmd.Parameters.AddWithValue("in_input_values", inputValueCSV)
                cmd.Parameters.AddWithValue("in_input_gid", inputGidCSV)
                cmd.Parameters.AddWithValue("in_inexreasons_gid", reason_gid)
                cmd.Parameters.AddWithValue("in_claimant_name", claimantname)
                cmd.Parameters.AddWithValue("in_claimant_addr", claimantaddr)
                cmd.Parameters.AddWithValue("in_claimant_email", claimantemail)
                cmd.Parameters.AddWithValue("in_req_inex_reason", inex_reason)


                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return reader.GetString(0)
                    Else
                        Return "No result returned from SP (NULL)"
                    End If
                End Using

                'Using da As New MySqlDataAdapter(cmd)
                '    Dim dt As New DataTable()
                '    da.Fill(dt)

                '    If dt.Rows.Count > 0 Then
                '        Return dt.Rows(0)(0).ToString()
                '    Else
                '        Return "No result returned from SP (NULL)"
                '    End If
                'End Using
            End Using

        Catch ex As Exception
            Return "Stored Procedure Error: " & ex.Message
        End Try
    End Function

    Private Function get_iepfclaims(folio_gid As Long, inward_gid As Long) As DataTable
        Dim dt As New DataTable()

        Try
            Using cmd As New MySqlCommand("pr_get_iepfclaims", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("in_folio_gid", folio_gid)
                cmd.Parameters.AddWithValue("in_inward_gid", inward_gid)

                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Stored Procedure Error: " & ex.Message)
        End Try

        Return dt
    End Function

    Private Sub Get_Remarks(inward_gid As Long, group_code As String, queue_from As String, hideflag As String)
        Dim folioNo As String = foliono_txt.Text.Trim()
        Try
            Using cmd As New MySqlCommand("pr_get_iepfclaimsremark", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("in_inward_gid", inward_gid)
                cmd.Parameters.AddWithValue("in_hideflag", hideflag)

                Dim remarkValue As Object = cmd.ExecuteScalar()

                If HideButtonsForViewMode = True Then
                    If hideflag = "Y" Then
                        pre_rmk_lab.Text = "Maker Remarks"
                        If remarkValue IsNot Nothing AndAlso remarkValue IsNot DBNull.Value Then
                            pre_rmk_txt.Text = remarkValue.ToString()
                        End If

                    Else
                        If remarkValue IsNot Nothing AndAlso remarkValue IsNot DBNull.Value Then
                            pre_rmk_txt.Text = remarkValue.ToString()
                        End If
                    End If

                Else
                    If (group_code = "C" Or group_code = "I") And queue_from = "Maker" Then
                        If remarkValue IsNot Nothing AndAlso remarkValue IsNot DBNull.Value Then
                            pre_rmk_lab.Text = "Maker Remarks"
                            pre_rmk_txt.Text = remarkValue.ToString()
                        Else
                            pre_rmk_txt.Text = String.Empty
                        End If
                    End If

                    If group_code = "M" And queue_from = "Checker" Then
                        If remarkValue IsNot Nothing AndAlso remarkValue IsNot DBNull.Value Then
                            pre_rmk_lab.Text = "Checker Remarks"
                            pre_rmk_txt.Text = remarkValue.ToString()
                        Else
                            pre_rmk_txt.Text = String.Empty
                        End If
                    End If
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching details: " & ex.Message)
        End Try
    End Sub

    Private Function Get_InputValues(req_gid As Long) As DataTable
        Dim dt As New DataTable()
        Try
            Using cmd As New MySqlCommand("pr_get_iepfclaimchklstinputvalues", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_req_gid", req_gid)

                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching details: " & ex.Message)
        End Try
        Return dt
    End Function

    Private Sub inexreasonview_btn_Click(sender As Object, e As EventArgs) Handles inexreasonview_btn.Click

        Dim frm As New frmiepfinexreasons(RequestType, coveringstatus, coveringReqGid)
        frm.Show()

    End Sub

    Private Sub Que_inex_btn_Click(sender As Object, e As EventArgs) Handles Que_inex_btn.Click
        Dim result As Long
        Dim Remark As String = remark_txt.Text

        result = UpdateQueue(GetInwardID, GetGroupCode, Remark, gnQueueSuccess)

        Dim frmQueue As New frmQueue(GetGroupCode)
        frmQueue.Refresh()
        Me.Hide()
    End Sub

    Private Sub reprocess_btn_Click(sender As Object, e As EventArgs) Handles reprocess_btn.Click
        Dim result As Long
        Dim Remark As String = remark_txt.Text

        result = UpdateQueue(GetInwardID, GetGroupCode, Remark, gnQueueReprocess)

        Dim frmQueue As New frmQueue(GetGroupCode)
        frmQueue.Refresh()
        Me.Hide()
    End Sub

    Private Sub LoadClaimDetails(inward_gid As Integer, groupcode As String)
        Try
            Using cmd As New MySqlCommand("pr_get_iepfclaimsfoliodtl", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_inward_gid", inward_gid)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        GetFolioNo = reader("folio_no").ToString()
                        GetCompName = reader("comp_name").ToString()
                        GetName = reader("holder1_name").ToString()
                        GetName2 = reader("holder2_name").ToString()
                        GetName3 = reader("holder3_name").ToString()
                        GetInwardID = inward_gid
                        GetFolioGid = Convert.ToInt32(reader("folio_gid"))
                        GetGroupCode = groupcode
                        'GetQueueFrom = queuefrom
                        GetQueueFrom = reader("group_name").ToString()
                        GetNomineeName = reader("nominee_name").ToString()
                        GetCompGid = Convert.ToInt32(reader("comp_gid"))
                        GetInwardNo = Convert.ToInt64(reader("inward_no"))
                        GetSharePrice = Convert.ToDecimal(reader("market_price"))

                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching details: " & ex.Message)
        End Try
    End Sub

End Class