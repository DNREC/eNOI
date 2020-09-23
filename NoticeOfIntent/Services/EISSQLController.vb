Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json

Public Class EISSQLController
    Inherits ApiController

    'Private ReadOnly db As New AdminRepository()

    ' GET api/<controller>/zip

    <HttpGet> _
    <Route("api/Zipcodes/{zip}")> _
    Public Function GetValue(ByVal zip As String) As Zipcodes
        Return CacheLookupData.GetCityStateAbvByZip(zip)
    End Function

    <HttpGet> _
    <Route("api/ZipcodesDE/{zip}")> _
    Public Function GetCityStateAbvByZipInDE(ByVal zip As String) As Zipcodes
        Return CacheLookupData.GetCityStateAbvByZipInDE(zip)
    End Function

    '<HttpGet> _
    '<Route("api/ZipcodesDE/{zip}")> _
    'Public Function GetCityStateAbvByZipInDE(ByVal zip As String) As String
    '    Return JsonConvert.SerializeObject(CacheLookupData.GetCityStateAbvByZipInDE(zip))
    'End Function

    <HttpGet>
    <Route("api/ExistingProjects/{projectname}")>
    Public Function GetExistingProjectInNOI(ByVal projectname As String) As List(Of NOIProjectInternal)
        Dim abal As New AdminBAL
        'Return JsonConvert.SerializeObject(abal.GetInteralProjectList(projectname))
        Return abal.GetInteralProjectList(projectname)
    End Function


    <HttpGet>
    <Route("api/ExistingProjects")>
    Public Function GetExistingProjectByReportID(<FromUri> ByVal projectname As String, <FromUri> ByVal reportid As Integer) As List(Of NOIProjectInternal)
        Dim abal As New AdminBAL
        Return abal.GetInteralProjectList(projectname, reportid)
    End Function

    <HttpGet>
    <Route("api/ProjectExists")>
    Public Function IsProjectNameExists(<FromUri> ByVal pname As String, <FromUri> ByVal pitypeid As Integer) As List(Of String)
        Dim abal As New AdminBAL
        Return abal.GetInternalProjectListByName(pname, pitypeid)
    End Function


End Class
