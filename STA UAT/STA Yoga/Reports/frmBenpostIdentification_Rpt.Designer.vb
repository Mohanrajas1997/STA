<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBenpostIdentification_Rpt
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cboDeposit = New System.Windows.Forms.ComboBox()
        Me.lblDepository = New System.Windows.Forms.Label()
        Me.dtpBenpostTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpBenpostFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboFileName = New System.Windows.Forms.ComboBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.pnlMain.Controls.Add(Me.cboDeposit)
        Me.pnlMain.Controls.Add(Me.lblDepository)
        Me.pnlMain.Controls.Add(Me.dtpBenpostTo)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.dtpBenpostFrom)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.cboFileName)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Location = New System.Drawing.Point(9, 10)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1152, 96)
        Me.pnlMain.TabIndex = 0
        '
        'cboDeposit
        '
        Me.cboDeposit.FormattingEnabled = True
        Me.cboDeposit.Location = New System.Drawing.Point(146, 59)
        Me.cboDeposit.Name = "cboDeposit"
        Me.cboDeposit.Size = New System.Drawing.Size(157, 29)
        Me.cboDeposit.TabIndex = 142
        Me.cboDeposit.Visible = False
        '
        'lblDepository
        '
        Me.lblDepository.Location = New System.Drawing.Point(-2, 60)
        Me.lblDepository.Name = "lblDepository"
        Me.lblDepository.Size = New System.Drawing.Size(137, 24)
        Me.lblDepository.TabIndex = 141
        Me.lblDepository.Text = "Depository"
        Me.lblDepository.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDepository.Visible = False
        '
        'dtpBenpostTo
        '
        Me.dtpBenpostTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpBenpostTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBenpostTo.Location = New System.Drawing.Point(435, 17)
        Me.dtpBenpostTo.Name = "dtpBenpostTo"
        Me.dtpBenpostTo.ShowCheckBox = True
        Me.dtpBenpostTo.Size = New System.Drawing.Size(157, 27)
        Me.dtpBenpostTo.TabIndex = 4
        Me.dtpBenpostTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(352, 20)
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
        Me.dtpBenpostFrom.Location = New System.Drawing.Point(146, 17)
        Me.dtpBenpostFrom.Name = "dtpBenpostFrom"
        Me.dtpBenpostFrom.ShowCheckBox = True
        Me.dtpBenpostFrom.Size = New System.Drawing.Size(157, 27)
        Me.dtpBenpostFrom.TabIndex = 3
        Me.dtpBenpostFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(-2, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(137, 24)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Benpost From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboFileName
        '
        Me.cboFileName.FormattingEnabled = True
        Me.cboFileName.Location = New System.Drawing.Point(705, 17)
        Me.cboFileName.Name = "cboFileName"
        Me.cboFileName.Size = New System.Drawing.Size(425, 29)
        Me.cboFileName.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1022, 54)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 35)
        Me.btnClose.TabIndex = 25
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(904, 54)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(108, 35)
        Me.btnClear.TabIndex = 24
        Me.btnClear.Text = "C&lear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(788, 54)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(108, 35)
        Me.btnRefresh.TabIndex = 23
        Me.btnRefresh.Text = "&Refresh"
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
        Me.dgvReport.Location = New System.Drawing.Point(9, 109)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(1152, 535)
        Me.dgvReport.TabIndex = 1
        '
        'frmBenpostIdentification_Rpt
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(9, 20)
        Me.ClientSize = New System.Drawing.Size(854, 502)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmBenpostIdentification_Rpt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Benpost Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cboCond As System.Windows.Forms.ComboBox
    Friend WithEvents txtIfscCode As System.Windows.Forms.TextBox
    Friend WithEvents txtMicrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents txtRbiRefNo As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtPan3 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder3 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtPan2 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtShareCount As System.Windows.Forms.TextBox
    Friend WithEvents txtSebiRegNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtClientId As System.Windows.Forms.TextBox
    Friend WithEvents txtDpId As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPan1 As System.Windows.Forms.TextBox
    Friend WithEvents txtHolder1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpBenpostTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpBenpostFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboDepository As System.Windows.Forms.ComboBox
    Friend WithEvents cboFileName As System.Windows.Forms.ComboBox
    Friend WithEvents txtIsinId As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtBenpostId As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents cboDeposit As System.Windows.Forms.ComboBox
    Friend WithEvents lblDepository As System.Windows.Forms.Label
End Class
