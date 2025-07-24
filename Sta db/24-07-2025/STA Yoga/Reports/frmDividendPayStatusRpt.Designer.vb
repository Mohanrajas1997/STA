<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDividendShareCapitalRpt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.dtpPaidDateto = New System.Windows.Forms.DateTimePicker()
        Me.lblpaidto = New System.Windows.Forms.Label()
        Me.dtpissueDateTo = New System.Windows.Forms.DateTimePicker()
        Me.lblIssDateTo = New System.Windows.Forms.Label()
        Me.CboDivstatus = New System.Windows.Forms.ComboBox()
        Me.lblDivstatus = New System.Windows.Forms.Label()
        Me.dtpPaidDatefrm = New System.Windows.Forms.DateTimePicker()
        Me.lblPayDatefrm = New System.Windows.Forms.Label()
        Me.CboIssPaymode = New System.Windows.Forms.ComboBox()
        Me.lblIssPaymode = New System.Windows.Forms.Label()
        Me.dtpIssuedatefrm = New System.Windows.Forms.DateTimePicker()
        Me.lblIssueDateFrm = New System.Windows.Forms.Label()
        Me.txtFoliodpid = New System.Windows.Forms.TextBox()
        Me.lblFoliodpid = New System.Windows.Forms.Label()
        Me.CboFinyear = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlSearch.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.dtpPaidDateto)
        Me.pnlSearch.Controls.Add(Me.lblpaidto)
        Me.pnlSearch.Controls.Add(Me.dtpissueDateTo)
        Me.pnlSearch.Controls.Add(Me.lblIssDateTo)
        Me.pnlSearch.Controls.Add(Me.CboDivstatus)
        Me.pnlSearch.Controls.Add(Me.lblDivstatus)
        Me.pnlSearch.Controls.Add(Me.dtpPaidDatefrm)
        Me.pnlSearch.Controls.Add(Me.lblPayDatefrm)
        Me.pnlSearch.Controls.Add(Me.CboIssPaymode)
        Me.pnlSearch.Controls.Add(Me.lblIssPaymode)
        Me.pnlSearch.Controls.Add(Me.dtpIssuedatefrm)
        Me.pnlSearch.Controls.Add(Me.lblIssueDateFrm)
        Me.pnlSearch.Controls.Add(Me.txtFoliodpid)
        Me.pnlSearch.Controls.Add(Me.lblFoliodpid)
        Me.pnlSearch.Controls.Add(Me.CboFinyear)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Location = New System.Drawing.Point(12, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(778, 150)
        Me.pnlSearch.TabIndex = 5
        '
        'dtpPaidDateto
        '
        Me.dtpPaidDateto.Checked = False
        Me.dtpPaidDateto.CustomFormat = "dd-MM-yyyy"
        Me.dtpPaidDateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaidDateto.Location = New System.Drawing.Point(366, 84)
        Me.dtpPaidDateto.Name = "dtpPaidDateto"
        Me.dtpPaidDateto.ShowCheckBox = True
        Me.dtpPaidDateto.Size = New System.Drawing.Size(139, 21)
        Me.dtpPaidDateto.TabIndex = 154
        Me.dtpPaidDateto.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'lblpaidto
        '
        Me.lblpaidto.Location = New System.Drawing.Point(254, 88)
        Me.lblpaidto.Name = "lblpaidto"
        Me.lblpaidto.Size = New System.Drawing.Size(101, 17)
        Me.lblpaidto.TabIndex = 155
        Me.lblpaidto.Text = "Paid Date To"
        Me.lblpaidto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpissueDateTo
        '
        Me.dtpissueDateTo.Checked = False
        Me.dtpissueDateTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpissueDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpissueDateTo.Location = New System.Drawing.Point(366, 48)
        Me.dtpissueDateTo.Name = "dtpissueDateTo"
        Me.dtpissueDateTo.ShowCheckBox = True
        Me.dtpissueDateTo.Size = New System.Drawing.Size(139, 21)
        Me.dtpissueDateTo.TabIndex = 152
        Me.dtpissueDateTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'lblIssDateTo
        '
        Me.lblIssDateTo.Location = New System.Drawing.Point(256, 52)
        Me.lblIssDateTo.Name = "lblIssDateTo"
        Me.lblIssDateTo.Size = New System.Drawing.Size(99, 17)
        Me.lblIssDateTo.TabIndex = 153
        Me.lblIssDateTo.Text = "Issue Date To"
        Me.lblIssDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CboDivstatus
        '
        Me.CboDivstatus.FormattingEnabled = True
        Me.CboDivstatus.Items.AddRange(New Object() {"Unpaid", "Paid", "Later"})
        Me.CboDivstatus.Location = New System.Drawing.Point(111, 117)
        Me.CboDivstatus.Name = "CboDivstatus"
        Me.CboDivstatus.Size = New System.Drawing.Size(139, 21)
        Me.CboDivstatus.TabIndex = 150
        '
        'lblDivstatus
        '
        Me.lblDivstatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivstatus.Location = New System.Drawing.Point(3, 116)
        Me.lblDivstatus.Name = "lblDivstatus"
        Me.lblDivstatus.Size = New System.Drawing.Size(97, 20)
        Me.lblDivstatus.TabIndex = 151
        Me.lblDivstatus.Text = "Div Status"
        Me.lblDivstatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpPaidDatefrm
        '
        Me.dtpPaidDatefrm.Checked = False
        Me.dtpPaidDatefrm.CustomFormat = "dd-MM-yyyy"
        Me.dtpPaidDatefrm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaidDatefrm.Location = New System.Drawing.Point(111, 84)
        Me.dtpPaidDatefrm.Name = "dtpPaidDatefrm"
        Me.dtpPaidDatefrm.ShowCheckBox = True
        Me.dtpPaidDatefrm.Size = New System.Drawing.Size(139, 21)
        Me.dtpPaidDatefrm.TabIndex = 148
        Me.dtpPaidDatefrm.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'lblPayDatefrm
        '
        Me.lblPayDatefrm.Location = New System.Drawing.Point(-1, 88)
        Me.lblPayDatefrm.Name = "lblPayDatefrm"
        Me.lblPayDatefrm.Size = New System.Drawing.Size(101, 17)
        Me.lblPayDatefrm.TabIndex = 149
        Me.lblPayDatefrm.Text = "Paid Date From"
        Me.lblPayDatefrm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CboIssPaymode
        '
        Me.CboIssPaymode.FormattingEnabled = True
        Me.CboIssPaymode.Location = New System.Drawing.Point(618, 48)
        Me.CboIssPaymode.Name = "CboIssPaymode"
        Me.CboIssPaymode.Size = New System.Drawing.Size(150, 21)
        Me.CboIssPaymode.TabIndex = 146
        '
        'lblIssPaymode
        '
        Me.lblIssPaymode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIssPaymode.Location = New System.Drawing.Point(515, 48)
        Me.lblIssPaymode.Name = "lblIssPaymode"
        Me.lblIssPaymode.Size = New System.Drawing.Size(97, 20)
        Me.lblIssPaymode.TabIndex = 147
        Me.lblIssPaymode.Text = "Issue Pay Mode"
        Me.lblIssPaymode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpIssuedatefrm
        '
        Me.dtpIssuedatefrm.Checked = False
        Me.dtpIssuedatefrm.CustomFormat = "dd-MM-yyyy"
        Me.dtpIssuedatefrm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIssuedatefrm.Location = New System.Drawing.Point(111, 48)
        Me.dtpIssuedatefrm.Name = "dtpIssuedatefrm"
        Me.dtpIssuedatefrm.ShowCheckBox = True
        Me.dtpIssuedatefrm.Size = New System.Drawing.Size(139, 21)
        Me.dtpIssuedatefrm.TabIndex = 144
        Me.dtpIssuedatefrm.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'lblIssueDateFrm
        '
        Me.lblIssueDateFrm.Location = New System.Drawing.Point(-3, 52)
        Me.lblIssueDateFrm.Name = "lblIssueDateFrm"
        Me.lblIssueDateFrm.Size = New System.Drawing.Size(103, 17)
        Me.lblIssueDateFrm.TabIndex = 145
        Me.lblIssueDateFrm.Text = "Issue Date From"
        Me.lblIssueDateFrm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFoliodpid
        '
        Me.txtFoliodpid.Location = New System.Drawing.Point(618, 84)
        Me.txtFoliodpid.Name = "txtFoliodpid"
        Me.txtFoliodpid.Size = New System.Drawing.Size(150, 21)
        Me.txtFoliodpid.TabIndex = 134
        '
        'lblFoliodpid
        '
        Me.lblFoliodpid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFoliodpid.Location = New System.Drawing.Point(515, 84)
        Me.lblFoliodpid.Name = "lblFoliodpid"
        Me.lblFoliodpid.Size = New System.Drawing.Size(97, 20)
        Me.lblFoliodpid.TabIndex = 133
        Me.lblFoliodpid.Text = "Folio Dpid"
        Me.lblFoliodpid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CboFinyear
        '
        Me.CboFinyear.FormattingEnabled = True
        Me.CboFinyear.Location = New System.Drawing.Point(618, 15)
        Me.CboFinyear.Name = "CboFinyear"
        Me.CboFinyear.Size = New System.Drawing.Size(150, 21)
        Me.CboFinyear.TabIndex = 128
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(515, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 20)
        Me.Label1.TabIndex = 129
        Me.Label1.Text = "Financial Year"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(618, 115)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(111, 14)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(394, 21)
        Me.cboCompany.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(27, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 15)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(696, 115)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(540, 115)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 11
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(11, 168)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(944, 282)
        Me.dgvList.TabIndex = 6
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(12, 458)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(951, 33)
        Me.pnlExport.TabIndex = 7
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(3, 11)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(433, 14)
        Me.txtTotRec.TabIndex = 0
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(871, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'frmDividendShareCapitalRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 499)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlSearch)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "frmDividendShareCapitalRpt"
        Me.Text = "Dividend Pay Status Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlSearch As Panel
    Friend WithEvents CboFinyear As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnClear As Button
    Friend WithEvents cboCompany As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents txtFoliodpid As TextBox
    Friend WithEvents lblFoliodpid As Label
    Friend WithEvents dtpIssuedatefrm As DateTimePicker
    Friend WithEvents lblIssueDateFrm As Label
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents pnlExport As Panel
    Friend WithEvents txtTotRec As TextBox
    Friend WithEvents btnExport As Button
    Friend WithEvents CboIssPaymode As ComboBox
    Friend WithEvents lblIssPaymode As Label
    Friend WithEvents CboDivstatus As ComboBox
    Friend WithEvents lblDivstatus As Label
    Friend WithEvents dtpPaidDatefrm As DateTimePicker
    Friend WithEvents lblPayDatefrm As Label
    Friend WithEvents dtpissueDateTo As DateTimePicker
    Friend WithEvents lblIssDateTo As Label
    Friend WithEvents dtpPaidDateto As DateTimePicker
    Friend WithEvents lblpaidto As Label
End Class
