Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOIProject")>
Partial Public Class NOIProject
    Implements IEntity


    Public Sub New()

        NOISubmission = New HashSet(Of NOISubmission)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property ProjectID As Integer

    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    <StringLength(33)>
    Public Property ProjectNumber As String

    <Required>
    <StringLength(80)>
    <Index(IsUnique:=True)>
    Public Property ProjectName As String

    <StringLength(60)>
    Public Property PermitNumber As String

    Public Property ProgramID As Integer

    <StringLength(64)>
    Public Property CreatedBy As String

    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    Public Property CreatedDate As Date

    <StringLength(64)>
    Public Property LastChgBy As String

    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    Public Property LastChgDate As Date

    <JsonIgnore>
    Public Overridable Property NOISubmission As ICollection(Of NOISubmission)

    Public Overridable Property NOIProgram As NOIProgram

    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState

    <NotMapped>
    Public Property label As String

    <NotMapped>
    Public Property value As String

End Class
