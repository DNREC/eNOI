Imports System
Imports System.IO

Public Class SessionInitiate
    Inherits FormBasePage


    Public Overrides Sub UIPrep()
        Try
            Dim submission As NOISubmission = IndNOISubmission

            Dim amountpaid As Decimal = IIf(submission.AmountPaid IsNot Nothing, submission.AmountPaid, 0.0)
            lblRequiredPayment.Text = String.Format("{0:c}", (CacheLookupData.GetFeesBySubmissionType(submission.ProgSubmissionTypeID) - amountpaid))
            txtRequiredPayment.Text = String.Format("{0:c}", (CacheLookupData.GetFeesBySubmissionType(submission.ProgSubmissionTypeID) - amountpaid))

            If Me.isTest Then
                txtRequiredPayment.Visible = True
                ViewState("RequiredPayment") = Decimal.Parse(txtRequiredPayment.Text, Globalization.NumberStyles.Currency).ToString()
            Else
                txtRequiredPayment.Visible = False
                ViewState("RequiredPayment") = CacheLookupData.GetFeesBySubmissionType(submission.ProgSubmissionTypeID) - amountpaid
            End If

            Dim billee As NOISubmissionPersonOrg = submission.NOISubmissionPersonOrg.Where(Function(a) a.NOIPersonOrgTypeID = NOIPersonOrgType.BilleeDetails).FirstOrDefault()
            If billee IsNot Nothing Then
                txtFirstName.Text = billee.FName
                txtLastName.Text = billee.LName
                txtAddress1.Text = billee.Address1
                txtCity.Text = billee.City
                ddlStateAbv.SelectedValue = billee.StateAbv
                txtZip.Text = billee.PostalCode
                txtEmail.Text = billee.EmailAddress
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function GetStates() As IList(Of StateAbvlst)
        Return CacheLookupData.GetStates()
    End Function

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        'Dim strURL As String
        Dim myremotepost As RemotePost = Nothing
        Try
            Dim submission As NOISubmission = IndNOISubmission
            Dim amountpaid As Decimal = IIf(submission.AmountPaid IsNot Nothing, submission.AmountPaid, 0.0)
            If Me.isTest Then
                ViewState("RequiredPayment") = Decimal.Parse(txtRequiredPayment.Text, Globalization.NumberStyles.Currency).ToString()
            Else
                ViewState("RequiredPayment") = CacheLookupData.GetFeesBySubmissionType(submission.ProgSubmissionTypeID) - amountpaid
            End If

            'strURL = Payment.PaymentSessionInitiate(Me, Me.getRegisteredAppIDForOnlinePay, Me.getVersionOfOnlinePayApplication, Me.txtEmail.Text, Me.logInVS.submissionid, txtFirstName.Text, txtLastName.Text, txtAddress1.Text, txtCity.Text, ddlStateAbv.SelectedValue, txtZip.Text, Convert.ToDecimal(ViewState("RequiredPayment")), LogInDetails.serialize(Me.logInVS))
            myremotepost = Payment.PaymentSessionInitiate(Me, Me.getRegisteredAppIDForOnlinePay, Me.getVersionOfOnlinePayApplication, Me.txtEmail.Text, Me.logInVS.submissionid, txtFirstName.Text, txtLastName.Text, txtAddress1.Text, txtCity.Text, ddlStateAbv.SelectedValue, txtZip.Text, Convert.ToDecimal(ViewState("RequiredPayment")), LogInDetails.serialize(Me.logInVS))

            If myremotepost IsNot Nothing Then ' Len(strURL) > 0 Then
                'Send response to Govolutions
                'Response.Clear()
                'Response.ClearContent()
                'Response.ClearHeaders()

                'Response.Redirect(strURL, False)
                'Dim parts As String() = strURL.Split(New Char() {"?"c})
                'Dim url As String = ""
                'Dim remittanceid As String = ""
                'Dim i As Integer = 0
                '' Loop through result strings with For Each
                'Dim strData As String
                'For Each strData In parts
                '    i = i + 1
                '    If i = 1 Then
                '        url = strData

                '    End If
                'Next

                'remittanceid = Session("remittance_Id")

                'If Len(strURL) > 0 Then

                '    Dim myremotepost As New RemotePost()
                '    myremotepost.Url = url
                '    myremotepost.Add("application_id", getRegisteredAppIDForOnlinePay)
                '    myremotepost.Add("message_version", getVersionOfOnlinePayApplication)
                '    myremotepost.Add("remittance_id", remittanceid)
                myremotepost.Post()
                'Response.Redirect("SessionVerification.aspx")
                'Else
                '    'Master.addError("Server Error. Please try later.")
                'End If


            Else
                ErrorSummary.AddError("Server Error. Please try later.", Me)
            End If
        Catch ex As Exception
            Dim bal As New NOIBAL()
            bal.LogError(Me, ex)
            Throw ex
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        responseRedirect("~/Forms/SubmissionDetails.aspx")
    End Sub
End Class