Public Class NOISubmissionSWConstructMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionSWConstruct)

    Public Sub New()

        Me.Property(Function(e) e.IsSWPPPPrepared) _
            .IsFixedLength()

        Me.Property(Function(e) e.TotalLandArea) _
            .HasPrecision(7, 2)

        Me.Property(Function(e) e.EstimatedArea) _
            .HasPrecision(7, 2)

        Me.HasRequired(Function(e) e.NOISubmission) _
            .WithOptional(Function(e) e.NOISubmissionSWConstruct)

    End Sub

End Class
