<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDividendAccountMasterRpt
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
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.CboBankCode = New System.Windows.Forms.ComboBox()
        Me.lblBankcode = New System.Windows.Forms.Label()
        Me.txtAccno = New System.Windows.Forms.TextBox()
        Me.CboFinyear = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblaccno = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(12, 424)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(786, 33)
        Me.pnlExport.TabIndex = 6
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
        Me.btnExport.Location = New System.Drawing.Point(706, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(12, 99)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(778, 319)
        Me.dgvList.TabIndex = 5
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.CboBankCode)
        Me.pnlSearch.Controls.Add(Me.lblBankcode)
        Me.pnlSearch.Controls.Add(Me.txtAccno)
        Me.pnlSearch.Controls.Add(Me.CboFinyear)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Controls.Add(Me.lblaccno)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Location = New System.Drawing.Point(12, 11)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(778, 82)
        Me.pnlSearch.TabIndex = 4
        '
        'CboBankCode
        '
        Me.CboBankCode.FormattingEnabled = True
        Me.CboBankCode.Location = New System.Drawing.Point(359, 49)
        Me.CboBankCode.Name = "CboBankCode"
        Me.CboBankCode.Size = New System.Drawing.Size(146, 21)
        Me.CboBankCode.TabIndex = 131
        '
        'lblBankcode
        '
        Me.lblBankcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankcode.Location = New System.Drawing.Point(256, 49)
        Me.lblBankcode.Name = "lblBankcode"
        Me.lblBankcode.Size = New System.Drawing.Size(97, 20)
        Me.lblBankcode.TabIndex = 132
        Me.lblBankcode.Text = "Bank Code"
        Me.lblBankcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAccno
        '
        Me.txtAccno.Location = New System.Drawing.Point(111, 49)
        Me.txtAccno.Name = "txtAccno"
        Me.txtAccno.Size = New System.Drawing.Size(139, 21)
        Me.txtAccno.TabIndex = 130
        '
        'CboFinyear
        '
        Me.CboFinyear.FormattingEnabled = True
        Me.CboFinyear.Location = New System.Drawing.Point(618, 15)
        Me.CboFinyear.Name = "CboFinyear"
        Me.CboFinyear.Size = New System.Drawing.Size(146, 21)
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
        'lblaccno
        '
        Me.lblaccno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblaccno.Location = New System.Drawing.Point(36, 46)
        Me.lblaccno.Name = "lblaccno"
        Me.lblaccno.Size = New System.Drawing.Size(64, 20)
        Me.lblaccno.TabIndex = 127
        Me.lblaccno.Text = "Acc No"
        Me.lblaccno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(614, 49)
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
        Me.btnClose.Location = New System.Drawing.Point(692, 49)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(536, 49)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 11
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'frmDividendAccountMasterRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 469)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlSearch)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "frmDividendAccountMasterRpt"
        Me.Text = "Dividend AccountMaster Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlExport As Panel
    Friend WithEvents txtTotRec As TextBox
    Friend WithEvents btnExport As Button
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents pnlSearch As Panel
    Friend WithEvents CboBankCode As ComboBox
    Friend WithEvents lblBankcode As Label
    Friend WithEvents txtAccno As TextBox
    Friend WithEvents CboFinyear As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblaccno As Label
    Friend WithEvents btnClear As Button
    Friend WithEvents cboCompany As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
End Class
