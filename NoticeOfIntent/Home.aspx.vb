Imports System.Web.ModelBinding

Public Class Home
    Inherits BasePage



    Private Sub Home_InitComplete(sender As Object, e As EventArgs) Handles Me.InitComplete
        'If Not IsPostBack Then
        '    Select Case CType(Request.QueryString("ReportID").ToString(), NOIProgramType)
        '        Case NOIProgramType.CSSGeneralPermit
        '            divExistingPermit.Visible = True
        '        Case NOIProgramType.ISGeneralPermit
        '            divExistingPermit.Visible = False
        '    End Select
        'End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            showProperPanel()

            Select Case CType(Request.QueryString("ReportID").ToString(), NOIProgramType)
                Case NOIProgramType.CSSGeneralPermit
                    SetupUIforCSSGeneralPermit()
                Case NOIProgramType.ISGeneralPermit
                    SetupUIforISGeneralPermit()
                Case NOIProgramType.PesticideGeneralPermit
                    SetupUIforPesticideGeneralPermit()
            End Select
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        
        If ConfigurationManager.AppSettings("dev") = True Then
            Response.Redirect("Login.aspx?Tkt=" & HttpUtility.UrlEncode("G3VEbztSmpJOJATLW8BpV/7SPzZku8M+7xIQfe3rXaE=") & "&ReportID=" & Request.QueryString("ReportID").ToString(), False)
        Else
            'Production code
            Response.Redirect(cromErrLoginURL & "?ReportID=" & Request.QueryString("ReportID").ToString(), False)
        End If

    End Sub


    Private Sub btnNewUser_Click(sender As Object, e As EventArgs) Handles btnNewUser.Click
        'Response.Redirect(cromErrNewRegistrationURL & "?ReportID=" & GetAppID(), False)
        Response.Redirect(cromErrNewRegistrationURL & "?ReportID=" & Request.QueryString("ReportID").ToString(), False)
    End Sub

    Private Sub btnSubmissions_Click(sender As Object, e As EventArgs) Handles btnSubmissions.Click
        authenticateAdmin()
        CacheLookupData.LoadStaticSessionCache()
        responseRedirect("~/Admin/Submissions.aspx")
    End Sub



    Private Sub SetupUIforCSSGeneralPermit()
        divISSearch.Visible = False
        divISNOIList.Visible = False
        ddlPlanApprovalAgency.DataBind()
        hylUserGuide.NavigateUrl = "~/UserGuide/eNOI_Guide_Construction.pdf"
        lblHeaderText.Text = "Storm Water Discharges Associated with CONSTRUCTION ACTIVITY under a NPDES General Permit"
        lblHeaderTextInternal.Text = "Storm Water Discharges Associated with CONSTRUCTION ACTIVITY under a NPDES General Permit"
        lblListHeaderText.Text = "Storm Water Discharges Associated with CONSTRUCTION ACTIVITY under a NPDES General Permit"

    End Sub
    Private Sub SetupUIforISGeneralPermit()
        If isExternal Then
            divExistingPermit.Visible = False
        Else
            divCSSearch.Visible = False
            divCSNOIList.Visible = False
        End If
        ddlPlanApprovalAgency.DataBind()
        divuserguide.Visible = False
        lblHeaderText.Text = "Storm Water Discharges Associated with INDUSTRIAL ACTIVITY under a NPDES General Permit"
        lblHeaderTextInternal.Text = "Storm Water Discharges Associated with INDUSTRIAL ACTIVITY under a NPDES General Permit"
        lblListHeaderText.Text = "Storm Water Discharges Associated with INDUSTRIAL ACTIVITY under a NPDES General Permit"
    End Sub

    Private Sub SetupUIforPesticideGeneralPermit()
        If isExternal Then
            divExistingPermit.Visible = False
        Else
            divCSSearch.Visible = False
            divCSNOIList.Visible = False
        End If
        ddlPlanApprovalAgency.DataBind()
        'divuserguide.Visible = False
        hylUserGuide.NavigateUrl = "~/UserGuide/eNOI_Guide_Pesticides.pdf"
        lblHeaderText.Text = "Aquatic Pesticides Application under a NPDES General Permit"
        lblHeaderTextInternal.Text = "Aquatic Pesticides Application under a NPDES General Permit"
        lblListHeaderText.Text = "Aquatic Pesticides Application under a NPDES General Permit"
    End Sub

    Private Sub showProperPanel()
        If isExternal Then
            pnlExternal.Visible = True
            pnlInternal.Visible = False
        Else
            pnlExternal.Visible = False
            pnlInternal.Visible = True
        End If
    End Sub

    Private Sub authenticateAdmin()
        'create a login cookie
        logInVS = NOILogin.getAdminLogin(Request.QueryString("ReportID").ToString())
        FormsAuthentication.SetAuthCookie(logInVS.user.userid, False)
    End Sub


    Public Function GetPlanApprovalAgency() As List(Of PlanApprovalAgency)
        'If Request.QueryString("ReportID") = NOIProgramType.CSSGeneralPermit Then
        Dim bal As New SWBAL
        Return bal.GetPlanApprovalAgencyList()
        'Else
        'Return Nothing
        'End If
    End Function

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvNOISearch_GetData(<Control("txtPermitNumber")> ProgID As String, <Control("txtProjectName")> PiName As String,
                                        <Control("ddlPlanApprovalAgency")> DelegateAgency As Int32, <Control("txtRecdDateFrom")> StartDate As String,
                                        <Control("txtRecdDateTo")> EndDate As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer,
                                        <System.Runtime.InteropServices.Out()> ByRef totalRowCount As Integer, sortByExpression As String) As IEnumerable(Of NOIPublicView)

        If Request.QueryString("ReportID") = NOIProgramType.CSSGeneralPermit Then
            Dim bal As New AdminBAL
            'Return bal.GetNOISearch(Nothing, Nothing, 0, Nothing, Nothing)
            'RegisterAsyncTask(New PageAsyncTask())
            Return bal.GetNOISearch(ProgID, PiName, DelegateAgency, StartDate, EndDate, maximumRows, startRowIndex, totalRowCount)
        Else
            Return Nothing
        End If

    End Function

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvProjectOwners_GetData(<Control> hfPiID As Integer) As IQueryable(Of NOIOwner)
        If Request.QueryString("ReportID") = NOIProgramType.CSSGeneralPermit Then
            Dim bal As New AdminBAL
            Return bal.GetNOIOwnerByPIID(hfPiID)
        Else
            Return Nothing
        End If
    End Function


    Protected Sub lvProjectOwners_ItemCommand(sender As Object, e As ListViewCommandEventArgs)

        Dim reportid As Integer = Request.QueryString("ReportID")

        If e.CommandName = "view" Then
            Dim param() As String = e.CommandArgument.ToString.Split(",")


            If isExternal Then
                'Dim buffer() As Byte = Nothing
                'buffer = NOIReports.createReport(param(0), Convert.ToInt32(param(1)), CType(Convert.ToInt16(param(2)), EISSqlAfflType))
                'Response.Clear()
                'Response.ClearHeaders()
                'Response.ContentType = "application/pdf"
                'Response.BinaryWrite(buffer)
                'Response.Flush()
                'Response.Redirect(NOIReports.createReport(param(0), Convert.ToInt32(param(1)), CType(Convert.ToInt16(param(2)), EISSqlAfflType)))

                Response.Write("<script>window.open('" + NOIReports.createReport(param(1), Convert.ToInt32(param(2)), CType(Convert.ToInt16(param(3)), EISSqlAfflType), reportid) + "','_blank');</script>")
            Else
                'eissql edit
                authenticateAdmin()
                CacheLookupData.LoadStaticSessionCache()
                logInVS.currentdb = AdminCurrentDB.EISSQL
                logInVS.submissionid = Convert.ToInt32(param(0))
                logInVS.reportid = reportid

                Select Case CType(Convert.ToInt16(param(3)), EISSqlAfflType)
                    Case EISSqlAfflType.Owner
                        responseRedirect("~/Admin/GeneralNOI.aspx")
                    Case EISSqlAfflType.Copermittee
                        responseRedirect("~/Admin/CoPermitteNOI.aspx?id=" & param(2))
                End Select
            End If
        ElseIf e.CommandName = "viewempty" Then
            If isExternal Then
                Dim permitnumber As String = CType(CType(sender, ListView).NamingContainer.NamingContainer, ListView).DataKeys(CType(CType(sender, ListView).NamingContainer, ListViewDataItem).DataItemIndex).Values(1)
                Response.Write("<script>window.open('" + NOIReports.createReport(permitnumber, 0, EISSqlAfflType.Owner, reportid) + "','_blank');</script>")
            Else
                Dim piid As Integer = CType(CType(sender, ListView).NamingContainer.NamingContainer, ListView).DataKeys(CType(CType(sender, ListView).NamingContainer, ListViewDataItem).DataItemIndex).Values(0)
                'eissql edit
                authenticateAdmin()
                CacheLookupData.LoadStaticSessionCache()
                logInVS.currentdb = AdminCurrentDB.EISSQL
                logInVS.submissionid = Convert.ToInt32(piid)
                logInVS.reportid = reportid
                responseRedirect("~/Admin/GeneralNOI.aspx")

            End If
        End If
    End Sub

    Private Sub btnSubmitToEPA_Click(sender As Object, e As EventArgs) Handles btnSubmitToEPA.Click
        authenticateAdmin()
        CacheLookupData.LoadStaticSessionCache()
        Response.Redirect("~/Admin/SendDataToEPA.aspx")
    End Sub

    Private Sub btnManageUser_Click(sender As Object, e As EventArgs) Handles btnManageUser.Click
        authenticateAdmin()
        CacheLookupData.LoadStaticSessionCache()
        Response.Redirect(cromErrAdminURL)
    End Sub

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvISNOISearch_GetData(<Control("txtPermitNumber1")> ProgID As String, <Control("txtProjectName1")> PiName As String,
                                          <Control("txtRecdDateFrom1")> StartDate As String, <Control("txtRecdDateTo1")> EndDate As String,
                                          ByVal maximumRows As Integer, ByVal startRowIndex As Integer,
                                        <System.Runtime.InteropServices.Out()> ByRef totalRowCount As Integer, sortByExpression As String) As IEnumerable(Of NoticeOfIntent.NOIPublicViewForIS)



        Dim bal As New AdminBAL

        '<QueryString> ReportID As Nullable(Of Integer),
        Dim ReportID As Integer = Request.QueryString("ReportID")
        If ReportID = NOIProgramType.ISGeneralPermit Or ReportID = NOIProgramType.PesticideGeneralPermit Then
            Return bal.GetISNOISearch(ProgID, PiName, StartDate, EndDate, ReportID, maximumRows, startRowIndex, totalRowCount)
        Else
            Return Nothing
        End If

    End Function

    Protected Sub lvISNOISearch_ItemCommand(sender As Object, e As ListViewCommandEventArgs)
        If e.CommandName = "view" Then
            Dim param() As String = e.CommandArgument.ToString.Split(",")
            Dim reportid As Integer = Request.QueryString("ReportID")

            If isExternal Then
                'Dim buffer() As Byte = Nothing
                'buffer = NOIReports.createReport(param(0), Convert.ToInt32(param(1)), CType(Convert.ToInt16(param(2)), EISSqlAfflType))
                'Response.Clear()
                'Response.ClearHeaders()
                'Response.ContentType = "application/pdf"
                'Response.BinaryWrite(buffer)
                'Response.Flush()
                'Response.Redirect(NOIReports.createReport(param(0), Convert.ToInt32(param(1)), CType(Convert.ToInt16(param(2)), EISSqlAfflType)))

                Response.Write("<script>window.open('" + NOIReports.createReport(param(1), Convert.ToInt32(param(2)), CType(Convert.ToInt16(param(3)), EISSqlAfflType), reportid) + "','_blank');</script>")
            Else
                'eissql edit
                authenticateAdmin()
                CacheLookupData.LoadStaticSessionCache()
                logInVS.currentdb = AdminCurrentDB.EISSQL
                logInVS.submissionid = Convert.ToInt32(param(0))

                Select Case CType(Convert.ToInt16(param(3)), EISSqlAfflType)
                    Case EISSqlAfflType.Owner
                        responseRedirect("~/Admin/GeneralNOI.aspx")
                    Case EISSqlAfflType.Copermittee
                        responseRedirect("~/Admin/CoPermitteNOI.aspx")
                End Select
            End If
        End If
    End Sub

    Private Sub Home_Error(sender As Object, e As EventArgs) Handles Me.[Error]
        Dim s As String
        s = "test"
        ' Me.registerErrorAndSendEmail(Server.GetLastError)
        'Response.Redirect("~/Error/ErrorPage.aspx")
    End Sub
End Class
