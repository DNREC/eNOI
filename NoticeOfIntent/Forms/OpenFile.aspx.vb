Public Class OpenFile
    Inherits BasePage



    Private Sub OpenFile_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Try
            With Response
                .Clear()
                .ClearHeaders()
                .ContentType = ("application/pdf")
                .BinaryWrite(NOIReports.createReport(logInVS)) '.submissiontype, logInVS.submissionid))
            End With
        Catch ex As Exception
            Me.registerErrorAndSendEmail(New Exception("Error opening report.", ex))
        End Try
    End Sub
End Class