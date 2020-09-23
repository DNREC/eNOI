Imports System.Web.Configuration
Imports System.Text


Public Class BasePage
    Inherits System.Web.UI.Page


    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("p")) Then
                Dim param As String = Request.QueryString("p")
                Dim login As LogInDetails = LogInDetails.deserialize(param)
                logInVS = login
            End If
        End If
    End Sub

    Private Sub BasePage_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'If Not IsPostBack Then
        '    If Not String.IsNullOrEmpty(Request.QueryString("p")) Then
        '        Dim param As String = Request.QueryString("p")
        '        Dim login As LogInDetails = LogInDetails.deserialize(param)
        '        logInVS = login
        '    End If
        'End If
    End Sub

    Public Property logInVS() As LogInDetails
        Get
            Return ViewState("_logInVS")
        End Get
        Set(ByVal value As LogInDetails)
            ViewState("_logInVS") = value
        End Set
    End Property

#Region "Web.Config Variables"
    ''' <summary>
    ''' return whether the application is used for external users or admins
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isExternal() As Boolean
        Get
            Return CBool(WebConfigurationManager.AppSettings("InDMZ"))
        End Get
    End Property

    ''' <summary>
    ''' returns whether current application is in test mode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isTest() As Boolean
        Get
            Return CBool(WebConfigurationManager.AppSettings("IsTest"))
        End Get
    End Property

    ''' <summary>
    ''' get cromerr login url
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property cromErrLoginURL() As String
        Get
            Return WebConfigurationManager.AppSettings("CromErrLogin")
        End Get
    End Property

    ''' <summary>
    ''' get cromerr new registration url
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property cromErrNewRegistrationURL() As String
        Get
            Return WebConfigurationManager.AppSettings("CromErrNewUser")
        End Get
    End Property
    ''' <summary>
    ''' get cromerr admin url
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public ReadOnly Property cromErrAdminURL() As String
        Get
            Return WebConfigurationManager.AppSettings("CromErrAdminURL")
        End Get
    End Property

    ''' <summary>
    ''' Get the registered application id for doing Online pay
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getRegisteredAppIDForOnlinePay() As String
        Get
            Dim appid As String = CacheLookupData.GetPaymentAppID(logInVS.reportid)
            Return appid     'WebConfigurationManager.AppSettings("OnlinePayAppID").ToString() "1562" '
        End Get
    End Property

    ''' <summary>
    ''' Get the Version number of the Online Pay Application.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getVersionOfOnlinePayApplication() As String
        Get
            Dim appversion As String = CacheLookupData.GetNOIPrograms().Where(Function(a) a.ProgramID = logInVS.reportid).Select(Function(b) b.OnlinePayAppVersion).Single().ToString()
            Return appversion     'WebConfigurationManager.AppSettings("OnlinePayAppVersion").ToString() "2.3" '
        End Get
    End Property

    ''' <summary>
    ''' get the application administrator email address
    ''' </summary>
    Public ReadOnly Property applicationAdminEmail() As String
        Get
            Return WebConfigurationManager.AppSettings("ApplicationAdminEmail").ToString()
        End Get
    End Property


    ''' <summary>
    ''' get the NOI administrator email address
    ''' </summary>
    Public ReadOnly Property NOIAdminEmail() As String
        Get
            Return CROMERRExchange.GetAdminGroupEmail(logInVS.reportid)  'WebConfigurationManager.AppSettings("NOIAdminEmailGroup").ToString()
        End Get
    End Property


    'Public ReadOnly Property NOIAdminPhone() As String
    '    Get
    '        Return WebConfigurationManager.AppSettings("NOIAdminPhone").ToString()
    '    End Get
    'End Property

    Public ReadOnly Property SendMailConfirmation() As Boolean
        Get
            Return WebConfigurationManager.AppSettings("SendConfirmationMails").ToString()
        End Get
    End Property

    'Public ReadOnly Property GetAppID() As String
    '    Get
    '        Return WebConfigurationManager.AppSettings("AppID").ToString()
    '    End Get
    'End Property

#End Region

    Public Sub responseRedirect(ByVal path As String, Optional ByVal addParameter As Boolean = True)
        If logInVS IsNot Nothing AndAlso addParameter Then
            Dim param As String = LogInDetails.serialize(logInVS)
            Session.Add(param, param)
            If path.IndexOf("?") > 0 Then
                Response.Redirect(path & "&p=" & Server.UrlEncode(param), False)
            Else
                Response.Redirect(path & "?p=" & Server.UrlEncode(param), False)
            End If
        Else
            Response.Redirect(path, False)
        End If
    End Sub

    Public Function GetResponseRedirect(ByVal path As String, Optional ByVal addparameter As Boolean = True) As String
        Dim url As String = path
        If logInVS IsNot Nothing AndAlso addparameter Then
            Dim param As String = LogInDetails.serialize(logInVS)
            Session.Add(param, param)
            If path.IndexOf("?") > 0 Then
                url = url & "&p=" & Server.UrlEncode(param)
            Else
                url = url & "?p=" & Server.UrlEncode(param)
            End If
        End If
        Return url
    End Function


    Public Sub registerErrorAndSendEmail(ByVal e As Exception)
        Try
            Dim bal As New SWBAL

            'save the unhandled error in the database
            Dim ticketNum As Integer = bal.LogError(Me, e)

            'send email to administrator
            If ticketNum > 0 Then
                Dim email As New StringBuilder("An error occured in the Online Notice Of Intent application.")
                email.Append(vbCrLf)
                email.Append(vbCrLf)
                email.Append("The error ID is: ")
                email.Append(ticketNum)

                Emailer.SendEmailToAdministrator(email.ToString())
            End If
            'Return True
        Catch ex As Exception
            Throw ex
        End Try

        'Return False
    End Sub


End Class
