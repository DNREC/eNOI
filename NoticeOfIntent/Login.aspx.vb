Public Class Login
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim login As New LogInDetails()
            With login
                .loginToken = Request.QueryString("Tkt")
                .reportid = Request.QueryString("ReportID")
            End With

            Dim userprojectlist As String = String.Empty

            'validate login
            If NOILogin.isValidToken(login, userprojectlist) Then

                Session("userprojectlist") = userprojectlist
                logInVS = login

                'create a login cookie
                FormsAuthentication.SetAuthCookie(logInVS.user.userid, False)
                CacheLookupData.LoadStaticSessionCache()
                'go to main page
                responseRedirect("~/Forms/Main.aspx")
            Else
                'go to invalid user error page
                Response.Redirect("~/Error/userauthenticationfailed.aspx")
            End If
        Catch ex As Exception
            Throw New Exception("Exception in login page.", ex)
        End Try
    End Sub

End Class