Public Class frmQryReport
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
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
    Friend WithEvents lblLoanNo As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents rtxtQry As System.Windows.Forms.RichTextBox
    Friend WithEvents cboQryCode As System.Windows.Forms.ComboBox
    Friend WithEvents lblRecordCount As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQryReport))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cboQryCode = New System.Windows.Forms.ComboBox()
        Me.rtxtQry = New System.Windows.Forms.RichTextBox()
        Me.lblLoanNo = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.lblRecordCount = New System.Windows.Forms.Label()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.pnlMain.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.cboQryCode)
        Me.pnlMain.Controls.Add(Me.rtxtQry)
        Me.pnlMain.Controls.Add(Me.lblLoanNo)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Location = New System.Drawing.Point(6, 7)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(611, 191)
        Me.pnlMain.TabIndex = 0
        '
        'cboQryCode
        '
        Me.cboQryCode.Location = New System.Drawing.Point(87, 4)
        Me.cboQryCode.Name = "cboQryCode"
        Me.cboQryCode.Size = New System.Drawing.Size(509, 21)
        Me.cboQryCode.TabIndex = 113
        '
        'rtxtQry
        '
        Me.rtxtQry.Location = New System.Drawing.Point(13, 31)
        Me.rtxtQry.Name = "rtxtQry"
        Me.rtxtQry.Size = New System.Drawing.Size(583, 112)
        Me.rtxtQry.TabIndex = 112
        Me.rtxtQry.Text = ""
        '
        'lblLoanNo
        '
        Me.lblLoanNo.Location = New System.Drawing.Point(-1, 5)
        Me.lblLoanNo.Name = "lblLoanNo"
        Me.lblLoanNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblLoanNo.Size = New System.Drawing.Size(82, 16)
        Me.lblLoanNo.TabIndex = 111
        Me.lblLoanNo.Text = "Query Code"
        Me.lblLoanNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(525, 153)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(447, 153)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "C&lear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(369, 153)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "&Refresh"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.btnExport)
        Me.pnlExport.Controls.Add(Me.lblRecordCount)
        Me.pnlExport.Location = New System.Drawing.Point(6, 316)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(635, 33)
        Me.pnlExport.TabIndex = 13
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(562, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 14
        Me.btnExport.Text = "&Export"
        '
        'lblRecordCount
        '
        Me.lblRecordCount.AutoSize = True
        Me.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblRecordCount.Location = New System.Drawing.Point(3, 10)
        Me.lblRecordCount.Name = "lblRecordCount"
        Me.lblRecordCount.Size = New System.Drawing.Size(94, 13)
        Me.lblRecordCount.TabIndex = 0
        Me.lblRecordCount.Text = "Total Records : "
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(6, 241)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(634, 69)
        Me.dgvReport.TabIndex = 12
        '
        'frmQryReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(825, 354)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmQryReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Query Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gsProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        rtxtQry.Text = ""
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

        Try
            lsSql = rtxtQry.Text

            If lsSql = "" Then
                MessageBox.Show("Please type the query !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                rtxtQry.Focus()
                Exit Sub
            End If

            lsSql = lsSql.ToLower()

            If lsSql.Contains("delete ") = True Or _
               lsSql.Contains("update ") = True Or _
               lsSql.Contains("truncate ") = True Or _
               lsSql.Contains("insert ") = True Or _
               lsSql.Contains("drop ") = True Or _
               lsSql.Contains("create ") = True Then
                MessageBox.Show("Invalid query !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                rtxtQry.Focus()
                Exit Sub
            End If

            gpPopGridView(dgvReport, lsSql, gOdbcConn)

            lblRecordCount.Text = "Total Records : " & dgvReport.RowCount
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmLoanReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        pnlMain.Top = 6
        pnlMain.Left = 6

        With dgvReport
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Left = 6
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
        End With

        pnlExport.Top = dgvReport.Top + dgvReport.Height + 6
        pnlExport.Left = dgvReport.Left
        pnlExport.Width = dgvReport.Width
        btnExport.Left = Math.Abs(pnlExport.Width - btnExport.Width)
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvReport, gsReportPath & "Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub frmQryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lsSql As String
        lsSql = " select * from sta_mst_tqry where delete_flag='N' order by qry_code"
        Call gpBindCombo(lsSql, "qry_code", "qry_desc", cboQryCode, gOdbcConn)
    End Sub

    Private Sub rtxtQry_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxtQry.GotFocus
        If cboQryCode.SelectedIndex <> -1 Then rtxtQry.Text = cboQryCode.SelectedValue.ToString
    End Sub

    Private Sub rtxtQry_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxtQry.TextChanged

    End Sub

    Private Sub cboQryCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQryCode.SelectedIndexChanged
        If cboQryCode.SelectedIndex <> -1 And cboQryCode.Text <> "" Then
            rtxtQry.Text = cboQryCode.SelectedValue.ToString
        Else
            rtxtQry.Text = ""
        End If
    End Sub
End Class
