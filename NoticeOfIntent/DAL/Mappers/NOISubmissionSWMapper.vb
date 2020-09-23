Public Class NOISubmissionSWMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionSW)

    Public Sub New()
        Me.Property(Function(e) e.IsSatisfiedDSSR) _
            .IsFixedLength()

        Me.Property(Function(e) e.IsPlanVerifiedDSSR) _
            .IsFixedLength()

        Me.Property(Function(e) e.IsFinalStablizationDone) _
            .IsFixedLength()

        Me.HasRequired(Function(e) e.NOISubmission) _
            .WithOptional(Function(e) e.NOISubmissionSW)

    End Sub
End Class
