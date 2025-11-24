<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmiepflettergenerate
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
        Me.remarks_lab = New System.Windows.Forms.Label()
        Me.remark_txt = New System.Windows.Forms.TextBox()
        Me.preview_btn = New System.Windows.Forms.Button()
        Me.generate_btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'remarks_lab
        '
        Me.remarks_lab.AutoSize = True
        Me.remarks_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.remarks_lab.Location = New System.Drawing.Point(15, 13)
        Me.remarks_lab.Name = "remarks_lab"
        Me.remarks_lab.Size = New System.Drawing.Size(321, 13)
        Me.remarks_lab.TabIndex = 0
        Me.remarks_lab.Text = "If any additional reason is required, you can add it here."
        Me.remarks_lab.Visible = False
        '
        'remark_txt
        '
        Me.remark_txt.AcceptsReturn = True
        Me.remark_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.remark_txt.Location = New System.Drawing.Point(19, 29)
        Me.remark_txt.MaxLength = 255
        Me.remark_txt.Multiline = True
        Me.remark_txt.Name = "remark_txt"
        Me.remark_txt.Size = New System.Drawing.Size(495, 64)
        Me.remark_txt.TabIndex = 29
        Me.remark_txt.Visible = False
        '
        'preview_btn
        '
        Me.preview_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.preview_btn.Location = New System.Drawing.Point(155, 111)
        Me.preview_btn.Name = "preview_btn"
        Me.preview_btn.Size = New System.Drawing.Size(87, 23)
        Me.preview_btn.TabIndex = 30
        Me.preview_btn.Text = "Preview"
        Me.preview_btn.UseVisualStyleBackColor = True
        '
        'generate_btn
        '
        Me.generate_btn.Enabled = False
        Me.generate_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.generate_btn.Location = New System.Drawing.Point(250, 111)
        Me.generate_btn.Name = "generate_btn"
        Me.generate_btn.Size = New System.Drawing.Size(87, 23)
        Me.generate_btn.TabIndex = 31
        Me.generate_btn.Text = "Generate"
        Me.generate_btn.UseVisualStyleBackColor = True
        '
        'frmiepflettergenerate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 157)
        Me.Controls.Add(Me.generate_btn)
        Me.Controls.Add(Me.preview_btn)
        Me.Controls.Add(Me.remark_txt)
        Me.Controls.Add(Me.remarks_lab)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmiepflettergenerate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmiepflettergenerate"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents remarks_lab As System.Windows.Forms.Label
    Friend WithEvents remark_txt As System.Windows.Forms.TextBox
    Friend WithEvents preview_btn As System.Windows.Forms.Button
    Friend WithEvents generate_btn As System.Windows.Forms.Button
End Class
