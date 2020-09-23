Public Class NOIPiNAICS
    Private _piid As Int32
    Private _naics As String
    Private _rankcode As String

    Public Property IsDeleted As Boolean = False


#Region "Public Properties"

    Public Property PIID As Int32
        Get
            Return _piid
        End Get
        Set(value As Int32)
            _piid = value
        End Set
    End Property

    Public Property NAICS As String
        Get
            Return _naics
        End Get
        Set(value As String)
            _naics = value
        End Set
    End Property

    Public Property RankCode As String
        Get
            Return _rankcode
        End Get
        Set(value As String)
            _rankcode = value
        End Set
    End Property

#End Region
End Class
