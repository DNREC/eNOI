Imports System
Public MustInherit Class MasterBasePage
    Inherits System.Web.UI.MasterPage

    Public Event MasterButtonClicked(ByVal sender As Object, ByVal e As MasterButtonClickEventArgs)
    Protected Sub raiseMasterButtonClicked(ByVal sender As Object, ByVal e As MasterButtonClickEventArgs)
        RaiseEvent MasterButtonClicked(sender, e)
    End Sub

    Public MustOverride WriteOnly Property showButton(ByVal btn As HeaderButton) As Boolean



End Class
