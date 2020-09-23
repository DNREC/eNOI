Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'txtSCSToken.Text = Request.Form("token")
        End If
    End Sub

    Private Sub Repeater1_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim link As HyperLink = CType(e.Item.FindControl("HyperLink1"), HyperLink)
            Dim currentitem As NOIProgram = e.Item.DataItem
            If currentitem.Active = "N" Then
                link.Enabled = False
            End If

        End If
    End Sub

    Public Function Repeater1_GetData() As IEnumerable(Of NoticeOfIntent.NOIProgram)
        Return CacheLookupData.GetNOIPrograms()
    End Function



End Class