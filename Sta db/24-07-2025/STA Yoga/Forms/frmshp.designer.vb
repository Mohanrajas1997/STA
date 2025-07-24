<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmshp
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
        Me.dtpBenpost = New System.Windows.Forms.DateTimePicker()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvList1 = New System.Windows.Forms.DataGridView()
        Me.dgvList2 = New System.Windows.Forms.DataGridView()
        Me.dgvList3 = New System.Windows.Forms.DataGridView()
        Me.dgvList4 = New System.Windows.Forms.DataGridView()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.equity_sharescount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ABC2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total_percentage_rights = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total_votingright = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.votingright_count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.calculation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total_sharecount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.paided_sharecount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Shareholder_count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.lblPromoter = New System.Windows.Forms.Label()
        Me.lblFrpromoter = New System.Windows.Forms.Label()
        Me.lblInstitutions = New System.Windows.Forms.Label()
        Me.lblNoninstitutions = New System.Windows.Forms.Label()
        Me.dgvList5 = New System.Windows.Forms.DataGridView()
        Me.lbdrshares = New System.Windows.Forms.Label()
        Me.lblemptrust = New System.Windows.Forms.Label()
        CType(Me.dgvList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvList2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvList3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvList4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvList5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpBenpost
        '
        Me.dtpBenpost.CustomFormat = "dd-MM-yyyy"
        Me.dtpBenpost.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBenpost.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBenpost.Location = New System.Drawing.Point(639, 7)
        Me.dtpBenpost.Name = "dtpBenpost"
        Me.dtpBenpost.Size = New System.Drawing.Size(105, 20)
        Me.dtpBenpost.TabIndex = 12
        Me.dtpBenpost.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(161, 6)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(299, 21)
        Me.cboCompany.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(528, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(1, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Company Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(691, 35)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(74, 23)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(531, 35)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 23)
        Me.btnSearch.TabIndex = 10
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(771, 35)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 23)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(611, 35)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(74, 23)
        Me.btnExport.TabIndex = 12
        Me.btnExport.Text = "&Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'dgvList1
        '
        Me.dgvList1.AllowUserToAddRows = False
        Me.dgvList1.AllowUserToDeleteRows = False
        Me.dgvList1.BackgroundColor = System.Drawing.Color.White
        Me.dgvList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList1.Location = New System.Drawing.Point(12, 173)
        Me.dgvList1.Name = "dgvList1"
        Me.dgvList1.ReadOnly = True
        Me.dgvList1.Size = New System.Drawing.Size(809, 86)
        Me.dgvList1.TabIndex = 2
        '
        'dgvList2
        '
        Me.dgvList2.AllowUserToAddRows = False
        Me.dgvList2.AllowUserToDeleteRows = False
        Me.dgvList2.BackgroundColor = System.Drawing.Color.White
        Me.dgvList2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList2.Location = New System.Drawing.Point(12, 265)
        Me.dgvList2.Name = "dgvList2"
        Me.dgvList2.ReadOnly = True
        Me.dgvList2.Size = New System.Drawing.Size(811, 86)
        Me.dgvList2.TabIndex = 3
        '
        'dgvList3
        '
        Me.dgvList3.AllowUserToAddRows = False
        Me.dgvList3.AllowUserToDeleteRows = False
        Me.dgvList3.BackgroundColor = System.Drawing.Color.White
        Me.dgvList3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList3.Location = New System.Drawing.Point(10, 357)
        Me.dgvList3.Name = "dgvList3"
        Me.dgvList3.ReadOnly = True
        Me.dgvList3.Size = New System.Drawing.Size(811, 86)
        Me.dgvList3.TabIndex = 4
        '
        'dgvList4
        '
        Me.dgvList4.AllowUserToAddRows = False
        Me.dgvList4.AllowUserToDeleteRows = False
        Me.dgvList4.BackgroundColor = System.Drawing.Color.White
        Me.dgvList4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList4.Location = New System.Drawing.Point(10, 449)
        Me.dgvList4.Name = "dgvList4"
        Me.dgvList4.ReadOnly = True
        Me.dgvList4.Size = New System.Drawing.Size(811, 86)
        Me.dgvList4.TabIndex = 5
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.dtpBenpost)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnSearch)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnExport)
        Me.pnlMain.Location = New System.Drawing.Point(10, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(852, 67)
        Me.pnlMain.TabIndex = 0
        '
        'equity_sharescount
        '
        Me.equity_sharescount.DataPropertyName = "equity_sharescount"
        Me.equity_sharescount.HeaderText = "Number of equity shares held in dematerialized form (XIV)"
        Me.equity_sharescount.Name = "equity_sharescount"
        Me.equity_sharescount.ReadOnly = True
        '
        'ABC2
        '
        Me.ABC2.DataPropertyName = "ABC2"
        Me.ABC2.HeaderText = "Total shareholding as a % assuming full conversion of convertible securities"
        Me.ABC2.Name = "ABC2"
        Me.ABC2.ReadOnly = True
        '
        'total_percentage_rights
        '
        Me.total_percentage_rights.DataPropertyName = "total_percentage_rights"
        Me.total_percentage_rights.HeaderText = "Total as a % of Total voting rights ( IX)"
        Me.total_percentage_rights.Name = "total_percentage_rights"
        Me.total_percentage_rights.ReadOnly = True
        '
        'total_votingright
        '
        Me.total_votingright.DataPropertyName = "total_votingright"
        Me.total_votingright.HeaderText = "No.Of Voting rights. Total"
        Me.total_votingright.Name = "total_votingright"
        Me.total_votingright.ReadOnly = True
        '
        'votingright_count
        '
        Me.votingright_count.DataPropertyName = "votingright_count"
        Me.votingright_count.HeaderText = "Number of Voting rights (XIV ). Class eg : X  "
        Me.votingright_count.Name = "votingright_count"
        Me.votingright_count.ReadOnly = True
        '
        'calculation
        '
        Me.calculation.DataPropertyName = "calculation"
        Me.calculation.HeaderText = "Share holding % calculated as per SCRR, 1957 As a % of (A+B+C2)"
        Me.calculation.Name = "calculation"
        Me.calculation.ReadOnly = True
        '
        'total_sharecount
        '
        Me.total_sharecount.DataPropertyName = "total_sharecount"
        Me.total_sharecount.HeaderText = "Total No. of shares held (VII = IV+V + VI)"
        Me.total_sharecount.Name = "total_sharecount"
        Me.total_sharecount.ReadOnly = True
        '
        'paided_sharecount
        '
        Me.paided_sharecount.DataPropertyName = "paided_sharecount"
        Me.paided_sharecount.HeaderText = "No. of fully paid-up equity shares held (IV)"
        Me.paided_sharecount.Name = "paided_sharecount"
        Me.paided_sharecount.ReadOnly = True
        '
        'Shareholder_count
        '
        Me.Shareholder_count.DataPropertyName = "Shareholder_count"
        Me.Shareholder_count.HeaderText = "Nos.Of shareholders ( III )"
        Me.Shareholder_count.Name = "Shareholder_count"
        Me.Shareholder_count.ReadOnly = True
        '
        'Category_Name
        '
        Me.Category_Name.DataPropertyName = "Category_Name"
        Me.Category_Name.HeaderText = "Category & Name of the Share Holders ( I )"
        Me.Category_Name.Name = "Category_Name"
        Me.Category_Name.ReadOnly = True
        '
        'Category
        '
        Me.Category.DataPropertyName = "Category"
        Me.Category.HeaderText = "Category(I)"
        Me.Category.Name = "Category"
        Me.Category.ReadOnly = True
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.BackgroundColor = System.Drawing.Color.White
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Category, Me.Category_Name, Me.Shareholder_count, Me.paided_sharecount, Me.total_sharecount, Me.calculation, Me.votingright_count, Me.total_votingright, Me.total_percentage_rights, Me.ABC2, Me.equity_sharescount})
        Me.dgvList.Location = New System.Drawing.Point(10, 115)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(811, 191)
        Me.dgvList.TabIndex = 6
        '
        'lblPromoter
        '
        Me.lblPromoter.AutoSize = True
        Me.lblPromoter.Location = New System.Drawing.Point(12, 82)
        Me.lblPromoter.Name = "lblPromoter"
        Me.lblPromoter.Size = New System.Drawing.Size(100, 13)
        Me.lblPromoter.TabIndex = 7
        Me.lblPromoter.Text = "Indian Promoter"
        '
        'lblFrpromoter
        '
        Me.lblFrpromoter.AutoSize = True
        Me.lblFrpromoter.Location = New System.Drawing.Point(118, 82)
        Me.lblFrpromoter.Name = "lblFrpromoter"
        Me.lblFrpromoter.Size = New System.Drawing.Size(106, 13)
        Me.lblFrpromoter.TabIndex = 8
        Me.lblFrpromoter.Text = "Foriegn Promoter"
        '
        'lblInstitutions
        '
        Me.lblInstitutions.AutoSize = True
        Me.lblInstitutions.Location = New System.Drawing.Point(230, 82)
        Me.lblInstitutions.Name = "lblInstitutions"
        Me.lblInstitutions.Size = New System.Drawing.Size(193, 13)
        Me.lblInstitutions.TabIndex = 9
        Me.lblInstitutions.Text = "Public Share Holder - Institutions"
        '
        'lblNoninstitutions
        '
        Me.lblNoninstitutions.AutoSize = True
        Me.lblNoninstitutions.Location = New System.Drawing.Point(429, 82)
        Me.lblNoninstitutions.Name = "lblNoninstitutions"
        Me.lblNoninstitutions.Size = New System.Drawing.Size(99, 13)
        Me.lblNoninstitutions.TabIndex = 10
        Me.lblNoninstitutions.Text = "Non-Institutions"
        '
        'dgvList5
        '
        Me.dgvList5.AllowUserToAddRows = False
        Me.dgvList5.AllowUserToDeleteRows = False
        Me.dgvList5.BackgroundColor = System.Drawing.Color.White
        Me.dgvList5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList5.Location = New System.Drawing.Point(10, 541)
        Me.dgvList5.Name = "dgvList5"
        Me.dgvList5.ReadOnly = True
        Me.dgvList5.Size = New System.Drawing.Size(811, 90)
        Me.dgvList5.TabIndex = 11
        '
        'lbdrshares
        '
        Me.lbdrshares.AutoSize = True
        Me.lbdrshares.Location = New System.Drawing.Point(538, 82)
        Me.lbdrshares.Name = "lbdrshares"
        Me.lbdrshares.Size = New System.Drawing.Size(86, 13)
        Me.lbdrshares.TabIndex = 12
        Me.lbdrshares.Text = "Shares For DR"
        '
        'lblemptrust
        '
        Me.lblemptrust.AutoSize = True
        Me.lblemptrust.Location = New System.Drawing.Point(630, 82)
        Me.lblemptrust.Name = "lblemptrust"
        Me.lblemptrust.Size = New System.Drawing.Size(68, 13)
        Me.lblemptrust.TabIndex = 13
        Me.lblemptrust.Text = "Summary  "
        '
        'frmshp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 687)
        Me.Controls.Add(Me.lblemptrust)
        Me.Controls.Add(Me.lbdrshares)
        Me.Controls.Add(Me.dgvList5)
        Me.Controls.Add(Me.lblNoninstitutions)
        Me.Controls.Add(Me.lblInstitutions)
        Me.Controls.Add(Me.lblFrpromoter)
        Me.Controls.Add(Me.lblPromoter)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.dgvList1)
        Me.Controls.Add(Me.dgvList4)
        Me.Controls.Add(Me.dgvList3)
        Me.Controls.Add(Me.dgvList2)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmshp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmshp"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvList2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvList3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvList4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvList5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpBenpost As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgvList1 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvList2 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvList3 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvList4 As System.Windows.Forms.DataGridView
    Friend WithEvents equity_sharescount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ABC2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total_percentage_rights As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total_votingright As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents votingright_count As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents calculation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total_sharecount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents paided_sharecount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Shareholder_count As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents lblPromoter As System.Windows.Forms.Label
    Friend WithEvents lblFrpromoter As System.Windows.Forms.Label
    Friend WithEvents lblInstitutions As System.Windows.Forms.Label
    Friend WithEvents lblNoninstitutions As System.Windows.Forms.Label
    Friend WithEvents dgvList5 As System.Windows.Forms.DataGridView
    Friend WithEvents lbdrshares As System.Windows.Forms.Label
    Friend WithEvents lblemptrust As System.Windows.Forms.Label
End Class
