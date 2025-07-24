<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPanChange
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
        Me.txtNewHolder3PanNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNewHolder2PanNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNewHolder1PanNo = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.grpCurrAddr = New System.Windows.Forms.GroupBox()
        Me.txtCurrHolder3PanNo = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCurrHolder2PanNo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCurrHolder1PanNo = New System.Windows.Forms.TextBox()
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
        Me.grpPropAddr.SuspendLayout()
        Me.grpCurrAddr.SuspendLayout()
        CType(Me.dgvChklst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpPropAddr
        '
        Me.grpPropAddr.Controls.Add(Me.txtNewHolder3PanNo)
        Me.grpPropAddr.Controls.Add(Me.Label2)
        Me.grpPropAddr.Controls.Add(Me.txtNewHolder2PanNo)
        Me.grpPropAddr.Controls.Add(Me.Label1)
        Me.grpPropAddr.Controls.Add(Me.txtNewHolder1PanNo)
        Me.grpPropAddr.Controls.Add(Me.lbl1)
        Me.grpPropAddr.Location = New System.Drawing.Point(12, 90)
        Me.grpPropAddr.Name = "grpPropAddr"
        Me.grpPropAddr.Size = New System.Drawing.Size(393, 107)
        Me.grpPropAddr.TabIndex = 1
        Me.grpPropAddr.TabStop = False
        Me.grpPropAddr.Text = "Proposed PAN Details"
        '
        'txtNewHolder3PanNo
        '
        Me.txtNewHolder3PanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewHolder3PanNo.Location = New System.Drawing.Point(107, 74)
        Me.txtNewHolder3PanNo.MaxLength = 64
        Me.txtNewHolder3PanNo.Name = "txtNewHolder3PanNo"
        Me.txtNewHolder3PanNo.Size = New System.Drawing.Size(263, 21)
        Me.txtNewHolder3PanNo.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "Holder3 PAN No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewHolder2PanNo
        '
        Me.txtNewHolder2PanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewHolder2PanNo.Location = New System.Drawing.Point(107, 47)
        Me.txtNewHolder2PanNo.MaxLength = 64
        Me.txtNewHolder2PanNo.Name = "txtNewHolder2PanNo"
        Me.txtNewHolder2PanNo.Size = New System.Drawing.Size(263, 21)
        Me.txtNewHolder2PanNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Holder2 PAN No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewHolder1PanNo
        '
        Me.txtNewHolder1PanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewHolder1PanNo.Location = New System.Drawing.Point(107, 20)
        Me.txtNewHolder1PanNo.MaxLength = 64
        Me.txtNewHolder1PanNo.Name = "txtNewHolder1PanNo"
        Me.txtNewHolder1PanNo.Size = New System.Drawing.Size(263, 21)
        Me.txtNewHolder1PanNo.TabIndex = 0
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(6, 23)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(95, 13)
        Me.lbl1.TabIndex = 68
        Me.lbl1.Text = "Holder1 PAN No"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpCurrAddr
        '
        Me.grpCurrAddr.Controls.Add(Me.txtCurrHolder3PanNo)
        Me.grpCurrAddr.Controls.Add(Me.Label15)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrHolder2PanNo)
        Me.grpCurrAddr.Controls.Add(Me.Label16)
        Me.grpCurrAddr.Controls.Add(Me.txtCurrHolder1PanNo)
        Me.grpCurrAddr.Controls.Add(Me.Label17)
        Me.grpCurrAddr.Enabled = False
        Me.grpCurrAddr.Location = New System.Drawing.Point(411, 90)
        Me.grpCurrAddr.Name = "grpCurrAddr"
        Me.grpCurrAddr.Size = New System.Drawing.Size(393, 107)
        Me.grpCurrAddr.TabIndex = 2
        Me.grpCurrAddr.TabStop = False
        Me.grpCurrAddr.Text = "Current PAN Details"
        '
        'txtCurrHolder3PanNo
        '
        Me.txtCurrHolder3PanNo.ForeColor = System.Drawing.Color.Red
        Me.txtCurrHolder3PanNo.Location = New System.Drawing.Point(107, 74)
        Me.txtCurrHolder3PanNo.MaxLength = 64
        Me.txtCurrHolder3PanNo.Name = "txtCurrHolder3PanNo"
        Me.txtCurrHolder3PanNo.Size = New System.Drawing.Size(263, 21)
        Me.txtCurrHolder3PanNo.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(6, 78)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(95, 13)
        Me.Label15.TabIndex = 72
        Me.Label15.Text = "Holder3 PAN No"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrHolder2PanNo
        '
        Me.txtCurrHolder2PanNo.ForeColor = System.Drawing.Color.Red
        Me.txtCurrHolder2PanNo.Location = New System.Drawing.Point(107, 47)
        Me.txtCurrHolder2PanNo.MaxLength = 64
        Me.txtCurrHolder2PanNo.Name = "txtCurrHolder2PanNo"
        Me.txtCurrHolder2PanNo.Size = New System.Drawing.Size(263, 21)
        Me.txtCurrHolder2PanNo.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 51)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(95, 13)
        Me.Label16.TabIndex = 70
        Me.Label16.Text = "Holder2 PAN No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrHolder1PanNo
        '
        Me.txtCurrHolder1PanNo.ForeColor = System.Drawing.Color.Red
        Me.txtCurrHolder1PanNo.Location = New System.Drawing.Point(107, 20)
        Me.txtCurrHolder1PanNo.MaxLength = 64
        Me.txtCurrHolder1PanNo.Name = "txtCurrHolder1PanNo"
        Me.txtCurrHolder1PanNo.Size = New System.Drawing.Size(263, 21)
        Me.txtCurrHolder1PanNo.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(6, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(95, 13)
        Me.Label17.TabIndex = 68
        Me.Label17.Text = "Holder1 PAN No"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvChklst
        '
        Me.dgvChklst.AllowUserToAddRows = False
        Me.dgvChklst.AllowUserToDeleteRows = False
        Me.dgvChklst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChklst.Location = New System.Drawing.Point(11, 203)
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
        Me.txtShareHolder.Location = New System.Drawing.Point(505, 47)
        Me.txtShareHolder.MaxLength = 0
        Me.txtShareHolder.Name = "txtShareHolder"
        Me.txtShareHolder.Size = New System.Drawing.Size(263, 21)
        Me.txtShareHolder.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(410, 50)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(89, 13)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "Share Holder"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(107, 47)
        Me.txtFolioNo.MaxLength = 0
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(263, 21)
        Me.txtFolioNo.TabIndex = 2
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(27, 50)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(74, 13)
        Me.Label22.TabIndex = 74
        Me.Label22.Text = "Folio No"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCompName
        '
        Me.txtCompName.Location = New System.Drawing.Point(505, 20)
        Me.txtCompName.MaxLength = 0
        Me.txtCompName.Name = "txtCompName"
        Me.txtCompName.Size = New System.Drawing.Size(263, 21)
        Me.txtCompName.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(425, 23)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 72
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Location = New System.Drawing.Point(107, 20)
        Me.txtInwardNo.MaxLength = 0
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(263, 21)
        Me.txtInwardNo.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(27, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 70
        Me.Label19.Text = "Inward No"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(518, 203)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(289, 88)
        Me.txtRemark.TabIndex = 8
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(438, 207)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 13)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Remark"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(489, 333)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(76, 24)
        Me.btnSubmit.TabIndex = 9
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.Location = New System.Drawing.Point(571, 333)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(76, 24)
        Me.btnReject.TabIndex = 10
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(653, 333)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(76, 24)
        Me.btnView.TabIndex = 11
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(735, 333)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lnkAddAttachment
        '
        Me.lnkAddAttachment.AutoSize = True
        Me.lnkAddAttachment.Location = New System.Drawing.Point(707, 308)
        Me.lnkAddAttachment.Name = "lnkAddAttachment"
        Me.lnkAddAttachment.Size = New System.Drawing.Size(100, 13)
        Me.lnkAddAttachment.TabIndex = 5
        Me.lnkAddAttachment.TabStop = True
        Me.lnkAddAttachment.Text = "Add Attachment"
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(435, 308)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 81
        Me.Label18.Text = "Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocStatus
        '
        Me.lblDocStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblDocStatus.Location = New System.Drawing.Point(515, 308)
        Me.lblDocStatus.Name = "lblDocStatus"
        Me.lblDocStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblDocStatus.TabIndex = 82
        Me.lblDocStatus.Text = "Status"
        Me.lblDocStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmPanChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 365)
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
        Me.Name = "frmPanChange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PAN Change"
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
    Friend WithEvents txtNewHolder3PanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNewHolder2PanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNewHolder1PanNo As System.Windows.Forms.TextBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents grpCurrAddr As System.Windows.Forms.GroupBox
    Friend WithEvents txtCurrHolder3PanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCurrHolder2PanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCurrHolder1PanNo As System.Windows.Forms.TextBox
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
