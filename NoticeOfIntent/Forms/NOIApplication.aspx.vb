Imports Newtonsoft.Json
Imports System.IO

Public Class NOIApplication
    Inherits FormBasePage


    Private Sub Page_Load1(sender As Object, e As EventArgs) Handles Me.Load

        AddHandler ucnaSiteInfo.CheckProjectName_Validated, AddressOf IsProjectNameExists

    End Sub

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ' btnMapPoint.Attributes.Add("OnClick", "javascript:showMapInDialog();")
    End Sub
    Private Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Dim master As MasterPage = Me.Master
        While master IsNot Nothing
            master = master.Master
        End While
    End Sub


    Private Sub Page_Error(sender As Object, e As EventArgs) Handles Me.Error
        'todo uncomment the below code after development.
        Me.registerErrorAndSendEmail(Server.GetLastError)
        Response.Redirect("~/Error/ErrorPage.aspx")
    End Sub
    Public Overrides Sub Validate(validationGroup As String)

        If validationGroup = "ValidateNOI" Then
            If ucnaOwnerInfo.CompanyType = "P" Then
                ucnaOwnerInfo.ValidateCompanyName = False
            End If


            If ddlProjectType.SelectedValue = "10" Then   'if the projecttype is 'Other'
                rfvProjectTypeOther.Enabled = True
            Else
                rfvProjectTypeOther.Enabled = False
            End If

            MyBase.Validate("ValidateNOI")
        End If

        If validationGroup = "ValidateBMP" Then
            If lvSWBMP.InsertItem IsNot Nothing Then
                Dim ddlbmp As DropDownList = CType(lvSWBMP.InsertItem.FindControl("ddlSWBMP"), DropDownList)
                Dim rfvBMPOther As RequiredFieldValidator = CType(lvSWBMP.InsertItem.FindControl("rfvBMPOther"), RequiredFieldValidator)
                If ddlbmp.SelectedValue = "8" Then    'if the SWBMP is otherBMP
                    rfvBMPOther.Enabled = True
                Else
                    rfvBMPOther.Enabled = False
                End If
            End If
            MyBase.Validate("ValidateBMP")
        End If

        If validationGroup = "ValidateSIC" Then
            MyBase.Validate("ValidateSIC")
        End If

        If validationGroup = "ValidateNAICS" Then
            MyBase.Validate("ValidateNAICS")
        End If


        If validationGroup = "OutfallValidationGrp" Then
            MyBase.Validate("OutfallValidationGrp")
        End If

        If validationGroup = "ValidateChemical" Then
            MyBase.Validate("ValidateChemical")
        End If

        If validationGroup = "ValidateEditChemical" Then
            MyBase.Validate("ValidateEditChemical")
        End If


    End Sub


    Public Overrides Sub UIPrep()
        Try
            hfPiTypeID.Value = CacheLookupData.GetPiTypeIDByReportID(logInVS.reportid)
            hfNOISubmissionID.Value = logInVS.submissionid
            ucnaOwnerInfo.populateddlStateAbv()
            ucnaOwnerInfo.StateAbv = "DE"
            ucnaContactInfo.populateddlStateAbv()
            ucnaContactInfo.StateAbv = "DE"
            ucnaBilleeInfo.populateddlStateAbv()
            ucnaBilleeInfo.StateAbv = "DE"
            ucnaSiteInfo.populateddlStateAbv()
            ucnaSiteInfo.StateAbv = "DE"

            If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
                PageLoadForExistingSubmission(hfNOISubmissionID.Value, logInVS.reportid)
            ElseIf Request.QueryString("proid") IsNot Nothing Then
                PageLoadByPermitNumber(Request.QueryString("proid"), logInVS.reportid)
            Else
                PageLoadForNewSubmission(logInVS.reportid)
            End If
        Catch ex As Exception
            Dim bal1 As New NOIBAL
            bal1.LogError(Me, ex)
            Throw ex
        End Try

    End Sub

    Private Sub PageLoadForExistingSubmission(submissionID As Integer, reportid As Integer)
        Dim submission As NOISubmission = Nothing
        Select Case reportid
            Case NOIProgramType.CSSGeneralPermit
                Dim bal As New SWBAL()
                submission = bal.GetSubmissionByIDForGeneralNOI(submissionID)
                SetupUIforCSSGeneralPermit(submission)
            Case NOIProgramType.ISGeneralPermit
                Dim bal As New ISWBAL()
                submission = bal.GetSubmissionByIDForGeneralNOI(submissionID)
                SetupUIforISGeneralPermit(submission)
            Case NOIProgramType.PesticideGeneralPermit
                Dim bal As New APBAL
                submission = bal.GetSubmissionByIDForGeneralNOI(submissionID)
                SetupUIforPesticideGeneralPermit(submission)
        End Select
        IndNOISubmission = submission
    End Sub

    Private Sub PageLoadByPermitNumber(permitnumber As String, reportid As Integer)
        Dim submission As NOISubmission = Nothing
        Select Case reportid
            Case NOIProgramType.PesticideGeneralPermit
                Dim bal As New APBAL
                submission = bal.GetProjectDetailsForNOICorrectionAndRenewalByProjectID(permitnumber)
                submission.ProgSubmissionTypeID = logInVS.progsubmisssiontype
                SetupUIforPesticideGeneralPermit(submission)
        End Select
        IndNOISubmission = submission
    End Sub

    Private Sub PageLoadForNewSubmission(reportid As Integer)
        Dim submission As NOISubmission = Nothing
        Dim bal As New NOIBAL()
        bal.RemoveSessionStorageByUser(logInVS.user.userid)
        submission = IndNOISubmission
        'IndNOISubmission = submission
        Select Case logInVS.reportid
            Case NOIProgramType.CSSGeneralPermit
                SetupUIforCSSGeneralPermit(submission)
            Case NOIProgramType.ISGeneralPermit
                SetupUIforISGeneralPermit(submission)
            Case NOIProgramType.PesticideGeneralPermit
                SetupUIforPesticideGeneralPermit(submission)
        End Select
    End Sub

    Private Sub SetupUIforCSSGeneralPermit(submission As NOISubmission)
        IndNOISubmission = submission
        Enabled = Not isViewOnly
        divSicCodes.Visible = False
        divNAICSCodes.Visible = False
        divNoExposure.Visible = False
        OutfallsVisible = False
        divChemicals.Visible = False
        divPesticidesEntity.Visible = False
        divPesticidesThreshold.Visible = False
    End Sub
    Private Sub SetupUIforISGeneralPermit(submission As NOISubmission)
        IndNOISubmission = submission
        Enabled = Not isViewOnly
        divChemicals.Visible = False
        ucnaSiteInfo.textCompanyName.Attributes.Add("placeholder", "Facility Name")
        ucnaSiteInfo.countymunicipalityvisible = False
        divProjectType.Visible = False
        divbmp.Visible = False
        divOtherInfo.Visible = False
        divPesticidesEntity.Visible = False
        divPesticidesThreshold.Visible = False
        If logInVS.progsubmisssiontype = 5 Then
            chkDisableNoExposure.Checked = True
            lvNoExposure.Enabled = False
            cvNoExposure.Enabled = False
            'ElseIf logInVS.progsubmisssiontype = 6 Then
            '    lvOutfall.Enabled = False
            '    If lvOutfall.InsertItem IsNot Nothing Then
            '        Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
            '        btnMapOutfallPoint.Disabled = True
            '    End If
        End If
        lblNOIHeading.Text = "Notice of Intent (NOI) for Storm Water Discharges Associated With INDUSTRIAL ACTIVITY Under a NPDES General Permit"
        wsProjectInfo.Title = "Facility Information"
        wsProjectInfo1.Title = "Facility Information (continued)"
        lblSiteInfoPnlHeading.Text = "Facility Information"
        ucnaSiteInfo.CompanyNameLabel = "Facility Name *"
        ucnaSiteInfo.Address1Label = "Facility Location/Address *"
    End Sub
    Private Sub SetupUIforPesticideGeneralPermit(submission As NOISubmission)
        IndNOISubmission = submission
        Enabled = Not isViewOnly
        divBilleeDetails.Visible = False
        ucnaSiteInfo.textCompanyName.Attributes.Add("placeholder", "Company Name/Operator Name")
        ucnaSiteInfo.countymunicipalityvisible = False
        ucnaSiteInfo.allowDEstateonly = False
        ucnaSiteInfo.populateddlStateAbv()
        ddlCopyContactInfoFrom.Items.Remove(ddlCopyContactInfoFrom.Items(1))

        divProjectType.Visible = False
        divTaxParcel.Visible = False
        divLocDet.Visible = False
        divbmp.Visible = False
        divOtherInfo.Visible = False
        divSicCodes.Visible = False
        divNAICSCodes.Visible = False
        divNoExposure.Visible = False
        OutfallsVisible = False

        wsProjectInfo.Title = "Operator Information"
        wsProjectInfo1.Title = "Operator Information (continued)"
        lblContactInfoHeading.Text = "Operator Contact Information"
        lblSiteInfoPnlHeading.Text = "Operator Information"
        ucnaSiteInfo.CompanyNameLabel = "Company Name/Operator Name *"
        ucnaSiteInfo.Address1Label = "Operator Location/Address *"

        lblNOIHeading.Text = "Notice of Intent (NOI) for Aquatic Pesticides"
        rblEntityType.DataBind()

        Select Case logInVS.submissiontype
            Case NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                ucnaOwnerInfo.EnableName = False
                ucnaOwnerInfo.EnableCompany = False
                ucnaSiteInfo.Enabled = False
        End Select
    End Sub
    Public Overrides Sub MapEntitiesToFields()
        If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then

            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                MapEntitiesToFieldsForCSSGeneralPermit(IndNOISubmission)
            ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                MapEntitiesToFieldsForISGeneralPermit(IndNOISubmission)
            ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                MapEntitiesToFieldsForAPGeneralPermit(IndNOISubmission)
            End If
        Else
            If logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                If lvOutfall.InsertItem IsNot Nothing Then
                    Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
                    btnMapOutfallPoint.Disabled = True
                End If
            ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit AndAlso (logInVS.submissiontype = NOISubmissionType.GeneralNOICorrection Or logInVS.submissiontype = NOISubmissionType.GeneralNOIRenewal) Then
                MapEntitiesToFieldsForAPGeneralPermit(IndNOISubmission)
            End If
        End If
    End Sub
    Private Sub MapEntitiesToFieldsForCSSGeneralPermit(submission As NOISubmission)

        ucnaSiteInfo.CompanyName = submission.NOIProject.ProjectName

        Dim projectadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)
        ucnaSiteInfo.Address1 = projectadd.Address1
        ucnaSiteInfo.City = projectadd.City
        ucnaSiteInfo.StateAbv = projectadd.StateAbv
        ucnaSiteInfo.Zip = projectadd.PostalCode

        ucnaSiteInfo.County = submission.NOISubmissionSWConstruct.ProjectCounty
        ucnaSiteInfo.Municipality = submission.NOISubmissionSWConstruct.ProjectMunicipality
        ddlProjectType.DataBind()
        ddlProjectType.SelectedValue = submission.NOISubmissionSWConstruct.ProjectTypeID
        If submission.NOISubmissionSWConstruct.ProjectTypeID = 10 Then
            txtProjectTypeOther.Text = submission.NOISubmissionSWConstruct.ProjectTypeOther
            txtProjectTypeOther.Enabled = True
        Else
            txtProjectTypeOther.Enabled = False
        End If
        txtLatitude.Text = common.NothingToString(submission.NOILoc.Latitude)
        txtLongitude.Text = common.NothingToString(submission.NOILoc.Longitude)
        hfX.Value = common.NothingToString(submission.NOILoc.X)
        hfY.Value = common.NothingToString(submission.NOILoc.Y)
        txtWatershed.Text = common.NothingToString(submission.NOILoc.Watershed)
        hfWaterShedCode.Value = common.NothingToString(submission.NOILoc.HUC_12_Code)
        Select Case submission.NOISubmissionSWConstruct.IsSWPPPPrepared
            Case "N"
                rblSWPPPYesNo.SelectedValue = "No"
                    'rbNo.Checked = True
            Case "Y"
                rblSWPPPYesNo.SelectedValue = "Yes"
                'rbYes.Checked = True
        End Select



        ddlPlanApprovalAgency.SelectedValue = submission.NOISubmissionSWConstruct.DelegatedAgencyID

        txtTotalAreaOfSite.Text = submission.NOISubmissionSWConstruct.TotalLandArea
        txtAreaOfDisturbed.Text = submission.NOISubmissionSWConstruct.EstimatedArea
        txtConstructStartDate.Text = Convert.ToDateTime(submission.NOISubmissionSWConstruct.ConstructStartDate).ToString("MM-dd-yyyy")
        txtConstructCompleteDate.Text = Convert.ToDateTime(submission.NOISubmissionSWConstruct.ConstructEndDate).ToString("MM-dd-yyyy")
        txtComments.Text = submission.Comments


        Dim owner As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)


        ucnaOwnerInfo.CompanyType = owner.PersonOrgTypeCode
        ucnaOwnerInfo.CompanyName = owner.OrgName
        ucnaOwnerInfo.FirstName = owner.FName
        ucnaOwnerInfo.LastName = owner.LName
        ucnaOwnerInfo.Address1 = owner.Address1
        ucnaOwnerInfo.Address2 = owner.Address2
        ucnaOwnerInfo.City = owner.City
        ucnaOwnerInfo.StateAbv = owner.StateAbv
        ucnaOwnerInfo.Zip = owner.PostalCode
        ucnaOwnerInfo.Phone = owner.Phone
        ucnaOwnerInfo.Ext = owner.PhoneExt
        ucnaOwnerInfo.Mobile = owner.Mobile
        ucnaOwnerInfo.Email = owner.EmailAddress



        Dim contact As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails)

        ucnaContactInfo.CompanyName = contact.OrgName
        ucnaContactInfo.FirstName = contact.FName
        ucnaContactInfo.LastName = contact.LName
        ucnaContactInfo.Address1 = contact.Address1
        ucnaContactInfo.Address2 = contact.Address2
        ucnaContactInfo.City = contact.City
        ucnaContactInfo.StateAbv = contact.StateAbv
        ucnaContactInfo.Zip = contact.PostalCode
        ucnaContactInfo.Phone = contact.Phone
        ucnaContactInfo.Ext = contact.PhoneExt
        ucnaContactInfo.Mobile = contact.Mobile
        ucnaContactInfo.Email = contact.EmailAddress

        Dim billee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.BilleeDetails)


        ucnaBilleeInfo.CompanyName = billee.OrgName
        ucnaBilleeInfo.FirstName = billee.FName
        ucnaBilleeInfo.LastName = billee.LName
        ucnaBilleeInfo.Address1 = billee.Address1
        ucnaBilleeInfo.Address2 = billee.Address2
        ucnaBilleeInfo.City = billee.City
        ucnaBilleeInfo.StateAbv = billee.StateAbv
        ucnaBilleeInfo.Zip = billee.PostalCode
        ucnaBilleeInfo.Phone = billee.Phone
        ucnaBilleeInfo.Ext = billee.PhoneExt
        ucnaBilleeInfo.Mobile = billee.Mobile
        ucnaBilleeInfo.Email = billee.EmailAddress
    End Sub
    Private Sub MapEntitiesToFieldsForISGeneralPermit(submission As NOISubmission)

        ucnaSiteInfo.CompanyName = submission.NOIProject.ProjectName

        Dim projectadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)
        ucnaSiteInfo.Address1 = projectadd.Address1
        ucnaSiteInfo.City = projectadd.City
        ucnaSiteInfo.StateAbv = projectadd.StateAbv
        ucnaSiteInfo.Zip = projectadd.PostalCode
        hfSiteInfo.Value = projectadd.Address1 + "," + projectadd.City + "," + projectadd.StateAbv + "," + projectadd.PostalCode

        txtLatitude.Text = common.NothingToString(submission.NOILoc.Latitude)
        txtLongitude.Text = common.NothingToString(submission.NOILoc.Longitude)
        hfX.Value = common.NothingToString(submission.NOILoc.X)
        hfY.Value = common.NothingToString(submission.NOILoc.Y)
        txtWatershed.Text = common.NothingToString(submission.NOILoc.Watershed)
        hfWaterShedCode.Value = common.NothingToString(submission.NOILoc.HUC_12_Code)

        txtComments.Text = submission.Comments


        Dim owner As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)


        ucnaOwnerInfo.CompanyType = owner.PersonOrgTypeCode
        ucnaOwnerInfo.CompanyName = owner.OrgName
        ucnaOwnerInfo.FirstName = owner.FName
        ucnaOwnerInfo.LastName = owner.LName
        ucnaOwnerInfo.Address1 = owner.Address1
        ucnaOwnerInfo.Address2 = owner.Address2
        ucnaOwnerInfo.City = owner.City
        ucnaOwnerInfo.StateAbv = owner.StateAbv
        ucnaOwnerInfo.Zip = owner.PostalCode
        ucnaOwnerInfo.Phone = owner.Phone
        ucnaOwnerInfo.Ext = owner.PhoneExt
        ucnaOwnerInfo.Mobile = owner.Mobile
        ucnaOwnerInfo.Email = owner.EmailAddress



        Dim contact As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails)

        ucnaContactInfo.CompanyName = contact.OrgName
        ucnaContactInfo.FirstName = contact.FName
        ucnaContactInfo.LastName = contact.LName
        ucnaContactInfo.Address1 = contact.Address1
        ucnaContactInfo.Address2 = contact.Address2
        ucnaContactInfo.City = contact.City
        ucnaContactInfo.StateAbv = contact.StateAbv
        ucnaContactInfo.Zip = contact.PostalCode
        ucnaContactInfo.Phone = contact.Phone
        ucnaContactInfo.Ext = contact.PhoneExt
        ucnaContactInfo.Mobile = contact.Mobile
        ucnaContactInfo.Email = contact.EmailAddress

        Dim billee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.BilleeDetails)


        ucnaBilleeInfo.CompanyName = billee.OrgName
        ucnaBilleeInfo.FirstName = billee.FName
        ucnaBilleeInfo.LastName = billee.LName
        ucnaBilleeInfo.Address1 = billee.Address1
        ucnaBilleeInfo.Address2 = billee.Address2
        ucnaBilleeInfo.City = billee.City
        ucnaBilleeInfo.StateAbv = billee.StateAbv
        ucnaBilleeInfo.Zip = billee.PostalCode
        ucnaBilleeInfo.Phone = billee.Phone
        ucnaBilleeInfo.Ext = billee.PhoneExt
        ucnaBilleeInfo.Mobile = billee.Mobile
        ucnaBilleeInfo.Email = billee.EmailAddress
    End Sub
    Private Sub MapEntitiesToFieldsForAPGeneralPermit(submission As NOISubmission)

        ucnaSiteInfo.CompanyName = submission.NOIProject.ProjectName


        rblEntityType.SelectedValue = submission.NOISubmissionAP.EntityTypeID

        chkInsectPestControl.Checked = IIf(submission.NOISubmissionAP.InsectPestControl = "Y", True, False)
        chkWeedPestControl.Checked = IIf(submission.NOISubmissionAP.WeedPestControl = "Y", True, False)
        chkAnimalPestControl.Checked = IIf(submission.NOISubmissionAP.AnimalPestControl = "Y", True, False)
        chkForestCanopyPestControl.Checked = IIf(submission.NOISubmissionAP.ForestCanopyPestControl = "Y", True, False)
        rbtnThresholdNotExceeded.Checked = IIf(submission.NOISubmissionAP.NotExceededThreshold = "Y", True, False)
        txtCommercialApplicatorID.Text = submission.NOISubmissionAP.CommercialApplicatorID



        Dim projectadd As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress)
        ucnaSiteInfo.Address1 = projectadd.Address1
        ucnaSiteInfo.City = projectadd.City
        ucnaSiteInfo.StateAbv = projectadd.StateAbv
        ucnaSiteInfo.Zip = projectadd.PostalCode


        hfSiteInfo.Value = projectadd.Address1 + "," + projectadd.City + "," + projectadd.StateAbv + "," + projectadd.PostalCode

        txtComments.Text = submission.Comments


        Dim owner As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)


        ucnaOwnerInfo.CompanyType = owner.PersonOrgTypeCode
        ucnaOwnerInfo.CompanyName = owner.OrgName
        ucnaOwnerInfo.FirstName = owner.FName
        ucnaOwnerInfo.LastName = owner.LName
        ucnaOwnerInfo.Address1 = owner.Address1
        ucnaOwnerInfo.Address2 = owner.Address2
        ucnaOwnerInfo.City = owner.City
        ucnaOwnerInfo.StateAbv = owner.StateAbv
        ucnaOwnerInfo.Zip = owner.PostalCode
        ucnaOwnerInfo.Phone = owner.Phone
        ucnaOwnerInfo.Ext = owner.PhoneExt
        ucnaOwnerInfo.Mobile = owner.Mobile
        ucnaOwnerInfo.Email = owner.EmailAddress



        Dim contact As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails)

        ucnaContactInfo.CompanyName = contact.OrgName
        ucnaContactInfo.FirstName = contact.FName
        ucnaContactInfo.LastName = contact.LName
        ucnaContactInfo.Address1 = contact.Address1
        ucnaContactInfo.Address2 = contact.Address2
        ucnaContactInfo.City = contact.City
        ucnaContactInfo.StateAbv = contact.StateAbv
        ucnaContactInfo.Zip = contact.PostalCode
        ucnaContactInfo.Phone = contact.Phone
        ucnaContactInfo.Ext = contact.PhoneExt
        ucnaContactInfo.Mobile = contact.Mobile
        ucnaContactInfo.Email = contact.EmailAddress

    End Sub
    Private Function MapFieldsToModifiedEntity(reportid As NOIProgramType) As NOISubmission
        Dim mainSubmission As NOISubmission = IndNOISubmission

        Dim user As String = logInVS.user.userid
        If reportid = NOIProgramType.CSSGeneralPermit Then
            mainSubmission = MapFieldsToModifiedEntityForCSSGeneralPermit(user, mainSubmission)
        ElseIf reportid = NOIProgramType.ISGeneralPermit Then
            mainSubmission = MapFieldsToModifiedEntityForISGeneralPermit(user, mainSubmission)
        ElseIf reportid = NOIProgramType.PesticideGeneralPermit Then
            mainSubmission = MapFieldsToModifiedEntityForPesticideGeneralPermit(user, mainSubmission)
        End If
        IndNOISubmission = mainSubmission
        Return mainSubmission
    End Function
    Private Function MapFieldsToModifiedEntityForCSSGeneralPermit(ByVal user As String, ByVal mainSubmission As NOISubmission) As NOISubmission
        mainSubmission.LastChgBy = user

        'If hfNOISubmissionID.Value = "0" Then
        '    mainSubmission.NOIProject.EntityState = EntityState.Added
        'Else
        '    mainSubmission.NOIProject.EntityState = EntityState.Modified
        'End If
        mainSubmission.Comments = txtComments.Text
        mainSubmission.NOIProject.ProjectName = ucnaSiteInfo.CompanyName
        SetProjectAddress(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress))

        'mainSubmission.NOIProject.ProjectAddress = ucnaSiteInfo.Address1
        'mainSubmission.NOIProject.ProjectCity = ucnaSiteInfo.City
        'mainSubmission.NOIProject.ProjectStateAbv = ucnaSiteInfo.StateAbv
        'mainSubmission.NOIProject.ProjectPostalCode = ucnaSiteInfo.Zip

        mainSubmission.NOISubmissionSWConstruct.ProjectCounty = ucnaSiteInfo.County
        mainSubmission.NOISubmissionSWConstruct.ProjectMunicipality = ucnaSiteInfo.Municipality
        mainSubmission.NOISubmissionSWConstruct.ProjectTypeID = ddlProjectType.SelectedValue
        If ddlProjectType.SelectedValue = 10 Then
            mainSubmission.NOISubmissionSWConstruct.ProjectTypeOther = txtProjectTypeOther.Text
        Else
            mainSubmission.NOISubmissionSWConstruct.ProjectTypeOther = Nothing
        End If
        mainSubmission.NOILoc.Latitude = txtLatitude.Text
        mainSubmission.NOILoc.Longitude = txtLongitude.Text
        If hfX.Value <> Nothing OrElse hfX.Value <> String.Empty Then
            mainSubmission.NOILoc.X = hfX.Value
        End If
        If hfY.Value <> Nothing OrElse hfY.Value <> String.Empty Then
            mainSubmission.NOILoc.Y = hfY.Value
        End If
        mainSubmission.NOILoc.Watershed = txtWatershed.Text
        If hfWaterShedCode.Value <> String.Empty Then
            mainSubmission.NOILoc.HUC_12_Code = common.DBNullToLong(hfWaterShedCode.Value)
        End If

        'If rbNo.Checked Then
        '    mainSubmission.NOIProject.IsSWPPPPrepared = "N"
        'ElseIf rbYes.Checked Then
        '    mainSubmission.NOIProject.IsSWPPPPrepared = "Y"
        'End If

        If rblSWPPPYesNo.SelectedValue = "No" Then
            mainSubmission.NOISubmissionSWConstruct.IsSWPPPPrepared = "N"
        ElseIf rblSWPPPYesNo.SelectedValue = "Yes" Then
            mainSubmission.NOISubmissionSWConstruct.IsSWPPPPrepared = "Y"
        End If

        mainSubmission.NOISubmissionSWConstruct.DelegatedAgencyID = ddlPlanApprovalAgency.SelectedValue
        mainSubmission.NOISubmissionSWConstruct.TotalLandArea = txtTotalAreaOfSite.Text
        mainSubmission.NOISubmissionSWConstruct.EstimatedArea = txtAreaOfDisturbed.Text
        mainSubmission.NOISubmissionSWConstruct.ConstructStartDate = txtConstructStartDate.Text
        mainSubmission.NOISubmissionSWConstruct.ConstructEndDate = txtConstructCompleteDate.Text
        mainSubmission.NOIProject.LastChgBy = user

        'If lvTaxparcel.Items.Count > 0 Then
        '    'Dim lstTaxParcel As New List(Of NOIProjectTaxParcels)
        '    'For Each item As ListViewItem In lvTaxparcel.Items
        '    '    If item.ItemType = ListViewItemType.DataItem Then
        '    '        Dim lblTaxParcelNumber As Label = CType(item.FindControl("lblTaxParcelNumber"), Label)
        '    '        Dim lblcounty As Label = CType(item.FindControl("lblcounty"), Label)
        '    '        Dim taxparcel As New NOIProjectTaxParcels
        '    '        taxparcel.EntityState = EntityState.Added
        '    '        taxparcel.TaxParcelNumber = lblTaxParcelNumber.Text
        '    '        taxparcel.TaxParcelCounty = lblcounty.Text.Substring(0, 1)
        '    '        lstTaxParcel.Add(taxparcel)
        '    '    End If
        '    'Next
        '    'mainSubmission.NOIProject.NOIProjectTaxParcels = lstTaxParcel
        'End If

        'mainSubmission.NOIProject = noiproj



        SetOwnerInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails))
        SetContactInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails))
        SetBilleeInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.BilleeDetails))


        Return mainSubmission

    End Function
    Private Function MapFieldsToModifiedEntityForISGeneralPermit(ByVal user As String, ByVal mainSubmission As NOISubmission) As NOISubmission
        mainSubmission.LastChgBy = user

        mainSubmission.Comments = txtComments.Text
        mainSubmission.NOIProject.ProjectName = ucnaSiteInfo.CompanyName
        SetProjectAddress(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress))

        mainSubmission.NOILoc.Latitude = txtLatitude.Text
        mainSubmission.NOILoc.Longitude = txtLongitude.Text
        If hfX.Value <> Nothing OrElse hfX.Value <> String.Empty Then
            mainSubmission.NOILoc.X = hfX.Value
        End If
        If hfY.Value <> Nothing OrElse hfY.Value <> String.Empty Then
            mainSubmission.NOILoc.Y = hfY.Value
        End If
        mainSubmission.NOILoc.Watershed = txtWatershed.Text
        If hfWaterShedCode.Value <> String.Empty Then
            mainSubmission.NOILoc.HUC_12_Code = common.DBNullToLong(hfWaterShedCode.Value)
        End If



        mainSubmission.NOIProject.LastChgBy = user



        If chkDisableNoExposure.Checked = False Then
            For Each item As ListViewItem In lvNoExposure.Items
                Dim lblNOExposureCLID As Label = CType(item.FindControl("lblNOExposureCLID"), Label)
                Dim hfSubmissionNEID As HiddenField = CType(item.FindControl("hfSubmissionNEID"), HiddenField)
                Dim ddlResult As DropDownList = CType(item.FindControl("ddlResult"), DropDownList)

                If hfSubmissionNEID.Value = 0 Then
                    Dim NE As New NOISubmissionNE
                    NE.EntityState = EntityState.Added
                    NE.NOExposureCLID = lblNOExposureCLID.Text
                    NE.Answer = ddlResult.SelectedValue
                    mainSubmission.NOISubmissionNE.Add(NE)
                Else
                    SetNOExposureResult(ddlResult.SelectedValue, mainSubmission.NOISubmissionNE.Single(Function(e) e.SubmissionNEID = hfSubmissionNEID.Value))
                End If
            Next
        End If

        SetOwnerInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails))
        SetContactInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails))
        SetBilleeInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.BilleeDetails))


        Return mainSubmission
    End Function
    Private Function MapFieldsToModifiedEntityForPesticideGeneralPermit(ByVal user As String, ByVal mainSubmission As NOISubmission) As NOISubmission
        mainSubmission.LastChgBy = user

        mainSubmission.Comments = txtComments.Text
        mainSubmission.NOIProject.ProjectName = ucnaSiteInfo.CompanyName
        SetProjectAddress(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress))

        mainSubmission.NOISubmissionAP.EntityTypeID = rblEntityType.SelectedValue

        If txtCommercialApplicatorID.Text <> String.Empty Then
            mainSubmission.NOISubmissionAP.CommercialApplicatorID = txtCommercialApplicatorID.Text
        End If

        If chkInsectPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.InsectPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.InsectPestControl = "N"
        End If

        If chkWeedPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.WeedPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.WeedPestControl = "N"
        End If
        If chkAnimalPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.AnimalPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.AnimalPestControl = "N"
        End If
        If chkForestCanopyPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.ForestCanopyPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.ForestCanopyPestControl = "N"
        End If
        If rbtnThresholdNotExceeded.Checked = True Then
            mainSubmission.NOISubmissionAP.NotExceededThreshold = "Y"
        Else
            mainSubmission.NOISubmissionAP.NotExceededThreshold = "N"
        End If

        SetOwnerInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails))
        SetContactInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails))
        Return mainSubmission
    End Function
    Private Function MapFieldsToAddEntity(reportid As NOIProgramType) As NOISubmission
        Dim user As String = logInVS.user.userid
        Dim mainSubmission As NOISubmission = IndNOISubmission
        'mainSubmission = IndNOISubmission
        If reportid = NOIProgramType.CSSGeneralPermit Then
            mainSubmission = MapFieldsToAddEntityForCSSGeneralPermit(user, mainSubmission)
        ElseIf reportid = NOIProgramType.ISGeneralPermit Then
            mainSubmission = MapFieldsToAddEntityForISGeneralPermit(user, mainSubmission)
        ElseIf reportid = NOIProgramType.PesticideGeneralPermit Then
            If logInVS.submissiontype = NOISubmissionType.GeneralNOIPermit Then
                mainSubmission = MapFieldsToAddEntityForPesticideGeneralPermit(user, mainSubmission)
            ElseIf logInVS.submissiontype = NOISubmissionType.GeneralNOICorrection Then
                mainSubmission = MapFieldsToAddEntityForPGPermitCorrection(user, mainSubmission)
            End If
        End If
            IndNOISubmission = mainSubmission
        Return mainSubmission
    End Function
    Private Function MapFieldsToAddEntityForCSSGeneralPermit(ByVal user As String, ByVal mainSubmission As NOISubmission) As NOISubmission

        mainSubmission.EntityState = EntityState.Added
        mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype
        mainSubmission.CreatedBy = user
        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text
        Dim noiproj As New NOIProject
        noiproj.EntityState = EntityState.Added
        noiproj.ProjectName = ucnaSiteInfo.CompanyName
        noiproj.ProgramID = logInVS.reportid
        noiproj.CreatedBy = user
        noiproj.LastChgBy = user

        mainSubmission.NOIProject = noiproj
        'noiproj.ProjectAddress = ucnaSiteInfo.Address1
        'noiproj.ProjectCity = ucnaSiteInfo.City
        'noiproj.ProjectStateAbv = ucnaSiteInfo.StateAbv
        'noiproj.ProjectPostalCode = ucnaSiteInfo.Zip
        Dim noiswconstruct As New NOISubmissionSWConstruct
        noiswconstruct.EntityState = EntityState.Added
        noiswconstruct.ProjectCounty = ucnaSiteInfo.County
        noiswconstruct.ProjectMunicipality = ucnaSiteInfo.Municipality
        noiswconstruct.ProjectTypeID = ddlProjectType.SelectedValue
        If ddlProjectType.SelectedValue = 10 Then
            noiswconstruct.ProjectTypeOther = txtProjectTypeOther.Text
        End If

        If rblSWPPPYesNo.SelectedValue = "No" Then
            noiswconstruct.IsSWPPPPrepared = "N"
        ElseIf rblSWPPPYesNo.SelectedValue = "Yes" Then
            noiswconstruct.IsSWPPPPrepared = "Y"
        End If
        noiswconstruct.DelegatedAgencyID = ddlPlanApprovalAgency.SelectedValue
        noiswconstruct.TotalLandArea = txtTotalAreaOfSite.Text
        noiswconstruct.EstimatedArea = txtAreaOfDisturbed.Text
        noiswconstruct.ConstructStartDate = txtConstructStartDate.Text
        noiswconstruct.ConstructEndDate = txtConstructCompleteDate.Text

        mainSubmission.NOISubmissionSWConstruct = noiswconstruct

        Dim noiLOC As New NOILoc
        noiLOC.EntityState = EntityState.Added
        noiLOC.Latitude = txtLatitude.Text
        noiLOC.Longitude = txtLongitude.Text
        If hfX.Value <> Nothing OrElse hfX.Value <> String.Empty Then
            noiLOC.X = hfX.Value
        End If
        If hfY.Value <> Nothing OrElse hfY.Value <> String.Empty Then
            noiLOC.Y = hfY.Value
        End If
        noiLOC.Watershed = txtWatershed.Text
        If hfWaterShedCode.Value <> String.Empty Then
            noiLOC.HUC_12_Code = common.NothingToString(hfWaterShedCode.Value)
        End If



        mainSubmission.NOILoc = noiLOC




        Dim lstPersonOrg As New List(Of NOISubmissionPersonOrg)
        Dim owner As New NOISubmissionPersonOrg
        owner.EntityState = EntityState.Added
        owner.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails
        owner.PersonOrgTypeCode = ucnaOwnerInfo.CompanyType
        If ucnaOwnerInfo.CompanyName <> String.Empty Then
            owner.OrgName = ucnaOwnerInfo.CompanyName
        End If
        owner.FName = ucnaOwnerInfo.FirstName
        owner.LName = ucnaOwnerInfo.LastName
        owner.Address1 = ucnaOwnerInfo.Address1
        owner.Address2 = ucnaOwnerInfo.Address2
        owner.City = ucnaOwnerInfo.City
        owner.StateAbv = ucnaOwnerInfo.StateAbv
        owner.PostalCode = ucnaOwnerInfo.Zip
        owner.Phone = ucnaOwnerInfo.Phone
        owner.PhoneExt = ucnaOwnerInfo.Ext
        owner.Mobile = ucnaOwnerInfo.Mobile
        owner.EmailAddress = ucnaOwnerInfo.Email

        lstPersonOrg.Add(owner)

        Dim contact As New NOISubmissionPersonOrg
        contact.EntityState = EntityState.Added
        contact.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails
        contact.OrgName = ucnaContactInfo.CompanyName
        contact.FName = ucnaContactInfo.FirstName
        contact.LName = ucnaContactInfo.LastName
        contact.Address1 = ucnaContactInfo.Address1
        contact.Address2 = ucnaContactInfo.Address2
        contact.City = ucnaContactInfo.City
        contact.StateAbv = ucnaContactInfo.StateAbv
        contact.PostalCode = ucnaContactInfo.Zip
        contact.Phone = ucnaContactInfo.Phone
        contact.PhoneExt = ucnaContactInfo.Ext
        contact.Mobile = ucnaContactInfo.Mobile
        contact.EmailAddress = ucnaContactInfo.Email

        lstPersonOrg.Add(contact)

        Dim billee As New NOISubmissionPersonOrg
        billee.EntityState = EntityState.Added
        billee.NOIPersonOrgTypeID = NOIPersonOrgType.BilleeDetails
        billee.OrgName = ucnaBilleeInfo.CompanyName
        billee.FName = ucnaBilleeInfo.FirstName
        billee.LName = ucnaBilleeInfo.LastName
        billee.Address1 = ucnaBilleeInfo.Address1
        billee.Address2 = ucnaBilleeInfo.Address2
        billee.City = ucnaBilleeInfo.City
        billee.StateAbv = ucnaBilleeInfo.StateAbv
        billee.PostalCode = ucnaBilleeInfo.Zip
        billee.Phone = ucnaBilleeInfo.Phone
        billee.PhoneExt = ucnaBilleeInfo.Ext
        billee.Mobile = ucnaBilleeInfo.Mobile
        billee.EmailAddress = ucnaBilleeInfo.Email

        lstPersonOrg.Add(billee)


        Dim projaddress As New NOISubmissionPersonOrg
        projaddress.EntityState = EntityState.Added
        projaddress.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress

        projaddress.Address1 = ucnaSiteInfo.Address1
        projaddress.City = ucnaSiteInfo.City
        projaddress.StateAbv = ucnaSiteInfo.StateAbv
        projaddress.PostalCode = ucnaSiteInfo.Zip

        lstPersonOrg.Add(projaddress)



        mainSubmission.NOISubmissionPersonOrg = lstPersonOrg




        Dim lstStatus As New List(Of NOISubmissionStatus)
        Dim openstatus As New NOISubmissionStatus
        openstatus.EntityState = EntityState.Added
        openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString

        lstStatus.Add(openstatus)

        mainSubmission.NOISubmissionStatus = lstStatus

        Return mainSubmission
    End Function
    Private Function MapFieldsToAddEntityForISGeneralPermit(ByVal user As String, ByVal mainSubmission As NOISubmission) As NOISubmission
        mainSubmission.EntityState = EntityState.Added
        mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype
        mainSubmission.CreatedBy = user
        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text
        Dim noiproj As New NOIProject
        noiproj.EntityState = EntityState.Added
        noiproj.ProjectName = ucnaSiteInfo.CompanyName
        noiproj.ProgramID = logInVS.reportid
        noiproj.CreatedBy = user
        noiproj.LastChgBy = user

        mainSubmission.NOIProject = noiproj


        Dim noiLOC As New NOILoc
        noiLOC.EntityState = EntityState.Added
        noiLOC.Latitude = txtLatitude.Text
        noiLOC.Longitude = txtLongitude.Text
        If hfX.Value <> Nothing OrElse hfX.Value <> String.Empty Then
            noiLOC.X = hfX.Value
        End If
        If hfY.Value <> Nothing OrElse hfY.Value <> String.Empty Then
            noiLOC.Y = hfY.Value
        End If
        noiLOC.Watershed = txtWatershed.Text
        If hfWaterShedCode.Value <> String.Empty Then
            noiLOC.HUC_12_Code = common.NothingToString(hfWaterShedCode.Value)
        End If


        mainSubmission.NOILoc = noiLOC


        If chkDisableNoExposure.Checked = False Then
            Dim lstNE As New List(Of NOISubmissionNE)
            For Each item As ListViewItem In lvNoExposure.Items
                Dim lblNOExposureCLID As Label = CType(item.FindControl("lblNOExposureCLID"), Label)
                Dim hfSubmissionNEID As HiddenField = CType(item.FindControl("hfSubmissionNEID"), HiddenField)
                Dim ddlResult As DropDownList = CType(item.FindControl("ddlResult"), DropDownList)

                Dim NE As New NOISubmissionNE
                NE.EntityState = EntityState.Added
                NE.NOExposureCLID = lblNOExposureCLID.Text
                NE.Answer = ddlResult.SelectedValue
                lstNE.Add(NE)
            Next
            mainSubmission.NOISubmissionNE = lstNE
        End If



        Dim lstPersonOrg As New List(Of NOISubmissionPersonOrg)
        Dim owner As New NOISubmissionPersonOrg
        owner.EntityState = EntityState.Added
        owner.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails
        owner.PersonOrgTypeCode = ucnaOwnerInfo.CompanyType
        If ucnaOwnerInfo.CompanyName <> String.Empty Then
            owner.OrgName = ucnaOwnerInfo.CompanyName
        End If
        owner.FName = ucnaOwnerInfo.FirstName
        owner.LName = ucnaOwnerInfo.LastName
        owner.Address1 = ucnaOwnerInfo.Address1
        owner.Address2 = ucnaOwnerInfo.Address2
        owner.City = ucnaOwnerInfo.City
        owner.StateAbv = ucnaOwnerInfo.StateAbv
        owner.PostalCode = ucnaOwnerInfo.Zip
        owner.Phone = ucnaOwnerInfo.Phone
        owner.PhoneExt = ucnaOwnerInfo.Ext
        owner.Mobile = ucnaOwnerInfo.Mobile
        owner.EmailAddress = ucnaOwnerInfo.Email

        lstPersonOrg.Add(owner)

        Dim contact As New NOISubmissionPersonOrg
        contact.EntityState = EntityState.Added
        contact.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails
        contact.OrgName = ucnaContactInfo.CompanyName
        contact.FName = ucnaContactInfo.FirstName
        contact.LName = ucnaContactInfo.LastName
        contact.Address1 = ucnaContactInfo.Address1
        contact.Address2 = ucnaContactInfo.Address2
        contact.City = ucnaContactInfo.City
        contact.StateAbv = ucnaContactInfo.StateAbv
        contact.PostalCode = ucnaContactInfo.Zip
        contact.Phone = ucnaContactInfo.Phone
        contact.PhoneExt = ucnaContactInfo.Ext
        contact.Mobile = ucnaContactInfo.Mobile
        contact.EmailAddress = ucnaContactInfo.Email

        lstPersonOrg.Add(contact)

        Dim billee As New NOISubmissionPersonOrg
        billee.EntityState = EntityState.Added
        billee.NOIPersonOrgTypeID = NOIPersonOrgType.BilleeDetails
        billee.OrgName = ucnaBilleeInfo.CompanyName
        billee.FName = ucnaBilleeInfo.FirstName
        billee.LName = ucnaBilleeInfo.LastName
        billee.Address1 = ucnaBilleeInfo.Address1
        billee.Address2 = ucnaBilleeInfo.Address2
        billee.City = ucnaBilleeInfo.City
        billee.StateAbv = ucnaBilleeInfo.StateAbv
        billee.PostalCode = ucnaBilleeInfo.Zip
        billee.Phone = ucnaBilleeInfo.Phone
        billee.PhoneExt = ucnaBilleeInfo.Ext
        billee.Mobile = ucnaBilleeInfo.Mobile
        billee.EmailAddress = ucnaBilleeInfo.Email

        lstPersonOrg.Add(billee)


        Dim projaddress As New NOISubmissionPersonOrg
        projaddress.EntityState = EntityState.Added
        projaddress.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress

        projaddress.Address1 = ucnaSiteInfo.Address1
        projaddress.City = ucnaSiteInfo.City
        projaddress.StateAbv = ucnaSiteInfo.StateAbv
        projaddress.PostalCode = ucnaSiteInfo.Zip

        lstPersonOrg.Add(projaddress)



        mainSubmission.NOISubmissionPersonOrg = lstPersonOrg

        Dim lstStatus As New List(Of NOISubmissionStatus)
        Dim openstatus As New NOISubmissionStatus
        openstatus.EntityState = EntityState.Added
        openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString
        lstStatus.Add(openstatus)

        mainSubmission.NOISubmissionStatus = lstStatus

        Return mainSubmission
    End Function
    Private Function MapFieldsToAddEntityForPesticideGeneralPermit(ByVal user As String, ByVal mainSubmission As NOISubmission) As NOISubmission
        mainSubmission.EntityState = EntityState.Added
        mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype
        mainSubmission.CreatedBy = user
        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text
        Dim noiproj As New NOIProject
        noiproj.EntityState = EntityState.Added
        noiproj.ProjectName = ucnaSiteInfo.CompanyName
        noiproj.ProgramID = logInVS.reportid
        noiproj.CreatedBy = user
        noiproj.LastChgBy = user

        mainSubmission.NOIProject = noiproj


        Dim noiap As New NOISubmissionAP

        noiap.EntityState = EntityState.Added
        noiap.EntityTypeID = rblEntityType.SelectedValue
        noiap.CommercialApplicatorID = txtCommercialApplicatorID.Text
        If chkInsectPestControl.Checked = True Then
            noiap.InsectPestControl = "Y"
        Else
            noiap.InsectPestControl = "N"
        End If

        If chkWeedPestControl.Checked = True Then
            noiap.WeedPestControl = "Y"
        Else
            noiap.WeedPestControl = "N"
        End If
        If chkAnimalPestControl.Checked = True Then
            noiap.AnimalPestControl = "Y"
        Else
            noiap.AnimalPestControl = "N"
        End If
        If chkForestCanopyPestControl.Checked = True Then
            noiap.ForestCanopyPestControl = "Y"
        Else
            noiap.ForestCanopyPestControl = "N"
        End If
        If rbtnThresholdNotExceeded.Checked = True Then
            noiap.NotExceededThreshold = "Y"
        Else
            noiap.NotExceededThreshold = "N"
        End If

        mainSubmission.NOISubmissionAP = noiap


        Dim lstPersonOrg As New List(Of NOISubmissionPersonOrg)
        Dim owner As New NOISubmissionPersonOrg
        owner.EntityState = EntityState.Added
        owner.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails
        owner.PersonOrgTypeCode = ucnaOwnerInfo.CompanyType
        If ucnaOwnerInfo.CompanyName <> String.Empty Then
            owner.OrgName = ucnaOwnerInfo.CompanyName
        End If
        owner.FName = ucnaOwnerInfo.FirstName
        owner.LName = ucnaOwnerInfo.LastName
        owner.Address1 = ucnaOwnerInfo.Address1
        owner.Address2 = ucnaOwnerInfo.Address2
        owner.City = ucnaOwnerInfo.City
        owner.StateAbv = ucnaOwnerInfo.StateAbv
        owner.PostalCode = ucnaOwnerInfo.Zip
        owner.Phone = ucnaOwnerInfo.Phone
        owner.PhoneExt = ucnaOwnerInfo.Ext
        owner.Mobile = ucnaOwnerInfo.Mobile
        owner.EmailAddress = ucnaOwnerInfo.Email

        lstPersonOrg.Add(owner)

        Dim contact As New NOISubmissionPersonOrg
        contact.EntityState = EntityState.Added
        contact.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails
        contact.OrgName = ucnaContactInfo.CompanyName
        contact.FName = ucnaContactInfo.FirstName
        contact.LName = ucnaContactInfo.LastName
        contact.Address1 = ucnaContactInfo.Address1
        contact.Address2 = ucnaContactInfo.Address2
        contact.City = ucnaContactInfo.City
        contact.StateAbv = ucnaContactInfo.StateAbv
        contact.PostalCode = ucnaContactInfo.Zip
        contact.Phone = ucnaContactInfo.Phone
        contact.PhoneExt = ucnaContactInfo.Ext
        contact.Mobile = ucnaContactInfo.Mobile
        contact.EmailAddress = ucnaContactInfo.Email

        lstPersonOrg.Add(contact)


        Dim projaddress As New NOISubmissionPersonOrg
        projaddress.EntityState = EntityState.Added
        projaddress.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress

        projaddress.FName = ucnaSiteInfo.FirstName
        projaddress.LName = ucnaSiteInfo.LastName
        projaddress.Address1 = ucnaSiteInfo.Address1
        projaddress.Address2 = ucnaSiteInfo.Address2
        projaddress.City = ucnaSiteInfo.City
        projaddress.StateAbv = ucnaSiteInfo.StateAbv
        projaddress.PostalCode = ucnaSiteInfo.Zip

        projaddress.Phone = ucnaSiteInfo.Phone
        projaddress.Mobile = ucnaSiteInfo.Mobile

        lstPersonOrg.Add(projaddress)



        mainSubmission.NOISubmissionPersonOrg = lstPersonOrg

        Dim lstStatus As New List(Of NOISubmissionStatus)
        Dim openstatus As New NOISubmissionStatus
        openstatus.EntityState = EntityState.Added
        openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString
        lstStatus.Add(openstatus)

        mainSubmission.NOISubmissionStatus = lstStatus

        Return mainSubmission
    End Function
    Private Function MapFieldsToAddEntityForPGPermitCorrection(ByVal user As String, ByVal mainSubmission As NOISubmission) As NOISubmission

        mainSubmission.ProgSubmissionTypeID = logInVS.progsubmisssiontype
        mainSubmission.CreatedBy = user
        mainSubmission.LastChgBy = user
        mainSubmission.Comments = txtComments.Text


        mainSubmission.NOISubmissionAP.EntityTypeID = rblEntityType.SelectedValue
        If txtCommercialApplicatorID.Text <> String.Empty Then
            mainSubmission.NOISubmissionAP.CommercialApplicatorID = txtCommercialApplicatorID.Text
        End If

        If chkInsectPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.InsectPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.InsectPestControl = "N"
        End If

        If chkWeedPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.WeedPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.WeedPestControl = "N"
        End If
        If chkAnimalPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.AnimalPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.AnimalPestControl = "N"
        End If
        If chkForestCanopyPestControl.Checked = True Then
            mainSubmission.NOISubmissionAP.ForestCanopyPestControl = "Y"
        Else
            mainSubmission.NOISubmissionAP.ForestCanopyPestControl = "N"
        End If
        If rbtnThresholdNotExceeded.Checked = True Then
            mainSubmission.NOISubmissionAP.NotExceededThreshold = "Y"
        Else
            mainSubmission.NOISubmissionAP.NotExceededThreshold = "N"
        End If


        SetOwnerInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails))
        SetContactInfo(mainSubmission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails))


        'Dim lstPersonOrg As New List(Of NOISubmissionPersonOrg)
        'Dim owner As New NOISubmissionPersonOrg
        'owner.EntityState = EntityState.Added
        'owner.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails
        'owner.PersonOrgTypeCode = ucnaOwnerInfo.CompanyType
        'If ucnaOwnerInfo.CompanyName <> String.Empty Then
        '    owner.OrgName = ucnaOwnerInfo.CompanyName
        'End If
        'owner.FName = ucnaOwnerInfo.FirstName
        'owner.LName = ucnaOwnerInfo.LastName
        'owner.Address1 = ucnaOwnerInfo.Address1
        'owner.Address2 = ucnaOwnerInfo.Address2
        'owner.City = ucnaOwnerInfo.City
        'owner.StateAbv = ucnaOwnerInfo.StateAbv
        'owner.PostalCode = ucnaOwnerInfo.Zip
        'owner.Phone = ucnaOwnerInfo.Phone
        'owner.PhoneExt = ucnaOwnerInfo.Ext
        'owner.Mobile = ucnaOwnerInfo.Mobile
        'owner.EmailAddress = ucnaOwnerInfo.Email

        'lstPersonOrg.Add(owner)

        'Dim contact As NOISubmissionPersonOrg = mainSubmission.NOISubmissionPersonOrg.Single(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.ContactDetails)
        'contact.OrgName = ucnaContactInfo.CompanyName
        'contact.FName = ucnaContactInfo.FirstName
        'contact.LName = ucnaContactInfo.LastName
        'contact.Address1 = ucnaContactInfo.Address1
        'contact.Address2 = ucnaContactInfo.Address2
        'contact.City = ucnaContactInfo.City
        'contact.StateAbv = ucnaContactInfo.StateAbv
        'contact.PostalCode = ucnaContactInfo.Zip
        'contact.Phone = ucnaContactInfo.Phone
        'contact.PhoneExt = ucnaContactInfo.Ext
        'contact.Mobile = ucnaContactInfo.Mobile
        'contact.EmailAddress = ucnaContactInfo.Email

        'lstPersonOrg.Add(contact)


        'Dim projaddress As New NOISubmissionPersonOrg
        'projaddress.EntityState = EntityState.Added
        'projaddress.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress

        'projaddress.FName = ucnaSiteInfo.FirstName
        'projaddress.LName = ucnaSiteInfo.LastName
        'projaddress.Address1 = ucnaSiteInfo.Address1
        'projaddress.Address2 = ucnaSiteInfo.Address2
        'projaddress.City = ucnaSiteInfo.City
        'projaddress.StateAbv = ucnaSiteInfo.StateAbv
        'projaddress.PostalCode = ucnaSiteInfo.Zip

        'projaddress.Phone = ucnaSiteInfo.Phone
        'projaddress.Mobile = ucnaSiteInfo.Mobile

        'lstPersonOrg.Add(projaddress)



        'mainSubmission.NOISubmissionPersonOrg = lstPersonOrg
        Dim lstStatus As New List(Of NOISubmissionStatus)
        Dim openstatus As New NOISubmissionStatus
        openstatus.EntityState = EntityState.Added
        openstatus.SubmissionStatusCode = SubmissionStatusCode.O.ToString
        lstStatus.Add(openstatus)

        mainSubmission.NOISubmissionStatus = lstStatus

        Return mainSubmission
    End Function
    Private WriteOnly Property Enabled As Boolean
        Set(value As Boolean)
            ucnaOwnerInfo.Enabled = value
            ucnaContactInfo.Enabled = value
            ucnaSiteInfo.Enabled = value
            hfIsLocked.Value = IIf(value = True, "N", "Y")
            txtComments.Enabled = value
            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    ucnaBilleeInfo.Enabled = value
                    ddlProjectType.Enabled = value
                    txtProjectTypeOther.Enabled = value
                    lvTaxparcel.Enabled = value
                    lvSWBMP.Enabled = value
                    'btnMapPoint.Disabled = Not value
                    txtLatitude.Enabled = value
                    txtLongitude.Enabled = value
                    'rbYes.Enabled = value
                    'rbNo.Enabled = value
                    rblSWPPPYesNo.Enabled = value
                    ddlPlanApprovalAgency.Enabled = value
                    txtTotalAreaOfSite.Enabled = value
                    txtAreaOfDisturbed.Enabled = value
                    txtConstructStartDate.Enabled = value
                    txtConstructCompleteDate.Enabled = value

                Case NOIProgramType.ISGeneralPermit
                    ucnaBilleeInfo.Enabled = value
                    lvTaxparcel.Enabled = value
                    txtLatitude.Enabled = value
                    txtLongitude.Enabled = value
                    'rbYes.Enabled = value
                    'rbNo.Enabled = value
                    lvSIC.Enabled = value
                    lvNAICS.Enabled = value
                    chkDisableNoExposure.Enabled = value
                    lvNoExposure.Enabled = value
                    lvOutfall.Enabled = value
                    If lvOutfall.InsertItem IsNot Nothing Then
                        Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
                        btnMapOutfallPoint.Disabled = Not value
                    End If
                Case NOIProgramType.PesticideGeneralPermit
                    rblEntityType.Enabled = value
                    txtCommercialApplicatorID.Enabled = value
                    chkInsectPestControl.Enabled = value
                    chkWeedPestControl.Enabled = value
                    chkAnimalPestControl.Enabled = value
                    chkForestCanopyPestControl.Enabled = value
                    rbtnThresholdNotExceeded.Enabled = value
                    lvAPChemicals.Enabled = value
            End Select

        End Set
    End Property
    Private Property ProjectTypeVisible As Boolean
        Get
            Return divProjectType.Visible
        End Get
        Set(value As Boolean)
            divProjectType.Visible = value
        End Set
    End Property

    Private Property OtherInfoVisible As Boolean
        Get
            Return divOtherInfo.Visible
        End Get
        Set(value As Boolean)
            divOtherInfo.Visible = value
        End Set
    End Property

    Private Property SicCodesVisible As Boolean
        Get
            Return divSicCodes.Visible
        End Get
        Set(value As Boolean)
            divSicCodes.Visible = value
        End Set
    End Property

    Private Property NAICSCodesVisible As Boolean
        Get
            Return divNAICSCodes.Visible
        End Get
        Set(value As Boolean)
            divNAICSCodes.Visible = value
        End Set
    End Property

    Private Property NoExposureVisible As Boolean
        Get
            Return divNoExposure.Visible
        End Get
        Set(value As Boolean)
            divNoExposure.Visible = value
        End Set
    End Property

    Private Property OutfallsVisible As Boolean
        Get
            Return divOutfall.Visible
        End Get
        Set(value As Boolean)
            divOutfall.Visible = value
        End Set
    End Property

    Private Property BMPVisible As Boolean
        Get
            Return divbmp.Visible
        End Get
        Set(value As Boolean)
            divbmp.Visible = value
        End Set
    End Property

    Public Function GetTaxParcelLst() As List(Of NOISubmissionTaxParcels)
        Dim lstNOITaxParcels As List(Of NOISubmissionTaxParcels)
        lstNOITaxParcels = IndNOISubmission.NOISubmissionTaxParcels.Where(Function(e) e.IsDeleted = False).ToList() 'TaxparcelLst'
        Dim parcels As String = String.Empty
        For Each tp As NOISubmissionTaxParcels In lstNOITaxParcels
            parcels = parcels + tp.TaxParcelNumber + ","
        Next
        If parcels = String.Empty Then
            hfTaxParcel.Value = String.Empty
        Else
            hfTaxParcel.Value = Left(parcels, parcels.Length - 1)
        End If

        Return lstNOITaxParcels
    End Function

    Public Function GetProjectType() As IQueryable(Of ProjectTypelst)
        Dim bal As New SWBAL()
        Return bal.GetProjectType()
    End Function

    Public Function GetProjectSWBMPLst() As List(Of NOISubmissionSWBMP)
        Dim lstSubmissionSWBMPs As List(Of NOISubmissionSWBMP)
        lstSubmissionSWBMPs = IndNOISubmission.NOISubmissionSWBMP.Where(Function(e) e.IsDeleted = False).ToList()

        Return lstSubmissionSWBMPs
    End Function

    Public Function GetSWBMPLst() As List(Of SWBMPlst)
        Dim bal As New SWBAL()
        Return bal.GetSWBMPList()
    End Function

    Public Function GetPesticidePatterns() As List(Of NOIPesticidePattern)
        Dim bal As New APBAL
        Return bal.GetPesticidePatterns()
    End Function


    Public Function GetPlanApprovalAgency() As List(Of PlanApprovalAgency)
        Dim bal As New SWBAL()
        Return bal.GetPlanApprovalAgencyList()
    End Function

    Public Function GetTaxKentHundred() As IList(Of TaxKentHundred)
        Dim bal As New SWBAL()
        Return bal.GetTaxKentHundred()
    End Function

    Public Function GetTaxKentTowns() As IList(Of TaxKentTown)
        Dim bal As New SWBAL()
        Return bal.GetTaxKentTowns()
    End Function

    Public Function GetSubmissionNAICSCodes() As List(Of NOISubmissionNAICS)
        Dim lstSubmissionNAICSCodes As List(Of NOISubmissionNAICS)
        lstSubmissionNAICSCodes = IndNOISubmission.NOISubmissionNAICS.Where(Function(e) e.IsDeleted = False).ToList()
        Return lstSubmissionNAICSCodes
    End Function

    Public Function GetSubmissionSICCodes() As List(Of NOISubmissionSIC)
        Dim lstSubmissionSICCodes As List(Of NOISubmissionSIC)
        lstSubmissionSICCodes = IndNOISubmission.NOISubmissionSIC.Where(Function(e) e.IsDeleted = False).ToList()
        Return lstSubmissionSICCodes
    End Function

    Public Function rblEntityType_GetData() As List(Of NOIEntityType)
        Dim bal As New APBAL()
        Return bal.GetAPEntityType()
    End Function

    Private Sub ResetScrollPosition()
        If Not ClientScript.IsClientScriptBlockRegistered(Me.GetType(), "CreateResetScrollPosition") Then
            'Create the ResetScrollPosition() function
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "CreateResetScrollPosition",
                            "function ResetScrollPosition() {" & vbCrLf &
                            "  var scrollX = document.getElementById('__SCROLLPOSITIONX');" & vbCrLf &
                            "  var scrollY = document.getElementById('__SCROLLPOSITIONY');" & vbCrLf &
                            "  if (scrollX && scrollY) {" & vbCrLf &
                            "    scrollX.value = 0;" & vbCrLf &
                            "    scrollY.value = 0;" & vbCrLf &
                            "  }" & vbCrLf &
                            "}", True)

            'Add the call to the ResetScrollPosition() function
            ClientScript.RegisterStartupScript(Me.GetType(), "CallResetScrollPosition", "ResetScrollPosition();", True)
        End If
    End Sub

    Private Sub wzNOI_ActiveStepChanged(sender As Object, e As EventArgs) Handles wzNOI.ActiveStepChanged

        Dim ns As NOISubmission
        ResetScrollPosition()

        If wzNOI.ActiveStepIndex = 1 Then

            If hfNOISubmissionID.Value = String.Empty OrElse hfNOISubmissionID.Value = "0" Then
                ucnaSiteInfo.StateAbv = "DE"
            Else
                ns = IndNOISubmission
                ucnaSiteInfo.StateAbv = ns.NOISubmissionPersonOrg.Single(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress).StateAbv
            End If
        ElseIf wzNOI.ActiveStepIndex = 2 Then
            If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
                ns = IndNOISubmission
                If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                    ddlPlanApprovalAgency.SelectedValue = ns.NOISubmissionSWConstruct.DelegatedAgencyID
                End If
            End If
        End If


    End Sub

    Private Sub wzNOI_CancelButtonClick(sender As Object, e As EventArgs) Handles wzNOI.CancelButtonClick
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

    Private Sub wzNOI_FinishButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles wzNOI.FinishButtonClick
        'TODO CODE UPDATE FOR MULTIPLE PROGRAMS.

        Dim str As String = String.Empty
        'Dim bal As New SWBAL()
        Dim ns As NOISubmission = Nothing
        Try
            Page.Validate("ValidateNOI")

            If Page.IsValid = True Then
                If hfNOISubmissionID.Value = String.Empty OrElse hfNOISubmissionID.Value = "0" Then
                    Select Case logInVS.reportid
                        Case NOIProgramType.CSSGeneralPermit
                            Dim bal As New SWBAL()
                            ns = bal.Insert(MapFieldsToAddEntity(NOIProgramType.CSSGeneralPermit))
                        Case NOIProgramType.ISGeneralPermit
                            Dim bal As New ISWBAL()
                            ns = bal.Insert(MapFieldsToAddEntity(NOIProgramType.ISGeneralPermit))
                        Case NOIProgramType.PesticideGeneralPermit
                            Dim bal As New APBAL()
                            ns = bal.Insert(MapFieldsToAddEntity(NOIProgramType.PesticideGeneralPermit))

                    End Select
                    hfNOISubmissionID.Value = ns.SubmissionID
                    logInVS.submissionid = ns.SubmissionID
                Else
                    ns = IndNOISubmission
                    If Not isViewOnly Then
                        Select Case logInVS.reportid
                            Case NOIProgramType.CSSGeneralPermit
                                Dim bal As New SWBAL()
                                ns = bal.Insert(MapFieldsToModifiedEntity(NOIProgramType.CSSGeneralPermit))
                            Case NOIProgramType.ISGeneralPermit
                                Dim bal As New ISWBAL()
                                ns = bal.Insert(MapFieldsToModifiedEntity(NOIProgramType.ISGeneralPermit))
                            Case NOIProgramType.PesticideGeneralPermit
                                Dim bal As New APBAL()
                                ns = bal.Insert(MapFieldsToModifiedEntity(NOIProgramType.PesticideGeneralPermit))

                        End Select


                        'ns = bal.Insert(MapFieldsToModifiedEntity())
                    End If

                End If
                IndNOISubmission = ns

                If (ns.NOISubmissionStatus.OrderByDescending(Function(a) a.SubmissionStatusDate).First().SubmissionStatusCode = SubmissionStatusCode.O.ToString) OrElse
                        (ns.NOISubmissionStatus.OrderByDescending(Function(a) a.SubmissionStatusDate).First().SubmissionStatusCode = SubmissionStatusCode.R.ToString) Then
                    responseRedirect("~/Forms/NOIAgreement.aspx")
                Else
                    responseRedirect("~/Forms/SubmissionDetails.aspx")
                End If
            End If
        Catch ex As Exception
            Dim bal1 As New NOIBAL
            bal1.LogError(Me, ex)
            Throw (ex)
        End Try


    End Sub

    Private Sub wzNOI_PreviousButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles wzNOI.PreviousButtonClick
        'TODO CODE FOR FINISH PREVIOUS BUTTON

    End Sub

    Private Sub wzNOI_SideBarButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles wzNOI.SideBarButtonClick


        If hfNOISubmissionID.Value = String.Empty OrElse hfNOISubmissionID.Value = "0" Then
            If e.CurrentStepIndex = 0 And e.NextStepIndex = 2 Then
                wzNOI.ActiveStepIndex = 1
            End If
        End If

        If (e.CurrentStepIndex = 0 And e.NextStepIndex = 1) Or (e.CurrentStepIndex = 1 And e.NextStepIndex = 2) Or (e.CurrentStepIndex = 0 And e.NextStepIndex = 2) Then
            Page.Validate("ValidateNOI")
            If Not Page.IsValid() Then
                e.Cancel = True
            End If
        End If


    End Sub

    Private Sub chkDisableNoExposure_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisableNoExposure.CheckedChanged
        If chkDisableNoExposure.Checked = True Then
            lvNoExposure.Enabled = False
            cvNoExposure.Enabled = False
            'lvOutfall.Enabled = True
            'Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
            'btnMapOutfallPoint.Disabled = False
            logInVS.progsubmisssiontype = 5
        Else
            lvNoExposure.Enabled = True
            cvNoExposure.Enabled = True
            lvNoExposure.DataBind()
            Dim ddlResultAll As DropDownList = CType(lvNoExposure.FindControl("ddlResultAll"), DropDownList)
            ddlResultAll.SelectedValue = ""
            'lvOutfall.Enabled = False
            'Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
            'btnMapOutfallPoint.Disabled = True
            logInVS.progsubmisssiontype = 6
        End If
    End Sub

    Private Sub SetNOExposureResult(ByVal answer As String, ByRef NE As NOISubmissionNE)
        If NE.Answer <> answer Then
            NE.EntityState = EntityState.Modified
            NE.Answer = answer
        End If
    End Sub

    Private Sub SetOwnerInfo(ByRef Owner As NOISubmissionPersonOrg)
        Owner.PersonOrgTypeCode = ucnaOwnerInfo.CompanyType
        If Owner.PersonOrgTypeCode <> "P" Then
            Owner.OrgName = ucnaOwnerInfo.CompanyName
        End If
        Owner.FName = ucnaOwnerInfo.FirstName
        Owner.LName = ucnaOwnerInfo.LastName
        Owner.Address1 = ucnaOwnerInfo.Address1
        Owner.Address2 = ucnaOwnerInfo.Address2
        Owner.City = ucnaOwnerInfo.City
        Owner.StateAbv = ucnaOwnerInfo.StateAbv
        Owner.PostalCode = ucnaOwnerInfo.Zip
        Owner.Phone = ucnaOwnerInfo.Phone
        Owner.PhoneExt = ucnaOwnerInfo.Ext
        Owner.Mobile = ucnaOwnerInfo.Mobile
        Owner.EmailAddress = ucnaOwnerInfo.Email
    End Sub

    Private Sub SetContactInfo(ByRef contact As NOISubmissionPersonOrg)
        contact.OrgName = ucnaContactInfo.CompanyName
        contact.FName = ucnaContactInfo.FirstName
        contact.LName = ucnaContactInfo.LastName
        contact.Address1 = ucnaContactInfo.Address1
        contact.Address2 = ucnaContactInfo.Address2
        contact.City = ucnaContactInfo.City
        contact.StateAbv = ucnaContactInfo.StateAbv
        contact.PostalCode = ucnaContactInfo.Zip
        contact.Phone = ucnaContactInfo.Phone
        contact.PhoneExt = ucnaContactInfo.Ext
        contact.Mobile = ucnaContactInfo.Mobile
        contact.EmailAddress = ucnaContactInfo.Email
    End Sub

    Private Sub SetBilleeInfo(ByRef billee As NOISubmissionPersonOrg)
        billee.OrgName = ucnaBilleeInfo.CompanyName
        billee.FName = ucnaBilleeInfo.FirstName
        billee.LName = ucnaBilleeInfo.LastName
        billee.Address1 = ucnaBilleeInfo.Address1
        billee.Address2 = ucnaBilleeInfo.Address2
        billee.City = ucnaBilleeInfo.City
        billee.StateAbv = ucnaBilleeInfo.StateAbv
        billee.PostalCode = ucnaBilleeInfo.Zip
        billee.Phone = ucnaBilleeInfo.Phone
        billee.PhoneExt = ucnaBilleeInfo.Ext
        billee.Mobile = ucnaBilleeInfo.Mobile
        billee.EmailAddress = ucnaBilleeInfo.Email
    End Sub

    Private Sub SetProjectAddress(ByRef ProAddress As NOISubmissionPersonOrg)
        ProAddress.Address1 = ucnaSiteInfo.Address1
        ProAddress.City = ucnaSiteInfo.City
        ProAddress.StateAbv = ucnaSiteInfo.StateAbv
        ProAddress.PostalCode = ucnaSiteInfo.Zip
    End Sub

    Public Sub lvTaxparcel_InsertItem()
        Dim item = New NoticeOfIntent.NOISubmissionTaxParcels()
        TryUpdateModel(item)
        Try
            If ModelState.IsValid Then
                Dim submission As NOISubmission
                submission = IndNOISubmission
                If Not IsTaxParcelDuplicated(submission, item) Then
                    item.EntityState = EntityState.Added
                    submission.NOISubmissionTaxParcels.Add(item) ' = noiTaxparcels
                End If
                'If hfTaxParcel.Value = String.Empty Then
                '    hfTaxParcel.Value = item.TaxParcelNumber
                'Else
                '    hfTaxParcel.Value = hfTaxParcel.Value + "," + item.TaxParcelNumber
                'End If
                IndNOISubmission = submission
                'TaxparcelLst = noiTaxparcels
            End If
        Catch ex As Exception
            Dim bal1 As New NOIBAL
            bal1.LogError(Me, ex)
            Throw ex
        End Try

    End Sub

    Private Function IsTaxParcelDuplicated(submission As NOISubmission, item As NOISubmissionTaxParcels) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOISubmissionTaxParcels In submission.NOISubmissionTaxParcels
            If i.TaxParcelNumber = item.TaxParcelNumber Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvTaxparcel_DeleteItem(ByVal SubmissionTaxParcelID As Integer, ByVal TaxParcelNumber As String)


        Dim submission As NOISubmission
        submission = IndNOISubmission
        Dim item As NOISubmissionTaxParcels
        If SubmissionTaxParcelID <> 0 Then
            item = submission.NOISubmissionTaxParcels.SingleOrDefault(Function(e) e.SubmissionTaxParcelID = SubmissionTaxParcelID)
            submission.NOISubmissionTaxParcels.Remove(item)
            item.IsDeleted = True
            item.EntityState = EntityState.Deleted
            submission.NOISubmissionTaxParcels.Add(item)
        Else
            item = submission.NOISubmissionTaxParcels.SingleOrDefault(Function(e) e.TaxParcelNumber = TaxParcelNumber)
            submission.NOISubmissionTaxParcels.Remove(item)
        End If
        IndNOISubmission = submission
    End Sub

    Private Function IsSWBMPDuplicated(submission As NOISubmission, item As NOISubmissionSWBMP) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOISubmissionSWBMP In submission.NOISubmissionSWBMP
            If i.SWBMPID = item.SWBMPID Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function

    Public Sub lvSWBMP_InsertItem()
        Dim item = New NoticeOfIntent.NOISubmissionSWBMP()
        TryUpdateModel(item)

        Dim bal As New SWBAL()
        item.SWBMPlst = bal.GetSWBMPList().Single(Function(a) a.SWBMPID = item.SWBMPID)
        If ModelState.IsValid Then
            ' Save changes here
            Dim submission As NOISubmission
            submission = IndNOISubmission
            If Not IsSWBMPDuplicated(submission, item) Then
                item.EntityState = EntityState.Added
                submission.NOISubmissionSWBMP.Add(item)
            End If
            IndNOISubmission = submission

        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvSWBMP_DeleteItem(ByVal SubmissionSWBMPID As Integer, ByVal SWBMPID As Integer)
        Dim submission As NOISubmission
        submission = IndNOISubmission
        Dim item As NOISubmissionSWBMP
        If SubmissionSWBMPID <> 0 Then
            item = submission.NOISubmissionSWBMP.SingleOrDefault(Function(e) e.SubmissionSWBMPID = SubmissionSWBMPID)
            submission.NOISubmissionSWBMP.Remove(item)
            item.IsDeleted = True
            item.EntityState = EntityState.Deleted
            submission.NOISubmissionSWBMP.Add(item)
        Else
            item = submission.NOISubmissionSWBMP.SingleOrDefault(Function(e) e.SWBMPID = SWBMPID)
            submission.NOISubmissionSWBMP.Remove(item)
        End If
        IndNOISubmission = submission

    End Sub

    Private Function IsSICDuplicated(submission As NOISubmission, item As NOISubmissionSIC) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOISubmissionSIC In submission.NOISubmissionSIC
            If i.SICCode = item.SICCode Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function

    Public Sub lvSIC_InsertItem()
        If lvSIC.Items.Count < 4 Then
            Dim item = New NoticeOfIntent.NOISubmissionSIC()
            TryUpdateModel(item)
            If ModelState.IsValid Then
                ' Save changes here
                Dim submission As NOISubmission
                submission = IndNOISubmission
                If Not IsSICDuplicated(submission, item) Then
                    item.EntityState = EntityState.Added
                    submission.NOISubmissionSIC.Add(item)
                End If
                IndNOISubmission = submission
            End If
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvSIC_DeleteItem(ByVal SubmissionSICID As Integer, ByVal SICCode As String)
        Dim submission As NOISubmission
        submission = IndNOISubmission
        Dim item As NOISubmissionSIC
        If SubmissionSICID <> 0 Then
            item = submission.NOISubmissionSIC.SingleOrDefault(Function(e) e.SubmissionSICID = SubmissionSICID)
            submission.NOISubmissionSIC.Remove(item)
            item.IsDeleted = True
            item.EntityState = EntityState.Deleted
            submission.NOISubmissionSIC.Add(item)
        Else
            item = submission.NOISubmissionSIC.SingleOrDefault(Function(e) e.SICCode = SICCode)
            submission.NOISubmissionSIC.Remove(item)
        End If
        IndNOISubmission = submission
    End Sub

    Private Function IsNAICSDuplicated(submission As NOISubmission, item As NOISubmissionNAICS) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOISubmissionNAICS In submission.NOISubmissionNAICS
            If i.NAICSCode = item.NAICSCode Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function
    Public Sub lvNAICS_InsertItem()
        If lvNAICS.Items.Count < 4 Then
            Dim item = New NoticeOfIntent.NOISubmissionNAICS()
        TryUpdateModel(item)
            If ModelState.IsValid Then
                ' Save changes here
                Dim submission As NOISubmission
                submission = IndNOISubmission
                If Not IsNAICSDuplicated(submission, item) Then
                    item.EntityState = EntityState.Added
                    submission.NOISubmissionNAICS.Add(item)
                End If
                IndNOISubmission = submission
            End If
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvNAICS_DeleteItem(ByVal SubmissionNAICSID As Integer, ByVal NAICSCode As String)
        Dim submission As NOISubmission
        submission = IndNOISubmission
        Dim item As NOISubmissionNAICS
        If SubmissionNAICSID <> 0 Then
            item = submission.NOISubmissionNAICS.SingleOrDefault(Function(e) e.SubmissionNAICSID = SubmissionNAICSID)
            submission.NOISubmissionNAICS.Remove(item)
            item.IsDeleted = True
            item.EntityState = EntityState.Deleted
            submission.NOISubmissionNAICS.Add(item)
        Else
            item = submission.NOISubmissionNAICS.SingleOrDefault(Function(e) e.NAICSCode = NAICSCode)
            submission.NOISubmissionNAICS.Remove(item)
        End If
        IndNOISubmission = submission
    End Sub

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvNoExposure_GetData() As IList
        Dim bal As New ISWBAL()
        Return bal.GetNoExposureList(IndNOISubmission)
    End Function

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvOutfall_GetData() As List(Of NoticeOfIntent.NOISubmissionUnit)
        Dim lstSubmissionUnits As List(Of NOISubmissionUnit)
        lstSubmissionUnits = IndNOISubmission.NOISubmissionUnit.Where(Function(e) e.IsDeleted = False).ToList()
        Dim outfalls As String = ""
        For Each outfall As NOISubmissionUnit In lstSubmissionUnits
            outfalls = outfalls + outfall.UnitName + ":" + outfall.NOILoc.Latitude.ToString + ":" + outfall.NOILoc.Longitude.ToString + "::"
        Next
        If outfalls = String.Empty Then
            hfOutfalllist.Value = String.Empty
        Else
            hfOutfalllist.Value = Left(outfalls, outfalls.Length - 2)
        End If

        Return lstSubmissionUnits
    End Function
    Private Function IsOutfallDuplicated(submission As NOISubmission, item As NOISubmissionUnit) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOISubmissionUnit In submission.NOISubmissionUnit
            If i.UnitName = item.UnitName Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function
    Public Sub lvOutfall_InsertItem()
        Dim item = New NoticeOfIntent.NOISubmissionUnit()
        TryUpdateModel(item)
        If ModelState.IsValid Then
            ' Save changes here
            Dim submission As NOISubmission
            submission = IndNOISubmission
            If Not IsOutfallDuplicated(submission, item) Then
                item.EntityState = EntityState.Added
                item.NOILoc.EntityState = EntityState.Added
                submission.NOISubmissionUnit.Add(item)
            End If
            IndNOISubmission = submission
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvOutfall_DeleteItem(ByVal SubmissionUnitID As Integer, ByVal UnitName As String)
        Dim submission As NOISubmission
        submission = IndNOISubmission
        Dim item As NOISubmissionUnit
        If SubmissionUnitID <> 0 Then
            item = submission.NOISubmissionUnit.SingleOrDefault(Function(e) e.SubmissionUnitID = SubmissionUnitID)
            submission.NOISubmissionUnit.Remove(item)
            item.IsDeleted = True
            item.EntityState = EntityState.Deleted
            submission.NOISubmissionUnit.Add(item)
        Else
            item = submission.NOISubmissionUnit.SingleOrDefault(Function(e) e.UnitName = UnitName)
            submission.NOISubmissionUnit.Remove(item)
        End If
        IndNOISubmission = submission
    End Sub


    Private Sub cvTaxparcel_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvTaxparcel.ServerValidate
        Dim submission As NOISubmission
        submission = IndNOISubmission
        If submission.NOISubmissionTaxParcels.Where(Function(e) e.IsDeleted = False).Count <= 0 Then
            'args.IsValid = False
        End If
    End Sub

    Private Sub cvSWPPPYesNo_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvSWPPPYesNo.ServerValidate
        If args.Value = "No" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub IsProjectNameExists(source As Object, args As ServerValidateEventArgs)
        'If hfNOISubmissionID.Value = String.Empty AndAlso hfNOISubmissionID.Value = "0" Then
        Dim abal As New AdminBAL
        Dim prolst As List(Of String)
        prolst = abal.GetInternalProjectListByName(ucnaSiteInfo.CompanyName, CacheLookupData.GetPiTypeIDByReportID(logInVS.reportid))
        If prolst.Count = 0 Then
            args.IsValid = True
        Else
            args.IsValid = False
        End If
        'End If

    End Sub

    Protected Sub sideBarList_ItemDataBound(sender As Object, e As ListViewItemEventArgs)
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim btnlink As LinkButton = CType(e.Item.FindControl("SideBarButton"), LinkButton)
            If e.Item.DataItemIndex = wzNOI.ActiveStepIndex Then
                btnlink.Font.Bold = True

            End If
        End If
    End Sub

    Private Sub lvOutfall_ItemCreated(sender As Object, e As ListViewItemEventArgs) Handles lvOutfall.ItemCreated
        If e.Item.ItemType = ListViewItemType.InsertItem Then
            Dim txtoutfallLatitude As TextBox = CType(e.Item.FindControl("txtoutfallLatitude"), TextBox)
            Dim txtoutfallLongitude As TextBox = CType(e.Item.FindControl("txtoutfallLongitude"), TextBox)
            Dim txtoutfallWatershed As TextBox = CType(e.Item.FindControl("txtoutfallWatershed"), TextBox)
            txtoutfallLatitude.Attributes.Add("readonly", "readonly")
            txtoutfallLongitude.Attributes.Add("readonly", "readonly")
            txtoutfallWatershed.Attributes.Add("readonly", "readonly")

        End If
    End Sub

    Protected Sub ddlResultAll_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlResultAll As DropDownList = CType(sender, DropDownList)
        For Each lst As ListViewItem In lvNoExposure.Items
            Dim ddlResult As DropDownList = CType(lst.FindControl("ddlResult"), DropDownList)
            ddlResult.SelectedValue = ddlResultAll.SelectedValue
        Next
        If ddlResultAll.SelectedValue = "Y" Or ddlResultAll.SelectedValue = "" Then
            chkDisableNoExposure.Checked = True
            lvNoExposure.Enabled = False
            'lvOutfall.Enabled = True
            'Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
            'btnMapOutfallPoint.Disabled = False
            logInVS.progsubmisssiontype = 5
        Else
            'lvOutfall.Enabled = False
            'Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
            'btnMapOutfallPoint.Disabled = True
            logInVS.progsubmisssiontype = 6
        End If
    End Sub

    Private Sub cvNoExposure_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvNoExposure.ServerValidate
        Dim ddlResult As DropDownList
        For Each lst As ListViewItem In lvNoExposure.Items
            ddlResult = CType(lst.FindControl("ddlResult"), DropDownList)
            If ddlResult.SelectedValue = "" Then
                cvNoExposure.ErrorMessage = "Please answer to all the questions."
                cvNoExposure.ToolTip = "Please answer to all the questions."
                args.IsValid = False
                Exit For
            End If
        Next
        If args.IsValid = True Then
            For Each lst As ListViewItem In lvNoExposure.Items
                ddlResult = CType(lst.FindControl("ddlResult"), DropDownList)
                If ddlResult.SelectedValue = "Y" Then
                    cvNoExposure.ErrorMessage = "Not eligible for the No Exposure."
                    cvNoExposure.ToolTip = "Not eligible for the No Exposure."
                    args.IsValid = False
                    Exit For
                End If
            Next
        End If

    End Sub

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvAPChemicals_GetData() As IList(Of NoticeOfIntent.NOISubmissionAPChemicals)

        Return IndNOISubmission.NOISubmissionAPChemicals.Where(Function(e) e.IsDeleted = False).ToList()

    End Function

    Private Function IsChemicalDuplicated(submission As NOISubmission, item As NOISubmissionAPChemicals) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOISubmissionAPChemicals In submission.NOISubmissionAPChemicals
            If i.Ingredient = item.Ingredient Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function

    Public Sub lvAPChemicals_InsertItem()
        If lvAPChemicals.EditIndex = -1 Then
            Dim item = New NoticeOfIntent.NOISubmissionAPChemicals()
            TryUpdateModel(item)
            Dim bal As New APBAL()
            item.NOIPesticidePattern = bal.GetPesticidePatterns().Single(Function(a) a.PesticidePatternID = item.PesticidePatternID)
            If ModelState.IsValid Then
                ' Save changes here
                Dim submission As NOISubmission
                submission = IndNOISubmission
                If Not IsChemicalDuplicated(submission, item) Then
                    item.EntityState = EntityState.Added
                    submission.NOISubmissionAPChemicals.Add(item)
                End If
                IndNOISubmission = submission
            End If
        Else
            ErrorSummary.AddError("Update or Cancel the pending edited row", Me)
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvAPChemicals_DeleteItem(ByVal APChemicalID As Integer, ByVal Ingredient As String)
        If lvAPChemicals.EditIndex = -1 Then
            Dim submission As NOISubmission
            submission = IndNOISubmission
            Dim item As NOISubmissionAPChemicals
            If APChemicalID <> 0 Then
                item = submission.NOISubmissionAPChemicals.SingleOrDefault(Function(e) e.APChemicalID = APChemicalID)
                submission.NOISubmissionAPChemicals.Remove(item)
                item.IsDeleted = True
                item.EntityState = EntityState.Deleted
                submission.NOISubmissionAPChemicals.Add(item)
            Else
                item = submission.NOISubmissionAPChemicals.SingleOrDefault(Function(e) e.Ingredient = Ingredient)
                submission.NOISubmissionAPChemicals.Remove(item)
            End If

            IndNOISubmission = submission
        Else
            ErrorSummary.AddError("Update or Cancel the pending edited row", Me)
        End If

    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvAPChemicals_UpdateItem(ByVal apc As NOISubmissionAPChemicals) ' ByVal APChemicalID As Integer, ByVal Ingredient As String)
        Dim item As NoticeOfIntent.NOISubmissionAPChemicals = Nothing
        Dim submission As NOISubmission
        submission = IndNOISubmission
        Dim bal As New APBAL()
        apc.NOIPesticidePattern = bal.GetPesticidePatterns().Single(Function(a) a.PesticidePatternID = apc.PesticidePatternID)
        If apc.APChemicalID <> 0 Then
            item = submission.NOISubmissionAPChemicals.SingleOrDefault(Function(e) e.APChemicalID = apc.APChemicalID)
            If item.Equals(apc) = False Then
                submission.NOISubmissionAPChemicals.Remove(item)
                apc.EntityState = EntityState.Modified
                submission.NOISubmissionAPChemicals.Add(apc)
            End If
        Else
            item = submission.NOISubmissionAPChemicals.SingleOrDefault(Function(e) e.Ingredient = apc.Ingredient)
            If item.Equals(apc) = False Then
                submission.NOISubmissionAPChemicals.Remove(item)
                apc.EntityState = EntityState.Added
                submission.NOISubmissionAPChemicals.Add(apc)
            End If
        End If
        IndNOISubmission = submission
        lvAPChemicals.EditIndex = -1

        'End If
    End Sub

    Private Sub rbtnThresholdNotExceeded_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnThresholdNotExceeded.CheckedChanged
        If rbtnThresholdNotExceeded.Checked = True Then
            chkInsectPestControl.Checked = False
            chkWeedPestControl.Checked = False
            chkAnimalPestControl.Checked = False
            chkForestCanopyPestControl.Checked = False

            'chkInsectPestControl.Enabled = False
            'chkWeedPestControl.Enabled = False
            'chkAnimalPestControl.Enabled = False
            'chkForestCanopyPestControl.Enabled = False
        End If
    End Sub

    Private Sub rblEntityType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblEntityType.SelectedIndexChanged
        'If rblEntityType.SelectedValue = 4 Then
        '    chkInsectPestControl.Enabled = False
        '    chkWeedPestControl.Enabled = False
        '    chkAnimalPestControl.Enabled = False
        '    chkForestCanopyPestControl.Enabled = False
        '    rbtnThresholdNotExceeded.Enabled = False
        'Else
        '    chkInsectPestControl.Enabled = True
        '    chkWeedPestControl.Enabled = True
        '    chkAnimalPestControl.Enabled = True
        '    chkForestCanopyPestControl.Enabled = True
        '    rbtnThresholdNotExceeded.Enabled = True
        'End If


        'chkInsectPestControl.Checked = False
        'chkWeedPestControl.Checked = False
        'chkAnimalPestControl.Checked = False
        'chkForestCanopyPestControl.Checked = False
        'rbtnThresholdNotExceeded.Checked = False
    End Sub

    Private Sub chkInsectPestControl_CheckedChanged(sender As Object, e As EventArgs) Handles chkInsectPestControl.CheckedChanged, chkWeedPestControl.CheckedChanged, chkAnimalPestControl.CheckedChanged, chkForestCanopyPestControl.CheckedChanged
        Dim chkbox As CheckBox = CType(sender, CheckBox)
        If chkbox.Checked = True Then
            rbtnThresholdNotExceeded.Checked = False
        End If

    End Sub

    Private Sub lvAPChemicals_ItemEditing(sender As Object, e As ListViewEditEventArgs) Handles lvAPChemicals.ItemEditing
        lvAPChemicals.EditIndex = e.NewEditIndex
    End Sub

    Private Sub lvAPChemicals_ItemCanceling(sender As Object, e As ListViewCancelEventArgs) Handles lvAPChemicals.ItemCanceling
        lvAPChemicals.EditIndex = -1
    End Sub


End Class