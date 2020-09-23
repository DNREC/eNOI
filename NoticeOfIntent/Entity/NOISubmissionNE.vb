
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionNE")>
Partial Public Class NOISubmissionNE
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property SubmissionNEID As Integer

    Public Property SubmissionID As Integer

    Public Property NOExposureCLID As Integer

    <Required>
    <StringLength(1)>
    Public Property Answer As String

    Public Overridable Property NOExposureCL As NOExposureCL

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState

End Class
