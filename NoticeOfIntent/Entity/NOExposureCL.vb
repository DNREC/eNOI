Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports Newtonsoft.Json

<Table("tblNOExposureCL")>
Partial Public Class NOExposureCL
    Public Sub New()
        NOISubmissionNE = New HashSet(Of NOISubmissionNE)()
    End Sub

    <Key>
    Public Property NOExposureCLID As Integer

    <Required>
    <StringLength(200)>
    Public Property Question As String

    <Required>
    <StringLength(1)>
    Public Property Active As String

    <JsonIgnore>
    Public Overridable Property NOISubmissionNE As ICollection(Of NOISubmissionNE)

End Class
