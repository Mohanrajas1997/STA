<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNameCorrection
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
        Me.grpPropName = New System.Windows.Forms.GroupBox()
        Me.txtNewSH3 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNewSH2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNewSH1 = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.grpCurrName = New System.Windows.Forms.GroupBox()
        Me.txtCurrSH3 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCurrSH2 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCurrSH1 = New System.Windows.Forms.TextBox()
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
        Me.grpPropName.SuspendLayout()
        Me.grpCurrName.SuspendLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpPropName
        '
        Me.grpPropName.Controls.Add(Me.txtNewSH3)
        Me.grpPropName.Controls.Add(Me.Label2)
        Me.grpPropName.Controls.Add(Me.txtNewSH2)
        Me.grpPropName.Controls.Add(Me.Label1)
        Me.grpPropName.Controls.Add(Me.txtNewSH1)
        Me.grpPropName.Controls.Add(Me.lbl1)
        Me.grpPropName.Location = New System.Drawing.Point(12, 90)
        Me.grpPropName.Name = "grpPropName"
        Me.grpPropName.Size = New System.Drawing.Size(393, 110)
        Me.grpPropName.TabIndex = 1
        Me.grpPropName.TabStop = False
        Me.grpPropName.Text = "Corrected Name"
        '
        'txtNewSH3
        '
        Me.txtNewSH3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewSH3.Location = New System.Drawing.Point(86, 74)
        Me.txtNewSH3.MaxLength = 64
        Me.txtNewSH3.Name = "txtNewSH3"
        Me.txtNewSH3.Size = New System.Drawing.Size(284, 21)
        Me.txtNewSH3.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "SH 3"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewSH2
        '
        Me.txtNewSH2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewSH2.Location = New System.Drawing.Point(86, 47)
        Me.txtNewSH2.MaxLength = 64
        Me.txtNewSH2.Name = "txtNewSH2"
        Me.txtNewSH2.Size = New System.Drawing.Size(284, 21)
        Me.txtNewSH2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "SH 2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewSH1
        '
        Me.txtNewSH1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewSH1.Location = New System.Drawing.Point(86, 20)
        Me.txtNewSH1.MaxLength = 64
        Me.txtNewSH1.Name = "txtNewSH1"
        Me.txtNewSH1.Size = New System.Drawing.Size(284, 21)
        Me.txtNewSH1.TabIndex = 0
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(6, 23)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(74, 13)
        Me.lbl1.TabIndex = 68
        Me.lbl1.Text = "SH 1"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpCurrName
        '
        Me.grpCurrName.Controls.Add(Me.txtCurrSH3)
        Me.grpCurrName.Controls.Add(Me.Label15)
        Me.grpCurrName.Controls.Add(Me.txtCurrSH2)
        Me.grpCurrName.Controls.Add(Me.Label16)
        Me.grpCurrName.Controls.Add(Me.txtCurrSH1)
        Me.grpCurrName.Controls.Add(Me.Label17)
        Me.grpCurrName.Enabled = False
        Me.grpCurrName.Location = New System.Drawing.Point(411, 90)
        Me.grpCurrName.Name = "grpCurrName"
        Me.grpCurrName.Size = New System.Drawing.Size(393, 110)
        Me.grpCurrName.TabIndex = 2
        Me.grpCurrName.TabStop = False
        Me.grpCurrName.Text = "Current Address"
        '
        'txtCurrSH3
        '
        Me.txtCurrSH3.ForeColor = System.Drawing.Color.Red
        Me.txtCurrSH3.Location = New System.Drawing.Point(86, 74)
        Me.txtCurrSH3.MaxLength = 64
        Me.txtCurrSH3.Name = "txtCurrSH3"
        Me.txtCurrSH3.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrSH3.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(6, 78)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 13)
        Me.Label15.TabIndex = 72
        Me.Label15.Text = "SH 3"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrSH2
        '
        Me.txtCurrSH2.ForeColor = System.Drawing.Color.Red
        Me.txtCurrSH2.Location = New System.Drawing.Point(86, 47)
        Me.txtCurrSH2.MaxLength = 64
        Me.txtCurrSH2.Name = "txtCurrSH2"
        Me.txtCurrSH2.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrSH2.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 51)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 13)
        Me.Label16.TabIndex = 70
        Me.Label16.Text = "SH 2"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrSH1
        '
        Me.txtCurrSH1.ForeColor = System.Drawing.Color.Red
        Me.txtCurrSH1.Location = New System.Drawing.Point(86, 20)
        Me.txtCurrSH1.MaxLength = 64
        Me.txtCurrSH1.Name = "txtCurrSH1"
        Me.txtCurrSH1.Size = New System.Drawing.Size(284, 21)
        Me.txtCurrSH1.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(6, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(74, 13)
        Me.Label17.TabIndex = 68
        Me.Label17.Text = "SH 1"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvChklst
        '
        Me.dgvChklst.AllowUserToAddRows = False
        Me.dgvChklst.AllowUserToDeleteRows = False
        Me.dgvChklst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChklst.Location = New System.Drawing.Point(11, 206)
        Me.dgvChklst.Name = "dgvChklst"
        Me.dgvChklst.Size = New System.Drawing.Size(394, 154)
        Me.dgvChklst.TabIndex = 4
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
        Me.txtRemark.Location = New System.Drawing.Point(497, 206)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(306, 88)
        Me.txtRemark.TabIndex = 8
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(417, 210)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 13)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Remark"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(481, 336)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 9
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(563, 336)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 10
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(645, 336)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 11
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(727, 336)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(703, 311)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(100, 13)
        Me.lnkAddAttachment.TabIndex = 5
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(421, 311)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 81
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(501, 311)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 82
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmNameCorrection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 370)
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
        Me.Controls.Add(Me.grpCurrName)
        Me.Controls.Add(Me.grpPropName)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNameCorrection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Name Correction"
        Me.grpPropName.ResumeLayout(False)
        Me.grpPropName.PerformLayout()
        Me.grpCurrName.ResumeLayout(False)
        Me.grpCurrName.PerformLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpPropName As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewSH3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNewSH2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNewSH1 As System.Windows.Forms.TextBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents grpCurrName As System.Windows.Forms.GroupBox
    Friend WithEvents txtCurrSH3 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCurrSH2 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCurrSH1 As System.Windows.Forms.TextBox
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
End Class
