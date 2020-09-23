Public Class NOITaxParcel
    Private Property _projecttaxparcelid As Integer

    Private Property _piid As Int32

    Private Property _taxparcelnumber As String

    Private Property _taxparcelcounty As String

    Public Property IsDeleted As Boolean = False



#Region "Public Properties"

    Public Property ProjectTaxParcelID As Integer
        Get
            Return _projecttaxparcelid
        End Get
        Set(value As Integer)
            _projecttaxparcelid = value
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

    Public Property TaxParcelNumber As String
        Get
            Return _taxparcelnumber
        End Get
        Set(value As String)
            _taxparcelnumber = value
        End Set
    End Property

    Public Property TaxParcelCounty As String
        Get
            Return _taxparcelcounty
        End Get
        Set(value As String)
            _taxparcelcounty = value
        End Set
    End Property

#End Region
End Class
