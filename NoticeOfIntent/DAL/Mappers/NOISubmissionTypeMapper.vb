Public Class NOISubmissionTypeMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionTypelst)

    Public Sub New()


        Me.HasMany(Function(e) e.NOIProgSubmissionType) _
            .WithRequired(Function(e) e.NOISubmissionTypelst) _
            .WillCascadeOnDelete(False)
    End Sub
End Class
