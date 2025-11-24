<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchEngine
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvFolio = New System.Windows.Forms.DataGridView()
        Me.dgvDematPend = New System.Windows.Forms.DataGridView()
        Me.dgvCertDist = New System.Windows.Forms.DataGridView()
        Me.dgvCert = New System.Windows.Forms.DataGridView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.dgvFolioTran = New System.Windows.Forms.DataGridView()
        Me.lblFolio = New System.Windows.Forms.Label()
        Me.lblCert = New System.Windows.Forms.Label()
        Me.lblCertDist = New System.Windows.Forms.Label()
        Me.lblDematPend = New System.Windows.Forms.Label()
        Me.lblTran = New System.Windows.Forms.Label()
        Me.dgvInward = New System.Windows.Forms.DataGridView()
        Me.lblInward = New System.Windows.Forms.Label()
        Me.lblCertCount = New System.Windows.Forms.Label()
        Me.lblCertDistCount = New System.Windows.Forms.Label()
        Me.lblDematPendCount = New System.Windows.Forms.Label()
        Me.lblTranCount = New System.Windows.Forms.Label()
        Me.lblInwardCount = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        CType(Me.dgvFolio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDematPend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCertDist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFolioTran, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvInward, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvFolio
        '
        Me.dgvFolio.AllowUserToAddRows = False
        Me.dgvFolio.AllowUserToDeleteRows = False
        Me.dgvFolio.BackgroundColor = System.Drawing.Color.White
        Me.dgvFolio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFolio.Location = New System.Drawing.Point(10, 115)
        Me.dgvFolio.Name = "dgvFolio"
        Me.dgvFolio.ReadOnly = True
        Me.dgvFolio.Size = New System.Drawing.Size(811, 52)
        Me.dgvFolio.TabIndex = 1
        '
        'dgvDematPend
        '
        Me.dgvDematPend.AllowUserToAddRows = False
        Me.dgvDematPend.AllowUserToDeleteRows = False
        Me.dgvDematPend.BackgroundColor = System.Drawing.Color.White
        Me.dgvDematPend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDematPend.Location = New System.Drawing.Point(10, 348)
        Me.dgvDematPend.Name = "dgvDematPend"
        Me.dgvDematPend.ReadOnly = True
        Me.dgvDematPend.Size = New System.Drawing.Size(811, 86)
        Me.dgvDematPend.TabIndex = 4
        '
        'dgvCertDist
        '
        Me.dgvCertDist.AllowUserToAddRows = False
        Me.dgvCertDist.AllowUserToDeleteRows = False
        Me.dgvCertDist.BackgroundColor = System.Drawing.Color.White
        Me.dgvCertDist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCertDist.Location = New System.Drawing.Point(10, 259)
        Me.dgvCertDist.Name = "dgvCertDist"
        Me.dgvCertDist.ReadOnly = True
        Me.dgvCertDist.Size = New System.Drawing.Size(811, 86)
        Me.dgvCertDist.TabIndex = 3
        '
        'dgvCert
        '
        Me.dgvCert.AllowUserToAddRows = False
        Me.dgvCert.AllowUserToDeleteRows = False
        Me.dgvCert.BackgroundColor = System.Drawing.Color.White
        Me.dgvCert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCert.Location = New System.Drawing.Point(10, 170)
        Me.dgvCert.Name = "dgvCert"
        Me.dgvCert.ReadOnly = True
        Me.dgvCert.Size = New System.Drawing.Size(811, 86)
        Me.dgvCert.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(771, 30)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(691, 30)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(74, 23)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(611, 30)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(74, 23)
        Me.btnExport.TabIndex = 4
        Me.btnExport.Text = "&Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(531, 30)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 23)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(640, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 17)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Certificate No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(405, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Folio No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertNo
        '
        Me.txtCertNo.Location = New System.Drawing.Point(729, 3)
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.Size = New System.Drawing.Size(116, 21)
        Me.txtCertNo.TabIndex = 2
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(482, 3)
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(116, 21)
        Me.txtFolioNo.TabIndex = 1
        '
        'dgvFolioTran
        '
        Me.dgvFolioTran.AllowUserToAddRows = False
        Me.dgvFolioTran.AllowUserToDeleteRows = False
        Me.dgvFolioTran.BackgroundColor = System.Drawing.Color.White
        Me.dgvFolioTran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFolioTran.Location = New System.Drawing.Point(10, 437)
        Me.dgvFolioTran.Name = "dgvFolioTran"
        Me.dgvFolioTran.ReadOnly = True
        Me.dgvFolioTran.Size = New System.Drawing.Size(811, 86)
        Me.dgvFolioTran.TabIndex = 5
        '
        'lblFolio
        '
        Me.lblFolio.AutoSize = True
        Me.lblFolio.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolio.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblFolio.Location = New System.Drawing.Point(19, 73)
        Me.lblFolio.Name = "lblFolio"
        Me.lblFolio.Size = New System.Drawing.Size(33, 13)
        Me.lblFolio.TabIndex = 16
        Me.lblFolio.Text = "Folio"
        '
        'lblCert
        '
        Me.lblCert.AutoSize = True
        Me.lblCert.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCert.Location = New System.Drawing.Point(58, 56)
        Me.lblCert.Name = "lblCert"
        Me.lblCert.Size = New System.Drawing.Size(66, 13)
        Me.lblCert.TabIndex = 17
        Me.lblCert.Text = "Certificate"
        '
        'lblCertDist
        '
        Me.lblCertDist.AutoSize = True
        Me.lblCertDist.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCertDist.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCertDist.Location = New System.Drawing.Point(58, 73)
        Me.lblCertDist.Name = "lblCertDist"
        Me.lblCertDist.Size = New System.Drawing.Size(174, 13)
        Me.lblCertDist.TabIndex = 18
        Me.lblCertDist.Text = "Certificate Distinction Serious"
        '
        'lblDematPend
        '
        Me.lblDematPend.AutoSize = True
        Me.lblDematPend.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDematPend.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDematPend.Location = New System.Drawing.Point(249, 73)
        Me.lblDematPend.Name = "lblDematPend"
        Me.lblDematPend.Size = New System.Drawing.Size(93, 13)
        Me.lblDematPend.TabIndex = 19
        Me.lblDematPend.Text = "Demat Pending"
        '
        'lblTran
        '
        Me.lblTran.AutoSize = True
        Me.lblTran.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTran.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTran.Location = New System.Drawing.Point(349, 73)
        Me.lblTran.Name = "lblTran"
        Me.lblTran.Size = New System.Drawing.Size(74, 13)
        Me.lblTran.TabIndex = 20
        Me.lblTran.Text = "Transaction"
        '
        'dgvInward
        '
        Me.dgvInward.AllowUserToAddRows = False
        Me.dgvInward.AllowUserToDeleteRows = False
        Me.dgvInward.BackgroundColor = System.Drawing.Color.White
        Me.dgvInward.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInward.Location = New System.Drawing.Point(10, 529)
        Me.dgvInward.Name = "dgvInward"
        Me.dgvInward.ReadOnly = True
        Me.dgvInward.Size = New System.Drawing.Size(811, 86)
        Me.dgvInward.TabIndex = 6
        '
        'lblInward
        '
        Me.lblInward.AutoSize = True
        Me.lblInward.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInward.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInward.Location = New System.Drawing.Point(446, 73)
        Me.lblInward.Name = "lblInward"
        Me.lblInward.Size = New System.Drawing.Size(47, 13)
        Me.lblInward.TabIndex = 22
        Me.lblInward.Text = "Inward"
        '
        'lblCertCount
        '
        Me.lblCertCount.AutoSize = True
        Me.lblCertCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCertCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCertCount.Location = New System.Drawing.Point(538, 81)
        Me.lblCertCount.Name = "lblCertCount"
        Me.lblCertCount.Size = New System.Drawing.Size(35, 13)
        Me.lblCertCount.TabIndex = 23
        Me.lblCertCount.Text = "Tot : "
        '
        'lblCertDistCount
        '
        Me.lblCertDistCount.AutoSize = True
        Me.lblCertDistCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCertDistCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCertDistCount.Location = New System.Drawing.Point(618, 81)
        Me.lblCertDistCount.Name = "lblCertDistCount"
        Me.lblCertDistCount.Size = New System.Drawing.Size(35, 13)
        Me.lblCertDistCount.TabIndex = 24
        Me.lblCertDistCount.Text = "Tot : "
        '
        'lblDematPendCount
        '
        Me.lblDematPendCount.AutoSize = True
        Me.lblDematPendCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDematPendCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDematPendCount.Location = New System.Drawing.Point(786, 72)
        Me.lblDematPendCount.Name = "lblDematPendCount"
        Me.lblDematPendCount.Size = New System.Drawing.Size(35, 13)
        Me.lblDematPendCount.TabIndex = 26
        Me.lblDematPendCount.Text = "Tot : "
        '
        'lblTranCount
        '
        Me.lblTranCount.AutoSize = True
        Me.lblTranCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTranCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTranCount.Location = New System.Drawing.Point(822, 72)
        Me.lblTranCount.Name = "lblTranCount"
        Me.lblTranCount.Size = New System.Drawing.Size(35, 13)
        Me.lblTranCount.TabIndex = 27
        Me.lblTranCount.Text = "Tot : "
        '
        'lblInwardCount
        '
        Me.lblInwardCount.AutoSize = True
        Me.lblInwardCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInwardCount.Location = New System.Drawing.Point(827, 179)
        Me.lblInwardCount.Name = "lblInwardCount"
        Me.lblInwardCount.Size = New System.Drawing.Size(35, 13)
        Me.lblInwardCount.TabIndex = 28
        Me.lblInwardCount.Text = "Tot : "
        Me.lblInwardCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(80, 3)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(315, 21)
        Me.cboCompany.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(1, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.txtCertNo)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.txtFolioNo)
        Me.pnlMain.Controls.Add(Me.btnSearch)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnExport)
        Me.pnlMain.Location = New System.Drawing.Point(10, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(852, 57)
        Me.pnlMain.TabIndex = 0
        '
        'frmSearchEngine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 687)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.lblInwardCount)
        Me.Controls.Add(Me.lblTranCount)
        Me.Controls.Add(Me.lblDematPendCount)
        Me.Controls.Add(Me.lblCertDistCount)
        Me.Controls.Add(Me.lblCertCount)
        Me.Controls.Add(Me.lblInward)
        Me.Controls.Add(Me.dgvInward)
        Me.Controls.Add(Me.lblTran)
        Me.Controls.Add(Me.lblDematPend)
        Me.Controls.Add(Me.lblCertDist)
        Me.Controls.Add(Me.lblCert)
        Me.Controls.Add(Me.lblFolio)
        Me.Controls.Add(Me.dgvCert)
        Me.Controls.Add(Me.dgvCertDist)
        Me.Controls.Add(Me.dgvFolioTran)
        Me.Controls.Add(Me.dgvDematPend)
        Me.Controls.Add(Me.dgvFolio)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSearchEngine"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Engine"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvFolio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDematPend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCertDist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFolioTran, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvInward, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        'Me.ResumeLayout(False)
        'Me.PerformLayout()

    End Sub

    Friend WithEvents dgvFolio As System.Windows.Forms.DataGridView
    Friend WithEvents dgvDematPend As System.Windows.Forms.DataGridView
    Friend WithEvents dgvCertDist As System.Windows.Forms.DataGridView
    Friend WithEvents dgvCert As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCertNo As System.Windows.Forms.TextBox
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dgvFolioTran As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents lblFolio As System.Windows.Forms.Label
    Friend WithEvents lblCert As System.Windows.Forms.Label
    Friend WithEvents lblCertDist As System.Windows.Forms.Label
    Friend WithEvents lblDematPend As System.Windows.Forms.Label
    Friend WithEvents lblTran As System.Windows.Forms.Label
    Friend WithEvents dgvInward As System.Windows.Forms.DataGridView
    Friend WithEvents lblInward As System.Windows.Forms.Label
    Friend WithEvents lblCertCount As System.Windows.Forms.Label
    Friend WithEvents lblCertDistCount As System.Windows.Forms.Label
    Friend WithEvents lblDematPendCount As System.Windows.Forms.Label
    Friend WithEvents lblTranCount As System.Windows.Forms.Label
    Friend WithEvents lblInwardCount As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
End Class
