Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common


Public Class SWRepository
    Implements IDisposable
    'Implements ISubmissionRepository


    Private db As SWDB

    Public Sub New()
        db = New SWDB
    End Sub


    ''verified
    Public Sub Delete(submissionid As Integer) 'Implements ISubmissionRepository.Delete
        Dim ns As NOISubmission
        ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOILoc) _
            .Include(Function(c) c.NOISubmissionSW) _
            .Include(Function(t) t.NOISubmissionSWConstruct) _
            .Include(Function(r) r.NOISubmissionTaxParcels) _
            .Include(Function(q) q.NOISubmissionSWBMP.Select(Function(c) c.SWBMPlst)) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)


        If ns.NOIProgSubmissionType.SubmissionTypeID = NOISubmissionType.GeneralNOIPermit Then
            ns.NOIProject.EntityState = EntityState.Deleted
            ns.NOILoc.EntityState = EntityState.Deleted
            ns.NOISubmissionSWConstruct.EntityState = EntityState.Deleted

            For Each taxparcel As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
                taxparcel.EntityState = EntityState.Deleted
            Next

            For Each swbmp As NOISubmissionSWBMP In ns.NOISubmissionSWBMP
                swbmp.EntityState = EntityState.Deleted
            Next

            For Each noifee As NOIFeeExemption In ns.NOIFeeExemption
                noifee.EntityState = EntityState.Deleted
            Next

        ElseIf ns.NOIProgSubmissionType.SubmissionTypeID = NOISubmissionType.CoPermittee Then
            For Each noifee As NOIFeeExemption In ns.NOIFeeExemption
                noifee.EntityState = EntityState.Deleted
            Next

        ElseIf ns.NOIProgSubmissionType.SubmissionTypeID = NOISubmissionType.TerminateCoPermittee Then
            ns.NOISubmissionSW.EntityState = EntityState.Deleted

        ElseIf ns.NOIProgSubmissionType.SubmissionTypeID = NOISubmissionType.TerminateNOI Then
            ns.NOISubmissionSW.EntityState = EntityState.Deleted

        End If



        For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noipersonorg.EntityState = EntityState.Deleted
        Next


        For Each noisign As NOISigningEmailAddress In ns.NOISigningEmailAddress
            noisign.EntityState = EntityState.Deleted
        Next

        For Each noidocs As NOISubmissionDocs In ns.NOISubmissionDocs
            noidocs.EntityState = EntityState.Deleted
        Next


        For Each noistatus As NOISubmissionStatus In ns.NOISubmissionStatus
            noistatus.EntityState = EntityState.Deleted
        Next

        ns.EntityState = EntityState.Deleted

        db.NOISubmissions.Add(ns)
        SaveChanges()
    End Sub


    ''verified
    Public Function GetAllSubmissionsByUserByProjects(user As String, commadelimitedProjectnames As String) As IQueryable(Of NOISubmissionSearchlst)
        Dim projectnamesarr As String() = commadelimitedProjectnames.Split(",")

        Try
            Return db.NOISubmissionSearchTable.Where(Function(e) projectnamesarr.Contains(e.ProjectNumber) Or e.CreatedBy = user And e.ProgramID = NOIProgramType.CSSGeneralPermit).OrderBy(Function(a) a.DisplayOrder)
        Catch ex As Exception
            Throw ex
        End Try



    End Function

    Public Function GetAllSubmissionsForAdmin() As IQueryable(Of NOISubmissionSearchlst) 'Implements ISubmissionRepository.GetAllSubmissionsForAdmin

        Try
            'db.Database.Log = Sub(val) Debug.Write(val)
            Return db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.CSSGeneralPermit).OrderBy(Function(a) a.DisplayOrder).ThenByDescending(Function(b) b.ReferenceNo)

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Function GetSubmissionByRefForAdmin(ByVal refno As Integer) As NOISubmissionSearchlst
        Try
            Return db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.CSSGeneralPermit And e.ReferenceNo = refno).SingleOrDefault()
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetAllSubmissionsForAdmin(ByVal ProjectName As String, ByVal PermitNumber As String,
                                          ByVal Owners As String, ByVal SubmissionType As Integer, ByVal ReferenceNo As String, SubmissionStatusCode As String,
                                              ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOISubmissionSearchlst)

        'db.Database.Log = Sub(val) Debug.Write(val)


        Dim Submissions As IQueryable(Of NOISubmissionSearchlst)

        Try
            Submissions = db.NOISubmissionSearchTable.Where(Function(e) e.ProgramID = NOIProgramType.CSSGeneralPermit)

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
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    ''verified
    Public Function GetAllApprovedCoPermitSubmissionsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String) As IQueryable(Of ProjectOwnerView)


        Try


            'Dim projectnumber As New List(Of String)
            'projectnumber = (From s In commadelimitedProjectnames.Split(",") Select s).ToList()

            'Dim commadelimitedpermitnumbers As String
            'commadelimitedpermitnumbers = String.Join(",", db.NOIProjects.Where(Function(a) projectnumber.Contains(a.ProjectNumber) And a.PermitNumber IsNot Nothing).Select(Function(y) y.PermitNumber).ToArray())


            Dim adrep As New AdminRepository
            Dim projectview As IQueryable(Of ProjectOwnerView) = adrep.GetNOIAndCoPermitteeDetails(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames)

            Return projectview
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''verified
    Public Function GetAllApprovedCoPermitSubmissions(ProjectNumber As String, ProjectName As String, PermitNumber As String) As IQueryable(Of ProjectOwnerView)
        Dim adrep As New AdminRepository
        Dim projectview As IQueryable(Of ProjectOwnerView) = adrep.GetAllNOIAndCoPermitteeDetails(ProjectNumber, ProjectName, PermitNumber) 'GetNOIAndCoPermitteeDetails(0)

        Return projectview
    End Function


    Public Function GetProjectsByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedProjectnames As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)


        Dim pitypeid As Integer
        pitypeid = CacheLookupData.GetPiTypeIDByReportID(NOIProgramType.CSSGeneralPermit)

        Dim adrep As New AdminRepository
        Dim projects As IEnumerable(Of NOIProject) = adrep.GetNOIByUser(ProjectNumber, ProjectName, PermitNumber, commadelimitedProjectnames, pitypeid, maximumRows, startRowIndex, totalRowCount)

        Return projects


    End Function


    Public Function GetAllProjectsOfGeneralNOI(ProjectNumber As String, ProjectName As String, PermitNumber As String, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)

        Dim pitypeid As Integer
        pitypeid = CacheLookupData.GetPiTypeIDByReportID(NOIProgramType.CSSGeneralPermit)



        Dim adrep As New AdminRepository
        Dim projects As IEnumerable(Of NOIProject) = adrep.GetNOIByUser(ProjectNumber, ProjectName, PermitNumber, String.Empty, pitypeid, maximumRows, startRowIndex, totalRowCount)

        Return projects

    End Function


    Public Function GetAllProjects() As IQueryable(Of NOIProject)
        Return db.NOIProjects.AsQueryable()
    End Function

    Public Function GetAllExemptionCodes(programid As Integer) As IQueryable(Of NOIFeeExemption)
        Return db.NOIFeeExemptions.Where(Function(a) a.ProgramID = programid).AsQueryable()
    End Function


    Public Function GetPlanApprovalAgencyList() As IList(Of PlanApprovalAgency)
        Return db.Database.SqlQuery(Of PlanApprovalAgency)("spPlanApprovalAgency_Get").ToList()
    End Function


    Public Function GetTaxKentHundred() As IList(Of TaxKentHundred)
        Return db.Database.SqlQuery(Of TaxKentHundred)("GettbltaxKentHundred").ToList()
    End Function


    Public Function GetTaxKentTowns() As IList(Of TaxKentTown)
        Return db.Database.SqlQuery(Of TaxKentTown)("GettbltaxKentTowns").ToList()
    End Function

    Public Function GetCompanyType() As IList(Of CompanyTypelst) 'Implements ISubmissionRepository.GetCompanyType
        Dim list As List(Of CompanyTypelst)
        list = db.CompanyTypeTable.ToList()
        Return list
    End Function

    Public Function GetProjectType() As IQueryable(Of ProjectTypelst) 'Implements ISubmissionRepository.GetProjectType
        Dim list As IQueryable(Of ProjectTypelst)
        list = db.ProjectTypeTable.OrderBy(Function(e) e.ProjectTypeID)
        Return list
    End Function


    Public Function GetSWBMPList() As IList(Of SWBMPlst)
        Return db.SWBMPTable.Where(Function(a) a.Active = "Y").OrderBy(Function(b) b.DisplayOrder).ToList()
    End Function

    Public Function GetStates() As IList(Of StateAbvlst) 'Implements ISubmissionRepository.GetStates
        Dim list As List(Of StateAbvlst)
        list = db.StateAbvTable.ToList()
        Return list
    End Function

    Public Function GetNOIPrograms() As IQueryable(Of NOIProgram)
        Return db.NOIProgram
    End Function

    Public Function GetSubmissionTypeList() As IQueryable(Of NOISubmissionTypelst) 'Implements ISubmissionRepository.GetSubmissionTypeList
        'Dim list As List(Of NOISubmissionTypelst)
        'list = db.NOISubmissionTypelst.ToList()
        Return db.NOISubmissionTypelst
    End Function

    Public Function GetNOIProgSubmissionTypes() As IQueryable(Of NOIProgSubmissionType)
        Return db.NOIProgSubmissionType
    End Function

    Public Function GetSubmissionStatusCodes() As IQueryable(Of NOISubmissionStatusCode)
        Return db.NOISubmissionStatusCodes
    End Function


    Public Function GetSubmissionStatusesBySubmissionID(subid As Integer) As IQueryable
        Dim query = db.NOISubmissionStatuses.Include(Function(b) b.NOISubmissionStatusCode).Where(Function(a) a.SubmissionID = subid).Select(Function(c) New With {c.SubmissionStatusDate, c.NOISubmissionStatusCode.SubmissionStatus, c.SubmissionStatusComment})
        Return query.AsQueryable()
    End Function


    ''verified
    Public Function GetSubmissionByIDForGeneralNOI(submissionid As Integer) As NOISubmission
        Dim ns As NOISubmission
        Try

            'db.Database.Log = Sub(val) Debug.Write(val)

            'ns = db.NOISubmissions.Include(Function(p) p.NOIProject.NOIProjectTaxParcels) _
            '    .Include(Function(q) q.NOIProject.NOIProjectSWBMP.Select(Function(c) c.SWBMPlst)) _
            '    .Include(Function(o) o.NOISubmissionPersonOrg) _
            '    .Include(Function(s) s.NOISubmissionStatus) _
            '    .Include(Function(b) b.NOIFeeExemption) _
            '    .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOILoc) _
            .Include(Function(c) c.NOISubmissionSW) _
            .Include(Function(t) t.NOISubmissionSWConstruct) _
            .Include(Function(r) r.NOISubmissionTaxParcels) _
            .Include(Function(q) q.NOISubmissionSWBMP.Select(Function(c) c.SWBMPlst)) _
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
            ns.NOISubmissionSWConstruct.EntityState = EntityState.Modified

            For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
                noipersonorg.EntityState = EntityState.Modified
            Next


            Return ns
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    ''verified
    Public Function GetSubmissionByIdForCoPermittee(submissionid As Integer) As NOISubmission
        Dim ns As NOISubmission

        'db.Database.Log = Sub(val) Debug.Write(val)

        ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(a) a.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid) ' And n.NOIProgSubmissionType.ProgramID = programid)

        Dim adrep As New AdminRepository
        adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrg(ns.NOIProject.PermitNumber, ns)


        ns.EntityState = EntityState.Modified
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOISubmissionSWConstruct.EntityState = EntityState.Unchanged
        For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            If noipersonorg.NOIPersonOrgTypeID = NOIPersonOrgType.CoPermitteeDetails Then
                noipersonorg.EntityState = EntityState.Modified
            Else
                noipersonorg.EntityState = EntityState.Unchanged
            End If
        Next
        For Each taxparcel As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            taxparcel.EntityState = EntityState.Unchanged
        Next

        For Each swbmp As NOISubmissionSWBMP In ns.NOISubmissionSWBMP
            swbmp.EntityState = EntityState.Unchanged
        Next

        ns.NOILoc = Nothing
        ns.NOISubmissionTaxParcels.Clear()
        ns.NOISubmissionSWBMP.Clear()
        Return ns
    End Function

    ''verified
    Public Function GetSubmissionByIDForCoPermitteeNOT(submissionid As Integer) As NOISubmission
        Dim ns As NOISubmission
        'ns = db.NOISubmissions.Include(Function(p) p.NOIProject.NOIProjectTaxParcels) _
        '    .Include(Function(q) q.NOIProject.NOIProjectSWBMP.Select(Function(c) c.SWBMPlst)) _
        '    .Include(Function(o) o.NOISubmissionPersonOrg) _
        '    .Include(Function(s) s.NOISubmissionStatus) _
        '    .Include(Function(b) b.NOIFeeExemption) _
        '    .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

        'ns.EntityState = EntityState.Modified
        'ns.NOIProject.EntityState = EntityState.Unchanged
        'For Each taxparcel As NOIProjectTaxParcels In ns.NOIProject.NOIProjectTaxParcels
        '    taxparcel.EntityState = EntityState.Unchanged
        'Next

        'For Each swbmp As NOIProjectSWBMP In ns.NOIProject.NOIProjectSWBMP
        '    swbmp.EntityState = EntityState.Unchanged
        'Next
        ns = db.NOISubmissions.Include(Function(r) r.NOIProject) _
            .Include(Function(c) c.NOISubmissionSW) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(a) a.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid) ' And n.NOIProgSubmissionType.ProgramID = programid)


        Dim adrep As New AdminRepository
        adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrg(ns.NOIProject.PermitNumber, ns)

        ns.EntityState = EntityState.Modified
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOISubmissionSW.EntityState = EntityState.Modified
        ns.NOISubmissionSWConstruct.EntityState = EntityState.Unchanged
        For Each taxparcel As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            taxparcel.EntityState = EntityState.Unchanged
        Next

        For Each swbmp As NOISubmissionSWBMP In ns.NOISubmissionSWBMP
            swbmp.EntityState = EntityState.Unchanged
        Next

        For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noipersonorg.EntityState = EntityState.Unchanged
        Next

        ns.NOILoc = Nothing
        Return ns
    End Function



    Public Function GetSubmissionByIDForNOT(submissionid As Integer) As NOISubmission
        Dim ns As NOISubmission
        'ns = db.NOISubmissions.Include(Function(p) p.NOIProject.NOIProjectTaxParcels) _
        '    .Include(Function(q) q.NOIProject.NOIProjectSWBMP.Select(Function(c) c.SWBMPlst)) _
        '    .Include(Function(o) o.NOISubmissionPersonOrg) _
        '    .Include(Function(s) s.NOISubmissionStatus) _
        '    .Include(Function(b) b.NOIFeeExemption) _
        '    .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

        'ns.EntityState = EntityState.Modified
        'ns.NOIProject.EntityState = EntityState.Unchanged
        'For Each taxparcel As NOIProjectTaxParcels In ns.NOIProject.NOIProjectTaxParcels
        '    taxparcel.EntityState = EntityState.Unchanged
        'Next

        'For Each swbmp As NOIProjectSWBMP In ns.NOIProject.NOIProjectSWBMP
        '    swbmp.EntityState = EntityState.Unchanged
        'Next
        ns = db.NOISubmissions.Include(Function(r) r.NOIProject) _
            .Include(Function(c) c.NOISubmissionSW) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(a) a.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid) ' And n.NOIProgSubmissionType.ProgramID = programid)


        Dim adrep As New AdminRepository
        adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrg(ns.NOIProject.PermitNumber, ns)

        ns.EntityState = EntityState.Modified
        ns.NOIProject.EntityState = EntityState.Unchanged
        ns.NOISubmissionSW.EntityState = EntityState.Modified
        ns.NOISubmissionSWConstruct.EntityState = EntityState.Unchanged
        For Each taxparcel As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            taxparcel.EntityState = EntityState.Unchanged
        Next

        For Each swbmp As NOISubmissionSWBMP In ns.NOISubmissionSWBMP
            swbmp.EntityState = EntityState.Unchanged
        Next

        For Each noipersonorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noipersonorg.EntityState = EntityState.Unchanged
        Next



        Return ns
    End Function

    ''verified
    Public Function GetSubmissionByIDForDisplay(submissionid As Integer, progsubtype As Integer) As NOISubmission
        Dim ns As NOISubmission

        'db.Database.Log = Sub(val) Debug.Write(val)
        Dim subtype As NOISubmissionType

        subtype = CType(db.NOIProgSubmissionType.Single(Function(a) a.ProgSubmissionTypeID = progsubtype).SubmissionTypeID, NOISubmissionType)

        If subtype = NOISubmissionType.GeneralNOIPermit Then
            ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
            .Include(Function(d) d.NOILoc) _
            .Include(Function(c) c.NOISubmissionSW) _
            .Include(Function(r) r.NOISubmissionSWConstruct) _
            .Include(Function(t) t.NOISubmissionTaxParcels) _
            .Include(Function(q) q.NOISubmissionSWBMP.Select(Function(c) c.SWBMPlst)) _
            .Include(Function(o) o.NOISubmissionPersonOrg) _
            .Include(Function(s) s.NOISubmissionStatus) _
            .Include(Function(b) b.NOIFeeExemption) _
            .Include(Function(e) e.NOIProgSubmissionType) _
            .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

            Return ns
        Else
            Dim adrep As New AdminRepository
            If subtype = NOISubmissionType.CoPermittee Then

                ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
                .Include(Function(o) o.NOISubmissionPersonOrg) _
                .Include(Function(s) s.NOISubmissionStatus) _
                .Include(Function(b) b.NOIFeeExemption) _
                .Include(Function(e) e.NOIProgSubmissionType) _
                .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)


                adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrg(ns.NOIProject.PermitNumber, ns)
                ns.NOISubmissionSWConstruct = Nothing
                ns.NOISubmissionTaxParcels.Clear()
                ns.NOILoc = Nothing
                ns.NOISubmissionSWBMP = Nothing

            Else
                ns = db.NOISubmissions.Include(Function(p) p.NOIProject) _
                .Include(Function(c) c.NOISubmissionSW) _
                .Include(Function(o) o.NOISubmissionPersonOrg) _
                .Include(Function(s) s.NOISubmissionStatus) _
                .Include(Function(b) b.NOIFeeExemption) _
                .Include(Function(e) e.NOIProgSubmissionType) _
                .Include(Function(a) a.NOISigningEmailAddress).AsNoTracking().Single(Function(n) n.SubmissionID = submissionid)

                adrep.GetNOIProjectDetailsByPermitNumberWithoutPersonOrg(ns.NOIProject.PermitNumber, ns)
                ns.NOISubmissionSWConstruct = Nothing
                ns.NOISubmissionTaxParcels.Clear()
                ns.NOILoc = Nothing
                ns.NOISubmissionSWBMP = Nothing
            End If



            Return ns

        End If



    End Function

    ''for testing purpose. could be deleted
    Private Sub displayentityinfo(ns As NOISubmission)
        Debug.WriteLine("{0} | {1} | {2}", ns.GetHashCode(), ns.SubmissionID, "noisubmission")
        Debug.WriteLine("{0} | {1} | {2}", ns.NOIProject.GetHashCode(), ns.NOIProject.ProjectID, "noiproject")

        If ns.NOISubmissionSWConstruct IsNot Nothing Then
            Debug.WriteLine("{0} | {1} | {2}", ns.NOISubmissionSWConstruct.GetHashCode(), ns.NOISubmissionSWConstruct.SubmissionID, "NOISubmissionSWConstruct")
        End If

        Dim count As Integer = 1
        For Each n As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            Debug.WriteLine("{0} | {1} | {2}", n.GetHashCode(), n.SubmissionID, "NOISubmissionPersonOrg" & count.ToString())
            count += 1
        Next
        count = 1
        For Each n As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            Debug.WriteLine("{0} | {1} | {2}", n.GetHashCode(), ns.SubmissionID, "NOISubmissionTaxParcels" & count.ToString())
            count += 1
        Next
        count = 1
        For Each n As NOISubmissionSWBMP In ns.NOISubmissionSWBMP
            Debug.WriteLine("{0} | {1} | {2}", n.GetHashCode(), ns.SubmissionID, "NOISubmissionSWBMP" & count.ToString())
        Next


    End Sub


    Public Function GetProjectOwnerDetailsByProjectIDForCoPermittee(projectid As Integer) As NOISubmission

        Dim ns As NOISubmission = Nothing
        Try

            db.Database.Log = Sub(val) Debug.Write(val)
            ns = db.NOISubmissions.Create()

            Dim query = db.NOIProjects.Where(Function(a) a.ProjectID = projectid)


            Dim result = query.Single()
            ns.NOIProject = result

            Dim adrep As New AdminRepository
            adrep.GetNOIProjectDetailsByPermitNumber(result.PermitNumber, ns)
            'Dim noiswconstruct As NOISubmissionSWConstruct = adrep.GetNOISWConstructDetails(result.PermitNumber)

            'ns.NOISubmissionSWConstruct = noiswconstruct

            'Dim noireceiveddate As Date
            'Dim noipersonorg As List(Of NOISubmissionPersonOrg) = adrep.GetNOIOwnerAndReceivedDate(result.PermitNumber, noireceiveddate)

            ''ns.NOISubmissionPersonOrg.Add(noipersonorg)
            'For Each noiperson As NOISubmissionPersonOrg In noipersonorg
            '    ns.NOISubmissionPersonOrg.Add(noiperson)
            'Next
            'ns.NOIReceivedDate = noireceiveddate

            ns.EntityState = EntityState.Added
            ns.NOIProject.EntityState = EntityState.Unchanged

            ns.NOISubmissionSWConstruct.EntityState = EntityState.Unchanged
            For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
                noiperorg.EntityState = EntityState.Added
            Next

            ns.NOILoc = Nothing
            ns.NOISubmissionTaxParcels.Clear()
            ns.NOISubmissionSWBMP.Clear()

        Catch ex As Exception
            Throw ex
        End Try
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
            adrep.GetNOIProjectDetailsByPermitNumber(result.PermitNumber, ns)

            'ns.EntityState = EntityState.Added
            'ns.NOIProject.EntityState = EntityState.Unchanged
            'ns.NOILoc.EntityState = EntityState.Unchanged
            'ns.NOISubmissionSWConstruct.EntityState = EntityState.Unchanged
            'For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            '    noiperorg.EntityState = EntityState.Added
            'Next
            'For Each noitp As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            '    noitp.EntityState = EntityState.Unchanged
            'Next
            'ns.NOISubmissionSWBMP.Clear()


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
        ns.NOISubmissionSWConstruct.EntityState = EntityState.Unchanged
        For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
            noiperorg.EntityState = EntityState.Added
        Next
        For Each noitp As NOISubmissionTaxParcels In ns.NOISubmissionTaxParcels
            noitp.EntityState = EntityState.Unchanged
        Next
        ns.NOISubmissionSWBMP.Clear()

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
        For Each noibmp As NOISubmissionSWBMP In ns.NOISubmissionSWBMP
            noibmp.EntityState = EntityState.Added
        Next
        ns.NOIReceivedDate = Nothing

        Return ns
    End Function


    ''verified
    Public Function GetProjectCopermitteeDetailsByProjectID(permitnumber As String, AfflID As Int32) As NOISubmission
        Dim ns As NOISubmission
        Try


            db.Database.Log = Sub(val) Debug.Write(val)
            ns = db.NOISubmissions.Create()

            Dim query = db.NOIProjects.Where(Function(a) a.PermitNumber = permitnumber)


            Dim result = query.Single()

            ns.NOIProject = result

            Dim adrep As New AdminRepository
            adrep.GetCoPermitteeAndReceivedDate(permitnumber, AfflID, ns)


            ns.EntityState = EntityState.Added
            ns.NOIProject.EntityState = EntityState.Unchanged
            ns.NOISubmissionSWConstruct.EntityState = EntityState.Unchanged
            For Each noiperorg As NOISubmissionPersonOrg In ns.NOISubmissionPersonOrg
                noiperorg.EntityState = EntityState.Added
            Next
            ns.NOISubmissionTaxParcels.Clear()
            ns.NOISubmissionSWBMP.Clear()


            Return ns
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetSubmissionTypeDetails(submissionTypeID As NOISubmissionType) As NOISubmissionTypelst
        Return db.NOISubmissionTypelst.Where(Function(a) a.SubmissionTypeID = submissionTypeID).Single()
    End Function


    'Public Function GetSessionStorageByUser(user As String) As String 'Implements ISubmissionRepository.GetSessionStorageByUser
    '    db.Database.Log = Sub(val) Debug.Write(val)
    '    Dim d As NOISessionStorage = db.NOISessionStorage.Where(Function(e) e.username = user).AsNoTracking().SingleOrDefault()
    '    If d IsNot Nothing Then
    '        Return d.SessionStorageJSON
    '    Else
    '        Return String.Empty
    '    End If
    'End Function

    'Public Sub SetSessionStorageByUser(user As String, sessionstorage As String) 'Implements ISubmissionRepository.SetSessionStorageByUser
    '    db.Database.Log = Sub(val) Debug.Write(val)
    '    Dim d As NOISessionStorage = db.NOISessionStorage.Where(Function(e) e.username = user).SingleOrDefault()
    '    If d IsNot Nothing Then
    '        db.NOISessionStorage.Attach(d)
    '        d.SessionStorageJSON = sessionstorage
    '    Else
    '        d = New NOISessionStorage()
    '        d.username = user
    '        d.SessionStorageJSON = sessionstorage
    '        db.NOISessionStorage.Add(d)
    '    End If
    '    db.SaveChanges()
    'End Sub


    'Public Sub RemoveSessionStorageByUser(user As String)

    '    Dim d As NOISessionStorage = db.NOISessionStorage.Where(Function(e) e.username = user).SingleOrDefault()
    '    If d IsNot Nothing Then
    '        db.NOISessionStorage.Remove(d)
    '        db.SaveChanges()
    '    End If

    'End Sub

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

    Public Sub InsertSubmissionStatus(submissionstatus As NOISubmissionStatus)
        db.NOISubmissionStatuses.Add(submissionstatus)
        db.SaveChanges()
    End Sub

    Public Sub DeleteExemptionCode(ExemptionID As Integer)
        Dim exemptioncode As NOIFeeExemption = db.NOIFeeExemptions.Where(Function(a) a.ExemptionID = ExemptionID).Single()
        exemptioncode.EntityState = EntityState.Deleted
        SaveExemptionCode(exemptioncode)
    End Sub

    Public Sub SaveExemptionCode(ExemptionCode As NOIFeeExemption)
        db.NOIFeeExemptions.Add(ExemptionCode)
        SaveChanges()
    End Sub

    ''verified
    Public Function Insert(submission As NOISubmission) As NOISubmission 'Implements ISubmissionRepository.Insert
        'db.NOISubmissions.Add(submission)
        'db.SaveChanges()
        Dim ns As NOISubmission
        ns = Update(submission)
        'If submission.SubmissionID <> 0 Then
        '    Dim ns As NOISubmission
        '    ns = db.NOISubmissions.Include(Function(p) p.NOIProject.NOIProjectTaxParcels) _
        '    .Include(Function(o) o.NOISubmissionPersonOrg) _
        '    .Include(Function(s) s.NOISubmissionStatus).Single(Function(n) n.SubmissionID = submission.SubmissionID)
        '    If ns IsNot Nothing Then
        '        Dim attachedentry As DbEntityEntry = db.Entry(ns)
        '        attachedentry.CurrentValues.SetValues(submission)
        '    End If
        'Else
        '    db.NOISubmissions.Add(submission)
        'End If
        'db.SaveChanges()
        Return ns
    End Function

    ''verified
    Public Function Update(submission As NOISubmission) As NOISubmission 'Implements ISubmissionRepository.Update

        db.NOISubmissions.Add(submission)
        'For Each entry As DbEntityEntry(Of IEntity) In db.ChangeTracker.Entries(Of IEntity)()
        '    Dim entity As IEntity = entry.Entity
        '    entry.State = GetEntityState(entity.EntityState)
        'Next
        Try
            'db.Database.Log = Sub(val) Debug.Write(val)
            'db.SaveChanges()

            SaveChanges()

            db.Entry(submission).State = Entity.EntityState.Detached

            Dim ss As NOISubmission = GetSubmissionByIDForDisplay(submission.SubmissionID, submission.ProgSubmissionTypeID)  'db.NOISubmissions.Where(Function(a) a.SubmissionID = submission.SubmissionID).Single()
            Return ss
        Catch ex As Exception
            LogError(String.Empty, String.Empty, ex.Source, common.getError(ex), submission.SubmissionID, common.getErrorStackTrace(ex))
            Throw ex
        End Try

    End Function

    ''verified
    Private Sub SaveChanges()
        Try


            'For Each entry As DbEntityEntry(Of IEntity) In db.ChangeTracker.Entries(Of IEntity)()
            '    Dim entity As IEntity = entry.Entity
            '    entry.State = GetEntityState(entity.EntityState)
            'Next

            common.ApplyStateChanges(db)
            db.Database.Log = Sub(val) Debug.Write(val)
            db.SaveChanges()
        Catch ex As Exception
            LogError(String.Empty, String.Empty, ex.Source, common.getError(ex), -99, common.getErrorStackTrace(ex))
            Throw ex
        End Try
    End Sub


    'Private Function GetEntityState(ByVal entityState As EntityState) As System.Data.Entity.EntityState
    '    Select Case entityState
    '        Case NoticeOfIntent.EntityState.Added
    '            Return System.Data.Entity.EntityState.Added
    '        Case NoticeOfIntent.EntityState.Modified
    '            Return System.Data.Entity.EntityState.Modified
    '        Case NoticeOfIntent.EntityState.Deleted
    '            Return System.Data.Entity.EntityState.Deleted
    '        Case NoticeOfIntent.EntityState.Unchanged
    '            Return System.Data.Entity.EntityState.Unchanged
    '        Case Else
    '            Return System.Data.Entity.EntityState.Detached
    '    End Select

    'End Function






#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        'GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Public Class MyLogger

    Public Shared Sub Log(component As String, message As String)
        Console.WriteLine("Component: {0} Message: {1} ", component, message)
    End Sub
End Class
