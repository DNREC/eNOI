Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOILoc")>
Partial Public Class NOILoc
    Implements IEntity

    Public Sub New()
        NOISubmission = New HashSet(Of NOISubmission)()
        NOISubmissionUnit = New HashSet(Of NOISubmissionUnit)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property NOILocID As Integer

    <Column(TypeName:="numeric")>
    Public Property Latitude As Decimal

    <Column(TypeName:="numeric")>
    Public Property Longitude As Decimal

    <Column(TypeName:="numeric")>
    Public Property X As Decimal

    <Column(TypeName:="numeric")>
    Public Property Y As Decimal

    <StringLength(12)>
    Public Property HUC_12_Code As String

    <StringLength(100)>
    Public Property Watershed As String

    <JsonIgnore>
    Public Overridable Property NOISubmission As ICollection(Of NOISubmission)

    <JsonIgnore>
    Public Overridable Property NOISubmissionUnit As ICollection(Of NOISubmissionUnit)

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState


End Class
