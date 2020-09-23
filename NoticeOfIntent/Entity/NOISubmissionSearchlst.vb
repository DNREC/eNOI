Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

'<Table("vwNOISubmissionSearchTable")>
Partial Public Class NOISubmissionSearchlst

    <Key>
    Public Property ReferenceNo As Integer


    Public Property ProjectName As String


    Public Property ProjectNumber As String

    <Column(TypeName:="date")>
    Public Property ReceivedDate As Date

    Public Property PermitNumber As String


    Public Property Owners As String


    Public Property SubmissionStatusCode As String


    Public Property SubmissionStatus As String

    Public Property DisplayOrder As Byte?

    Public Property IsSigned As Integer

    Public Property ProgSubmissionTypeID As Integer

    Public Property SubmissionTypeID As Integer

    Public Property SubmissionType As String


    Public Property IsLocked As String


    Public Property CreatedBy As String

    Public Property ProgramID As Integer
End Class
