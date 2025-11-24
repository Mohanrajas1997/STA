<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmiepfinexreasons
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
        Me.dgv_inexreason = New System.Windows.Forms.DataGridView()
        Me.save_btn = New System.Windows.Forms.Button()
        Me.reason_search_box = New System.Windows.Forms.TextBox()
        Me.inex_reason = New System.Windows.Forms.Label()
        Me.inex_reason_txt = New System.Windows.Forms.TextBox()
        CType(Me.dgv_inexreason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_inexreason
        '
        Me.dgv_inexreason.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_inexreason.GridColor = System.Drawing.SystemColors.Control
        Me.dgv_inexreason.Location = New System.Drawing.Point(12, 39)
        Me.dgv_inexreason.Name = "dgv_inexreason"
        Me.dgv_inexreason.Size = New System.Drawing.Size(681, 246)
        Me.dgv_inexreason.TabIndex = 0
        '
        'save_btn
        '
        Me.save_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save_btn.Location = New System.Drawing.Point(606, 347)
        Me.save_btn.Name = "save_btn"
        Me.save_btn.Size = New System.Drawing.Size(87, 23)
        Me.save_btn.TabIndex = 1
        Me.save_btn.Text = "Save"
        Me.save_btn.UseVisualStyleBackColor = True
        '
        'reason_search_box
        '
        Me.reason_search_box.Location = New System.Drawing.Point(276, 12)
        Me.reason_search_box.Name = "reason_search_box"
        Me.reason_search_box.Size = New System.Drawing.Size(417, 21)
        Me.reason_search_box.TabIndex = 2
        '
        'inex_reason
        '
        Me.inex_reason.AutoSize = True
        Me.inex_reason.Location = New System.Drawing.Point(12, 288)
        Me.inex_reason.Name = "inex_reason"
        Me.inex_reason.Size = New System.Drawing.Size(78, 13)
        Me.inex_reason.TabIndex = 3
        Me.inex_reason.Text = "Inex Reason"
        '
        'inex_reason_txt
        '
        Me.inex_reason_txt.AcceptsReturn = True
        Me.inex_reason_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inex_reason_txt.Location = New System.Drawing.Point(15, 305)
        Me.inex_reason_txt.MaxLength = 255
        Me.inex_reason_txt.Multiline = True
        Me.inex_reason_txt.Name = "inex_reason_txt"
        Me.inex_reason_txt.Size = New System.Drawing.Size(448, 65)
        Me.inex_reason_txt.TabIndex = 29
        '
        'frmiepfinexreasons
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(709, 382)
        Me.Controls.Add(Me.inex_reason_txt)
        Me.Controls.Add(Me.inex_reason)
        Me.Controls.Add(Me.reason_search_box)
        Me.Controls.Add(Me.save_btn)
        Me.Controls.Add(Me.dgv_inexreason)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmiepfinexreasons"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmiepfinexreasons"
        CType(Me.dgv_inexreason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv_inexreason As System.Windows.Forms.DataGridView
    Friend WithEvents save_btn As System.Windows.Forms.Button
    Friend WithEvents reason_search_box As System.Windows.Forms.TextBox
    Friend WithEvents inex_reason As System.Windows.Forms.Label
    Friend WithEvents inex_reason_txt As System.Windows.Forms.TextBox
End Class
