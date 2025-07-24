<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmECSInitialRejectImport
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Cbofinyr = New System.Windows.Forms.ComboBox()
        Me.cbointerimcode = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.cboSheetName = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblFinYear = New System.Windows.Forms.Label()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.lsvFile = New System.Windows.Forms.ListView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnConvert = New System.Windows.Forms.Button()
        Me.lsvStatus = New System.Windows.Forms.ListView()
        Me.ofdInput = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Cbofinyr)
        Me.Panel1.Controls.Add(Me.cbointerimcode)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cboCompany)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnBrowse)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.cboSheetName)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblFinYear)
        Me.Panel1.Controls.Add(Me.lblCompany)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(508, 152)
        Me.Panel1.TabIndex = 0
        '
        'Cbofinyr
        '
        Me.Cbofinyr.FormattingEnabled = True
        Me.Cbofinyr.Location = New System.Drawing.Point(91, 34)
        Me.Cbofinyr.Name = "Cbofinyr"
        Me.Cbofinyr.Size = New System.Drawing.Size(154, 21)
        Me.Cbofinyr.TabIndex = 2
        '
        'cbointerimcode
        '
        Me.cbointerimcode.FormattingEnabled = True
        Me.cbointerimcode.Location = New System.Drawing.Point(327, 34)
        Me.cbointerimcode.Name = "cbointerimcode"
        Me.cbointerimcode.Size = New System.Drawing.Size(134, 21)
        Me.cbointerimcode.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(234, 31)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(87, 23)
        Me.Label12.TabIndex = 129
        Me.Label12.Text = "Interim Code"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(91, 7)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(370, 21)
        Me.cboCompany.TabIndex = 1
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(389, 114)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(72, 24)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(467, 61)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(28, 21)
        Me.btnBrowse.TabIndex = 5
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(13, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "File"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(91, 61)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(370, 21)
        Me.txtFileName.TabIndex = 4
        '
        'cboSheetName
        '
        Me.cboSheetName.Enabled = False
        Me.cboSheetName.FormattingEnabled = True
        Me.cboSheetName.Location = New System.Drawing.Point(91, 87)
        Me.cboSheetName.Name = "cboSheetName"
        Me.cboSheetName.Size = New System.Drawing.Size(370, 21)
        Me.cboSheetName.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(13, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Sheet Name"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFinYear
        '
        Me.lblFinYear.Location = New System.Drawing.Point(1, 38)
        Me.lblFinYear.Name = "lblFinYear"
        Me.lblFinYear.Size = New System.Drawing.Size(84, 17)
        Me.lblFinYear.TabIndex = 4
        Me.lblFinYear.Text = "Financial Year"
        Me.lblFinYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompany
        '
        Me.lblCompany.Location = New System.Drawing.Point(13, 7)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(72, 17)
        Me.lblCompany.TabIndex = 0
        Me.lblCompany.Text = "Company"
        Me.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lsvFile
        '
        Me.lsvFile.Location = New System.Drawing.Point(11, 187)
        Me.lsvFile.Name = "lsvFile"
        Me.lsvFile.Size = New System.Drawing.Size(508, 183)
        Me.lsvFile.TabIndex = 8
        Me.lsvFile.UseCompatibleStateImageBehavior = False
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 167)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 17)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "File List"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkAll.Location = New System.Drawing.Point(12, 381)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(69, 17)
        Me.chkAll.TabIndex = 3
        Me.chkAll.Text = "Select All"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(458, 376)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(62, 24)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(391, 376)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(62, 24)
        Me.btnClear.TabIndex = 10
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnConvert
        '
        Me.btnConvert.Location = New System.Drawing.Point(324, 376)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(62, 24)
        Me.btnConvert.TabIndex = 9
        Me.btnConvert.Text = "Import"
        Me.btnConvert.UseVisualStyleBackColor = True
        '
        'lsvStatus
        '
        Me.lsvStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lsvStatus.Location = New System.Drawing.Point(11, 406)
        Me.lsvStatus.Name = "lsvStatus"
        Me.lsvStatus.Size = New System.Drawing.Size(508, 116)
        Me.lsvStatus.TabIndex = 7
        Me.lsvStatus.UseCompatibleStateImageBehavior = False
        '
        'ofdInput
        '
        Me.ofdInput.FileName = "OpenFileDialog1"
        '
        'frmECSInitialRejectImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(535, 534)
        Me.Controls.Add(Me.lsvStatus)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnConvert)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lsvFile)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmECSInitialRejectImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ECS Reject File Import"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents cboSheetName As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lsvFile As System.Windows.Forms.ListView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnConvert As System.Windows.Forms.Button
    Friend WithEvents lsvStatus As System.Windows.Forms.ListView
    Friend WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblFinYear As System.Windows.Forms.Label
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents cbointerimcode As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Cbofinyr As System.Windows.Forms.ComboBox
End Class
