<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSRNpopup
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
        Me.srnno_lab = New System.Windows.Forms.Label()
        Me.scrdate_lab = New System.Windows.Forms.Label()
        Me.save_btn = New System.Windows.Forms.Button()
        Me.cancel_btn = New System.Windows.Forms.Button()
        Me.srnno_txt = New System.Windows.Forms.TextBox()
        Me.srndate = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'srnno_lab
        '
        Me.srnno_lab.AutoSize = True
        Me.srnno_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.srnno_lab.Location = New System.Drawing.Point(13, 22)
        Me.srnno_lab.Name = "srnno_lab"
        Me.srnno_lab.Size = New System.Drawing.Size(46, 13)
        Me.srnno_lab.TabIndex = 0
        Me.srnno_lab.Text = "SRN No"
        '
        'scrdate_lab
        '
        Me.scrdate_lab.AutoSize = True
        Me.scrdate_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.scrdate_lab.Location = New System.Drawing.Point(12, 58)
        Me.scrdate_lab.Name = "scrdate_lab"
        Me.scrdate_lab.Size = New System.Drawing.Size(59, 13)
        Me.scrdate_lab.TabIndex = 1
        Me.scrdate_lab.Text = "SRN Date"
        '
        'save_btn
        '
        Me.save_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save_btn.Location = New System.Drawing.Point(69, 105)
        Me.save_btn.Name = "save_btn"
        Me.save_btn.Size = New System.Drawing.Size(75, 23)
        Me.save_btn.TabIndex = 2
        Me.save_btn.Text = "Save"
        Me.save_btn.UseVisualStyleBackColor = True
        '
        'cancel_btn
        '
        Me.cancel_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancel_btn.Location = New System.Drawing.Point(171, 105)
        Me.cancel_btn.Name = "cancel_btn"
        Me.cancel_btn.Size = New System.Drawing.Size(75, 23)
        Me.cancel_btn.TabIndex = 3
        Me.cancel_btn.Text = "Cancel"
        Me.cancel_btn.UseVisualStyleBackColor = True
        '
        'srnno_txt
        '
        Me.srnno_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.srnno_txt.Location = New System.Drawing.Point(93, 19)
        Me.srnno_txt.Name = "srnno_txt"
        Me.srnno_txt.Size = New System.Drawing.Size(218, 21)
        Me.srnno_txt.TabIndex = 4
        '
        'srndate
        '
        Me.srndate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.srndate.Location = New System.Drawing.Point(93, 58)
        Me.srndate.Name = "srndate"
        Me.srndate.Size = New System.Drawing.Size(218, 21)
        Me.srndate.TabIndex = 6
        '
        'frmSRNpopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 144)
        Me.Controls.Add(Me.srndate)
        Me.Controls.Add(Me.srnno_txt)
        Me.Controls.Add(Me.cancel_btn)
        Me.Controls.Add(Me.save_btn)
        Me.Controls.Add(Me.scrdate_lab)
        Me.Controls.Add(Me.srnno_lab)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSRNpopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmSRNpopup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents srnno_lab As System.Windows.Forms.Label
    Friend WithEvents scrdate_lab As System.Windows.Forms.Label
    Friend WithEvents save_btn As System.Windows.Forms.Button
    Friend WithEvents cancel_btn As System.Windows.Forms.Button
    Friend WithEvents srnno_txt As System.Windows.Forms.TextBox
    Friend WithEvents srndate As System.Windows.Forms.DateTimePicker
End Class
