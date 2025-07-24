<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_SH2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_SH2))
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dtp_dateto = New System.Windows.Forms.DateTimePicker()
        Me.sh2dateto = New System.Windows.Forms.Label()
        Me.dtp_datefrom = New System.Windows.Forms.DateTimePicker()
        Me.sh2datefrom = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panelhead = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Sno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.folio_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.shareholder_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.approved_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.equity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.issue_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cert_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.share_count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dist = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dist_to = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.adate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.trantype_desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.scount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tdb2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tdb3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        Me.Panelhead.SuspendLayout()
        Me.SuspendLayout()
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
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(688, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'dtp_dateto
        '
        Me.dtp_dateto.Checked = False
        Me.dtp_dateto.CustomFormat = "dd-MM-yyyy"
        Me.dtp_dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_dateto.Location = New System.Drawing.Point(652, 9)
        Me.dtp_dateto.Name = "dtp_dateto"
        Me.dtp_dateto.ShowCheckBox = True
        Me.dtp_dateto.Size = New System.Drawing.Size(118, 21)
        Me.dtp_dateto.TabIndex = 129
        Me.dtp_dateto.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'sh2dateto
        '
        Me.sh2dateto.AutoSize = True
        Me.sh2dateto.Location = New System.Drawing.Point(591, 13)
        Me.sh2dateto.Name = "sh2dateto"
        Me.sh2dateto.Size = New System.Drawing.Size(51, 13)
        Me.sh2dateto.TabIndex = 130
        Me.sh2dateto.Text = "Date To"
        '
        'dtp_datefrom
        '
        Me.dtp_datefrom.Checked = False
        Me.dtp_datefrom.CustomFormat = "dd-MM-yyyy"
        Me.dtp_datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_datefrom.Location = New System.Drawing.Point(442, 8)
        Me.dtp_datefrom.Name = "dtp_datefrom"
        Me.dtp_datefrom.ShowCheckBox = True
        Me.dtp_datefrom.Size = New System.Drawing.Size(118, 21)
        Me.dtp_datefrom.TabIndex = 127
        Me.dtp_datefrom.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'sh2datefrom
        '
        Me.sh2datefrom.AutoSize = True
        Me.sh2datefrom.Location = New System.Drawing.Point(368, 12)
        Me.sh2datefrom.Name = "sh2datefrom"
        Me.sh2datefrom.Size = New System.Drawing.Size(66, 13)
        Me.sh2datefrom.TabIndex = 128
        Me.sh2datefrom.Text = "Date From"
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(65, 9)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(297, 21)
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
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Sno, Me.folio_no, Me.shareholder_name, Me.approved_date, Me.equity, Me.issue_date, Me.cert_no, Me.share_count, Me.Dist, Me.dist_to, Me.adate, Me.trantype_desc, Me.tbd, Me.scount, Me.tdb2, Me.tdb3})
        Me.dgvList.Location = New System.Drawing.Point(12, 180)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(781, 480)
        Me.dgvList.TabIndex = 21
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.dtp_dateto)
        Me.pnlSearch.Controls.Add(Me.sh2dateto)
        Me.pnlSearch.Controls.Add(Me.dtp_datefrom)
        Me.pnlSearch.Controls.Add(Me.sh2datefrom)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(11, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(782, 161)
        Me.pnlSearch.TabIndex = 20
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(11, 670)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(781, 46)
        Me.pnlExport.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(212, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 18)
        Me.Label3.TabIndex = 131
        Me.Label3.Text = "FORM NO. SH.2"
        '
        'Panelhead
        '
        Me.Panelhead.Controls.Add(Me.Label4)
        Me.Panelhead.Controls.Add(Me.Label2)
        Me.Panelhead.Controls.Add(Me.Label3)
        Me.Panelhead.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panelhead.Location = New System.Drawing.Point(117, 81)
        Me.Panelhead.Name = "Panelhead"
        Me.Panelhead.Size = New System.Drawing.Size(586, 81)
        Me.Panelhead.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(2, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(582, 26)
        Me.Label4.TabIndex = 133
        Me.Label4.Text = resources.GetString("Label4.Text")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(99, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(336, 14)
        Me.Label2.TabIndex = 132
        Me.Label2.Text = "Register of Renewed and Duplicate Share Certificates"
        '
        'Sno
        '
        Me.Sno.DataPropertyName = "Sno"
        Me.Sno.HeaderText = "S. No."
        Me.Sno.Name = "Sno"
        Me.Sno.ReadOnly = True
        '
        'folio_no
        '
        Me.folio_no.DataPropertyName = "folio_no"
        Me.folio_no.HeaderText = "Folio No."
        Me.folio_no.Name = "folio_no"
        Me.folio_no.ReadOnly = True
        '
        'shareholder_name
        '
        Me.shareholder_name.DataPropertyName = "shareholder_name"
        Me.shareholder_name.HeaderText = "Name of the person(s) to whom Renewed / Duplicate share certificate is issued"
        Me.shareholder_name.Name = "shareholder_name"
        Me.shareholder_name.ReadOnly = True
        '
        'approved_date
        '
        Me.approved_date.DataPropertyName = "app_date"
        Me.approved_date.HeaderText = "Date of approval of issue of Renewed/ Duplicate share certificate"
        Me.approved_date.Name = "approved_date"
        Me.approved_date.ReadOnly = True
        '
        'equity
        '
        Me.equity.DataPropertyName = "equity"
        Me.equity.HeaderText = "Class of shares"
        Me.equity.Name = "equity"
        Me.equity.ReadOnly = True
        '
        'issue_date
        '
        Me.issue_date.DataPropertyName = "issdate"
        Me.issue_date.HeaderText = "Date of issue of original share certificate"
        Me.issue_date.Name = "issue_date"
        Me.issue_date.ReadOnly = True
        '
        'cert_no
        '
        Me.cert_no.DataPropertyName = "cert_no"
        Me.cert_no.HeaderText = "Original share certificate number"
        Me.cert_no.Name = "cert_no"
        Me.cert_no.ReadOnly = True
        '
        'share_count
        '
        Me.share_count.DataPropertyName = "share_count"
        Me.share_count.HeaderText = "Total number of shares in the Original Share Certificate"
        Me.share_count.Name = "share_count"
        Me.share_count.ReadOnly = True
        '
        'Dist
        '
        Me.Dist.DataPropertyName = "dist_from"
        Me.Dist.HeaderText = "Distinctive No. From"
        Me.Dist.Name = "Dist"
        Me.Dist.ReadOnly = True
        '
        'dist_to
        '
        Me.dist_to.DataPropertyName = "dist_to"
        Me.dist_to.HeaderText = "Distinctive No. To"
        Me.dist_to.Name = "dist_to"
        Me.dist_to.ReadOnly = True
        '
        'adate
        '
        Me.adate.DataPropertyName = "adate"
        Me.adate.HeaderText = "Date of issue of Renewed/ Duplicate Share Certificate"
        Me.adate.Name = "adate"
        Me.adate.ReadOnly = True
        '
        'trantype_desc
        '
        Me.trantype_desc.DataPropertyName = "trantype_desc"
        Me.trantype_desc.HeaderText = "Reasons for issue of Renewed/ Duplicate Share Certificate"
        Me.trantype_desc.Name = "trantype_desc"
        Me.trantype_desc.ReadOnly = True
        '
        'tbd
        '
        Me.tbd.DataPropertyName = "tran_cert_no"
        Me.tbd.HeaderText = "Number of the Renewed share certificate, if applicable"
        Me.tbd.Name = "tbd"
        Me.tbd.ReadOnly = True
        '
        'scount
        '
        Me.scount.DataPropertyName = "scount"
        Me.scount.HeaderText = "Total Number of Shares in the Renewed/ Duplicate Share Certificate"
        Me.scount.Name = "scount"
        Me.scount.ReadOnly = True
        '
        'tdb2
        '
        Me.tdb2.DataPropertyName = "tdb2"
        Me.tdb2.HeaderText = "Reference to entry in Register of Members"
        Me.tdb2.Name = "tdb2"
        Me.tdb2.ReadOnly = True
        '
        'tdb3
        '
        Me.tdb3.DataPropertyName = "tdb3"
        Me.tdb3.HeaderText = "Remarks"
        Me.tdb3.Name = "tdb3"
        Me.tdb3.ReadOnly = True
        '
        'frm_SH2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 723)
        Me.Controls.Add(Me.Panelhead)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlExport)
        Me.Name = "frm_SH2"
        Me.Text = "frm_SH2"
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.Panelhead.ResumeLayout(False)
        Me.Panelhead.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dtp_dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents sh2dateto As System.Windows.Forms.Label
    Friend WithEvents dtp_datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents sh2datefrom As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panelhead As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Sno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents folio_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents shareholder_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents approved_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents equity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents issue_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cert_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents share_count As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dist As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dist_to As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents adate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents trantype_desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tbd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents scount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tdb2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tdb3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
