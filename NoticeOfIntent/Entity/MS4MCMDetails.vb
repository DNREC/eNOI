Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblMS4MCMDetails")>
Partial Public Class MS4MCMDetails
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property MCMDetailsID As Integer

    Public Property MCMTypeID As Integer

    Public Property SubmissionID As Integer

    Public Property BMPCategoryID As Integer

    Public Property TargetedAudienceID As Integer?

    Public Property ResponsibleDeptID As Integer

    <Required>
    <StringLength(100)>
    Public Property MeasurableGoal As String

    Public Property BMPImplementedYear As Integer

    Public Overridable Property MS4BMPCatagory As MS4BMPCatagory

    Public Overridable Property MS4MCMType As MS4MCMType

    Public Overridable Property MS4ResponsibleDept As MS4ResponsibleDept

    Public Overridable Property MS4TargetedAudience As MS4TargetedAudience

    <JsonIgnore>
    Public Overridable Property NOISubmission As NOISubmission

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
