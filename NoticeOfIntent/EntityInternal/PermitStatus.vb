Public Class PermitStatus
    Private _peventsid As Int32
    Private _permitid As Int32
    Private _peventdate As Date
    Private _peventcode As Int16
    Private _pevent As String
    Private _peventcomment As String
    Public Property IsDeleted As Boolean = False

#Region "Public Properties"
    Public Property PEventsID As Int32
        Get
            Return _peventsid
        End Get
        Set(value As Int32)
            _peventsid = value
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

    Public Property PEventDate As Date
        Get
            Return _peventdate
        End Get
        Set(value As Date)
            _peventdate = value
        End Set
    End Property

    Public Property PEventCode As Int16
        Get
            Return _peventcode
        End Get
        Set(value As Int16)
            _peventcode = value
        End Set
    End Property

    Public Property PEvent As String
        Get
            Return _pevent
        End Get
        Set(value As String)
            _pevent = value
        End Set
    End Property

    Public Property PEventComment As String
        Get
            Return _peventcomment
        End Get
        Set(value As String)
            _peventcomment = value
        End Set
    End Property


#End Region
End Class
