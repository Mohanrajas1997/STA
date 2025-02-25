<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDaywisebenpostreport
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
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboSecurityType = New System.Windows.Forms.ComboBox()
        Me.lblSecurityType = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.dtpBenpost = New System.Windows.Forms.DateTimePicker()
        Me.lblBenpostDate = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.txtISIN = New System.Windows.Forms.TextBox()
        Me.pnlSearch.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.txtISIN)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Controls.Add(Me.cboSecurityType)
        Me.pnlSearch.Controls.Add(Me.lblSecurityType)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.dtpBenpost)
        Me.pnlSearch.Controls.Add(Me.lblBenpostDate)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Location = New System.Drawing.Point(6, 6)
        Me.pnlSearch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(761, 112)
        Me.pnlSearch.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 21)
        Me.Label1.TabIndex = 128
        Me.Label1.Text = "ISIN ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboSecurityType
        '
        Me.cboSecurityType.FormattingEnabled = True
        Me.cboSecurityType.Location = New System.Drawing.Point(454, 42)
        Me.cboSecurityType.Name = "cboSecurityType"
        Me.cboSecurityType.Size = New System.Drawing.Size(298, 29)
        Me.cboSecurityType.TabIndex = 126
        '
        'lblSecurityType
        '
        Me.lblSecurityType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSecurityType.Location = New System.Drawing.Point(320, 42)
        Me.lblSecurityType.Name = "lblSecurityType"
        Me.lblSecurityType.Size = New System.Drawing.Size(128, 21)
        Me.lblSecurityType.TabIndex = 127
        Me.lblSecurityType.Text = "SecurityType"
        Me.lblSecurityType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(454, 9)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(298, 29)
        Me.cboCompany.TabIndex = 124
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(315, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 21)
        Me.Label3.TabIndex = 125
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(568, 77)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 25)
        Me.btnClear.TabIndex = 10
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'dtpBenpost
        '
        Me.dtpBenpost.CustomFormat = "dd-MM-yyyy"
        Me.dtpBenpost.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBenpost.Location = New System.Drawing.Point(145, 7)
        Me.dtpBenpost.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtpBenpost.Name = "dtpBenpost"
        Me.dtpBenpost.ShowCheckBox = True
        Me.dtpBenpost.Size = New System.Drawing.Size(165, 27)
        Me.dtpBenpost.TabIndex = 0
        Me.dtpBenpost.Value = New Date(2024, 7, 1, 0, 0, 0, 0)
        '
        'lblBenpostDate
        '
        Me.lblBenpostDate.Location = New System.Drawing.Point(6, 10)
        Me.lblBenpostDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBenpostDate.Name = "lblBenpostDate"
        Me.lblBenpostDate.Size = New System.Drawing.Size(131, 21)
        Me.lblBenpostDate.TabIndex = 119
        Me.lblBenpostDate.Text = "Benpost Date"
        Me.lblBenpostDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(663, 77)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 25)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(472, 77)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(88, 25)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(656, 5)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(88, 25)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(15, 467)
        Me.pnlExport.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(752, 35)
        Me.pnlExport.TabIndex = 8
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(4, 12)
        Me.txtTotRec.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(529, 20)
        Me.txtTotRec.TabIndex = 0
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(6, 124)
        Me.dgvList.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(761, 337)
        Me.dgvList.TabIndex = 7
        '
        'txtISIN
        '
        Me.txtISIN.Location = New System.Drawing.Point(145, 42)
        Me.txtISIN.MaxLength = 0
        Me.txtISIN.Name = "txtISIN"
        Me.txtISIN.Size = New System.Drawing.Size(165, 27)
        Me.txtISIN.TabIndex = 168
        '
        'frmDaywisebenpostreport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 503)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvList)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmDaywisebenpostreport"
        Me.Text = "Day wise Benpost Report"
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents dtpBenpost As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblBenpostDate As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboSecurityType As System.Windows.Forms.ComboBox
    Friend WithEvents lblSecurityType As System.Windows.Forms.Label
    Friend WithEvents txtISIN As System.Windows.Forms.TextBox
End Class
