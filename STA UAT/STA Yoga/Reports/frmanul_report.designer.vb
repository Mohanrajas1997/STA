<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmanul_report
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
        Me.cbtype = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtp_tranto = New System.Windows.Forms.DateTimePicker()
        Me.trandateto = New System.Windows.Forms.Label()
        Me.dtp_tranfrom = New System.Windows.Forms.DateTimePicker()
        Me.trandatefrom = New System.Windows.Forms.Label()
        Me.dtp_anualrpt = New System.Windows.Forms.DateTimePicker()
        Me.bendate = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.pnlSearch.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.cbtype)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.dtp_tranto)
        Me.pnlSearch.Controls.Add(Me.trandateto)
        Me.pnlSearch.Controls.Add(Me.dtp_tranfrom)
        Me.pnlSearch.Controls.Add(Me.trandatefrom)
        Me.pnlSearch.Controls.Add(Me.dtp_anualrpt)
        Me.pnlSearch.Controls.Add(Me.bendate)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(12, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(781, 72)
        Me.pnlSearch.TabIndex = 14
        '
        'cbtype
        '
        Me.cbtype.FormattingEnabled = True
        Me.cbtype.Location = New System.Drawing.Point(412, 10)
        Me.cbtype.Name = "cbtype"
        Me.cbtype.Size = New System.Drawing.Size(121, 21)
        Me.cbtype.TabIndex = 132
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(341, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 17)
        Me.Label5.TabIndex = 131
        Me.Label5.Text = "Type"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtp_tranto
        '
        Me.dtp_tranto.Checked = False
        Me.dtp_tranto.CustomFormat = "dd-MM-yyyy"
        Me.dtp_tranto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_tranto.Location = New System.Drawing.Point(321, 38)
        Me.dtp_tranto.Name = "dtp_tranto"
        Me.dtp_tranto.ShowCheckBox = True
        Me.dtp_tranto.Size = New System.Drawing.Size(118, 21)
        Me.dtp_tranto.TabIndex = 129
        Me.dtp_tranto.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        Me.dtp_tranto.Visible = False
        '
        'trandateto
        '
        Me.trandateto.AutoSize = True
        Me.trandateto.Location = New System.Drawing.Point(234, 42)
        Me.trandateto.Name = "trandateto"
        Me.trandateto.Size = New System.Drawing.Size(79, 13)
        Me.trandateto.TabIndex = 130
        Me.trandateto.Text = "Tran date To"
        Me.trandateto.Visible = False
        '
        'dtp_tranfrom
        '
        Me.dtp_tranfrom.Checked = False
        Me.dtp_tranfrom.CustomFormat = "dd-MM-yyyy"
        Me.dtp_tranfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_tranfrom.Location = New System.Drawing.Point(104, 37)
        Me.dtp_tranfrom.Name = "dtp_tranfrom"
        Me.dtp_tranfrom.ShowCheckBox = True
        Me.dtp_tranfrom.Size = New System.Drawing.Size(118, 21)
        Me.dtp_tranfrom.TabIndex = 127
        Me.dtp_tranfrom.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        Me.dtp_tranfrom.Visible = False
        '
        'trandatefrom
        '
        Me.trandatefrom.AutoSize = True
        Me.trandatefrom.Location = New System.Drawing.Point(7, 41)
        Me.trandatefrom.Name = "trandatefrom"
        Me.trandatefrom.Size = New System.Drawing.Size(94, 13)
        Me.trandatefrom.TabIndex = 128
        Me.trandatefrom.Text = "Tran date From"
        Me.trandatefrom.Visible = False
        '
        'dtp_anualrpt
        '
        Me.dtp_anualrpt.Checked = False
        Me.dtp_anualrpt.CustomFormat = "dd-MM-yyyy"
        Me.dtp_anualrpt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_anualrpt.Location = New System.Drawing.Point(630, 9)
        Me.dtp_anualrpt.Name = "dtp_anualrpt"
        Me.dtp_anualrpt.ShowCheckBox = True
        Me.dtp_anualrpt.Size = New System.Drawing.Size(118, 21)
        Me.dtp_anualrpt.TabIndex = 10
        Me.dtp_anualrpt.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        Me.dtp_anualrpt.Visible = False
        '
        'bendate
        '
        Me.bendate.AutoSize = True
        Me.bendate.Location = New System.Drawing.Point(547, 13)
        Me.bendate.Name = "bendate"
        Me.bendate.Size = New System.Drawing.Size(86, 13)
        Me.bendate.TabIndex = 126
        Me.bendate.Text = "Benpost Date "
        Me.bendate.Visible = False
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(65, 9)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(275, 21)
        Me.cboCompany.TabIndex = 1
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(621, 36)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(699, 36)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(543, 36)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(12, 90)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(781, 480)
        Me.dgvList.TabIndex = 15
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(688, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(12, 576)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(781, 46)
        Me.pnlExport.TabIndex = 16
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(3, 13)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(364, 14)
        Me.txtTotRec.TabIndex = 1
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'frmanul_report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(798, 631)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlSearch)
        Me.Name = "frmanul_report"
        Me.Text = "Annual return"
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents dtp_anualrpt As System.Windows.Forms.DateTimePicker
    Friend WithEvents bendate As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents dtp_tranfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents trandatefrom As System.Windows.Forms.Label
    Friend WithEvents dtp_tranto As System.Windows.Forms.DateTimePicker
    Friend WithEvents trandateto As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbtype As System.Windows.Forms.ComboBox
End Class
