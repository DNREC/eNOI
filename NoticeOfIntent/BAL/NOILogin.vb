

Public Class NOILogin

    Public Shared Function getAdminLogin(programid As Integer) As LogInDetails
        Dim lin As LogInDetails = Nothing

        'If Not bp.isExternal Then
        'Dim login As New us.de.state.dnrec.ADLogin
        Dim username As String = ""
            lin = New LogInDetails()
            username = HttpContext.Current.User.Identity.Name
            With lin
                .user.fullName = username
                If InStr(.user.fullName, "\") Then
                    .user.fullName = Split(.user.fullName, "\")(1)
                End If
                .user.fullName = Left(.user.fullName, 64)
                .user.userid = .user.fullName
                .loginToken = String.Empty
                .reportid = programid
                .submissionid = 0
            '.projectnames = String.Empty
        End With
        'End If

        Return lin
    End Function

    Public Shared Function isValidToken(ByVal login As LogInDetails, ByRef userprojectnumbers As String) As Boolean

        Dim ValidToken As Integer = 0
        Dim ws As New wsCROMTOM.wsCROMTOM

        ValidToken = ws.ReturnUserInfo(login.loginToken, login.user.userid, login.user.fullName, login.user.useremail, userprojectnumbers)

        If ValidToken = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function isValidToken(ByVal login As LogInDetails) As Boolean
        Dim ValidToken As Integer = 0
        Dim ws As New wsCROMTOM.wsCROMTOM

        ValidToken = ws.AuthenticateToken(login.loginToken)

        If ValidToken = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function GetProjectNumbersByUser(ByVal login As LogInDetails) As String
        Dim ValidToken As Integer = 0
        Dim ws As New wsCROMTOM.wsCROMTOM

        Dim projectnames As String = String.Empty

        ValidToken = ws.ReturnUserInfo(login.loginToken, login.user.userid, login.user.fullName, login.user.useremail, projectnames)

        Return projectnames
    End Function

    Public Shared Function GetUserInfo(ByVal userid As String, ByRef FullName As String, ByRef EMail As String, ByRef CompanyName As String) As Boolean
        Dim count As Integer = 0
        Dim ws As New wsCROMTOM.wsCROMTOM

        Try


            count = ws.ReturnUserInfoByUserID(userid, FullName, EMail, CompanyName)
            If count = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
