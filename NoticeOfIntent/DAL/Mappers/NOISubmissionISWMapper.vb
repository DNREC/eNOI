Public Class NOISubmissionISWMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmission)

    Public Sub New()
        Me.Property(Function(e) e.IsLocked) _
            .IsFixedLength()

        Me.Property(Function(e) e.CertificationAgreed) _
            .IsFixedLength()

        Me.Property(Function(e) e.AmountPaid) _
            .HasPrecision(19, 4)


        Me.HasMany(Function(e) e.NOISigningEmailAddress) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionAcceptedPDF) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionDocs) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionPersonOrg) _
                .WithRequired(Function(e) e.NOISubmission) _
                .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionStatus) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)


        Me.HasMany(Function(e) e.NOISubmissionTaxParcels) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionNE) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionNAICS) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionSIC) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasOptional(Function(e) e.NOISubmissionISWAP) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

        Me.HasMany(Function(e) e.NOISubmissionUnit) _
            .WithRequired(Function(e) e.NOISubmission) _
            .WillCascadeOnDelete(True)

    End Sub
End Class
