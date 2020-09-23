Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblNOISessionStorage")>
Partial Public Class NOISessionStorage
    <Key>
    <StringLength(100)>
    Public Property username As String

    <Required>
    Public Property SessionStorageJSON As String
End Class
