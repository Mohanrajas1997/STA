<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCAPhysicalToIEPF
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
        Me.txtCreditLockin = New System.Windows.Forms.TextBox()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.txtDebitLockin = New System.Windows.Forms.TextBox()
        Me.lblCreditLockin = New System.Windows.Forms.Label()
        Me.lblDebitLockin = New System.Windows.Forms.Label()
        Me.dtpExecDate = New System.Windows.Forms.DateTimePicker()
        Me.lblAllotmentDate = New System.Windows.Forms.Label()
        Me.dtpApprovalDate = New System.Windows.Forms.DateTimePicker()
        Me.cbodivFinyear = New System.Windows.Forms.ComboBox()
        Me.lblStampDuty = New System.Windows.Forms.Label()
        Me.txtCreditQty = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDebitQty = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblDebitQty = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboCAtype = New System.Windows.Forms.ComboBox()
        Me.txtCompName = New System.Windows.Forms.TextBox()
        Me.lblCAtype = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtCINno = New System.Windows.Forms.TextBox()
        Me.txtShareHolder = New System.Windows.Forms.TextBox()
        Me.lblCinNo = New System.Windows.Forms.Label()
        Me.txtSharesCount = New System.Windows.Forms.TextBox()
        Me.lblCreditQty = New System.Windows.Forms.Label()
        Me.lblExecutionDate = New System.Windows.Forms.Label()
        Me.grpHeader = New System.Windows.Forms.GroupBox()
        Me.grpPhyysicalIEPFhdr = New System.Windows.Forms.GroupBox()
        Me.txtCompEmailId = New System.Windows.Forms.TextBox()
        Me.lblCompEmailId = New System.Windows.Forms.Label()
        Me.txtRtarefNo = New System.Windows.Forms.TextBox()
        Me.lblRtarefNo = New System.Windows.Forms.Label()
        Me.btnCleardgvrec2 = New System.Windows.Forms.Button()
        Me.lblProgressbar = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lblChooseFile = New System.Windows.Forms.Label()
        Me.DisplayTotrecords = New System.Windows.Forms.Label()
        Me.lbltotalrecords = New System.Windows.Forms.Label()
        Me.displayTotshare = New System.Windows.Forms.Label()
        Me.txtIsinId = New System.Windows.Forms.TextBox()
        Me.dgvRecord2 = New System.Windows.Forms.DataGridView()
        Me.dp_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clientid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.foliono = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dist_from = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dist_to = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.credit_qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.debit_qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.holder1_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.creditqty_lockin_reasoncode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.creditqty_lockin_releasedate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.debitqty_lockin_reasoncode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.debitqty_lockin_releasedate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.investor_category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bo_correspondence_addr1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bo_correspondence_addr2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bo_correspondence_addr3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bo_correspondence_city = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bo_correspondence_state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bo_correspondence_country = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bo_correspondence_pincode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.action = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.lbltotalShares = New System.Windows.Forms.Label()
        Me.lnkAddAttachment = New System.Windows.Forms.LinkLabel()
        Me.lblDocStatus = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ofdInput = New System.Windows.Forms.OpenFileDialog()
        Me.btnView = New System.Windows.Forms.Button()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.dgvChklst = New System.Windows.Forms.DataGridView()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.grpHeader.SuspendLayout()
        Me.grpPhyysicalIEPFhdr.SuspendLayout()
        CType(Me.dgvRecord2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCreditLockin
        '
        Me.txtCreditLockin.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditLockin.Location = New System.Drawing.Point(110, 314)
        Me.txtCreditLockin.MaxLength = 0
        Me.txtCreditLockin.Name = "txtCreditLockin"
        Me.txtCreditLockin.Size = New System.Drawing.Size(140, 27)
        Me.txtCreditLockin.TabIndex = 14
        '
        'txtFolioNo
        '
        Me.txtFolioNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtFolioNo.Enabled = False
        Me.txtFolioNo.Location = New System.Drawing.Point(111, 65)
        Me.txtFolioNo.MaxLength = 0
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.ReadOnly = True
        Me.txtFolioNo.Size = New System.Drawing.Size(372, 27)
        Me.txtFolioNo.TabIndex = 2
        Me.txtFolioNo.TabStop = False
        '
        'txtDebitLockin
        '
        Me.txtDebitLockin.BackColor = System.Drawing.SystemColors.Window
        Me.txtDebitLockin.Location = New System.Drawing.Point(345, 314)
        Me.txtDebitLockin.MaxLength = 0
        Me.txtDebitLockin.Name = "txtDebitLockin"
        Me.txtDebitLockin.Size = New System.Drawing.Size(140, 27)
        Me.txtDebitLockin.TabIndex = 15
        '
        'lblCreditLockin
        '
        Me.lblCreditLockin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCreditLockin.Location = New System.Drawing.Point(16, 321)
        Me.lblCreditLockin.Name = "lblCreditLockin"
        Me.lblCreditLockin.Size = New System.Drawing.Size(87, 13)
        Me.lblCreditLockin.TabIndex = 168
        Me.lblCreditLockin.Text = "Credit Lockin"
        Me.lblCreditLockin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDebitLockin
        '
        Me.lblDebitLockin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDebitLockin.Location = New System.Drawing.Point(246, 319)
        Me.lblDebitLockin.Name = "lblDebitLockin"
        Me.lblDebitLockin.Size = New System.Drawing.Size(89, 13)
        Me.lblDebitLockin.TabIndex = 167
        Me.lblDebitLockin.Text = "Debit Lockin"
        Me.lblDebitLockin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpExecDate
        '
        Me.dtpExecDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpExecDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExecDate.Location = New System.Drawing.Point(343, 228)
        Me.dtpExecDate.Name = "dtpExecDate"
        Me.dtpExecDate.Size = New System.Drawing.Size(140, 27)
        Me.dtpExecDate.TabIndex = 9
        Me.dtpExecDate.Value = New Date(2024, 6, 11, 0, 0, 0, 0)
        '
        'lblAllotmentDate
        '
        Me.lblAllotmentDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAllotmentDate.Location = New System.Drawing.Point(7, 237)
        Me.lblAllotmentDate.Name = "lblAllotmentDate"
        Me.lblAllotmentDate.Size = New System.Drawing.Size(94, 13)
        Me.lblAllotmentDate.TabIndex = 163
        Me.lblAllotmentDate.Text = "Approval Date"
        Me.lblAllotmentDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpApprovalDate
        '
        Me.dtpApprovalDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpApprovalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApprovalDate.Location = New System.Drawing.Point(111, 228)
        Me.dtpApprovalDate.Name = "dtpApprovalDate"
        Me.dtpApprovalDate.Size = New System.Drawing.Size(140, 27)
        Me.dtpApprovalDate.TabIndex = 8
        Me.dtpApprovalDate.Value = New Date(2024, 6, 11, 0, 0, 0, 0)
        '
        'cbodivFinyear
        '
        Me.cbodivFinyear.FormattingEnabled = True
        Me.cbodivFinyear.Location = New System.Drawing.Point(110, 257)
        Me.cbodivFinyear.Name = "cbodivFinyear"
        Me.cbodivFinyear.Size = New System.Drawing.Size(140, 29)
        Me.cbodivFinyear.TabIndex = 10
        '
        'lblStampDuty
        '
        Me.lblStampDuty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStampDuty.Location = New System.Drawing.Point(-2, 263)
        Me.lblStampDuty.Name = "lblStampDuty"
        Me.lblStampDuty.Size = New System.Drawing.Size(103, 13)
        Me.lblStampDuty.TabIndex = 166
        Me.lblStampDuty.Text = "Div Finyear"
        Me.lblStampDuty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCreditQty
        '
        Me.txtCreditQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditQty.Location = New System.Drawing.Point(111, 285)
        Me.txtCreditQty.MaxLength = 0
        Me.txtCreditQty.Name = "txtCreditQty"
        Me.txtCreditQty.ReadOnly = True
        Me.txtCreditQty.Size = New System.Drawing.Size(140, 27)
        Me.txtCreditQty.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(248, 264)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 158
        Me.Label5.Text = "Shares"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDebitQty
        '
        Me.txtDebitQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtDebitQty.Location = New System.Drawing.Point(345, 285)
        Me.txtDebitQty.MaxLength = 0
        Me.txtDebitQty.Name = "txtDebitQty"
        Me.txtDebitQty.ReadOnly = True
        Me.txtDebitQty.Size = New System.Drawing.Size(140, 27)
        Me.txtDebitQty.TabIndex = 13
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(31, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 153
        Me.Label19.Text = "Inward No"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDebitQty
        '
        Me.lblDebitQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDebitQty.Location = New System.Drawing.Point(246, 292)
        Me.lblDebitQty.Name = "lblDebitQty"
        Me.lblDebitQty.Size = New System.Drawing.Size(89, 13)
        Me.lblDebitQty.TabIndex = 164
        Me.lblDebitQty.Text = "Debit Qty"
        Me.lblDebitQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInwardNo.Enabled = False
        Me.txtInwardNo.Location = New System.Drawing.Point(111, 11)
        Me.txtInwardNo.MaxLength = 0
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.ReadOnly = True
        Me.txtInwardNo.Size = New System.Drawing.Size(372, 27)
        Me.txtInwardNo.TabIndex = 0
        Me.txtInwardNo.TabStop = False
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(31, 45)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 154
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCAtype
        '
        Me.cboCAtype.FormattingEnabled = True
        Me.cboCAtype.Location = New System.Drawing.Point(111, 199)
        Me.cboCAtype.Name = "cboCAtype"
        Me.cboCAtype.Size = New System.Drawing.Size(372, 29)
        Me.cboCAtype.TabIndex = 7
        '
        'txtCompName
        '
        Me.txtCompName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompName.Enabled = False
        Me.txtCompName.Location = New System.Drawing.Point(111, 38)
        Me.txtCompName.MaxLength = 0
        Me.txtCompName.Name = "txtCompName"
        Me.txtCompName.ReadOnly = True
        Me.txtCompName.Size = New System.Drawing.Size(372, 27)
        Me.txtCompName.TabIndex = 1
        Me.txtCompName.TabStop = False
        '
        'lblCAtype
        '
        Me.lblCAtype.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAtype.Location = New System.Drawing.Point(16, 209)
        Me.lblCAtype.Name = "lblCAtype"
        Me.lblCAtype.Size = New System.Drawing.Size(85, 13)
        Me.lblCAtype.TabIndex = 161
        Me.lblCAtype.Text = "CA Type"
        Me.lblCAtype.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(31, 72)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(74, 13)
        Me.Label22.TabIndex = 155
        Me.Label22.Text = "Folio No"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(16, 99)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(89, 13)
        Me.Label21.TabIndex = 156
        Me.Label21.Text = "Share Holder"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCINno
        '
        Me.txtCINno.BackColor = System.Drawing.SystemColors.Window
        Me.txtCINno.Location = New System.Drawing.Point(111, 145)
        Me.txtCINno.MaxLength = 16
        Me.txtCINno.Name = "txtCINno"
        Me.txtCINno.ReadOnly = True
        Me.txtCINno.Size = New System.Drawing.Size(372, 27)
        Me.txtCINno.TabIndex = 5
        '
        'txtShareHolder
        '
        Me.txtShareHolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtShareHolder.Enabled = False
        Me.txtShareHolder.Location = New System.Drawing.Point(111, 92)
        Me.txtShareHolder.MaxLength = 0
        Me.txtShareHolder.Name = "txtShareHolder"
        Me.txtShareHolder.ReadOnly = True
        Me.txtShareHolder.Size = New System.Drawing.Size(372, 27)
        Me.txtShareHolder.TabIndex = 3
        Me.txtShareHolder.TabStop = False
        '
        'lblCinNo
        '
        Me.lblCinNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCinNo.Location = New System.Drawing.Point(11, 152)
        Me.lblCinNo.Name = "lblCinNo"
        Me.lblCinNo.Size = New System.Drawing.Size(89, 13)
        Me.lblCinNo.TabIndex = 159
        Me.lblCinNo.Text = "CIN No"
        Me.lblCinNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSharesCount
        '
        Me.txtSharesCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSharesCount.Location = New System.Drawing.Point(345, 257)
        Me.txtSharesCount.MaxLength = 10
        Me.txtSharesCount.Name = "txtSharesCount"
        Me.txtSharesCount.Size = New System.Drawing.Size(140, 27)
        Me.txtSharesCount.TabIndex = 11
        '
        'lblCreditQty
        '
        Me.lblCreditQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCreditQty.Location = New System.Drawing.Point(16, 292)
        Me.lblCreditQty.Name = "lblCreditQty"
        Me.lblCreditQty.Size = New System.Drawing.Size(87, 13)
        Me.lblCreditQty.TabIndex = 165
        Me.lblCreditQty.Text = "Credit Qty"
        Me.lblCreditQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExecutionDate
        '
        Me.lblExecutionDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblExecutionDate.Location = New System.Drawing.Point(269, 237)
        Me.lblExecutionDate.Name = "lblExecutionDate"
        Me.lblExecutionDate.Size = New System.Drawing.Size(70, 13)
        Me.lblExecutionDate.TabIndex = 157
        Me.lblExecutionDate.Text = "Execution"
        Me.lblExecutionDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.grpPhyysicalIEPFhdr)
        Me.grpHeader.Controls.Add(Me.btnCleardgvrec2)
        Me.grpHeader.Controls.Add(Me.lblProgressbar)
        Me.grpHeader.Controls.Add(Me.txtFileName)
        Me.grpHeader.Controls.Add(Me.btnBrowse)
        Me.grpHeader.Controls.Add(Me.lblChooseFile)
        Me.grpHeader.Controls.Add(Me.DisplayTotrecords)
        Me.grpHeader.Controls.Add(Me.lbltotalrecords)
        Me.grpHeader.Controls.Add(Me.displayTotshare)
        Me.grpHeader.Controls.Add(Me.txtIsinId)
        Me.grpHeader.Controls.Add(Me.dgvRecord2)
        Me.grpHeader.Controls.Add(Me.lbltotalShares)
        Me.grpHeader.Location = New System.Drawing.Point(7, 0)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(1131, 380)
        Me.grpHeader.TabIndex = 2
        Me.grpHeader.TabStop = False
        '
        'grpPhyysicalIEPFhdr
        '
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtCompEmailId)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblCompEmailId)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtRtarefNo)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblRtarefNo)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtCreditLockin)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtFolioNo)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtDebitLockin)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblDebitLockin)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblCreditLockin)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.dtpExecDate)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblAllotmentDate)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.dtpApprovalDate)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.cbodivFinyear)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblStampDuty)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.Label5)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblCreditQty)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtDebitQty)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtCreditQty)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.Label19)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblDebitQty)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtInwardNo)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.Label20)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.cboCAtype)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtCompName)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblCAtype)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.Label22)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.Label21)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtCINno)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtShareHolder)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblCinNo)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.txtSharesCount)
        Me.grpPhyysicalIEPFhdr.Controls.Add(Me.lblExecutionDate)
        Me.grpPhyysicalIEPFhdr.Location = New System.Drawing.Point(6, 17)
        Me.grpPhyysicalIEPFhdr.Name = "grpPhyysicalIEPFhdr"
        Me.grpPhyysicalIEPFhdr.Size = New System.Drawing.Size(489, 357)
        Me.grpPhyysicalIEPFhdr.TabIndex = 138
        Me.grpPhyysicalIEPFhdr.TabStop = False
        Me.grpPhyysicalIEPFhdr.Text = "Physical To IEPF Header"
        '
        'txtCompEmailId
        '
        Me.txtCompEmailId.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompEmailId.Location = New System.Drawing.Point(111, 119)
        Me.txtCompEmailId.MaxLength = 50
        Me.txtCompEmailId.Name = "txtCompEmailId"
        Me.txtCompEmailId.ReadOnly = True
        Me.txtCompEmailId.Size = New System.Drawing.Size(372, 27)
        Me.txtCompEmailId.TabIndex = 4
        '
        'lblCompEmailId
        '
        Me.lblCompEmailId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompEmailId.Location = New System.Drawing.Point(11, 126)
        Me.lblCompEmailId.Name = "lblCompEmailId"
        Me.lblCompEmailId.Size = New System.Drawing.Size(89, 13)
        Me.lblCompEmailId.TabIndex = 172
        Me.lblCompEmailId.Text = "Email Id"
        Me.lblCompEmailId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRtarefNo
        '
        Me.txtRtarefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRtarefNo.Location = New System.Drawing.Point(111, 173)
        Me.txtRtarefNo.MaxLength = 16
        Me.txtRtarefNo.Name = "txtRtarefNo"
        Me.txtRtarefNo.Size = New System.Drawing.Size(372, 27)
        Me.txtRtarefNo.TabIndex = 6
        '
        'lblRtarefNo
        '
        Me.lblRtarefNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRtarefNo.Location = New System.Drawing.Point(11, 180)
        Me.lblRtarefNo.Name = "lblRtarefNo"
        Me.lblRtarefNo.Size = New System.Drawing.Size(89, 13)
        Me.lblRtarefNo.TabIndex = 170
        Me.lblRtarefNo.Text = "RTA Ref No"
        Me.lblRtarefNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCleardgvrec2
        '
        Me.btnCleardgvrec2.Location = New System.Drawing.Point(1040, 15)
        Me.btnCleardgvrec2.Name = "btnCleardgvrec2"
        Me.btnCleardgvrec2.Size = New System.Drawing.Size(76, 24)
        Me.btnCleardgvrec2.TabIndex = 2
        Me.btnCleardgvrec2.Text = "Clear Grid"
        Me.btnCleardgvrec2.UseVisualStyleBackColor = True
        '
        'lblProgressbar
        '
        Me.lblProgressbar.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lblProgressbar.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgressbar.ForeColor = System.Drawing.Color.Red
        Me.lblProgressbar.Location = New System.Drawing.Point(683, 171)
        Me.lblProgressbar.Name = "lblProgressbar"
        Me.lblProgressbar.Size = New System.Drawing.Size(110, 19)
        Me.lblProgressbar.TabIndex = 129
        Me.lblProgressbar.Text = "Please Wait ..."
        Me.lblProgressbar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblProgressbar.Visible = False
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(586, 17)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(402, 27)
        Me.txtFileName.TabIndex = 0
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(994, 17)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(40, 21)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblChooseFile
        '
        Me.lblChooseFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChooseFile.Location = New System.Drawing.Point(499, 17)
        Me.lblChooseFile.Name = "lblChooseFile"
        Me.lblChooseFile.Size = New System.Drawing.Size(81, 19)
        Me.lblChooseFile.TabIndex = 108
        Me.lblChooseFile.Text = "Choose File"
        Me.lblChooseFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisplayTotrecords
        '
        Me.DisplayTotrecords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DisplayTotrecords.Location = New System.Drawing.Point(611, 348)
        Me.DisplayTotrecords.Name = "DisplayTotrecords"
        Me.DisplayTotrecords.Size = New System.Drawing.Size(86, 13)
        Me.DisplayTotrecords.TabIndex = 107
        Me.DisplayTotrecords.Text = "0"
        Me.DisplayTotrecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbltotalrecords
        '
        Me.lbltotalrecords.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbltotalrecords.Location = New System.Drawing.Point(506, 345)
        Me.lbltotalrecords.Name = "lbltotalrecords"
        Me.lbltotalrecords.Size = New System.Drawing.Size(99, 19)
        Me.lbltotalrecords.TabIndex = 106
        Me.lbltotalrecords.Text = "Total Records : "
        Me.lbltotalrecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'displayTotshare
        '
        Me.displayTotshare.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.displayTotshare.Location = New System.Drawing.Point(1052, 348)
        Me.displayTotshare.Name = "displayTotshare"
        Me.displayTotshare.Size = New System.Drawing.Size(64, 13)
        Me.displayTotshare.TabIndex = 94
        Me.displayTotshare.Text = "0"
        Me.displayTotshare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIsinId
        '
        Me.txtIsinId.BackColor = System.Drawing.SystemColors.Window
        Me.txtIsinId.Location = New System.Drawing.Point(422, 61)
        Me.txtIsinId.MaxLength = 0
        Me.txtIsinId.Name = "txtIsinId"
        Me.txtIsinId.ReadOnly = True
        Me.txtIsinId.Size = New System.Drawing.Size(30, 27)
        Me.txtIsinId.TabIndex = 79
        Me.txtIsinId.TabStop = False
        Me.txtIsinId.Visible = False
        '
        'dgvRecord2
        '
        Me.dgvRecord2.AllowUserToAddRows = False
        Me.dgvRecord2.AllowUserToDeleteRows = False
        Me.dgvRecord2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRecord2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dp_id, Me.clientid, Me.foliono, Me.dist_from, Me.dist_to, Me.credit_qty, Me.debit_qty, Me.holder1_name, Me.creditqty_lockin_reasoncode, Me.creditqty_lockin_releasedate, Me.debitqty_lockin_reasoncode, Me.debitqty_lockin_releasedate, Me.investor_category, Me.bo_correspondence_addr1, Me.bo_correspondence_addr2, Me.bo_correspondence_addr3, Me.bo_correspondence_city, Me.bo_correspondence_state, Me.bo_correspondence_country, Me.bo_correspondence_pincode, Me.action})
        Me.dgvRecord2.Location = New System.Drawing.Point(509, 47)
        Me.dgvRecord2.Name = "dgvRecord2"
        Me.dgvRecord2.Size = New System.Drawing.Size(607, 295)
        Me.dgvRecord2.TabIndex = 3
        '
        'dp_id
        '
        Me.dp_id.HeaderText = "DP ID"
        Me.dp_id.Name = "dp_id"
        '
        'clientid
        '
        Me.clientid.HeaderText = "Client ID"
        Me.clientid.Name = "clientid"
        '
        'foliono
        '
        Me.foliono.HeaderText = "Folio No"
        Me.foliono.Name = "foliono"
        '
        'dist_from
        '
        Me.dist_from.HeaderText = "Dist From"
        Me.dist_from.Name = "dist_from"
        '
        'dist_to
        '
        Me.dist_to.HeaderText = "Dist To"
        Me.dist_to.Name = "dist_to"
        '
        'credit_qty
        '
        Me.credit_qty.HeaderText = "Credit Qty"
        Me.credit_qty.Name = "credit_qty"
        '
        'debit_qty
        '
        Me.debit_qty.HeaderText = "Debit Qty"
        Me.debit_qty.Name = "debit_qty"
        '
        'holder1_name
        '
        Me.holder1_name.HeaderText = "Holder1 Name"
        Me.holder1_name.Name = "holder1_name"
        '
        'creditqty_lockin_reasoncode
        '
        Me.creditqty_lockin_reasoncode.HeaderText = "Credit Qty Lock-in ReasonCode"
        Me.creditqty_lockin_reasoncode.Name = "creditqty_lockin_reasoncode"
        '
        'creditqty_lockin_releasedate
        '
        Me.creditqty_lockin_releasedate.HeaderText = "Credit Qty Lockin ReleaseDate"
        Me.creditqty_lockin_releasedate.Name = "creditqty_lockin_releasedate"
        '
        'debitqty_lockin_reasoncode
        '
        Me.debitqty_lockin_reasoncode.HeaderText = "Debit Qty Lock-in ReasonCode"
        Me.debitqty_lockin_reasoncode.Name = "debitqty_lockin_reasoncode"
        '
        'debitqty_lockin_releasedate
        '
        Me.debitqty_lockin_releasedate.HeaderText = "Debit Qty Lockin ReleaseDate"
        Me.debitqty_lockin_releasedate.Name = "debitqty_lockin_releasedate"
        '
        'investor_category
        '
        Me.investor_category.HeaderText = "Investor Category"
        Me.investor_category.Name = "investor_category"
        '
        'bo_correspondence_addr1
        '
        Me.bo_correspondence_addr1.HeaderText = "BO Correspondence Address1"
        Me.bo_correspondence_addr1.Name = "bo_correspondence_addr1"
        '
        'bo_correspondence_addr2
        '
        Me.bo_correspondence_addr2.HeaderText = "BO Correspondence Address2"
        Me.bo_correspondence_addr2.Name = "bo_correspondence_addr2"
        '
        'bo_correspondence_addr3
        '
        Me.bo_correspondence_addr3.HeaderText = "BO Correspondence Address3"
        Me.bo_correspondence_addr3.Name = "bo_correspondence_addr3"
        '
        'bo_correspondence_city
        '
        Me.bo_correspondence_city.HeaderText = "BO Correspondence City"
        Me.bo_correspondence_city.Name = "bo_correspondence_city"
        '
        'bo_correspondence_state
        '
        Me.bo_correspondence_state.HeaderText = "BO Correspondence State"
        Me.bo_correspondence_state.Name = "bo_correspondence_state"
        '
        'bo_correspondence_country
        '
        Me.bo_correspondence_country.HeaderText = "BO Correspondence Country"
        Me.bo_correspondence_country.Name = "bo_correspondence_country"
        '
        'bo_correspondence_pincode
        '
        Me.bo_correspondence_pincode.HeaderText = "BO Correspondence Pincode"
        Me.bo_correspondence_pincode.Name = "bo_correspondence_pincode"
        '
        'action
        '
        Me.action.HeaderText = "Remove"
        Me.action.Name = "action"
        '
        'lbltotalShares
        '
        Me.lbltotalShares.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbltotalShares.Location = New System.Drawing.Point(953, 348)
        Me.lbltotalShares.Name = "lbltotalShares"
        Me.lbltotalShares.Size = New System.Drawing.Size(81, 13)
        Me.lbltotalShares.TabIndex = 93
        Me.lbltotalShares.Text = "Total Shares"
        Me.lbltotalShares.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(1043, 518)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(149, 21)
        Me.lnkAddAttachment.TabIndex = 6
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(712, 544)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 17
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1062, 537)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(980, 537)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 4
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(712, 386)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(427, 127)
        Me.txtRemark.TabIndex = 1
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(898, 537)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 3
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(816, 537)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'dgvChklst
        '
        Me.dgvChklst.AllowUserToAddRows = False
        Me.dgvChklst.AllowUserToDeleteRows = False
        Me.dgvChklst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChklst.Location = New System.Drawing.Point(7, 387)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(619, 170)
        Me.dgvChklst.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(632, 389)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 13)
        Me.Label23.TabIndex = 22
        Me.Label23.Text = "Remark"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(639, 544)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(55, 13)
        Me.Label18.TabIndex = 23
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCAPhysicalToIEPF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1151, 575)
        Me.Controls.Add(Me.lnkAddAttachment)
        Me.Controls.Add(Me.lblDocStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.dgvChklst)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.grpHeader)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCAPhysicalToIEPF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCAPhysicalToIEPF"
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        Me.grpPhyysicalIEPFhdr.ResumeLayout(False)
        Me.grpPhyysicalIEPFhdr.PerformLayout()
        CType(Me.dgvRecord2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCreditLockin As System.Windows.Forms.TextBox
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents txtDebitLockin As System.Windows.Forms.TextBox
    Friend WithEvents lblCreditLockin As System.Windows.Forms.Label
    Friend WithEvents lblDebitLockin As System.Windows.Forms.Label
    Friend WithEvents dtpExecDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAllotmentDate As System.Windows.Forms.Label
    Friend WithEvents dtpApprovalDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbodivFinyear As System.Windows.Forms.ComboBox
    Friend WithEvents lblStampDuty As System.Windows.Forms.Label
    Friend WithEvents txtCreditQty As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDebitQty As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblDebitQty As System.Windows.Forms.Label
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboCAtype As System.Windows.Forms.ComboBox
    Friend WithEvents txtCompName As System.Windows.Forms.TextBox
    Friend WithEvents lblCAtype As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtCINno As System.Windows.Forms.TextBox
    Friend WithEvents txtShareHolder As System.Windows.Forms.TextBox
    Friend WithEvents lblCinNo As System.Windows.Forms.Label
    Friend WithEvents txtSharesCount As System.Windows.Forms.TextBox
    Friend WithEvents lblCreditQty As System.Windows.Forms.Label
    Friend WithEvents lblExecutionDate As System.Windows.Forms.Label
    Friend WithEvents grpHeader As System.Windows.Forms.GroupBox
    Friend WithEvents grpPhyysicalIEPFhdr As System.Windows.Forms.GroupBox
    Friend WithEvents btnCleardgvrec2 As System.Windows.Forms.Button
    Friend WithEvents lblProgressbar As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lblChooseFile As System.Windows.Forms.Label
    Friend WithEvents DisplayTotrecords As System.Windows.Forms.Label
    Friend WithEvents lbltotalrecords As System.Windows.Forms.Label
    Friend WithEvents displayTotshare As System.Windows.Forms.Label
    Friend WithEvents txtIsinId As System.Windows.Forms.TextBox
    Friend WithEvents dgvRecord2 As System.Windows.Forms.DataGridView
    Friend WithEvents lbltotalShares As System.Windows.Forms.Label
    Friend WithEvents lnkAddAttachment As System.Windows.Forms.LinkLabel
    Friend WithEvents lblDocStatus As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents btnReject As System.Windows.Forms.Button
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents dgvChklst As System.Windows.Forms.DataGridView
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtCompEmailId As System.Windows.Forms.TextBox
    Friend WithEvents lblCompEmailId As System.Windows.Forms.Label
    Friend WithEvents txtRtarefNo As System.Windows.Forms.TextBox
    Friend WithEvents lblRtarefNo As System.Windows.Forms.Label
    Friend WithEvents dp_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clientid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents foliono As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dist_from As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dist_to As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents credit_qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents debit_qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents holder1_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents creditqty_lockin_reasoncode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents creditqty_lockin_releasedate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents debitqty_lockin_reasoncode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents debitqty_lockin_releasedate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents investor_category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bo_correspondence_addr1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bo_correspondence_addr2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bo_correspondence_addr3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bo_correspondence_city As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bo_correspondence_state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bo_correspondence_country As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bo_correspondence_pincode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents action As System.Windows.Forms.DataGridViewButtonColumn
End Class
