Public Class ucTaxParcelKent
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function GetTaxKentHundred() As IList(Of TaxKentHundred)
        Dim bal As New SWBAL
        Return bal.GetTaxKentHundred()
    End Function

    Public Function GetTaxKentTowns() As IList(Of TaxKentTown)
        Dim bal As New SWBAL
        Return bal.GetTaxKentTowns()
    End Function


    'MN-00-123.01-02-34.00.000

    Public Property FullTaxParcel() As String
        Get
            Return ddlTaxKentHundred.SelectedValue & "-" & ddlKentCommunityCode.SelectedValue & "-" & txtKentTP3.Text & "." & txtKentTP4.Text & "-" & txtKentTP5.Text & "-" & txtKentTP6.Text & "." & txtKentTP7.Text & "." & txtKentTP8.Text
        End Get
        Set(value As String)
            ddlTaxKentHundred.SelectedValue = Strings.Left(value, 2)
            ddlKentCommunityCode.SelectedValue = Strings.Mid(value, 4, 2)
            txtKentTP3.Text = Strings.Mid(value, 7, 3)
            txtKentTP4.Text = Strings.Mid(value, 11, 2)
            txtKentTP5.Text = Strings.Mid(value, 14, 2)
            txtKentTP6.Text = Strings.Mid(value, 17, 2)
            txtKentTP8.Text = Strings.Right(value, 3)
        End Set
    End Property

End Class