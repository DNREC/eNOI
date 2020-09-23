Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionSIC")>
Partial Public Class NOISubmissionSIC
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property SubmissionSICID As Integer

    <Required>
    <StringLength(4)>
    Public Property SICCode As String

    <Required>
    <StringLength(1)>
    Public Property RankCode As String

    Public Property SubmissionID As Integer

    <NotMapped>
    Public Property IsDeleted As Boolean = False

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState

End Class
