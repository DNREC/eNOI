Imports System.Web.Services
Imports Microsoft.Reporting.WebForms

Public Class NOIAgreement
    Inherits FormBasePage



    Public Overrides Sub UIPrep()

    End Sub

    Public Overrides Sub MapEntitiesToFields()
        Dim submission As NOISubmission = IndNOISubmission

        If isExternal = True Then

            hfCurrentUseremail.Value = logInVS.user.useremail
            Dim owner As NOISubmissionPersonOrg
            Dim copermittee As NOISubmissionPersonOrg


            Select Case logInVS.submissiontype
                Case NOISubmissionType.GeneralNOIPermit, NOISubmissionType.TerminateNOI, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                    owner = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)
                    If submission.NOISigningEmailAddress.Count > 0 Then
                        txtOrigPermitteeEmail.Text = submission.NOISigningEmailAddress.OrderBy(Function(a) a.NOISignEmailAddressID).First().EmailAddress
                        If txtOrigPermitteeEmail.Text = owner.EmailAddress Then
                            chkOrigPermitteeEmail.Checked = True
                        Else
                            chkOrigPermitteeEmail.Checked = False
                        End If
                    End If
                Case NOISubmissionType.TerminateCoPermittee
                    copermittee = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)
                    If submission.NOISigningEmailAddress.Count > 0 Then
                        txtOrigPermitteeEmail.Text = submission.NOISigningEmailAddress.OrderBy(Function(a) a.NOISignEmailAddressID).First().EmailAddress
                        If txtOrigPermitteeEmail.Text = copermittee.EmailAddress Then
                            chkOrigPermitteeEmail.Checked = True
                        Else
                            chkOrigPermitteeEmail.Checked = False
                        End If
                    End If
                Case Else
                    lblCertHeading.Text = "Certification of Co-Permittee"
                    divPermittee2.Visible = True
                    owner = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)
                    copermittee = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)
                    If submission.NOISigningEmailAddress.Count > 0 Then
                        Dim origPermitteeEmail As String = String.Empty
                        origPermitteeEmail = submission.NOISigningEmailAddress.OrderBy(Function(a) a.NOISignEmailAddressID).First().EmailAddress
                        If origPermitteeEmail <> owner.EmailAddress Then
                            origPermitteeEmail = owner.EmailAddress
                        End If
                        txtOrigPermitteeEmail.Text = origPermitteeEmail
                        If Len(txtOrigPermitteeEmail.Text) > 0 Then
                            txtOrigPermitteeEmail.Enabled = False
                            chkOrigPermitteeEmail.Enabled = False
                        End If
                        txtCoPermitteeEmail.Text = submission.NOISigningEmailAddress.OrderByDescending(Function(a) a.NOISignEmailAddressID).First().EmailAddress
                        If txtCoPermitteeEmail.Text = copermittee.EmailAddress Then
                            chkOrigPermitteeEmail.Checked = True
                        Else
                            chkOrigPermitteeEmail.Checked = False
                        End If
                    Else
                        txtOrigPermitteeEmail.Text = owner.EmailAddress
                        If Len(owner.EmailAddress) > 0 Then
                            txtOrigPermitteeEmail.Enabled = False
                            chkOrigPermitteeEmail.Enabled = False
                        Else
                            txtOrigPermitteeEmail.Enabled = True
                            chkOrigPermitteeEmail.Enabled = False
                        End If
                        If hfCurrentUseremail.Value = owner.EmailAddress Then
                                chkCoPermittee.Enabled = False
                            End If
                        End If
            End Select

        Else
            divEmailAddressOfParties.Visible = False
        End If
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "dsAgreementcert"
        Dim bal As New NOIBAL()
        rds.Value = bal.GetAgreementTextByProgSubType(logInVS.progsubmisssiontype)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(rds)
        ReportViewer1.LocalReport.Refresh()
        'Select Case submission.CertificationAgreed
        '    Case "N"
        '        chkAgree.Checked = False
        '    Case "Y"
        '        chkAgree.Checked = True
        '        btnSubmit.Enabled = True
        'End Select

    End Sub

    Private Function MapFieldsToEntities() As NoticeOfIntent.NOISubmission
        Dim submission As NOISubmission = IndNOISubmission
        submission.EntityState = EntityState.Modified
        If isExternal = True Then
            If submission.NOISigningEmailAddress.Count = 0 Then
                If txtOrigPermitteeEmail.Text <> String.Empty Then
                    Dim OrigPermitteeemail As New NOISigningEmailAddress
                    OrigPermitteeemail.SubmissionID = submission.SubmissionID
                    OrigPermitteeemail.EmailAddress = txtOrigPermitteeEmail.Text
                    OrigPermitteeemail.EntityState = EntityState.Added
                    submission.NOISigningEmailAddress.Add(OrigPermitteeemail)
                End If

                If divPermittee2.Visible = True AndAlso txtCoPermitteeEmail.Text <> String.Empty Then
                    Dim CoPermitteeemail As New NOISigningEmailAddress
                    CoPermitteeemail.SubmissionID = submission.SubmissionID
                    CoPermitteeemail.EmailAddress = txtCoPermitteeEmail.Text
                    CoPermitteeemail.EntityState = EntityState.Added
                    submission.NOISigningEmailAddress.Add(CoPermitteeemail)
                End If
            Else
                If txtOrigPermitteeEmail.Text <> String.Empty Then
                    With submission.NOISigningEmailAddress.OrderBy(Function(a) a.NOISignEmailAddressID).First()
                        .SubmissionID = submission.SubmissionID
                        .EmailAddress = txtOrigPermitteeEmail.Text
                        .EntityState = EntityState.Modified
                    End With
                End If

                If divPermittee2.Visible = True AndAlso txtCoPermitteeEmail.Text <> String.Empty Then
                    With submission.NOISigningEmailAddress.OrderByDescending(Function(a) a.NOISignEmailAddressID).First()
                        .SubmissionID = submission.SubmissionID
                        .EmailAddress = txtCoPermitteeEmail.Text
                        .EntityState = EntityState.Modified
                    End With
                End If
            End If
        End If
        'submission.CertificationAgreed = IIf(chkAgree.Checked = True, "Y", "N")
        Return submission
    End Function

    'Private Sub chkAgree_CheckedChanged(sender As Object, e As EventArgs) Handles chkAgree.CheckedChanged
    '    If chkAgree.Checked = True Then
    '        btnSubmit.Enabled = True
    '    Else
    '        btnSubmit.Enabled = False
    '    End If
    'End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim ns As NOISubmission
        Dim bal As New SWBAL()
        Try

            If Page.IsValid Then
                ns = bal.Insert(MapFieldsToEntities())
                IndNOISubmission = ns
            End If

            Select Case logInVS.reportid
                Case NOIProgramType.ISGeneralPermit
                    If logInVS.progsubmisssiontype = 5 Then
                        responseRedirect("~/Forms/NOIDocs.aspx")
                    Else
                        responseRedirect("~/Forms/SubmissionDetails.aspx")
                    End If
                Case Else
                    responseRedirect("~/Forms/SubmissionDetails.aspx")
            End Select





            ''to be moved to Submission Details page.
            'Dim subid As Integer = logInVS.submissionid
            'Dim filedstatus As New NOISubmissionStatus
            'filedstatus.EntityState = EntityState.Added
            'filedstatus.SubmissionStatusCode = SubmissionStatusCode.F.ToString
            'filedstatus.SubmissionID = subid

            'Dim emailaddresseslst As New List(Of NOISigningEmailAddress)
            'If txtOrigPermitteeEmail.Text <> String.Empty Then
            '    Dim OrigPermitteeemail As New NOISigningEmailAddress
            '    OrigPermitteeemail.SubmissionID = subid
            '    OrigPermitteeemail.EmailAddress = txtOrigPermitteeEmail.Text
            '    OrigPermitteeemail.EntityState = EntityState.Added
            '    emailaddresseslst.Add(OrigPermitteeemail)
            'End If

            'If divPermittee2.Visible = True AndAlso txtCoPermitteeEmail.Text <> String.Empty Then
            '    Dim CoPermitteeemail As New NOISigningEmailAddress
            '    CoPermitteeemail.SubmissionID = subid
            '    CoPermitteeemail.EmailAddress = txtOrigPermitteeEmail.Text
            '    CoPermitteeemail.EntityState = EntityState.Added
            '    emailaddresseslst.Add(CoPermitteeemail)
            'End If

            'bal.InsertFiledSubmissionStatus(filedstatus, emailaddresseslst)

            'bal.RemoveSessionStorageByUser(logInVS.user.userid) ' HttpContext.Current.User.Identity.Name)

            'logInVS.submissionid = 0
            'logInVS.submissiontype = 0
            'If isExternal = True Then
            '    responseRedirect("~/Forms/main.aspx")
            'Else
            '    responseRedirect("~/Admin/Submissions.aspx")
            'End If



        Catch ex As Exception
            Dim bal1 As New NOIBAL
            bal1.LogError(Me, ex)
            Throw (ex)
        End Try
    End Sub

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



    Private Sub NOIAgreement_Error(sender As Object, e As EventArgs) Handles Me.Error
        Me.registerErrorAndSendEmail(Server.GetLastError)
        Response.Redirect("~/Error/ErrorPage.aspx")
    End Sub
End Class