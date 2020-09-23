
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial


<Table("vwEntityType")>
Public Class NOIEntityType
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property EntityTypeID As Integer

    <StringLength(100)>
    Public Property EntityType As String

    <Required>
    <StringLength(500)>
    Public Property EntityTypeDesc As String


    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
