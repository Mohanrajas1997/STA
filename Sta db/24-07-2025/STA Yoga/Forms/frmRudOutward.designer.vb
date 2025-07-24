<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRudOutward
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Txtsubtype = New System.Windows.Forms.TextBox()
        Me.Txtdoctype = New System.Windows.Forms.TextBox()
        Me.Txtaddress = New System.Windows.Forms.RichTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txtawbno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSearchInward = New System.Windows.Forms.Button()
        Me.CboRecvby = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCertificateNo = New System.Windows.Forms.TextBox()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpRcvdDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCourier = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbl2 = New System.Windows.Forms.Label()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.Txtsubtype)
        Me.pnlMain.Controls.Add(Me.Txtdoctype)
        Me.pnlMain.Controls.Add(Me.Txtaddress)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.Txtawbno)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.btnSearchInward)
        Me.pnlMain.Controls.Add(Me.CboRecvby)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.txtReason)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.txtCertificateNo)
        Me.pnlMain.Controls.Add(Me.txtFolioNo)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.dtpRcvdDate)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.cboCourier)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.lbl2)
        Me.pnlMain.Controls.Add(Me.txtInwardNo)
        Me.pnlMain.Controls.Add(Me.lbl1)
        Me.pnlMain.Controls.Add(Me.txtId)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(6, 6)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(608, 245)
        Me.pnlMain.TabIndex = 0
        '
        'Txtsubtype
        '
        Me.Txtsubtype.Enabled = False
        Me.Txtsubtype.Location = New System.Drawing.Point(408, 37)
        Me.Txtsubtype.MaxLength = 32
        Me.Txtsubtype.Name = "Txtsubtype"
        Me.Txtsubtype.Size = New System.Drawing.Size(183, 21)
        Me.Txtsubtype.TabIndex = 106
        '
        'Txtdoctype
        '
        Me.Txtdoctype.Enabled = False
        Me.Txtdoctype.Location = New System.Drawing.Point(408, 9)
        Me.Txtdoctype.MaxLength = 32
        Me.Txtdoctype.Name = "Txtdoctype"
        Me.Txtdoctype.Size = New System.Drawing.Size(184, 21)
        Me.Txtdoctype.TabIndex = 105
        '
        'Txtaddress
        '
        Me.Txtaddress.Location = New System.Drawing.Point(403, 118)
        Me.Txtaddress.Name = "Txtaddress"
        Me.Txtaddress.Size = New System.Drawing.Size(189, 74)
        Me.Txtaddress.TabIndex = 4
        Me.Txtaddress.Text = ""
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(312, 121)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 13)
        Me.Label9.TabIndex = 103
        Me.Label9.Text = "Address"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txtawbno
        '
        Me.Txtawbno.Location = New System.Drawing.Point(102, 117)
        Me.Txtawbno.MaxLength = 32
        Me.Txtawbno.Name = "Txtawbno"
        Me.Txtawbno.Size = New System.Drawing.Size(183, 21)
        Me.Txtawbno.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(16, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 13)
        Me.Label6.TabIndex = 101
        Me.Label6.Text = "AWB"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSearchInward
        '
        Me.btnSearchInward.Location = New System.Drawing.Point(250, 8)
        Me.btnSearchInward.Name = "btnSearchInward"
        Me.btnSearchInward.Size = New System.Drawing.Size(34, 21)
        Me.btnSearchInward.TabIndex = 1
        Me.btnSearchInward.TabStop = False
        Me.btnSearchInward.Text = "..."
        Me.btnSearchInward.UseVisualStyleBackColor = True
        '
        'CboRecvby
        '
        Me.CboRecvby.FormattingEnabled = True
        Me.CboRecvby.Location = New System.Drawing.Point(101, 171)
        Me.CboRecvby.Name = "CboRecvby"
        Me.CboRecvby.Size = New System.Drawing.Size(183, 21)
        Me.CboRecvby.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(4, 175)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 13)
        Me.Label11.TabIndex = 99
        Me.Label11.Text = "Dispatched By"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(102, 198)
        Me.txtReason.MaxLength = 255
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(490, 38)
        Me.txtReason.TabIndex = 7
        Me.txtReason.TabStop = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(19, 202)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "&Remark"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertificateNo
        '
        Me.txtCertificateNo.Enabled = False
        Me.txtCertificateNo.Location = New System.Drawing.Point(102, 36)
        Me.txtCertificateNo.MaxLength = 64
        Me.txtCertificateNo.Name = "txtCertificateNo"
        Me.txtCertificateNo.Size = New System.Drawing.Size(183, 21)
        Me.txtCertificateNo.TabIndex = 2
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Enabled = False
        Me.txtFolioNo.Location = New System.Drawing.Point(102, 89)
        Me.txtFolioNo.MaxLength = 32
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(183, 21)
        Me.txtFolioNo.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(19, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "Folio No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(303, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Document Type"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.Enabled = False
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(102, 62)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(490, 21)
        Me.cboCompany.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(-1, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 15)
        Me.Label3.TabIndex = 84
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpRcvdDate
        '
        Me.dtpRcvdDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpRcvdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRcvdDate.Location = New System.Drawing.Point(102, 143)
        Me.dtpRcvdDate.Name = "dtpRcvdDate"
        Me.dtpRcvdDate.Size = New System.Drawing.Size(183, 21)
        Me.dtpRcvdDate.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 147)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "Dispatch Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCourier
        '
        Me.cboCourier.FormattingEnabled = True
        Me.cboCourier.Location = New System.Drawing.Point(405, 90)
        Me.cboCourier.Name = "cboCourier"
        Me.cboCourier.Size = New System.Drawing.Size(187, 21)
        Me.cboCourier.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(320, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 15)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "Courier"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl2
        '
        Me.lbl2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl2.Location = New System.Drawing.Point(303, 38)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(97, 13)
        Me.lbl2.TabIndex = 68
        Me.lbl2.Text = "Sub Type"
        Me.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Enabled = False
        Me.txtInwardNo.Location = New System.Drawing.Point(102, 8)
        Me.txtInwardNo.MaxLength = 16
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(142, 21)
        Me.txtInwardNo.TabIndex = 0
        '
        'lbl1
        '
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(19, 12)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(77, 13)
        Me.lbl1.TabIndex = 66
        Me.lbl1.Text = "Inward No"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(3, 4)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(26, 21)
        Me.txtId.TabIndex = 64
        Me.txtId.Visible = False
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 13)
        Me.Label8.TabIndex = 93
        Me.Label8.Text = "Certificate No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(386, 260)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(542, 260)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.SystemColors.Control
        Me.btnClear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Location = New System.Drawing.Point(464, 260)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmRudOutward
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 290)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.btnSave)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRudOutward"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rud\Correspondence Outward Entry"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lbl2 As System.Windows.Forms.Label
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboCourier As System.Windows.Forms.ComboBox
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpRcvdDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCertificateNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CboRecvby As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchInward As System.Windows.Forms.Button
    Friend WithEvents Txtawbno As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Txtaddress As System.Windows.Forms.RichTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Txtsubtype As System.Windows.Forms.TextBox
    Friend WithEvents Txtdoctype As System.Windows.Forms.TextBox
End Class
