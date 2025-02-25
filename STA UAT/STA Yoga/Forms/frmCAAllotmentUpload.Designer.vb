<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCAAllotmentUpload
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
        Me.btnNsdl = New System.Windows.Forms.Button()
        Me.btnCDSL = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnNsdl
        '
        Me.btnNsdl.Location = New System.Drawing.Point(42, 52)
        Me.btnNsdl.Name = "btnNsdl"
        Me.btnNsdl.Size = New System.Drawing.Size(75, 23)
        Me.btnNsdl.TabIndex = 0
        Me.btnNsdl.Text = "NSDL"
        Me.btnNsdl.UseVisualStyleBackColor = True
        '
        'btnCDSL
        '
        Me.btnCDSL.Location = New System.Drawing.Point(123, 52)
        Me.btnCDSL.Name = "btnCDSL"
        Me.btnCDSL.Size = New System.Drawing.Size(75, 23)
        Me.btnCDSL.TabIndex = 1
        Me.btnCDSL.Text = "CDSL"
        Me.btnCDSL.UseVisualStyleBackColor = True
        '
        'frmCAAllotmentUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(239, 147)
        Me.Controls.Add(Me.btnCDSL)
        Me.Controls.Add(Me.btnNsdl)
        Me.Name = "frmCAAllotmentUpload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CA Allotment Upload"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnNsdl As System.Windows.Forms.Button
    Friend WithEvents btnCDSL As System.Windows.Forms.Button
End Class
