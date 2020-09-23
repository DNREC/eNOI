
Public Class CacheLookupData

    Public Shared Sub LoadStaticCache()
        Dim bal As New NOIBAL
        HttpContext.Current.Application("StateAbvlst") = bal.GetStates()
        HttpContext.Current.Application("CompanyTypelst") = bal.GetCompanyType()
        Dim adminbal As New AdminBAL
        HttpContext.Current.Application("ZipCodes") = adminbal.GetZipCodes()
        HttpContext.Current.Application("NOIPrograms") = bal.GetNOIPrograms()
        HttpContext.Current.Application("NOIProgSubmissionType") = bal.GetNOIProgSubmissionTypes()
        HttpContext.Current.Application("NOISubmissionTypelst") = bal.GetSubmissionTypeList()
        HttpContext.Current.Application("NOISubmissionStatusCodes") = bal.GetSubmissionStatusCodes()
        'HttpContext.Current.Application("NOIWebPageCtrlTxtlst") = bal.GetNOIWebPageCtrlTxt()
    End Sub

    Public Shared Function GetStates() As IList(Of StateAbvlst)
        If HttpContext.Current.Application("StateAbvlst") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("StateAbvlst"), IList(Of StateAbvlst))
    End Function

    Public Shared Function GetCompanyType() As IList(Of CompanyTypelst)
        If HttpContext.Current.Application("CompanyTypelst") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("CompanyTypelst"), IList(Of CompanyTypelst))
    End Function

    Public Shared Function GetCityStateAbvByZip(zip As String) As Zipcodes
        If HttpContext.Current.Application("ZipCodes") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("ZipCodes"), IList(Of Zipcodes)).Where(Function(a) a.ZIP5 = zip).Distinct.FirstOrDefault
    End Function

    Public Shared Function GetCityStateAbvByZipInDE(zip As String) As Zipcodes
        If HttpContext.Current.Application("ZipCodes") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("ZipCodes"), IList(Of Zipcodes)).Where(Function(a) a.ZIP5 = zip And a.StateAbv = "DE").Distinct.FirstOrDefault
    End Function

    Public Shared Function GetNOIPrograms() As IList(Of NOIProgram)
        If HttpContext.Current.Application("NOIPrograms") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("NOIPrograms"), IList(Of NOIProgram))
    End Function

    Public Shared Function GetPiTypeIDByReportID(ByVal ReportID As Integer) As Integer
        Return GetNOIPrograms.Where(Function(a) a.ProgramID = ReportID).Select(Function(b) b.PiTypeID).SingleOrDefault()
    End Function

    Public Shared Function GetPaymentAppID(programid As Integer) As String
        Return GetNOIPrograms().Where(Function(a) a.ProgramID = programid).Select(Function(b) b.OnlinePayAppID).Single().ToString()
    End Function


    Public Shared Function GetSubmissionTypes() As IList(Of NOISubmissionTypelst)
        If HttpContext.Current.Application("NOISubmissionTypelst") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("NOISubmissionTypelst"), IList(Of NOISubmissionTypelst))
    End Function

    Public Shared Function GetNOIProgSubmissionTypes() As IList(Of NOIProgSubmissionType)
        If HttpContext.Current.Application("NOIProgSubmissionType") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("NOIProgSubmissionType"), IList(Of NOIProgSubmissionType))
    End Function

    Public Shared Function GetSubmissionStatusCodes() As IList(Of NOISubmissionStatusCode)
        If HttpContext.Current.Application("NOISubmissionStatusCodes") Is Nothing Then
            LoadStaticCache()
        End If
        Return CType(HttpContext.Current.Application("NOISubmissionStatusCodes"), IList(Of NOISubmissionStatusCode))
    End Function

    Public Shared Function GetFeesBySubmissionType(subtype As Integer) As Decimal
        Dim stype As NOIProgSubmissionType = GetSubmissionTypeDetailsBySubmissionTypeID(subtype)
        Return stype.Fee
    End Function

    Public Shared Function GetNoOfSignaturesBySubmissionType(subtype As Integer) As Integer
        Dim stype As NOIProgSubmissionType = GetSubmissionTypeDetailsBySubmissionTypeID(subtype)
        Return stype.NoOfSignatures
    End Function

    Public Shared Function GetSubmissionTypeDetailsBySubmissionTypeID(subtype As Integer) As NOIProgSubmissionType
        Return GetNOIProgSubmissionTypes().Where(Function(a) a.ProgSubmissionTypeID = subtype).FirstOrDefault
        'Return CType(HttpContext.Current.Application("NOIProgSubmissionType"), IList(Of NOIProgSubmissionType)).Where(Function(a) a.ProgSubmissionTypeID = subtype).FirstOrDefault
    End Function

    Public Shared Sub LoadStaticSessionCache()
        Dim deptname As String = String.Empty
        Dim email As String = String.Empty
        Dim phone As String = String.Empty
        Dim ctx As HttpContext = HttpContext.Current
        Dim reportid As Integer = Convert.ToInt16(ctx.Request.QueryString("ReportID"))
        CROMERRExchange.GetAdminEmailAndPhone(reportid, deptname, email, phone)
        ctx.Session("deptname") = deptname
        ctx.Session("deptemail") = email
        ctx.Session("deptphone") = phone
    End Sub

    Public Shared Function GetDeptName() As String
        If HttpContext.Current.Session("deptname") Is Nothing Then
            LoadStaticSessionCache()
        End If
        Return HttpContext.Current.Session("deptname").ToString()

    End Function

    Public Shared Function GetDeptEmail() As String
        If HttpContext.Current.Session("deptemail") Is Nothing Then
            LoadStaticSessionCache()
        End If
        Return HttpContext.Current.Session("deptemail").ToString()
    End Function

    Public Shared Function GetDeptPhone() As String
        If HttpContext.Current.Session("deptphone") Is Nothing Then
            LoadStaticSessionCache()
        End If
        Return HttpContext.Current.Session("deptphone").ToString()
    End Function

    Public Shared Function GetUserProjectList(login As LogInDetails) As String
        Dim projectnames As String = String.Empty
        If HttpContext.Current.Session("userprojectlist") Is Nothing Then
            projectnames = NOILogin.GetProjectNumbersByUser(login)
            HttpContext.Current.Session("userprojectlist") = projectnames
        Else
            projectnames = HttpContext.Current.Session("userprojectlist")
        End If
        Return projectnames
    End Function


    'Public Shared Function GetNOIWebPageCtrlTxt() As IList(Of NOIWebPage)
    '    Return CType(HttpContext.Current.Application("NOIWebPageCtrlTxtlst"), IList(Of NOIWebPage))
    'End Function

    'Public Shared Function GetNOIWebPageCtrlTxtByProgramIDAndWebPage(programid As NOIProgramType, pagename As String) As IList(Of NOIWebPageCtrlTxt)
    '    Dim webpageCtrlTxtlst As List(Of NOIWebPageCtrlTxt)
    '    webpageCtrlTxtlst = CType(HttpContext.Current.Application("NOIWebPageCtrlTxtlst"), IList(Of NOIWebPage)).Where(Function(a) a.ProgramID = programid And a.WebPage = pagename).Select(Function(e) e.NOIWebPageCtrlTxts)(0).ToList()
    '    Return webpageCtrlTxtlst
    'End Function

End Class
