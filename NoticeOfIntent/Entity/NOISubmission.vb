Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("tblNOISubmission")>
Partial Public Class NOISubmission
    Implements IEntity

    Public Sub New()
        'NOIProject = New NOIProject()
        MS4MCMDetails = New HashSet(Of MS4MCMDetails)()
        MS4ReceivingWaters = New HashSet(Of MS4ReceivingWaters)()
        NOIFeeExemption = New HashSet(Of NOIFeeExemption)()
        NOISigningEmailAddress = New HashSet(Of NOISigningEmailAddress)()
        NOISubmissionAcceptedPDF = New HashSet(Of NOISubmissionAcceptedPDF)()
        NOISubmissionAPChemicals = New HashSet(Of NOISubmissionAPChemicals)()
        NOISubmissionNAICS = New HashSet(Of NOISubmissionNAICS)()
        NOISubmissionPersonOrg = New HashSet(Of NOISubmissionPersonOrg)()
        NOISubmissionSIC = New HashSet(Of NOISubmissionSIC)()
        NOISubmissionStatus = New HashSet(Of NOISubmissionStatus)()
        NOISubmissionSWBMP = New HashSet(Of NOISubmissionSWBMP)()
        NOISubmissionTaxParcels = New HashSet(Of NOISubmissionTaxParcels)()
        NOISubmissionUnit = New HashSet(Of NOISubmissionUnit)()
        NOISubmissionNE = New HashSet(Of NOISubmissionNE)()
        NOISubmissionDocs = New HashSet(Of NOISubmissionDocs)()

        IsLocked = "N"
        CertificationAgreed = "N"
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property SubmissionID As Integer

    Public Property ProjectID As Integer

    Public Property ProgSubmissionTypeID As Integer

    <Required>
    <StringLength(1)>
    <DefaultSettingValue("N")>
    Public Property IsLocked As String

    <Required>
    <StringLength(1)>
    <DefaultSettingValue("N")>
    Public Property CertificationAgreed As String

    <Column(TypeName:="money")>
    Public Property AmountPaid As Decimal?

    <StringLength(256)>
    Public Property CORURL As String

    <Column(TypeName:="smalldatetime")>
    Public Property NOIReceivedDate As Date?

    <Column(TypeName:="smalldatetime")>
    Public Property CopermitteeReceivedDate As Date?

    Public Property NOILocID As Integer?


    <StringLength(2000)>
    Public Property Comments As String

    <Required>
    <StringLength(64)>
    Public Property CreatedBy As String

    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    Public Property CreatedDate As Date

    <Required>
    <StringLength(64)>
    Public Property LastChgBy As String

    <DatabaseGenerated(DatabaseGeneratedOption.Computed)>
    Public Property LastChgDate As Date

    Public Overridable Property MS4MCMDetails As ICollection(Of MS4MCMDetails)

    Public Overridable Property MS4ReceivingWaters As ICollection(Of MS4ReceivingWaters)

    Public Overridable Property NOIFeeExemption As ICollection(Of NOIFeeExemption)

    Public Overridable Property NOILoc As NOILoc

    Public Overridable Property NOIProject As NOIProject

    Public Overridable Property NOIProgSubmissionType As NOIProgSubmissionType

    Public Overridable Property NOISigningEmailAddress As ICollection(Of NOISigningEmailAddress)

    Public Overridable Property NOISubmissionAcceptedPDF As ICollection(Of NOISubmissionAcceptedPDF)

    Public Overridable Property NOISubmissionAPChemicals As ICollection(Of NOISubmissionAPChemicals)

    Public Overridable Property NOISubmissionISWAP As NOISubmissionISWAP

    Public Overridable Property NOISubmissionNAICS As ICollection(Of NOISubmissionNAICS)

    Public Overridable Property NOISubmissionPersonOrg As ICollection(Of NOISubmissionPersonOrg)

    Public Overridable Property NOISubmissionSIC As ICollection(Of NOISubmissionSIC)

    Public Overridable Property NOISubmissionStatus As ICollection(Of NOISubmissionStatus)

    Public Overridable Property NOISubmissionSW As NOISubmissionSW

    Public Overridable Property NOISubmissionSWBMP As ICollection(Of NOISubmissionSWBMP)

    Public Overridable Property NOISubmissionSWConstruct As NOISubmissionSWConstruct

    Public Overridable Property NOISubmissionTaxParcels As ICollection(Of NOISubmissionTaxParcels)

    Public Overridable Property NOISubmissionUnit As ICollection(Of NOISubmissionUnit)

    Public Overridable Property NOISubmissionNE As ICollection(Of NOISubmissionNE)

    Public Overridable Property NOISubmissionAP As NOISubmissionAP

    Public Overridable Property NOISubmissionDocs As ICollection(Of NOISubmissionDocs)


    Public Property EntityState As EntityState = NoticeOfIntent.EntityState.Unchanged Implements IEntity.EntityState
End Class
