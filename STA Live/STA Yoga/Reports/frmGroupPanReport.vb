Public Class frmGroupPanReport
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents dtpBenpostTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpBenpostFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtHolder1Pan As System.Windows.Forms.TextBox
    Friend WithEvents lblHolder1Pan As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGroupPanReport))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dtpBenpostTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpBenpostFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.txtRecCount = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.txtHolder1Pan = New System.Windows.Forms.TextBox()
        Me.lblHolder1Pan = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.lblHolder1Pan)
        Me.pnlMain.Controls.Add(Me.txtHolder1Pan)
        Me.pnlMain.Controls.Add(Me.dtpBenpostTo)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.dtpBenpostFrom)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.cboCompany)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Location = New System.Drawing.Point(6, 7)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(768, 67)
        Me.pnlMain.TabIndex = 0
        '
        'dtpBenpostTo
        '
        Me.dtpBenpostTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpBenpostTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBenpostTo.Location = New System.Drawing.Point(269, 7)
        Me.dtpBenpostTo.Name = "dtpBenpostTo"
        Me.dtpBenpostTo.ShowCheckBox = True
        Me.dtpBenpostTo.Size = New System.Drawing.Size(132, 21)
        Me.dtpBenpostTo.TabIndex = 4
        Me.dtpBenpostTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(241, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 17)
        Me.Label3.TabIndex = 135
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpBenpostFrom
        '
        Me.dtpBenpostFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpBenpostFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBenpostFrom.Location = New System.Drawing.Point(103, 7)
        Me.dtpBenpostFrom.Name = "dtpBenpostFrom"
        Me.dtpBenpostFrom.ShowCheckBox = True
        Me.dtpBenpostFrom.Size = New System.Drawing.Size(132, 21)
        Me.dtpBenpostFrom.TabIndex = 3
        Me.dtpBenpostFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(5, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 17)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Benpost From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(476, 7)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(283, 21)
        Me.cboCompany.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(391, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 113
        Me.Label5.Text = "Company"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(687, 34)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 25
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(609, 34)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 24
        Me.btnClear.Text = "C&lear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(531, 34)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 23
        Me.btnRefresh.Text = "&Refresh"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Location = New System.Drawing.Point(12, 457)
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
        Me.dgvReport.Location = New System.Drawing.Point(6, 80)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(768, 371)
        Me.dgvReport.TabIndex = 1
        '
        'txtHolder1Pan
        '
        Me.txtHolder1Pan.Location = New System.Drawing.Point(103, 34)
        Me.txtHolder1Pan.MaxLength = 0
        Me.txtHolder1Pan.Name = "txtHolder1Pan"
        Me.txtHolder1Pan.Size = New System.Drawing.Size(132, 21)
        Me.txtHolder1Pan.TabIndex = 137
        '
        'lblHolder1Pan
        '
        Me.lblHolder1Pan.Location = New System.Drawing.Point(4, 35)
        Me.lblHolder1Pan.Name = "lblHolder1Pan"
        Me.lblHolder1Pan.Size = New System.Drawing.Size(91, 17)
        Me.lblHolder1Pan.TabIndex = 138
        Me.lblHolder1Pan.Text = "Holder1 Pan"
        Me.lblHolder1Pan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmGroupPanReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(787, 502)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGroupPanReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Group Pan Report"
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
#End Region
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False

        Call LoadDataNew()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String
        Dim lsCond1 As String
        Dim i As Integer

        Try

            If dtpBenpostFrom.Checked = False Then
                MessageBox.Show("Please select the BenpostFrom !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpBenpostFrom.Focus()
                Exit Sub
            End If

            If dtpBenpostTo.Checked = False Then
                MessageBox.Show("Please select the BenpostTo !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpBenpostTo.Focus()
                Exit Sub
            End If

            If cboCompany.Text = "" Then
                MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboCompany.Focus()
                Exit Sub
            End If

            lsCond = ""
            lsCond1 = ""

            If dtpBenpostFrom.Checked = True Then lsCond &= " and benpost_date >= '" & Format(CDate(dtpBenpostFrom.Value), "yyyy-MM-dd") & "' "
            If dtpBenpostTo.Checked = True Then lsCond &= " and benpost_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpBenpostTo.Value), "yyyy-MM-dd") & "' "

            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond &= " and comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            If txtHolder1Pan.Text <> "" Then lsCond &= " and holder1_pan = '" & txtHolder1Pan.Text.ToString() & "' "
            If txtHolder1Pan.Text <> "" Then lsCond1 &= " and holder1_pan_no = '" & txtHolder1Pan.Text.ToString() & "' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "
            If lsCond1 = "" Then lsCond1 &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select c.holder1_pan as 'Holder Pan',"
            lsSql &= " c.holder1_name as 'Holder Name',"
            lsSql &= " cdsl,"
            lsSql &= " nsdl,"
            lsSql &= " physical,(cdsl+nsdl+physical) as total from (select"
            lsSql &= " holder1_pan,"
            lsSql &= " holder1_name,"
            lsSql &= " '0' as 'cdsl',"
            lsSql &= " share_count as 'nsdl',"
            lsSql &= " '0' as 'physical'"
            lsSql &= " from sta_trn_tbenpost"
            lsSql &= " where depository_code = 'N'"
            lsSql &= " and (holder1_pan <> null or holder1_pan <> '')"
            lsSql &= " and share_count > 0"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= lsCond
            lsSql &= " group by holder1_pan"
            lsSql &= " union"
            lsSql &= " select holder1_pan,"
            lsSql &= " holder1_name,"
            lsSql &= " share_count as 'cdsl',"
            lsSql &= " '0' as 'nsdl',"
            lsSql &= " '0' as 'physical'"
            lsSql &= " from sta_trn_tbenpost"
            lsSql &= " where depository_code = 'C'"
            lsSql &= " and (holder1_pan <> null or holder1_pan <> '')"
            lsSql &= " and share_count > 0"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= lsCond
            lsSql &= " group by holder1_pan"
            lsSql &= " union"
            lsSql &= " select holder1_pan_no,"
            lsSql &= " holder1_name,"
            lsSql &= " '0' as 'cdsl',"
            lsSql &= " '0'  as 'nsdl',"
            lsSql &= " folio_shares as 'physical'"
            lsSql &= " from sta_trn_tfolio"
            lsSql &= " where "
            lsSql &= " (holder1_pan_no <> null or holder1_pan_no <> '' )"
            lsSql &= " and folio_shares > 0"
            lsSql &= " and delete_flag = 'N'"
            lsSql &= " and comp_gid = " & Val(cboCompany.SelectedValue.ToString())
            lsSql &= lsCond1
            lsSql &= " group by holder1_pan_no) as c"

            Call gpPopGridView(dgvReport, lsSql, gOdbcConn)

            For i = 0 To dgvReport.Columns.Count - 1
                dgvReport.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            txtRecCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadDataNew()
        Dim lsSql As String
        Dim lsCond As String
        Dim lsCond1 As String
        Dim i As Integer

        Try

            If dtpBenpostFrom.Checked = False Then
                MessageBox.Show("Please select the BenpostFrom !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpBenpostFrom.Focus()
                Exit Sub
            End If

            If dtpBenpostTo.Checked = False Then
                MessageBox.Show("Please select the BenpostTo !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpBenpostTo.Focus()
                Exit Sub
            End If

            If cboCompany.Text = "" Then
                MessageBox.Show("Please select the company !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboCompany.Focus()
                Exit Sub
            End If

            lsCond = ""
            lsCond1 = ""

            If dtpBenpostFrom.Checked = True Then lsCond &= " and a.benpost_date >= '" & Format(CDate(dtpBenpostFrom.Value), "yyyy-MM-dd") & "' "
            If dtpBenpostTo.Checked = True Then lsCond &= " and a.benpost_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpBenpostTo.Value), "yyyy-MM-dd") & "' "

            If cboCompany.Text <> "" And cboCompany.SelectedIndex >= 0 Then lsCond &= " and a.comp_gid = '" & Val(cboCompany.SelectedValue.ToString) & "' "
            If txtHolder1Pan.Text <> "" Then lsCond &= " and a.holder1_pan = '" & txtHolder1Pan.Text.ToString() & "' "
            If txtHolder1Pan.Text <> "" Then lsCond1 &= " and a.holder1_pan_no = '" & txtHolder1Pan.Text.ToString() & "' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "
            If lsCond1 = "" Then lsCond1 &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select c.comp_name,c.isin_id,c.depository,c.benpost_date,c.dp_id,c.client_id,c.holder1_name as 'Holder Name1',"
            lsSql &= " c.holder2_name as 'Holder Name2',c.holder3_name as 'Holder Name3',(cdsl+nsdl+physical) as share_count,"
            lsSql &= " c.lockin,c.pledge,c.holder1_fh_name,c.holder2_fh_name,c.holder3_fh_name,c.holder1_pan as 'Holder Pan1',"
            lsSql &= " c.holder2_pan as 'Holder Pan2',c.holder3_pan as 'Holder Pan3',c.holder1_email_id, c.holder2_email_id, c.holder3_email_id, c.holder1_addr1,"
            lsSql &= " c.holder1_addr2,c.holder1_addr3,c.city,c.state,c.country,c.pincode,c.phone_no,c.fax_no,c.holder1_per_addr1,c.holder1_per_addr2,"
            lsSql &= " c.holder1_per_addr3,c.holder1_per_city,c.holder1_per_state,c.holder1_per_country,c.holder1_per_pin,c.nominee_name,c.nominee_part1,"
            lsSql &= " c.nominee_part2,c.nominee_part3,c.nominee_part4,c.nominee_part5,c.bank_name, c.bank_addr1, c.bank_addr2, c.bank_addr3, c.bank_city,"
            lsSql &= " c.bank_state,c.bank_country,c.bank_pin,c.bank_acc_no,c.bank_acc_type,c.bank_micr_code,c.bank_ifsc_code,c.rbi_ref_no,c.rbi_app_date,"
            lsSql &= " c.bene_type,c.Bene_Category,c.bene_subtype,c.Bene_Sub_Category,c.Bene_AC_Category, c.Bene_Occupation"
            lsSql &= " from (select"
            lsSql &= " b.comp_name,b.isin_id,'NSDL' as depository, a.benpost_date,a.dp_id,a.client_id,"
            lsSql &= " a.holder1_name,a.holder2_name,a.holder3_name,a.lockin,a.pledge,a.holder1_fh_name,a.holder2_fh_name,a.holder3_fh_name,"
            lsSql &= " a.holder1_pan,a.holder2_pan,a.holder3_pan,a.holder1_email_id,a.holder2_email_id,a.holder3_email_id,"
            lsSql &= " a.holder1_addr1,a.holder1_addr2,a.holder1_addr3,a.holder1_city  as 'city',a.holder1_state as 'state',a.holder1_country as 'country',a.holder1_pin as 'pincode',"
            lsSql &= " a.holder1_contact_no as 'phone_no', a.holder1_fax_no as 'fax_no',a.holder1_per_addr1,a.holder1_per_addr2,a.holder1_per_addr3,a.holder1_per_city,"
            lsSql &= " a.holder1_per_state,a.holder1_per_country,a.holder1_per_pin,a.nominee_name,a.nominee_part1,a.nominee_part2,a.nominee_part3,a.nominee_part4,a.nominee_part5,"
            lsSql &= " a.bank_name,a.bank_addr1,a.bank_addr2,a.bank_addr3,a.bank_city,a.bank_state,a.bank_country,a.bank_pin,"
            lsSql &= " a.bank_acc_no,a.bank_acc_type,a.bank_micr_code,a.bank_ifsc_code,a.rbi_ref_no,a.rbi_app_date,a.bene_type,'' as Bene_Category,"
            lsSql &= " a.bene_subtype,'' as Bene_Sub_Category,'' as Bene_AC_Category, '' as Bene_Occupation,"
            lsSql &= " '0' as 'cdsl', a.share_count as 'nsdl', '0' as 'physical' "
            lsSql &= " from sta_trn_tbenpost as a"
            lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N'"
            lsSql &= " where a.depository_code = 'N'"
            lsSql &= " and (a.holder1_pan <> null or a.holder1_pan <> '')"
            lsSql &= " and a.share_count > 0"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= lsCond
            lsSql &= " group by a.holder1_pan"
            lsSql &= " union"
            lsSql &= " select "
            lsSql &= " b.comp_name,b.isin_id,'CDSL' as depository, a.benpost_date,a.dp_id,a.client_id,"
            lsSql &= " a.holder1_name,a.holder2_name,a.holder3_name,a.lockin,a.pledge,a.holder1_fh_name,a.holder2_fh_name,a.holder3_fh_name,"
            lsSql &= " a.holder1_pan,a.holder2_pan,a.holder3_pan,a.holder1_email_id,a.holder2_email_id,a.holder3_email_id,"
            lsSql &= " a.holder1_addr1,a.holder1_addr2,a.holder1_addr3,a.holder1_city  as 'city',a.holder1_state as 'state',a.holder1_country as 'country',a.holder1_pin as 'pincode',"
            lsSql &= " a.holder1_contact_no as 'phone_no', a.holder1_fax_no as 'fax_no',a.holder1_per_addr1,a.holder1_per_addr2,a.holder1_per_addr3,a.holder1_per_city,"
            lsSql &= " a.holder1_per_state,a.holder1_per_country,a.holder1_per_pin,a.nominee_name,a.nominee_part1,a.nominee_part2,a.nominee_part3,a.nominee_part4,a.nominee_part5,"
            lsSql &= " a.bank_name,a.bank_addr1,a.bank_addr2,a.bank_addr3,a.bank_city,a.bank_state,a.bank_country,a.bank_pin,"
            lsSql &= " a.bank_acc_no,a.bank_acc_type,a.bank_micr_code,a.bank_ifsc_code,a.rbi_ref_no,a.rbi_app_date,a.bene_type,'' as Bene_Category,"
            lsSql &= " a.bene_subtype,'' as Bene_Sub_Category,'' as Bene_AC_Category, '' as Bene_Occupation,"
            lsSql &= " a.share_count as 'cdsl', '0' as 'nsdl', '0' as 'physical' "
            lsSql &= " from sta_trn_tbenpost as a"
            lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N'"
            lsSql &= " where a.depository_code = 'C'"
            lsSql &= " and (a.holder1_pan <> null or a.holder1_pan <> '')"
            lsSql &= " and a.share_count > 0"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= lsCond
            lsSql &= " group by a.holder1_pan"
            lsSql &= " union"
            lsSql &= " select b.comp_name,b.isin_id,'Physical' as depository,'' as benpost_date,'' as dp_id,'' as client_id,a.holder1_name,"
            lsSql &= " a.holder2_name,a.holder3_name,a.holder1_fh_name,a.holder2_fh_name,a.holder3_fh_name,'' as lockin,'' as pledge,"
            lsSql &= " a.holder1_pan_no,a.holder2_pan_no,a.holder3_pan_no,a.folio_mail_id as holder1_email_id,'' as holder2_email_id,"
            lsSql &= " '' as holder3_email_id,a.folio_addr1 as holder1_addr1,a.folio_addr2 as holder1_addr2,a.folio_addr3 as holder1_addr3,"
            lsSql &= " a.folio_city as 'city',a.folio_state as 'state',a.folio_country as 'country',a.folio_pincode as 'Pincode',a.folio_contact_no as 'phone_no',"
            lsSql &= " '' as 'fax_no','' as holder1_per_addr1,'' as holder1_per_addr2,'' as holder1_per_addr3,'' as holder1_per_city,'' as holder1_per_state,'' as holder1_per_country,'' as holder1_per_pin,"
            lsSql &= " a.nominee_name,''as nominee_part1,'' as nominee_part2,'' as nominee_part3,'' as nominee_part4,'' as nominee_part5,a.bank_name,"
            lsSql &= " '' as bank_addr1,'' as bank_addr2,'' as bank_addr3,'' as bank_city,'' as bank_state,'' as bank_country,'' as bank_pin,a.bank_acc_no,a.bank_acc_type,"
            lsSql &= " a.bank_micr_code,a.bank_ifsc_code,'' as rbi_ref_no,'' as rbi_app_date,'' as bene_type,'' as Bene_Category,'' as bene_subtype,"
            lsSql &= " '' as Bene_Sub_Category,'' as BeneAC_Category, '' as Bene_Occupation,"
            lsSql &= " '0' as 'cdsl', '0'  as 'nsdl', a.folio_shares as 'physical' "
            lsSql &= " from sta_trn_tfolio as a"
            lsSql &= " inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N'"
            lsSql &= " where "
            lsSql &= " (a.holder1_pan_no <> null or a.holder1_pan_no <> '' )"
            lsSql &= " and a.folio_shares > 0"
            lsSql &= " and a.delete_flag = 'N'"
            lsSql &= " and a.comp_gid = " & Val(cboCompany.SelectedValue.ToString())
            lsSql &= lsCond1
            lsSql &= " group by a.holder1_pan_no) as c"

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

            Call frmDtpCtrClear(Me)

            btnClear.PerformClick()
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

End Class
