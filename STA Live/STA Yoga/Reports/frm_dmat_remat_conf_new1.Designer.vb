<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_dmat_remat_conf_new1
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
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.btnDRpdf = New System.Windows.Forms.Button()
        Me.btnDRNotepad = New System.Windows.Forms.Button()
        Me.cbotype = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbdepotype = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtp_dateto = New System.Windows.Forms.DateTimePicker()
        Me.trandateto = New System.Windows.Forms.Label()
        Me.dtp_datefrom = New System.Windows.Forms.DateTimePicker()
        Me.trandatefrom = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.btnpdf = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(12, 90)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(863, 480)
        Me.dgvList.TabIndex = 18
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.btnDRpdf)
        Me.pnlSearch.Controls.Add(Me.btnDRNotepad)
        Me.pnlSearch.Controls.Add(Me.cbotype)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.cbdepotype)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.dtp_dateto)
        Me.pnlSearch.Controls.Add(Me.trandateto)
        Me.pnlSearch.Controls.Add(Me.dtp_datefrom)
        Me.pnlSearch.Controls.Add(Me.trandatefrom)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(12, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(863, 72)
        Me.pnlSearch.TabIndex = 17
        '
        'btnDRpdf
        '
        Me.btnDRpdf.Location = New System.Drawing.Point(497, 37)
        Me.btnDRpdf.Name = "btnDRpdf"
        Me.btnDRpdf.Size = New System.Drawing.Size(89, 24)
        Me.btnDRpdf.TabIndex = 135
        Me.btnDRpdf.Text = "DR-PDF"
        '
        'btnDRNotepad
        '
        Me.btnDRNotepad.Location = New System.Drawing.Point(408, 37)
        Me.btnDRNotepad.Name = "btnDRNotepad"
        Me.btnDRNotepad.Size = New System.Drawing.Size(89, 24)
        Me.btnDRNotepad.TabIndex = 134
        Me.btnDRNotepad.Text = "DR - Notepad"
        '
        'cbotype
        '
        Me.cbotype.FormattingEnabled = True
        Me.cbotype.Location = New System.Drawing.Point(732, 9)
        Me.cbotype.Name = "cbotype"
        Me.cbotype.Size = New System.Drawing.Size(121, 21)
        Me.cbotype.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(663, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 17)
        Me.Label2.TabIndex = 133
        Me.Label2.Text = "Type"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbdepotype
        '
        Me.cbdepotype.FormattingEnabled = True
        Me.cbdepotype.Location = New System.Drawing.Point(515, 9)
        Me.cbdepotype.Name = "cbdepotype"
        Me.cbdepotype.Size = New System.Drawing.Size(121, 21)
        Me.cbdepotype.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(398, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 17)
        Me.Label5.TabIndex = 131
        Me.Label5.Text = "Depository_type"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtp_dateto
        '
        Me.dtp_dateto.Checked = False
        Me.dtp_dateto.CustomFormat = "dd-MM-yyyy"
        Me.dtp_dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_dateto.Location = New System.Drawing.Point(273, 38)
        Me.dtp_dateto.Name = "dtp_dateto"
        Me.dtp_dateto.ShowCheckBox = True
        Me.dtp_dateto.Size = New System.Drawing.Size(118, 21)
        Me.dtp_dateto.TabIndex = 4
        Me.dtp_dateto.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'trandateto
        '
        Me.trandateto.AutoSize = True
        Me.trandateto.Location = New System.Drawing.Point(216, 42)
        Me.trandateto.Name = "trandateto"
        Me.trandateto.Size = New System.Drawing.Size(51, 13)
        Me.trandateto.TabIndex = 130
        Me.trandateto.Text = "Date To"
        '
        'dtp_datefrom
        '
        Me.dtp_datefrom.Checked = False
        Me.dtp_datefrom.CustomFormat = "dd-MM-yyyy"
        Me.dtp_datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_datefrom.Location = New System.Drawing.Point(89, 37)
        Me.dtp_datefrom.Name = "dtp_datefrom"
        Me.dtp_datefrom.ShowCheckBox = True
        Me.dtp_datefrom.Size = New System.Drawing.Size(118, 21)
        Me.dtp_datefrom.TabIndex = 3
        Me.dtp_datefrom.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'trandatefrom
        '
        Me.trandatefrom.AutoSize = True
        Me.trandatefrom.Location = New System.Drawing.Point(4, 41)
        Me.trandatefrom.Name = "trandatefrom"
        Me.trandatefrom.Size = New System.Drawing.Size(66, 13)
        Me.trandatefrom.TabIndex = 128
        Me.trandatefrom.Text = "Date From"
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(89, 9)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(302, 21)
        Me.cboCompany.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(675, 37)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(89, 24)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(764, 37)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(89, 24)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(586, 37)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(89, 24)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(3, 13)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(364, 14)
        Me.txtTotRec.TabIndex = 1
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.btnpdf)
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(12, 576)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(863, 46)
        Me.pnlExport.TabIndex = 19
        '
        'btnpdf
        '
        Me.btnpdf.Location = New System.Drawing.Point(693, 9)
        Me.btnpdf.Name = "btnpdf"
        Me.btnpdf.Size = New System.Drawing.Size(72, 24)
        Me.btnpdf.TabIndex = 1
        Me.btnpdf.Text = "PDF"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(771, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "Notepad"
        '
        'frm_dmat_remat_conf_new1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 641)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlExport)
        Me.Name = "frm_dmat_remat_conf_new1"
        Me.Text = "Dmat/Remat Conformation Report"
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents cbdepotype As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtp_dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents trandateto As System.Windows.Forms.Label
    Friend WithEvents dtp_datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents trandatefrom As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents cbotype As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnpdf As System.Windows.Forms.Button
    Friend WithEvents btnDRNotepad As System.Windows.Forms.Button
    Friend WithEvents btnDRpdf As System.Windows.Forms.Button
End Class
