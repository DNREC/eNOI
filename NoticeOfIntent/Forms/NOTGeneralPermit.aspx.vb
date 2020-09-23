Imports Newtonsoft.Json

Public Class NOTGeneralPermit
    Inherits FormBasePage



    Private WriteOnly Property Enabled As Boolean
        Set(value As Boolean)
            txtCompletionDate.Enabled = value
            rblAchievedYesNo.Enabled = value
            rblStatisfiedYesNo.Enabled = value
            rblVerifiedYesNO.Enabled = value
            txtComments.Text = value
            'btnSave.Enabled = value
            'btnSubmit.Enabled = value
        End Set
    End Property

    Public Overrides Sub UIPrep()
        Try

            hfNOISubmissionID.Value = logInVS.submissionid ' Request.QueryString("refno")
            ucnaOriPermitteeInfo.StateAbv = "DE"
            Dim submission As NOISubmission = Nothing
            If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
                Select Case logInVS.reportid
                    Case NOIProgramType.CSSGeneralPermit
                        SetupUIforCSSGeneralPermit()
                        Dim bal As New SWBAL()
                        submission = bal.GetSubmissionByIDForNOT(hfNOISubmissionID.Value)
                    Case NOIProgramType.ISGeneralPermit
                        SetupUIforISGeneralPermit()
                        Dim bal As New ISWBAL()
                        submission = bal.GetSubmissionByIDForNOT(hfNOISubmissionID.Value)
                    Case NOIProgramType.PesticideGeneralPermit
                        SetupUIforAPGeneralPermit()
                        Dim bal As New APBAL()
                        submission = bal.GetSubmissionByIDForNOT(hfNOISubmissionID.Value)
                End Select
                IndNOISubmission = submission
                Enabled = Not isViewOnly
            ElseIf Request.QueryString("proid") <> String.Empty Then
                Select Case logInVS.reportid
                    Case NOIProgramType.CSSGeneralPermit
                        SetupUIforCSSGeneralPermit()
                        Dim bal As New SWBAL()
                        submission = bal.GetProjectOwnerDetailsByProjectIDForNOT(Request.QueryString("proid"))
                    Case NOIProgramType.ISGeneralPermit
                        SetupUIforISGeneralPermit()
                        Dim bal As New ISWBAL()
                        submission = bal.GetProjectOwnerDetailsByProjectIDForNOT(Request.QueryString("proid"))
                    Case NOIProgramType.PesticideGeneralPermit
                        SetupUIforAPGeneralPermit()
                        Dim bal As New APBAL()
                        submission = bal.GetProjectOwnerDetailsByProjectIDForNOT(Request.QueryString("proid"))
                End Select
                IndNOISubmission = submission

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub SetupUIforCSSGeneralPermit()
        divTerminationForISAndAP.Visible = False
        divTerminationForCSS.Visible = True


    End Sub
    Private Sub SetupUIforISGeneralPermit()
        divTerminationForISAndAP.Visible = True
        divTerminationForCSS.Visible = False
        lblNOIHeading.Text = "Notice of Termination (NOT) for Storm Water Discharges Associated With INDUSTRIAL ACTIVITY Under a NPDES General Permit"
        lblFacHeading.Text = "Facility Information"
        lblContactHeading.Text = "Contact Information"
        ucnaProjectDetails.countymunicipalityvisible = False
    End Sub

    Private Sub SetupUIforAPGeneralPermit()
        divTerminationForISAndAP.Visible = True
        divTerminationForCSS.Visible = False
        divLocation.Visible = False
        divLocation1.Visible = False
        lblNOIHeading.Text = "Notice of Intent (NOI) for Aquatic Pesticides"
        lblFacHeading.Text = "Operator Information"
        lblContactHeading.Text = "Operator Contact Information"
        chkNoDischargeAssociated.Text = "You no longer apply aquatic pesticides subject to regulation under the NPDES program"
        ucnaProjectDetails.countymunicipalityvisible = False
    End Sub

    Public Function GetTaxParcelLst() As List(Of NOISubmissionTaxParcels)
        Return IndNOISubmission.NOISubmissionTaxParcels.ToList() 'TaxparcelLst'
    End Function

    Public Overrides Sub MapEntitiesToFields()

        If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
            MapEntitiesToFieldsForCSSGeneralPermit(IndNOISubmission)
        ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
            MapEntitiesToFieldsForISGeneralPermit(IndNOISubmission)
        ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
            MapEntitiesToFieldsForAPGeneralPermit(IndNOISubmission)
        End If
    End Sub

    Private Sub MapEntitiesToFieldsForCSSGeneralPermit(submission As NOISubmission)

        lblNOIIDDisplay.Text = submission.NOIProject.PermitNumber
        lblNOIDateReceivedDisplay.Text = submission.NOIReceivedDate
        txtComments.Text = submission.Comments

        ucnaProjectDetails.CompanyName = submission.NOIProject.ProjectName

        Dim projadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)

        ucnaProjectDetails.Address1 = projadd.Address1
        ucnaProjectDetails.City = projadd.City
        ucnaProjectDetails.StateAbv = projadd.StateAbv
        ucnaProjectDetails.Zip = projadd.PostalCode
        ucnaProjectDetails.County = submission.NOISubmissionSWConstruct.ProjectCounty
        ucnaProjectDetails.Municipality = submission.NOISubmissionSWConstruct.ProjectMunicipality
        lblLatitude.Text = submission.NOILoc.Latitude
        lblLongitude.Text = submission.NOILoc.Longitude



        Dim Originalpermittee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)

        ucnaOriPermitteeInfo.CompanyType = Originalpermittee.PersonOrgTypeCode
        If Originalpermittee.PersonOrgTypeCode = "P" Then
            ucnaOriPermitteeInfo.companynamevisible = False
            ucnaOriPermitteeInfo.personnamevisible = True
            ucnaOriPermitteeInfo.FirstName = Originalpermittee.FName
            ucnaOriPermitteeInfo.LastName = Originalpermittee.LName
        Else
            ucnaOriPermitteeInfo.companynamevisible = True
            ucnaOriPermitteeInfo.personnamevisible = False
            ucnaOriPermitteeInfo.CompanyName = Originalpermittee.OrgName
        End If
        ucnaOriPermitteeInfo.Address1 = Originalpermittee.Address1
        If Originalpermittee.Address2 <> "" Then
            ucnaOriPermitteeInfo.address2visible = True
            ucnaOriPermitteeInfo.Address2 = Originalpermittee.Address2
        Else
            ucnaOriPermitteeInfo.address2visible = False
        End If
        ucnaOriPermitteeInfo.City = Originalpermittee.City
        ucnaOriPermitteeInfo.StateAbv = Originalpermittee.StateAbv
        ucnaOriPermitteeInfo.Zip = Originalpermittee.PostalCode
        ucnaOriPermitteeInfo.Phone = Originalpermittee.Phone
        ucnaOriPermitteeInfo.Ext = Originalpermittee.PhoneExt
        ucnaOriPermitteeInfo.Mobile = Originalpermittee.Mobile
        ucnaOriPermitteeInfo.Email = Originalpermittee.EmailAddress

        If submission.SubmissionID <> 0 Then
            txtCompletionDate.Text = Convert.ToDateTime(submission.NOISubmissionSW.CCDateTermination).ToString("MM-dd-yyyy")
            rblStatisfiedYesNo.SelectedValue = submission.NOISubmissionSW.IsSatisfiedDSSR
            rblVerifiedYesNO.SelectedValue = submission.NOISubmissionSW.IsPlanVerifiedDSSR
            rblAchievedYesNo.SelectedValue = submission.NOISubmissionSW.IsFinalStablizationDone
        End If
    End Sub

    Private Sub MapEntitiesToFieldsForISGeneralPermit(submission As NOISubmission)

        lblNOIIDDisplay.Text = submission.NOIProject.PermitNumber
        lblNOIDateReceivedDisplay.Text = submission.NOIReceivedDate
        txtComments.Text = submission.Comments

        ucnaProjectDetails.CompanyName = submission.NOIProject.ProjectName

        Dim projadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)

        ucnaProjectDetails.Address1 = projadd.Address1
        ucnaProjectDetails.City = projadd.City
        ucnaProjectDetails.StateAbv = projadd.StateAbv
        ucnaProjectDetails.Zip = projadd.PostalCode
        lblLatitude.Text = submission.NOILoc.Latitude
        lblLongitude.Text = submission.NOILoc.Longitude



        Dim Originalpermittee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)

        ucnaOriPermitteeInfo.CompanyType = Originalpermittee.PersonOrgTypeCode
        If Originalpermittee.PersonOrgTypeCode = "P" Then
            ucnaOriPermitteeInfo.companynamevisible = False
            ucnaOriPermitteeInfo.personnamevisible = True
            ucnaOriPermitteeInfo.FirstName = Originalpermittee.FName
            ucnaOriPermitteeInfo.LastName = Originalpermittee.LName
        Else
            ucnaOriPermitteeInfo.companynamevisible = True
            ucnaOriPermitteeInfo.personnamevisible = False
            ucnaOriPermitteeInfo.CompanyName = Originalpermittee.OrgName
        End If
        ucnaOriPermitteeInfo.Address1 = Originalpermittee.Address1
        If Originalpermittee.Address2 <> "" Then
            ucnaOriPermitteeInfo.address2visible = True
            ucnaOriPermitteeInfo.Address2 = Originalpermittee.Address2
        Else
            ucnaOriPermitteeInfo.address2visible = False
        End If
        ucnaOriPermitteeInfo.City = Originalpermittee.City
        ucnaOriPermitteeInfo.StateAbv = Originalpermittee.StateAbv
        ucnaOriPermitteeInfo.Zip = Originalpermittee.PostalCode
        ucnaOriPermitteeInfo.Phone = Originalpermittee.Phone
        ucnaOriPermitteeInfo.Ext = Originalpermittee.PhoneExt
        ucnaOriPermitteeInfo.Mobile = Originalpermittee.Mobile
        ucnaOriPermitteeInfo.Email = Originalpermittee.EmailAddress

        If submission.SubmissionID <> 0 Then
            chkOpTransferred.Checked = IIf(submission.NOISubmissionISWAP.IsOPTransferred = "Y", True, False)
            chkNoDischargeAssociated.Checked = IIf(submission.NOISubmissionISWAP.IsNoDischargeAssociated = "Y", True, False)
            chkCoveredNPDESPermit.Checked = IIf(submission.NOISubmissionISWAP.IsCoveredNPDESPermit = "Y", True, False)
        End If
    End Sub

    Private Sub MapEntitiesToFieldsForAPGeneralPermit(submission As NOISubmission)

        lblNOIIDDisplay.Text = submission.NOIProject.PermitNumber
        lblNOIDateReceivedDisplay.Text = submission.NOIReceivedDate
        txtComments.Text = submission.Comments

        ucnaProjectDetails.CompanyName = submission.NOIProject.ProjectName

        Dim projadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)

        ucnaProjectDetails.Address1 = projadd.Address1
        ucnaProjectDetails.City = projadd.City
        ucnaProjectDetails.StateAbv = projadd.StateAbv
        ucnaProjectDetails.Zip = projadd.PostalCode



        Dim Originalpermittee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)

        ucnaOriPermitteeInfo.CompanyType = Originalpermittee.PersonOrgTypeCode
        If Originalpermittee.PersonOrgTypeCode = "P" Then
            ucnaOriPermitteeInfo.companynamevisible = False
            ucnaOriPermitteeInfo.personnamevisible = True
            ucnaOriPermitteeInfo.FirstName = Originalpermittee.FName
            ucnaOriPermitteeInfo.LastName = Originalpermittee.LName
        Else
            ucnaOriPermitteeInfo.companynamevisible = True
            ucnaOriPermitteeInfo.personnamevisible = False
            ucnaOriPermitteeInfo.CompanyName = Originalpermittee.OrgName
        End If
        ucnaOriPermitteeInfo.Address1 = Originalpermittee.Address1
        If Originalpermittee.Address2 <> "" Then
            ucnaOriPermitteeInfo.address2visible = True
            ucnaOriPermitteeInfo.Address2 = Originalpermittee.Address2
        Else
            ucnaOriPermitteeInfo.address2visible = False
        End If
        ucnaOriPermitteeInfo.City = Originalpermittee.City
        ucnaOriPermitteeInfo.StateAbv = Originalpermittee.StateAbv
        ucnaOriPermitteeInfo.Zip = Originalpermittee.PostalCode
        ucnaOriPermitteeInfo.Phone = Originalpermittee.Phone
        ucnaOriPermitteeInfo.Ext = Originalpermittee.PhoneExt
        ucnaOriPermitteeInfo.Mobile = Originalpermittee.Mobile
        ucnaOriPermitteeInfo.Email = Originalpermittee.EmailAddress

        If submission.SubmissionID <> 0 Then
            chkOpTransferred.Checked = IIf(submission.NOISubmissionISWAP.IsOPTransferred = "Y", True, False)
            chkNoDischargeAssociated.Checked = IIf(submission.NOISubmissionISWAP.IsNoDischargeAssociated = "Y", True, False)
            chkCoveredNPDESPermit.Checked = IIf(submission.NOISubmissionISWAP.IsCoveredNPDESPermit = "Y", True, False)
        End If
    End Sub


    Private Function MapFieldsToEntity() As NOISubmission
        Dim mainSubmission As NOISubmission = IndNOISubmission
        Dim user As String = logInVS.user.userid

        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text
        If mainSubmission.EntityState = EntityState.Added Then
            mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype ' NOISubmissionType.TerminateNOI
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

        mainSubmission.NOILoc = Nothing
        mainSubmission.NOISubmissionSWConstruct = Nothing
        mainSubmission.NOISubmissionTaxParcels.Clear()
        IndNOISubmission = mainSubmission

        Return mainSubmission

    End Function

    Private Function MapFieldsToEntityForIS() As NOISubmission
        Dim mainSubmission As NOISubmission = IndNOISubmission
        Dim user As String = logInVS.user.userid

        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text
        If mainSubmission.EntityState = EntityState.Added Then
            mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype ' NOISubmissionType.TerminateNOI
            mainSubmission.CreatedBy = user
            Dim openstatus As New NOISubmissionStatus
            openstatus.EntityState = EntityState.Added
            openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString
            mainSubmission.NOISubmissionStatus.Add(openstatus)

            Dim noiISWAP As New NOISubmissionISWAP
            noiISWAP.EntityState = EntityState.Added
            noiISWAP.IsOPTransferred = IIf(chkOpTransferred.Checked = True, "Y", "N")
            noiISWAP.IsNoDischargeAssociated = IIf(chkNoDischargeAssociated.Checked = True, "Y", "N")
            noiISWAP.IsCoveredNPDESPermit = IIf(chkCoveredNPDESPermit.Checked = True, "Y", "N")

            mainSubmission.NOISubmissionISWAP = noiISWAP
        Else
            mainSubmission.NOISubmissionISWAP.IsOPTransferred = IIf(chkOpTransferred.Checked = True, "Y", "N")
            mainSubmission.NOISubmissionISWAP.IsNoDischargeAssociated = IIf(chkNoDischargeAssociated.Checked = True, "Y", "N")
            mainSubmission.NOISubmissionISWAP.IsCoveredNPDESPermit = IIf(chkCoveredNPDESPermit.Checked = True, "Y", "N")
        End If

        mainSubmission.NOILoc = Nothing
        mainSubmission.NOISubmissionTaxParcels.Clear()
        IndNOISubmission = mainSubmission

        Return mainSubmission

    End Function

    Private Function MapFieldsToEntityForAP() As NOISubmission
        Dim mainSubmission As NOISubmission = IndNOISubmission
        Dim user As String = logInVS.user.userid

        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text
        If mainSubmission.EntityState = EntityState.Added Then
            mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype ' NOISubmissionType.TerminateNOI
            mainSubmission.CreatedBy = user
            Dim openstatus As New NOISubmissionStatus
            openstatus.EntityState = EntityState.Added
            openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString
            mainSubmission.NOISubmissionStatus.Add(openstatus)

            Dim noiISWAP As New NOISubmissionISWAP
            noiISWAP.EntityState = EntityState.Added
            noiISWAP.IsOPTransferred = IIf(chkOpTransferred.Checked = True, "Y", "N")
            noiISWAP.IsNoDischargeAssociated = IIf(chkNoDischargeAssociated.Checked = True, "Y", "N")
            noiISWAP.IsCoveredNPDESPermit = IIf(chkCoveredNPDESPermit.Checked = True, "Y", "N")

            mainSubmission.NOISubmissionISWAP = noiISWAP
        Else
            mainSubmission.NOISubmissionISWAP.IsOPTransferred = IIf(chkOpTransferred.Checked = True, "Y", "N")
            mainSubmission.NOISubmissionISWAP.IsNoDischargeAssociated = IIf(chkNoDischargeAssociated.Checked = True, "Y", "N")
            mainSubmission.NOISubmissionISWAP.IsCoveredNPDESPermit = IIf(chkCoveredNPDESPermit.Checked = True, "Y", "N")
        End If

        IndNOISubmission = mainSubmission

        Return mainSubmission

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim ns As NOISubmission

        Try
            ns = IndNOISubmission

            If Not isViewOnly Then
                Select Case logInVS.reportid
                    Case NOIProgramType.CSSGeneralPermit
                        Dim bal As New SWBAL()
                        ns = bal.Insert(MapFieldsToEntity())
                    Case NOIProgramType.ISGeneralPermit
                        Dim bal As New ISWBAL()
                        ns = bal.Insert(MapFieldsToEntityForIS())
                    Case NOIProgramType.PesticideGeneralPermit
                        Dim bal As New APBAL()
                        ns = bal.Insert(MapFieldsToEntityForAP())
                End Select
            End If
            hfNOISubmissionID.Value = ns.SubmissionID
            logInVS.submissionid = ns.SubmissionID
            IndNOISubmission = ns


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



    Private Sub cvSatisfiedYesNo_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvSatisfiedYesNo.ServerValidate
        If args.Value = "No" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Private Sub cvVerifiedYesNO_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvVerifiedYesNO.ServerValidate
        If args.Value = "No" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Private Sub cvAchievedYesNo_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvAchievedYesNo.ServerValidate
        If args.Value = "No" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Private Sub NOTGeneralPermit_Error(sender As Object, e As EventArgs) Handles Me.Error
        'todo uncomment the below code after development.
        'Me.registerErrorAndSendEmail(Server.GetLastError)
        'Response.Redirect("~/Error/ErrorPage.aspx")
    End Sub

    Private Sub cvTerReasonForISAndAP_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvTerReasonForISAndAP.ServerValidate
        If chkOpTransferred.Checked = True Or chkNoDischargeAssociated.Checked = True Or chkCoveredNPDESPermit.Checked = True Then
            args.IsValid = True
        Else
            args.IsValid = False
        End If
    End Sub
End Class