<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLocReminderReport
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
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtHolderName = New System.Windows.Forms.TextBox()
        Me.lblHolder = New System.Windows.Forms.Label()
        Me.lblFolioNo = New System.Windows.Forms.Label()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtRemiday = New System.Windows.Forms.TextBox()
        Me.lblReminderDay = New System.Windows.Forms.Label()
        Me.txtCalcDays = New System.Windows.Forms.TextBox()
        Me.lblCalcdays = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.lblComp = New System.Windows.Forms.Label()
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.lblDocType = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.lblInwardNo = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.pnlExport.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(662, 103)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 10
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtHolderName
        '
        Me.txtHolderName.Location = New System.Drawing.Point(490, 40)
        Me.txtHolderName.Name = "txtHolderName"
        Me.txtHolderName.Size = New System.Drawing.Size(322, 27)
        Me.txtHolderName.TabIndex = 5
        '
        'lblHolder
        '
        Me.lblHolder.Location = New System.Drawing.Point(399, 42)
        Me.lblHolder.Name = "lblHolder"
        Me.lblHolder.Size = New System.Drawing.Size(85, 15)
        Me.lblHolder.TabIndex = 136
        Me.lblHolder.Text = "Holder Name"
        Me.lblHolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFolioNo
        '
        Me.lblFolioNo.Location = New System.Drawing.Point(605, 14)
        Me.lblFolioNo.Name = "lblFolioNo"
        Me.lblFolioNo.Size = New System.Drawing.Size(97, 17)
        Me.lblFolioNo.TabIndex = 135
        Me.lblFolioNo.Text = "Folio No"
        Me.lblFolioNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(708, 12)
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(105, 27)
        Me.txtFolioNo.TabIndex = 3
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(562, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(20, 243)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(635, 33)
        Me.pnlExport.TabIndex = 5
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(13, 8)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(433, 20)
        Me.txtTotRec.TabIndex = 0
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.txtRemiday)
        Me.pnlSearch.Controls.Add(Me.lblReminderDay)
        Me.pnlSearch.Controls.Add(Me.txtCalcDays)
        Me.pnlSearch.Controls.Add(Me.lblCalcdays)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.txtHolderName)
        Me.pnlSearch.Controls.Add(Me.lblHolder)
        Me.pnlSearch.Controls.Add(Me.lblFolioNo)
        Me.pnlSearch.Controls.Add(Me.txtFolioNo)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.lblComp)
        Me.pnlSearch.Controls.Add(Me.cboDocType)
        Me.pnlSearch.Controls.Add(Me.lblDocType)
        Me.pnlSearch.Controls.Add(Me.dtpTo)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.dtpFrom)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnRefresh)
        Me.pnlSearch.Controls.Add(Me.txtInwardNo)
        Me.pnlSearch.Controls.Add(Me.lblInwardNo)
        Me.pnlSearch.Location = New System.Drawing.Point(20, 10)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(832, 138)
        Me.pnlSearch.TabIndex = 3
        '
        'txtRemiday
        '
        Me.txtRemiday.Location = New System.Drawing.Point(708, 70)
        Me.txtRemiday.Name = "txtRemiday"
        Me.txtRemiday.Size = New System.Drawing.Size(105, 27)
        Me.txtRemiday.TabIndex = 8
        Me.txtRemiday.Text = "0"
        '
        'lblReminderDay
        '
        Me.lblReminderDay.Location = New System.Drawing.Point(601, 72)
        Me.lblReminderDay.Name = "lblReminderDay"
        Me.lblReminderDay.Size = New System.Drawing.Size(101, 17)
        Me.lblReminderDay.TabIndex = 139
        Me.lblReminderDay.Text = "Reminder Day"
        Me.lblReminderDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCalcDays
        '
        Me.txtCalcDays.Location = New System.Drawing.Point(490, 70)
        Me.txtCalcDays.Name = "txtCalcDays"
        Me.txtCalcDays.Size = New System.Drawing.Size(105, 27)
        Me.txtCalcDays.TabIndex = 7
        Me.txtCalcDays.Text = "0"
        '
        'lblCalcdays
        '
        Me.lblCalcdays.Location = New System.Drawing.Point(411, 72)
        Me.lblCalcdays.Name = "lblCalcdays"
        Me.lblCalcdays.Size = New System.Drawing.Size(73, 17)
        Me.lblCalcdays.TabIndex = 137
        Me.lblCalcdays.Text = "Days"
        Me.lblCalcdays.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(97, 40)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(298, 29)
        Me.cboCompany.TabIndex = 4
        '
        'lblComp
        '
        Me.lblComp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComp.Location = New System.Drawing.Point(17, 42)
        Me.lblComp.Name = "lblComp"
        Me.lblComp.Size = New System.Drawing.Size(73, 15)
        Me.lblComp.TabIndex = 123
        Me.lblComp.Text = "Company"
        Me.lblComp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDocType
        '
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Location = New System.Drawing.Point(97, 71)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(298, 29)
        Me.cboDocType.TabIndex = 6
        '
        'lblDocType
        '
        Me.lblDocType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDocType.Location = New System.Drawing.Point(8, 70)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(82, 20)
        Me.lblDocType.TabIndex = 121
        Me.lblDocType.Text = "Document"
        Me.lblDocType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(290, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(105, 27)
        Me.dtpTo.TabIndex = 1
        Me.dtpTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(235, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 17)
        Me.Label11.TabIndex = 118
        Me.Label11.Text = "To"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(97, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(105, 27)
        Me.dtpFrom.TabIndex = 0
        Me.dtpFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(6, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 17)
        Me.Label10.TabIndex = 119
        Me.Label10.Text = "Inward From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(740, 103)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(584, 103)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Location = New System.Drawing.Point(490, 12)
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(105, 27)
        Me.txtInwardNo.TabIndex = 2
        '
        'lblInwardNo
        '
        Me.lblInwardNo.Location = New System.Drawing.Point(411, 14)
        Me.lblInwardNo.Name = "lblInwardNo"
        Me.lblInwardNo.Size = New System.Drawing.Size(73, 17)
        Me.lblInwardNo.TabIndex = 0
        Me.lblInwardNo.Text = "Inward No"
        Me.lblInwardNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(20, 154)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.Size = New System.Drawing.Size(832, 83)
        Me.dgvList.TabIndex = 0
        '
        'frmLocReminderReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 288)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.dgvList)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmLocReminderReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLocReminderReport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtHolderName As System.Windows.Forms.TextBox
    Friend WithEvents lblHolder As System.Windows.Forms.Label
    Friend WithEvents lblFolioNo As System.Windows.Forms.Label
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents lblComp As System.Windows.Forms.Label
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDocType As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents lblInwardNo As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents txtRemiday As System.Windows.Forms.TextBox
    Friend WithEvents lblReminderDay As System.Windows.Forms.Label
    Friend WithEvents txtCalcDays As System.Windows.Forms.TextBox
    Friend WithEvents lblCalcdays As System.Windows.Forms.Label
End Class
