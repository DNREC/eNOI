Imports System.Collections.Generic

Public Class ucNameAddressInfo
    Inherits System.Web.UI.UserControl

    Public Event CheckProjectName_Validated(ByVal sender As Object, ByVal e As ServerValidateEventArgs)

    Private iscompanynamerequired As Boolean = True
    Private _usercontroltype As ucPersonorgType = ucPersonorgType.person         ' "person"  'or project

    Public Property controlname As String

    Public Property allowDEstateonly As Boolean = False


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If usercontroltype = ucPersonorgType.project Then
                cvCheckProjectName.ClientValidationFunction = "cvCheckProjectName_ClientValidation"
                txtCompanyName.Attributes.Add("placeholder", "Project Name")
            Else
                txtCompanyName.Attributes.Add("placeholder", "Company Name")
            End If
        End If
    End Sub

#Region "Properties"

    Public ReadOnly Property textCompanyName As TextBox
        Get
            Return txtCompanyName
        End Get
    End Property

    Public Property usercontroltype As ucPersonorgType
        Get
            Return _usercontroltype
        End Get
        Set(value As ucPersonorgType)
            _usercontroltype = value
        End Set
    End Property

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
            Return ddlCompanyType.SelectedValue
        End Get
        Set(value As String)
            ddlCompanyType.SelectedValue = value
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
            Return txtCompanyName.Text
        End Get
        Set(value As String)
            txtCompanyName.Text = value
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
            Return txtLastName.Text
        End Get
        Set(value As String)
            txtLastName.Text = value
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
            Return txtFirstName.Text
        End Get
        Set(value As String)
            txtFirstName.Text = value
        End Set
    End Property

    Public ReadOnly Property PersonFullName() As String
        Get
            Return txtLastName.Text + ", " + txtFirstName.Text
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
            Return txtAddress1.Text
        End Get
        Set(value As String)
            txtAddress1.Text = value
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
            Return txtAddress2.Text
        End Get
        Set(value As String)
            txtAddress2.Text = value
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
            Return txtZip.Text
        End Get
        Set(value As String)
            txtZip.Text = value
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
            Return txtCity.Text
        End Get
        Set(value As String)
            txtCity.Text = value
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
            Return ddlStateAbv.SelectedValue
        End Get
        Set(value As String)
            ddlStateAbv.SelectedValue = value
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
            Return txtPhone.Text
        End Get
        Set(value As String)
            txtPhone.Text = value
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
            Return txtExt.Text
        End Get
        Set(value As String)
            txtExt.Text = value
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
            Return txtMobile.Text
        End Get
        Set(value As String)
            txtMobile.Text = value
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
            Return txtEmail.Text
        End Get
        Set(value As String)
            txtEmail.Text = value
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
            Return ddlCounty.SelectedValue
        End Get
        Set(value As String)
            ddlCounty.SelectedValue = value
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
            Return txtMunicipality.Text
        End Get
        Set(value As String)
            txtMunicipality.Text = value
        End Set
    End Property

    Public WriteOnly Property EnableName As Boolean
        Set(value As Boolean)
            txtFirstName.Enabled = value
            txtLastName.Enabled = value
        End Set
    End Property

    Public WriteOnly Property EnableCompany As Boolean
        Set(value As Boolean)
            ddlCompanyType.Enabled = value
            txtCompanyName.Enabled = value
        End Set
    End Property

    Public WriteOnly Property Enabled As Boolean
        Set(value As Boolean)
            'For Each ctrl As Control In Me.Controls
            '    If ctrl.GetType() Is GetType(TextBox) Then
            '        Dim txt As TextBox = CType(ctrl, TextBox)
            '        txt.Enabled = value
            '    End If

            '    If ctrl.GetType() Is GetType(DropDownList) Then
            '        Dim txt As DropDownList = CType(ctrl, DropDownList)
            '        txt.Enabled = value
            '    End If
            'Next

            ddlCompanyType.Enabled = value
            ddlCounty.Enabled = value
            ddlStateAbv.Enabled = value
            txtCompanyName.Enabled = value
            txtFirstName.Enabled = value
            txtLastName.Enabled = value
            txtAddress1.Enabled = value
            txtAddress2.Enabled = value
            txtCity.Enabled = value
            txtZip.Enabled = value
            txtPhone.Enabled = value
            txtExt.Enabled = value
            txtMobile.Enabled = value
            txtEmail.Enabled = value
            txtMunicipality.Enabled = value

            ValidationEnabled = value

            'If iscompanynamerequired = True Then
            '    rfvCompanyName.Enabled = value
            'End If
            'rfvFirstName.Enabled = value
            'rfvLastName.Enabled = value
            'rfvAddress1.Enabled = value
            'rfvCity.Enabled = value
            'rfvZip.Enabled = value
            'rfvStateAbv.Enabled = value
            'rfvPhone.Enabled = value
            'rfvEmail.Enabled = value
            'rfvCounty.Enabled = value
            'rfvMunicipality.Enabled = value
            'If usercontroltype = ucPersonorgType.project Then
            '    cvCheckProjectName.Enabled = value
            'End If

        End Set
    End Property

    Public WriteOnly Property ValidationEnabled As Boolean
        Set(value As Boolean)

            If iscompanynamerequired = True Then
                rfvCompanyName.Enabled = value
            End If
            rfvFirstName.Enabled = value
            rfvLastName.Enabled = value
            rfvAddress1.Enabled = value
            rfvCity.Enabled = value
            rfvZip.Enabled = value
            rfvStateAbv.Enabled = value
            rfvPhone.Enabled = value
            rfvEmail.Enabled = value
            rfvCounty.Enabled = value
            rfvMunicipality.Enabled = value
            If usercontroltype = ucPersonorgType.project Then
                cvCheckProjectName.Enabled = value
            End If
        End Set
    End Property

    Public Property ValidateCompanyName As Boolean
        Get
            Return iscompanynamerequired
        End Get
        Set(value As Boolean)
            iscompanynamerequired = value
            rfvCompanyName.Enabled = iscompanynamerequired
        End Set
    End Property

    Public Property rfvLastNameEnabled As Boolean
        Get
            Return rfvLastName.Enabled
        End Get
        Set(value As Boolean)
            rfvLastName.Enabled = value
        End Set
    End Property

    Public Property rfvFirstNameEnabled As Boolean
        Get
            Return rfvFirstName.Enabled
        End Get
        Set(value As Boolean)
            rfvFirstName.Enabled = value
        End Set
    End Property

    Public Property cvCheckProjectNameEnabled As Boolean
        Get
            Return cvCheckProjectName.Enabled
        End Get
        Set(value As Boolean)
            cvCheckProjectName.Enabled = value
        End Set
    End Property

    Public WriteOnly Property ValidationGroup As String
        Set(value As String)
            rfvCompanyName.ValidationGroup = value
            cvCheckProjectName.ValidationGroup = value
            rfvLastName.ValidationGroup = value
            rfvFirstName.ValidationGroup = value
            rfvAddress1.ValidationGroup = value
            rfvZip.ValidationGroup = value
            rfvCity.ValidationGroup = value
            rfvStateAbv.ValidationGroup = value
            rfvPhone.ValidationGroup = value
            rfvEmail.ValidationGroup = value
            rfvCounty.ValidationGroup = value
            rfvMunicipality.ValidationGroup = value
        End Set
    End Property





