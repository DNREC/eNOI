Public Class ucNameAddressInfoReadOnly
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

#Region "Properties"

    Public Property companytypevisible As Boolean
        Get
            Return divcompanytype.Visible
        End Get
        Set(value As Boolean)
            divcompanytype.Visible = value
        End Set
    End Property

    Public Property companynamevisible As Boolean
        Get
            Return divcompanyname.Visible
        End Get
        Set(value As Boolean)
            divcompanyname.Visible = value
        End Set
    End Property

    Public Property personnamevisible As Boolean
        Get
            Return divpersonname.Visible
        End Get
        Set(value As Boolean)
            divpersonname.Visible = value
        End Set
    End Property

    Public Property address1visible As Boolean
        Get
            Return divaddress1.Visible
        End Get
        Set(value As Boolean)
            divaddress1.Visible = value
        End Set
    End Property

    Public Property address2visible As Boolean
        Get
            Return divaddress2.Visible
        End Get
        Set(value As Boolean)
            divaddress2.Visible = value
        End Set
    End Property

    Public Property cityzipstatevisible As Boolean
        Get
            Return divcityzipstate.Visible
        End Get
        Set(ByVal value As Boolean)
            divcityzipstate.Visible = value
        End Set
    End Property

    Public Property phonevisible As Boolean
        Get
            Return divphone.Visible
        End Get
        Set(value As Boolean)
            divphone.Visible = value
        End Set
    End Property

    Public Property emailvisible As Boolean
        Get
            Return divemail.Visible
        End Get
        Set(value As Boolean)
            divemail.Visible = value
        End Set
    End Property

    Public Property countymunicipalityvisible As Boolean
        Get
            Return countymunicipality.Visible
        End Get
        Set(value As Boolean)
            countymunicipality.Visible = value
        End Set
    End Property



    Public Property CompanyType As String
        Get
            Return hfCompanyType.Value ' lblCompanyTypeDisp.Text
        End Get
        Set(value As String)
            hfCompanyType.Value = value
            lblCompanyTypeDisp.Text = CacheLookupData.GetCompanyType().Where(Function(a) a.PersonOrgTypeCode = value).Select(Function(b) b.PersonOrgType).Single()
        End Set
    End Property

    Public Property CompanyNameLabel As String
        Get
            Return lblCompanyName.Text
        End Get
        Set(value As String)
            lblCompanyName.Text = value
        End Set
    End Property

    Public Property CompanyName As String
        Get
            Return lblCompanyNameDisp.Text
        End Get
        Set(value As String)
            lblCompanyNameDisp.Text = value
        End Set
    End Property

    Public Property LastNameLabel As String
        Get
            Return lblLastName.Text
        End Get
        Set(value As String)
            lblLastName.Text = value
        End Set
    End Property

    Public Property LastName As String
        Get
            Return lblLastNameDisp.Text
        End Get
        Set(value As String)
            lblLastNameDisp.Text = value
        End Set
    End Property

    Public Property FirstNameLabel As String
        Get
            Return lblFirstName.Text
        End Get
        Set(value As String)
            lblFirstName.Text = value
        End Set
    End Property

    Public Property FirstName As String
        Get
            Return lblFirstNameDisp.Text
        End Get
        Set(value As String)
            lblFirstNameDisp.Text = value
        End Set
    End Property

    Public ReadOnly Property PersonFullName() As String
        Get
            Return lblLastNameDisp.Text + ", " + lblFirstNameDisp.Text
        End Get

    End Property

    Public Property Address1Label As String
        Get
            Return lblAddress1.Text
        End Get
        Set(value As String)
            lblAddress1.Text = value
        End Set
    End Property

    Public Property Address1 As String
        Get
            Return lblAddress1Disp.Text
        End Get
        Set(value As String)
            lblAddress1Disp.Text = value
        End Set
    End Property

    Public Property Address2Label As String
        Get
            Return lblAddress2.Text
        End Get
        Set(value As String)
            lblAddress2.Text = value
        End Set
    End Property

    Public Property Address2 As String
        Get
            Return lblAddress2Disp.Text
        End Get
        Set(value As String)
            lblAddress2Disp.Text = value
        End Set
    End Property

    Public Property ZipLabel As String
        Get
            Return lblZip.Text
        End Get
        Set(value As String)
            lblZip.Text = value
        End Set
    End Property

    Public Property Zip As String
        Get
            Return lblZipDisp.Text
        End Get
        Set(value As String)
            lblZipDisp.Text = value
        End Set
    End Property

    Public Property CityLabel As String
        Get
            Return lblCity.Text
        End Get
        Set(value As String)
            lblCity.Text = value
        End Set
    End Property

    Public Property City As String
        Get
            Return lblCityDisp.Text
        End Get
        Set(value As String)
            lblCityDisp.Text = value
        End Set
    End Property

    Public Property StateLabel As String
        Get
            Return lblState.Text
        End Get
        Set(value As String)
            lblState.Text = value
        End Set
    End Property

    Public Property StateAbv As String
        Get
            Return hfStateAbv.Value
        End Get
        Set(value As String)
            hfStateAbv.Value = value
            If value <> Nothing Then
                lblStateDisp.Text = CacheLookupData.GetStates().Where(Function(a) a.StateAbv.ToUpper = value.ToUpper).Select(Function(b) b.State).Single()
            End If
        End Set
    End Property

    Public Property PhoneLabel As String
        Get
            Return lblPhone.Text
        End Get
        Set(value As String)
            lblPhone.Text = value
        End Set
    End Property

    Public Property Phone As String
        Get
            Return lblPhoneDisp.Text
        End Get
        Set(value As String)
            lblPhoneDisp.Text = value
        End Set
    End Property

    Public Property ExtensionLabel As String
        Get
            Return lblExt.Text
        End Get
        Set(value As String)
            lblExt.Text = value
        End Set
    End Property

    Public Property Ext As String
        Get
            Return lblExtDisp.Text
        End Get
        Set(value As String)
            lblExtDisp.Text = value
        End Set
    End Property

    Public Property MobileLabel As String
        Get
            Return lblMobile.Text
        End Get
        Set(value As String)
            lblMobile.Text = value
        End Set
    End Property

    Public Property Mobile As String
        Get
            Return lblMobileDisp.Text
        End Get
        Set(value As String)
            lblMobileDisp.Text = value
        End Set
    End Property

    Public Property EmailLabel As String
        Get
            Return lblEmail.Text
        End Get
        Set(value As String)
            lblEmail.Text = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return lblEmailDisp.Text
        End Get
        Set(value As String)
            lblEmailDisp.Text = value
        End Set
    End Property

    Public Property CountyLabel As String
        Get
            Return lblCounty.Text
        End Get
        Set(value As String)
            lblCounty.Text = value
        End Set
    End Property

    Public Property County As String
        Get
            Return lblCountyDisp.Text
        End Get
        Set(value As String)
            lblCountyDisp.Text = value
        End Set
    End Property

    Public Property MunicipalityLabel As String
        Get
            Return lblMunicipality.Text
        End Get
        Set(value As String)
            lblMunicipality.Text = value
        End Set
    End Property

    Public Property Municipality As String
        Get
            Return lblMunicipalityDisp.Text
        End Get
        Set(value As String)
            lblMunicipalityDisp.Text = value
        End Set
    End Property












#End Region

End Class