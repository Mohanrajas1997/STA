Imports System.Windows.Forms

Public Class frmMain
    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ms As New MenuStrip

        Call Main()

        If gsLoginUserCode <> "Admin" Then
            ms = Me.MenuStrip

            For i = 0 To ms.Items.Count - 1
                Application.DoEvents()
                Call LoadSubMenuItems(ms.Items(i))
            Next i
        End If

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub mnuCourier_Click(sender As Object, e As EventArgs) Handles mnuCourier.Click
        Dim objFrm As New frmSpNameMaster("sta_mst_tcourier", "courier_gid", "Courier Id", "courier_name", _
                                          "Courier Name", "32", "delete_flag", "Courier Name", "Courier Master", "pr_sta_mst_tcourier")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuCountryMaster_Click(sender As Object, e As EventArgs) Handles mnuCountryMaster.Click
        Dim objFrm As New frmSpCodeMaster("sta_mst_tcountry", "country_gid", "Country Id", "country_code", "Country Code", 8, _
                                           "country_name", "Country Name", "64", "delete_flag", "Country Master", "pr_sta_mst_tcountry")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuStateMaster_Click(sender As Object, e As EventArgs) Handles mnuStateMaster.Click
        Dim objFrm As New frmStateMaster
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuInwardEntry_Click(sender As Object, e As EventArgs) Handles mnuInwardEntry.Click
        Dim objFrm As New frmInwardEntry("ADD", , False)
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuQueueMaker_Click(sender As Object, e As EventArgs) Handles mnuQueueMaker.Click
        Dim objFrm As New frmQueue("M")
        objFrm.MdiParent = Me
        objFrm.Text = "Queue Maker"
        objFrm.Show()
    End Sub

    Private Sub mnuQueueChecker_Click(sender As Object, e As EventArgs) Handles mnuQueueChecker.Click
        Dim objFrm As New frmQueue("C")
        objFrm.MdiParent = Me
        objFrm.Text = "Queue Checker"
        objFrm.Show()
    End Sub

    Private Sub mnuExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click
        If MessageBox.Show("Are you sure to exit ?", gsProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub mnuInwardList_Click(sender As Object, e As EventArgs) Handles mnuInwardList.Click
        Dim objFrm As New frmInwardList
        objFrm.MdiParent = Me
        objFrm.Show()
    End Sub

    Private Sub mnuQueueInex_Click(sender As Object, e As EventArgs) Handles mnuQueueInex.Click
        Dim objFrm As New frmQueue("I")
        objFrm.MdiParent = Me
        objFrm.Text = "Queue Inex"
        objFrm.Show()
    End Sub

    Private Sub mnuQueueDespatch_Click(sender As Object, e As EventArgs) Handles mnuQueueDespatch.Click
        Dim objFrm As New frmQueue("D")
        objFrm.MdiParent = Me
        objFrm.Text = "Queue Despatch"
        objFrm.Show()
    End Sub

    Private Sub mnuAttachmentTypeMaster_Click(sender As Object, e As EventArgs) Handles mnuAttachmentTypeMaster.Click
        Dim objFrm As New frmSpNameMaster("sta_mst_tattachmenttype", "attachmenttype_gid", "Attachment Type Id", "attachmenttype_name", _
                                          "Attachment Type", "32", "delete_flag", "Attachment Type", "Attachment Type Master", "pr_sta_mst_tattachmenttype")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuBankMaster_Click(sender As Object, e As EventArgs) Handles mnuBankMaster.Click
        Dim objFrm As New frmSpCodeMaster("sta_mst_tbank", "bank_gid", "Bank Id", "bank_code", "Bank Code", 8, _
                                           "bank_name", "Bank Name", "64", "delete_flag", "Bank Master", "pr_sta_mst_tbank")
        objFrm.ShowDialog()
    End Sub

    Private Sub BankACTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuBankAccTypeMaster.Click
        Dim objFrm As New frmSpCodeMaster("sta_mst_tbankacctype", "bankacctype_gid", "A/C Type Id", "bankacctype_code", "A/C Type Code", 1, _
                                           "bankacctype_name", "A/C Type Name", "64", "delete_flag", "Bank A/C Type Master", "pr_sta_mst_tbankacctype")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuCategoryMaster_Click(sender As Object, e As EventArgs) Handles mnuCategoryMaster.Click
        'Dim objFrm As New frmSpNameMaster("sta_mst_tcategory", "category_gid", "Category Id", "category_name", _
        '                                  "Category Name", "32", "delete_flag", "Category Name", "Category Master", "pr_sta_mst_tcategory")
        'objFrm.ShowDialog()
        Dim objFrm As New frmSpCodeMaster("sta_mst_tcategory", "category_gid", "Category Id", "category_name", "Category Name", 32, _
                                          "category_alias_name", "Alias Name", "32", "delete_flag", "Category Master", "pr_sta_mst_tcategory_new")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuSignatureBulkAdd_Click(sender As Object, e As EventArgs) Handles mnuSignatureBulkAdd.Click
        Dim objFrm As New frmSignatureAdd
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuSignatureSingleAdd_Click(sender As Object, e As EventArgs) Handles mnuSignatureSingleAdd.Click
        Dim objFrm As New frmSignatureAddSingle
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuImport_Click(sender As Object, e As EventArgs) Handles mnuImport.Click
        Dim objFrm As New frmImportAll
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuUserCreation_Click(sender As Object, e As EventArgs) Handles mnuUserCreation.Click
        Dim objfrm As New frmUserMaster
        objfrm.ShowDialog()
    End Sub

    Private Sub SetPasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetPasswordToolStripMenuItem.Click
        Dim objfrm As New frmChngPwd
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuUserGrp_Click(sender As Object, e As EventArgs) Handles mnuUserGrp.Click
        Dim objFrm As New frmSpNameMaster("soft_mst_tusergroup", "usergroup_gid", "User Group Id", "usergroup_name", "Region Name", "32", _
                                           "delete_flag", "User Group Name", "User Group Master", "pr_soft_mst_tusergroup")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuUserGrpRights_Click(sender As Object, e As EventArgs) Handles mnuUserGrpRights.Click
        Dim objfrm As New frmRights
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuUserAuth_Click(sender As Object, e As EventArgs) Handles mnuUserAuth.Click
        Dim frm As New frmUserAuth
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuUserAuthRpt_Click(sender As Object, e As EventArgs) Handles mnuUserAuthRpt.Click
        Dim frm As New frmUserAuthReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuUserLog_Click(sender As Object, e As EventArgs) Handles mnuUserLog.Click
        Dim frm As New frmUserManagementReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuServerConfiguration_Click(sender As Object, e As EventArgs) Handles mnuServerConfiguration.Click
        Dim objfrm As New frmServerConfiguration
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuRptFile_Click(sender As Object, e As EventArgs) Handles mnuRptFile.Click
        Dim frm As New frmFileReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuRptImpErrLine_Click(sender As Object, e As EventArgs) Handles mnuRptImpErrLine.Click
        Dim frm As New frmErrLineReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuUploadStatusUpdate_Click(sender As Object, e As EventArgs) Handles mnuUploadStatusUpdate.Click
        Dim objFrm As New frmUploadStatusUpdate
        objFrm.MdiParent = Me
        objFrm.Show()
    End Sub

    Private Sub mnuGenerateUpload_Click(sender As Object, e As EventArgs) Handles mnuGenerateUpload.Click
        Dim objFrm As New frmUploadQueue("U")
        objFrm.MdiParent = Me
        objFrm.Show()
    End Sub

    Private Sub mnuPrintUpload_Click(sender As Object, e As EventArgs) Handles mnuPrintUpload.Click
        Dim objFrm As New frmUploadPrint
        objFrm.MdiParent = Me
        objFrm.Show()
    End Sub

    Private Sub mnuFolioReport_Click(sender As Object, e As EventArgs) Handles mnuFolioReport.Click
        Dim frm As New frmFolioReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuCertReport_Click(sender As Object, e As EventArgs) Handles mnuCertReport.Click
        Dim frm As New frmCertReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuCertDistReport_Click(sender As Object, e As EventArgs) Handles mnuCertDistReport.Click
        Dim frm As New frmCertDistReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuDematPendReport_Click(sender As Object, e As EventArgs) Handles mnuDematPendReport.Click
        Dim frm As New frmDematPendingReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuFolioTranReport_Click(sender As Object, e As EventArgs) Handles mnuFolioTranReport.Click
        Dim frm As New frmFolioTranReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuInwardReport_Click(sender As Object, e As EventArgs) Handles mnuInwardReport.Click
        Dim frm As New frmInwardReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuUploadReport_Click(sender As Object, e As EventArgs) Handles mnuUploadReport.Click
        Dim frm As New frmUploadReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuOutwardReport_Click(sender As Object, e As EventArgs) Handles mnuOutwardReport.Click
        Dim frm As New frmOutwardReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuConfigMaster_Click(sender As Object, e As EventArgs) Handles mnuConfigMaster.Click
        Dim objFrm As New frmSpCodeMaster("sta_mst_tconfig", "config_gid", "Config Id", "config_name", "Config Name", 64, _
                                           "config_value", "Config Value", 128, "delete_flag", "Config Master", "pr_sta_mst_tconfig")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuQueueInward_Click(sender As Object, e As EventArgs) Handles mnuQueueInward.Click
        Dim objFrm As New frmQueue("N")
        objFrm.MdiParent = Me
        objFrm.Text = "Queue Inward"
        objFrm.Show()
    End Sub


    Private Sub LoadSubMenuItems(ByVal mnu As ToolStripMenuItem)
        Dim i As Integer
        Dim lsSql As String
        Dim lnCount As Integer

        lsSql = ""
        lsSql &= " select count(*) from soft_mst_trights "
        lsSql &= " where usergroup_gid = '" & gnLoginUserGrpId & "' "
        lsSql &= " and menu_name = '" & mnu.Name & "' "
        lsSql &= " and rights_flag = 1 "
        lsSql &= " and delete_flag = 'N' "

        lnCount = Val(gfExecuteScalar(lsSql, gOdbcConn))

        If lnCount > 0 Then
            If mnu.DropDownItems.Count > 0 Then
                For i = 0 To mnu.DropDownItems.Count - 1
                    If mnu.DropDownItems.Item(i).Text <> "" Then
                        Application.DoEvents()
                        LoadSubMenuItems(mnu.DropDownItems.Item(i))
                    End If
                Next i
            End If
        Else
            mnu.Enabled = False
        End If
    End Sub

    Private Sub DisableSubMenuItems(ByVal mnu As ToolStripMenuItem)
        Dim i As Integer

        mnu.Enabled = False

        If mnu.DropDownItems.Count > 0 Then
            For i = 0 To mnu.DropDownItems.Count - 1
                If mnu.DropDownItems.Item(i).Text <> "" Then
                    Application.DoEvents()
                    LoadSubMenuItems(mnu.DropDownItems.Item(i))
                End If
            Next i
        End If
    End Sub

    Private Sub mnuAutoInwardEntry_Click(sender As Object, e As EventArgs) Handles mnuAutoInwardEntry.Click
        'Dim objFrm As New frmInwardEntry("ADD")
        Dim objFrm As New frmInwardEntryNew("ADD")
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuQuery_Click(sender As Object, e As EventArgs) Handles mnuQuery.Click
        Dim frm As New frmQryReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuSearchEngine_Click(sender As Object, e As EventArgs)
        Dim frm As New frmSearchEngine
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuQryBuilder_Click(sender As Object, e As EventArgs) Handles mnuQryBuilder.Click
        Dim frm As New frmReportQryMaking
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuSummaryReport_Click(sender As Object, e As EventArgs) Handles mnuSummaryReport.Click
        Dim frm As New frmSummaryReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuAttachment_Click(sender As Object, e As EventArgs) Handles mnuAttachment.Click
        Dim objFrm As New frmInwardList(True)
        objFrm.MdiParent = Me
        objFrm.Text = "Add Attachment"
        objFrm.Show()
    End Sub

    Private Sub mnuQueueDespatchUpdate_Click(sender As Object, e As EventArgs) Handles mnuQueueDespatchUpdate.Click
        Dim objFrm As New frmQueue("DU")
        objFrm.MdiParent = Me
        objFrm.Text = "Queue Despatch"
        objFrm.Show()
    End Sub

    Private Sub mnuInwardUpdateRcvdDate_Click(sender As Object, e As EventArgs) Handles mnuInwardUpdateRcvdDate.Click
        Dim objFrm As New frmInwardList(0, True)
        objFrm.MdiParent = Me
        objFrm.Show()
    End Sub

    Private Sub mnuHistoryReport_Click(sender As Object, e As EventArgs) Handles mnuHistoryReport.Click
        Dim frm As New frmHistoryReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BenpostToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BenpostToolStripMenuItem.Click
        Dim frm As New frmBenpostReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuBenpostRecon_Click(sender As Object, e As EventArgs) Handles mnuBenpostRecon.Click
        Dim frm As New frmBenpostRecon
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuInterDepositoryReport_Click(sender As Object, e As EventArgs) Handles mnuInterDepositoryReport.Click
        Dim frm As New frmInterDepositoryReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuGenCoveringLetter_Click(sender As Object, e As EventArgs) Handles mnuGenCoveringLetter.Click
        Dim objFrm As New frmGenerateCoveringLetter2
        objFrm.MdiParent = Me
        objFrm.Show()
    End Sub

    Private Sub mnuDelFile_Click(sender As Object, e As EventArgs) Handles mnuDelFile.Click
        Dim objFrm As New frmDeleteFile
        objFrm.ShowDialog()
    End Sub

    Private Sub mnuComparisonReport_Click(sender As Object, e As EventArgs) Handles mnuComparisonReport.Click
        Dim frm As New frmBenpostComparisonReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuFolioSharesReport_Click(sender As Object, e As EventArgs)
        Dim frm As New frmFolioSharesReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuAlphaIndex_Click(sender As Object, e As EventArgs) Handles mnuAlphaIndex.Click
        Dim frm As New frmalphaindex
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuTopShareHolderList_Click(sender As Object, e As EventArgs) Handles mnuTopShareHolderList.Click
        Dim frm As New frmtop_shareholdersnew
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuBenpostShareSummary_Click(sender As Object, e As EventArgs) Handles mnuBenpostShareSummary.Click
        Dim frm As New frmBenpostsharesabove1000
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuInwardSummary_Click(sender As Object, e As EventArgs) Handles mnuInwardSummary.Click
        Dim frm As New frminwreport_period
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuDisbScheReport_Click(sender As Object, e As EventArgs) Handles mnuDisbScheReport.Click
        Dim frm As New frmdis_schedule
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuCatReport_Click(sender As Object, e As EventArgs) Handles mnuCatReport.Click
        Dim frm As New frmcategoryreport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ECSValidationFileGenerateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSValidationFileGenerateToolStripMenuItem.Click
        Dim frm As New frmECSFileGenerate
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PaymentProcessFileGenerationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentProcessFileGenerationToolStripMenuItem.Click
        Dim frm As New frmDividendFileGeneration
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PaymenyUploadFileGenerationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymenyUploadFileGenerationToolStripMenuItem.Click
        Dim frm As New frmPaymentupload
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ImportECSValidationFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportECSValidationFileToolStripMenuItem.Click

    End Sub

    Private Sub ECSValidFileImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSValidFileImportToolStripMenuItem.Click
        Dim frm As New frmECSValidfileimport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ECSBounceFileImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSBounceFileImportToolStripMenuItem.Click
        Dim frm As New frmECSBounceFileImport
        frm.ShowDialog()
    End Sub

    Private Sub ECSIntialRejectFileImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSIntialRejectFileImportToolStripMenuItem.Click
        Dim frm As New frmECSInitialRejectImport
        frm.ShowDialog()
    End Sub

    Private Sub DDWarrantDollarNoUpdationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DDWarrantDollarNoUpdationToolStripMenuItem.Click
        Dim frm As New frmDDWarrantUpdation
        frm.ShowDialog()
    End Sub

    Private Sub RevokePaymentUploadFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevokePaymentUploadFileToolStripMenuItem.Click
        Dim frm As New frmRevokePaymentupload
        frm.ShowDialog()
    End Sub

    Private Sub RevokePaymentProcessFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevokePaymentProcessFileToolStripMenuItem.Click
        Dim frm As New frmRevokePaymentprocess
        frm.ShowDialog()
    End Sub

    Private Sub PostPaymentProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PostPaymentProcessToolStripMenuItem.Click
        Dim frm As New frmPostPaymentProcess
        frm.ShowDialog()
    End Sub

    Private Sub RevokePostPaymentProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevokePostPaymentProcessToolStripMenuItem.Click
        Dim frm As New frmRevokepostpayment
        frm.ShowDialog()
    End Sub

    Private Sub PostRepaymentGenerateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PostRepaymentGenerateToolStripMenuItem.Click
        Dim frm As New frmPostRepayment
        frm.ShowDialog()
    End Sub

    Private Sub RevokeRepaymentGenerateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevokeRepaymentGenerateToolStripMenuItem.Click
        Dim frm As New frmRevokeRepaymentGenerate
        frm.ShowDialog()
    End Sub

    Private Sub DividendRegisterReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DividendRegisterReportToolStripMenuItem.Click
        Dim frm As New frmDividendReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DemandDraftFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DemandDraftFileToolStripMenuItem.Click
        Dim frm As New frmDemandDraft
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub WarrantFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WarrantFileToolStripMenuItem.Click
        Dim frm As New frmwarrantfile
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DollarDraftFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DollarDraftFileToolStripMenuItem.Click
        Dim frm As New frmDollarDraft
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ECSFileGenerateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSFileGenerateToolStripMenuItem.Click
        Dim frm As New frmECSValidationfile
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ECSValidationReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSValidationReportToolStripMenuItem.Click
        Dim frm As New frmECSValidationReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ECSInitialRejectReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSInitialRejectReportToolStripMenuItem.Click
        Dim frm As New frmECSInitialReject
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ECSBounceReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSBounceReportToolStripMenuItem.Click
        Dim frm As New frmECSBounceReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DividendFileGenerateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DividendFileGenerateToolStripMenuItem.Click
        Dim frm As New frmdividend_report
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ECSPaymentReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECSPaymentReportToolStripMenuItem.Click
        Dim frm As New frmECSPaymentReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub AnnualReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnnualReportToolStripMenuItem.Click
        Dim frm As New frmAnnualReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PaymentInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentInfoToolStripMenuItem.Click
        Dim frm As New frmpaymentinfo
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BankInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BankInfoToolStripMenuItem.Click
        Dim frm As New FrmDividendbankinfo
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub AnnualReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnnualReturnToolStripMenuItem.Click
        Dim frm As New frmanul_report
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DmatRematConformationReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DmatRematConformationReportToolStripMenuItem.Click
        Dim frm As New frm_dmat_remat_conf_new1
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub SH2ReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SH2ReportToolStripMenuItem.Click
        Dim frm As New frm_SH2
        frm.MdiParent = Me
        frm.Show()
    End Sub
   
    Private Sub InwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InwardToolStripMenuItem.Click
        Dim frm As New frmrudinward
        frm.ShowDialog()
    End Sub

    Private Sub DespatchToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DespatchToolStripMenuItem.Click
        Dim frm As New frmRudOutward
        frm.ShowDialog()
    End Sub

    Private Sub RUDCorrespondenceToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles RUDCorrespondenceToolStripMenuItem1.Click
        Dim frm As New frmRudCorresReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DividendPaidUnpaidReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DividendPaidUnpaidReportToolStripMenuItem.Click
        Dim frm As New Frm_divpaid_unpaid
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PaymentPaidFileImportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PaymentPaidFileImportToolStripMenuItem.Click
        Dim frm As New frmPaymentpaidimport
        frm.ShowDialog()
    End Sub

    Private Sub PostPaymentPaidProcessToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PostPaymentPaidProcessToolStripMenuItem.Click
        Dim frm As New frmPostpaidprocess
        frm.ShowDialog()
    End Sub

    Private Sub RevokePaymentPaidProcessToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RevokePaymentPaidProcessToolStripMenuItem.Click
        Dim frm As New frmRevokepaidprocess
        frm.ShowDialog()
    End Sub

    Private Sub UnpaidRemainderLetterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnpaidRemainderLetterToolStripMenuItem.Click
        Dim frm As New Frm_unpaidremainderLetter
        frm.ShowDialog()
    End Sub

    Private Sub ShareholdingPatternToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShareholdingPatternToolStripMenuItem.Click
        Dim frm As New frmshp
        frm.ShowDialog()
    End Sub

    Private Sub PromoterMaintanenceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PromoterMaintanenceToolStripMenuItem.Click
        Dim frm As New frmShareholdingpatterninfo
        frm.ShowDialog()
    End Sub

    Private Sub EVotingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EVotingToolStripMenuItem.Click
        Dim frm As New Frm_eVoting
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuTran_Click(sender As Object, e As EventArgs) Handles mnuTran.Click

    End Sub

    Private Sub mnuInwardUpdateApprovedDate_Click(sender As Object, e As EventArgs) Handles mnuInwardUpdateApprovedDate.Click
        Dim objFrm As New frmInwardList(0, False, True)
        objFrm.MdiParent = Me
        objFrm.Show()
    End Sub

    Private Sub mnuInterDepositoryNewReport_Click(sender As Object, e As EventArgs) Handles mnuInterDepositoryNewReport.Click
        Dim frm As New frmInterDepositoryNewReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuDTInwardEntry_Click(sender As Object, e As EventArgs) Handles mnuDTInwardEntry.Click
        Dim objFrm As New frmDTInwardEntry("ADD")
        objFrm.ShowDialog()
    End Sub

    Private Sub BeneficiaryDetailsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BeneficiaryDetailsToolStripMenuItem.Click
        Dim objfrm As New frmBeneficiaryDetails
        objfrm.ShowDialog()
    End Sub

    Private Sub PostDispatchAndResponseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PostDispatchAndResponseToolStripMenuItem.Click
        Dim objfrm As New frmPostDispatchResponse
        objfrm.ShowDialog()
    End Sub

    Private Sub LetterDispatchToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LetterDispatchToolStripMenuItem.Click
        Dim objfrm As New frmLetterDispatchRpt
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub LetterResponseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LetterResponseToolStripMenuItem.Click
        Dim objfrm As New frmLetterResponseRpt
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub AuditLogToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AuditLogToolStripMenuItem.Click
        Dim objfrm As New frmAuditLog_Rpt
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub AddressLabelPrintingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddressLabelPrintingToolStripMenuItem.Click
        Dim objFrm As New frmAddressLabelPrinting
        objFrm.ShowDialog()
    End Sub

    Private Sub AMLReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AMLReportToolStripMenuItem.Click
        Dim objfrm As New frmAMLReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub InterDepositoryReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InterDepositoryReportToolStripMenuItem.Click
        Dim objfrm As New frmInterDepositoryRpt
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub InterDepositoryEntryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InterDepositoryEntryToolStripMenuItem.Click
        Dim objFrm As New frmInterDepositoryEnt
        objFrm.ShowDialog()
    End Sub

    Private Sub IEPFUploadToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles IEPFUploadToolStripMenuItem.Click
        Dim objFrm As New frmIEPFUpload
        objFrm.ShowDialog()
    End Sub

    Private Sub RevokeIEPFUploadToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RevokeIEPFUploadToolStripMenuItem.Click
        Dim objFrm As New frmRevokeIEPF
        objFrm.ShowDialog()
    End Sub

    Private Sub IEPFReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles IEPFReportToolStripMenuItem.Click
        Dim frm As New frmIEPFRpt
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DividendStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DividendStatusToolStripMenuItem.Click
        Dim frm As New frmDividendStatusRpt
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DividendMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DividendMasterToolStripMenuItem.Click
        Dim frm As New frmDividendMasterRpt
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub AMLRuleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AMLRuleToolStripMenuItem.Click
        Dim Objfrm As New frmAmlRule()
        Objfrm.ShowDialog()
    End Sub

    Private Sub AMLApplyRuleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AMLApplyRuleToolStripMenuItem.Click
        Dim Objfrm As New frmAmlApplyRule()
        Objfrm.ShowDialog()
    End Sub

    Private Sub AMLProcessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AMLProcessToolStripMenuItem.Click
        Dim Objfrm As New frmAMLResult()
        Objfrm.ShowDialog()
    End Sub

    Private Sub AMLResultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AMLResultToolStripMenuItem.Click
        Dim frm As New frmAmlresultRpt()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BenpostToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BenpostToolStripMenuItem1.Click
        Dim frm As New frmBenpostIdentification_Rpt()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub NSDLCategory_Click(sender As System.Object, e As System.EventArgs) Handles NSDLCategory.Click
        Dim Objfrm As New frmNSDLCategory()
        Objfrm.ShowDialog()
    End Sub

    Private Sub NSDL_Click(sender As System.Object, e As System.EventArgs) Handles NSDL.Click
        Dim Objfrm As New frmEvotingNsdl()
        Objfrm.ShowDialog()
    End Sub

    Private Sub CDSL_Click(sender As System.Object, e As System.EventArgs) Handles CDSL.Click
        Dim Objfrm As New frmEvotingCdsl()
        Objfrm.ShowDialog()
    End Sub

    Private Sub DividendAccountMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DividendAccountMasterToolStripMenuItem.Click
        Dim frm As New frmDividendAccountMasterRpt
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DividendShareCapitalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DividendShareCapitalToolStripMenuItem.Click
        Dim frm As New frmDividendShareCapitalRpt
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DividendLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DividendLogToolStripMenuItem.Click
        Dim frm As New frmDividendLogRpt
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DividendSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DividendSummaryToolStripMenuItem.Click
        Dim frm As New frmDividendSummaryRpt
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        Dim frm As New frmDividendReset
        frm.ShowDialog()
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        Dim Objfrm As New frmCertificatePrePrint()
        Objfrm.ShowDialog()
    End Sub

    Private Sub CertificatePrePrintHeadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CertificatePrePrintHeadToolStripMenuItem.Click
        Dim frm As New frmCertificateppHeadReport()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub CertificatePrePrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CertificatePrePrintToolStripMenuItem.Click
        Dim frm As New frmCertificatePrePrintReport()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub UtilizedToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles UtilizedToolStripMenuItem.Click
        Dim Objfrm As New frmCertificatePrePrintUtilized()
        Objfrm.ShowDialog()
    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        Dim Objfrm As New frmCertificatePPCancel()
        Objfrm.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem20_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem20.Click
        Dim Objfrm As New frmCertificatePPSplitofShares()
        Objfrm.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem21_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem21.Click
        Dim frm As New frmPrevandCurrwkbenpostComparsionReport()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PendingBenpost_Click(sender As Object, e As EventArgs) Handles PendingBenpost.Click
        Dim frm As New frmPendingBenpostReport()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub CompanyMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanyMasterToolStripMenuItem.Click
        Dim Objfrm As New frmCompanyMasterNew()
        Objfrm.ShowDialog()
    End Sub

    Private Sub GroupPanToolStripMenuItem(sender As Object, e As EventArgs) Handles mnuGroupPanReport.Click
        Dim objfrm As New frmGroupPanReport()
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuLetterOfConfirmation_Click(sender As Object, e As EventArgs) Handles mnuLetterOfConfirmation.Click
        Dim objfrm As New frmLetterOfConfirmation()
        objfrm.ShowDialog()
    End Sub

    Private Sub PledgeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PledgeToolStripMenuItem.Click
        Dim frm As New frmPledgeReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub IEPFReportToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles IEPFReportToolStripMenuItem1.Click
        Dim frm As New frmIEPFreport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DebarrtEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebarrtEntryToolStripMenuItem.Click
        Dim objfrm As New frmDebarrtEntry()
        objfrm.ShowDialog()
    End Sub

    Private Sub DebarrtReleaseEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebarrtReleaseEntryToolStripMenuItem.Click
        Dim objfrm As New frmDebarrtReleaseEntry()
        objfrm.ShowDialog()
    End Sub

    Private Sub DebarrtReleaseOrderReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebarrtReleaseOrderReportToolStripMenuItem.Click
        Dim frm As New frmDebarrtOrderReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PanStatusReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PanStatusReportToolStripMenuItem.Click
        Dim frm As New frmPanstatusReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DebarrtPanBenpostReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebarrtPanBenpostReportToolStripMenuItem.Click
        Dim frm As New frmBenpostDebarrtPanReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DebarrtPanFolioReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebarrtPanFolioReportToolStripMenuItem.Click
        Dim frm As New frmFolioDebarrtPanReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub CompanyMasterReport_Click(sender As Object, e As EventArgs) Handles CompanyMasterReport.Click
        Dim frm As New frmCompMasterReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuInwardDashboardReport_Click(sender As Object, e As EventArgs) Handles mnuInwardDashboardReport.Click
        Dim frm As New frmInwardDashboardReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DayToolStripMenuItem.Click
        Dim frm As New frmDaywisebenpostreport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DpMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DpMasterToolStripMenuItem.Click
        Dim Objfrm As New frmDpMaster()
        Objfrm.ShowDialog()
    End Sub

    Private Sub mnulettergenerator_Click(sender As Object, e As EventArgs) Handles mnulettergenerator.Click
        Dim frm As New frmLetterGenerator
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub CompanyGroupMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanyGroupMasterToolStripMenuItem.Click
        Dim Objfrm As New frmCompanyGroupMaster
        Objfrm.ShowDialog()
    End Sub

    Private Sub CompanySubGroupMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanySubGroupMasterToolStripMenuItem.Click
        Dim Objfrm As New frmCompanySubGroupMaster
        Objfrm.ShowDialog()
    End Sub

    Private Sub LOCReminderToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem23.Click
        Dim frm As New frmLocReminderReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub LOCReminderSentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOCReminderSentToolStripMenuItem.Click
        Dim frm As New frmLocReminderSentReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BenpostBulkImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BenpostBulkImportToolStripMenuItem.Click
        Dim frm As New frmBenposBulkimport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BenpostBulkUploadStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BenpostBulkUploadStatusToolStripMenuItem.Click
        Dim frm As New frmBenpostBulkImportReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuInwardDocsubtypeRptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuInwardDocsubtypeRptToolStripMenuItem.Click
        Dim frm As New frmInwardDocSubtypeReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuCompanyMstVsFolioMstTallyRpt_Click(sender As Object, e As EventArgs) Handles mnuCompanyMstVsFolioMstTallyRpt.Click
        Dim frm As New frmCompMstvsFolioMstReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuElectronicCreditReport_Click(sender As Object, e As EventArgs) Handles mnuElectronicCreditReport.Click
        Dim frm As New frmElectronicCreditReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuIEPFCoveringLetter_Click(sender As Object, e As EventArgs) Handles mnuIEPFCoveringLetter.Click
        Dim frm As New frmiepfgeneratecoveringletter
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub FolioSignatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FolioSignatureToolStripMenuItem.Click
        Dim frm As New frmFolioSignatureReport
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ISRGenerateCovertingLetterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ISRGenerateCovertingLetterToolStripMenuItem.Click
        Dim frm As New frmISRGenerateCoveringLetter
        frm.MdiParent = Me
        frm.Show()
    End Sub
End Class
