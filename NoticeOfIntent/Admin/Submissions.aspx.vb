Imports System.Web.ModelBinding
Imports System.Runtime.InteropServices
Public Class Submissions
    Inherits FormBasePage


    Private Sub Submissions_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'ddlSubmissionType1.DataBind()
        End If

    End Sub


    Public Function GetSubmissionTypeList() As IList(Of NOIProgSubmissionType)
        Return CacheLookupData.GetNOIProgSubmissionTypes().Where(Function(a) a.ProgramID = logInVS.reportid).ToList()
    End Function

    Public Function GetSubmissionStatusCodeList() As IList(Of NOISubmissionStatusCode)
        Return CacheLookupData.GetSubmissionStatusCodes()
    End Function

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvSubmissions_GetData(<Control("txtProjectName")> ProjectName As String, <Control("txtPermitNumber")> PermitNumber As String,
                                          <Control("txtOwner")> Owners As String, <Control("ddlSubmissionType1")> SubmissionType As Integer,
                                           <Control("txtReferenceNo")> ReferenceNo As String, <Control("ddlSubmissionStatusCode")> StatusCode As String,
                                          ByVal maximumRows As Integer, ByVal startRowIndex As Integer, <Out()> ByRef totalRowCount As Integer, ByVal sortByExpression As String) As IEnumerable(Of NoticeOfIntent.NOISubmissionSearchlst)

        If hffiltercheat.Value = "1" Then
            'StatusCode = SubmissionStatusCode.A.ToString() 'Admin user's request. changed their mind later to show all instead of just the filed ones on load
            hffiltercheat.Value = "0"
        End If
        Select Case logInVS.reportid
            Case NOIProgramType.CSSGeneralPermit
                Dim bal As New SWBAL()
                Return bal.GetAllSubmissionsForAdmin(ProjectName, PermitNumber, Owners, SubmissionType, ReferenceNo, StatusCode, maximumRows, startRowIndex, totalRowCount)
            Case NOIProgramType.ISGeneralPermit
                Dim bal As New ISWBAL()
                Return bal.GetAllSubmissionsForAdmin(ProjectName, PermitNumber, Owners, SubmissionType, ReferenceNo, StatusCode, maximumRows, startRowIndex, totalRowCount)
            Case NOIProgramType.PesticideGeneralPermit
                Dim bal As New APBAL()
                Return bal.GetAllSubmissionsForAdmin(ProjectName, PermitNumber, Owners, SubmissionType, ReferenceNo, StatusCode, maximumRows, startRowIndex, totalRowCount)
        End Select
        Return Nothing
    End Function


    '' The return type can be changed to IEnumerable, however to support
    '' paging and sorting, the following parameters must be added:
    ''     ByVal maximumRows as Integer
    ''     ByVal startRowIndex as Integer
    ''     ByRef totalRowCount as Integer
    ''     ByVal sortByExpression as String
    'Public Function lvSubmissions_GetData() As IQueryable(Of NoticeOfIntent.NOISubmissionSearchlst)
    '    Dim bal As New SubmissionBAL
    '    Return bal.GetAllSubmissionsForAdmin()
    'End Function

    Private Sub lvSubmissions_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles lvSubmissions.ItemCommand

        If e.CommandName = "select" Then
            Dim refno As Integer = CType(e.CommandArgument, Integer)
            Dim submission As NOISubmissionSearchlst = Nothing
            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim bal As New SWBAL
                    submission = bal.GetSubmissionByRefForAdmin(refno)
                Case NOIProgramType.ISGeneralPermit
                    Dim bal As New ISWBAL
                    submission = bal.GetSubmissionByRefForAdmin(refno)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim bal As New APBAL
                    submission = bal.GetSubmissionByRefForAdmin(refno)
            End Select

            logInVS.submissionid = submission.ReferenceNo
            logInVS.submissiontype = CType(submission.SubmissionTypeID, NOISubmissionType)
            logInVS.progsubmisssiontype = submission.ProgSubmissionTypeID
            Select Case CType(submission.SubmissionTypeID, NOISubmissionType)
                Case NOISubmissionType.GeneralNOIPermit, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                    responseRedirect("~/Forms/NOIApplication.aspx") '?ReportID=" + logInVS.reportid.ToString())
                Case NOISubmissionType.CoPermittee
                    responseRedirect("~/Forms/CoPermitteApplication.aspx")
                Case NOISubmissionType.TerminateCoPermittee
                    responseRedirect("~/Forms/NOTCoPermittee.aspx")
                Case NOISubmissionType.TerminateNOI
                    responseRedirect("~/Forms/NOTGeneralPermit.aspx")
            End Select
        ElseIf e.CommandName = "select1" Then
            Dim refno As Integer = CType(e.CommandArgument, Integer)
            Dim submissiontypeid As NOISubmissionType = lvSubmissions.DataKeys(e.Item.DisplayIndex).Values(1)
            Dim submission As NOISubmission = Nothing
            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim bal As New SWBAL
                    submission = bal.GetSubmissionByIDForDisplay(refno, submissiontypeid)
                Case NOIProgramType.ISGeneralPermit
                    Dim bal As New ISWBAL
                    submission = bal.GetSubmissionByIDForDisplay(refno, submissiontypeid)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim bal As New APBAL
                    submission = bal.GetSubmissionByIDForDisplay(refno, submissiontypeid)
            End Select
            IndNOISubmission = submission
            logInVS.submissionid = submission.SubmissionID
            logInVS.submissiontype = submissiontypeid
            logInVS.progsubmisssiontype = submission.ProgSubmissionTypeID
            responseRedirect("~/Forms/SubmissionDetails.aspx")

        ElseIf e.CommandName = "add1" Then
            Dim ddlSubmissionType As DropDownList = CType(e.Item.FindControl("ddlSubmissionType"), DropDownList)
            logInVS.progsubmisssiontype = ddlSubmissionType.SelectedValue
            logInVS.submissiontype = CType(CacheLookupData.GetNOIProgSubmissionTypes().Single(Function(a) a.ProgSubmissionTypeID = ddlSubmissionType.SelectedValue).SubmissionTypeID, NOISubmissionType)
            Select Case logInVS.submissiontype
                Case NOISubmissionType.GeneralNOIPermit
                    responseRedirect("~/Forms/NOIApplication.aspx") '?ReportID=" + logInVS.reportid.ToString())
                Case NOISubmissionType.CoPermittee, NOISubmissionType.TerminateCoPermittee, NOISubmissionType.TerminateNOI, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                    responseRedirect("~/Forms/DisplayProjects.aspx")
            End Select
        ElseIf e.CommandName = "add" Then

            Dim ddlSubmissionType As DropDownList = CType(CType(sender, ListView).FindControl("ddlSubmissionType"), DropDownList)
            logInVS.progsubmisssiontype = ddlSubmissionType.SelectedValue
            logInVS.submissiontype = CType(CacheLookupData.GetNOIProgSubmissionTypes().Single(Function(a) a.ProgSubmissionTypeID = ddlSubmissionType.SelectedValue).SubmissionTypeID, NOISubmissionType)
            Select Case logInVS.submissiontype
                Case NOISubmissionType.GeneralNOIPermit
                    responseRedirect("~/Forms/NOIApplication.aspx") '?ReportID=" + logInVS.reportid.ToString())
                Case NOISubmissionType.CoPermittee, NOISubmissionType.TerminateCoPermittee, NOISubmissionType.TerminateNOI, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                    responseRedirect("~/Forms/DisplayProjects.aspx")
            End Select
        End If
    End Sub

    Private Sub lvSubmissions_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lvSubmissions.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim currentdataitem As NOISubmissionSearchlst = e.Item.DataItem

            If currentdataitem.SubmissionStatusCode = SubmissionStatusCode.F.ToString Or currentdataitem.SubmissionStatusCode = SubmissionStatusCode.A.ToString Then
                lbtnDelete.Enabled = False
            End If

        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvSubmissions_DeleteItem(ByVal ReferenceNo As Integer)

        Select Case logInVS.reportid
            Case NOIProgramType.CSSGeneralPermit
                Dim bal As New SWBAL()
                bal.Delete(ReferenceNo)
            Case NOIProgramType.ISGeneralPermit
                Dim bal As New ISWBAL()
                bal.Delete(ReferenceNo)
            Case NOIProgramType.PesticideGeneralPermit
                Dim bal As New APBAL()
                bal.Delete(ReferenceNo)
        End Select


        lvSubmissions.DataBind()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lvSubmissions.Sort("", SortDirection.Ascending)
    End Sub

    Private Sub ddlSubmissionStatusCode_DataBound(sender As Object, e As EventArgs) Handles ddlSubmissionStatusCode.DataBound
        'ddlSubmissionStatusCode.SelectedValue = "F" 'Admin user's request. changed their mind later to show all instead of just the filed ones on load
    End Sub
End Class