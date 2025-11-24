<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHistoryReport
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
        Me.dtpOutwardTo = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpOutwardFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtToFolioNo = New System.Windows.Forms.TextBox()
        Me.txtToHolderName = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtHolderName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.cboValid = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpInwardTo = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpInwardFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.txtOutwardNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTranNo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dtpTranTo = New System.Windows.Forms.DateTimePicker()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dtpTranFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDistTo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtDistFrom = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtDPName = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtDPId = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtClientName = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtClientId = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
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
        Me.dgvList.Location = New System.Drawing.Point(12, 253)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(832, 83)
        Me.dgvList.TabIndex = 1
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.txtClientName)
        Me.pnlSearch.Controls.Add(Me.Label24)
        Me.pnlSearch.Controls.Add(Me.txtClientId)
        Me.pnlSearch.Controls.Add(Me.Label25)
        Me.pnlSearch.Controls.Add(Me.txtDPName)
        Me.pnlSearch.Controls.Add(Me.Label22)
        Me.pnlSearch.Controls.Add(Me.txtDPId)
        Me.pnlSearch.Controls.Add(Me.Label23)
        Me.pnlSearch.Controls.Add(Me.txtRemark)
        Me.pnlSearch.Controls.Add(Me.Label21)
        Me.pnlSearch.Controls.Add(Me.txtDistTo)
        Me.pnlSearch.Controls.Add(Me.Label19)
        Me.pnlSearch.Controls.Add(Me.txtDistFrom)
        Me.pnlSearch.Controls.Add(Me.Label20)
        Me.pnlSearch.Controls.Add(Me.dtpTranTo)
        Me.pnlSearch.Controls.Add(Me.Label17)
        Me.pnlSearch.Controls.Add(Me.dtpTranFrom)
        Me.pnlSearch.Controls.Add(Me.Label18)
        Me.pnlSearch.Controls.Add(Me.txtTranNo)
        Me.pnlSearch.Controls.Add(Me.Label16)
        Me.pnlSearch.Controls.Add(Me.txtCertNo)
        Me.pnlSearch.Controls.Add(Me.Label13)
        Me.pnlSearch.Controls.Add(Me.txtOutwardNo)
        Me.pnlSearch.Controls.Add(Me.Label8)
        Me.pnlSearch.Controls.Add(Me.dtpOutwardTo)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.dtpOutwardFrom)
        Me.pnlSearch.Controls.Add(Me.Label7)
        Me.pnlSearch.Controls.Add(Me.Label14)
        Me.pnlSearch.Controls.Add(Me.txtToFolioNo)
        Me.pnlSearch.Controls.Add(Me.txtToHolderName)
        Me.pnlSearch.Controls.Add(Me.Label15)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.txtHolderName)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.txtFolioNo)
        Me.pnlSearch.Controls.Add(Me.cboValid)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.cboDocType)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.dtpInwardTo)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.dtpInwardFrom)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.txtInwardNo)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Location = New System.Drawing.Point(12, 8)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(832, 239)
        Me.pnlSearch.TabIndex = 0
        '
        'dtpOutwardTo
        '
        Me.dtpOutwardTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpOutwardTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpOutwardTo.Location = New System.Drawing.Point(290, 39)
        Me.dtpOutwardTo.Name = "dtpOutwardTo"
        Me.dtpOutwardTo.ShowCheckBox = True
        Me.dtpOutwardTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpOutwardTo.TabIndex = 5
        Me.dtpOutwardTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(235, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 17)
        Me.Label5.TabIndex = 146
        Me.Label5.Text = "To"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpOutwardFrom
        '
        Me.dtpOutwardFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpOutwardFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpOutwardFrom.Location = New System.Drawing.Point(97, 39)
        Me.dtpOutwardFrom.Name = "dtpOutwardFrom"
        Me.dtpOutwardFrom.ShowCheckBox = True
        Me.dtpOutwardFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpOutwardFrom.TabIndex = 4
        Me.dtpOutwardFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(2, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 20)
        Me.Label7.TabIndex = 147
        Me.Label7.Text = "Outward From"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(629, 41)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 20)
        Me.Label14.TabIndex = 143
        Me.Label14.Text = "To Folio No"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtToFolioNo
        '
        Me.txtToFolioNo.Location = New System.Drawing.Point(708, 39)
        Me.txtToFolioNo.Name = "txtToFolioNo"
        Me.txtToFolioNo.Size = New System.Drawing.Size(105, 21)
        Me.txtToFolioNo.TabIndex = 7
        '
        'txtToHolderName
        '
        Me.txtToHolderName.Location = New System.Drawing.Point(514, 93)
        Me.txtToHolderName.Name = "txtToHolderName"
        Me.txtToHolderName.Size = New System.Drawing.Size(299, 21)
        Me.txtToHolderName.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(412, 94)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(96, 20)
        Me.Label15.TabIndex = 140
        Me.Label15.Text = "To Holder Name"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(663, 203)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 26
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtHolderName
        '
        Me.txtHolderName.Location = New System.Drawing.Point(514, 66)
        Me.txtHolderName.Name = "txtHolderName"
        Me.txtHolderName.Size = New System.Drawing.Size(299, 21)
        Me.txtHolderName.TabIndex = 10
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(423, 65)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 20)
        Me.Label12.TabIndex = 136
        Me.Label12.Text = "Holder Name"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(435, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 17)
        Me.Label2.TabIndex = 135
        Me.Label2.Text = "Folio No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(514, 39)
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(105, 21)
        Me.txtFolioNo.TabIndex = 6
        '
        'cboValid
        '
        Me.cboValid.FormattingEnabled = True
        Me.cboValid.Location = New System.Drawing.Point(514, 174)
        Me.cboValid.Name = "cboValid"
        Me.cboValid.Size = New System.Drawing.Size(299, 21)
        Me.cboValid.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(419, 175)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 20)
        Me.Label9.TabIndex = 133
        Me.Label9.Text = "Valid"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(97, 93)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(298, 21)
        Me.cboCompany.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(17, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 20)
        Me.Label3.TabIndex = 123
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDocType
        '
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Location = New System.Drawing.Point(97, 120)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(298, 21)
        Me.cboDocType.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 20)
        Me.Label4.TabIndex = 121
        Me.Label4.Text = "Document"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpInwardTo
        '
        Me.dtpInwardTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpInwardTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInwardTo.Location = New System.Drawing.Point(290, 12)
        Me.dtpInwardTo.Name = "dtpInwardTo"
        Me.dtpInwardTo.ShowCheckBox = True
        Me.dtpInwardTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpInwardTo.TabIndex = 1
        Me.dtpInwardTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(235, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 17)
        Me.Label11.TabIndex = 118
        Me.Label11.Text = "To"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpInwardFrom
        '
        Me.dtpInwardFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpInwardFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInwardFrom.Location = New System.Drawing.Point(97, 12)
        Me.dtpInwardFrom.Name = "dtpInwardFrom"
        Me.dtpInwardFrom.ShowCheckBox = True
        Me.dtpInwardFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpInwardFrom.TabIndex = 0
        Me.dtpInwardFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(6, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 20)
        Me.Label10.TabIndex = 119
        Me.Label10.Text = "Inward From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(741, 203)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(585, 203)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 25
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Location = New System.Drawing.Point(514, 12)
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(105, 21)
        Me.txtInwardNo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(435, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Inward No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(12, 342)
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
        'txtOutwardNo
        '
        Me.txtOutwardNo.Location = New System.Drawing.Point(708, 12)
        Me.txtOutwardNo.Name = "txtOutwardNo"
        Me.txtOutwardNo.Size = New System.Drawing.Size(105, 21)
        Me.txtOutwardNo.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(629, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 17)
        Me.Label8.TabIndex = 148
        Me.Label8.Text = "Outward No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertNo
        '
        Me.txtCertNo.Location = New System.Drawing.Point(514, 120)
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.Size = New System.Drawing.Size(105, 21)
        Me.txtCertNo.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(435, 122)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 17)
        Me.Label13.TabIndex = 151
        Me.Label13.Text = "Cert No"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranNo
        '
        Me.txtTranNo.Location = New System.Drawing.Point(708, 120)
        Me.txtTranNo.Name = "txtTranNo"
        Me.txtTranNo.Size = New System.Drawing.Size(105, 21)
        Me.txtTranNo.TabIndex = 15
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(629, 124)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(73, 17)
        Me.Label16.TabIndex = 153
        Me.Label16.Text = "Tran No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTranTo
        '
        Me.dtpTranTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTranTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTranTo.Location = New System.Drawing.Point(290, 66)
        Me.dtpTranTo.Name = "dtpTranTo"
        Me.dtpTranTo.ShowCheckBox = True
        Me.dtpTranTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpTranTo.TabIndex = 9
        Me.dtpTranTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(235, 68)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(49, 17)
        Me.Label17.TabIndex = 156
        Me.Label17.Text = "To"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTranFrom
        '
        Me.dtpTranFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpTranFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTranFrom.Location = New System.Drawing.Point(97, 66)
        Me.dtpTranFrom.Name = "dtpTranFrom"
        Me.dtpTranFrom.ShowCheckBox = True
        Me.dtpTranFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpTranFrom.TabIndex = 8
        Me.dtpTranFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(2, 66)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(88, 20)
        Me.Label18.TabIndex = 157
        Me.Label18.Text = "Tran From"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDistTo
        '
        Me.txtDistTo.Location = New System.Drawing.Point(708, 147)
        Me.txtDistTo.Name = "txtDistTo"
        Me.txtDistTo.Size = New System.Drawing.Size(105, 21)
        Me.txtDistTo.TabIndex = 19
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(629, 151)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(73, 17)
        Me.Label19.TabIndex = 161
        Me.Label19.Text = "Dist To"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDistFrom
        '
        Me.txtDistFrom.Location = New System.Drawing.Point(514, 147)
        Me.txtDistFrom.Name = "txtDistFrom"
        Me.txtDistFrom.Size = New System.Drawing.Size(105, 21)
        Me.txtDistFrom.TabIndex = 18
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(435, 149)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 17)
        Me.Label20.TabIndex = 159
        Me.Label20.Text = "Dist From"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(96, 201)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(298, 21)
        Me.txtRemark.TabIndex = 24
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(17, 203)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(73, 17)
        Me.Label21.TabIndex = 163
        Me.Label21.Text = "Remark"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDPName
        '
        Me.txtDPName.Location = New System.Drawing.Point(290, 147)
        Me.txtDPName.Name = "txtDPName"
        Me.txtDPName.Size = New System.Drawing.Size(105, 21)
        Me.txtDPName.TabIndex = 17
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(211, 149)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(73, 17)
        Me.Label22.TabIndex = 167
        Me.Label22.Text = "DP Name"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDPId
        '
        Me.txtDPId.Location = New System.Drawing.Point(96, 147)
        Me.txtDPId.Name = "txtDPId"
        Me.txtDPId.Size = New System.Drawing.Size(105, 21)
        Me.txtDPId.TabIndex = 16
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(17, 149)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(73, 17)
        Me.Label23.TabIndex = 165
        Me.Label23.Text = "DP Id"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClientName
        '
        Me.txtClientName.Location = New System.Drawing.Point(290, 174)
        Me.txtClientName.Name = "txtClientName"
        Me.txtClientName.Size = New System.Drawing.Size(105, 21)
        Me.txtClientName.TabIndex = 21
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(207, 175)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 17)
        Me.Label24.TabIndex = 171
        Me.Label24.Text = "Client Name"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClientId
        '
        Me.txtClientId.Location = New System.Drawing.Point(96, 174)
        Me.txtClientId.Name = "txtClientId"
        Me.txtClientId.Size = New System.Drawing.Size(105, 21)
        Me.txtClientId.TabIndex = 20
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(17, 175)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(73, 17)
        Me.Label25.TabIndex = 169
        Me.Label25.Text = "Client Id"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmHistoryReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 387)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.dgvList)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmHistoryReport"
        Me.Text = "History Report"
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
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dtpInwardTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpInwardFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtHolderName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtToFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents txtToHolderName As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtpOutwardTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpOutwardFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboValid As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtOutwardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCertNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtpTranTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents dtpTranFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtTranNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtDistTo As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtDistFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtClientName As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtClientId As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtDPName As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtDPId As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
End Class
