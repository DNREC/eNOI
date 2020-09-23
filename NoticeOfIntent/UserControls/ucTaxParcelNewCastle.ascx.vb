Public Class ucTaxParcelNewCastle
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property FullTaxParcel() As String
        Get
            Return txtTPNC1.Text & "-" & txtTPNC2.Text & "." & txtTPNC3.Text & "-" & txtTPNC4.Text
        End Get
        Set(value As String)
            txtTPNC1.Text = Strings.Left(value, 2)
            txtTPNC2.Text = Strings.Mid(value, 4, 3)
            txtTPNC3.Text = Strings.Mid(value, 8, 2)
            txtTPNC4.Text = Strings.Right(value, 3)
        End Set
    End Property

End Class