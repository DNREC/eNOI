Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISigningEmailAddress")>
Partial Public Class NOISigningEmailAddress
    Implements IEntity


    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property NOISignEmailAddressID As Integer

    <Required>
    Public Property SubmissionID As Integer

    <Required>
    <StringLength(50)>
    Public Property EmailAddress As String

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState Implements IEntity.EntityState
End Class
