Public Class frmBenpostReport
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBenpostId As System.Windows.Forms.TextBox
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents cboFileName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDepository As System.Windows.Forms.ComboBox
    Friend WithEvents txtPan1 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpBenpostTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpBenpostFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtShareCount As System.Windows.Forms.TextBox
    Friend WithEvents txtSebiRegNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtClientId As System.Windows.Forms.TextBox
    Friend WithEvents txtDpId As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtIsinId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPan3 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder3 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtPan2 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtIfscCode As System.Windows.Forms.TextBox
    Friend WithEvents txtMicrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents txtRbiRefNo As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cboCond As System.Windows.Forms.ComboBox
    Friend WithEvents lblbenfolio As System.Windows.Forms.Label
    Friend WithEvents ckbBenFolio As System.Windows.Forms.CheckBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBenpostReport))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lblbenfolio = New System.Windows.Forms.Label()
        Me.ckbBenFolio = New System.Windows.Forms.CheckBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.txtIfscCode = New System.Windows.Forms.TextBox()
        Me.txtMicrCode = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtRbiRefNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPan3 = New System.Windows.Forms.TextBox()
        Me.txtHolder3 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtPan2 = New System.Windows.Forms.TextBox()
        Me.txtHolder2 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtShareCount = New System.Windows.Forms.TextBox()
        Me.txtSebiRegNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtClientId = New System.Windows.Forms.TextBox()
        Me.txtDpId = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPan1 = New System.Windows.Forms.TextBox()
        Me.txtHolder1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpBenpostTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpBenpostFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboDepository = New System.Windows.Forms.ComboBox()
        Me.cboFileName = New System.Windows.Forms.ComboBox()
        Me.txtIsinId = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtBenpostId = New System.Windows.Forms.TextBox()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtRecCount = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.pnlMain.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.lblbenfolio)
        Me.pnlMain.Controls.Add(Me.ckbBenFolio)
        Me.pnlMain.Controls.Add(Me.cboCond)
        Me.pnlMain.Controls.Add(Me.txtIfscCode)
        Me.pnlMain.Controls.Add(Me.txtMicrCode)
        Me.pnlMain.Controls.Add(Me.Label24)
        Me.pnlMain.Controls.Add(Me.Label25)
        Me.pnlMain.Controls.Add(Me.txtBankName)
        Me.pnlMain.Controls.Add(Me.txtRbiRefNo)
        Me.pnlMain.Controls.Add(Me.Label22)
        Me.pnlMain.Controls.Add(Me.Label23)
        Me.pnlMain.Controls.Add(Me.txtPan3)
        Me.pnlMain.Controls.Add(Me.txtHolder3)
        Me.pnlMain.Controls.Add(Me.Label19)
        Me.pnlMain.Controls.Add(Me.Label20)
        Me.pnlMain.Controls.Add(Me.txtPan2)
        Me.pnlMain.Controls.Add(Me.txtHolder2)
        Me.pnlMain.Controls.Add(Me.Label17)
        Me.pnlMain.Controls.Add(Me.Label18)
        Me.pnlMain.Controls.Add(Me.txtShareCount)
        Me.pnlMain.Controls.Add(Me.txtSebiRegNo)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.Label14)
        Me.pnlMain.Controls.Add(Me.txtClientId)
        Me.pnlMain.Controls.Add(Me.txtDpId)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.txtPan1)
        Me.pnlMain.Controls.Add(Me.txtHolder1)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.dtpBenpostTo)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.dtpBenpostFrom)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.cboDepository)
        Me.pnlMain.Controls.Add(Me.cboFileName)
        Me.pnlMain.Controls.Add(Me.txtIsinId)
        Me.pnlMain.Controls.Add(Me.Label21)
        Me.pnlMain.Controls.Add(Me.txtBenpostId)
        Me.pnlMain.Controls.Add(Me.dtpTo)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.dtpFrom)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Location = New System.Drawing.Point(9, 10)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1152, 301)
        Me.pnlMain.TabIndex = 0
        '
        'lblbenfolio
        '
        Me.lblbenfolio.Location = New System.Drawing.Point(322, 256)
        Me.lblbenfolio.Name = "lblbenfolio"
        Me.lblbenfolio.Size = New System.Drawing.Size(104, 18)
        Me.lblbenfolio.TabIndex = 169
        Me.lblbenfolio.Text = "With Foilo"
        Me.lblbenfolio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ckbBenFolio
        '
        Me.ckbBenFolio.AutoSize = True
        Me.ckbBenFolio.Location = New System.Drawing.Point(435, 256)
        Me.ckbBenFolio.Name = "ckbBenFolio"
        Me.ckbBenFolio.Size = New System.Drawing.Size(22, 21)
        Me.ckbBenFolio.TabIndex = 168
        Me.ckbBenFolio.UseVisualStyleBackColor = True
        '
        'cboCond
        '
        Me.cboCond.FormattingEnabled = True
        Me.cboCond.ItemHeight = 21
        Me.cboCond.Location = New System.Drawing.Point(435, 133)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.Size = New System.Drawing.Size(63, 29)
        Me.cboCond.TabIndex = 11
        '
        'txtIfscCode
        '
        Me.txtIfscCode.Location = New System.Drawing.Point(435, 210)
        Me.txtIfscCode.MaxLength = 0
        Me.txtIfscCode.Name = "txtIfscCode"
        Me.txtIfscCode.Size = New System.Drawing.Size(157, 27)
        Me.txtIfscCode.TabIndex = 19
        '
        'txtMicrCode
        '
        Me.txtMicrCode.Location = New System.Drawing.Point(146, 210)
        Me.txtMicrCode.MaxLength = 0
        Me.txtMicrCode.Name = "txtMicrCode"
        Me.txtMicrCode.Size = New System.Drawing.Size(157, 27)
        Me.txtMicrCode.TabIndex = 18
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(14, 213)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(121, 18)
        Me.Label24.TabIndex = 166
        Me.Label24.Text = "Micr Code"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(306, 213)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(120, 18)
        Me.Label25.TabIndex = 167
        Me.Label25.Text = "Ifsc Code"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(435, 171)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(157, 27)
        Me.txtBankName.TabIndex = 15
        '
        'txtRbiRefNo
        '
        Me.txtRbiRefNo.Location = New System.Drawing.Point(146, 171)
        Me.txtRbiRefNo.MaxLength = 0
        Me.txtRbiRefNo.Name = "txtRbiRefNo"
        Me.txtRbiRefNo.Size = New System.Drawing.Size(157, 27)
        Me.txtRbiRefNo.TabIndex = 14
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(14, 174)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(121, 19)
        Me.Label22.TabIndex = 162
        Me.Label22.Text = "RBI Ref No"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(306, 174)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 19)
        Me.Label23.TabIndex = 163
        Me.Label23.Text = "Bank Name"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPan3
        '
        Me.txtPan3.Location = New System.Drawing.Point(972, 210)
        Me.txtPan3.MaxLength = 0
        Me.txtPan3.Name = "txtPan3"
        Me.txtPan3.Size = New System.Drawing.Size(158, 27)
        Me.txtPan3.TabIndex = 21
        '
        'txtHolder3
        '
        Me.txtHolder3.Location = New System.Drawing.Point(705, 210)
        Me.txtHolder3.MaxLength = 0
        Me.txtHolder3.Name = "txtHolder3"
        Me.txtHolder3.Size = New System.Drawing.Size(157, 27)
        Me.txtHolder3.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(573, 213)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(121, 18)
        Me.Label19.TabIndex = 158
        Me.Label19.Text = "Holder3"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(854, 213)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(109, 18)
        Me.Label20.TabIndex = 159
        Me.Label20.Text = "PAN3"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPan2
        '
        Me.txtPan2.Location = New System.Drawing.Point(972, 171)
        Me.txtPan2.MaxLength = 0
        Me.txtPan2.Name = "txtPan2"
        Me.txtPan2.Size = New System.Drawing.Size(158, 27)
        Me.txtPan2.TabIndex = 17
        '
        'txtHolder2
        '
        Me.txtHolder2.Location = New System.Drawing.Point(705, 171)
        Me.txtHolder2.MaxLength = 0
        Me.txtHolder2.Name = "txtHolder2"
        Me.txtHolder2.Size = New System.Drawing.Size(157, 27)
        Me.txtHolder2.TabIndex = 16
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(573, 174)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(121, 19)
        Me.Label17.TabIndex = 154
        Me.Label17.Text = "Holder2"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(854, 174)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(109, 19)
        Me.Label18.TabIndex = 155
        Me.Label18.Text = "PAN2"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShareCount
        '
        Me.txtShareCount.Location = New System.Drawing.Point(498, 133)
        Me.txtShareCount.MaxLength = 0
        Me.txtShareCount.Name = "txtShareCount"
        Me.txtShareCount.Size = New System.Drawing.Size(94, 27)
        Me.txtShareCount.TabIndex = 11
        '
        'txtSebiRegNo
        '
        Me.txtSebiRegNo.Location = New System.Drawing.Point(146, 133)
        Me.txtSebiRegNo.MaxLength = 0
        Me.txtSebiRegNo.Name = "txtSebiRegNo"
        Me.txtSebiRegNo.Size = New System.Drawing.Size(157, 27)
        Me.txtSebiRegNo.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(14, 136)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(121, 18)
        Me.Label13.TabIndex = 146
        Me.Label13.Text = "Sebi Reg No"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(306, 136)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 18)
        Me.Label14.TabIndex = 148
        Me.Label14.Text = "Share Count"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClientId
        '
        Me.txtClientId.Location = New System.Drawing.Point(435, 94)
        Me.txtClientId.MaxLength = 0
        Me.txtClientId.Name = "txtClientId"
        Me.txtClientId.Size = New System.Drawing.Size(157, 27)
        Me.txtClientId.TabIndex = 7
        '
        'txtDpId
        '
        Me.txtDpId.Location = New System.Drawing.Point(146, 94)
        Me.txtDpId.MaxLength = 0
        Me.txtDpId.Name = "txtDpId"
        Me.txtDpId.Size = New System.Drawing.Size(157, 27)
        Me.txtDpId.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(14, 97)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 19)
        Me.Label8.TabIndex = 142
        Me.Label8.Text = "DP Id"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(316, 97)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(110, 19)
        Me.Label9.TabIndex = 144
        Me.Label9.Text = "Client Id"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPan1
        '
        Me.txtPan1.Location = New System.Drawing.Point(972, 133)
        Me.txtPan1.MaxLength = 0
        Me.txtPan1.Name = "txtPan1"
        Me.txtPan1.Size = New System.Drawing.Size(158, 27)
        Me.txtPan1.TabIndex = 13
        '
        'txtHolder1
        '
        Me.txtHolder1.Location = New System.Drawing.Point(705, 133)
        Me.txtHolder1.MaxLength = 0
        Me.txtHolder1.Name = "txtHolder1"
        Me.txtHolder1.Size = New System.Drawing.Size(157, 27)
        Me.txtHolder1.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(573, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 18)
        Me.Label6.TabIndex = 138
        Me.Label6.Text = "Holder1"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpBenpostTo
        '
        Me.dtpBenpostTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpBenpostTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBenpostTo.Location = New System.Drawing.Point(435, 56)
        Me.dtpBenpostTo.Name = "dtpBenpostTo"
        Me.dtpBenpostTo.ShowCheckBox = True
        Me.dtpBenpostTo.Size = New System.Drawing.Size(157, 27)
        Me.dtpBenpostTo.TabIndex = 4
        Me.dtpBenpostTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(352, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 24)
        Me.Label3.TabIndex = 135
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpBenpostFrom
        '
        Me.dtpBenpostFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpBenpostFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBenpostFrom.Location = New System.Drawing.Point(146, 56)
        Me.dtpBenpostFrom.Name = "dtpBenpostFrom"
        Me.dtpBenpostFrom.ShowCheckBox = True
        Me.dtpBenpostFrom.Size = New System.Drawing.Size(157, 27)
        Me.dtpBenpostFrom.TabIndex = 3
        Me.dtpBenpostFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(-2, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(137, 24)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Benpost From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDepository
        '
        Me.cboDepository.FormattingEnabled = True
        Me.cboDepository.Location = New System.Drawing.Point(705, 94)
        Me.cboDepository.Name = "cboDepository"
        Me.cboDepository.Size = New System.Drawing.Size(157, 29)
        Me.cboDepository.TabIndex = 8
        '
        'cboFileName
        '
        Me.cboFileName.FormattingEnabled = True
        Me.cboFileName.Location = New System.Drawing.Point(705, 17)
        Me.cboFileName.Name = "cboFileName"
        Me.cboFileName.Size = New System.Drawing.Size(425, 29)
        Me.cboFileName.TabIndex = 2
        '
        'txtIsinId
        '
        Me.txtIsinId.Location = New System.Drawing.Point(972, 94)
        Me.txtIsinId.MaxLength = 0
        Me.txtIsinId.Name = "txtIsinId"
        Me.txtIsinId.Size = New System.Drawing.Size(158, 27)
        Me.txtIsinId.TabIndex = 9
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(578, 97)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(116, 19)
        Me.Label21.TabIndex = 129
        Me.Label21.Text = "Depository"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBenpostId
        '
        Me.txtBenpostId.Location = New System.Drawing.Point(146, 249)
        Me.txtBenpostId.MaxLength = 0
        Me.txtBenpostId.Name = "txtBenpostId"
        Me.txtBenpostId.Size = New System.Drawing.Size(157, 27)
        Me.txtBenpostId.TabIndex = 22
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(435, 17)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(157, 27)
        Me.dtpTo.TabIndex = 1
        Me.dtpTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(352, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 24)
        Me.Label11.TabIndex = 115
        Me.Label11.Text = "To"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(146, 17)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(157, 27)
        Me.dtpFrom.TabIndex = 0
        Me.dtpFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(9, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(126, 24)
        Me.Label10.TabIndex = 115
        Me.Label10.Text = "Import From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(705, 56)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(425, 29)
        Me.cboCompany.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(578, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 19)
        Me.Label5.TabIndex = 113
        Me.Label5.Text = "Company"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1022, 249)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 34)
        Me.btnClose.TabIndex = 25
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(904, 249)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(108, 34)
        Me.btnClear.TabIndex = 24
        Me.btnClear.Text = "C&lear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(788, 249)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(108, 34)
        Me.btnRefresh.TabIndex = 23
        Me.btnRefresh.Text = "&Refresh"
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(849, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 19)
        Me.Label2.TabIndex = 113
        Me.Label2.Text = "Isin Id"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(854, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 18)
        Me.Label7.TabIndex = 140
        Me.Label7.Text = "PAN1"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(578, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 19)
        Me.Label1.TabIndex = 131
        Me.Label1.Text = "File Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(-2, 251)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(137, 25)
        Me.Label12.TabIndex = 117
        Me.Label12.Text = "Benpost Id"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(18, 653)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(951, 47)
        Me.pnlExport.TabIndex = 2
        '
        'txtRecCount
        '
        Me.txtRecCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRecCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRecCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRecCount.Location = New System.Drawing.Point(9, 11)
        Me.txtRecCount.MaxLength = 100
        Me.txtRecCount.Name = "txtRecCount"
        Me.txtRecCount.ReadOnly = True
        Me.txtRecCount.Size = New System.Drawing.Size(649, 20)
        Me.txtRecCount.TabIndex = 0
        Me.txtRecCount.TabStop = False
        Me.txtRecCount.Text = "Record Count : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(837, 7)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(108, 34)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(9, 320)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(1152, 324)
        Me.dgvReport.TabIndex = 1
        '
        'frmBenpostReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(9, 20)
        Me.ClientSize = New System.Drawing.Size(787, 502)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBenpostReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Benpost Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Local Declaration"
