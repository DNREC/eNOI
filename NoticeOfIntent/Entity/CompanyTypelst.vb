Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("vwCompanyTypeTable")>
Partial Public Class CompanyTypelst
    <Key>
    <Column(Order:=0)>
    <StringLength(1)>
    Public Property PersonOrgTypeCode As String

    <Key>
    <Column(Order:=1)>
    <StringLength(30)>
    Public Property PersonOrgType As String
End Class
