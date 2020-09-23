Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionAPChemicals")>
Partial Public Class NOISubmissionAPChemicals
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property APChemicalID As Integer

    Public Property SubmissionID As Integer

    <StringLength(200)>
    Public Property Ingredient As String

    Public Property PesticidePatternID As Integer

    <Column(TypeName:="numeric")>
    Public Property ApplicationRate As Decimal?

    <StringLength(50)>
    Public Property ApplicationRateUnit As String

    <Column(TypeName:="numeric")>
    Public Property AnnlAvgQty As Decimal?

    <StringLength(50)>
    Public Property AnnlAvgQtyUnit As String

    <Column(TypeName:="numeric")>
    Public Property AnnlAvgArea As Decimal?

    <StringLength(50)>
    Public Property AnnlAvgAreaUnit As String

    Public Overridable Property NOIPesticidePattern As NOIPesticidePattern

    <NotMapped>
    Public Property IsDeleted As Boolean = False

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
