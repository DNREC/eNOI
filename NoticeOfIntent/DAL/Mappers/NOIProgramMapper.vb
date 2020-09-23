Public Class NOIProgramMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOIProgram)

    Public Sub New()

        Me.HasMany(Function(e) e.NOIFeeExemption) _
           .WithRequired(Function(e) e.NOIProgram) _
           .WillCascadeOnDelete(False)

        Me.HasMany(Function(e) e.NOIProgSubmissionType) _
            .WithRequired(Function(e) e.NOIProgram) _
            .WillCascadeOnDelete(False)

        Me.HasMany(Function(e) e.NOIProject) _
            .WithRequired(Function(e) e.NOIProgram) _
            .WillCascadeOnDelete(False)
    End Sub
End Class
