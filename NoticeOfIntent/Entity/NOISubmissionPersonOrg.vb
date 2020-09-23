Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionPersonOrg")>
Public Class NOISubmissionPersonOrg
    Implements IEntity

    Private _OrgName As String
    Private _Address2 As String
    Private _PhoneExt As String
    Private _Mobile As String




    Public Sub New()
        PersonOrgTypeCode = "P"
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property PersonOrgID As Integer

    <StringLength(80)>
    Public Property OrgName As String
        Get
            Return _OrgName
        End Get
        Set(value As String)
            If value = String.Empty Then
                _OrgName = Nothing
            Else
                _OrgName = value
            End If
        End Set
    End Property

    <StringLength(30)>
    Public Property LName As String

    <StringLength(20)>
    Public Property FName As String

    <StringLength(20)>
    Public Property MName As String

    <StringLength(3)>
    Public Property NamePrefix As String

    <StringLength(3)>
    Public Property NameSuffix As String

    <StringLength(50)>
    Public Property Address1 As String

    <StringLength(50)>
    Public Property Address2 As String
        Get
            Return _Address2
        End Get
        Set(value As String)
            If value = String.Empty Then
                _Address2 = Nothing
            Else
                _Address2 = value
            End If
        End Set
    End Property
    <StringLength(30)>
    Public Property City As String

    <StringLength(2)>
    Public Property StateAbv As String

    <StringLength(14)>
    Public Property PostalCode As String

    <StringLength(16)>
    Public Property Phone As String

    <StringLength(50)>
    Public Property PhoneExt As String
        Get
            Return _PhoneExt
        End Get
        Set(value As String)
            If value = String.Empty Then
                _PhoneExt = Nothing
            Else
                _PhoneExt = value
            End If
        End Set
    End Property

    <StringLength(16)>
    Public Property Mobile As String
        Get
            Return _Mobile
        End Get
        Set(value As String)
            If value = String.Empty Then
                _Mobile = Nothing
            Else
                _Mobile = value
            End If
        End Set
    End Property

    <StringLength(50)>
    Public Property EmailAddress As String

    Public Property SubmissionID As Integer

    Public Property NOIPersonOrgTypeID As NOIPersonOrgType

    <Required>
    <StringLength(1)>
    <DefaultSettingValue("P")>
    Public Property PersonOrgTypeCode As String

    <Column(TypeName:="date")>
    Public Property CoPermitteeEffectiveDate As Date?

    Public Property AfflID As Int32?

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission


    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class