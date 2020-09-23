Public Class NOISubmissionNAICSMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionNAICS)

    Public Sub New()

        Me.Property(Function(e) e.NAICSCode) _
            .IsFixedLength()

        Me.Property(Function(e) e.RankCode) _
            .IsFixedLength()
    End Sub

End Class
