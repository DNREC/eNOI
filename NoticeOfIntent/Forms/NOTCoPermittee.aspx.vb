Imports Newtonsoft.Json

Public Class NOTCoPermittee
    Inherits FormBasePage


    Private WriteOnly Property Enabled As Boolean
        Set(value As Boolean)
            txtCompletionDate.Enabled = value
            rblAchievedYesNo.Enabled = value
            rblStatisfiedYesNo.Enabled = value
            rblVerifiedYesNO.Enabled = value
            txtComments.Enabled = value
            'btnSave.Enabled = value
            'btnSubmit.Enabled = value
        End Set
    End Property

    Public Overrides Sub UIPrep()
        Try
            Dim bal As New SWBAL()

            hfNOISubmissionID.Value = logInVS.submissionid  ' Request.QueryString("refno")
            ucnaCoPermitteeInfo.StateAbv = "DE"
            Dim submission As NOISubmission
            If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
                submission = bal.GetSubmissionByIDForCoPermitteeNOT(hfNOISubmissionID.Value)
                IndNOISubmission = submission
                Enabled = Not isViewOnly
                'MapEntitiesToFields()
            ElseIf Request.QueryString("pnum") <> String.Empty AndAlso Request.QueryString("aid") <> String.Empty Then
                submission = bal.GetProjectCopermitteeDetailsByProjectID(Request.QueryString("pnum"), Convert.ToInt32(Request.QueryString("aid")))
                'bal.GetProjectCopermitteeDetailsByOriginalSubmission(Request.QueryString("origno"))
                IndNOISubmission = submission
                'MapEntitiesToFields()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub MapEntitiesToFields()
        Dim bal As New SWBAL()
        Dim submission As NOISubmission = IndNOISubmission


        lblNOIIDDisplay.Text = submission.NOIProject.PermitNumber
        lblNOIDateReceivedDisplay.Text = submission.NOIReceivedDate      ' bal.GetMainNOISubmissionFiledDateByProjectIDForDisplay(submission.NOIProject.ProjectID).ToString("MM-dd-yyyy")  
        txtComments.Text = submission.Comments

        ucnaProjectDetails.CompanyName = submission.NOIProject.ProjectName
        'ucnaProjectDetails.Address1 = submission.NOIProject.ProjectAddress
        'ucnaProjectDetails.City = submission.NOIProject.ProjectCity
        'ucnaProjectDetails.StateAbv = submission.NOIProject.ProjectStateAbv
        'ucnaProjectDetails.Zip = submission.NOIProject.ProjectPostalCode
        ucnaProjectDetails.County = submission.NOISubmissionSWConstruct.ProjectCounty
        ucnaProjectDetails.Municipality = submission.NOISubmissionSWConstruct.ProjectMunicipality

        lblCoPermitteeAppRecDateDisplay.Text = submission.CopermitteeReceivedDate

        Dim Copermittee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)


        ucnaCoPermitteeInfo.FirstName = Copermittee.FName
        ucnaCoPermitteeInfo.LastName = Copermittee.LName
        ucnaCoPermitteeInfo.CompanyName = Copermittee.OrgName
        ucnaCoPermitteeInfo.Address1 = Copermittee.Address1
        If Copermittee.Address2 <> "" Then
            ucnaCoPermitteeInfo.address2visible = True
            ucnaCoPermitteeInfo.Address2 = Copermittee.Address2
        Else
            ucnaCoPermitteeInfo.address2visible = False
        End If
        ucnaCoPermitteeInfo.City = Copermittee.City
        ucnaCoPermitteeInfo.StateAbv = Copermittee.StateAbv
        ucnaCoPermitteeInfo.Zip = Copermittee.PostalCode
        ucnaCoPermitteeInfo.Phone = Copermittee.Phone
        ucnaCoPermitteeInfo.Ext = Copermittee.PhoneExt
        ucnaCoPermitteeInfo.Mobile = Copermittee.Mobile
        ucnaCoPermitteeInfo.Email = Copermittee.EmailAddress

        If submission.SubmissionID <> 0 Then
            txtCompletionDate.Text = Convert.ToDateTime(submission.NOISubmissionSW.CCDateTermination).ToString("MM-dd-yyyy")
            rblStatisfiedYesNo.SelectedValue = submission.NOISubmissionSW.IsSatisfiedDSSR
            rblVerifiedYesNO.SelectedValue = submission.NOISubmissionSW.IsPlanVerifiedDSSR
            rblAchievedYesNo.SelectedValue = submission.NOISubmissionSW.IsFinalStablizationDone
        End If



    End Sub

    Private Function MapFieldsToEntity(Optional IsSubmitted As Boolean = False) As NOISubmission
        Dim mainSubmission As NOISubmission = IndNOISubmission
        Dim user As String = logInVS.user.userid

        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text
        If mainSubmission.EntityState = EntityState.Added Then
            mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype ' CacheLookupData.GetNOIProgSubmissionTypes().Where(Function(a) a.SubmissionTypeID = NOISubmissionType.TerminateCoPermittee And a.ProgramID = logInVS.reportid).Single.ProgSubmissionTypeID
            mainSubmission.CreatedBy = user
            Dim openstatus As New NOISubmissionStatus
            openstatus.EntityState = EntityState.Added
            openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString
            mainSubmission.NOISubmissionStatus.Add(openstatus)

            Dim noisw As New NOISubmissionSW()
            noisw.EntityState = EntityState.Added
            noisw.CCDateTermination = txtCompletionDate.Text
            noisw.IsSatisfiedDSSR = rblStatisfiedYesNo.SelectedValue
            noisw.IsPlanVerifiedDSSR = rblVerifiedYesNO.SelectedValue
            noisw.IsFinalStablizationDone = rblAchievedYesNo.SelectedValue

            mainSubmission.NOISubmissionSW = noisw
        Else
            mainSubmission.NOISubmissionSW.CCDateTermination = txtCompletionDate.Text
            mainSubmission.NOISubmissionSW.IsSatisfiedDSSR = rblStatisfiedYesNo.SelectedValue
            mainSubmission.NOISubmissionSW.IsPlanVerifiedDSSR = rblVerifiedYesNO.SelectedValue
            mainSubmission.NOISubmissionSW.IsFinalStablizationDone = rblAchievedYesNo.SelectedValue
        End If


        'If IsSubmitted = True Then
        '    mainSubmission.CopermitteeReceivedDate = Now.Date
        '    mainSubmission.IsLocked = "Y"
        '    Dim filedstatus As New NOISubmissionStatus
        '    filedstatus.EntityState = EntityState.Added
        '    filedstatus.SubmissionStatusCode = SubmissionStatusCode.F.ToString
        '    mainSubmission.NOISubmissionStatus.Add(filedstatus)
        'End If

        mainSubmission.NOISubmissionSWConstruct = Nothing
        IndNOISubmission = mainSubmission
        Return mainSubmission

    End Function


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim bal As New SWBAL()
        Dim ns As NOISubmission
        Try
            ns = IndNOISubmission
            If Not isViewOnly Then
                ns = bal.Insert(MapFieldsToEntity())
            End If

            hfNOISubmissionID.Value = ns.SubmissionID
            logInVS.submissionid = ns.SubmissionID
            IndNOISubmission = ns
            'bal.RemoveSessionStorageByUser(HttpContext.Current.User.Identity.Name)
            'logInVS.submissionid = 0
            'logInVS.submissiontype = 0
            'responseRedirect("~/Forms/main.aspx")

            If (ns.NOISubmissionStatus.OrderByDescending(Function(a) a.SubmissionStatusDate).First().SubmissionStatusCode = SubmissionStatusCode.O.ToString) OrElse
                        (ns.NOISubmissionStatus.OrderByDescending(Function(a) a.SubmissionStatusDate).First().SubmissionStatusCode = SubmissionStatusCode.R.ToString) Then
                responseRedirect("~/Forms/NOIAgreement.aspx")
            Else
                responseRedirect("~/Forms/SubmissionDetails.aspx")
            End If

        Catch ex As Exception
            Throw (ex)
        End Try
    End Sub



    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim bal As New NOIBAL()
        bal.RemoveSessionStorageByUser(logInVS.user.userid)   ' HttpContext.Current.User.Identity.Name)
        logInVS.submissionid = 0
        logInVS.submissiontype = 0
        If isExternal = True Then
            responseRedirect("~/Forms/main.aspx")
        Else
            responseRedirect("~/Admin/Submissions.aspx")
        End If
    End Sub

    Private Sub cvSatisfiedYesNo_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvSatisfiedYesNo.ServerValidate
        If args.Value = "N" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Private Sub cvVerifiedYesNO_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvVerifiedYesNO.ServerValidate
        If args.Value = "N" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Private Sub cvAchievedYesNo_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvAchievedYesNo.ServerValidate
        If args.Value = "N" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Private Sub NOTCoPermittee_Error(sender As Object, e As EventArgs) Handles Me.Error
        Me.registerErrorAndSendEmail(Server.GetLastError)
        Response.Redirect("~/Error/ErrorPage.aspx")
    End Sub
End Class