Imports System.IO
Imports System.Security.Cryptography
Imports System.Web.Services.Protocols

Public Class CROMERRExchange
    Public Event TransferCompleted(ByVal sender As Object, ByVal e As CROMERREventArgs)
    Public Event TransferFailed(ByVal sender As Object, ByVal e As CROMERREventArgs)



    ''' <summary>
    ''' upload report to CromErr
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendReportToCROMERR(ByVal bp As BasePage, ByVal report() As Byte, ByVal login As LogInDetails, ByVal submission As NOISubmission) As Boolean
        Dim fStream As FileStream = Nothing
        Dim returnval As Boolean = False
        If report Is Nothing Then
            Return returnval
        End If

        Dim ws As New wsCROMTOM.wsCROMTOM


        Dim ValidToken As Integer = 0
        Dim LocalFileHash As String = String.Empty
        Dim RemoteFileHash As String = String.Empty
        Dim RedirectPath As String = String.Empty

        Try
            'validate the token before uploading the document
            ValidToken = ws.AuthenticateToken(login.loginToken)


            'ws.UploadDocument(login.submissionid.ToString() & ".pdf", report)

            'RedirectPath = Convert.ToString(ws.SaveFile(login.submissionid.ToString() & ".pdf", LocalFileHash, login.loginToken, login.reportid, 0, bp.Request.UserHostAddress, CommonFunctions.getText(TypeCodeTableCache.typeCodeTableGet(TypeTables.SubmissionTypeCodes, bp), bp.submission(login.submissionid).submissionType), login.submissionid))

            Dim Owneremailaddresses As String = String.Join(",", submission.NOISigningEmailAddress.Select(Function(a) a.EmailAddress.ToUpper()))
            Dim currentuseremailaddress As String = login.user.useremail.ToUpper()
            Dim emailaddresstobeattached As String = String.Empty
            If InStr(Owneremailaddresses, currentuseremailaddress) Then
                emailaddresstobeattached = Owneremailaddresses
            Else
                emailaddresstobeattached = Owneremailaddresses + "," + currentuseremailaddress
            End If

            If ValidToken <> 0 Then
                RedirectPath = Convert.ToString(ws.SubmitDocument(login.loginToken, "eNOI-" & login.submissiontype.ToString() & "-" & login.submissionid.ToString() & ".pdf", report, login.reportid, 0, submission.NOIProject.ProjectNumber, submission.NOIProject.ProjectName, bp.Request.UserHostAddress, login.submissiontype.ToString(), login.submissionid.ToString(), CacheLookupData.GetNoOfSignaturesBySubmissionType(submission.ProgSubmissionTypeID), emailaddresstobeattached))
                If RedirectPath <> String.Empty AndAlso InStr(RedirectPath, "Error") = 0 Then
                    RaiseTransferCompletedEvent(New CROMERREventArgs(RedirectPath))
                    returnval = True
                Else
                    If RedirectPath = String.Empty Then
                        RaiseTransferFailedEvent(New CROMERREventArgs(New Exception("Redirect Path is not valid")))
                    Else
                        RaiseTransferFailedEvent(New CROMERREventArgs(New Exception(RedirectPath)))
                    End If
                    returnval = False
                End If
            Else
                RaiseTransferFailedEvent(New CROMERREventArgs(New Exception("Login token is not valid")))
                returnval = False
            End If


        Catch cromerrwserr As SoapException
            ErrorSummary.AddError("Error sending report to CROMERR--" + cromerrwserr.Message, bp)
            Dim bal As New NOIBAL
            bal.LogError(bp, New Exception("Error sending report to CROMERR--", cromerrwserr))
            Return False
        Catch ex As Exception
            ErrorSummary.AddError("Error sending report to CROMERR" + ex.StackTrace.ToString(), bp)
            Dim bal As New NOIBAL
            bal.LogError(bp, New Exception("Error sending report to CROMERR--", ex))
            Return False
        Finally

        End Try

        Return returnval
    End Function
    ''' <summary>
    ''' raises a complete event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RaiseTransferCompletedEvent(ByVal e As CROMERREventArgs)
        RaiseEvent TransferCompleted(Me, e)
    End Sub

    ''' <summary>
    ''' raises a tansfer file failed event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RaiseTransferFailedEvent(ByVal e As CROMERREventArgs)
        RaiseEvent TransferFailed(Me, e)
    End Sub


    ''' <summary>
    ''' set the Crom Err document to have the status rejected
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Submission_ChangeStatus_Reject(ByVal bp As BasePage, ByVal login As LogInDetails) As Boolean

        Dim ws As New wsCROMTOM.wsCROMTOM
        Dim documentstatus As String = String.Empty

        Try
            documentstatus = ws.ChangeStatus_Reject(0, login.reportid, login.submissionid)

            Return documentstatus.ToLower = "rejected"

        Catch ex As Exception
            Dim bal As New NOIBAL
            bal.LogError(bp, New Exception("Error changing the status of CromErr submission file to Reject.", ex))
        End Try

        Return False
    End Function

    Public Shared Function IsDocumentSigned(ByVal bp As BasePage, ByVal login As LogInDetails) As Boolean
        Dim ws As New wsCROMTOM.wsCROMTOM
        Dim documentstatus As String = String.Empty
        Dim bal As New NOIBAL
        Try
            documentstatus = ws.DocumentStatus(0, login.reportid, login.submissionid)
            'bal.LogError(bp, New Exception("THE STATUS IS: " + documentstatus.ToString + " : " + login.reportid.ToString + " : " + login.submissionid))

            Return documentstatus.ToLower = "signed"

        Catch ex As Exception

            bal.LogError(bp, New Exception("Error check the signed status of CromErr submission file", ex))
        End Try

        Return False
    End Function


    Public Function AddProjectToCROMERR(ByVal ProjectNumber As String, ProjectName As String, ReportID As Integer, userid As String) As Boolean
        Dim ws As New wsCROMTOM.wsCROMTOM
        Dim count As Integer = 0
        Try
            count = ws.AddUpdateProject(ProjectNumber, ProjectName, ReportID, True, 0)
        Catch ex As Exception
            Dim bal As New NOIBAL
            bal.LogError(Nothing, New Exception("Error inserting the Project in CROMERR. " + ProjectNumber, ex))
        End Try
        Return count
    End Function

    Public Shared Function GetAdminGroupEmail(ByVal reportid As NOIProgramType) As String
        Dim ws As New wsCROMTOM.wsCROMTOM
        Dim email As String = String.Empty
        Try
            email = ws.GetAdminGroupEmailByReportID(reportid)
        Catch ex As Exception
            Dim bal As New NOIBAL
            bal.LogError(Nothing, New Exception("Error in admin group email from CROMERR for Report:  " + reportid, ex))
        End Try
        Return email
    End Function

    Public Shared Sub GetAdminEmailAndPhone(ByVal reportid As NOIProgramType, ByRef deptname As String, ByRef email As String, ByRef phone As String)
        Dim ws As New wsCROMTOM.wsCROMTOM

        Try
            ws.GetAdminDeptDetailsByReportID(reportid, deptname, email, phone)
        Catch ex As Exception
            Dim bal As New NOIBAL
            bal.LogError(Nothing, New Exception("Error in admin email and phone from CROMERR for Report:  " + Convert.ToString(reportid), ex))
        End Try

    End Sub

End Class
