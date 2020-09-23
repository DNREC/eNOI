Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq
Imports System.Configuration

Partial Public Class EISSQLDB
    Inherits DbContext

    Public Sub New(connstring As String)
        MyBase.New(connstring)
        System.Data.Entity.Database.SetInitializer(Of EISSQLDB)(Nothing)
        Configuration.ProxyCreationEnabled = False
        Configuration.LazyLoadingEnabled = False

    End Sub

    Public Overridable Property Zipcodes As DbSet(Of Zipcodes)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)

    End Sub

End Class