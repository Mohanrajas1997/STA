<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportAll
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportAll))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.cboSheetName = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboFileType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnBrowse)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.cboSheetName)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.cboFileType)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.dtpDate)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(508, 152)
        Me.Panel1.TabIndex = 0
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
        Me.btnBrowse.TabIndex = 4
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
        Me.txtFileName.TabIndex = 3
        '
        'cboSheetName
        '
        Me.cboSheetName.Enabled = False
        Me.cboSheetName.FormattingEnabled = True
        Me.cboSheetName.Location = New System.Drawing.Point(91, 87)
        Me.cboSheetName.Name = "cboSheetName"
        Me.cboSheetName.Size = New System.Drawing.Size(370, 21)
        Me.cboSheetName.TabIndex = 5
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
        'cboFileType
        '
        Me.cboFileType.FormattingEnabled = True
        Me.cboFileType.Location = New System.Drawing.Point(91, 34)
        Me.cboFileType.Name = "cboFileType"
        Me.cboFileType.Size = New System.Drawing.Size(370, 21)
        Me.cboFileType.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(13, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "File Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(91, 7)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(107, 21)
        Me.dtpDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lsvFile
        '
        Me.lsvFile.Location = New System.Drawing.Point(11, 187)
        Me.lsvFile.Name = "lsvFile"
        Me.lsvFile.Size = New System.Drawing.Size(508, 183)
        Me.lsvFile.TabIndex = 2
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
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(391, 376)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(62, 24)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnConvert
        '
        Me.btnConvert.Location = New System.Drawing.Point(324, 376)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(62, 24)
        Me.btnConvert.TabIndex = 4
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
        'frmImportAll
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportAll"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Files"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents cboSheetName As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboFileType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lsvFile As System.Windows.Forms.ListView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnConvert As System.Windows.Forms.Button
    Friend WithEvents lsvStatus As System.Windows.Forms.ListView
    Friend WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
End Class
