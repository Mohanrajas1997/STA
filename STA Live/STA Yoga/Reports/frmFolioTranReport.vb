Public Class frmFolioTranReport
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(FolioId As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mnFolioId = FolioId
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtInwardNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents txtFolioNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTranCount As System.Windows.Forms.TextBox
    Friend WithEvents txtTranRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtInwardId As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cboMode As System.Windows.Forms.ComboBox
    Friend WithEvents txtTranDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFolioId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFolioTranReport))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtFolioId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTranDesc = New System.Windows.Forms.TextBox()
        Me.cboMode = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtInwardId = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtTranCount = New System.Windows.Forms.TextBox()
        Me.txtTranRemark = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.txtFolioNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.txtInwardNo = New System.Windows.Forms.TextBox()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtRecCount = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.pnlMain.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.txtFolioId)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.txtTranDesc)
        Me.pnlMain.Controls.Add(Me.cboMode)
        Me.pnlMain.Controls.Add(Me.Label16)
        Me.pnlMain.Controls.Add(Me.txtInwardId)
        Me.pnlMain.Controls.Add(Me.Label15)
        Me.pnlMain.Controls.Add(Me.txtTranCount)
        Me.pnlMain.Controls.Add(Me.txtTranRemark)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.Label14)
        Me.pnlMain.Controls.Add(Me.txtCustName)
        Me.pnlMain.Controls.Add(Me.txtFolioNo)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.cboDocType)
        Me.pnlMain.Controls.Add(Me.txtInwardNo)
        Me.pnlMain.Controls.Add(Me.dtpTo)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.dtpFrom)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Location = New System.Drawing.Point(6, 7)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(768, 156)
        Me.pnlMain.TabIndex = 0
        '
        'txtFolioId
        '
        Me.txtFolioId.Location = New System.Drawing.Point(290, 93)
        Me.txtFolioId.MaxLength = 0
        Me.txtFolioId.Name = "txtFolioId"
        Me.txtFolioId.Size = New System.Drawing.Size(105, 21)
        Me.txtFolioId.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(213, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 155
        Me.Label3.Text = "Folio Id"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranDesc
        '
        Me.txtTranDesc.Location = New System.Drawing.Point(470, 66)
        Me.txtTranDesc.MaxLength = 0
        Me.txtTranDesc.Name = "txtTranDesc"
        Me.txtTranDesc.Size = New System.Drawing.Size(283, 21)
        Me.txtTranDesc.TabIndex = 8
        '
        'cboMode
        '
        Me.cboMode.FormattingEnabled = True
        Me.cboMode.Location = New System.Drawing.Point(97, 120)
        Me.cboMode.Name = "cboMode"
        Me.cboMode.Size = New System.Drawing.Size(105, 21)
        Me.cboMode.TabIndex = 12
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(28, 122)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(62, 13)
        Me.Label16.TabIndex = 151
        Me.Label16.Text = "Mode"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInwardId
        '
        Me.txtInwardId.Location = New System.Drawing.Point(97, 93)
        Me.txtInwardId.MaxLength = 0
        Me.txtInwardId.Name = "txtInwardId"
        Me.txtInwardId.Size = New System.Drawing.Size(105, 21)
        Me.txtInwardId.TabIndex = 9
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(20, 95)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 13)
        Me.Label15.TabIndex = 150
        Me.Label15.Text = "Inward Id"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranCount
        '
        Me.txtTranCount.Location = New System.Drawing.Point(290, 66)
        Me.txtTranCount.MaxLength = 0
        Me.txtTranCount.Name = "txtTranCount"
        Me.txtTranCount.Size = New System.Drawing.Size(105, 21)
        Me.txtTranCount.TabIndex = 7
        '
        'txtTranRemark
        '
        Me.txtTranRemark.Location = New System.Drawing.Point(470, 93)
        Me.txtTranRemark.MaxLength = 0
        Me.txtTranRemark.Name = "txtTranRemark"
        Me.txtTranRemark.Size = New System.Drawing.Size(283, 21)
        Me.txtTranRemark.TabIndex = 11
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(383, 95)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 13)
        Me.Label13.TabIndex = 146
        Me.Label13.Text = "Remark"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(204, 68)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 148
        Me.Label14.Text = "Tran Count"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustName
        '
        Me.txtCustName.Location = New System.Drawing.Point(290, 39)
        Me.txtCustName.MaxLength = 0
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(105, 21)
        Me.txtCustName.TabIndex = 4
        '
        'txtFolioNo
        '
        Me.txtFolioNo.Location = New System.Drawing.Point(97, 39)
        Me.txtFolioNo.MaxLength = 0
        Me.txtFolioNo.Name = "txtFolioNo"
        Me.txtFolioNo.Size = New System.Drawing.Size(105, 21)
        Me.txtFolioNo.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(9, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 13)
        Me.Label6.TabIndex = 138
        Me.Label6.Text = "Folio No"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDocType
        '
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Location = New System.Drawing.Point(470, 39)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(283, 21)
        Me.cboDocType.TabIndex = 5
        '
        'txtInwardNo
        '
        Me.txtInwardNo.Location = New System.Drawing.Point(97, 66)
        Me.txtInwardNo.MaxLength = 0
        Me.txtInwardNo.Name = "txtInwardNo"
        Me.txtInwardNo.Size = New System.Drawing.Size(105, 21)
        Me.txtInwardNo.TabIndex = 6
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(290, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpTo.TabIndex = 1
        Me.dtpTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(235, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 17)
        Me.Label11.TabIndex = 115
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
        Me.dtpFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpFrom.TabIndex = 0
        Me.dtpFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(6, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 17)
        Me.Label10.TabIndex = 115
        Me.Label10.Text = "Tran From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(470, 12)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(283, 21)
        Me.cboCompany.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(385, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 17)
        Me.Label5.TabIndex = 113
        Me.Label5.Text = "Company"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(681, 122)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(603, 122)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 14
        Me.btnClear.Text = "C&lear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(525, 122)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 13
        Me.btnRefresh.Text = "&Refresh"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(211, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 140
        Me.Label7.Text = "Cust Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(385, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 131
        Me.Label1.Text = "Doc Type"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(-1, 68)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 13)
        Me.Label12.TabIndex = 117
        Me.Label12.Text = "Inward No"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(383, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 153
        Me.Label2.Text = "Tran Desc"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(6, 316)
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
        Me.btnExport.Location = New System.Drawing.Point(558, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "&Export"
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(6, 169)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(768, 130)
        Me.dgvReport.TabIndex = 1
        '
        'frmFolioTranReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(794, 354)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFolioTranReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Folio Transaction Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Local Declaration"
    Dim mnFolioId As Long
#End Region
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dtpFrom.Checked = False
        dtpTo.Checked = False

        Call frmCtrClear(Me)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsFld As String
        Dim lsCond As String
        Dim i As Integer

        Try
            lsCond = ""

            If mnFolioId > 0 Then lsCond &= " and a.folio_gid = " & mnFolioId & " "

            If (cboCompany.Text = "" Or cboCompany.SelectedIndex = -1) And mnFolioId = 0 Then
                MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboCompany.Focus()
                Exit Sub
            End If

            If dtpFrom.Checked = True Then lsCond &= " and a.tran_date >= '" & Format(CDate(dtpFrom.Value), "yyyy-MM-dd") & "' "
            If dtpTo.Checked = True Then lsCond &= " and a.tran_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond &= " and b.comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            If cboDocType.Text <> "" And cboDocType.SelectedIndex >= 0 Then lsCond &= " and d.tran_code = '" & cboDocType.SelectedValue.ToString & "' "

            If txtInwardNo.Text <> "" Then lsCond &= " and d.inward_no = '" & Val(txtInwardNo.Text) & "' "
            If txtInwardId.Text <> "" Then lsCond &= " and d.inward_gid = '" & Val(txtInwardId.Text) & "' "
            If Val(txtTranCount.Text) > 0 Then lsCond &= " and a.tran_count = " & Val(txtTranCount.Text) & " "
            If txtFolioNo.Text <> "" Then lsCond &= " and b.folio_no = '" & QuoteFilter(txtFolioNo.Text) & "' "
            If txtCustName.Text <> "" Then lsCond &= " and b.holder1_name = '" & QuoteFilter(txtFolioNo.Text) & "' "

            If Val(txtFolioId.Text) > 0 Then lsCond &= " and a.folio_gid = " & Val(txtFolioId.Text) & " "
            If Val(txtInwardId.Text) > 0 Then lsCond &= " and d.inward_gid = " & Val(txtInwardId.Text) & " "

            If txtTranDesc.Text <> "" Then lsCond &= " and a.tran_desc = '" & QuoteFilter(txtTranDesc.Text) & "' "
            If txtTranRemark.Text <> "" Then lsCond &= " and a.tran_remark = '" & QuoteFilter(txtTranRemark.Text) & "' "

            Select Case cboMode.Text.ToString
                Case "DEBIT"
                    lsCond &= " and a.tran_mode = 'D' "
                Case "CREDIT"
                    lsCond &= " and a.tran_mode = 'C' "
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsFld = ""
            lsFld &= " c.comp_name as 'Company',"
            lsFld &= " a.tran_date as 'Tran Date',"
            lsFld &= " b.folio_no as 'Folio No',"
            lsFld &= " b.holder1_name as 'Customer Name',"
            lsFld &= " a.tran_count as 'Tran Count',"
            lsFld &= " a.tran_mode as 'Tran Mode',"
            lsFld &= " a.mult as 'Mult',"
            lsFld &= " a.tran_desc as 'Tran Desc',"
            lsFld &= " a.tran_remark as 'Tran Remark',"
            lsFld &= " e.folio_no as 'Ref Folio No',"
            lsFld &= " e.holder1_name as 'Ref Customer Name',"
            lsFld &= " d.inward_no as 'Inward No',"
            lsFld &= " d.received_date as 'Received Date',"
            lsFld &= " d.tran_code as 'Doc Tran Code',"
            lsFld &= " a.foliotran_gid as 'Folio Tran Id',"
            lsFld &= " a.inward_gid as 'Inward Id',"
            lsFld &= " a.folio_gid as 'Folio Id',"
            lsFld &= " a.ref_folio_gid as 'Ref Folio Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld

            lsSql &= " from sta_trn_tfoliotran as a "
            lsSql &= " inner join sta_trn_tfolio as b on a.folio_gid = b.folio_gid and b.delete_flag = 'N' "
            lsSql &= " inner join sta_mst_tcompany as c on b.comp_gid = c.comp_gid and c.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tinward as d on a.inward_gid = d.inward_gid and d.delete_flag = 'N' "
            lsSql &= " left join sta_trn_tfolio as e on a.ref_folio_gid = e.folio_gid and e.delete_flag = 'N' "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and a.delete_flag = 'N' "
            lsSql &= " order by a.foliotran_gid"

            Call gpPopGridView(dgvReport, lsSql, gOdbcConn)

            For i = 0 To dgvReport.Columns.Count - 1
                dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lsSql As String

        Try
            ' company
            lsSql = ""
            lsSql &= " select comp_gid,comp_name from sta_mst_tcompany "
            lsSql &= " where delete_flag = 'N' "
            lsSql &= " order by comp_name asc "

            Call gpBindCombo(lsSql, "comp_name", "comp_gid", cboCompany, gOdbcConn)

            ' doc type
            lsSql = ""
            lsSql &= " select trantype_code,concat(trantype_code,'-',trantype_desc) as trantype_desc from sta_mst_ttrantype "
            lsSql &= " where transfer_flag = 'Y' and delete_flag = 'N' "
            lsSql &= " order by trantype_code asc "

            Call gpBindCombo(lsSql, "trantype_desc", "trantype_code", cboDocType, gOdbcConn)

            With cboMode
                .Items.Clear()
                .Items.Add("Debit")
                .Items.Add("Credit")
            End With

            dtpFrom.Value = Now
            dtpTo.Value = Now

            btnClear.PerformClick()

            If mnFolioId > 0 Then btnRefresh.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvReport
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvReport.Top + dgvReport.Height + 6
        pnlExport.Width = Me.Width
        btnExport.Left = pnlExport.Width - btnExport.Width - 24
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvReport, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboFileName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDocType.SelectedIndexChanged

    End Sub
End Class
