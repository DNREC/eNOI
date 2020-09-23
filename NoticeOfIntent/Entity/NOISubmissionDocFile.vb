Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports NoticeOfIntent
Imports Newtonsoft.Json

Partial Public Class NOISubmissionDocFile
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property NOIDocID As Integer
    Public Property UploadedDocument As Byte()

    Public Property NOISubmissionDocs As NOISubmissionDocs

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
