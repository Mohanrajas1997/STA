<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPendingBenpostReport
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
        Me.btnproccess = New System.Windows.Forms.Button()
        Me.dtpTodate = New System.Windows.Forms.DateTimePicker()
        Me.lblTodate = New System.Windows.Forms.Label()
        Me.cbdepotype = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.btnproccess)
        Me.pnlSearch.Controls.Add(Me.dtpTodate)
        Me.pnlSearch.Controls.Add(Me.lblTodate)
        Me.pnlSearch.Controls.Add(Me.cbdepotype)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.dtpFrom)
        Me.pnlSearch.Controls.Add(Me.lblFrom)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.cboCompany)
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Location = New System.Drawing.Point(11, 7)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(475, 99)
        Me.pnlSearch.TabIndex = 6
        '
        'btnproccess
        '
        Me.btnproccess.Location = New System.Drawing.Point(228, 68)
        Me.btnproccess.Name = "btnproccess"
        Me.btnproccess.Size = New System.Drawing.Size(72, 24)
        Me.btnproccess.TabIndex = 4
        Me.btnproccess.Text = "Process"
        '
        'dtpTodate
        '
        Me.dtpTodate.CustomFormat = "dd-MM-yyyy"
        Me.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTodate.Location = New System.Drawing.Point(349, 40)
        Me.dtpTodate.Name = "dtpTodate"
        Me.dtpTodate.Size = New System.Drawing.Size(105, 21)
        Me.dtpTodate.TabIndex = 2
        Me.dtpTodate.Value = New Date(2021, 2, 25, 0, 0, 0, 0)
        '
        'lblTodate
        '
        Me.lblTodate.Location = New System.Drawing.Point(245, 42)
        Me.lblTodate.Name = "lblTodate"
        Me.lblTodate.Size = New System.Drawing.Size(97, 17)
        Me.lblTodate.TabIndex = 135
        Me.lblTodate.Text = "To Date"
        Me.lblTodate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbdepotype
        '
        Me.cbdepotype.FormattingEnabled = True
        Me.cbdepotype.Location = New System.Drawing.Point(104, 68)
        Me.cbdepotype.Name = "cbdepotype"
        Me.cbdepotype.Size = New System.Drawing.Size(105, 21)
        Me.cbdepotype.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(-13, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 17)
        Me.Label5.TabIndex = 133
        Me.Label5.Text = "Depository"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(104, 42)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpFrom.TabIndex = 1
        Me.dtpFrom.Value = New Date(2021, 1, 22, 0, 0, 0, 0)
        '
        'lblFrom
        '
        Me.lblFrom.Location = New System.Drawing.Point(0, 44)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(97, 17)
        Me.lblFrom.TabIndex = 121
        Me.lblFrom.Text = "From Date"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(306, 68)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(104, 12)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(352, 21)
        Me.cboCompany.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(384, 68)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmPendingBenpostReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 118)
        Me.Controls.Add(Me.pnlSearch)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPendingBenpostReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pending Benpost Report"
        Me.pnlSearch.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents cbdepotype As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnproccess As System.Windows.Forms.Button
    Friend WithEvents dtpTodate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTodate As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
End Class
