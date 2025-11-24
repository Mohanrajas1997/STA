<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEvotingNsdl
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
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.cboSheetName = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtEvenNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtShrId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtBatchno = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpCutoffdate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CboCompany = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
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
        Me.pnlmain.Controls.Add(Me.Label7)
        Me.pnlmain.Controls.Add(Me.TxtEvenNo)
        Me.pnlmain.Controls.Add(Me.Label5)
        Me.pnlmain.Controls.Add(Me.TxtShrId)
        Me.pnlmain.Controls.Add(Me.Label4)
        Me.pnlmain.Controls.Add(Me.TxtBatchno)
        Me.pnlmain.Controls.Add(Me.Label3)
        Me.pnlmain.Controls.Add(Me.dtpCutoffdate)
        Me.pnlmain.Controls.Add(Me.Label2)
        Me.pnlmain.Controls.Add(Me.CboCompany)
        Me.pnlmain.Controls.Add(Me.Label1)
        Me.pnlmain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlmain.Location = New System.Drawing.Point(4, 5)
        Me.pnlmain.Name = "pnlmain"
        Me.pnlmain.Size = New System.Drawing.Size(447, 154)
        Me.pnlmain.TabIndex = 0
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(414, 96)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(28, 21)
        Me.btnBrowse.TabIndex = 18
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(5, 97)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 17)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "File Name"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(85, 96)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(323, 21)
        Me.txtFileName.TabIndex = 6
        '
        'cboSheetName
        '
        Me.cboSheetName.Enabled = False
        Me.cboSheetName.FormattingEnabled = True
        Me.cboSheetName.Location = New System.Drawing.Point(85, 125)
        Me.cboSheetName.Name = "cboSheetName"
        Me.cboSheetName.Size = New System.Drawing.Size(357, 21)
        Me.cboSheetName.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(5, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 17)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Sheet Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtEvenNo
        '
        Me.TxtEvenNo.Location = New System.Drawing.Point(324, 64)
        Me.TxtEvenNo.Name = "TxtEvenNo"
        Me.TxtEvenNo.Size = New System.Drawing.Size(118, 21)
        Me.TxtEvenNo.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(265, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Even No"
        '
        'TxtShrId
        '
        Me.TxtShrId.Location = New System.Drawing.Point(85, 64)
        Me.TxtShrId.MaxLength = 8
        Me.TxtShrId.Name = "TxtShrId"
        Me.TxtShrId.Size = New System.Drawing.Size(131, 21)
        Me.TxtShrId.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(36, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Shr Id"
        '
        'TxtBatchno
        '
        Me.TxtBatchno.Location = New System.Drawing.Point(85, 36)
        Me.TxtBatchno.MaxLength = 7
        Me.TxtBatchno.Name = "TxtBatchno"
        Me.TxtBatchno.Size = New System.Drawing.Size(131, 21)
        Me.TxtBatchno.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Batch No"
        '
        'dtpCutoffdate
        '
        Me.dtpCutoffdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpCutoffdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCutoffdate.Location = New System.Drawing.Point(324, 36)
        Me.dtpCutoffdate.Name = "dtpCutoffdate"
        Me.dtpCutoffdate.Size = New System.Drawing.Size(118, 21)
        Me.dtpCutoffdate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(241, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Cut Off Date"
        '
        'CboCompany
        '
        Me.CboCompany.FormattingEnabled = True
        Me.CboCompany.Location = New System.Drawing.Point(85, 7)
        Me.CboCompany.Name = "CboCompany"
        Me.CboCompany.Size = New System.Drawing.Size(357, 21)
        Me.CboCompany.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company"
        '
        'btnGenerate
        '
        Me.btnGenerate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.Location = New System.Drawing.Point(9, 5)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerate.TabIndex = 8
        Me.btnGenerate.Text = "Save"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnClose)
        Me.Panel2.Controls.Add(Me.btnGenerate)
        Me.Panel2.Location = New System.Drawing.Point(138, 164)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(172, 32)
        Me.Panel2.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(86, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'ofdInput
        '
        Me.ofdInput.FileName = "OpenFileDialog1"
        '
        'frmEvotingNsdl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(463, 200)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlmain)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEvotingNsdl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "NSDL E-Voting Process"
        Me.pnlmain.ResumeLayout(False)
        Me.pnlmain.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlmain As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpCutoffdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtBatchno As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtShrId As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtEvenNo As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents cboSheetName As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
End Class
