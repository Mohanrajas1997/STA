<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRudCorresReport
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
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CboCompany = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtCertificate = New System.Windows.Forms.TextBox()
        Me.CboStatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboDoctype = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtDisAWD = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRcdAwd = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFoliono = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboSubtype = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.Txtholdername = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
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
        Me.dgvList.Location = New System.Drawing.Point(12, 155)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(778, 126)
        Me.dgvList.TabIndex = 1
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.Txtholdername)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.dtpTo)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.dtpFrom)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.CboCompany)
        Me.pnlSearch.Controls.Add(Me.Label6)
        Me.pnlSearch.Controls.Add(Me.TxtCertificate)
        Me.pnlSearch.Controls.Add(Me.CboStatus)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Controls.Add(Me.txtInwardNo)
        Me.pnlSearch.Controls.Add(Me.Label8)
        Me.pnlSearch.Controls.Add(Me.cboDoctype)
        Me.pnlSearch.Controls.Add(Me.Label7)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Controls.Add(Me.txtDisAWD)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.txtRcdAwd)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.txtFoliono)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.cboSubtype)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Location = New System.Drawing.Point(12, 8)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(978, 141)
        Me.pnlSearch.TabIndex = 0
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(243, 8)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(124, 21)
        Me.dtpTo.TabIndex = 146
        Me.dtpTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(220, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(21, 13)
        Me.Label10.TabIndex = 145
        Me.Label10.Text = "To"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(93, 8)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(124, 21)
        Me.dtpFrom.TabIndex = 144
        Me.dtpFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(0, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 15)
        Me.Label9.TabIndex = 143
        Me.Label9.Text = "Inward Date"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CboCompany
        '
        Me.CboCompany.FormattingEnabled = True
        Me.CboCompany.Location = New System.Drawing.Point(432, 8)
        Me.CboCompany.Name = "CboCompany"
        Me.CboCompany.Size = New System.Drawing.Size(240, 21)
        Me.CboCompany.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(350, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 13)
        Me.Label6.TabIndex = 142
        Me.Label6.Text = "Company"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtCertificate
        '
        Me.TxtCertificate.Location = New System.Drawing.Point(92, 42)
        Me.TxtCertificate.Name = "TxtCertificate"
        Me.TxtCertificate.Size = New System.Drawing.Size(122, 21)
        Me.TxtCertificate.TabIndex = 5
        '
        'CboStatus
        '
        Me.CboStatus.FormattingEnabled = True
        Me.CboStatus.Location = New System.Drawing.Point(746, 71)
        Me.CboStatus.Name = "CboStatus"
        Me.CboStatus.Size = New System.Drawing.Size(126, 21)
        Me.CboStatus.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(679, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 22)
        Me.Label1.TabIndex = 139
        Me.Label1.Text = "Status"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Location = New System.Drawing.Point(92, 70)
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(123, 21)
        Me.txtInwardNo.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(11, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 15)
        Me.Label8.TabIndex = 138
        Me.Label8.Text = "Inward No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDoctype
        '
        Me.cboDoctype.FormattingEnabled = True
        Me.cboDoctype.Location = New System.Drawing.Point(745, 9)
        Me.cboDoctype.Name = "cboDoctype"
        Me.cboDoctype.Size = New System.Drawing.Size(221, 21)
        Me.cboDoctype.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(672, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 137
        Me.Label7.Text = "Doc Type"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(-2, 42)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 19)
        Me.Label12.TabIndex = 127
        Me.Label12.Text = "Certificate No"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDisAWD
        '
        Me.txtDisAWD.Location = New System.Drawing.Point(544, 70)
        Me.txtDisAWD.Name = "txtDisAWD"
        Me.txtDisAWD.Size = New System.Drawing.Size(126, 21)
        Me.txtDisAWD.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(442, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 15)
        Me.Label5.TabIndex = 95
        Me.Label5.Text = "Dispatched AWB No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRcdAwd
        '
        Me.txtRcdAwd.Location = New System.Drawing.Point(315, 69)
        Me.txtRcdAwd.Name = "txtRcdAwd"
        Me.txtRcdAwd.Size = New System.Drawing.Size(124, 21)
        Me.txtRcdAwd.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(224, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "Received AWB No"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFoliono
        '
        Me.txtFoliono.Location = New System.Drawing.Point(315, 43)
        Me.txtFoliono.Name = "txtFoliono"
        Me.txtFoliono.Size = New System.Drawing.Size(124, 21)
        Me.txtFoliono.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(231, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Folio No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(791, 102)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'cboSubtype
        '
        Me.cboSubtype.FormattingEnabled = True
        Me.cboSubtype.Location = New System.Drawing.Point(745, 44)
        Me.cboSubtype.Name = "cboSubtype"
        Me.cboSubtype.Size = New System.Drawing.Size(222, 21)
        Me.cboSubtype.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(669, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 15)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Sub Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(869, 102)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(713, 102)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 10
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(12, 288)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(635, 33)
        Me.pnlExport.TabIndex = 2
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
        Me.txtTotRec.Size = New System.Drawing.Size(433, 14)
        Me.txtTotRec.TabIndex = 0
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(562, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 13
        Me.btnExport.Text = "&Export"
        '
        'Txtholdername
        '
        Me.Txtholdername.Location = New System.Drawing.Point(543, 44)
        Me.Txtholdername.Name = "Txtholdername"
        Me.Txtholdername.Size = New System.Drawing.Size(127, 21)
        Me.Txtholdername.TabIndex = 147
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(445, 42)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 20)
        Me.Label11.TabIndex = 148
        Me.Label11.Text = "Holder Name"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmRudCorresReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 469)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.dgvList)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmRudCorresReport"
        Me.Text = "Rud\Correspondence Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()


    End Sub

    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents cboSubtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtDisAWD As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFoliono As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRcdAwd As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboDoctype As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtCertificate As System.Windows.Forms.TextBox
    Friend WithEvents CboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Txtholdername As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
