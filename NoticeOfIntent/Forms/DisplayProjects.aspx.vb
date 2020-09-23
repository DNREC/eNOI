Imports System.Web.ModelBinding
Imports System.Runtime.InteropServices
Public Class DisplayProjects
    Inherits FormBasePage


    Public Overrides Sub UIPrep()

        lblAdminDeptName.Text = CacheLookupData.GetDeptName()
        Select Case CType(logInVS.submissiontype, NOISubmissionType)
            Case NOISubmissionType.CoPermittee, NOISubmissionType.TerminateNOI, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                mvExistingPermits.ActiveViewIndex = 0
            Case NOISubmissionType.TerminateCoPermittee
                mvExistingPermits.ActiveViewIndex = 1
        End Select
    End Sub


    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvSubmissionByUser_GetData(<Control("txtProjectNumber")> ProjectNumber As String, <Control("txtProjectName")> ProjectName As String, <Control("txtPermitNumber")> PermitNumber As String,
                                               ByVal maximumRows As Integer, ByVal startRowIndex As Integer, <Out()> ByRef totalRowCount As Integer, ByVal sortByExpression As String) As IEnumerable(Of NoticeOfIntent.NOIProject)

        Dim lstNOIProject As New List(Of NOIProject)

        If isExternal = True Then
            Dim projectnames As String = String.Empty
            projectnames = CacheLookupData.GetUserProjectList(logInVS)
            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim bal As New SWBAL()
                    lstNOIProject = bal.GetProjectsByUser(ProjectNumber, ProjectName, PermitNumber, projectnames, maximumRows, startRowIndex, totalRowCount)
                Case NOIProgramType.ISGeneralPermit
                    Dim bal As New ISWBAL()
                    lstNOIProject = bal.GetProjectsByUser(ProjectNumber, ProjectName, PermitNumber, projectnames, maximumRows, startRowIndex, totalRowCount)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim bal As New APBAL()
                    lstNOIProject = bal.GetProjectsByUser(ProjectNumber, ProjectName, PermitNumber, projectnames, maximumRows, startRowIndex, totalRowCount)

            End Select
        Else
            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim bal As New SWBAL()
                    lstNOIProject = bal.GetAllProjectsOfGeneralNOI(ProjectNumber, ProjectName, PermitNumber, maximumRows, startRowIndex, totalRowCount)
                Case NOIProgramType.ISGeneralPermit
                    Dim bal As New ISWBAL()
                    lstNOIProject = bal.GetAllProjectsOfGeneralNOI(ProjectNumber, ProjectName, PermitNumber, maximumRows, startRowIndex, totalRowCount)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim bal As New APBAL()
                    lstNOIProject = bal.GetAllProjectsOfGeneralNOI(ProjectNumber, ProjectName, PermitNumber, maximumRows, startRowIndex, totalRowCount)
            End Select
        End If
        Return lstNOIProject
    End Function


    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvCoPermitSubmissionByUser_GetData(<Control("txtProjectNumber")> ProjectNumber As String, <Control("txtProjectName")> ProjectName As String, <Control("txtPermitNumber")> PermitNumber As String) As IQueryable(Of NoticeOfIntent.ProjectOwnerView)

        Dim bal As New SWBAL
        Try
            If isExternal = True Then
                Dim projectnames As String = String.Empty
                projectnames = CacheLookupData.GetUserProjectList(logInVS)
                Return bal.GetAllApprovedCoPermitSubmissionsByUser(ProjectNumber, ProjectName, PermitNumber, projectnames)
            Else
                Return bal.GetAllApprovedCoPermitSubmissions(ProjectNumber, ProjectName, PermitNumber)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Private Sub lvSubmissionByUser_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles lvSubmissionByUser.ItemCommand
        If e.CommandName = "Add" Then
            Dim commandArgs As IList(Of String) = e.CommandArgument.ToString().Split(",").ToList()
            Dim projectid As Integer = CType(commandArgs(0), Integer)
            'Dim projectid As Integer = CType(e.CommandArgument, Integer)
            Select Case CType(logInVS.submissiontype, NOISubmissionType)
                Case NOISubmissionType.CoPermittee
                    responseRedirect("~/Forms/CoPermitteApplication.aspx?proid=" & projectid.ToString)
                Case NOISubmissionType.TerminateNOI
                    Dim copermitteeslst As List(Of NOIOwner) = Nothing
                    If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                        Dim bal As New AdminBAL
                        'copermitteeslst = New List(Of NOIOwner)
                        copermitteeslst = bal.GetNOICoPermitteesByPermitNumber(commandArgs(1))
                    End If

                    If copermitteeslst Is Nothing OrElse copermitteeslst.Count = 0 Then
                        responseRedirect("~/Forms/NOTGeneralPermit.aspx?proid=" & projectid.ToString)
                    Else
                        ErrorSummary.AddError("Need to terminate the Co-Permittees before terminating the NOI", Page)
                    End If
                Case NOISubmissionType.GeneralNOICorrection
                    responseRedirect("~/Forms/NOIApplication.aspx?proid=" + projectid.ToString())
                Case NOISubmissionType.GeneralNOIRenewal
                    responseRedirect("~/Forms/NOIApplication.aspx?proid=" + projectid.ToString())
            End Select

        End If
    End Sub

    Private Sub lvCoPermitSubmissionByUser_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles lvCoPermitSubmissionByUser.ItemCommand
        If e.CommandName = "Add" Then
            Dim commandArgs As IList(Of String) = e.CommandArgument.ToString().Split(",").ToList()
            responseRedirect("~/Forms/NOTCoPermittee.aspx?pnum=" & commandArgs(0) & "&aid=" & commandArgs(1))
        End If
    End Sub

    'Private Sub lvGeneralPermitSubmissionByUser_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles lvGeneralPermitSubmissionByUser.ItemCommand
    '    If e.CommandName = "Add" Then
    '        Dim commandArgs As IList(Of String) = e.CommandArgument.ToString().Split(",").ToList()
    '        Dim projectid As Integer = CType(commandArgs(0), Integer)

    '        Select Case logInVS.reportid
    '            Case NOIProgramType.CSSGeneralPermit
    '                Dim bal As New AdminBAL
    '                Dim copermitteeslst As New List(Of NOIOwner)
    '                copermitteeslst = bal.GetNOICoPermitteesByPermitNumber(commandArgs(1))
    '                If copermitteeslst.Count = 0 Then
    '                    responseRedirect("~/Forms/NOTGeneralPermit.aspx?proid=" & projectid.ToString)
    '                Else
    '                    ErrorSummary.AddError("Need to terminate the Co-Permittees before terminating the NOI", Page)
    '                End If
    '            Case NOIProgramType.ISGeneralPermit, NOIProgramType.PesticideGeneralPermit
    '                responseRedirect("~/Forms/NOTGeneralPermit.aspx?proid=" & projectid.ToString)
    '        End Select
    '    End If
    'End Sub


    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        If isExternal = True Then
            responseRedirect("~/Forms/main.aspx")
        Else
            responseRedirect("~/Admin/Submissions.aspx")
        End If
    End Sub

    Private Sub lvSubmissionByUser_ItemCreated(sender As Object, e As ListViewItemEventArgs) Handles lvSubmissionByUser.ItemCreated
        Dim lbtnAddCoPermittee As LinkButton = CType(e.Item.FindControl("lbtnAddCoPermittee"), LinkButton)
        Select Case CType(logInVS.submissiontype, NOISubmissionType)
            Case NOISubmissionType.TerminateNOI
                lbtnAddCoPermittee.Text = "Add Termination"
            Case NOISubmissionType.GeneralNOICorrection
                lbtnAddCoPermittee.Text = "Add NOI Correction"
            Case NOISubmissionType.GeneralNOIRenewal
                lbtnAddCoPermittee.Text = "Add NOI Renewal"
        End Select

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Select Case CType(logInVS.submissiontype, NOISubmissionType)
            Case NOISubmissionType.CoPermittee, NOISubmissionType.TerminateNOI, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                lvSubmissionByUser.Sort("", SortDirection.Ascending)
            Case NOISubmissionType.TerminateCoPermittee
                lvCoPermitSubmissionByUser.Sort("", SortDirection.Ascending)
        End Select

    End Sub

    'Protected Sub LinqDataSource1_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs)
    '    Dim bal As New SWBAL
    '    Try
    '        If isExternal = True Then
    '            Dim projectnames As String = String.Empty
    '            projectnames = CacheLookupData.GetUserProjectList(logInVS)
    '            e.Result = bal.GetAllApprovedCoPermitSubmissionsByUser(projectnames)
    '        Else
    '            e.Result = bal.GetAllApprovedCoPermitSubmissions()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
End Class