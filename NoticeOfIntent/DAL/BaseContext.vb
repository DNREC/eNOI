Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq
Imports System.Data.Common
Public Class BaseContext(Of TContext As DbContext)
    Inherits DbContext

    Shared Sub New()
        Database.SetInitializer(Of TContext)(Nothing)
    End Sub

    Protected Sub New()
        MyBase.New("name=NOIDB")

        Configuration.ProxyCreationEnabled = False
        Configuration.LazyLoadingEnabled = False
    End Sub
End Class

