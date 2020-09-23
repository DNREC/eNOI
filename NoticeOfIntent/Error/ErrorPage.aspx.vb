Public Class ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Label1.Text = "The Application administrator has been notified. On emergency please contact the administrator at " & CacheLookupData.GetDeptEmail()   'ConfigurationManager.AppSettings("NOIAdminContactEmail").ToString()
        End If
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        Response.Redirect("~/default.aspx")
    End Sub
End Class