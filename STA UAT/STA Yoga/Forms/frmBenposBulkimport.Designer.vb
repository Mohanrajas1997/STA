<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBenposBulkimport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lblFolderPath = New System.Windows.Forms.Label()
        Me.txtFolderPath = New System.Windows.Forms.TextBox()
        Me.cboFileType = New System.Windows.Forms.ComboBox()
        Me.lblFileType = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lsvFile = New System.Windows.Forms.ListView()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnConvert = New System.Windows.Forms.Button()
        Me.lsvStatus = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnBrowse)
        Me.Panel1.Controls.Add(Me.lblFolderPath)
        Me.Panel1.Controls.Add(Me.txtFolderPath)
        Me.Panel1.Controls.Add(Me.cboFileType)
        Me.Panel1.Controls.Add(Me.lblFileType)
        Me.Panel1.Controls.Add(Me.dtpDate)
        Me.Panel1.Controls.Add(Me.lblDate)
        Me.Panel1.Location = New System.Drawing.Point(14, 12)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(578, 96)
        Me.Panel1.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(534, 61)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(33, 21)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblFolderPath
        '
        Me.lblFolderPath.Location = New System.Drawing.Point(4, 62)
        Me.lblFolderPath.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFolderPath.Name = "lblFolderPath"
        Me.lblFolderPath.Size = New System.Drawing.Size(84, 17)
        Me.lblFolderPath.TabIndex = 11
        Me.lblFolderPath.Text = "Folder Path"
        Me.lblFolderPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolderPath
        '
        Me.txtFolderPath.Enabled = False
        Me.txtFolderPath.Location = New System.Drawing.Point(95, 61)
        Me.txtFolderPath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtFolderPath.Name = "txtFolderPath"
        Me.txtFolderPath.Size = New System.Drawing.Size(431, 21)
        Me.txtFolderPath.TabIndex = 3
        '
        'cboFileType
        '
        Me.cboFileType.FormattingEnabled = True
        Me.cboFileType.Location = New System.Drawing.Point(95, 34)
        Me.cboFileType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cboFileType.Name = "cboFileType"
        Me.cboFileType.Size = New System.Drawing.Size(431, 21)
        Me.cboFileType.TabIndex = 2
        '
        'lblFileType
        '
        Me.lblFileType.Location = New System.Drawing.Point(4, 35)
        Me.lblFileType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFileType.Name = "lblFileType"
        Me.lblFileType.Size = New System.Drawing.Size(84, 17)
        Me.lblFileType.TabIndex = 4
        Me.lblFileType.Text = "File Type"
        Me.lblFileType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(95, 7)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(124, 21)
        Me.dtpDate.TabIndex = 0
        '
        'lblDate
        '
        Me.lblDate.Location = New System.Drawing.Point(4, 7)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(84, 17)
        Me.lblDate.TabIndex = 0
        Me.lblDate.Text = "Date"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lsvFile
        '
        Me.lsvFile.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lsvFile.HideSelection = False
        Me.lsvFile.Location = New System.Drawing.Point(14, 117)
        Me.lsvFile.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lsvFile.Name = "lsvFile"
        Me.lsvFile.Size = New System.Drawing.Size(578, 132)
        Me.lsvFile.TabIndex = 12
        Me.lsvFile.UseCompatibleStateImageBehavior = False
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkAll.Location = New System.Drawing.Point(19, 260)
        Me.chkAll.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(78, 17)
        Me.chkAll.TabIndex = 13
        Me.chkAll.Text = "Select All"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(521, 255)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(443, 255)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 15
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnConvert
        '
        Me.btnConvert.Location = New System.Drawing.Point(365, 255)
        Me.btnConvert.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(72, 24)
        Me.btnConvert.TabIndex = 14
        Me.btnConvert.Text = "Import"
        Me.btnConvert.UseVisualStyleBackColor = True
        '
        'lsvStatus
        '
        Me.lsvStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lsvStatus.HideSelection = False
        Me.lsvStatus.Location = New System.Drawing.Point(14, 285)
        Me.lsvStatus.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lsvStatus.Name = "lsvStatus"
        Me.lsvStatus.Size = New System.Drawing.Size(579, 134)
        Me.lsvStatus.TabIndex = 17
        Me.lsvStatus.UseCompatibleStateImageBehavior = False
        '
        'frmBenposBulkimport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 431)
        Me.Controls.Add(Me.lsvStatus)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnConvert)
        Me.Controls.Add(Me.lsvFile)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmBenposBulkimport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Benpos Bulk Import"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lblFolderPath As System.Windows.Forms.Label
    Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents cboFileType As System.Windows.Forms.ComboBox
    Friend WithEvents lblFileType As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lsvFile As System.Windows.Forms.ListView
    Friend WithEvents chkAll As CheckBox
    Friend WithEvents btnClose As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnConvert As Button
    Friend WithEvents lsvStatus As ListView
End Class
