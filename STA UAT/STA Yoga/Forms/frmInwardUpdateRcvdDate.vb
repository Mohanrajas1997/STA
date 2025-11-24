Imports MySql.Data.MySqlClient

Public Class frmInwardUpdateRcvdDate
    Dim mnInwardId As Long

    Public Sub New(InwardId As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        mnInwardId = InwardId
    End Sub

    Private Sub ListAll(ByVal InwardId As Long)
        Dim lsSql As String
        Dim lobjDataReader As MySqlDataReader

        Try
            lsSql = ""
            lsSql &= " select * from sta_trn_tinward "
            lsSql &= " where inward_gid = " & mnInwardId & " "
            lsSql &= " and delete_flag = 'N' "

            lobjDataReader = gfExecuteQry(lsSql, gOdbcConn)

            cboCourier.SelectedIndex = -1
            cboDocType.SelectedIndex = -1
            cboCompany.SelectedIndex = -1
            cboRcvdMode.SelectedIndex = -1

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("inward_gid").ToString
                        txtInwardNo.Text = .Item("inward_no").ToString
                        cboDocType.SelectedValue = .Item("tran_code").ToString
                        cboRcvdMode.SelectedValue = .Item("received_mode").ToString
                        dtpRcvdDate.Value = .Item("received_date")
                        cboCourier.SelectedValue = .Item("courier_gid").ToString
                        txtAwbNo.Text = .Item("awb_no").ToString
                        cboCompany.SelectedValue = .Item("comp_gid").ToString
                        txtFolioNo.Text = .Item("folio_no").ToString
                        txtShareHolderName.Text = .Item("shareholder_name").ToString
                        txtPanNo.Text = .Item("shareholder_pan_no").ToString
                        txtContactNo.Text = .Item("shareholder_contact_no").ToString
                        txtMailId.Text = .Item("shareholder_email_id").ToString
                        txtRemark.Text = .Item("inward_remark").ToString
                    End If
                End If

                .Close()
            End With

            Call gpAutoFillCombo(cboDocType)
            Call gpAutoFillCombo(cboRcvdMode)
            Call gpAutoFillCombo(cboCompany)
            Call gpAutoFillCombo(cboCourier)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmInwardEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmBankMaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        ' received mode
        lsSql = ""
        lsSql &= " select receivedmode_code,receivedmode_desc from sta_mst_treceivedmode "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by receivedmode_desc asc "

        Call gpBindCombo(lsSql, "receivedmode_desc", "receivedmode_code", cboRcvdMode, gOdbcConn)

        ' doc type
        lsSql = ""
        lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by trantype_code asc "

        Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

        ' courier
        lsSql = ""
        lsSql &= " select courier_gid,courier_name from sta_mst_tcourier "
        lsSql &= " where delete_flag = 'N' "
        lsSql &= " order by courier_name asc "

        Call gpBindCombo(lsSql, "courier_name", "courier_gid", cboCourier, gOdbcConn)

        ' company
        lsSql = ""
        lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
        lsSql &= " where entity_gid = " & gnEntityId & " "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by comp_name asc "

        Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

        dtpRcvdDate.Value = Now

        Call ClearControl()

        If mnInwardId > 0 Then
            Call ListAll(mnInwardId)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim lsTxt As String
        Dim lnInwardId As Long
        Dim ldRcvdDate As Date

        Try
            ldRcvdDate = dtpRcvdDate.Value

            lnInwardId = Val(txtId.Text)

            Using cmd As New MySqlCommand("pr_sta_upd_inwardrcvddate", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_inward_gid", lnInwardId)
                cmd.Parameters.AddWithValue("?in_received_date", Format(ldRcvdDate, "yyyy-MM-dd"))
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
                    Me.Close()
                Else
                    MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gsProjectName)
        End Try
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtInwardNo.Focus()
    End Sub

    Private Sub txtRemark_GotFocus(sender As Object, e As EventArgs) Handles txtRemark.GotFocus
        KeyPreview = False
    End Sub

    Private Sub txtRemark_LostFocus(sender As Object, e As EventArgs) Handles txtRemark.LostFocus
        KeyPreview = True
    End Sub

    Private Sub txtFolioNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFolioNo.Validating
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable
        Dim lnCompId As Long = 0
        Dim lsFolioNo As String

        If btnSave.Visible = True And txtShareHolderName.Text = "" Then
            If cboCompany.SelectedIndex <> -1 And cboCompany.Text <> "" Then lnCompId = Val(cboCompany.SelectedValue.ToString)
            lsFolioNo = QuoteFilter(txtFolioNo.Text)

            If lnCompId = 0 Then lsFolioNo = ""

            cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("?in_folio_gid", 0)
            cmd.Parameters.AddWithValue("?in_comp_gid", lnCompId)
            cmd.Parameters.AddWithValue("?in_folio_no", lsFolioNo)

            cmd.CommandTimeout = 0

            dt = New DataTable
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                txtShareHolderName.Text = dt.Rows(0).Item("holder1_name").ToString
                txtPanNo.Text = dt.Rows(0).Item("holder1_pan_no").ToString
            End If
        End If
    End Sub

    Private Sub LoadFolio(FolioId As Long)
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim dt As DataTable

        cmd = New MySqlCommand("pr_sta_get_folio", gOdbcConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("?in_folio_gid", FolioId)
        cmd.Parameters.AddWithValue("?in_comp_gid", 0)
        cmd.Parameters.AddWithValue("?in_folio_no", "")

        cmd.CommandTimeout = 0

        dt = New DataTable
        da = New MySqlDataAdapter(cmd)
        da.Fill(dt)

        With dt
            If .Rows.Count > 0 Then
                txtFolioNo.Text = .Rows(0).Item("folio_no").ToString
                txtShareHolderName.Text = .Rows(0).Item("holder1_name").ToString
                txtPanNo.Text = .Rows(0).Item("holder1_pan_no").ToString
            End If
        End With

        da.Dispose()
        dt.Dispose()
        cmd.Dispose()
    End Sub
End Class