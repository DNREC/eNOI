Imports System.Threading.Tasks

Public Class AdminBAL
    Private ReadOnly _adminrepository As AdminRepository

    Public Sub New()
        _adminrepository = New AdminRepository()
    End Sub


    Public Function GetZipCodes() As IList(Of Zipcodes)
        Return _adminrepository.GetZipCodes()
    End Function


    Public Function GetPermitStatusCodeLst() As List(Of PermitStatusCodeLst)
        Return _adminrepository.GetPermitStatusCodeLst()
    End Function


    Public Function AcceptGeneralNOI(logInVS As LogInDetails) As String
        Return _adminrepository.AcceptGeneralNOI(logInVS)
    End Function


    Public Function AcceptCoPermitteeNOI(logInVS As LogInDetails) As String
        Return _adminrepository.AcceptCoPermitteeNOI(logInVS)
    End Function


    Public Function AcceptCoPermitteeNOT(logInVS As LogInDetails) As String
        Return _adminrepository.AcceptCoPermitteeNOT(logInVS)
    End Function


    Public Function AcceptGeneralNOT(logInVS As LogInDetails) As String
        Return _adminrepository.AcceptGeneralNOT(logInVS)
    End Function

    Public Function AcceptModifiedGeneralNOI(logInVS As LogInDetails) As String
        Return _adminrepository.AcceptModifiedGeneralNOI(logInVS)
    End Function

    Public Function AcceptRenewalGeneralNOI(logInVS As LogInDetails) As String
        Return _adminrepository.AcceptRenewalGeneralNOI(logInVS)
    End Function

    Public Function GetInteralProjectList(projectname As String) As List(Of NOIProjectInternal)
        Return _adminrepository.GetInternalProjectList(projectname)
    End Function

    Public Function GetInteralProjectList(projectname As String, reportid As Integer) As List(Of NOIProjectInternal)
        Return _adminrepository.GetInternalProjectList(projectname, reportid)
    End Function

    ''verified
    Public Function GetInternalProjectListByName(projectname As String, pitypeid As Integer) As List(Of String)
        Return _adminrepository.GetInternalProjectListByName(projectname, pitypeid)
    End Function


    Public Function ImportProjectFromDEN(permitnumber As String, programid As Integer, user As String) As String
        Return _adminrepository.ImportProjectFromDEN(permitnumber, programid, user)
    End Function


    Public Function GetNOISearch(ProgID As String, PiName As String, DelegateAgency As Int32, StartDate As String, EndDate As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIPublicView)
        Return _adminrepository.GetNOISearch(ProgID, PiName, DelegateAgency, StartDate, EndDate, maximumRows, startRowIndex, totalRowCount)
    End Function

    'Public Async Function GetNOISearchAsync(ProgID As String, PiName As String, DelegateAgency As Int32, StartDate As String, EndDate As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer) As Task(Of IEnumerable(Of NOIPublicView))
    '    Return _adminrepository.GetNOISearchAsync(ProgID, PiName, DelegateAgency, StartDate, EndDate, maximumRows, startRowIndex)
    'End Function

    Public Function GetISNOISearch(ProgID As String, PiName As String, StartDate As String, EndDate As String, ReportID As Integer, maximumRows As Integer,
                                    ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIPublicViewForIS)
        Return _adminrepository.GetISNOISearch(ProgID, PiName, StartDate, EndDate, ReportID, maximumRows, startRowIndex, totalRowCount)

    End Function


    Public Function GetNOIOwnerByPIID(Piid As Integer) As IQueryable(Of NOIOwner)
        Return _adminrepository.GetNOIOwnerByPIID(Piid)
    End Function

    ''verified
    Public Function GetNOICoPermitteesByPermitNumber(permitnumber As String) As IList(Of NOIOwner)
        Return _adminrepository.GetNOICoPermitteesByPermitNumber(permitnumber)
    End Function


    Public Function GetGeneralNOIByPIId(Piid As Integer) As NOIProjectInternal
        Return _adminrepository.GetGeneralNOIByPIId(Piid)
    End Function

    Public Function GetGeneralNOIByPIIdForIS(Piid As Integer) As NOIProjectInternal
        Return _adminrepository.GetGeneralNOIByPIIdForIS(Piid)
    End Function

    Public Function GetGeneralNOIByPIIdForAP(Piid As Integer) As NOIProjectInternal
        Return _adminrepository.GetGeneralNOIByPIIdForAP(Piid)
    End Function

    Public Function GetCoPermitteeNOIByPIId(Piid As Integer, Afflid As Integer) As NOIProjectInternal
        Return _adminrepository.GetCoPermitteeNOIByPIId(Piid, Afflid)
    End Function

    Public Function SaveGeneralNOI(ByVal Project As NOIProjectInternal, userid As String) As Boolean
        Return _adminrepository.SaveGeneralNOI(Project, userid)
    End Function

    Public Function SaveCopermitteeNOI(Project As NOIProjectInternal, userid As String) As Boolean
        Return _adminrepository.SaveCopermitteeNOI(Project, userid)
    End Function

    Public Function SaveISGeneralNOI(Project As NOIProjectInternal, userid As String) As Boolean
        Return _adminrepository.SaveISGeneralNOI(Project, userid)
    End Function

    Public Function SaveAPGeneralNOI(project As NOIProjectInternal, userid As String) As Boolean
        Return _adminrepository.SaveAPGeneralNOI(project, userid)
    End Function

End Class
