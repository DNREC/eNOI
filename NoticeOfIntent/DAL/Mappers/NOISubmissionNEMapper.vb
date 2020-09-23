Public Class NOISubmissionNEMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionNE)

    Public Sub New()
        Me.Property(Function(e) e.Answer) _
            .IsFixedLength()

    End Sub

End Class
