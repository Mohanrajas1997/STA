<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBenpostDebarrtPanReport
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
        Me.txtClientId = New System.Windows.Forms.TextBox()
        Me.txtDpId = New System.Windows.Forms.TextBox()
        Me.lblDpid = New System.Windows.Forms.Label()
        Me.lblClientId = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPanno = New System.Windows.Forms.TextBox()
        Me.lblPanno = New System.Windows.Forms.Label()
        Me.txtHoldername = New System.Windows.Forms.TextBox()
        Me.lblHoldername = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
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
        Me.pnlMain.Controls.Add(Me.txtClientId)
        Me.pnlMain.Controls.Add(Me.txtDpId)
        Me.pnlMain.Controls.Add(Me.lblDpid)
        Me.pnlMain.Controls.Add(Me.lblClientId)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.txtPanno)
        Me.pnlMain.Controls.Add(Me.lblPanno)
        Me.pnlMain.Controls.Add(Me.txtHoldername)
        Me.pnlMain.Controls.Add(Me.lblHoldername)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Location = New System.Drawing.Point(18, 14)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(728, 103)
        Me.pnlMain.TabIndex = 0
        '
        'txtClientId
        '
        Me.txtClientId.Location = New System.Drawing.Point(546, 40)
        Me.txtClientId.MaxLength = 0
        Me.txtClientId.Name = "txtClientId"
        Me.txtClientId.Size = New System.Drawing.Size(171, 21)
        Me.txtClientId.TabIndex = 3
        '
        'txtDpId
        '
        Me.txtDpId.Location = New System.Drawing.Point(546, 9)
        Me.txtDpId.MaxLength = 0
        Me.txtDpId.Name = "txtDpId"
        Me.txtDpId.Size = New System.Drawing.Size(171, 21)
        Me.txtDpId.TabIndex = 1
        '
        'lblDpid
        '
        Me.lblDpid.Location = New System.Drawing.Point(466, 13)
        Me.lblDpid.Name = "lblDpid"
        Me.lblDpid.Size = New System.Drawing.Size(73, 13)
        Me.lblDpid.TabIndex = 163
        Me.lblDpid.Text = "DP Id"
        Me.lblDpid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblClientId
        '
        Me.lblClientId.Location = New System.Drawing.Point(467, 43)
        Me.lblClientId.Name = "lblClientId"
        Me.lblClientId.Size = New System.Drawing.Size(73, 13)
        Me.lblClientId.TabIndex = 164
        Me.lblClientId.Text = "Client Id"
        Me.lblClientId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(113, 11)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(347, 21)
        Me.cboCompany.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(32, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 15)
        Me.Label3.TabIndex = 160
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPanno
        '
        Me.txtPanno.Location = New System.Drawing.Point(113, 68)
        Me.txtPanno.MaxLength = 0
        Me.txtPanno.Name = "txtPanno"
        Me.txtPanno.Size = New System.Drawing.Size(171, 21)
        Me.txtPanno.TabIndex = 4
        '
        'lblPanno
        '
        Me.lblPanno.Location = New System.Drawing.Point(36, 70)
        Me.lblPanno.Name = "lblPanno"
        Me.lblPanno.Size = New System.Drawing.Size(70, 13)
        Me.lblPanno.TabIndex = 155
        Me.lblPanno.Text = "PAN No"
        Me.lblPanno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHoldername
        '
        Me.txtHoldername.Location = New System.Drawing.Point(113, 40)
        Me.txtHoldername.MaxLength = 0
        Me.txtHoldername.Name = "txtHoldername"
        Me.txtHoldername.Size = New System.Drawing.Size(347, 21)
        Me.txtHoldername.TabIndex = 2
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
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(646, 68)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(568, 68)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "&Clear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(490, 68)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "&Refresh"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(18, 356)
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
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(17, 123)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(729, 227)
        Me.dgvReport.TabIndex = 1
        '
        'frmBenpostDebarrtPanReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 402)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvReport)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "frmBenpostDebarrtPanReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Debarrt Pan List (Benpost)"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPanno As System.Windows.Forms.TextBox
    Friend WithEvents lblPanno As System.Windows.Forms.Label
    Friend WithEvents txtHoldername As System.Windows.Forms.TextBox
    Friend WithEvents lblHoldername As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents txtClientId As System.Windows.Forms.TextBox
    Friend WithEvents txtDpId As System.Windows.Forms.TextBox
    Friend WithEvents lblDpid As System.Windows.Forms.Label
    Friend WithEvents lblClientId As System.Windows.Forms.Label
End Class
