<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUploadStatus
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
        Me.grpHeader = New System.Windows.Forms.GroupBox()
        Me.txtUploadDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtObjxTo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtObjxFrom = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCertTo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCertFrom = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTranTo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTranFrom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtObjxCount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCertCount = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtTranCount = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCompName = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtUploadNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.dtpApprovedDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.grpHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.txtUploadDate)
        Me.grpHeader.Controls.Add(Me.Label8)
        Me.grpHeader.Controls.Add(Me.txtObjxTo)
        Me.grpHeader.Controls.Add(Me.Label6)
        Me.grpHeader.Controls.Add(Me.txtObjxFrom)
        Me.grpHeader.Controls.Add(Me.Label7)
        Me.grpHeader.Controls.Add(Me.txtCertTo)
        Me.grpHeader.Controls.Add(Me.Label4)
        Me.grpHeader.Controls.Add(Me.txtCertFrom)
        Me.grpHeader.Controls.Add(Me.Label5)
        Me.grpHeader.Controls.Add(Me.txtTranTo)
        Me.grpHeader.Controls.Add(Me.Label3)
        Me.grpHeader.Controls.Add(Me.txtTranFrom)
        Me.grpHeader.Controls.Add(Me.Label2)
        Me.grpHeader.Controls.Add(Me.txtObjxCount)
        Me.grpHeader.Controls.Add(Me.Label1)
        Me.grpHeader.Controls.Add(Me.txtCertCount)
        Me.grpHeader.Controls.Add(Me.Label21)
        Me.grpHeader.Controls.Add(Me.txtTranCount)
        Me.grpHeader.Controls.Add(Me.Label22)
        Me.grpHeader.Controls.Add(Me.txtCompName)
        Me.grpHeader.Controls.Add(Me.Label20)
        Me.grpHeader.Controls.Add(Me.txtUploadNo)
        Me.grpHeader.Controls.Add(Me.Label19)
        Me.grpHeader.Location = New System.Drawing.Point(12, 6)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(643, 160)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        '
        'txtUploadDate
        '
        Me.txtUploadDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtUploadDate.Location = New System.Drawing.Point(418, 20)
        Me.txtUploadDate.MaxLength = 0
        Me.txtUploadDate.Name = "txtUploadDate"
        Me.txtUploadDate.ReadOnly = True
        Me.txtUploadDate.Size = New System.Drawing.Size(207, 21)
        Me.txtUploadDate.TabIndex = 1
        Me.txtUploadDate.TabStop = False
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(334, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 92
        Me.Label8.Text = "Upload Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtObjxTo
        '
        Me.txtObjxTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtObjxTo.Location = New System.Drawing.Point(515, 128)
        Me.txtObjxTo.MaxLength = 0
        Me.txtObjxTo.Name = "txtObjxTo"
        Me.txtObjxTo.ReadOnly = True
        Me.txtObjxTo.Size = New System.Drawing.Size(110, 21)
        Me.txtObjxTo.TabIndex = 11
        Me.txtObjxTo.TabStop = False
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(458, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "To"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtObjxFrom
        '
        Me.txtObjxFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtObjxFrom.Location = New System.Drawing.Point(334, 128)
        Me.txtObjxFrom.MaxLength = 0
        Me.txtObjxFrom.Name = "txtObjxFrom"
        Me.txtObjxFrom.ReadOnly = True
        Me.txtObjxFrom.Size = New System.Drawing.Size(110, 21)
        Me.txtObjxFrom.TabIndex = 10
        Me.txtObjxFrom.TabStop = False
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(277, 131)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 88
        Me.Label7.Text = "From"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertTo
        '
        Me.txtCertTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertTo.Location = New System.Drawing.Point(515, 101)
        Me.txtCertTo.MaxLength = 0
        Me.txtCertTo.Name = "txtCertTo"
        Me.txtCertTo.ReadOnly = True
        Me.txtCertTo.Size = New System.Drawing.Size(110, 21)
        Me.txtCertTo.TabIndex = 8
        Me.txtCertTo.TabStop = False
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(458, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "To"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertFrom
        '
        Me.txtCertFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertFrom.Location = New System.Drawing.Point(334, 101)
        Me.txtCertFrom.MaxLength = 0
        Me.txtCertFrom.Name = "txtCertFrom"
        Me.txtCertFrom.ReadOnly = True
        Me.txtCertFrom.Size = New System.Drawing.Size(110, 21)
        Me.txtCertFrom.TabIndex = 7
        Me.txtCertFrom.TabStop = False
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(277, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 84
        Me.Label5.Text = "From"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranTo
        '
        Me.txtTranTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTranTo.Location = New System.Drawing.Point(515, 74)
        Me.txtTranTo.MaxLength = 0
        Me.txtTranTo.Name = "txtTranTo"
        Me.txtTranTo.ReadOnly = True
        Me.txtTranTo.Size = New System.Drawing.Size(110, 21)
        Me.txtTranTo.TabIndex = 5
        Me.txtTranTo.TabStop = False
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(458, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranFrom
        '
        Me.txtTranFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtTranFrom.Location = New System.Drawing.Point(334, 74)
        Me.txtTranFrom.MaxLength = 0
        Me.txtTranFrom.Name = "txtTranFrom"
        Me.txtTranFrom.ReadOnly = True
        Me.txtTranFrom.Size = New System.Drawing.Size(110, 21)
        Me.txtTranFrom.TabIndex = 4
        Me.txtTranFrom.TabStop = False
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(277, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 80
        Me.Label2.Text = "From"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtObjxCount
        '
        Me.txtObjxCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtObjxCount.Location = New System.Drawing.Point(121, 128)
        Me.txtObjxCount.MaxLength = 0
        Me.txtObjxCount.Name = "txtObjxCount"
        Me.txtObjxCount.ReadOnly = True
        Me.txtObjxCount.Size = New System.Drawing.Size(110, 21)
        Me.txtObjxCount.TabIndex = 9
        Me.txtObjxCount.TabStop = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 131)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "Objection Count"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertCount
        '
        Me.txtCertCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertCount.Location = New System.Drawing.Point(121, 101)
        Me.txtCertCount.MaxLength = 0
        Me.txtCertCount.Name = "txtCertCount"
        Me.txtCertCount.ReadOnly = True
        Me.txtCertCount.Size = New System.Drawing.Size(110, 21)
        Me.txtCertCount.TabIndex = 6
        Me.txtCertCount.TabStop = False
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(6, 104)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(109, 13)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "Certificate Count"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranCount
        '
        Me.txtTranCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTranCount.Location = New System.Drawing.Point(121, 74)
        Me.txtTranCount.MaxLength = 0
        Me.txtTranCount.Name = "txtTranCount"
        Me.txtTranCount.ReadOnly = True
        Me.txtTranCount.Size = New System.Drawing.Size(110, 21)
        Me.txtTranCount.TabIndex = 3
        Me.txtTranCount.TabStop = False
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(21, 77)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(94, 13)
        Me.Label22.TabIndex = 74
        Me.Label22.Text = "Transfer Count"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCompName
        '
        Me.txtCompName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompName.Location = New System.Drawing.Point(121, 47)
        Me.txtCompName.MaxLength = 0
        Me.txtCompName.Name = "txtCompName"
        Me.txtCompName.ReadOnly = True
        Me.txtCompName.Size = New System.Drawing.Size(504, 21)
        Me.txtCompName.TabIndex = 2
        Me.txtCompName.TabStop = False
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(41, 50)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 72
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUploadNo
        '
        Me.txtUploadNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtUploadNo.Location = New System.Drawing.Point(121, 20)
        Me.txtUploadNo.MaxLength = 0
        Me.txtUploadNo.Name = "txtUploadNo"
        Me.txtUploadNo.ReadOnly = True
        Me.txtUploadNo.Size = New System.Drawing.Size(207, 21)
        Me.txtUploadNo.TabIndex = 0
        Me.txtUploadNo.TabStop = False
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(41, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 70
        Me.Label19.Text = "Upload No"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemark.Location = New System.Drawing.Point(133, 172)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(522, 62)
        Me.txtRemark.TabIndex = 1
        Me.txtRemark.TabStop = False
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(53, 175)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 13)
        Me.Label9.TabIndex = 74
        Me.Label9.Text = "Remark"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(581, 240)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(425, 240)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(72, 24)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(503, 240)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(72, 24)
        Me.btnView.TabIndex = 4
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'dtpApprovedDate
        '
        Me.dtpApprovedDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpApprovedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApprovedDate.Location = New System.Drawing.Point(133, 240)
        Me.dtpApprovedDate.Name = "dtpApprovedDate"
        Me.dtpApprovedDate.Size = New System.Drawing.Size(105, 21)
        Me.dtpApprovedDate.TabIndex = 2
        Me.dtpApprovedDate.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(12, 242)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 17)
        Me.Label10.TabIndex = 117
        Me.Label10.Text = "Approved Date"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmUploadStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 272)
        Me.Controls.Add(Me.dtpApprovedDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.grpHeader)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUploadStatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Upload Status"
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpHeader As System.Windows.Forms.GroupBox
    Friend WithEvents txtUploadDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtObjxTo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtObjxFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCertTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCertFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTranTo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTranFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtObjxCount As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCertCount As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtTranCount As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtCompName As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtUploadNo As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents dtpApprovedDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
