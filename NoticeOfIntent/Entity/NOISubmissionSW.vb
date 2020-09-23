Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports NoticeOfIntent
Imports Newtonsoft.Json

<Table("tblNOISubmissionSW")>
Partial Public Class NOISubmissionSW
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property SubmissionID As Integer

    <Column(TypeName:="date")>
    Public Property CCDateTermination As Date?

    <StringLength(1)>
    Public Property IsSatisfiedDSSR As String

    <StringLength(1)>
    Public Property IsPlanVerifiedDSSR As String

    <StringLength(1)>
    Public Property IsFinalStablizationDone As String

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission
    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState

End Class
