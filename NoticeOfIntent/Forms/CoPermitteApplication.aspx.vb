Imports Newtonsoft.Json

Public Class CoPermitteApplication
    Inherits FormBasePage


    'Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
    '    If Not IsPostBack Then
    '        UIPrep()
    '    End If
    'End Sub

    Private WriteOnly Property Enabled As Boolean
        Set(value As Boolean)
            ucnaCoPermitteeInfo.Enabled = value
            'btnSave.Enabled = value
            txtEffectiveDate.Enabled = value
            txtComments.Enabled = value
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
                submission = bal.GetSubmissionByIdForCoPermittee(hfNOISubmissionID.Value)
                IndNOISubmission = submission
                Enabled = Not isViewOnly
                'MapEntitiesToFields()
            ElseIf Request.QueryString("proid") <> String.Empty Then
                submission = bal.GetProjectOwnerDetailsByProjectIDForCoPermittee(Request.QueryString("proid"))
                IndNOISubmission = submission
                'MapEntitiesToFields()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub MapEntitiesToFields()
        'Dim bal As New SubmissionBAL()
        Dim submission As NOISubmission = IndNOISubmission


        lblNOIIDDisplay.Text = submission.NOIProject.PermitNumber
        lblNOIDateReceivedDisplay.Text = submission.NOIReceivedDate ' bal.GetMainNOISubmissionFiledDateByProjectIDForDisplay(submission.NOIProject.ProjectID).ToString("MM-dd-yyyy")  
        txtComments.Text = submission.Comments


        ucnaProjectDetails.CompanyName = submission.NOIProject.ProjectName

        Dim projadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)

        ucnaProjectDetails.Address1 = projadd.Address1
        ucnaProjectDetails.City = projadd.City
        ucnaProjectDetails.StateAbv = projadd.StateAbv
        ucnaProjectDetails.Zip = projadd.PostalCode

        ucnaProjectDetails.County = submission.NOISubmissionSWConstruct.ProjectCounty
        ucnaProjectDetails.Municipality = submission.NOISubmissionSWConstruct.ProjectMunicipality

        Dim owner As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)


        ucnaOriPermitteeInfo.CompanyType = owner.PersonOrgTypeCode
        If owner.PersonOrgTypeCode = "P" Then
            ucnaOriPermitteeInfo.companynamevisible = False
        Else
            ucnaOriPermitteeInfo.companynamevisible = True
            ucnaOriPermitteeInfo.CompanyName = owner.OrgName
        End If
        ucnaOriPermitteeInfo.FirstName = owner.FName
        ucnaOriPermitteeInfo.LastName = owner.LName
        ucnaOriPermitteeInfo.Address1 = owner.Address1
        If owner.Address2 <> "" Then
            ucnaOriPermitteeInfo.address2visible = True
            ucnaOriPermitteeInfo.Address2 = owner.Address2
        Else
            ucnaOriPermitteeInfo.address2visible = False
        End If
        ucnaOriPermitteeInfo.City = owner.City
        ucnaOriPermitteeInfo.StateAbv = owner.StateAbv
        ucnaOriPermitteeInfo.Zip = owner.PostalCode
        ucnaOriPermitteeInfo.Phone = owner.Phone
        ucnaOriPermitteeInfo.Ext = owner.PhoneExt
        ucnaOriPermitteeInfo.Mobile = owner.Mobile
        ucnaOriPermitteeInfo.Email = owner.EmailAddress

        If submission.SubmissionID <> 0 Then
            Dim copermittee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)

            ucnaCoPermitteeInfo.CompanyType = copermittee.PersonOrgTypeCode
            ucnaCoPermitteeInfo.CompanyName = copermittee.OrgName
            ucnaCoPermitteeInfo.FirstName = copermittee.FName
            ucnaCoPermitteeInfo.LastName = copermittee.LName
            ucnaCoPermitteeInfo.Address1 = copermittee.Address1
            ucnaCoPermitteeInfo.Address2 = copermittee.Address2
            ucnaCoPermitteeInfo.City = copermittee.City
            ucnaCoPermitteeInfo.StateAbv = copermittee.StateAbv
            ucnaCoPermitteeInfo.Zip = copermittee.PostalCode
            ucnaCoPermitteeInfo.Phone = copermittee.Phone
            ucnaCoPermitteeInfo.Ext = copermittee.PhoneExt
            ucnaCoPermitteeInfo.Mobile = copermittee.Mobile
            ucnaCoPermitteeInfo.Email = copermittee.EmailAddress

            txtEffectiveDate.Text = Convert.ToDateTime(copermittee.CoPermitteeEffectiveDate).ToString("MM-dd-yyyy")
        End If

    End Sub

    Private Function MapFieldsToEntity(Optional IsSubmitted As Boolean = False) As NOISubmission
        Dim mainSubmission As NOISubmission = IndNOISubmission
        Dim user As String = logInVS.user.userid

        mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype 'CacheLookupData.GetNOIProgSubmissionTypes().Where(Function(a) a.SubmissionTypeID = NOISubmissionType.CoPermittee And a.ProgramID = logInVS.reportid).Single.ProgSubmissionTypeID
        'mainSubmission.CoPermitteeEffectiveDate = txtEffectiveDate.Text
        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text

        Dim copermittee As NOISubmissionPersonOrg
        If mainSubmission.EntityState = EntityState.Added Then
            copermittee = New NOISubmissionPersonOrg
            copermittee.EntityState = EntityState.Added
            copermittee.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails
            copermittee.PersonOrgTypeCode = ucnaCoPermitteeInfo.CompanyType
            copermittee.OrgName = ucnaCoPermitteeInfo.CompanyName
            copermittee.FName = ucnaCoPermitteeInfo.FirstName
            copermittee.LName = ucnaCoPermitteeInfo.LastName
            copermittee.Address1 = ucnaCoPermitteeInfo.Address1
            copermittee.Address2 = ucnaCoPermitteeInfo.Address2
            copermittee.City = ucnaCoPermitteeInfo.City
            copermittee.StateAbv = ucnaCoPermitteeInfo.StateAbv
            copermittee.PostalCode = ucnaCoPermitteeInfo.Zip
            copermittee.Phone = ucnaCoPermitteeInfo.Phone
            copermittee.PhoneExt = ucnaCoPermitteeInfo.Ext
            copermittee.Mobile = ucnaCoPermitteeInfo.Mobile
            copermittee.EmailAddress = ucnaCoPermitteeInfo.Email
            copermittee.CoPermitteeEffectiveDate = txtEffectiveDate.Text
            mainSubmission.NOISubmissionPersonOrg.Add(copermittee)
            mainSubmission.CreatedBy = user
            Dim openstatus As New NOISubmissionStatus
            openstatus.EntityState = EntityState.Added
            openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString

            mainSubmission.NOISubmissionStatus.Add(openstatus)
        Else
            With mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)
                .PersonOrgTypeCode = ucnaCoPermitteeInfo.CompanyType
                .OrgName = ucnaCoPermitteeInfo.CompanyName
                .FName = ucnaCoPermitteeInfo.FirstName
                .LName = ucnaCoPermitteeInfo.LastName
                .Address1 = ucnaCoPermitteeInfo.Address1
                .Address2 = ucnaCoPermitteeInfo.Address2
                .City = ucnaCoPermitteeInfo.City
                .StateAbv = ucnaCoPermitteeInfo.StateAbv
                .PostalCode = ucnaCoPermitteeInfo.Zip
                .Phone = ucnaCoPermitteeInfo.Phone
                .PhoneExt = ucnaCoPermitteeInfo.Ext
                .Mobile = ucnaCoPermitteeInfo.Mobile
                .EmailAddress = ucnaCoPermitteeInfo.Email
                .CoPermitteeEffectiveDate = txtEffectiveDate.Text
            End With

        End If





        'If IsSubmitted = True Then
        '    mainSubmission.IsLocked = "Y"
        '    mainSubmission.CopermitteeReceivedDate = Now.Date()
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
        bal.RemoveSessionStorageByUser(logInVS.user.userid)  ' HttpContext.Current.User.Identity.Name)
        logInVS.submissionid = 0
        logInVS.submissiontype = 0
        If isExternal = True Then
            responseRedirect("~/Forms/main.aspx")
        Else
            responseRedirect("~/Admin/Submissions.aspx")
        End If
    End Sub


    Private Sub Page_Error(sender As Object, e As EventArgs) Handles Me.Error
        Me.registerErrorAndSendEmail(Server.GetLastError)
        Response.Redirect("~/Error/ErrorPage.aspx")
    End Sub
End Class