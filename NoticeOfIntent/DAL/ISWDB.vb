Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq
Imports System.Data.Common

Public Class ISWDB
    Inherits BaseContext(Of ISWDB)

    Public Overridable Property ErrorLogs As DbSet(Of ErrorLog)
    Public Overridable Property NOExposureCL As DbSet(Of NOExposureCL)
    Public Overridable Property NOIFeeExemptions As DbSet(Of NOIFeeExemption)
    Public Overridable Property NOILoc As DbSet(Of NOILoc)
    Public Overridable Property NOIProgram As DbSet(Of NOIProgram)
    Public Overridable Property NOIProgSubmissionType As DbSet(Of NOIProgSubmissionType)
    Public Overridable Property NOIProjects As DbSet(Of NOIProject)
    Public Overridable Property NOISessionStorage As DbSet(Of NOISessionStorage)
    Public Overridable Property NOISubmissions As DbSet(Of NOISubmission)
    Public Overridable Property NOISubmissionAcceptedPDF As DbSet(Of NOISubmissionAcceptedPDF)
    Public Overridable Property NOISubmissionISWAP As DbSet(Of NOISubmissionISWAP)
    Public Overridable Property NOISubmissionNAICS As DbSet(Of NOISubmissionNAICS)
    Public Overridable Property NOISubmissionPersonOrgs As DbSet(Of NOISubmissionPersonOrg)
    Public Overridable Property NOISubmissionSIC As DbSet(Of NOISubmissionSIC)
    Public Overridable Property NOISubmissionStatuses As DbSet(Of NOISubmissionStatus)
    Public Overridable Property NOISubmissionStatusCodes As DbSet(Of NOISubmissionStatusCode)
    Public Overridable Property NOISubmissionTaxParcels As DbSet(Of NOISubmissionTaxParcels)
    Public Overridable Property NOISigningEmailAddresses As DbSet(Of NOISigningEmailAddress)
    Public Overridable Property NOISubmissionTypelst As DbSet(Of NOISubmissionTypelst)
    Public Overridable Property NOISubmissionUnit As DbSet(Of NOISubmissionUnit)
    Public Overridable Property NOISubmissionNE As DbSet(Of NOISubmissionNE)
    Public Overridable Property NOISubmissionDocs As DbSet(Of NOISubmissionDocs)
    'Public Overridable Property NOISubmissionFile As DbSet(Of NOISubmissionDocFile)



    Public Overridable Property NOISubmissionSearchTable As DbSet(Of NOISubmissionSearchlst)
    Public Overridable Property CompanyTypeTable As DbSet(Of CompanyTypelst)
    'Public Overridable Property ProjectTypeTable As DbSet(Of ProjectTypelst)
    Public Overridable Property StateAbvTable As DbSet(Of StateAbvlst)
    'Public Overridable Property SWBMPTable As DbSet(Of SWBMPlst)
    'Private _SWBMPTable As DbSet(Of SWBMPlst)
    'Public ReadOnly Property SWBMPTable As DbSet(Of SWBMPlst)
    '    Get
    '        Return _SWBMPTable
    '    End Get
    'End Property

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)


        modelBuilder.Entity(Of NOISubmissionDocs)() _
            .HasRequired(Function(e) e.NOISubmissionDocFile) _
            .WithRequiredPrincipal(Function(e) e.NOISubmissionDocs) _
            .WillCascadeOnDelete(True)

        'modelBuilder.Entity(Of NOISubmissionDocFile)() _
        '    .HasRequired(Function(e) e.NOISubmissionDocs) _
        '    .WithRequiredDependent(Function(e) e.NOISubmissionDocFile)


        modelBuilder.Entity(Of NOISubmissionSearchlst)().ToTable("vwNOISubmissionSearchTableISW")
        modelBuilder.Entity(Of NOISubmissionDocFile)().ToTable("tblNOISubmissionDocs")
        modelBuilder.Entity(Of NOISubmissionDocs)().ToTable("tblNOISubmissionDocs")




        modelBuilder.Configurations.Add(New NOIProjectMapper())
        modelBuilder.Configurations.Add(New NOISubmissionISWMapper())
        modelBuilder.Configurations.Add(New NOISubmissionTaxParcelsMapper())
        modelBuilder.Configurations.Add(New NOISubmissionPersonOrgMapper())
        modelBuilder.Configurations.Add(New NOISubmissionStatusCodeMapper())
        modelBuilder.Configurations.Add(New NOISubmissionStatusMapper())
        modelBuilder.Configurations.Add(New NOILocISWMapper())
        modelBuilder.Configurations.Add(New NOIProgramMapper())
        modelBuilder.Configurations.Add(New NOISubmissionTypeMapper())
        modelBuilder.Configurations.Add(New NOISubmissionISWAPMapper())
        modelBuilder.Configurations.Add(New NOISubmissionNAICSMapper())
        modelBuilder.Configurations.Add(New NOISubmissionNEMapper())
        modelBuilder.Configurations.Add(New NOISubmissionSICMapper())





        modelBuilder.Properties(Of String)().Configure(Function(config) config.IsUnicode(False))
        modelBuilder.Types(Of IEntity).Configure(Function(e) e.Ignore(Function(b) b.EntityState))


        modelBuilder.Ignore(Of NOISubmissionSWBMP)()
        modelBuilder.Ignore(Of NOISubmissionSWConstruct)()
        modelBuilder.Ignore(Of NOISubmissionSW)()
        modelBuilder.Ignore(Of NOISubmissionAP)()

        MyBase.OnModelCreating(modelBuilder)

    End Sub

End Class
