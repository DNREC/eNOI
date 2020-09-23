Imports System.IO

Public Class PreviewSubmission
    Inherits FormBasePage


    Public Overrides Sub UIPrep()
        iframeDoc.Attributes.Add("Src", GetResponseRedirect("OpenFile.aspx"))

        btnContinue.Visible = (isExternal AndAlso (Not isViewOnly))

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        responseRedirect("~/Forms/SubmissionDetails.aspx")
    End Sub


    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Dim reportsent As Boolean = False
        Dim submission As NOISubmission = IndNOISubmission
        Dim totalfee As Decimal = CacheLookupData.GetFeesBySubmissionType(submission.ProgSubmissionTypeID)

        If totalfee = 0 OrElse (totalfee - submission.AmountPaid = 0) OrElse submission.NOIFeeExemption.Count > 0 Then

            Dim buffer() As Byte = Nothing
            buffer = NOIReports.createReport(logInVS) '.submissiontype, logInVS.submissionid)

            Dim CROMERRMgr As New CROMERRExchange
            AddHandler CROMERRMgr.TransferCompleted, AddressOf SentToCROMERRCompleted
            AddHandler CROMERRMgr.TransferFailed, AddressOf SentToCROMERRFailed

            reportsent = CROMERRMgr.SendReportToCROMERR(Me, buffer, logInVS, submission)

            If reportsent Then
                Dim owner As NOISubmissionPersonOrg
                If logInVS.submissiontype = NOISubmissionType.GeneralNOIPermit Or logInVS.submissiontype = NOISubmissionType.TerminateNOI Then
                    owner = submission.NOISubmissionPersonOrg.Single(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)
                ElseIf logInVS.submissiontype = NOISubmissionType.CoPermittee Or logInVS.submissiontype = NOISubmissionType.TerminateCoPermittee Then
                    owner = submission.NOISubmissionPersonOrg.Single(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)
                End If
                Dim subtype As NOIProgSubmissionType = CacheLookupData.GetSubmissionTypeDetailsBySubmissionTypeID(submission.ProgSubmissionTypeID)
                Dim sendemailto As String = String.Join(",", submission.NOISigningEmailAddress.Select(Function(a) a.EmailAddress))

                'Emailer.SendEmailNewSubmissionToOwner(submission.SubmissionID, owner.EmailAddress, subtype.SubmissionTypeDesc, buffer)
                If sendemailto.Length > 0 AndAlso Me.SendMailConfirmation = True Then
                    Emailer.SendEmailNewSubmissionToOwner(submission.SubmissionID, sendemailto, subtype.SubmissionTypeDesc, submission.NOIProject.ProjectName, buffer)
                    Emailer.SendEmailNewSubmissionToAdmin(submission.SubmissionID, Me.NOIAdminEmail, subtype.SubmissionTypeDesc, submission.NOIProject.ProjectName, logInVS.user.userid)
                End If

                If isExternal = True Then
                    responseRedirect(Me.cromErrLoginURL & "?Tkt=" & HttpUtility.UrlEncode(logInVS.loginToken))
                Else
                    responseRedirect("~/Admin/Submissions.aspx")
                End If
            End If
        Else
            responseRedirect("~/Forms/SessionInitiate.aspx")
        End If
    End Sub


    Private Sub SentToCROMERRCompleted(sender As Object, e As CROMERREventArgs)

        Dim submission As NOISubmission = IndNOISubmission

        submission.EntityState = EntityState.Modified
        Dim filedstatus As New NOISubmissionStatus
        filedstatus.EntityState = EntityState.Added
        filedstatus.SubmissionStatusCode = SubmissionStatusCode.F.ToString
        filedstatus.SubmissionID = submission.SubmissionID
        submission.NOISubmissionStatus.Add(filedstatus)
        submission.CORURL = e.documentURL
        If logInVS.submissiontype = NOISubmissionType.GeneralNOIPermit Then
            submission.NOIReceivedDate = Now.Date
        ElseIf logInVS.SubmissionType = NOISubmissionType.CoPermittee Then
            submission.CopermitteeReceivedDate = Now.Date
        End If
        submission.IsLocked = YESNO.Y.ToString()


        Try

            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim bal As New SWBAL
                    submission = bal.Insert(submission)
                Case NOIProgramType.ISGeneralPermit
                    Dim bal As New ISWBAL
                    submission = bal.Insert(submission)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim bal As New APBAL
                    submission = bal.Insert(submission)
            End Select

            IndNOISubmission = submission

            'todo commented for successful compilation. has to be removed and rewritten in a different way
            'bal.RemoveSessionStorageByUser(logInVS.user.userid) ' HttpContext.Current.User.Identity.Name)
            logInVS.submissionid = 0
            logInVS.submissiontype = 0
        Catch ex As Exception
            Dim bal As New NOIBAL
            ErrorSummary.AddError("Save Submission Failed", Me)
            bal.LogError(Me, ex)
        End Try



    End Sub

    Private Sub SentToCROMERRFailed(sender As Object, e As CROMERREventArgs)
        ErrorSummary.AddError("Filed Electronically is Failed. Try later.", Me)
    End Sub
End Class