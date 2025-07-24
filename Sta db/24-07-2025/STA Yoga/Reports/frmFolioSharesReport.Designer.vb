<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFolioSharesReport
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
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtPanNo3 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtFHName3 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtHolder3 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPanNo2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFHName2 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtHolder2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPanNo1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFHName1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtHolder1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(12, 170)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(778, 103)
        Me.dgvList.TabIndex = 1
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.dtpDate)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Controls.Add(Me.txtPanNo3)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.txtFHName3)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.txtHolder3)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.txtPanNo2)
        Me.pnlSearch.Controls.Add(Me.Label6)
        Me.pnlSearch.Controls.Add(Me.txtFHName2)
        Me.pnlSearch.Controls.Add(Me.Label7)
        Me.pnlSearch.Controls.Add(Me.txtHolder2)
        Me.pnlSearch.Controls.Add(Me.Label8)
        Me.pnlSearch.Controls.Add(Me.txtPanNo1)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.txtFHName1)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.txtHolder1)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.txtFolioNo)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Location = New System.Drawing.Point(12, 8)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(778, 156)
        Me.pnlSearch.TabIndex = 0
        '
        'txtPanNo3
        '
        Me.txtPanNo3.Location = New System.Drawing.Point(593, 96)
        Me.txtPanNo3.Name = "txtPanNo3"
        Me.txtPanNo3.Size = New System.Drawing.Size(171, 21)
        Me.txtPanNo3.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(514, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 20)
        Me.Label9.TabIndex = 107
        Me.Label9.Text = "PAN No 3"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFHName3
        '
        Me.txtFHName3.Location = New System.Drawing.Point(332, 96)
        Me.txtFHName3.Name = "txtFHName3"
        Me.txtFHName3.Size = New System.Drawing.Size(171, 21)
        Me.txtFHName3.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(253, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 20)
        Me.Label10.TabIndex = 105
        Me.Label10.Text = "F/H Name 3"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHolder3
        '
        Me.txtHolder3.Location = New System.Drawing.Point(82, 96)
        Me.txtHolder3.Name = "txtHolder3"
        Me.txtHolder3.Size = New System.Drawing.Size(171, 21)
        Me.txtHolder3.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(3, 96)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 20)
        Me.Label11.TabIndex = 103
        Me.Label11.Text = "Holder 3"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPanNo2
        '
        Me.txtPanNo2.Location = New System.Drawing.Point(593, 69)
        Me.txtPanNo2.Name = "txtPanNo2"
        Me.txtPanNo2.Size = New System.Drawing.Size(171, 21)
        Me.txtPanNo2.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(514, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 20)
        Me.Label6.TabIndex = 101
        Me.Label6.Text = "PAN No 2"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFHName2
        '
        Me.txtFHName2.Location = New System.Drawing.Point(332, 69)
        Me.txtFHName2.Name = "txtFHName2"
        Me.txtFHName2.Size = New System.Drawing.Size(171, 21)
        Me.txtFHName2.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(253, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 20)
        Me.Label7.TabIndex = 99
        Me.Label7.Text = "F/H Name 2"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHolder2
        '
        Me.txtHolder2.Location = New System.Drawing.Point(82, 69)
        Me.txtHolder2.Name = "txtHolder2"
        Me.txtHolder2.Size = New System.Drawing.Size(171, 21)
        Me.txtHolder2.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(3, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 20)
        Me.Label8.TabIndex = 97
        Me.Label8.Text = "Holder 2"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPanNo1
        '
        Me.txtPanNo1.Location = New System.Drawing.Point(593, 42)
        Me.txtPanNo1.Name = "txtPanNo1"
        Me.txtPanNo1.Size = New System.Drawing.Size(171, 21)
        Me.txtPanNo1.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(514, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 20)
        Me.Label5.TabIndex = 95
        Me.Label5.Text = "PAN No 1"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFHName1
        '
        Me.txtFHName1.Location = New System.Drawing.Point(332, 42)
        Me.txtFHName1.Name = "txtFHName1"
        Me.txtFHName1.Size = New System.Drawing.Size(171, 21)
        Me.txtFHName1.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(253, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 20)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "F/H Name 1"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHolder1
        '
        Me.txtHolder1.Location = New System.Drawing.Point(82, 42)
        Me.txtHolder1.Name = "txtHolder1"
        Me.txtHolder1.Size = New System.Drawing.Size(171, 21)
        Me.txtHolder1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(3, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Holder 1"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(614, 123)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(82, 15)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(421, 21)
        Me.cboCompany.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 15)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(692, 123)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(536, 123)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 11
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(593, 15)
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(171, 21)
        Me.txtFolioNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(514, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Folio No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(12, 288)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(635, 33)
        Me.pnlExport.TabIndex = 2
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
        Me.btnExport.Location = New System.Drawing.Point(562, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(82, 123)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(105, 21)
        Me.dtpDate.TabIndex = 116
        Me.dtpDate.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(-9, 125)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(84, 17)
        Me.Label12.TabIndex = 117
        Me.Label12.Text = "Date"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmFolioSharesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 469)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.dgvList)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmFolioSharesReport"
        Me.Text = "Folio Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
    End Sub

    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtPanNo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFHName1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtHolder1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPanNo3 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtFHName3 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtHolder3 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPanNo2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFHName2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtHolder2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
