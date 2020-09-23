Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblNOISubmissionAcceptedPDF")>
Public Class NOISubmissionAcceptedPDF
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property NOIAcceptedPDFID As Integer

    Public Property SubmissionID As Integer

    Public Property AcceptedReportPDF As Byte()

    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState

End Class