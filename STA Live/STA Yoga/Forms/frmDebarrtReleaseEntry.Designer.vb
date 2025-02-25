<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDebarrtReleaseEntry
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
        Me.pnlOrderHeader = New System.Windows.Forms.Panel()
        Me.lblOrderdate = New System.Windows.Forms.Label()
        Me.lblOrderno = New System.Windows.Forms.Label()
        Me.txtOrderno = New System.Windows.Forms.TextBox()
        Me.txtremark = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpOrderDate = New System.Windows.Forms.DateTimePicker()
        Me.txtorder_Id = New System.Windows.Forms.TextBox()
        Me.dgvDbtDetail = New System.Windows.Forms.DataGridView()
        Me.lblDPD = New System.Windows.Forms.Label()
        Me.txtorderdtl_id = New System.Windows.Forms.TextBox()
        Me.pnlDbtDtl = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnClearGrid = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtpanno = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtholdername = New System.Windows.Forms.TextBox()
        Me.lblSlno = New System.Windows.Forms.Label()
        Me.txtSlno = New System.Windows.Forms.TextBox()
        Me.lblDPHeader = New System.Windows.Forms.Label()
        Me.pnlDbtDtlMain = New System.Windows.Forms.Panel()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlOrderHeader.SuspendLayout()
        CType(Me.dgvDbtDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDbtDtl.SuspendLayout()
        Me.pnlDbtDtlMain.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlOrderHeader
        '
        Me.pnlOrderHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlOrderHeader.Controls.Add(Me.lblOrderdate)
        Me.pnlOrderHeader.Controls.Add(Me.lblOrderno)
        Me.pnlOrderHeader.Controls.Add(Me.txtOrderno)
        Me.pnlOrderHeader.Controls.Add(Me.txtremark)
        Me.pnlOrderHeader.Controls.Add(Me.Label1)
        Me.pnlOrderHeader.Controls.Add(Me.dtpOrderDate)
        Me.pnlOrderHeader.Controls.Add(Me.txtorder_Id)
        Me.pnlOrderHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlOrderHeader.Location = New System.Drawing.Point(12, 34)
        Me.pnlOrderHeader.Name = "pnlOrderHeader"
        Me.pnlOrderHeader.Size = New System.Drawing.Size(538, 96)
        Me.pnlOrderHeader.TabIndex = 0
        '
        'lblOrderdate
        '
        Me.lblOrderdate.Location = New System.Drawing.Point(275, 6)
        Me.lblOrderdate.Name = "lblOrderdate"
        Me.lblOrderdate.Size = New System.Drawing.Size(73, 20)
        Me.lblOrderdate.TabIndex = 2
        Me.lblOrderdate.Text = "Order Date"
        Me.lblOrderdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOrderno
        '
        Me.lblOrderno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOrderno.Location = New System.Drawing.Point(42, 9)
        Me.lblOrderno.Name = "lblOrderno"
        Me.lblOrderno.Size = New System.Drawing.Size(58, 13)
        Me.lblOrderno.TabIndex = 0
        Me.lblOrderno.Text = "Order No"
        Me.lblOrderno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOrderno
        '
        Me.txtOrderno.BackColor = System.Drawing.SystemColors.Window
        Me.txtOrderno.ForeColor = System.Drawing.Color.Black
        Me.txtOrderno.Location = New System.Drawing.Point(106, 6)
        Me.txtOrderno.MaxLength = 128
        Me.txtOrderno.Name = "txtOrderno"
        Me.txtOrderno.Size = New System.Drawing.Size(163, 21)
        Me.txtOrderno.TabIndex = 0
        '
        'txtremark
        '
        Me.txtremark.Location = New System.Drawing.Point(107, 34)
        Me.txtremark.MaxLength = 255
        Me.txtremark.Multiline = True
        Me.txtremark.Name = "txtremark"
        Me.txtremark.Size = New System.Drawing.Size(410, 49)
        Me.txtremark.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(45, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "&Remark"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpOrderDate
        '
        Me.dtpOrderDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpOrderDate.Location = New System.Drawing.Point(354, 6)
        Me.dtpOrderDate.Name = "dtpOrderDate"
        Me.dtpOrderDate.Size = New System.Drawing.Size(163, 21)
        Me.dtpOrderDate.TabIndex = 1
        '
        'txtorder_Id
        '
        Me.txtorder_Id.Location = New System.Drawing.Point(8, 6)
        Me.txtorder_Id.Name = "txtorder_Id"
        Me.txtorder_Id.Size = New System.Drawing.Size(30, 21)
        Me.txtorder_Id.TabIndex = 64
        Me.txtorder_Id.Visible = False
        '
        'dgvDbtDetail
        '
        Me.dgvDbtDetail.AllowUserToAddRows = False
        Me.dgvDbtDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDbtDetail.Location = New System.Drawing.Point(8, 116)
        Me.dgvDbtDetail.Name = "dgvDbtDetail"
        Me.dgvDbtDetail.Size = New System.Drawing.Size(518, 150)
        Me.dgvDbtDetail.TabIndex = 1
        '
        'lblDPD
        '
        Me.lblDPD.AutoSize = True
        Me.lblDPD.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDPD.ForeColor = System.Drawing.Color.Blue
        Me.lblDPD.Location = New System.Drawing.Point(14, 135)
        Me.lblDPD.Name = "lblDPD"
        Me.lblDPD.Size = New System.Drawing.Size(165, 13)
        Me.lblDPD.TabIndex = 7
        Me.lblDPD.Text = "Debarrt Release Pan Details"
        '
        'txtorderdtl_id
        '
        Me.txtorderdtl_id.Location = New System.Drawing.Point(7, 6)
        Me.txtorderdtl_id.Name = "txtorderdtl_id"
        Me.txtorderdtl_id.Size = New System.Drawing.Size(30, 21)
        Me.txtorderdtl_id.TabIndex = 143
        Me.txtorderdtl_id.Visible = False
        '
        'pnlDbtDtl
        '
        Me.pnlDbtDtl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDbtDtl.Controls.Add(Me.btnAdd)
        Me.pnlDbtDtl.Controls.Add(Me.btnClearGrid)
        Me.pnlDbtDtl.Controls.Add(Me.txtorderdtl_id)
        Me.pnlDbtDtl.Controls.Add(Me.Label2)
        Me.pnlDbtDtl.Controls.Add(Me.txtpanno)
        Me.pnlDbtDtl.Controls.Add(Me.Label3)
        Me.pnlDbtDtl.Controls.Add(Me.txtholdername)
        Me.pnlDbtDtl.Controls.Add(Me.lblSlno)
        Me.pnlDbtDtl.Controls.Add(Me.txtSlno)
        Me.pnlDbtDtl.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDbtDtl.Location = New System.Drawing.Point(8, 8)
        Me.pnlDbtDtl.Name = "pnlDbtDtl"
        Me.pnlDbtDtl.Size = New System.Drawing.Size(518, 98)
        Me.pnlDbtDtl.TabIndex = 0
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(188, 63)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(72, 24)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnClearGrid
        '
        Me.btnClearGrid.Location = New System.Drawing.Point(266, 63)
        Me.btnClearGrid.Name = "btnClearGrid"
        Me.btnClearGrid.Size = New System.Drawing.Size(72, 24)
        Me.btnClearGrid.TabIndex = 4
        Me.btnClearGrid.Text = "Clear"
        Me.btnClearGrid.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(283, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 140
        Me.Label2.Text = "Pan No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtpanno
        '
        Me.txtpanno.BackColor = System.Drawing.SystemColors.Window
        Me.txtpanno.ForeColor = System.Drawing.Color.Black
        Me.txtpanno.Location = New System.Drawing.Point(345, 6)
        Me.txtpanno.MaxLength = 10
        Me.txtpanno.Name = "txtpanno"
        Me.txtpanno.Size = New System.Drawing.Size(163, 21)
        Me.txtpanno.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 138
        Me.Label3.Text = "Holder Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtholdername
        '
        Me.txtholdername.BackColor = System.Drawing.SystemColors.Window
        Me.txtholdername.ForeColor = System.Drawing.Color.Black
        Me.txtholdername.Location = New System.Drawing.Point(98, 36)
        Me.txtholdername.MaxLength = 128
        Me.txtholdername.Name = "txtholdername"
        Me.txtholdername.Size = New System.Drawing.Size(410, 21)
        Me.txtholdername.TabIndex = 2
        '
        'lblSlno
        '
        Me.lblSlno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSlno.Location = New System.Drawing.Point(38, 9)
        Me.lblSlno.Name = "lblSlno"
        Me.lblSlno.Size = New System.Drawing.Size(53, 13)
        Me.lblSlno.TabIndex = 132
        Me.lblSlno.Text = "Sl No"
        Me.lblSlno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSlno
        '
        Me.txtSlno.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlno.ForeColor = System.Drawing.Color.Red
        Me.txtSlno.Location = New System.Drawing.Point(98, 6)
        Me.txtSlno.MaxLength = 3
        Me.txtSlno.Name = "txtSlno"
        Me.txtSlno.Size = New System.Drawing.Size(163, 21)
        Me.txtSlno.TabIndex = 0
        '
        'lblDPHeader
        '
        Me.lblDPHeader.AutoSize = True
        Me.lblDPHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDPHeader.ForeColor = System.Drawing.Color.Blue
        Me.lblDPHeader.Location = New System.Drawing.Point(12, 9)
        Me.lblDPHeader.Name = "lblDPHeader"
        Me.lblDPHeader.Size = New System.Drawing.Size(167, 13)
        Me.lblDPHeader.TabIndex = 145
        Me.lblDPHeader.Text = "Debarrt Release Pan Header"
        '
        'pnlDbtDtlMain
        '
        Me.pnlDbtDtlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDbtDtlMain.Controls.Add(Me.dgvDbtDetail)
        Me.pnlDbtDtlMain.Controls.Add(Me.pnlDbtDtl)
        Me.pnlDbtDtlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDbtDtlMain.Location = New System.Drawing.Point(12, 161)
        Me.pnlDbtDtlMain.Name = "pnlDbtDtlMain"
        Me.pnlDbtDtlMain.Size = New System.Drawing.Size(538, 278)
        Me.pnlDbtDtlMain.TabIndex = 1
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlButtons.Location = New System.Drawing.Point(84, 445)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(386, 28)
        Me.pnlButtons.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(313, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFind.Location = New System.Drawing.Point(157, 1)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(72, 24)
        Me.btnFind.TabIndex = 13
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Location = New System.Drawing.Point(235, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 24)
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEdit.Location = New System.Drawing.Point(79, 1)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(72, 24)
        Me.btnEdit.TabIndex = 12
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.SystemColors.Control
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Location = New System.Drawing.Point(1, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(72, 24)
        Me.btnNew.TabIndex = 11
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'pnlSave
        '
        Me.pnlSave.CausesValidation = False
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSave.Location = New System.Drawing.Point(209, 445)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(152, 28)
        Me.pnlSave.TabIndex = 3
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(80, 1)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(2, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmDebarrtReleaseEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 487)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.pnlDbtDtlMain)
        Me.Controls.Add(Me.lblDPHeader)
        Me.Controls.Add(Me.lblDPD)
        Me.Controls.Add(Me.pnlOrderHeader)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MaximizeBox = False
        Me.Name = "frmDebarrtReleaseEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Debarrt Release Entry"
        Me.pnlOrderHeader.ResumeLayout(False)
        Me.pnlOrderHeader.PerformLayout()
        CType(Me.dgvDbtDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDbtDtl.ResumeLayout(False)
        Me.pnlDbtDtl.PerformLayout()
        Me.pnlDbtDtlMain.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlSave.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlOrderHeader As System.Windows.Forms.Panel
    Friend WithEvents lblOrderdate As System.Windows.Forms.Label
    Friend WithEvents lblOrderno As System.Windows.Forms.Label
    Friend WithEvents txtOrderno As System.Windows.Forms.TextBox
    Friend WithEvents txtremark As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpOrderDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtorder_Id As System.Windows.Forms.TextBox
    Friend WithEvents dgvDbtDetail As System.Windows.Forms.DataGridView
    Friend WithEvents lblDPD As System.Windows.Forms.Label
    Friend WithEvents txtorderdtl_id As System.Windows.Forms.TextBox
    Friend WithEvents pnlDbtDtl As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtpanno As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtholdername As System.Windows.Forms.TextBox
    Friend WithEvents lblDPHeader As System.Windows.Forms.Label
    Friend WithEvents lblSlno As System.Windows.Forms.Label
    Friend WithEvents txtSlno As System.Windows.Forms.TextBox
    Friend WithEvents pnlDbtDtlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnClearGrid As System.Windows.Forms.Button
End Class
