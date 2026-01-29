<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmISRGenerateCoveringLetter
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.dtp_to = New System.Windows.Forms.DateTimePicker()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dtp_from = New System.Windows.Forms.DateTimePicker()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txt_inward = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.dgv_covering = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtTotRec = New System.Windows.Forms.TextBox()
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
        Me.pnlSearch.Controls.Add(Me.dtp_to)
        Me.pnlSearch.Controls.Add(Me.Label22)
        Me.pnlSearch.Controls.Add(Me.dtp_from)
        Me.pnlSearch.Controls.Add(Me.Label23)
        Me.pnlSearch.Controls.Add(Me.txt_inward)
        Me.pnlSearch.Controls.Add(Me.Label24)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(18, 18)
        Me.pnlSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(1247, 95)
        Me.pnlSearch.TabIndex = 4
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnClear.Location = New System.Drawing.Point(1036, 51)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(92, 37)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'dtp_owd
        '
        Me.dtp_owd.Checked = False
        Me.dtp_owd.CustomFormat = "dd-MM-yyyy"
        Me.dtp_owd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_owd.Location = New System.Drawing.Point(750, 51)
        Me.dtp_owd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtp_owd.Name = "dtp_owd"
        Me.dtp_owd.ShowCheckBox = True
        Me.dtp_owd.Size = New System.Drawing.Size(175, 31)
        Me.dtp_owd.TabIndex = 5
        Me.dtp_owd.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(630, 52)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 26)
        Me.Label2.TabIndex = 151
        Me.Label2.Text = "Outward"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1138, 49)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 37)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btn_search
        '
        Me.btn_search.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_search.Location = New System.Drawing.Point(934, 49)
        Me.btn_search.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn_search.Name = "btn_search"
        Me.btn_search.Size = New System.Drawing.Size(92, 37)
        Me.btn_search.TabIndex = 6
        Me.btn_search.Text = "Search"
        Me.btn_search.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(324, 49)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(110, 26)
        Me.Label14.TabIndex = 147
        Me.Label14.Text = "Folio No"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_folio
        '
        Me.txt_folio.Location = New System.Drawing.Point(447, 49)
        Me.txt_folio.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txt_folio.Name = "txt_folio"
        Me.txt_folio.Size = New System.Drawing.Size(175, 31)
        Me.txt_folio.TabIndex = 4
        '
        'cb_cmpy
        '
        Me.cb_cmpy.BackColor = System.Drawing.SystemColors.Window
        Me.cb_cmpy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cb_cmpy.FormattingEnabled = True
        Me.cb_cmpy.Location = New System.Drawing.Point(750, 8)
        Me.cb_cmpy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cb_cmpy.Name = "cb_cmpy"
        Me.cb_cmpy.Size = New System.Drawing.Size(481, 32)
        Me.cb_cmpy.TabIndex = 2
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(631, 11)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(110, 23)
        Me.Label20.TabIndex = 146
        Me.Label20.Text = "Company"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtp_to
        '
        Me.dtp_to.Checked = False
        Me.dtp_to.CustomFormat = "dd-MM-yyyy"
        Me.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_to.Location = New System.Drawing.Point(447, 6)
        Me.dtp_to.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtp_to.Name = "dtp_to"
        Me.dtp_to.ShowCheckBox = True
        Me.dtp_to.Size = New System.Drawing.Size(175, 31)
        Me.dtp_to.TabIndex = 1
        Me.dtp_to.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(360, 9)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(74, 26)
        Me.Label22.TabIndex = 143
        Me.Label22.Text = "To"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtp_from
        '
        Me.dtp_from.Checked = False
        Me.dtp_from.CustomFormat = "dd-MM-yyyy"
        Me.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_from.Location = New System.Drawing.Point(144, 6)
        Me.dtp_from.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtp_from.Name = "dtp_from"
        Me.dtp_from.ShowCheckBox = True
        Me.dtp_from.Size = New System.Drawing.Size(175, 31)
        Me.dtp_from.TabIndex = 0
        Me.dtp_from.Value = New Date(2017, 2, 8, 0, 0, 0, 0)
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(4, 9)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(126, 26)
        Me.Label23.TabIndex = 144
        Me.Label23.Text = "Inward From"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_inward
        '
        Me.txt_inward.Location = New System.Drawing.Point(144, 47)
        Me.txt_inward.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txt_inward.Name = "txt_inward"
        Me.txt_inward.Size = New System.Drawing.Size(175, 31)
        Me.txt_inward.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(25, 49)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(110, 26)
        Me.Label24.TabIndex = 137
        Me.Label24.Text = "Inward No"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgv_covering
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_covering.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_covering.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_covering.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_covering.Location = New System.Drawing.Point(16, 123)
        Me.dgv_covering.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgv_covering.Name = "dgv_covering"
        Me.dgv_covering.Size = New System.Drawing.Size(1249, 446)
        Me.dgv_covering.TabIndex = 5
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(1032, 4)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(108, 37)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "&Export"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtTotRec)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlExport.Location = New System.Drawing.Point(18, 579)
        Me.pnlExport.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(1249, 58)
        Me.pnlExport.TabIndex = 20
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(4, 12)
        Me.txtTotRec.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotRec.MaxLength = 100
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(546, 20)
        Me.txtTotRec.TabIndex = 1
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'frmISRGenerateCoveringLetter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 648)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.dgv_covering)
        Me.Controls.Add(Me.pnlSearch)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmISRGenerateCoveringLetter"
        Me.Text = "ISR - Generate Covering Letter"
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
    Friend WithEvents dtp_to As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dtp_from As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txt_inward As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents dgv_covering As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button

End Class
