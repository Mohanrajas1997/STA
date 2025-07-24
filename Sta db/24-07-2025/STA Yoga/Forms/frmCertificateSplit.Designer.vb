<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCertificateSplit
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
        Me.btnClear = New System.Windows.Forms.Button()
        Me.dgvSplitDist = New System.Windows.Forms.DataGridView()
        Me.dgvSplit = New System.Windows.Forms.DataGridView()
        Me.btnSplit = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSplit = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblShareCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        CType(Me.dgvSplitDist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(624, 391)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 14
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(951, 391)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(100, 13)
        Me.lnkAddAttachment.TabIndex = 15
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(624, 289)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(427, 99)
        Me.txtRemark.TabIndex = 11
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(544, 291)
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
        Me.dgvChklst.Location = New System.Drawing.Point(12, 289)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(517, 127)
        Me.dgvChklst.TabIndex = 12
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(544, 391)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.lblTotal)
        Me.grpHeader.Controls.Add(Me.Label27)
        Me.grpHeader.Controls.Add(Me.txtCertNo)
        Me.grpHeader.Controls.Add(Me.Label26)
        Me.grpHeader.Controls.Add(Me.txtPanNo)
        Me.grpHeader.Controls.Add(Me.Label4)
        Me.grpHeader.Controls.Add(Me.btnClear)
        Me.grpHeader.Controls.Add(Me.dgvSplitDist)
        Me.grpHeader.Controls.Add(Me.dgvSplit)
        Me.grpHeader.Controls.Add(Me.btnSplit)
        Me.grpHeader.Controls.Add(Me.Label3)
        Me.grpHeader.Controls.Add(Me.txtSplit)
        Me.grpHeader.Controls.Add(Me.Label1)
        Me.grpHeader.Controls.Add(Me.lblShareCount)
        Me.grpHeader.Controls.Add(Me.Label2)
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
        Me.grpHeader.Size = New System.Drawing.Size(1038, 283)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(944, 155)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(76, 24)
        Me.btnClear.TabIndex = 85
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'dgvSplitDist
        '
        Me.dgvSplitDist.AllowUserToAddRows = False
        Me.dgvSplitDist.AllowUserToDeleteRows = False
        Me.dgvSplitDist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSplitDist.Location = New System.Drawing.Point(394, 182)
        Me.dgvSplitDist.Name = "dgvSplitDist"
        Me.dgvSplitDist.ReadOnly = True
        Me.dgvSplitDist.Size = New System.Drawing.Size(626, 95)
        Me.dgvSplitDist.TabIndex = 84
        '
        'dgvSplit
        '
        Me.dgvSplit.AllowUserToAddRows = False
        Me.dgvSplit.AllowUserToDeleteRows = False
        Me.dgvSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSplit.Location = New System.Drawing.Point(9, 182)
        Me.dgvSplit.Name = "dgvSplit"
        Me.dgvSplit.ReadOnly = True
        Me.dgvSplit.Size = New System.Drawing.Size(376, 95)
        Me.dgvSplit.TabIndex = 83
        '
        'btnSplit
        '
        Me.btnSplit.Location = New System.Drawing.Point(864, 155)
        Me.btnSplit.Name = "btnSplit"
        Me.btnSplit.Size = New System.Drawing.Size(76, 24)
        Me.btnSplit.TabIndex = 82
        Me.btnSplit.Text = "Split"
        Me.btnSplit.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(688, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(147, 13)
        Me.Label3.TabIndex = 81
        Me.Label3.Text = "example : 5+10"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSplit
        '
        Me.txtSplit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSplit.Location = New System.Drawing.Point(471, 155)
        Me.txtSplit.MaxLength = 0
        Me.txtSplit.Name = "txtSplit"
        Me.txtSplit.Size = New System.Drawing.Size(196, 21)
        Me.txtSplit.TabIndex = 79
        Me.txtSplit.TabStop = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(376, 158)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Share Split"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblShareCount
        '
        Me.lblShareCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShareCount.Location = New System.Drawing.Point(946, 23)
        Me.lblShareCount.Name = "lblShareCount"
        Me.lblShareCount.Size = New System.Drawing.Size(74, 13)
        Me.lblShareCount.TabIndex = 78
        Me.lblShareCount.Text = "0"
        Me.lblShareCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(827, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "Selected : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvCert
        '
        Me.dgvCert.AllowUserToAddRows = False
        Me.dgvCert.AllowUserToDeleteRows = False
        Me.dgvCert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCert.Location = New System.Drawing.Point(391, 47)
        Me.dgvCert.Name = "dgvCert"
        Me.dgvCert.Size = New System.Drawing.Size(629, 102)
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
        Me.btnSubmit.Location = New System.Drawing.Point(729, 422)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 3
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(811, 422)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 4
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(893, 422)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 5
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(975, 422)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtPanNo
        '
        Me.txtPanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo.Location = New System.Drawing.Point(101, 128)
        Me.txtPanNo.MaxLength = 10
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.ReadOnly = True
        Me.txtPanNo.Size = New System.Drawing.Size(284, 21)
        Me.txtPanNo.TabIndex = 86
        Me.txtPanNo.TabStop = False
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 87
        Me.Label4.Text = "PAN No"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(749, 23)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(74, 13)
        Me.lblTotal.TabIndex = 105
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(669, 23)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(74, 13)
        Me.Label27.TabIndex = 104
        Me.Label27.Text = "Total : "
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertNo
        '
        Me.txtCertNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertNo.Location = New System.Drawing.Point(471, 20)
        Me.txtCertNo.MaxLength = 0
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.Size = New System.Drawing.Size(196, 21)
        Me.txtCertNo.TabIndex = 103
        '
        'Label26
        '
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(391, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(74, 13)
        Me.Label26.TabIndex = 102
        Me.Label26.Text = "Certificate"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCertificateSplit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1063, 458)
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
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmCertificateSplit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Split of Shares"
        CType(Me.dgvChklst,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpHeader.ResumeLayout(false)
        Me.grpHeader.PerformLayout
        CType(Me.dgvSplitDist,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgvSplit,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgvCert,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

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
    Friend WithEvents lblShareCount As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSplit As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvSplitDist As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSplit As System.Windows.Forms.DataGridView
    Friend WithEvents btnSplit As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtPanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtCertNo As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
End Class
