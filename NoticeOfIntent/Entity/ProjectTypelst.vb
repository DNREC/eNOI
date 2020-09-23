Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("vwProjectTypeTable")>
Partial Public Class ProjectTypelst
    <Key>
    <Column(Order:=0)>
    Public Property ProjectTypeID As Byte

    <Key>
    <Column(Order:=1)>
    <StringLength(50)>
    Public Property ProjectType As String
End Class
