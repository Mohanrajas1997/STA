Public Class frmDematPendingReport
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtDematPendId As System.Windows.Forms.TextBox
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents txtDrnNo As System.Windows.Forms.TextBox
    Friend WithEvents cboFileName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDepository As System.Windows.Forms.ComboBox
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpReqTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpReqFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtShareCount As System.Windows.Forms.TextBox
    Friend WithEvents txtPendFlag As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtClientId As System.Windows.Forms.TextBox
    Friend WithEvents txtDpId As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtInwardId As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDematPendingReport))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtInwardId = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtShareCount = New System.Windows.Forms.TextBox()
        Me.txtPendFlag = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtClientId = New System.Windows.Forms.TextBox()
        Me.txtDpId = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpReqTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpReqFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboDepository = New System.Windows.Forms.ComboBox()
        Me.cboFileName = New System.Windows.Forms.ComboBox()
        Me.txtDrnNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtDematPendId = New System.Windows.Forms.TextBox()
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
        Me.pnlMain.Controls.Add(Me.cboStatus)
        Me.pnlMain.Controls.Add(Me.Label16)
        Me.pnlMain.Controls.Add(Me.txtInwardId)
        Me.pnlMain.Controls.Add(Me.Label15)
        Me.pnlMain.Controls.Add(Me.txtShareCount)
        Me.pnlMain.Controls.Add(Me.txtPendFlag)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.Label14)
        Me.pnlMain.Controls.Add(Me.txtClientId)
        Me.pnlMain.Controls.Add(Me.txtDpId)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.txtCustName)
        Me.pnlMain.Controls.Add(Me.txtFolioNo)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.dtpReqTo)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.dtpReqFrom)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.cboDepository)
        Me.pnlMain.Controls.Add(Me.cboFileName)
        Me.pnlMain.Controls.Add(Me.txtDrnNo)
        Me.pnlMain.Controls.Add(Me.Label21)
        Me.pnlMain.Controls.Add(Me.txtDematPendId)
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
        Me.pnlMain.Location = New System.Drawing.Point(6, 7)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(768, 182)
        Me.pnlMain.TabIndex = 0
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(470, 120)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(283, 21)
        Me.cboStatus.TabIndex = 16
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(401, 122)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(62, 17)
        Me.Label16.TabIndex = 151
        Me.Label16.Text = "Status"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardId
        '
        Me.txtInwardId.Location = New System.Drawing.Point(290, 120)
        Me.txtInwardId.MaxLength = 0
        Me.txtInwardId.Name = "txtInwardId"
        Me.txtInwardId.Size = New System.Drawing.Size(105, 21)
        Me.txtInwardId.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(214, 122)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 17)
        Me.Label15.TabIndex = 150
        Me.Label15.Text = "Inward Id"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShareCount
        '
        Me.txtShareCount.Location = New System.Drawing.Point(290, 93)
        Me.txtShareCount.MaxLength = 0
        Me.txtShareCount.Name = "txtShareCount"
        Me.txtShareCount.Size = New System.Drawing.Size(105, 21)
        Me.txtShareCount.TabIndex = 11
        '
        'txtPendFlag
        '
        Me.txtPendFlag.Location = New System.Drawing.Point(97, 93)
        Me.txtPendFlag.MaxLength = 0
        Me.txtPendFlag.Name = "txtPendFlag"
        Me.txtPendFlag.Size = New System.Drawing.Size(105, 21)
        Me.txtPendFlag.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(9, 95)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 13)
        Me.Label13.TabIndex = 146
        Me.Label13.Text = "Pending Flag"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(204, 95)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 148
        Me.Label14.Text = "Share Count"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClientId
        '
        Me.txtClientId.Location = New System.Drawing.Point(290, 66)
        Me.txtClientId.MaxLength = 0
        Me.txtClientId.Name = "txtClientId"
        Me.txtClientId.Size = New System.Drawing.Size(105, 21)
        Me.txtClientId.TabIndex = 7
        '
        'txtDpId
        '
        Me.txtDpId.Location = New System.Drawing.Point(97, 66)
        Me.txtDpId.MaxLength = 0
        Me.txtDpId.Name = "txtDpId"
        Me.txtDpId.Size = New System.Drawing.Size(105, 21)
        Me.txtDpId.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(9, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 142
        Me.Label8.Text = "DP Id"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(211, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 13)
        Me.Label9.TabIndex = 144
        Me.Label9.Text = "Client Id"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustName
        '
        Me.txtCustName.Location = New System.Drawing.Point(648, 93)
        Me.txtCustName.MaxLength = 0
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(105, 21)
        Me.txtCustName.TabIndex = 13
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(470, 93)
        Me.txtFolioNo.MaxLength = 0
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(105, 21)
        Me.txtFolioNo.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(382, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 13)
        Me.Label6.TabIndex = 138
        Me.Label6.Text = "Folio No"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpReqTo
        '
        Me.dtpReqTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpReqTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReqTo.Location = New System.Drawing.Point(290, 39)
        Me.dtpReqTo.Name = "dtpReqTo"
        Me.dtpReqTo.ShowCheckBox = True
        Me.dtpReqTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpReqTo.TabIndex = 4
        Me.dtpReqTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(235, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 17)
        Me.Label3.TabIndex = 135
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpReqFrom
        '
        Me.dtpReqFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpReqFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReqFrom.Location = New System.Drawing.Point(97, 39)
        Me.dtpReqFrom.Name = "dtpReqFrom"
        Me.dtpReqFrom.ShowCheckBox = True
        Me.dtpReqFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpReqFrom.TabIndex = 3
        Me.dtpReqFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(6, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 17)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Req From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDepository
        '
        Me.cboDepository.FormattingEnabled = True
        Me.cboDepository.Location = New System.Drawing.Point(470, 66)
        Me.cboDepository.Name = "cboDepository"
        Me.cboDepository.Size = New System.Drawing.Size(105, 21)
        Me.cboDepository.TabIndex = 8
        '
        'cboFileName
        '
        Me.cboFileName.FormattingEnabled = True
        Me.cboFileName.Location = New System.Drawing.Point(470, 12)
        Me.cboFileName.Name = "cboFileName"
        Me.cboFileName.Size = New System.Drawing.Size(283, 21)
        Me.cboFileName.TabIndex = 2
        '
        'txtDrnNo
        '
        Me.txtDrnNo.Location = New System.Drawing.Point(648, 66)
        Me.txtDrnNo.MaxLength = 0
        Me.txtDrnNo.Name = "txtDrnNo"
        Me.txtDrnNo.Size = New System.Drawing.Size(105, 21)
        Me.txtDrnNo.TabIndex = 9
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(385, 68)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(78, 13)
        Me.Label21.TabIndex = 129
        Me.Label21.Text = "Depository"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDematPendId
        '
        Me.txtDematPendId.Location = New System.Drawing.Point(97, 120)
        Me.txtDematPendId.MaxLength = 0
        Me.txtDematPendId.Name = "txtDematPendId"
        Me.txtDematPendId.Size = New System.Drawing.Size(105, 21)
        Me.txtDematPendId.TabIndex = 14
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(290, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpTo.TabIndex = 1
        Me.dtpTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(235, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 17)
        Me.Label11.TabIndex = 115
        Me.Label11.Text = "To"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(97, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpFrom.TabIndex = 0
        Me.dtpFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(6, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 17)
        Me.Label10.TabIndex = 115
        Me.Label10.Text = "Import From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(470, 39)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(283, 21)
        Me.cboCompany.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(385, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 113
        Me.Label5.Text = "Company"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(681, 147)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 19
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(603, 147)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 18
        Me.btnClear.Text = "C&lear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(525, 147)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 17
        Me.btnRefresh.Text = "&Refresh"
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(566, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 113
        Me.Label2.Text = "DRN No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(569, 95)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 140
        Me.Label7.Text = "Cust Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(385, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 131
        Me.Label1.Text = "File Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(-1, 122)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 17)
        Me.Label12.TabIndex = 117
        Me.Label12.Text = "Demat Pend Id"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(6, 316)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(634, 33)
        Me.pnlExport.TabIndex = 2
        '
        'txtRecCount
        '
        Me.txtRecCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRecCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRecCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRecCount.Location = New System.Drawing.Point(6, 8)
        Me.txtRecCount.MaxLength = 100
        Me.txtRecCount.Name = "txtRecCount"
        Me.txtRecCount.ReadOnly = True
        Me.txtRecCount.Size = New System.Drawing.Size(433, 14)
        Me.txtRecCount.TabIndex = 0
        Me.txtRecCount.TabStop = False
        Me.txtRecCount.Text = "Record Count : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(558, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(6, 195)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(768, 108)
        Me.dgvReport.TabIndex = 1
        '
        'frmDematPendingReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(854, 354)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDematPendingReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Demat Pending Report"
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

        Call LoadData()

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

            If dtpReqFrom.Checked = True Then lsCond &= " and d.req_date >= '" & Format(CDate(dtpReqFrom.Value), "yyyy-MM-dd") & "' "
            If dtpReqTo.Checked = True Then lsCond &= " and d.req_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpReqTo.Value), "yyyy-MM-dd") & "' "

            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond &= " and d.comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            If cboFileName.Text <> "" And cboFileName.SelectedIndex >= 0 Then lsCond &= " and f.file_gid = '" & Val(cboFileName.SelectedValue.ToString) & "' "
            If cboDepository.Text <> "" And cboDepository.SelectedIndex >= 0 Then lsCond &= " and d.depository_code = '" & cboDepository.SelectedValue.ToString & "' "

            If txtDematPendId.Text <> "" Then lsCond &= " and d.dematpend_gid = '" & Val(txtDematPendId.Text) & "' "
            If txtInwardId.Text <> "" Then lsCond &= " and d.inward_gid = '" & Val(txtInwardId.Text) & "' "
            If txtDrnNo.Text <> "" Then lsCond &= " and d.drn_no like '" & QuoteFilter(txtDrnNo.Text) & "%' "
            If txtDpId.Text <> "" Then lsCond &= " and d.dp_id like '" & QuoteFilter(txtDpId.Text) & "%' "
            If txtClientId.Text <> "" Then lsCond &= " and d.client_id like '" & QuoteFilter(txtClientId.Text) & "%' "
            If txtPendFlag.Text <> "" Then lsCond &= " and d.dematpend_flag = '" & txtPendFlag.Text & "' "
            If Val(txtShareCount.Text) > 0 Then lsCond &= " and d.share_count = " & Val(txtShareCount.Text) & " "
            If txtFolioNo.Text <> "" Then lsCond &= " and o.folio_no = '" & QuoteFilter(txtFolioNo.Text) & "' "
            If txtCustName.Text <> "" Then lsCond &= " and d.cust_name = '" & QuoteFilter(txtFolioNo.Text) & "' "

            If Val(txtDematPendId.Text) > 0 Then lsCond &= " and d.dematpend_gid = " & Val(txtDematPendId.Text) & " "
            If Val(txtInwardId.Text) > 0 Then lsCond &= " and d.inward_gid = " & Val(txtInwardId.Text) & " "

            Select Case cboStatus.Text.ToString.ToUpper
                Case "PENDING"
                    lsCond &= " and d.inward_gid = 0 "
                Case "RECEIVED"
                    lsCond &= " and d.inward_gid > 0 "
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsFld = ""
            lsFld &= " c.comp_name as 'Company',"
            lsFld &= " p.depository_name as 'Depository',"
            lsFld &= " d.req_date as 'Req Date',"
            lsFld &= " d.drn_no as 'DRN No',"
            lsFld &= " o.folio_no as 'Folio No',"
            lsFld &= " d.cust_name as 'Customer Name',"
            lsFld &= " d.joint1_name as 'Joint1 Name',"
            lsFld &= " d.joint2_name as 'Joint2 Name',"
            lsFld &= " d.share_count as 'Share Count',"
            lsFld &= " d.client_id as 'Client Id',"
            lsFld &= " d.dp_id as 'DP Id',"
            lsFld &= " d.dematpend_flag as 'Pending Flag',"
            lsFld &= " i.inward_comp_no as 'Inward No',"
            lsFld &= " i.received_date as 'Received Date',"
            lsFld &= " d.dematpend_gid as 'Demat Pending Id',"
            lsFld &= " d.inward_gid as 'Inward Id',"
            lsFld &= " f.insert_date as 'Import Date',f.file_name as 'File Name',f.sheet_name as 'Sheet Name',"
            lsFld &= " f.insert_by as 'Import By',"
            lsFld &= " f.file_type as 'File Type',"
            lsFld &= " f.file_gid as 'File Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld

            lsSql &= " from sta_trn_tdematpend as d "
            lsSql &= " inner join sta_trn_tfile as f on d.file_gid = f.file_gid and f.delete_flag = 'N' "
            lsSql &= " inner join sta_trn_tfolio as o on d.folio_gid = o.folio_gid and o.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on d.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tdepository as p on d.depository_code = p.depository_code and p.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinward as i on d.inward_gid = i.inward_gid and i.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and d.delete_flag = 'N' "
            lsSql &= " order by d.dematpend_gid desc"

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

            With cboStatus
                .Items.Clear()
                .Items.Add("Received")
                .Items.Add("Pending")
            End With

            dtpFrom.Value = Now
            dtpTo.Value = Now

            dtpReqFrom.Value = Now
            dtpReqTo.Value = Now

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

        lsSql &= " and file_type in (" & gnFilePendNSDL & "," & gnFilePendCDSL & ") "
        lsSql &= " and delete_flag = 'N' "
        lsSql &= " order by file_gid desc"

        gpBindCombo(lsSql, "file_name", "file_gid", cboFileName, gOdbcConn)
    End Sub
End Class
