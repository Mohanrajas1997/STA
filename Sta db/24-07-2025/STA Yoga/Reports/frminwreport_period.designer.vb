<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frminwreport_period
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
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.Dateinwardto = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtp_inwardfrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(3, 571)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(781, 33)
        Me.pnlExport.TabIndex = 15
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(3, 11)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(364, 14)
        Me.txtTotRec.TabIndex = 0
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(695, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(3, 95)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(781, 468)
        Me.dgvList.TabIndex = 1
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.Dateinwardto)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.cboDocType)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.dtp_inwardfrom)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(3, 5)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(781, 84)
        Me.pnlSearch.TabIndex = 0
        '
        'Dateinwardto
        '
        Me.Dateinwardto.Checked = False
        Me.Dateinwardto.CustomFormat = "dd-MM-yyyy"
        Me.Dateinwardto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dateinwardto.Location = New System.Drawing.Point(616, 22)
        Me.Dateinwardto.Name = "Dateinwardto"
        Me.Dateinwardto.ShowCheckBox = True
        Me.Dateinwardto.Size = New System.Drawing.Size(118, 21)
        Me.Dateinwardto.TabIndex = 2
        Me.Dateinwardto.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(552, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 143
        Me.Label5.Text = "Date To"
        '
        'cboDocType
        '
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Location = New System.Drawing.Point(93, 55)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(117, 21)
        Me.cboDocType.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 13)
        Me.Label4.TabIndex = 141
        Me.Label4.Text = "Inward Code "
        '
        'dtp_inwardfrom
        '
        Me.dtp_inwardfrom.Checked = False
        Me.dtp_inwardfrom.CustomFormat = "dd-MM-yyyy"
        Me.dtp_inwardfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_inwardfrom.Location = New System.Drawing.Point(420, 22)
        Me.dtp_inwardfrom.Name = "dtp_inwardfrom"
        Me.dtp_inwardfrom.ShowCheckBox = True
        Me.dtp_inwardfrom.Size = New System.Drawing.Size(118, 21)
        Me.dtp_inwardfrom.TabIndex = 1
        Me.dtp_inwardfrom.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(344, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 126
        Me.Label3.Text = "Date  From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 124
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(77, 25)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(264, 21)
        Me.cboCompany.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(619, 52)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(697, 52)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(541, 52)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frminwreport_period
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 616)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlSearch)
        Me.Name = "frminwreport_period"
        Me.Text = "Inward Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
    End Sub
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents dtp_inwardfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Dateinwardto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
