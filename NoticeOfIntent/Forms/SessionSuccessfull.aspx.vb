Imports System
Imports System.IO

Public Class SessionSuccessfull
    Inherits FormBasePage


    Public Overrides Sub UIPrep()
        Try
            Dim boolSubmitToCROMERR As Boolean = False
            Dim submission As NOISubmission = IndNOISubmission
            submission = processPaymentHandoff(submission)
            IndNOISubmission = submission
            If NOILogin.isValidToken(logInVS) Then
                boolSubmitToCROMERR = submitToCROMERR()
            Else
                ErrorSummary.AddError("Login invalid. Please click the below button to log back in.", Me)
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
            Throw ex
        End Try
    End Sub

    Private Function processPaymentHandoff(submission As NOISubmission) As NOISubmission
        Try

            submission.EntityState = EntityState.Modified
            Dim amountpaid As Decimal = IIf(submission.AmountPaid IsNot Nothing, submission.AmountPaid, 0.0)
            'If submission.AmountPaid IsNot Nothing Then
            '    amountpaid = submission.AmountPaid
            'Else
            '    amountpaid = 0.0
            'End If
            submission.AmountPaid = Payment.GetAmountPaid(Session("remittance_id").ToString)
            buildReceipt(submission)

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
            Dim subtype As NOIProgSubmissionType = CacheLookupData.GetSubmissionTypeDetailsBySubmissionTypeID(submission.ProgSubmissionTypeID)
            If Me.SendMailConfirmation = True Then
                Emailer.SendEmailForSuccessfullPayment(Me, subtype.SubmissionTypeDesc, submission.AmountPaid)
            End If

        Catch ex As Exception
            Dim bal As New NOIBAL
            ErrorSummary.AddError("Save payment details to Submission data Failed", Me)
            bal.LogError(Me, ex)
        End Try
        Return submission
    End Function



    Private Function submitToCROMERR() As Boolean
        Try
            Dim reportsent As Boolean = False
            Dim submission As NOISubmission = IndNOISubmission
            'submission = processPaymentHandoff(submission)


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
            End If

            Return reportsent

        Catch ex As Exception
            Throw ex
        End Try

    End Function

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
        ElseIf logInVS.submissiontype = NOISubmissionType.CoPermittee Then
            submission.CopermitteeReceivedDate = Now.Date
        End If
        submission.IsLocked = YESNO.Y.ToString()
        'submission.AmountPaid = Payment.GetAmountPaid(Me, Session("remittance_id").ToString)
        'buildReceipt(submission)

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



            hfpaymentsuccessfull.Value = 1
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
        Dim submission As NOISubmission = IndNOISubmission
        buildReceipt(submission)
        ErrorSummary.AddError("Filed Electronically is Failed. Try later.", Me)
    End Sub

    Protected Sub RemoveReceipt()
        ' Payment.RemoveReceipt(Me, Session("remittance_id"))
    End Sub

    Private Sub buildReceipt(submission As NOISubmission)
        Dim fetchDetails As Boolean = False
        Dim remittanceid As String = String.Empty
        Dim transactiondate As Date = Date.MinValue
        Dim paymentdesc As String = String.Empty
        Dim cardtype As String = String.Empty
        Dim partialcardnum As String = String.Empty
        Dim totalamount As Decimal = 0
        Dim appdesc As String = String.Empty
        Dim appaddress As String = String.Empty
        Dim appcity As String = String.Empty
        Dim appstate As String = String.Empty
        Dim appzip As String = String.Empty
        Dim appphone As String = String.Empty


        Try
            remittanceid = Session("remittance_id").ToString()
            'fetchDetails = Payment.PaymentReceiptDetails(Me, Me.getRegisteredAppIDForOnlinePay, remittanceid, transactiondate, paymentdesc, cardtype, partialcardnum, _
            'totalamount, appdesc, appaddress, appcity, appstate, appzip, appphone)
            fetchDetails = Payment.PaymentReceiptDetails(Me.getRegisteredAppIDForOnlinePay, remittanceid, transactiondate, paymentdesc, cardtype,
                                                totalamount, appdesc, appaddress, appcity, appstate, appzip, appphone)

            If fetchDetails = True Then
                Dim subtype As NOIProgSubmissionType = CacheLookupData.GetSubmissionTypeDetailsBySubmissionTypeID(submission.ProgSubmissionTypeID)

                lblDate.Text = transactiondate.ToShortDateString()
                lblDescription.Text = paymentdesc
                lblCardType.Text = cardtype
                lblCardNumber.Text = String.Empty
                lblAmount.Text = String.Format("{0:c}", totalamount)
                lblRemittanceID.Text = remittanceid
                lblPayeeName.Text = logInVS.user.fullName
                lblSubmissionID.Text = logInVS.submissionid
                lblAppType.Text = subtype.SubmissionTypeDesc
                lblApplicationDetails.Text = appdesc
                lblApplicationDetails.Text &= "<br/>" & appaddress
                lblApplicationDetails.Text &= "<br/>" & appcity & ", DE "
                lblApplicationDetails.Text &= appzip
                lblApplicationDetails.Text &= "<br/>" & appphone
            Else
                ErrorSummary.AddError("For security purposes, the transaction information is no longer available to view.", Me)
            End If
        Catch ex As Exception
            Dim bal As New NOIBAL
            bal.LogError(Me, ex)
            'clsPaymentDB.AddError("error message in buildreceipt function: " & ex.Message)
            Throw ex
        End Try
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        'RemoveReceipt()
        If hfpaymentsuccessfull.Value = 1 Then
            Response.Redirect(Me.cromErrLoginURL & "?Tkt=" & HttpUtility.UrlEncode(logInVS.loginToken))
        Else
            If NOILogin.isValidToken(logInVS) Then
                responseRedirect("~/Forms/SubmissionDetails.aspx")
            Else
                Response.Redirect(Me.cromErrLoginURL & "?ReportID=" & logInVS.reportid)
            End If
        End If
    End Sub
End Class