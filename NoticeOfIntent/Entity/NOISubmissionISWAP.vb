Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionISWAP")>
Partial Public Class NOISubmissionISWAP
    Implements IEntity
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property SubmissionID As Integer

    <Required>
    <StringLength(1)>
    Public Property IsOPTransferred As String

    <Required>
    <StringLength(1)>
    Public Property IsNoDischargeAssociated As String

    <Required>
    <StringLength(1)>
    Public Property IsCoveredNPDESPermit As String

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
