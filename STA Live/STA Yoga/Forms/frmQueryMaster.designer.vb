<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQueryMaster
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
        Me.pnlmain = New System.Windows.Forms.Panel()
        Me.rtfqrydesc = New System.Windows.Forms.RichTextBox()
        Me.lblqrydesc = New System.Windows.Forms.Label()
        Me.txtqrycode = New System.Windows.Forms.TextBox()
        Me.lblqrycode = New System.Windows.Forms.Label()
        Me.txtid = New System.Windows.Forms.TextBox()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.pnlsave = New System.Windows.Forms.Panel()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.pnlmain.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlsave.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlmain
        '
        Me.pnlmain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlmain.Controls.Add(Me.rtfqrydesc)
        Me.pnlmain.Controls.Add(Me.lblqrydesc)
        Me.pnlmain.Controls.Add(Me.txtqrycode)
        Me.pnlmain.Controls.Add(Me.lblqrycode)
        Me.pnlmain.Location = New System.Drawing.Point(6, 6)
        Me.pnlmain.Name = "pnlmain"
        Me.pnlmain.Size = New System.Drawing.Size(455, 161)
        Me.pnlmain.TabIndex = 0
        '
        'rtfqrydesc
        '
        Me.rtfqrydesc.Location = New System.Drawing.Point(117, 39)
        Me.rtfqrydesc.Name = "rtfqrydesc"
        Me.rtfqrydesc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtfqrydesc.Size = New System.Drawing.Size(323, 108)
        Me.rtfqrydesc.TabIndex = 38
        Me.rtfqrydesc.Text = ""
        '
        'lblqrydesc
        '
        Me.lblqrydesc.AutoSize = True
        Me.lblqrydesc.Location = New System.Drawing.Point(3, 67)
        Me.lblqrydesc.Name = "lblqrydesc"
        Me.lblqrydesc.Size = New System.Drawing.Size(108, 13)
        Me.lblqrydesc.TabIndex = 2
        Me.lblqrydesc.Text = "Query Description"
        Me.lblqrydesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtqrycode
        '
        Me.txtqrycode.Location = New System.Drawing.Point(117, 12)
        Me.txtqrycode.MaxLength = 64
        Me.txtqrycode.Name = "txtqrycode"
        Me.txtqrycode.Size = New System.Drawing.Size(323, 21)
        Me.txtqrycode.TabIndex = 1
        '
        'lblqrycode
        '
        Me.lblqrycode.AutoSize = True
        Me.lblqrycode.Location = New System.Drawing.Point(39, 15)
        Me.lblqrycode.Name = "lblqrycode"
        Me.lblqrycode.Size = New System.Drawing.Size(72, 13)
        Me.lblqrycode.TabIndex = 0
        Me.lblqrycode.Text = "Query Code"
        Me.lblqrycode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtid
        '
        Me.txtid.Location = New System.Drawing.Point(13, 178)
        Me.txtid.Name = "txtid"
        Me.txtid.Size = New System.Drawing.Size(11, 21)
        Me.txtid.TabIndex = 0
        Me.txtid.Visible = False
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Location = New System.Drawing.Point(39, 172)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(389, 28)
        Me.pnlButtons.TabIndex = 39
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(312, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFind.Location = New System.Drawing.Point(156, 1)
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
        Me.btnDelete.Location = New System.Drawing.Point(234, 1)
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
        Me.btnEdit.Location = New System.Drawing.Point(78, 1)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(72, 24)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.UseVisualStyleBackColor = False
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
        'pnlsave
        '
        Me.pnlsave.Controls.Add(Me.btncancel)
        Me.pnlsave.Controls.Add(Me.btnsave)
        Me.pnlsave.Location = New System.Drawing.Point(153, 172)
        Me.pnlsave.Name = "pnlsave"
        Me.pnlsave.Size = New System.Drawing.Size(160, 28)
        Me.pnlsave.TabIndex = 38
        '
        'btncancel
        '
        Me.btncancel.Location = New System.Drawing.Point(81, 2)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(72, 24)
        Me.btncancel.TabIndex = 1
        Me.btncancel.Text = "&Cancel"
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'btnsave
        '
        Me.btnsave.Location = New System.Drawing.Point(3, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(72, 24)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "&Save"
        Me.btnsave.UseVisualStyleBackColor = False
        '
        'frmQueryMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 204)
        Me.Controls.Add(Me.pnlsave)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.txtid)
        Me.Controls.Add(Me.pnlmain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQueryMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Query Master"
        Me.pnlmain.ResumeLayout(False)
        Me.pnlmain.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlsave.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents pnlmain As System.Windows.Forms.Panel
    Friend WithEvents lblqrycode As System.Windows.Forms.Label
    Friend WithEvents txtid As System.Windows.Forms.TextBox
    Friend WithEvents lblqrydesc As System.Windows.Forms.Label
    Friend WithEvents txtqrycode As System.Windows.Forms.TextBox
    Friend WithEvents rtfqrydesc As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents pnlsave As System.Windows.Forms.Panel
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnsave As System.Windows.Forms.Button
End Class
