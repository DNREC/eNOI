Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.HttpApplication
Imports System.Web.UI
Imports System.Xml
Imports System.IO

Module Payment
    '''' <summary>
    '''' To create a Transaction table in the Application to hold the payment transactions.
    '''' </summary>
    '''' <param name="bp">Base page to get the application object to hold the Transaction Datatable.</param>
    '''' <remarks>This function has to be called in the Global.asax or at the time of starting the transaction with check whether the table is existing.</remarks>
    'Private Function CreatePaymentTransactionTable(ByVal bp As Page) As DataTable
    '    If bp.Application("tblTransaction") Is Nothing Then
    '        Dim PrimaryKeyColumns(0) As DataColumn
    '        bp.Application("tblTransaction") = New DataTable("tblTransaction")
    '        With CType(bp.Application("tblTransaction"), DataTable)
    '            .Columns.Add("SessionID", Type.GetType("System.String"))
    '            .Columns.Add("apprelateddata", Type.GetType("System.String"))
    '            .Columns.Add("ordernumber", Type.GetType("System.String"))
    '            .Columns.Add("application_id", Type.GetType("System.String"))
    '            .Columns.Add("message_version", Type.GetType("System.String"))
    '            .Columns.Add("remittance_id", Type.GetType("System.String"))
    '            .Columns.Add("security_id", Type.GetType("System.String"))
    '            .Columns.Add("billing_firstname", Type.GetType("System.String"))
    '            .Columns.Add("billing_lastname", Type.GetType("System.String"))
    '            .Columns.Add("continue_processing", Type.GetType("System.String"))
    '            .Columns.Add("user_message", Type.GetType("System.String"))
    '            .Columns.Add("success", Type.GetType("System.String"))
    '            .Columns.Add("transaction_status", Type.GetType("System.String"))
    '            .Columns.Add("payment_type", Type.GetType("System.String"))
    '            .Columns.Add("avs_response", Type.GetType("System.String"))
    '            .Columns.Add("fail_code", Type.GetType("System.String"))
    '            .Columns.Add("amount", Type.GetType("System.String"))
    '            .Columns.Add("card_type", Type.GetType("System.String"))
    '            .Columns.Add("partial_card_number", Type.GetType("System.String"))
    '            .Columns.Add("partial_acct_number", Type.GetType("System.String"))
    '            .Columns.Add("convenience_fee_amount", Type.GetType("System.String"))
    '            .Columns.Add("convenience_fee_collected", Type.GetType("System.String"))
    '            .Columns.Add("total_amount", Type.GetType("System.String"))
    '            .Columns.Add("transaction_date", Type.GetType("System.String"))
    '            PrimaryKeyColumns(0) = .Columns("remittance_id")
    '            .PrimaryKey = PrimaryKeyColumns
    '        End With
    '    End If
    '    Return CType(bp.Application("tblTransaction"), DataTable)
    'End Function

    'Public Function PaymentSessionInitiate(ByVal bp As Page, ByVal AppIDForOnlinePay As String, ByVal versionOfOnlinePayApp As String, ByVal emailaddress As String, ByVal ordernumber As String, ByVal firstname As String, ByVal lastname As String, ByVal address As String,
    '                                            ByVal city As String, ByVal state As String, ByVal zip As String, ByVal fee As Decimal, Optional ByVal apprelateddata As String = "") As String
    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim remittanceID As String = String.Empty
    '    Dim strURL As String = String.Empty
    '    Dim strParameters As String = String.Empty
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.NewRow

    '    remittanceID = onlinepay.SessionInitiate(Convert.ToInt32(AppIDForOnlinePay), versionOfOnlinePayApp, emailaddress, ordernumber)
    '    If Len(remittanceID) > 0 Then
    '        onlinepay.AcceptBillingAddress(Convert.ToInt32(AppIDForOnlinePay), remittanceID, firstname, lastname, address, city, state, zip, fee)
    '        strURL = onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "PaymentGatewayURL").ToString
    '        strParameters = "application_id=" & bp.Server.UrlEncode(Convert.ToInt32(AppIDForOnlinePay).ToString()).ToString &
    '              "&message_version=" & bp.Server.UrlEncode(versionOfOnlinePayApp).ToString &
    '              "&remittance_id=" & bp.Server.UrlEncode(remittanceID).ToString
    '        bp.Session("remittance_Id") = remittanceID


    '        'store the data in the Applicaiton table for future reference
    '        'Set datarow values and add row to datatable
    '        drTransaction.Item("SessionID") = bp.Session.SessionID.ToString()
    '        'dr.Item("SubmissionID") = logInVS.submissionID

    '        drTransaction.Item("application_id") = AppIDForOnlinePay
    '        drTransaction.Item("message_version") = versionOfOnlinePayApp
    '        drTransaction.Item("remittance_id") = remittanceID
    '        drTransaction.Item("transaction_date") = Date.Now.ToString
    '        drTransaction.Item("apprelateddata") = apprelateddata
    '        drTransaction.Item("ordernumber") = ordernumber
    '        dtTransaction.Rows.Add(drTransaction)
    '        bp.Application("tblTransaction") = dtTransaction
    '        Return strURL & "?" & strParameters
    '    Else
    '        Return String.Empty
    '    End If
    'End Function

    Public Function PaymentSessionInitiate(ByVal bp As Page, ByVal AppIDForOnlinePay As Integer, ByVal versionOfOnlinePayApp As String, ByVal emailaddress As String, ByVal ordernumber As String, ByVal firstname As String, ByVal lastname As String, ByVal address As String,
                                                ByVal city As String, ByVal state As String, ByVal zip As String, ByVal fee As Decimal, Optional ByVal apprelateddata As String = "") As RemotePost
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim remittanceID As String
        Dim strURL As String
        'Dim dtTransaction As DataTable
        'Dim drTransaction As DataRow
        'dtTransaction = CreatePaymentTransactionTable(bp)
        'drTransaction = dtTransaction.NewRow

        remittanceID = onlinepay.SessionInitiateVer2(AppIDForOnlinePay, versionOfOnlinePayApp, ordernumber, fee, 0, 0, 0, fee, emailaddress, firstname, lastname, address, city, state, zip, apprelateddata)
        If Len(remittanceID) > 0 Then
            strURL = onlinepay.Application_GetParamValueByName(AppIDForOnlinePay, "PaymentGatewayURL").ToString
            bp.Session("remittance_Id") = remittanceID

            Dim myremotepost As New RemotePost()
            myremotepost.Url = strURL
            myremotepost.Add("application_id", AppIDForOnlinePay)
            myremotepost.Add("message_version", versionOfOnlinePayApp)
            myremotepost.Add("remittance_id", remittanceID)


            Return myremotepost
        Else
            Return Nothing
        End If
    End Function

    'Public Function PaymentSessionVerification(ByVal bp As Page, ByVal AppIDForOnlinePay As String, ByVal versionOfOnlinePayApp As String, ByVal remittanceid As String, ByVal securityid As String, ByVal IPAddress As String) As String
    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim IsBillingAddressExist As Boolean
    '    Dim BFName As String = String.Empty
    '    Dim BLName As String = String.Empty
    '    Dim BAddress As String = String.Empty
    '    Dim BCity As String = String.Empty
    '    Dim BState As String = String.Empty
    '    Dim BZip As String = String.Empty
    '    Dim strAmount As Decimal = 0.0
    '    Dim strResponse As String = String.Empty
    '    Dim isValid As Boolean = False
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)

    '    isValid = PaymentRequestIPAddressValid(IPAddress, AppIDForOnlinePay)

    '    bp.Session("application_id") = AppIDForOnlinePay
    '    bp.Session("message_version") = versionOfOnlinePayApp
    '    bp.Session("remittance_id") = remittanceid
    '    bp.Session("security_id") = securityid
    '    If isValid Then
    '        IsBillingAddressExist = onlinepay.GetBillingAddress(Convert.ToInt32(AppIDForOnlinePay), remittanceid, BFName, BLName, BAddress, BCity, BState, BZip, strAmount)
    '        If IsBillingAddressExist Then
    '            isValid = onlinepay.SessionVerification(Convert.ToInt32(AppIDForOnlinePay), _
    '                                    versionOfOnlinePayApp, _
    '                                    remittanceid, _
    '                                    securityid, _
    '                                    BFName, _
    '                                    BLName, _
    '                                    BAddress, _
    '                                    BCity, _
    '                                    BState, _
    '                                    BZip, _
    '                                    Convert.ToDouble(strAmount))


    '            'store the data in the Applicaiton table for future reference

    '            drTransaction.Item("billing_firstname") = BFName
    '            drTransaction.Item("billing_lastname") = BLName
    '            drTransaction.Item("amount") = strAmount.ToString()
    '            drTransaction.Item("security_id") = securityid
    '            bp.Application("tblTransaction") = dtTransaction
    '        Else
    '            isValid = False

    '        End If
    '    End If
    '    If isValid Then
    '        strResponse = "continue_processing=" & bp.Server.UrlEncode("true") & "&billing_firstname=" & bp.Server.UrlEncode(BFName) & "&billing_lastname=" & bp.Server.UrlEncode(BLName) & "&billing_address=" & bp.Server.UrlEncode(BAddress) & "&billing_city=" & bp.Server.UrlEncode(BCity) & "&billing_state=" & bp.Server.UrlEncode(BState) & "&billing_zip=" & bp.Server.UrlEncode(BZip) & "&order_number=" & bp.Server.UrlEncode(drTransaction.Item("ordernumber")) & "&amount=" & bp.Server.UrlEncode(strAmount.ToString())
    '    Else
    '        strResponse = "continue_processing=" & bp.Server.UrlEncode("false") & _
    '                            "&redirect_user_url=" & bp.Server.UrlEncode(onlinepay.Application_GetParamValueByName(Convert.ToInt32("AppIDForOnlinePay"), "URLContinueAfterFail")) & "&remitance_id=" & remittanceid & _
    '                        "&user_message=" & bp.Server.UrlEncode("Failed Verification Process")
    '    End If
    '    Return strResponse

    'End Function

    Public Function PaymentSessionVerification(ByVal bp As Page, ByVal AppIDForOnlinePay As String, ByVal versionOfOnlinePayApp As String, ByVal remittanceid As String, ByVal securityid As String, ByVal IPAddress As String) As String
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim IsBillingAddressExist As Boolean
        Dim BFName As String = String.Empty
        Dim BLName As String = String.Empty
        Dim BAddress As String = String.Empty
        Dim BCity As String = String.Empty
        Dim BState As String = String.Empty
        Dim BZip As String = String.Empty
        Dim strAmount As Decimal = 0.0
        Dim ordernumber As String = String.Empty
        Dim strResponse As String = String.Empty
        Dim isValid As Boolean = False

        isValid = PaymentRequestIPAddressValid(IPAddress, AppIDForOnlinePay)

        bp.Session("application_id") = AppIDForOnlinePay
        bp.Session("message_version") = versionOfOnlinePayApp
        bp.Session("remittance_id") = remittanceid
        bp.Session("security_id") = securityid

        If isValid Then
            IsBillingAddressExist = onlinepay.GetBillingDetails(AppIDForOnlinePay, remittanceid, BFName, BLName, BAddress, BCity, BState, BZip, strAmount, ordernumber)
            If IsBillingAddressExist Then
                isValid = onlinepay.SessionVerificationVer2(AppIDForOnlinePay, versionOfOnlinePayApp, remittanceid, securityid)
            Else
                isValid = False
            End If
        End If
        If isValid Then
            strResponse = "continue_processing=" & bp.Server.UrlEncode("true") & "&billing_firstname=" & bp.Server.UrlEncode(BFName) & "&billing_lastname=" & bp.Server.UrlEncode(BLName) & "&billing_address=" & bp.Server.UrlEncode(BAddress) & "&billing_city=" & bp.Server.UrlEncode(BCity) & "&billing_state=" & bp.Server.UrlEncode(BState) & "&billing_zip=" & bp.Server.UrlEncode(BZip) & "&order_number=" & bp.Server.UrlEncode(ordernumber) & "&amount=" & bp.Server.UrlEncode(strAmount.ToString())
        Else
            strResponse = "continue_processing=" & bp.Server.UrlEncode("false") &
                                "&redirect_user_url=" & bp.Server.UrlEncode(onlinepay.Application_GetParamValueByName(Convert.ToInt32("AppIDForOnlinePay"), "URLContinueAfterFail")) & "&remitance_id=" & remittanceid &
                            "&user_message=" & bp.Server.UrlEncode("Failed Verification Process")
        End If
        Return strResponse

    End Function

    'Public Function PaymentSessionNotification(ByVal bp As Page, ByVal AppIDForOnlinePay As String, ByVal versionOfOnlinePayApp As String, ByVal securityid As String, _
    '                                                ByVal remittanceid As String, ByVal transactionstatus As String, ByVal avsresponse As String, ByVal failcode As Integer, _
    '                                                ByVal paymenttype As String, ByVal cardtype As String, ByVal partialcardnumber As String, ByVal partialacctnumber As String, _
    '                                                ByVal conveniencefeeamount As Decimal, ByVal basicamount As Decimal, ByVal totalamount As String) As String
    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim strResponse As String = String.Empty
    '    Dim success As Boolean = False

    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)


    '    'store the data in the Applicaiton table for future reference
    '    drTransaction.Item("transaction_status") = transactionstatus
    '    drTransaction.Item("avs_response") = avsresponse
    '    drTransaction.Item("fail_code") = failcode
    '    drTransaction.Item("card_type") = cardtype
    '    drTransaction.Item("partial_card_number") = partialcardnumber
    '    drTransaction.Item("partial_acct_number") = partialacctnumber
    '    drTransaction.Item("convenience_fee_amount") = conveniencefeeamount
    '    drTransaction.Item("total_amount") = totalamount

    '    bp.Application("tblTransaction") = dtTransaction

    '    success = onlinepay.SessionNotification(Convert.ToInt32(AppIDForOnlinePay), _
    '                                                                versionOfOnlinePayApp, _
    '                                                                remittanceid, _
    '                                                                securityid, _
    '                                                     "SESSION_NOTIFICATION", _
    '                                                     transactionstatus, _
    '                                                     String.Empty, avsresponse, _
    '                                                     failcode, _
    '                                                     paymenttype, _
    '                                                     basicamount, _
    '                                                     conveniencefeeamount, _
    '                                                     totalamount)

    '    strResponse &= "success=" & bp.Server.UrlEncode(success.ToString())
    '    If Not success Then
    '        strResponse &= "&user_message=" & bp.Server.UrlEncode("The transaction is failed due to server issues. Please try later.") '& Server.UrlEncode(OnlinePay.UserMessage.ToString)
    '    End If
    '    Return strResponse
    'End Function

    Public Function PaymentSessionNotification(ByVal bp As Page, ByVal AppIDForOnlinePay As String, ByVal versionOfOnlinePayApp As String, ByVal securityid As String,
                                                    ByVal remittanceid As String, ByVal transactionstatus As String, ByVal avsresponse As String, ByVal failcode As Integer,
                                                    ByVal paymenttype As String, ByVal cardtype As String, ByVal conveniencefeeamount As Decimal, ByVal basicamount As Decimal, ByVal totalamount As String,
                                                    ByVal firstname As String, ByVal lastname As String, ByVal address As String, ByVal city As String, ByVal state As String,
                                                    ByVal zip As String) As String
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim strResponse As String = String.Empty
        Dim success As Boolean = False

        success = onlinepay.SessionNotificationVer2(AppIDForOnlinePay, versionOfOnlinePayApp, remittanceid, securityid, transactionstatus, avsresponse, failcode, paymenttype, cardtype, basicamount,
                                                    conveniencefeeamount, totalamount, firstname, lastname, address, city, state, zip)

        strResponse &= "success=" & bp.Server.UrlEncode(success.ToString())
        If Not success Then
            strResponse &= "&user_message=" & bp.Server.UrlEncode("The transaction is failed due to server issues. Please try later.") '& Server.UrlEncode(OnlinePay.UserMessage.ToString)
        End If
        Return strResponse
    End Function
    'Public Function paymentSessionComplete(ByVal bp As Page, ByVal remittanceid As String) As String
    '    Dim strResponse As String = String.Empty
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)
    '    strResponse = drTransaction.Item("apprelateddata")
    '    Return strResponse



    'End Function
    Public Function paymentSessionComplete(ByVal remittanceid As String) As String
        Dim strResponse As String = String.Empty

        Using onlinepay As New OnlinePay.OnlinePayService
            strResponse = onlinepay.GetAppRelatedData(remittanceid)
        End Using

        Return strResponse



    End Function


    Private Function PaymentRequestIPAddressValid(ByVal IPAddress As String, ByVal AppIDForOnlinePay As Integer) As Boolean
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim IsOTAccessAuthorized As Boolean = False
        If InStr(onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "PaymentGatewayIPs"), IPAddress) = 0 Then
            IsOTAccessAuthorized = False
        Else
            IsOTAccessAuthorized = True
        End If
        Return IsOTAccessAuthorized
    End Function

    'Public Function GetFailcode(ByVal bp As Page, ByVal remittanceid As String) As Integer
    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim failcode As Integer = 0
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)

    '    failcode = Convert.ToInt32(drTransaction.Item("fail_code"))

    '    Return failcode
    'End Function

    Public Function GetFailcode(ByVal remittanceid As String) As Integer
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim failcode As Integer

        failcode = onlinepay.GetFailCodeByRemittanceID(remittanceid)

        Return failcode
    End Function

    'Public Function GetDescByAVSCode(ByVal bp As Page, ByVal remittanceid As String) As String
    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)
    '    Dim AVSDesc As String = String.Empty
    '    AVSDesc = onlinepay.Application_GetDescByAVSCode(drTransaction.Item("avs_response"))
    '    Return AVSDesc
    'End Function

    Public Function GetDescByAVSCode(ByVal remittanceid As String) As String
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim AVSDesc As String = String.Empty
        AVSDesc = onlinepay.GetAVSCodeDescByRemittanceID(remittanceid)
        Return AVSDesc
    End Function

    'Public Function GetDescByFailCode(ByVal bp As Page, ByVal remittanceid As String) As String
    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)
    '    Dim FailCode As String = String.Empty
    '    FailCode = onlinepay.Application_GetDescByFailCode(Convert.ToInt32(drTransaction.Item("fail_code")))
    '    Return FailCode
    'End Function

    Public Function GetDescByFailCode(ByVal remittanceid As String) As String
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim FailCode As String = String.Empty
        FailCode = onlinepay.GetFailCodeDescByRemittanceID(remittanceid)
        Return FailCode
    End Function

    'Public Sub RemoveReceipt(ByVal bp As Page, ByVal remittanceid As String)
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)
    '    dtTransaction.Rows.Remove(drTransaction)
    'End Sub

    'Public Function PaymentReceiptDetails(ByVal bp As Page, ByVal AppIDForOnlinePay As String, ByRef remittanceid As String, ByRef transactiondate As Date, ByRef paymentdesc As String, _
    '                                    ByRef cardtype As String, ByRef partialcardnum As String, ByRef totalamount As Decimal, ByRef appdesc As String, ByRef appaddress As String, _
    '                                    ByRef appcity As String, ByRef appstate As String, ByRef appzip As String, ByRef appphone As String) As Boolean

    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim out As Boolean = False
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)

    '    If Not IsNothing(drTransaction) Then
    '        'todo insert the remitanceid in the tblWellPayment table in the onlinewellpermitting database.
    '        transactiondate = drTransaction.Item("transaction_date")
    '        paymentdesc = onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "PaymentDescription")
    '        cardtype = drTransaction.Item("card_type")
    '        partialcardnum = drTransaction.Item("partial_card_number")
    '        totalamount = drTransaction.Item("total_amount")
    '        appdesc = onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "ApplicationDescription")
    '        appaddress = onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "ApplicationAddress")
    '        appcity = onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "ApplicationCity")
    '        appstate = "DE"
    '        appzip = onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "ApplicationZip")
    '        appphone = onlinepay.Application_GetParamValueByName(Convert.ToInt32(AppIDForOnlinePay), "ApplicationPhone")
    '        out = True

    '    Else
    '        out = False
    '    End If
    '    Return out
    'End Function

    Public Function PaymentReceiptDetails(ByVal AppIDForOnlinePay As String, ByRef remittanceid As String, ByRef transactiondate As Date, ByRef paymentdesc As String,
                                        ByRef cardtype As String, ByRef totalamount As Decimal, ByRef appdesc As String, ByRef appaddress As String,
                                        ByRef appcity As String, ByRef appstate As String, ByRef appzip As String, ByRef appphone As String) As Boolean

        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim out As Boolean = False

        out = onlinepay.GetTransactionDetails(AppIDForOnlinePay, remittanceid, transactiondate, paymentdesc, cardtype, totalamount, appdesc, appaddress, appcity, appstate, appzip, appphone)


        Return out
    End Function

    'Public Function GetAmountPaid(ByVal bp As Page, ByVal remittanceid As String) As Decimal
    '    Dim onlinepay As New Onlinepay.OnlinePayService
    '    Dim dtTransaction As DataTable
    '    Dim drTransaction As DataRow
    '    dtTransaction = CreatePaymentTransactionTable(bp)
    '    drTransaction = dtTransaction.Rows.Find(remittanceid)
    '    Dim amountpaid As Decimal = 0
    '    amountpaid = Convert.ToDecimal(drTransaction.Item("total_amount"))
    '    Return amountpaid
    'End Function

    Public Function GetAmountPaid(ByVal remittanceid As String) As Decimal
        Dim onlinepay As New OnlinePay.OnlinePayService
        Dim amountpaid As Decimal = 0
        amountpaid = onlinepay.GetAmountPaidByRemittanceID(remittanceid)
        Return amountpaid
    End Function

    Public Function GetTransactionForDateRange(ByVal AppIDForOnlinePay As String, ByVal startdate As Date, ByVal enddate As Date) As DataTable
        Dim onlinepay As New Onlinepay.OnlinePayService
        Dim dtTransaction As New DataTable
        Dim strData As String
        Dim ds As New DataSet
        strData = onlinepay.GetTransactionsForDateRange(AppIDForOnlinePay, startdate, enddate)
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(strData))
        ds.ReadXml(ms)
        If ds.Tables.Count > 0 Then
            dtTransaction = ds.Tables(0).Copy
        Else
            dtTransaction = New DataTable
        End If
        Return dtTransaction
    End Function



End Module
