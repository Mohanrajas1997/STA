<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInwardEntry
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInwardEntry))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cboThreshold = New System.Windows.Forms.ComboBox()
        Me.lblThreshold = New System.Windows.Forms.Label()
        Me.txtMarketValue = New System.Windows.Forms.TextBox()
        Me.lblMarketValue = New System.Windows.Forms.Label()
        Me.txtMarketPrice = New System.Windows.Forms.TextBox()
        Me.lblMarketPrice = New System.Windows.Forms.Label()
        Me.txtInwardShareCount = New System.Windows.Forms.TextBox()
        Me.lblInwardShareCount = New System.Windows.Forms.Label()
        Me.btnSearchFolio = New System.Windows.Forms.Button()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.cboDocSubType = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMailId = New System.Windows.Forms.TextBox()
        Me.txtContactNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtShareHolderName = New System.Windows.Forms.TextBox()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAwbNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboRcvdMode = New System.Windows.Forms.ComboBox()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpRcvdDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCourier = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbl2 = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lnkAddAttachment = New System.Windows.Forms.LinkLabel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.cboThreshold)
        Me.pnlMain.Controls.Add(Me.lblThreshold)
        Me.pnlMain.Controls.Add(Me.txtMarketValue)
        Me.pnlMain.Controls.Add(Me.lblMarketValue)
        Me.pnlMain.Controls.Add(Me.txtMarketPrice)
        Me.pnlMain.Controls.Add(Me.lblMarketPrice)
        Me.pnlMain.Controls.Add(Me.txtInwardShareCount)
        Me.pnlMain.Controls.Add(Me.lblInwardShareCount)
        Me.pnlMain.Controls.Add(Me.btnSearchFolio)
        Me.pnlMain.Controls.Add(Me.txtPanNo)
        Me.pnlMain.Controls.Add(Me.cboDocSubType)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.txtRemark)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.txtMailId)
        Me.pnlMain.Controls.Add(Me.txtContactNo)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.txtShareHolderName)
        Me.pnlMain.Controls.Add(Me.txtFolioNo)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.txtAwbNo)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.cboDocType)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.cboRcvdMode)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.dtpRcvdDate)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.cboCourier)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.lbl2)
        Me.pnlMain.Controls.Add(Me.txtInwardNo)
        Me.pnlMain.Controls.Add(Me.lbl1)
        Me.pnlMain.Controls.Add(Me.txtId)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(12, 9)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(970, 501)
        Me.pnlMain.TabIndex = 0
        '
        'cboThreshold
        '
        Me.cboThreshold.Enabled = False
        Me.cboThreshold.FormattingEnabled = True
        Me.cboThreshold.Items.AddRange(New Object() {"High", "Low"})
        Me.cboThreshold.Location = New System.Drawing.Point(669, 291)
        Me.cboThreshold.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboThreshold.Name = "cboThreshold"
        Me.cboThreshold.Size = New System.Drawing.Size(272, 29)
        Me.cboThreshold.TabIndex = 13
        '
        'lblThreshold
        '
        Me.lblThreshold.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblThreshold.Location = New System.Drawing.Point(513, 297)
        Me.lblThreshold.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblThreshold.Name = "lblThreshold"
        Me.lblThreshold.Size = New System.Drawing.Size(146, 20)
        Me.lblThreshold.TabIndex = 115
        Me.lblThreshold.Text = "Threshold"
        Me.lblThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMarketValue
        '
        Me.txtMarketValue.Location = New System.Drawing.Point(198, 291)
        Me.txtMarketValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMarketValue.MaxLength = 128
        Me.txtMarketValue.Name = "txtMarketValue"
        Me.txtMarketValue.ReadOnly = True
        Me.txtMarketValue.Size = New System.Drawing.Size(272, 27)
        Me.txtMarketValue.TabIndex = 12
        '
        'lblMarketValue
        '
        Me.lblMarketValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMarketValue.Location = New System.Drawing.Point(34, 294)
        Me.lblMarketValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMarketValue.Name = "lblMarketValue"
        Me.lblMarketValue.Size = New System.Drawing.Size(156, 20)
        Me.lblMarketValue.TabIndex = 113
        Me.lblMarketValue.Text = "Market Value"
        Me.lblMarketValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMarketPrice
        '
        Me.txtMarketPrice.Location = New System.Drawing.Point(669, 252)
        Me.txtMarketPrice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMarketPrice.MaxLength = 16
        Me.txtMarketPrice.Name = "txtMarketPrice"
        Me.txtMarketPrice.Size = New System.Drawing.Size(272, 27)
        Me.txtMarketPrice.TabIndex = 11
        Me.txtMarketPrice.Text = "1"
        '
        'lblMarketPrice
        '
        Me.lblMarketPrice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMarketPrice.Location = New System.Drawing.Point(498, 258)
        Me.lblMarketPrice.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMarketPrice.Name = "lblMarketPrice"
        Me.lblMarketPrice.Size = New System.Drawing.Size(161, 20)
        Me.lblMarketPrice.TabIndex = 111
        Me.lblMarketPrice.Text = "Market Price"
        Me.lblMarketPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardShareCount
        '
        Me.txtInwardShareCount.Location = New System.Drawing.Point(198, 252)
        Me.txtInwardShareCount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtInwardShareCount.MaxLength = 128
        Me.txtInwardShareCount.Name = "txtInwardShareCount"
        Me.txtInwardShareCount.ReadOnly = True
        Me.txtInwardShareCount.Size = New System.Drawing.Size(272, 27)
        Me.txtInwardShareCount.TabIndex = 10
        '
        'lblInwardShareCount
        '
        Me.lblInwardShareCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInwardShareCount.Location = New System.Drawing.Point(4, 255)
        Me.lblInwardShareCount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblInwardShareCount.Name = "lblInwardShareCount"
        Me.lblInwardShareCount.Size = New System.Drawing.Size(186, 20)
        Me.lblInwardShareCount.TabIndex = 110
        Me.lblInwardShareCount.Text = "Inward Share Count"
        Me.lblInwardShareCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSearchFolio
        '
        Me.btnSearchFolio.Location = New System.Drawing.Point(478, 213)
        Me.btnSearchFolio.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearchFolio.Name = "btnSearchFolio"
        Me.btnSearchFolio.Size = New System.Drawing.Size(51, 32)
        Me.btnSearchFolio.TabIndex = 7
        Me.btnSearchFolio.TabStop = False
        Me.btnSearchFolio.Text = "..."
        Me.btnSearchFolio.UseVisualStyleBackColor = True
        '
        'txtPanNo
        '
        Me.txtPanNo.Location = New System.Drawing.Point(669, 330)
        Me.txtPanNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPanNo.MaxLength = 16
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.ReadOnly = True
        Me.txtPanNo.Size = New System.Drawing.Size(272, 27)
        Me.txtPanNo.TabIndex = 15
        '
        'cboDocSubType
        '
        Me.cboDocSubType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboDocSubType.FormattingEnabled = True
        Me.cboDocSubType.Location = New System.Drawing.Point(198, 49)
        Me.cboDocSubType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboDocSubType.Name = "cboDocSubType"
        Me.cboDocSubType.Size = New System.Drawing.Size(743, 29)
        Me.cboDocSubType.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(0, 56)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(189, 20)
        Me.Label12.TabIndex = 88
        Me.Label12.Text = "Document Sub Type"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(543, 333)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(116, 20)
        Me.Label11.TabIndex = 99
        Me.Label11.Text = "PAN No"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(198, 411)
        Me.txtRemark.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(744, 73)
        Me.txtRemark.TabIndex = 17
        Me.txtRemark.TabStop = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(74, 417)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 20)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "&Remark"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMailId
        '
        Me.txtMailId.Location = New System.Drawing.Point(198, 370)
        Me.txtMailId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMailId.MaxLength = 128
        Me.txtMailId.Name = "txtMailId"
        Me.txtMailId.Size = New System.Drawing.Size(744, 27)
        Me.txtMailId.TabIndex = 16
        '
        'txtContactNo
        '
        Me.txtContactNo.Location = New System.Drawing.Point(198, 330)
        Me.txtContactNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtContactNo.MaxLength = 128
        Me.txtContactNo.Name = "txtContactNo"
        Me.txtContactNo.Size = New System.Drawing.Size(272, 27)
        Me.txtContactNo.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(74, 333)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(116, 20)
        Me.Label9.TabIndex = 95
        Me.Label9.Text = "Contact No"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 373)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(180, 20)
        Me.Label10.TabIndex = 97
        Me.Label10.Text = "Mail Id"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShareHolderName
        '
        Me.txtShareHolderName.Location = New System.Drawing.Point(669, 216)
        Me.txtShareHolderName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtShareHolderName.MaxLength = 64
        Me.txtShareHolderName.Name = "txtShareHolderName"
        Me.txtShareHolderName.ReadOnly = True
        Me.txtShareHolderName.Size = New System.Drawing.Size(272, 27)
        Me.txtShareHolderName.TabIndex = 9
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(198, 216)
        Me.txtFolioNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolioNo.MaxLength = 32
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.ReadOnly = True
        Me.txtFolioNo.Size = New System.Drawing.Size(272, 27)
        Me.txtFolioNo.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(74, 219)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 20)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "Folio No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAwbNo
        '
        Me.txtAwbNo.Location = New System.Drawing.Point(669, 133)
        Me.txtAwbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAwbNo.MaxLength = 32
        Me.txtAwbNo.Name = "txtAwbNo"
        Me.txtAwbNo.Size = New System.Drawing.Size(272, 27)
        Me.txtAwbNo.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(543, 136)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(116, 20)
        Me.Label6.TabIndex = 89
        Me.Label6.Text = "Awb No"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDocType
        '
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Location = New System.Drawing.Point(669, 12)
        Me.cboDocType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(272, 29)
        Me.cboDocType.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(513, 18)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(146, 20)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Document Type"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboRcvdMode
        '
        Me.cboRcvdMode.FormattingEnabled = True
        Me.cboRcvdMode.Location = New System.Drawing.Point(669, 92)
        Me.cboRcvdMode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboRcvdMode.Name = "cboRcvdMode"
        Me.cboRcvdMode.Size = New System.Drawing.Size(272, 29)
        Me.cboRcvdMode.TabIndex = 4
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(198, 175)
        Me.cboCompany.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(744, 29)
        Me.cboCompany.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(44, 178)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(146, 23)
        Me.Label3.TabIndex = 84
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpRcvdDate
        '
        Me.dtpRcvdDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpRcvdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRcvdDate.Location = New System.Drawing.Point(198, 92)
        Me.dtpRcvdDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpRcvdDate.Name = "dtpRcvdDate"
        Me.dtpRcvdDate.Size = New System.Drawing.Size(272, 27)
        Me.dtpRcvdDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(52, 95)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 20)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "Received Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCourier
        '
        Me.cboCourier.FormattingEnabled = True
        Me.cboCourier.Location = New System.Drawing.Point(198, 133)
        Me.cboCourier.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboCourier.Name = "cboCourier"
        Me.cboCourier.Size = New System.Drawing.Size(272, 29)
        Me.cboCourier.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(82, 135)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 23)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "Courier"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl2
        '
        Me.lbl2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl2.Location = New System.Drawing.Point(513, 96)
        Me.lbl2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(146, 20)
        Me.lbl2.TabIndex = 68
        Me.lbl2.Text = "Received Mode"
        Me.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Location = New System.Drawing.Point(198, 12)
        Me.txtInwardNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtInwardNo.MaxLength = 16
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(272, 27)
        Me.txtInwardNo.TabIndex = 0
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(74, 18)
        Me.lbl1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(116, 20)
        Me.lbl1.TabIndex = 66
        Me.lbl1.Text = "Inward No"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(150, 454)
        Me.txtId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(37, 27)
        Me.txtId.TabIndex = 64
        Me.txtId.Visible = False
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(518, 219)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(141, 20)
        Me.Label8.TabIndex = 93
        Me.Label8.Text = "Holder Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(646, 525)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 37)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.SystemColors.Control
        Me.btnClear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Location = New System.Drawing.Point(760, 525)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(108, 37)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lnkAddAttachment.Location = New System.Drawing.Point(490, 532)
        Me.lnkAddAttachment.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(149, 21)
        Me.lnkAddAttachment.TabIndex = 89
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(874, 525)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(108, 37)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmInwardEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1001, 572)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lnkAddAttachment)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.btnSave)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInwardEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inward Entry"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lbl2 As System.Windows.Forms.Label
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboCourier As System.Windows.Forms.ComboBox
    Friend WithEvents txtShareHolderName As System.Windows.Forms.TextBox
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAwbNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboRcvdMode As System.Windows.Forms.ComboBox
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpRcvdDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMailId As System.Windows.Forms.TextBox
    Friend WithEvents txtContactNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtPanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnSearchFolio As System.Windows.Forms.Button
    Friend WithEvents cboDocSubType As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lnkAddAttachment As System.Windows.Forms.LinkLabel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cboThreshold As System.Windows.Forms.ComboBox
    Friend WithEvents lblThreshold As System.Windows.Forms.Label
    Friend WithEvents txtMarketValue As System.Windows.Forms.TextBox
    Friend WithEvents lblMarketValue As System.Windows.Forms.Label
    Friend WithEvents txtMarketPrice As System.Windows.Forms.TextBox
    Friend WithEvents lblMarketPrice As System.Windows.Forms.Label
    Friend WithEvents txtInwardShareCount As System.Windows.Forms.TextBox
    Friend WithEvents lblInwardShareCount As System.Windows.Forms.Label
End Class
