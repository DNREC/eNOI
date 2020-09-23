Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("vwSWBMP")>
Public Class SWBMPlst
    Implements IEntity


    'Public Sub New()
    '    NOIProjectSWBMPs = New HashSet(Of NOIProjectSWBMP)()
    'End Sub


    <Key>
    Public Property SWBMPID As Byte

    <StringLength(50)>
    Public Property SWBMP As String

    <StringLength(1000)>
    Public Property SWBMPDesc As String

    <StringLength(1)>
    Public Property Active As String

    Public Property DisplayOrder As Byte

    '<JsonIgnore>
    'Public Overridable Property NOIProjectSWBMPs As ICollection(Of NOIProjectSWBMP)

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Detached Implements IEntity.EntityState
End Class
