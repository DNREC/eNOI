Public Class ManageProjects
    Inherits FormBasePage

    Public Overrides Sub UIPrep()
        hfReportID.Value = logInVS.reportid
    End Sub


    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvProjects_GetData() As IQueryable(Of NoticeOfIntent.NOIProject)

        Dim bal As New NOIBAL
        Return bal.GetAllProjects(logInVS.reportid).OrderByDescending(Function(a) a.ProjectID)
    End Function



    Private Sub btnImportProject_Click(sender As Object, e As EventArgs) Handles btnImportProject.Click
        Dim admindb As New AdminBAL
        Dim projectnumber As String = String.Empty
        Try
            projectnumber = admindb.ImportProjectFromDEN(hfProgID.Value, logInVS.reportid, logInVS.user.userid)
            If projectnumber <> String.Empty Then
                Dim CROMERRMgr As New CROMERRExchange
                CROMERRMgr.AddProjectToCROMERR(projectnumber, hfProjectName.Value, logInVS.reportid, logInVS.user.userid)
                lvProjects.DataBind()
                txtIntProject.Text = String.Empty
            End If

        Catch ex As Exception
            Throw ex
        End Try


    End Sub
End Class