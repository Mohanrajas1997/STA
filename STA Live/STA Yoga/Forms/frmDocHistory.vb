Imports System.IO

Public Class frmDocHistory
    Dim mnInwardId As Long
    Dim mnFolioId As Long
    Dim mnSignatureId As Long
    Dim msTranCode As String

    Private Sub btnQueue_Click(sender As Object, e As EventArgs) Handles btnQueue.Click
        Dim frm As New frmViewQueueLog(mnInwardId)
        frm.ShowDialog()
    End Sub

    Public Sub New(InwardId As Long)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnInwardId = InwardId

        msTranCode = gfExecuteScalar("select tran_code from sta_trn_tinward where inward_gid = " & mnInwardId & " and delete_flag = 'N'", gOdbcConn)
        mnFolioId = Val(gfExecuteScalar("select folio_gid from sta_trn_tinward where inward_gid = " & mnInwardId & " and delete_flag = 'N'", gOdbcConn))
        mnSignatureId = Val(gfExecuteScalar("select signature_gid from sta_trn_tfolio where folio_gid = " & mnFolioId & " and delete_flag = 'N'", gOdbcConn))
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDoc_Click(sender As Object, e As EventArgs) Handles btnDoc.Click
        Dim objFrm As Form

        Select Case msTranCode
            Case "CA"
                objFrm = New frmAddressChange("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "NC"
                objFrm = New frmNameCorrection("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "CP"
                objFrm = New frmPanChange("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "CC"
                objFrm = New frmCategoryChange("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "NU"
                objFrm = New frmNomineeChange("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "CB"
                objFrm = New frmBankDetailsChange("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "TR"
                objFrm = New frmCertificateTransfer("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "TM"
                objFrm = New frmCertificateTransmission_new("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Certificate Transmission"
                objFrm.ShowDialog()
            Case "OL"
                objFrm = New frmCertificateTransmission_new("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Certificate Old Transfer"
                objFrm.ShowDialog()
            Case "TP"
                objFrm = New frmCertificateTransmission_new("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Certificate Transposition"
                objFrm.ShowDialog()
            Case "FC"
                objFrm = New frmCertificateTransmission_new("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Folio Consolidation"
                objFrm.ShowDialog()
            Case "ST"
                ' Stop Transfer
                objFrm = New frmCertificateHold("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Certificate Stop Transfer"
                objFrm.ShowDialog()
            Case "RT"   ' release for transfer
                objFrm = New frmCertificateHold("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Certificate Release Stop Transfer"
                objFrm.ShowDialog()
            Case "CL"
                ' Certificate Lock
                objFrm = New frmCertificateLock("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "RL"
                ' Release Lock
                objFrm = New frmCertificateTran("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Release Lock"
                objFrm.ShowDialog()
            Case "CO"
                ' Consolidation
                objFrm = New frmCertificateTran("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Certificate Consolidation"
                objFrm.ShowDialog()
            Case "LS"
                ' Consolidation
                objFrm = New frmCertificateTran("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Lose of Certificate"
                objFrm.ShowDialog()
            Case "DP"
                ' Duplicate
                objFrm = New frmCertificateTran("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Duplicate Certificate"
                objFrm.ShowDialog()
            Case "SP"
                ' Split
                objFrm = New frmCertificateSplit("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "DM"
                ' Demat
                objFrm = New frmCertificateDemat("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "RM"
                ' remat 
                objFrm = New frmCertificateTran("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "Remat"
                objFrm.ShowDialog()
            Case "DT"
                ' Depository Transfer
                objFrm = New frmDepositoryTransfer("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "CS"
                ' Depository Certificate Split
                objFrm = New frmDepCertificateSplit("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "DS"
                ' Depository Certificate Distinctive Series Split
                objFrm = New frmDepCertificateDistSplit("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "IS"
                objFrm = New frmISR1("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "AM"
                objFrm = New frmCAAllotment("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "IE"
                objFrm = New frmiepfmaker(mnInwardId, "V")
                objFrm.ShowDialog()
            Case "PI"
                objFrm = New frmCAPhysicalToIEPF("V", mnInwardId, 0, msTranCode)
                objFrm.ShowDialog()
            Case "LS"
                ' LS Cum Transmission
                objFrm = New frmCertificateTran("V", mnInwardId, 0, msTranCode)
                objFrm.Text = "LS Cum Transmission"
                objFrm.ShowDialog()
        End Select
    End Sub

    Private Sub frmDocHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CancelButton = btnClose
    End Sub

    Private Sub btnAttachment_Click(sender As Object, e As EventArgs) Handles btnAttachment.Click
        Dim frm As New frmAttachmentView(mnInwardId)
        frm.ShowDialog()
    End Sub

    Private Sub btnInward_Click(sender As Object, e As EventArgs) Handles btnInward.Click
        'Dim objFrm As New frmInwardEntry("VIEW", mnInwardId)
        Dim objFrm As New frmInwardEntryNew("VIEW", mnInwardId)
        objFrm.ShowDialog()
    End Sub

    Private Sub btnFolio_Click(sender As Object, e As EventArgs) Handles btnFolio.Click
        Dim frm As New frmFolioView(mnFolioId)
        frm.ShowDialog()
    End Sub

    Private Sub btnOutward_Click(sender As Object, e As EventArgs) Handles btnOutward.Click
        Dim objFrm As New frmOutwardEntry("VIEW", mnInwardId)
        objFrm.ShowDialog()
    End Sub

    Private Sub btnCertificate_Click(sender As Object, e As EventArgs) Handles btnCertificate.Click
        Dim objFrm As New frmCertificateList(mnFolioId)
        objFrm.ShowDialog()
    End Sub

    Private Sub btnSignature_Click(sender As Object, e As EventArgs) Handles btnSignature.Click
        If mnSignatureId > 0 Then
            Call ShowSignature(mnSignatureId)
            'Dim frm As New frmSignatureView(mnFolioId)
            'frm.ShowDialog()
        Else
            MessageBox.Show("Signature not available !", gsProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnAllAttachment_Click(sender As Object, e As EventArgs) Handles btnAllAttachment.Click
        Dim frm As New frmFolioAttachmentView(mnFolioId)
        frm.ShowDialog()
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