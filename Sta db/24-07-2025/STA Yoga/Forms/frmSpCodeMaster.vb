Option Explicit On 
Imports MySql.Data.MySqlClient

Public Class frmSpCodeMaster
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal TbName As String, ByVal PkFldName As String, ByVal PkFldDesc As String, _
                   ByVal CodeFldName As String, ByVal CodeFldDesc As String, ByVal CodeFldLen As Integer, _
                   ByVal NameFldName As String, ByVal NameFldDesc As String, ByVal NameFldLen As Integer, _
                   ByVal IsRemovedFldName As String, ByVal FrmCaption As String, _
                   ByVal SpName As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        Me.Text = FrmCaption
        lblCode.Text = CodeFldDesc
        lblName.Text = NameFldDesc

        msTbName = TbName
        msPkFldName = PkFldName
        msPkFldDesc = PkFldDesc

        msCodeFldName = CodeFldName
        msCodeFldDesc = CodeFldDesc

        msNameFldName = NameFldName
        msNameFldDesc = NameFldDesc

        msIsRemovedFldName = IsRemovedFldName

        msSpName = SpName

        txtCode.MaxLength = CodeFldLen
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.pnlSave = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.pnlButtons = New System.Windows.Forms.Panel
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnFind = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.txtName = New System.Windows.Forms.TextBox
        Me.lblName = New System.Windows.Forms.Label
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.lblCode = New System.Windows.Forms.Label
        Me.txtId = New System.Windows.Forms.TextBox
        Me.pnlSave.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.SystemColors.Control
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Location = New System.Drawing.Point(1, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(72, 24)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(2, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'pnlSave
        '
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Location = New System.Drawing.Point(133, 87)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(156, 28)
        Me.pnlSave.TabIndex = 33
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(80, 1)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Location = New System.Drawing.Point(17, 87)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(388, 28)
        Me.pnlButtons.TabIndex = 34
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(313, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFind.Location = New System.Drawing.Point(157, 1)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(72, 24)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Location = New System.Drawing.Point(235, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 24)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEdit.Location = New System.Drawing.Point(79, 1)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(72, 24)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.txtName)
        Me.pnlMain.Controls.Add(Me.lblName)
        Me.pnlMain.Controls.Add(Me.txtCode)
        Me.pnlMain.Controls.Add(Me.lblCode)
        Me.pnlMain.Location = New System.Drawing.Point(8, 8)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(406, 73)
        Me.pnlMain.TabIndex = 0
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(110, 39)
        Me.txtName.MaxLength = 64
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(280, 21)
        Me.txtName.TabIndex = 2
        '
        'lblName
        '
        Me.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblName.Location = New System.Drawing.Point(10, 43)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(94, 13)
        Me.lblName.TabIndex = 61
        Me.lblName.Text = "A/C Type Name"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(110, 12)
        Me.txtCode.MaxLength = 64
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(280, 21)
        Me.txtCode.TabIndex = 1
        '
        'lblCode
        '
        Me.lblCode.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCode.Location = New System.Drawing.Point(10, 16)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(94, 13)
        Me.lblCode.TabIndex = 55
        Me.lblCode.Text = "A/C Type Name"
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(9, 86)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(24, 21)
        Me.txtId.TabIndex = 60
        Me.txtId.Visible = False
        '
        'frmCodeMaster
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(423, 119)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCodeMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "A/C Type Master"
        Me.pnlSave.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.PerformLayout()
    End Sub
#End Region

    Dim msSQL As String
    Dim mnResult As Integer

    Dim msFrmCaption As String
    Dim msLblCaption As String
    Dim msTbName As String
    Dim msPkFldName As String
    Dim msPkFldDesc As String
    Dim msCodeFldName As String
    Dim msCodeFldDesc As String
    Dim msNameFldName As String
    Dim msNameFldDesc As String
    Dim msIsRemovedFldName As String
    Dim msSpName As String
    Dim mnFldLen As String
    Dim SqlStr As String

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        EnableSave(True)
        txtCode.Text = ""
        txtName.Text = ""
        txtId.Text = ""
        txtCode.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Long = 0
        Dim lnId As Long
        Dim lsCode As String
        Dim lsName As String
        Dim lsAction As String
        Dim lsTxt As String

        Try
            If txtCode.Text = "" Then
                MsgBox("Enter " & msCodeFldDesc & " !", MsgBoxStyle.Information)
                txtCode.Focus()
                Exit Sub
            End If

            If txtName.Text = "" Then
                MsgBox("Enter " & msNameFldDesc & " !", MsgBoxStyle.Information)
                txtName.Focus()
                Exit Sub
            End If

            lnId = Val(txtId.Text)
            lsCode = QuoteFilter(txtCode.Text).ToUpper
            lsName = QuoteFilter(txtName.Text).ToUpper

            If lnId = 0 Then
                lsAction = "INSERT"
            Else
                lsAction = "UPDATE"
            End If

            Using cmd As New MySqlCommand(msSpName, gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("?in_" & msPkFldName, lnId)
                cmd.Parameters.AddWithValue("?in_" & msCodeFldName, lsCode)
                cmd.Parameters.AddWithValue("?in_" & msNameFldName, lsName)
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

            If MsgBox("Do you want to insert new record ?", MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
                Call btnNew_Click(sender, e)
            Else
                txtCode.Text = ""
                txtName.Text = ""
                txtId.Text = ""
                EnableSave(False)
                btnNew.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtBankName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If txtId.Text = "" Then
                If MsgBox("Do you want to select the record ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                    EnableSave(False)
                End If
            Else
                EnableSave(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As frmSearch

        Try
            SearchDialog = New frmSearch(gOdbcConn, _
                             " SELECT " & msPkFldName & " as '" & msPkFldDesc & "'," & _
                             " " & msCodeFldName & " as '" & msCodeFldDesc & "'," & _
                             " " & msNameFldName & " as '" & msNameFldDesc & "' FROM " & msTbName & " ", _
                             " " & msPkFldName & "," & msCodeFldName & "," & msNameFldName, _
                             " 1 = 1 and " & msIsRemovedFldName & " = 'N'")

            SearchDialog.ShowDialog()

            If gnSearchId <> 0 Then
                Call ListAll("select * from " & msTbName & " " _
                    & "where " & msPkFldName & " = " & gnSearchId & " " _
                    & "and " & msIsRemovedFldName & " = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As MySql.Data.MySqlClient.MySqlConnection)
        Dim ds As New DataSet

        Try
            ds = gfDataSet(SqlStr, msTbName, gOdbcConn)

            txtId.Text = Val(ds.Tables(msTbName).Rows(0).Item(msPkFldName)).ToString
            txtCode.Text = ds.Tables(msTbName).Rows(0).Item(msCodeFldName).ToString
            txtName.Text = ds.Tables(msTbName).Rows(0).Item(msNameFldName).ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnResult As Long = 0
        Dim lnId As Long
        Dim lsCode As String
        Dim lsName As String
        Dim lsAction As String
        Dim lsTxt As String

        Try
            If txtId.Text = "" Then
                If MsgBox("Do you want to select the record ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    btnFind.PerformClick()
                End If
            Else
                EnableSave(True)
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
                    lnId = Val(txtId.Text)
                    lsCode = QuoteFilter(txtCode.Text).ToUpper
                    lsName = QuoteFilter(txtName.Text).ToUpper
                    lsAction = "DELETE"

                    Using cmd As New MySqlCommand(msSpName, gOdbcConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("?in_" & msPkFldName, lnId)
                        cmd.Parameters.AddWithValue("?in_" & msCodeFldName, lsCode)
                        cmd.Parameters.AddWithValue("?in_" & msNameFldName, lsName)
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

                            txtId.Text = ""
                            txtCode.Text = ""
                            txtName.Text = ""
                            EnableSave(False)
                        Else
                            MessageBox.Show(lsTxt, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    End Using
                Else
                    txtId.Text = ""
                    txtCode.Text = ""
                    txtName.Text = ""
                    EnableSave(False)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        EnableSave(False)
        txtId.Text = ""
        txtCode.Text = ""
        txtName.Text = ""

        btnNew.Focus()
    End Sub

    Private Sub frmAcTypeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call EnableSave(False)
        btnNew.Focus()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
