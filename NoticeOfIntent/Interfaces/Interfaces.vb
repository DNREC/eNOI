Public Interface IEntity
    Property EntityState As EntityState
End Interface

Public Interface ISubmissionRepository
    Inherits IDisposable


    Function GetAllSubmissionsForAdmin() As IQueryable(Of NOISubmissionSearchlst)
    Function GetCompanyType() As IList(Of CompanyTypelst)
    Function GetProjectType() As IQueryable(Of ProjectTypelst)
    Function GetStates() As IList(Of StateAbvlst)
    Function GetSubmissionTypeList() As IQueryable(Of NOISubmissionTypelst)
    Function GetSessionStorageByUser(user As String) As String
    Sub SetSessionStorageByUser(user As String, sessionstorage As String)
    Function Insert(submission As NOISubmission) As NOISubmission
    Function Update(submission As NOISubmission) As NOISubmission
    Sub Delete(submissionid As Integer)



End Interface


