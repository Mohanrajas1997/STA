Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmUploadRegister
    Dim mnUploadId As Long

    ' Aligns the given text in specified format
    Private Function AlignTxt(ByVal txt As String, ByVal Length As Integer, ByVal Alignment As Integer) As String
        Select Case Alignment
            Case 1
                Return LSet(txt, Length)
            Case 4
                Return CSet(txt, Length)
            Case 7
                Return RSet(txt, Length)
            Case Else
                Return txt
        End Select
    End Function

    ' Center Align the Given Text
    Private Function CSet(ByVal txt As String, ByVal PaperChrWidth As Integer) As String
        Dim s As String                 ' Temporary String Variable
        Dim l As Integer                ' Length of the String
        If Len(txt) > PaperChrWidth Then
            CSet = Mid(txt, 1, PaperChrWidth)
        Else
            l = (PaperChrWidth - Len(txt)) / 2
            s = RSet(txt, l + Len(txt))
            CSet = Space(PaperChrWidth - Len(s))
            CSet = s + CSet
        End If
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MessageBox.Show("Are you sure to close ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmUploadStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Public Sub New(UploadId As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mnUploadId = UploadId
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String


        lsSql = ""
        lsSql &= " select a.*,b.comp_name "
        lsSql &= " from sta_trn_tupload as a "
        lsSql &= " left join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and b.delete_flag = 'N' "
        lsSql &= " where true "
        lsSql &= " and a.upload_gid = " & mnUploadId & " "
        lsSql &= " and a.delete_flag = 'N' "

        Call gpDataSet(lsSql, "upload", gOdbcConn, ds)

        With ds.Tables("upload")
            If .Rows.Count = 1 Then
                txtUploadNo.Text = .Rows(0).Item("upload_no").ToString
                txtUploadDate.Text = Format(.Rows(0).Item("upload_date"), "dd-MM-yyyy")
                txtCompName.Text = .Rows(0).Item("comp_name").ToString

                txtTranCount.Text = .Rows(0).Item("transfer_count").ToString
                txtTranFrom.Text = .Rows(0).Item("transfer_start_sno").ToString
                txtTranTo.Text = .Rows(0).Item("transfer_end_sno").ToString

                txtCertCount.Text = .Rows(0).Item("cert_count").ToString
                txtCertFrom.Text = .Rows(0).Item("cert_start_sno").ToString
                txtCertTo.Text = .Rows(0).Item("cert_end_sno").ToString

                txtObjxCount.Text = .Rows(0).Item("objx_count").ToString
                txtObjxFrom.Text = .Rows(0).Item("objx_start_sno").ToString
                txtObjxTo.Text = .Rows(0).Item("objx_end_sno").ToString
            End If
        End With
    End Sub

    Private Sub btnTranser_Click(sender As Object, e As EventArgs) Handles btnTranser.Click
    End Sub

    Private Sub btnCertificate_Click(sender As Object, e As EventArgs) Handles btnCertificate.Click
    End Sub

    Private Sub btnObjx_Click(sender As Object, e As EventArgs) Handles btnObjx.Click
    End Sub

    Private Sub frmUploadRegister_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        With rtfRpt
            .Left = grpHeader.Left
            .Top = grpHeader.Top * 2 + grpHeader.Height
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (grpHeader.Top + grpHeader.Height) - pnlExport.Height - 42)
            .Tag = .Height
        End With

        pnlExport.Top = Me.Height - (pnlExport.Height * 2)
        pnlExport.Width = rtfRpt.Width

        pnlPrint.Left = pnlExport.Width - pnlPrint.Width
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Call gpOpenFile(gsReportPath & "temp.txt")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintDotMatrix_Click(sender As Object, e As EventArgs) Handles btnPrintDotMatrix.Click
        If MsgBox("Are you sure to print ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
            Dim clsPrint As New clsPrint
            clsPrint.PrintDotMatrix(gsReportPath & "temp.txt", True)
            MsgBox("Printed successfully !", MsgBoxStyle.Information, gsProjectName)
        End If
    End Sub

    Private Sub btnPrintLaser_Click(sender As Object, e As EventArgs) Handles btnPrintLaser.Click
        If MsgBox("Are you sure to print ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gsProjectName) = MsgBoxResult.Yes Then
            Dim clsPrint As New clsPrint
            clsPrint.Printing(gsReportPath & "temp.txt")
            MsgBox("Printed successfully !", MsgBoxStyle.Information, gsProjectName)
        End If
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim frm As New frmUploadView(mnUploadId, txtCompName.Text)
        frm.MdiParent = frmMain
        frm.Show()
    End Sub
End Class