Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class ISWRepository
    Implements IDisposable

    Private db As ISWDB

    Public Sub New()
        db = New ISWDB
    End Sub


    Public Function LogError(ByVal browser As String, ByVal requestedURL As String,
                            ByVal errorSource As String, ByVal errorMessage As String, ByVal submissionID As Integer,
                            ByVal stackTrace As String) As Integer

        Dim errlog As New ErrorLog
        errlog.ErrorDate = Date.Now
        errlog.Browser = browser
        errlog.RequestedURL = requestedURL
        errlog.ErrorSource = errorSource
        errlog.ErrorMessage = errorMessage
        errlog.SubmissionID = submissionID
        errlog.StackTrace = stackTrace

        db.ErrorLogs.Add(errlog)


        Try
            db.SaveChanges()

        Catch ex As Exception
            Emailer.SendEmailToAdministrator("Failed to save error in the database. " + common.getError(ex) + " Stack trace:" + common.getErrorStackTrace(ex) + " original error: " + errorMessage + " stackTrace: " + stackTrace + " SubmissionID: " + submissionID.ToString())
        End Try
        Return errlog.ErrorID

    End Function

    Public Sub Delete(submissionid As Integer) 'Implements ISubmissionRepository.Delete
        Dim ns As NOISubmission

        ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOILoc) _
            .Include(Function(c) c.NOISubmissionISWAP) _
            .Include(Function(t) t.NOISubmissionSIC) _
            .Include(Function(f) f.NOISubmissionNAICS) _
            .Include(Function(r) r.NOISubmissionTaxParcels) _
            .Include(Function(q) q.NOISubmissionUnit.Select(Function(c) c.NOILoc)) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress) _
            .Include(Function(g) g.NOISubmissionDocs).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)


        If ns.NOIProgSubmissionType.SubmissionTypeID = NOISubmissionType.GeneralNOIPermit Then
            ns.NOIProject.EntityState = EntityState.Deleted
            ns.NOILoc.EntityState = EntityState.Deleted

            For Each taxparcel As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
                taxparcel.EntityState = EntityState.Deleted
            Next

            For Each unit As NOISubmissionUnit In ns.NOISubmissionUnit
                unit.EntityState = EntityState.Deleted
                unit.NOILoc.EntityState = EntityState.Deleted
            Next


            For Each sic As NOISubmissionSIC In ns.NOISubmissionSIC
                sic.EntityState = EntityState.Deleted
            Next

            For Each NAICS As NOISubmissionNAICS In ns.NOISubmissionNAICS
                NAICS.EntityState = EntityState.Deleted
            Next

            For Each noifee As NOIFeeExemption In ns.NOIFeeExemption
                noifee.EntityState = EntityState.Deleted
            Next

        Else
            ns.NOISubmissionISWAP.EntityState = EntityState.Deleted

        End If

        For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noipersonorg.EntityState = EntityState.Deleted
        Next


        For Each noisign As NOISigningEmailAddress In ns.NOISigningEmailAddress
            noisign.EntityState = EntityState.Deleted
        Next

        For Each noistatus As NOISubmissionStatus In ns.NOISubmissionStatus
            noistatus.EntityState = EntityState.Deleted
        Next

        For Each noidocs As NOISubmissionDocs In ns.NOISubmissionDocs
            noidocs.EntityState = EntityState.Deleted
        Next

        ns.EntityState = EntityState.Deleted

        db.NOISubmissions.Add(ns)

        SaveChanges()
    End Sub

    Public Function GetNoExposureList(submission As NOISubmission) As IList
        Dim cl As New List(Of NOExposureCL)
        cl = db.NOExposureCL.ToList()

        Dim qry = (From l In cl.Where(Function(a) a.Active = "Y")
                   Group Join d In submission.NOISubmissionNE On
            l.NOExposureCLID Equals d.NOExposureCLID
            Into cl1 = Group From c In cl1.DefaultIfEmpty
                   Select l.NOExposureCLID, l.Question, SubmissionNEID = If(c Is Nothing, Nothing, c.SubmissionNEID),
            SubmissionID = If(c Is Nothing, Nothing, c.SubmissionID),
            Answer = If(c Is Nothing, Nothing, c.Answer))
        Return qry.ToList()
    End Function

    Public Function GetAllSubmissionsByUserByProjects(user As String, commadelimitedProjectnames As String) As IQueryable(Of NOISubmissionSearchlst)

        Dim projectnamesarr As String() = commadelimitedProjectnames.Split(",")
        Return db.NOISubmissionSearchTable.Where(Function(e) projectnamesarr.Contains(e.ProjectNumber) Or e.CreatedBy = user And e.ProgramID = NOIProgramType.ISGeneralPermit).OrderBy(Function(a) a.DisplayOrder)

    End Function


    Public Function GetAllSubmissionsForAdmin(ByVal ProjectName As String, ByVal PermitNumber As String,
                                          ByVal Owners As String, ByVal SubmissionType As Integer, ByVal ReferenceNo As String, ByVal SubmissionStatusCode As String,
                                              ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOISubmissionSearchlst)

        'db.Database.Log = Sub(val) Debug.Write(val)

        Dim Submissions As IQueryable(Of NOISubmissionSearchlst)

        Submissions = db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.ISGeneralPermit)

        If Not ReferenceNo Is Nothing AndAlso Convert.ToInt32(ReferenceNo) <> 0 Then
            Dim refno As Int32 = Convert.ToInt32(ReferenceNo)
            Submissions = Submissions.Where(Function(b) b.ReferenceNo = refno)
        End If

        If Not ProjectName Is Nothing AndAlso Trim(ProjectName) <> String.Empty Then
            Submissions = Submissions.Where(Function(b) b.ProjectName.Contains(ProjectName))
        End If

        If Not PermitNumber Is Nothing AndAlso Trim(PermitNumber) <> String.Empty Then
            Submissions = Submissions.Where(Function(b) b.PermitNumber.Contains(PermitNumber))
        End If

        If Not Owners Is Nothing AndAlso Trim(Owners) <> String.Empty Then
            Submissions = Submissions.Where(Function(b) b.Owners.Contains(Owners))
        End If

        If SubmissionType <> 0 Then
            Submissions = Submissions.Where(Function(b) b.ProgSubmissionTypeID = SubmissionType)
        End If

        If Not SubmissionStatusCode Is Nothing AndAlso Trim(SubmissionStatusCode) <> String.Empty Then
            Submissions = Submissions.Where(Function(b) b.SubmissionStatusCode = SubmissionStatusCode)
        End If


        totalRowCount = Submissions.Count()

        Return Submissions.OrderBy(Function(a) a.DisplayOrder).ThenByDescending(Function(b) b.IsSigned).ThenByDescending(Function(c) c.ReferenceNo).Skip(startRowIndex).Take(maximumRows)




        'Return db.NOISubmissionSearchTable.OrderBy(Function(a) a.DisplayOrder).ThenByDescending(Function(b) b.ReferenceNo)

    End Function

    Public Function GetAllSubmissionsForAdmin() As IQueryable(Of NOISubmissionSearchlst) 'Implements ISubmissionRepository.GetAllSubmissionsForAdmin

        'db.Database.Log = Sub(val) Debug.Write(val)
        Return db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.ISGeneralPermit).OrderBy(Function(a) a.DisplayOrder).ThenByDescending(Function(b) b.ReferenceNo)

    End Function

    Public Function GetSubmissionByRefForAdmin(ByVal refno As Integer) As NOISubmissionSearchlst
        Return db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.ISGeneralPermit And e.ReferenceNo = refno).SingleOrDefault()
    End Function


    Public Function GetSubmissionByIDForDisplay(submissionid As Integer, progsubtype As Integer) As NOISubmission
        Dim ns As NOISubmission

        'db.Database.Log = Sub(val) Debug.Write(val)
        Dim subtype As NOISubmissionType

        subtype = CType(db.NOIProgSubmissionType.Single(Function(a) a.ProgSubmissionTypeID = progsubtype).SubmissionTypeID, NOISubmissionType)

        If subtype = NOISubmissionType.GeneralNOIPermit Then

            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOILoc) _
            .Include(Function(c) c.NOISubmissionISWAP) _
            .Include(Function(t) t.NOISubmissionSIC) _
            .Include(Function(f) f.NOISubmissionNAICS) _
            .Include(Function(r) r.NOISubmissionTaxParcels) _
            .Include(Function(q) q.NOISubmissionUnit.Select(Function(c) c.NOILoc)) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

            Return ns
        Else

            Dim adrep As New AdminRepository
            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
                .Include(Function(c) c.NOISubmissionISWAP) _
                .Include(Function(o) o.NOISubmissionPersonOrg) _
                .Include(Function(s) s.NOISubmissionStatus) _
                .Include(Function(b) b.NOIFeeExemption) _
                .Include(Function(e) e.NOIProgSubmissionType) _
                .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

            adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrgForIS(ns.NOIProject.PermitNumber, ns)
            ns.NOILoc = Nothing
            ns.NOISubmissionTaxParcels.Clear()


            Return ns

        End If



    End Function

    Public Function GetSubmissionByIDForGeneralNOI(submissionid As Integer) As NOISubmission
        Dim ns As NOISubmission
        Try

            'db.Database.Log = Sub(val) Debug.Write(val)

            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOILoc) _
            .Include(Function(c) c.NOISubmissionISWAP) _
            .Include(Function(t) t.NOISubmissionSIC) _
            .Include(Function(f) f.NOISubmissionNAICS) _
            .Include(Function(r) r.NOISubmissionTaxParcels) _
            .Include(Function(g) g.NOISubmissionNE) _
            .Include(Function(q) q.NOISubmissionUnit.Select(Function(c) c.NOILoc)) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid) ' And n.NOIProgSubmissionType.ProgramID = programid)


            ns.EntityState = EntityState.Modified
            ns.NOIProject.EntityState = EntityState.Modified
            If ns.NOILoc IsNot Nothing Then
                ns.NOILoc.EntityState = EntityState.Modified
            End If
            If ns.NOISubmissionISWAP IsNot Nothing Then
                ns.NOISubmissionISWAP.EntityState = EntityState.Modified
            End If

            For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
                noipersonorg.EntityState = EntityState.Modified
            Next

            Return ns
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GetSubmissionByIDForNOT(submissionid As Integer) As NOISubmission
        Dim ns As NOISubmission

        ns = db.NOISubmissions.Include(Function(r) r.NOIProject) _
            .Include(Function(c) c.NOISubmissionISWAP) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(a) a.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid) ' And n.NOIProgSubmissionType.ProgramID = programid)


        Dim adrep As New AdminRepository
        adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrgForIS(ns.NOIProject.PermitNumber, ns)

        ns.EntityState = EntityState.Modified
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOISubmissionISWAP.EntityState = EntityState.Modified

        For Each taxparcel As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            taxparcel.EntityState = EntityState.Unchanged
        Next

        For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noipersonorg.EntityState = EntityState.Unchanged
        Next

        Return ns
    End Function

    Public Function GetProjectOwnerDetailsByProjectID(projectid As Integer) As NOISubmission

        Dim ns As NOISubmission = Nothing
        Try

            db.Database.Log = Sub(val) Debug.Write(val)
            ns = db.NOISubmissions.Create()

            Dim query = db.NOIProjects.Where(Function(a) a.ProjectID = projectid)


            Dim result = query.Single()
            ns.NOIProject = result

            Dim adrep As New AdminRepository
            adrep.GetNOIProjectDetailsByPermitNumberForIS(result.PermitNumber, ns)

            'ns.EntityState = EntityState.Added
            'ns.NOIProject.EntityState = EntityState.Unchanged
            'ns.NOILoc.EntityState = EntityState.Unchanged

            'For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            '    noiperorg.EntityState = EntityState.Added
            'Next
            'For Each noitp As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            '    noitp.EntityState = EntityState.Unchanged
            'Next


        Catch ex As Exception
            Throw ex
        End Try
        Return ns
    End Function


    Public Function GetProjectOwnerDetailsByProjectIDForNOT(projectid As Integer) As NOISubmission
        Dim ns As NOISubmission = GetProjectOwnerDetailsByProjectID(projectid)
        ns.EntityState = EntityState.Added
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOILoc.EntityState = EntityState.Unchanged

        For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noiperorg.EntityState = EntityState.Added
        Next
        For Each noitp As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            noitp.EntityState = EntityState.Unchanged
        Next

        Return ns
    End Function

    Public Function GetProjectDetailsForNOICorrectionAndRenewalByProjectID(projectid As Integer) As NOISubmission
        Dim ns As NOISubmission = GetProjectOwnerDetailsByProjectID(projectid)
        ns.EntityState = EntityState.Added
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOILoc.EntityState = EntityState.Added
        ns.NOISubmissionSWConstruct.EntityState = EntityState.Added
        For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noiperorg.EntityState = EntityState.Added
        Next
        For Each noitp As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            noitp.EntityState = EntityState.Added
        Next

        ns.NOIReceivedDate = Nothing

        Return ns
    End Function


    Public Function GetProjectsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)


        Dim pitypeid As Integer
        pitypeid = CacheLookupData.GetPiTypeIDByReportID(NOIProgramType.ISGeneralPermit)

        Dim adrep As New AdminRepository
        Dim projects As IEnumerable(Of NOIProject) = adrep.GetNOIByUser(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames, pitypeid, maximumRows, startRowIndex, totalRowCount)

        Return projects


    End Function

    Public Function GetAllProjectsOfGeneralNOI(ProjectNumber As String, ProjectName As String, PermitNumber As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        'db.Database.Log = Sub(val) Debug.Write(val)
        Dim submissiontypeid As Integer = NOISubmissionType.GeneralNOIPermit

        Dim pitypeid As Integer
        pitypeid = CacheLookupData.GetPiTypeIDByReportID(NOIProgramType.ISGeneralPermit)


        Dim adrep As New AdminRepository
        Dim projects As IEnumerable(Of NOIProject) = adrep.GetNOIByUser(ProjectNumber, ProjectName, PermitNumber, String.Empty, pitypeid, maximumRows, startRowIndex, totalRowCount)

        Return projects



    End Function

    Public Function GetUploadedDocuments(submissionid As Integer) As IQueryable(Of NOISubmissionDocs)
        Return db.NOISubmissionDocs.Where(Function(a) a.SubmissionID = submissionid)
    End Function

    Public Function GetUploadedDocumentByNOIDocID(noidocid As Integer, submissionid As Integer) As NOISubmissionDocs
        Return db.NOISubmissionDocs.Where(Function(a) a.SubmissionID = submissionid And a.NOIDocID = noidocid).Include(Function(b) b.NOISubmissionDocFile).FirstOrDefault()
    End Function

    Public Sub DeleteFile(NOIDocID As Integer, SubmissionID As Integer)
        Dim doc As NOISubmissionDocs
        doc = db.NOISubmissionDocs.Where(Function(a) a.SubmissionID = SubmissionID And a.NOIDocID = NOIDocID).Include(Function(b) b.NOISubmissionDocFile).FirstOrDefault()
        db.NOISubmissionDocs.Remove(doc)
        db.SaveChanges()
    End Sub

    Public Sub InsertFile(submissionId As Integer, documentName As String, documentDesc As String, documentType As String, document As Byte())
        Dim doc As New NOISubmissionDocs
        doc.SubmissionID = submissionId
        doc.DocumentDesc = documentDesc
        doc.DocumentName = documentName
        doc.DocumentType = documentType
        Dim docbyte As New NOISubmissionDocFile
        docbyte.UploadedDocument = document
        doc.NOISubmissionDocFile = docbyte

        db.NOISubmissionDocs.Add(doc)
        db.SaveChanges()
        'Return db.NOISubmissionDocs
    End Sub


    Public Function Insert(submission As NOISubmission) As NOISubmission 'Implements ISubmissionRepository.Insert

        Dim ns As NOISubmission
        ns = Update(submission)
        Return ns

    End Function

    Public Function Update(submission As NOISubmission) As NOISubmission 'Implements ISubmissionRepository.Update

        db.NOISubmissions.Add(submission)

        Try
            'db.Database.Log = Sub(val) Debug.Write(val)
            SaveChanges()

            Dim ss As NOISubmission = GetSubmissionByIDForDisplay(submission.SubmissionID, submission.ProgSubmissionTypeID)  'db.NOISubmissions.Where(Function(a) a.SubmissionID = submission.SubmissionID).Single()
            Return ss
        Catch ex As Exception
            LogError(String.Empty, String.Empty, ex.Source, common.getError(ex), submission.SubmissionID, common.getErrorStackTrace(ex))
            Throw ex
        End Try

    End Function


    Private Sub SaveChanges()
        Try
            common.ApplyStateChanges(db)
            'db.Database.Log = Sub(val) Debug.Write(val)
            db.SaveChanges()
        Catch ex As Exception
            LogError(String.Empty, String.Empty, ex.Source, common.getError(ex), -99, common.getErrorStackTrace(ex))
            Throw ex
        End Try
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
