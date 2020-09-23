Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports NoticeOfIntent

<Table("tblNOIProgram")>
Public Class NOIProgram
    Implements IEntity

    Public Sub New()
        NOIFeeExemption = New HashSet(Of NOIFeeExemption)()
        NOIProgSubmissionType = New HashSet(Of NOIProgSubmissionType)()
        NOIProject = New HashSet(Of NOIProject)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property ProgramID As Integer

    <Required>
    <StringLength(100)>
    Public Property Program As String

    <StringLength(500)>
    Public Property ProgramDesc As String

    Public Property PiTypeID As Integer?

    Public Property OnlinePayAppID As String

    Public Property OnlinePayAppVersion As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Overridable Property NOIFeeExemption As ICollection(Of NOIFeeExemption)

    Public Overridable Property NOIProgSubmissionType As ICollection(Of NOIProgSubmissionType)

    Public Overridable Property NOIProject As ICollection(Of NOIProject)

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