#End Region

    'Public Sub populateCompanyType(ByVal companytypelst As IList(Of CompanyTypelst))
    '    ddlCompanyType.DataSource = companytypelst
    '    ddlCompanyType.DataBind()
    'End Sub

    'Public Sub populateState(ByVal statelst As IList(Of StateAbvlst))
    '    ddlStateAbv.DataSource = statelst
    '    ddlStateAbv.DataBind()
    'End Sub

    'Public Function ControlName() As String
    '    If Me.ID = "ucnaOwnerInfo" Then
    '        Return "Owner Information"
    '    ElseIf Me.ID = "ucnaContactInfo" Then
    '        Return "Contact Information"
    '    ElseIf Me.ID = "ucnaBilleeInfo" Then
    '        Return "Payee Information"
    '    Else
    '        Return ""
    '    End If
    'End Function

    Public Function GetStates() As IList(Of StateAbvlst)
        'If Me.ID = "ucnaSiteInfo" Then
        If allowDEstateonly = True Then
            Return CacheLookupData.GetStates().Where(Function(a) a.StateAbv = "DE").ToList()
        Else
            Return CacheLookupData.GetStates()
        End If


    End Function

    Public Function GetCompanyType() As IList(Of CompanyTypelst)
        'Dim bal As New SubmissionBAL
        Return CacheLookupData.GetCompanyType() 'bal.GetCompanyType()
    End Function

    Public Sub populateddlStateAbv()
        ddlStateAbv.DataBind()
    End Sub

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        rfvCompanyName.DataBind()
        rfvFirstName.DataBind()
        rfvLastName.DataBind()
        rfvAddress1.DataBind()
        rfvCity.DataBind()
        rfvZip.DataBind()
        rfvStateAbv.DataBind()
        rfvPhone.DataBind()
        rfvEmail.DataBind()
        rfvCounty.DataBind()
        rfvMunicipality.DataBind()
    End Sub

    Private Sub cvCheckProjectName_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvCheckProjectName.ServerValidate
        If usercontroltype = ucPersonorgType.project Then
            RaiseEvent CheckProjectName_Validated(source, args)
        End If
    End Sub

End Class