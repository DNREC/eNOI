
Public Class NOIProjectMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOIProject)

    Public Sub New()




        'Me.Property(Function(e) e.Latitude) _
        '            .HasPrecision(9, 6)

        'Me.Property(Function(e) e.Longitude) _
        '    .HasPrecision(10, 6)

        'Me.Property(Function(e) e.X) _
        '    .HasPrecision(8, 2)

        'Me.Property(Function(e) e.Y) _
        '    .HasPrecision(8, 2)

        'Me.Property(Function(e) e.IsSWPPPPrepared) _
        '    .IsFixedLength()

        'Me.Property(Function(e) e.ProjectStateAbv) _
        '    .IsFixedLength()

        'Me.Property(Function(e) e.TotalLandArea) _
        '    .HasPrecision(7, 2)

        'Me.Property(Function(e) e.EstimatedArea) _
        '    .HasPrecision(7, 2)

        'Me.HasMany(Function(e) e.NOIProjectSWBMP) _
        '   .WithRequired(Function(e) e.NOIProject) _
        '   .WillCascadeOnDelete(True)

        'Me.HasMany(Function(e) e.NOIProjectTaxParcels) _
        '    .WithRequired(Function(e) e.NOIProject) _
        '    .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmission) _
            .WithRequired(Function(e) e.NOIProject) _
            .WillCascadeOnDelete(False)






    End Sub

End Class
