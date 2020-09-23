Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json


<Table("tblNOISubmissionStatus")>
Partial Public Class NOISubmissionStatus
    Implements IEntity


    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property SubmissionStatusID As Integer

    Public Property SubmissionID As Integer

    <Column(TypeName:="smalldatetime")>
    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    Public Property SubmissionStatusDate As DateTime

    <StringLength(1)>
    Public Property SubmissionStatusCode As String

    <StringLength(100)>
    Public Property SubmissionStatusComment As String

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Overridable Property NOISubmissionStatusCode As NOISubmissionStatusCode

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
