Public Class SessionNotification
    Inherits BasePage

    Private Sub SessionNotification_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Try
            Dim strResponse As String = String.Empty
            'Dim success As Boolean
            Dim convFee As Double
            Dim logInVS As New LogInDetails
            Dim avsresponse As String = String.Empty
            Dim param As String = String.Empty

            If Request.QueryString("action") <> "notification" Then
                Session("remittance_Id") = Request.QueryString("remittance_id")
                strResponse = Payment.paymentSessionComplete(Session("remittance_Id").ToString())
                logInVS = LogInDetails.deserialize(strResponse)
                param = LogInDetails.serialize(logInVS)
                Session("param") = param
            End If

            Select Case Request.QueryString("action")
                Case "notification"
                    'Set Session Variables

                    If Char.IsLetter(Request("avs_response")) = True Then
                        avsresponse = Request("avs_response").ToString()
                    Else
                        avsresponse = String.Empty
                    End If
                    convFee = IIf(Request("convenience_fee_amount") = "", 0.0, Request("convenience_fee_amount"))

                    strResponse = Payment.PaymentSessionNotification(Me, Request.Form("application_id"), Request.Form("message_version"), Request.Form("security_id").ToString,
                                        Request.Form("remittance_id").ToString, Request("transaction_status"), avsresponse, Request("fail_code"), Request("payment_type"), Request("card_type"),
                                         convFee, Request("amount"), Request("total_amount"), Request("billing_firstname"), Request("billing_lastname"), Request("billing_address"),
                                         Request("billing_city"), Request("billing_state"), Request("billing_zip"))

                    Response.Clear()
                    Response.ContentType = "application/x-www-form-urlencoded"
                    Response.Write(strResponse)
                Case "success"
                    FormsAuthentication.SetAuthCookie(logInVS.user.userid, False)
                    Response.Redirect("~/Forms/SessionSuccessfull.aspx?P=" & Server.UrlEncode(param), False)
                Case "fail"
                    FormsAuthentication.SetAuthCookie(logInVS.user.userid, False)
                    Response.Redirect("~/Forms/SessionFailure.aspx?P=" & Server.UrlEncode(param), False)
                Case "exit"  'Get Exit V-Relay URL parameter for application and redirect user to it
                    FormsAuthentication.SetAuthCookie(logInVS.user.userid, False)
                    Response.Redirect("~/Forms/SubmissionDetails.aspx?P=" & Server.UrlEncode(param), False)
                Case "cfail"  'Get Continue after fail URL parameter for application and redirect user to it
                    FormsAuthentication.SetAuthCookie(logInVS.user.userid, False)
                    Response.Redirect("~/Forms/SessionFailure.aspx?P=" & Server.UrlEncode(param), False)
            End Select
        Catch ex As Exception
            Dim bal As New NOIBAL
            bal.LogError(Me, ex)
            Throw (ex)
        End Try
    End Sub
End Class