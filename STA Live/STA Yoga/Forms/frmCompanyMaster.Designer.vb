<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompanyMaster
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
        Me.pnlElectronics = New System.Windows.Forms.Panel()
        Me.rb_electronics_no = New System.Windows.Forms.RadioButton()
        Me.rb_electronics_yes = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.lblPanNo = New System.Windows.Forms.Label()
        Me.txtCinNo = New System.Windows.Forms.TextBox()
        Me.lblcin_no = New System.Windows.Forms.Label()
        Me.txtPincode = New System.Windows.Forms.TextBox()
        Me.lblPincode = New System.Windows.Forms.Label()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.lblCountry = New System.Windows.Forms.Label()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.lblState = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.lblAddress3 = New System.Windows.Forms.Label()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.lblAddress2 = New System.Windows.Forms.Label()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.lblAddress1 = New System.Windows.Forms.Label()
        Me.txtPaidupValue = New System.Windows.Forms.TextBox()
        Me.lblPaidupValue = New System.Windows.Forms.Label()
        Me.txtShareQty = New System.Windows.Forms.TextBox()
        Me.lblShareQty = New System.Windows.Forms.Label()
        Me.txtSecurityType = New System.Windows.Forms.TextBox()
        Me.lblSecurityType = New System.Windows.Forms.Label()
        Me.pnlPrefixsno = New System.Windows.Forms.Panel()
        Me.RbdprefixSnoNo = New System.Windows.Forms.RadioButton()
        Me.RbdprefixSnoYes = New System.Windows.Forms.RadioButton()
        Me.pnlPrefix = New System.Windows.Forms.Panel()
        Me.RbdprefixNo = New System.Windows.Forms.RadioButton()
        Me.RbdprefixYes = New System.Windows.Forms.RadioButton()
        Me.pnlComp = New System.Windows.Forms.Panel()
        Me.RbdCompYes = New System.Windows.Forms.RadioButton()
        Me.RbdCompNo = New System.Windows.Forms.RadioButton()
        Me.pnlAct1 = New System.Windows.Forms.Panel()
        Me.Rbdactiveyes = New System.Windows.Forms.RadioButton()
        Me.RbdActiveNo = New System.Windows.Forms.RadioButton()
        Me.lblActiveFlag = New System.Windows.Forms.Label()
        Me.txtid = New System.Windows.Forms.TextBox()
        Me.txtShareCapital = New System.Windows.Forms.TextBox()
        Me.lblShareCapital = New System.Windows.Forms.Label()
        Me.lblCompList = New System.Windows.Forms.Label()
        Me.txtInwardSlno = New System.Windows.Forms.TextBox()
        Me.lblInwardSlno = New System.Windows.Forms.Label()
        Me.txtObjxSlno = New System.Windows.Forms.TextBox()
        Me.lblObjxSlno = New System.Windows.Forms.Label()
        Me.txtCertSlno = New System.Windows.Forms.TextBox()
        Me.lblCertSlno = New System.Windows.Forms.Label()
        Me.txtFolioTranSlno = New System.Windows.Forms.TextBox()
        Me.lblFolioTransferSlno = New System.Windows.Forms.Label()
        Me.txtFolioSlno = New System.Windows.Forms.TextBox()
        Me.lblFolioSlno = New System.Windows.Forms.Label()
        Me.txtUploadSlno = New System.Windows.Forms.TextBox()
        Me.lblUploadSlno = New System.Windows.Forms.Label()
        Me.txtFolioPrefixField = New System.Windows.Forms.TextBox()
        Me.lblFolioPrefixField = New System.Windows.Forms.Label()
        Me.txtFolioPrefix = New System.Windows.Forms.TextBox()
        Me.lblFolioPrefix = New System.Windows.Forms.Label()
        Me.lblFolioPrefixSnoFlag = New System.Windows.Forms.Label()
        Me.lblFolioPrefixFlag = New System.Windows.Forms.Label()
        Me.txtFolioNoFormat = New System.Windows.Forms.TextBox()
        Me.lblfolionoformat = New System.Windows.Forms.Label()
        Me.txtIsinId = New System.Windows.Forms.TextBox()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.lblisinid = New System.Windows.Forms.Label()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.txtCompShortCode = New System.Windows.Forms.TextBox()
        Me.lblCompShortCode = New System.Windows.Forms.Label()
        Me.txtCompanyCode = New System.Windows.Forms.TextBox()
        Me.lblCompanyCode = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.pnlElectronics.SuspendLayout()
        Me.pnlPrefixsno.SuspendLayout()
        Me.pnlPrefix.SuspendLayout()
        Me.pnlComp.SuspendLayout()
        Me.pnlAct1.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.pnlElectronics)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.txtPanNo)
        Me.pnlMain.Controls.Add(Me.lblPanNo)
        Me.pnlMain.Controls.Add(Me.txtCinNo)
        Me.pnlMain.Controls.Add(Me.lblcin_no)
        Me.pnlMain.Controls.Add(Me.txtPincode)
        Me.pnlMain.Controls.Add(Me.lblPincode)
        Me.pnlMain.Controls.Add(Me.txtCountry)
        Me.pnlMain.Controls.Add(Me.lblCountry)
        Me.pnlMain.Controls.Add(Me.txtState)
        Me.pnlMain.Controls.Add(Me.lblState)
        Me.pnlMain.Controls.Add(Me.txtCity)
        Me.pnlMain.Controls.Add(Me.lblCity)
        Me.pnlMain.Controls.Add(Me.txtAddress3)
        Me.pnlMain.Controls.Add(Me.lblAddress3)
        Me.pnlMain.Controls.Add(Me.txtAddress2)
        Me.pnlMain.Controls.Add(Me.lblAddress2)
        Me.pnlMain.Controls.Add(Me.txtAddress1)
        Me.pnlMain.Controls.Add(Me.lblAddress1)
        Me.pnlMain.Controls.Add(Me.txtPaidupValue)
        Me.pnlMain.Controls.Add(Me.lblPaidupValue)
        Me.pnlMain.Controls.Add(Me.txtShareQty)
        Me.pnlMain.Controls.Add(Me.lblShareQty)
        Me.pnlMain.Controls.Add(Me.txtSecurityType)
        Me.pnlMain.Controls.Add(Me.lblSecurityType)
        Me.pnlMain.Controls.Add(Me.pnlPrefixsno)
        Me.pnlMain.Controls.Add(Me.pnlPrefix)
        Me.pnlMain.Controls.Add(Me.pnlComp)
        Me.pnlMain.Controls.Add(Me.pnlAct1)
        Me.pnlMain.Controls.Add(Me.lblActiveFlag)
        Me.pnlMain.Controls.Add(Me.txtid)
        Me.pnlMain.Controls.Add(Me.txtShareCapital)
        Me.pnlMain.Controls.Add(Me.lblShareCapital)
        Me.pnlMain.Controls.Add(Me.lblCompList)
        Me.pnlMain.Controls.Add(Me.txtInwardSlno)
        Me.pnlMain.Controls.Add(Me.lblInwardSlno)
        Me.pnlMain.Controls.Add(Me.txtObjxSlno)
        Me.pnlMain.Controls.Add(Me.lblObjxSlno)
        Me.pnlMain.Controls.Add(Me.txtCertSlno)
        Me.pnlMain.Controls.Add(Me.lblCertSlno)
        Me.pnlMain.Controls.Add(Me.txtFolioTranSlno)
        Me.pnlMain.Controls.Add(Me.lblFolioTransferSlno)
        Me.pnlMain.Controls.Add(Me.txtFolioSlno)
        Me.pnlMain.Controls.Add(Me.lblFolioSlno)
        Me.pnlMain.Controls.Add(Me.txtUploadSlno)
        Me.pnlMain.Controls.Add(Me.lblUploadSlno)
        Me.pnlMain.Controls.Add(Me.txtFolioPrefixField)
        Me.pnlMain.Controls.Add(Me.lblFolioPrefixField)
        Me.pnlMain.Controls.Add(Me.txtFolioPrefix)
        Me.pnlMain.Controls.Add(Me.lblFolioPrefix)
        Me.pnlMain.Controls.Add(Me.lblFolioPrefixSnoFlag)
        Me.pnlMain.Controls.Add(Me.lblFolioPrefixFlag)
        Me.pnlMain.Controls.Add(Me.txtFolioNoFormat)
        Me.pnlMain.Controls.Add(Me.lblfolionoformat)
        Me.pnlMain.Controls.Add(Me.txtIsinId)
        Me.pnlMain.Controls.Add(Me.txtCompanyName)
        Me.pnlMain.Controls.Add(Me.lblisinid)
        Me.pnlMain.Controls.Add(Me.lblCompanyName)
        Me.pnlMain.Controls.Add(Me.txtCompShortCode)
        Me.pnlMain.Controls.Add(Me.lblCompShortCode)
        Me.pnlMain.Controls.Add(Me.txtCompanyCode)
        Me.pnlMain.Controls.Add(Me.lblCompanyCode)
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(9, 11)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(802, 747)
        Me.pnlMain.TabIndex = 1
        '
        'pnlElectronics
        '
        Me.pnlElectronics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlElectronics.Controls.Add(Me.rb_electronics_no)
        Me.pnlElectronics.Controls.Add(Me.rb_electronics_yes)
        Me.pnlElectronics.Location = New System.Drawing.Point(598, 605)
        Me.pnlElectronics.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlElectronics.Name = "pnlElectronics"
        Me.pnlElectronics.Size = New System.Drawing.Size(182, 34)
        Me.pnlElectronics.TabIndex = 175
        '
        'rb_electronics_no
        '
        Me.rb_electronics_no.AutoSize = True
        Me.rb_electronics_no.Checked = True
        Me.rb_electronics_no.Location = New System.Drawing.Point(117, 2)
        Me.rb_electronics_no.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rb_electronics_no.Name = "rb_electronics_no"
        Me.rb_electronics_no.Size = New System.Drawing.Size(59, 25)
        Me.rb_electronics_no.TabIndex = 120
        Me.rb_electronics_no.TabStop = True
        Me.rb_electronics_no.Text = "No"
        Me.rb_electronics_no.UseVisualStyleBackColor = True
        '
        'rb_electronics_yes
        '
        Me.rb_electronics_yes.AutoSize = True
        Me.rb_electronics_yes.Location = New System.Drawing.Point(4, 2)
        Me.rb_electronics_yes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rb_electronics_yes.Name = "rb_electronics_yes"
        Me.rb_electronics_yes.Size = New System.Drawing.Size(65, 25)
        Me.rb_electronics_yes.TabIndex = 119
        Me.rb_electronics_yes.Text = "Yes"
        Me.rb_electronics_yes.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(432, 606)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 23)
        Me.Label1.TabIndex = 174
        Me.Label1.Text = "Electronic RTA"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPanNo
        '
        Me.txtPanNo.Location = New System.Drawing.Point(598, 474)
        Me.txtPanNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPanNo.MaxLength = 64
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.Size = New System.Drawing.Size(181, 27)
        Me.txtPanNo.TabIndex = 173
        '
        'lblPanNo
        '
        Me.lblPanNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPanNo.Location = New System.Drawing.Point(428, 474)
        Me.lblPanNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPanNo.Name = "lblPanNo"
        Me.lblPanNo.Size = New System.Drawing.Size(152, 23)
        Me.lblPanNo.TabIndex = 172
        Me.lblPanNo.Text = "Pan No"
        Me.lblPanNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCinNo
        '
        Me.txtCinNo.Location = New System.Drawing.Point(206, 474)
        Me.txtCinNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCinNo.MaxLength = 64
        Me.txtCinNo.Name = "txtCinNo"
        Me.txtCinNo.Size = New System.Drawing.Size(181, 27)
        Me.txtCinNo.TabIndex = 171
        '
        'lblcin_no
        '
        Me.lblcin_no.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblcin_no.Location = New System.Drawing.Point(33, 474)
        Me.lblcin_no.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblcin_no.Name = "lblcin_no"
        Me.lblcin_no.Size = New System.Drawing.Size(152, 23)
        Me.lblcin_no.TabIndex = 170
        Me.lblcin_no.Text = "CIN No"
        Me.lblcin_no.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPincode
        '
        Me.txtPincode.Location = New System.Drawing.Point(600, 689)
        Me.txtPincode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPincode.MaxLength = 6
        Me.txtPincode.Name = "txtPincode"
        Me.txtPincode.Size = New System.Drawing.Size(181, 27)
        Me.txtPincode.TabIndex = 169
        '
        'lblPincode
        '
        Me.lblPincode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPincode.Location = New System.Drawing.Point(429, 689)
        Me.lblPincode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPincode.Name = "lblPincode"
        Me.lblPincode.Size = New System.Drawing.Size(152, 23)
        Me.lblPincode.TabIndex = 168
        Me.lblPincode.Text = "Pincode"
        Me.lblPincode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(206, 689)
        Me.txtCountry.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCountry.MaxLength = 64
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(181, 27)
        Me.txtCountry.TabIndex = 167
        '
        'lblCountry
        '
        Me.lblCountry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCountry.Location = New System.Drawing.Point(34, 689)
        Me.lblCountry.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(152, 23)
        Me.lblCountry.TabIndex = 166
        Me.lblCountry.Text = "Country"
        Me.lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(600, 648)
        Me.txtState.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtState.MaxLength = 64
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(181, 27)
        Me.txtState.TabIndex = 165
        '
        'lblState
        '
        Me.lblState.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblState.Location = New System.Drawing.Point(429, 648)
        Me.lblState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(152, 23)
        Me.lblState.TabIndex = 164
        Me.lblState.Text = "State"
        Me.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(206, 648)
        Me.txtCity.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCity.MaxLength = 64
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(181, 27)
        Me.txtCity.TabIndex = 163
        '
        'lblCity
        '
        Me.lblCity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCity.Location = New System.Drawing.Point(34, 648)
        Me.lblCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(152, 23)
        Me.lblCity.TabIndex = 162
        Me.lblCity.Text = "City"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAddress3
        '
        Me.txtAddress3.Location = New System.Drawing.Point(206, 606)
        Me.txtAddress3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAddress3.MaxLength = 256
        Me.txtAddress3.Multiline = True
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(181, 30)
        Me.txtAddress3.TabIndex = 161
        '
        'lblAddress3
        '
        Me.lblAddress3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddress3.Location = New System.Drawing.Point(34, 609)
        Me.lblAddress3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAddress3.Name = "lblAddress3"
        Me.lblAddress3.Size = New System.Drawing.Size(152, 23)
        Me.lblAddress3.TabIndex = 160
        Me.lblAddress3.Text = "Address3"
        Me.lblAddress3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAddress2
        '
        Me.txtAddress2.Location = New System.Drawing.Point(206, 562)
        Me.txtAddress2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAddress2.MaxLength = 256
        Me.txtAddress2.Multiline = True
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(576, 30)
        Me.txtAddress2.TabIndex = 159
        '
        'lblAddress2
        '
        Me.lblAddress2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddress2.Location = New System.Drawing.Point(34, 565)
        Me.lblAddress2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAddress2.Name = "lblAddress2"
        Me.lblAddress2.Size = New System.Drawing.Size(152, 23)
        Me.lblAddress2.TabIndex = 158
        Me.lblAddress2.Text = "Address2"
        Me.lblAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAddress1
        '
        Me.txtAddress1.Location = New System.Drawing.Point(206, 520)
        Me.txtAddress1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAddress1.MaxLength = 256
        Me.txtAddress1.Multiline = True
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(576, 30)
        Me.txtAddress1.TabIndex = 157
        '
        'lblAddress1
        '
        Me.lblAddress1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddress1.Location = New System.Drawing.Point(34, 523)
        Me.lblAddress1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(152, 23)
        Me.lblAddress1.TabIndex = 156
        Me.lblAddress1.Text = "Address1"
        Me.lblAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPaidupValue
        '
        Me.txtPaidupValue.Location = New System.Drawing.Point(600, 432)
        Me.txtPaidupValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPaidupValue.MaxLength = 64
        Me.txtPaidupValue.Name = "txtPaidupValue"
        Me.txtPaidupValue.Size = New System.Drawing.Size(181, 27)
        Me.txtPaidupValue.TabIndex = 155
        '
        'lblPaidupValue
        '
        Me.lblPaidupValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaidupValue.Location = New System.Drawing.Point(429, 432)
        Me.lblPaidupValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPaidupValue.Name = "lblPaidupValue"
        Me.lblPaidupValue.Size = New System.Drawing.Size(152, 23)
        Me.lblPaidupValue.TabIndex = 154
        Me.lblPaidupValue.Text = "Paid up Value"
        Me.lblPaidupValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShareQty
        '
        Me.txtShareQty.Location = New System.Drawing.Point(206, 432)
        Me.txtShareQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtShareQty.MaxLength = 64
        Me.txtShareQty.Name = "txtShareQty"
        Me.txtShareQty.Size = New System.Drawing.Size(181, 27)
        Me.txtShareQty.TabIndex = 153
        '
        'lblShareQty
        '
        Me.lblShareQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareQty.Location = New System.Drawing.Point(34, 432)
        Me.lblShareQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShareQty.Name = "lblShareQty"
        Me.lblShareQty.Size = New System.Drawing.Size(152, 23)
        Me.lblShareQty.TabIndex = 152
        Me.lblShareQty.Text = "Share Quantity"
        Me.lblShareQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSecurityType
        '
        Me.txtSecurityType.Location = New System.Drawing.Point(600, 391)
        Me.txtSecurityType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSecurityType.MaxLength = 64
        Me.txtSecurityType.Name = "txtSecurityType"
        Me.txtSecurityType.Size = New System.Drawing.Size(181, 27)
        Me.txtSecurityType.TabIndex = 151
        '
        'lblSecurityType
        '
        Me.lblSecurityType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSecurityType.Location = New System.Drawing.Point(429, 391)
        Me.lblSecurityType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSecurityType.Name = "lblSecurityType"
        Me.lblSecurityType.Size = New System.Drawing.Size(152, 23)
        Me.lblSecurityType.TabIndex = 150
        Me.lblSecurityType.Text = "Security Type"
        Me.lblSecurityType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPrefixsno
        '
        Me.pnlPrefixsno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrefixsno.Controls.Add(Me.RbdprefixSnoNo)
        Me.pnlPrefixsno.Controls.Add(Me.RbdprefixSnoYes)
        Me.pnlPrefixsno.Location = New System.Drawing.Point(600, 135)
        Me.pnlPrefixsno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPrefixsno.Name = "pnlPrefixsno"
        Me.pnlPrefixsno.Size = New System.Drawing.Size(182, 34)
        Me.pnlPrefixsno.TabIndex = 129
        '
        'RbdprefixSnoNo
        '
        Me.RbdprefixSnoNo.AutoSize = True
        Me.RbdprefixSnoNo.Location = New System.Drawing.Point(117, 5)
        Me.RbdprefixSnoNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdprefixSnoNo.Name = "RbdprefixSnoNo"
        Me.RbdprefixSnoNo.Size = New System.Drawing.Size(59, 25)
        Me.RbdprefixSnoNo.TabIndex = 122
        Me.RbdprefixSnoNo.Text = "No"
        Me.RbdprefixSnoNo.UseVisualStyleBackColor = True
        '
        'RbdprefixSnoYes
        '
        Me.RbdprefixSnoYes.AutoSize = True
        Me.RbdprefixSnoYes.Checked = True
        Me.RbdprefixSnoYes.Location = New System.Drawing.Point(4, 2)
        Me.RbdprefixSnoYes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdprefixSnoYes.Name = "RbdprefixSnoYes"
        Me.RbdprefixSnoYes.Size = New System.Drawing.Size(65, 25)
        Me.RbdprefixSnoYes.TabIndex = 121
        Me.RbdprefixSnoYes.TabStop = True
        Me.RbdprefixSnoYes.Text = "Yes"
        Me.RbdprefixSnoYes.UseVisualStyleBackColor = True
        '
        'pnlPrefix
        '
        Me.pnlPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrefix.Controls.Add(Me.RbdprefixNo)
        Me.pnlPrefix.Controls.Add(Me.RbdprefixYes)
        Me.pnlPrefix.Location = New System.Drawing.Point(206, 137)
        Me.pnlPrefix.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPrefix.Name = "pnlPrefix"
        Me.pnlPrefix.Size = New System.Drawing.Size(182, 34)
        Me.pnlPrefix.TabIndex = 127
        '
        'RbdprefixNo
        '
        Me.RbdprefixNo.AutoSize = True
        Me.RbdprefixNo.Location = New System.Drawing.Point(117, 2)
        Me.RbdprefixNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdprefixNo.Name = "RbdprefixNo"
        Me.RbdprefixNo.Size = New System.Drawing.Size(59, 25)
        Me.RbdprefixNo.TabIndex = 120
        Me.RbdprefixNo.TabStop = True
        Me.RbdprefixNo.Text = "No"
        Me.RbdprefixNo.UseVisualStyleBackColor = True
        '
        'RbdprefixYes
        '
        Me.RbdprefixYes.AutoSize = True
        Me.RbdprefixYes.Checked = True
        Me.RbdprefixYes.Location = New System.Drawing.Point(4, 2)
        Me.RbdprefixYes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdprefixYes.Name = "RbdprefixYes"
        Me.RbdprefixYes.Size = New System.Drawing.Size(65, 25)
        Me.RbdprefixYes.TabIndex = 119
        Me.RbdprefixYes.TabStop = True
        Me.RbdprefixYes.Text = "Yes"
        Me.RbdprefixYes.UseVisualStyleBackColor = True
        '
        'pnlComp
        '
        Me.pnlComp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlComp.Controls.Add(Me.RbdCompYes)
        Me.pnlComp.Controls.Add(Me.RbdCompNo)
        Me.pnlComp.Location = New System.Drawing.Point(206, 346)
        Me.pnlComp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlComp.Name = "pnlComp"
        Me.pnlComp.Size = New System.Drawing.Size(182, 34)
        Me.pnlComp.TabIndex = 126
        '
        'RbdCompYes
        '
        Me.RbdCompYes.AutoSize = True
        Me.RbdCompYes.Checked = True
        Me.RbdCompYes.Location = New System.Drawing.Point(9, 5)
        Me.RbdCompYes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdCompYes.Name = "RbdCompYes"
        Me.RbdCompYes.Size = New System.Drawing.Size(65, 25)
        Me.RbdCompYes.TabIndex = 123
        Me.RbdCompYes.TabStop = True
        Me.RbdCompYes.Text = "Yes"
        Me.RbdCompYes.UseVisualStyleBackColor = True
        '
        'RbdCompNo
        '
        Me.RbdCompNo.AutoSize = True
        Me.RbdCompNo.Location = New System.Drawing.Point(117, 5)
        Me.RbdCompNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdCompNo.Name = "RbdCompNo"
        Me.RbdCompNo.Size = New System.Drawing.Size(59, 25)
        Me.RbdCompNo.TabIndex = 124
        Me.RbdCompNo.Text = "No"
        Me.RbdCompNo.UseVisualStyleBackColor = True
        '
        'pnlAct1
        '
        Me.pnlAct1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAct1.Controls.Add(Me.Rbdactiveyes)
        Me.pnlAct1.Controls.Add(Me.RbdActiveNo)
        Me.pnlAct1.Location = New System.Drawing.Point(600, 346)
        Me.pnlAct1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlAct1.Name = "pnlAct1"
        Me.pnlAct1.Size = New System.Drawing.Size(182, 34)
        Me.pnlAct1.TabIndex = 125
        '
        'Rbdactiveyes
        '
        Me.Rbdactiveyes.AutoSize = True
        Me.Rbdactiveyes.Checked = True
        Me.Rbdactiveyes.Location = New System.Drawing.Point(20, 5)
        Me.Rbdactiveyes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Rbdactiveyes.Name = "Rbdactiveyes"
        Me.Rbdactiveyes.Size = New System.Drawing.Size(65, 25)
        Me.Rbdactiveyes.TabIndex = 11
        Me.Rbdactiveyes.TabStop = True
        Me.Rbdactiveyes.Text = "Yes"
        Me.Rbdactiveyes.UseVisualStyleBackColor = True
        '
        'RbdActiveNo
        '
        Me.RbdActiveNo.AutoSize = True
        Me.RbdActiveNo.Location = New System.Drawing.Point(120, 3)
        Me.RbdActiveNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbdActiveNo.Name = "RbdActiveNo"
        Me.RbdActiveNo.Size = New System.Drawing.Size(59, 25)
        Me.RbdActiveNo.TabIndex = 12
        Me.RbdActiveNo.Text = "No"
        Me.RbdActiveNo.UseVisualStyleBackColor = True
        '
        'lblActiveFlag
        '
        Me.lblActiveFlag.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblActiveFlag.Location = New System.Drawing.Point(462, 354)
        Me.lblActiveFlag.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblActiveFlag.Name = "lblActiveFlag"
        Me.lblActiveFlag.Size = New System.Drawing.Size(116, 20)
        Me.lblActiveFlag.TabIndex = 118
        Me.lblActiveFlag.Text = "Active Flag"
        Me.lblActiveFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtid
        '
        Me.txtid.Location = New System.Drawing.Point(4, 6)
        Me.txtid.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtid.Name = "txtid"
        Me.txtid.Size = New System.Drawing.Size(37, 27)
        Me.txtid.TabIndex = 117
        Me.txtid.Visible = False
        '
        'txtShareCapital
        '
        Me.txtShareCapital.Location = New System.Drawing.Point(206, 391)
        Me.txtShareCapital.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtShareCapital.MaxLength = 64
        Me.txtShareCapital.Name = "txtShareCapital"
        Me.txtShareCapital.Size = New System.Drawing.Size(181, 27)
        Me.txtShareCapital.TabIndex = 116
        '
        'lblShareCapital
        '
        Me.lblShareCapital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareCapital.Location = New System.Drawing.Point(34, 391)
        Me.lblShareCapital.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShareCapital.Name = "lblShareCapital"
        Me.lblShareCapital.Size = New System.Drawing.Size(152, 23)
        Me.lblShareCapital.TabIndex = 115
        Me.lblShareCapital.Text = "Share Capital"
        Me.lblShareCapital.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompList
        '
        Me.lblCompList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompList.Location = New System.Drawing.Point(39, 349)
        Me.lblCompList.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompList.Name = "lblCompList"
        Me.lblCompList.Size = New System.Drawing.Size(147, 23)
        Me.lblCompList.TabIndex = 111
        Me.lblCompList.Text = "Company Listed"
        Me.lblCompList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardSlno
        '
        Me.txtInwardSlno.Location = New System.Drawing.Point(600, 305)
        Me.txtInwardSlno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtInwardSlno.MaxLength = 64
        Me.txtInwardSlno.Name = "txtInwardSlno"
        Me.txtInwardSlno.Size = New System.Drawing.Size(181, 27)
        Me.txtInwardSlno.TabIndex = 105
        '
        'lblInwardSlno
        '
        Me.lblInwardSlno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInwardSlno.Location = New System.Drawing.Point(410, 309)
        Me.lblInwardSlno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblInwardSlno.Name = "lblInwardSlno"
        Me.lblInwardSlno.Size = New System.Drawing.Size(168, 20)
        Me.lblInwardSlno.TabIndex = 106
        Me.lblInwardSlno.Text = "Inward Slno"
        Me.lblInwardSlno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblInwardSlno.UseWaitCursor = True
        '
        'txtObjxSlno
        '
        Me.txtObjxSlno.Location = New System.Drawing.Point(206, 305)
        Me.txtObjxSlno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtObjxSlno.MaxLength = 64
        Me.txtObjxSlno.Name = "txtObjxSlno"
        Me.txtObjxSlno.Size = New System.Drawing.Size(181, 27)
        Me.txtObjxSlno.TabIndex = 104
        '
        'lblObjxSlno
        '
        Me.lblObjxSlno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblObjxSlno.Location = New System.Drawing.Point(39, 305)
        Me.lblObjxSlno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblObjxSlno.Name = "lblObjxSlno"
        Me.lblObjxSlno.Size = New System.Drawing.Size(147, 23)
        Me.lblObjxSlno.TabIndex = 103
        Me.lblObjxSlno.Text = "Objx Slno"
        Me.lblObjxSlno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCertSlno
        '
        Me.txtCertSlno.Location = New System.Drawing.Point(600, 263)
        Me.txtCertSlno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCertSlno.MaxLength = 64
        Me.txtCertSlno.Name = "txtCertSlno"
        Me.txtCertSlno.Size = New System.Drawing.Size(181, 27)
        Me.txtCertSlno.TabIndex = 101
        '
        'lblCertSlno
        '
        Me.lblCertSlno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCertSlno.Location = New System.Drawing.Point(410, 268)
        Me.lblCertSlno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCertSlno.Name = "lblCertSlno"
        Me.lblCertSlno.Size = New System.Drawing.Size(168, 20)
        Me.lblCertSlno.TabIndex = 102
        Me.lblCertSlno.Text = "Certificate Slno"
        Me.lblCertSlno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCertSlno.UseWaitCursor = True
        '
        'txtFolioTranSlno
        '
        Me.txtFolioTranSlno.Location = New System.Drawing.Point(206, 263)
        Me.txtFolioTranSlno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolioTranSlno.MaxLength = 64
        Me.txtFolioTranSlno.Name = "txtFolioTranSlno"
        Me.txtFolioTranSlno.Size = New System.Drawing.Size(181, 27)
        Me.txtFolioTranSlno.TabIndex = 100
        '
        'lblFolioTransferSlno
        '
        Me.lblFolioTransferSlno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFolioTransferSlno.Location = New System.Drawing.Point(10, 263)
        Me.lblFolioTransferSlno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFolioTransferSlno.Name = "lblFolioTransferSlno"
        Me.lblFolioTransferSlno.Size = New System.Drawing.Size(176, 23)
        Me.lblFolioTransferSlno.TabIndex = 99
        Me.lblFolioTransferSlno.Text = "Folio Transfer Slno"
        Me.lblFolioTransferSlno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioSlno
        '
        Me.txtFolioSlno.Location = New System.Drawing.Point(600, 222)
        Me.txtFolioSlno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolioSlno.MaxLength = 64
        Me.txtFolioSlno.Name = "txtFolioSlno"
        Me.txtFolioSlno.Size = New System.Drawing.Size(181, 27)
        Me.txtFolioSlno.TabIndex = 97
        '
        'lblFolioSlno
        '
        Me.lblFolioSlno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFolioSlno.Location = New System.Drawing.Point(410, 226)
        Me.lblFolioSlno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFolioSlno.Name = "lblFolioSlno"
        Me.lblFolioSlno.Size = New System.Drawing.Size(168, 20)
        Me.lblFolioSlno.TabIndex = 98
        Me.lblFolioSlno.Text = "Folio Slno"
        Me.lblFolioSlno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFolioSlno.UseWaitCursor = True
        '
        'txtUploadSlno
        '
        Me.txtUploadSlno.Location = New System.Drawing.Point(206, 222)
        Me.txtUploadSlno.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUploadSlno.MaxLength = 64
        Me.txtUploadSlno.Name = "txtUploadSlno"
        Me.txtUploadSlno.Size = New System.Drawing.Size(181, 27)
        Me.txtUploadSlno.TabIndex = 96
        '
        'lblUploadSlno
        '
        Me.lblUploadSlno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUploadSlno.Location = New System.Drawing.Point(39, 222)
        Me.lblUploadSlno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUploadSlno.Name = "lblUploadSlno"
        Me.lblUploadSlno.Size = New System.Drawing.Size(147, 23)
        Me.lblUploadSlno.TabIndex = 95
        Me.lblUploadSlno.Text = "Upload Slno"
        Me.lblUploadSlno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioPrefixField
        '
        Me.txtFolioPrefixField.Location = New System.Drawing.Point(600, 180)
        Me.txtFolioPrefixField.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolioPrefixField.MaxLength = 64
        Me.txtFolioPrefixField.Name = "txtFolioPrefixField"
        Me.txtFolioPrefixField.Size = New System.Drawing.Size(181, 27)
        Me.txtFolioPrefixField.TabIndex = 93
        '
        'lblFolioPrefixField
        '
        Me.lblFolioPrefixField.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFolioPrefixField.Location = New System.Drawing.Point(410, 185)
        Me.lblFolioPrefixField.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFolioPrefixField.Name = "lblFolioPrefixField"
        Me.lblFolioPrefixField.Size = New System.Drawing.Size(168, 20)
        Me.lblFolioPrefixField.TabIndex = 94
        Me.lblFolioPrefixField.Text = "Folio Prefix Field"
        Me.lblFolioPrefixField.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFolioPrefixField.UseWaitCursor = True
        '
        'txtFolioPrefix
        '
        Me.txtFolioPrefix.Location = New System.Drawing.Point(206, 180)
        Me.txtFolioPrefix.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolioPrefix.MaxLength = 64
        Me.txtFolioPrefix.Name = "txtFolioPrefix"
        Me.txtFolioPrefix.Size = New System.Drawing.Size(181, 27)
        Me.txtFolioPrefix.TabIndex = 92
        '
        'lblFolioPrefix
        '
        Me.lblFolioPrefix.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFolioPrefix.Location = New System.Drawing.Point(39, 180)
        Me.lblFolioPrefix.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFolioPrefix.Name = "lblFolioPrefix"
        Me.lblFolioPrefix.Size = New System.Drawing.Size(147, 23)
        Me.lblFolioPrefix.TabIndex = 91
        Me.lblFolioPrefix.Text = "Folio Prefix"
        Me.lblFolioPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFolioPrefixSnoFlag
        '
        Me.lblFolioPrefixSnoFlag.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFolioPrefixSnoFlag.Location = New System.Drawing.Point(384, 143)
        Me.lblFolioPrefixSnoFlag.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFolioPrefixSnoFlag.Name = "lblFolioPrefixSnoFlag"
        Me.lblFolioPrefixSnoFlag.Size = New System.Drawing.Size(194, 20)
        Me.lblFolioPrefixSnoFlag.TabIndex = 88
        Me.lblFolioPrefixSnoFlag.Text = "Folio Prefix Sno Flag"
        Me.lblFolioPrefixSnoFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFolioPrefixSnoFlag.UseWaitCursor = True
        '
        'lblFolioPrefixFlag
        '
        Me.lblFolioPrefixFlag.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFolioPrefixFlag.Location = New System.Drawing.Point(39, 138)
        Me.lblFolioPrefixFlag.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFolioPrefixFlag.Name = "lblFolioPrefixFlag"
        Me.lblFolioPrefixFlag.Size = New System.Drawing.Size(147, 23)
        Me.lblFolioPrefixFlag.TabIndex = 85
        Me.lblFolioPrefixFlag.Text = "Folio Prefix Flag"
        Me.lblFolioPrefixFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolioNoFormat
        '
        Me.txtFolioNoFormat.Location = New System.Drawing.Point(600, 97)
        Me.txtFolioNoFormat.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolioNoFormat.MaxLength = 64
        Me.txtFolioNoFormat.Name = "txtFolioNoFormat"
        Me.txtFolioNoFormat.Size = New System.Drawing.Size(181, 27)
        Me.txtFolioNoFormat.TabIndex = 83
        '
        'lblfolionoformat
        '
        Me.lblfolionoformat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblfolionoformat.Location = New System.Drawing.Point(410, 102)
        Me.lblfolionoformat.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblfolionoformat.Name = "lblfolionoformat"
        Me.lblfolionoformat.Size = New System.Drawing.Size(168, 20)
        Me.lblfolionoformat.TabIndex = 84
        Me.lblfolionoformat.Text = "Folio No Format"
        Me.lblfolionoformat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblfolionoformat.UseWaitCursor = True
        '
        'txtIsinId
        '
        Me.txtIsinId.Location = New System.Drawing.Point(206, 97)
        Me.txtIsinId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIsinId.MaxLength = 64
        Me.txtIsinId.Name = "txtIsinId"
        Me.txtIsinId.Size = New System.Drawing.Size(181, 27)
        Me.txtIsinId.TabIndex = 82
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(206, 55)
        Me.txtCompanyName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCompanyName.MaxLength = 125
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(576, 27)
        Me.txtCompanyName.TabIndex = 81
        '
        'lblisinid
        '
        Me.lblisinid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblisinid.Location = New System.Drawing.Point(39, 97)
        Me.lblisinid.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblisinid.Name = "lblisinid"
        Me.lblisinid.Size = New System.Drawing.Size(147, 23)
        Me.lblisinid.TabIndex = 80
        Me.lblisinid.Text = "Isin Id"
        Me.lblisinid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompanyName
        '
        Me.lblCompanyName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompanyName.Location = New System.Drawing.Point(34, 55)
        Me.lblCompanyName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(152, 23)
        Me.lblCompanyName.TabIndex = 78
        Me.lblCompanyName.Text = "Company Name"
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCompShortCode
        '
        Me.txtCompShortCode.Location = New System.Drawing.Point(600, 14)
        Me.txtCompShortCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCompShortCode.MaxLength = 64
        Me.txtCompShortCode.Name = "txtCompShortCode"
        Me.txtCompShortCode.Size = New System.Drawing.Size(181, 27)
        Me.txtCompShortCode.TabIndex = 1
        '
        'lblCompShortCode
        '
        Me.lblCompShortCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompShortCode.Location = New System.Drawing.Point(410, 18)
        Me.lblCompShortCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompShortCode.Name = "lblCompShortCode"
        Me.lblCompShortCode.Size = New System.Drawing.Size(168, 20)
        Me.lblCompShortCode.TabIndex = 68
        Me.lblCompShortCode.Text = "Comp Short Code"
        Me.lblCompShortCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCompanyCode
        '
        Me.txtCompanyCode.Location = New System.Drawing.Point(206, 14)
        Me.txtCompanyCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCompanyCode.MaxLength = 8
        Me.txtCompanyCode.Name = "txtCompanyCode"
        Me.txtCompanyCode.Size = New System.Drawing.Size(181, 27)
        Me.txtCompanyCode.TabIndex = 0
        '
        'lblCompanyCode
        '
        Me.lblCompanyCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompanyCode.Location = New System.Drawing.Point(30, 18)
        Me.lblCompanyCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompanyCode.Name = "lblCompanyCode"
        Me.lblCompanyCode.Size = New System.Drawing.Size(156, 20)
        Me.lblCompanyCode.TabIndex = 66
        Me.lblCompanyCode.Text = "Company Code"
        Me.lblCompanyCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlButtons.Location = New System.Drawing.Point(135, 769)
        Me.pnlButtons.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(579, 43)
        Me.pnlButtons.TabIndex = 11
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(470, 2)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 37)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFind.Location = New System.Drawing.Point(236, 2)
        Me.btnFind.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(108, 37)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Location = New System.Drawing.Point(352, 2)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(108, 37)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEdit.Location = New System.Drawing.Point(118, 2)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(108, 37)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.SystemColors.Control
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Location = New System.Drawing.Point(2, 2)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(108, 37)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'pnlSave
        '
        Me.pnlSave.CausesValidation = False
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSave.Location = New System.Drawing.Point(312, 768)
        Me.pnlSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(228, 43)
        Me.pnlSave.TabIndex = 12
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(120, 2)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(108, 37)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 37)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmCompanyMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 828)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmCompanyMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Company Master"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlElectronics.ResumeLayout(False)
        Me.pnlElectronics.PerformLayout()
        Me.pnlPrefixsno.ResumeLayout(False)
        Me.pnlPrefixsno.PerformLayout()
        Me.pnlPrefix.ResumeLayout(False)
        Me.pnlPrefix.PerformLayout()
        Me.pnlComp.ResumeLayout(False)
        Me.pnlComp.PerformLayout()
        Me.pnlAct1.ResumeLayout(False)
        Me.pnlAct1.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlSave.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblisinid As System.Windows.Forms.Label
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents txtCompShortCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCompShortCode As System.Windows.Forms.Label
    Friend WithEvents txtCompanyCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyCode As System.Windows.Forms.Label
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents txtIsinId As System.Windows.Forms.TextBox
    Friend WithEvents lblFolioPrefixSnoFlag As System.Windows.Forms.Label
    Friend WithEvents lblFolioPrefixFlag As System.Windows.Forms.Label
    Friend WithEvents txtFolioNoFormat As System.Windows.Forms.TextBox
    Friend WithEvents lblfolionoformat As System.Windows.Forms.Label
    Friend WithEvents lblCompList As System.Windows.Forms.Label
    Friend WithEvents txtInwardSlno As System.Windows.Forms.TextBox
    Friend WithEvents lblInwardSlno As System.Windows.Forms.Label
    Friend WithEvents txtObjxSlno As System.Windows.Forms.TextBox
    Friend WithEvents lblObjxSlno As System.Windows.Forms.Label
    Friend WithEvents txtCertSlno As System.Windows.Forms.TextBox
    Friend WithEvents lblCertSlno As System.Windows.Forms.Label
    Friend WithEvents txtFolioTranSlno As System.Windows.Forms.TextBox
    Friend WithEvents lblFolioTransferSlno As System.Windows.Forms.Label
    Friend WithEvents txtFolioSlno As System.Windows.Forms.TextBox
    Friend WithEvents lblFolioSlno As System.Windows.Forms.Label
    Friend WithEvents txtUploadSlno As System.Windows.Forms.TextBox
    Friend WithEvents lblUploadSlno As System.Windows.Forms.Label
    Friend WithEvents txtFolioPrefixField As System.Windows.Forms.TextBox
    Friend WithEvents lblFolioPrefixField As System.Windows.Forms.Label
    Friend WithEvents txtFolioPrefix As System.Windows.Forms.TextBox
    Friend WithEvents lblFolioPrefix As System.Windows.Forms.Label
    Friend WithEvents txtShareCapital As System.Windows.Forms.TextBox
    Friend WithEvents lblShareCapital As System.Windows.Forms.Label
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtid As System.Windows.Forms.TextBox
    Friend WithEvents RbdActiveNo As System.Windows.Forms.RadioButton
    Friend WithEvents Rbdactiveyes As System.Windows.Forms.RadioButton
    Friend WithEvents lblActiveFlag As System.Windows.Forms.Label
    Friend WithEvents RbdCompNo As System.Windows.Forms.RadioButton
    Friend WithEvents RbdCompYes As System.Windows.Forms.RadioButton
    Friend WithEvents RbdprefixSnoNo As System.Windows.Forms.RadioButton
    Friend WithEvents RbdprefixSnoYes As System.Windows.Forms.RadioButton
    Friend WithEvents RbdprefixNo As System.Windows.Forms.RadioButton
    Friend WithEvents RbdprefixYes As System.Windows.Forms.RadioButton
    Friend WithEvents pnlAct1 As System.Windows.Forms.Panel
    Friend WithEvents pnlPrefixsno As System.Windows.Forms.Panel
    Friend WithEvents pnlPrefix As System.Windows.Forms.Panel
    Friend WithEvents pnlComp As System.Windows.Forms.Panel
    Friend WithEvents txtPincode As System.Windows.Forms.TextBox
    Friend WithEvents lblPincode As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents lblCountry As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress3 As System.Windows.Forms.Label
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress2 As System.Windows.Forms.Label
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress1 As System.Windows.Forms.Label
    Friend WithEvents txtPaidupValue As System.Windows.Forms.TextBox
    Friend WithEvents lblPaidupValue As System.Windows.Forms.Label
    Friend WithEvents txtShareQty As System.Windows.Forms.TextBox
    Friend WithEvents lblShareQty As System.Windows.Forms.Label
    Friend WithEvents txtSecurityType As System.Windows.Forms.TextBox
    Friend WithEvents lblSecurityType As System.Windows.Forms.Label
    Friend WithEvents txtPanNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPanNo As System.Windows.Forms.Label
    Friend WithEvents txtCinNo As System.Windows.Forms.TextBox
    Friend WithEvents lblcin_no As System.Windows.Forms.Label
    Friend WithEvents pnlElectronics As System.Windows.Forms.Panel
    Friend WithEvents rb_electronics_no As System.Windows.Forms.RadioButton
    Friend WithEvents rb_electronics_yes As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
