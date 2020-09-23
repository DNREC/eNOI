Imports Newtonsoft.Json

Public Class FormBasePage
    Inherits BasePage

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'add master page handler
        If Master IsNot Nothing Then
            AddHandler CType(Master, MasterBasePage).MasterButtonClicked, AddressOf masterButtonClick
        End If
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            UIPrep()
            MasterPrep()
            MapEntitiesToFields()
        End If
    End Sub

#Region "Public Properties"
    ''' <summary>
    ''' get whether this page should be in view only
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property isViewOnly() As Boolean
        Get

            If IndNOISubmission IsNot Nothing Then
                If isExternal Then
                    Return (IndNOISubmission.IsLocked = "Y")
                Else
                    Dim substatus As NOISubmissionStatus = IndNOISubmission.NOISubmissionStatus.OrderByDescending(Function(a) a.SubmissionStatusDate).FirstOrDefault()
                    If substatus IsNot Nothing Then
                        Return (substatus.SubmissionStatusCode = SubmissionStatusCode.A.ToString) OrElse _
                                    (substatus.SubmissionStatusCode = SubmissionStatusCode.X.ToString)
                    End If
                End If
            End If
            Return False
        End Get
    End Property

#End Region

    Public Property IndNOISubmission() As NOISubmission
        Get
            Dim bal As New NOIBAL
            Dim submission As NOISubmission

            Dim storedsubmission As String = bal.GetSessionStorageByUser(logInVS.user.userid)
            If storedsubmission <> String.Empty Then
                submission = JsonConvert.DeserializeObject(Of NOISubmission)(storedsubmission)
            Else
                submission = New NOISubmission()

            End If
            Return submission
        End Get
        Set(value As NOISubmission)
            Dim bal As New NOIBAL
            bal.SetSessionStorageByUser(logInVS.user.userid, JsonConvert.SerializeObject(value)) 'HttpContext.Current.User.Identity.Name, JsonConvert.SerializeObject(value))
        End Set
    End Property

    ''' <summary>
    ''' This property is used when accessing the noi's stored in the internal database EISSql.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InternalNOIProject() As NOIProjectInternal
        Get
            Dim inNOIProject As NOIProjectInternal
            If Session("noiprojectinternal") IsNot Nothing Then
                inNOIProject = CType(Session("noiprojectinternal"), NOIProjectInternal)
            Else
                inNOIProject = New NOIProjectInternal()
            End If
            Return inNOIProject
        End Get
        Set(value As NOIProjectInternal)
            Session("noiprojectinternal") = value
        End Set
    End Property


    ''' <summary>
    ''' sets up the controls on the page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub UIPrep()
    End Sub


    ''' <summary>
    ''' copy data from database object to page controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub MapEntitiesToFields()
    End Sub

    ''' <summary>
    ''' sets up the master page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub MasterPrep()
        If Master IsNot Nothing Then
            Dim CurrentMaster As CLFNested = CType(Master, CLFNested)

            If CacheLookupData.GetDeptName() = String.Empty Then
                Response.Redirect("~/Logout.aspx?ReportID=" & logInVS.reportid)
            End If

            CurrentMaster.AdminDeptName = CacheLookupData.GetDeptName()
            CurrentMaster.AdminDeptPhone = CacheLookupData.GetDeptPhone()
            CurrentMaster.AdminDeptEmail = CacheLookupData.GetDeptEmail()

            If isExternal Then
                CurrentMaster.showButton(HeaderButton.DOCUMENT_MANAGEMENT) = True
                CurrentMaster.showButton(HeaderButton.LOGOUT) = True
                CurrentMaster.showButton(HeaderButton.PROJECT_DETAILS) = False
                CurrentMaster.showButton(HeaderButton.CREATE_COUPON) = False
                CurrentMaster.showButton(HeaderButton.SUBMISSIONS) = False
            Else
                CurrentMaster.showButton(HeaderButton.SUBMISSIONS) = True
                CurrentMaster.showButton(HeaderButton.DOCUMENT_MANAGEMENT) = False
                CurrentMaster.showButton(HeaderButton.LOGOUT) = False
                CurrentMaster.showButton(HeaderButton.PROJECT_DETAILS) = True
                CurrentMaster.showButton(HeaderButton.CREATE_COUPON) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' handles master page button click event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub masterButtonClick(sender As Object, e As MasterButtonClickEventArgs)
        If isExternal Then
            Select Case e.clickedButton
                Case HeaderButton.HOME
                    responseRedirect("~/Forms/main.aspx")
                Case HeaderButton.DOCUMENT_MANAGEMENT
                    Response.Redirect("~/Logout.aspx?RedirectURL=" & _
                                Server.UrlEncode(Me.cromErrLoginURL & "?Tkt=" & Server.UrlEncode(logInVS.loginToken)), False)
                Case HeaderButton.LOGOUT
                    Response.Redirect("~/Logout.aspx?ReportID=" & logInVS.reportid)
            End Select
        Else
            Select Case e.clickedButton
                Case HeaderButton.HOME
                    Response.Redirect("~/Home.aspx?ReportID=" & logInVS.reportid)
                Case HeaderButton.SUBMISSIONS
                    responseRedirect("~/Admin/Submissions.aspx")
                Case HeaderButton.PROJECT_DETAILS
                    responseRedirect("~/Admin/ManageProjects.aspx")
                Case HeaderButton.CREATE_COUPON
                    responseRedirect("~/Admin/ManageCoupons.aspx")
            End Select
        End If
    End Sub


End Class
