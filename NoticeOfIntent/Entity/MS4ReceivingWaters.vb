Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblMS4ReceivingWaters")>
Partial Public Class MS4ReceivingWaters
    Implements IEntity
    Public Sub New()
        MS4RWPollutants = New HashSet(Of MS4RWPollutants)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property RWID As Integer

    <Required>
    <StringLength(100)>
    Public Property WaterBody As String

    Public Property OutfallCount As Integer

    Public Property SubmissionID As Integer

    Public Overridable Property MS4RWPollutants As ICollection(Of MS4RWPollutants)

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
