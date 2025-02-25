Public Class frmCertificateView
    Dim mnCertId As Long

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvList, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmCertificateView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Public Sub New(CertId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnCertId = CertId
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim ds As New DataSet

        Try
            ' load certificate data
            lsSql = ""
            lsSql &= " select "
            lsSql &= " if(a.cert_sub_no = 1,cast(a.cert_no as nchar),concat(cast(a.cert_no as nchar),'-',cast(a.cert_sub_no as nchar))) as cert_no,"
            lsSql &= " a.share_count,"
            lsSql &= " date_format(a.issue_date,'%d-%m-%Y') as issue_date,"
            lsSql &= " date_format(a.expired_date,'%d-%m-%Y') as expired_date,"
            lsSql &= " date_format(a.lockin_period_from,'%d-%m-%Y') as lockin_period_from,"
            lsSql &= " date_format(a.lockin_period_to,'%d-%m-%Y') as lockin_period_to,"
            lsSql &= " date_format(a.hold_date,'%d-%m-%Y') as hold_date,"
            lsSql &= " a.cert_remark,"
            lsSql &= " make_set(a.cert_status," & gsCertStatusDesc & ") as cert_status_desc "
            lsSql &= " from sta_trn_tcert as a "
            lsSql &= " where a.cert_gid = " & mnCertId & " "
            lsSql &= " and a.delete_flag = 'N' "

            Call gpDataSet(lsSql, "cert", gOdbcConn, ds)

            With ds.Tables("cert")
                If .Rows.Count > 0 Then
                    txtCertNo.Text = .Rows(0).Item("cert_no").ToString
                    lblShareCount.Text = "Total Shares : " & .Rows(0).Item("share_count").ToString
                    txtStatus.Text = .Rows(0).Item("cert_status_desc").ToString
                    txtIssueDate.Text = .Rows(0).Item("issue_date").ToString
                    txtExpireDate.Text = .Rows(0).Item("expired_date").ToString
                    txtLockInFrom.Text = .Rows(0).Item("lockin_period_from").ToString
                    txtLockInTo.Text = .Rows(0).Item("lockin_period_to").ToString
                    txtHoldDate.Text = .Rows(0).Item("hold_date").ToString
                    txtRemark.Text = .Rows(0).Item("cert_remark").ToString
                End If

                .Rows.Clear()
            End With


            ' load dist grid
            lsSql = ""
            lsSql &= " select "
            lsSql &= " @a := @a + 1 as 'SNo',"
            lsSql &= " a.dist_from as 'From',"
            lsSql &= " a.dist_to as 'To',"
            lsSql &= " a.dist_count as 'Count',"
            lsSql &= " a.cert_gid,a.certdist_gid "
            lsSql &= " from sta_trn_tcertdist as a "
            lsSql &= " where a.cert_gid = " & mnCertId & " "
            lsSql &= " and a.delete_flag = 'N' "

            dgvList.Columns.Clear()

            Call gfInsertQry("set @a := 0", gOdbcConn)
            Call gpPopGridView(dgvList, lsSql, gOdbcConn)

            dgvList.Columns("cert_gid").Visible = False
            dgvList.Columns("certdist_gid").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class