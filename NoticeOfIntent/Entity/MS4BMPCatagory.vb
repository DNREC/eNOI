Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblMS4BMPCatagory")>
Partial Public Class MS4BMPCatagory
    Implements IEntity
    Public Sub New()
        MS4MCMDetails = New HashSet(Of MS4MCMDetails)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property BMPCategoryID As Integer

    <Required>
    <StringLength(100)>
    Public Property BMPCategory As String

    <StringLength(200)>
    Public Property BMPDescription As String

    Public Property MCMTypeID As Integer

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Overridable Property MS4MCMType As MS4MCMType

    Public Overridable Property MS4MCMDetails As ICollection(Of MS4MCMDetails)

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
