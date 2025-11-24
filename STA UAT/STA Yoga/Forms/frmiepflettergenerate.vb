Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.Text

Public Class frmiepflettergenerate
#Region "Local Variables"
    Dim GetReqGid As Long = 0
    Dim GetInwardNo As Long = 0
    Dim GetStatus As String = ""
    Dim GetInwardGid As Long = 0
    Dim GetCompGid As Long = 0
    Dim GetShareCount As Decimal = 0
    Dim GetReqType As String = ""
    Dim GetClaimantName As String = ""
    Dim GetShareHolderName As String = ""
    Dim GetFolioNo As String = ""
    Dim GetCompInwardNo As String = ""
    Dim GetCompanyName As String = ""
    Dim GetClaimantAddr As String = ""
    Dim GetClaimantEmail As String = ""
    Dim GetHolderAddr As String = ""
    Dim GetHolderEmail As String = ""
    Dim GetShareAmount As Decimal = 0
    Dim GetDpClientID As String = ""
    Dim GetNomineeName As String = ""
    Dim GetNomineeAddr As String = ""
    Dim GetFolioGid As Long = 0
#End Region

    Public Sub New(req_gid As Long, inwardno As Long, status As String, inwardgid As Long, compgid As Long, sharecount As Decimal,
                   reqtype As String, claimantname As String, shareholdername As String, folioNo As String, compinwardno As String, companyname As String,
                   claimantaddr As String, claimantemail As String, holderaddr As String, holderemail As String, shareamount As Decimal,
                   dpclientid As String, nomineename As String, nomineeaddr As String, foliogid As Long)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        GetReqGid = req_gid
        GetInwardNo = inwardno
        GetStatus = status
        GetInwardGid = inwardgid
        GetCompGid = compgid
        GetShareCount = sharecount
        GetReqType = reqtype
        GetClaimantName = claimantname
        GetShareHolderName = shareholdername
        GetFolioNo = folioNo
        GetCompInwardNo = compinwardno
        GetCompanyName = companyname
        GetClaimantAddr = claimantaddr
        GetClaimantEmail = claimantemail
        GetHolderAddr = holderaddr
        GetHolderEmail = holderemail
        GetShareAmount = shareamount
        GetDpClientID = dpclientid
        GetNomineeName = nomineename
        GetNomineeAddr = nomineeaddr
        GetFolioGid = foliogid
    End Sub

    Private Sub frmiepflettergenerate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        generate_btn.Enabled = False

        If GetStatus = "Inex" Then
            remarks_lab.Visible = True
            remark_txt.Visible = True
            Dim remark As String = ""
            remark_txt.ReadOnly = False

            remark = GetAdditionalRemarks(GetReqGid)
            remark_txt.Text = remark
        Else
            'remark_txt.ReadOnly = True
            'Dim spacing As Integer = 20 ' space between buttons

            '' Calculate total width of both buttons + spacing
            'Dim totalWidth As Integer = preview_btn.Width + spacing + generate_btn.Width

            '' Starting left position (to center both buttons as a group)
            'Dim startLeft As Integer = (Me.ClientSize.Width - totalWidth) \ 2
            'Dim centerTop As Integer = (Me.ClientSize.Height - preview_btn.Height) \ 2

            '' Position preview button (left)
            'preview_btn.Left = startLeft
            'preview_btn.Top = centerTop

            '' Position generate button (right of preview)
            'generate_btn.Left = startLeft + preview_btn.Width + spacing
            'generate_btn.Top = centerTop

            generate_btn_Click(Nothing, EventArgs.Empty)
            Me.Close()
        End If
    End Sub

    'Private Sub preview_btn_Click(sender As Object, e As EventArgs) Handles preview_btn.Click
    '    Try
    '        'Dim reqGid As Integer = Convert.ToInt32(dgv_covering.Rows(e.RowIndex).Cells("req_gid").Value)
    '        Dim inexReasons As String = ""
    '        Dim remarks As String = remark_txt.Text

    '        If GetStatus = "Inex" Then
    '            UpdateAdditionalRemarks(GetReqGid, remarks)
    '            inexReasons = GetInexReasons(GetReqGid)

    '            'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\New\IEPF Rejection Letter Format.rtf"
    '            Dim rtfContent As String

    '            Using fs As New FileStream(gsiepfRejectiontemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '                Using sr As New StreamReader(fs)
    '                    rtfContent = sr.ReadToEnd()
    '                End Using
    '            End Using

    '            ' Replace placeholders
    '            rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '            rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
    '            rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)
    '            rtfContent = rtfContent.Replace("<<inward_no>>", GetCompInwardNo)

    '            If GetReqType = "ShareHolder" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetHolderAddr))
    '            ElseIf GetReqType = "Claimant" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetClaimantAddr))
    '            Else
    '                rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetNomineeAddr))
    '            End If

    '            Dim reasonsList = inexReasons.Split(New String() {" /* "}, StringSplitOptions.RemoveEmptyEntries)
    '            'Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r) r.Trim()))
    '            Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r, i) (i + 1).ToString() & ". " & r.Trim()))

    '            rtfContent = rtfContent.Replace("<<inex_reasons>>", reasonsFormatted)

    '            'Dim tempPath As String = Path.Combine(Path.GetTempPath(), "IEPF Rejection Letter Format_" & Guid.NewGuid().ToString() & ".rtf")
    '            'Dim rejpathname As String = "IEPF CLAIM\Reject"
    '            'Dim tempPath As String = Path.Combine(gsiepfRejectiontemplatepath, rejpathname, "IEPF Rejection Letter Format_" & Guid.NewGuid().ToString() & ".rtf")
    '            'File.WriteAllText(tempPath, rtfContent)

    '            Dim outputFile As String = "C:\temp\IEPF CLAIM\Reject"

    '            ' If folder doesnot exists means create a directory folder
    '            If Not System.IO.Directory.Exists(outputFile) Then
    '                System.IO.Directory.CreateDirectory(outputFile)
    '            End If

    '            outputFile = Path.ChangeExtension(outputFile + "\IEPF Rejection Letter Format.rtf", ".rtf")

    '            ' Read our HTML file a string.
    '            Dim htmlString As String = File.ReadAllText(gsiepfRejectiontemplatepath)

    '            ' Open the result for demonstration purposes.
    '            If Not String.IsNullOrEmpty(outputFile) Then
    '                File.WriteAllText(outputFile, rtfContent)
    '                System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
    '            End If

    '            'Dim p As New ProcessStartInfo(tempPath) With {
    '            '    .UseShellExecute = True
    '            '}
    '            'Process.Start(p)

    '            generate_btn.Enabled = True

    '        ElseIf GetStatus = "Approved" Then
    '            'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\New\Entitlement letter.rtf"
    '            Dim rtfContent As String

    '            Using fs As New FileStream(gsiepfApprovetemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '                Using sr As New StreamReader(fs)
    '                    rtfContent = sr.ReadToEnd()
    '                End Using
    '            End Using

    '            ' Replace placeholders
    '            rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '            rtfContent = rtfContent.Replace("<<year>>", DateTime.Now.ToString("yyyy"))
    '            rtfContent = rtfContent.Replace("<<share_count>>", GetShareCount)
    '            rtfContent = rtfContent.Replace("<<shareholder_name>>", GetShareHolderName)
    '            rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
    '            rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)

    '            Dim dtComp As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_code")
    '            Dim compCode As String = ""

    '            If dtComp IsNot Nothing AndAlso dtComp.Rows.Count > 0 Then
    '                compCode = dtComp.Rows(0)(0).ToString()   ' first column, first row
    '            End If

    '            rtfContent = rtfContent.Replace("<<comp_code>>", compCode)

    '            Dim dtCompSec As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_sec")
    '            Dim compSec As String = ""

    '            If dtCompSec IsNot Nothing AndAlso dtCompSec.Rows.Count > 0 Then
    '                compSec = dtCompSec.Rows(0)(0).ToString()   ' first column, first row
    '            End If

    '            rtfContent = rtfContent.Replace("<<comp_sec_name>>", compSec)

    '            'If GetReqType = "ShareHolder" Then
    '            '    rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '            '    rtfContent = rtfContent.Replace("<<address>>", GetHolderAddr)
    '            'ElseIf GetReqType = "Claimant" Then
    '            '    rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '            '    rtfContent = rtfContent.Replace("<<address>>", GetClaimantAddr)
    '            'Else
    '            '    rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
    '            '    rtfContent = rtfContent.Replace("<<address>>", GetNomineeAddr)
    '            'End If


    '            If GetReqType = "ShareHolder" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetHolderAddr))
    '            ElseIf GetReqType = "Claimant" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetClaimantAddr))
    '            Else
    '                rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetNomineeAddr))
    '            End If

    '            ' Build table rows dynamically
    '            Dim dt As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "lessthan7years")
    '            Dim rowBuilder As New System.Text.StringBuilder()
    '            Dim headerBuilder As New System.Text.StringBuilder()

    '            'headerBuilder.Append("{\trowd")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio Dpid\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Share Holder\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Warrent No\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Dividend Year\b0\cell")
    '            'headerBuilder.Append("\row}")

    '            'For Each dr As DataRow In dt.Rows
    '            '    rowBuilder.Append("{\trowd")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr("Warrant No").ToString() & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("Net Amount").ToString() & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("Dividend Year").ToString() & "\cell")
    '            '    rowBuilder.Append("\row}")
    '            'Next

    '            headerBuilder.Append("{\trowd")
    '            headerBuilder.Append("\cellx2880 \b Folio Dpid\b0\cell")
    '            headerBuilder.Append("\cellx7200 \b Share Holder\b0\cell")
    '            headerBuilder.Append("\cellx9360 \b Warrent No\b0\cell")
    '            headerBuilder.Append("\cellx12240 \b Net Amount\b0\cell")
    '            headerBuilder.Append("\cellx15120 \b Dividend Year\b0\cell")
    '            headerBuilder.Append("\row}")

    '            For Each dr As DataRow In dt.Rows
    '                rowBuilder.Append("{\trowd")
    '                rowBuilder.Append("\cellx2880 " & GetFolioNo & "\cell")
    '                rowBuilder.Append("\cellx7200 " & GetShareHolderName & "\cell")
    '                rowBuilder.Append("\cellx9360 " & dr("Warrant No").ToString() & "\cell")
    '                rowBuilder.Append("\cellx12240 " & dr("Net Amount").ToString() & "\cell")
    '                rowBuilder.Append("\cellx15120 " & dr("Dividend Year").ToString() & "\cell")
    '                rowBuilder.Append("\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '            ' Build table rows dynamically
    '            Dim dt2 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "annexure")
    '            Dim rowBuilder2 As New System.Text.StringBuilder()
    '            Dim headerBuilder2 As New System.Text.StringBuilder()

    '            headerBuilder2.Append("{\trowd")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio Dpid\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Share Holder\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Warrent No\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Dividend Year\b0\cell")
    '            headerBuilder2.Append("\row}")

    '            For Each dr2 As DataRow In dt2.Rows
    '                rowBuilder2.Append("{\trowd")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr2("Warrant No").ToString() & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr2("Net Amount").ToString() & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("Dividend Year").ToString() & "\cell")
    '                rowBuilder2.Append("\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table4>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '            ' Build table rows dynamically
    '            Dim dt3 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_iepf")
    '            Dim rowBuilder3 As New System.Text.StringBuilder()
    '            Dim headerBuilder3 As New System.Text.StringBuilder()

    '            headerBuilder3.Append("{\trowd")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio Dpid\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Share Holder\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Warrent No\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Dividend Year\b0\cell")
    '            headerBuilder3.Append("\row}")

    '            For Each dr3 As DataRow In dt3.Rows
    '                rowBuilder3.Append("{\trowd")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr3("Warrant No").ToString() & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr3("Net Amount").ToString() & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr3("Dividend Year").ToString() & "\cell")
    '                rowBuilder3.Append("\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table2>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '            ' Build table rows dynamically
    '            Dim dt4 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_details")
    '            Dim rowBuilder4 As New System.Text.StringBuilder()
    '            Dim headerBuilder4 As New System.Text.StringBuilder()

    '            headerBuilder4.Append("{\trowd")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2000 \b Folio Dpid\b0\cell")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 \b Share Holder\b0\cell")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7500 \b Date Of Transfer\b0\cell")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 \b No Of Shares Transferred To IEPF\b0\cell")
    '            headerBuilder4.Append("\row}")

    '            For Each dr4 As DataRow In dt4.Rows
    '                rowBuilder4.Append("{\trowd")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2000 " & GetFolioNo & "\cell")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 " & GetShareHolderName & "\cell")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7500 " & dr4("Transfer Date").ToString() & "\cell")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 " & dr4("No of Shares").ToString() & "\cell")
    '                rowBuilder4.Append("\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table3>>", headerBuilder4.ToString() & rowBuilder4.ToString())

    '            ' Save to temp file
    '            Dim tempPath As String = Path.Combine(Path.GetTempPath(), "Entitlement letter_" & Guid.NewGuid().ToString() & ".rtf")
    '            File.WriteAllText(tempPath, rtfContent)

    '            ' Open file
    '            Dim p As New ProcessStartInfo(tempPath) With {
    '                .UseShellExecute = True
    '            }
    '            Process.Start(p)

    '            generate_btn.Enabled = True
    '        Else
    '            MessageBox.Show("No Letter Available", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            Exit Sub
    '        End If

    '        '' GetCompGid = 2 - Bimetal Bearings Limited
    '        'ElseIf GetStatus = "Approved" AndAlso GetCompGid = 2 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\BIMETAL IEPF Confirmation letter format.rtf"
    '        '    'BimetalConfirmationLetter(templatePath, GetFolioNo, GetReqType, GetShareHolderName, GetClaimantName, GetShareCount)
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<Date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<sharecount>>", GetShareCount)

    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<type>>", "ShareHolder")
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<type>>", "Claimant")
    '        '    End If

    '        '    ' Build table rows dynamically
    '        '    Dim dt As DataTable = GetCertDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()
    '        '    headerBuilder.Append("{\trowd")

    '        '    ' First column (Folio No.)
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrl\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrb\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrr\brdrs\brdrw10")
    '        '    headerBuilder.Append("\cellx2000 \b Folio No.\b0\cell")

    '        '    ' Second column (Share Certificate No.)
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrl\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrb\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrr\brdrs\brdrw10")
    '        '    headerBuilder.Append("\cellx4500 \b Share Certificate No.\b0\cell")

    '        '    ' Third column (Distinctive Nos.)
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrl\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrb\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrr\brdrs\brdrw10")
    '        '    headerBuilder.Append("\cellx7500 \b Distinctive Nos.\b0\cell")

    '        '    ' Fourth column (No. of shares)
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrl\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrb\brdrs\brdrw10")
    '        '    headerBuilder.Append("\clbrdrr\brdrs\brdrw10")
    '        '    headerBuilder.Append("\cellx9070 \b No. of shares\b0\cell")

    '        '    ' End header row
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")

    '        '        ' First column (Folio No.)
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10") ' top border
    '        '        rowBuilder.Append("\clbrdrl\brdrs\brdrw10") ' left border
    '        '        rowBuilder.Append("\clbrdrb\brdrs\brdrw10") ' bottom border
    '        '        rowBuilder.Append("\clbrdrr\brdrs\brdrw10") ' right border
    '        '        rowBuilder.Append("\cellx2000 " & GetFolioNo & "\cell")

    '        '        ' Second column (Share Certificate No.)
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrl\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrb\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrr\brdrs\brdrw10")
    '        '        rowBuilder.Append("\cellx4500 " & dr("cert_no").ToString() & "\cell")

    '        '        ' Third column (Distinctive Nos.)
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrl\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrb\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrr\brdrs\brdrw10")
    '        '        rowBuilder.Append("\cellx7500 " & dr("dist_series").ToString() & "\cell")

    '        '        ' Fourth column (No. of shares)
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrl\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrb\brdrs\brdrw10")
    '        '        rowBuilder.Append("\clbrdrr\brdrs\brdrw10")
    '        '        rowBuilder.Append("\cellx9070 " & dr("share_count").ToString() & "\cell")

    '        '        ' End row
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    ' Replace placeholder in RTF with generated rows
    '        '    rtfContent = rtfContent.Replace("<<ROWPLACEHOLDER>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")

    '        '    ' First column (Folio No.)
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrl\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrb\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrr\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\cellx3023 \b Financial year\b0\cell")

    '        '    ' Second column (Share Certificate No.)
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrl\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrb\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrr\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\cellx6046 \b Date of dividend\b0\cell")

    '        '    ' Third column (Distinctive Nos.)
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrl\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrb\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\clbrdrr\brdrs\brdrw10")
    '        '    headerBuilder2.Append("\cellx9070 \b Dividend Amount\b0\cell")

    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt2.Rows
    '        '        rowBuilder2.Append("{\trowd")

    '        '        ' First column (Folio No.)
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10") ' top border
    '        '        rowBuilder2.Append("\clbrdrl\brdrs\brdrw10") ' left border
    '        '        rowBuilder2.Append("\clbrdrb\brdrs\brdrw10") ' bottom border
    '        '        rowBuilder2.Append("\clbrdrr\brdrs\brdrw10") ' right border
    '        '        rowBuilder2.Append("\cellx3023 " & dr2("Fin Year").ToString() & "\cell")

    '        '        ' Second column (Share Certificate No.)
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\clbrdrl\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\clbrdrb\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\clbrdrr\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\cellx6046 " & dr2("Dividend Date").ToString() & "\cell")

    '        '        ' Third column (Distinctive Nos.)
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\clbrdrl\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\clbrdrb\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\clbrdrr\brdrs\brdrw10")
    '        '        rowBuilder2.Append("\cellx9070 " & dr2("Dividend Amount").ToString() & "\cell")

    '        '        ' End row
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    ' Replace placeholder in RTF with generated rows
    '        '    rtfContent = rtfContent.Replace("<<ROWPLACEHOLDER2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    ' Save to temp file
    '        '    Dim tempPath As String = Path.Combine(Path.GetTempPath(), "BIMETAL IEPF Confirmation letter format_" & Guid.NewGuid().ToString() & ".rtf")
    '        '    File.WriteAllText(tempPath, rtfContent)

    '        '    ' Open file
    '        '    Dim p As New ProcessStartInfo(tempPath) With {
    '        '        .UseShellExecute = True
    '        '    }
    '        '    Process.Start(p)

    '        '    generate_btn.Enabled = True

    '        '' GetCompGid = 11 - Butterfly Gandhimathi Appliances Ltd
    '        'ElseIf GetStatus = "Approved" AndAlso GetCompGid = 11 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\BUTTERFLY  IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<Date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<inwardno>>", GetCompInwardNo)

    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '        '    End If

    '        '    ' Build table rows dynamically
    '        '    Dim dt As DataTable = GetCertDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Share Certificate No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b No. of shares\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Distinctive Nos\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr("cert_no").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("share_count").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("dist_series").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 \b Company\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 \b Fin Year Code\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 \b Folio No\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 \b Holder Name\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 \b No. of shares\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 \b Net Amount\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Warrant No\b0\cell")
    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt2.Rows
    '        '        rowBuilder2.Append("{\trowd")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 " & GetCompanyName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 " & dr2("Fin Year").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 " & GetFolioNo & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 " & GetShareHolderName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 " & dr2("Share Count").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 " & dr2("net_amount").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("Warrant No").ToString() & "\cell")
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    Dim dt3 As DataTable = GetDividendDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder3 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder3 As New System.Text.StringBuilder()

    '        '    headerBuilder3.Append("{\trowd")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Warrant No\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Divident Year\b0\cell")
    '        '    headerBuilder3.Append("\row}")

    '        '    For Each dr3 As DataRow In dt3.Rows
    '        '        rowBuilder3.Append("{\trowd")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr3("Warrant No").ToString() & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr3("net_amount").ToString() & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr3("Fin Year").ToString() & "\cell")
    '        '        rowBuilder3.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE3>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '        '    ' Save to temp file
    '        '    Dim tempPath As String = Path.Combine(Path.GetTempPath(), "BUTTERFLY  IEPF Confirmation letter format_" & Guid.NewGuid().ToString() & ".rtf")
    '        '    File.WriteAllText(tempPath, rtfContent)

    '        '    ' Open file
    '        '    Dim p As New ProcessStartInfo(tempPath) With {
    '        '        .UseShellExecute = True
    '        '    }
    '        '    Process.Start(p)

    '        '    generate_btn.Enabled = True

    '        '' GetCompGid = 3 - LOYAL TEXTILE MILLS LTD
    '        'ElseIf GetStatus = "Approved" AndAlso GetCompGid = 3 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\LOYAL IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<DATE>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<INWARD NO>>", GetCompInwardNo)

    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<TYPE>>", "ShareHolder")
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<TYPE>>", "Claimant")
    '        '    End If

    '        '    ' Build table rows dynamically
    '        '    Dim dt As DataTable = GetCertDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Share Certificate No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b No. of shares\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Distinctive Nos\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr("cert_no").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("share_count").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("dist_series").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 \b Company\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 \b Fin Year Code\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 \b Folio No\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 \b Holder Name\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 \b No. of shares\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 \b Net Amount\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Warrant No\b0\cell")
    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt2.Rows
    '        '        rowBuilder2.Append("{\trowd")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 " & GetCompanyName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 " & dr2("Fin Year").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 " & GetFolioNo & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 " & GetShareHolderName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 " & dr2("Share Count").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 " & dr2("net_amount").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("Warrant No").ToString() & "\cell")
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    Dim dt3 As DataTable = GetDividendDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder3 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder3 As New System.Text.StringBuilder()

    '        '    headerBuilder3.Append("{\trowd")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Warrant No\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Divident Year\b0\cell")
    '        '    headerBuilder3.Append("\row}")

    '        '    For Each dr3 As DataRow In dt3.Rows
    '        '        rowBuilder3.Append("{\trowd")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr3("Warrant No").ToString() & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr3("net_amount").ToString() & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr3("Fin Year").ToString() & "\cell")
    '        '        rowBuilder3.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE3>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '        '    ' Save to temp file
    '        '    Dim tempPath As String = Path.Combine(Path.GetTempPath(), "LOYAL IEPF Confirmation letter format_" & Guid.NewGuid().ToString() & ".rtf")
    '        '    File.WriteAllText(tempPath, rtfContent)

    '        '    ' Open file
    '        '    Dim p As New ProcessStartInfo(tempPath) With {
    '        '        .UseShellExecute = True
    '        '    }
    '        '    Process.Start(p)

    '        '    generate_btn.Enabled = True

    '        '    ' GetCompGid = 6 - Salzer Electronics Ltd
    '        'ElseIf GetStatus = "Approved" AndAlso GetCompGid = 6 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\SALZER IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String
    '        '    Dim amountinwords As String = NumberToWords(GetShareAmount)

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<DATE>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<INWARD NO>>", GetCompInwardNo)
    '        '    rtfContent = rtfContent.Replace("<<company name>>", GetCompanyName)
    '        '    rtfContent = rtfContent.Replace("<<folio no>>", GetFolioNo)
    '        '    rtfContent = rtfContent.Replace("<<shares>>", GetShareCount)
    '        '    rtfContent = rtfContent.Replace("<<RS>>", GetShareCount)
    '        '    rtfContent = rtfContent.Replace("<<AMOUNTINWORDS>>", amountinwords)


    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetHolderAddr)
    '        '        rtfContent = rtfContent.Replace("<<EMAIL>>", GetHolderEmail)
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetClaimantAddr)
    '        '        rtfContent = rtfContent.Replace("<<EMAIL>>", GetClaimantEmail)
    '        '    End If

    '        '    'Build table rows dynamically
    '        '    Dim dt As DataTable = GetTranDetails(GetFolioNo, GetCompGid) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Company\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Date Of Transfer\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b No.Of Shares Transferred To IEPF\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetCompanyName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("tran_date").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("no_of_shares").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Company\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Fin Year\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Warrant No\b0\cell")
    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt.Rows
    '        '        rowBuilder2.Append("{\trowd")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetCompanyName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & dr2("Fin Year").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr2("net_amount").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("Warrant No").ToString() & "\cell")
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    ' Save to temp file
    '        '    Dim tempPath As String = Path.Combine(Path.GetTempPath(), "SALZER IEPF Confirmation letter format_" & Guid.NewGuid().ToString() & ".rtf")
    '        '    File.WriteAllText(tempPath, rtfContent)

    '        '    ' Open file
    '        '    Dim p As New ProcessStartInfo(tempPath) With {
    '        '        .UseShellExecute = True
    '        '    }
    '        '    Process.Start(p)

    '        '    generate_btn.Enabled = True

    '        '    ' GetCompGid = 1 - KOVAI MEDICAL CENTER & HOSPITAL LTD
    '        'ElseIf GetStatus = "Approved" AndAlso GetCompGid = 1 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\KMCH IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<YEAR>>", DateTime.Now.ToString("yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<DATE>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<folio no>>", GetFolioNo)
    '        '    rtfContent = rtfContent.Replace("<<shares>>", GetShareCount)


    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetHolderAddr)
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetClaimantAddr)
    '        '    End If

    '        '    'Build table rows dynamically
    '        '    Dim dt As DataTable = GetDividendDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Fin Year\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Warrant No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Net Amount\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & dr("Fin Year").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("Warrant No").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("net_amount").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt3 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder3 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder3 As New System.Text.StringBuilder()
    '        '    Dim slNo As Integer = 1

    '        '    headerBuilder3.Append("{\trowd")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Sl No.\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Folio No\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Date Of Transfer\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b No Of Shares Transferred To IEPF\b0\cell")
    '        '    headerBuilder3.Append("\row}")

    '        '    For Each dr2 As DataRow In dt.Rows
    '        '        rowBuilder3.Append("{\trowd")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & slNo & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetFolioNo & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr2("tran_date").ToString() & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("no_of_shares").ToString() & "\cell")
    '        '        rowBuilder3.Append("\row}")
    '        '        slNo += 1
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE3>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '        '    ' Save to temp file
    '        '    Dim tempPath As String = Path.Combine(Path.GetTempPath(), "KMCH IEPF Confirmation letter format_" & Guid.NewGuid().ToString() & ".rtf")
    '        '    File.WriteAllText(tempPath, rtfContent)

    '        '    ' Open file
    '        '    Dim p As New ProcessStartInfo(tempPath) With {
    '        '        .UseShellExecute = True
    '        '    }
    '        '    Process.Start(p)

    '        '    generate_btn.Enabled = True

    '        'Else
    '        '    MessageBox.Show("No Letter Available", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        '    Exit Sub
    '        'End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error generating letter: " & ex.Message)
    '    End Try
    'End Sub

    'Private Sub generate_btn_Click(sender As Object, e As EventArgs) Handles generate_btn.Click
    '    Try
    '        Dim inexReasons As String = ""
    '        Dim remarks As String = remark_txt.Text

    '        If GetStatus = "Inex" Then
    '            'inexReasons = GetInexReasons(GetReqGid)

    '            'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\IEPF_INEX_Covering_Letter_Template.rtf"
    '            'Dim rtfContent As String

    '            'Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '            '    Using sr As New StreamReader(fs)
    '            '        rtfContent = sr.ReadToEnd()
    '            '    End Using
    '            'End Using

    '            '' Format reasons
    '            'Dim reasonsList = inexReasons.Split(New String() {" /* "}, StringSplitOptions.RemoveEmptyEntries)
    '            ''Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r) r.Trim()))
    '            'Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r, i) (i + 1).ToString() & ". " & r.Trim()))
    '            'rtfContent = rtfContent.Replace("<<InexReasons>>", reasonsFormatted)

    '            UpdateAdditionalRemarks(GetReqGid, remarks)
    '            inexReasons = GetInexReasons(GetReqGid)

    '            'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\New\IEPF Rejection Letter Format.rtf"
    '            Dim rtfContent As String

    '            Using fs As New FileStream(gsiepfRejectiontemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '                Using sr As New StreamReader(fs)
    '                    rtfContent = sr.ReadToEnd()
    '                End Using
    '            End Using

    '            ' Replace placeholders
    '            rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '            rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
    '            rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)
    '            rtfContent = rtfContent.Replace("<<inward_no>>", GetCompInwardNo)


    '            If GetReqType = "ShareHolder" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetHolderAddr))
    '            ElseIf GetReqType = "Claimant" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetClaimantAddr))
    '            Else
    '                rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetNomineeAddr))
    '            End If

    '            Dim reasonsList = inexReasons.Split(New String() {" /* "}, StringSplitOptions.RemoveEmptyEntries)
    '            'Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r) r.Trim()))
    '            Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r, i) (i + 1).ToString() & ". " & r.Trim()))

    '            rtfContent = rtfContent.Replace("<<inex_reasons>>", reasonsFormatted)

    '            ' DB Save
    '            Dim attachmentGid As Integer = 0
    '            Dim dbFileName As String = Path.GetFileName(gsiepfRejectiontemplatepath)
    '            Dim msg As String = ""
    '            Dim result As Integer

    '            ' First check if record exists
    '            result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

    '            If result > 0 Then
    '                ' Exists → Update
    '                result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
    '            Else
    '                ' New → Insert
    '                result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
    '            End If

    '            ' Only if DB success → save file
    '            If result = 1 AndAlso attachmentGid > 0 Then
    '                Dim localFolder As String = gsAttachmentPath
    '                If Not Directory.Exists(localFolder) Then
    '                    Directory.CreateDirectory(localFolder)
    '                End If

    '                ' Use attachment_gid as filename
    '                Dim localFileName As String = attachmentGid.ToString() & ".sta"
    '                Dim fullPath As String = Path.Combine(localFolder, localFileName)

    '                File.WriteAllText(fullPath, rtfContent)

    '                MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                ' Open file
    '                'Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
    '                'Process.Start(p)

    '                Dim outputFile As String = "C:\temp\IEPF CLAIM\Reject"

    '                ' If folder doesnot exists means create a directory folder
    '                If Not System.IO.Directory.Exists(outputFile) Then
    '                    System.IO.Directory.CreateDirectory(outputFile)
    '                End If

    '                outputFile = Path.ChangeExtension(outputFile + "\IEPF Rejection Letter Format.rtf", ".rtf")

    '                ' Read our HTML file a string.
    '                Dim htmlString As String = File.ReadAllText(gsiepfRejectiontemplatepath)

    '                ' Open the result for demonstration purposes.
    '                If Not String.IsNullOrEmpty(outputFile) Then
    '                    File.WriteAllText(outputFile, rtfContent)
    '                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
    '                End If

    '            Else
    '                MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            End If
    '        End If

    '        If GetStatus = "Approved" Then
    '            'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\New\Entitlement letter.rtf"
    '            Dim rtfContent As String

    '            Using fs As New FileStream(gsiepfApprovetemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '                Using sr As New StreamReader(fs)
    '                    rtfContent = sr.ReadToEnd()
    '                End Using
    '            End Using

    '            ' Replace placeholders
    '            rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '            rtfContent = rtfContent.Replace("<<year>>", DateTime.Now.ToString("yyyy"))
    '            rtfContent = rtfContent.Replace("<<share_count>>", GetShareCount)
    '            rtfContent = rtfContent.Replace("<<shareholder_name>>", GetShareHolderName)
    '            rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
    '            rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)

    '            Dim dtComp As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_code")
    '            Dim compCode As String = ""

    '            If dtComp IsNot Nothing AndAlso dtComp.Rows.Count > 0 Then
    '                compCode = dtComp.Rows(0)(0).ToString()   ' first column, first row
    '            End If

    '            rtfContent = rtfContent.Replace("<<comp_code>>", compCode)

    '            Dim dtCompSec As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_sec")
    '            Dim compSec As String = ""

    '            If dtCompSec IsNot Nothing AndAlso dtCompSec.Rows.Count > 0 Then
    '                compSec = dtCompSec.Rows(0)(0).ToString()   ' first column, first row
    '            End If

    '            rtfContent = rtfContent.Replace("<<comp_sec_name>>", compSec)


    '            If GetReqType = "ShareHolder" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetHolderAddr))
    '            ElseIf GetReqType = "Claimant" Then
    '                rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetClaimantAddr))
    '            Else
    '                rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
    '                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetNomineeAddr))
    '            End If

    '            ' Build table rows dynamically
    '            Dim dt As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "lessthan7years")
    '            Dim rowBuilder As New System.Text.StringBuilder()
    '            Dim headerBuilder As New System.Text.StringBuilder()

    '            'headerBuilder.Append("{\trowd\trqc")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872  \qc\b Folio Dpid\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3744  \qc\b Share Holder\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5616  \qc\b Warrent No\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7488  \qc\b Net Amount\b0\cell")
    '            'headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360  \qc\b Dividend Year\b0\cell")
    '            'headerBuilder.Append("\row}")

    '            'For Each dr As DataRow In dt.Rows
    '            '    rowBuilder.Append("{\trowd\trqc")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872  " & GetFolioNo & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3744  " & GetShareHolderName & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5616  " & dr("Warrant No").ToString() & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7488  " & dr("Net Amount").ToString() & "\cell")
    '            '    rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360  " & dr("Dividend Year").ToString() & "\cell")
    '            '    rowBuilder.Append("\row}")
    '            'Next

    '            headerBuilder.Append("{\trowd\trqc" &
    '                                 "\trgaph108\trleft0" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872\qc\b Folio Dpid\b0\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500\qc\b Share Holder\b0\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6500\qc\b Warrant No\b0\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx8000\qc\b Net Amount\b0\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360\qc\b Dividend Year\b0\cell" &
    '                    "\row}")

    '            For Each dr As DataRow In dt.Rows
    '                rowBuilder.Append("{\trowd\trqc" &
    '                                  "\trgaph108\trleft0" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872  " & GetFolioNo & "\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500  " & GetShareHolderName & "\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6500  " & dr("Warrant No").ToString() & "\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx8000  " & dr("Net Amount").ToString() & "\cell" &
    '                    "\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360  " & dr("Dividend Year").ToString() & "\cell" &
    '                    "\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '            ' Build table rows dynamically
    '            Dim dt2 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "annexure")
    '            Dim rowBuilder2 As New System.Text.StringBuilder()
    '            Dim headerBuilder2 As New System.Text.StringBuilder()

    '            headerBuilder2.Append("{\trowd\trqc")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872 \qc\b Folio Dpid\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 \qc\b Share Holder\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6500 \qc\b Warrent No\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx8000 \qc\b Net Amount\b0\cell")
    '            headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360 \qc\b Dividend Year\b0\cell")
    '            headerBuilder2.Append("\row}")

    '            For Each dr2 As DataRow In dt2.Rows
    '                rowBuilder2.Append("{\trowd\trqc")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872 " & GetFolioNo & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 " & GetShareHolderName & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6500 " & dr2("Warrant No").ToString() & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx8000 " & dr2("Net Amount").ToString() & "\cell")
    '                rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360 " & dr2("Dividend Year").ToString() & "\cell")
    '                rowBuilder2.Append("\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table4>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '            ' Build table rows dynamically
    '            Dim dt3 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_iepf")
    '            Dim rowBuilder3 As New System.Text.StringBuilder()
    '            Dim headerBuilder3 As New System.Text.StringBuilder()

    '            headerBuilder3.Append("{\trowd\trqc")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872 \qc\b Folio Dpid\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 \qc\b Share Holder\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6500 \qc\b Warrent No\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx8000 \qc\b Net Amount\b0\cell")
    '            headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360 \qc\b Dividend Year\b0\cell")
    '            headerBuilder3.Append("\row}")

    '            For Each dr3 As DataRow In dt3.Rows
    '                rowBuilder3.Append("{\trowd\trqc")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1872 " & GetFolioNo & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 " & GetShareHolderName & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6500 " & dr3("Warrant No").ToString() & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx8000 " & dr3("Net Amount").ToString() & "\cell")
    '                rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9360 " & dr3("Dividend Year").ToString() & "\cell")
    '                rowBuilder3.Append("\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table2>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '            ' Build table rows dynamically
    '            Dim dt4 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_details")
    '            Dim rowBuilder4 As New System.Text.StringBuilder()
    '            Dim headerBuilder4 As New System.Text.StringBuilder()

    '            headerBuilder4.Append("{\trowd\trqc")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2000 \qc\b Folio Dpid\b0\cell")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 \qc\b Share Holder\b0\cell")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7500 \qc\b Date Of Transfer\b0\cell")
    '            headerBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 \qc\b No Of Shares Transferred To IEPF\b0\cell")
    '            headerBuilder4.Append("\row}")

    '            For Each dr4 As DataRow In dt4.Rows
    '                rowBuilder4.Append("{\trowd\trqc")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2000 " & GetFolioNo & "\cell")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 " & GetShareHolderName & "\cell")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7500 " & dr4("Transfer Date").ToString() & "\cell")
    '                rowBuilder4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 " & dr4("No of Shares").ToString() & "\cell")
    '                rowBuilder4.Append("\row}")
    '            Next

    '            rtfContent = rtfContent.Replace("<<table3>>", headerBuilder4.ToString() & rowBuilder4.ToString())

    '            ' DB Save
    '            Dim attachmentGid As Integer = 0
    '            Dim dbFileName As String = Path.GetFileName(gsiepfApprovetemplatepath)
    '            Dim msg As String = ""
    '            Dim result As Integer

    '            ' First check if record exists
    '            result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

    '            If result > 0 Then
    '                ' Exists → Update
    '                result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
    '            Else
    '                ' New → Insert
    '                result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
    '            End If

    '            ' Only if DB success → save file
    '            If result = 1 AndAlso attachmentGid > 0 Then
    '                Dim localFolder As String = gsAttachmentPath
    '                If Not Directory.Exists(localFolder) Then
    '                    Directory.CreateDirectory(localFolder)
    '                End If

    '                ' Use attachment_gid as filename
    '                Dim localFileName As String = attachmentGid.ToString() & ".sta"
    '                Dim fullPath As String = Path.Combine(localFolder, localFileName)

    '                File.WriteAllText(fullPath, rtfContent)

    '                MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                Dim outputFile As String = "C:\temp\IEPF CLAIM\Approve"

    '                ' If folder doesnot exists means create a directory folder
    '                If Not System.IO.Directory.Exists(outputFile) Then
    '                    System.IO.Directory.CreateDirectory(outputFile)
    '                End If

    '                outputFile = Path.ChangeExtension(outputFile + "\Entitlement letter.rtf", ".rtf")

    '                ' Read our HTML file a string.
    '                Dim htmlString As String = File.ReadAllText(gsiepfApprovetemplatepath)

    '                ' Open the result for demonstration purposes.
    '                If Not String.IsNullOrEmpty(outputFile) Then
    '                    File.WriteAllText(outputFile, rtfContent)
    '                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
    '                End If

    '                ' Open file // 13-10-2025
    '                'Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
    '                'Process.Start(p)

    '            Else
    '                MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            End If
    '        End If

    '        '' GetCompGid = 2 - Bimetal Bearings Limited
    '        'If GetStatus = "Approved" AndAlso GetCompGid = 2 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\BIMETAL IEPF Confirmation letter format.rtf"
    '        '    'BimetalConfirmationLetter(templatePath, GetFolioNo, GetReqType, GetShareHolderName, GetClaimantName, GetShareCount)
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<Date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<sharecount>>", GetShareCount)

    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<type>>", "ShareHolder")
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<type>>", "Claimant")
    '        '    End If

    '        '    ' Build table rows dynamically
    '        '    Dim dt As DataTable = GetCertDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2000 \b Folio No.\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 \b Share Certificate No.\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7500 \b Distinctive Nos.\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 \b No. of shares\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2000 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx4500 " & dr("cert_no").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7500 " & dr("dist_series").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 " & dr("share_count").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    ' Replace placeholder in RTF with generated rows
    '        '    rtfContent = rtfContent.Replace("<<ROWPLACEHOLDER>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3023 \b Financial year\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6046 \b Date of dividend\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 \b Dividend Amount\b0\cell")
    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt2.Rows
    '        '        rowBuilder2.Append("{\trowd")

    '        '        ' First column (Folio No.)
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10") ' top border
    '        '        rowBuilder2.Append("\clbrdrl\brdrs\brdrw10") ' left border
    '        '        rowBuilder2.Append("\clbrdrb\brdrs\brdrw10") ' bottom border
    '        '        rowBuilder2.Append("\clbrdrr\brdrs\brdrw10") ' right border
    '        '        rowBuilder2.Append("\cellx3023 " & dr2("Fin Year").ToString() & "\cell")

    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6046 " & dr2("Dividend Date").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9070 " & dr2("Dividend Amount").ToString() & "\cell")
    '        '        rowBuilder2.Append("\row}")
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    ' Replace placeholder in RTF with generated rows
    '        '    rtfContent = rtfContent.Replace("<<ROWPLACEHOLDER2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    ' DB Save
    '        '    Dim attachmentGid As Integer = 0
    '        '    Dim dbFileName As String = Path.GetFileName(templatePath)
    '        '    Dim msg As String = ""
    '        '    Dim result As Integer

    '        '    ' First check if record exists
    '        '    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

    '        '    If result > 0 Then
    '        '        ' Exists → Update
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
    '        '    Else
    '        '        ' New → Insert
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
    '        '    End If

    '        '    ' Only if DB success → save file
    '        '    If result = 1 AndAlso attachmentGid > 0 Then
    '        '        Dim localFolder As String = gsAttachmentPath
    '        '        If Not Directory.Exists(localFolder) Then
    '        '            Directory.CreateDirectory(localFolder)
    '        '        End If

    '        '        ' Use attachment_gid as filename
    '        '        Dim localFileName As String = attachmentGid.ToString() & ".sta"
    '        '        Dim fullPath As String = Path.Combine(localFolder, localFileName)

    '        '        File.WriteAllText(fullPath, rtfContent)

    '        '        MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        '        ' Open file
    '        '        Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
    '        '        Process.Start(p)
    '        '    Else
    '        '        MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        '    End If
    '        'End If

    '        '' GetCompGid = 11 - Butterfly Gandhimathi Appliances Ltd
    '        'If GetStatus = "Approved" AndAlso GetCompGid = 11 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\BUTTERFLY  IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<Date>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<inwardno>>", GetCompInwardNo)

    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
    '        '    End If

    '        '    ' Build table rows dynamically
    '        '    Dim dt As DataTable = GetCertDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Share Certificate No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b No. of shares\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Distinctive Nos\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr("cert_no").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("share_count").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("dist_series").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 \b Company\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 \b Fin Year Code\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 \b Folio No\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 \b Holder Name\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 \b No. of shares\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 \b Net Amount\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Warrant No\b0\cell")
    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt2.Rows
    '        '        rowBuilder2.Append("{\trowd")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 " & GetCompanyName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 " & dr2("Fin Year").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 " & GetFolioNo & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 " & GetShareHolderName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 " & dr2("Share Count").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 " & dr2("net_amount").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("Warrant No").ToString() & "\cell")
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    'Dim dt3 As DataTable = GetDividendDetails(GetFolioNo) ' your stored procedure call
    '        '    'Dim rowBuilder3 As New System.Text.StringBuilder()
    '        '    'Dim headerBuilder3 As New System.Text.StringBuilder()

    '        '    'headerBuilder3.Append("{\trowd")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Warrant No\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Divident Year\b0\cell")
    '        '    'headerBuilder3.Append("\row}")

    '        '    'For Each dr3 As DataRow In dt3.Rows
    '        '    '    rowBuilder3.Append("{\trowd")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr3("Warrant No").ToString() & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr3("net_amount").ToString() & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr3("Fin Year").ToString() & "\cell")
    '        '    '    rowBuilder3.Append("\row}")
    '        '    'Next

    '        '    'rtfContent = rtfContent.Replace("<<TABLE3>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '        '    ' DB Save
    '        '    Dim attachmentGid As Integer = 0
    '        '    Dim dbFileName As String = Path.GetFileName(templatePath)
    '        '    Dim msg As String = ""
    '        '    Dim result As Integer

    '        '    ' First check if record exists
    '        '    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

    '        '    If result > 0 Then
    '        '        ' Exists → Update
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
    '        '    Else
    '        '        ' New → Insert
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
    '        '    End If

    '        '    ' Only if DB success → save file
    '        '    If result = 1 AndAlso attachmentGid > 0 Then
    '        '        Dim localFolder As String = gsAttachmentPath
    '        '        If Not Directory.Exists(localFolder) Then
    '        '            Directory.CreateDirectory(localFolder)
    '        '        End If

    '        '        ' Use attachment_gid as filename
    '        '        Dim localFileName As String = attachmentGid.ToString() & ".sta"
    '        '        Dim fullPath As String = Path.Combine(localFolder, localFileName)

    '        '        File.WriteAllText(fullPath, rtfContent)

    '        '        MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        '        ' Open file
    '        '        Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
    '        '        Process.Start(p)
    '        '    Else
    '        '        MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        '    End If
    '        'End If

    '        '' GetCompGid = 3 - LOYAL TEXTILE MILLS LTD
    '        'If GetStatus = "Approved" AndAlso GetCompGid = 3 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\LOYAL IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<DATE>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<INWARD NO>>", GetCompInwardNo)

    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<TYPE>>", "ShareHolder")
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<TYPE>>", "Claimant")
    '        '    End If

    '        '    ' Build table rows dynamically
    '        '    Dim dt As DataTable = GetCertDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Share Certificate No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b No. of shares\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Distinctive Nos\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr("cert_no").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("share_count").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("dist_series").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 \b Company\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 \b Fin Year Code\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 \b Folio No\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 \b Holder Name\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 \b No. of shares\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 \b Net Amount\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Warrant No\b0\cell")
    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt2.Rows
    '        '        rowBuilder2.Append("{\trowd")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1290 " & GetCompanyName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx2580 " & dr2("Fin Year").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3870 " & GetFolioNo & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5160 " & GetShareHolderName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx6450 " & dr2("Share Count").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7740 " & dr2("net_amount").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("Warrant No").ToString() & "\cell")
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    'Dim dt3 As DataTable = GetDividendDetails(GetFolioNo) ' your stored procedure call
    '        '    'Dim rowBuilder3 As New System.Text.StringBuilder()
    '        '    'Dim headerBuilder3 As New System.Text.StringBuilder()

    '        '    'headerBuilder3.Append("{\trowd")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Folio No\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Holder Name\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Warrant No\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '        '    'headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Divident Year\b0\cell")
    '        '    'headerBuilder3.Append("\row}")

    '        '    'For Each dr3 As DataRow In dt3.Rows
    '        '    '    rowBuilder3.Append("{\trowd")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetFolioNo & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetShareHolderName & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & dr3("Warrant No").ToString() & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr3("net_amount").ToString() & "\cell")
    '        '    '    rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr3("Fin Year").ToString() & "\cell")
    '        '    '    rowBuilder3.Append("\row}")
    '        '    'Next

    '        '    'rtfContent = rtfContent.Replace("<<TABLE3>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '        '    ' DB Save
    '        '    Dim attachmentGid As Integer = 0
    '        '    Dim dbFileName As String = Path.GetFileName(templatePath)
    '        '    Dim msg As String = ""
    '        '    Dim result As Integer

    '        '    ' First check if record exists
    '        '    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

    '        '    If result > 0 Then
    '        '        ' Exists → Update
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
    '        '    Else
    '        '        ' New → Insert
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
    '        '    End If

    '        '    ' Only if DB success → save file
    '        '    If result = 1 AndAlso attachmentGid > 0 Then
    '        '        Dim localFolder As String = gsAttachmentPath
    '        '        If Not Directory.Exists(localFolder) Then
    '        '            Directory.CreateDirectory(localFolder)
    '        '        End If

    '        '        ' Use attachment_gid as filename
    '        '        Dim localFileName As String = attachmentGid.ToString() & ".sta"
    '        '        Dim fullPath As String = Path.Combine(localFolder, localFileName)

    '        '        File.WriteAllText(fullPath, rtfContent)

    '        '        MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        '        ' Open file
    '        '        Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
    '        '        Process.Start(p)
    '        '    Else
    '        '        MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        '    End If
    '        'End If

    '        '' GetCompGid = 6 - Salzer Electronics Ltd
    '        'If GetStatus = "Approved" AndAlso GetCompGid = 6 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\SALZER IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String
    '        '    Dim amountinwords As String = NumberToWords(GetShareAmount)

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<DATE>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<INWARD NO>>", GetCompInwardNo)
    '        '    rtfContent = rtfContent.Replace("<<company name>>", GetCompanyName)
    '        '    rtfContent = rtfContent.Replace("<<folio no>>", GetFolioNo)
    '        '    rtfContent = rtfContent.Replace("<<shares>>", GetShareCount)
    '        '    rtfContent = rtfContent.Replace("<<RS>>", GetShareCount)
    '        '    rtfContent = rtfContent.Replace("<<AMOUNTINWORDS>>", amountinwords)


    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetHolderAddr)
    '        '        rtfContent = rtfContent.Replace("<<EMAIL>>", GetHolderEmail)
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetClaimantAddr)
    '        '        rtfContent = rtfContent.Replace("<<EMAIL>>", GetClaimantEmail)
    '        '    End If

    '        '    'Build table rows dynamically
    '        '    Dim dt As DataTable = GetCertDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Company\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Date Of Transfer\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b No.Of Shares Transfer To IEPF\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetCompanyName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & "-" & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & "-" & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt2 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder2 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder2 As New System.Text.StringBuilder()

    '        '    headerBuilder2.Append("{\trowd")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Company\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Fin Year\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Net Amount\b0\cell")
    '        '    headerBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Warrant No\b0\cell")
    '        '    headerBuilder2.Append("\row}")

    '        '    For Each dr2 As DataRow In dt.Rows
    '        '        rowBuilder2.Append("{\trowd")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & GetCompanyName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & dr2("Fin Year").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr2("net_amount").ToString() & "\cell")
    '        '        rowBuilder2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("Warrant No").ToString() & "\cell")
    '        '        rowBuilder2.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE2>>", headerBuilder2.ToString() & rowBuilder2.ToString())

    '        '    ' DB Save
    '        '    Dim attachmentGid As Integer = 0
    '        '    Dim dbFileName As String = Path.GetFileName(templatePath)
    '        '    Dim msg As String = ""
    '        '    Dim result As Integer

    '        '    ' First check if record exists
    '        '    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

    '        '    If result > 0 Then
    '        '        ' Exists → Update
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
    '        '    Else
    '        '        ' New → Insert
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
    '        '    End If

    '        '    ' Only if DB success → save file
    '        '    If result = 1 AndAlso attachmentGid > 0 Then
    '        '        Dim localFolder As String = gsAttachmentPath
    '        '        If Not Directory.Exists(localFolder) Then
    '        '            Directory.CreateDirectory(localFolder)
    '        '        End If

    '        '        ' Use attachment_gid as filename
    '        '        Dim localFileName As String = attachmentGid.ToString() & ".sta"
    '        '        Dim fullPath As String = Path.Combine(localFolder, localFileName)

    '        '        File.WriteAllText(fullPath, rtfContent)

    '        '        MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        '        ' Open file
    '        '        Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
    '        '        Process.Start(p)
    '        '    Else
    '        '        MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        '    End If
    '        'End If

    '        '' GetCompGid = 1 - KOVAI MEDICAL CENTER & HOSPITAL LTD
    '        'If GetStatus = "Approved" AndAlso GetCompGid = 1 Then
    '        '    Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\KMCH IEPF Confirmation letter format.rtf"
    '        '    Dim rtfContent As String

    '        '    Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '        '        Using sr As New StreamReader(fs)
    '        '            rtfContent = sr.ReadToEnd()
    '        '        End Using
    '        '    End Using

    '        '    ' Replace placeholders
    '        '    rtfContent = rtfContent.Replace("<<YEAR>>", DateTime.Now.ToString("yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<DATE>>", DateTime.Now.ToString("dd-MM-yyyy"))
    '        '    rtfContent = rtfContent.Replace("<<folio no>>", GetFolioNo)
    '        '    rtfContent = rtfContent.Replace("<<shares>>", GetShareCount)


    '        '    If GetReqType = "Nominee" Or GetReqType = "ShareHolder" Then
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetShareHolderName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetHolderAddr)
    '        '    Else
    '        '        rtfContent = rtfContent.Replace("<<NAME>>", GetClaimantName)
    '        '        rtfContent = rtfContent.Replace("<<ADDRESS>>", GetClaimantAddr)
    '        '    End If

    '        '    'Build table rows dynamically
    '        '    Dim dt As DataTable = GetDividendDetails(GetFolioNo) ' your stored procedure call
    '        '    Dim rowBuilder As New System.Text.StringBuilder()
    '        '    Dim headerBuilder As New System.Text.StringBuilder()

    '        '    headerBuilder.Append("{\trowd")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Fin Year\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Folio No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Warrant No\b0\cell")
    '        '    headerBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b Net Amount\b0\cell")
    '        '    headerBuilder.Append("\row}")

    '        '    For Each dr As DataRow In dt.Rows
    '        '        rowBuilder.Append("{\trowd")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & dr("Fin Year").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetFolioNo & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr("Warrant No").ToString() & "\cell")
    '        '        rowBuilder.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr("net_amount").ToString() & "\cell")
    '        '        rowBuilder.Append("\row}")
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE1>>", headerBuilder.ToString() & rowBuilder.ToString())

    '        '    Dim dt3 As DataTable = GetDividendDetails(GetFolioNo)
    '        '    Dim rowBuilder3 As New System.Text.StringBuilder()
    '        '    Dim headerBuilder3 As New System.Text.StringBuilder()
    '        '    Dim slNo As Integer = 1

    '        '    headerBuilder3.Append("{\trowd")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 \b Sl No.\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 \b Folio No\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 \b Holder Name\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 \b Date Of Transfer\b0\cell")
    '        '    headerBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 \b No Of Shares Transferred To IEPF\b0\cell")
    '        '    headerBuilder3.Append("\row}")

    '        '    For Each dr2 As DataRow In dt.Rows
    '        '        rowBuilder3.Append("{\trowd")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx1806 " & slNo & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx3612 " & GetFolioNo & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx5418 " & GetShareHolderName & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx7224 " & dr2("tran_date").ToString() & "\cell")
    '        '        rowBuilder3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\cellx9030 " & dr2("no_of_shares").ToString() & "\cell")
    '        '        rowBuilder3.Append("\row}")
    '        '        slNo += 1
    '        '    Next

    '        '    rtfContent = rtfContent.Replace("<<TABLE3>>", headerBuilder3.ToString() & rowBuilder3.ToString())

    '        '    ' DB Save
    '        '    Dim attachmentGid As Integer = 0
    '        '    Dim dbFileName As String = Path.GetFileName(templatePath)
    '        '    Dim msg As String = ""
    '        '    Dim result As Integer

    '        '    ' First check if record exists
    '        '    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

    '        '    If result > 0 Then
    '        '        ' Exists → Update
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
    '        '    Else
    '        '        ' New → Insert
    '        '        result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
    '        '    End If

    '        '    ' Only if DB success → save file
    '        '    If result = 1 AndAlso attachmentGid > 0 Then
    '        '        Dim localFolder As String = gsAttachmentPath
    '        '        If Not Directory.Exists(localFolder) Then
    '        '            Directory.CreateDirectory(localFolder)
    '        '        End If

    '        '        ' Use attachment_gid as filename
    '        '        Dim localFileName As String = attachmentGid.ToString() & ".sta"
    '        '        Dim fullPath As String = Path.Combine(localFolder, localFileName)

    '        '        File.WriteAllText(fullPath, rtfContent)

    '        '        MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        '        ' Open file
    '        '        Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
    '        '        Process.Start(p)
    '        '    Else
    '        'MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        '    End If
    '        'End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error generating letter: " & ex.Message)
    '    End Try
    'End Sub

    Private Function GetInexReasons(req_gid As Integer) As String
        Dim reasons As New List(Of String)()

        Using cmd As New MySqlCommand("pr_get_iepfinexreasonslist", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_req_gid", req_gid)

            Using rdr As MySqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    If Not rdr.IsDBNull(rdr.GetOrdinal("inexreason_description")) Then
                        reasons.Add(rdr("inexreason_description").ToString())
                    End If
                End While
            End Using
        End Using

        Return String.Join(vbCrLf & " /* ", reasons)
    End Function

    Public Function GetAdditionalRemarks(reqGid As Long) As String
        Dim remarks As String = String.Empty
        Try
            Using cmd As New MySqlCommand("pr_get_iepfadditionalremarks", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("in_req_gid", reqGid)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        remarks = reader("remarks").ToString()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching remarks: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return remarks
    End Function

    Public Function UpdateAdditionalRemarks(reqGid As Long, remarks As String) As Boolean
        Dim success As Boolean = False
        Try
            Using cmd As New MySqlCommand("pr_upd_iepfadditionalremarks", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("in_req_gid", reqGid)
                cmd.Parameters.AddWithValue("in_remarks", remarks)

                cmd.ExecuteNonQuery()
                success = True
            End Using

        Catch ex As Exception
            MessageBox.Show("Error updating remarks: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return success
    End Function

    Public Function ManageAttachment(ByRef attachmentGid As Integer,
                                 inwardGid As Integer,
                                 attachmentTypeGid As Integer,
                                 fileName As String,
                                 action As String,
                                 actionBy As String,
                                 ByRef outMsg As String,
                                 Optional ByRef dt As DataTable = Nothing) As Integer

        Dim outResult As Integer = 0

        Try
            Using cmd As New MySqlCommand("pr_sta_trn_tattachment", gOdbcConn)
                cmd.CommandType = CommandType.StoredProcedure

                ' INOUT parameter
                Dim pAttachmentGid As New MySqlParameter("in_attachment_gid", MySqlDbType.Int32)
                pAttachmentGid.Direction = ParameterDirection.InputOutput
                pAttachmentGid.Value = attachmentGid
                cmd.Parameters.Add(pAttachmentGid)

                ' Other IN params
                cmd.Parameters.AddWithValue("in_inward_gid", inwardGid)
                cmd.Parameters.AddWithValue("in_attachmenttype_gid", attachmentTypeGid)
                cmd.Parameters.AddWithValue("in_file_name", fileName)
                cmd.Parameters.AddWithValue("in_action", action)
                cmd.Parameters.AddWithValue("in_action_by", actionBy)

                ' OUT params
                Dim pOutResult As New MySqlParameter("out_result", MySqlDbType.Int32)
                pOutResult.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pOutResult)

                Dim pOutMsg As New MySqlParameter("out_msg", MySqlDbType.Text)
                pOutMsg.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pOutMsg)

                If action.ToUpper() = "SELECT" Then
                    ' SELECT → fill DataTable
                    Dim adapter As New MySqlDataAdapter(cmd)
                    dt = New DataTable()
                    adapter.Fill(dt)
                Else
                    ' INSERT / UPDATE / DELETE
                    cmd.ExecuteNonQuery()
                End If

                ' Update OUT values
                'attachmentGid = Convert.ToInt32(pAttachmentGid.Value)
                'outResult = Convert.ToInt32(If(IsDBNull(pOutResult.Value), 0, pOutResult.Value))
                'outMsg = If(IsDBNull(pOutMsg.Value), "", pOutMsg.Value.ToString())

                attachmentGid = If(IsDBNull(pAttachmentGid.Value), 0, Convert.ToInt32(pAttachmentGid.Value))
                outResult = If(IsDBNull(pOutResult.Value), 0, Convert.ToInt32(pOutResult.Value))
                outMsg = If(IsDBNull(pOutMsg.Value), "", pOutMsg.Value.ToString())
            End Using

        Catch ex As Exception
            outResult = 0
            outMsg = "Error: " & ex.Message
        End Try

        Return outResult
    End Function

    Private Function GetCertDetails(folioNo As String) As DataTable
        Dim dt As New DataTable()

        Using cmd As New MySqlCommand("pr_get_avblechk_iepf", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_folio_no", folioNo)
            cmd.Parameters.AddWithValue("in_inward_no", "")
            cmd.Parameters.AddWithValue("in_comp_gid", GetCompGid)
            cmd.Parameters.AddWithValue("in_action", "details")

            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        End Using

        Return dt
    End Function

    Private Function Getletterdetails(foliogid As String, compgid As Integer, action As String) As DataTable
        Dim dt As New DataTable()

        Using cmd As New MySqlCommand("pr_get_iepfgenerateletterdtl", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_folio_gid", foliogid)
            cmd.Parameters.AddWithValue("in_comp_gid", compgid)
            cmd.Parameters.AddWithValue("in_tran_type", "M")
            cmd.Parameters.AddWithValue("in_div_status", "U")
            cmd.Parameters.AddWithValue("in_action", action)

            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        End Using

        Return dt
    End Function

    Private Function GetDividendDetails(foliogid As Integer, compgid As Integer) As DataTable
        Dim dt As New DataTable()

        Using cmd As New MySqlCommand("pr_get_dividentdetails", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_folio_gid", foliogid)
            cmd.Parameters.AddWithValue("in_comp_gid", compgid)

            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        End Using

        Return dt
    End Function

    Public Function NumberToWords(ByVal number As Decimal) As String
        Dim integerPart As Long = Math.Truncate(number)
        Dim decimalPart As Integer = CInt((number - integerPart) * 100)

        Dim words As String = ConvertIntegerToWords(integerPart)

        If decimalPart > 0 Then
            words &= " and " & ConvertIntegerToWords(decimalPart) & " Paise"
        End If

        Return words
    End Function

    Private Function ConvertIntegerToWords(ByVal number As Long) As String
        Dim units() As String = {"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
            "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}

        Dim tens() As String = {"", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}

        If number < 20 Then
            Return units(number)
        ElseIf number < 100 Then
            Return tens(number \ 10) & If(number Mod 10 > 0, " " & units(number Mod 10), "")
        ElseIf number < 1000 Then
            Return units(number \ 100) & " Hundred" & If(number Mod 100 > 0, " " & ConvertIntegerToWords(number Mod 100), "")
        ElseIf number < 1000000 Then
            Return ConvertIntegerToWords(number \ 1000) & " Thousand" & If(number Mod 1000 > 0, " " & ConvertIntegerToWords(number Mod 1000), "")
        ElseIf number < 1000000000 Then
            Return ConvertIntegerToWords(number \ 1000000) & " Million" & If(number Mod 1000000 > 0, " " & ConvertIntegerToWords(number Mod 1000000), "")
        Else
            Return ConvertIntegerToWords(number \ 1000000000) & " Billion" & If(number Mod 1000000000 > 0, " " & ConvertIntegerToWords(number Mod 1000000000), "")
        End If
    End Function

    Private Function GetTranDetails(foliogid As Long, comp_gid As Integer) As DataTable
        Dim dt As New DataTable()

        Using cmd As New MySqlCommand("pr_get_iepftransferdetails", gOdbcConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("in_foliono", foliogid)
            cmd.Parameters.AddWithValue("in_comp_gid", comp_gid)

            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        End Using

        Return dt
    End Function

    Private Function FormatAddressForRtf(rawAddr As String) As String
        'If String.IsNullOrEmpty(rawAddr) Then
        '    Return ""
        'End If

        'Dim addr As String = rawAddr

        '' Replace "|" with line breaks
        'addr = addr.Replace("|", vbLf)

        '' Normalize CRLF, CR → LF
        'addr = addr.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)

        '' Convert LF to RTF \line
        'addr = addr.Replace(vbLf, "\line ")

        'Return addr

        If String.IsNullOrWhiteSpace(rawAddr) Then Return ""

        ' Normalize input
        Dim addr As String = rawAddr.Trim()
        addr = addr.Replace("|", vbLf)
        addr = addr.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)

        ' Split each line and format it properly for RTF
        Dim lines() As String = addr.Split(New String() {vbLf}, StringSplitOptions.RemoveEmptyEntries)

        Dim sb As New StringBuilder()
        sb.Append("{\pard\li720") ' left indent 0.5 inch (optional)

        For i As Integer = 0 To lines.Length - 1
            Dim lineText As String = lines(i).Trim()

            ' Escape any RTF control characters inside content
            lineText = lineText.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}")

            If i > 0 Then sb.Append("\line ")
            sb.Append(lineText)
        Next

        sb.Append("\par}") ' close paragraph group

        Return sb.ToString()
    End Function

    Private Sub preview_btn_Click(sender As Object, e As EventArgs) Handles preview_btn.Click
        Try
            'Dim reqGid As Integer = Convert.ToInt32(dgv_covering.Rows(e.RowIndex).Cells("req_gid").Value)
            Dim inexReasons As String = ""
            Dim remarks As String = remark_txt.Text

            If GetStatus = "Inex" Then
                UpdateAdditionalRemarks(GetReqGid, remarks)
                inexReasons = GetInexReasons(GetReqGid)

                Dim rtfContent As String

                Using fs As New FileStream(gsiepfRejectiontemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Using sr As New StreamReader(fs)
                        rtfContent = sr.ReadToEnd()
                    End Using
                End Using

                ' Replace placeholders
                rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
                rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
                rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)
                rtfContent = rtfContent.Replace("<<inward_no>>", GetCompInwardNo)

                If GetReqType = "ShareHolder" Then
                    rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetHolderAddr))
                ElseIf GetReqType = "Claimant" Then
                    rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetClaimantAddr))
                Else
                    rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetNomineeAddr))
                End If

                Dim reasonsList = inexReasons.Split(New String() {" /* "}, StringSplitOptions.RemoveEmptyEntries)
                'Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r) r.Trim()))
                Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r, i) (i + 1).ToString() & ". " & r.Trim()))

                rtfContent = rtfContent.Replace("<<inex_reasons>>", reasonsFormatted)

                'Dim tempPath As String = Path.Combine(Path.GetTempPath(), "IEPF Rejection Letter Format_" & Guid.NewGuid().ToString() & ".rtf")
                'Dim rejpathname As String = "IEPF CLAIM\Reject"
                'Dim tempPath As String = Path.Combine(gsiepfRejectiontemplatepath, rejpathname, "IEPF Rejection Letter Format_" & Guid.NewGuid().ToString() & ".rtf")
                'File.WriteAllText(tempPath, rtfContent)

                Dim outputFile As String = "C:\temp\IEPF CLAIM\Reject"

                ' If folder doesnot exists means create a directory folder
                If Not System.IO.Directory.Exists(outputFile) Then
                    System.IO.Directory.CreateDirectory(outputFile)
                End If

                outputFile = Path.ChangeExtension(outputFile + "\IEPF Rejection Letter Format.rtf", ".rtf")

                ' Read our HTML file a string.
                Dim htmlString As String = File.ReadAllText(gsiepfRejectiontemplatepath)

                ' Open the result for demonstration purposes.
                If Not String.IsNullOrEmpty(outputFile) Then
                    File.WriteAllText(outputFile, rtfContent)
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
                End If

                'Dim p As New ProcessStartInfo(tempPath) With {
                '    .UseShellExecute = True
                '}
                'Process.Start(p)

                generate_btn.Enabled = True
            Else
                MessageBox.Show("No Letter Available", "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show("Error generating letter: " & ex.Message)
        End Try
    End Sub

    Private Sub generate_btn_Click(sender As Object, e As EventArgs) Handles generate_btn.Click
        Try
            Dim inexReasons As String = ""
            Dim remarks As String = remark_txt.Text

            If GetStatus = "Inex" Then

                UpdateAdditionalRemarks(GetReqGid, remarks)
                inexReasons = GetInexReasons(GetReqGid)

                'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\New\IEPF Rejection Letter Format.rtf"
                Dim rtfContent As String

                Using fs As New FileStream(gsiepfRejectiontemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Using sr As New StreamReader(fs)
                        rtfContent = sr.ReadToEnd()
                    End Using
                End Using

                ' Replace placeholders
                rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
                rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
                rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)
                rtfContent = rtfContent.Replace("<<inward_no>>", GetCompInwardNo)


                If GetReqType = "ShareHolder" Then
                    rtfContent = rtfContent.Replace("<<name>>", GetShareHolderName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetHolderAddr))
                ElseIf GetReqType = "Claimant" Then
                    rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetClaimantAddr))
                Else
                    rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetNomineeAddr))
                End If

                Dim reasonsList = inexReasons.Split(New String() {" /* "}, StringSplitOptions.RemoveEmptyEntries)
                'Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r) r.Trim()))
                Dim reasonsFormatted As String = String.Join("\par ", reasonsList.Select(Function(r, i) (i + 1).ToString() & ". " & r.Trim()))

                rtfContent = rtfContent.Replace("<<inex_reasons>>", reasonsFormatted)

                ' DB Save
                Dim attachmentGid As Integer = 0
                Dim dbFileName As String = Path.GetFileName(gsiepfRejectiontemplatepath)
                Dim msg As String = ""
                Dim result As Integer

                ' First check if record exists
                result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

                If result > 0 Then
                    ' Exists → Update
                    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
                Else
                    ' New → Insert
                    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
                End If

                ' Only if DB success → save file
                If result = 1 AndAlso attachmentGid > 0 Then
                    Dim localFolder As String = gsAttachmentPath
                    If Not Directory.Exists(localFolder) Then
                        Directory.CreateDirectory(localFolder)
                    End If

                    ' Use attachment_gid as filename
                    Dim localFileName As String = attachmentGid.ToString() & ".sta"
                    Dim fullPath As String = Path.Combine(localFolder, localFileName)

                    File.WriteAllText(fullPath, rtfContent)

                    MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Open file
                    'Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
                    'Process.Start(p)

                    Dim outputFile As String = "C:\temp\IEPF CLAIM\Reject"

                    ' If folder doesnot exists means create a directory folder
                    If Not System.IO.Directory.Exists(outputFile) Then
                        System.IO.Directory.CreateDirectory(outputFile)
                    End If

                    outputFile = Path.ChangeExtension(outputFile + "\IEPF Rejection Letter Format.rtf", ".rtf")

                    ' Read our HTML file a string.
                    Dim htmlString As String = File.ReadAllText(gsiepfRejectiontemplatepath)

                    ' Open the result for demonstration purposes.
                    If Not String.IsNullOrEmpty(outputFile) Then
                        File.WriteAllText(outputFile, rtfContent)
                        System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
                    End If

                Else
                    MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If

            If GetStatus = "Approved" AndAlso GetReqType = "ShareHolder" Then
                'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\New\Entitlement letter.rtf"
                Dim rtfContent As String

                Using fs As New FileStream(gsiepfApproveLStemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Using sr As New StreamReader(fs)
                        rtfContent = sr.ReadToEnd()
                    End Using
                End Using

                Dim currentDate As DateTime = DateTime.Now
                Dim finYearStart As Integer

                ' In India, financial year starts on April 1
                If currentDate.Month >= 4 Then
                    finYearStart = currentDate.Year
                Else
                    finYearStart = currentDate.Year - 1
                End If

                Dim finYearText As String = finYearStart.ToString() & "-" & (finYearStart + 1).ToString().Substring(2)

                ' Replace placeholders
                rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
                rtfContent = rtfContent.Replace("<<fin_year>>", finYearText)
                rtfContent = rtfContent.Replace("<<inw_no>>", GetInwardNo)
                rtfContent = rtfContent.Replace("<<share_count>>", GetShareCount)
                rtfContent = rtfContent.Replace("<<shareholder_name>>", GetShareHolderName)
                rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
                rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)

                Dim dtComp As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_code")
                Dim compCode As String = ""

                If dtComp IsNot Nothing AndAlso dtComp.Rows.Count > 0 Then
                    compCode = dtComp.Rows(0)(0).ToString()   ' first column, first row
                End If

                rtfContent = rtfContent.Replace("<<comp_code>>", compCode)

                Dim dtCompSec As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_sec")
                Dim compSec As String = ""

                If dtCompSec IsNot Nothing AndAlso dtCompSec.Rows.Count > 0 Then
                    compSec = dtCompSec.Rows(0)(0).ToString()   ' first column, first row
                End If

                'rtfContent = rtfContent.Replace("<<comp_sec_name>>", compSec)
                ' --- Escape special RTF characters ---
                compSec = compSec.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}")

                ' --- Add some spacing after the name (2 line breaks) ---
                Dim compSecRtf As String = "{\rtlch\fcs1 \af37 \ltrch\fcs0 \b0\caps " & compSec & "}\par\par"

                ' --- Replace placeholder with formatted text ---
                rtfContent = rtfContent.Replace("<<comp_sec_name>>", compSecRtf)

                rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetHolderAddr))

                ' Step 1: Get data
                Dim dt As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "lessthan7years")

                ' Step 2: Prepare builder & config
                Dim rtfTable As New StringBuilder()
                Dim colWidths() As Integer = {3800, 2550, 5550, 3550, 3050, 2850}
                Dim headers() As String = {"Dividend Year", "Folio Dpid", "Share Holder", "Share Count", "Net Amount", "Warrant No"}

                ' Local sub to build a row (header or data)
                Dim buildRow As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths.Length - 1
                            totalWidth += colWidths(i)
                            rtfTable.Append("\clvertalc") ' vertical center
                            rtfTable.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable.Append("\cellx" & totalWidth)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow(headers, True)

                ' --- Build data rows ---
                For Each dr As DataRow In dt.Rows
                    Dim rowValues() As String = {
                        Convert.ToString(dr("Dividend Year")),
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr("Share Count")),
                        Convert.ToString(dr("Net Amount")),
                        Convert.ToString(dr("Warrant No"))
                    }
                    buildRow(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table1>>", rtfTable.ToString())

                ' Step 1: Get data
                Dim dt2 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "annexure")

                ' Step 2: Prepare builder & config
                Dim rtfTable2 As New StringBuilder()
                Dim colWidths2() As Integer = {3800, 2550, 5550, 3550, 3050, 2850}
                Dim headers2() As String = {"Dividend Year", "Folio Dpid", "Share Holder", "Share Count", "Net Amount", "Warrant No"}

                ' Local sub to build a row (header or data)
                Dim buildRow2 As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth2 As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable2.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths2.Length - 1
                            totalWidth2 += colWidths2(i)
                            rtfTable2.Append("\clvertalc") ' vertical center
                            rtfTable2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable2.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable2.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable2.Append("\cellx" & totalWidth2)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable2.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable2.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable2.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow2(headers2, True)

                ' --- Build data rows ---
                For Each dr2 As DataRow In dt2.Rows
                    Dim rowValues() As String = {
                        Convert.ToString(dr2("Dividend Year")),
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr2("Share Count")),
                        Convert.ToString(dr2("Net Amount")),
                        Convert.ToString(dr2("Warrant No"))
                    }
                    buildRow2(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table4>>", rtfTable2.ToString())

                ' Step 1: Get data
                Dim dt3 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_iepf")

                ' Step 2: Prepare builder & config
                Dim rtfTable3 As New StringBuilder()
                Dim colWidths3() As Integer = {3800, 2550, 5550, 3050, 2850}
                Dim headers3() As String = {"Dividend Year", "Folio Dpid", "Share Holder", "Net Amount", "Warrant No"}

                ' Local sub to build a row (header or data)
                Dim buildRow3 As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth3 As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable3.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths3.Length - 1
                            totalWidth3 += colWidths3(i)
                            rtfTable3.Append("\clvertalc") ' vertical center
                            rtfTable3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable3.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable3.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable3.Append("\cellx" & totalWidth3)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable3.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable3.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable3.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow3(headers3, True)

                ' --- Build data rows ---
                For Each dr3 As DataRow In dt3.Rows
                    Dim rowValues() As String = {
                        Convert.ToString(dr3("Dividend Year")),
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr3("Net Amount")),
                        Convert.ToString(dr3("Warrant No"))
                    }
                    buildRow3(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table2>>", rtfTable3.ToString())

                ' Step 1: Get data
                Dim dt4 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_details")

                ' Step 2: Prepare builder & config
                Dim rtfTable4 As New StringBuilder()
                Dim colWidths4() As Integer = {4800, 5550, 2550, 3050}
                Dim headers4() As String = {"Folio Dpid", "Share Holder", "Date Of Transfer", "No Of Shares Transferred To IEPF"}

                ' Local sub to build a row (header or data)
                Dim buildRow4 As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth4 As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable4.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths4.Length - 1
                            totalWidth4 += colWidths4(i)
                            rtfTable4.Append("\clvertalc") ' vertical center
                            rtfTable4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable4.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable4.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable4.Append("\cellx" & totalWidth4)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable4.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable4.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable4.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow4(headers4, True)

                ' --- Build data rows ---
                For Each dr4 As DataRow In dt4.Rows
                    Dim rowValues() As String = {
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr4("Transfer Date")),
                        Convert.ToString(dr4("No of Shares"))
                    }
                    buildRow4(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table3>>", rtfTable4.ToString())

                ' DB Save
                Dim attachmentGid As Integer = 0
                Dim dbFileName As String = Path.GetFileName(gsiepfApproveLStemplatepath)
                Dim msg As String = ""
                Dim result As Integer

                ' First check if record exists
                result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

                If result > 0 Then
                    ' Exists → Update
                    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
                Else
                    ' New → Insert
                    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
                End If

                ' Only if DB success → save file
                If result = 1 AndAlso attachmentGid > 0 Then
                    Dim localFolder As String = gsAttachmentPath
                    If Not Directory.Exists(localFolder) Then
                        Directory.CreateDirectory(localFolder)
                    End If

                    ' Use attachment_gid as filename
                    Dim localFileName As String = attachmentGid.ToString() & ".sta"
                    Dim fullPath As String = Path.Combine(localFolder, localFileName)

                    File.WriteAllText(fullPath, rtfContent)

                    MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Dim outputFile As String = "C:\temp\IEPF CLAIM\Approve"

                    ' If folder doesnot exists means create a directory folder
                    If Not System.IO.Directory.Exists(outputFile) Then
                        System.IO.Directory.CreateDirectory(outputFile)
                    End If

                    outputFile = Path.ChangeExtension(outputFile + "\Entitlement letter_LS.rtf", ".rtf")

                    ' Read our HTML file a string.
                    Dim htmlString As String = File.ReadAllText(gsiepfApproveLStemplatepath)

                    ' Open the result for demonstration purposes.
                    If Not String.IsNullOrEmpty(outputFile) Then
                        File.WriteAllText(outputFile, rtfContent)
                        System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
                    End If

                    ' Open file // 13-10-2025
                    'Dim p As New ProcessStartInfo(fullPath) With {.UseShellExecute = True}
                    'Process.Start(p)
                End If

            ElseIf GetStatus = "Approved" AndAlso GetReqType <> "ShareHolder" Then
                'Dim templatePath As String = "E:\Mangai\GNSA\STA_Yoga\Templates\New\Entitlement letter.rtf"
                Dim rtfContent As String

                Using fs As New FileStream(gsiepfApproveTMtemplatepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Using sr As New StreamReader(fs)
                        rtfContent = sr.ReadToEnd()
                    End Using
                End Using

                Dim currentDate As DateTime = DateTime.Now
                Dim finYearStart As Integer

                ' In India, financial year starts on April 1
                If currentDate.Month >= 4 Then
                    finYearStart = currentDate.Year
                Else
                    finYearStart = currentDate.Year - 1
                End If

                Dim finYearText As String = finYearStart.ToString() & "-" & (finYearStart + 1).ToString().Substring(2)

                ' Replace placeholders
                rtfContent = rtfContent.Replace("<<date>>", DateTime.Now.ToString("dd-MM-yyyy"))
                rtfContent = rtfContent.Replace("<<fin_year>>", finYearText)
                rtfContent = rtfContent.Replace("<<inw_no>>", GetInwardNo)
                rtfContent = rtfContent.Replace("<<share_count>>", GetShareCount)
                rtfContent = rtfContent.Replace("<<shareholder_name>>", GetShareHolderName)
                rtfContent = rtfContent.Replace("<<folio_no>>", GetFolioNo)
                rtfContent = rtfContent.Replace("<<comp_name>>", GetCompanyName)

                Dim dtComp As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_code")
                Dim compCode As String = ""

                If dtComp IsNot Nothing AndAlso dtComp.Rows.Count > 0 Then
                    compCode = dtComp.Rows(0)(0).ToString()   ' first column, first row
                End If

                rtfContent = rtfContent.Replace("<<comp_code>>", compCode)

                Dim dtCompSec As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "get_comp_sec")
                Dim compSec As String = ""

                If dtCompSec IsNot Nothing AndAlso dtCompSec.Rows.Count > 0 Then
                    compSec = dtCompSec.Rows(0)(0).ToString()   ' first column, first row
                End If

                'rtfContent = rtfContent.Replace("<<comp_sec_name>>", compSec)
                ' --- Escape special RTF characters ---
                compSec = compSec.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}")

                ' --- Add some spacing after the name (2 line breaks) ---
                Dim compSecRtf As String = "{\rtlch\fcs1 \af37 \ltrch\fcs0 \b0\caps " & compSec & "}\par\par"

                ' --- Replace placeholder with formatted text ---
                rtfContent = rtfContent.Replace("<<comp_sec_name>>", compSecRtf)

                If GetReqType = "Claimant" Then
                    rtfContent = rtfContent.Replace("<<name>>", GetClaimantName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetClaimantAddr))
                ElseIf GetReqType = "Nominee" Then
                    rtfContent = rtfContent.Replace("<<name>>", GetNomineeName)
                    rtfContent = rtfContent.Replace("<<address>>", FormatAddressForRtf(GetNomineeAddr))
                End If

                ' Step 1: Get data
                Dim dt As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "lessthan7years")

                ' Step 2: Prepare builder & config
                Dim rtfTable As New StringBuilder()
                Dim colWidths() As Integer = {3800, 2550, 5550, 3550, 3050, 2850}
                Dim headers() As String = {"Dividend Year", "Folio Dpid", "Share Holder", "Share Count", "Net Amount", "Warrant No"}

                ' Local sub to build a row (header or data)
                Dim buildRow As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths.Length - 1
                            totalWidth += colWidths(i)
                            rtfTable.Append("\clvertalc") ' vertical center
                            rtfTable.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable.Append("\cellx" & totalWidth)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow(headers, True)

                ' --- Build data rows ---
                For Each dr As DataRow In dt.Rows
                    Dim rowValues() As String = {
                        Convert.ToString(dr("Dividend Year")),
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr("Share Count")),
                        Convert.ToString(dr("Net Amount")),
                        Convert.ToString(dr("Warrant No"))
                    }
                    buildRow(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table1>>", rtfTable.ToString())

                ' Step 1: Get data
                Dim dt2 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "annexure")

                ' Step 2: Prepare builder & config
                Dim rtfTable2 As New StringBuilder()
                Dim colWidths2() As Integer = {3800, 2550, 5550, 3550, 3050, 2850}
                Dim headers2() As String = {"Dividend Year", "Folio Dpid", "Share Holder", "Share Count", "Net Amount", "Warrant No"}

                ' Local sub to build a row (header or data)
                Dim buildRow2 As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth2 As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable2.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths2.Length - 1
                            totalWidth2 += colWidths2(i)
                            rtfTable2.Append("\clvertalc") ' vertical center
                            rtfTable2.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable2.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable2.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable2.Append("\cellx" & totalWidth2)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable2.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable2.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable2.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow2(headers2, True)

                ' --- Build data rows ---
                For Each dr2 As DataRow In dt2.Rows
                    Dim rowValues() As String = {
                        Convert.ToString(dr2("Dividend Year")),
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr2("Share Count")),
                        Convert.ToString(dr2("Net Amount")),
                        Convert.ToString(dr2("Warrant No"))
                    }
                    buildRow2(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table4>>", rtfTable2.ToString())

                ' Step 1: Get data
                Dim dt3 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_iepf")

                ' Step 2: Prepare builder & config
                Dim rtfTable3 As New StringBuilder()
                Dim colWidths3() As Integer = {6800, 2750, 5550, 3050, 2850}
                Dim headers3() As String = {"Dividend Year", "Folio Dpid", "Share Holder", "Net Amount", "Warrant No"}

                ' Local sub to build a row (header or data)
                Dim buildRow3 As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth3 As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable3.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths3.Length - 1
                            totalWidth3 += colWidths3(i)
                            rtfTable3.Append("\clvertalc") ' vertical center
                            rtfTable3.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable3.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable3.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable3.Append("\cellx" & totalWidth3)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable3.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable3.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable3.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow3(headers3, True)

                ' --- Build data rows ---
                For Each dr3 As DataRow In dt3.Rows
                    Dim rowValues() As String = {
                        Convert.ToString(dr3("Dividend Year")),
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr3("Net Amount")),
                        Convert.ToString(dr3("Warrant No"))
                    }
                    buildRow3(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table2>>", rtfTable3.ToString())

                ' Step 1: Get data
                Dim dt4 As DataTable = Getletterdetails(GetFolioGid, GetCompGid, "transfer_details")

                ' Step 2: Prepare builder & config
                Dim rtfTable4 As New StringBuilder()
                Dim colWidths4() As Integer = {4800, 5550, 2550, 3050}
                Dim headers4() As String = {"Folio Dpid", "Share Holder", "Date Of Transfer", "No Of Shares Transferred To IEPF"}

                ' Local sub to build a row (header or data)
                Dim buildRow4 As Action(Of String(), Boolean) =
                    Sub(values() As String, isHeader As Boolean)
                        Dim totalWidth4 As Integer = 0

                        ' Start row — use trqcen to center table horizontally
                        rtfTable4.Append("{\trowd\trgaph108\trleft720\trftsWidth3\trwWidth9000")

                        ' column definitions with borders; header gets shading
                        For i As Integer = 0 To colWidths4.Length - 1
                            totalWidth4 += colWidths4(i)
                            rtfTable4.Append("\clvertalc") ' vertical center
                            rtfTable4.Append("\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10")
                            rtfTable4.Append("\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10")
                            If isHeader Then
                                rtfTable4.Append("\clcbpat8") ' header shading
                            End If
                            rtfTable4.Append("\cellx" & totalWidth4)
                        Next

                        ' cell contents
                        For i As Integer = 0 To values.Length - 1
                            Dim raw As String = If(values(i) Is Nothing, "", values(i))
                            Dim v As String = EscapeForRtf(raw)
                            ' align first column left with small indent, others center
                            Dim alignCode As String = If(i = 0, "\ql\li200", "\qc")
                            If isHeader Then
                                rtfTable4.Append("\pard\intbl" & alignCode & "\b " & v & "\b0\cell")
                            Else
                                rtfTable4.Append("\pard\intbl" & alignCode & " " & v & "\cell")
                            End If
                        Next

                        rtfTable4.Append("\row}")
                    End Sub

                ' --- Build header ---
                buildRow4(headers4, True)

                ' --- Build data rows ---
                For Each dr4 As DataRow In dt4.Rows
                    Dim rowValues() As String = {
                        GetFolioNo,
                        GetShareHolderName,
                        Convert.ToString(dr4("Transfer Date")),
                        Convert.ToString(dr4("No of Shares"))
                    }
                    buildRow4(rowValues, False)
                Next

                ' --- Replace placeholder in the RTF template content ---
                rtfContent = rtfContent.Replace("<<table3>>", rtfTable4.ToString())

                ' DB Save
                Dim attachmentGid As Integer = 0
                Dim dbFileName As String = Path.GetFileName(gsiepfApproveLStemplatepath)
                Dim msg As String = ""
                Dim result As Integer

                ' First check if record exists
                result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "SELECT", gsLoginUserCode, msg)

                If result > 0 Then
                    ' Exists → Update
                    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "UPDATE", gsLoginUserCode, msg)
                Else
                    ' New → Insert
                    result = ManageAttachment(attachmentGid, GetInwardGid, 8, dbFileName, "INSERT", gsLoginUserCode, msg)
                End If

                ' Only if DB success → save file
                If result = 1 AndAlso attachmentGid > 0 Then
                    Dim localFolder As String = gsAttachmentPath
                    If Not Directory.Exists(localFolder) Then
                        Directory.CreateDirectory(localFolder)
                    End If

                    ' Use attachment_gid as filename
                    Dim localFileName As String = attachmentGid.ToString() & ".sta"
                    Dim fullPath As String = Path.Combine(localFolder, localFileName)

                    File.WriteAllText(fullPath, rtfContent)

                    MessageBox.Show("Letter generated successfully!", "STA", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Dim outputFile As String = "C:\temp\IEPF CLAIM\Approve"

                    ' If folder doesnot exists means create a directory folder
                    If Not System.IO.Directory.Exists(outputFile) Then
                        System.IO.Directory.CreateDirectory(outputFile)
                    End If

                    outputFile = Path.ChangeExtension(outputFile + "\Entitlement letter_TM.rtf", ".rtf")

                    ' Read our HTML file a string.
                    Dim htmlString As String = File.ReadAllText(gsiepfApproveTMtemplatepath)

                    ' Open the result for demonstration purposes.
                    If Not String.IsNullOrEmpty(outputFile) Then
                        File.WriteAllText(outputFile, rtfContent)
                        System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(outputFile) With {.UseShellExecute = True})
                    End If

                Else
                    MessageBox.Show("DB operation failed: " & msg, "STA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error generating letter: " & ex.Message)
        End Try
    End Sub

    Private Function EscapeForRtf(ByVal input As String) As String
        If input Is Nothing Then Return ""
        Return input.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}")
        'Return input
    End Function
End Class