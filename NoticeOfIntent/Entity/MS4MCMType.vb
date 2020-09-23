Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblMS4MCMType")>
Partial Public Class MS4MCMType
    Implements IEntity
    Public Sub New()
        MS4BMPCatagory = New HashSet(Of MS4BMPCatagory)()
        MS4MCMDetails = New HashSet(Of MS4MCMDetails)()
        MS4ResponsibleDept = New HashSet(Of MS4ResponsibleDept)()
        MS4TargetedAudience = New HashSet(Of MS4TargetedAudience)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property MCMTypeID As Integer

    <Required>
    <StringLength(100)>
    Public Property MCMName As String

    <StringLength(200)>
    Public Property MCMDesc As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Overridable Property MS4BMPCatagory As ICollection(Of MS4BMPCatagory)

    Public Overridable Property MS4MCMDetails As ICollection(Of MS4MCMDetails)

    Public Overridable Property MS4ResponsibleDept As ICollection(Of MS4ResponsibleDept)

    Public Overridable Property MS4TargetedAudience As ICollection(Of MS4TargetedAudience)

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
