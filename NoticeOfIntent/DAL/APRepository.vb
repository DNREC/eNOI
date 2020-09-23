Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class APRepository
    Implements IDisposable


    Private db As PesticidesDB

    Public Sub New()
        db = New PesticidesDB
    End Sub

    Public Function GetAPEntityType() As IList(Of NOIEntityType)
        Return db.NOIEntityType.ToList()
    End Function

    Public Function GetPesticidePatterns() As IList(Of NOIPesticidePattern)
        Return db.NOIPesticidePatterns.ToList()
    End Function

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
            .Include(Function(d) d.NOISubmissionAP) _
            .Include(Function(c) c.NOISubmissionISWAP) _
            .Include(Function(f) f.NOISubmissionAPChemicals) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)


        If ns.NOIProgSubmissionType.SubmissionTypeID = NOISubmissionType.GeneralNOIPermit Then

            ns.NOIProject.EntityState = EntityState.Deleted
            ns.NOISubmissionAP.EntityState = EntityState.Deleted

            For Each apchemical As NOISubmissionAPChemicals In ns.NOISubmissionAPChemicals
                apchemical.EntityState = EntityState.Deleted
            Next

        End If

        If ns.NOISubmissionISWAP IsNot Nothing Then
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

        ns.EntityState = EntityState.Deleted

        db.NOISubmissions.Add(ns)
        SaveChanges()
    End Sub


    Public Function GetAllSubmissionsByUserByProjects(user As String, commadelimitedProjectnames As String) As IQueryable(Of NOISubmissionSearchlst)

        Dim projectnamesarr As String() = commadelimitedProjectnames.Split(",")
        Return db.NOISubmissionSearchTable.Where(Function(e) projectnamesarr.Contains(e.ProjectNumber) Or e.CreatedBy = user And e.ProgramID = NOIProgramType.PesticideGeneralPermit).OrderBy(Function(a) a.DisplayOrder)

    End Function


    Public Function GetAllSubmissionsForAdmin(ByVal ProjectName As String, ByVal PermitNumber As String,
                                          ByVal Owners As String, ByVal SubmissionType As Integer, ByVal ReferenceNo As String, ByVal SubmissionStatusCode As String,
                                              ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOISubmissionSearchlst)

        'db.Database.Log = Sub(val) Debug.Write(val)

        Dim Submissions As IQueryable(Of NOISubmissionSearchlst)

        Submissions = db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.PesticideGeneralPermit)

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
        Return db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.PesticideGeneralPermit).OrderBy(Function(a) a.DisplayOrder).ThenByDescending(Function(b) b.ReferenceNo)

    End Function

    Public Function GetSubmissionByRefForAdmin(ByVal refno As Integer) As NOISubmissionSearchlst
        Return db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.PesticideGeneralPermit And e.ReferenceNo = refno).SingleOrDefault()
    End Function

    Public Function GetSubmissionByIDForDisplay(submissionid As Integer, progsubtype As Integer) As NOISubmission
        Dim ns As NOISubmission

        'db.Database.Log = Sub(val) Debug.Write(val)
        Dim subtype As NOISubmissionType

        subtype = CType(db.NOIProgSubmissionType.Single(Function(a) a.ProgSubmissionTypeID = progsubtype).SubmissionTypeID, NOISubmissionType)

        If subtype = NOISubmissionType.GeneralNOIPermit Then

            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOISubmissionAP) _
            .Include(Function(c) c.NOISubmissionISWAP) _
            .Include(Function(f) f.NOISubmissionAPChemicals) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

            Return ns
        Else

            Dim adrep As New AdminRepository
            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
                .Include(Function(c) c.NOISubmissionISWAP) _
                .Include(Function(o) o.NOISubmissionPersonOrg) _
                .Include(Function(s) s.NOISubmissionStatus) _
                .Include(Function(e) e.NOIProgSubmissionType) _
                .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

            adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrgForAP(ns.NOIProject.PermitNumber, ns)

            Return ns

        End If



    End Function

    Public Function GetSubmissionByIDForGeneralNOI(submissionid As Integer) As NOISubmission
        Dim ns As NOISubmission
        Try

            'db.Database.Log = Sub(val) Debug.Write(val)

            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOISubmissionAP) _
            .Include(Function(c) c.NOISubmissionISWAP) _
            .Include(Function(t) t.NOISubmissionAPChemicals.Select(Function(x) x.NOIPesticidePattern)) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid) ' And n.NOIProgSubmissionType.ProgramID = programid)


            ns.EntityState = EntityState.Modified

            ns.NOIProject.EntityState = EntityState.Modified

            If ns.NOISubmissionAP IsNot Nothing Then
                ns.NOISubmissionAP.EntityState = EntityState.Modified
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
            .Include(Function(a) a.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid) ' And n.NOIProgSubmissionType.ProgramID = programid)


        Dim adrep As New AdminRepository
        adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrgForAP(ns.NOIProject.PermitNumber, ns)

        ns.EntityState = EntityState.Modified
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOISubmissionISWAP.EntityState = EntityState.Modified

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
            adrep.GetNOIProjectDetailsByPermitNumberForAP(result.PermitNumber, ns)

            'ns.EntityState = EntityState.Added
            'ns.NOIProject.EntityState = EntityState.Unchanged

            'For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            '    noiperorg.EntityState = EntityState.Added
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
        Dim removepersonorg As New HashSet(Of NOISubmissionPersonOrg)

        For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            If noiperorg.NOIPersonOrgTypeID = NOIPersonOrgType.OwnerDetails Or noiperorg.NOIPersonOrgTypeID = NOIPersonOrgType.ProjectAddress Then
                noiperorg.EntityState = EntityState.Added
            Else
                removepersonorg.Add(noiperorg)
                'ns.NOISubmissionPersonOrg.Remove(noiperorg)
            End If
        Next
        For Each removePerorg As NOISubmissionPersonOrg In removepersonorg
            ns.NOISubmissionPersonOrg.Remove(removePerorg)
        Next

        ns.NOISubmissionAP = Nothing
        ns.NOISubmissionAPChemicals.Clear()

        Return ns

    End Function

    Public Function GetProjectDetailsForNOICorrectionAndRenewalByProjectID(projectid As Integer) As NOISubmission
        Dim ns As NOISubmission = GetProjectOwnerDetailsByProjectID(projectid)
        ns.EntityState = EntityState.Added
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOISubmissionAP.EntityState = EntityState.Added
        For Each apchemical As NOISubmissionAPChemicals In ns.NOISubmissionAPChemicals
            apchemical.EntityState = EntityState.Added
            apchemical.NOIPesticidePattern = db.NOIPesticidePatterns.Where(Function(a) a.PesticidePatternID = apchemical.PesticidePatternID).SingleOrDefault()
        Next
        For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noiperorg.EntityState = EntityState.Added
            'noiperorg.AfflID = Nothing
        Next
        ns.NOIReceivedDate = Nothing

        Return ns
    End Function

    Public Function GetProjectsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)


        Dim pitypeid As Integer
        pitypeid = CacheLookupData.GetPiTypeIDByReportID(NOIProgramType.PesticideGeneralPermit)

        Dim adrep As New AdminRepository
        Dim projects As IEnumerable(Of NOIProject) = adrep.GetNOIByUser(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames, pitypeid, maximumRows, startRowIndex, totalRowCount)

        Return projects


    End Function


    Public Function GetAllProjectsOfGeneralNOI(ProjectNumber As String, ProjectName As String, PermitNumber As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)
        'db.Database.Log = Sub(val) Debug.Write(val)
        Dim submissiontypeid As Integer = NOISubmissionType.GeneralNOIPermit

        Dim pitypeid As Integer
        pitypeid = CacheLookupData.GetPiTypeIDByReportID(NOIProgramType.PesticideGeneralPermit)


        Dim adrep As New AdminRepository
        Dim projects As IEnumerable(Of NOIProject) = adrep.GetNOIByUser(ProjectNumber, ProjectName, PermitNumber,String.Empty, pitypeid, maximumRows, startRowIndex, totalRowCount)

        Return projects



    End Function






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
            db.Database.Log = Sub(val) Debug.Write(val)
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
