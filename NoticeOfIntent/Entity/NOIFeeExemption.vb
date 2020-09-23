Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOIFeeExemption")>
Partial Public Class NOIFeeExemption
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property ExemptionID As Integer

    <Required>
    <StringLength(50)>
    Public Property ExemptionCode As String

    <Required>
    <Column(TypeName:="date")>
    Public Property ActiveFrom As Date

    <Required>
    <Column(TypeName:="date")>
    Public Property ExpiresOn As Date

    Public Property SubmissionID As Integer?

    Public Property ProgramID As Integer

    <Required>
    <StringLength(64)>
    Public Property CreatedBy As String

    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    Public Property CreatedDate As Date

    <Required>
    <StringLength(64)>
    Public Property LastChgBy As String

    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    Public Property LastChgDate As Date

    Public Overridable Property NOIProgram As NOIProgram

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState Implements IEntity.EntityState

End Class
