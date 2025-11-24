<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDebarrtOrderReport
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
        Me.lblReporttype = New System.Windows.Forms.Label()
        Me.cboReporttype = New System.Windows.Forms.ComboBox()
        Me.txtPanno = New System.Windows.Forms.TextBox()
        Me.lblPanno = New System.Windows.Forms.Label()
        Me.txtOrderno = New System.Windows.Forms.TextBox()
        Me.txtHoldername = New System.Windows.Forms.TextBox()
        Me.lblHoldername = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblOrderto = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblOrderfrom = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblOrderno = New System.Windows.Forms.Label()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtRecCount = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.lblReporttype)
        Me.pnlMain.Controls.Add(Me.cboReporttype)
        Me.pnlMain.Controls.Add(Me.txtPanno)
        Me.pnlMain.Controls.Add(Me.lblPanno)
        Me.pnlMain.Controls.Add(Me.txtOrderno)
        Me.pnlMain.Controls.Add(Me.txtHoldername)
        Me.pnlMain.Controls.Add(Me.lblHoldername)
        Me.pnlMain.Controls.Add(Me.dtpTo)
        Me.pnlMain.Controls.Add(Me.lblOrderto)
        Me.pnlMain.Controls.Add(Me.dtpFrom)
        Me.pnlMain.Controls.Add(Me.lblOrderfrom)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Controls.Add(Me.lblOrderno)
        Me.pnlMain.Location = New System.Drawing.Point(14, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(699, 103)
        Me.pnlMain.TabIndex = 0
        '
        'lblReporttype
        '
        Me.lblReporttype.Location = New System.Drawing.Point(11, 68)
        Me.lblReporttype.Name = "lblReporttype"
        Me.lblReporttype.Size = New System.Drawing.Size(94, 13)
        Me.lblReporttype.TabIndex = 158
        Me.lblReporttype.Text = "Report Type"
        Me.lblReporttype.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboReporttype
        '
        Me.cboReporttype.FormattingEnabled = True
        Me.cboReporttype.Items.AddRange(New Object() {"Debarred", "Released"})
        Me.cboReporttype.Location = New System.Drawing.Point(114, 67)
        Me.cboReporttype.Name = "cboReporttype"
        Me.cboReporttype.Size = New System.Drawing.Size(121, 21)
        Me.cboReporttype.TabIndex = 157
        '
        'txtPanno
        '
        Me.txtPanno.Location = New System.Drawing.Point(561, 13)
        Me.txtPanno.MaxLength = 0
        Me.txtPanno.Name = "txtPanno"
        Me.txtPanno.Size = New System.Drawing.Size(122, 21)
        Me.txtPanno.TabIndex = 2
        '
        'lblPanno
        '
        Me.lblPanno.Location = New System.Drawing.Point(469, 15)
        Me.lblPanno.Name = "lblPanno"
        Me.lblPanno.Size = New System.Drawing.Size(85, 13)
        Me.lblPanno.TabIndex = 155
        Me.lblPanno.Text = "PAN No"
        Me.lblPanno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOrderno
        '
        Me.txtOrderno.Location = New System.Drawing.Point(561, 40)
        Me.txtOrderno.MaxLength = 0
        Me.txtOrderno.Name = "txtOrderno"
        Me.txtOrderno.Size = New System.Drawing.Size(122, 21)
        Me.txtOrderno.TabIndex = 4
        '
        'txtHoldername
        '
        Me.txtHoldername.Location = New System.Drawing.Point(113, 40)
        Me.txtHoldername.MaxLength = 0
        Me.txtHoldername.Name = "txtHoldername"
        Me.txtHoldername.Size = New System.Drawing.Size(347, 21)
        Me.txtHoldername.TabIndex = 3
        '
        'lblHoldername
        '
        Me.lblHoldername.Location = New System.Drawing.Point(11, 42)
        Me.lblHoldername.Name = "lblHoldername"
        Me.lblHoldername.Size = New System.Drawing.Size(94, 13)
        Me.lblHoldername.TabIndex = 138
        Me.lblHoldername.Text = "Holder Name"
        Me.lblHoldername.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(338, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(122, 21)
        Me.dtpTo.TabIndex = 1
        Me.dtpTo.Value = New Date(2024, 5, 31, 11, 8, 25, 0)
        '
        'lblOrderto
        '
        Me.lblOrderto.Location = New System.Drawing.Point(274, 14)
        Me.lblOrderto.Name = "lblOrderto"
        Me.lblOrderto.Size = New System.Drawing.Size(57, 17)
        Me.lblOrderto.TabIndex = 115
        Me.lblOrderto.Text = "Order To"
        Me.lblOrderto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(113, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(122, 21)
        Me.dtpFrom.TabIndex = 0
        Me.dtpFrom.Value = New Date(2024, 5, 31, 11, 9, 33, 0)
        '
        'lblOrderfrom
        '
        Me.lblOrderfrom.Location = New System.Drawing.Point(14, 14)
        Me.lblOrderfrom.Name = "lblOrderfrom"
        Me.lblOrderfrom.Size = New System.Drawing.Size(91, 17)
        Me.lblOrderfrom.TabIndex = 115
        Me.lblOrderfrom.Text = "Order From"
        Me.lblOrderfrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(599, 68)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 24)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(508, 68)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(84, 24)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "&Clear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(417, 68)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(84, 24)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "&Refresh"
        '
        'lblOrderno
        '
        Me.lblOrderno.Location = New System.Drawing.Point(469, 42)
        Me.lblOrderno.Name = "lblOrderno"
        Me.lblOrderno.Size = New System.Drawing.Size(85, 13)
        Me.lblOrderno.TabIndex = 140
        Me.lblOrderno.Text = "Order No"
        Me.lblOrderno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(13, 121)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(700, 227)
        Me.dgvReport.TabIndex = 0
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(14, 354)
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
        Me.btnExport.Location = New System.Drawing.Point(521, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'frmDebarrtOrderReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(727, 402)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "frmDebarrtOrderReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Debarrt/Release Order List (Pan No Wise)"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtPanno As System.Windows.Forms.TextBox
    Friend WithEvents lblPanno As System.Windows.Forms.Label
    Friend WithEvents txtOrderno As System.Windows.Forms.TextBox
    Friend WithEvents txtHoldername As System.Windows.Forms.TextBox
    Friend WithEvents lblHoldername As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblOrderto As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblOrderfrom As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblOrderno As System.Windows.Forms.Label
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents lblReporttype As System.Windows.Forms.Label
    Friend WithEvents cboReporttype As System.Windows.Forms.ComboBox
End Class
