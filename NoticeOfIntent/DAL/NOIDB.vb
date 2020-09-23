Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq
Imports System.Data.Common

Partial Public Class NOIDB
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=NOIDB")
        System.Data.Entity.Database.SetInitializer(Of NOIDB)(Nothing)
        Configuration.ProxyCreationEnabled = False
        Configuration.LazyLoadingEnabled = False

    End Sub

    Public Overridable Property ErrorLogs As DbSet(Of ErrorLog)
    Public Overridable Property NOIFeeExemptions As DbSet(Of NOIFeeExemption)
    Public Overridable Property NOILoc As DbSet(Of NOILoc)
    Public Overridable Property NOIProgram As DbSet(Of NOIProgram)
    Public Overridable Property NOIProgSubmissionType As DbSet(Of NOIProgSubmissionType)
    Public Overridable Property NOIProjects As DbSet(Of NOIProject)
    Public Overridable Property NOISessionStorage As DbSet(Of NOISessionStorage)
    Public Overridable Property NOISubmissions As DbSet(Of NOISubmission)
    Public Overridable Property NOISubmissionAcceptedPDF As DbSet(Of NOISubmissionAcceptedPDF)
    Public Overridable Property NOISubmissionPersonOrgs As DbSet(Of NOISubmissionPersonOrg)
    Public Overridable Property NOISubmissionStatuses As DbSet(Of NOISubmissionStatus)
    Public Overridable Property NOISubmissionStatusCodes As DbSet(Of NOISubmissionStatusCode)
    Public Overridable Property NOISubmissionSW As DbSet(Of NOISubmissionSW)
    Public Overridable Property NOISubmissionSWBMPs As DbSet(Of NOISubmissionSWBMP)
    Public Overridable Property NOISubmissionTaxParcels As DbSet(Of NOISubmissionTaxParcels)
    Public Overridable Property NOISigningEmailAddresses As DbSet(Of NOISigningEmailAddress)
    Public Overridable Property NOISubmissionTypelst As DbSet(Of NOISubmissionTypelst)
    Public Overridable Property NOISubmissionSWConstruct As DbSet(Of NOISubmissionSWConstruct)
    Public Overridable Property NOISubmissionSearchTable As DbSet(Of NOISubmissionSearchlst)
    Public Overridable Property CompanyTypeTable As DbSet(Of CompanyTypelst)
    Public Overridable Property ProjectTypeTable As DbSet(Of ProjectTypelst)
    Public Overridable Property StateAbvTable As DbSet(Of StateAbvlst)
    Public Overridable Property SWBMPTable As DbSet(Of SWBMPlst)
    Public Overridable Property NOIWebPageCtrlTxts As DbSet(Of NOIWebPageCtrlTxt)
    Public Overridable Property NOIWebPages As DbSet(Of NOIWebPage)

    'Private _SWBMPTable As DbSet(Of SWBMPlst)
    'Public ReadOnly Property SWBMPTable As DbSet(Of SWBMPlst)
    '    Get
    '        Return _SWBMPTable
    '    End Get
    'End Property

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)


        modelBuilder.Entity(Of NOISubmissionSearchlst)().ToTable("vwNOISubmissionSearchTable")

        modelBuilder.Configurations.Add(New NOIProjectMapper())
        modelBuilder.Configurations.Add(New NOISubmissionMapper())
        modelBuilder.Configurations.Add(New NOISubmissionTaxParcelsMapper())
        modelBuilder.Configurations.Add(New NOISubmissionPersonOrgMapper())
        modelBuilder.Configurations.Add(New NOISubmissionStatusCodeMapper())
        modelBuilder.Configurations.Add(New NOISubmissionStatusMapper())
        modelBuilder.Configurations.Add(New NOISubmissionSWConstructMapper())
        modelBuilder.Configurations.Add(New NOILocMapper())
        modelBuilder.Configurations.Add(New NOIProgramMapper())
        modelBuilder.Configurations.Add(New NOISubmissionTypeMapper())
        modelBuilder.Configurations.Add(New NOISubmissionISWAPMapper())
        modelBuilder.Configurations.Add(New NOISubmissionNAICSMapper())
        modelBuilder.Configurations.Add(New NOISubmissionNEMapper())
        modelBuilder.Configurations.Add(New NOISubmissionSICMapper())


        modelBuilder.Properties(Of String)().Configure(Function(config) config.IsUnicode(False))
        modelBuilder.Types(Of IEntity).Configure(Function(e) e.Ignore(Function(b) b.EntityState))


        modelBuilder.Ignore(Of NOISubmissionAP)()
        modelBuilder.Ignore(Of NOISubmissionDocs)()

        MyBase.OnModelCreating(modelBuilder)

    End Sub
End Class
