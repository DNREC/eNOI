Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionAP")>
Public Class NOISubmissionAP
    Implements IEntity


    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property SubmissionID As Integer

    Public Property EntityTypeID As Integer

    <StringLength(1)>
    Public Property InsectPestControl As String

    <StringLength(1)>
    Public Property WeedPestControl As String

    <StringLength(1)>
    Public Property AnimalPestControl As String

    <StringLength(1)>
    Public Property ForestCanopyPestControl As String

    <StringLength(1)>
    Public Property NotExceededThreshold As String

    <StringLength(50)>
    Public Property CommercialApplicatorID As String

    Public Overridable Property NOIEntityType As NOIEntityType

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState

End Class
