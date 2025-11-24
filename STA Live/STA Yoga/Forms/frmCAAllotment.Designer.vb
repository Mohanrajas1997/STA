<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCAAllotment
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
        Me.lblDocStatus = New System.Windows.Forms.Label()
        Me.lnkAddAttachment = New System.Windows.Forms.LinkLabel()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.dgvChklst = New System.Windows.Forms.DataGridView()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.grpHeader = New System.Windows.Forms.GroupBox()
        Me.grpInwardDtl = New System.Windows.Forms.GroupBox()
        Me.txtPaidupamt = New System.Windows.Forms.TextBox()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.txtIssueamt = New System.Windows.Forms.TextBox()
        Me.lblPaidupamt = New System.Windows.Forms.Label()
        Me.lblissueamt = New System.Windows.Forms.Label()
        Me.dtpExecDate = New System.Windows.Forms.DateTimePicker()
        Me.lblAllotmentDate = New System.Windows.Forms.Label()
        Me.dtpAllotmentDate = New System.Windows.Forms.DateTimePicker()
        Me.cboStampDuty = New System.Windows.Forms.ComboBox()
        Me.lblAllotmentdesc = New System.Windows.Forms.Label()
        Me.lblStampDuty = New System.Windows.Forms.Label()
        Me.txtDistto = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDistfrom = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblDistFrom = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.cboAllotmentdesc = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboCAtype = New System.Windows.Forms.ComboBox()
        Me.txtCompName = New System.Windows.Forms.TextBox()
        Me.lblCAtype = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCAno = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtRtarefno = New System.Windows.Forms.TextBox()
        Me.txtShareHolder = New System.Windows.Forms.TextBox()
        Me.lblRtarefno = New System.Windows.Forms.Label()
        Me.txtSharesCount = New System.Windows.Forms.TextBox()
        Me.lblDistTo = New System.Windows.Forms.Label()
        Me.lblExecutionDate = New System.Windows.Forms.Label()
        Me.lblCAno = New System.Windows.Forms.Label()
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
        Me.client_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dist_from = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dist_to = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.share_count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.face_value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.offerprice_premium = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lock_in_flag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lock_in_reason_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lock_in_realease_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.share_price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.purchase_cost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stamp_duty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.action = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.lbltotalShares = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ofdInput = New System.Windows.Forms.OpenFileDialog()
        Me.txtInputIsinid = New System.Windows.Forms.TextBox()
        Me.lblisinid = New System.Windows.Forms.Label()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        Me.grpInwardDtl.SuspendLayout()
        CType(Me.dgvRecord2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(609, 512)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 3
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(935, 486)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(149, 21)
        Me.lnkAddAttachment.TabIndex = 6
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(609, 354)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(427, 127)
        Me.txtRemark.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(529, 357)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 13)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Remark"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvChklst
        '
        Me.dgvChklst.AllowUserToAddRows = False
        Me.dgvChklst.AllowUserToDeleteRows = False
        Me.dgvChklst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChklst.Location = New System.Drawing.Point(13, 354)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(517, 170)
        Me.dgvChklst.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(529, 512)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.grpInwardDtl)
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
        Me.grpHeader.Location = New System.Drawing.Point(12, -6)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(1039, 351)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'grpInwardDtl
        '
        Me.grpInwardDtl.Controls.Add(Me.txtInputIsinid)
        Me.grpInwardDtl.Controls.Add(Me.lblisinid)
        Me.grpInwardDtl.Controls.Add(Me.txtPaidupamt)
        Me.grpInwardDtl.Controls.Add(Me.txtFolioNo)
        Me.grpInwardDtl.Controls.Add(Me.txtIssueamt)
        Me.grpInwardDtl.Controls.Add(Me.lblPaidupamt)
        Me.grpInwardDtl.Controls.Add(Me.lblissueamt)
        Me.grpInwardDtl.Controls.Add(Me.dtpExecDate)
        Me.grpInwardDtl.Controls.Add(Me.lblAllotmentDate)
        Me.grpInwardDtl.Controls.Add(Me.dtpAllotmentDate)
        Me.grpInwardDtl.Controls.Add(Me.cboStampDuty)
        Me.grpInwardDtl.Controls.Add(Me.lblAllotmentdesc)
        Me.grpInwardDtl.Controls.Add(Me.lblStampDuty)
        Me.grpInwardDtl.Controls.Add(Me.txtDistto)
        Me.grpInwardDtl.Controls.Add(Me.Label5)
        Me.grpInwardDtl.Controls.Add(Me.txtDistfrom)
        Me.grpInwardDtl.Controls.Add(Me.Label19)
        Me.grpInwardDtl.Controls.Add(Me.lblDistFrom)
        Me.grpInwardDtl.Controls.Add(Me.txtInwardNo)
        Me.grpInwardDtl.Controls.Add(Me.cboAllotmentdesc)
        Me.grpInwardDtl.Controls.Add(Me.Label20)
        Me.grpInwardDtl.Controls.Add(Me.cboCAtype)
        Me.grpInwardDtl.Controls.Add(Me.txtCompName)
        Me.grpInwardDtl.Controls.Add(Me.lblCAtype)
        Me.grpInwardDtl.Controls.Add(Me.Label22)
        Me.grpInwardDtl.Controls.Add(Me.txtCAno)
        Me.grpInwardDtl.Controls.Add(Me.Label21)
        Me.grpInwardDtl.Controls.Add(Me.txtRtarefno)
        Me.grpInwardDtl.Controls.Add(Me.txtShareHolder)
        Me.grpInwardDtl.Controls.Add(Me.lblRtarefno)
        Me.grpInwardDtl.Controls.Add(Me.txtSharesCount)
        Me.grpInwardDtl.Controls.Add(Me.lblDistTo)
        Me.grpInwardDtl.Controls.Add(Me.lblExecutionDate)
        Me.grpInwardDtl.Controls.Add(Me.lblCAno)
        Me.grpInwardDtl.Location = New System.Drawing.Point(-5, 13)
        Me.grpInwardDtl.Name = "grpInwardDtl"
        Me.grpInwardDtl.Size = New System.Drawing.Size(415, 341)
        Me.grpInwardDtl.TabIndex = 0
        Me.grpInwardDtl.TabStop = False
        Me.grpInwardDtl.Text = "Inward Details"
        '
        'txtPaidupamt
        '
        Me.txtPaidupamt.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaidupamt.Enabled = False
        Me.txtPaidupamt.Location = New System.Drawing.Point(292, 311)
        Me.txtPaidupamt.MaxLength = 0
        Me.txtPaidupamt.Name = "txtPaidupamt"
        Me.txtPaidupamt.ReadOnly = True
        Me.txtPaidupamt.Size = New System.Drawing.Size(114, 27)
        Me.txtPaidupamt.TabIndex = 16
        '
        'txtFolioNo
        '
        Me.txtFolioNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtFolioNo.Enabled = False
        Me.txtFolioNo.Location = New System.Drawing.Point(111, 65)
        Me.txtFolioNo.MaxLength = 0
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.ReadOnly = True
        Me.txtFolioNo.Size = New System.Drawing.Size(295, 27)
        Me.txtFolioNo.TabIndex = 2
        Me.txtFolioNo.TabStop = False
        '
        'txtIssueamt
        '
        Me.txtIssueamt.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssueamt.Enabled = False
        Me.txtIssueamt.Location = New System.Drawing.Point(110, 309)
        Me.txtIssueamt.MaxLength = 0
        Me.txtIssueamt.Name = "txtIssueamt"
        Me.txtIssueamt.ReadOnly = True
        Me.txtIssueamt.Size = New System.Drawing.Size(114, 27)
        Me.txtIssueamt.TabIndex = 15
        '
        'lblPaidupamt
        '
        Me.lblPaidupamt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaidupamt.Location = New System.Drawing.Point(197, 316)
        Me.lblPaidupamt.Name = "lblPaidupamt"
        Me.lblPaidupamt.Size = New System.Drawing.Size(70, 13)
        Me.lblPaidupamt.TabIndex = 168
        Me.lblPaidupamt.Text = "₹ Paidup"
        Me.lblPaidupamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblissueamt
        '
        Me.lblissueamt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblissueamt.Location = New System.Drawing.Point(8, 316)
        Me.lblissueamt.Name = "lblissueamt"
        Me.lblissueamt.Size = New System.Drawing.Size(97, 13)
        Me.lblissueamt.TabIndex = 167
        Me.lblissueamt.Text = "₹ Issue"
        Me.lblissueamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpExecDate
        '
        Me.dtpExecDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpExecDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExecDate.Location = New System.Drawing.Point(292, 229)
        Me.dtpExecDate.Name = "dtpExecDate"
        Me.dtpExecDate.Size = New System.Drawing.Size(114, 27)
        Me.dtpExecDate.TabIndex = 10
        Me.dtpExecDate.Value = New Date(2024, 6, 11, 0, 0, 0, 0)
        '
        'lblAllotmentDate
        '
        Me.lblAllotmentDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAllotmentDate.Location = New System.Drawing.Point(8, 237)
        Me.lblAllotmentDate.Name = "lblAllotmentDate"
        Me.lblAllotmentDate.Size = New System.Drawing.Size(97, 13)
        Me.lblAllotmentDate.TabIndex = 163
        Me.lblAllotmentDate.Text = "Allotment Date"
        Me.lblAllotmentDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpAllotmentDate
        '
        Me.dtpAllotmentDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpAllotmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAllotmentDate.Location = New System.Drawing.Point(111, 228)
        Me.dtpAllotmentDate.Name = "dtpAllotmentDate"
        Me.dtpAllotmentDate.Size = New System.Drawing.Size(114, 27)
        Me.dtpAllotmentDate.TabIndex = 9
        Me.dtpAllotmentDate.Value = New Date(2024, 6, 11, 0, 0, 0, 0)
        '
        'cboStampDuty
        '
        Me.cboStampDuty.FormattingEnabled = True
        Me.cboStampDuty.Items.AddRange(New Object() {"Yes", "No"})
        Me.cboStampDuty.Location = New System.Drawing.Point(110, 256)
        Me.cboStampDuty.Name = "cboStampDuty"
        Me.cboStampDuty.Size = New System.Drawing.Size(114, 29)
        Me.cboStampDuty.TabIndex = 11
        '
        'lblAllotmentdesc
        '
        Me.lblAllotmentdesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAllotmentdesc.Location = New System.Drawing.Point(8, 209)
        Me.lblAllotmentdesc.Name = "lblAllotmentdesc"
        Me.lblAllotmentdesc.Size = New System.Drawing.Size(97, 13)
        Me.lblAllotmentdesc.TabIndex = 162
        Me.lblAllotmentdesc.Text = "Allotment Desc"
        Me.lblAllotmentdesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStampDuty
        '
        Me.lblStampDuty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStampDuty.Location = New System.Drawing.Point(8, 263)
        Me.lblStampDuty.Name = "lblStampDuty"
        Me.lblStampDuty.Size = New System.Drawing.Size(97, 13)
        Me.lblStampDuty.TabIndex = 166
        Me.lblStampDuty.Text = "Stamp Duty"
        Me.lblStampDuty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDistto
        '
        Me.txtDistto.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistto.Location = New System.Drawing.Point(292, 283)
        Me.txtDistto.MaxLength = 0
        Me.txtDistto.Name = "txtDistto"
        Me.txtDistto.Size = New System.Drawing.Size(114, 27)
        Me.txtDistto.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(195, 263)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 158
        Me.Label5.Text = "Shares"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDistfrom
        '
        Me.txtDistfrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistfrom.Location = New System.Drawing.Point(110, 283)
        Me.txtDistfrom.MaxLength = 0
        Me.txtDistfrom.Name = "txtDistfrom"
        Me.txtDistfrom.Size = New System.Drawing.Size(114, 27)
        Me.txtDistfrom.TabIndex = 13
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(8, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(97, 13)
        Me.Label19.TabIndex = 153
        Me.Label19.Text = "Inward No"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDistFrom
        '
        Me.lblDistFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDistFrom.Location = New System.Drawing.Point(8, 290)
        Me.lblDistFrom.Name = "lblDistFrom"
        Me.lblDistFrom.Size = New System.Drawing.Size(97, 13)
        Me.lblDistFrom.TabIndex = 164
        Me.lblDistFrom.Text = "Dist From"
        Me.lblDistFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInwardNo.Enabled = False
        Me.txtInwardNo.Location = New System.Drawing.Point(111, 11)
        Me.txtInwardNo.MaxLength = 0
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.ReadOnly = True
        Me.txtInwardNo.Size = New System.Drawing.Size(295, 27)
        Me.txtInwardNo.TabIndex = 0
        Me.txtInwardNo.TabStop = False
        '
        'cboAllotmentdesc
        '
        Me.cboAllotmentdesc.FormattingEnabled = True
        Me.cboAllotmentdesc.Location = New System.Drawing.Point(111, 202)
        Me.cboAllotmentdesc.Name = "cboAllotmentdesc"
        Me.cboAllotmentdesc.Size = New System.Drawing.Size(295, 29)
        Me.cboAllotmentdesc.TabIndex = 8
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(8, 45)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(97, 13)
        Me.Label20.TabIndex = 154
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCAtype
        '
        Me.cboCAtype.FormattingEnabled = True
        Me.cboCAtype.Location = New System.Drawing.Point(111, 175)
        Me.cboCAtype.Name = "cboCAtype"
        Me.cboCAtype.Size = New System.Drawing.Size(295, 29)
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
        Me.txtCompName.Size = New System.Drawing.Size(295, 27)
        Me.txtCompName.TabIndex = 1
        Me.txtCompName.TabStop = False
        '
        'lblCAtype
        '
        Me.lblCAtype.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAtype.Location = New System.Drawing.Point(8, 182)
        Me.lblCAtype.Name = "lblCAtype"
        Me.lblCAtype.Size = New System.Drawing.Size(97, 13)
        Me.lblCAtype.TabIndex = 161
        Me.lblCAtype.Text = "CA Type"
        Me.lblCAtype.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(8, 72)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(97, 13)
        Me.Label22.TabIndex = 155
        Me.Label22.Text = "Folio No"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCAno
        '
        Me.txtCAno.BackColor = System.Drawing.SystemColors.Window
        Me.txtCAno.Location = New System.Drawing.Point(292, 147)
        Me.txtCAno.MaxLength = 16
        Me.txtCAno.Name = "txtCAno"
        Me.txtCAno.Size = New System.Drawing.Size(114, 27)
        Me.txtCAno.TabIndex = 6
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(8, 99)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(97, 13)
        Me.Label21.TabIndex = 156
        Me.Label21.Text = "Share Holder"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRtarefno
        '
        Me.txtRtarefno.BackColor = System.Drawing.SystemColors.Window
        Me.txtRtarefno.Location = New System.Drawing.Point(111, 147)
        Me.txtRtarefno.MaxLength = 16
        Me.txtRtarefno.Name = "txtRtarefno"
        Me.txtRtarefno.Size = New System.Drawing.Size(114, 27)
        Me.txtRtarefno.TabIndex = 5
        '
        'txtShareHolder
        '
        Me.txtShareHolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtShareHolder.Enabled = False
        Me.txtShareHolder.Location = New System.Drawing.Point(111, 92)
        Me.txtShareHolder.MaxLength = 0
        Me.txtShareHolder.Name = "txtShareHolder"
        Me.txtShareHolder.ReadOnly = True
        Me.txtShareHolder.Size = New System.Drawing.Size(295, 27)
        Me.txtShareHolder.TabIndex = 3
        Me.txtShareHolder.TabStop = False
        '
        'lblRtarefno
        '
        Me.lblRtarefno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRtarefno.Location = New System.Drawing.Point(8, 150)
        Me.lblRtarefno.Name = "lblRtarefno"
        Me.lblRtarefno.Size = New System.Drawing.Size(97, 13)
        Me.lblRtarefno.TabIndex = 159
        Me.lblRtarefno.Text = "RTA Ref No"
        Me.lblRtarefno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSharesCount
        '
        Me.txtSharesCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSharesCount.Enabled = False
        Me.txtSharesCount.Location = New System.Drawing.Point(292, 256)
        Me.txtSharesCount.MaxLength = 10
        Me.txtSharesCount.Name = "txtSharesCount"
        Me.txtSharesCount.Size = New System.Drawing.Size(114, 27)
        Me.txtSharesCount.TabIndex = 12
        '
        'lblDistTo
        '
        Me.lblDistTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDistTo.Location = New System.Drawing.Point(201, 290)
        Me.lblDistTo.Name = "lblDistTo"
        Me.lblDistTo.Size = New System.Drawing.Size(70, 13)
        Me.lblDistTo.TabIndex = 165
        Me.lblDistTo.Text = "Dist To"
        Me.lblDistTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExecutionDate
        '
        Me.lblExecutionDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblExecutionDate.Location = New System.Drawing.Point(231, 237)
        Me.lblExecutionDate.Name = "lblExecutionDate"
        Me.lblExecutionDate.Size = New System.Drawing.Size(70, 13)
        Me.lblExecutionDate.TabIndex = 157
        Me.lblExecutionDate.Text = "Execution"
        Me.lblExecutionDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCAno
        '
        Me.lblCAno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAno.Location = New System.Drawing.Point(212, 150)
        Me.lblCAno.Name = "lblCAno"
        Me.lblCAno.Size = New System.Drawing.Size(74, 13)
        Me.lblCAno.TabIndex = 160
        Me.lblCAno.Text = "CA No"
        Me.lblCAno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCleardgvrec2
        '
        Me.btnCleardgvrec2.Location = New System.Drawing.Point(947, 15)
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
        Me.txtFileName.Location = New System.Drawing.Point(493, 17)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(402, 27)
        Me.txtFileName.TabIndex = 128
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(901, 17)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(40, 21)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblChooseFile
        '
        Me.lblChooseFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChooseFile.Location = New System.Drawing.Point(406, 17)
        Me.lblChooseFile.Name = "lblChooseFile"
        Me.lblChooseFile.Size = New System.Drawing.Size(81, 19)
        Me.lblChooseFile.TabIndex = 108
        Me.lblChooseFile.Text = "Choose File"
        Me.lblChooseFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisplayTotrecords
        '
        Me.DisplayTotrecords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DisplayTotrecords.Location = New System.Drawing.Point(518, 329)
        Me.DisplayTotrecords.Name = "DisplayTotrecords"
        Me.DisplayTotrecords.Size = New System.Drawing.Size(86, 13)
        Me.DisplayTotrecords.TabIndex = 107
        Me.DisplayTotrecords.Text = "0"
        Me.DisplayTotrecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbltotalrecords
        '
        Me.lbltotalrecords.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbltotalrecords.Location = New System.Drawing.Point(413, 326)
        Me.lbltotalrecords.Name = "lbltotalrecords"
        Me.lbltotalrecords.Size = New System.Drawing.Size(99, 19)
        Me.lbltotalrecords.TabIndex = 106
        Me.lbltotalrecords.Text = "Total Records : "
        Me.lbltotalrecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'displayTotshare
        '
        Me.displayTotshare.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.displayTotshare.Location = New System.Drawing.Point(959, 329)
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
        Me.dgvRecord2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dp_id, Me.client_id, Me.dist_from, Me.dist_to, Me.share_count, Me.face_value, Me.offerprice_premium, Me.lock_in_flag, Me.lock_in_reason_code, Me.lock_in_realease_date, Me.share_price, Me.purchase_cost, Me.stamp_duty, Me.action})
        Me.dgvRecord2.Location = New System.Drawing.Point(416, 47)
        Me.dgvRecord2.Name = "dgvRecord2"
        Me.dgvRecord2.Size = New System.Drawing.Size(607, 274)
        Me.dgvRecord2.TabIndex = 0
        '
        'dp_id
        '
        Me.dp_id.HeaderText = "DP ID"
        Me.dp_id.Name = "dp_id"
        '
        'client_id
        '
        Me.client_id.HeaderText = "Client ID"
        Me.client_id.Name = "client_id"
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
        'share_count
        '
        Me.share_count.HeaderText = "Share Count"
        Me.share_count.Name = "share_count"
        '
        'face_value
        '
        Me.face_value.HeaderText = "Face Value"
        Me.face_value.Name = "face_value"
        '
        'offerprice_premium
        '
        Me.offerprice_premium.HeaderText = "Offer Price/Premium"
        Me.offerprice_premium.Name = "offerprice_premium"
        '
        'lock_in_flag
        '
        Me.lock_in_flag.HeaderText = "Lock In Flag"
        Me.lock_in_flag.Name = "lock_in_flag"
        '
        'lock_in_reason_code
        '
        Me.lock_in_reason_code.HeaderText = "Lock In ReasonCode"
        Me.lock_in_reason_code.Name = "lock_in_reason_code"
        '
        'lock_in_realease_date
        '
        Me.lock_in_realease_date.HeaderText = "Lock In Release Date"
        Me.lock_in_realease_date.Name = "lock_in_realease_date"
        '
        'share_price
        '
        Me.share_price.HeaderText = "Share Price"
        Me.share_price.Name = "share_price"
        '
        'purchase_cost
        '
        Me.purchase_cost.HeaderText = "Purchase Cost"
        Me.purchase_cost.Name = "purchase_cost"
        '
        'stamp_duty
        '
        Me.stamp_duty.HeaderText = "Stamp Duty"
        Me.stamp_duty.Name = "stamp_duty"
        '
        'action
        '
        Me.action.HeaderText = "Remove"
        Me.action.Name = "action"
        '
        'lbltotalShares
        '
        Me.lbltotalShares.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbltotalShares.Location = New System.Drawing.Point(860, 329)
        Me.lbltotalShares.Name = "lbltotalShares"
        Me.lbltotalShares.Size = New System.Drawing.Size(81, 13)
        Me.lbltotalShares.TabIndex = 93
        Me.lbltotalShares.Text = "Total Shares"
        Me.lbltotalShares.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(729, 505)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(811, 505)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 3
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(893, 505)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 4
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(975, 505)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtInputIsinid
        '
        Me.txtInputIsinid.BackColor = System.Drawing.SystemColors.Window
        Me.txtInputIsinid.Location = New System.Drawing.Point(111, 119)
        Me.txtInputIsinid.MaxLength = 0
        Me.txtInputIsinid.Name = "txtInputIsinid"
        Me.txtInputIsinid.Size = New System.Drawing.Size(295, 27)
        Me.txtInputIsinid.TabIndex = 4
        '
        'lblisinid
        '
        Me.lblisinid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblisinid.Location = New System.Drawing.Point(8, 126)
        Me.lblisinid.Name = "lblisinid"
        Me.lblisinid.Size = New System.Drawing.Size(97, 13)
        Me.lblisinid.TabIndex = 172
        Me.lblisinid.Text = "ISIN ID"
        Me.lblisinid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCAAllotment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1063, 535)
        Me.Controls.Add(Me.grpHeader)
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
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCAAllotment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Corporate Action Allotment"
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        Me.grpInwardDtl.ResumeLayout(False)
        Me.grpInwardDtl.PerformLayout()
        CType(Me.dgvRecord2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvChklst As System.Windows.Forms.DataGridView
    Friend WithEvents grpHeader As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents btnReject As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lnkAddAttachment As System.Windows.Forms.LinkLabel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblDocStatus As System.Windows.Forms.Label
    Friend WithEvents dgvRecord2 As System.Windows.Forms.DataGridView
    Friend WithEvents txtIsinId As System.Windows.Forms.TextBox
    Friend WithEvents displayTotshare As System.Windows.Forms.Label
    Friend WithEvents lbltotalShares As System.Windows.Forms.Label
    Friend WithEvents DisplayTotrecords As System.Windows.Forms.Label
    Friend WithEvents lbltotalrecords As System.Windows.Forms.Label
    Friend WithEvents lblChooseFile As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dp_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents client_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dist_from As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dist_to As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents share_count As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents face_value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents offerprice_premium As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lock_in_flag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lock_in_reason_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lock_in_realease_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents share_price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents purchase_cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents stamp_duty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents action As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents lblProgressbar As System.Windows.Forms.Label
    Friend WithEvents btnCleardgvrec2 As System.Windows.Forms.Button
    Friend WithEvents grpInwardDtl As System.Windows.Forms.GroupBox
    Friend WithEvents txtPaidupamt As System.Windows.Forms.TextBox
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents txtIssueamt As System.Windows.Forms.TextBox
    Friend WithEvents lblPaidupamt As System.Windows.Forms.Label
    Friend WithEvents lblissueamt As System.Windows.Forms.Label
    Friend WithEvents dtpExecDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAllotmentDate As System.Windows.Forms.Label
    Friend WithEvents dtpAllotmentDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboStampDuty As System.Windows.Forms.ComboBox
    Friend WithEvents lblAllotmentdesc As System.Windows.Forms.Label
    Friend WithEvents lblStampDuty As System.Windows.Forms.Label
    Friend WithEvents txtDistto As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDistfrom As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblDistFrom As System.Windows.Forms.Label
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents cboAllotmentdesc As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboCAtype As System.Windows.Forms.ComboBox
    Friend WithEvents txtCompName As System.Windows.Forms.TextBox
    Friend WithEvents lblCAtype As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtCAno As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtRtarefno As System.Windows.Forms.TextBox
    Friend WithEvents txtShareHolder As System.Windows.Forms.TextBox
    Friend WithEvents lblRtarefno As System.Windows.Forms.Label
    Friend WithEvents txtSharesCount As System.Windows.Forms.TextBox
    Friend WithEvents lblDistTo As System.Windows.Forms.Label
    Friend WithEvents lblExecutionDate As System.Windows.Forms.Label
    Friend WithEvents lblCAno As System.Windows.Forms.Label
    Friend WithEvents txtInputIsinid As System.Windows.Forms.TextBox
    Friend WithEvents lblisinid As System.Windows.Forms.Label
End Class
