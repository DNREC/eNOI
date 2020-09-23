Imports System.Web.UI

Public Class ErrorSummary
    Inherits CustomValidator

    Private _message As String

    Private Sub New(ByVal message As String)
        MyBase.New()
        _message = message
        MyBase.ValidationGroup = "ValidateNOI"
        MyBase.IsValid = False
        MyBase.ErrorMessage = message

    End Sub


    Public Shared Sub AddError(ByVal message As String, ByVal page As Page)
        Dim error1 As ErrorSummary = New ErrorSummary(message)
        page.Validators.Add(error1)

    End Sub



    'Public Property ErrorMessage() As String Implements System.Web.UI.IValidator.ErrorMessage
    '    Get
    '        Return _message
    '    End Get
    '    Set(ByVal value As String)

    '    End Set
    'End Property

    'Public Property IsValid() As Boolean Implements System.Web.UI.IValidator.IsValid
    '    Get
    '        Return False
    '    End Get
    '    Set(ByVal value As Boolean)

    '    End Set
    'End Property

    'Public Sub Validate() Implements System.Web.UI.IValidator.Validate

    'End Sub
End Class