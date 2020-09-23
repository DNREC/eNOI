Public Class NOISubmissionStatusCodeMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionStatusCode)


    Public Sub New()
        Me.Property(Function(e) e.SubmissionStatusCode) _
            .IsFixedLength()

        Me.Property(Function(e) e.Active) _
            .IsFixedLength()


        Me.HasMany(Function(e) e.NOISubmissionStatus) _
            .WithRequired(Function(e) e.NOISubmissionStatusCode) _
            .WillCascadeOnDelete(False)

    End Sub

End Class
