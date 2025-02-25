<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_unpaidremainderLetter
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
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.Bulkmail = New System.Windows.Forms.Button()
        Me.dtp_wardate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cb_interim = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cb_finyear = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.dgvList1 = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtplastdate = New System.Windows.Forms.DateTimePicker()
        Me.dtp_iepf = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvList1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btnClear.Location = New System.Drawing.Point(621, 66)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(699, 66)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(507, 66)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(108, 24)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Generate Letter"
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
        Me.dgvList.Location = New System.Drawing.Point(12, 115)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(781, 513)
        Me.dgvList.TabIndex = 24
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.dtp_iepf)
        Me.pnlSearch.Controls.Add(Me.Label6)
        Me.pnlSearch.Controls.Add(Me.dtplastdate)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.Bulkmail)
        Me.pnlSearch.Controls.Add(Me.dtp_wardate)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.Cb_interim)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.Cb_finyear)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(12, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(781, 97)
        Me.pnlSearch.TabIndex = 23
        '
        'Bulkmail
        '
        Me.Bulkmail.Location = New System.Drawing.Point(426, 66)
        Me.Bulkmail.Name = "Bulkmail"
        Me.Bulkmail.Size = New System.Drawing.Size(75, 24)
        Me.Bulkmail.TabIndex = 139
        Me.Bulkmail.Text = "Bulk Mail"
        Me.Bulkmail.UseVisualStyleBackColor = True
        '
        'dtp_wardate
        '
        Me.dtp_wardate.Checked = False
        Me.dtp_wardate.CustomFormat = "dd-MM-yyyy"
        Me.dtp_wardate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_wardate.Location = New System.Drawing.Point(93, 39)
        Me.dtp_wardate.Name = "dtp_wardate"
        Me.dtp_wardate.ShowCheckBox = True
        Me.dtp_wardate.Size = New System.Drawing.Size(118, 21)
        Me.dtp_wardate.TabIndex = 138
        Me.dtp_wardate.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 137
        Me.Label4.Text = "Warrant Date"
        '
        'Cb_interim
        '
        Me.Cb_interim.FormattingEnabled = True
        Me.Cb_interim.Location = New System.Drawing.Point(650, 11)
        Me.Cb_interim.Name = "Cb_interim"
        Me.Cb_interim.Size = New System.Drawing.Size(121, 21)
        Me.Cb_interim.TabIndex = 136
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(559, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 17)
        Me.Label3.TabIndex = 135
        Me.Label3.Text = "Interim Code"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Cb_finyear
        '
        Me.Cb_finyear.FormattingEnabled = True
        Me.Cb_finyear.Location = New System.Drawing.Point(434, 8)
        Me.Cb_finyear.Name = "Cb_finyear"
        Me.Cb_finyear.Size = New System.Drawing.Size(121, 21)
        Me.Cb_finyear.TabIndex = 134
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(343, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 17)
        Me.Label2.TabIndex = 133
        Me.Label2.Text = "Financial Year"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(12, 634)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(781, 46)
        Me.pnlExport.TabIndex = 25
        '
        'dgvList1
        '
        Me.dgvList1.AllowUserToAddRows = False
        Me.dgvList1.AllowUserToDeleteRows = False
        Me.dgvList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList1.Location = New System.Drawing.Point(138, 182)
        Me.dgvList1.Name = "dgvList1"
        Me.dgvList1.ReadOnly = True
        Me.dgvList1.Size = New System.Drawing.Size(559, 320)
        Me.dgvList1.TabIndex = 26
        Me.dgvList1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(229, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(147, 13)
        Me.Label5.TabIndex = 140
        Me.Label5.Text = "Last Date for Submission"
        '
        'dtplastdate
        '
        Me.dtplastdate.Checked = False
        Me.dtplastdate.CustomFormat = "ddMMMMyyyy"
        Me.dtplastdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtplastdate.Location = New System.Drawing.Point(379, 39)
        Me.dtplastdate.Name = "dtplastdate"
        Me.dtplastdate.ShowCheckBox = True
        Me.dtplastdate.Size = New System.Drawing.Size(148, 21)
        Me.dtplastdate.TabIndex = 141
        Me.dtplastdate.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'dtp_iepf
        '
        Me.dtp_iepf.Checked = False
        Me.dtp_iepf.CustomFormat = "ddMMMMyyyy"
        Me.dtp_iepf.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_iepf.Location = New System.Drawing.Point(624, 41)
        Me.dtp_iepf.Name = "dtp_iepf"
        Me.dtp_iepf.ShowCheckBox = True
        Me.dtp_iepf.Size = New System.Drawing.Size(147, 21)
        Me.dtp_iepf.TabIndex = 143
        Me.dtp_iepf.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(533, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 142
        Me.Label6.Text = "IEPF due Date"
        '
        'Frm_unpaidremainderLetter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 687)
        Me.Controls.Add(Me.dgvList1)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlExport)
        Me.Name = "Frm_unpaidremainderLetter"
        Me.Text = "Unpaid Remainder Letter"
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Cb_interim As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Cb_finyear As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtp_wardate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgvList1 As System.Windows.Forms.DataGridView
    Friend WithEvents Bulkmail As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtplastdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_iepf As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
