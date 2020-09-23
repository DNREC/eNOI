Public Class NOISubmissionPersonOrgMapper
    Inherits Entity.ModelConfiguration.EntityTypeConfiguration(Of NOISubmissionPersonOrg)

    Public Sub New()

        Me.Property(Function(e) e.StateAbv) _
            .IsFixedLength()

        Me.Property(Function(e) e.PersonOrgTypeCode) _
            .IsFixedLength()

    End Sub
End Class
