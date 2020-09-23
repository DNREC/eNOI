Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOISubmissionSWBMP")>
Partial Public Class NOISubmissionSWBMP
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property SubmissionSWBMPID As Integer

    Public Property SubmissionID As Integer

    Public Property SWBMPID As Byte

    <StringLength(50)>
    Public Property SWBMPOtherName As String

    Public Property SWBMPQty As Integer?

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Overridable Property SWBMPlst As SWBMPlst

    <NotMapped>
    Public Property IsDeleted As Boolean = False

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
