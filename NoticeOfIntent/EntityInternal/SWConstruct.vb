Public Class SWConstruct
    Private _permitid As Int32
    Private _piid As Int32
    Private _projecttypeid As Byte
    Private _otherprojecttype As String
    Private _constructcounty As String
    Private _constructmunicipality As String
    Private _isswpppprepared As String
    Private _totallandarea As Decimal
    Private _estimatedarea As Decimal
    Private _constructstartdate As Date?
    Private _constructenddate As Date?


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

    Public Property ProjectTypeID As Byte
        Get
            Return _projecttypeid
        End Get
        Set(value As Byte)
            If _projecttypeid <> value Then
                _haschanged = True
            End If
            _projecttypeid = value
        End Set
    End Property

    Public Property OtherProjectType As String
        Get
            Return _otherprojecttype
        End Get
        Set(value As String)
            If _otherprojecttype <> value Then
                _haschanged = True
            End If
            _otherprojecttype = value
        End Set
    End Property

    Public Property ConstructCounty As String
        Get
            Return _constructcounty
        End Get
        Set(value As String)
            If _constructcounty <> value Then
                _haschanged = True
            End If
            _constructcounty = value
        End Set
    End Property

    Public Property ConstructMunicipality As String
        Get
            Return _constructmunicipality
        End Get
        Set(value As String)
            If _constructmunicipality <> value Then
                _haschanged = True
            End If
            _constructmunicipality = value
        End Set
    End Property

    Public Property IsSWPPPPrepared As String
        Get
            Return _isswpppprepared
        End Get
        Set(value As String)
            If _isswpppprepared <> value Then
                _haschanged = True
            End If
            _isswpppprepared = value
        End Set
    End Property

    Public Property TotalLandArea As Decimal
        Get
            Return _totallandarea
        End Get
        Set(value As Decimal)
            If _totallandarea <> value Then
                _haschanged = True
            End If
            _totallandarea = value
        End Set
    End Property

    Public Property EstimatedArea As Decimal
        Get
            Return _estimatedarea
        End Get
        Set(value As Decimal)
            If _estimatedarea <> value Then
                _haschanged = True
            End If
            _estimatedarea = value
        End Set
    End Property

    Public Property ConstructStartDate As Date?
        Get
            Return _constructstartdate
        End Get
        Set(value As Date?)
            If _constructstartdate <> value Then
                _haschanged = True
            End If
            _constructstartdate = value
        End Set
    End Property

    Public Property ConstructEndDate As Date?
        Get
            Return _constructenddate
        End Get
        Set(value As Date?)
            If _constructenddate <> value Then
                _haschanged = True
            End If
            _constructenddate = value
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