#End Region
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dtpFrom.Checked = False
        dtpTo.Checked = False

        Call frmCtrClear(Me)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False

        If ckbBenFolio.Checked = True Then
            Call BenFolioLoadData()
        Else
            Call LoadData()
        End If

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim i As Integer

        Try
            lsCond = ""

            If dtpFrom.Checked = True Then lsCond &= " and f.insert_date >= '" & Format(CDate(dtpFrom.Value), "yyyy-MM-dd") & "' "
            If dtpTo.Checked = True Then lsCond &= " and f.insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

            If dtpBenpostFrom.Checked = True Then lsCond &= " and d.benpost_date >= '" & Format(CDate(dtpBenpostFrom.Value), "yyyy-MM-dd") & "' "
            If dtpBenpostTo.Checked = True Then lsCond &= " and d.benpost_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpBenpostTo.Value), "yyyy-MM-dd") & "' "

            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond &= " and d.comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            If cboFileName.Text <> "" And cboFileName.SelectedIndex >= 0 Then lsCond &= " and f.file_gid = '" & Val(cboFileName.SelectedValue.ToString) & "' "
            If cboDepository.Text <> "" And cboDepository.SelectedIndex >= 0 Then lsCond &= " and d.depository_code = '" & cboDepository.SelectedValue.ToString & "' "

            If txtBenpostId.Text <> "" Then lsCond &= " and d.benpost_gid = '" & Val(txtBenpostId.Text) & "' "

            If txtDpId.Text <> "" Then lsCond &= " and d.dp_id like '" & QuoteFilter(txtDpId.Text) & "%' "
            If txtClientId.Text <> "" Then lsCond &= " and d.client_id like '" & QuoteFilter(txtClientId.Text) & "%' "
            If txtIsinId.Text <> "" Then lsCond &= " and d.isin_id like '" & QuoteFilter(txtIsinId.Text) & "%' "

            If txtSebiRegNo.Text <> "" Then lsCond &= " and d.sebi_ref_no like '" & txtSebiRegNo.Text & "%' "

            If Val(txtShareCount.Text) > 0 Then
                Select Case cboCond.SelectedIndex
                    Case -1
                        lsCond &= " and d.share_count = " & Val(txtShareCount.Text) & " "
                    Case Else
                        lsCond &= " and d.share_count " & cboCond.Text & " " & Val(txtShareCount.Text) & " "
                End Select
            End If

            If txtHolder1.Text <> "" Then lsCond &= " and d.holder1_name like '" & QuoteFilter(txtHolder1.Text) & "%' "
            If txtPan1.Text <> "" Then lsCond &= " and d.holder1_pan like '" & QuoteFilter(txtPan1.Text) & "%' "
            If txtHolder2.Text <> "" Then lsCond &= " and d.holder2_name like '" & QuoteFilter(txtHolder2.Text) & "%' "
            If txtPan2.Text <> "" Then lsCond &= " and d.holder2_pan like '" & QuoteFilter(txtPan2.Text) & "%' "
            If txtHolder3.Text <> "" Then lsCond &= " and d.holder3_name like '" & QuoteFilter(txtHolder3.Text) & "%' "
            If txtPan3.Text <> "" Then lsCond &= " and d.holder3_pan like '" & QuoteFilter(txtPan3.Text) & "%' "
            If txtRbiRefNo.Text <> "" Then lsCond &= " and d.rbi_ref_no like '" & QuoteFilter(txtRbiRefNo.Text) & "%' "
            If txtBankName.Text <> "" Then lsCond &= " and d.bank_name like '" & QuoteFilter(txtBankName.Text) & "%' "
            If txtMicrCode.Text <> "" Then lsCond &= " and d.bank_micr_code like '" & QuoteFilter(txtMicrCode.Text) & "%' "
            If txtIfscCode.Text <> "" Then lsCond &= " and d.bank_ifsc_code like '" & QuoteFilter(txtIfscCode.Text) & "%' "

            If Val(txtBenpostId.Text) > 0 Then lsCond &= " and d.dematpend_gid = " & Val(txtBenpostId.Text) & " "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsFld = ""
            'lsFld &= " c.comp_code as 'Company Code',"
            lsFld &= " c.comp_name as 'Company Name',"
            lsFld &= " d.isin_id as 'Isin Id',"
            lsFld &= " p.depository_name as 'Depository',"
            lsFld &= " d.benpost_date as 'Benpost Date',"
            lsFld &= " d.dp_id as 'DP Id',"
            lsFld &= " d.client_id as 'Client Id',"
            lsFld &= " d.holder1_name as 'Holder1',"
            lsFld &= " d.holder2_name as 'Holder2',"
            lsFld &= " d.holder3_name as 'Holder3',"
            lsFld &= " d.share_count as 'Share Count',"
            lsFld &= " d.lockin as 'Lock In',"
            lsFld &= " d.pledge as 'Pledge',"
            lsFld &= " d.safe_keep_bal as 'Safe Keep Bal',"
            lsFld &= " d.earmark_bal as 'Earmark Bal',"
            lsFld &= " d.bene_free_positions as 'Free Balance',"
            'lsFld &= " d.free_bal as 'Free Balance',"
            lsFld &= " d.bene_block_positions as 'Bene Block Positions',"
            lsFld &= " d.bene_pledge_with_lockin_position as 'Pledge With Lockin Postions',"
            lsFld &= " d.bene_pledged_unconfirmed_Position as 'Pledge Unconfirmed Positions',"
            lsFld &= " d.bene_uncnfrm_Pledged_with_Lockin_position as 'Unconfirmed Pledge with Lockin Positions',"
            lsFld &= " d.bene_cm_idd_positions as 'CM IDD Positions',"
            lsFld &= " d.pending_demat_verification as 'Pending Demat Verification',"
            lsFld &= " d.pending_demat_confirmation as 'Pending Demat Confirmation',"
            lsFld &= " d.holder1_fh_name as 'Holder1 F/H Name',"
            lsFld &= " d.holder2_fh_name as 'Holder2 F/H Name',"
            lsFld &= " d.holder3_fh_name as 'Holder3 F/H Name',"
            lsFld &= " d.holder1_pan as 'Holder1 PAN',"
            lsFld &= " d.holder2_pan as 'Holder2 PAN',"
            lsFld &= " d.holder3_pan as 'Holder3 PAN',"
            lsFld &= " d.holder1_email_id as 'Holder1 Email Id',"
            lsFld &= " d.holder2_email_id as 'Holder2 Email Id',"
            lsFld &= " d.holder3_email_id as 'Holder3 Email Id',"
            lsFld &= " d.holder1_addr1 as 'Addr1',"
            lsFld &= " d.holder1_addr2 as 'Addr2',"
            lsFld &= " d.holder1_addr3 as 'Addr3',"
            lsFld &= " d.holder1_city as 'City',"
            lsFld &= " d.holder1_state as 'State',"
            lsFld &= " d.holder1_country as 'Country',"
            lsFld &= " d.holder1_pin as 'Pincode',"
            lsFld &= " d.holder1_contact_no as 'Contact No',"
            lsFld &= " d.holder1_fax_no as 'Fax No',"
            lsFld &= " d.holder1_per_addr1 as 'Permanent Addr1',"
            lsFld &= " d.holder1_per_addr2 as 'Permanent Addr2',"
            lsFld &= " d.holder1_per_addr3 as 'Permanent Addr3',"
            lsFld &= " d.holder1_per_city as 'Permanent City',"
            lsFld &= " d.holder1_per_state as 'Permanent State',"
            lsFld &= " d.holder1_per_country as 'Permanent Country',"
            lsFld &= " d.holder1_per_pin as 'Permanent Pincode',"
            lsFld &= " d.nominee_name as 'Nominee Name',"
            lsFld &= " d.nominee_part1 as 'Nominee Part1',"
            lsFld &= " d.nominee_part2 as 'Nominee Part2',"
            lsFld &= " d.nominee_part3 as 'Nominee Part3',"
            lsFld &= " d.nominee_part4 as 'Nominee Part4',"
            lsFld &= " d.nominee_part5 as 'Nominee Part5',"
            lsFld &= " d.bank_name as 'Bank Name',"
            lsFld &= " d.bank_addr1 as 'Bank Addr1',"
            lsFld &= " d.bank_addr2 as 'Bank Addr2',"
            lsFld &= " d.bank_addr3 as 'Bank Addr3',"
            lsFld &= " d.bank_city as 'Bank City',"
            lsFld &= " d.bank_state as 'Bank State',"
            lsFld &= " d.bank_country as 'Bank Country',"
            lsFld &= " d.bank_pin as 'Bank Pincode',"
            lsFld &= " d.bank_acc_no as 'Bank A/C No',"
            lsFld &= " d.bank_acc_type as 'Bank A/C Type',"
            lsFld &= " d.bank_micr_code as 'Bank Micr Code',"
            lsFld &= " d.bank_ifsc_code as 'Bank Ifsc Code',"
            lsFld &= " d.rbi_ref_no as 'RBI Reg No',"
            lsFld &= " d.rbi_app_date as 'RBI App Date',"
            lsFld &= " d.bene_type as 'Bene Type',"
            lsFld &= " b.bencategory_name as 'Bene Category',"
            lsFld &= " d.bene_subtype as 'Bene Sub Type',"
            lsFld &= " b.bensubcategory_name as 'Bene Sub Category',"
            lsFld &= " d.bene_acccat as 'Bene A/C Category',"
            lsFld &= " d.bene_occupation as 'Bene Occupation',"
            lsFld &= " d.sebi_reg_no as 'Sebi Reg No',"
            lsFld &= " d.bene_tax_deduction_status as 'Bene Tax Deduction status',"
            lsFld &= " d.bene_status as 'Bene Status',"
            lsFld &= " d.holder1_pan_flag as 'Holder1 Pan Flag',"
            lsFld &= " d.holder2_pan_flag as 'Holder2 Pan Flag',"
            lsFld &= " d.holder3_pan_flag as 'Holder3 Pan Flag',"
            lsFld &= " d.guardian_name as 'Guardian Name',"
            lsFld &= " case when d.birth_date = '0000-00-00' or d.birth_date is null or d.birth_date = '0001-01-01' then '' else d.birth_date end as 'Birth Date' ,"
            lsFld &= " d.account_status as 'Account Status',"
            lsFld &= " d.bo_freeze_flag as 'BO Freeze Flag',"
            lsFld &= " d.freeze_reason_code as 'Freeze Reasoncode',"
            lsFld &= " d.isin_status as 'ISIN Status',"
            lsFld &= " case when d.acc_opening_date = '0000-00-00' or d.acc_opening_date is null or d.acc_opening_date = '0001-01-01' then '' else d.acc_opening_date  end as 'Account Opening Date' ,"
            'lsFld &= " d.tax_deduction_status as 'Tax Deduction Status',"
            lsFld &= " d.nationality as 'Nationality',"
            lsFld &= " d.secondary_phone_no as 'Secondary PhoneNo',"
            lsFld &= " d.ecs_mandate_flag as 'ECS Mandate Flag',"
            lsFld &= " d.divident_bank_currency as 'Dividend Bank Currency',"
            lsFld &= " d.pledge_setup_bal as 'Pledge Setup Bal',"
            lsFld &= " d.uid_of_holder1 as 'UID of Holder1',"
            lsFld &= " d.uid_of_holder2 as 'UID of Holder2',"
            lsFld &= " d.uid_of_holder3 as 'UID of Holder3',"
            lsFld &= " d.benpost_gid as 'Benpost Id',"
            lsFld &= " d.comp_gid as 'Comp Id',"
            lsFld &= " f.insert_date as 'Import Date',f.file_name as 'File Name',f.sheet_name as 'Sheet Name',"
            lsFld &= " f.insert_by as 'Import By',"
            lsFld &= " f.file_type as 'File Type',"
            lsFld &= " f.file_gid as 'File Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld

            lsSql &= " from sta_trn_tbenpost as d "
            lsSql &= " inner join sta_trn_tfile as f on d.file_gid = f.file_gid and f.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on d.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tdepository as p on d.depository_code = p.depository_code and p.delete_flag = 'N' "
            lsSql &= " left  join sta_mst_tbencategory as b on d.bene_type = b.bencategory_type and d.bene_subtype = b.bencategory_subtype and p.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and d.share_count > 0 "
            lsSql &= " and d.delete_flag = 'N' "
            lsSql &= " order by d.benpost_date,d.benpost_gid desc"

            Call gpPopGridView(dgvReport, lsSql, gOdbcConn)

            For i = 0 To dgvReport.Columns.Count - 1
                dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BenFolioLoadData()
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim lsCond1 As String
        Dim i As Integer

        Try
            lsCond = ""
            lsCond1 = ""

            If dtpFrom.Checked = True Then lsCond &= " and f.insert_date >= '" & Format(CDate(dtpFrom.Value), "yyyy-MM-dd") & "' "
            If dtpTo.Checked = True Then lsCond &= " and f.insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

            If dtpBenpostFrom.Checked = True Then lsCond &= " and d.benpost_date >= '" & Format(CDate(dtpBenpostFrom.Value), "yyyy-MM-dd") & "' "
            If dtpBenpostTo.Checked = True Then lsCond &= " and d.benpost_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpBenpostTo.Value), "yyyy-MM-dd") & "' "

            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond &= " and d.comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond1 &= " and d.comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "

            If cboFileName.Text <> "" And cboFileName.SelectedIndex >= 0 Then lsCond &= " and f.file_gid = '" & Val(cboFileName.SelectedValue.ToString) & "' "
            If cboDepository.Text <> "" And cboDepository.SelectedIndex >= 0 Then lsCond &= " and d.depository_code = '" & cboDepository.SelectedValue.ToString & "' "

            If txtBenpostId.Text <> "" Then lsCond &= " and d.benpost_gid = '" & Val(txtBenpostId.Text) & "' "

            If txtDpId.Text <> "" Then lsCond &= " and d.dp_id like '" & QuoteFilter(txtDpId.Text) & "%' "
            If txtClientId.Text <> "" Then lsCond &= " and d.client_id like '" & QuoteFilter(txtClientId.Text) & "%' "
            If txtIsinId.Text <> "" Then lsCond &= " and d.isin_id like '" & QuoteFilter(txtIsinId.Text) & "%' "

            If txtSebiRegNo.Text <> "" Then lsCond &= " and d.sebi_ref_no like '" & txtSebiRegNo.Text & "%' "

            If Val(txtShareCount.Text) > 0 Then
                Select Case cboCond.SelectedIndex
                    Case -1
                        lsCond &= " and d.share_count = " & Val(txtShareCount.Text) & " "
                    Case Else
                        lsCond &= " and d.share_count " & cboCond.Text & " " & Val(txtShareCount.Text) & " "
                End Select
            End If

            If txtHolder1.Text <> "" Then lsCond &= " and d.holder1_name like '" & QuoteFilter(txtHolder1.Text) & "%' "
            If txtHolder1.Text <> "" Then lsCond1 &= " and d.holder1_name like '" & QuoteFilter(txtHolder1.Text) & "%' "

            If txtPan1.Text <> "" Then lsCond &= " and d.holder1_pan like '" & QuoteFilter(txtPan1.Text) & "%' "
            If txtPan1.Text <> "" Then lsCond1 &= " and d.holder1_pan_no like '" & QuoteFilter(txtPan1.Text) & "%' "

            If txtHolder2.Text <> "" Then lsCond &= " and d.holder2_name like '" & QuoteFilter(txtHolder2.Text) & "%' "
            If txtHolder2.Text <> "" Then lsCond1 &= " and d.holder2_name like '" & QuoteFilter(txtHolder2.Text) & "%' "

            If txtPan2.Text <> "" Then lsCond &= " and d.holder2_pan like '" & QuoteFilter(txtPan2.Text) & "%' "
            If txtPan2.Text <> "" Then lsCond1 &= " and d.holder2_pan_no like '" & QuoteFilter(txtPan2.Text) & "%' "

            If txtHolder3.Text <> "" Then lsCond &= " and d.holder3_name like '" & QuoteFilter(txtHolder3.Text) & "%' "
            If txtHolder3.Text <> "" Then lsCond1 &= " and d.holder3_name like '" & QuoteFilter(txtHolder3.Text) & "%' "

            If txtPan3.Text <> "" Then lsCond &= " and d.holder3_pan like '" & QuoteFilter(txtPan3.Text) & "%' "
            If txtPan3.Text <> "" Then lsCond1 &= " and d.holder3_pan_no like '" & QuoteFilter(txtPan3.Text) & "%' "

            If txtRbiRefNo.Text <> "" Then lsCond &= " and d.rbi_ref_no like '" & QuoteFilter(txtRbiRefNo.Text) & "%' "
            If txtBankName.Text <> "" Then lsCond &= " and d.bank_name like '" & QuoteFilter(txtBankName.Text) & "%' "
            If txtBankName.Text <> "" Then lsCond1 &= " and d.bank_name like '" & QuoteFilter(txtBankName.Text) & "%' "

            If txtMicrCode.Text <> "" Then lsCond &= " and d.bank_micr_code like '" & QuoteFilter(txtMicrCode.Text) & "%' "
            If txtMicrCode.Text <> "" Then lsCond1 &= " and d.bank_micr_code like '" & QuoteFilter(txtMicrCode.Text) & "%' "

            If txtIfscCode.Text <> "" Then lsCond &= " and d.bank_ifsc_code like '" & QuoteFilter(txtIfscCode.Text) & "%' "
            If txtIfscCode.Text <> "" Then lsCond1 &= " and d.bank_ifsc_code like '" & QuoteFilter(txtIfscCode.Text) & "%' "

            If Val(txtBenpostId.Text) > 0 Then lsCond &= " and d.dematpend_gid = " & Val(txtBenpostId.Text) & " "

            If lsCond = "" Then lsCond &= " and 1 = 2 "
            If lsCond1 = "" Then lsCond1 &= " and 1 = 2 "

            lsFld = ""
            'lsFld &= " c.comp_code as 'Company Code',"
            lsFld &= " c.comp_name as 'Company Name',"
            lsFld &= " d.isin_id as 'Isin Id',"
            lsFld &= " p.depository_name as 'Depository',"
            lsFld &= " d.benpost_date as 'Benpost Date',"
            lsFld &= " d.dp_id as 'DP Id',"
            lsFld &= " d.client_id as 'Client Id',"
            lsFld &= " d.holder1_name as 'Holder1',"
            lsFld &= " d.holder2_name as 'Holder2',"
            lsFld &= " d.holder3_name as 'Holder3',"
            lsFld &= " d.share_count as 'Share Count',"
            lsFld &= " d.lockin as 'Lock In',"
            lsFld &= " d.pledge as 'Pledge',"
            lsFld &= " d.safe_keep_bal as 'Safe Keep Bal',"
            lsFld &= " d.earmark_bal as 'Earmark Bal',"
            lsFld &= " d.bene_free_positions as 'Free Balance',"
            'lsFld &= " d.free_bal as 'Free Balance',"
            lsFld &= " d.bene_block_positions as 'Bene Block Positions',"
            lsFld &= " d.bene_pledge_with_lockin_position as 'Pledge With Lockin Postions',"
            lsFld &= " d.bene_pledged_unconfirmed_Position as 'Pledge Unconfirmed Positions',"
            lsFld &= " d.bene_uncnfrm_Pledged_with_Lockin_position as 'Unconfirmed Pledge with Lockin Positions',"
            lsFld &= " d.bene_cm_idd_positions as 'CM IDD Positions',"
            lsFld &= " d.pending_demat_verification as 'Pending Demat Verification',"
            lsFld &= " d.pending_demat_confirmation as 'Pending Demat Confirmation',"
            lsFld &= " d.holder1_fh_name as 'Holder1 F/H Name',"
            lsFld &= " d.holder2_fh_name as 'Holder2 F/H Name',"
            lsFld &= " d.holder3_fh_name as 'Holder3 F/H Name',"
            lsFld &= " d.holder1_pan as 'Holder1 PAN',"
            lsFld &= " d.holder2_pan as 'Holder2 PAN',"
            lsFld &= " d.holder3_pan as 'Holder3 PAN',"
            lsFld &= " d.holder1_email_id as 'Holder1 Email Id',"
            lsFld &= " d.holder2_email_id as 'Holder2 Email Id',"
            lsFld &= " d.holder3_email_id as 'Holder3 Email Id',"
            lsFld &= " d.holder1_addr1 as 'Addr1',"
            lsFld &= " d.holder1_addr2 as 'Addr2',"
            lsFld &= " d.holder1_addr3 as 'Addr3',"
            lsFld &= " d.holder1_city as 'City',"
            lsFld &= " d.holder1_state as 'State',"
            lsFld &= " d.holder1_country as 'Country',"
            lsFld &= " d.holder1_pin as 'Pincode',"
            lsFld &= " d.holder1_contact_no as 'Contact No',"
            lsFld &= " d.holder1_fax_no as 'Fax No',"
            lsFld &= " d.holder1_per_addr1 as 'Permanent Addr1',"
            lsFld &= " d.holder1_per_addr2 as 'Permanent Addr2',"
            lsFld &= " d.holder1_per_addr3 as 'Permanent Addr3',"
            lsFld &= " d.holder1_per_city as 'Permanent City',"
            lsFld &= " d.holder1_per_state as 'Permanent State',"
            lsFld &= " d.holder1_per_country as 'Permanent Country',"
            lsFld &= " d.holder1_per_pin as 'Permanent Pincode',"
            lsFld &= " d.nominee_name as 'Nominee Name',"
            lsFld &= " d.nominee_part1 as 'Nominee Part1',"
            lsFld &= " d.nominee_part2 as 'Nominee Part2',"
            lsFld &= " d.nominee_part3 as 'Nominee Part3',"
            lsFld &= " d.nominee_part4 as 'Nominee Part4',"
            lsFld &= " d.nominee_part5 as 'Nominee Part5',"
            lsFld &= " d.bank_name as 'Bank Name',"
            lsFld &= " d.bank_addr1 as 'Bank Addr1',"
            lsFld &= " d.bank_addr2 as 'Bank Addr2',"
            lsFld &= " d.bank_addr3 as 'Bank Addr3',"
            lsFld &= " d.bank_city as 'Bank City',"
            lsFld &= " d.bank_state as 'Bank State',"
            lsFld &= " d.bank_country as 'Bank Country',"
            lsFld &= " d.bank_pin as 'Bank Pincode',"
            lsFld &= " d.bank_acc_no as 'Bank A/C No',"
            lsFld &= " d.bank_acc_type as 'Bank A/C Type',"
            lsFld &= " d.bank_micr_code as 'Bank Micr Code',"
            lsFld &= " d.bank_ifsc_code as 'Bank Ifsc Code',"
            lsFld &= " d.rbi_ref_no as 'RBI Reg No',"
            lsFld &= " d.rbi_app_date as 'RBI App Date',"
            lsFld &= " d.bene_type as 'Bene Type',"
            lsFld &= " b.bencategory_name as 'Bene Category',"
            lsFld &= " d.bene_subtype as 'Bene Sub Type',"
            lsFld &= " b.bensubcategory_name as 'Bene Sub Category',"
            lsFld &= " d.bene_acccat as 'Bene A/C Category',"
            lsFld &= " d.bene_occupation as 'Bene Occupation',"
            lsFld &= " d.sebi_reg_no as 'Sebi Reg No',"
            lsFld &= " d.bene_tax_deduction_status as 'Bene Tax Deduction status',"
            lsFld &= " d.bene_status as 'Bene Status',"
            lsFld &= " d.holder1_pan_flag as 'Holder1 Pan Flag',"
            lsFld &= " d.holder2_pan_flag as 'Holder2 Pan Flag',"
            lsFld &= " d.holder3_pan_flag as 'Holder3 Pan Flag',"
            lsFld &= " d.guardian_name as 'Guardian Name',"
            lsFld &= " case when d.birth_date = '0000-00-00' or d.birth_date is null or d.birth_date = '0001-01-01' then '' else d.birth_date end as 'Birth Date' ,"
            'lsFld &= " d.birth_date as 'Birth Date',"
            lsFld &= " d.account_status as 'Account Status',"
            lsFld &= " d.bo_freeze_flag as 'BO Freeze Flag',"
            lsFld &= " d.freeze_reason_code as 'Freeze Reasoncode',"
            lsFld &= " d.isin_status as 'ISIN Status',"
            lsFld &= " case when d.acc_opening_date = '0000-00-00' or d.acc_opening_date is null or d.acc_opening_date = '0001-01-01' then '' else d.acc_opening_date  end as 'Account Opening Date' ,"
            'lsFld &= " d.acc_opening_date as 'Account Opening Date',"
            'lsFld &= " d.tax_deduction_status as 'Tax Deduction Status',"
            lsFld &= " d.nationality as 'Nationality',"
            lsFld &= " d.secondary_phone_no as 'Secondary PhoneNo',"
            lsFld &= " d.ecs_mandate_flag as 'ECS Mandate Flag',"
            lsFld &= " d.divident_bank_currency as 'Dividend Bank Currency',"
            lsFld &= " d.pledge_setup_bal as 'Pledge Setup Bal',"
            lsFld &= " d.uid_of_holder1 as 'UID of Holder1',"
            lsFld &= " d.uid_of_holder2 as 'UID of Holder2',"
            lsFld &= " d.uid_of_holder3 as 'UID of Holder3',"
            lsFld &= " d.benpost_gid as 'Benpost Id',"
            lsFld &= " d.comp_gid as 'Comp Id',"
            lsFld &= " f.insert_date as 'Import Date',f.file_name as 'File Name',f.sheet_name as 'Sheet Name',"
            lsFld &= " f.insert_by as 'Import By',"
            lsFld &= " f.file_type as 'File Type',"
            lsFld &= " f.file_gid as 'File Id' "

            lsSql = ""
            lsSql &= " (select "
            lsSql &= lsFld

            lsSql &= " from sta_trn_tbenpost as d "
            lsSql &= " inner join sta_trn_tfile as f on d.file_gid = f.file_gid and f.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on d.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tdepository as p on d.depository_code = p.depository_code and p.delete_flag = 'N' "
            lsSql &= " left  join sta_mst_tbencategory as b on d.bene_type = b.bencategory_type and d.bene_subtype = b.bencategory_subtype and p.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and d.share_count > 0 "
            lsSql &= " and d.delete_flag = 'N' "
            lsSql &= " order by d.benpost_date,d.benpost_gid desc)"

            lsSql &= " union all"

            lsSql &= " select "
            'lsSql &= " c.comp_code as 'Company Code',"
            lsSql &= " c.comp_name as 'Company Name',"
            lsSql &= " c.isin_id as 'Isin Id',"
            lsSql &= " 'Physical' as 'Depository',"
            lsSql &= " '' as 'Benpost Date',"
            lsSql &= " '' as 'DP Id',"
            lsSql &= " d.folio_no as 'Client Id',"
            lsSql &= " d.holder1_name as 'Holder1',"
            lsSql &= " d.holder2_name as 'Holder2',"
            lsSql &= " d.holder3_name as 'Holder3',"
            lsSql &= " d.folio_shares as 'Share Count',"
            lsSql &= " 0 as 'Lock In',"
            lsSql &= " 0 as 'Pledge',"
            lsSql &= " 0 as 'Safe Keep Bal',"
            lsSql &= " 0 as 'Earmark Bal',"
            lsSql &= " 0 as 'Free Balance',"
            lsSql &= " '' as 'Bene Block Positions',"
            lsSql &= " '' as 'Pledge With Lockin Postions',"
            lsSql &= " '' as 'Pledge Unconfirmed Positions',"
            lsSql &= " '' as 'Unconfirmed Pledge with Lockin Positions',"
            lsSql &= " '' as 'CM IDD Positions',"
            lsSql &= " 0 as 'Pending Demat Verification',"
            lsSql &= " 0 as 'Pending Demat Confirmation',"
            lsSql &= " d.holder1_fh_name as 'Holder1 F/H Name',"
            lsSql &= " d.holder2_fh_name as 'Holder2 F/H Name',"
            lsSql &= " d.holder3_fh_name as 'Holder3 F/H Name',"
            lsSql &= " d.holder1_pan_no as 'Holder1 PAN',"
            lsSql &= " d.holder2_pan_no as 'Holder2 PAN',"
            lsSql &= " d.holder3_pan_no as 'Holder3 PAN',"
            lsSql &= " d.folio_mail_id as 'Holder1 Email Id',"
            lsSql &= " '' as 'Holder2 Email Id',"
            lsSql &= " '' as 'Holder3 Email Id',"
            lsSql &= " d.folio_addr1 as 'Addr1',"
            lsSql &= " d.folio_addr2 as 'Addr2',"
            lsSql &= " d.folio_addr3 as 'Addr3',"
            lsSql &= " d.folio_city as 'City',"
            lsSql &= " d.folio_state as 'State',"
            lsSql &= " d.folio_country as 'Country',"
            lsSql &= " d.folio_pincode as 'Pincode',"
            lsSql &= " d.folio_contact_no as 'Contact No',"
            lsSql &= " ''as 'Fax No',"
            lsSql &= " d.folio_addr1 as 'Permanent Addr1',"
            lsSql &= " d.folio_addr2 as 'Permanent Addr2',"
            lsSql &= " d.folio_addr3 as 'Permanent Addr3',"
            lsSql &= " d.folio_city as 'Permanent City',"
            lsSql &= " d.folio_state as 'Permanent State',"
            lsSql &= " d.folio_country as 'Permanent Country',"
            lsSql &= " d.folio_pincode as 'Permanent Pincode',"
            lsSql &= " d.nominee_name as 'Nominee Name',"
            lsSql &= " '' as 'Nominee Part1',"
            lsSql &= " '' as 'Nominee Part2',"
            lsSql &= " '' as 'Nominee Part3',"
            lsSql &= " '' as 'Nominee Part4',"
            lsSql &= " '' as 'Nominee Part5',"
            lsSql &= " d.bank_name as 'Bank Name',"
            lsSql &= " '' as 'Bank Addr1',"
            lsSql &= " '' as 'Bank Addr2',"
            lsSql &= " '' as 'Bank Addr3',"
            lsSql &= " '' as 'Bank City',"
            lsSql &= " '' as 'Bank State',"
            lsSql &= " '' as 'Bank Country',"
            lsSql &= " '' as 'Bank Pincode',"
            lsSql &= " d.bank_acc_no as 'Bank A/C No',"
            lsSql &= " d.bank_acc_type as 'Bank A/C Type',"
            lsSql &= " d.bank_micr_code as 'Bank Micr Code',"
            lsSql &= " d.bank_ifsc_code as 'Bank Ifsc Code',"
            lsSql &= " '' as 'RBI Reg No',"
            lsSql &= " '' as 'RBI App Date',"
            lsSql &= " '' as 'Bene Type',"
            lsSql &= " b.category_alias_name as 'Bene Category',"
            lsSql &= " '' as 'Bene Sub Type',"
            lsSql &= " '' as 'Bene Sub Category',"
            lsSql &= " '' as 'Bene A/C Category',"
            lsSql &= " '' as 'Bene Occupation',"
            lsSql &= " '' as 'Sebi Reg No',"
            'lsSql &= " '' as 'Bene Tax Deduction status',"
            lsSql &= " '' as 'Bene Status',"
            'lsSql &= " '' as 'Bene Free Positions',"
            lsSql &= " '' as 'Holder1 Pan Flag',"
            lsSql &= " '' as 'Holder2 Pan Flag',"
            lsSql &= " '' as 'Holder3 Pan Flag',"
            lsSql &= " '' as 'Guardian Name',"
            lsSql &= " '' as 'Birth Date',"
            lsSql &= " '' as 'Account Status',"
            lsSql &= " '' as 'BO Freeze Flag',"
            lsSql &= " '' as 'Freeze Reasoncode',"
            lsSql &= " '' as 'ISIN Status',"
            lsSql &= " '' as 'Account Opening Date',"
            lsSql &= " '' as 'Tax Deduction Status',"
            lsSql &= " '' as 'Nationality',"
            lsSql &= " '' as 'Secondary PhoneNo',"
            lsSql &= " '' as 'ECS Mandate Flag',"
            lsSql &= " '' as 'Dividend Bank Currency',"
            lsSql &= " '' as 'Pledge Setup Bal',"
            lsSql &= " '' as 'UID of Holder1',"
            lsSql &= " '' as 'UID of Holder2',"
            lsSql &= " '' as 'UID of Holder3',"
            lsSql &= " '' as 'Benpost Id',"
            lsSql &= " d.comp_gid as 'Comp Id',"
            lsSql &= " '' as 'Import Date','' as 'File Name','' as 'Sheet Name',"
            lsSql &= " '' as 'Import By',"
            lsSql &= " '' as 'File Type',"
            lsSql &= " '' as 'File Id' "

            lsSql &= " from sta_trn_tfolio as d "
            lsSql &= " inner join sta_mst_tcompany as c on d.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " inner  join sta_mst_tcategory as b on d.category_gid = b.category_gid and b.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond1
            'lsSql &= " and c.comp_gid = '" + Val(cboCompany.SelectedValue.ToString).ToString() + "'"
            lsSql &= " and d.folio_shares > 0 and d.folio_no not in (00777777,00888888,00999999) "
            lsSql &= " and d.delete_flag = 'N' "

            Call gpPopGridView(dgvReport, lsSql, gOdbcConn)

            For i = 0 To dgvReport.Columns.Count - 1
                dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lsSql As String

        Try
            ' company
            lsSql = ""
            lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
            lsSql &= " where delete_flag = 'N' "
            lsSql &= " order by comp_name asc "

            Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

            ' depository
            lsSql = ""
            lsSql &= " select depository_code,depository_name from sta_mst_tdepository "
            lsSql &= " where delete_flag = 'N' "
            lsSql &= " order by depository_name "

            Call gpBindCombo(lsSql, "depository_name", "depository_code", cboDepository, gOdbcConn)

            With cboCond
                .Items.Clear()
                .Items.Add("=")
                .Items.Add(">")
                .Items.Add(">=")
                .Items.Add("<")
                .Items.Add("<=")
                .Items.Add("<>")
            End With

            Call frmDtpCtrClear(Me)

            btnClear.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvReport
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvReport.Top + dgvReport.Height + 6
        pnlExport.Width = Me.Width
        btnExport.Left = pnlExport.Width - btnExport.Width - 24
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvReport, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboFileName_GotFocus(sender As Object, e As EventArgs) Handles cboFileName.GotFocus
        Dim lsSql As String = ""

        lsSql = ""
        lsSql &= " select file_gid,concat(file_name,' ',ifnull(sheet_name,'')) as file_name from sta_trn_tfile "
        lsSql &= " where 1 = 1"

        If dtpFrom.Checked = True Then
            lsSql &= " and insert_date >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpTo.Checked = True Then
            lsSql &= " AND insert_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "'"
        End If

        lsSql &= " and file_type in (" & gnFileBenpostCDSL & "," & gnFileBenpostNSDL & ") "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by file_gid desc"

        gpBindCombo(lsSql, "file_name", "file_gid", cboFileName, gOdbcConn)
    End Sub
End Class
