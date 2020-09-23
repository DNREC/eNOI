Imports System.IO

Public Class SessionFailure
    Inherits FormBasePage

    Private Sub SessionFailure_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Dim failcode As Integer = 0
        Dim remittanceid As String = Session("remittance_id").ToString()
        'failcode = Payment.GetFailcode(Me, remittanceid)
        failcode = Payment.GetFailcode(remittanceid)


        If failcode > 0 Then
            If failcode = 100 Then
                'fetch the AVS response code description. Address Verification service is Void
                'lblTranFailMsg.Text = Payment.GetDescByAVSCode(Me, remittanceid)
                lblTranFailMsg.Text = Payment.GetDescByAVSCode(remittanceid)
            Else
                'lblTranFailMsg.Text = Payment.GetDescByFailCode(Me, remittanceid)
                lblTranFailMsg.Text = Payment.GetDescByFailCode(remittanceid)
            End If
        Else
            lblTranFailMsg.Text = "The transaction is failed due to server issues. Please try later."
        End If
    End Sub

    'Protected Sub RemoveReceipt()
    '    Payment.RemoveReceipt(Me, Session("remittance_id"))
    'End Sub


    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        'RemoveReceipt()
        responseRedirect("~/Forms/SubmissionDetails.aspx")
    End Sub
End Class