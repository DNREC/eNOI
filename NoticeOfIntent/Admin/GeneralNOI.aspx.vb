Imports Newtonsoft.Json

Public Class GeneralNOI
    Inherits FormBasePage

    Private Sub Page_Load1(sender As Object, e As EventArgs) Handles Me.Load

        'AddHandler ucnaSiteInfo.CheckProjectName_Validated, AddressOf IsProjectNameExists
    End Sub

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ' btnMapPoint.Attributes.Add("OnClick", "javascript:showMapInDialog();")
    End Sub

    Public Overrides Sub Validate(validationGroup As String)

        If validationGroup = "ValidateNOI" Then
            If ucnaOwnerInfo.CompanyType = "P" Then
                ucnaOwnerInfo.ValidateCompanyName = False
            Else
                'ucnaOwnerInfo.rfvLastNameEnabled = False
                'ucnaOwnerInfo.rfvFirstNameEnabled = False
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

            PageValidationEnabled = chkPageValidationEnable.Checked
            hfNOISubmissionID.Value = logInVS.submissionid
            ucnaOwnerInfo.StateAbv = "DE"
            ucnaContactInfo.StateAbv = "DE"
            ucnaBilleeInfo.StateAbv = "DE"
            ucnaSiteInfo.populateddlStateAbv()
            ucnaSiteInfo.StateAbv = "DE"
            Dim internalProject As NOIProjectInternal = Nothing
            If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
                Dim abal As New AdminBAL()
                Select Case logInVS.reportid
                    Case NOIProgramType.CSSGeneralPermit
                        internalProject = abal.GetGeneralNOIByPIId(hfNOISubmissionID.Value)
                        SetupUIforCSSGeneralPermit()
                    Case NOIProgramType.ISGeneralPermit
                        internalProject = abal.GetGeneralNOIByPIIdForIS(hfNOISubmissionID.Value)
                        SetupUIforISGeneralPermit(internalProject)
                    Case NOIProgramType.PesticideGeneralPermit
                        internalProject = abal.GetGeneralNOIByPIIdForAP(hfNOISubmissionID.Value)
                        SetupUIforPesticideGeneralPermit()
                End Select
                InternalNOIProject = internalProject
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub SetupUIforCSSGeneralPermit()
        divSicCodes.Visible = False
        divNAICSCodes.Visible = False
        divOutfall.Visible = False
        divChemicals.Visible = False
        divPesticidesEntity.Visible = False
        divPesticidesThreshold.Visible = False
    End Sub
    Private Sub SetupUIforISGeneralPermit(internalProject As NOIProjectInternal)
        divChemicals.Visible = False
        ucnaSiteInfo.countymunicipalityvisible = False
        ucnaSiteInfo.textCompanyName.Attributes.Add("placeholder", "Facility Name")
        divProjectType.Visible = False
        divbmp.Visible = False
        divOtherInfo.Visible = False
        divPesticidesEntity.Visible = False
        divPesticidesThreshold.Visible = False
        If internalProject.PermitTypeCode = 91 Then
            '    lvOutfall.Enabled = False
            '    If lvOutfall.InsertItem IsNot Nothing Then
            '        Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
            '        btnMapOutfallPoint.Disabled = True
            '    End If
            lblNOIHeading.Text = "Notice of Intent (NOI) for Storm Water Discharges Associated With INDUSTRIAL ACTIVITY Under a NPDES General Permit (No Exposure)"
        Else
            lblNOIHeading.Text = "Notice of Intent (NOI) for Storm Water Discharges Associated With INDUSTRIAL ACTIVITY Under a NPDES General Permit"
        End If

        ProjectInfo.Title = "Facility Information"
        ProjectInfo1.Title = "Facility Information (continued)"
        lblSiteInfoPnlHeading.Text = "Facility Information"
        ucnaSiteInfo.CompanyNameLabel = "Facility Name *"
        ucnaSiteInfo.Address1Label = "Facility Location/Address *"
    End Sub


    Private Sub SetupUIforPesticideGeneralPermit()
        divBilleeDetails.Visible = False
        ucnaSiteInfo.textCompanyName.Attributes.Add("placeholder", "Operator Name")
        ucnaSiteInfo.countymunicipalityvisible = False
        ucnaSiteInfo.allowDEstateonly = False
        ucnaSiteInfo.populateddlStateAbv()
        divProjectType.Visible = False
        divTaxParcel.Visible = False
        divLocDet.Visible = False
        divbmp.Visible = False
        divOtherInfo.Visible = False
        divSicCodes.Visible = False
        divNAICSCodes.Visible = False
        divOutfall.Visible = False

        ProjectInfo.Title = "Operator Information"
        ProjectInfo1.Title = "Operator Information (continued)"
        lblSiteInfoPnlHeading.Text = "Operator Information"
        ucnaSiteInfo.CompanyNameLabel = "Operator Name *"
        ucnaSiteInfo.Address1Label = "Operator Location/Address *"

        lblNOIHeading.Text = "Notice of Intent (NOI) for Aquatic Pesticides"
        rblEntityType.DataBind()
    End Sub

    Public Overrides Sub MapEntitiesToFields()
        If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
            Dim internalProj As NOIProjectInternal = InternalNOIProject

            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                MapEntitiesToFieldsForCSSGeneralPermit(internalProj)
            ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                MapEntitiesToFieldsForISGeneralPermit(internalProj)
            ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                MapEntitiesToFieldsForAPGeneralPermit(internalProj)
            End If
        Else
            If logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                If lvOutfall.InsertItem IsNot Nothing Then
                    Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
                    btnMapOutfallPoint.Disabled = True
                End If
            End If
        End If

    End Sub

    Private Sub MapEntitiesToFieldsForCSSGeneralPermit(internalProj As NOIProjectInternal)
        ucnaSiteInfo.CompanyName = internalProj.ProjectName
        ucnaSiteInfo.Address1 = internalProj.ProjectAddress
        ucnaSiteInfo.City = internalProj.ProjectCity
        ucnaSiteInfo.StateAbv = internalProj.ProjectStateAbv
        ucnaSiteInfo.Zip = internalProj.ProjectPostalCode
        ucnaSiteInfo.County = internalProj.SWConstructDet.ConstructCounty
        ucnaSiteInfo.Municipality = internalProj.SWConstructDet.ConstructMunicipality

        hfSiteInfo.Value = internalProj.ProjectAddress + "," + internalProj.ProjectCity + "," + internalProj.ProjectStateAbv + "," + internalProj.ProjectPostalCode

        ddlProjectType.DataBind()
        ddlProjectType.SelectedValue = internalProj.SWConstructDet.ProjectTypeID
        If internalProj.SWConstructDet.ProjectTypeID = 10 Then
            txtProjectTypeOther.Text = internalProj.SWConstructDet.OtherProjectType
            txtProjectTypeOther.Enabled = True
        Else
            txtProjectTypeOther.Enabled = False
        End If
        txtLatitude.Text = common.NothingToString(internalProj.Latitude)
        txtLongitude.Text = common.NothingToString(internalProj.Longitude)
        hfX.Value = common.NothingToString(internalProj.X)
        hfY.Value = common.NothingToString(internalProj.Y)
        txtWatershed.Text = common.NothingToString(internalProj.Watershed)
        hfWaterShedCode.Value = common.NothingToString(internalProj.HUC12)
        Select Case internalProj.SWConstructDet.IsSWPPPPrepared
            Case "N"
                rblSWPPPYesNo.SelectedValue = "No"
                    'rbNo.Checked = True
            Case "Y"
                rblSWPPPYesNo.SelectedValue = "Yes"
                'rbYes.Checked = True
        End Select


        If internalProj.DelegatedAgencyID IsNot Nothing Then
            ddlPlanApprovalAgency.SelectedValue = internalProj.DelegatedAgencyID
        End If

        txtTotalAreaOfSite.Text = internalProj.SWConstructDet.TotalLandArea
        txtAreaOfDisturbed.Text = internalProj.SWConstructDet.EstimatedArea
        txtConstructStartDate.Text = Convert.ToDateTime(internalProj.SWConstructDet.ConstructStartDate).ToString("MM-dd-yyyy")
        txtConstructCompleteDate.Text = Convert.ToDateTime(internalProj.SWConstructDet.ConstructEndDate).ToString("MM-dd-yyyy")
        txtComments.Text = internalProj.Comments


        Dim owner As NOIPersonOrg = internalProj.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Owner)

        If owner IsNot Nothing Then
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
        End If


        Dim contact As NOIPersonOrg = internalProj.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Contact)
        If contact IsNot Nothing Then
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
        End If

        Dim billee As NOIPersonOrg = internalProj.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Billee)

        If billee IsNot Nothing Then
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
        End If
    End Sub

    Private Sub MapEntitiesToFieldsForISGeneralPermit(internalProj As NOIProjectInternal)

        ucnaSiteInfo.CompanyName = internalProj.ProjectName
        ucnaSiteInfo.Address1 = internalProj.ProjectAddress
        ucnaSiteInfo.City = internalProj.ProjectCity
        ucnaSiteInfo.StateAbv = internalProj.ProjectStateAbv
        ucnaSiteInfo.Zip = internalProj.ProjectPostalCode
        hfSiteInfo.Value = internalProj.ProjectAddress + "," + internalProj.ProjectCity + "," + internalProj.ProjectStateAbv + "," + internalProj.ProjectPostalCode

        txtLatitude.Text = common.NothingToString(internalProj.Latitude)
        txtLongitude.Text = common.NothingToString(internalProj.Longitude)
        hfX.Value = common.NothingToString(internalProj.X)
        hfY.Value = common.NothingToString(internalProj.Y)
        txtWatershed.Text = common.NothingToString(internalProj.Watershed)
        hfWaterShedCode.Value = common.NothingToString(internalProj.HUC12)

        txtComments.Text = internalProj.Comments


        Dim owner As NOIPersonOrg = internalProj.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Owner)

        If owner IsNot Nothing Then
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
        End If



        Dim contact As NOIPersonOrg = internalProj.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Contact)
        If contact IsNot Nothing Then

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
        End If
        Dim billee As NOIPersonOrg = internalProj.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Billee)

        If billee IsNot Nothing Then


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
        End If
    End Sub

    Private Sub MapEntitiesToFieldsForAPGeneralPermit(internalProj As NOIProjectInternal)
        ucnaSiteInfo.CompanyName = internalProj.ProjectName
        ucnaSiteInfo.Address1 = internalProj.ProjectAddress
        ucnaSiteInfo.City = internalProj.ProjectCity
        ucnaSiteInfo.StateAbv = internalProj.ProjectStateAbv
        ucnaSiteInfo.Zip = internalProj.ProjectPostalCode


        hfSiteInfo.Value = internalProj.ProjectAddress + "," + internalProj.ProjectCity + "," + internalProj.ProjectStateAbv + "," + internalProj.ProjectPostalCode

        rblEntityType.SelectedValue = internalProj.NOIAPAnnualThresholdDet.EntityTypeID

        chkInsectPestControl.Checked = IIf(internalProj.NOIAPAnnualThresholdDet.InsectPestControl = "Y", True, False)
        chkWeedPestControl.Checked = IIf(internalProj.NOIAPAnnualThresholdDet.WeedPestControl = "Y", True, False)
        chkAnimalPestControl.Checked = IIf(internalProj.NOIAPAnnualThresholdDet.AnimalPestControl = "Y", True, False)
        chkForestCanopyPestControl.Checked = IIf(internalProj.NOIAPAnnualThresholdDet.ForestCanopyPestControl = "Y", True, False)
        rbtnThresholdNotExceeded.Checked = IIf(internalProj.NOIAPAnnualThresholdDet.NotExceededThreshold = "Y", True, False)
        txtCommercialApplicatorID.Text = internalProj.NOIAPAnnualThresholdDet.CommercialApplicatorID

        txtComments.Text = internalProj.Comments


        Dim owner As NOIPersonOrg = internalProj.PersonOrg.Single(Function(e) e.AfflType = EISSqlAfflType.Owner)


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



        Dim contact As NOIPersonOrg = internalProj.PersonOrg.Single(Function(e) e.AfflType = EISSqlAfflType.Contact)

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

    Private Function MapFieldsToModifiedEntityForCSSGeneralPermit(ByVal mainSubmission As NOIProjectInternal) As NOIProjectInternal
        mainSubmission.Comments = txtComments.Text
        mainSubmission.ProjectName = ucnaSiteInfo.CompanyName
        mainSubmission.ProjectAddress = ucnaSiteInfo.Address1
        mainSubmission.ProjectCity = ucnaSiteInfo.City
        mainSubmission.ProjectStateAbv = ucnaSiteInfo.StateAbv
        mainSubmission.ProjectPostalCode = ucnaSiteInfo.Zip
        mainSubmission.SWConstructDet.ConstructCounty = ucnaSiteInfo.County
        mainSubmission.SWConstructDet.ConstructMunicipality = ucnaSiteInfo.Municipality
        mainSubmission.SWConstructDet.ProjectTypeID = ddlProjectType.SelectedValue
        If ddlProjectType.SelectedValue = 10 Then
            mainSubmission.SWConstructDet.OtherProjectType = txtProjectTypeOther.Text
        Else
            mainSubmission.SWConstructDet.OtherProjectType = Nothing
        End If
        mainSubmission.Latitude = txtLatitude.Text
        mainSubmission.Longitude = txtLongitude.Text
        If hfX.Value <> Nothing OrElse hfX.Value <> String.Empty Then
            mainSubmission.X = hfX.Value
        End If
        If hfY.Value <> Nothing OrElse hfY.Value <> String.Empty Then
            mainSubmission.Y = hfY.Value
        End If
        mainSubmission.Watershed = txtWatershed.Text
        If hfWaterShedCode.Value <> String.Empty Then
            mainSubmission.HUC12 = hfWaterShedCode.Value
        End If

        If rblSWPPPYesNo.SelectedValue = "No" Then
            mainSubmission.SWConstructDet.IsSWPPPPrepared = "N"
        ElseIf rblSWPPPYesNo.SelectedValue = "Yes" Then
            mainSubmission.SWConstructDet.IsSWPPPPrepared = "Y"
        End If

        mainSubmission.DelegatedAgencyID = ddlPlanApprovalAgency.SelectedValue
        mainSubmission.SWConstructDet.TotalLandArea = txtTotalAreaOfSite.Text
        mainSubmission.SWConstructDet.EstimatedArea = txtAreaOfDisturbed.Text
        mainSubmission.SWConstructDet.ConstructStartDate = txtConstructStartDate.Text
        mainSubmission.SWConstructDet.ConstructEndDate = txtConstructCompleteDate.Text
        'mainSubmission.NOIProject.LastChgBy = user


        SetOwnerInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Owner))
        SetContactInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Contact))
        SetBilleeInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Billee))


        InternalNOIProject = mainSubmission

        Return mainSubmission
    End Function

    Private Function MapFieldsToModifiedEntityForISGeneralPermit(ByVal mainSubmission As NOIProjectInternal) As NOIProjectInternal
        mainSubmission.Comments = txtComments.Text
        mainSubmission.ProjectName = ucnaSiteInfo.CompanyName
        mainSubmission.ProjectAddress = ucnaSiteInfo.Address1
        mainSubmission.ProjectCity = ucnaSiteInfo.City
        mainSubmission.ProjectStateAbv = ucnaSiteInfo.StateAbv
        mainSubmission.ProjectPostalCode = ucnaSiteInfo.Zip
        mainSubmission.Latitude = txtLatitude.Text
        mainSubmission.Longitude = txtLongitude.Text
        If hfX.Value <> Nothing OrElse hfX.Value <> String.Empty Then
            mainSubmission.X = hfX.Value
        End If
        If hfY.Value <> Nothing OrElse hfY.Value <> String.Empty Then
            mainSubmission.Y = hfY.Value
        End If
        mainSubmission.Watershed = txtWatershed.Text
        If hfWaterShedCode.Value <> String.Empty Then
            mainSubmission.HUC12 = hfWaterShedCode.Value
        End If


        SetOwnerInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Owner))
        SetContactInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Contact))
        SetBilleeInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Billee))

        InternalNOIProject = mainSubmission

        Return mainSubmission
    End Function

    Private Function MapFieldsToModifiedEntityForAPGeneralPermit(ByVal mainSubmission As NOIProjectInternal) As NOIProjectInternal
        mainSubmission.Comments = txtComments.Text
        mainSubmission.ProjectName = ucnaSiteInfo.CompanyName
        mainSubmission.ProjectAddress = ucnaSiteInfo.Address1
        mainSubmission.ProjectCity = ucnaSiteInfo.City
        mainSubmission.ProjectStateAbv = ucnaSiteInfo.StateAbv
        mainSubmission.ProjectPostalCode = ucnaSiteInfo.Zip

        mainSubmission.NOIAPAnnualThresholdDet.EntityTypeID = rblEntityType.SelectedValue
        mainSubmission.NOIAPAnnualThresholdDet.InsectPestControl = IIf(chkInsectPestControl.Checked, "Y", "N")
        mainSubmission.NOIAPAnnualThresholdDet.WeedPestControl = IIf(chkWeedPestControl.Checked, "Y", "N")
        mainSubmission.NOIAPAnnualThresholdDet.AnimalPestControl = IIf(chkAnimalPestControl.Checked, "Y", "N")
        mainSubmission.NOIAPAnnualThresholdDet.ForestCanopyPestControl = IIf(chkForestCanopyPestControl.Checked, "Y", "N")
        mainSubmission.NOIAPAnnualThresholdDet.NotExceededThreshold = IIf(rbtnThresholdNotExceeded.Checked, "Y", "N")
        mainSubmission.NOIAPAnnualThresholdDet.CommercialApplicatorID = txtCommercialApplicatorID.Text


        SetOwnerInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Owner))
        SetContactInfo(mainSubmission.PersonOrg.SingleOrDefault(Function(e) e.AfflType = EISSqlAfflType.Contact))
        InternalNOIProject = mainSubmission

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
                    lvOutfall.Enabled = value
                    If lvOutfall.InsertItem IsNot Nothing Then
                        Dim btnMapOutfallPoint As HtmlInputButton = CType(lvOutfall.InsertItem.FindControl("btnMapOutfallPoint"), HtmlInputButton)
                        btnMapOutfallPoint.Disabled = Not value
                    End If
                Case NOIProgramType.PesticideGeneralPermit
                    rblEntityType.Enabled = value
                    chkInsectPestControl.Enabled = value
                    chkWeedPestControl.Enabled = value
                    chkAnimalPestControl.Enabled = value
                    chkForestCanopyPestControl.Enabled = value
                    rbtnThresholdNotExceeded.Enabled = value
                    lvAPChemicals.Enabled = value
                    txtCommercialApplicatorID.Enabled = value
            End Select


        End Set
    End Property

    Private WriteOnly Property PageValidationEnabled As Boolean
        Set(value As Boolean)
            ucnaOwnerInfo.ValidationEnabled = value
            ucnaContactInfo.ValidationEnabled = value
            ucnaSiteInfo.ValidationEnabled = value
            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    ucnaBilleeInfo.ValidationEnabled = value
                    cvTaxparcel.Enabled = value
                    rfvLatitude.Enabled = value
                    rfvLongitude.Enabled = value
                    rfvWatershed.Enabled = value
                    rfvPlanApprovalAgency.Enabled = value
                    rfvTotalAreaOfSite.Enabled = value
                    rfvAreaOfDisturbed.Enabled = value
                    rfvConstructStartDate.Enabled = value
                    rfvConstructCompleteDate.Enabled = value
                    cvConstructCompleteDate.Enabled = value
                Case NOIProgramType.ISGeneralPermit
                    ucnaBilleeInfo.ValidationEnabled = value
                    cvTaxparcel.Enabled = value
                    rfvLatitude.Enabled = value
                    rfvLongitude.Enabled = value
                    rfvWatershed.Enabled = value
            End Select
        End Set
    End Property

    Public Function GetTaxParcelLst() As List(Of NOITaxParcel)
        Dim lstNOITaxParcels As List(Of NOITaxParcel)
        Dim parcels As String = String.Empty
        lstNOITaxParcels = InternalNOIProject.TaxParcel.Where(Function(a) a.IsDeleted = False).ToList()

        For Each tp As NOITaxParcel In lstNOITaxParcels
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
        Dim bal As New SWBAL
        Return bal.GetProjectType()
    End Function

    Public Function GetProjectSWBMPLst() As List(Of SWConstructBMP)
        Dim lstProjectSWBMPs As List(Of SWConstructBMP)
        lstProjectSWBMPs = InternalNOIProject.SWConstructBMPDet.Where(Function(a) a.IsDeleted = False).ToList()
        Return lstProjectSWBMPs
    End Function

    Public Function GetSWBMPLst() As List(Of SWBMPlst)
        Dim bal As New SWBAL
        Return bal.GetSWBMPList()
    End Function

    Public Function GetPermitStatusCodeLst() As List(Of PermitStatusCodeLst)
        Dim bal As New AdminBAL
        Return bal.GetPermitStatusCodeLst()
    End Function


    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvStatus_GetData() As List(Of NoticeOfIntent.PermitStatus)
        Dim PermitStatuses As List(Of PermitStatus)
        PermitStatuses = InternalNOIProject.PermitStatuses.Where(Function(a) a.IsDeleted = False).ToList()
        Return PermitStatuses
    End Function


    Public Function GetPlanApprovalAgency() As List(Of PlanApprovalAgency)
        Dim bal As New SWBAL
        Return bal.GetPlanApprovalAgencyList()
    End Function

    Public Function GetTaxKentHundred() As IList(Of TaxKentHundred)
        Dim bal As New SWBAL
        Return bal.GetTaxKentHundred()
    End Function

    Public Function GetTaxKentTowns() As IList(Of TaxKentTown)
        Dim bal As New SWBAL
        Return bal.GetTaxKentTowns()
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


        Dim ns As NOIProjectInternal

        ResetScrollPosition()

        If wzNOI.ActiveStepIndex = 1 Then

            If hfNOISubmissionID.Value = String.Empty OrElse hfNOISubmissionID.Value = "0" Then
                ucnaSiteInfo.StateAbv = "DE"
            Else
                ns = InternalNOIProject
                ucnaSiteInfo.StateAbv = ns.ProjectStateAbv
            End If
        ElseIf wzNOI.ActiveStepIndex = 2 Then
            If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
                ns = InternalNOIProject
                If ns.DelegatedAgencyID IsNot Nothing Then
                    ddlPlanApprovalAgency.SelectedValue = ns.DelegatedAgencyID
                End If
            End If
        End If


    End Sub

    Private Sub wzNOI_CancelButtonClick(sender As Object, e As EventArgs) Handles wzNOI.CancelButtonClick
        InternalNOIProject = Nothing
        logInVS.submissionid = 0
        responseRedirect("~/Home.aspx?ReportID=" & logInVS.reportid)
    End Sub

    Private Sub wzNOI_FinishButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles wzNOI.FinishButtonClick


        Dim str As String = String.Empty
        Dim bal As New AdminBAL()
        Dim ns As NOIProjectInternal = InternalNOIProject
        Dim save As Boolean = False
        Try
            Page.Validate("ValidateNOI")
            If Page.IsValid = True Then

                Select Case logInVS.reportid
                    Case NOIProgramType.CSSGeneralPermit
                        ns = MapFieldsToModifiedEntityForCSSGeneralPermit(ns)
                        save = bal.SaveGeneralNOI(ns, logInVS.user.userid)
                    Case NOIProgramType.ISGeneralPermit
                        ns = MapFieldsToModifiedEntityForISGeneralPermit(ns)
                        save = bal.SaveISGeneralNOI(ns, logInVS.user.userid)
                    Case NOIProgramType.PesticideGeneralPermit
                        ns = MapFieldsToModifiedEntityForAPGeneralPermit(ns)
                        save = bal.SaveAPGeneralNOI(ns, logInVS.user.userid)
                End Select

                If save Then
                    hfSavedSuccessfull.Value = "1"
                    '    ErrorSummary.AddError("General NOI saved successful!", Me)
                Else
                    '    ErrorSummary.AddError("General NOI save changes failed", Me)
                    hfSavedSuccessfull.Value = "0"
                End If
            End If
        Catch ex As Exception
            ErrorSummary.AddError("General NOI save changes failed", Me)
            ErrorSummary.AddError(ex.Message + "--" + ex.StackTrace.ToString(), Me)
            'Throw (ex)
        End Try


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

    Private Sub SetOwnerInfo(ByRef Owner As NOIPersonOrg)
        If Owner IsNot Nothing Then
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
        End If
    End Sub

    Private Sub SetContactInfo(ByRef contact As NOIPersonOrg)
        If contact IsNot Nothing Then
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
        End If
    End Sub

    Private Sub SetBilleeInfo(ByRef billee As NOIPersonOrg)
        If billee IsNot Nothing Then
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
        End If
    End Sub

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Sub lvTaxparcel_InsertItem()
        Dim item = New NoticeOfIntent.NOITaxParcel()
        TryUpdateModel(item)
        Try
            If ModelState.IsValid Then
                Dim submission As NOIProjectInternal
                submission = InternalNOIProject
                submission.TaxParcel.Add(item)
                'If hfTaxParcel.Value = String.Empty Then
                '    hfTaxParcel.Value = item.TaxParcelNumber
                'Else
                '    hfTaxParcel.Value = hfTaxParcel.Value + "," + item.TaxParcelNumber
                'End If
                InternalNOIProject = submission
                'TaxparcelLst = noiTaxparcels
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvTaxparcel_DeleteItem(ByVal ProjectTaxParcelID As Integer, ByVal TaxParcelNumber As String)


        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item As NOITaxParcel
        If ProjectTaxParcelID <> 0 Then
            item = submission.TaxParcel.SingleOrDefault(Function(e) e.ProjectTaxParcelID = ProjectTaxParcelID)
            submission.TaxParcel.Remove(item)
            item.IsDeleted = True
            submission.TaxParcel.Add(item)
        Else
            item = submission.TaxParcel.SingleOrDefault(Function(e) e.TaxParcelNumber = TaxParcelNumber)
            submission.TaxParcel.Remove(item)
        End If

        InternalNOIProject = submission
    End Sub

    Public Sub lvSWBMP_InsertItem()
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item = New SWConstructBMP()
        TryUpdateModel(item)
        If Not submission.SWConstructBMPDet.Where(Function(a) a.SWBMPID = item.SWBMPID And a.IsDeleted = False).Count > 0 Then
            Dim bal As New SWBAL
            item.SWBMPName = bal.GetSWBMPList().Single(Function(a) a.SWBMPID = item.SWBMPID).SWBMP.ToString()
            item.PermitID = submission.PermitID
            If ModelState.IsValid Then
                ' Save changes here
                submission.SWConstructBMPDet.Add(item) ' = noiTaxparcels

                InternalNOIProject = submission

            End If
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvSWBMP_DeleteItem(ByVal SWBMPID As Integer)
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item As SWConstructBMP
        item = submission.SWConstructBMPDet.SingleOrDefault(Function(e) e.SWBMPID = SWBMPID)
        If item.PermitID <> 0 Then
            'submission.SWConstructBMPDet.Remove(item)
            item.IsDeleted = True
            'submission.SWConstructBMPDet.Add(item)
        Else
            submission.SWConstructBMPDet.Remove(item)
        End If
        InternalNOIProject = submission

    End Sub

    Private Sub cvTaxparcel_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvTaxparcel.ServerValidate

        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        If submission.TaxParcel.Where(Function(e) e.IsDeleted = False).Count <= 0 Then
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


    Private Sub chkPageValidationEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkPageValidationEnable.CheckedChanged
        PageValidationEnabled = chkPageValidationEnable.Checked
        'responseRedirect("~/Admin/GeneralNOI.aspx")
    End Sub

    Public Sub lvStatus_InsertItem()
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item = New NoticeOfIntent.PermitStatus()
        TryUpdateModel(item)
        If Not submission.PermitStatuses.Where(Function(a) a.PEventCode = item.PEventCode And a.IsDeleted = False).Count > 0 Then
            Dim bal As New AdminBAL
            item.PEvent = bal.GetPermitStatusCodeLst().Single(Function(a) a.PEventCode = item.PEventCode).PEvent.ToString()
            item.PermitID = submission.PermitID
            If ModelState.IsValid Then
                ' Save changes here
                submission.PermitStatuses.Add(item)
                InternalNOIProject = submission
            End If
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvStatus_DeleteItem(ByVal PEventsID As Integer)
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item As PermitStatus
        item = submission.PermitStatuses.SingleOrDefault(Function(e) e.PEventsID = PEventsID)
        If item.PEventsID <> 0 Then
            submission.PermitStatuses.Remove(item)
            item.IsDeleted = True
            submission.PermitStatuses.Add(item)
        Else
            submission.PermitStatuses.Remove(item)
        End If
        InternalNOIProject = submission
    End Sub

    Protected Sub sideBarList_ItemDataBound(sender As Object, e As ListViewItemEventArgs)
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim btnlink As LinkButton = CType(e.Item.FindControl("SideBarButton"), LinkButton)
            If e.Item.DataItemIndex = wzNOI.ActiveStepIndex Then
                btnlink.Font.Bold = True

            End If
        End If
    End Sub

    Public Function GetSubmissionSICCodes() As List(Of NOIPiSIC)
        Dim lstsic As List(Of NOIPiSIC)
        lstsic = InternalNOIProject.SICCodes.Where(Function(a) a.IsDeleted = False).ToList()
        Return lstsic
    End Function

    Private Function IsSICDuplicated(submission As NOIProjectInternal, item As NOIPiSIC) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOIPiSIC In submission.SICCodes
            If i.SIC = item.SIC Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function

    Public Sub lvSIC_InsertItem()
        If lvSIC.Items.Count < 4 Then
            Dim item = New NOIPiSIC()
            TryUpdateModel(item)
            If ModelState.IsValid Then
                ' Save changes here
                Dim submission As NOIProjectInternal
                submission = InternalNOIProject
                If Not IsSICDuplicated(submission, item) Then
                    submission.SICCodes.Add(item)
                    InternalNOIProject = submission
                End If

            End If
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvSIC_DeleteItem(ByVal PiID As Integer, ByVal SICCode As String)
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item As NOIPiSIC
        If PiID <> 0 Then
            item = submission.SICCodes.SingleOrDefault(Function(e) e.SIC = SICCode)
            item.IsDeleted = True
        Else
            item = submission.SICCodes.SingleOrDefault(Function(e) e.SIC = SICCode)
            submission.SICCodes.Remove(item)
        End If
        InternalNOIProject = submission
    End Sub

    Public Function GetSubmissionNAICSCodes() As List(Of NOIPiNAICS)
        Dim lstNAICS As List(Of NOIPiNAICS)
        lstNAICS = InternalNOIProject.NAICSCodes.Where(Function(e) e.IsDeleted = False).ToList()
        Return lstNAICS
    End Function

    Private Function IsNAICSDuplicated(submission As NOIProjectInternal, item As NOIPiNAICS) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOIPiNAICS In submission.NAICSCodes
            If i.NAICS = item.NAICS Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function
    Public Sub lvNAICS_InsertItem()
        If lvNAICS.Items.Count < 4 Then
            Dim item = New NoticeOfIntent.NOIPiNAICS()
            TryUpdateModel(item)
            If ModelState.IsValid Then
                ' Save changes here
                Dim submission As NOIProjectInternal
                submission = InternalNOIProject
                If Not IsNAICSDuplicated(submission, item) Then
                    submission.NAICSCodes.Add(item)
                    InternalNOIProject = submission
                End If

            End If
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvNAICS_DeleteItem(ByVal PiID As Integer, ByVal NAICSCode As String)
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item As NOIPiNAICS
        If PiID <> 0 Then
            item = submission.NAICSCodes.SingleOrDefault(Function(e) e.NAICS = NAICSCode)
            item.IsDeleted = True
        Else
            item = submission.NAICSCodes.SingleOrDefault(Function(e) e.NAICS = NAICSCode)
            submission.NAICSCodes.Remove(item)
        End If
        InternalNOIProject = submission
    End Sub

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvOutfall_GetData() As List(Of NoticeOfIntent.NOIOutfall)
        Dim lstUnits As List(Of NOIOutfall)
        lstUnits = InternalNOIProject.Outfalls.Where(Function(e) e.IsDeleted = False).ToList()
        Dim outfalls As String = ""
        For Each outfall As NOIOutfall In lstUnits
            outfalls = outfalls + outfall.UnitName + ":" + outfall.Latitude.ToString + ":" + outfall.Longitude.ToString + "::"
        Next
        If outfalls = String.Empty Then
            hfOutfalllist.Value = String.Empty
        Else
            hfOutfalllist.Value = Left(outfalls, outfalls.Length - 2)
        End If

        Return lstUnits
    End Function
    Private Function IsOutfallDuplicated(submission As NOIProjectInternal, item As NOIOutfall) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOIOutfall In submission.Outfalls
            If i.UnitName = item.UnitName Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function
    Public Sub lvOutfall_InsertItem()
        Dim item = New NoticeOfIntent.NOIOutfall()
        TryUpdateModel(item)
        If ModelState.IsValid Then
            ' Save changes here
            Dim submission As NOIProjectInternal
            submission = InternalNOIProject
            If Not IsOutfallDuplicated(submission, item) Then
                submission.Outfalls.Add(item)
            End If
            InternalNOIProject = submission
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvOutfall_DeleteItem(ByVal UnitID As Integer, ByVal UnitName As String)
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item As NOIOutfall
        If UnitID <> 0 Then
            item = submission.Outfalls.SingleOrDefault(Function(e) e.UnitID = UnitID)
            item.IsDeleted = True
        Else
            item = submission.Outfalls.SingleOrDefault(Function(e) e.UnitName = UnitName)
            submission.Outfalls.Remove(item)
        End If
        InternalNOIProject = submission
    End Sub
    Public Function rblEntityType_GetData() As List(Of NOIEntityType)
        Dim bal As New APBAL()
        Return bal.GetAPEntityType()
    End Function

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvAPChemicals_GetData() As List(Of NoticeOfIntent.NOIAPChemicals)
        Return InternalNOIProject.NOIAPChemicalsLst.Where(Function(e) e.IsDeleted = False).ToList()
    End Function

    Private Function IsChemicalDuplicated(submission As NOIProjectInternal, item As NOIAPChemicals) As Boolean
        Dim duplicated As Boolean = False
        For Each i As NOIAPChemicals In submission.NOIAPChemicalsLst
            If i.Ingredient = item.Ingredient Then
                duplicated = True
                Exit For
            End If
        Next
        Return duplicated
    End Function


    Public Sub lvAPChemicals_InsertItem()
        Dim item = New NoticeOfIntent.NOIAPChemicals()
        TryUpdateModel(item)
        If ModelState.IsValid Then
            ' Save changes here
            Dim submission As NOIProjectInternal
            submission = InternalNOIProject
            If Not IsChemicalDuplicated(submission, item) Then
                item.PermitID = submission.PermitID
                submission.NOIAPChemicalsLst.Add(item)
            End If
            InternalNOIProject = submission

        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvAPChemicals_DeleteItem(ByVal APChemicalID As Integer, ByVal Ingredient As String)
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim item As NOIAPChemicals
        If APChemicalID <> 0 Then
            item = submission.NOIAPChemicalsLst.SingleOrDefault(Function(e) e.APChemicalID = APChemicalID)
            item.IsDeleted = True
        Else
            item = submission.NOIAPChemicalsLst.SingleOrDefault(Function(e) e.Ingredient = Ingredient)
            submission.NOIAPChemicalsLst.Remove(item)
        End If
        InternalNOIProject = submission
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvAPChemicals_UpdateItem(ByVal apc As NOIAPChemicals)
        Dim item As NoticeOfIntent.NOIAPChemicals = Nothing
        Dim submission As NOIProjectInternal
        submission = InternalNOIProject
        Dim bal As New APBAL()
        apc.PermitID = submission.PermitID
        If apc.APChemicalID <> 0 Then
            item = submission.NOIAPChemicalsLst.SingleOrDefault(Function(e) e.APChemicalID = apc.APChemicalID)
            If item.Equals(apc) = False Then
                submission.NOIAPChemicalsLst.Remove(item)
                apc.IsModified = True
                submission.NOIAPChemicalsLst.Add(apc)
            End If
        Else
            item = submission.NOIAPChemicalsLst.SingleOrDefault(Function(e) e.Ingredient = apc.Ingredient)
            If item.Equals(apc) = False Then
                submission.NOIAPChemicalsLst.Remove(item)
                submission.NOIAPChemicalsLst.Add(apc)
            End If
        End If
        InternalNOIProject = submission
        lvAPChemicals.EditIndex = -1
    End Sub


    Public Function GetPesticidePatterns() As List(Of NOIPesticidePattern)
        Dim bal As New APBAL
        Return bal.GetPesticidePatterns()
    End Function

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