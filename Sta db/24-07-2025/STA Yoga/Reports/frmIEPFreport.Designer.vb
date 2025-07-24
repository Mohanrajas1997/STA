<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIEPFreport
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.CboFinyear = New System.Windows.Forms.ComboBox()
        Me.Finyear = New System.Windows.Forms.CheckedListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpPaidDate = New System.Windows.Forms.DateTimePicker()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblPaidDate = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlPrefixsno = New System.Windows.Forms.Panel()
        Me.RbdIEPF2 = New System.Windows.Forms.RadioButton()
        Me.RbdIEPF1 = New System.Windows.Forms.RadioButton()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.RbdIEPF4 = New System.Windows.Forms.RadioButton()
        Me.pnlSearch.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        Me.pnlPrefixsno.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.CboFinyear)
        Me.pnlSearch.Controls.Add(Me.pnlPrefixsno)
        Me.pnlSearch.Controls.Add(Me.Finyear)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.dtpPaidDate)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.lblPaidDate)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(16, 18)
        Me.pnlSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(965, 156)
        Me.pnlSearch.TabIndex = 21
        '
        'CboFinyear
        '
        Me.CboFinyear.FormattingEnabled = True
        Me.CboFinyear.Location = New System.Drawing.Point(722, 17)
        Me.CboFinyear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CboFinyear.Name = "CboFinyear"
        Me.CboFinyear.Size = New System.Drawing.Size(223, 29)
        Me.CboFinyear.TabIndex = 1
        '
        'Finyear
        '
        Me.Finyear.FormattingEnabled = True
        Me.Finyear.Location = New System.Drawing.Point(722, 15)
        Me.Finyear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Finyear.Name = "Finyear"
        Me.Finyear.Size = New System.Drawing.Size(223, 114)
        Me.Finyear.TabIndex = 140
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(27, 56)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 26)
        Me.Label4.TabIndex = 139
        Me.Label4.Text = "Type"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpPaidDate
        '
        Me.dtpPaidDate.Checked = False
        Me.dtpPaidDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpPaidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaidDate.Location = New System.Drawing.Point(123, 96)
        Me.dtpPaidDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpPaidDate.Name = "dtpPaidDate"
        Me.dtpPaidDate.ShowCheckBox = True
        Me.dtpPaidDate.Size = New System.Drawing.Size(208, 27)
        Me.dtpPaidDate.TabIndex = 2
        Me.dtpPaidDate.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(469, 96)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(108, 37)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(351, 96)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(108, 37)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblPaidDate
        '
        Me.lblPaidDate.AutoSize = True
        Me.lblPaidDate.Location = New System.Drawing.Point(12, 96)
        Me.lblPaidDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPaidDate.Name = "lblPaidDate"
        Me.lblPaidDate.Size = New System.Drawing.Size(97, 21)
        Me.lblPaidDate.TabIndex = 136
        Me.lblPaidDate.Text = "Paid Date "
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(585, 96)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 37)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(585, 17)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(132, 26)
        Me.Label2.TabIndex = 133
        Me.Label2.Text = "Financial Year"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(123, 14)
        Me.cboCompany.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(457, 29)
        Me.cboCompany.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(18, 707)
        Me.pnlExport.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(963, 71)
        Me.pnlExport.TabIndex = 23
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(12, 20)
        Me.txtTotRec.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(546, 20)
        Me.txtTotRec.TabIndex = 1
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(825, 14)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(108, 37)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'pnlPrefixsno
        '
        Me.pnlPrefixsno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrefixsno.Controls.Add(Me.RbdIEPF4)
        Me.pnlPrefixsno.Controls.Add(Me.RbdIEPF2)
        Me.pnlPrefixsno.Controls.Add(Me.RbdIEPF1)
        Me.pnlPrefixsno.Location = New System.Drawing.Point(123, 55)
        Me.pnlPrefixsno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPrefixsno.Name = "pnlPrefixsno"
        Me.pnlPrefixsno.Size = New System.Drawing.Size(313, 31)
        Me.pnlPrefixsno.TabIndex = 131
        '
        'RbdIEPF2
        '
        Me.RbdIEPF2.AutoSize = True
        Me.RbdIEPF2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbdIEPF2.Location = New System.Drawing.Point(104, 2)
        Me.RbdIEPF2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdIEPF2.Name = "RbdIEPF2"
        Me.RbdIEPF2.Size = New System.Drawing.Size(84, 24)
        Me.RbdIEPF2.TabIndex = 1
        Me.RbdIEPF2.Text = "IEPF2"
        Me.RbdIEPF2.UseVisualStyleBackColor = True
        '
        'RbdIEPF1
        '
        Me.RbdIEPF1.AutoSize = True
        Me.RbdIEPF1.Checked = True
        Me.RbdIEPF1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbdIEPF1.Location = New System.Drawing.Point(4, 2)
        Me.RbdIEPF1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdIEPF1.Name = "RbdIEPF1"
        Me.RbdIEPF1.Size = New System.Drawing.Size(84, 24)
        Me.RbdIEPF1.TabIndex = 0
        Me.RbdIEPF1.TabStop = True
        Me.RbdIEPF1.Text = "IEPF1"
        Me.RbdIEPF1.UseVisualStyleBackColor = True
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(16, 185)
        Me.dgvList.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(965, 512)
        Me.dgvList.TabIndex = 132
        '
        'RbdIEPF4
        '
        Me.RbdIEPF4.AutoSize = True
        Me.RbdIEPF4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbdIEPF4.Location = New System.Drawing.Point(207, 2)
        Me.RbdIEPF4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdIEPF4.Name = "RbdIEPF4"
        Me.RbdIEPF4.Size = New System.Drawing.Size(84, 24)
        Me.RbdIEPF4.TabIndex = 142
        Me.RbdIEPF4.Text = "IEPF4"
        Me.RbdIEPF4.UseVisualStyleBackColor = True
        '
        'frmIEPFreport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1001, 795)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlSearch)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmIEPFreport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IEPF Report"
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.pnlPrefixsno.ResumeLayout(False)
        Me.pnlPrefixsno.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpPaidDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblPaidDate As System.Windows.Forms.Label
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlPrefixsno As System.Windows.Forms.Panel
    Friend WithEvents RbdIEPF2 As System.Windows.Forms.RadioButton
    Friend WithEvents RbdIEPF1 As System.Windows.Forms.RadioButton
    Friend WithEvents Finyear As System.Windows.Forms.CheckedListBox
    Friend WithEvents CboFinyear As System.Windows.Forms.ComboBox
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents RbdIEPF4 As System.Windows.Forms.RadioButton
End Class
