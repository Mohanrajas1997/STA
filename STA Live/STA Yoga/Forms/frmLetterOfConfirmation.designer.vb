<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLetterOfConfirmation
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
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.dtp_owd = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btn_search = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_folio = New System.Windows.Forms.TextBox()
        Me.cb_cmpy = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cbo_doc = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dtp_to = New System.Windows.Forms.DateTimePicker()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dtp_from = New System.Windows.Forms.DateTimePicker()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txt_inward = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv_covering = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
        Me.txttotalrecord = New System.Windows.Forms.TextBox()
        Me.pnlSearch.SuspendLayout()
        CType(Me.dgv_covering, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.dtp_owd)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btn_search)
        Me.pnlSearch.Controls.Add(Me.Label14)
        Me.pnlSearch.Controls.Add(Me.txt_folio)
        Me.pnlSearch.Controls.Add(Me.cb_cmpy)
        Me.pnlSearch.Controls.Add(Me.Label20)
        Me.pnlSearch.Controls.Add(Me.cbo_doc)
        Me.pnlSearch.Controls.Add(Me.Label21)
        Me.pnlSearch.Controls.Add(Me.dtp_to)
        Me.pnlSearch.Controls.Add(Me.Label22)
        Me.pnlSearch.Controls.Add(Me.dtp_from)
        Me.pnlSearch.Controls.Add(Me.Label23)
        Me.pnlSearch.Controls.Add(Me.txt_inward)
        Me.pnlSearch.Controls.Add(Me.Label24)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(12, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(827, 103)
        Me.pnlSearch.TabIndex = 4
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(669, 75)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 152
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'dtp_owd
        '
        Me.dtp_owd.Checked = False
        Me.dtp_owd.CustomFormat = "dd-MM-yyyy"
        Me.dtp_owd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_owd.Location = New System.Drawing.Point(101, 75)
        Me.dtp_owd.Name = "dtp_owd"
        Me.dtp_owd.ShowCheckBox = True
        Me.dtp_owd.Size = New System.Drawing.Size(118, 23)
        Me.dtp_owd.TabIndex = 150
        Me.dtp_owd.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 17)
        Me.Label2.TabIndex = 151
        Me.Label2.Text = "Outward"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(745, 75)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 149
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btn_search
        '
        Me.btn_search.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_search.Location = New System.Drawing.Point(590, 75)
        Me.btn_search.Name = "btn_search"
        Me.btn_search.Size = New System.Drawing.Size(72, 24)
        Me.btn_search.TabIndex = 148
        Me.btn_search.Text = "Search"
        Me.btn_search.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(633, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 17)
        Me.Label14.TabIndex = 147
        Me.Label14.Text = "Folio No"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_folio
        '
        Me.txt_folio.Location = New System.Drawing.Point(712, 21)
        Me.txt_folio.Name = "txt_folio"
        Me.txt_folio.Size = New System.Drawing.Size(105, 23)
        Me.txt_folio.TabIndex = 140
        '
        'cb_cmpy
        '
        Me.cb_cmpy.BackColor = System.Drawing.SystemColors.Window
        Me.cb_cmpy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cb_cmpy.FormattingEnabled = True
        Me.cb_cmpy.Location = New System.Drawing.Point(101, 48)
        Me.cb_cmpy.Name = "cb_cmpy"
        Me.cb_cmpy.Size = New System.Drawing.Size(298, 24)
        Me.cb_cmpy.TabIndex = 141
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(21, 50)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 15)
        Me.Label20.TabIndex = 146
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbo_doc
        '
        Me.cbo_doc.FormattingEnabled = True
        Me.cbo_doc.Location = New System.Drawing.Point(518, 48)
        Me.cbo_doc.Name = "cbo_doc"
        Me.cbo_doc.Size = New System.Drawing.Size(298, 24)
        Me.cbo_doc.TabIndex = 142
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(430, 48)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(82, 20)
        Me.Label21.TabIndex = 145
        Me.Label21.Text = "Document"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtp_to
        '
        Me.dtp_to.Checked = False
        Me.dtp_to.CustomFormat = "dd-MM-yyyy"
        Me.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_to.Location = New System.Drawing.Point(279, 21)
        Me.dtp_to.Name = "dtp_to"
        Me.dtp_to.ShowCheckBox = True
        Me.dtp_to.Size = New System.Drawing.Size(118, 23)
        Me.dtp_to.TabIndex = 138
        Me.dtp_to.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(221, 23)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(49, 17)
        Me.Label22.TabIndex = 143
        Me.Label22.Text = "To"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtp_from
        '
        Me.dtp_from.Checked = False
        Me.dtp_from.CustomFormat = "dd-MM-yyyy"
        Me.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_from.Location = New System.Drawing.Point(101, 21)
        Me.dtp_from.Name = "dtp_from"
        Me.dtp_from.ShowCheckBox = True
        Me.dtp_from.Size = New System.Drawing.Size(118, 23)
        Me.dtp_from.TabIndex = 136
        Me.dtp_from.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(8, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(84, 17)
        Me.Label23.TabIndex = 144
        Me.Label23.Text = "Inward From"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_inward
        '
        Me.txt_inward.Location = New System.Drawing.Point(518, 21)
        Me.txt_inward.Name = "txt_inward"
        Me.txt_inward.Size = New System.Drawing.Size(105, 23)
        Me.txt_inward.TabIndex = 139
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(439, 23)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 17)
        Me.Label24.TabIndex = 137
        Me.Label24.Text = "Inward No"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Letter Of Confirmation"
        '
        'dgv_covering
        '
        Me.dgv_covering.AllowUserToAddRows = False
        Me.dgv_covering.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_covering.Location = New System.Drawing.Point(11, 121)
        Me.dgv_covering.Name = "dgv_covering"
        Me.dgv_covering.Size = New System.Drawing.Size(828, 290)
        Me.dgv_covering.TabIndex = 5
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(688, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txttotalrecord)
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(12, 417)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(827, 46)
        Me.pnlExport.TabIndex = 20
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
        Me.txtTotRec.Size = New System.Drawing.Size(90, 14)
        Me.txtTotRec.TabIndex = 1
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'txttotalrecord
        '
        Me.txttotalrecord.BackColor = System.Drawing.SystemColors.Control
        Me.txttotalrecord.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txttotalrecord.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txttotalrecord.Location = New System.Drawing.Point(93, 13)
        Me.txttotalrecord.Name = "txttotalrecord"
        Me.txttotalrecord.Size = New System.Drawing.Size(105, 14)
        Me.txttotalrecord.TabIndex = 140
        '
        'frmLetterOfConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 472)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgv_covering)
        Me.Controls.Add(Me.pnlSearch)
        Me.Name = "frmLetterOfConfirmation"
        Me.Text = "Letter Of Confirmation"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.dgv_covering, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents dtp_owd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btn_search As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txt_folio As System.Windows.Forms.TextBox
    Friend WithEvents cb_cmpy As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cbo_doc As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents dtp_to As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dtp_from As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txt_inward As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv_covering As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txttotalrecord As System.Windows.Forms.TextBox

End Class
