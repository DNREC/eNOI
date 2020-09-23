Public Class LogOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'log user out
        Session.Abandon()
        FormsAuthentication.SignOut()

        'got to the requested page
        If Not String.IsNullOrEmpty(Request.QueryString("RedirectURL")) Then
            Response.Redirect(Request.QueryString("RedirectURL"))
        Else
            Response.Redirect("~/home.aspx?ReportID=" & Request.QueryString("ReportID").ToString())
        End If
    End Sub

End Class