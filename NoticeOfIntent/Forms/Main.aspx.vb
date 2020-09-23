Imports System.Web.ModelBinding

Public Class Main
    Inherits FormBasePage

    Public Overrides Sub UIPrep()
        Session("user") = logInVS.user.userid  ' HttpContext.Current.User.Identity.Name
        lblUser.Text = logInVS.user.fullName
        Select Case logInVS.reportid
            Case NOIProgramType.CSSGeneralPermit
                lblheadertext.Text = "Storm Water Discharges Associated with CONSTRUCTION ACTIVITY under a NPDES General Permit"
            Case NOIProgramType.ISGeneralPermit
                lblheadertext.Text = "Storm Water Discharges Associated with INDUSTRIAL ACTIVITY under a NPDES General Permit"
            Case NOIProgramType.PesticideGeneralPermit
                lblheadertext.Text = "Aquatic Pesticides Application under a NPDES General Permit"
        End Select
    End Sub

    Public Function GetSubmissionTypeList() As IList(Of NOIProgSubmissionType)
        Return CacheLookupData.GetNOIProgSubmissionTypes().Where(Function(a) a.ProgramID = logInVS.reportid).ToList()
    End Function
    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function GetAllSubmissionByUserSearch() As IQueryable(Of NOISubmissionSearchlst)
        'ByVal maximumRows As Integer, ByVal startRowIndex As Integer,
        '<System.Runtime.InteropServices.Out()> ByRef totalRowCount As Integer, sortByExpression As String) As IQueryable(Of NOISubmissionSearchlst)
        Dim projectnames As String = String.Empty
        projectnames = CacheLookupData.GetUserProjectList(logInVS)

        Try
            Select Case logInVS.reportid
                Case NOIProgramType.CSSGeneralPermit
                    Dim bal As New SWBAL()
                    'Return bal.GetAllSubmissionsByUserSearch(logInVS.user.userid, maximumRows, startRowIndex, totalRowCount)

                    Return bal.GetAllSubmissionsByUserByProjects(logInVS.user.userid, projectnames)    'GetAllSubmissionsByUser(logInVS.user.userid)

                Case NOIProgramType.ISGeneralPermit
                    Dim bal As New ISWBAL()
                    'Return bal.GetAllSubmissionsByUserSearch(logInVS.user.userid, maximumRows, startRowIndex, totalRowCount)
                    Return bal.GetAllSubmissionsByUserByProjects(logInVS.user.userid, projectnames)    'GetAllSubmissionsByUser(logInVS.user.userid)
                Case NOIProgramType.PesticideGeneralPermit
                    Dim bal As New APBAL()
                    Return bal.GetAllSubmissionsByUserByProjects(logInVS.user.userid, projectnames)    'GetAllSubmissionsByUser(logInVS.user.userid)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

        Return Nothing
    End Function



    'Public Sub selectsubmission(<Control("lvSubmissionByUser")> ReferenceNo As Integer)
    '    If ReferenceNo <> 0 Then
    '        Dim submission As NOISubmissionSearchlst = GetAllSubmissionByUser(HttpContext.Current.User.Identity.Name).First(Function(e) e.ReferenceNo = ReferenceNo)
    '        Select Case CType(submission.SubmissionType, NOISubmissionType)
    '            Case NOISubmissionType.GeneralNOIPermit
    '                'Response.Redirect("~/Forms/NOIApplication.aspx?refno=" & ReferenceNo)
    '                responseRedirect("~/Forms/NOIApplication.aspx?refno=" & ReferenceNo)
    '            Case NOISubmissionType.CoPermittee
    '                responseRedirect("~/Forms/CoPermitteApplication.aspx?refno=" & ReferenceNo)
    '            Case NOISubmissionType.TerminateCoPermittee
    '                responseRedirect("~/Forms/NOTCoPermittee.aspx?refno=" & ReferenceNo)
    '            Case NOISubmissionType.TerminateNOI
    '                responseRedirect("~/Forms/NOTGeneralPermit.aspx?refno=" & ReferenceNo)
    '        End Select

    '    End If
    'End Sub


    Private Sub lvSubmissionByUser_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles lvSubmissionByUser.ItemCommand
        Dim bal As New SWBAL()
        If e.CommandName = "select" Then
            Dim refno As Integer = CType(e.CommandArgument, Integer)
            Dim submissiontypeid As NOISubmissionType = lvSubmissionByUser.DataKeys(e.Item.DisplayIndex).Values(1)
            'Dim submission As NOISubmissionSearchlst
            'submission = bal.GetAllSubmissionsByUser(HttpContext.Current.User.Identity.Name).Single(Function(o) o.ReferenceNo = refno)
            'submission = bal.GetAllSubmissionsByUser(logInVS.user.userid).Single(Function(o) o.ReferenceNo = refno)

            logInVS.submissionid = refno  ' submission.ReferenceNo
            logInVS.submissiontype = submissiontypeid  ' CType(submission.SubmissionTypeID, NOISubmissionType)
            logInVS.progsubmisssiontype = lvSubmissionByUser.DataKeys(e.Item.DisplayIndex).Values(2)
            'Select Case CType(submission.SubmissionTypeID, NOISubmissionType)
            Select Case submissiontypeid
                Case NOISubmissionType.GeneralNOIPermit, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                    responseRedirect("~/Forms/NOIApplication.aspx")  '    ?ReportID=" + logInVS.reportid.ToString())
                Case NOISubmissionType.CoPermittee
                    responseRedirect("~/Forms/CoPermitteApplication.aspx")
                Case NOISubmissionType.TerminateCoPermittee
                    responseRedirect("~/Forms/NOTCoPermittee.aspx")
                Case NOISubmissionType.TerminateNOI
                    responseRedirect("~/Forms/NOTGeneralPermit.aspx")
            End Select
        ElseIf e.CommandName = "select1" Then
            Dim refno As Integer = CType(e.CommandArgument, Integer)
            Dim submissiontypeid As NOISubmissionType = lvSubmissionByUser.DataKeys(e.Item.DisplayIndex).Values(1)
            logInVS.submissionid = refno  ' submission.ReferenceNo
            logInVS.submissiontype = submissiontypeid  ' CType(submission.SubmissionTypeID, NOISubmissionType)
            logInVS.progsubmisssiontype = lvSubmissionByUser.DataKeys(e.Item.DisplayIndex).Values(2)
            Dim submission As NOISubmission = bal.GetSubmissionByIDForDisplay(refno, logInVS.submissiontype)
            IndNOISubmission = submission
            'If submission.CertificationAgreed = "Y" Then
            '    responseRedirect("~/Forms/SubmissionDetails.aspx")
            'Else
            responseRedirect("~/Forms/NOIAgreement.aspx")
            'End If

        ElseIf e.CommandName = "add1" Then

            Dim ddlSubmissionType As DropDownList = CType(e.Item.FindControl("ddlSubmissionType"), DropDownList) '  CType(CType(sender, ListView).FindControl("ddlSubmissionType"), DropDownList)
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

    Private Sub lvSubmissionByUser_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lvSubmissionByUser.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim currentdataitem As NOISubmissionSearchlst = e.Item.DataItem

            If currentdataitem.SubmissionStatusCode = SubmissionStatusCode.F.ToString Or currentdataitem.SubmissionStatusCode = SubmissionStatusCode.A.ToString Then
                lbtnDelete.Enabled = False
            End If

        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvSubmissionByUser_DeleteItem(ByVal ReferenceNo As Integer)
        Dim bal As New SWBAL()
        bal.Delete(ReferenceNo)
        lvSubmissionByUser.DataBind()
    End Sub


End Class