Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class NOIRepository
    Implements IDisposable

    Private db As NOIDB

    Public Sub New()
        db = New NOIDB
    End Sub


    Public Function GetNOIWebPageCtrlTxt() As IList(Of NOIWebPage)
        Return db.NOIWebPages.Include(Function(a) a.NOIWebPageCtrlTxts).ToList()
    End Function

    Public Function GetStates() As IList(Of StateAbvlst) 'Implements ISubmissionRepository.GetStates
        Dim list As List(Of StateAbvlst)
        list = db.StateAbvTable.ToList()
        Return list
    End Function


    Public Function GetCompanyType() As IList(Of CompanyTypelst) 'Implements ISubmissionRepository.GetCompanyType
        Dim list As List(Of CompanyTypelst)
        list = db.CompanyTypeTable.ToList()
        Return list
    End Function

    Public Function GetNOIPrograms() As IList(Of NOIProgram)
        Return db.NOIProgram.ToList
    End Function


    Public Function GetSubmissionTypeList() As IList(Of NOISubmissionTypelst) 'Implements ISubmissionRepository.GetSubmissionTypeList
        'Dim list As List(Of NOISubmissionTypelst)
        'list = db.NOISubmissionTypelst.ToList()
        Return db.NOISubmissionTypelst.ToList()
    End Function

    Public Function GetNOIProgSubmissionTypes() As IList(Of NOIProgSubmissionType)
        Return db.NOIProgSubmissionType.ToList()
    End Function

    Public Function GetSubmissionStatusCodes() As IList(Of NOISubmissionStatusCode)
        Return db.NOISubmissionStatusCodes.ToList()
    End Function


    Public Sub RemoveSessionStorageByUser(user As String)

        Dim d As NOISessionStorage = db.NOISessionStorage.Where(Function(e) e.username = user).SingleOrDefault()
        If d IsNot Nothing Then
            db.NOISessionStorage.Remove(d)
            db.SaveChanges()
        End If

    End Sub


    Public Function GetSessionStorageByUser(user As String) As String 'Implements ISubmissionRepository.GetSessionStorageByUser
        'db.Database.Log = Sub(val) Debug.Write(val)
        Dim d As NOISessionStorage = db.NOISessionStorage.Where(Function(e) e.username = user).AsNoTracking().SingleOrDefault()
        If d IsNot Nothing Then
            Return d.SessionStorageJSON
        Else
            Return String.Empty
        End If
    End Function

    Public Sub SetSessionStorageByUser(user As String, sessionstorage As String) 'Implements ISubmissionRepository.SetSessionStorageByUser
        'db.Database.Log = Sub(val) Debug.Write(val)
        Dim d As NOISessionStorage = db.NOISessionStorage.Where(Function(e) e.username = user).SingleOrDefault()
        If d IsNot Nothing Then
            'db.NOISessionStorage.Attach(d)
            db.Entry(d).State = Entity.EntityState.Modified
            d.SessionStorageJSON = sessionstorage
        Else
            d = New NOISessionStorage()
            d.username = user
            d.SessionStorageJSON = sessionstorage
            db.NOISessionStorage.Add(d)
        End If
        db.SaveChanges()
    End Sub

    Public Function GetAgreementTextByProgSubType(progSubmissionTypeID As Integer) As IList(Of NOIProgSubmissionType)
        Return db.NOIProgSubmissionType.Where(Function(a) a.ProgSubmissionTypeID = progSubmissionTypeID).ToList()
    End Function

    Public Function GetSubmissionStatusesBySubmissionID(subid As Integer) As IQueryable
        Dim query = db.NOISubmissionStatuses.Include(Function(b) b.NOISubmissionStatusCode).Where(Function(a) a.SubmissionID = subid).Select(Function(c) New With {c.SubmissionStatusDate, c.NOISubmissionStatusCode.SubmissionStatus, c.SubmissionStatusComment})
        Return query.AsQueryable()
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

    Public Function GetAllProjects(programid As Integer) As IQueryable(Of NOIProject)
        Return db.NOIProjects.Where(Function(a) a.ProgramID = programid).AsQueryable()
    End Function
    Public Function GetAllExemptionCodes(programid As Integer) As IQueryable(Of NOIFeeExemption)
        Return db.NOIFeeExemptions.Where(Function(a) a.ProgramID = programid).AsQueryable()
    End Function

    Public Sub SaveExemptionCode(ExemptionCode As NOIFeeExemption)
        db.NOIFeeExemptions.Add(ExemptionCode)
        common.ApplyStateChanges(db)
        db.SaveChanges()
    End Sub

    Public Sub DeleteExemptionCode(ExemptionID As Integer)
        Dim exemptioncode As NOIFeeExemption = db.NOIFeeExemptions.Where(Function(a) a.ExemptionID = ExemptionID).Single()
        exemptioncode.EntityState = EntityState.Deleted
        SaveExemptionCode(exemptioncode)
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
