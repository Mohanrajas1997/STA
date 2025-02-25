<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCertificateTransposition
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
        Me.txtCertId = New System.Windows.Forms.TextBox()
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
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpProposed = New System.Windows.Forms.GroupBox()
        Me.btnSwapTwoAndThree = New System.Windows.Forms.Button()
        Me.btnSwapOneAndTwo = New System.Windows.Forms.Button()
        Me.txtNewPanNo3 = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtNewFHName3 = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtNewHolder3 = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtNewPanNo2 = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtNewFHName2 = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtNewHolder2 = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtNewPanNo1 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtNewFHName1 = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtNewHolder1 = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.grpCurrent = New System.Windows.Forms.GroupBox()
        Me.txtCurrPanNo3 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCurrFHName3 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCurrHolder3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCurrPanNo2 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCurrFHName2 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCurrHolder2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCurrPanNo1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCurrFHName1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCurrHolder1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProposed.SuspendLayout()
        Me.grpCurrent.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(624, 500)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 14
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(951, 500)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(100, 13)
        Me.lnkAddAttachment.TabIndex = 15
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(624, 423)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(427, 70)
        Me.txtRemark.TabIndex = 11
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(544, 425)
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
        Me.dgvChklst.Location = New System.Drawing.Point(12, 423)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(517, 124)
        Me.dgvChklst.TabIndex = 12
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(544, 500)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.txtCertId)
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
        Me.grpHeader.Location = New System.Drawing.Point(13, 0)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(1038, 136)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'txtCertId
        '
        Me.txtCertId.Location = New System.Drawing.Point(471, 15)
        Me.txtCertId.MaxLength = 0
        Me.txtCertId.Name = "txtCertId"
        Me.txtCertId.Size = New System.Drawing.Size(45, 21)
        Me.txtCertId.TabIndex = 5
        Me.txtCertId.Visible = False
        '
        'Label26
        '
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(391, 20)
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
        Me.dgvCert.Location = New System.Drawing.Point(391, 36)
        Me.dgvCert.Name = "dgvCert"
        Me.dgvCert.Size = New System.Drawing.Size(629, 86)
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
        Me.btnSubmit.Location = New System.Drawing.Point(729, 523)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 3
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(811, 523)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 4
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(893, 523)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 5
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(975, 523)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpProposed
        '
        Me.grpProposed.Controls.Add(Me.btnSwapTwoAndThree)
        Me.grpProposed.Controls.Add(Me.btnSwapOneAndTwo)
        Me.grpProposed.Controls.Add(Me.txtNewPanNo3)
        Me.grpProposed.Controls.Add(Me.Label27)
        Me.grpProposed.Controls.Add(Me.txtNewFHName3)
        Me.grpProposed.Controls.Add(Me.Label28)
        Me.grpProposed.Controls.Add(Me.txtNewHolder3)
        Me.grpProposed.Controls.Add(Me.Label29)
        Me.grpProposed.Controls.Add(Me.txtNewPanNo2)
        Me.grpProposed.Controls.Add(Me.Label30)
        Me.grpProposed.Controls.Add(Me.txtNewFHName2)
        Me.grpProposed.Controls.Add(Me.Label31)
        Me.grpProposed.Controls.Add(Me.txtNewHolder2)
        Me.grpProposed.Controls.Add(Me.Label32)
        Me.grpProposed.Controls.Add(Me.txtNewPanNo1)
        Me.grpProposed.Controls.Add(Me.Label33)
        Me.grpProposed.Controls.Add(Me.txtNewFHName1)
        Me.grpProposed.Controls.Add(Me.Label34)
        Me.grpProposed.Controls.Add(Me.txtNewHolder1)
        Me.grpProposed.Controls.Add(Me.Label35)
        Me.grpProposed.Location = New System.Drawing.Point(13, 142)
        Me.grpProposed.Name = "grpProposed"
        Me.grpProposed.Size = New System.Drawing.Size(516, 273)
        Me.grpProposed.TabIndex = 7
        Me.grpProposed.TabStop = False
        Me.grpProposed.Text = "Proposed"
        '
        'btnSwapTwoAndThree
        '
        Me.btnSwapTwoAndThree.Location = New System.Drawing.Point(389, 120)
        Me.btnSwapTwoAndThree.Name = "btnSwapTwoAndThree"
        Me.btnSwapTwoAndThree.Size = New System.Drawing.Size(108, 94)
        Me.btnSwapTwoAndThree.TabIndex = 141
        Me.btnSwapTwoAndThree.Text = "Swap " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Holder 2 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Holder 3"
        Me.btnSwapTwoAndThree.UseVisualStyleBackColor = True
        '
        'btnSwapOneAndTwo
        '
        Me.btnSwapOneAndTwo.Location = New System.Drawing.Point(389, 20)
        Me.btnSwapOneAndTwo.Name = "btnSwapOneAndTwo"
        Me.btnSwapOneAndTwo.Size = New System.Drawing.Size(108, 94)
        Me.btnSwapOneAndTwo.TabIndex = 140
        Me.btnSwapOneAndTwo.Text = "Swap " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Holder 1 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Holder 2"
        Me.btnSwapOneAndTwo.UseVisualStyleBackColor = True
        '
        'txtNewPanNo3
        '
        Me.txtNewPanNo3.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewPanNo3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewPanNo3.Location = New System.Drawing.Point(89, 236)
        Me.txtNewPanNo3.MaxLength = 10
        Me.txtNewPanNo3.Name = "txtNewPanNo3"
        Me.txtNewPanNo3.ReadOnly = True
        Me.txtNewPanNo3.Size = New System.Drawing.Size(284, 21)
        Me.txtNewPanNo3.TabIndex = 8
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(9, 239)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(74, 13)
        Me.Label27.TabIndex = 139
        Me.Label27.Text = "PAN3"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewFHName3
        '
        Me.txtNewFHName3.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewFHName3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewFHName3.Location = New System.Drawing.Point(89, 209)
        Me.txtNewFHName3.MaxLength = 128
        Me.txtNewFHName3.Name = "txtNewFHName3"
        Me.txtNewFHName3.ReadOnly = True
        Me.txtNewFHName3.Size = New System.Drawing.Size(284, 21)
        Me.txtNewFHName3.TabIndex = 7
        '
        'Label28
        '
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(9, 212)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(74, 13)
        Me.Label28.TabIndex = 138
        Me.Label28.Text = "F/H Name3"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewHolder3
        '
        Me.txtNewHolder3.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewHolder3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewHolder3.Location = New System.Drawing.Point(89, 182)
        Me.txtNewHolder3.MaxLength = 128
        Me.txtNewHolder3.Name = "txtNewHolder3"
        Me.txtNewHolder3.ReadOnly = True
        Me.txtNewHolder3.Size = New System.Drawing.Size(284, 21)
        Me.txtNewHolder3.TabIndex = 6
        '
        'Label29
        '
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(9, 185)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(74, 13)
        Me.Label29.TabIndex = 137
        Me.Label29.Text = "Holder3"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewPanNo2
        '
        Me.txtNewPanNo2.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewPanNo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewPanNo2.Location = New System.Drawing.Point(89, 155)
        Me.txtNewPanNo2.MaxLength = 10
        Me.txtNewPanNo2.Name = "txtNewPanNo2"
        Me.txtNewPanNo2.ReadOnly = True
        Me.txtNewPanNo2.Size = New System.Drawing.Size(284, 21)
        Me.txtNewPanNo2.TabIndex = 5
        '
        'Label30
        '
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(9, 158)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(74, 13)
        Me.Label30.TabIndex = 136
        Me.Label30.Text = "PAN2"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewFHName2
        '
        Me.txtNewFHName2.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewFHName2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewFHName2.Location = New System.Drawing.Point(89, 128)
        Me.txtNewFHName2.MaxLength = 128
        Me.txtNewFHName2.Name = "txtNewFHName2"
        Me.txtNewFHName2.ReadOnly = True
        Me.txtNewFHName2.Size = New System.Drawing.Size(284, 21)
        Me.txtNewFHName2.TabIndex = 4
        '
        'Label31
        '
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(9, 131)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(74, 13)
        Me.Label31.TabIndex = 135
        Me.Label31.Text = "F/H Name2"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewHolder2
        '
        Me.txtNewHolder2.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewHolder2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewHolder2.Location = New System.Drawing.Point(89, 101)
        Me.txtNewHolder2.MaxLength = 128
        Me.txtNewHolder2.Name = "txtNewHolder2"
        Me.txtNewHolder2.ReadOnly = True
        Me.txtNewHolder2.Size = New System.Drawing.Size(284, 21)
        Me.txtNewHolder2.TabIndex = 3
        '
        'Label32
        '
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(9, 104)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(74, 13)
        Me.Label32.TabIndex = 134
        Me.Label32.Text = "Holder2"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewPanNo1
        '
        Me.txtNewPanNo1.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewPanNo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewPanNo1.Location = New System.Drawing.Point(89, 74)
        Me.txtNewPanNo1.MaxLength = 10
        Me.txtNewPanNo1.Name = "txtNewPanNo1"
        Me.txtNewPanNo1.ReadOnly = True
        Me.txtNewPanNo1.Size = New System.Drawing.Size(284, 21)
        Me.txtNewPanNo1.TabIndex = 2
        '
        'Label33
        '
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(9, 77)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(74, 13)
        Me.Label33.TabIndex = 133
        Me.Label33.Text = "PAN1"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewFHName1
        '
        Me.txtNewFHName1.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewFHName1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewFHName1.Location = New System.Drawing.Point(89, 47)
        Me.txtNewFHName1.MaxLength = 128
        Me.txtNewFHName1.Name = "txtNewFHName1"
        Me.txtNewFHName1.ReadOnly = True
        Me.txtNewFHName1.Size = New System.Drawing.Size(284, 21)
        Me.txtNewFHName1.TabIndex = 1
        '
        'Label34
        '
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(9, 50)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(74, 13)
        Me.Label34.TabIndex = 132
        Me.Label34.Text = "F/H Name1"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewHolder1
        '
        Me.txtNewHolder1.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewHolder1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewHolder1.Location = New System.Drawing.Point(89, 20)
        Me.txtNewHolder1.MaxLength = 128
        Me.txtNewHolder1.Name = "txtNewHolder1"
        Me.txtNewHolder1.ReadOnly = True
        Me.txtNewHolder1.Size = New System.Drawing.Size(284, 21)
        Me.txtNewHolder1.TabIndex = 0
        '
        'Label35
        '
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(9, 23)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(74, 13)
        Me.Label35.TabIndex = 131
        Me.Label35.Text = "Holder1"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpCurrent
        '
        Me.grpCurrent.Controls.Add(Me.txtCurrPanNo3)
        Me.grpCurrent.Controls.Add(Me.Label1)
        Me.grpCurrent.Controls.Add(Me.txtCurrFHName3)
        Me.grpCurrent.Controls.Add(Me.Label2)
        Me.grpCurrent.Controls.Add(Me.txtCurrHolder3)
        Me.grpCurrent.Controls.Add(Me.Label3)
        Me.grpCurrent.Controls.Add(Me.txtCurrPanNo2)
        Me.grpCurrent.Controls.Add(Me.Label4)
        Me.grpCurrent.Controls.Add(Me.txtCurrFHName2)
        Me.grpCurrent.Controls.Add(Me.Label5)
        Me.grpCurrent.Controls.Add(Me.txtCurrHolder2)
        Me.grpCurrent.Controls.Add(Me.Label6)
        Me.grpCurrent.Controls.Add(Me.txtCurrPanNo1)
        Me.grpCurrent.Controls.Add(Me.Label7)
        Me.grpCurrent.Controls.Add(Me.txtCurrFHName1)
        Me.grpCurrent.Controls.Add(Me.Label8)
        Me.grpCurrent.Controls.Add(Me.txtCurrHolder1)
        Me.grpCurrent.Controls.Add(Me.Label9)
        Me.grpCurrent.Location = New System.Drawing.Point(535, 142)
        Me.grpCurrent.Name = "grpCurrent"
        Me.grpCurrent.Size = New System.Drawing.Size(516, 273)
        Me.grpCurrent.TabIndex = 16
        Me.grpCurrent.TabStop = False
        Me.grpCurrent.Text = "Current"
        '
        'txtCurrPanNo3
        '
        Me.txtCurrPanNo3.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrPanNo3.ForeColor = System.Drawing.Color.Black
        Me.txtCurrPanNo3.Location = New System.Drawing.Point(89, 236)
        Me.txtCurrPanNo3.MaxLength = 10
        Me.txtCurrPanNo3.Name = "txtCurrPanNo3"
        Me.txtCurrPanNo3.ReadOnly = True
        Me.txtCurrPanNo3.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrPanNo3.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(9, 239)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 139
        Me.Label1.Text = "PAN3"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrFHName3
        '
        Me.txtCurrFHName3.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrFHName3.ForeColor = System.Drawing.Color.Black
        Me.txtCurrFHName3.Location = New System.Drawing.Point(89, 209)
        Me.txtCurrFHName3.MaxLength = 128
        Me.txtCurrFHName3.Name = "txtCurrFHName3"
        Me.txtCurrFHName3.ReadOnly = True
        Me.txtCurrFHName3.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrFHName3.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(9, 212)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 138
        Me.Label2.Text = "F/H Name3"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrHolder3
        '
        Me.txtCurrHolder3.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrHolder3.ForeColor = System.Drawing.Color.Black
        Me.txtCurrHolder3.Location = New System.Drawing.Point(89, 182)
        Me.txtCurrHolder3.MaxLength = 128
        Me.txtCurrHolder3.Name = "txtCurrHolder3"
        Me.txtCurrHolder3.ReadOnly = True
        Me.txtCurrHolder3.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrHolder3.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(9, 185)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 137
        Me.Label3.Text = "Holder3"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrPanNo2
        '
        Me.txtCurrPanNo2.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrPanNo2.ForeColor = System.Drawing.Color.Black
        Me.txtCurrPanNo2.Location = New System.Drawing.Point(89, 155)
        Me.txtCurrPanNo2.MaxLength = 10
        Me.txtCurrPanNo2.Name = "txtCurrPanNo2"
        Me.txtCurrPanNo2.ReadOnly = True
        Me.txtCurrPanNo2.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrPanNo2.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(9, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "PAN2"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrFHName2
        '
        Me.txtCurrFHName2.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrFHName2.ForeColor = System.Drawing.Color.Black
        Me.txtCurrFHName2.Location = New System.Drawing.Point(89, 128)
        Me.txtCurrFHName2.MaxLength = 128
        Me.txtCurrFHName2.Name = "txtCurrFHName2"
        Me.txtCurrFHName2.ReadOnly = True
        Me.txtCurrFHName2.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrFHName2.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(9, 131)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 135
        Me.Label5.Text = "F/H Name2"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrHolder2
        '
        Me.txtCurrHolder2.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrHolder2.ForeColor = System.Drawing.Color.Black
        Me.txtCurrHolder2.Location = New System.Drawing.Point(89, 101)
        Me.txtCurrHolder2.MaxLength = 128
        Me.txtCurrHolder2.Name = "txtCurrHolder2"
        Me.txtCurrHolder2.ReadOnly = True
        Me.txtCurrHolder2.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrHolder2.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(9, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 134
        Me.Label6.Text = "Holder2"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrPanNo1
        '
        Me.txtCurrPanNo1.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrPanNo1.ForeColor = System.Drawing.Color.Black
        Me.txtCurrPanNo1.Location = New System.Drawing.Point(89, 74)
        Me.txtCurrPanNo1.MaxLength = 10
        Me.txtCurrPanNo1.Name = "txtCurrPanNo1"
        Me.txtCurrPanNo1.ReadOnly = True
        Me.txtCurrPanNo1.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrPanNo1.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(9, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 133
        Me.Label7.Text = "PAN1"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrFHName1
        '
        Me.txtCurrFHName1.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrFHName1.ForeColor = System.Drawing.Color.Black
        Me.txtCurrFHName1.Location = New System.Drawing.Point(89, 47)
        Me.txtCurrFHName1.MaxLength = 128
        Me.txtCurrFHName1.Name = "txtCurrFHName1"
        Me.txtCurrFHName1.ReadOnly = True
        Me.txtCurrFHName1.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrFHName1.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(9, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 132
        Me.Label8.Text = "F/H Name1"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrHolder1
        '
        Me.txtCurrHolder1.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrHolder1.ForeColor = System.Drawing.Color.Black
        Me.txtCurrHolder1.Location = New System.Drawing.Point(89, 20)
        Me.txtCurrHolder1.MaxLength = 128
        Me.txtCurrHolder1.Name = "txtCurrHolder1"
        Me.txtCurrHolder1.ReadOnly = True
        Me.txtCurrHolder1.Size = New System.Drawing.Size(410, 21)
        Me.txtCurrHolder1.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(9, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 13)
        Me.Label9.TabIndex = 131
        Me.Label9.Text = "Holder1"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCertificateTransposition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1063, 559)
        Me.Controls.Add(Me.grpCurrent)
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
        Me.Controls.Add(Me.grpProposed)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCertificateTransposition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Certificate Transposition"
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProposed.ResumeLayout(False)
        Me.grpProposed.PerformLayout()
        Me.grpCurrent.ResumeLayout(False)
        Me.grpCurrent.PerformLayout()
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
    Friend WithEvents txtCertId As System.Windows.Forms.TextBox
    Friend WithEvents grpProposed As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewPanNo3 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtNewFHName3 As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtNewHolder3 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtNewPanNo2 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtNewFHName2 As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtNewHolder2 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtNewPanNo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtNewFHName1 As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtNewHolder1 As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents grpCurrent As System.Windows.Forms.GroupBox
    Friend WithEvents txtCurrPanNo3 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCurrFHName3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCurrHolder3 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCurrPanNo2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCurrFHName2 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCurrHolder2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCurrPanNo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCurrFHName1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCurrHolder1 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSwapTwoAndThree As System.Windows.Forms.Button
    Friend WithEvents btnSwapOneAndTwo As System.Windows.Forms.Button
End Class
