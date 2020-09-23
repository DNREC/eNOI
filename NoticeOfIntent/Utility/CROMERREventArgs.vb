Public Class CROMERREventArgs
    Inherits EventArgs

    Private docURL As String
    Private ex As Exception

    Public Sub New()
        docURL = ""
    End Sub
    Public Sub New(ByVal documentURL As String)
        docURL = documentURL
    End Sub

    Public Sub New(ByVal exception As Exception)
        ex = exception
    End Sub

    ''' <summary>
    ''' get cromErr document URL
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property documentURL() As String
        Get
            Return docURL
        End Get
    End Property

    ''' <summary>
    ''' get error
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property [error]() As Exception
        Get
            Return ex
        End Get
    End Property


End Class
