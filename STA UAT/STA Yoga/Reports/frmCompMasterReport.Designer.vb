<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompMasterReport
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
        Me.txtISIN = New System.Windows.Forms.TextBox()
        Me.lblISIN = New System.Windows.Forms.Label()
        Me.lblCompName = New System.Windows.Forms.Label()
        Me.txtPanno = New System.Windows.Forms.TextBox()
        Me.lblPanno = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtRecCount = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.cboEntryMode = New System.Windows.Forms.ComboBox()
        Me.lblEntryMode = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.cboEntryMode)
        Me.pnlMain.Controls.Add(Me.lblEntryMode)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.txtISIN)
        Me.pnlMain.Controls.Add(Me.lblISIN)
        Me.pnlMain.Controls.Add(Me.lblCompName)
        Me.pnlMain.Controls.Add(Me.txtPanno)
        Me.pnlMain.Controls.Add(Me.lblPanno)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Location = New System.Drawing.Point(14, 14)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(768, 77)
        Me.pnlMain.TabIndex = 3
        '
        'txtISIN
        '
        Me.txtISIN.Location = New System.Drawing.Point(589, 9)
        Me.txtISIN.MaxLength = 0
        Me.txtISIN.Name = "txtISIN"
        Me.txtISIN.Size = New System.Drawing.Size(171, 27)
        Me.txtISIN.TabIndex = 1
        '
        'lblISIN
        '
        Me.lblISIN.Location = New System.Drawing.Point(512, 11)
        Me.lblISIN.Name = "lblISIN"
        Me.lblISIN.Size = New System.Drawing.Size(70, 13)
        Me.lblISIN.TabIndex = 168
        Me.lblISIN.Text = "Isin ID"
        Me.lblISIN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompName
        '
        Me.lblCompName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompName.Location = New System.Drawing.Point(32, 13)
        Me.lblCompName.Name = "lblCompName"
        Me.lblCompName.Size = New System.Drawing.Size(73, 15)
        Me.lblCompName.TabIndex = 160
        Me.lblCompName.Text = "Company"
        Me.lblCompName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPanno
        '
        Me.txtPanno.Location = New System.Drawing.Point(113, 41)
        Me.txtPanno.MaxLength = 0
        Me.txtPanno.Name = "txtPanno"
        Me.txtPanno.Size = New System.Drawing.Size(171, 27)
        Me.txtPanno.TabIndex = 2
        '
        'lblPanno
        '
        Me.lblPanno.Location = New System.Drawing.Point(36, 43)
        Me.lblPanno.Name = "lblPanno"
        Me.lblPanno.Size = New System.Drawing.Size(70, 13)
        Me.lblPanno.TabIndex = 155
        Me.lblPanno.Text = "PAN No"
        Me.lblPanno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(686, 43)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(608, 43)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "&Clear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(530, 43)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "&Refresh"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(14, 356)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(555, 33)
        Me.pnlExport.TabIndex = 5
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
        Me.txtRecCount.Size = New System.Drawing.Size(433, 20)
        Me.txtRecCount.TabIndex = 0
        Me.txtRecCount.TabStop = False
        Me.txtRecCount.Text = "Record Count : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(465, 5)
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
        Me.dgvReport.Location = New System.Drawing.Point(13, 97)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(769, 253)
        Me.dgvReport.TabIndex = 4
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(113, 7)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(394, 29)
        Me.cboCompany.TabIndex = 169
        '
        'cboEntryMode
        '
        Me.cboEntryMode.FormattingEnabled = True
        Me.cboEntryMode.Items.AddRange(New Object() {"New", "Edit"})
        Me.cboEntryMode.Location = New System.Drawing.Point(373, 42)
        Me.cboEntryMode.Name = "cboEntryMode"
        Me.cboEntryMode.Size = New System.Drawing.Size(134, 29)
        Me.cboEntryMode.TabIndex = 171
        '
        'lblEntryMode
        '
        Me.lblEntryMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEntryMode.Location = New System.Drawing.Point(292, 43)
        Me.lblEntryMode.Name = "lblEntryMode"
        Me.lblEntryMode.Size = New System.Drawing.Size(73, 15)
        Me.lblEntryMode.TabIndex = 170
        Me.lblEntryMode.Text = "Entry Mode"
        Me.lblEntryMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCompMasterReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 394)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvReport)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "frmCompMasterReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Company Master Report"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtISIN As System.Windows.Forms.TextBox
    Friend WithEvents lblISIN As System.Windows.Forms.Label
    Friend WithEvents lblCompName As System.Windows.Forms.Label
    Friend WithEvents txtPanno As System.Windows.Forms.TextBox
    Friend WithEvents lblPanno As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents cboEntryMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblEntryMode As System.Windows.Forms.Label
End Class
