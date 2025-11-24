<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDepositoryTransfer
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
        Me.btnAutoSplit = New System.Windows.Forms.Button()
        Me.txtShareCount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpTransferDate = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblDocStatus = New System.Windows.Forms.Label()
        Me.lnkAddAttachment = New System.Windows.Forms.LinkLabel()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.dgvChklst = New System.Windows.Forms.DataGridView()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.grpHeader = New System.Windows.Forms.GroupBox()
        Me.txtToFolioId = New System.Windows.Forms.TextBox()
        Me.txtToDep = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtToFolioNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblShareCount = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.dgvCert = New System.Windows.Forms.DataGridView()
        Me.txtFromDepository = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFromFolioNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCompName = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpPropAddr.SuspendLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpPropAddr
        '
        Me.grpPropAddr.Controls.Add(Me.btnAutoSplit)
        Me.grpPropAddr.Controls.Add(Me.txtShareCount)
        Me.grpPropAddr.Controls.Add(Me.Label3)
        Me.grpPropAddr.Controls.Add(Me.dtpTransferDate)
        Me.grpPropAddr.Controls.Add(Me.Label13)
        Me.grpPropAddr.Controls.Add(Me.lblDocStatus)
        Me.grpPropAddr.Controls.Add(Me.lnkAddAttachment)
        Me.grpPropAddr.Controls.Add(Me.txtRemark)
        Me.grpPropAddr.Controls.Add(Me.Label23)
        Me.grpPropAddr.Controls.Add(Me.dgvChklst)
        Me.grpPropAddr.Controls.Add(Me.Label18)
        Me.grpPropAddr.Location = New System.Drawing.Point(13, 193)
        Me.grpPropAddr.Name = "grpPropAddr"
        Me.grpPropAddr.Size = New System.Drawing.Size(1199, 206)
        Me.grpPropAddr.TabIndex = 1
        Me.grpPropAddr.TabStop = False
        '
        'btnAutoSplit
        '
        Me.btnAutoSplit.Location = New System.Drawing.Point(280, 43)
        Me.btnAutoSplit.Name = "btnAutoSplit"
        Me.btnAutoSplit.Size = New System.Drawing.Size(105, 24)
        Me.btnAutoSplit.TabIndex = 2
        Me.btnAutoSplit.Text = "Find && Split"
        Me.btnAutoSplit.UseVisualStyleBackColor = True
        '
        'txtShareCount
        '
        Me.txtShareCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtShareCount.Location = New System.Drawing.Point(280, 20)
        Me.txtShareCount.MaxLength = 0
        Me.txtShareCount.Name = "txtShareCount"
        Me.txtShareCount.ReadOnly = True
        Me.txtShareCount.Size = New System.Drawing.Size(105, 21)
        Me.txtShareCount.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(212, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 125
        Me.Label3.Text = "Shares"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTransferDate
        '
        Me.dtpTransferDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpTransferDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTransferDate.Location = New System.Drawing.Point(101, 20)
        Me.dtpTransferDate.Name = "dtpTransferDate"
        Me.dtpTransferDate.Size = New System.Drawing.Size(105, 21)
        Me.dtpTransferDate.TabIndex = 0
        Me.dtpTransferDate.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(6, 22)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 17)
        Me.Label13.TabIndex = 123
        Me.Label13.Text = "Transfer Date"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(865, 182)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 6
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(1093, 182)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(100, 13)
        Me.lnkAddAttachment.TabIndex = 7
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(101, 70)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(284, 125)
        Me.txtRemark.TabIndex = 3
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(21, 70)
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
        Me.dgvChklst.Location = New System.Drawing.Point(394, 20)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(799, 152)
        Me.dgvChklst.TabIndex = 4
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(394, 182)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.txtToFolioId)
        Me.grpHeader.Controls.Add(Me.txtToDep)
        Me.grpHeader.Controls.Add(Me.Label1)
        Me.grpHeader.Controls.Add(Me.txtToFolioNo)
        Me.grpHeader.Controls.Add(Me.Label2)
        Me.grpHeader.Controls.Add(Me.lblTotal)
        Me.grpHeader.Controls.Add(Me.Label27)
        Me.grpHeader.Controls.Add(Me.lblShareCount)
        Me.grpHeader.Controls.Add(Me.Label28)
        Me.grpHeader.Controls.Add(Me.txtCertNo)
        Me.grpHeader.Controls.Add(Me.Label26)
        Me.grpHeader.Controls.Add(Me.dgvCert)
        Me.grpHeader.Controls.Add(Me.txtFromDepository)
        Me.grpHeader.Controls.Add(Me.Label21)
        Me.grpHeader.Controls.Add(Me.txtFromFolioNo)
        Me.grpHeader.Controls.Add(Me.Label22)
        Me.grpHeader.Controls.Add(Me.txtCompName)
        Me.grpHeader.Controls.Add(Me.Label20)
        Me.grpHeader.Controls.Add(Me.txtInwardNo)
        Me.grpHeader.Controls.Add(Me.Label19)
        Me.grpHeader.Location = New System.Drawing.Point(13, 0)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(1199, 187)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'txtToFolioId
        '
        Me.txtToFolioId.BackColor = System.Drawing.SystemColors.Window
        Me.txtToFolioId.Location = New System.Drawing.Point(713, 12)
        Me.txtToFolioId.MaxLength = 0
        Me.txtToFolioId.Name = "txtToFolioId"
        Me.txtToFolioId.ReadOnly = True
        Me.txtToFolioId.Size = New System.Drawing.Size(44, 21)
        Me.txtToFolioId.TabIndex = 90
        Me.txtToFolioId.TabStop = False
        Me.txtToFolioId.Visible = False
        '
        'txtToDep
        '
        Me.txtToDep.BackColor = System.Drawing.SystemColors.Window
        Me.txtToDep.Location = New System.Drawing.Point(101, 155)
        Me.txtToDep.MaxLength = 0
        Me.txtToDep.Name = "txtToDep"
        Me.txtToDep.ReadOnly = True
        Me.txtToDep.Size = New System.Drawing.Size(284, 21)
        Me.txtToDep.TabIndex = 5
        Me.txtToDep.TabStop = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 158)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "To Dep"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtToFolioNo
        '
        Me.txtToFolioNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtToFolioNo.Location = New System.Drawing.Point(101, 128)
        Me.txtToFolioNo.MaxLength = 0
        Me.txtToFolioNo.Name = "txtToFolioNo"
        Me.txtToFolioNo.ReadOnly = True
        Me.txtToFolioNo.Size = New System.Drawing.Size(284, 21)
        Me.txtToFolioNo.TabIndex = 4
        Me.txtToFolioNo.TabStop = False
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(21, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 88
        Me.Label2.Text = "To Folio"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(864, 23)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(74, 13)
        Me.lblTotal.TabIndex = 85
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(784, 23)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(74, 13)
        Me.Label27.TabIndex = 84
        Me.Label27.Text = "Total : "
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblShareCount
        '
        Me.lblShareCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShareCount.Location = New System.Drawing.Point(1110, 23)
        Me.lblShareCount.Name = "lblShareCount"
        Me.lblShareCount.Size = New System.Drawing.Size(74, 13)
        Me.lblShareCount.TabIndex = 83
        Me.lblShareCount.Text = "0"
        Me.lblShareCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(1030, 23)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(74, 13)
        Me.Label28.TabIndex = 82
        Me.Label28.Text = "Selected : "
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertNo
        '
        Me.txtCertNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertNo.Location = New System.Drawing.Point(461, 20)
        Me.txtCertNo.MaxLength = 0
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.Size = New System.Drawing.Size(196, 21)
        Me.txtCertNo.TabIndex = 6
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
        Me.dgvCert.Location = New System.Drawing.Point(391, 47)
        Me.dgvCert.Name = "dgvCert"
        Me.dgvCert.Size = New System.Drawing.Size(799, 129)
        Me.dgvCert.TabIndex = 7
        '
        'txtFromDepository
        '
        Me.txtFromDepository.BackColor = System.Drawing.SystemColors.Window
        Me.txtFromDepository.Location = New System.Drawing.Point(101, 101)
        Me.txtFromDepository.MaxLength = 0
        Me.txtFromDepository.Name = "txtFromDepository"
        Me.txtFromDepository.ReadOnly = True
        Me.txtFromDepository.Size = New System.Drawing.Size(284, 21)
        Me.txtFromDepository.TabIndex = 3
        Me.txtFromDepository.TabStop = False
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(6, 104)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(89, 13)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "From Dep"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFromFolioNo
        '
        Me.txtFromFolioNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtFromFolioNo.Location = New System.Drawing.Point(101, 74)
        Me.txtFromFolioNo.MaxLength = 0
        Me.txtFromFolioNo.Name = "txtFromFolioNo"
        Me.txtFromFolioNo.ReadOnly = True
        Me.txtFromFolioNo.Size = New System.Drawing.Size(284, 21)
        Me.txtFromFolioNo.TabIndex = 2
        Me.txtFromFolioNo.TabStop = False
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(21, 77)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(74, 13)
        Me.Label22.TabIndex = 74
        Me.Label22.Text = "From Folio"
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
        Me.btnSubmit.Location = New System.Drawing.Point(884, 405)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 3
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(966, 405)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 4
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(1048, 405)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 5
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1130, 405)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmDepositoryTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 437)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.grpHeader)
        Me.Controls.Add(Me.grpPropAddr)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDepositoryTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Depository Transfer"
        Me.grpPropAddr.ResumeLayout(False)
        Me.grpPropAddr.PerformLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpPropAddr As System.Windows.Forms.GroupBox
    Friend WithEvents dgvChklst As System.Windows.Forms.DataGridView
    Friend WithEvents grpHeader As System.Windows.Forms.GroupBox
    Friend WithEvents txtFromDepository As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtFromFolioNo As System.Windows.Forms.TextBox
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
    Friend WithEvents txtCertNo As System.Windows.Forms.TextBox
    Friend WithEvents lblShareCount As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtToDep As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtToFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtToFolioId As System.Windows.Forms.TextBox
    Friend WithEvents dtpTransferDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnAutoSplit As System.Windows.Forms.Button
    Friend WithEvents txtShareCount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
