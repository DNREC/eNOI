Public Class NOILocMapper
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

    End Sub
End Class
