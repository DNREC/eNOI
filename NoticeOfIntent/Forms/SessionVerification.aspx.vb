
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class SessionVerification
    Inherits BasePage

    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Dim strResponse As String = String.Empty

        Try

            'strResponse = Payment.PaymentSessionVerification(Me, Me.getRegisteredAppIDForOnlinePay, Me.getVersionOfOnlinePayApplication, Request.Form("remittance_id"), Request.Form("security_id"), Request.ServerVariables("REMOTE_ADDR"))
            strResponse = Payment.PaymentSessionVerification(Me, Request.Form("application_id"), Request.Form("message_version"), Request.Form("remittance_id"), Request.Form("security_id"), Request.ServerVariables("REMOTE_ADDR"))

            'Send response to Govolutions
            Response.Clear()
            Response.ContentType = "application/x-www-form-urlencoded"
            Response.Write(strResponse)
        Catch ex As Exception
            'Dim ex1 As Exception = ex
            'While Not (ex1 Is Nothing)
            '    ex1 = ex1.InnerException()
            'End While
            Dim bal As New NOIBAL()
            bal.LogError(Me, ex)
            Throw ex

        End Try
    End Sub
End Class