Public Class SendDataToEPA
    Inherits FormBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides Sub MasterPrep()
        If Master IsNot Nothing Then
            Dim CurrentMaster As CLFNested = CType(Master, CLFNested)
            CurrentMaster.showButton(HeaderButton.DOCUMENT_MANAGEMENT) = False
            CurrentMaster.showButton(HeaderButton.LOGOUT) = False
            CurrentMaster.showButton(HeaderButton.PROJECT_DETAILS) = False
            CurrentMaster.showButton(HeaderButton.CREATE_COUPON) = False
            CurrentMaster.showButton(HeaderButton.SUBMISSIONS) = False
        End If

    End Sub

End Class