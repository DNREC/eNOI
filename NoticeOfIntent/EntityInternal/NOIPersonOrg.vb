Public Class NOIPersonOrg
    Private _progafflid As Int32
    Private _piid As Int32
    Private _personorgtypecode As String
    Private _orgname As String
    Private _lname As String
    Private _fname As String
    Private _mname As String
    Private _nameprefix As String
    Private _namesuffix As String
    Private _address1 As String
    Private _address2 As String
    Private _city As String
    Private _stateabv As String
    Private _postalcode As String
    Private _phone As String
    Private _phoneext As String
    Private _mobile As String
    Private _emailaddress As String
    Private _affltype As Int16
    Private _comments As String
    Private _startdate As Date?
    Private _enddate As Date?
    Private _active As String

    Private _haschanged As Boolean = False

#Region "Public Properties"

    Public Property ProgAfflID As Int32
        Get
            Return _progafflid
        End Get
        Set(value As Int32)
            _progafflid = value
        End Set
    End Property

    Public Property PIID As Int32
        Get
            Return _piid
        End Get
        Set(value As Int32)
            _piid = value
        End Set
    End Property

    Public Property PersonOrgTypeCode As String
        Get
            Return _personorgtypecode
        End Get
        Set(value As String)
            If _personorgtypecode <> value Then
                _haschanged = True
            End If
            _personorgtypecode = value
        End Set
    End Property

    Public Property OrgName As String
        Get
            Return _orgname
        End Get
        Set(value As String)
            If _orgname <> value Then
                _haschanged = True
            End If
            _orgname = value
        End Set
    End Property

    Public Property LName As String
        Get
            Return _lname
        End Get
        Set(value As String)
            If _lname <> value Then
                _haschanged = True
            End If
            _lname = value
        End Set
    End Property

    Public Property FName As String
        Get
            Return _fname
        End Get
        Set(value As String)
            If _fname <> value Then
                _haschanged = True
            End If
            _fname = value
        End Set
    End Property

    Public Property MName As String
        Get
            Return _mname
        End Get
        Set(value As String)
            If _mname <> value Then
                _haschanged = True
            End If
            _mname = value
        End Set
    End Property

    Public Property NamePrefix As String
        Get
            Return _nameprefix
        End Get
        Set(value As String)
            If _nameprefix <> value Then
                _haschanged = True
            End If
            _nameprefix = value
        End Set
    End Property

    Public Property NameSuffix As String
        Get
            Return _namesuffix
        End Get
        Set(value As String)
            If _namesuffix <> value Then
                _haschanged = True
            End If
            _namesuffix = value
        End Set
    End Property

    Public Property Address1 As String
        Get
            Return _address1
        End Get
        Set(value As String)
            If _address1 <> value Then
                _haschanged = True
            End If
            _address1 = value
        End Set
    End Property

    Public Property Address2 As String
        Get
            Return _address2
        End Get
        Set(value As String)
            If _address2 <> value Then
                _haschanged = True
            End If
            _address2 = value
        End Set
    End Property

    Public Property City As String
        Get
            Return _city
        End Get
        Set(value As String)
            If _city <> value Then
                _haschanged = True
            End If
            _city = value
        End Set
    End Property

    Public Property StateAbv As String
        Get
            Return _stateabv
        End Get
        Set(value As String)
            If _stateabv <> value Then
                _haschanged = True
            End If
            _stateabv = value
        End Set
    End Property

    Public Property PostalCode As String
        Get
            Return _postalcode
        End Get
        Set(value As String)
            If _postalcode <> value Then
                _haschanged = True
            End If
            _postalcode = value
        End Set
    End Property

    Public Property Phone As String
        Get
            Return _phone
        End Get
        Set(value As String)
            If _phone <> value Then
                _haschanged = True
            End If
            _phone = value
        End Set
    End Property

    Public Property PhoneExt As String
        Get
            Return _phoneext
        End Get
        Set(value As String)
            If _phoneext <> value Then
                _haschanged = True
            End If
            _phoneext = value
        End Set
    End Property

    Public Property Mobile As String
        Get
            Return _mobile
        End Get
        Set(value As String)
            If _mobile <> value Then
                _haschanged = True
            End If
            _mobile = value
        End Set
    End Property

    Public Property EmailAddress As String
        Get
            Return _emailaddress
        End Get
        Set(value As String)
            If _emailaddress <> value Then
                _haschanged = True
            End If
            _emailaddress = value
        End Set
    End Property

    Public Property AfflType As Int16
        Get
            Return _affltype
        End Get
        Set(value As Int16)
            _affltype = value
        End Set
    End Property

    Public Property Comments As String
        Get
            Return _comments
        End Get
        Set(value As String)
            If _comments <> value Then
                _haschanged = True
            End If
            _comments = value
        End Set
    End Property

    Public Property StartDate As Date?
        Get
            Return _startdate
        End Get
        Set(value As Date?)
            If _startdate <> value Then
                _haschanged = True
            End If
            _startdate = value
        End Set
    End Property

    Public Property EndDate() As Date?
        Get
            Return _enddate
        End Get
        Set(ByVal value As Date?)
            If _enddate <> value Then
                _haschanged = True
            End If
            _enddate = value
        End Set
    End Property

    Public Property Active() As String
        Get
            Return _active
        End Get
        Set(ByVal value As String)
            If _active <> value Then
                _haschanged = True
            End If
            _active = value
        End Set
    End Property


    Public Property HasChanged As Boolean
        Get
            Return _haschanged
        End Get
        Set(value As Boolean)
            _haschanged = value
        End Set
    End Property

#End Region

End Class
