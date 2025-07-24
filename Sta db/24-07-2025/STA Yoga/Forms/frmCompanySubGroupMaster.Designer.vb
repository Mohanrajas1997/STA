<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompanySubGroupMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cboCompanyGrp = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCompSubGrpCode = New System.Windows.Forms.TextBox()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtCompSubGrpName = New System.Windows.Forms.TextBox()
        Me.lblCompGrpName = New System.Windows.Forms.Label()
        Me.lblCompGrpCode = New System.Windows.Forms.Label()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.cboCompanyGrp)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.txtCompSubGrpCode)
        Me.pnlMain.Controls.Add(Me.txtId)
        Me.pnlMain.Controls.Add(Me.txtCompSubGrpName)
        Me.pnlMain.Controls.Add(Me.lblCompGrpName)
        Me.pnlMain.Controls.Add(Me.lblCompGrpCode)
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(13, 14)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(680, 126)
        Me.pnlMain.TabIndex = 72
        '
        'cboCompanyGrp
        '
        Me.cboCompanyGrp.FormattingEnabled = True
        Me.cboCompanyGrp.Location = New System.Drawing.Point(239, 6)
        Me.cboCompanyGrp.Name = "cboCompanyGrp"
        Me.cboCompanyGrp.Size = New System.Drawing.Size(425, 29)
        Me.cboCompanyGrp.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(49, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(184, 25)
        Me.Label5.TabIndex = 115
        Me.Label5.Text = "Comp Group Name"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCompSubGrpCode
        '
        Me.txtCompSubGrpCode.Location = New System.Drawing.Point(239, 42)
        Me.txtCompSubGrpCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCompSubGrpCode.MaxLength = 64
        Me.txtCompSubGrpCode.Name = "txtCompSubGrpCode"
        Me.txtCompSubGrpCode.Size = New System.Drawing.Size(425, 27)
        Me.txtCompSubGrpCode.TabIndex = 1
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(4, 10)
        Me.txtId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(27, 27)
        Me.txtId.TabIndex = 64
        Me.txtId.Visible = False
        '
        'txtCompSubGrpName
        '
        Me.txtCompSubGrpName.Location = New System.Drawing.Point(239, 79)
        Me.txtCompSubGrpName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCompSubGrpName.MaxLength = 64
        Me.txtCompSubGrpName.Name = "txtCompSubGrpName"
        Me.txtCompSubGrpName.Size = New System.Drawing.Size(425, 27)
        Me.txtCompSubGrpName.TabIndex = 2
        '
        'lblCompGrpName
        '
        Me.lblCompGrpName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompGrpName.Location = New System.Drawing.Point(10, 82)
        Me.lblCompGrpName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompGrpName.Name = "lblCompGrpName"
        Me.lblCompGrpName.Size = New System.Drawing.Size(223, 25)
        Me.lblCompGrpName.TabIndex = 66
        Me.lblCompGrpName.Text = "Comp Sub Group Name"
        Me.lblCompGrpName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompGrpCode
        '
        Me.lblCompGrpCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompGrpCode.Location = New System.Drawing.Point(10, 45)
        Me.lblCompGrpCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompGrpCode.Name = "lblCompGrpCode"
        Me.lblCompGrpCode.Size = New System.Drawing.Size(223, 25)
        Me.lblCompGrpCode.TabIndex = 67
        Me.lblCompGrpCode.Text = "Comp Sub Group Code"
        Me.lblCompGrpCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlSave
        '
        Me.pnlSave.CausesValidation = False
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSave.Location = New System.Drawing.Point(252, 147)
        Me.pnlSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(228, 43)
        Me.pnlSave.TabIndex = 74
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(120, 2)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(108, 37)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 37)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlButtons.Location = New System.Drawing.Point(67, 148)
        Me.pnlButtons.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(579, 43)
        Me.pnlButtons.TabIndex = 73
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(470, 2)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 37)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFind.Location = New System.Drawing.Point(236, 2)
        Me.btnFind.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(108, 37)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Enabled = False
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Location = New System.Drawing.Point(352, 2)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(108, 37)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEdit.Location = New System.Drawing.Point(118, 2)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(108, 37)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.SystemColors.Control
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Location = New System.Drawing.Point(2, 2)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(108, 37)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'frmCompanySubGroupMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 203)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.pnlButtons)
        Me.Name = "frmCompanySubGroupMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Company Sub Group Master"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlSave.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtCompSubGrpCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCompSubGrpName As System.Windows.Forms.TextBox
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents lblCompGrpName As System.Windows.Forms.Label
    Friend WithEvents lblCompGrpCode As System.Windows.Forms.Label
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents cboCompanyGrp As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
