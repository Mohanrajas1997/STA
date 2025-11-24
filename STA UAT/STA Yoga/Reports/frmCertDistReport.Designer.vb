<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCertDistReport
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
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCertShares = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpReleaseTo = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpReleaseFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtHolderName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpHoldTo = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpHoldFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpLockinTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpLockinFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpIssueTo = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpIssueFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.txtDistFrom = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtDistTo = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDistShares = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
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
        Me.dgvList.Location = New System.Drawing.Point(12, 197)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(830, 76)
        Me.dgvList.TabIndex = 1
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.txtDistShares)
        Me.pnlSearch.Controls.Add(Me.Label19)
        Me.pnlSearch.Controls.Add(Me.txtDistTo)
        Me.pnlSearch.Controls.Add(Me.Label18)
        Me.pnlSearch.Controls.Add(Me.txtDistFrom)
        Me.pnlSearch.Controls.Add(Me.Label17)
        Me.pnlSearch.Controls.Add(Me.cboStatus)
        Me.pnlSearch.Controls.Add(Me.Label16)
        Me.pnlSearch.Controls.Add(Me.txtCertShares)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.txtRemark)
        Me.pnlSearch.Controls.Add(Me.Label15)
        Me.pnlSearch.Controls.Add(Me.dtpReleaseTo)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.dtpReleaseFrom)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.txtHolderName)
        Me.pnlSearch.Controls.Add(Me.Label7)
        Me.pnlSearch.Controls.Add(Me.txtPanNo)
        Me.pnlSearch.Controls.Add(Me.Label8)
        Me.pnlSearch.Controls.Add(Me.dtpHoldTo)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.dtpHoldFrom)
        Me.pnlSearch.Controls.Add(Me.Label6)
        Me.pnlSearch.Controls.Add(Me.dtpLockinTo)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.dtpLockinFrom)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.txtCertNo)
        Me.pnlSearch.Controls.Add(Me.Label14)
        Me.pnlSearch.Controls.Add(Me.dtpIssueTo)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Controls.Add(Me.dtpIssueFrom)
        Me.pnlSearch.Controls.Add(Me.Label13)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.txtFolioNo)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Location = New System.Drawing.Point(12, 8)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(830, 183)
        Me.pnlSearch.TabIndex = 0
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(689, 122)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(120, 21)
        Me.cboStatus.TabIndex = 18
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(609, 124)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(73, 17)
        Me.Label16.TabIndex = 143
        Me.Label16.Text = "Status"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertShares
        '
        Me.txtCertShares.Location = New System.Drawing.Point(485, 95)
        Me.txtCertShares.Name = "txtCertShares"
        Me.txtCertShares.Size = New System.Drawing.Size(120, 21)
        Me.txtCertShares.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(391, 97)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 17)
        Me.Label11.TabIndex = 140
        Me.Label11.Text = "Cert Shares"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(689, 95)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(120, 21)
        Me.txtRemark.TabIndex = 14
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(610, 97)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(73, 17)
        Me.Label15.TabIndex = 138
        Me.Label15.Text = "Remark"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpReleaseTo
        '
        Me.dtpReleaseTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpReleaseTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReleaseTo.Location = New System.Drawing.Point(288, 95)
        Me.dtpReleaseTo.Name = "dtpReleaseTo"
        Me.dtpReleaseTo.ShowCheckBox = True
        Me.dtpReleaseTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpReleaseTo.TabIndex = 12
        Me.dtpReleaseTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(233, 97)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 17)
        Me.Label9.TabIndex = 136
        Me.Label9.Text = "To"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpReleaseFrom
        '
        Me.dtpReleaseFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpReleaseFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReleaseFrom.Location = New System.Drawing.Point(95, 95)
        Me.dtpReleaseFrom.Name = "dtpReleaseFrom"
        Me.dtpReleaseFrom.ShowCheckBox = True
        Me.dtpReleaseFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpReleaseFrom.TabIndex = 11
        Me.dtpReleaseFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(4, 97)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 17)
        Me.Label10.TabIndex = 137
        Me.Label10.Text = "Release From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHolderName
        '
        Me.txtHolderName.Location = New System.Drawing.Point(485, 68)
        Me.txtHolderName.Name = "txtHolderName"
        Me.txtHolderName.Size = New System.Drawing.Size(120, 21)
        Me.txtHolderName.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(406, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 17)
        Me.Label7.TabIndex = 132
        Me.Label7.Text = "Holder"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPanNo
        '
        Me.txtPanNo.Location = New System.Drawing.Point(689, 68)
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.Size = New System.Drawing.Size(120, 21)
        Me.txtPanNo.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(610, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 17)
        Me.Label8.TabIndex = 130
        Me.Label8.Text = "Pan No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpHoldTo
        '
        Me.dtpHoldTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpHoldTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHoldTo.Location = New System.Drawing.Point(288, 68)
        Me.dtpHoldTo.Name = "dtpHoldTo"
        Me.dtpHoldTo.ShowCheckBox = True
        Me.dtpHoldTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpHoldTo.TabIndex = 8
        Me.dtpHoldTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(233, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 17)
        Me.Label5.TabIndex = 128
        Me.Label5.Text = "To"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpHoldFrom
        '
        Me.dtpHoldFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpHoldFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHoldFrom.Location = New System.Drawing.Point(95, 68)
        Me.dtpHoldFrom.Name = "dtpHoldFrom"
        Me.dtpHoldFrom.ShowCheckBox = True
        Me.dtpHoldFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpHoldFrom.TabIndex = 7
        Me.dtpHoldFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(4, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 17)
        Me.Label6.TabIndex = 129
        Me.Label6.Text = "Hold From"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpLockinTo
        '
        Me.dtpLockinTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpLockinTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLockinTo.Location = New System.Drawing.Point(288, 41)
        Me.dtpLockinTo.Name = "dtpLockinTo"
        Me.dtpLockinTo.ShowCheckBox = True
        Me.dtpLockinTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpLockinTo.TabIndex = 4
        Me.dtpLockinTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(233, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 17)
        Me.Label2.TabIndex = 124
        Me.Label2.Text = "To"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpLockinFrom
        '
        Me.dtpLockinFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpLockinFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLockinFrom.Location = New System.Drawing.Point(95, 41)
        Me.dtpLockinFrom.Name = "dtpLockinFrom"
        Me.dtpLockinFrom.ShowCheckBox = True
        Me.dtpLockinFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpLockinFrom.TabIndex = 3
        Me.dtpLockinFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 17)
        Me.Label4.TabIndex = 125
        Me.Label4.Text = "Lockin From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertNo
        '
        Me.txtCertNo.Location = New System.Drawing.Point(485, 41)
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.Size = New System.Drawing.Size(120, 21)
        Me.txtCertNo.TabIndex = 5
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(406, 43)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 17)
        Me.Label14.TabIndex = 120
        Me.Label14.Text = "Cert No"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpIssueTo
        '
        Me.dtpIssueTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpIssueTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIssueTo.Location = New System.Drawing.Point(288, 12)
        Me.dtpIssueTo.Name = "dtpIssueTo"
        Me.dtpIssueTo.ShowCheckBox = True
        Me.dtpIssueTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpIssueTo.TabIndex = 1
        Me.dtpIssueTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(233, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 17)
        Me.Label12.TabIndex = 118
        Me.Label12.Text = "To"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpIssueFrom
        '
        Me.dtpIssueFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpIssueFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIssueFrom.Location = New System.Drawing.Point(95, 12)
        Me.dtpIssueFrom.Name = "dtpIssueFrom"
        Me.dtpIssueFrom.ShowCheckBox = True
        Me.dtpIssueFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpIssueFrom.TabIndex = 0
        Me.dtpIssueFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(4, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 17)
        Me.Label13.TabIndex = 119
        Me.Label13.Text = "Issue From"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(659, 149)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 20
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(485, 12)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(324, 21)
        Me.cboCompany.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(406, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 17)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(737, 149)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(581, 149)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 19
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(689, 41)
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(120, 21)
        Me.txtFolioNo.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(610, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Folio No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'txtDistFrom
        '
        Me.txtDistFrom.Location = New System.Drawing.Point(95, 122)
        Me.txtDistFrom.Name = "txtDistFrom"
        Me.txtDistFrom.Size = New System.Drawing.Size(105, 21)
        Me.txtDistFrom.TabIndex = 15
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(9, 124)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 17)
        Me.Label17.TabIndex = 145
        Me.Label17.Text = "Dist From"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDistTo
        '
        Me.txtDistTo.Location = New System.Drawing.Point(288, 122)
        Me.txtDistTo.Name = "txtDistTo"
        Me.txtDistTo.Size = New System.Drawing.Size(105, 21)
        Me.txtDistTo.TabIndex = 16
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(202, 124)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 17)
        Me.Label18.TabIndex = 147
        Me.Label18.Text = "Dist To"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDistShares
        '
        Me.txtDistShares.Location = New System.Drawing.Point(485, 122)
        Me.txtDistShares.Name = "txtDistShares"
        Me.txtDistShares.Size = New System.Drawing.Size(120, 21)
        Me.txtDistShares.TabIndex = 17
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(399, 124)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 17)
        Me.Label19.TabIndex = 149
        Me.Label19.Text = "Dist Shares"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCertDistReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 469)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.dgvList)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCertDistReport"
        Me.Text = "Certificate Distinctive Series Report"
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
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtCertNo As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpIssueTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpIssueFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtpLockinTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpLockinFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpHoldTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpHoldFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtHolderName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpReleaseTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpReleaseFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCertShares As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtDistShares As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtDistTo As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDistFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
