<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmiepfmaker
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
        Me.reqtyp_pannel = New System.Windows.Forms.Panel()
        Me.reprocess_btn = New System.Windows.Forms.Button()
        Me.Que_inex_btn = New System.Windows.Forms.Button()
        Me.inexreasonview_btn = New System.Windows.Forms.Button()
        Me.remark_txt = New System.Windows.Forms.TextBox()
        Me.chklst_inputs = New System.Windows.Forms.GroupBox()
        Me.reject_btn = New System.Windows.Forms.Button()
        Me.approve_btn = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dummy = New System.Windows.Forms.RadioButton()
        Me.claimant = New System.Windows.Forms.RadioButton()
        Me.nominee = New System.Windows.Forms.RadioButton()
        Me.shareholder = New System.Windows.Forms.RadioButton()
        Me.remark_lab = New System.Windows.Forms.Label()
        Me.inex = New System.Windows.Forms.Button()
        Me.submit = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pre_rmk_txt = New System.Windows.Forms.TextBox()
        Me.pre_rmk_lab = New System.Windows.Forms.Label()
        Me.status_value_lab = New System.Windows.Forms.Label()
        Me.status_lab = New System.Windows.Forms.Label()
        Me.dgvChecklist = New System.Windows.Forms.DataGridView()
        Me.foliono_txt = New System.Windows.Forms.TextBox()
        Me.foliono_lab = New System.Windows.Forms.Label()
        Me.dgv_cert_details = New System.Windows.Forms.DataGridView()
        Me.cert_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.issued_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.share_count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dist_series = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.claimant_addr_txt = New System.Windows.Forms.TextBox()
        Me.claimant_email_txt = New System.Windows.Forms.TextBox()
        Me.claimant_name_txt = New System.Windows.Forms.TextBox()
        Me.claimant_email_lab = New System.Windows.Forms.Label()
        Me.claimant_addr_lab = New System.Windows.Forms.Label()
        Me.claimant_name_lab = New System.Windows.Forms.Label()
        Me.sharevalue_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nominee_txt = New System.Windows.Forms.TextBox()
        Me.nominee_lab = New System.Windows.Forms.Label()
        Me.totsharecount_lab = New System.Windows.Forms.Label()
        Me.totalshare_txt = New System.Windows.Forms.TextBox()
        Me.name2_txt = New System.Windows.Forms.TextBox()
        Me.name1_txt = New System.Windows.Forms.TextBox()
        Me.name_txt = New System.Windows.Forms.TextBox()
        Me.cmp_txt = New System.Windows.Forms.TextBox()
        Me.joint2_lab = New System.Windows.Forms.Label()
        Me.joint1_lab = New System.Windows.Forms.Label()
        Me.name_lab = New System.Windows.Forms.Label()
        Me.cmp_lab = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.reqtyp_pannel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_cert_details, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'reqtyp_pannel
        '
        Me.reqtyp_pannel.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor
        Me.reqtyp_pannel.AutoSize = True
        Me.reqtyp_pannel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.reqtyp_pannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.reqtyp_pannel.Controls.Add(Me.inexreasonview_btn)
        Me.reqtyp_pannel.Controls.Add(Me.reprocess_btn)
        Me.reqtyp_pannel.Controls.Add(Me.Que_inex_btn)
        Me.reqtyp_pannel.Controls.Add(Me.remark_txt)
        Me.reqtyp_pannel.Controls.Add(Me.chklst_inputs)
        Me.reqtyp_pannel.Controls.Add(Me.reject_btn)
        Me.reqtyp_pannel.Controls.Add(Me.approve_btn)
        Me.reqtyp_pannel.Controls.Add(Me.Panel3)
        Me.reqtyp_pannel.Controls.Add(Me.remark_lab)
        Me.reqtyp_pannel.Controls.Add(Me.inex)
        Me.reqtyp_pannel.Controls.Add(Me.submit)
        Me.reqtyp_pannel.Controls.Add(Me.Panel1)
        Me.reqtyp_pannel.Location = New System.Drawing.Point(12, 310)
        Me.reqtyp_pannel.Name = "reqtyp_pannel"
        Me.reqtyp_pannel.Size = New System.Drawing.Size(1086, 363)
        Me.reqtyp_pannel.TabIndex = 1
        '
        'reprocess_btn
        '
        Me.reprocess_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reprocess_btn.Location = New System.Drawing.Point(731, 328)
        Me.reprocess_btn.Name = "reprocess_btn"
        Me.reprocess_btn.Size = New System.Drawing.Size(87, 23)
        Me.reprocess_btn.TabIndex = 31
        Me.reprocess_btn.Text = "Reporcess"
        Me.reprocess_btn.UseVisualStyleBackColor = True
        Me.reprocess_btn.Visible = False
        '
        'Que_inex_btn
        '
        Me.Que_inex_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Que_inex_btn.Location = New System.Drawing.Point(638, 328)
        Me.Que_inex_btn.Name = "Que_inex_btn"
        Me.Que_inex_btn.Size = New System.Drawing.Size(87, 23)
        Me.Que_inex_btn.TabIndex = 30
        Me.Que_inex_btn.Text = "Inex"
        Me.Que_inex_btn.UseVisualStyleBackColor = True
        Me.Que_inex_btn.Visible = False
        '
        'inexreasonview_btn
        '
        Me.inexreasonview_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inexreasonview_btn.Location = New System.Drawing.Point(925, 335)
        Me.inexreasonview_btn.Name = "inexreasonview_btn"
        Me.inexreasonview_btn.Size = New System.Drawing.Size(149, 23)
        Me.inexreasonview_btn.TabIndex = 29
        Me.inexreasonview_btn.Text = "Inex Reasons View"
        Me.inexreasonview_btn.UseVisualStyleBackColor = True
        Me.inexreasonview_btn.Visible = False
        '
        'remark_txt
        '
        Me.remark_txt.AcceptsReturn = True
        Me.remark_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.remark_txt.Location = New System.Drawing.Point(693, 12)
        Me.remark_txt.MaxLength = 255
        Me.remark_txt.Multiline = True
        Me.remark_txt.Name = "remark_txt"
        Me.remark_txt.Size = New System.Drawing.Size(383, 42)
        Me.remark_txt.TabIndex = 28
        '
        'chklst_inputs
        '
        Me.chklst_inputs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklst_inputs.Location = New System.Drawing.Point(638, 60)
        Me.chklst_inputs.Name = "chklst_inputs"
        Me.chklst_inputs.Size = New System.Drawing.Size(438, 248)
        Me.chklst_inputs.TabIndex = 27
        Me.chklst_inputs.TabStop = False
        Me.chklst_inputs.Text = "CheckList Inputs"
        '
        'reject_btn
        '
        Me.reject_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reject_btn.Location = New System.Drawing.Point(987, 310)
        Me.reject_btn.Name = "reject_btn"
        Me.reject_btn.Size = New System.Drawing.Size(87, 23)
        Me.reject_btn.TabIndex = 26
        Me.reject_btn.Text = "Reject"
        Me.reject_btn.UseVisualStyleBackColor = True
        Me.reject_btn.Visible = False
        '
        'approve_btn
        '
        Me.approve_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.approve_btn.Location = New System.Drawing.Point(894, 310)
        Me.approve_btn.Name = "approve_btn"
        Me.approve_btn.Size = New System.Drawing.Size(87, 23)
        Me.approve_btn.TabIndex = 25
        Me.approve_btn.Text = "Approve"
        Me.approve_btn.UseVisualStyleBackColor = True
        Me.approve_btn.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.dummy)
        Me.Panel3.Controls.Add(Me.claimant)
        Me.Panel3.Controls.Add(Me.nominee)
        Me.Panel3.Controls.Add(Me.shareholder)
        Me.Panel3.Location = New System.Drawing.Point(10, 12)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(596, 42)
        Me.Panel3.TabIndex = 24
        '
        'dummy
        '
        Me.dummy.AutoSize = True
        Me.dummy.Checked = True
        Me.dummy.Location = New System.Drawing.Point(339, 12)
        Me.dummy.Name = "dummy"
        Me.dummy.Size = New System.Drawing.Size(68, 17)
        Me.dummy.TabIndex = 24
        Me.dummy.TabStop = True
        Me.dummy.Text = "dummy"
        Me.dummy.UseVisualStyleBackColor = True
        Me.dummy.Visible = False
        '
        'claimant
        '
        Me.claimant.AutoSize = True
        Me.claimant.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.claimant.Location = New System.Drawing.Point(3, 12)
        Me.claimant.Name = "claimant"
        Me.claimant.Size = New System.Drawing.Size(75, 17)
        Me.claimant.TabIndex = 0
        Me.claimant.Text = "Claimant"
        Me.claimant.UseVisualStyleBackColor = True
        '
        'nominee
        '
        Me.nominee.AutoSize = True
        Me.nominee.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nominee.Location = New System.Drawing.Point(216, 12)
        Me.nominee.Name = "nominee"
        Me.nominee.Size = New System.Drawing.Size(74, 17)
        Me.nominee.TabIndex = 23
        Me.nominee.Text = "Nominee"
        Me.nominee.UseVisualStyleBackColor = True
        '
        'shareholder
        '
        Me.shareholder.AutoSize = True
        Me.shareholder.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.shareholder.Location = New System.Drawing.Point(98, 12)
        Me.shareholder.Name = "shareholder"
        Me.shareholder.Size = New System.Drawing.Size(95, 17)
        Me.shareholder.TabIndex = 1
        Me.shareholder.Text = "ShareHolder"
        Me.shareholder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.shareholder.UseVisualStyleBackColor = True
        '
        'remark_lab
        '
        Me.remark_lab.AutoSize = True
        Me.remark_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.remark_lab.Location = New System.Drawing.Point(635, 12)
        Me.remark_lab.Name = "remark_lab"
        Me.remark_lab.Size = New System.Drawing.Size(52, 13)
        Me.remark_lab.TabIndex = 19
        Me.remark_lab.Text = "Remark"
        '
        'inex
        '
        Me.inex.Enabled = False
        Me.inex.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inex.Location = New System.Drawing.Point(987, 315)
        Me.inex.Name = "inex"
        Me.inex.Size = New System.Drawing.Size(89, 24)
        Me.inex.TabIndex = 18
        Me.inex.Text = "Inex"
        Me.inex.UseVisualStyleBackColor = True
        '
        'submit
        '
        Me.submit.Enabled = False
        Me.submit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.submit.Location = New System.Drawing.Point(892, 315)
        Me.submit.Name = "submit"
        Me.submit.Size = New System.Drawing.Size(89, 24)
        Me.submit.TabIndex = 17
        Me.submit.Text = "Submit"
        Me.submit.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pre_rmk_txt)
        Me.Panel1.Controls.Add(Me.pre_rmk_lab)
        Me.Panel1.Controls.Add(Me.status_value_lab)
        Me.Panel1.Controls.Add(Me.status_lab)
        Me.Panel1.Controls.Add(Me.dgvChecklist)
        Me.Panel1.Location = New System.Drawing.Point(3, 60)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(611, 294)
        Me.Panel1.TabIndex = 14
        '
        'pre_rmk_txt
        '
        Me.pre_rmk_txt.AcceptsReturn = True
        Me.pre_rmk_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pre_rmk_txt.Location = New System.Drawing.Point(140, 252)
        Me.pre_rmk_txt.MaxLength = 255
        Me.pre_rmk_txt.Multiline = True
        Me.pre_rmk_txt.Name = "pre_rmk_txt"
        Me.pre_rmk_txt.ReadOnly = True
        Me.pre_rmk_txt.Size = New System.Drawing.Size(356, 39)
        Me.pre_rmk_txt.TabIndex = 29
        Me.pre_rmk_txt.Visible = False
        '
        'pre_rmk_lab
        '
        Me.pre_rmk_lab.AutoSize = True
        Me.pre_rmk_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pre_rmk_lab.Location = New System.Drawing.Point(12, 255)
        Me.pre_rmk_lab.Name = "pre_rmk_lab"
        Me.pre_rmk_lab.Size = New System.Drawing.Size(104, 13)
        Me.pre_rmk_lab.TabIndex = 15
        Me.pre_rmk_lab.Text = "Previous Remark"
        Me.pre_rmk_lab.Visible = False
        '
        'status_value_lab
        '
        Me.status_value_lab.AutoSize = True
        Me.status_value_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.status_value_lab.ForeColor = System.Drawing.SystemColors.Control
        Me.status_value_lab.Location = New System.Drawing.Point(552, 252)
        Me.status_value_lab.Name = "status_value_lab"
        Me.status_value_lab.Size = New System.Drawing.Size(44, 13)
        Me.status_value_lab.TabIndex = 14
        Me.status_value_lab.Text = "Status"
        Me.status_value_lab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'status_lab
        '
        Me.status_lab.AutoSize = True
        Me.status_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.status_lab.Location = New System.Drawing.Point(502, 251)
        Me.status_lab.Name = "status_lab"
        Me.status_lab.Size = New System.Drawing.Size(44, 13)
        Me.status_lab.TabIndex = 13
        Me.status_lab.Text = "Status"
        '
        'dgvChecklist
        '
        Me.dgvChecklist.AllowUserToAddRows = False
        Me.dgvChecklist.AllowUserToDeleteRows = False
        Me.dgvChecklist.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvChecklist.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvChecklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChecklist.Location = New System.Drawing.Point(7, 3)
        Me.dgvChecklist.Name = "dgvChecklist"
        Me.dgvChecklist.ReadOnly = True
        Me.dgvChecklist.Size = New System.Drawing.Size(596, 245)
        Me.dgvChecklist.TabIndex = 12
        '
        'foliono_txt
        '
        Me.foliono_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.foliono_txt.Location = New System.Drawing.Point(142, 14)
        Me.foliono_txt.Name = "foliono_txt"
        Me.foliono_txt.ReadOnly = True
        Me.foliono_txt.Size = New System.Drawing.Size(357, 21)
        Me.foliono_txt.TabIndex = 9
        '
        'foliono_lab
        '
        Me.foliono_lab.AutoSize = True
        Me.foliono_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.foliono_lab.Location = New System.Drawing.Point(7, 14)
        Me.foliono_lab.Name = "foliono_lab"
        Me.foliono_lab.Size = New System.Drawing.Size(50, 13)
        Me.foliono_lab.TabIndex = 6
        Me.foliono_lab.Text = "Folio No"
        '
        'dgv_cert_details
        '
        Me.dgv_cert_details.AllowUserToAddRows = False
        Me.dgv_cert_details.AllowUserToDeleteRows = False
        Me.dgv_cert_details.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgv_cert_details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_cert_details.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cert_no, Me.issued_date, Me.share_count, Me.dist_series})
        Me.dgv_cert_details.Location = New System.Drawing.Point(548, 12)
        Me.dgv_cert_details.Name = "dgv_cert_details"
        Me.dgv_cert_details.ReadOnly = True
        Me.dgv_cert_details.Size = New System.Drawing.Size(550, 292)
        Me.dgv_cert_details.TabIndex = 17
        '
        'cert_no
        '
        Me.cert_no.HeaderText = "Certificate No"
        Me.cert_no.Name = "cert_no"
        Me.cert_no.ReadOnly = True
        '
        'issued_date
        '
        Me.issued_date.HeaderText = "Issued Date"
        Me.issued_date.Name = "issued_date"
        Me.issued_date.ReadOnly = True
        '
        'share_count
        '
        Me.share_count.HeaderText = "Share Count"
        Me.share_count.Name = "share_count"
        Me.share_count.ReadOnly = True
        '
        'dist_series
        '
        Me.dist_series.HeaderText = "Dist Series"
        Me.dist_series.Name = "dist_series"
        Me.dist_series.ReadOnly = True
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.claimant_addr_txt)
        Me.Panel4.Controls.Add(Me.claimant_email_txt)
        Me.Panel4.Controls.Add(Me.claimant_name_txt)
        Me.Panel4.Controls.Add(Me.claimant_email_lab)
        Me.Panel4.Controls.Add(Me.claimant_addr_lab)
        Me.Panel4.Controls.Add(Me.claimant_name_lab)
        Me.Panel4.Controls.Add(Me.sharevalue_txt)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.nominee_txt)
        Me.Panel4.Controls.Add(Me.nominee_lab)
        Me.Panel4.Controls.Add(Me.totsharecount_lab)
        Me.Panel4.Controls.Add(Me.totalshare_txt)
        Me.Panel4.Controls.Add(Me.name2_txt)
        Me.Panel4.Controls.Add(Me.name1_txt)
        Me.Panel4.Controls.Add(Me.name_txt)
        Me.Panel4.Controls.Add(Me.cmp_txt)
        Me.Panel4.Controls.Add(Me.joint2_lab)
        Me.Panel4.Controls.Add(Me.foliono_txt)
        Me.Panel4.Controls.Add(Me.foliono_lab)
        Me.Panel4.Controls.Add(Me.joint1_lab)
        Me.Panel4.Controls.Add(Me.name_lab)
        Me.Panel4.Controls.Add(Me.cmp_lab)
        Me.Panel4.Location = New System.Drawing.Point(12, 13)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(504, 291)
        Me.Panel4.TabIndex = 18
        '
        'claimant_addr_txt
        '
        Me.claimant_addr_txt.AcceptsReturn = True
        Me.claimant_addr_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.claimant_addr_txt.Location = New System.Drawing.Point(142, 226)
        Me.claimant_addr_txt.MaxLength = 255
        Me.claimant_addr_txt.Multiline = True
        Me.claimant_addr_txt.Name = "claimant_addr_txt"
        Me.claimant_addr_txt.ReadOnly = True
        Me.claimant_addr_txt.Size = New System.Drawing.Size(357, 33)
        Me.claimant_addr_txt.TabIndex = 29
        '
        'claimant_email_txt
        '
        Me.claimant_email_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.claimant_email_txt.Location = New System.Drawing.Point(142, 265)
        Me.claimant_email_txt.Name = "claimant_email_txt"
        Me.claimant_email_txt.ReadOnly = True
        Me.claimant_email_txt.Size = New System.Drawing.Size(357, 21)
        Me.claimant_email_txt.TabIndex = 25
        '
        'claimant_name_txt
        '
        Me.claimant_name_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.claimant_name_txt.Location = New System.Drawing.Point(142, 200)
        Me.claimant_name_txt.Name = "claimant_name_txt"
        Me.claimant_name_txt.ReadOnly = True
        Me.claimant_name_txt.Size = New System.Drawing.Size(357, 21)
        Me.claimant_name_txt.TabIndex = 24
        '
        'claimant_email_lab
        '
        Me.claimant_email_lab.AutoSize = True
        Me.claimant_email_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.claimant_email_lab.Location = New System.Drawing.Point(7, 265)
        Me.claimant_email_lab.Name = "claimant_email_lab"
        Me.claimant_email_lab.Size = New System.Drawing.Size(58, 13)
        Me.claimant_email_lab.TabIndex = 23
        Me.claimant_email_lab.Text = "E-mail ID"
        '
        'claimant_addr_lab
        '
        Me.claimant_addr_lab.AutoSize = True
        Me.claimant_addr_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.claimant_addr_lab.Location = New System.Drawing.Point(7, 233)
        Me.claimant_addr_lab.Name = "claimant_addr_lab"
        Me.claimant_addr_lab.Size = New System.Drawing.Size(53, 13)
        Me.claimant_addr_lab.TabIndex = 22
        Me.claimant_addr_lab.Text = "Address"
        '
        'claimant_name_lab
        '
        Me.claimant_name_lab.AutoSize = True
        Me.claimant_name_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.claimant_name_lab.Location = New System.Drawing.Point(7, 199)
        Me.claimant_name_lab.Name = "claimant_name_lab"
        Me.claimant_name_lab.Size = New System.Drawing.Size(92, 13)
        Me.claimant_name_lab.TabIndex = 21
        Me.claimant_name_lab.Text = "Claimant Name"
        '
        'sharevalue_txt
        '
        Me.sharevalue_txt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sharevalue_txt.Location = New System.Drawing.Point(369, 174)
        Me.sharevalue_txt.Name = "sharevalue_txt"
        Me.sharevalue_txt.ReadOnly = True
        Me.sharevalue_txt.Size = New System.Drawing.Size(130, 20)
        Me.sharevalue_txt.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(289, 172)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Share Value"
        '
        'nominee_txt
        '
        Me.nominee_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nominee_txt.Location = New System.Drawing.Point(142, 145)
        Me.nominee_txt.Name = "nominee_txt"
        Me.nominee_txt.ReadOnly = True
        Me.nominee_txt.Size = New System.Drawing.Size(357, 21)
        Me.nominee_txt.TabIndex = 18
        '
        'nominee_lab
        '
        Me.nominee_lab.AutoSize = True
        Me.nominee_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nominee_lab.Location = New System.Drawing.Point(7, 145)
        Me.nominee_lab.Name = "nominee_lab"
        Me.nominee_lab.Size = New System.Drawing.Size(91, 13)
        Me.nominee_lab.TabIndex = 17
        Me.nominee_lab.Text = "Nominee Name"
        '
        'totsharecount_lab
        '
        Me.totsharecount_lab.AutoSize = True
        Me.totsharecount_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totsharecount_lab.Location = New System.Drawing.Point(7, 172)
        Me.totsharecount_lab.Name = "totsharecount_lab"
        Me.totsharecount_lab.Size = New System.Drawing.Size(108, 13)
        Me.totsharecount_lab.TabIndex = 16
        Me.totsharecount_lab.Text = "Total Share Count"
        '
        'totalshare_txt
        '
        Me.totalshare_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totalshare_txt.Location = New System.Drawing.Point(142, 172)
        Me.totalshare_txt.Name = "totalshare_txt"
        Me.totalshare_txt.ReadOnly = True
        Me.totalshare_txt.Size = New System.Drawing.Size(141, 21)
        Me.totalshare_txt.TabIndex = 15
        '
        'name2_txt
        '
        Me.name2_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.name2_txt.Location = New System.Drawing.Point(142, 118)
        Me.name2_txt.Name = "name2_txt"
        Me.name2_txt.ReadOnly = True
        Me.name2_txt.Size = New System.Drawing.Size(357, 21)
        Me.name2_txt.TabIndex = 13
        '
        'name1_txt
        '
        Me.name1_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.name1_txt.Location = New System.Drawing.Point(142, 92)
        Me.name1_txt.Name = "name1_txt"
        Me.name1_txt.ReadOnly = True
        Me.name1_txt.Size = New System.Drawing.Size(357, 21)
        Me.name1_txt.TabIndex = 12
        '
        'name_txt
        '
        Me.name_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.name_txt.Location = New System.Drawing.Point(142, 68)
        Me.name_txt.Name = "name_txt"
        Me.name_txt.ReadOnly = True
        Me.name_txt.Size = New System.Drawing.Size(357, 21)
        Me.name_txt.TabIndex = 11
        '
        'cmp_txt
        '
        Me.cmp_txt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmp_txt.Location = New System.Drawing.Point(142, 41)
        Me.cmp_txt.Name = "cmp_txt"
        Me.cmp_txt.ReadOnly = True
        Me.cmp_txt.Size = New System.Drawing.Size(357, 21)
        Me.cmp_txt.TabIndex = 10
        '
        'joint2_lab
        '
        Me.joint2_lab.AutoSize = True
        Me.joint2_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.joint2_lab.Location = New System.Drawing.Point(7, 119)
        Me.joint2_lab.Name = "joint2_lab"
        Me.joint2_lab.Size = New System.Drawing.Size(85, 13)
        Me.joint2_lab.TabIndex = 4
        Me.joint2_lab.Text = "Joint Holder 2"
        '
        'joint1_lab
        '
        Me.joint1_lab.AutoSize = True
        Me.joint1_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.joint1_lab.Location = New System.Drawing.Point(7, 92)
        Me.joint1_lab.Name = "joint1_lab"
        Me.joint1_lab.Size = New System.Drawing.Size(85, 13)
        Me.joint1_lab.TabIndex = 3
        Me.joint1_lab.Text = "Joint Holder 1"
        '
        'name_lab
        '
        Me.name_lab.AutoSize = True
        Me.name_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.name_lab.Location = New System.Drawing.Point(7, 66)
        Me.name_lab.Name = "name_lab"
        Me.name_lab.Size = New System.Drawing.Size(39, 13)
        Me.name_lab.TabIndex = 2
        Me.name_lab.Text = "Name"
        '
        'cmp_lab
        '
        Me.cmp_lab.AutoSize = True
        Me.cmp_lab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmp_lab.Location = New System.Drawing.Point(7, 38)
        Me.cmp_lab.Name = "cmp_lab"
        Me.cmp_lab.Size = New System.Drawing.Size(88, 13)
        Me.cmp_lab.TabIndex = 1
        Me.cmp_lab.Text = "Comany Name"
        '
        'frmiepfmaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1110, 681)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.dgv_cert_details)
        Me.Controls.Add(Me.reqtyp_pannel)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmiepfmaker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmiepfmaker"
        Me.reqtyp_pannel.ResumeLayout(False)
        Me.reqtyp_pannel.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_cert_details, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents reqtyp_pannel As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents claimant As System.Windows.Forms.RadioButton
    Friend WithEvents nominee As System.Windows.Forms.RadioButton
    Friend WithEvents shareholder As System.Windows.Forms.RadioButton
    Friend WithEvents foliono_txt As System.Windows.Forms.TextBox
    Friend WithEvents foliono_lab As System.Windows.Forms.Label
    Friend WithEvents remark_lab As System.Windows.Forms.Label
    Friend WithEvents inex As System.Windows.Forms.Button
    Friend WithEvents submit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents status_value_lab As System.Windows.Forms.Label
    Friend WithEvents status_lab As System.Windows.Forms.Label
    Friend WithEvents dgvChecklist As System.Windows.Forms.DataGridView
    Friend WithEvents dgv_cert_details As System.Windows.Forms.DataGridView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents totsharecount_lab As System.Windows.Forms.Label
    Friend WithEvents totalshare_txt As System.Windows.Forms.TextBox
    Friend WithEvents name2_txt As System.Windows.Forms.TextBox
    Friend WithEvents name1_txt As System.Windows.Forms.TextBox
    Friend WithEvents name_txt As System.Windows.Forms.TextBox
    Friend WithEvents cmp_txt As System.Windows.Forms.TextBox
    Friend WithEvents joint2_lab As System.Windows.Forms.Label
    Friend WithEvents joint1_lab As System.Windows.Forms.Label
    Friend WithEvents name_lab As System.Windows.Forms.Label
    Friend WithEvents cmp_lab As System.Windows.Forms.Label
    Friend WithEvents reject_btn As System.Windows.Forms.Button
    Friend WithEvents approve_btn As System.Windows.Forms.Button
    Friend WithEvents chklst_inputs As System.Windows.Forms.GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents nominee_lab As System.Windows.Forms.Label
    Friend WithEvents nominee_txt As System.Windows.Forms.TextBox
    Friend WithEvents dummy As System.Windows.Forms.RadioButton
    Friend WithEvents sharevalue_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pre_rmk_lab As System.Windows.Forms.Label
    Friend WithEvents pre_rmk_txt As System.Windows.Forms.TextBox
    Friend WithEvents inexreasonview_btn As System.Windows.Forms.Button
    Friend WithEvents claimant_addr_txt As System.Windows.Forms.TextBox
    Friend WithEvents claimant_email_txt As System.Windows.Forms.TextBox
    Friend WithEvents claimant_name_txt As System.Windows.Forms.TextBox
    Friend WithEvents claimant_email_lab As System.Windows.Forms.Label
    Friend WithEvents claimant_addr_lab As System.Windows.Forms.Label
    Friend WithEvents claimant_name_lab As System.Windows.Forms.Label
    Friend WithEvents remark_txt As System.Windows.Forms.TextBox
    Friend WithEvents reprocess_btn As System.Windows.Forms.Button
    Friend WithEvents Que_inex_btn As System.Windows.Forms.Button
    Friend WithEvents cert_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents issued_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents share_count As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dist_series As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
