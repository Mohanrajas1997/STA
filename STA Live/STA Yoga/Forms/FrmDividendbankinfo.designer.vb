<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDividendbankinfo
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
        Me.pnldiv = New System.Windows.Forms.Panel()
        Me.txtuserno = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtsponsor = New System.Windows.Forms.TextBox()
        Me.txtbankifsc = New System.Windows.Forms.TextBox()
        Me.txtdivamt = New System.Windows.Forms.TextBox()
        Me.txtbankaccno = New System.Windows.Forms.TextBox()
        Me.txtbankbranch = New System.Windows.Forms.TextBox()
        Me.txtmicr = New System.Windows.Forms.TextBox()
        Me.txtbankname = New System.Windows.Forms.TextBox()
        Me.txtdivpercent = New System.Windows.Forms.TextBox()
        Me.dtnopen = New System.Windows.Forms.DateTimePicker()
        Me.dtnsettle = New System.Windows.Forms.DateTimePicker()
        Me.cbointcode = New System.Windows.Forms.ComboBox()
        Me.cbofinyear = New System.Windows.Forms.ComboBox()
        Me.cbocompany = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.pnlnew = New System.Windows.Forms.Panel()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.pnlsave = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnldiv.SuspendLayout()
        Me.pnlnew.SuspendLayout()
        Me.pnlsave.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnldiv
        '
        Me.pnldiv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnldiv.Controls.Add(Me.txtuserno)
        Me.pnldiv.Controls.Add(Me.Label14)
        Me.pnldiv.Controls.Add(Me.txtsponsor)
        Me.pnldiv.Controls.Add(Me.txtbankifsc)
        Me.pnldiv.Controls.Add(Me.txtdivamt)
        Me.pnldiv.Controls.Add(Me.txtbankaccno)
        Me.pnldiv.Controls.Add(Me.txtbankbranch)
        Me.pnldiv.Controls.Add(Me.txtmicr)
        Me.pnldiv.Controls.Add(Me.txtbankname)
        Me.pnldiv.Controls.Add(Me.txtdivpercent)
        Me.pnldiv.Controls.Add(Me.dtnopen)
        Me.pnldiv.Controls.Add(Me.dtnsettle)
        Me.pnldiv.Controls.Add(Me.cbointcode)
        Me.pnldiv.Controls.Add(Me.cbofinyear)
        Me.pnldiv.Controls.Add(Me.cbocompany)
        Me.pnldiv.Controls.Add(Me.Label13)
        Me.pnldiv.Controls.Add(Me.Label12)
        Me.pnldiv.Controls.Add(Me.Label11)
        Me.pnldiv.Controls.Add(Me.Label10)
        Me.pnldiv.Controls.Add(Me.Label9)
        Me.pnldiv.Controls.Add(Me.Label8)
        Me.pnldiv.Controls.Add(Me.Label7)
        Me.pnldiv.Controls.Add(Me.Label6)
        Me.pnldiv.Controls.Add(Me.Label5)
        Me.pnldiv.Controls.Add(Me.Label4)
        Me.pnldiv.Controls.Add(Me.Label3)
        Me.pnldiv.Controls.Add(Me.Label2)
        Me.pnldiv.Controls.Add(Me.Label1)
        Me.pnldiv.Controls.Add(Me.txtId)
        Me.pnldiv.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnldiv.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pnldiv.Location = New System.Drawing.Point(12, 10)
        Me.pnldiv.Name = "pnldiv"
        Me.pnldiv.Size = New System.Drawing.Size(842, 237)
        Me.pnldiv.TabIndex = 0
        '
        'txtuserno
        '
        Me.txtuserno.Location = New System.Drawing.Point(564, 81)
        Me.txtuserno.Name = "txtuserno"
        Me.txtuserno.Size = New System.Drawing.Size(271, 21)
        Me.txtuserno.TabIndex = 10
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(446, 85)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 13)
        Me.Label14.TabIndex = 65
        Me.Label14.Text = "User No."
        '
        'txtsponsor
        '
        Me.txtsponsor.Location = New System.Drawing.Point(128, 204)
        Me.txtsponsor.Name = "txtsponsor"
        Me.txtsponsor.Size = New System.Drawing.Size(258, 21)
        Me.txtsponsor.TabIndex = 7
        '
        'txtbankifsc
        '
        Me.txtbankifsc.Location = New System.Drawing.Point(128, 143)
        Me.txtbankifsc.Name = "txtbankifsc"
        Me.txtbankifsc.Size = New System.Drawing.Size(258, 21)
        Me.txtbankifsc.TabIndex = 5
        '
        'txtdivamt
        '
        Me.txtdivamt.Location = New System.Drawing.Point(564, 15)
        Me.txtdivamt.Name = "txtdivamt"
        Me.txtdivamt.Size = New System.Drawing.Size(271, 21)
        Me.txtdivamt.TabIndex = 8
        '
        'txtbankaccno
        '
        Me.txtbankaccno.Location = New System.Drawing.Point(130, 49)
        Me.txtbankaccno.Name = "txtbankaccno"
        Me.txtbankaccno.Size = New System.Drawing.Size(258, 21)
        Me.txtbankaccno.TabIndex = 2
        '
        'txtbankbranch
        '
        Me.txtbankbranch.Location = New System.Drawing.Point(128, 113)
        Me.txtbankbranch.Name = "txtbankbranch"
        Me.txtbankbranch.Size = New System.Drawing.Size(258, 21)
        Me.txtbankbranch.TabIndex = 4
        '
        'txtmicr
        '
        Me.txtmicr.Location = New System.Drawing.Point(130, 173)
        Me.txtmicr.Name = "txtmicr"
        Me.txtmicr.Size = New System.Drawing.Size(256, 21)
        Me.txtmicr.TabIndex = 6
        '
        'txtbankname
        '
        Me.txtbankname.Location = New System.Drawing.Point(130, 81)
        Me.txtbankname.Name = "txtbankname"
        Me.txtbankname.Size = New System.Drawing.Size(258, 21)
        Me.txtbankname.TabIndex = 3
        '
        'txtdivpercent
        '
        Me.txtdivpercent.Location = New System.Drawing.Point(564, 47)
        Me.txtdivpercent.Name = "txtdivpercent"
        Me.txtdivpercent.Size = New System.Drawing.Size(271, 21)
        Me.txtdivpercent.TabIndex = 9
        '
        'dtnopen
        '
        Me.dtnopen.CustomFormat = "dd-MM-yyyy"
        Me.dtnopen.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtnopen.Location = New System.Drawing.Point(563, 173)
        Me.dtnopen.Name = "dtnopen"
        Me.dtnopen.Size = New System.Drawing.Size(110, 21)
        Me.dtnopen.TabIndex = 12
        '
        'dtnsettle
        '
        Me.dtnsettle.CustomFormat = "dd-MM-yyyy"
        Me.dtnsettle.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtnsettle.Location = New System.Drawing.Point(562, 203)
        Me.dtnsettle.Name = "dtnsettle"
        Me.dtnsettle.Size = New System.Drawing.Size(110, 21)
        Me.dtnsettle.TabIndex = 13
        '
        'cbointcode
        '
        Me.cbointcode.FormattingEnabled = True
        Me.cbointcode.Location = New System.Drawing.Point(564, 141)
        Me.cbointcode.Name = "cbointcode"
        Me.cbointcode.Size = New System.Drawing.Size(110, 21)
        Me.cbointcode.TabIndex = 11
        '
        'cbofinyear
        '
        Me.cbofinyear.FormattingEnabled = True
        Me.cbofinyear.Location = New System.Drawing.Point(564, 111)
        Me.cbofinyear.Name = "cbofinyear"
        Me.cbofinyear.Size = New System.Drawing.Size(110, 21)
        Me.cbofinyear.TabIndex = 10
        '
        'cbocompany
        '
        Me.cbocompany.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cbocompany.FormattingEnabled = True
        Me.cbocompany.Location = New System.Drawing.Point(130, 18)
        Me.cbocompany.Name = "cbocompany"
        Me.cbocompany.Size = New System.Drawing.Size(258, 21)
        Me.cbocompany.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(446, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(104, 13)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Dividend Amount"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(21, 210)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(84, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Sponsor Code"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(21, 178)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(37, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "MICR"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(21, 53)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Account No."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(21, 146)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "IFSC"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(21, 118)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Branch"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(21, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Bank Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(446, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Dividend %"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(446, 208)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Settlement Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(446, 177)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Account Open Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(446, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Interim Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(446, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Financial Year"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company Name"
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(3, 4)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(26, 21)
        Me.txtId.TabIndex = 64
        Me.txtId.Visible = False
        '
        'pnlnew
        '
        Me.pnlnew.Controls.Add(Me.btnclear)
        Me.pnlnew.Controls.Add(Me.btndelete)
        Me.pnlnew.Controls.Add(Me.btnFind)
        Me.pnlnew.Controls.Add(Me.btnEdit)
        Me.pnlnew.Controls.Add(Me.btnnew)
        Me.pnlnew.Location = New System.Drawing.Point(215, 249)
        Me.pnlnew.Name = "pnlnew"
        Me.pnlnew.Size = New System.Drawing.Size(405, 31)
        Me.pnlnew.TabIndex = 2
        '
        'btnclear
        '
        Me.btnclear.Location = New System.Drawing.Point(328, 4)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(75, 23)
        Me.btnclear.TabIndex = 20
        Me.btnclear.Text = "Close"
        Me.btnclear.UseVisualStyleBackColor = True
        '
        'btndelete
        '
        Me.btndelete.Location = New System.Drawing.Point(247, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(75, 23)
        Me.btndelete.TabIndex = 19
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(166, 5)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 23)
        Me.btnFind.TabIndex = 18
        Me.btnFind.Text = "Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(85, 4)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 17
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnnew
        '
        Me.btnnew.Location = New System.Drawing.Point(4, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(75, 23)
        Me.btnnew.TabIndex = 16
        Me.btnnew.Text = "New"
        Me.btnnew.UseVisualStyleBackColor = True
        '
        'pnlsave
        '
        Me.pnlsave.Controls.Add(Me.btnCancel)
        Me.pnlsave.Controls.Add(Me.btnSave)
        Me.pnlsave.Location = New System.Drawing.Point(351, 253)
        Me.pnlsave.Name = "pnlsave"
        Me.pnlsave.Size = New System.Drawing.Size(164, 27)
        Me.pnlsave.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(84, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'FrmDividendbankinfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(861, 287)
        Me.Controls.Add(Me.pnlnew)
        Me.Controls.Add(Me.pnlsave)
        Me.Controls.Add(Me.pnldiv)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDividendbankinfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bank Information"
        Me.pnldiv.ResumeLayout(False)
        Me.pnldiv.PerformLayout()
        Me.pnlnew.ResumeLayout(False)
        Me.pnlsave.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnldiv As System.Windows.Forms.Panel
    Friend WithEvents pnlsave As System.Windows.Forms.Panel
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents txtsponsor As System.Windows.Forms.TextBox
    Friend WithEvents txtbankifsc As System.Windows.Forms.TextBox
    Friend WithEvents txtdivamt As System.Windows.Forms.TextBox
    Friend WithEvents txtbankaccno As System.Windows.Forms.TextBox
    Friend WithEvents txtbankbranch As System.Windows.Forms.TextBox
    Friend WithEvents txtmicr As System.Windows.Forms.TextBox
    Friend WithEvents txtbankname As System.Windows.Forms.TextBox
    Friend WithEvents txtdivpercent As System.Windows.Forms.TextBox
    Friend WithEvents dtnopen As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtnsettle As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbointcode As System.Windows.Forms.ComboBox
    Friend WithEvents cbofinyear As System.Windows.Forms.ComboBox
    Friend WithEvents cbocompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlnew As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnnew As System.Windows.Forms.Button
    Friend WithEvents txtuserno As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
End Class
