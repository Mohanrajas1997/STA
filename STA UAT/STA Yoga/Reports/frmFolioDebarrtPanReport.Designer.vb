﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFolioDebarrtPanReport
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.lblFoliono = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPanno = New System.Windows.Forms.TextBox()
        Me.lblPanno = New System.Windows.Forms.Label()
        Me.txtHoldername = New System.Windows.Forms.TextBox()
        Me.lblHoldername = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtRecCount = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.txtFolioNo)
        Me.pnlMain.Controls.Add(Me.lblFoliono)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.txtPanno)
        Me.pnlMain.Controls.Add(Me.lblPanno)
        Me.pnlMain.Controls.Add(Me.txtHoldername)
        Me.pnlMain.Controls.Add(Me.lblHoldername)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Location = New System.Drawing.Point(14, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(728, 103)
        Me.pnlMain.TabIndex = 0
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(546, 11)
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(171, 21)
        Me.txtFolioNo.TabIndex = 1
        '
        'lblFoliono
        '
        Me.lblFoliono.Location = New System.Drawing.Point(466, 13)
        Me.lblFoliono.Name = "lblFoliono"
        Me.lblFoliono.Size = New System.Drawing.Size(73, 15)
        Me.lblFoliono.TabIndex = 161
        Me.lblFoliono.Text = "Folio No"
        Me.lblFoliono.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(113, 11)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(347, 21)
        Me.cboCompany.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(32, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 15)
        Me.Label3.TabIndex = 160
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPanno
        '
        Me.txtPanno.Location = New System.Drawing.Point(546, 40)
        Me.txtPanno.MaxLength = 0
        Me.txtPanno.Name = "txtPanno"
        Me.txtPanno.Size = New System.Drawing.Size(171, 21)
        Me.txtPanno.TabIndex = 3
        '
        'lblPanno
        '
        Me.lblPanno.Location = New System.Drawing.Point(469, 42)
        Me.lblPanno.Name = "lblPanno"
        Me.lblPanno.Size = New System.Drawing.Size(70, 13)
        Me.lblPanno.TabIndex = 155
        Me.lblPanno.Text = "PAN No"
        Me.lblPanno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHoldername
        '
        Me.txtHoldername.Location = New System.Drawing.Point(113, 40)
        Me.txtHoldername.MaxLength = 0
        Me.txtHoldername.Name = "txtHoldername"
        Me.txtHoldername.Size = New System.Drawing.Size(347, 21)
        Me.txtHoldername.TabIndex = 2
        '
        'lblHoldername
        '
        Me.lblHoldername.Location = New System.Drawing.Point(11, 42)
        Me.lblHoldername.Name = "lblHoldername"
        Me.lblHoldername.Size = New System.Drawing.Size(94, 13)
        Me.lblHoldername.TabIndex = 138
        Me.lblHoldername.Text = "Holder Name"
        Me.lblHoldername.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(646, 68)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(568, 68)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "&Clear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(490, 68)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "&Refresh"
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(13, 121)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(729, 227)
        Me.dgvReport.TabIndex = 1
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(14, 354)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(634, 33)
        Me.pnlExport.TabIndex = 2
        '
        'txtRecCount
        '
        Me.txtRecCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRecCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRecCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRecCount.Location = New System.Drawing.Point(6, 8)
        Me.txtRecCount.MaxLength = 100
        Me.txtRecCount.Name = "txtRecCount"
        Me.txtRecCount.ReadOnly = True
        Me.txtRecCount.Size = New System.Drawing.Size(433, 14)
        Me.txtRecCount.TabIndex = 0
        Me.txtRecCount.TabStop = False
        Me.txtRecCount.Text = "Record Count : "
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(521, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'frmFolioDebarrtPanReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 402)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "frmFolioDebarrtPanReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Debarrt Pan List (Folio)"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtPanno As System.Windows.Forms.TextBox
    Friend WithEvents lblPanno As System.Windows.Forms.Label
    Friend WithEvents txtHoldername As System.Windows.Forms.TextBox
    Friend WithEvents lblHoldername As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents lblFoliono As System.Windows.Forms.Label
End Class