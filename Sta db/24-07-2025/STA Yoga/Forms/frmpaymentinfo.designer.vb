<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmpaymentinfo
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Rbdyes = New System.Windows.Forms.RadioButton()
        Me.RbdNo = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CboCurrency = New System.Windows.Forms.ComboBox()
        Me.Txtbeneficiary = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtDpid = New System.Windows.Forms.TextBox()
        Me.Txtclientid = New System.Windows.Forms.TextBox()
        Me.Cbocompany = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboclienttype = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbopaymode = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl2 = New System.Windows.Forms.Label()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.pnlnew = New System.Windows.Forms.Panel()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.pnlMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlnew.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.GroupBox1)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.CboCurrency)
        Me.pnlMain.Controls.Add(Me.Txtbeneficiary)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.TxtDpid)
        Me.pnlMain.Controls.Add(Me.Txtclientid)
        Me.pnlMain.Controls.Add(Me.Cbocompany)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.cboclienttype)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.cbopaymode)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.lbl2)
        Me.pnlMain.Controls.Add(Me.lbl1)
        Me.pnlMain.Controls.Add(Me.txtId)
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(6, 4)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(608, 161)
        Me.pnlMain.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Rbdyes)
        Me.GroupBox1.Controls.Add(Me.RbdNo)
        Me.GroupBox1.Location = New System.Drawing.Point(408, 106)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(140, 40)
        Me.GroupBox1.TabIndex = 110
        Me.GroupBox1.TabStop = False
        '
        'Rbdyes
        '
        Me.Rbdyes.AutoSize = True
        Me.Rbdyes.Location = New System.Drawing.Point(13, 8)
        Me.Rbdyes.Name = "Rbdyes"
        Me.Rbdyes.Size = New System.Drawing.Size(45, 17)
        Me.Rbdyes.TabIndex = 11
        Me.Rbdyes.TabStop = True
        Me.Rbdyes.Text = "Yes"
        Me.Rbdyes.UseVisualStyleBackColor = True
        '
        'RbdNo
        '
        Me.RbdNo.AutoSize = True
        Me.RbdNo.Location = New System.Drawing.Point(80, 9)
        Me.RbdNo.Name = "RbdNo"
        Me.RbdNo.Size = New System.Drawing.Size(39, 17)
        Me.RbdNo.TabIndex = 12
        Me.RbdNo.TabStop = True
        Me.RbdNo.Text = "No"
        Me.RbdNo.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(325, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 109
        Me.Label1.Text = "Active Flag"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CboCurrency
        '
        Me.CboCurrency.FormattingEnabled = True
        Me.CboCurrency.Location = New System.Drawing.Point(102, 130)
        Me.CboCurrency.Name = "CboCurrency"
        Me.CboCurrency.Size = New System.Drawing.Size(183, 21)
        Me.CboCurrency.TabIndex = 10
        '
        'Txtbeneficiary
        '
        Me.Txtbeneficiary.Location = New System.Drawing.Point(409, 79)
        Me.Txtbeneficiary.MaxLength = 64
        Me.Txtbeneficiary.Name = "Txtbeneficiary"
        Me.Txtbeneficiary.Size = New System.Drawing.Size(183, 21)
        Me.Txtbeneficiary.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(305, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 15)
        Me.Label6.TabIndex = 106
        Me.Label6.Text = "Beneficiary"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtDpid
        '
        Me.TxtDpid.Location = New System.Drawing.Point(105, 72)
        Me.TxtDpid.MaxLength = 32
        Me.TxtDpid.Name = "TxtDpid"
        Me.TxtDpid.Size = New System.Drawing.Size(183, 21)
        Me.TxtDpid.TabIndex = 4
        '
        'Txtclientid
        '
        Me.Txtclientid.Location = New System.Drawing.Point(409, 40)
        Me.Txtclientid.MaxLength = 32
        Me.Txtclientid.Name = "Txtclientid"
        Me.Txtclientid.Size = New System.Drawing.Size(183, 21)
        Me.Txtclientid.TabIndex = 3
        '
        'Cbocompany
        '
        Me.Cbocompany.FormattingEnabled = True
        Me.Cbocompany.Location = New System.Drawing.Point(102, 9)
        Me.Cbocompany.Name = "Cbocompany"
        Me.Cbocompany.Size = New System.Drawing.Size(490, 21)
        Me.Cbocompany.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(19, 133)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "Currency"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboclienttype
        '
        Me.cboclienttype.FormattingEnabled = True
        Me.cboclienttype.Location = New System.Drawing.Point(105, 39)
        Me.cboclienttype.Name = "cboclienttype"
        Me.cboclienttype.Size = New System.Drawing.Size(183, 21)
        Me.cboclienttype.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(-10, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 13)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Folio/Client Type"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbopaymode
        '
        Me.cbopaymode.FormattingEnabled = True
        Me.cbopaymode.Location = New System.Drawing.Point(102, 102)
        Me.cbopaymode.Name = "cbopaymode"
        Me.cbopaymode.Size = New System.Drawing.Size(183, 21)
        Me.cbopaymode.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(-1, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 15)
        Me.Label3.TabIndex = 84
        Me.Label3.Text = "Paymode"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(305, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "Folio/Client Id"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl2
        '
        Me.lbl2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl2.Location = New System.Drawing.Point(-1, 74)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(97, 13)
        Me.lbl2.TabIndex = 68
        Me.lbl2.Text = "Dp Id"
        Me.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(19, 12)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(77, 13)
        Me.lbl1.TabIndex = 66
        Me.lbl1.Text = "Company"
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
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(6, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(342, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(88, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFind.Location = New System.Drawing.Point(179, 7)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(72, 24)
        Me.btnFind.TabIndex = 16
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEdit.Location = New System.Drawing.Point(100, 6)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(72, 24)
        Me.btnEdit.TabIndex = 17
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.SystemColors.Control
        Me.btnNew.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Location = New System.Drawing.Point(17, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(72, 24)
        Me.btnNew.TabIndex = 18
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'pnlnew
        '
        Me.pnlnew.Controls.Add(Me.btnClose)
        Me.pnlnew.Controls.Add(Me.btnDelete)
        Me.pnlnew.Controls.Add(Me.btnNew)
        Me.pnlnew.Controls.Add(Me.btnFind)
        Me.pnlnew.Controls.Add(Me.btnEdit)
        Me.pnlnew.Location = New System.Drawing.Point(82, 173)
        Me.pnlnew.Name = "pnlnew"
        Me.pnlnew.Size = New System.Drawing.Size(437, 35)
        Me.pnlnew.TabIndex = 1
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Location = New System.Drawing.Point(259, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 24)
        Me.btnDelete.TabIndex = 19
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'pnlSave
        '
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Location = New System.Drawing.Point(191, 177)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(164, 30)
        Me.pnlSave.TabIndex = 20
        '
        'frmpaymentinfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 211)
        Me.Controls.Add(Me.pnlnew)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSave)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmpaymentinfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Info"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlnew.ResumeLayout(False)
        Me.pnlSave.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lbl2 As System.Windows.Forms.Label
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboclienttype As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbopaymode As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Cbocompany As System.Windows.Forms.ComboBox
    Friend WithEvents TxtDpid As System.Windows.Forms.TextBox
    Friend WithEvents Txtclientid As System.Windows.Forms.TextBox
    Friend WithEvents Txtbeneficiary As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Rbdyes As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CboCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents RbdNo As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents pnlnew As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
End Class
