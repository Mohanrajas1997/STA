<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddressLabelPrinting
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
        Me.pnlmain = New System.Windows.Forms.Panel()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.cboSheetName = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ofdInput = New System.Windows.Forms.OpenFileDialog()
        Me.pnlmain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlmain
        '
        Me.pnlmain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlmain.Controls.Add(Me.btnBrowse)
        Me.pnlmain.Controls.Add(Me.Label6)
        Me.pnlmain.Controls.Add(Me.txtFileName)
        Me.pnlmain.Controls.Add(Me.cboSheetName)
        Me.pnlmain.Controls.Add(Me.Label4)
        Me.pnlmain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlmain.Location = New System.Drawing.Point(13, 27)
        Me.pnlmain.Name = "pnlmain"
        Me.pnlmain.Size = New System.Drawing.Size(519, 77)
        Me.pnlmain.TabIndex = 0
        '
        'btnGenerate
        '
        Me.btnGenerate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.Location = New System.Drawing.Point(168, 5)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerate.TabIndex = 5
        Me.btnGenerate.Text = "Import"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnClose)
        Me.Panel2.Controls.Add(Me.btnGenerate)
        Me.Panel2.Location = New System.Drawing.Point(13, 110)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(519, 32)
        Me.Panel2.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(245, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(460, 16)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(28, 21)
        Me.btnBrowse.TabIndex = 13
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(6, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 17)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "File Name"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(84, 16)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(370, 21)
        Me.txtFileName.TabIndex = 12
        '
        'cboSheetName
        '
        Me.cboSheetName.Enabled = False
        Me.cboSheetName.FormattingEnabled = True
        Me.cboSheetName.Location = New System.Drawing.Point(84, 42)
        Me.cboSheetName.Name = "cboSheetName"
        Me.cboSheetName.Size = New System.Drawing.Size(370, 21)
        Me.cboSheetName.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(6, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 17)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Sheet Name"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ofdInput
        '
        Me.ofdInput.FileName = "OpenFileDialog1"
        '
        'frmAddressLabelPrinting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 144)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlmain)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddressLabelPrinting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Address Label Printing"
        Me.pnlmain.ResumeLayout(False)
        Me.pnlmain.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlmain As System.Windows.Forms.Panel
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents cboSheetName As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
End Class
