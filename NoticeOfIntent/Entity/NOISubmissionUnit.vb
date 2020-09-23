Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json


<Table("tblNOISubmissionUnit")>
Partial Public Class NOISubmissionUnit
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property SubmissionUnitID As Integer

    <Required>
    <StringLength(100)>
    Public Property UnitName As String

    <StringLength(100)>
    Public Property UnitDesc As String

    Public Property NOILocID As Integer

    Public Property SubmissionID As Integer

    Public Overridable Property NOILoc As NOILoc

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    <NotMapped>
    Public Property IsDeleted As Boolean = False

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
