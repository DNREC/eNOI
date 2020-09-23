Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionTaxParcels")>
Partial Public Class NOISubmissionTaxParcels
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property SubmissionTaxParcelID As Integer

    Public Property SubmissionID As Integer

    <Required>
    <StringLength(100)>
    Public Property TaxParcelNumber As String

    <Required>
    <StringLength(1)>
    Public Property TaxParcelCounty As String

    <NotMapped>
    Public Property IsDeleted As Boolean = False

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
