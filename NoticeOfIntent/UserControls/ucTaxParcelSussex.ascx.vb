Public Class ucTaxParcelSussex
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    '1-30-09.00-0046.00

    Public Property FullTaxParcel() As String
        Get
            Return txtTPSussex1.Text & "-" & txtTPSussex2.Text & "-" & txtTPSussex3.Text & "." & txtTPSussex4.Text & "-" & txtTPSussex5.Text & "." & txtTPSussex6.Text
        End Get
        Set(value As String)
            txtTPSussex1.Text = Strings.Left(value, 1)
            txtTPSussex2.Text = Strings.Mid(value, 3, 2)
            txtTPSussex3.Text = Strings.Mid(value, 6, 2)
            txtTPSussex4.Text = Strings.Mid(value, 9, 2)
            txtTPSussex5.Text = Strings.Mid(value, 12, 4)
            txtTPSussex6.Text = Strings.Right(value, 2)
        End Set
    End Property

End Class