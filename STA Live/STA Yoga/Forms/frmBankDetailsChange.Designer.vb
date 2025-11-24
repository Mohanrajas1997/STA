<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankDetailsChange
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
        Me.cboNewAccType = New System.Windows.Forms.ComboBox()
        Me.cboNewBank = New System.Windows.Forms.ComboBox()
        Me.txtNewAccNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNewIfscCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewAddr = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNewBranch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNewBeneficiary = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.grpCurrAddr = New System.Windows.Forms.GroupBox()
        Me.txtCurrAccNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCurrAccType = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCurrIfscCode = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCurrAddr = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtCurrBranch = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCurrBank = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCurrBeneficiary = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dgvChklst = New System.Windows.Forms.DataGridView()
        Me.grpHeader = New System.Windows.Forms.GroupBox()
        Me.txtShareHolder = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCompName = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lnkAddAttachment = New System.Windows.Forms.LinkLabel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblDocStatus = New System.Windows.Forms.Label()
        Me.txtNewMicrCode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCurrMicrCode = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grpPropAddr.SuspendLayout()
        Me.grpCurrAddr.SuspendLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpPropAddr
        '
        Me.grpPropAddr.Controls.Add(Me.txtNewMicrCode)
        Me.grpPropAddr.Controls.Add(Me.Label7)
        Me.grpPropAddr.Controls.Add(Me.cboNewAccType)
        Me.grpPropAddr.Controls.Add(Me.cboNewBank)
        Me.grpPropAddr.Controls.Add(Me.txtNewAccNo)
        Me.grpPropAddr.Controls.Add(Me.Label6)
        Me.grpPropAddr.Controls.Add(Me.Label5)
        Me.grpPropAddr.Controls.Add(Me.txtNewIfscCode)
        Me.grpPropAddr.Controls.Add(Me.Label4)
        Me.grpPropAddr.Controls.Add(Me.txtNewAddr)
        Me.grpPropAddr.Controls.Add(Me.Label3)
        Me.grpPropAddr.Controls.Add(Me.txtNewBranch)
        Me.grpPropAddr.Controls.Add(Me.Label2)
        Me.grpPropAddr.Controls.Add(Me.Label1)
        Me.grpPropAddr.Controls.Add(Me.txtNewBeneficiary)
        Me.grpPropAddr.Controls.Add(Me.lbl1)
        Me.grpPropAddr.Location = New System.Drawing.Point(12, 90)
        Me.grpPropAddr.Name = "grpPropAddr"
        Me.grpPropAddr.Size = New System.Drawing.Size(393, 295)
        Me.grpPropAddr.TabIndex = 1
        Me.grpPropAddr.TabStop = False
        Me.grpPropAddr.Text = "Proposed Bank Details"
        '
        'cboNewAccType
        '
        Me.cboNewAccType.FormattingEnabled = True
        Me.cboNewAccType.Location = New System.Drawing.Point(86, 209)
        Me.cboNewAccType.Name = "cboNewAccType"
        Me.cboNewAccType.Size = New System.Drawing.Size(284, 21)
        Me.cboNewAccType.TabIndex = 5
        '
        'cboNewBank
        '
        Me.cboNewBank.FormattingEnabled = True
        Me.cboNewBank.Location = New System.Drawing.Point(86, 47)
        Me.cboNewBank.Name = "cboNewBank"
        Me.cboNewBank.Size = New System.Drawing.Size(284, 21)
        Me.cboNewBank.TabIndex = 1
        '
        'txtNewAccNo
        '
        Me.txtNewAccNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewAccNo.Location = New System.Drawing.Point(86, 236)
        Me.txtNewAccNo.MaxLength = 64
        Me.txtNewAccNo.Name = "txtNewAccNo"
        Me.txtNewAccNo.Size = New System.Drawing.Size(284, 21)
        Me.txtNewAccNo.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 240)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 80
        Me.Label6.Text = "A/C No"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 213)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "A/C Type"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewIfscCode
        '
        Me.txtNewIfscCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewIfscCode.Location = New System.Drawing.Point(86, 182)
        Me.txtNewIfscCode.MaxLength = 64
        Me.txtNewIfscCode.Name = "txtNewIfscCode"
        Me.txtNewIfscCode.Size = New System.Drawing.Size(284, 21)
        Me.txtNewIfscCode.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 186)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "Ifsc Code"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewAddr
        '
        Me.txtNewAddr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewAddr.Location = New System.Drawing.Point(86, 101)
        Me.txtNewAddr.MaxLength = 128
        Me.txtNewAddr.Multiline = True
        Me.txtNewAddr.Name = "txtNewAddr"
        Me.txtNewAddr.Size = New System.Drawing.Size(284, 75)
        Me.txtNewAddr.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Address"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewBranch
        '
        Me.txtNewBranch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewBranch.Location = New System.Drawing.Point(86, 74)
        Me.txtNewBranch.MaxLength = 128
        Me.txtNewBranch.Name = "txtNewBranch"
        Me.txtNewBranch.Size = New System.Drawing.Size(284, 21)
        Me.txtNewBranch.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "Branch"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Bank"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewBeneficiary
        '
        Me.txtNewBeneficiary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewBeneficiary.Location = New System.Drawing.Point(86, 20)
        Me.txtNewBeneficiary.MaxLength = 128
        Me.txtNewBeneficiary.Name = "txtNewBeneficiary"
        Me.txtNewBeneficiary.Size = New System.Drawing.Size(284, 21)
        Me.txtNewBeneficiary.TabIndex = 0
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(6, 23)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(74, 13)
        Me.lbl1.TabIndex = 68
        Me.lbl1.Text = "Beneficiary"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpCurrAddr
        '
        Me.grpCurrAddr.Controls.Add(Me.txtCurrMicrCode)
        Me.grpCurrAddr.Controls.Add(Me.Label8)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrAccNo)
        Me.grpCurrAddr.Controls.Add(Me.Label9)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrAccType)
        Me.grpCurrAddr.Controls.Add(Me.Label10)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrIfscCode)
        Me.grpCurrAddr.Controls.Add(Me.Label11)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrAddr)
        Me.grpCurrAddr.Controls.Add(Me.Label14)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrBranch)
        Me.grpCurrAddr.Controls.Add(Me.Label15)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrBank)
        Me.grpCurrAddr.Controls.Add(Me.Label16)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrBeneficiary)
        Me.grpCurrAddr.Controls.Add(Me.Label17)
        Me.grpCurrAddr.Enabled = False
        Me.grpCurrAddr.Location = New System.Drawing.Point(411, 90)
        Me.grpCurrAddr.Name = "grpCurrAddr"
        Me.grpCurrAddr.Size = New System.Drawing.Size(393, 295)
        Me.grpCurrAddr.TabIndex = 2
        Me.grpCurrAddr.TabStop = False
        Me.grpCurrAddr.Text = "Current Bank Details"
        '
        'txtCurrAccNo
        '
        Me.txtCurrAccNo.ForeColor = System.Drawing.Color.Red
        Me.txtCurrAccNo.Location = New System.Drawing.Point(86, 236)
        Me.txtCurrAccNo.MaxLength = 64
        Me.txtCurrAccNo.Name = "txtCurrAccNo"
        Me.txtCurrAccNo.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrAccNo.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(6, 240)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 13)
        Me.Label9.TabIndex = 84
        Me.Label9.Text = "A/C No"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrAccType
        '
        Me.txtCurrAccType.ForeColor = System.Drawing.Color.Red
        Me.txtCurrAccType.Location = New System.Drawing.Point(86, 209)
        Me.txtCurrAccType.MaxLength = 64
        Me.txtCurrAccType.Name = "txtCurrAccType"
        Me.txtCurrAccType.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrAccType.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(6, 213)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 82
        Me.Label10.Text = "A/C Type"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrIfscCode
        '
        Me.txtCurrIfscCode.ForeColor = System.Drawing.Color.Red
        Me.txtCurrIfscCode.Location = New System.Drawing.Point(86, 182)
        Me.txtCurrIfscCode.MaxLength = 16
        Me.txtCurrIfscCode.Name = "txtCurrIfscCode"
        Me.txtCurrIfscCode.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrIfscCode.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(6, 186)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 13)
        Me.Label11.TabIndex = 80
        Me.Label11.Text = "Ifsc Code"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrAddr
        '
        Me.txtCurrAddr.ForeColor = System.Drawing.Color.Red
        Me.txtCurrAddr.Location = New System.Drawing.Point(86, 101)
        Me.txtCurrAddr.MaxLength = 128
        Me.txtCurrAddr.Multiline = True
        Me.txtCurrAddr.Name = "txtCurrAddr"
        Me.txtCurrAddr.Size = New System.Drawing.Size(284, 75)
        Me.txtCurrAddr.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(6, 105)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 74
        Me.Label14.Text = "Address"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrBranch
        '
        Me.txtCurrBranch.ForeColor = System.Drawing.Color.Red
        Me.txtCurrBranch.Location = New System.Drawing.Point(86, 74)
        Me.txtCurrBranch.MaxLength = 128
        Me.txtCurrBranch.Name = "txtCurrBranch"
        Me.txtCurrBranch.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrBranch.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(6, 78)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 13)
        Me.Label15.TabIndex = 72
        Me.Label15.Text = "Branch"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrBank
        '
        Me.txtCurrBank.ForeColor = System.Drawing.Color.Red
        Me.txtCurrBank.Location = New System.Drawing.Point(86, 47)
        Me.txtCurrBank.MaxLength = 128
        Me.txtCurrBank.Name = "txtCurrBank"
        Me.txtCurrBank.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrBank.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 51)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 13)
        Me.Label16.TabIndex = 70
        Me.Label16.Text = "Bank"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrBeneficiary
        '
        Me.txtCurrBeneficiary.ForeColor = System.Drawing.Color.Red
        Me.txtCurrBeneficiary.Location = New System.Drawing.Point(86, 20)
        Me.txtCurrBeneficiary.MaxLength = 128
        Me.txtCurrBeneficiary.Name = "txtCurrBeneficiary"
        Me.txtCurrBeneficiary.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrBeneficiary.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(6, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(74, 13)
        Me.Label17.TabIndex = 68
        Me.Label17.Text = "Beneficiary"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvChklst
        '
        Me.dgvChklst.AllowUserToAddRows = False
        Me.dgvChklst.AllowUserToDeleteRows = False
        Me.dgvChklst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChklst.Location = New System.Drawing.Point(12, 391)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(394, 154)
        Me.dgvChklst.TabIndex = 3
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.txtShareHolder)
        Me.grpHeader.Controls.Add(Me.Label21)
        Me.grpHeader.Controls.Add(Me.txtFolioNo)
        Me.grpHeader.Controls.Add(Me.Label22)
        Me.grpHeader.Controls.Add(Me.txtCompName)
        Me.grpHeader.Controls.Add(Me.Label20)
        Me.grpHeader.Controls.Add(Me.txtInwardNo)
        Me.grpHeader.Controls.Add(Me.Label19)
        Me.grpHeader.Enabled = False
        Me.grpHeader.Location = New System.Drawing.Point(13, 0)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(790, 84)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'txtShareHolder
        '
        Me.txtShareHolder.Location = New System.Drawing.Point(484, 47)
        Me.txtShareHolder.MaxLength = 0
        Me.txtShareHolder.Name = "txtShareHolder"
        Me.txtShareHolder.Size = New System.Drawing.Size(284, 21)
        Me.txtShareHolder.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(389, 50)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(89, 13)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "Share Holder"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(85, 47)
        Me.txtFolioNo.MaxLength = 0
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(284, 21)
        Me.txtFolioNo.TabIndex = 2
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(5, 50)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(74, 13)
        Me.Label22.TabIndex = 74
        Me.Label22.Text = "Folio No"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCompName
        '
        Me.txtCompName.Location = New System.Drawing.Point(484, 20)
        Me.txtCompName.MaxLength = 0
        Me.txtCompName.Name = "txtCompName"
        Me.txtCompName.Size = New System.Drawing.Size(284, 21)
        Me.txtCompName.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(404, 23)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 72
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Location = New System.Drawing.Point(85, 20)
        Me.txtInwardNo.MaxLength = 0
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(284, 21)
        Me.txtInwardNo.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(5, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 70
        Me.Label19.Text = "Inward No"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(497, 391)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(306, 88)
        Me.txtRemark.TabIndex = 5
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(417, 395)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 13)
        Me.Label23.TabIndex = 4
        Me.Label23.Text = "Remark"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(485, 521)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 9
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(567, 521)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 10
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(649, 521)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 11
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(731, 521)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(703, 496)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(100, 13)
        Me.lnkAddAttachment.TabIndex = 8
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(417, 496)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(497, 496)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 7
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNewMicrCode
        '
        Me.txtNewMicrCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewMicrCode.Location = New System.Drawing.Point(86, 263)
        Me.txtNewMicrCode.MaxLength = 16
        Me.txtNewMicrCode.Name = "txtNewMicrCode"
        Me.txtNewMicrCode.Size = New System.Drawing.Size(284, 21)
        Me.txtNewMicrCode.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(6, 267)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 82
        Me.Label7.Text = "Micr Code"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrMicrCode
        '
        Me.txtCurrMicrCode.ForeColor = System.Drawing.Color.Red
        Me.txtCurrMicrCode.Location = New System.Drawing.Point(86, 264)
        Me.txtCurrMicrCode.MaxLength = 16
        Me.txtCurrMicrCode.Name = "txtCurrMicrCode"
        Me.txtCurrMicrCode.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrMicrCode.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 268)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 86
        Me.Label8.Text = "Micr Code"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmBankDetailsChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 562)
        Me.Controls.Add(Me.lblDocStatus)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.lnkAddAttachment)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.grpHeader)
        Me.Controls.Add(Me.dgvChklst)
        Me.Controls.Add(Me.grpCurrAddr)
        Me.Controls.Add(Me.grpPropAddr)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBankDetailsChange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bank Details Change"
        Me.grpPropAddr.ResumeLayout(False)
        Me.grpPropAddr.PerformLayout()
        Me.grpCurrAddr.ResumeLayout(False)
        Me.grpCurrAddr.PerformLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpPropAddr As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewAccNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNewIfscCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNewAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNewBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNewBeneficiary As System.Windows.Forms.TextBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents grpCurrAddr As System.Windows.Forms.GroupBox
    Friend WithEvents txtCurrAccNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCurrAccType As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCurrIfscCode As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCurrAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtCurrBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCurrBank As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCurrBeneficiary As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
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
    Friend WithEvents cboNewAccType As System.Windows.Forms.ComboBox
    Friend WithEvents cboNewBank As System.Windows.Forms.ComboBox
    Friend WithEvents txtNewMicrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCurrMicrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
