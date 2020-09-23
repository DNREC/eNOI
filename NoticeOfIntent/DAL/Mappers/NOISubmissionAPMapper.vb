Public Class NOISubmissionAPMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionAP)


    Public Sub New()

        Me.Property(Function(e) e.InsectPestControl) _
            .IsFixedLength()

        Me.Property(Function(e) e.WeedPestControl) _
            .IsFixedLength()

        Me.Property(Function(e) e.AnimalPestControl) _
            .IsFixedLength()

        Me.Property(Function(e) e.ForestCanopyPestControl) _
            .IsFixedLength()

        Me.Property(Function(e) e.NotExceededThreshold) _
            .IsFixedLength()

        'Me.HasRequired(Function(e) e.NOIEntityType) _
        '    .WithMany(Function(e) e.NOISubmissionAP)

        Me.HasRequired(Function(e) e.NOISubmission) _
            .WithOptional(Function(e) e.NOISubmissionAP)
    End Sub

End Class
