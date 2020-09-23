Public Class NOIAPAnnualThreshold
    Private _permitid As Int32
    Private _piid As Int32
    Private _entitytypeid As Integer
    Private _insectpestcontrol As String
    Private _weedpestcontrol As String
    Private _animalpestcontrol As String
    Private _forestcanopypestcontrol As String
    Private _notexceededthreshold As String
    Private _commercialapplicatorid As String

    Private _haschanged As Boolean = False

#Region "Public Properties"

    Public Property PermitID As Int32
        Get
            Return _permitid
        End Get
        Set(value As Int32)
            _permitid = value
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

    Public Property EntityTypeID As Integer
        Get
            Return _entitytypeid
        End Get
        Set(value As Integer)
            If _entitytypeid <> value Then
                _haschanged = True
            End If
            _entitytypeid = value
        End Set
    End Property

    Public Property InsectPestControl As String
        Get
            Return _insectpestcontrol
        End Get
        Set(value As String)
            If _insectpestcontrol <> value Then
                _haschanged = True
            End If
            _insectpestcontrol = value
        End Set
    End Property

    Public Property WeedPestControl As String
        Get
            Return _weedpestcontrol
        End Get
        Set(value As String)
            If _weedpestcontrol <> value Then
                _haschanged = True
            End If
            _weedpestcontrol = value
        End Set
    End Property

    Public Property AnimalPestControl As String
        Get
            Return _animalpestcontrol
        End Get
        Set(value As String)
            If _animalpestcontrol <> value Then
                _haschanged = True
            End If
            _animalpestcontrol = value
        End Set
    End Property

    Public Property ForestCanopyPestControl As String
        Get
            Return _forestcanopypestcontrol
        End Get
        Set(value As String)
            If _forestcanopypestcontrol <> value Then
                _haschanged = True
            End If
            _forestcanopypestcontrol = value
        End Set
    End Property

    Public Property NotExceededThreshold As String
        Get
            Return _notexceededthreshold
        End Get
        Set(value As String)
            If _notexceededthreshold <> value Then
                _haschanged = True
            End If
            _notexceededthreshold = value
        End Set
    End Property


    Public Property CommercialApplicatorID As String
        Get
            Return _commercialapplicatorid
        End Get
        Set(value As String)
            If _commercialapplicatorid <> value Then
                _haschanged = True
            End If
            _commercialapplicatorid = value
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
