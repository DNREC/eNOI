Imports System.IO


Public Class SubmissionDetails
    Inherits FormBasePage


    Public Overrides Sub UIPrep()
        Try
            Dim submission As NOISubmission = IndNOISubmission

            Dim substatus As NoticeOfIntent.NOISubmissionStatus = submission.NOISubmissionStatus.OrderByDescending(Function(a) a.SubmissionStatusDate).First()

            If substatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString Or substatus.SubmissionStatusCode = SubmissionStatusCode.R.ToString Then
                btnDelete.Visible = True
                btnFile.Visible = True
                btnView.Visible = True
                If isExternal = False Then
                    btnNewPayment.Visible = True
                End If
            ElseIf substatus.SubmissionStatusCode = SubmissionStatusCode.F.ToString Then
                btnCopyOfRecord.Visible = True
                btnView.Visible = True
                If isExternal = False Then
                    btnPreview.Visible = True
                    btnAccept.Visible = True
                    btnReject.Visible = True
                    btnReturn.Visible = True
                Else
                    btnReturn.Visible = True
                End If
            ElseIf substatus.SubmissionStatusCode = SubmissionStatusCode.A.ToString Or substatus.SubmissionStatusCode = SubmissionStatusCode.X.ToString Then
                btnCopyOfRecord.Visible = True
                btnPreview.Visible = True
                btnView.Visible = True
            End If

            Dim owner As NOISubmissionPersonOrg = Nothing

            Select Case logInVS.submissiontype
                Case NOISubmissionType.GeneralNOIPermit, NOISubmissionType.TerminateNOI, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                    owner = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)
                Case NOISubmissionType.CoPermittee, NOISubmissionType.TerminateCoPermittee
                    owner = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)
            End Select

            Dim subtype As NOIProgSubmissionType = CacheLookupData.GetSubmissionTypeDetailsBySubmissionTypeID(submission.ProgSubmissionTypeID)

            Select Case logInVS.reportid
                Case NOIProgramType.PesticideGeneralPermit
                    SetupUIForPesticides()
                    txtOwnerName.Text = owner.LName & ", " & owner.FName
                Case NOIProgramType.CSSGeneralPermit
                    btnUploadDoc.Visible = False
                    If owner.PersonOrgTypeCode = "P" Then
                        txtOwnerName.Text = owner.LName & ", " & owner.FName
                    Else
                        txtOwnerName.Text = owner.OrgName
                    End If
                Case Else
                    If owner.PersonOrgTypeCode = "P" Then
                        txtOwnerName.Text = owner.LName & ", " & owner.FName
                    Else
                        txtOwnerName.Text = owner.OrgName
                    End If
            End Select



            txtOwnerAddress.Text = owner.Address1 & IIf(Not String.IsNullOrEmpty(owner.Address2), ", " & owner.Address2, "") & ", " & owner.StateAbv & ", " & owner.PostalCode

            Dim project As NOIProject = submission.NOIProject
            txtPermitNumber.Text = project.PermitNumber
            txtProjectName.Text = project.ProjectName
            Dim projectadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)
            txtProjectAddress.Text = projectadd.Address1 & ", " & projectadd.City & ", " & projectadd.StateAbv & ", " & projectadd.PostalCode
            txtSubmissionType.Text = subtype.SubmissionTypeDesc

            Dim Preparedby As String = String.Empty
            Dim Prepareremail As String = String.Empty
            Dim Preparercompany As String = String.Empty

            If NOILogin.GetUserInfo(submission.CreatedBy, Preparedby, Prepareremail, Preparercompany) Then
                If Preparedby = String.Empty Then
                    txtPreparedBy.Text = logInVS.user.userid
                Else
                    txtPreparedBy.Text = Preparedby
                    txtPreparedCompany.Text = Preparercompany
                End If

            End If

            If submission.SubmissionID > 0 Then
                txtSubmissionID.Text = submission.SubmissionID
            End If

            txtApplicationFee.Text = subtype.Fee
            If submission.NOIFeeExemption.Count > 0 Then
                txtExemptionCode.Text = submission.NOIFeeExemption(0).ExemptionCode
                txtExemptionCode.Enabled = False
            End If

            If Not submission.AmountPaid Is Nothing Then
                txtAmountPaid.Text = Convert.ToDecimal(submission.AmountPaid).ToString("0.00")
            Else
                txtAmountPaid.Text = "0.00"
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub SetupUIForPesticides()
        btnUploadDoc.Visible = False
        lblProjectName.Text = "Company Name/Operator Name"
    End Sub


    Public Function rptSubmissionStatus_GetData() As IQueryable
        Dim bal As New NOIBAL()
        Return bal.GetSubmissionStatusesBySubmissionID(logInVS.submissionid)
    End Function

    'Private Function isProjectNameExists(submission As NOISubmission) As Boolean
    '    Dim abal As New AdminBAL
    '    Dim prolst As List(Of String)
    '    prolst = abal.GetInternalProjectListByName(submission.NOIProject.ProjectName)
    '    If prolst.Count = 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim bal As New NOIBAL()
        bal.RemoveSessionStorageByUser(logInVS.user.userid) ' HttpContext.Current.User.Identity.Name)
        logInVS.submissionid = 0
        logInVS.submissiontype = 0
        If isExternal = True Then
            responseRedirect("~/Forms/main.aspx")
        Else
            responseRedirect("~/Admin/Submissions.aspx")
        End If
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click

        Select Case logInVS.submissiontype
            Case NOISubmissionType.GeneralNOIPermit, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                responseRedirect("~/Forms/NOIApplication.aspx")
            Case NOISubmissionType.CoPermittee
                responseRedirect("~/Forms/CoPermitteApplication.aspx")
            Case NOISubmissionType.TerminateCoPermittee
                responseRedirect("~/Forms/NOTCoPermittee.aspx")
            Case NOISubmissionType.TerminateNOI
                responseRedirect("~/Forms/NOTGeneralPermit.aspx")
        End Select
    End Sub


    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If cvtxtExemptionCode.IsValid Then
            responseRedirect("~/Forms/PreviewSubmission.aspx")
        End If

    End Sub

    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click

        Dim submission As NOISubmission = IndNOISubmission
        If cvtxtExemptionCode.IsValid Then  'isProjectNameExists(submission) = True And
            If isExternal = True Then
                responseRedirect("~/Forms/PreviewSubmission.aspx")
            Else
                submission.EntityState = EntityState.Modified
                Dim filedstatus As New NOISubmissionStatus
                filedstatus.EntityState = EntityState.Added
                filedstatus.SubmissionStatusCode = SubmissionStatusCode.F.ToString
                filedstatus.SubmissionID = submission.SubmissionID
                submission.NOISubmissionStatus.Add(filedstatus)
                If logInVS.submissiontype = NOISubmissionType.GeneralNOIPermit Then
                    submission.NOIReceivedDate = Now.Date
                ElseIf logInVS.submissiontype = NOISubmissionType.CoPermittee Then
                    submission.CopermitteeReceivedDate = Now.Date
                End If
                submission.IsLocked = YESNO.Y.ToString()


                Select Case logInVS.reportid
                    Case NOIProgramType.CSSGeneralPermit
                        Dim bal As New SWBAL
                        IndNOISubmission = bal.Insert(submission)
                    Case NOIProgramType.ISGeneralPermit
                        Dim bal As New ISWBAL
                        IndNOISubmission = bal.Insert(submission)
                    Case NOIProgramType.PesticideGeneralPermit
                        Dim bal As New APBAL
                        IndNOISubmission = bal.Insert(submission)
                End Select

                If AcceptSubmission(txtAcceptComments.Text) Then
                    responseRedirect("~/Forms/SubmissionDetails.aspx")
                End If
            End If
        End If
    End Sub


    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnSubmitReject.Click
        If Not RejectSubmission(txtRejectComments.Text) Then
            ErrorSummary.AddError("Failed to reject the submission", Me)
        Else
            responseRedirect("~/Forms/SubmissionDetails.aspx")
        End If
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnSubmitReturn.Click
        If Not ReturnSubmission(txtReturnComments.Text) Then
            ErrorSummary.AddError("Failed to return the submission", Me)
        Else
            responseRedirect("~/Forms/SubmissionDetails.aspx")
        End If
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnSubmitAccept.Click
        Dim bal As New NOIBAL
        Dim submission As NOISubmission = IndNOISubmission
        Try
            If submission.CORURL <> String.Empty Then
                If CROMERRExchange.IsDocumentSigned(Me, logInVS) Then
                    If AcceptSubmission(txtAcceptComments.Text) Then

                        responseRedirect("~/Forms/SubmissionDetails.aspx")
                    End If
                Else
                    ErrorSummary.AddError("Couldn't accept the submission as the Copy of Record is not yet signed by the respective person(s).", Me)
                End If
            Else
                If AcceptSubmission(txtAcceptComments.Text) Then
                    responseRedirect("~/Forms/SubmissionDetails.aspx")
                End If
            End If
        Catch ex As Exception
            ErrorSummary.AddError("Couldn't accept the submission check inner exception", Me)
            bal.LogError(Me, ex)
        End Try
    End Sub

    Private Sub btnCopyOfRecord_Click(sender As Object, e As EventArgs) Handles btnCopyOfRecord.Click
        Dim submission As NOISubmission = IndNOISubmission
        If Not String.IsNullOrEmpty(submission.CORURL) Then
            Response.Write("<script>window.open('" + submission.CORURL + "','_blank');</script>")
            'Response.Redirect(submission.CORURL)
        Else
            ErrorSummary.AddError("The copy of record for this submission does not exist.", Me)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub


    Private Function RejectSubmission(comments As String) As Boolean

        If CROMERRExchange.Submission_ChangeStatus_Reject(Me, logInVS) Then
            Dim bal As New NOIBAL()
            Dim submission As NOISubmission = IndNOISubmission

            submission.EntityState = EntityState.Modified
            Dim rejectstatus As New NOISubmissionStatus
            rejectstatus.EntityState = EntityState.Added
            rejectstatus.SubmissionStatusCode = SubmissionStatusCode.X.ToString
            rejectstatus.SubmissionID = submission.SubmissionID
            rejectstatus.SubmissionStatusComment = comments
            submission.NOISubmissionStatus.Add(rejectstatus)

            Try
                If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                    Dim balcs As New SWBAL
                    IndNOISubmission = balcs.Insert(submission)
                ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                    Dim balIS As New ISWBAL
                    IndNOISubmission = balIS.Insert(submission)
                ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                    Dim balAP As New APBAL
                    IndNOISubmission = balAP.Insert(submission)
                End If

                If Me.SendMailConfirmation = True Then
                    submission = IndNOISubmission
                    Emailer.SendEmailSubmissionRejectedToOwner(logInVS, submission, comments)
                End If

                Return True

            Catch ex As Exception
                ErrorSummary.AddError("Failed to reject submission", Me)
                bal.LogError(Me, ex)
                Return False
            End Try
        Else
            ErrorSummary.AddError("Failed to reject submission", Me)
            Return False
        End If
    End Function


    Private Function ReturnSubmission(comments As String) As Boolean

        If CROMERRExchange.Submission_ChangeStatus_Reject(Me, logInVS) Then
            Dim bal As New NOIBAL()
            Dim submission As NOISubmission = IndNOISubmission

            submission.EntityState = EntityState.Modified
            submission.IsLocked = YESNO.N.ToString()
            Dim returnstatus As New NOISubmissionStatus
            returnstatus.EntityState = EntityState.Added
            returnstatus.SubmissionStatusCode = SubmissionStatusCode.R.ToString
            returnstatus.SubmissionID = submission.SubmissionID
            returnstatus.SubmissionStatusComment = comments
            submission.NOISubmissionStatus.Add(returnstatus)

            Try

                If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                    Dim balcs As New SWBAL
                    IndNOISubmission = balcs.Insert(submission)
                ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                    Dim balIS As New ISWBAL
                    IndNOISubmission = balIS.Insert(submission)
                ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                    Dim balAP As New APBAL
                    IndNOISubmission = balAP.Insert(submission)
                End If


                If Me.SendMailConfirmation = True Then
                    submission = IndNOISubmission
                    Emailer.SendEmailSubmissionReturnedToOwner(logInVS, submission, comments)
                End If


                Return True

            Catch ex As Exception
                ErrorSummary.AddError("Failed to return the submission", Me)
                bal.LogError(Me, ex)
                Return False
            End Try
        Else
            ErrorSummary.AddError("Failed to return the submission", Me)
            Return False
        End If
    End Function

    Private Function AcceptSubmission(comments As String) As Boolean
        Dim admindb As New AdminBAL

        Dim submission As NOISubmission = IndNOISubmission
        Try

            Select Case logInVS.submissiontype

                Case NOISubmissionType.GeneralNOIPermit
                    admindb.AcceptGeneralNOI(logInVS)
                Case NOISubmissionType.CoPermittee
                    admindb.AcceptCoPermitteeNOI(logInVS)
                Case NOISubmissionType.TerminateCoPermittee
                    admindb.AcceptCoPermitteeNOT(logInVS)
                Case NOISubmissionType.TerminateNOI
                    admindb.AcceptGeneralNOT(logInVS)
                Case NOISubmissionType.GeneralNOICorrection
                    admindb.AcceptModifiedGeneralNOI(logInVS)
                Case NOISubmissionType.GeneralNOIRenewal
                    admindb.AcceptRenewalGeneralNOI(logInVS)
            End Select

            Dim buffer() As Byte = Nothing
            'buffer = NOIReports.createReport(logInVS.submissiontype, logInVS.submissionid)
            buffer = NOIReports.createReport(logInVS)


            Dim NOISubmissionAcceptedPDF As New NOISubmissionAcceptedPDF
            NOISubmissionAcceptedPDF.AcceptedReportPDF = buffer
            NOISubmissionAcceptedPDF.SubmissionID = logInVS.submissionid
            NOISubmissionAcceptedPDF.EntityState = EntityState.Added


            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim balcs As New SWBAL
                    submission = balcs.GetSubmissionByIDForDisplay(logInVS.submissionid, logInVS.submissiontype)
                Case NOIProgramType.ISGeneralPermit
                    Dim balIS As New ISWBAL
                    submission = balIS.GetSubmissionByIDForDisplay(logInVS.submissionid, logInVS.submissiontype)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim balap As New APBAL
                    submission = balap.GetSubmissionByIDForDisplay(logInVS.submissionid, logInVS.submissiontype)

            End Select


            Dim acceptstatus As NOISubmissionStatus = submission.NOISubmissionStatus.Where(Function(a) a.SubmissionID = logInVS.submissionid And a.SubmissionStatusCode = "A").SingleOrDefault()
            If acceptstatus IsNot Nothing Then
                acceptstatus.EntityState = EntityState.Modified
                acceptstatus.SubmissionStatusComment = acceptstatus.SubmissionStatusComment + " " + comments
            Else
                acceptstatus = New NOISubmissionStatus
                acceptstatus.SubmissionStatusCode = SubmissionStatusCode.A.ToString()
                acceptstatus.SubmissionID = submission.SubmissionID
                acceptstatus.SubmissionStatusComment = comments
                acceptstatus.EntityState = EntityState.Added
            End If


            submission.NOISubmissionAcceptedPDF.Add(NOISubmissionAcceptedPDF)
            submission.NOISubmissionStatus.Add(acceptstatus)

            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim balcs As New SWBAL
                    IndNOISubmission = balcs.Insert(submission)
                Case NOIProgramType.ISGeneralPermit
                    Dim balIS As New ISWBAL
                    IndNOISubmission = balIS.Insert(submission)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim balap As New APBAL
                    IndNOISubmission = balap.Insert(submission)
            End Select

            If Me.SendMailConfirmation = True Then
                submission = IndNOISubmission
                Emailer.SendEmailSubmissionApprovedToOwner(logInVS, submission)
            End If

            Return True
        Catch ex As Exception
            Dim bal As New NOIBAL()
            ErrorSummary.AddError("Failed to Accept submission" + ex.StackTrace.ToString() + "--" + ex.Message, Me)
            bal.LogError(Me, ex)
            Return False
        End Try
    End Function

    Private Sub btnAcceptNewPayment_Click(sender As Object, e As EventArgs) Handles btnAcceptNewPayment.Click
        Dim submission As NOISubmission = IndNOISubmission
        Dim bal As New SWBAL
        If IsNumeric(txtNewPayment.Text) Then
            Dim oldAmount As Double = 0.0
            If submission.AmountPaid IsNot Nothing Then
                oldAmount = submission.AmountPaid
            End If
            submission.AmountPaid = CDbl(txtNewPayment.Text)
            txtNewPayment.Text = String.Empty
            submission.EntityState = EntityState.Modified


            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                Dim balcs As New SWBAL
                IndNOISubmission = balcs.Insert(submission)
            Else
                Dim balIS As New ISWBAL
                IndNOISubmission = balIS.Insert(submission)
            End If
            UIPrep()
        Else
            ErrorSummary.AddError("New payment must be a valid numeric value.", Me)
        End If
    End Sub

    Private Sub cvtxtExemptionCode_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvtxtExemptionCode.ServerValidate
        Dim bal As New NOIBAL
        Dim today As Date = Now.Date
        Dim feeexemption As NOIFeeExemption = bal.GetAllExemptionCodes(logInVS.reportid).Where(Function(a) a.ExemptionCode = args.Value And a.ExpiresOn >= today).SingleOrDefault()
        If feeexemption Is Nothing Then
            args.IsValid = False
        Else
            Dim submission As NOISubmission = IndNOISubmission
            feeexemption.SubmissionID = submission.SubmissionID
            feeexemption.EntityState = EntityState.Modified
            bal.SaveExemptionCode(feeexemption)

            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                Dim balcs As New SWBAL
                IndNOISubmission = balcs.GetSubmissionByIDForDisplay(submission.SubmissionID, logInVS.submissiontype)
            Else
                Dim balIS As New ISWBAL
                IndNOISubmission = balIS.GetSubmissionByIDForDisplay(submission.SubmissionID, logInVS.submissiontype)
            End If
            args.IsValid = True
        End If
    End Sub


    Private Sub SubmissionDetails_Error(sender As Object, e As EventArgs) Handles Me.Error
        Me.registerErrorAndSendEmail(Server.GetLastError)
        Response.Redirect("~/Error/ErrorPage.aspx")
    End Sub

    Private Sub btnUploadDoc_Click(sender As Object, e As EventArgs) Handles btnUploadDoc.Click
        responseRedirect("~/Forms/NOIDocs.aspx")
    End Sub

    Private Sub btnApprovedEmail_Click(sender As Object, e As EventArgs) Handles btnApprovedEmail.Click
        Dim submission As NOISubmission = IndNOISubmission
        Emailer.SendEmailSubmissionApprovedToOwner(logInVS, submission)
    End Sub
End Class