Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblNOIWebPageCtrlTxt")>
Partial Public Class NOIWebPageCtrlTxt
    <Key>
    Public Property WebPageCtrlID As Integer

    Public Property WebPageID As Integer

    <Required>
    <StringLength(100)>
    Public Property CtrlID As String

    <Required>
    <StringLength(100)>
    Public Property CtrlProperty As String

    <Required>
    <StringLength(500)>
    Public Property CtrlText As String

    'Public Overridable Property NOIWebPage As NOIWebPage
End Class
