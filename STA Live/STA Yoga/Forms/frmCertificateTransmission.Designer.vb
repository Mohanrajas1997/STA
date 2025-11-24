<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCertificateTransmission
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
        Me.grpPropAddr = New System.Windows.Forms.GroupBox()
        Me.lblDocStatus = New System.Windows.Forms.Label()
        Me.txtPincode = New System.Windows.Forms.TextBox()
        Me.lnkAddAttachment = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAddr3 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAddr2 = New System.Windows.Forms.TextBox()
        Me.dgvChklst = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAddr1 = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.grpHeader = New System.Windows.Forms.GroupBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblShareCount = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dgvCert = New System.Windows.Forms.DataGridView()
        Me.txtShareHolder = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCompName = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpName = New System.Windows.Forms.GroupBox()
        Me.btnCreateNewFolio = New System.Windows.Forms.Button()
        Me.txtNewFolioNo = New System.Windows.Forms.TextBox()
        Me.txtNewFolioId = New System.Windows.Forms.TextBox()
        Me.txtPanNo3 = New System.Windows.Forms.TextBox()
        Me.txtPanNo2 = New System.Windows.Forms.TextBox()
        Me.txtPanNo1 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtFHName3 = New System.Windows.Forms.TextBox()
        Me.txtFHName2 = New System.Windows.Forms.TextBox()
        Me.txtFHName1 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnSearchFolio = New System.Windows.Forms.Button()
        Me.txtHolder3 = New System.Windows.Forms.TextBox()
        Me.txtHolder2 = New System.Windows.Forms.TextBox()
        Me.txtHolder1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnUpdateFolioPan = New System.Windows.Forms.Button()
        Me.grpPropAddr.SuspendLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpName.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpPropAddr
        '
        Me.grpPropAddr.Controls.Add(Me.lblDocStatus)
        Me.grpPropAddr.Controls.Add(Me.txtPincode)
        Me.grpPropAddr.Controls.Add(Me.lnkAddAttachment)
        Me.grpPropAddr.Controls.Add(Me.Label6)
        Me.grpPropAddr.Controls.Add(Me.txtCountry)
        Me.grpPropAddr.Controls.Add(Me.Label5)
        Me.grpPropAddr.Controls.Add(Me.txtState)
        Me.grpPropAddr.Controls.Add(Me.Label4)
        Me.grpPropAddr.Controls.Add(Me.txtRemark)
        Me.grpPropAddr.Controls.Add(Me.txtCity)
        Me.grpPropAddr.Controls.Add(Me.Label3)
        Me.grpPropAddr.Controls.Add(Me.txtAddr3)
        Me.grpPropAddr.Controls.Add(Me.Label2)
        Me.grpPropAddr.Controls.Add(Me.txtAddr2)
        Me.grpPropAddr.Controls.Add(Me.dgvChklst)
        Me.grpPropAddr.Controls.Add(Me.Label1)
        Me.grpPropAddr.Controls.Add(Me.txtAddr1)
        Me.grpPropAddr.Controls.Add(Me.lbl1)
        Me.grpPropAddr.Controls.Add(Me.Label18)
        Me.grpPropAddr.Controls.Add(Me.Label23)
        Me.grpPropAddr.Location = New System.Drawing.Point(12, 296)
        Me.grpPropAddr.Name = "grpPropAddr"
        Me.grpPropAddr.Size = New System.Drawing.Size(1190, 218)
        Me.grpPropAddr.TabIndex = 2
        Me.grpPropAddr.TabStop = False
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(864, 186)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 14
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPincode
        '
        Me.txtPincode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPincode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPincode.Location = New System.Drawing.Point(103, 182)
        Me.txtPincode.MaxLength = 16
        Me.txtPincode.Name = "txtPincode"
        Me.txtPincode.ReadOnly = True
        Me.txtPincode.Size = New System.Drawing.Size(284, 21)
        Me.txtPincode.TabIndex = 6
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(1070, 186)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(100, 13)
        Me.lnkAddAttachment.TabIndex = 15
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(23, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 80
        Me.Label6.Text = "Pincode"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCountry
        '
        Me.txtCountry.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCountry.Location = New System.Drawing.Point(103, 155)
        Me.txtCountry.MaxLength = 64
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.ReadOnly = True
        Me.txtCountry.Size = New System.Drawing.Size(284, 21)
        Me.txtCountry.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(23, 159)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "Country"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtState
        '
        Me.txtState.BackColor = System.Drawing.SystemColors.Window
        Me.txtState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtState.Location = New System.Drawing.Point(103, 128)
        Me.txtState.MaxLength = 64
        Me.txtState.Name = "txtState"
        Me.txtState.ReadOnly = True
        Me.txtState.Size = New System.Drawing.Size(284, 21)
        Me.txtState.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(23, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "State"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(862, 20)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(308, 156)
        Me.txtRemark.TabIndex = 11
        '
        'txtCity
        '
        Me.txtCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCity.Location = New System.Drawing.Point(103, 101)
        Me.txtCity.MaxLength = 64
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.Size = New System.Drawing.Size(284, 21)
        Me.txtCity.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(23, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "City"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAddr3
        '
        Me.txtAddr3.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAddr3.Location = New System.Drawing.Point(103, 74)
        Me.txtAddr3.MaxLength = 64
        Me.txtAddr3.Name = "txtAddr3"
        Me.txtAddr3.ReadOnly = True
        Me.txtAddr3.Size = New System.Drawing.Size(284, 21)
        Me.txtAddr3.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(23, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "Address3"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAddr2
        '
        Me.txtAddr2.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAddr2.Location = New System.Drawing.Point(103, 47)
        Me.txtAddr2.MaxLength = 64
        Me.txtAddr2.Name = "txtAddr2"
        Me.txtAddr2.ReadOnly = True
        Me.txtAddr2.Size = New System.Drawing.Size(284, 21)
        Me.txtAddr2.TabIndex = 1
        '
        'dgvChklst
        '
        Me.dgvChklst.AllowUserToAddRows = False
        Me.dgvChklst.AllowUserToDeleteRows = False
        Me.dgvChklst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChklst.Location = New System.Drawing.Point(394, 20)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(373, 183)
        Me.dgvChklst.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(23, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Address2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAddr1
        '
        Me.txtAddr1.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAddr1.Location = New System.Drawing.Point(103, 20)
        Me.txtAddr1.MaxLength = 64
        Me.txtAddr1.Name = "txtAddr1"
        Me.txtAddr1.ReadOnly = True
        Me.txtAddr1.Size = New System.Drawing.Size(284, 21)
        Me.txtAddr1.TabIndex = 0
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(23, 23)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(74, 13)
        Me.lbl1.TabIndex = 68
        Me.lbl1.Text = "Address1"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(784, 186)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(782, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 13)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Remark"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.btnUpdateFolioPan)
        Me.grpHeader.Controls.Add(Me.lblTotal)
        Me.grpHeader.Controls.Add(Me.Label27)
        Me.grpHeader.Controls.Add(Me.lblShareCount)
        Me.grpHeader.Controls.Add(Me.Label28)
        Me.grpHeader.Controls.Add(Me.txtCertNo)
        Me.grpHeader.Controls.Add(Me.Label26)
        Me.grpHeader.Controls.Add(Me.txtPanNo)
        Me.grpHeader.Controls.Add(Me.Label9)
        Me.grpHeader.Controls.Add(Me.dgvCert)
        Me.grpHeader.Controls.Add(Me.txtShareHolder)
        Me.grpHeader.Controls.Add(Me.Label21)
        Me.grpHeader.Controls.Add(Me.txtFolioNo)
        Me.grpHeader.Controls.Add(Me.Label22)
        Me.grpHeader.Controls.Add(Me.txtCompName)
        Me.grpHeader.Controls.Add(Me.Label20)
        Me.grpHeader.Controls.Add(Me.txtInwardNo)
        Me.grpHeader.Controls.Add(Me.Label19)
        Me.grpHeader.Location = New System.Drawing.Point(13, 0)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(1190, 158)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'lblTotal
        '
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(851, 23)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(74, 13)
        Me.lblTotal.TabIndex = 91
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(771, 23)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(74, 13)
        Me.Label27.TabIndex = 90
        Me.Label27.Text = "Total : "
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblShareCount
        '
        Me.lblShareCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShareCount.Location = New System.Drawing.Point(1097, 23)
        Me.lblShareCount.Name = "lblShareCount"
        Me.lblShareCount.Size = New System.Drawing.Size(74, 13)
        Me.lblShareCount.TabIndex = 89
        Me.lblShareCount.Text = "0"
        Me.lblShareCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(1017, 23)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(74, 13)
        Me.Label28.TabIndex = 88
        Me.Label28.Text = "Selected : "
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertNo
        '
        Me.txtCertNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertNo.Location = New System.Drawing.Point(460, 20)
        Me.txtCertNo.MaxLength = 0
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.Size = New System.Drawing.Size(196, 21)
        Me.txtCertNo.TabIndex = 87
        '
        'Label26
        '
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(390, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(74, 13)
        Me.Label26.TabIndex = 86
        Me.Label26.Text = "Certificate"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPanNo
        '
        Me.txtPanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo.Location = New System.Drawing.Point(101, 128)
        Me.txtPanNo.MaxLength = 0
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.ReadOnly = True
        Me.txtPanNo.Size = New System.Drawing.Size(198, 21)
        Me.txtPanNo.TabIndex = 3
        Me.txtPanNo.TabStop = False
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(6, 131)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 81
        Me.Label9.Text = "PAN No"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvCert
        '
        Me.dgvCert.AllowUserToAddRows = False
        Me.dgvCert.AllowUserToDeleteRows = False
        Me.dgvCert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCert.Location = New System.Drawing.Point(393, 47)
        Me.dgvCert.Name = "dgvCert"
        Me.dgvCert.Size = New System.Drawing.Size(780, 102)
        Me.dgvCert.TabIndex = 6
        '
        'txtShareHolder
        '
        Me.txtShareHolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtShareHolder.Location = New System.Drawing.Point(101, 101)
        Me.txtShareHolder.MaxLength = 0
        Me.txtShareHolder.Name = "txtShareHolder"
        Me.txtShareHolder.ReadOnly = True
        Me.txtShareHolder.Size = New System.Drawing.Size(284, 21)
        Me.txtShareHolder.TabIndex = 3
        Me.txtShareHolder.TabStop = False
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(6, 104)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(89, 13)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "Share Holder"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioNo
        '
        Me.txtFolioNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtFolioNo.Location = New System.Drawing.Point(101, 74)
        Me.txtFolioNo.MaxLength = 0
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.ReadOnly = True
        Me.txtFolioNo.Size = New System.Drawing.Size(284, 21)
        Me.txtFolioNo.TabIndex = 2
        Me.txtFolioNo.TabStop = False
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(21, 77)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(74, 13)
        Me.Label22.TabIndex = 74
        Me.Label22.Text = "Folio No"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCompName
        '
        Me.txtCompName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompName.Location = New System.Drawing.Point(101, 47)
        Me.txtCompName.MaxLength = 0
        Me.txtCompName.Name = "txtCompName"
        Me.txtCompName.ReadOnly = True
        Me.txtCompName.Size = New System.Drawing.Size(284, 21)
        Me.txtCompName.TabIndex = 1
        Me.txtCompName.TabStop = False
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(21, 50)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 72
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInwardNo.Location = New System.Drawing.Point(101, 20)
        Me.txtInwardNo.MaxLength = 0
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.ReadOnly = True
        Me.txtInwardNo.Size = New System.Drawing.Size(284, 21)
        Me.txtInwardNo.TabIndex = 0
        Me.txtInwardNo.TabStop = False
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(21, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 70
        Me.Label19.Text = "Inward No"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(874, 520)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 3
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(956, 520)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 4
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(1038, 520)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 5
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1120, 520)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpName
        '
        Me.grpName.Controls.Add(Me.btnCreateNewFolio)
        Me.grpName.Controls.Add(Me.txtNewFolioNo)
        Me.grpName.Controls.Add(Me.txtNewFolioId)
        Me.grpName.Controls.Add(Me.txtPanNo3)
        Me.grpName.Controls.Add(Me.txtPanNo2)
        Me.grpName.Controls.Add(Me.txtPanNo1)
        Me.grpName.Controls.Add(Me.Label15)
        Me.grpName.Controls.Add(Me.txtFHName3)
        Me.grpName.Controls.Add(Me.txtFHName2)
        Me.grpName.Controls.Add(Me.txtFHName1)
        Me.grpName.Controls.Add(Me.Label12)
        Me.grpName.Controls.Add(Me.btnSearchFolio)
        Me.grpName.Controls.Add(Me.txtHolder3)
        Me.grpName.Controls.Add(Me.txtHolder2)
        Me.grpName.Controls.Add(Me.txtHolder1)
        Me.grpName.Controls.Add(Me.Label8)
        Me.grpName.Controls.Add(Me.Label7)
        Me.grpName.Location = New System.Drawing.Point(12, 164)
        Me.grpName.Name = "grpName"
        Me.grpName.Size = New System.Drawing.Size(1190, 126)
        Me.grpName.TabIndex = 1
        Me.grpName.TabStop = False
        '
        'btnCreateNewFolio
        '
        Me.btnCreateNewFolio.Location = New System.Drawing.Point(501, 16)
        Me.btnCreateNewFolio.Name = "btnCreateNewFolio"
        Me.btnCreateNewFolio.Size = New System.Drawing.Size(114, 21)
        Me.btnCreateNewFolio.TabIndex = 107
        Me.btnCreateNewFolio.Text = "Create New Folio"
        Me.btnCreateNewFolio.UseVisualStyleBackColor = True
        '
        'txtNewFolioNo
        '
        Me.txtNewFolioNo.Location = New System.Drawing.Point(103, 16)
        Me.txtNewFolioNo.MaxLength = 0
        Me.txtNewFolioNo.Name = "txtNewFolioNo"
        Me.txtNewFolioNo.Size = New System.Drawing.Size(352, 21)
        Me.txtNewFolioNo.TabIndex = 0
        '
        'txtNewFolioId
        '
        Me.txtNewFolioId.Location = New System.Drawing.Point(658, 16)
        Me.txtNewFolioId.MaxLength = 0
        Me.txtNewFolioId.Name = "txtNewFolioId"
        Me.txtNewFolioId.Size = New System.Drawing.Size(45, 21)
        Me.txtNewFolioId.TabIndex = 104
        Me.txtNewFolioId.Visible = False
        '
        'txtPanNo3
        '
        Me.txtPanNo3.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo3.Location = New System.Drawing.Point(819, 96)
        Me.txtPanNo3.MaxLength = 0
        Me.txtPanNo3.Name = "txtPanNo3"
        Me.txtPanNo3.ReadOnly = True
        Me.txtPanNo3.Size = New System.Drawing.Size(352, 21)
        Me.txtPanNo3.TabIndex = 12
        '
        'txtPanNo2
        '
        Me.txtPanNo2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo2.Location = New System.Drawing.Point(461, 96)
        Me.txtPanNo2.MaxLength = 0
        Me.txtPanNo2.Name = "txtPanNo2"
        Me.txtPanNo2.ReadOnly = True
        Me.txtPanNo2.Size = New System.Drawing.Size(352, 21)
        Me.txtPanNo2.TabIndex = 9
        '
        'txtPanNo1
        '
        Me.txtPanNo1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo1.Location = New System.Drawing.Point(103, 96)
        Me.txtPanNo1.MaxLength = 0
        Me.txtPanNo1.Name = "txtPanNo1"
        Me.txtPanNo1.ReadOnly = True
        Me.txtPanNo1.Size = New System.Drawing.Size(352, 21)
        Me.txtPanNo1.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(6, 99)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(91, 13)
        Me.Label15.TabIndex = 89
        Me.Label15.Text = "PAN No"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFHName3
        '
        Me.txtFHName3.BackColor = System.Drawing.SystemColors.Window
        Me.txtFHName3.Location = New System.Drawing.Point(819, 69)
        Me.txtFHName3.MaxLength = 0
        Me.txtFHName3.Name = "txtFHName3"
        Me.txtFHName3.ReadOnly = True
        Me.txtFHName3.Size = New System.Drawing.Size(352, 21)
        Me.txtFHName3.TabIndex = 11
        '
        'txtFHName2
        '
        Me.txtFHName2.BackColor = System.Drawing.SystemColors.Window
        Me.txtFHName2.Location = New System.Drawing.Point(461, 69)
        Me.txtFHName2.MaxLength = 0
        Me.txtFHName2.Name = "txtFHName2"
        Me.txtFHName2.ReadOnly = True
        Me.txtFHName2.Size = New System.Drawing.Size(352, 21)
        Me.txtFHName2.TabIndex = 8
        '
        'txtFHName1
        '
        Me.txtFHName1.BackColor = System.Drawing.SystemColors.Window
        Me.txtFHName1.Location = New System.Drawing.Point(103, 69)
        Me.txtFHName1.MaxLength = 0
        Me.txtFHName1.Name = "txtFHName1"
        Me.txtFHName1.ReadOnly = True
        Me.txtFHName1.Size = New System.Drawing.Size(352, 21)
        Me.txtFHName1.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(6, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 13)
        Me.Label12.TabIndex = 82
        Me.Label12.Text = "F/H Name"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSearchFolio
        '
        Me.btnSearchFolio.Location = New System.Drawing.Point(461, 16)
        Me.btnSearchFolio.Name = "btnSearchFolio"
        Me.btnSearchFolio.Size = New System.Drawing.Size(34, 21)
        Me.btnSearchFolio.TabIndex = 1
        Me.btnSearchFolio.Text = "..."
        Me.btnSearchFolio.UseVisualStyleBackColor = True
        '
        'txtHolder3
        '
        Me.txtHolder3.BackColor = System.Drawing.SystemColors.Window
        Me.txtHolder3.Location = New System.Drawing.Point(819, 42)
        Me.txtHolder3.MaxLength = 0
        Me.txtHolder3.Name = "txtHolder3"
        Me.txtHolder3.ReadOnly = True
        Me.txtHolder3.Size = New System.Drawing.Size(352, 21)
        Me.txtHolder3.TabIndex = 10
        '
        'txtHolder2
        '
        Me.txtHolder2.BackColor = System.Drawing.SystemColors.Window
        Me.txtHolder2.Location = New System.Drawing.Point(461, 42)
        Me.txtHolder2.MaxLength = 0
        Me.txtHolder2.Name = "txtHolder2"
        Me.txtHolder2.ReadOnly = True
        Me.txtHolder2.Size = New System.Drawing.Size(352, 21)
        Me.txtHolder2.TabIndex = 7
        '
        'txtHolder1
        '
        Me.txtHolder1.BackColor = System.Drawing.SystemColors.Window
        Me.txtHolder1.Location = New System.Drawing.Point(103, 42)
        Me.txtHolder1.MaxLength = 0
        Me.txtHolder1.Name = "txtHolder1"
        Me.txtHolder1.ReadOnly = True
        Me.txtHolder1.Size = New System.Drawing.Size(352, 21)
        Me.txtHolder1.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 13)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "Holder Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(-9, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 106
        Me.Label7.Text = "New Folio No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnUpdateFolioPan
        '
        Me.btnUpdateFolioPan.Location = New System.Drawing.Point(305, 128)
        Me.btnUpdateFolioPan.Name = "btnUpdateFolioPan"
        Me.btnUpdateFolioPan.Size = New System.Drawing.Size(80, 21)
        Me.btnUpdateFolioPan.TabIndex = 3
        Me.btnUpdateFolioPan.TabStop = False
        Me.btnUpdateFolioPan.Text = "Set PAN"
        Me.btnUpdateFolioPan.UseVisualStyleBackColor = True
        '
        'frmCertificateTransmission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 550)
        Me.Controls.Add(Me.grpName)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.grpHeader)
        Me.Controls.Add(Me.grpPropAddr)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCertificateTransmission"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Certificate Transmission"
        Me.grpPropAddr.ResumeLayout(False)
        Me.grpPropAddr.PerformLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpName.ResumeLayout(False)
        Me.grpName.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpPropAddr As System.Windows.Forms.GroupBox
    Friend WithEvents txtPincode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAddr3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAddr2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAddr1 As System.Windows.Forms.TextBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents dgvChklst As System.Windows.Forms.DataGridView
    Friend WithEvents grpHeader As System.Windows.Forms.GroupBox
    Friend WithEvents txtShareHolder As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtCompName As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents btnReject As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lnkAddAttachment As System.Windows.Forms.LinkLabel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblDocStatus As System.Windows.Forms.Label
    Friend WithEvents grpName As System.Windows.Forms.GroupBox
    Friend WithEvents txtHolder3 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder2 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPanNo3 As System.Windows.Forms.TextBox
    Friend WithEvents txtPanNo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPanNo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFHName3 As System.Windows.Forms.TextBox
    Friend WithEvents txtFHName2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFHName1 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnSearchFolio As System.Windows.Forms.Button
    Friend WithEvents dgvCert As System.Windows.Forms.DataGridView
    Friend WithEvents txtNewFolioId As System.Windows.Forms.TextBox
    Friend WithEvents txtNewFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnCreateNewFolio As System.Windows.Forms.Button
    Friend WithEvents txtPanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblShareCount As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtCertNo As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateFolioPan As System.Windows.Forms.Button
End Class
