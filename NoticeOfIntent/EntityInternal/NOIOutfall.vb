Public Class NOIOutfall
    Private _unitid As Int32
    Private _piid As Int32
    Private _permitid As Int32
    Private _unitname As String
    Private _unittypeid As Int16 = 7
    Private _locid As Int32
    Private _latitude As Decimal?
    Private _longitude As Decimal?
    Private _x As Decimal?
    Private _y As Decimal?
    Private _huc12 As String
    Private _watershed As String

    Public Property IsDeleted As Boolean = False

#Region "Public Properties"

    Public Property UnitID As Int32
        Get
            Return _unitid
        End Get
        Set(value As Int32)
            _unitid = value
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

    Public Property PermitID As Int32
        Get
            Return _permitid
        End Get
        Set(value As Int32)
            _permitid = value
        End Set
    End Property

    Public Property UnitName As String
        Get
            Return _unitname
        End Get
        Set(value As String)
            _unitname = value
        End Set
    End Property

    Public Property UnitTypeID As Int16
        Get
            Return _unittypeid
        End Get
        Set(value As Int16)
            _unittypeid = value
        End Set
    End Property

    Public Property LocID As Int32
        Get
            Return _locid
        End Get
        Set(value As Int32)
            _locid = value
        End Set
    End Property

    Public Property Latitude As Decimal?
        Get
            Return _latitude
        End Get
        Set(value As Decimal?)
            _latitude = value
        End Set
    End Property

    Public Property Longitude As Decimal?
        Get
            Return _longitude
        End Get
        Set(value As Decimal?)
            _longitude = value
        End Set
    End Property

    Public Property X As Decimal?
        Get
            Return _x
        End Get
        Set(value As Decimal?)
            _x = value
        End Set
    End Property

    Public Property Y As Decimal?
        Get
            Return _y
        End Get
        Set(value As Decimal?)
            _y = value
        End Set
    End Property

    Public Property HUC12 As String
        Get
            Return _huc12

        End Get
        Set(value As String)
            _huc12 = value
        End Set
    End Property

    Public Property Watershed As String
        Get
            Return _watershed

        End Get
        Set(value As String)
            _watershed = value
        End Set
    End Property

#End Region

End Class
