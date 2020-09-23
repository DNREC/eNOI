Public Class NOIProjectInternal

    Public Sub New()
        _swconstructbmpdet = New HashSet(Of SWConstructBMP)()
        _taxparcel = New HashSet(Of NOITaxParcel)()
        _personorg = New HashSet(Of NOIPersonOrg)()
        _permitstatuses = New HashSet(Of PermitStatus)()

        _siccodes = New HashSet(Of NOIPiSIC)()
        _naicscodes = New HashSet(Of NOIPiNAICS)()
        _outfalls = New HashSet(Of NOIOutfall)()

        _noiapchemicalslst = New HashSet(Of NOIAPChemicals)()
    End Sub


    Public Property PIID As Int32
    Public Property PermitNumber As String

    Public Property PermitID As Int32
    Public Property PermitTypeCode As Int16

    Private _projectname As String
    Private _projectaddress As String
    Private _projectcity As String
    Private _projectstateabv As String
    Private _projectpostalcode As String
    Private _latitude As Decimal?
    Private _longitude As Decimal?
    Private _x As Decimal?
    Private _y As Decimal?
    Private _delegatedagencyid As Integer?
    Public Property ProjectAfflID As Int32

    Public Property HUC12 As String
    Public Property Watershed As String
    Private _comments As String
    Public Property SWConstructDet As SWConstruct

    Private _personorg As ICollection(Of NOIPersonOrg)
    Private _swconstructbmpdet As ICollection(Of SWConstructBMP)
    Private _taxparcel As ICollection(Of NOITaxParcel)
    Private _permitstatuses As ICollection(Of PermitStatus)

    'for ISW

    Private _siccodes As ICollection(Of NOIPiSIC)
    Private _naicscodes As ICollection(Of NOIPiNAICS)
    Private _outfalls As ICollection(Of NOIOutfall)

    'For AP
    Private _noiapannualthresholddet As NOIAPAnnualThreshold
    Private _noiapchemicalslst As ICollection(Of NOIAPChemicals)



    Private _datereceived As Date

    Private _haschanged As Boolean = False




#Region "Public Properties"

    Public Property ProjectName As String
        Get
            Return _projectname
        End Get
        Set(value As String)
            If _projectname <> value Then
                _haschanged = True
            End If
            _projectname = value
        End Set
    End Property

    Public Property ProjectAddress As String
        Get
            Return _projectaddress
        End Get
        Set(value As String)
            If _projectaddress <> value Then
                _haschanged = True
            End If
            _projectaddress = value
        End Set
    End Property


    Public Property ProjectCity As String
        Get
            Return _projectcity
        End Get
        Set(value As String)
            If _projectcity <> value Then
                _haschanged = True
            End If
            _projectcity = value
        End Set
    End Property

    Public Property ProjectStateAbv As String
        Get
            Return _projectstateabv
        End Get
        Set(value As String)
            If _projectstateabv <> value Then
                _haschanged = True
            End If
            _projectstateabv = value
        End Set
    End Property

    Public Property ProjectPostalCode As String
        Get
            Return _projectpostalcode
        End Get
        Set(value As String)
            If _projectpostalcode <> value Then
                _haschanged = True
            End If
            _projectpostalcode = value
        End Set
    End Property

    Public Property Latitude As Decimal?
        Get
            Return _latitude
        End Get
        Set(value As Decimal?)
            If _latitude <> value Then
                _haschanged = True
            End If
            _latitude = value
        End Set
    End Property

    Public Property Longitude As Decimal?
        Get
            Return _longitude
        End Get
        Set(value As Decimal?)
            If _longitude <> value Then
                _haschanged = True
            End If
            _longitude = value
        End Set
    End Property

    Public Property X As Decimal?
        Get
            Return _x
        End Get
        Set(value As Decimal?)
            If _x <> value Then
                _haschanged = True
            End If
            _x = value
        End Set
    End Property

    Public Property Y As Decimal?
        Get
            Return _y
        End Get
        Set(value As Decimal?)
            If _y <> value Then
                _haschanged = True
            End If
            _y = value
        End Set
    End Property

    Public Property DelegatedAgencyID As Integer?
        Get
            Return _delegatedagencyid
        End Get
        Set(value As Integer?)
            If _delegatedagencyid <> value Then
                _haschanged = True
            End If
            _delegatedagencyid = value
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

    Public Property HasChanged As Boolean
        Get
            Return _haschanged
        End Get
        Set(value As Boolean)
            _haschanged = value
        End Set
    End Property



    Public Property PersonOrg As ICollection(Of NOIPersonOrg)
        Get
            Return _personorg
        End Get
        Set(value As ICollection(Of NOIPersonOrg))
            _personorg = value
        End Set
    End Property

    Public Property SWConstructBMPDet As ICollection(Of SWConstructBMP)
        Get
            Return _swconstructbmpdet
        End Get
        Set(value As ICollection(Of SWConstructBMP))
            _swconstructbmpdet = value
        End Set
    End Property

    Public Property TaxParcel As ICollection(Of NOITaxParcel)
        Get
            Return _taxparcel
        End Get
        Set(value As ICollection(Of NOITaxParcel))
            _taxparcel = value
        End Set
    End Property

    Public Property SICCodes As ICollection(Of NOIPiSIC)
        Get
            Return _siccodes
        End Get
        Set(value As ICollection(Of NOIPiSIC))
            _siccodes = value
        End Set
    End Property

    Public Property NAICSCodes As ICollection(Of NOIPiNAICS)
        Get
            Return _naicscodes
        End Get
        Set(value As ICollection(Of NOIPiNAICS))
            _naicscodes = value
        End Set
    End Property

    Public Property Outfalls As ICollection(Of NOIOutfall)
        Get
            Return _outfalls
        End Get
        Set(value As ICollection(Of NOIOutfall))
            _outfalls = value
        End Set
    End Property

    Public Property PermitStatuses As ICollection(Of PermitStatus)
        Get
            Return _permitstatuses
        End Get
        Set(value As ICollection(Of PermitStatus))
            _permitstatuses = value
        End Set
    End Property

    Public Property NOIAPAnnualThresholdDet As NOIAPAnnualThreshold
        Get
            Return _noiapannualthresholddet
        End Get
        Set(value As NOIAPAnnualThreshold)
            _noiapannualthresholddet = value
        End Set
    End Property

    Public Property NOIAPChemicalsLst As ICollection(Of NOIAPChemicals)
        Get
            Return _noiapchemicalslst
        End Get
        Set(value As ICollection(Of NOIAPChemicals))
            _noiapchemicalslst = value
        End Set
    End Property


    Public Property DateReceived As Date
        Get
            Return _datereceived
        End Get
        Set(value As Date)
            _datereceived = value
        End Set
    End Property


    Public Property label As String

    Public Property value As String

#End Region









End Class
