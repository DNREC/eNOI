Public Class NOISubmissionStatusMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionStatus)

    Public Sub New()
        Me.Property(Function(e) e.SubmissionStatusCode) _
            .IsFixedLength()


    End Sub

End Class
