<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSignatureAddSingle
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboComp = New System.Windows.Forms.ComboBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtShareHolderName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(35, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboComp
        '
        Me.cboComp.FormattingEnabled = True
        Me.cboComp.Location = New System.Drawing.Point(103, 9)
        Me.cboComp.Name = "cboComp"
        Me.cboComp.Size = New System.Drawing.Size(377, 21)
        Me.cboComp.TabIndex = 0
        Me.cboComp.Tag = "*"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(486, 90)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(34, 21)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.Enabled = False
        Me.txtFileName.Location = New System.Drawing.Point(103, 90)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(377, 21)
        Me.txtFileName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(13, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 19)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Select File"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(408, 119)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "&Close"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(330, 119)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(72, 24)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "Add"
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(103, 36)
        Me.txtFolioNo.MaxLength = 32
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(377, 21)
        Me.txtFolioNo.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(20, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 93
        Me.Label7.Text = "Folio No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShareHolderName
        '
        Me.txtShareHolderName.Enabled = False
        Me.txtShareHolderName.Location = New System.Drawing.Point(103, 63)
        Me.txtShareHolderName.MaxLength = 64
        Me.txtShareHolderName.Name = "txtShareHolderName"
        Me.txtShareHolderName.Size = New System.Drawing.Size(377, 21)
        Me.txtShareHolderName.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(0, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 13)
        Me.Label8.TabIndex = 95
        Me.Label8.Text = "Share Holder"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmSignatureAddSingle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 154)
        Me.Controls.Add(Me.txtShareHolderName)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtFolioNo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboComp)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSignatureAddSingle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Signature Add"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboComp As System.Windows.Forms.ComboBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtShareHolderName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
