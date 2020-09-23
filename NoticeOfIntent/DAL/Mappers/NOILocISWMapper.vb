Public Class NOILocISWMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOILoc)

    Public Sub New()
        Me.Property(Function(e) e.Latitude) _
                    .HasPrecision(9, 6)

        Me.Property(Function(e) e.Longitude) _
            .HasPrecision(10, 6)

        Me.Property(Function(e) e.X) _
            .HasPrecision(8, 2)

        Me.Property(Function(e) e.Y) _
            .HasPrecision(8, 2)

        Me.HasMany(Function(e) e.NOISubmission) _
                .WithOptional(Function(e) e.NOILoc)

        Me.HasMany(Function(e) e.NOISubmissionUnit) _
            .WithRequired(Function(e) e.NOILoc) _
            .WillCascadeOnDelete(True)


    End Sub
End Class
