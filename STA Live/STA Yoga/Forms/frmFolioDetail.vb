Public Class frmFolioDetail
    Dim mnFolioId As Long

    Private Sub btnFolio_Click(sender As Object, e As EventArgs) Handles btnFolio.Click
        Dim frm As New frmFolioView(mnFolioId)
        frm.ShowDialog()
    End Sub

    Public Sub New(FolioId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnFolioId = FolioId
    End Sub

    Private Sub btnCertificate_Click(sender As Object, e As EventArgs) Handles btnCertificate.Click
        Dim objFrm As New frmCertificateList(mnFolioId)
        objFrm.ShowDialog()
    End Sub

    Private Sub btnInward_Click(sender As Object, e As EventArgs) Handles btnInward.Click
        Dim objFrm As New frmInwardList(mnFolioId)
        objFrm.ShowDialog()
    End Sub

    Private Sub btnQueue_Click(sender As Object, e As EventArgs) Handles btnQueue.Click
        Dim frm As New frmViewQueueLog(0, mnFolioId)
        frm.ShowDialog()
    End Sub

    Private Sub btnAttachment_Click(sender As Object, e As EventArgs) Handles btnAttachment.Click
        Dim frm As New frmFolioAttachmentView(mnFolioId)
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnOutward_Click(sender As Object, e As EventArgs) Handles btnOutward.Click
        Dim objFrm As New frmOutwardReport(mnFolioId)
        objFrm.ShowDialog()
    End Sub

    Private Sub btnSignature_Click(sender As Object, e As EventArgs) Handles btnSignature.Click
        Call FolioSignature(mnFolioId)
    End Sub

    Private Sub frmFolioDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CancelButton = btnClose
    End Sub


    Private Sub btnDistSeries_Click(sender As Object, e As EventArgs) Handles btnDistSeries.Click
        Dim frm As New frmCertDistReport(0, mnFolioId)
        frm.ShowDialog()
    End Sub

    Private Sub btnTran_Click(sender As Object, e As EventArgs) Handles btnTran.Click
        Dim frm As New frmFolioTranReport(mnFolioId)
        frm.ShowDialog()
    End Sub
End Class