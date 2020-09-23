Public Class NOISubmissionISWAPMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionISWAP)

    Public Sub New()

        Me.Property(Function(e) e.IsOPTransferred) _
            .IsFixedLength()

        Me.Property(Function(e) e.IsNoDischargeAssociated) _
            .IsFixedLength()

        Me.Property(Function(e) e.IsCoveredNPDESPermit) _
            .IsFixedLength()

        Me.HasRequired(Function(e) e.NOISubmission) _
            .WithOptional(Function(e) e.NOISubmissionISWAP)
    End Sub

End Class
