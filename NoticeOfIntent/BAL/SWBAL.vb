Public Class SWBAL

    Private ReadOnly _submissionRepository As SWRepository

    Public Sub New()
        _submissionRepository = New SWRepository()
    End Sub

    'Public Function GetCompanyType() As IList(Of CompanyTypelst)
    '    Return _submissionRepository.GetCompanyType()
    'End Function


    Public Function GetProjectType() As IQueryable(Of ProjectTypelst)
        Return _submissionRepository.GetProjectType
    End Function


    Public Function GetSWBMPList() As IList(Of SWBMPlst)
        Return _submissionRepository.GetSWBMPList()
    End Function

    'Public Function GetStates() As IList(Of StateAbvlst)
    '    Return _submissionRepository.GetStates
    'End Function


    Public Function GetPlanApprovalAgencyList() As IList(Of PlanApprovalAgency)
        Return _submissionRepository.GetPlanApprovalAgencyList()
    End Function


    Public Function GetTaxKentHundred() As IList(Of TaxKentHundred)
        Return _submissionRepository.GetTaxKentHundred
    End Function


    Public Function GetTaxKentTowns() As IList(Of TaxKentTown)
        Return _submissionRepository.GetTaxKentTowns
    End Function


    Public Function GetNOIPrograms() As IQueryable(Of NOIProgram)
        Return _submissionRepository.GetNOIPrograms
    End Function
    ''verified
    Public Function GetAllSubmissionsByUserByProjects(user As String, commadelimitedProjectnames As String) As IQueryable(Of NOISubmissionSearchlst)
        Return _submissionRepository.GetAllSubmissionsByUserByProjects(user, commadelimitedProjectnames)
    End Function


    Public Function GetAllSubmissionsForAdmin() As IQueryable(Of NOISubmissionSearchlst)
        Return _submissionRepository.GetAllSubmissionsForAdmin()
    End Function

    Public Function GetSubmissionByRefForAdmin(ByVal refno As Integer) As NOISubmissionSearchlst
        Return _submissionRepository.GetSubmissionByRefForAdmin(refno)
    End Function

    Public Function GetAllSubmissionsForAdmin(ByVal ProjectName As String, ByVal PermitNumber As String,
                                          ByVal Owners As String, ByVal SubmissionType As Integer, ByVal ReferenceNo As String, SubmissionStatusCode As String,
                                              ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOISubmissionSearchlst)
        Return _submissionRepository.GetAllSubmissionsForAdmin(ProjectName, PermitNumber, Owners, SubmissionType, ReferenceNo, SubmissionStatusCode, maximumRows, startRowIndex, totalRowCount)
    End Function


    Public Function GetAllApprovedCoPermitSubmissions(ProjectNumber As String, ProjectName As String, PermitNumber As String) As IQueryable(Of ProjectOwnerView)
        Return _submissionRepository.GetAllApprovedCoPermitSubmissions(ProjectNumber, ProjectName, PermitNumber)
    End Function


    Public Function GetAllApprovedCoPermitSubmissionsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String) As IEnumerable(Of ProjectOwnerView)
        Return _submissionRepository.GetAllApprovedCoPermitSubmissionsByUser(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames)
    End Function


    Public Function GetProjectsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        Return _submissionRepository.GetProjectsByUser(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames, maximumRows, startRowIndex, totalRowCount)
    End Function



    Public Function GetAllProjectsOfGeneralNOI(ProjectNumber As String, ProjectName As String, PermitNumber As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        Return _submissionRepository.GetAllProjectsOfGeneralNOI(ProjectNumber, ProjectName, PermitNumber, maximumRows, startRowIndex, totalRowCount)
    End Function

    Public Function GetAllProjects() As IQueryable(Of NOIProject)
        Return _submissionRepository.GetAllProjects()
    End Function

    Public Function GetAllExemptionCodes(programid As Integer) As IQueryable(Of NOIFeeExemption)
        Return _submissionRepository.GetAllExemptionCodes(programid)
    End Function

    ''verified
    Public Function GetSubmissionByIDForGeneralNOI(submissionid As Integer) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForGeneralNOI(submissionid)
    End Function

    ''verified
    Public Function GetSubmissionByIdForCoPermittee(submissionid As Integer) As NOISubmission
        Return _submissionRepository.GetSubmissionByIdForCoPermittee(submissionid)
    End Function

    ''verified
    Public Function GetSubmissionByIDForCoPermitteeNOT(submissionid As Integer) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForCoPermitteeNOT(submissionid)
    End Function


    Public Function GetSubmissionByIDForNOT(submissionid As Integer) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForNOT(submissionid)
    End Function

    ''verified
    Public Function GetSubmissionByIDForDisplay(submissionid As Integer, subtype As NOISubmissionType) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForDisplay(submissionid, subtype)
    End Function


    ''verified
    Public Function GetProjectOwnerDetailsByProjectIDForCoPermittee(projectid As Integer) As NOISubmission
        Return _submissionRepository.GetProjectOwnerDetailsByProjectIDForCoPermittee(projectid)
    End Function


    'Public Function GetProjectOwnerDetailsByProjectID(projectid As Integer) As NOISubmission
    '    Return _submissionRepository.GetProjectOwnerDetailsByProjectID(projectid)
    'End Function

    Public Function GetProjectOwnerDetailsByProjectIDForNOT(projectid As Integer) As NOISubmission
        Return _submissionRepository.GetProjectOwnerDetailsByProjectIDForNOT(projectid)
    End Function

    Public Function GetProjectDetailsForNOICorrectionAndRenewalByProjectID(projectid As Integer) As NOISubmission
        Return _submissionRepository.GetProjectDetailsForNOICorrectionAndRenewalByProjectID(projectid)
    End Function


    ''verified
    Public Function GetProjectCopermitteeDetailsByProjectID(permitnumber As String, AfflID As Int32) As NOISubmission
        Return _submissionRepository.GetProjectCopermitteeDetailsByProjectID(permitnumber, AfflID)
    End Function


    'Public Function GetSubmissionTypeList() As IQueryable(Of NOISubmissionTypelst)
    '    Return _submissionRepository.GetSubmissionTypeList()
    'End Function

    'Public Function GetNOIProgSubmissionTypes() As IQueryable(Of NOIProgSubmissionType)
    '    Return _submissionRepository.GetNOIProgSubmissionTypes()
    'End Function


    'Public Function GetSubmissionStatusCodes() As IQueryable(Of NOISubmissionStatusCode)
    '    Return _submissionRepository.GetSubmissionStatusCodes()
    'End Function


    Public Function GetSubmissionStatusesBySubmissionID(subid As Integer) As IQueryable
        Return _submissionRepository.GetSubmissionStatusesBySubmissionID(subid)
    End Function

    Public Function GetSubmissionTypeDetails(submissionTypeID As NOISubmissionType) As NOISubmissionTypelst
        Return _submissionRepository.GetSubmissionTypeDetails(submissionTypeID)
    End Function


    'Public Function GetSessionStorageByUser(user As String) As String
    '    Return _submissionRepository.GetSessionStorageByUser(user)
    'End Function

    'Public Sub SetSessionStorageByUser(user As String, SessionStorage As String)
    '    _submissionRepository.SetSessionStorageByUser(user, SessionStorage)
    'End Sub


    'Public Sub RemoveSessionStorageByUser(user As String)
    '    _submissionRepository.RemoveSessionStorageByUser(user)
    'End Sub

    ''verified
    Public Function Insert(submission As NOISubmission) As NOISubmission
        Return _submissionRepository.Insert(submission)
    End Function

    ''verified
    Public Sub Delete(submissionid As Integer)
        _submissionRepository.Delete(submissionid)
    End Sub

    Public Sub InsertSubmissionStatus(submissionstatus As NOISubmissionStatus)
        _submissionRepository.InsertSubmissionStatus(submissionstatus)
    End Sub

    Public Sub SaveExemptionCode(ExemptionCode As NOIFeeExemption)
        _submissionRepository.SaveExemptionCode(ExemptionCode)
    End Sub

    Public Sub DeleteExemptionCode(ExemptionId As Integer)
        _submissionRepository.DeleteExemptionCode(ExemptionId)
    End Sub

    Public Function LogError(bp As BasePage, ByVal e As Exception) As Integer
        Dim subID As Integer = 0
        Dim rtn As Integer = 0
        If bp.logInVS IsNot Nothing Then subID = bp.logInVS.submissionid
        If bp IsNot Nothing Then
            rtn = _submissionRepository.LogError(bp.Request.Browser.Browser, bp.Request.Url.PathAndQuery, e.Source, common.getError(e), subID, common.getErrorStackTrace(e))
        Else
            rtn = _submissionRepository.LogError(String.Empty, String.Empty, e.Source, common.getError(e), subID, common.getErrorStackTrace(e))
        End If
        Return rtn
    End Function

End Class
