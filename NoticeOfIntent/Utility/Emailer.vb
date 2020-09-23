Imports System.Net.Mail
Imports System.Configuration
Imports System.IO


Public Class Emailer
    Public Shared Sub SendEmail(
            ByVal SmtpServer As String,
            ByVal FromAddress As String,
            ByVal ToAddress As String,
            ByVal CcAddress As String,
            ByVal BccAddress As String,
            ByVal Subject As String,
            ByVal Body As String,
            ByVal BodyEncoding As System.Text.Encoding,
            ByVal IsBodyHtml As Boolean,
            ByVal Priority As MailPriority,
            ByVal Attachment As Attachment)
        Dim objMail As New MailMessage()
        Dim smtpClient As New SmtpClient(SmtpServer)

        With objMail
            .From = New MailAddress(FromAddress, CacheLookupData.GetDeptName()) '"DNREC Sediment and Stormwater Program")
            'Dim SentToAddress() As String = ToAddress.Split(",")
            'For Each toadd As String In SentToAddress
            '    .To.Add(toadd)
            'Next
            .To.Add(ToAddress)
            If Not CcAddress Is Nothing AndAlso CcAddress.Length > 0 Then
                .CC.Add(CcAddress)
            End If
            If Not BccAddress Is Nothing AndAlso BccAddress.Length > 0 Then
                .Bcc.Add(BccAddress)
            End If
            .Subject = Subject
            .Body = Body
            If Not BodyEncoding Is Nothing Then
                .BodyEncoding = BodyEncoding
            End If
            .IsBodyHtml = IsBodyHtml
            If BodyEncoding Is Nothing Then
                .Priority = MailPriority.Normal
            Else
                .Priority = Priority
            End If
            If Attachment IsNot Nothing Then
                .Attachments.Add(Attachment)
            End If
        End With
        'smtpClient.Port = 23
        smtpClient.Send(objMail)
    End Sub

    Public Shared Sub SendEmail(
            ByVal SmtpServer As String,
            ByVal FromAddress As String,
            ByVal ToAddress As String,
            ByVal CcAddress As String,
            ByVal BccAddress As String,
            ByVal Subject As String,
            ByVal Body As String,
            ByVal BodyEncoding As System.Text.Encoding,
            ByVal IsBodyHtml As Boolean,
            ByVal Priority As MailPriority)
        SendEmail(SmtpServer, FromAddress, ToAddress, CcAddress, BccAddress, Subject, Body, BodyEncoding, IsBodyHtml, Priority, Nothing)
    End Sub

    Public Shared Sub SendEmail(
            ByVal SmtpServer As String,
            ByVal FromAddress As String,
            ByVal ToAddress As String,
            ByVal Subject As String,
            ByVal Body As String,
            ByVal Priority As MailPriority)
        SendEmail(SmtpServer, FromAddress, ToAddress, Nothing, Nothing, Subject, Body, Nothing, True, Priority, Nothing)
    End Sub

    Public Shared Sub SendEmail(
            ByVal SmtpServer As String,
            ByVal FromAddress As String,
            ByVal ToAddress As String,
            ByVal Subject As String,
            ByVal Body As String)
        SendEmail(SmtpServer, FromAddress, ToAddress, Nothing, Nothing, Subject, Body, Nothing, True, Nothing, Nothing)
    End Sub


    ''' <summary>
    ''' sends an email to the application programmer
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SendEmailToAdministrator(ByVal email As String)
        Dim toAddress As String = ConfigurationManager.AppSettings("ApplicationAdminEmail")
        SendEmail(ConfigurationManager.AppSettings("MailServer"),
        ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
        toAddress,
        "Notice Of Intent Email",
        email)
    End Sub


    ''' <summary>
    ''' sends an email to the NOI administrators
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SendEmailNewSubmissionToAdmin(ByVal submissionid As Integer, ByVal adminemail As String, submissiontypedesc As String, projectname As String, userid As String)

        If Not String.IsNullOrEmpty(adminemail) Then

            Dim emailBody As New System.Text.StringBuilder("<html><head></head><body><p>New Submission for a " & submissiontypedesc & " has been filed with a project name as " & projectname & " by " & userid & " .</p>")
            emailBody.Append("<p>Submission Number: ")
            emailBody.Append(submissionid.ToString())
            emailBody.Append("</p>")
            emailBody.Append("<p>NOTE:</p>")
            emailBody.Append("<p>Go to CROMERR admin and approve the above project for the user to do a digital signature. </p>")
            emailBody.Append("</body></html>")

            SendEmail(ConfigurationManager.AppSettings("MailServer"),
            ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
            adminemail, Nothing, Nothing,
            "New Submission has been filed.",
            emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
        End If
    End Sub

    Public Shared Sub SendEmailSubmissionRejectedToOwner(ByVal logInVS As LogInDetails, ByVal submission As NOISubmission, statuscomments As String)
        Try



            Dim emailBody As String = String.Empty
            Dim emailSubject As String = String.Empty
            Dim toAddress As String = String.Empty
            Dim ccAddress As String = String.Empty


            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    GetRejectedEmailContentForCSS(submission, statuscomments, emailSubject, emailBody, toAddress, ccAddress)

                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)

                Case NOIProgramType.ISGeneralPermit
                    GetRejectedEmailContentForIS(submission, statuscomments, emailSubject, emailBody, toAddress, ccAddress)

                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
                Case NOIProgramType.PesticideGeneralPermit
                    GetRejectedEmailContentForAP(submission, statuscomments, emailSubject, emailBody, toAddress, ccAddress)

                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
            End Select




        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Shared Sub GetRejectedEmailContentForCSS(ByVal submission As NOISubmission, ByVal statuscomments As String, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)
        Dim owneremail As List(Of String)

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()


        Dim emailBody As New System.Text.StringBuilder()

        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()


        Select Case submission.NOIProgSubmissionType.SubmissionTypeID   ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit
                emailBody.Append("<html><head></head><body><p>This is to notify you that the General NOI with Submission Number ")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)
                End If




            Case NOISubmissionType.CoPermittee

                emailBody.Append("<html><head></head><body><p>This is to notify you that the CoPermittee NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    Dim noipersonorgid As Int16() = {NOIPersonOrgType.OwnerDetails, NOIPersonOrgType.CoPermitteeDetails}
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) noipersonorgid.Contains(a.NOIPersonOrgTypeID)).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                ElseIf owneremail.Count = 1 Then
                    owneremail.Add(submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToString())
                End If
                If owneremail.Count = 2 Then
                    toAddress = owneremail(1)
                    ccAddress = owneremail(0)
                End If


            Case NOISubmissionType.TerminateCoPermittee

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of CoPermittee NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>")    ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If
                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If



            Case NOISubmissionType.TerminateNOI

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>")  ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If



        End Select

        eBody = emailBody.ToString()
    End Sub

    Private Shared Sub GetRejectedEmailContentForIS(ByVal submission As NOISubmission, ByVal statuscomments As String, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)
        Dim owneremail As List(Of String)

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()


        Dim emailBody As New System.Text.StringBuilder()

        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()


        Select Case submission.NOIProgSubmissionType.SubmissionTypeID   ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit
                emailBody.Append("<html><head></head><body><p>This is to notify you that the General NOI with Submission Number ")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)
                End If

            Case NOISubmissionType.TerminateNOI

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>")  ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If



        End Select

        eBody = emailBody.ToString()
    End Sub


    Private Shared Sub GetRejectedEmailContentForAP(ByVal submission As NOISubmission, ByVal statuscomments As String, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)
        Dim owneremail As List(Of String)

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()


        Dim emailBody As New System.Text.StringBuilder()

        Dim admindepartmentname As String = CacheLookupData.GetDeptName()
        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()


        Select Case submission.NOIProgSubmissionType.SubmissionTypeID   ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit
                emailBody.Append("<html><head></head><body><p>This is to notify you that the NOI submission for the Aquatic Pesticide General Permit Program, Reference No. ")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(", filed by you or on your behalf for the above mentioned project is rejected due to the reason given below. If you have any additional questions, Please contact ")
                emailBody.Append("the " & admindepartmentname & " at " & admincontactphone & ".</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                'emailBody.Append("<small><b>For questions contact the " & admindepartmentname & "at " & admincontactphone & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)
                End If

            Case NOISubmissionType.GeneralNOICorrection

                emailBody.Append("<html><head></head><body><p>This is to notify you that the General NOI Correction with Reference No. ")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below. If you have any additional questions, Please contact ")
                emailBody.Append("the " & admindepartmentname & " at " & admincontactphone & ".</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                'emailBody.Append("<small><b>For questions contact the " & admindepartmentname & "at " & admincontactphone & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)
                End If

            Case NOISubmissionType.TerminateNOI

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is rejected due to the reason given below.If you have any additional questions, Please contact ")
                emailBody.Append("the " & admindepartmentname & " at " & admincontactphone & ".</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                'emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>")  ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If

        End Select

        eBody = emailBody.ToString()
    End Sub

    Public Shared Sub SendEmailSubmissionReturnedToOwner(ByVal logInVS As LogInDetails, ByVal submission As NOISubmission, statuscomments As String)
        Try



            Dim emailBody As String = String.Empty
            Dim emailSubject As String = String.Empty
            Dim toAddress As String = String.Empty
            Dim ccAddress As String = String.Empty


            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    GetReturnedEmailContentForCSS(submission, statuscomments, emailSubject, emailBody, toAddress, ccAddress)

                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)

                Case NOIProgramType.ISGeneralPermit
                    GetReturnedEmailContentForIS(submission, statuscomments, emailSubject, emailBody, toAddress, ccAddress)

                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
                Case NOIProgramType.PesticideGeneralPermit
                    GetReturnedEmailContentForAP(submission, statuscomments, emailSubject, emailBody, toAddress, ccAddress)
                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
            End Select



        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Shared Sub GetReturnedEmailContentForCSS(ByVal submission As NOISubmission, ByVal statuscomments As String, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)
        Dim owneremail As List(Of String)

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()


        Dim emailBody As New System.Text.StringBuilder()

        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()


        Select Case submission.NOIProgSubmissionType.SubmissionTypeID   ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit
                emailBody.Append("<html><head></head><body><p>This is to notify you that the General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)

                    'SendEmail(ConfigurationManager.AppSettings("MailServer"),
                    '                       ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                    '                       owneremail(0), Nothing, Nothing,
                    '                       emailSubject,
                    '                       emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
                End If



            Case NOISubmissionType.CoPermittee

                emailBody.Append("<html><head></head><body><p>This is to notify you that the CoPermittee NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>")  ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    Dim noipersonorgid As Int16() = {NOIPersonOrgType.OwnerDetails, NOIPersonOrgType.CoPermitteeDetails}
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) noipersonorgid.Contains(a.NOIPersonOrgTypeID)).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                ElseIf owneremail.Count = 1 Then
                    owneremail.Add(submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToString())
                End If
                If owneremail.Count = 2 Then
                    toAddress = owneremail(1)
                    ccAddress = owneremail(0)
                End If

                'If owneremail.Count = 2 Then
                '    'Not owneremail Is Nothing AndAlso Not String.IsNullOrEmpty(owneremail(0)) AndAlso Not String.IsNullOrEmpty(owneremail(1)) Then
                '    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                '                           ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                '                           owneremail(0), owneremail(1), Nothing,
                '                           emailSubject,
                '                           emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
                'End If
            Case NOISubmissionType.TerminateCoPermittee

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of CoPermittee NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>")  ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If
                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If

                'If owneremail.Count > 0 Then
                '    'Not owneremail Is Nothing AndAlso Not String.IsNullOrEmpty(owneremail(0)) AndAlso Not String.IsNullOrEmpty(owneremail(1)) Then
                '    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                '                           ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                '                           owneremail(0), Nothing, Nothing,
                '                           emailSubject,
                '                           emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
                'End If

            Case NOISubmissionType.TerminateNOI

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If

                'If owneremail.Count > 0 Then
                '    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                '                           ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                '                           owneremail(0), Nothing, Nothing,
                '                           emailSubject,
                '                           emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal)
                'End If

        End Select

        eBody = emailBody.ToString()
    End Sub

    Private Shared Sub GetReturnedEmailContentForIS(ByVal submission As NOISubmission, ByVal statuscomments As String, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)
        Dim owneremail As List(Of String)

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()


        Dim emailBody As New System.Text.StringBuilder()

        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()


        Select Case submission.NOIProgSubmissionType.SubmissionTypeID   ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit
                emailBody.Append("<html><head></head><body><p>This is to notify you that the General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)
                End If

            Case NOISubmissionType.TerminateNOI

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If
        End Select

        eBody = emailBody.ToString()
    End Sub

    Private Shared Sub GetReturnedEmailContentForAP(ByVal submission As NOISubmission, ByVal statuscomments As String, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)
        Dim owneremail As List(Of String)

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()


        Dim emailBody As New System.Text.StringBuilder()

        Dim admindepartmentname As String = CacheLookupData.GetDeptName()
        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()


        Select Case submission.NOIProgSubmissionType.SubmissionTypeID   ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit
                emailBody.Append("<html><head></head><body><p>This is to notify you that the NOI submission for the Aquatic Pesticide General Permit Program, Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(", filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<p>To resubmit the document, return to the home page of your account and click ""Click here to do data entry"". Select ""V"" to view and edit the document for resubmission. ")
                emailBody.Append("If you have any additional questions, please contact the " & admindepartmentname & "at " & admincontactphone & ".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                'emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                ' emailBody.Append("<small><b>For questions contact the " & admindepartmentname & "at " & admincontactphone & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)
                End If
            Case NOISubmissionType.GeneralNOICorrection
                emailBody.Append("<html><head></head><body><p>This is to notify you that the General NOI Correction Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count > 0 Then
                    toAddress = owneremail(0)
                End If
            Case NOISubmissionType.TerminateNOI

                emailBody.Append("<html><head></head><body><p>This is to notify you that the Termination of General NOI Submission with Reference No.")
                emailBody.Append(submission.SubmissionID)
                emailBody.Append(" filed by you or on your behalf for the above mentioned project is returned for resubmission due to the reason given below.</p>")
                emailBody.Append("<p>")
                emailBody.Append(statuscomments)
                emailBody.Append(".</p>")
                emailBody.Append("<br /><br />")
                emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />")
                emailBody.Append("<small><b>For questions contact the DNREC Online Reporting System Administrator at " & admincontactemail & ".</b></small>") ' ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If

        End Select

        eBody = emailBody.ToString()
    End Sub


    Public Shared Sub SendEmailSubmissionApprovedToOwner(ByVal logInVS As LogInDetails, ByVal submission As NOISubmission)

        ',  ByVal submissionid As Integer, ByVal projectname As String, ByVal permitnumber As String, ByVal owneremail As String, submissiontype As NOISubmissionType

        Try



            Dim emailBody As String = String.Empty
            Dim emailSubject As String = String.Empty
            Dim toAddress As String = String.Empty
            Dim ccAddress As String = String.Empty

            Dim buffer() As Byte = Nothing
            'buffer = NOIReports.createReport(submission.NOIProgSubmissionType.SubmissionTypeID, submission.SubmissionID)
            buffer = NOIReports.createReport(logInVS)
            Dim ms As New MemoryStream(buffer)
            Dim att As New Attachment(ms, submission.NOIProgSubmissionType.SubmissionTypeDesc.ToString() & " " & submission.SubmissionID.ToString() & ".pdf")


            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    GetApprovedEmailContentForCSS(submission, emailSubject, emailBody, toAddress, ccAddress)

                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal, att)

                Case NOIProgramType.ISGeneralPermit
                    GetApprovedEmailContentForIS(submission, emailSubject, emailBody, toAddress, ccAddress)

                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                               ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                               toAddress, ccAddress, Nothing,
                                               emailSubject,
                                               emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal, att)
                Case NOIProgramType.PesticideGeneralPermit
                    GetApprovedEmailContentForAP(submission, emailSubject, emailBody, toAddress, ccAddress)
                    SendEmail(ConfigurationManager.AppSettings("MailServer"),
                                                                  ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
                                                                  toAddress, ccAddress, Nothing,
                                                                  emailSubject,
                                                                  emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal, att)

            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub



    Private Shared Sub GetApprovedEmailContentForCSS(ByVal submission As NOISubmission, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)

        Dim emailBody As New System.Text.StringBuilder()
        Dim owneremail As List(Of String)
        Dim statusdate As String = String.Empty

        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()

        Select Case submission.NOIProgSubmissionType.SubmissionTypeID  ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit


                emailBody.Append("<html><head></head><body><p>This letter serves as confirmation that ""The Notice of Intent (NOI) for Storm Water Discharges Associated with Construction Activity under a NPDES General Permit"" for the above mentioned project has been processed and your NOI number is ")
                emailBody.Append(submission.NOIProject.PermitNumber)  'permitnumber)
                emailBody.Append(".</p>")
                emailBody.Append("<p>By signing the NOI, the signatory agrees to fully comply with the Special Conditions for StormWater Discharges Associated with Construction Activities which can be viewed at <a href=""http://regulations.delaware.gov/AdminCode/title7/7000/7200/7201.shtml#TopOfPage"">Click Here</a></p>")
                emailBody.Append("<p>Outlined below are several responsibilities that should be noted:</p>")
                emailBody.Append("<ul><li><p>During construction the approved Sediment and Stormwater Plan shall remain at the site at all times (&sect; 9.1.02.4.B.1).  A copy of the NOI shall be kept at the site as well.</p></li>")
                emailBody.Append("<li><p>Maintenance inspections of erosion and sediment (E &amp; S) controls and stormwater management facilities must be conducted weekly and the next day after a rainfall event that results in runoff. (&sect; 9.1.02.4.B.2).</p></li>")
                emailBody.Append("<li><p>Maintenance inspections must be documented in a weekly log that must be maintained on-site (&sect; 9.1.02.4.C.1).  The documentation must contain:</p><ol type=""1""><li>the date and time of inspection;</li><li>the inspector's name;</li><li>assessment of the condition of the E&amp;S controls and stormwater management facilities;</li><li>any construction, implementation, or maintenance performed; and </li><li>a description of the site's present phase of construction.</li></ol></li>")
                emailBody.Append("<li><p>If the site employs a Certified Construction Reviewer (CCR), the weekly CCR reports may suffice as the weekly log.  CCR reports must be maintained on site.</p></li>")
                'emailBody.Append("<li><p>If ownership or operational control of the permitted activities is transferred a Transfer of Authorization form must be filled out and submitted to the Department to update the NOI (&sect; 9.1.02.1.E).</p></li>") removed as requested by the program.
                emailBody.Append("<li><p>To closeout the ""Notice of Intent (NOI) for Storm Water Discharges Associated with Construction Activity under a NPDES General Permit"" a completed Notice of Termination (NOT) form must be submitted to the Department for review and approval.  The following requirements (&sect; 9.1.02.7.B) need to be met prior to submittal of the NOT form:</p><ol type=""1""><li>All items and conditions of the Plan have been satisfied in accordance with the <i>Delaware Sediment and Stormwater Regulations</i>;</li><li>As-built documentation verifies that permanent stormwater management measures have been constructed in accordance with the approved Plan and the <i>Delaware Sediment and Stormwater Regulations</i>; and </li><li>Final stabilization has been achieved in accordance with the definition in (&sect; 9.1.02.0).</li></ol></li>")
                emailBody.Append("<li><p>The Project permitted under this Notice of Intent is also subject to an annual fee of $")
                emailBody.Append(CacheLookupData.GetFeesBySubmissionType(submission.ProgSubmissionTypeID))
                emailBody.Append("until such time as a NOT has been submitted and accepted by the Department as described above.  The applicant/owner will receive an invoice from DNREC Fiscal Management.</p></li></ul>")
                emailBody.Append("</body></html>")

                emailSubject = "NOI Approval Letter - " + submission.NOIProject.ProjectName     ' projectname

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)

                End If

            Case NOISubmissionType.CoPermittee
                Dim owner As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails)
                Dim copermittee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Single(Function(e) e.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails)

                emailBody.Append("<html><head></head><body><p>The Co-Permittee Application for Shared Operational Control of Storm Water Discharges Associates with Construction Activity Under a NPDES General Permit has been added to the current Notice of Intent with NOI Number " & submission.NOIProject.PermitNumber.ToString() & " And project name  " & submission.NOIProject.ProjectName & ".</P>")
                emailBody.Append("<p>Original Permittee Information</p>")
                emailBody.Append("<dl><dd>")
                emailBody.Append(owner.FName)
                emailBody.Append(" ")
                emailBody.Append(owner.LName)
                emailBody.Append("<br />")
                emailBody.Append(owner.Address1)
                emailBody.Append("<br />")
                If owner.Address2 <> String.Empty Then
                    emailBody.Append(owner.Address2)
                    emailBody.Append("<br />")
                End If
                emailBody.Append(owner.City)
                emailBody.Append(", ")
                emailBody.Append(owner.StateAbv)
                emailBody.Append(" ")
                emailBody.Append(owner.PostalCode)
                emailBody.Append("<br />")
                emailBody.Append(owner.Phone)
                emailBody.Append("</dd></dl>")
                emailBody.Append("<p>Co-Permittee Owner/Operator</p>")
                emailBody.Append("<dl><dd>")
                emailBody.Append(copermittee.FName)
                emailBody.Append(" ")
                emailBody.Append(copermittee.LName)
                emailBody.Append("<br />")
                emailBody.Append(copermittee.Address1)
                emailBody.Append("<br />")
                If owner.Address2 <> String.Empty Then
                    emailBody.Append(copermittee.Address2)
                    emailBody.Append("<br />")
                End If
                emailBody.Append(copermittee.City)
                emailBody.Append(", ")
                emailBody.Append(copermittee.StateAbv)
                emailBody.Append(" ")
                emailBody.Append(copermittee.PostalCode)
                emailBody.Append("<br />")
                emailBody.Append(copermittee.Phone)
                emailBody.Append("</dd></dl>")
                emailBody.Append("<p>Date Co-Permittee was entered into the database ")
                emailBody.Append(submission.CopermitteeReceivedDate)
                emailBody.Append("</p>")
                emailBody.Append("<p>Please make a note of these changes in your files.</p>")
                emailBody.Append("<p>Also by signing the NOI, Co-Permittee Application, the signatory agrees to fully comply with the Special Conditions for StormWater Discharges Associated with Construction Activities which can be viewed at <a href=""http//www.dnrec.state.de.us/DNREC2000/Divisions/Soil/Stormwater/PDF/NPDES_Sect9_GP.pdf"">Click Here</a></p>")
                emailBody.Append("<p>Outlined below are several responsibilities that should be noted</p>")
                emailBody.Append("<ul><li><p>During construction the approved Sediment And Stormwater Plan shall remain at the site at all times (&sect; 9.1.02.4.B.1).  A copy of the NOI shall be kept at the site as well.</p></li>")
                emailBody.Append("<li><p>Maintenance inspections of erosion And sediment (E &amp; S) controls And stormwater management facilities must be conducted weekly And the next day after a rainfall event that results in runoff. (&sect; 9.1.02.4.B.2).</p></li>")
                emailBody.Append("<li><p>Maintenance inspections must be documented in a weekly log that must be maintained on-site (&sect; 9.1.02.4.C.1).  The documentation must contain</p><ol type=""1""><li>the date And time of inspection;</li><li>the inspector's name;</li><li>assessment of the condition of the E&amp;S controls and stormwater management facilities;</li><li>any construction, implementation, or maintenance performed; and </li><li>a description of the site's present phase of construction.</li></ol></li><br />")
                emailBody.Append("<li><p>If the site employs a Certified Construction Reviewer (CCR), the weekly CCR reports may suffice as the weekly log.  CCR reports must be maintained on site.</p></li>")
                emailBody.Append("<li><p>If ownership or operational control of the permitted activities is transferred a new NOI form must be filled out and submitted to the Department.</p></li>")
                emailBody.Append("<li><p>To closeout your General NPDES Storm Water permit coverage and its requirements a completed Notice of Termination (NOT) form must be submitted to the Department for review and approval.  The following requirements (&sect; 9.1.02.7.B) need to be met prior to submittal of the NOT form:</p><ol type=""1""><li>All items and conditions of the Plan have been satisfied in accordance with the <i>Delaware Sediment and Stormwater Regulations</i>;</li><li>As-built documentation verifies that permanent stormwater management measures have been constructed in accordance with the approved Plan and the <i>Delaware Sediment and Stormwater Regulations</i>; and </li><li>Final stabilization has been achieved in achieved in accordance with the definition in (&sect; 9.1.02.0).</li></ol></li>")
                emailBody.Append("</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    Dim noipersonorgid As Int16() = {NOIPersonOrgType.OwnerDetails, NOIPersonOrgType.CoPermitteeDetails}
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) noipersonorgid.Contains(a.NOIPersonOrgTypeID)).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                ElseIf owneremail.Count = 1 Then
                    owneremail.Add(submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToString())
                End If

                If owneremail.Count = 2 Then
                    toAddress = owneremail(1)
                    ccAddress = owneremail(0)
                End If

            Case NOISubmissionType.TerminateCoPermittee

                statusdate = submission.NOISubmissionStatus.Where(Function(a) a.SubmissionStatusCode = "A").OrderByDescending(Function(c) c.SubmissionStatusDate).Take(1).Select(Function(b) b.SubmissionStatusDate).FirstOrDefault().ToString()

                emailBody.Append("<html><head></head><body><p>Thank you for your submittal of ""The Notice of Termination (NOT) of Shared Operation Control for Storm Water Discharges Associated with Construction Activity under a NPDES General Permit"" for the subject project.  Permit coverage for this co-permittee has been terminated.  Permit coverage for the original permittee remains until terminated.</p>")
                emailBody.Append("<p>Date NOT information was entered into the database: ")
                emailBody.Append(statusdate)
                emailBody.Append("<p>If you have any questions, please contact Sediment and Stormwater Program at ")
                emailBody.Append(admincontactphone) ' ConfigurationManager.AppSettings("NOIAdminPhone"))
                emailBody.Append(" or by email at ")
                emailBody.Append(admincontactemail) '  ConfigurationManager.AppSettings("NOIAdminContactEmail"))
                emailBody.Append(".</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If

            Case NOISubmissionType.TerminateNOI

                statusdate = submission.NOISubmissionStatus.Where(Function(a) a.SubmissionStatusCode = "A").OrderByDescending(Function(c) c.SubmissionStatusDate).Take(1).Select(Function(b) b.SubmissionStatusDate).FirstOrDefault().ToString()

                emailBody.Append("<html><head></head><body><p>Thank you for your submittal of ""The Notice of Termination (NOT) for Storm Water Discharges Associated with Construction Activity under a NPDES General Permit"" for the subject project.  Permit coverage has been terminated.</p>")
                emailBody.Append("<p>Date NOT information was entered into the database: ")
                emailBody.Append(statusdate)
                emailBody.Append("<p>If you have any questions, please contact Sediment and Stormwater Program at ")
                emailBody.Append(admincontactphone) ' ConfigurationManager.AppSettings("NOIAdminPhone"))
                emailBody.Append(" or by email at ")
                emailBody.Append(admincontactemail) 'ConfigurationManager.AppSettings("NOIAdminContactEmail"))
                emailBody.Append(".</body></html>")

                emailSubject = submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If
        End Select

        eBody = emailBody.ToString()


    End Sub

    Private Shared Sub GetApprovedEmailContentForIS(ByVal submission As NOISubmission, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)

        Dim emailBody As New System.Text.StringBuilder()
        Dim owneremail As List(Of String)
        Dim owner As NOISubmissionPersonOrg
        Dim projectadd As NOISubmissionPersonOrg
        Dim statusdate As String = String.Empty

        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()
        owner = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).FirstOrDefault()
        projectadd = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress).FirstOrDefault()

        Select Case submission.NOIProgSubmissionType.SubmissionTypeID  ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit

                Select Case submission.ProgSubmissionTypeID
                    Case 5
                        emailBody.Append("<html><head></head><body><p>Dear Mr./Mrs. ")
                        emailBody.Append(owner.LName)
                        emailBody.Append(",</p><br />")
                        emailBody.Append("<p>The Department of Natural Resources and Environmental Control (DNREC) has received your request for coverage under the ")
                        emailBody.Append("NPDES Storm Water General Permit Program for storm water discharges from the facility located at ")
                        emailBody.Append(projectadd.Address1 + ", " + projectadd.City + ", " + projectadd.StateAbv + ", " + projectadd.PostalCode + ". ")
                        emailBody.Append("This letter serves as authorization to discharge storm water from the above-referenced facility in ")
                        emailBody.Append("compliance with 7 Del. Admin. C. &sect;7201 of the State of Delaware ""Regulations Governing Storm Water Discharges Associated with Industrial Activities"", ")
                        emailBody.Append("to a surface water body of the state.</p>")

                        emailBody.Append("<p>Permit coverage began on ")
                        emailBody.Append(Now.ToShortDateString())
                        emailBody.Append(" and will be in effect until such time that a new NPDES Industrial Storm Water General Permit is issued within the State of Delaware. ")
                        emailBody.Append("Upon issuance of a new Industrial Storm Water General Permit, you will be required to submit a new Notice of Intent (NOI) form to the ")
                        emailBody.Append("Department in order to continue permit coverage. The schedule for this NOI submission will be outlined in the new Industrial Storm Water General Permit once issued and communicated to you. ")
                        emailBody.Append("Under no circumstances shall this authorization extend beyond five years.</p>")

                        emailBody.Append("<p>Any changes in facility operations or contact information will require the Storm Water Plan (SWP) for this facility to be amended. ")
                        emailBody.Append("A signed copy of the SWP must be maintained at the facility at all times. NOI forms And other resources can be found online at: http://www.wr.dnrec.delaware.gov/Information/SWDInfo/Pages/SWDSStormWater.aspx .</p>")

                        emailBody.Append("<p>Please maintain this approval on file at the facility at all times. If you have any questions or require further assistance, please contact us at ")
                        emailBody.Append(admincontactphone)
                        emailBody.Append(" or by e-mail at: ")
                        emailBody.Append(admincontactemail)
                        emailBody.Append(".</p></body></html>")

                        emailSubject = "Authorization to Discharge Under the National Pollutant Discharge Elimination System (NPDES) Storm Water General Permit Program at " + submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                    Case 6
                        emailBody.Append("<html><head></head><body><p>Dear Mr./Mrs. ")
                        emailBody.Append(owner.LName)
                        emailBody.Append(",</p><br />")
                        emailBody.Append("<p>The Department has approved your request for Conditional ""No Exposure"" Exclusion, for ""No Exposure"" coverage under the ")
                        emailBody.Append("National Pollutant Discharge Elimination System (NPDES) Storm Water General Permit Program. ")
                        emailBody.Append("Permit coverage began on ")
                        emailBody.Append(Now.ToShortDateString())
                        emailBody.Append(" and will be in effect until such time that a new NPDES Industrial Storm Water General Permit is issued within the State of Delaware. ")
                        emailBody.Append("Upon issuance of a new Industrial Storm Water General Permit, you will be required to submit a new ""No Exposure"" Certification form to the Department in order to continue permit coverage. ")
                        emailBody.Append("The schedule for this ""No Exposure"" submission will be outlined in the new Industrial Storm Water General Permit once issued and communicated to you. ")
                        emailBody.Append("Under no circumstances shall this authorization extend beyond five years.</p>")

                        emailBody.Append("<p>Approval was granted because the certification submitted specifies that all processes and materials are protected from rain, snow, snowmelt, and/or runoff, ")
                        emailBody.Append("in accordance with Section 9.1.1.5 of 7 Del. Admin. C. &sect;7201 of the State of Delaware ""Regulations Governing Storm Water Discharges Associated with Industrial Activities"" (the Regulations). ")
                        emailBody.Append("This approval means the site located at ")
                        emailBody.Append(projectadd.Address1 + ", " + projectadd.City + ", " + projectadd.StateAbv + ", " + projectadd.PostalCode + ". ")
                        emailBody.Append("is covered under the Regulations, with the monitoring requirement (9.1.4) and the requirement for a Storm Water Plan (9.1.5) being exempt for this type of coverage.</p>")


                        emailBody.Append("<p>""No Exposure"" permit coverage is conditional. If a change in facility operations causes exposure of industrial activities or materials to storm water, ")
                        emailBody.Append("a Notice of Intent (NOI) form must be submitted to the Department, along with a Storm Water Plan (submitted both hard copy and digitally). ")
                        emailBody.Append("""No Exposure"" forms And NOI forms can be found at: http://www.wr.dnrec.delaware.gov/Information/SWDInfo/Pages/SWDSStormWater.aspx .</p>")

                        emailBody.Append("<p>Please maintain this ""No Exposure"" approval and NPDES authorization on file at the facility at all times. If you have any questions or require further assistance, please contact us at ")
                        emailBody.Append(admincontactphone)
                        emailBody.Append(" or by e-mail at: ")
                        emailBody.Append(admincontactemail)
                        emailBody.Append(".</p></body></html>")

                        emailSubject = "Conditional ""No Exposure"" Exclusion Approval and Authorization to Discharge Under the NPDES Storm Water General Permit Program at " + submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                End Select



                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)

                End If
            Case NOISubmissionType.TerminateNOI

                statusdate = submission.NOISubmissionStatus.Where(Function(a) a.SubmissionStatusCode = "A").OrderByDescending(Function(c) c.SubmissionStatusDate).Take(1).Select(Function(b) b.SubmissionStatusDate).FirstOrDefault().ToString()

                emailBody.Append("<html><head></head><body><p>Dear Mr./Mrs. ")
                emailBody.Append(owner.LName)
                emailBody.Append(",</p><br />")
                emailBody.Append("<p>Per your request, General Permit Coverage for Industrial Stormwater for the site located at ")
                emailBody.Append(projectadd.Address1 + ", " + projectadd.City + ", " + projectadd.StateAbv + ", " + projectadd.PostalCode + ". ")
                emailBody.Append("has been terminated effective . A permit fee is no longer required for the above referenced facility. If you receive subsequent billings, please contact DNREC's Accounting Department at (302) 739-9940.</p>")
                emailBody.Append("<p>If you have any questions or require further assistance, please contact us at ")
                emailBody.Append(admincontactphone)
                emailBody.Append(" or by e-mail at: ")
                emailBody.Append(admincontactemail)
                emailBody.Append(".</p></body></html>")

                emailSubject = "Notice of Termination for National Pollutant Discharge Elimination System (NPDES) General Permit Coverage for Industrial Stormwater at " + submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If
        End Select

        eBody = emailBody.ToString()


    End Sub


    Private Shared Sub GetApprovedEmailContentForAP(ByVal submission As NOISubmission, ByRef emailSubject As String, ByRef eBody As String, ByRef toAddress As String, ByRef ccAddress As String)
        Dim emailBody As New System.Text.StringBuilder()
        Dim owneremail As List(Of String)
        Dim owner As NOISubmissionPersonOrg
        Dim statusdate As String = String.Empty

        Dim admincontactphone As String = CacheLookupData.GetDeptPhone()
        Dim admincontactemail As String = CacheLookupData.GetDeptEmail()

        owneremail = submission.NOISigningEmailAddress.OrderBy(Function(b) b.NOISignEmailAddressID).Select(Function(a) a.EmailAddress).ToList()
        owner = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).FirstOrDefault()


        Select Case submission.NOIProgSubmissionType.SubmissionTypeID  ' submissiontype
            Case NOISubmissionType.GeneralNOIPermit

                emailBody.Append("<html><head></head><body><p>Dear Mr./Mrs. ")
                emailBody.Append(owner.LName)
                emailBody.Append(",</p><br />")
                emailBody.Append("<p>The Department of Natural Resources and Environmental Control (DNREC) has received and approved ")
                emailBody.Append(submission.NOIProject.ProjectName)
                emailBody.Append("'s request for coverage under the NPDES Aquatic Pesticides General Permit Program (""Regulations Governing Discharges from the Application of Pesticides to Waters of the State""). ")
                emailBody.Append("Permit coverage began on ")
                emailBody.Append(Now.ToShortDateString())
                emailBody.Append(" and will expire on ")
                emailBody.Append(Now.AddYears(5).ToShortDateString())
                emailBody.Append(". ")
                emailBody.Append("A new Notice of Intent (NOI) form must be submitted to DNREC for review and approval 60 days prior to the expiration date in order to continue permit coverage. ")
                emailBody.Append("In addition, a new NOI form must be submitted to DNREC for any one of the following conditions:</p>")
                emailBody.Append("<ul><li><p>Change in contact information;</p></li>")
                emailBody.Append("<li><p>Change in active ingredients used; or</p></li>")
                emailBody.Append("<li><p>Change in quantities of active ingredients that vary by more than 15%</p></li></ul>")
                emailBody.Append("<p>If at any time you are no longer operating in the State of Delaware, a Notice of Termination(NOT) form must be submitted to DNREC. Additional resources can be found online at:")
                emailBody.Append("<a href=""https://dnrec.alpha.delaware.gov/water/surface-water/npdes/aquatic-pesticides/"">Click Here</a></p>")
                emailBody.Append("<p>Please maintain this approval and a copy of the signed NOI with the Operator. If you have any questions or require further assistance, please contact us at ")
                emailBody.Append(admincontactphone)
                emailBody.Append(" or by e-mail at: ")
                emailBody.Append(admincontactemail)
                emailBody.Append(".</p></body></html>")

                emailSubject = "Authorization to Discharge Under the NPDES Aquatic Pesticides General Permit Program for " + submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"    ' projectname




                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)

                End If
            Case NOISubmissionType.TerminateNOI

                statusdate = submission.NOISubmissionStatus.Where(Function(a) a.SubmissionStatusCode = "A").OrderByDescending(Function(c) c.SubmissionStatusDate).Take(1).Select(Function(b) b.SubmissionStatusDate).FirstOrDefault().ToString()

                emailBody.Append("<html><head></head><body><p>Dear Mr./Mrs. ")
                emailBody.Append(owner.LName)
                emailBody.Append(",</p><br />")
                emailBody.Append("<p>The Department of Natural Resources and Environmental Control (DNREC) has ")
                emailBody.Append(submission.NOIProject.ProjectName)
                emailBody.Append("'s request to terminate coverage under the NPDES Aquatic Pesticides General Permit Program (""Regulations Governing Discharges from the Application of Pesticides to Waters of the State"").</p>")
                emailBody.Append("<p>Per your request, coverage has been terminated effective")
                emailBody.Append(statusdate)
                emailBody.Append(".</p><br /><p>If you have any questions or require further assistance, please contact us at ")
                emailBody.Append(admincontactphone) ' ConfigurationManager.AppSettings("NOIAdminPhone"))
                emailBody.Append(" or by email at ")
                emailBody.Append(admincontactemail) ' ConfigurationManager.AppSettings("NOIAdminContactEmail"))
                emailBody.Append(".</p></body></html>")

                emailSubject = "Notice of Termination for the National Pollutant Discharge Elimination System (NPDES) General Permit Coverage For Aquatic Persticides for " + submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If
            Case NOISubmissionType.GeneralNOICorrection

                emailBody.Append("<html><head></head><body><p>Dear Mr./Mrs. ")
                emailBody.Append(owner.LName)
                emailBody.Append(",</p><br />")
                emailBody.Append("<p>Your changes to the Permit ")
                emailBody.Append(submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")")
                emailBody.Append(" is been approved and is been modified as requested.</p>")
                emailBody.Append("</body></html>")

                emailSubject = "Change request approved for the NPDES Aquatic Pesticides General Permit " + submission.NOIProject.ProjectName + "(" + submission.NOIProject.PermitNumber + ")"

                If owneremail.Count = 0 Then
                    owneremail = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails).OrderBy(Function(b) b.NOIPersonOrgTypeID).Select(Function(c) c.EmailAddress).ToList()
                End If

                If owneremail.Count = 1 Then
                    toAddress = owneremail(0)
                End If

        End Select

        eBody = emailBody.ToString()

    End Sub

    ''' <summary>
    ''' sends an email to the Owner of the submission
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SendEmailNewSubmissionToOwner(ByVal submissionid As Integer, ByVal owneremail As String, submissiontypedesc As String, projectname As String, ByVal pdf() As Byte)

        If Not String.IsNullOrEmpty(owneremail) Then
            Dim ms As New MemoryStream(pdf)
            Dim att As New Attachment(ms, submissiontypedesc & " " & submissionid.ToString() & ".pdf")

            Dim emailBody As New System.Text.StringBuilder("<html><head></head><body><p>" & submissiontypedesc & " has been filed with a project name as " & projectname & " by you or on your behalf.</p>")
            emailBody.Append("<p>Submission Number: ")
            emailBody.Append(submissionid.ToString())
            emailBody.Append("</p>")
            emailBody.Append("<p>NOTE:</p>")
            emailBody.Append("<p>Users please click here " & ConfigurationManager.AppSettings("Homepage") & " to sign the submitted document. New users will need to create an account prior to signing the submitted document. You will be able to create an account by clicking the ""NewUser"" button.</p>")
            emailBody.Append("<p>If you have not applied for the above application or have not hired anyone to apply on your behalf, please contact us at " & CacheLookupData.GetDeptEmail() & ".</p>") '  ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</p>")
            emailBody.Append("</body></html>")

            SendEmail(ConfigurationManager.AppSettings("MailServer"),
            ConfigurationManager.AppSettings("DoNotReplyEmailAddress"),
            owneremail, Nothing, Nothing,
            "New Submission has been filed.",
            emailBody.ToString(), Text.Encoding.Default, True, MailPriority.Normal, att)
        End If
    End Sub

    ''' <summary>
    ''' sends an email to the submitter
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SendEmailForSuccessfullPayment(ByVal bp As BasePage, submissiontypedesc As String, amountpaid As Decimal)

        Dim emailAddr As String = bp.logInVS.user.useremail

        Dim emailSubject As String = "Thank You for the Payment - Notice Of Intent Application Fee"
        Dim emailBody As New System.Text.StringBuilder("<html><body>Thank you for your payment of " & amountpaid.ToString("C2") & " for the " & submissiontypedesc & " with reference no." & bp.logInVS.submissionid & ".")
        emailBody.Append("<br /><br />")
        emailBody.Append("<br /><br />Thank you.<br /><br /><br />DNREC Online Reporting System Administrator.<br />" &
                        "<small><b>For questions contact the DNREC Online Reporting System Administrator at " & CacheLookupData.GetDeptEmail() & ".</b></small></body></html>") 'ConfigurationManager.AppSettings("NOIAdminContactEmail") & ".</b></small></body></html>")

        If bp.SendMailConfirmation Then
            SendEmail(ConfigurationManager.AppSettings("MailServer"), ConfigurationManager.AppSettings("DoNotReplyEmailAddress"), emailAddr, Nothing, Nothing, emailSubject, emailBody.ToString(),
                        Nothing, True, MailPriority.Normal, Nothing)
        End If

    End Sub


End Class
