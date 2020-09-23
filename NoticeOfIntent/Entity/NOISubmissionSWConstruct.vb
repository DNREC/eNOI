Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json
Imports NoticeOfIntent

<Table("tblNOISubmissionSWConstruct")>
Partial Public Class NOISubmissionSWConstruct
    Implements IEntity

    '<Key>
    '<DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    'Public Property SubmissionSWConstructID As Integer

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property SubmissionID As Integer

    Public Property ProjectTypeID As Integer

    <StringLength(50)>
    Public Property ProjectTypeOther As String

    <StringLength(50)>
    Public Property ProjectCounty As String

    <StringLength(50)>
    Public Property ProjectMunicipality As String

    <Required>
    <StringLength(1)>
    Public Property IsSWPPPPrepared As String

    Public Property TotalLandArea As Decimal?

    Public Property EstimatedArea As Decimal?

    <Column(TypeName:="smalldatetime")>
    Public Property ConstructStartDate As Date?

    <Column(TypeName:="smalldatetime")>
    Public Property ConstructEndDate As Date?

    Public Property DelegatedAgencyID As Integer?

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
