Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblMS4RWPollutants")>
Partial Public Class MS4RWPollutants
    Implements IEntity

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property RWPollutantID As Integer

    Public Property RWID As Integer

    Public Property PollutantID As Integer

    Public Property ResponsibleDeptID As Integer

    Public Overridable Property MS4Pollutants As MS4Pollutants

    Public Overridable Property MS4ReceivingWaters As MS4ReceivingWaters

    Public Overridable Property MS4ResponsibleDept As MS4ResponsibleDept

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
