Public Class APBAL

    Private ReadOnly _submissionRepository As APRepository

    Public Sub New()
        _submissionRepository = New APRepository()
    End Sub

    Public Function GetAPEntityType() As IList(Of NOIEntityType)
        Return _submissionRepository.GetAPEntityType()
    End Function

    Public Function GetPesticidePatterns() As IList(Of NOIPesticidePattern)
        Return _submissionRepository.GetPesticidePatterns()
    End Function

    Public Sub Delete(submissionid As Integer)
        _submissionRepository.Delete(submissionid)
    End Sub
    Public Function GetAllSubmissionsByUserByProjects(user As String, commadelimitedProjectnames As String) As IQueryable(Of NOISubmissionSearchlst)
        Return _submissionRepository.GetAllSubmissionsByUserByProjects(user, commadelimitedProjectnames)
    End Function

    Public Function GetAllSubmissionsForAdmin(ByVal ProjectName As String, ByVal PermitNumber As String,
                                          ByVal Owners As String, ByVal SubmissionType As Integer, ByVal ReferenceNo As String, ByVal SubmissionStatusCode As String,
                                              ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOISubmissionSearchlst)
        Return _submissionRepository.GetAllSubmissionsForAdmin(ProjectName, PermitNumber, Owners, SubmissionType, ReferenceNo, SubmissionStatusCode, maximumRows, startRowIndex, totalRowCount)
    End Function

    Public Function GetAllSubmissionsForAdmin() As IQueryable(Of NOISubmissionSearchlst)
        Return _submissionRepository.GetAllSubmissionsForAdmin()
    End Function

    Public Function GetSubmissionByRefForAdmin(ByVal refno As Integer) As NOISubmissionSearchlst
        Return _submissionRepository.GetSubmissionByRefForAdmin(refno)
    End Function

    Public Function GetSubmissionByIDForDisplay(submissionid As Integer, subtype As NOISubmissionType) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForDisplay(submissionid, subtype)
    End Function

    Public Function GetSubmissionByIDForGeneralNOI(submissionid As Integer) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForGeneralNOI(submissionid)
    End Function


    Public Function GetSubmissionByIDForNOT(submissionid As Integer) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForNOT(submissionid)
    End Function

    Public Function GetProjectOwnerDetailsByProjectIDForNOT(projectid As Integer) As NOISubmission
        Return _submissionRepository.GetProjectOwnerDetailsByProjectIDForNOT(projectid)
    End Function

    Public Function GetProjectDetailsForNOICorrectionAndRenewalByProjectID(projectid As Integer) As NOISubmission
        Return _submissionRepository.GetProjectDetailsForNOICorrectionAndRenewalByProjectID(projectid)
    End Function

    Public Function GetProjectsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        Return _submissionRepository.GetProjectsByUser(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames, maximumRows, startRowIndex, totalRowCount)
    End Function

    Public Function GetAllProjectsOfGeneralNOI(ProjectNumber As String, ProjectName As String, PermitNumber As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        Return _submissionRepository.GetAllProjectsOfGeneralNOI(ProjectNumber, ProjectName, PermitNumber, maximumRows, startRowIndex, totalRowCount)
    End Function

    Public Function Insert(submission As NOISubmission) As NOISubmission
        Return _submissionRepository.Insert(submission)
    End Function

End Class
