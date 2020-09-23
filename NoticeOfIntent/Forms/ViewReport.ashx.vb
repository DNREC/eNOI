Imports System.Web
Imports System.Web.Services

Public Class ViewReport
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim report As New ReportServerDMZ.ReportExecutionService
        Dim creds As New System.Net.NetworkCredential()
        'Dim wc As New System.Net.WebClient


        creds.UserName = ConfigurationManager.AppSettings("ReportUserName")
        creds.Password = ConfigurationManager.AppSettings("ReportPassword")
        creds.Domain = ConfigurationManager.AppSettings("ReportDomain")
        report.Credentials = creds

        Dim result() As Byte = Nothing
        Dim credentials() As ReportServerDMZ.DataSourceCredentials = Nothing
        Dim warnings() As ReportServerDMZ.Warning = Nothing
        Dim parameters(1) As ReportServerDMZ.ParameterValue
        Dim sh As New ReportServerDMZ.ExecutionHeader
        report.ExecutionHeaderValue = sh

        parameters(0) = New ReportServerDMZ.ParameterValue()
        parameters(0).Name = "progid"
        parameters(0).Value = context.Request.QueryString("progid")
        Dim afflid As Int32 = context.Request.QueryString("afflid")
        If afflid > 0 Then
            parameters(1) = New ReportServerDMZ.ParameterValue()
            parameters(1).Name = "afflid"
            parameters(1).Value = afflid
        End If

        report.LoadReport(context.Request.QueryString("reportURL"), Nothing)
        report.SetExecutionParameters(parameters, Nothing)
        result = _
        report.Render("PDF", Nothing, "PDF", Nothing, Nothing, Nothing, Nothing)

        context.Response.AddHeader("content-disposition", String.Format("inline; filename=""{0}""", String.Concat("Report.", "PDF")))
        context.Response.ContentType = "Application/pdf"
        'response.Clear()
        context.Response.BinaryWrite(result)
        context.Response.End()
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class