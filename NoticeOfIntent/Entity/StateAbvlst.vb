Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("vwStateAbvTable")>
Partial Public Class StateAbvlst
    <Key>
    <Column(Order:=0)>
    <StringLength(2)>
    Public Property StateAbv As String

    <Key>
    <Column(Order:=1)>
    <StringLength(30)>
    Public Property State As String
End Class
