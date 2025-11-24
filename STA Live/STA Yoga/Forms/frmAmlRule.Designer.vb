<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAmlRule
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.TxtlenStart = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ChkYes = New System.Windows.Forms.CheckBox()
        Me.ChkRmvSpace = New System.Windows.Forms.CheckBox()
        Me.ChkRmvChr = New System.Windows.Forms.CheckBox()
        Me.ChkRmvIntial = New System.Windows.Forms.CheckBox()
        Me.CboMatchingType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRuleName = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.CboMatching = New System.Windows.Forms.ComboBox()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtlenEnd = New System.Windows.Forms.TextBox()
        Me.pnlMain.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.TxtlenEnd)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.TxtlenStart)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.ChkYes)
        Me.pnlMain.Controls.Add(Me.ChkRmvSpace)
        Me.pnlMain.Controls.Add(Me.ChkRmvChr)
        Me.pnlMain.Controls.Add(Me.ChkRmvIntial)
        Me.pnlMain.Controls.Add(Me.CboMatchingType)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.txtRuleName)
        Me.pnlMain.Controls.Add(Me.lbl1)
        Me.pnlMain.Controls.Add(Me.txtId)
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(6, 6)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(467, 205)
        Me.pnlMain.TabIndex = 0
        '
        'TxtlenStart
        '
        Me.TxtlenStart.Location = New System.Drawing.Point(102, 77)
        Me.TxtlenStart.Name = "TxtlenStart"
        Me.TxtlenStart.Size = New System.Drawing.Size(100, 21)
        Me.TxtlenStart.TabIndex = 75
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "Length Start"
        '
        'ChkYes
        '
        Me.ChkYes.AutoSize = True
        Me.ChkYes.Location = New System.Drawing.Point(102, 179)
        Me.ChkYes.Name = "ChkYes"
        Me.ChkYes.Size = New System.Drawing.Size(102, 17)
        Me.ChkYes.TabIndex = 73
        Me.ChkYes.Text = "Active Status"
        Me.ChkYes.UseVisualStyleBackColor = True
        '
        'ChkRmvSpace
        '
        Me.ChkRmvSpace.AutoSize = True
        Me.ChkRmvSpace.Location = New System.Drawing.Point(102, 154)
        Me.ChkRmvSpace.Name = "ChkRmvSpace"
        Me.ChkRmvSpace.Size = New System.Drawing.Size(110, 17)
        Me.ChkRmvSpace.TabIndex = 71
        Me.ChkRmvSpace.Text = "Remove Space"
        Me.ChkRmvSpace.UseVisualStyleBackColor = True
        '
        'ChkRmvChr
        '
        Me.ChkRmvChr.AutoSize = True
        Me.ChkRmvChr.Location = New System.Drawing.Point(102, 131)
        Me.ChkRmvChr.Name = "ChkRmvChr"
        Me.ChkRmvChr.Size = New System.Drawing.Size(175, 17)
        Me.ChkRmvChr.TabIndex = 70
        Me.ChkRmvChr.Text = "Remove Special Character"
        Me.ChkRmvChr.UseVisualStyleBackColor = True
        '
        'ChkRmvIntial
        '
        Me.ChkRmvIntial.AutoSize = True
        Me.ChkRmvIntial.Location = New System.Drawing.Point(102, 106)
        Me.ChkRmvIntial.Name = "ChkRmvIntial"
        Me.ChkRmvIntial.Size = New System.Drawing.Size(106, 17)
        Me.ChkRmvIntial.TabIndex = 69
        Me.ChkRmvIntial.Text = "Remove Intial"
        Me.ChkRmvIntial.UseVisualStyleBackColor = True
        '
        'CboMatchingType
        '
        Me.CboMatchingType.FormattingEnabled = True
        Me.CboMatchingType.Location = New System.Drawing.Point(102, 45)
        Me.CboMatchingType.Name = "CboMatchingType"
        Me.CboMatchingType.Size = New System.Drawing.Size(349, 21)
        Me.CboMatchingType.TabIndex = 68
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(2, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "Matching Type"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRuleName
        '
        Me.txtRuleName.Location = New System.Drawing.Point(102, 8)
        Me.txtRuleName.MaxLength = 64
        Me.txtRuleName.Name = "txtRuleName"
        Me.txtRuleName.Size = New System.Drawing.Size(349, 21)
        Me.txtRuleName.TabIndex = 0
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(19, 12)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(77, 13)
        Me.lbl1.TabIndex = 66
        Me.lbl1.Text = "Rule Name"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(3, 4)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(26, 21)
        Me.txtId.TabIndex = 64
        Me.txtId.Visible = False
        '
        'CboMatching
        '
        Me.CboMatching.FormattingEnabled = True
        Me.CboMatching.Location = New System.Drawing.Point(102, 45)
        Me.CboMatching.Name = "CboMatching"
        Me.CboMatching.Size = New System.Drawing.Size(349, 21)
        Me.CboMatching.TabIndex = 68
        '
        'pnlSave
        '
        Me.pnlSave.CausesValidation = False
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSave.Location = New System.Drawing.Point(165, 217)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(152, 28)
        Me.pnlSave.TabIndex = 13
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(80, 1)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(2, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
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
        Me.pnlButtons.Location = New System.Drawing.Point(48, 218)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(386, 28)
        Me.pnlButtons.TabIndex = 12
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
        Me.btnClose.UseVisualStyleBackColor = True
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
        Me.btnFind.UseVisualStyleBackColor = True
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
        Me.btnDelete.UseVisualStyleBackColor = True
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
        Me.btnEdit.UseVisualStyleBackColor = True
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
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(208, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 76
        Me.Label3.Text = "Length End"
        '
        'TxtlenEnd
        '
        Me.TxtlenEnd.Location = New System.Drawing.Point(292, 77)
        Me.TxtlenEnd.Name = "TxtlenEnd"
        Me.TxtlenEnd.Size = New System.Drawing.Size(100, 21)
        Me.TxtlenEnd.TabIndex = 77
        '
        'frmAmlRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 253)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlMain)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAmlRule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AML Rule"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlSave.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtRuleName As System.Windows.Forms.TextBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CboMatchingType As System.Windows.Forms.ComboBox
    Friend WithEvents ChkRmvIntial As System.Windows.Forms.CheckBox
    Friend WithEvents ChkRmvSpace As System.Windows.Forms.CheckBox
    Friend WithEvents ChkRmvChr As System.Windows.Forms.CheckBox
    Friend WithEvents ChkYes As System.Windows.Forms.CheckBox
    Friend WithEvents CboMatching As System.Windows.Forms.ComboBox
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtlenStart As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtlenEnd As System.Windows.Forms.TextBox
End Class
