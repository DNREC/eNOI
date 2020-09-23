Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblNOISubmissionType")>
Partial Public Class NOISubmissionTypelst
    Public Sub New()
        NOIProgSubmissionType = New HashSet(Of NOIProgSubmissionType)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property SubmissionTypeID As Integer

    <Required>
    <StringLength(50)>
    Public Property SubmissionType As String

    <StringLength(200)>
    Public Property SubmissionTypeDesc As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Overridable Property NOIProgSubmissionType As ICollection(Of NOIProgSubmissionType)

End Class
