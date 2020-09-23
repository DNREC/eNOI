Public Class ISWBAL

    Private ReadOnly _submissionRepository As ISWRepository

    Public Sub New()
        _submissionRepository = New ISWRepository()
    End Sub

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

    Public Function GetNoExposureList(submission As NOISubmission) As IList
        Return _submissionRepository.GetNoExposureList(submission)
    End Function

    Public Function GetSubmissionByIDForNOT(submissionid As Integer) As NOISubmission
        Return _submissionRepository.GetSubmissionByIDForNOT(submissionid)
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

    Public Function GetProjectsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        Return _submissionRepository.GetProjectsByUser(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames, maximumRows, startRowIndex, totalRowCount)
    End Function

    Public Function GetAllProjectsOfGeneralNOI(ProjectNumber As String, ProjectName As String, PermitNumber As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        Return _submissionRepository.GetAllProjectsOfGeneralNOI(ProjectNumber, ProjectName, PermitNumber, maximumRows, startRowIndex, totalRowCount)
    End Function

    Public Function GetUploadedDocuments(submissionid As Integer) As IQueryable(Of NOISubmissionDocs)
        Return _submissionRepository.GetUploadedDocuments(submissionid)
    End Function

    Public Function GetUploadedDocumentByNOIDocID(noidocid As Integer, submissionid As Integer) As NOISubmissionDocs
        Return _submissionRepository.GetUploadedDocumentByNOIDocID(noidocid, submissionid)
    End Function

    Public Sub DeleteFile(NOIDocID As Integer, SubmissionID As Integer)
        _submissionRepository.DeleteFile(NOIDocID, SubmissionID)
    End Sub

    Public Sub InsertFile(submissionid As Integer, documentName As String, documentDesc As String, documentType As String, document As Byte())
        _submissionRepository.InsertFile(submissionid, documentName, documentDesc, documentType, document)
    End Sub

    Public Function Insert(submission As NOISubmission) As NOISubmission
        Return _submissionRepository.Insert(submission)
    End Function
End Class
