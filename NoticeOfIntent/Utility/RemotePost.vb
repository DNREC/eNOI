Imports Microsoft.VisualBasic

Public Class RemotePost
    Private Inputs As New System.Collections.Specialized.NameValueCollection()

    Public Url As String = ""
    Public Method As String = "post"
    Public FormName As String = "form1"

    Public Sub Add(ByVal name As String, ByVal value As String)
        Inputs.Add(name, value)
    End Sub

    Public Sub Post()
        System.Web.HttpContext.Current.Response.Clear()

        System.Web.HttpContext.Current.Response.Write("<html><head>")

        System.Web.HttpContext.Current.Response.Write(String.Format("</head><body onload=""document.{0}.submit()"">", FormName))


        System.Web.HttpContext.Current.Response.Write(String.Format("<form name=""{0}"" method=""{1}"" action=""{2}"" >", FormName, Method, Url))
        For i As Integer = 0 To Inputs.Keys.Count - 1
            System.Web.HttpContext.Current.Response.Write(String.Format("<input name=""{0}"" type=""hidden"" value=""{1}"">", Inputs.Keys(i), Inputs(Inputs.Keys(i))))
        Next
        System.Web.HttpContext.Current.Response.Write("</form>")
        System.Web.HttpContext.Current.Response.Write("</body></html>")
        System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest()
        'System.Web.HttpContext.Current.Response.[End]()
    End Sub
End Class

