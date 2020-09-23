Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq
Imports System.Data.Common


Public Class PesticidesDB
    Inherits BaseContext(Of PesticidesDB)


    Public Overridable Property ErrorLogs As DbSet(Of ErrorLog)
    Public Overridable Property NOIEntityType As DbSet(Of NOIEntityType)
    Public Overridable Property NOIFeeExemptions As DbSet(Of NOIFeeExemption)
    Public Overridable Property NOIPesticidePatterns As DbSet(Of NOIPesticidePattern)
    Public Overridable Property NOIProgram As DbSet(Of NOIProgram)
    Public Overridable Property NOIProgSubmissionType As DbSet(Of NOIProgSubmissionType)
    Public Overridable Property NOIProjects As DbSet(Of NOIProject)
    Public Overridable Property NOISessionStorage As DbSet(Of NOISessionStorage)
    Public Overridable Property NOISubmissions As DbSet(Of NOISubmission)
    Public Overridable Property NOISubmissionAcceptedPDF As DbSet(Of NOISubmissionAcceptedPDF)
    Public Overridable Property NOISubmissionAP As DbSet(Of NOISubmissionAP)
    Public Overridable Property NOISubmissionAPChemicals As DbSet(Of NOISubmissionAPChemicals)
    Public Overridable Property NOISubmissionISWAP As DbSet(Of NOISubmissionISWAP)
    Public Overridable Property NOISubmissionPersonOrgs As DbSet(Of NOISubmissionPersonOrg)
    Public Overridable Property NOISubmissionStatuses As DbSet(Of NOISubmissionStatus)
    Public Overridable Property NOISubmissionStatusCodes As DbSet(Of NOISubmissionStatusCode)
    Public Overridable Property NOISigningEmailAddresses As DbSet(Of NOISigningEmailAddress)
    Public Overridable Property NOISubmissionTypelst As DbSet(Of NOISubmissionTypelst)
    Public Overridable Property NOISubmissionDocs As DbSet(Of NOISubmissionDocs)
    Public Overridable Property NOISubmissionSearchTable As DbSet(Of NOISubmissionSearchlst)
    Public Overridable Property CompanyTypeTable As DbSet(Of CompanyTypelst)
    'Public Overridable Property ProjectTypeTable As DbSet(Of ProjectTypelst)
    Public Overridable Property StateAbvTable As DbSet(Of StateAbvlst)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)


        modelBuilder.Entity(Of NOISubmissionDocs)() _
            .HasRequired(Function(e) e.NOISubmissionDocFile) _
            .WithRequiredPrincipal(Function(e) e.NOISubmissionDocs) _
            .WillCascadeOnDelete(True)

        modelBuilder.Entity(Of NOISubmissionSearchlst)().ToTable("vwNOISubmissionSearchTableAP")

        modelBuilder.Configurations.Add(New NOIProjectMapper())
        modelBuilder.Configurations.Add(New NOISubmissionForAPMapper())
        modelBuilder.Configurations.Add(New NOISubmissionPersonOrgMapper())
        modelBuilder.Configurations.Add(New NOISubmissionStatusCodeMapper())
        modelBuilder.Configurations.Add(New NOISubmissionStatusMapper())
        modelBuilder.Configurations.Add(New NOIProgramMapper())
        modelBuilder.Configurations.Add(New NOISubmissionTypeMapper())
        modelBuilder.Configurations.Add(New NOISubmissionAPMapper())
        modelBuilder.Configurations.Add(New NOISubmissionISWAPMapper())

        'modelBuilder.Entity(Of NOIPesticidePattern)() _
        '    .HasMany(Function(e) e.NOISubmissionAPChemicals) _
        '    .WithRequired(Function(e) e.NOIPesticidePattern) _
        '    .HasForeignKey(Function(e) e.PesticidePatternID) _
        '    .WillCascadeOnDelete(False)


        modelBuilder.Properties(Of String)().Configure(Function(config) config.IsUnicode(False))
        modelBuilder.Types(Of IEntity).Configure(Function(e) e.Ignore(Function(b) b.EntityState))


        modelBuilder.Ignore(Of NOISubmissionSWBMP)()
        modelBuilder.Ignore(Of NOISubmissionSWConstruct)()
        modelBuilder.Ignore(Of NOISubmissionSW)()
        modelBuilder.Ignore(Of NOISubmissionSIC)()
        modelBuilder.Ignore(Of NOISubmissionNAICS)()
        modelBuilder.Ignore(Of NOISubmissionNE)()
        modelBuilder.Ignore(Of NOILoc)()
        'modelBuilder.Ignore(Of NOISubmissionDocs)()



        MyBase.OnModelCreating(modelBuilder)
    End Sub
End Class
