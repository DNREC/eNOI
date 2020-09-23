Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblErrorLog")>
Partial Public Class ErrorLog

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property ErrorID As Integer

    Public Property ErrorDate As Date

    <StringLength(400)>
    Public Property Browser As String

    <StringLength(1000)>
    Public Property RequestedURL As String

    <StringLength(500)>
    Public Property ErrorSource As String

    Public Property ErrorMessage As String

    Public Property SubmissionID As Integer?

    Public Property StackTrace As String
End Class
