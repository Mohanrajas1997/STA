<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocHistory
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnQueue = New System.Windows.Forms.Button()
        Me.btnCertificate = New System.Windows.Forms.Button()
        Me.btnFolio = New System.Windows.Forms.Button()
        Me.btnInward = New System.Windows.Forms.Button()
        Me.btnDoc = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnAttachment = New System.Windows.Forms.Button()
        Me.btnOutward = New System.Windows.Forms.Button()
        Me.btnSignature = New System.Windows.Forms.Button()
        Me.btnAllAttachment = New System.Windows.Forms.Button()
        Me.btnDistSeries = New System.Windows.Forms.Button()
        Me.btnTran = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnQueue
        '
        Me.btnQueue.Location = New System.Drawing.Point(12, 102)
        Me.btnQueue.Name = "btnQueue"
        Me.btnQueue.Size = New System.Drawing.Size(124, 24)
        Me.btnQueue.TabIndex = 6
        Me.btnQueue.Text = "Queue"
        Me.btnQueue.UseVisualStyleBackColor = True
        '
        'btnCertificate
        '
        Me.btnCertificate.Location = New System.Drawing.Point(142, 12)
        Me.btnCertificate.Name = "btnCertificate"
        Me.btnCertificate.Size = New System.Drawing.Size(124, 24)
        Me.btnCertificate.TabIndex = 1
        Me.btnCertificate.Text = "Certificate"
        Me.btnCertificate.UseVisualStyleBackColor = True
        '
        'btnFolio
        '
        Me.btnFolio.Location = New System.Drawing.Point(12, 12)
        Me.btnFolio.Name = "btnFolio"
        Me.btnFolio.Size = New System.Drawing.Size(124, 24)
        Me.btnFolio.TabIndex = 0
        Me.btnFolio.Text = "Folio"
        Me.btnFolio.UseVisualStyleBackColor = True
        '
        'btnInward
        '
        Me.btnInward.Location = New System.Drawing.Point(12, 42)
        Me.btnInward.Name = "btnInward"
        Me.btnInward.Size = New System.Drawing.Size(124, 24)
        Me.btnInward.TabIndex = 2
        Me.btnInward.Text = "Inward"
        Me.btnInward.UseVisualStyleBackColor = True
        '
        'btnDoc
        '
        Me.btnDoc.Location = New System.Drawing.Point(12, 72)
        Me.btnDoc.Name = "btnDoc"
        Me.btnDoc.Size = New System.Drawing.Size(124, 24)
        Me.btnDoc.TabIndex = 4
        Me.btnDoc.Text = "Document"
        Me.btnDoc.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(142, 163)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(124, 24)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnAttachment
        '
        Me.btnAttachment.Location = New System.Drawing.Point(142, 72)
        Me.btnAttachment.Name = "btnAttachment"
        Me.btnAttachment.Size = New System.Drawing.Size(124, 24)
        Me.btnAttachment.TabIndex = 5
        Me.btnAttachment.Text = "Doc Attachment"
        Me.btnAttachment.UseVisualStyleBackColor = True
        '
        'btnOutward
        '
        Me.btnOutward.Location = New System.Drawing.Point(142, 42)
        Me.btnOutward.Name = "btnOutward"
        Me.btnOutward.Size = New System.Drawing.Size(124, 24)
        Me.btnOutward.TabIndex = 3
        Me.btnOutward.Text = "Outward"
        Me.btnOutward.UseVisualStyleBackColor = True
        '
        'btnSignature
        '
        Me.btnSignature.Location = New System.Drawing.Point(12, 163)
        Me.btnSignature.Name = "btnSignature"
        Me.btnSignature.Size = New System.Drawing.Size(124, 24)
        Me.btnSignature.TabIndex = 10
        Me.btnSignature.Text = "Signature"
        Me.btnSignature.UseVisualStyleBackColor = True
        '
        'btnAllAttachment
        '
        Me.btnAllAttachment.Location = New System.Drawing.Point(142, 102)
        Me.btnAllAttachment.Name = "btnAllAttachment"
        Me.btnAllAttachment.Size = New System.Drawing.Size(124, 24)
        Me.btnAllAttachment.TabIndex = 7
        Me.btnAllAttachment.Text = "Folio Attachment"
        Me.btnAllAttachment.UseVisualStyleBackColor = True
        '
        'btnDistSeries
        '
        Me.btnDistSeries.Location = New System.Drawing.Point(12, 132)
        Me.btnDistSeries.Name = "btnDistSeries"
        Me.btnDistSeries.Size = New System.Drawing.Size(124, 24)
        Me.btnDistSeries.TabIndex = 8
        Me.btnDistSeries.Text = "Dist Series"
        Me.btnDistSeries.UseVisualStyleBackColor = True
        '
        'btnTran
        '
        Me.btnTran.Location = New System.Drawing.Point(142, 132)
        Me.btnTran.Name = "btnTran"
        Me.btnTran.Size = New System.Drawing.Size(124, 24)
        Me.btnTran.TabIndex = 9
        Me.btnTran.Text = "Transaction"
        Me.btnTran.UseVisualStyleBackColor = True
        '
        'frmDocHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(279, 198)
        Me.Controls.Add(Me.btnDistSeries)
        Me.Controls.Add(Me.btnTran)
        Me.Controls.Add(Me.btnAllAttachment)
        Me.Controls.Add(Me.btnSignature)
        Me.Controls.Add(Me.btnOutward)
        Me.Controls.Add(Me.btnAttachment)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnDoc)
        Me.Controls.Add(Me.btnInward)
        Me.Controls.Add(Me.btnFolio)
        Me.Controls.Add(Me.btnCertificate)
        Me.Controls.Add(Me.btnQueue)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDocHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "History"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnQueue As System.Windows.Forms.Button
    Friend WithEvents btnCertificate As System.Windows.Forms.Button
    Friend WithEvents btnFolio As System.Windows.Forms.Button
    Friend WithEvents btnInward As System.Windows.Forms.Button
    Friend WithEvents btnDoc As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnAttachment As System.Windows.Forms.Button
    Friend WithEvents btnOutward As System.Windows.Forms.Button
    Friend WithEvents btnSignature As System.Windows.Forms.Button
    Friend WithEvents btnAllAttachment As System.Windows.Forms.Button
    Friend WithEvents btnDistSeries As System.Windows.Forms.Button
    Friend WithEvents btnTran As System.Windows.Forms.Button
End Class
