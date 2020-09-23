Public Class NOISubmissionSICMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionSIC)

    Public Sub New()

        Me.Property(Function(e) e.SICCode) _
            .IsFixedLength()

        Me.Property(Function(e) e.RankCode) _
            .IsFixedLength()


    End Sub

End Class
