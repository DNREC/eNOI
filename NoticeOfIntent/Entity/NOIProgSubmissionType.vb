Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOIProgSubmissionType")>
Partial Public Class NOIProgSubmissionType
    Implements IEntity
    Public Sub New()
        NOISubmission = New HashSet(Of NOISubmission)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property ProgSubmissionTypeID As Integer

    Public Property SubmissionTypeID As Integer

    Public Property ProgramID As Integer

    <StringLength(100)>
    Public Property SubmissionTypeDesc As String

    <Column(TypeName:="numeric")>
    Public Property Fee As Decimal?

    Public Property NoOfSignatures As Integer

    <StringLength(5000)>
    Public Property CertificateAgreementText As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Overridable Property NOIProgram As NOIProgram

    Public Overridable Property NOISubmissionTypelst As NOISubmissionTypelst

    <JsonIgnore>
    Public Overridable Property NOISubmission As ICollection(Of NOISubmission)

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Detached Implements IEntity.EntityState

End Class
