Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports NoticeOfIntent
Imports Newtonsoft.Json

Partial Public Class NOISubmissionDocs
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property NOIDocID As Integer
    Public Property SubmissionID As Integer
    Public Property DocumentName As String
    Public Property DocumentDesc As String
    Public Property DocumentType As String

    Public Overridable Property NOISubmissionDocFile As NOISubmissionDocFile

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState

End Class
