Imports System.Collections.Generic
Imports Microsoft.Reporting.WebForms
Imports System.Configuration

Public Class NOIReports
    ''' <summary>
    ''' create a binary of the report in pdf format
    ''' </summary>
    ''' <param name="subType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function createReport(ByVal subType As NOISubmissionType, subid As Integer) As Byte()

        Dim reportPath As String = ConfigurationManager.AppSettings("ReportFolder")

        Select Case (subType)
            Case NOISubmissionType.GeneralNOIPermit
                reportPath = reportPath & "rptGeneralNOI"
            Case NOISubmissionType.CoPermittee
                reportPath = reportPath & "rptCoPermitteeNOI"
            Case NOISubmissionType.TerminateCoPermittee
                reportPath = reportPath & "rptNOTCoPermitteeNOI"
            Case NOISubmissionType.TerminateNOI
                reportPath = reportPath & "rptNOTGeneralNOI"
        End Select

        Return getReport(reportPath, "PDF", subid)

    End Function


    Public Shared Function createReport(loginvs As LogInDetails) As Byte()

        Dim reportPath As String = ConfigurationManager.AppSettings("ReportFolder")

        If loginvs.reportid = NOIProgramType.CSSGeneralPermit Then
            Select Case (loginvs.submissiontype)
                Case NOISubmissionType.GeneralNOIPermit
                    reportPath = reportPath & "rptGeneralNOI"
                Case NOISubmissionType.CoPermittee
                    reportPath = reportPath & "rptCoPermitteeNOI"
                Case NOISubmissionType.TerminateCoPermittee
                    reportPath = reportPath & "rptNOTCoPermitteeNOI"
                Case NOISubmissionType.TerminateNOI
                    reportPath = reportPath & "rptNOTGeneralNOI"
            End Select
        ElseIf loginvs.reportid = NOIProgramType.ISGeneralPermit Then
            Select Case (loginvs.submissiontype)
                Case NOISubmissionType.GeneralNOIPermit
                    If loginvs.progsubmisssiontype = 5 Then
                        reportPath = reportPath & "rptISWNOI"
                    ElseIf loginvs.progsubmisssiontype = 6 Then
                        reportPath = reportPath & "rptISWNOINoExposure"
                    End If
                Case NOISubmissionType.TerminateNOI
                    reportPath = reportPath & "rptISWNOTGeneralNOI"
            End Select
        ElseIf loginvs.reportid = NOIProgramType.PesticideGeneralPermit Then
            Select Case (loginvs.submissiontype)
                Case NOISubmissionType.GeneralNOIPermit, NOISubmissionType.GeneralNOICorrection, NOISubmissionType.GeneralNOIRenewal
                    reportPath = reportPath & "rptAPNOI"
                Case NOISubmissionType.TerminateNOI
                    reportPath = reportPath & "rptAPNOTGeneralNOI"
            End Select
        End If


        Return getReport(reportPath, "PDF", loginvs.submissionid)

    End Function



    Public Shared Function createReport(ByVal progid As String, ByVal afflid As Int32, affltype As EISSqlAfflType, reportid As Integer) As String
        Dim reportPath As String = ConfigurationManager.AppSettings("ReportFolder")

        If reportid = NOIProgramType.CSSGeneralPermit Then
            Select Case (affltype)
                Case EISSqlAfflType.Owner
                    reportPath = reportPath & "rptGeneralNOIPublic"
                    afflid = 0
                Case EISSqlAfflType.Copermittee
                    reportPath = reportPath & "rptCoPermitteeNOIPublic"
            End Select
        ElseIf reportid = NOIProgramType.ISGeneralPermit Then


        ElseIf reportid = NOIProgramType.PesticideGeneralPermit Then

        End If

        'Return getReport(reportPath, "PDF", progid, afflid)
        Return String.Format("Forms/ViewReport.ashx?progid={0}&afflid={1}&reportURL={2}", progid, afflid, reportPath)

    End Function



    ''' <summary>
    ''' returns a report from the external report server 
    ''' </summary>
    ''' <param name="reportURL">report URL on the report server</param>
    ''' <param name="reportFormat">format of the report</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getReport(ByVal reportURL As String, ByVal reportFormat As String, subid As Integer) As Byte()
        'load the report
        'Dim report As New ReportServerDMZ.ReportingService()
        Dim report As New ReportServerDMZ.ReportExecutionService
        Dim creds As New System.Net.NetworkCredential()
        'Dim wc As New System.Net.WebClient


        creds.UserName = ConfigurationManager.AppSettings("ReportUserName")
        creds.Password = ConfigurationManager.AppSettings("ReportPassword")
        creds.Domain = ConfigurationManager.AppSettings("ReportDomain")
        report.Credentials = creds

        Dim result() As Byte = Nothing
        Dim credentials() As ReportServerDMZ.DataSourceCredentials = Nothing
        Dim warnings() As ReportServerDMZ.Warning = Nothing
        Dim parameters(1) As ReportServerDMZ.ParameterValue
        Dim sh As New ReportServerDMZ.ExecutionHeader
        report.ExecutionHeaderValue = sh

        parameters(0) = New ReportServerDMZ.ParameterValue()
        parameters(0).Name = "submissionid"
        parameters(0).Value = subid

        Try
            report.LoadReport(reportURL, Nothing)
            report.SetExecutionParameters(parameters, Nothing)
            result = _
            report.Render(reportFormat, Nothing, reportFormat, Nothing, Nothing, Nothing, Nothing)

            If warnings IsNot Nothing Then
                For Each warning As ReportServerDMZ.Warning In warnings
                    If warning.Severity = "Error" OrElse warning.Code.Contains("Error") OrElse warning.Message.Contains("Error") Then
                        result = Nothing
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception("Error generating report.", ex)
        End Try

        Return result
    End Function


    Private Shared Function getReport(ByVal reportURL As String, ByVal reportFormat As String, ByVal progid As String, ByVal afflid As Int32) As Byte()
        'load the report
        'Dim report As New ReportServerDMZ.ReportingService()
        Dim report As New ReportServerDMZ.ReportExecutionService
        Dim creds As New System.Net.NetworkCredential()
        'Dim wc As New System.Net.WebClient


        creds.UserName = ConfigurationManager.AppSettings("ReportUserName")
        creds.Password = ConfigurationManager.AppSettings("ReportPassword")
        creds.Domain = ConfigurationManager.AppSettings("ReportDomain")
        report.Credentials = creds

        Dim result() As Byte = Nothing
        Dim credentials() As ReportServerDMZ.DataSourceCredentials = Nothing
        Dim warnings() As ReportServerDMZ.Warning = Nothing
        Dim parameters(1) As ReportServerDMZ.ParameterValue
        Dim sh As New ReportServerDMZ.ExecutionHeader
        report.ExecutionHeaderValue = sh

        parameters(0) = New ReportServerDMZ.ParameterValue()
        parameters(0).Name = "progid"
        parameters(0).Value = progid
        If afflid > 0 Then
            parameters(1) = New ReportServerDMZ.ParameterValue()
            parameters(1).Name = "afflid"
            parameters(1).Value = afflid
        End If

        Try
            report.LoadReport(reportURL, Nothing)
            report.SetExecutionParameters(parameters, Nothing)
            result = _
            report.Render(reportFormat, Nothing, reportFormat, Nothing, Nothing, Nothing, Nothing)

            If warnings IsNot Nothing Then
                For Each warning As ReportServerDMZ.Warning In warnings
                    If warning.Severity = "Error" OrElse warning.Code.Contains("Error") OrElse warning.Message.Contains("Error") Then
                        result = Nothing
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception("Error generating report.", ex)
        End Try

        Return result
    End Function

End Class
