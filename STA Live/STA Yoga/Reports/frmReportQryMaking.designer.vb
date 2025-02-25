<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportQryMaking
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
        Me.grpRptFields = New System.Windows.Forms.GroupBox()
        Me.lstRptFields = New System.Windows.Forms.ListBox()
        Me.lstRptSelectedFields = New System.Windows.Forms.ListBox()
        Me.btnRptFieldDeselect = New System.Windows.Forms.Button()
        Me.btnRptFieldSelect = New System.Windows.Forms.Button()
        Me.grpTables = New System.Windows.Forms.GroupBox()
        Me.lstTables = New System.Windows.Forms.ListBox()
        Me.lstSelectedTables = New System.Windows.Forms.ListBox()
        Me.btnTblSelect = New System.Windows.Forms.Button()
        Me.btnTblDeselect = New System.Windows.Forms.Button()
        Me.grpConditions = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCondAdd = New System.Windows.Forms.Button()
        Me.cboCondField = New System.Windows.Forms.ComboBox()
        Me.cboCondition = New System.Windows.Forms.ComboBox()
        Me.btnCondRemove = New System.Windows.Forms.Button()
        Me.lblFld = New System.Windows.Forms.Label()
        Me.cboCondValue = New System.Windows.Forms.ComboBox()
        Me.lstConditions = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grpRptFields.SuspendLayout()
        Me.grpTables.SuspendLayout()
        Me.grpConditions.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpRptFields
        '
        Me.grpRptFields.Controls.Add(Me.lstRptFields)
        Me.grpRptFields.Controls.Add(Me.lstRptSelectedFields)
        Me.grpRptFields.Controls.Add(Me.btnRptFieldDeselect)
        Me.grpRptFields.Controls.Add(Me.btnRptFieldSelect)
        Me.grpRptFields.Location = New System.Drawing.Point(438, 12)
        Me.grpRptFields.Name = "grpRptFields"
        Me.grpRptFields.Size = New System.Drawing.Size(427, 238)
        Me.grpRptFields.TabIndex = 58
        Me.grpRptFields.TabStop = False
        Me.grpRptFields.Text = "Report Fields"
        '
        'lstRptFields
        '
        Me.lstRptFields.FormattingEnabled = True
        Me.lstRptFields.Location = New System.Drawing.Point(6, 20)
        Me.lstRptFields.Name = "lstRptFields"
        Me.lstRptFields.ScrollAlwaysVisible = True
        Me.lstRptFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRptFields.Size = New System.Drawing.Size(184, 212)
        Me.lstRptFields.TabIndex = 49
        '
        'lstRptSelectedFields
        '
        Me.lstRptSelectedFields.FormattingEnabled = True
        Me.lstRptSelectedFields.Location = New System.Drawing.Point(235, 20)
        Me.lstRptSelectedFields.Name = "lstRptSelectedFields"
        Me.lstRptSelectedFields.ScrollAlwaysVisible = True
        Me.lstRptSelectedFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRptSelectedFields.Size = New System.Drawing.Size(184, 212)
        Me.lstRptSelectedFields.TabIndex = 52
        '
        'btnRptFieldDeselect
        '
        Me.btnRptFieldDeselect.Location = New System.Drawing.Point(196, 129)
        Me.btnRptFieldDeselect.Name = "btnRptFieldDeselect"
        Me.btnRptFieldDeselect.Size = New System.Drawing.Size(33, 23)
        Me.btnRptFieldDeselect.TabIndex = 51
        Me.btnRptFieldDeselect.Text = "<<"
        Me.btnRptFieldDeselect.UseVisualStyleBackColor = True
        '
        'btnRptFieldSelect
        '
        Me.btnRptFieldSelect.Location = New System.Drawing.Point(196, 100)
        Me.btnRptFieldSelect.Name = "btnRptFieldSelect"
        Me.btnRptFieldSelect.Size = New System.Drawing.Size(33, 23)
        Me.btnRptFieldSelect.TabIndex = 50
        Me.btnRptFieldSelect.Text = ">>"
        Me.btnRptFieldSelect.UseVisualStyleBackColor = True
        '
        'grpTables
        '
        Me.grpTables.Controls.Add(Me.lstTables)
        Me.grpTables.Controls.Add(Me.lstSelectedTables)
        Me.grpTables.Controls.Add(Me.btnTblSelect)
        Me.grpTables.Controls.Add(Me.btnTblDeselect)
        Me.grpTables.Location = New System.Drawing.Point(10, 12)
        Me.grpTables.Name = "grpTables"
        Me.grpTables.Size = New System.Drawing.Size(422, 238)
        Me.grpTables.TabIndex = 57
        Me.grpTables.TabStop = False
        Me.grpTables.Text = "Select Tables for Report"
        '
        'lstTables
        '
        Me.lstTables.FormattingEnabled = True
        Me.lstTables.Location = New System.Drawing.Point(6, 20)
        Me.lstTables.Name = "lstTables"
        Me.lstTables.ScrollAlwaysVisible = True
        Me.lstTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTables.Size = New System.Drawing.Size(184, 212)
        Me.lstTables.Sorted = True
        Me.lstTables.TabIndex = 0
        '
        'lstSelectedTables
        '
        Me.lstSelectedTables.FormattingEnabled = True
        Me.lstSelectedTables.Location = New System.Drawing.Point(232, 20)
        Me.lstSelectedTables.Name = "lstSelectedTables"
        Me.lstSelectedTables.ScrollAlwaysVisible = True
        Me.lstSelectedTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstSelectedTables.Size = New System.Drawing.Size(184, 212)
        Me.lstSelectedTables.Sorted = True
        Me.lstSelectedTables.TabIndex = 3
        '
        'btnTblSelect
        '
        Me.btnTblSelect.Location = New System.Drawing.Point(196, 100)
        Me.btnTblSelect.Name = "btnTblSelect"
        Me.btnTblSelect.Size = New System.Drawing.Size(33, 23)
        Me.btnTblSelect.TabIndex = 1
        Me.btnTblSelect.Text = ">>"
        Me.btnTblSelect.UseVisualStyleBackColor = True
        '
        'btnTblDeselect
        '
        Me.btnTblDeselect.Location = New System.Drawing.Point(196, 129)
        Me.btnTblDeselect.Name = "btnTblDeselect"
        Me.btnTblDeselect.Size = New System.Drawing.Size(33, 23)
        Me.btnTblDeselect.TabIndex = 2
        Me.btnTblDeselect.Text = "<<"
        Me.btnTblDeselect.UseVisualStyleBackColor = True
        '
        'grpConditions
        '
        Me.grpConditions.Controls.Add(Me.Label1)
        Me.grpConditions.Controls.Add(Me.btnCondAdd)
        Me.grpConditions.Controls.Add(Me.cboCondField)
        Me.grpConditions.Controls.Add(Me.cboCondition)
        Me.grpConditions.Controls.Add(Me.btnCondRemove)
        Me.grpConditions.Controls.Add(Me.lblFld)
        Me.grpConditions.Controls.Add(Me.cboCondValue)
        Me.grpConditions.Controls.Add(Me.lstConditions)
        Me.grpConditions.Controls.Add(Me.Label3)
        Me.grpConditions.Controls.Add(Me.Label4)
        Me.grpConditions.Location = New System.Drawing.Point(10, 256)
        Me.grpConditions.Name = "grpConditions"
        Me.grpConditions.Size = New System.Drawing.Size(855, 197)
        Me.grpConditions.TabIndex = 56
        Me.grpConditions.TabStop = False
        Me.grpConditions.Text = "Conditions"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(16, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(390, 13)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Date Format (yyyy-MM-dd)   eg.  Input 24-May-2011 as 2011-05-24"
        '
        'btnCondAdd
        '
        Me.btnCondAdd.Location = New System.Drawing.Point(693, 47)
        Me.btnCondAdd.Name = "btnCondAdd"
        Me.btnCondAdd.Size = New System.Drawing.Size(74, 22)
        Me.btnCondAdd.TabIndex = 3
        Me.btnCondAdd.Text = "Add"
        Me.btnCondAdd.UseVisualStyleBackColor = True
        '
        'cboCondField
        '
        Me.cboCondField.Location = New System.Drawing.Point(61, 20)
        Me.cboCondField.Name = "cboCondField"
        Me.cboCondField.Size = New System.Drawing.Size(286, 21)
        Me.cboCondField.TabIndex = 0
        '
        'cboCondition
        '
        Me.cboCondition.Location = New System.Drawing.Point(425, 20)
        Me.cboCondition.Name = "cboCondition"
        Me.cboCondition.Size = New System.Drawing.Size(85, 21)
        Me.cboCondition.TabIndex = 1
        '
        'btnCondRemove
        '
        Me.btnCondRemove.Location = New System.Drawing.Point(773, 47)
        Me.btnCondRemove.Name = "btnCondRemove"
        Me.btnCondRemove.Size = New System.Drawing.Size(74, 22)
        Me.btnCondRemove.TabIndex = 4
        Me.btnCondRemove.Text = "Remove"
        Me.btnCondRemove.UseVisualStyleBackColor = True
        '
        'lblFld
        '
        Me.lblFld.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFld.AutoSize = True
        Me.lblFld.Location = New System.Drawing.Point(16, 24)
        Me.lblFld.Name = "lblFld"
        Me.lblFld.Size = New System.Drawing.Size(39, 13)
        Me.lblFld.TabIndex = 39
        Me.lblFld.Text = "Fields"
        '
        'cboCondValue
        '
        Me.cboCondValue.Location = New System.Drawing.Point(561, 20)
        Me.cboCondValue.Name = "cboCondValue"
        Me.cboCondValue.Size = New System.Drawing.Size(286, 21)
        Me.cboCondValue.TabIndex = 2
        '
        'lstConditions
        '
        Me.lstConditions.FormattingEnabled = True
        Me.lstConditions.Location = New System.Drawing.Point(6, 79)
        Me.lstConditions.Name = "lstConditions"
        Me.lstConditions.Size = New System.Drawing.Size(841, 108)
        Me.lstConditions.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(361, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Operator"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(519, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Value"
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(655, 459)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(121, 23)
        Me.btnGenerate.TabIndex = 59
        Me.btnGenerate.Text = "Generate Report"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(782, 459)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 60
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmReportQryMaking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 486)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.grpRptFields)
        Me.Controls.Add(Me.grpTables)
        Me.Controls.Add(Me.grpConditions)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportQryMaking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Query Engine"
        Me.grpRptFields.ResumeLayout(False)
        Me.grpTables.ResumeLayout(False)
        Me.grpConditions.ResumeLayout(False)
        Me.grpConditions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpRptFields As System.Windows.Forms.GroupBox
    Friend WithEvents lstRptFields As System.Windows.Forms.ListBox
    Friend WithEvents lstRptSelectedFields As System.Windows.Forms.ListBox
    Friend WithEvents btnRptFieldDeselect As System.Windows.Forms.Button
    Friend WithEvents btnRptFieldSelect As System.Windows.Forms.Button
    Friend WithEvents grpTables As System.Windows.Forms.GroupBox
    Friend WithEvents lstTables As System.Windows.Forms.ListBox
    Friend WithEvents lstSelectedTables As System.Windows.Forms.ListBox
    Friend WithEvents btnTblSelect As System.Windows.Forms.Button
    Friend WithEvents btnTblDeselect As System.Windows.Forms.Button
    Friend WithEvents grpConditions As System.Windows.Forms.GroupBox
    Friend WithEvents btnCondAdd As System.Windows.Forms.Button
    Friend WithEvents cboCondField As System.Windows.Forms.ComboBox
    Friend WithEvents cboCondition As System.Windows.Forms.ComboBox
    Friend WithEvents btnCondRemove As System.Windows.Forms.Button
    Friend WithEvents lblFld As System.Windows.Forms.Label
    Friend WithEvents cboCondValue As System.Windows.Forms.ComboBox
    Friend WithEvents lstConditions As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
