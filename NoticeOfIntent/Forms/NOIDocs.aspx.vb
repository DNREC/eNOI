Imports System.IO
Imports System.Reflection.Emit

Public Class NOIDocs
    Inherits FormBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfMaxlength.Value = ConfigurationManager.AppSettings("MaxFileSize")
        End If
    End Sub

    Private Sub btnUploadFiles_Click(sender As Object, e As EventArgs) Handles btnUploadFiles.Click
        'fileupload.SaveAs()
        Dim bal As New ISWBAL()
        If Page.IsValid Then
            If fileupload.FileName <> String.Empty Then
                Dim uf As New FileInfo(fileupload.FileName)
                Dim submission As NOISubmission = IndNOISubmission

                bal.InsertFile(submission.SubmissionID, fileupload.FileName, txtFileDesc.Text, uf.Extension, fileupload.FileBytes)

                'IndNOISubmission = submission
                lvUploadedFiles.DataBind()
            End If
            txtFileDesc.Text = String.Empty
        End If
    End Sub

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvUploadedFiles_GetData() As IQueryable(Of NoticeOfIntent.NOISubmissionDocs)
        Dim submission As NOISubmission = IndNOISubmission

        Dim bal As New ISWBAL()
        Return bal.GetUploadedDocuments(submission.SubmissionID)
    End Function

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvUploadedFiles_DeleteItem(ByVal NOIDocID As Integer, ByVal SubmissionID As Integer)

        Dim bal As New ISWBAL()
        bal.DeleteFile(NOIDocID, SubmissionID)

        lvUploadedFiles.DataBind()
    End Sub

    Private Sub cvCheckDocument_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvCheckDocument.ServerValidate

        Select Case logInVS.reportid
            Case NOIProgramType.ISGeneralPermit
                If lvUploadedFiles.Items.Count > 0 Then
                    args.IsValid = True
                Else
                    args.IsValid = False
                End If
            Case Else
                args.IsValid = True
        End Select


    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        If Page.IsValid Then
            responseRedirect("~/Forms/SubmissionDetails.aspx")
        End If
    End Sub

    Private Sub lvUploadedFiles_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles lvUploadedFiles.ItemCommand
        If e.CommandName = "download" Then
            Dim bal As New ISWBAL()
            Dim file As NOISubmissionDocs
            file = bal.GetUploadedDocumentByNOIDocID(e.CommandArgument, logInVS.submissionid)
            If Not file Is Nothing Then
                download(file)
            End If
        End If
    End Sub


    Private Sub download(ByVal file As NOISubmissionDocs)

        Dim bytes() As Byte = file.NOISubmissionDocFile.UploadedDocument
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.AddHeader("content-disposition", "attachment;filename=" & file.DocumentName)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub

    Private Sub cvFileUpload_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvFileUpload.ServerValidate
        Dim Fullfilename As String
        Dim MaxFileLength As Integer
        MaxFileLength = hfMaxlength.Value 'ConfigurationManager.AppSettings("MaxFileSize")
        Fullfilename = fileupload.PostedFile.FileName
        If fileupload.FileName <> String.Empty Then
            If Regex.IsMatch(Fullfilename, "^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.pdf|.PDF|.tif|.TIF|.png|.PNG)$", RegexOptions.IgnoreCase) _
                    OrElse Regex.IsMatch(Fullfilename, "^((\w[\w-].*))(.jpg|.JPG|.gif|.GIF|.pdf|.PDF|.tif|.TIF|.png|.PNG)$", RegexOptions.IgnoreCase) Then
                If (fileupload.PostedFile.ContentLength / 1024) <= MaxFileLength Then
                    If Not fileupload.HasFile Then
                        args.IsValid = False
                        cvFileUpload.ErrorMessage = String.Format("Could not find file {0}", Fullfilename)
                    Else
                        args.IsValid = True
                    End If
                Else
                    args.IsValid = False
                    cvFileUpload.ErrorMessage = "You can only upload a document of size " & (MaxFileLength / 1024).ToString() & " mb."
                End If
            Else
                args.IsValid = False
                cvFileUpload.ErrorMessage = "Only JPG, GIF, TIF, PNG and PDF can be uploaded"
            End If
        Else
            args.IsValid = False
            cvFileUpload.ErrorMessage = "No file is selected"
        End If
    End Sub
End Class