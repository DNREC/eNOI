Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("vwPesticidePattern")>
Public Class NOIPesticidePattern
    Implements IEntity

    <Key>
    Public Property PesticidePatternID As Integer

    <Required>
    <StringLength(100)>
    Public Property PesticidePattern As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Detached Implements IEntity.EntityState
End Class
