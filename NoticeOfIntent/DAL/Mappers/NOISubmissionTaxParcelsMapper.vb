Public Class NOISubmissionTaxParcelsMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionTaxParcels)

    Public Sub New()
        Me.Property(Function(e) e.TaxParcelCounty) _
            .IsFixedLength()
    End Sub
End Class
