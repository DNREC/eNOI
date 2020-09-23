Public Class NOIBAL
    Private ReadOnly _noiRepository As NOIRepository

    Public Sub New()
        _noiRepository = New NOIRepository()
    End Sub


    Public Function GetStates() As IList(Of StateAbvlst)
        Return _noiRepository.GetStates
    End Function

    Public Function GetCompanyType() As IList(Of CompanyTypelst)
        Return _noiRepository.GetCompanyType()
    End Function

    Public Function GetNOIPrograms() As IList(Of NOIProgram)
        Return _noiRepository.GetNOIPrograms
    End Function

    Public Function GetSubmissionTypeList() As IList(Of NOISubmissionTypelst)
        Return _noiRepository.GetSubmissionTypeList()
    End Function

    Public Function GetNOIProgSubmissionTypes() As IList(Of NOIProgSubmissionType)
        Return _noiRepository.GetNOIProgSubmissionTypes()
    End Function


    Public Function GetSubmissionStatusCodes() As IList(Of NOISubmissionStatusCode)
        Return _noiRepository.GetSubmissionStatusCodes()
    End Function
    Public Sub RemoveSessionStorageByUser(user As String)
        _noiRepository.RemoveSessionStorageByUser(user)
    End Sub


    Public Function GetSessionStorageByUser(user As String) As String
        Return _noiRepository.GetSessionStorageByUser(user)
    End Function

    Public Sub SetSessionStorageByUser(user As String, SessionStorage As String)
        _noiRepository.SetSessionStorageByUser(user, SessionStorage)
    End Sub

    Public Function GetAgreementTextByProgSubType(progSubmissionTypeID As Integer) As IList(Of NOIProgSubmissionType)
        Return _noiRepository.GetAgreementTextByProgSubType(progSubmissionTypeID)
    End Function
    Public Function GetSubmissionStatusesBySubmissionID(subid As Integer) As IQueryable
        Return _noiRepository.GetSubmissionStatusesBySubmissionID(subid)
    End Function
    Public Function LogError(bp As BasePage, ByVal e As Exception) As Integer
        Dim subID As Integer = 0
        Dim rtn As Integer = 0

        If bp IsNot Nothing Then
            If bp.logInVS IsNot Nothing Then subID = bp.logInVS.submissionid
            rtn = _noiRepository.LogError(bp.Request.Browser.Browser, bp.Request.Url.PathAndQuery, e.Source, common.getError(e), subID, common.getErrorStackTrace(e))
        Else
            rtn = _noiRepository.LogError(String.Empty, String.Empty, e.Source, common.getError(e), subID, common.getErrorStackTrace(e))
        End If
        Return rtn
    End Function

    Public Function GetAllProjects(ByVal programid As Integer) As IQueryable(Of NOIProject)
        Return _noiRepository.GetAllProjects(programid)
    End Function
    Public Function GetAllExemptionCodes(programid As Integer) As IQueryable(Of NOIFeeExemption)
        Return _noiRepository.GetAllExemptionCodes(programid)
    End Function
    Public Sub SaveExemptionCode(ExemptionCode As NOIFeeExemption)
        _noiRepository.SaveExemptionCode(ExemptionCode)
    End Sub

    Public Sub DeleteExemptionCode(ExemptionId As Integer)
        _noiRepository.DeleteExemptionCode(ExemptionId)
    End Sub

    Public Function GetNOIWebPageCtrlTxt() As IList(Of NOIWebPage)
        Return _noiRepository.GetNOIWebPageCtrlTxt()
    End Function


End Class
