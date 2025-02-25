Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.IO

Public Class clsPrint
    Private PrintFont As Font
    Private streamToPrint As StreamReader
    Private PrintMode As PrinterSettings
    Private PageSet As PageSettings

    Public Structure SECURITY_ATTRIBUTES
        Private nLength As Integer
        Private lpSecurityDescriptor As Integer
        Private bInheritHandle As Integer
    End Structure

    Private Const GENERIC_WRITE = &H40000000
    Private Const OPEN_EXISTING = 3
    Private Const FILE_SHARE_WRITE = &H2

    Private Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" ( _
            ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, _
            ByVal dwShareMode As Integer, _
            ByRef lpSecurityAttributes As SECURITY_ATTRIBUTES, _
            ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, _
            ByVal hTemplateFile As Integer) As Integer
    Private Declare Function CloseHandle Lib "kernel32" Alias "CloseHandle" (ByVal hObject As Integer) As Integer

    ' The PrintPage event is raised for each page to be printed.
    Private Sub pd_PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim lnLinesPerPage As Single = 0
        Dim lnYPos As Single = 0
        Dim lnCount As Integer = 0
        Dim lnLeftMargin As Single = ev.MarginBounds.Left
        Dim lnTopMargin As Single = ev.MarginBounds.Top
        Dim lsLine As String = Nothing

        ' Calculate the number of lines per page.
        lnLinesPerPage = ev.MarginBounds.Height / PrintFont.GetHeight(ev.Graphics)

        ' Iterate over the file, printing each line.
        While lnCount < lnLinesPerPage
            lsLine = streamToPrint.ReadLine()

            If lsLine Is Nothing Or lsLine = Chr(12) Then
                Exit While
            End If

            lnYPos = lnTopMargin + lnCount * PrintFont.GetHeight(ev.Graphics)

            ev.Graphics.DrawString(lsLine, PrintFont, Brushes.Black, lnLeftMargin, _
                lnYPos, New StringFormat())
            lnCount += 1
        End While

        ' If more lines exist, print another page.
        If lnCount > 0 Then
            If Not (lsLine Is Nothing) Then
                ev.HasMorePages = True
            Else
                ev.HasMorePages = False
            End If
        End If
    End Sub

    ' Print the file.
    Public Sub Printing(ByVal FileName As String)
        Try
            streamToPrint = New StreamReader(FileName)

            Try
                PrintFont = New Font("Courier New", 8)

                Dim pd As New PrintDocument()

                AddHandler pd.PrintPage, AddressOf pd_PrintPage
                pd.DefaultPageSettings.Landscape = True

                Dim margins As New Margins(20, 20, 20, 20)
                pd.DefaultPageSettings.Margins = margins
                pd.PrinterSettings.Copies = 1

                Dim PrintDialog1 As New PrintDialog()
                PrintDialog1.Document = pd
                Dim result As DialogResult = PrintDialog1.ShowDialog()

                If (result = DialogResult.OK) Then
                    pd.Print()
                End If
            Finally
                streamToPrint.Close()
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub 'Printing    

    Public Function PrintDotMatrix(ByVal FileName As String, Optional ByVal CondensedFlag As Boolean = False) As Long
        Dim SA As SECURITY_ATTRIBUTES
        Dim outFile As FileStream
        Dim hPortP As IntPtr
        Dim hPort As Integer
        Dim FileWriter As StreamWriter
        Dim lsLine As String = ""
        Try

            If File.Exists(FileName) = True Then
                hPort = CreateFile("LPT1", GENERIC_WRITE, FILE_SHARE_WRITE, SA, OPEN_EXISTING, 0, 0)
                hPortP = New IntPtr(hPort) 'convert Integer to IntPtr 
                outFile = New FileStream(hPortP, FileAccess.Write, False) 'Create FileStream using Handle 

                FileWriter = New StreamWriter(outFile)
                FileOpen(1, FileName, OpenMode.Input)

                If CondensedFlag = True Then FileWriter.Write(Chr(15))

                While Not EOF(1)
                    Input(1, lsLine)
                    FileWriter.WriteLine(lsLine)
                End While

                FileClose(1)

                FileWriter.Write(Chr(18))

                FileWriter.Flush()
                FileWriter.Close()
                outFile.Close()

                Return CloseHandle(hPort)
            End If

            Return 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gsProjectName)
            Return 0
        End Try
    End Function
End Class
