Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblNOIWebPages")>
Partial Public Class NOIWebPage
    Public Sub New()
        NOIWebPageCtrlTxts = New HashSet(Of NOIWebPageCtrlTxt)()
    End Sub

    <Key>
    Public Property WebPageID As Integer

    Public Property ProgramID As Integer

    <Required>
    <StringLength(100)>
    Public Property WebPage As String

    Public Overridable Property NOIProgram As NOIProgram

    Public Overridable Property NOIWebPageCtrlTxts As ICollection(Of NOIWebPageCtrlTxt)
End Class
