Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblMS4Pollutants")>
Partial Public Class MS4Pollutants
    Implements IEntity
    Public Sub New()
        MS4RWPollutants = New HashSet(Of MS4RWPollutants)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property PollutantID As Integer

    <Required>
    <StringLength(100)>
    Public Property PollutantName As String

    <StringLength(200)>
    Public Property PollutantDesc As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Overridable Property MS4RWPollutants As ICollection(Of MS4RWPollutants)

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
