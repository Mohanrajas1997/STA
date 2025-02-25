<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFolioDetail
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
        Me.btnFolio = New System.Windows.Forms.Button()
        Me.btnCertificate = New System.Windows.Forms.Button()
        Me.btnInward = New System.Windows.Forms.Button()
        Me.btnSignature = New System.Windows.Forms.Button()
        Me.btnAttachment = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnQueue = New System.Windows.Forms.Button()
        Me.btnOutward = New System.Windows.Forms.Button()
        Me.btnDistSeries = New System.Windows.Forms.Button()
        Me.btnTran = New System.Windows.Forms.Button()
        Me.SuspendLayout()
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
        'btnCertificate
        '
        Me.btnCertificate.Location = New System.Drawing.Point(142, 12)
        Me.btnCertificate.Name = "btnCertificate"
        Me.btnCertificate.Size = New System.Drawing.Size(124, 24)
        Me.btnCertificate.TabIndex = 1
        Me.btnCertificate.Text = "Certificate"
        Me.btnCertificate.UseVisualStyleBackColor = True
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
        'btnSignature
        '
        Me.btnSignature.Location = New System.Drawing.Point(12, 132)
        Me.btnSignature.Name = "btnSignature"
        Me.btnSignature.Size = New System.Drawing.Size(124, 24)
        Me.btnSignature.TabIndex = 8
        Me.btnSignature.Text = "Signature"
        Me.btnSignature.UseVisualStyleBackColor = True
        '
        'btnAttachment
        '
        Me.btnAttachment.Location = New System.Drawing.Point(142, 72)
        Me.btnAttachment.Name = "btnAttachment"
        Me.btnAttachment.Size = New System.Drawing.Size(124, 24)
        Me.btnAttachment.TabIndex = 5
        Me.btnAttachment.Text = "Attachment"
        Me.btnAttachment.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(142, 132)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(124, 24)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnQueue
        '
        Me.btnQueue.Location = New System.Drawing.Point(12, 72)
        Me.btnQueue.Name = "btnQueue"
        Me.btnQueue.Size = New System.Drawing.Size(124, 24)
        Me.btnQueue.TabIndex = 4
        Me.btnQueue.Text = "Queue"
        Me.btnQueue.UseVisualStyleBackColor = True
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
        'btnDistSeries
        '
        Me.btnDistSeries.Location = New System.Drawing.Point(12, 102)
        Me.btnDistSeries.Name = "btnDistSeries"
        Me.btnDistSeries.Size = New System.Drawing.Size(124, 24)
        Me.btnDistSeries.TabIndex = 6
        Me.btnDistSeries.Text = "Dist Series"
        Me.btnDistSeries.UseVisualStyleBackColor = True
        '
        'btnTran
        '
        Me.btnTran.Location = New System.Drawing.Point(142, 102)
        Me.btnTran.Name = "btnTran"
        Me.btnTran.Size = New System.Drawing.Size(124, 24)
        Me.btnTran.TabIndex = 7
        Me.btnTran.Text = "Transaction"
        Me.btnTran.UseVisualStyleBackColor = True
        '
        'frmFolioDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(280, 165)
        Me.Controls.Add(Me.btnDistSeries)
        Me.Controls.Add(Me.btnTran)
        Me.Controls.Add(Me.btnOutward)
        Me.Controls.Add(Me.btnQueue)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAttachment)
        Me.Controls.Add(Me.btnSignature)
        Me.Controls.Add(Me.btnInward)
        Me.Controls.Add(Me.btnCertificate)
        Me.Controls.Add(Me.btnFolio)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFolioDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Folio Detail"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnFolio As System.Windows.Forms.Button
    Friend WithEvents btnCertificate As System.Windows.Forms.Button
    Friend WithEvents btnInward As System.Windows.Forms.Button
    Friend WithEvents btnSignature As System.Windows.Forms.Button
    Friend WithEvents btnAttachment As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnQueue As System.Windows.Forms.Button
    Friend WithEvents btnOutward As System.Windows.Forms.Button
    Friend WithEvents btnDistSeries As System.Windows.Forms.Button
    Friend WithEvents btnTran As System.Windows.Forms.Button
End Class
