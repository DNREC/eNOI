Public Class SWConstructBMP
    Private _permitid As Int32
    Private _piid As Int32
    Private _swbmpid As Byte
    Private _swbmpname As String
    Private _swbmpothername As String
    Private _swbmpqty As Integer
    Public Property IsDeleted As Boolean = False


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

    Public Property SWBMPID As Byte
        Get
            Return _swbmpid
        End Get
        Set(value As Byte)
            _swbmpid = value
        End Set
    End Property

    Public Property SWBMPName As String
        Get
            Return _swbmpname
        End Get
        Set(value As String)
            _swbmpname = value
        End Set
    End Property

    Public Property SWBMPOtherName As String
        Get
            Return _swbmpothername
        End Get
        Set(value As String)
            _swbmpothername = value
        End Set
    End Property

    Public Property SWBMPQty As Integer
        Get
            Return _swbmpqty
        End Get
        Set(value As Integer)
            _swbmpqty = value
        End Set
    End Property
#End Region
End Class
