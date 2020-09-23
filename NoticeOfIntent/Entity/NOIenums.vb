Imports System.ComponentModel


Public Enum NOIProgramType
    CSSGeneralPermit = 11
    ISGeneralPermit = 12
    PesticideGeneralPermit = 13
    MS4GeneralPermit = 14
End Enum


Public Enum NOISubmissionType
    GeneralNOIPermit = 1
    CoPermittee = 2
    TerminateCoPermittee = 3
    TerminateNOI = 4
    GeneralNOICorrection = 5
    GeneralNOIRenewal = 6
End Enum

Public Enum NOIPersonOrgType
    OwnerDetails = 1
    ContactDetails = 2
    BilleeDetails = 3
    CoPermitteeDetails = 4
    ProjectAddress = 5
End Enum

Public Enum EntityState
    Unchanged
    Added
    Modified
    Deleted
    Detached
End Enum

Public Enum SubmissionStatusCode
    A = 1 'Accept
    F = 2 'Filed
    O = 3 'Open
    R = 4 'Returned For Resubmission
    X = 5 'Rejected

End Enum


Public Enum YESNO
    Y = 1
    N = 2
End Enum


Public Enum EISSqlAfflType
    Owner = 1
    Contact = 13
    Billee = 111
    Copermittee = 86
End Enum


Public Enum ucPersonorgType
    person = 1
    project = 2
End Enum


''' <summary>
''' This is used in the Admin module to store whether the NOI information is retrieved from EISSql or from external NOI. This is stored in the LoginDetails object.
''' </summary>
''' <remarks></remarks>
Public Enum AdminCurrentDB
    EISSQL = 1
    NOI = 2
End Enum