Public Class ucTaxParcelAdd
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


    Public ReadOnly Property SelectedCounty() As String
        Get
            Dim countyStr As String = String.Empty
            If rbNewCastle.Checked = True Then
                countyStr = Left(rbNewCastle.Text, 1)
            ElseIf rbKent.Checked = True Then
                countyStr = Left(rbKent.Text, 1)
            ElseIf rbSussex.Checked = True Then
                countyStr = Left(rbSussex.Text, 1)
            End If
            Return countyStr
        End Get
    End Property

    Public ReadOnly Property FullTaxParcel() As String
        Get
            Dim taxparcelstr As String = String.Empty
            If rbNewCastle.Checked = True Then
                If txtTPNC1.Text <> String.Empty AndAlso txtTPNC2.Text <> String.Empty AndAlso txtTPNC3.Text <> String.Empty AndAlso txtTPNC4.Text <> String.Empty Then
                    taxparcelstr = txtTPNC1.Text & "-" & txtTPNC2.Text & "." & txtTPNC3.Text & "-" & txtTPNC4.Text
                End If
            ElseIf rbKent.Checked = True Then
                If txtKentTP3.Text <> String.Empty AndAlso txtKentTP4.Text <> String.Empty AndAlso txtKentTP5.Text <> String.Empty AndAlso txtKentTP6.Text <> String.Empty AndAlso txtKentTP7.Text <> String.Empty AndAlso txtKentTP8.Text <> String.Empty Then
                    taxparcelstr = ddlTaxKentHundred.SelectedValue & "-" & ddlKentCommunityCode.SelectedValue & "-" & txtKentTP3.Text & "." & txtKentTP4.Text & "-" & txtKentTP5.Text & "-" & txtKentTP6.Text & "." & txtKentTP7.Text & "." & txtKentTP8.Text
                End If
            ElseIf rbSussex.Checked = True Then
                If txtTPSussex1.Text <> String.Empty AndAlso txtTPSussex2.Text <> String.Empty AndAlso txtTPSussex3.Text <> String.Empty AndAlso txtTPSussex4.Text <> String.Empty AndAlso txtTPSussex5.Text <> String.Empty AndAlso txtTPSussex6.Text <> String.Empty Then
                    taxparcelstr = txtTPSussex1.Text & "-" & txtTPSussex2.Text & "-" & txtTPSussex3.Text & "." & txtTPSussex4.Text & "-" & txtTPSussex5.Text & "." & txtTPSussex6.Text
                End If
            End If
            Return taxparcelstr
        End Get
    End Property
End Class