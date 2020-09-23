Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblZipcodes")>
Partial Public Class Zipcodes

    <Key>
    Public Property ZipID As Integer

    <StringLength(35)>
    Public Property PO_Name As String

    <StringLength(4)>
    Public Property StateAbv As String

    <Required>
    <StringLength(10)>
    Public Property ZIP5 As String

    <StringLength(4)>
    Public Property AreaCode As String

    <StringLength(10)>
    Public Property FIPS As String

    <StringLength(35)>
    Public Property County As String
End Class