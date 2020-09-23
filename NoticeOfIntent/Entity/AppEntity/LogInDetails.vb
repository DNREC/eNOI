Imports System.Text
Imports System.Security.Cryptography
Imports System.IO


<Serializable()> _
Public Class LogInDetails
    Public Property currentdb As AdminCurrentDB
    Public Property loginToken As String
    Public Property reportid As Integer
    Public Property user As UserInfo
    Public Property submissionid As Integer
    Public Property submissiontype As NOISubmissionType
    Public Property progsubmisssiontype As Integer
    'Public Property projectnames As String



    Public Sub New()
        currentdb = AdminCurrentDB.NOI
        loginToken = String.Empty
        reportid = 0
        user = New UserInfo()
        submissionid = 0
        submissiontype = Nothing
        progsubmisssiontype = 0
        'projectnames = String.Empty
    End Sub


    Public Shared Function serialize(ByVal login As LogInDetails) As String
        Dim rtn As New StringBuilder()
        With login
            rtn.Append(.currentdb)
            rtn.Append(":")
            rtn.Append(.loginToken)
            rtn.Append(":")
            rtn.Append(.reportid)
            rtn.Append(":")
            rtn.Append(.submissionid)
            rtn.Append(":")
            rtn.Append(.submissiontype)
            rtn.Append(":")
            rtn.Append(.progsubmisssiontype)
            rtn.Append(":")
            rtn.Append(.user.useremail)
            rtn.Append(":")
            rtn.Append(.user.userFname)
            rtn.Append(":")
            rtn.Append(.user.userLname)
            rtn.Append(":")
            rtn.Append(.user.fullName)
            rtn.Append(":")
            rtn.Append(.user.userid)
            'rtn.Append(":")
            'rtn.Append(.projectnames)
        End With

        Dim key() As Byte = Encoding.UTF8.GetBytes("&^1$#A!$")
        Dim input() As Byte = Encoding.UTF8.GetBytes(rtn.ToString())
        Dim IV() As Byte = {1, 5, 15, 30, 50, 75, 105, 140}
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream
            Dim des As New DESCryptoServiceProvider()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)

            cs.Write(input, 0, input.Length)
            cs.FlushFinalBlock()

            Return Convert.ToBase64String(ms.ToArray())
        Catch ex As Exception
            Throw ex
        Finally
            If ms IsNot Nothing Then
                ms.Close()
            End If
        End Try

        Return ""
    End Function


    Public Shared Function deserialize(ByVal login As String) As LogInDetails
        Dim key() As Byte = Encoding.UTF8.GetBytes("&^1$#A!$")
        Dim input() As Byte = Convert.FromBase64String(login)
        Dim IV() As Byte = {1, 5, 15, 30, 50, 75, 105, 140}
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream
            Dim des As New DESCryptoServiceProvider()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)

            cs.Write(input, 0, input.Length)
            cs.FlushFinalBlock()

            Dim vals() As String = Encoding.UTF8.GetString(ms.ToArray()).Split(":")
            Dim log As New LogInDetails()
            With log
                .currentdb = vals(0)
                .loginToken = vals(1)
                .reportid = vals(2)
                .submissionid = vals(3)
                .submissiontype = vals(4)
                .progsubmisssiontype = vals(5)
                .user.useremail = vals(6)
                .user.userFname = vals(7)
                .user.userLname = vals(8)
                .user.fullName = vals(9)
                .user.userid = vals(10)
                '.projectnames = vals(11)
            End With
            Return log
        Catch ex As Exception
            Throw ex
        Finally
            If ms IsNot Nothing Then
                ms.Close()
            End If
        End Try

        Return Nothing
    End Function




End Class
