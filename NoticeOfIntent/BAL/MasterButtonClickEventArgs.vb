Public Class MasterButtonClickEventArgs
    Inherits EventArgs


    Private _btn As HeaderButton

    Public Sub New(ByVal btn As HeaderButton)
        _btn = btn
    End Sub

    ''' <summary>
    ''' get the clicked button
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property clickedButton() As HeaderButton
        Get
            Return _btn
        End Get
    End Property

End Class

Public Enum HeaderButton
    LOGOUT = 0
    HOME = 1
    SUBMISSIONS = 2
    DOCUMENT_MANAGEMENT = 3

    'admin buttons
    PROJECT_DETAILS = 4
    CREATE_COUPON = 5


End Enum