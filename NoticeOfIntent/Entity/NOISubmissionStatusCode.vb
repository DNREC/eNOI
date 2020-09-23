Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblNOISubmissionStatusCode")>
Partial Public Class NOISubmissionStatusCode
    Public Sub New()
        NOISubmissionStatus = New HashSet(Of NOISubmissionStatus)()
    End Sub

    <Key>
    <StringLength(1)>
    Public Property SubmissionStatusCode As String

    <Required>
    <StringLength(50)>
    Public Property SubmissionStatus As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Property DisplayOrder As Byte?

    Public Overridable Property NOISubmissionStatus As ICollection(Of NOISubmissionStatus)
End Class
