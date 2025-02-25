Public Class frmViewQueueLog
    Dim mnInwardId As Long = 0
    Dim mnFolioId As Long = 0

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If dgvQueryLog.Rows.Count > 0 Then
            PrintDGridXML(dgvQueryLog, gsReportPath & "\report.xls", "Report")
        End If
    End Sub

    Private Sub frmViewQueueLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Public Sub New(InwardId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnInwardId = InwardId
    End Sub

    Public Sub New(InwardId As Long, FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnInwardId = InwardId
        mnFolioId = FolioId
    End Sub

    Private Sub LoadData()
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select "
        lsSql &= " d.inward_no as 'Inward No',"
        lsSql &= " a.queue_date as 'Queue Date',"
        'lsSql &= " concat(b.group_name,'-',a.queue_from_user) as 'From',"
        lsSql &= " concat(b.group_name,'-',fn_sta_get_behalfof(a.queue_from_user,a.queue_date)) as 'From',"
        'lsSql &= " concat(c.group_name,ifnull(concat('-',a.action_by),'')) as 'To',"
        lsSql &= " concat(c.group_name,ifnull(concat('-',fn_sta_get_behalfof(a.action_by,a.action_date)),'')) as 'To',"
        lsSql &= " make_set(a.action_status," & gsQueueActionStatusDesc & ") as 'Action Status',"
        lsSql &= " a.action_date as 'Action Date',"
        lsSql &= " a.action_remark as 'Remark' "
        lsSql &= " from sta_trn_tqueue as a "
        lsSql &= " inner join sta_mst_tgroup as b on b.group_code = a.queue_from and b.delete_flag = 'N' "
        lsSql &= " inner join sta_mst_tgroup as c on c.group_code = a.queue_to and c.delete_flag = 'N' "
        lsSql &= " inner join sta_trn_tinward as d on a.inward_gid = d.inward_gid and d.delete_flag = 'N' "
        lsSql &= " where true "

        If mnInwardId > 0 Then
            lsSql &= " and a.inward_gid = " & mnInwardId & " "
        End If

        If mnFolioId > 0 Then
            lsSql &= " and d.folio_gid = " & mnFolioId & " "
        End If
        lsSql &= " and a.delete_flag = 'N' "

        lsSql &= " order by a.inward_gid,a.queue_gid"

        Call gpPopGridView(dgvQueryLog, lsSql, gOdbcConn)
    End Sub
End Class