Public Class CLFNested
    Inherits MasterBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'lblAdminDeptName.Text = CacheLookupData.GetDeptName()
            'lblAdminPhone.Text = CacheLookupData.GetDeptPhone() ' ConfigurationManager.AppSettings("NOIAdminPhone").ToString
            'hlAdminEmail.NavigateUrl = "mailto:" & CacheLookupData.GetDeptEmail() ' ConfigurationManager.AppSettings("NOIAdminContactEmail").ToString()
            'hlAdminEmail.Text = CacheLookupData.GetDeptEmail() ' ConfigurationManager.AppSettings("NOIAdminContactEmail").ToString()
        End If
    End Sub

    Public WriteOnly Property ValidationGroup() As String
        Set(value As String)
            ValidationSummary1.ValidationGroup = value
        End Set
    End Property


    Public Overrides WriteOnly Property showButton(btn As HeaderButton) As Boolean
        Set(value As Boolean)
            Select Case btn
                Case HeaderButton.HOME
                    lnkHome.Visible = value
                Case HeaderButton.PROJECT_DETAILS
                    lnkProjectDet.Visible = value
                Case HeaderButton.CREATE_COUPON
                    lnkExemption.Visible = value
                Case HeaderButton.DOCUMENT_MANAGEMENT
                    lnkDocMng.Visible = value
                Case HeaderButton.LOGOUT
                    lnkLogout.Visible = value
                Case HeaderButton.SUBMISSIONS
                    lnkSubmissions.Visible = value
            End Select
        End Set
    End Property


    Private Sub lnkHome_Click(sender As Object, e As EventArgs) Handles lnkHome.Click, lnkProjectDet.Click, lnkLogout.Click, lnkDocMng.Click, lnkExemption.Click, lnkSubmissions.Click
        Dim e1 As MasterButtonClickEventArgs = Nothing
        Select Case CType(sender, LinkButton).ID
            Case lnkHome.ID
                e1 = New MasterButtonClickEventArgs(HeaderButton.HOME)
            Case lnkProjectDet.ID
                e1 = New MasterButtonClickEventArgs(HeaderButton.PROJECT_DETAILS)
            Case lnkExemption.ID
                e1 = New MasterButtonClickEventArgs(HeaderButton.CREATE_COUPON)
            Case lnkDocMng.ID
                e1 = New MasterButtonClickEventArgs(HeaderButton.DOCUMENT_MANAGEMENT)
            Case lnkLogout.ID
                e1 = New MasterButtonClickEventArgs(HeaderButton.LOGOUT)
            Case lnkSubmissions.ID
                e1 = New MasterButtonClickEventArgs(HeaderButton.SUBMISSIONS)
        End Select
        raiseMasterButtonClicked(sender, e1)
    End Sub

    Public WriteOnly Property AdminDeptName() As String
        Set(value As String)
            lblAdminDeptName.Text = value
        End Set
    End Property

    Public WriteOnly Property AdminDeptPhone() As String
        Set(value As String)
            lblAdminPhone.Text = value
        End Set
    End Property

    Public WriteOnly Property AdminDeptEmail() As String
        Set(value As String)
            hlAdminEmail.Text = value
            hlAdminEmail.NavigateUrl = "mailto:" & value
        End Set
    End Property

End Class