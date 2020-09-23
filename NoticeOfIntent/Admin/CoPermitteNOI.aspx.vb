Imports Newtonsoft.Json

Public Class CoPermitteeNOI
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

    Private WriteOnly Property PageValidationEnabled As Boolean
        Set(value As Boolean)
            ucnaCoPermitteeInfo.ValidationEnabled = value
            rfvEffectiveDate.Enabled = value
        End Set
    End Property

    Public Overrides Sub UIPrep()
        Try
            PageValidationEnabled = chkPageValidationEnable.Checked
            hfNOISubmissionID.Value = logInVS.submissionid  ' Request.QueryString("refno")
            hfAfflid.Value = Request.QueryString("id")
            ucnaCoPermitteeInfo.StateAbv = "DE"
            If hfNOISubmissionID.Value <> String.Empty AndAlso hfNOISubmissionID.Value <> "0" Then
                Dim abal As New AdminBAL()
                Dim internalProject As NOIProjectInternal = abal.GetCoPermitteeNOIByPIId(hfNOISubmissionID.Value,hfAfflid.Value)
                InternalNOIProject = internalProject
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub MapEntitiesToFields()
        Dim bal As New SWBAL()
        Dim submission As NOIProjectInternal = InternalNOIProject


        lblNOIIDDisplay.Text = submission.PermitNumber
        lblNOIDateReceivedDisplay.Text = Convert.ToDateTime(submission.DateReceived).ToString("MM-dd-yyyy")
        'txtComments.Text = submission.


        ucnaProjectDetails.CompanyName = submission.ProjectName
        ucnaProjectDetails.Address1 = submission.ProjectAddress
        ucnaProjectDetails.City = submission.ProjectCity
        ucnaProjectDetails.StateAbv = submission.ProjectStateAbv
        ucnaProjectDetails.Zip = submission.ProjectPostalCode
        ucnaProjectDetails.County = submission.SWConstructDet.ConstructCounty
        ucnaProjectDetails.Municipality = submission.SWConstructDet.ConstructMunicipality

        Dim owner As NOIPersonOrg = submission.PersonOrg.Single(Function(e) e.AfflType = EISSqlAfflType.Owner And e.Active = "Y")


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

        If submission.PIID <> 0 Then
            Dim copermittee As NOIPersonOrg = submission.PersonOrg.Single(Function(e) e.AfflType = EISSqlAfflType.Copermittee)
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

            txtEffectiveDate.Text = Convert.ToDateTime(copermittee.StartDate).ToString("MM-dd-yyyy")
            txtComments.Text = copermittee.Comments
            ddlCoPermitteeStatus.SelectedValue = copermittee.Active
            If copermittee.EndDate IsNot Nothing Then
                txtTerminationDate.Text = Convert.ToDateTime(copermittee.EndDate).ToString("MM-dd-yyyy")
            End If
        End If





    End Sub

    Private Function MapFieldsToEntity() As NOIProjectInternal
        Dim submission As NOIProjectInternal = InternalNOIProject
        Dim user As String = logInVS.user.userid

        With submission.PersonOrg.Single(Function(e) e.AfflType = EISSqlAfflType.Copermittee)
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
            .StartDate = txtEffectiveDate.Text
            .Comments = txtComments.Text
            .Active = ddlCoPermitteeStatus.SelectedValue
            If txtTerminationDate.Text <> String.Empty Then
                .EndDate = txtTerminationDate.Text
            End If
        End With

        InternalNOIProject = submission

        Return submission

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim bal As New AdminBAL()
        Dim ns As NOIProjectInternal
        Dim save As Boolean = False
        Try
            ns = MapFieldsToEntity()
            save = bal.SaveCopermitteeNOI(ns, logInVS.user.userid)
            If save Then
                '    ErrorSummary.AddError("Copermittee saved successful!", Me)
                hfSavedSuccessfull.Value = "1"
            Else
                '    ErrorSummary.AddError("Copermittee save changes failed", Me)
                hfSavedSuccessfull.Value = "0"
            End If

        Catch ex As Exception
            ErrorSummary.AddError("Copermittee save changes failed", Me)
            ErrorSummary.AddError(ex.Message + "--" + ex.StackTrace.ToString(), Me)
            'Throw (ex)
        End Try
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        InternalNOIProject = Nothing
        logInVS.submissionid = 0
        responseRedirect("~/Home.aspx?ReportID=" & logInVS.reportid.ToString)
    End Sub


    Private Sub chkPageValidationEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkPageValidationEnable.CheckedChanged
        PageValidationEnabled = chkPageValidationEnable.Checked
    End Sub

End Class