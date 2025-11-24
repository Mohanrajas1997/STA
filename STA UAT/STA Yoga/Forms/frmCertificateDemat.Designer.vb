<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCertificateDemat
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
        Me.lblDepository = New System.Windows.Forms.Label()
        Me.txtInputClientid = New System.Windows.Forms.TextBox()
        Me.lblClientid = New System.Windows.Forms.Label()
        Me.ChkNameChng = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvReason = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvNameChange = New System.Windows.Forms.DataGridView()
        Me.btnUpdateFolioPan = New System.Windows.Forms.Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblShareSelected = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDematPendId = New System.Windows.Forms.TextBox()
        Me.txtSharesDrn = New System.Windows.Forms.TextBox()
        Me.txtDpId = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtNameElect = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtReqDate = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDepository = New System.Windows.Forms.TextBox()
        Me.txtDrnNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtIsinId = New System.Windows.Forms.TextBox()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.dgvCert = New System.Windows.Forms.DataGridView()
        Me.txtShareHolder = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCompName = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtJoint1Name = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtJoint2Name = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtClientid = New System.Windows.Forms.TextBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Picbox_sign = New System.Windows.Forms.PictureBox()
        Me.lblnosignature = New System.Windows.Forms.Label()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvReason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvNameChange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picbox_sign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(621, 478)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 3
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(952, 478)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(149, 21)
        Me.lnkAddAttachment.TabIndex = 4
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(624, 356)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(427, 58)
        Me.txtRemark.TabIndex = 2
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(544, 356)
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
        Me.dgvChklst.Location = New System.Drawing.Point(13, 356)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(517, 170)
        Me.dgvChklst.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(541, 478)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.lblDepository)
        Me.grpHeader.Controls.Add(Me.txtInputClientid)
        Me.grpHeader.Controls.Add(Me.lblClientid)
        Me.grpHeader.Controls.Add(Me.ChkNameChng)
        Me.grpHeader.Controls.Add(Me.TabControl1)
        Me.grpHeader.Controls.Add(Me.btnUpdateFolioPan)
        Me.grpHeader.Controls.Add(Me.lblTotal)
        Me.grpHeader.Controls.Add(Me.Label27)
        Me.grpHeader.Controls.Add(Me.lblShareSelected)
        Me.grpHeader.Controls.Add(Me.Label8)
        Me.grpHeader.Controls.Add(Me.txtDematPendId)
        Me.grpHeader.Controls.Add(Me.txtSharesDrn)
        Me.grpHeader.Controls.Add(Me.txtDpId)
        Me.grpHeader.Controls.Add(Me.Label6)
        Me.grpHeader.Controls.Add(Me.txtNameElect)
        Me.grpHeader.Controls.Add(Me.Label7)
        Me.grpHeader.Controls.Add(Me.txtReqDate)
        Me.grpHeader.Controls.Add(Me.Label4)
        Me.grpHeader.Controls.Add(Me.txtDepository)
        Me.grpHeader.Controls.Add(Me.txtDrnNo)
        Me.grpHeader.Controls.Add(Me.Label2)
        Me.grpHeader.Controls.Add(Me.txtIsinId)
        Me.grpHeader.Controls.Add(Me.txtCertNo)
        Me.grpHeader.Controls.Add(Me.txtPanNo)
        Me.grpHeader.Controls.Add(Me.Label1)
        Me.grpHeader.Controls.Add(Me.Label26)
        Me.grpHeader.Controls.Add(Me.dgvCert)
        Me.grpHeader.Controls.Add(Me.txtShareHolder)
        Me.grpHeader.Controls.Add(Me.Label21)
        Me.grpHeader.Controls.Add(Me.txtFolioNo)
        Me.grpHeader.Controls.Add(Me.Label22)
        Me.grpHeader.Controls.Add(Me.txtCompName)
        Me.grpHeader.Controls.Add(Me.Label20)
        Me.grpHeader.Controls.Add(Me.txtInwardNo)
        Me.grpHeader.Controls.Add(Me.Label19)
        Me.grpHeader.Controls.Add(Me.txtJoint1Name)
        Me.grpHeader.Controls.Add(Me.Label10)
        Me.grpHeader.Controls.Add(Me.txtJoint2Name)
        Me.grpHeader.Controls.Add(Me.Label11)
        Me.grpHeader.Controls.Add(Me.Label5)
        Me.grpHeader.Location = New System.Drawing.Point(13, 0)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(1038, 350)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'lblDepository
        '
        Me.lblDepository.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDepository.Location = New System.Drawing.Point(13, 266)
        Me.lblDepository.Name = "lblDepository"
        Me.lblDepository.Size = New System.Drawing.Size(82, 18)
        Me.lblDepository.TabIndex = 117
        Me.lblDepository.Text = "Depository"
        Me.lblDepository.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInputClientid
        '
        Me.txtInputClientid.BackColor = System.Drawing.SystemColors.Window
        Me.txtInputClientid.Location = New System.Drawing.Point(101, 290)
        Me.txtInputClientid.MaxLength = 16
        Me.txtInputClientid.Name = "txtInputClientid"
        Me.txtInputClientid.Size = New System.Drawing.Size(284, 27)
        Me.txtInputClientid.TabIndex = 116
        Me.txtInputClientid.TabStop = False
        '
        'lblClientid
        '
        Me.lblClientid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClientid.Location = New System.Drawing.Point(13, 294)
        Me.lblClientid.Name = "lblClientid"
        Me.lblClientid.Size = New System.Drawing.Size(82, 18)
        Me.lblClientid.TabIndex = 115
        Me.lblClientid.Text = "Client Id"
        Me.lblClientid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ChkNameChng
        '
        Me.ChkNameChng.AutoSize = True
        Me.ChkNameChng.Location = New System.Drawing.Point(282, 157)
        Me.ChkNameChng.Name = "ChkNameChng"
        Me.ChkNameChng.Size = New System.Drawing.Size(165, 25)
        Me.ChkNameChng.TabIndex = 113
        Me.ChkNameChng.Text = "Name Changed"
        Me.ChkNameChng.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(391, 179)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(641, 139)
        Me.TabControl1.TabIndex = 112
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvReason)
        Me.TabPage1.Location = New System.Drawing.Point(4, 30)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(633, 105)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Rejected Reason"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvReason
        '
        Me.dgvReason.AllowUserToAddRows = False
        Me.dgvReason.AllowUserToDeleteRows = False
        Me.dgvReason.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReason.Location = New System.Drawing.Point(3, 3)
        Me.dgvReason.Name = "dgvReason"
        Me.dgvReason.Size = New System.Drawing.Size(627, 132)
        Me.dgvReason.TabIndex = 14
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvNameChange)
        Me.TabPage2.Location = New System.Drawing.Point(4, 30)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(633, 105)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Name Mismatch Reason"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvNameChange
        '
        Me.dgvNameChange.AllowUserToAddRows = False
        Me.dgvNameChange.AllowUserToDeleteRows = False
        Me.dgvNameChange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNameChange.Location = New System.Drawing.Point(3, 3)
        Me.dgvNameChange.Name = "dgvNameChange"
        Me.dgvNameChange.Size = New System.Drawing.Size(627, 107)
        Me.dgvNameChange.TabIndex = 15
        '
        'btnUpdateFolioPan
        '
        Me.btnUpdateFolioPan.Location = New System.Drawing.Point(305, 128)
        Me.btnUpdateFolioPan.Name = "btnUpdateFolioPan"
        Me.btnUpdateFolioPan.Size = New System.Drawing.Size(80, 21)
        Me.btnUpdateFolioPan.TabIndex = 4
        Me.btnUpdateFolioPan.TabStop = False
        Me.btnUpdateFolioPan.Text = "Set PAN"
        Me.btnUpdateFolioPan.UseVisualStyleBackColor = True
        '
        'lblTotal
        '
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(759, 23)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(74, 13)
        Me.lblTotal.TabIndex = 107
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(679, 23)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(74, 13)
        Me.Label27.TabIndex = 106
        Me.Label27.Text = "Total : "
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblShareSelected
        '
        Me.lblShareSelected.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShareSelected.Location = New System.Drawing.Point(946, 23)
        Me.lblShareSelected.Name = "lblShareSelected"
        Me.lblShareSelected.Size = New System.Drawing.Size(74, 13)
        Me.lblShareSelected.TabIndex = 94
        Me.lblShareSelected.Text = "0"
        Me.lblShareSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(827, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(113, 13)
        Me.Label8.TabIndex = 93
        Me.Label8.Text = "Selected : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDematPendId
        '
        Me.txtDematPendId.BackColor = System.Drawing.SystemColors.Window
        Me.txtDematPendId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDematPendId.Location = New System.Drawing.Point(719, 74)
        Me.txtDematPendId.MaxLength = 10
        Me.txtDematPendId.Name = "txtDematPendId"
        Me.txtDematPendId.ReadOnly = True
        Me.txtDematPendId.Size = New System.Drawing.Size(37, 27)
        Me.txtDematPendId.TabIndex = 92
        Me.txtDematPendId.TabStop = False
        Me.txtDematPendId.Visible = False
        '
        'txtSharesDrn
        '
        Me.txtSharesDrn.BackColor = System.Drawing.SystemColors.Window
        Me.txtSharesDrn.Location = New System.Drawing.Point(271, 317)
        Me.txtSharesDrn.MaxLength = 10
        Me.txtSharesDrn.Name = "txtSharesDrn"
        Me.txtSharesDrn.ReadOnly = True
        Me.txtSharesDrn.Size = New System.Drawing.Size(114, 27)
        Me.txtSharesDrn.TabIndex = 12
        Me.txtSharesDrn.TabStop = False
        '
        'txtDpId
        '
        Me.txtDpId.BackColor = System.Drawing.SystemColors.Window
        Me.txtDpId.Location = New System.Drawing.Point(271, 264)
        Me.txtDpId.MaxLength = 10
        Me.txtDpId.Name = "txtDpId"
        Me.txtDpId.Size = New System.Drawing.Size(114, 27)
        Me.txtDpId.TabIndex = 10
        Me.txtDpId.TabStop = False
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(217, 269)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 89
        Me.Label6.Text = "DP Id"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNameElect
        '
        Me.txtNameElect.BackColor = System.Drawing.SystemColors.Window
        Me.txtNameElect.Location = New System.Drawing.Point(101, 182)
        Me.txtNameElect.MaxLength = 10
        Me.txtNameElect.Name = "txtNameElect"
        Me.txtNameElect.ReadOnly = True
        Me.txtNameElect.Size = New System.Drawing.Size(284, 27)
        Me.txtNameElect.TabIndex = 8
        Me.txtNameElect.TabStop = False
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(6, 185)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 13)
        Me.Label7.TabIndex = 87
        Me.Label7.Text = "Name (Elect)"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtReqDate
        '
        Me.txtReqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqDate.Location = New System.Drawing.Point(101, 317)
        Me.txtReqDate.MaxLength = 10
        Me.txtReqDate.Name = "txtReqDate"
        Me.txtReqDate.ReadOnly = True
        Me.txtReqDate.Size = New System.Drawing.Size(114, 27)
        Me.txtReqDate.TabIndex = 11
        Me.txtReqDate.TabStop = False
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(-13, 324)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(108, 13)
        Me.Label4.TabIndex = 85
        Me.Label4.Text = "Request Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDepository
        '
        Me.txtDepository.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepository.ForeColor = System.Drawing.Color.Black
        Me.txtDepository.Location = New System.Drawing.Point(101, 264)
        Me.txtDepository.MaxLength = 10
        Me.txtDepository.Name = "txtDepository"
        Me.txtDepository.ReadOnly = True
        Me.txtDepository.Size = New System.Drawing.Size(114, 27)
        Me.txtDepository.TabIndex = 9
        Me.txtDepository.TabStop = False
        '
        'txtDrnNo
        '
        Me.txtDrnNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDrnNo.Location = New System.Drawing.Point(101, 155)
        Me.txtDrnNo.MaxLength = 32
        Me.txtDrnNo.Name = "txtDrnNo"
        Me.txtDrnNo.Size = New System.Drawing.Size(175, 27)
        Me.txtDrnNo.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 158)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "DRN No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtIsinId
        '
        Me.txtIsinId.BackColor = System.Drawing.SystemColors.Window
        Me.txtIsinId.Location = New System.Drawing.Point(674, 74)
        Me.txtIsinId.MaxLength = 0
        Me.txtIsinId.Name = "txtIsinId"
        Me.txtIsinId.ReadOnly = True
        Me.txtIsinId.Size = New System.Drawing.Size(39, 27)
        Me.txtIsinId.TabIndex = 79
        Me.txtIsinId.TabStop = False
        Me.txtIsinId.Visible = False
        '
        'txtCertNo
        '
        Me.txtCertNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertNo.Location = New System.Drawing.Point(462, 20)
        Me.txtCertNo.MaxLength = 0
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.Size = New System.Drawing.Size(193, 27)
        Me.txtCertNo.TabIndex = 5
        Me.txtCertNo.TabStop = False
        '
        'txtPanNo
        '
        Me.txtPanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo.Location = New System.Drawing.Point(101, 128)
        Me.txtPanNo.MaxLength = 10
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.ReadOnly = True
        Me.txtPanNo.Size = New System.Drawing.Size(198, 27)
        Me.txtPanNo.TabIndex = 4
        Me.txtPanNo.TabStop = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 131)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "PAN No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(391, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(74, 13)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "Certificate"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgvCert
        '
        Me.dgvCert.AllowUserToAddRows = False
        Me.dgvCert.AllowUserToDeleteRows = False
        Me.dgvCert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCert.Location = New System.Drawing.Point(394, 47)
        Me.dgvCert.Name = "dgvCert"
        Me.dgvCert.Size = New System.Drawing.Size(629, 129)
        Me.dgvCert.TabIndex = 6
        '
        'txtShareHolder
        '
        Me.txtShareHolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtShareHolder.Location = New System.Drawing.Point(101, 101)
        Me.txtShareHolder.MaxLength = 0
        Me.txtShareHolder.Name = "txtShareHolder"
        Me.txtShareHolder.ReadOnly = True
        Me.txtShareHolder.Size = New System.Drawing.Size(284, 27)
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
        Me.txtFolioNo.Size = New System.Drawing.Size(284, 27)
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
        Me.txtCompName.Size = New System.Drawing.Size(284, 27)
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
        Me.txtInwardNo.Size = New System.Drawing.Size(198, 27)
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
        'txtJoint1Name
        '
        Me.txtJoint1Name.BackColor = System.Drawing.SystemColors.Window
        Me.txtJoint1Name.Location = New System.Drawing.Point(101, 209)
        Me.txtJoint1Name.MaxLength = 10
        Me.txtJoint1Name.Name = "txtJoint1Name"
        Me.txtJoint1Name.ReadOnly = True
        Me.txtJoint1Name.Size = New System.Drawing.Size(284, 27)
        Me.txtJoint1Name.TabIndex = 8
        Me.txtJoint1Name.TabStop = False
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(6, 212)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 109
        Me.Label10.Text = "Joint1 Name"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtJoint2Name
        '
        Me.txtJoint2Name.BackColor = System.Drawing.SystemColors.Window
        Me.txtJoint2Name.Location = New System.Drawing.Point(101, 236)
        Me.txtJoint2Name.MaxLength = 10
        Me.txtJoint2Name.Name = "txtJoint2Name"
        Me.txtJoint2Name.ReadOnly = True
        Me.txtJoint2Name.Size = New System.Drawing.Size(284, 27)
        Me.txtJoint2Name.TabIndex = 8
        Me.txtJoint2Name.TabStop = False
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(6, 239)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 111
        Me.Label11.Text = "Joint2 Name"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(174, 320)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 91
        Me.Label5.Text = "Shares"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClientid
        '
        Me.txtClientid.BackColor = System.Drawing.SystemColors.Window
        Me.txtClientid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtClientid.Location = New System.Drawing.Point(548, 387)
        Me.txtClientid.MaxLength = 10
        Me.txtClientid.Name = "txtClientid"
        Me.txtClientid.ReadOnly = True
        Me.txtClientid.Size = New System.Drawing.Size(31, 27)
        Me.txtClientid.TabIndex = 114
        Me.txtClientid.TabStop = False
        Me.txtClientid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtClientid.Visible = False
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(729, 502)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 5
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(811, 502)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 6
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(893, 502)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 7
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(975, 502)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Picbox_sign
        '
        Me.Picbox_sign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Picbox_sign.Location = New System.Drawing.Point(624, 419)
        Me.Picbox_sign.Name = "Picbox_sign"
        Me.Picbox_sign.Size = New System.Drawing.Size(427, 58)
        Me.Picbox_sign.TabIndex = 14
        Me.Picbox_sign.TabStop = False
        '
        'lblnosignature
        '
        Me.lblnosignature.ForeColor = System.Drawing.Color.Red
        Me.lblnosignature.Location = New System.Drawing.Point(645, 440)
        Me.lblnosignature.Name = "lblnosignature"
        Me.lblnosignature.Size = New System.Drawing.Size(388, 13)
        Me.lblnosignature.TabIndex = 77
        Me.lblnosignature.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCertificateDemat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1063, 538)
        Me.Controls.Add(Me.lnkAddAttachment)
        Me.Controls.Add(Me.lblDocStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.dgvChklst)
        Me.Controls.Add(Me.grpHeader)
        Me.Controls.Add(Me.lblnosignature)
        Me.Controls.Add(Me.Picbox_sign)
        Me.Controls.Add(Me.txtClientid)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCertificateDemat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Certificate Demat"
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgvReason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvNameChange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picbox_sign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents dgvCert As System.Windows.Forms.DataGridView
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCertNo As System.Windows.Forms.TextBox
    Friend WithEvents txtIsinId As System.Windows.Forms.TextBox
    Friend WithEvents txtSharesDrn As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDpId As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNameElect As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtReqDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDepository As System.Windows.Forms.TextBox
    Friend WithEvents txtDrnNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDematPendId As System.Windows.Forms.TextBox
    Friend WithEvents lblShareSelected As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents dgvReason As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdateFolioPan As System.Windows.Forms.Button
    Friend WithEvents txtJoint1Name As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtJoint2Name As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgvNameChange As System.Windows.Forms.DataGridView
    Friend WithEvents ChkNameChng As System.Windows.Forms.CheckBox
    Friend WithEvents Picbox_sign As PictureBox
    Friend WithEvents lblnosignature As Label
    Friend WithEvents txtClientid As System.Windows.Forms.TextBox
    Friend WithEvents lblClientid As System.Windows.Forms.Label
    Friend WithEvents txtInputClientid As System.Windows.Forms.TextBox
    Friend WithEvents lblDepository As System.Windows.Forms.Label
End Class
