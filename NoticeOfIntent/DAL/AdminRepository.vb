Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Entity.Core.Objects
Imports System.Threading.Tasks

Public Class AdminRepository
    Implements IDisposable

    Private db As EISSQLDB
    Public Sub New()
        If Convert.ToBoolean(ConfigurationManager.AppSettings("InDMZ")) = True Then
            db = New EISSQLDB(ConfigurationManager.ConnectionStrings("EISSQLExternalDB").ToString())   'EISSQLExternalDB
        Else
            db = New EISSQLDB(ConfigurationManager.ConnectionStrings("EISSQLInternalDB").ToString())   'TODO
        End If
    End Sub


    Public Function GetCityStateAbvByZip(zip As String) As Zipcodes
        Return db.Zipcodes.Where(Function(a) a.ZIP5 = zip).Distinct.FirstOrDefault
    End Function

    Public Function GetZipCodes() As IList(Of Zipcodes)
        Return db.Zipcodes.ToList()
    End Function


    Public Function AcceptGeneralNOI(logInVS As LogInDetails) As String
        Try


            Dim psubid As SqlParameter = New SqlParameter("@submissionid", SqlDbType.Int)
            psubid.Value = logInVS.submissionid
            Dim puserid As SqlParameter = New SqlParameter("@userid", SqlDbType.VarChar, 64)
            puserid.Value = logInVS.user.userid
            Dim ppnumber As SqlParameter = New SqlParameter("@permitnumber", SqlDbType.VarChar, 20)
            ppnumber.Value = String.Empty
            ppnumber.Direction = ParameterDirection.Output

            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportGeneralNOI @submissionid,@userid,@permitnumber out", psubid, puserid, ppnumber)
            ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportISWGeneralNOI @submissionid,@userid,@permitnumber out", psubid, puserid, ppnumber)
            ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportAPGeneralNOI @submissionid,@userid,@permitnumber out", psubid, puserid, ppnumber)
            End If

            Return ppnumber.Value
        Catch sqlex As SqlException
            Throw sqlex
        Catch ex As Exception
            Throw ex
        End Try


    End Function


    Public Function AcceptCoPermitteeNOI(logInVS As LogInDetails) As String
        Try


            Dim psubid As SqlParameter = New SqlParameter("@submissionid", SqlDbType.Int)
            psubid.Value = logInVS.submissionid
            Dim puserid As SqlParameter = New SqlParameter("@userid", SqlDbType.VarChar, 64)
            puserid.Value = logInVS.user.userid
            Dim isCompleted As SqlParameter = New SqlParameter("@iscompleted", SqlDbType.Char, 1)
            isCompleted.Direction = ParameterDirection.Output

            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportCoPermitteeNOI @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)


            Return isCompleted.Value
        Catch ex As Exception
            Throw ex
        End Try


    End Function


    Public Function AcceptCoPermitteeNOT(logInVS As LogInDetails) As String
        Try


            Dim psubid As SqlParameter = New SqlParameter("@submissionid", SqlDbType.Int)
            psubid.Value = logInVS.submissionid
            Dim puserid As SqlParameter = New SqlParameter("@userid", SqlDbType.VarChar, 64)
            puserid.Value = logInVS.user.userid
            Dim isCompleted As SqlParameter = New SqlParameter("@iscompleted", SqlDbType.Char, 1)
            isCompleted.Direction = ParameterDirection.Output

            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportCoPermitteeNOT @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)


            Return isCompleted.Value
        Catch ex As Exception
            Throw ex
        End Try


    End Function


    Public Function AcceptGeneralNOT(logInVS As LogInDetails) As String
        Try


            Dim psubid As SqlParameter = New SqlParameter("@submissionid", SqlDbType.Int)
            psubid.Value = logInVS.submissionid
            Dim puserid As SqlParameter = New SqlParameter("@userid", SqlDbType.VarChar, 64)
            puserid.Value = logInVS.user.userid
            Dim isCompleted As SqlParameter = New SqlParameter("@iscompleted", SqlDbType.Char, 1)
            isCompleted.Direction = ParameterDirection.Output

            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportGeneralNOT @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportISWGeneralNOT @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportAPGeneralNOT @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            End If


            Return isCompleted.Value
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Function AcceptModifiedGeneralNOI(logInVS As LogInDetails) As String
        Try


            Dim psubid As SqlParameter = New SqlParameter("@submissionid", SqlDbType.Int)
            psubid.Value = logInVS.submissionid
            Dim puserid As SqlParameter = New SqlParameter("@userid", SqlDbType.VarChar, 64)
            puserid.Value = logInVS.user.userid
            Dim isCompleted As SqlParameter = New SqlParameter("@iscompleted", SqlDbType.Char, 1)
            isCompleted.Direction = ParameterDirection.Output

            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                'db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportGeneralNOI @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                'db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportISWGeneralNOI @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportAPModifiedGeneralNOI @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            End If


            Return isCompleted.Value
        Catch sqlex As SqlException
            Throw sqlex
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Function AcceptRenewalGeneralNOI(logInVS As LogInDetails) As String
        Try


            Dim psubid As SqlParameter = New SqlParameter("@submissionid", SqlDbType.Int)
            psubid.Value = logInVS.submissionid
            Dim puserid As SqlParameter = New SqlParameter("@userid", SqlDbType.VarChar, 64)
            puserid.Value = logInVS.user.userid
            Dim isCompleted As SqlParameter = New SqlParameter("@iscompleted", SqlDbType.Char, 1)
            isCompleted.Direction = ParameterDirection.Output

            If logInVS.reportid = NOIProgramType.CSSGeneralPermit Then
                'db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportGeneralNOI @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            ElseIf logInVS.reportid = NOIProgramType.ISGeneralPermit Then
                'db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportISWGeneralNOI @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            ElseIf logInVS.reportid = NOIProgramType.PesticideGeneralPermit Then
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ImportAPRenewalGeneralNOI @submissionid,@userid,@iscompleted out", psubid, puserid, isCompleted)
            End If


            Return isCompleted.Value
        Catch sqlex As SqlException
            Throw sqlex
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Function GetInternalProjectList(projectname As String) As List(Of NOIProjectInternal)
        Dim noiprojectlst As List(Of NOIProjectInternal)
        noiprojectlst = db.Database.SqlQuery(Of NOIProjectInternal)("spNOI_GetNOIProjectList @projectname", New SqlParameter("@projectname", projectname)).ToList()
        Return noiprojectlst
    End Function

    Public Function GetInternalProjectList(projectname As String, reportid As Integer) As List(Of NOIProjectInternal)
        Dim noiprojectlst As List(Of NOIProjectInternal) = Nothing
        If reportid = NOIProgramType.CSSGeneralPermit Then
            noiprojectlst = db.Database.SqlQuery(Of NOIProjectInternal)("spNOI_GetNOIProjectList @projectname", New SqlParameter("@projectname", projectname)).ToList()
        ElseIf reportid = NOIProgramType.ISGeneralPermit Then
            noiprojectlst = db.Database.SqlQuery(Of NOIProjectInternal)("spNOI_GetNOIProjectListIS @projectname", New SqlParameter("@projectname", projectname)).ToList() ' sp needs to be implemented.
        ElseIf reportid = NOIProgramType.PesticideGeneralPermit Then
            noiprojectlst = db.Database.SqlQuery(Of NOIProjectInternal)("spNOI_GetNOIProjectListAP @projectname", New SqlParameter("@projectname", projectname)).ToList()
        End If

        Return noiprojectlst
    End Function


    Public Function GetPermitStatusCodeLst() As List(Of PermitStatusCodeLst)
        Dim permitstatuscodelst As List(Of PermitStatusCodeLst)
        permitstatuscodelst = db.Database.SqlQuery(Of PermitStatusCodeLst)("dbo.spNOIPEventCode_GetRS").ToList()
        Return permitstatuscodelst
    End Function

    ''verified
    Public Function GetInternalProjectListByName(projectname As String, pitypeid As Integer) As List(Of String)
        Dim pprojectname As New SqlParameter("@piname", SqlDbType.VarChar, 80)
        pprojectname.Value = projectname
        Dim ppitypeid As New SqlParameter("@pitypeid", SqlDbType.Int)
        ppitypeid.Value = pitypeid

        Dim noiprojectlst As List(Of String)
        noiprojectlst = db.Database.SqlQuery(Of String)("spNOI_GetProjectByPiName @piname,@pitypeid", pprojectname, ppitypeid).ToList()
        Return noiprojectlst
    End Function


    Public Function ImportProjectFromDEN(permitnumber As String, programid As Integer, user As String) As String
        Try
            Dim ppermitnumber As SqlParameter = New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
            ppermitnumber.Value = permitnumber
            Dim pprogramid As SqlParameter = New SqlParameter("@ProgramID", SqlDbType.Int)
            pprogramid.Value = programid
            Dim puserid As SqlParameter = New SqlParameter("@userid", SqlDbType.VarChar, 64)
            puserid.Value = user
            Dim pprojectnumber As SqlParameter = New SqlParameter("@Projectnumber", SqlDbType.VarChar, 33)
            pprojectnumber.Direction = ParameterDirection.Output


            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec spNOI_ExportNOIProjectToNOIDB @Permitnumber,@ProgramID,@userid,@Projectnumber out", ppermitnumber, pprogramid, puserid, pprojectnumber)

            Return pprojectnumber.Value

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''verified
    Public Sub GetNOIProjectDetailsByPermitNumber(ByVal permitnumber As String, ByRef submission As NOISubmission)

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand
        cmd.CommandText = "spNOI_GetProjectDetailsByPermitNumber"
        Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = permitnumber
        cmd.Parameters.Add(ppermitnumber)
        cmd.CommandType = CommandType.StoredProcedure
        Dim octx As ObjectContext
        octx = CType(db, IObjectContextAdapter).ObjectContext()

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ''Read NOILoc
            Dim noiloc As NOILoc = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOILoc)(reader).SingleOrDefault()
            submission.NOILoc = noiloc

            reader.NextResult()

            ''Read NOISubmissionSWConstruct
            Dim noiswconstruct As NOISubmissionSWConstruct = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionSWConstruct)(reader).SingleOrDefault()
            submission.NOISubmissionSWConstruct = noiswconstruct
            'octx.Detach(noiswconstruct)

            reader.NextResult()

            Dim noipersonorg As List(Of NOISubmissionPersonOrg) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionPersonOrg)(reader).ToList()
            submission.NOISubmissionPersonOrg = noipersonorg
            'For Each n As NOISubmissionPersonOrg In noipersonorg
            '    octx.Detach(n)
            'Next
            reader.NextResult()

            Dim noitaxparcel As List(Of NOISubmissionTaxParcels) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionTaxParcels)(reader).ToList()
            submission.NOISubmissionTaxParcels = noitaxparcel
            'For Each n As NOISubmissionTaxParcels In noitaxparcel
            '    octx.Detach(n)
            'Next
            reader.NextResult()

            Dim noiswbmp As List(Of NOISubmissionSWBMP) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionSWBMP)(reader).ToList()
            submission.NOISubmissionSWBMP = noiswbmp
            'For Each n As NOISubmissionSWBMP In noiswbmp
            '    octx.Detach(n)
            'Next
            reader.NextResult()

            If reader.HasRows() Then
                While reader.Read
                    submission.NOIReceivedDate = reader("NOIReceivedDate")
                End While
            End If

            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Sub

    ''verified
    Public Sub GetNOIProjectDetailsByPermitNumberWithoutPersonOrg(ByVal permitnumber As String, ByRef submission As NOISubmission)
        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand
        cmd.CommandText = "spNOI_GetProjectDetailsByPermitNumber"
        Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = permitnumber
        cmd.Parameters.Add(ppermitnumber)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ''Read NOILoc
            Dim noiloc As NOILoc = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOILoc)(reader).SingleOrDefault()
            submission.NOILoc = noiloc


            reader.NextResult()

            ''Read NOISubmissionSWConstruct
            Dim noiswconstruct As NOISubmissionSWConstruct = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionSWConstruct)(reader).SingleOrDefault()
            submission.NOISubmissionSWConstruct = noiswconstruct

            reader.NextResult()

            'Dim noipersonorg As List(Of NOISubmissionPersonOrg) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionPersonOrg)(reader).ToList()
            ''not using NOISubmissionPersonOrg from EISQL as the submission already has these details.

            reader.NextResult()

            Dim noitaxparcel As List(Of NOISubmissionTaxParcels) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionTaxParcels)(reader).ToList()
            submission.NOISubmissionTaxParcels = noitaxparcel

            reader.NextResult()

            Dim noiswbmp As List(Of NOISubmissionSWBMP) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionSWBMP)(reader).ToList()
            submission.NOISubmissionSWBMP = noiswbmp

            reader.NextResult()

            'submission.NOIReceivedDate = reader("NOIReceiveDate")
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Public Sub GetNOIProjectDetailsByPermitNumberForIS(ByVal permitnumber As String, ByRef submission As NOISubmission)

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand
        cmd.CommandText = "spNOI_GetProjectDetailsByPermitNumberForIS"
        Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = permitnumber
        cmd.Parameters.Add(ppermitnumber)

        cmd.CommandType = CommandType.StoredProcedure
        Dim octx As ObjectContext
        octx = CType(db, IObjectContextAdapter).ObjectContext()

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ''Read NOILoc
            Dim noiloc As NOILoc = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOILoc)(reader).SingleOrDefault()
            submission.NOILoc = noiloc

            reader.NextResult()

            Dim noipersonorg As List(Of NOISubmissionPersonOrg) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionPersonOrg)(reader).ToList()
            submission.NOISubmissionPersonOrg = noipersonorg

            reader.NextResult()

            Dim noitaxparcel As List(Of NOISubmissionTaxParcels) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionTaxParcels)(reader).ToList()
            submission.NOISubmissionTaxParcels = noitaxparcel

            reader.NextResult()

            If reader.HasRows() Then
                While reader.Read
                    submission.NOIReceivedDate = reader("NOIReceivedDate")
                End While
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub GetNOIProjectDetailsByPermitNumberWithoutPersonOrgForIS(ByVal permitnumber As String, ByRef submission As NOISubmission)

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand
        cmd.CommandText = "spNOI_GetProjectDetailsByPermitNumberForIS"
        Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = permitnumber
        cmd.Parameters.Add(ppermitnumber)
        cmd.CommandType = CommandType.StoredProcedure
        Dim octx As ObjectContext
        octx = CType(db, IObjectContextAdapter).ObjectContext()

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ''Read NOILoc
            Dim noiloc As NOILoc = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOILoc)(reader).SingleOrDefault()
            submission.NOILoc = noiloc

            reader.NextResult()

            'Dim noipersonorg As List(Of NOISubmissionPersonOrg) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionPersonOrg)(reader).ToList()
            'submission.NOISubmissionPersonOrg = noipersonorg

            reader.NextResult()

            Dim noitaxparcel As List(Of NOISubmissionTaxParcels) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionTaxParcels)(reader).ToList()
            submission.NOISubmissionTaxParcels = noitaxparcel

            reader.NextResult()

            If reader.HasRows() Then
                While reader.Read
                    submission.NOIReceivedDate = reader("NOIReceivedDate")
                End While
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub GetNOIProjectDetailsByPermitNumberForAP(ByVal permitnumber As String, ByRef submission As NOISubmission)

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand
        cmd.CommandText = "spNOI_GetProjectDetailsByPermitNumberForAP"
        Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = permitnumber
        cmd.Parameters.Add(ppermitnumber)
        cmd.CommandType = CommandType.StoredProcedure
        Dim octx As ObjectContext
        octx = CType(db, IObjectContextAdapter).ObjectContext()

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            Dim noiAP As NOISubmissionAP = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionAP)(reader).SingleOrDefault()
            submission.NOISubmissionAP = noiAP
            reader.NextResult()

            Dim noiAPChemicals As List(Of NOISubmissionAPChemicals) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionAPChemicals)(reader).ToList()
            submission.NOISubmissionAPChemicals = noiAPChemicals
            reader.NextResult()

            Dim noipersonorg As List(Of NOISubmissionPersonOrg) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionPersonOrg)(reader).ToList()
            submission.NOISubmissionPersonOrg = noipersonorg

            reader.NextResult()

            If reader.HasRows() Then
                While reader.Read
                    submission.NOIReceivedDate = reader("NOIReceivedDate")
                End While
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub GetNOIProjectDetailsByPermitNumberWithoutPersonOrgForAP(ByVal permitnumber As String, ByRef submission As NOISubmission)

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand
        cmd.CommandText = "spNOI_GetProjectDetailsByPermitNumberForAP"
        Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = permitnumber
        cmd.Parameters.Add(ppermitnumber)
        cmd.CommandType = CommandType.StoredProcedure
        Dim octx As ObjectContext
        octx = CType(db, IObjectContextAdapter).ObjectContext()

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            reader.NextResult()

            reader.NextResult()

            'Dim noipersonorg As List(Of NOISubmissionPersonOrg) = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOISubmissionPersonOrg)(reader).ToList()
            'submission.NOISubmissionPersonOrg = noipersonorg

            reader.NextResult()

            If reader.HasRows() Then
                While reader.Read
                    submission.NOIReceivedDate = reader("NOIReceivedDate")
                End While
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Function GetNOIOwnerAndReceivedDate(permitnumber As String, ByRef NOIReceivedDate As Date) As List(Of NOISubmissionPersonOrg)

        Dim noipersonorg As List(Of NOISubmissionPersonOrg) = Nothing
        Try


            Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
            ppermitnumber.Value = permitnumber
            Dim noireceivedate As New SqlParameter("@NOIReceivedDate", SqlDbType.Date)
            noireceivedate.Direction = ParameterDirection.Output

            Dim results = db.Database.SqlQuery(Of NOISubmissionPersonOrg)("spNOI_GetNOIProjectOwnerAndReceiveDate @Permitnumber,@NOIReceivedDate out", ppermitnumber, noireceivedate)
            noipersonorg = results.ToList()
            If noipersonorg Is Nothing Then
                Throw New Exception("The given Permitnumber " + permitnumber.ToString() + " doesn't exist")
            End If
            NOIReceivedDate = noireceivedate.Value

            Return noipersonorg

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Sub GetCoPermitteeAndReceivedDate(permitnumber As String, afflid As Int32, ByRef submission As NOISubmission)

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand
        cmd.CommandText = "spNOI_GetProjectCoPermitteeAndReceiveDates"
        Dim ppermitnumber As New SqlParameter("@Permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = permitnumber
        cmd.Parameters.Add(ppermitnumber)
        Dim pafflid As New SqlParameter("@AfflID", SqlDbType.Int)
        pafflid.Value = afflid
        cmd.Parameters.Add(pafflid)
        cmd.CommandType = CommandType.StoredProcedure
        Dim octx As ObjectContext
        octx = CType(db, IObjectContextAdapter).ObjectContext()
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ''Read NOILoc
            Dim noiloc As NOILoc = octx.Translate(Of NOILoc)(reader).SingleOrDefault()
            submission.NOILoc = noiloc


            reader.NextResult()

            ''Read NOISubmissionSWConstruct
            Dim noiswconstruct As NOISubmissionSWConstruct = octx.Translate(Of NOISubmissionSWConstruct)(reader).SingleOrDefault()
            submission.NOISubmissionSWConstruct = noiswconstruct


            reader.NextResult()

            Dim noipersonorg As List(Of NOISubmissionPersonOrg) = octx.Translate(Of NOISubmissionPersonOrg)(reader).ToList()
            submission.NOISubmissionPersonOrg = noipersonorg

            reader.NextResult()

            Dim noitaxparcel As List(Of NOISubmissionTaxParcels) = octx.Translate(Of NOISubmissionTaxParcels)(reader).ToList()
            submission.NOISubmissionTaxParcels = noitaxparcel


            reader.NextResult()

            Dim noiswbmp As List(Of NOISubmissionSWBMP) = octx.Translate(Of NOISubmissionSWBMP)(reader).ToList()
            submission.NOISubmissionSWBMP = noiswbmp

            reader.NextResult()

            If reader.HasRows() Then
                While reader.Read
                    submission.NOIReceivedDate = reader("NOIReceivedDate")
                End While
            End If

            reader.NextResult()

            If reader.HasRows() Then
                While reader.Read
                    submission.CopermitteeReceivedDate = reader("CoPermitteeReceivedDate")
                End While
            End If

            submission.NOILoc = Nothing
        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try


    End Sub

    ''verified
    Public Function GetNOIAndCoPermitteeDetails(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedprojectnumbers As String) As IQueryable(Of ProjectOwnerView)

        Dim pprojectnumber As New SqlParameter("@projectnumber", SqlDbType.VarChar, 32)
        pprojectnumber.Value = common.nullIfBlank(ProjectNumber)
        Dim pprojectname As New SqlParameter("@projectname", SqlDbType.VarChar, 80)
        pprojectname.Value = common.nullIfBlank(ProjectName)
        Dim ppermitnumber As New SqlParameter("@permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = common.nullIfBlank(PermitNumber)
        Dim pprojectnumbers As New SqlParameter("@projectnumbers", SqlDbType.VarChar, 1000)
        pprojectnumbers.Value = common.nullIfBlank(commadelimitedprojectnumbers)

        Dim results = db.Database.SqlQuery(Of ProjectOwnerView)("spNOI_GetProjectAndCoPermitteeDetails @projectnumber,@projectname,@permitnumber,@projectnumbers", pprojectnumber, pprojectname, ppermitnumber, pprojectnumbers)
        Return results.ToList().AsQueryable()
    End Function


    Public Function GetNOIByUser(ProjectNumber As String, ProjectName As String, PermitNumber As String, commadelimitedprojectnumbers As String, pitypeid As Integer, ByVal maximumRows As Integer, ByVal startRowIndex As Integer, <System.Runtime.InteropServices.Out()> ByRef totalRowCount As Integer) As IEnumerable(Of NOIProject)

        Dim pprojectnumber As New SqlParameter("@projectnumber", SqlDbType.VarChar, 32)
        pprojectnumber.Value = common.nullIfBlank(ProjectNumber)
        Dim pprojectname As New SqlParameter("@projectname", SqlDbType.VarChar, 80)
        pprojectname.Value = common.nullIfBlank(ProjectName)
        Dim ppermitnumber As New SqlParameter("@permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = common.nullIfBlank(PermitNumber)
        Dim pprojectnumbers As New SqlParameter("@projectnumbers", SqlDbType.VarChar, 1000)
        pprojectnumbers.Value = common.nullIfBlank(commadelimitedprojectnumbers)
        Dim ppitypeid As New SqlParameter("@pitypeid", SqlDbType.Int)
        ppitypeid.Value = pitypeid
        Dim pmaximumRows As New SqlParameter("@maximumRows", SqlDbType.Int)
        pmaximumRows.Value = maximumRows
        Dim pstartRowIndex As New SqlParameter("@startRowIndex", SqlDbType.Int)
        pstartRowIndex.Value = startRowIndex
        Dim ptotalRowCount As New SqlParameter("@totalrowcount", SqlDbType.Int)
        ptotalRowCount.Direction = ParameterDirection.Output

        Dim result As List(Of NOIProject) = db.Database.SqlQuery(Of NOIProject)("spNOI_GetProjectsByProjectNumbers @projectnumber,@projectname,@permitnumber, @projectnumbers,@pitypeid,@maximumRows,@startRowIndex,@totalrowcount out", pprojectnumber, pprojectname, ppermitnumber, pprojectnumbers, ppitypeid, pmaximumRows, pstartRowIndex, ptotalRowCount).ToList()
        'Dim projects As List(Of NOIProject)
        'projects = result.ToList()
        totalRowCount = ptotalRowCount.Value

        Return result

    End Function


    ''verified
    Public Function GetAllNOIAndCoPermitteeDetails(ProjectNumber As String, ProjectName As String, PermitNumber As String) As IQueryable(Of ProjectOwnerView)
        Dim pprojectnumber As New SqlParameter("@projectnumber", SqlDbType.VarChar, 32)
        pprojectnumber.Value = common.nullIfBlank(ProjectNumber)
        Dim pprojectname As New SqlParameter("@projectname", SqlDbType.VarChar, 80)
        pprojectname.Value = common.nullIfBlank(ProjectName)
        Dim ppermitnumber As New SqlParameter("@permitnumber", SqlDbType.VarChar, 20)
        ppermitnumber.Value = common.nullIfBlank(PermitNumber)

        Dim results = db.Database.SqlQuery(Of ProjectOwnerView)("spNOI_GetAllProjectAndCoPermitteeDetails @projectnumber,@projectname,@permitnumber", pprojectnumber, pprojectname, ppermitnumber)
        Return results.ToList().AsQueryable()
    End Function


    Public Function GetNOISearch(ProgID As String, PiName As String, DelegateAgency As Int32, StartDate As String, EndDate As String,
                                ByVal maximumRows As Integer, ByVal startRowIndex As Integer, <System.Runtime.InteropServices.Out()> ByRef totalRowCount As Integer) As IEnumerable(Of NOIPublicView)

        Try


            Dim pprogid As New SqlParameter("@ProgID", SqlDbType.VarChar, 20)
            Dim psearchname As New SqlParameter("@SearchName", SqlDbType.VarChar, 80)
            Dim pdelegAgency As New SqlParameter("@DelegAgency", SqlDbType.Int)
            Dim pStartDate As New SqlParameter("@StartDate", SqlDbType.Date)
            Dim pEndDate As New SqlParameter("@EndDate", SqlDbType.Date)
            Dim pmaximumRows As New SqlParameter("@maximumRows", SqlDbType.Int)
            Dim pstartRowIndex As New SqlParameter("@startRowIndex", SqlDbType.Int)
            Dim ptotalRowCount As New SqlParameter("@totalRowCount", SqlDbType.Int)
            ptotalRowCount.Direction = ParameterDirection.Output

            pprogid.Value = IIf(ProgID = String.Empty, DBNull.Value, ProgID)
            psearchname.Value = IIf(PiName = String.Empty, DBNull.Value, PiName)
            pdelegAgency.Value = IIf(DelegateAgency = 0, DBNull.Value, DelegateAgency)
            pStartDate.Value = IIf(StartDate = Nothing, DBNull.Value, Convert.ToDateTime(StartDate))
            pEndDate.Value = IIf(EndDate = Nothing, DBNull.Value, Convert.ToDateTime(EndDate))
            pmaximumRows.Value = maximumRows
            pstartRowIndex.Value = startRowIndex

            Dim result = db.Database.SqlQuery(Of NOIPublicView)("spNOI_NOISearchForPublicView @ProgID,@SearchName,@DelegAgency,@StartDate,@EndDate,@maximumRows,@startRowIndex,@totalRowCount out", pprogid, psearchname, pdelegAgency, pStartDate, pEndDate, pmaximumRows, pstartRowIndex, ptotalRowCount)

            Dim noi As List(Of NOIPublicView)
            noi = result.OrderByDescending(Function(a) a.PiID).ToList()
            totalRowCount = ptotalRowCount.Value
            Return noi

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Async Function GetNOISearchAsync(ProgID As String, PiName As String, DelegateAgency As Int32, StartDate As String, EndDate As String,
    '                            ByVal maximumRows As Integer, ByVal startRowIndex As Integer) As Task(Of IEnumerable(Of NOIPublicView))

    '    Try


    '        Dim pprogid As New SqlParameter("@ProgID", SqlDbType.VarChar, 20)
    '        Dim psearchname As New SqlParameter("@SearchName", SqlDbType.VarChar, 80)
    '        Dim pdelegAgency As New SqlParameter("@DelegAgency", SqlDbType.Int)
    '        Dim pStartDate As New SqlParameter("@StartDate", SqlDbType.Date)
    '        Dim pEndDate As New SqlParameter("@EndDate", SqlDbType.Date)
    '        Dim pmaximumRows As New SqlParameter("@maximumRows", SqlDbType.Int)
    '        Dim pstartRowIndex As New SqlParameter("@startRowIndex", SqlDbType.Int)
    '        Dim ptotalRowCount As New SqlParameter("@totalRowCount", SqlDbType.Int)
    '        ptotalRowCount.Direction = ParameterDirection.Output

    '        pprogid.Value = IIf(ProgID = String.Empty, DBNull.Value, ProgID)
    '        psearchname.Value = IIf(PiName = String.Empty, DBNull.Value, PiName)
    '        pdelegAgency.Value = IIf(DelegateAgency = 0, DBNull.Value, DelegateAgency)
    '        pStartDate.Value = IIf(StartDate = Nothing, DBNull.Value, Convert.ToDateTime(StartDate))
    '        pEndDate.Value = IIf(EndDate = Nothing, DBNull.Value, Convert.ToDateTime(EndDate))
    '        pmaximumRows.Value = maximumRows
    '        pstartRowIndex.Value = startRowIndex

    '        Dim result = db.Database.SqlQuery(Of NOIPublicView)("spNOI_NOISearchForPublicView @ProgID,@SearchName,@DelegAgency,@StartDate,@EndDate,@maximumRows,@startRowIndex,@totalRowCount out", pprogid, psearchname, pdelegAgency, pStartDate, pEndDate, pmaximumRows, pstartRowIndex, ptotalRowCount)

    '        Dim noi As List(Of NOIPublicView)
    '        noi = result.OrderByDescending(Function(a) a.PiID).ToList()
    '        'totalRowCount = ptotalRowCount.Value
    '        Return noi

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function


    Public Function GetISNOISearch(ProgID As String, PiName As String, StartDate As String, EndDate As String, ReportID As Integer, maximumRows As Integer,
                                    ByVal startRowIndex As Integer, <System.Runtime.InteropServices.Out()> ByRef totalRowCount As Integer) As IEnumerable(Of NOIPublicViewForIS)

        Try

            Dim pitypeid As Integer = 0
            pitypeid = CacheLookupData.GetPiTypeIDByReportID(ReportID)

            Dim pprogid As New SqlParameter("@ProgID", SqlDbType.VarChar, 20)
            Dim psearchname As New SqlParameter("@SearchName", SqlDbType.VarChar, 80)
            Dim pStartDate As New SqlParameter("@StartDate", SqlDbType.Date)
            Dim pEndDate As New SqlParameter("@EndDate", SqlDbType.Date)
            Dim pmaximumRows As New SqlParameter("@maximumRows", SqlDbType.Int)
            Dim pstartRowIndex As New SqlParameter("@startRowIndex", SqlDbType.Int)
            Dim ppitypeid As New SqlParameter("@PiTypeID", SqlDbType.Int)
            Dim ptotalRowCount As New SqlParameter("@totalRowCount", SqlDbType.Int)
            ptotalRowCount.Direction = ParameterDirection.Output

            pprogid.Value = IIf(ProgID = String.Empty, DBNull.Value, ProgID)
            psearchname.Value = IIf(PiName = String.Empty, DBNull.Value, PiName)
            pStartDate.Value = IIf(StartDate = Nothing, DBNull.Value, Convert.ToDateTime(StartDate))
            pEndDate.Value = IIf(EndDate = Nothing, DBNull.Value, Convert.ToDateTime(EndDate))
            pmaximumRows.Value = maximumRows
            ppitypeid.Value = pitypeid
            pstartRowIndex.Value = startRowIndex

            Dim result = db.Database.SqlQuery(Of NOIPublicViewForIS)("spNOI_ISNOISearchForPublicView @ProgID,@SearchName,@StartDate,@EndDate,@maximumRows,@startRowIndex,@PiTypeID,@totalRowCount out", pprogid, psearchname, pStartDate, pEndDate, pmaximumRows, pstartRowIndex, ppitypeid, ptotalRowCount)

            Dim noi As List(Of NOIPublicViewForIS)
            noi = result.OrderByDescending(Function(a) a.PiID).ToList()
            totalRowCount = ptotalRowCount.Value
            Return noi

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetNOIOwnerByPIID(Piid As Integer) As IQueryable(Of NOIOwner)
        Try
            Dim ppiid As New SqlParameter("@PiID", SqlDbType.Int)
            ppiid.Value = Piid

            Dim result = db.Database.SqlQuery(Of NOIOwner)("spNOI_GetNOIOwnerByPIID @PiID", ppiid)
            Return result.ToList().AsQueryable() '.OrderBy(Function(a) a.AfflID)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''verified
    Public Function GetNOICoPermitteesByPermitNumber(permitnumber As String) As IList(Of NOIOwner)
        Try
            Dim pprogid As New SqlParameter("@ProgID", SqlDbType.VarChar, 60)
            pprogid.Value = permitnumber

            Dim result = db.Database.SqlQuery(Of NOIOwner)("spNOI_GetNOICoPermitteesByProgID @ProgID", pprogid)
            Return result.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetGeneralNOIByPIId(Piid As Integer) As NOIProjectInternal

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand()
        conn.Open()
        cmd.CommandText = "spNOI_GetGeneralNOIByPIID"

        Dim paraPiid As New SqlParameter("@PiID", SqlDbType.Int)
        paraPiid.Value = Piid
        cmd.Parameters.Add(paraPiid)
        cmd.CommandType = CommandType.StoredProcedure

        Dim npi As NOIProjectInternal
        Dim swc As SWConstruct
        Dim persons As ICollection(Of NOIPersonOrg)
        Dim tp As ICollection(Of NOITaxParcel)
        'Dim tp As ICollection(Of NOIProjectTaxParcels)
        Dim swbmps As ICollection(Of SWConstructBMP)
        Dim permitstatuses As ICollection(Of PermitStatus)

        Try

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            ' While reader.Read()

            npi = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIProjectInternal)(reader).Single()

            'End While

            reader.NextResult()


            swc = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of SWConstruct)(reader).Single()


            reader.NextResult()


            persons = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIPersonOrg)(reader).ToList()

            reader.NextResult()


            tp = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOITaxParcel)(reader).ToList()
            'tp = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIProjectTaxParcels)(reader).ToList()


            reader.NextResult()


            swbmps = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of SWConstructBMP)(reader).ToList()

            reader.NextResult()

            permitstatuses = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of PermitStatus)(reader).ToList()

            npi.HasChanged = False
            swc.HasChanged = False
            For Each p As NOIPersonOrg In persons
                p.HasChanged = False
            Next

            npi.SWConstructDet = swc
            npi.PersonOrg = persons
            npi.TaxParcel = tp
            npi.SWConstructBMPDet = swbmps
            npi.PermitStatuses = permitstatuses

        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try


        Return npi
    End Function

    Public Function GetGeneralNOIByPIIdForIS(Piid As Integer) As NOIProjectInternal

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand()
        conn.Open()
        cmd.CommandText = "spNOI_GetISGeneralNOIByPIID"

        Dim paraPiid As New SqlParameter("@PiID", SqlDbType.Int)
        paraPiid.Value = Piid
        cmd.Parameters.Add(paraPiid)
        cmd.CommandType = CommandType.StoredProcedure

        Dim npi As NOIProjectInternal
        Dim persons As ICollection(Of NOIPersonOrg)
        Dim tp As ICollection(Of NOITaxParcel)
        Dim siccodes As ICollection(Of NOIPiSIC)
        Dim naicscodes As ICollection(Of NOIPiNAICS)
        Dim outfalls As ICollection(Of NOIOutfall)
        Dim permitstatuses As ICollection(Of PermitStatus)

        Try

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            npi = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIProjectInternal)(reader).Single()

            reader.NextResult()

            persons = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIPersonOrg)(reader).ToList()

            reader.NextResult()

            tp = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOITaxParcel)(reader).ToList()

            reader.NextResult()

            siccodes = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIPiSIC)(reader).ToList()

            reader.NextResult()

            naicscodes = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIPiNAICS)(reader).ToList()

            reader.NextResult()

            outfalls = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIOutfall)(reader).ToList()

            reader.NextResult()

            permitstatuses = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of PermitStatus)(reader).ToList()

            npi.HasChanged = False

            For Each p As NOIPersonOrg In persons
                p.HasChanged = False
            Next

            npi.PersonOrg = persons
            npi.TaxParcel = tp
            npi.SICCodes = siccodes
            npi.NAICSCodes = naicscodes
            npi.Outfalls = outfalls
            npi.PermitStatuses = permitstatuses

        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try


        Return npi
    End Function

    Public Function GetGeneralNOIByPIIdForAP(Piid As Integer) As NOIProjectInternal
        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand()
        conn.Open()
        cmd.CommandText = "spNOI_GetAPGeneralNOIByPIID"

        Dim paraPiid As New SqlParameter("@PiID", SqlDbType.Int)
        paraPiid.Value = Piid
        cmd.Parameters.Add(paraPiid)
        cmd.CommandType = CommandType.StoredProcedure

        Dim npi As NOIProjectInternal
        Dim nap As NOIAPAnnualThreshold
        Dim persons As ICollection(Of NOIPersonOrg)
        Dim apchemicals As ICollection(Of NOIAPChemicals)
        Dim permitstatuses As ICollection(Of PermitStatus)

        Try

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            npi = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIProjectInternal)(reader).Single()

            reader.NextResult()

            nap = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIAPAnnualThreshold)(reader).Single()

            reader.NextResult()

            persons = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIPersonOrg)(reader).ToList()

            reader.NextResult()

            apchemicals = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIAPChemicals)(reader).ToList()

            reader.NextResult()

            permitstatuses = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of PermitStatus)(reader).ToList()

            npi.HasChanged = False

            For Each p As NOIPersonOrg In persons
                p.HasChanged = False
            Next

            npi.NOIAPAnnualThresholdDet = nap
            npi.PersonOrg = persons
            npi.NOIAPChemicalsLst = apchemicals
            npi.PermitStatuses = permitstatuses

        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try


        Return npi
    End Function

    Public Function GetCoPermitteeNOIByPIId(Piid As Integer, afflid As Integer) As NOIProjectInternal

        Dim conn As SqlConnection = db.Database.Connection
        Dim cmd As SqlCommand = conn.CreateCommand()
        conn.Open()
        cmd.CommandText = "spNOI_GetCoPermitteeNOIByPIID"

        Dim paraPiid As New SqlParameter("@PiID", SqlDbType.Int)
        paraPiid.Value = Piid
        cmd.Parameters.Add(paraPiid)
        Dim paraAfflid As New SqlParameter("@AfflID", SqlDbType.Int)
        paraAfflid.Value = afflid
        cmd.Parameters.Add(paraAfflid)

        cmd.CommandType = CommandType.StoredProcedure

        Dim npi As NOIProjectInternal
        Dim swc As SWConstruct
        Dim persons As ICollection(Of NOIPersonOrg)

        Try

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            ' While reader.Read()

            npi = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIProjectInternal)(reader).Single()

            'End While

            reader.NextResult()


            swc = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of SWConstruct)(reader).Single()


            reader.NextResult()


            persons = CType(db, IObjectContextAdapter).ObjectContext().Translate(Of NOIPersonOrg)(reader).ToList()


            npi.SWConstructDet = swc
            npi.PersonOrg = persons

        Catch ex As Exception
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try


        Return npi
    End Function


    Public Function SaveGeneralNOI(project As NOIProjectInternal, userid As String) As Boolean

        Dim conn As SqlConnection = db.Database.Connection
        Dim tran As SqlTransaction = Nothing
        Dim projectresult As Boolean = True
        Dim swcresult As Boolean = True
        Dim poresult As Boolean = True
        Dim tpdeleteresult As Boolean = True
        Dim tpinsertresult As Boolean = True
        Dim bmpdeleteresult As Boolean = True
        Dim bmpinsertresult As Boolean = True
        Dim psdeleteresult As Boolean = True
        Dim psinsertresult As Boolean = True
        Dim returnresult As Boolean = True



        Try
            conn.Open()
            tran = conn.BeginTransaction
            If project.HasChanged Then
                projectresult = SaveProject(project, userid, tran)
            End If
            'Save SWConstruct
            If project.SWConstructDet.HasChanged Then
                swcresult = SaveSwConstruct(project.SWConstructDet, userid, tran)
            End If

            'Save project personorg
            For Each po As NOIPersonOrg In project.PersonOrg
                If po.HasChanged Then
                    poresult = SavePersonOrg(po, userid, tran)
                    If poresult = False Then
                        Exit For
                    End If
                End If
            Next

            'save project taxparcel
            For Each tp As NOITaxParcel In project.TaxParcel
                If tp.IsDeleted Then
                    tpdeleteresult = DeleteTaxParcel(tp, tran)
                    If tpdeleteresult = False Then
                        Exit For
                    End If
                End If
                If tp.ProjectTaxParcelID = 0 Then
                    tpinsertresult = InsertTaxParcel(tp, project.PIID, userid, tran)
                    If tpinsertresult = False Then
                        Exit For
                    End If
                End If
            Next

            'Save SWBMP

            For Each bmp As SWConstructBMP In project.SWConstructBMPDet
                If bmp.IsDeleted Then
                    bmpdeleteresult = DeleteSWBMP(bmp, tran)
                    If bmpdeleteresult = False Then
                        Exit For
                    End If
                End If
                If bmp.PermitID = 0 Then
                    bmpinsertresult = InsertSWBMP(bmp, tran)
                    If bmpinsertresult = False Then
                        Exit For
                    End If
                End If
            Next

            'Save PermitStatus

            For Each ps As PermitStatus In project.PermitStatuses
                If ps.IsDeleted Then
                    psdeleteresult = DeletePermitStatus(ps, tran)
                    If psdeleteresult = False Then
                        Exit For
                    End If
                End If
                If ps.PEventsID = 0 Then
                    psinsertresult = InsertPermitStatus(ps, userid, tran)
                    If psinsertresult = False Then
                        Exit For
                    End If
                End If
            Next

            If projectresult And swcresult And poresult And tpdeleteresult And tpinsertresult And bmpdeleteresult And bmpinsertresult And psdeleteresult And psinsertresult Then
                tran.Commit()
                returnresult = True
            Else
                Throw New ApplicationException("Saving the General NOI is unsuccessful.")
            End If


        Catch ex As Exception
            tran.Rollback()
            returnresult = False
            'Throw ex
            Dim bal As New NOIBAL
            bal.LogError(Nothing, New Exception("Error while saving the General NOI for :  " + project.PIID.ToString(), ex))
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
        Return returnresult
    End Function

    Public Function SaveISGeneralNOI(Project As NOIProjectInternal, userid As String) As Boolean
        Dim conn As SqlConnection = db.Database.Connection
        Dim tran As SqlTransaction = Nothing
        Dim projectresult As Boolean = True
        Dim poresult As Boolean = True
        Dim tpdeleteresult As Boolean = True
        Dim tpinsertresult As Boolean = True
        Dim sicdeleteresult As Boolean = True
        Dim sicinsertresult As Boolean = True
        Dim naicsdeleteresult As Boolean = True
        Dim naicsinsertresult As Boolean = True
        Dim outfalldeleteresult As Boolean = True
        Dim outfallinsertresult As Boolean = True
        Dim psdeleteresult As Boolean = True
        Dim psinsertresult As Boolean = True
        Dim returnresult As Boolean = True



        Try
            conn.Open()
            tran = conn.BeginTransaction
            If Project.HasChanged Then
                projectresult = SaveProject(Project, userid, tran)
            End If

            'Save project personorg
            For Each po As NOIPersonOrg In Project.PersonOrg
                If po.HasChanged Then
                    poresult = SavePersonOrg(po, userid, tran)
                    If poresult = False Then
                        Exit For
                    End If
                End If
            Next

            'save project taxparcel
            For Each tp As NOITaxParcel In Project.TaxParcel
                If tp.IsDeleted Then
                    tpdeleteresult = DeleteTaxParcel(tp, tran)
                    If tpdeleteresult = False Then
                        Exit For
                    End If
                End If
                If tp.ProjectTaxParcelID = 0 Then
                    tpinsertresult = InsertTaxParcel(tp, Project.PIID, userid, tran)
                    If tpinsertresult = False Then
                        Exit For
                    End If
                End If
            Next


            'Save Sic

            For Each sic As NOIPiSIC In Project.SICCodes
                If sic.IsDeleted Then
                    sicdeleteresult = DeleteSIC(sic, tran)
                    If sicdeleteresult = False Then
                        Exit For
                    End If
                End If
                If sic.PIID = 0 Then
                    sicinsertresult = InsertSIC(sic, Project.PIID, tran)
                    If sicinsertresult = False Then
                        Exit For
                    End If
                End If
            Next


            'save naics
            For Each naics As NOIPiNAICS In Project.NAICSCodes
                If naics.IsDeleted Then
                    naicsdeleteresult = DeleteNAICS(naics, tran)
                    If naicsdeleteresult = False Then
                        Exit For
                    End If
                End If
                If naics.PIID = 0 Then
                    naicsinsertresult = InsertNAICS(naics, Project.PIID, tran)
                    If naicsinsertresult = False Then
                        Exit For
                    End If
                End If
            Next


            'save outfall
            For Each outfall As NOIOutfall In Project.Outfalls
                If outfall.IsDeleted Then
                    outfalldeleteresult = DeleteOutfall(outfall, tran)
                    If outfalldeleteresult = False Then
                        Exit For
                    End If
                End If
                If outfall.PIID = 0 Then
                    outfallinsertresult = InsertOutfall(outfall, Project.PIID, Project.PermitID, userid, tran)
                    If outfallinsertresult = False Then
                        Exit For
                    End If
                End If
            Next



            'Save PermitStatus

            For Each ps As PermitStatus In Project.PermitStatuses
                If ps.IsDeleted Then
                    psdeleteresult = DeletePermitStatus(ps, tran)
                    If psdeleteresult = False Then
                        Exit For
                    End If
                End If
                If ps.PEventsID = 0 Then
                    psinsertresult = InsertPermitStatus(ps, userid, tran)
                    If psinsertresult = False Then
                        Exit For
                    End If
                End If
            Next

            If projectresult And poresult And tpdeleteresult And tpinsertresult And sicdeleteresult And sicinsertresult And naicsdeleteresult And naicsinsertresult And outfalldeleteresult And outfallinsertresult And psdeleteresult And psinsertresult Then
                tran.Commit()
                returnresult = True
            Else
                Throw New ApplicationException("Saving the Industrial Stormwater General NOI is unsuccessful.")
            End If


        Catch ex As Exception
            tran.Rollback()
            returnresult = False
            'Throw ex
            Dim bal As New NOIBAL
            bal.LogError(Nothing, New Exception("Error while saving the Industrial Stormwater General NOI for : " + Project.PIID.ToString(), ex))
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
        Return returnresult
    End Function

    Public Function SaveAPGeneralNOI(Project As NOIProjectInternal, userid As String) As Boolean
        Dim conn As SqlConnection = db.Database.Connection
        Dim tran As SqlTransaction = Nothing
        Dim projectresult As Boolean = True
        Dim poresult As Boolean = True
        Dim atresult As Boolean = True
        Dim apcdeleteresult As Boolean = True
        Dim apcupdateresult As Boolean = True
        Dim apcinsertresult As Boolean = True
        Dim psdeleteresult As Boolean = True
        Dim psinsertresult As Boolean = True
        Dim returnresult As Boolean = True



        Try
            conn.Open()
            tran = conn.BeginTransaction
            If Project.HasChanged Then
                projectresult = SaveProject(Project, userid, tran)
            End If

            'Save AnnualThreshold
            If Project.NOIAPAnnualThresholdDet.HasChanged Then
                atresult = SaveAPAnnualThreshold(Project.NOIAPAnnualThresholdDet, userid, tran)
            End If

            'Save project personorg
            For Each po As NOIPersonOrg In Project.PersonOrg
                If po.HasChanged Then
                    poresult = SavePersonOrg(po, userid, tran)
                    If poresult = False Then
                        Exit For
                    End If
                End If
            Next

            'save apChemicals
            For Each apc As NOIAPChemicals In Project.NOIAPChemicalsLst
                If apc.IsDeleted Then
                    apcdeleteresult = DeleteApChemical(apc, tran)
                    If apcdeleteresult = False Then
                        Exit For
                    End If
                End If
                If apc.IsModified Then
                    apcupdateresult = DeleteApChemical(apc, tran)
                    If apcupdateresult = False Then
                        Exit For
                    End If
                End If
                If apc.APChemicalID = 0 Then
                    apcinsertresult = InsertAPChemical(apc, tran)
                    If apcinsertresult = False Then
                        Exit For
                    End If
                End If
            Next


            'Save PermitStatus

            For Each ps As PermitStatus In Project.PermitStatuses
                If ps.IsDeleted Then
                    psdeleteresult = DeletePermitStatus(ps, tran)
                    If psdeleteresult = False Then
                        Exit For
                    End If
                End If
                If ps.PEventsID = 0 Then
                    psinsertresult = InsertPermitStatus(ps, userid, tran)
                    If psinsertresult = False Then
                        Exit For
                    End If
                End If
            Next

            If projectresult And atresult And poresult And apcdeleteresult And apcinsertresult And psdeleteresult And psinsertresult Then
                tran.Commit()
                returnresult = True
            Else
                Throw New ApplicationException("Saving the Aquatic Pesticide General NOI is unsuccessful.")
            End If


        Catch ex As Exception
            tran.Rollback()
            returnresult = False
            'Throw ex
            Dim bal As New NOIBAL
            bal.LogError(Nothing, New Exception("Error while saving the Aquatic Pesticide General NOI for :  " + Project.PIID.ToString(), ex))
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
        Return returnresult
    End Function

    Private Function SaveProject(ByVal project As NOIProjectInternal, userid As String, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_UpdateProject")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@piid", SqlDbType.Int).Value = project.PIID
                .Parameters.Add("@piname", SqlDbType.VarChar, 80).Value = project.ProjectName
                .Parameters.Add("@piaddress_1", SqlDbType.VarChar, 50).Value = project.ProjectAddress
                .Parameters.Add("@picity", SqlDbType.VarChar, 30).Value = project.ProjectCity
                .Parameters.Add("@pistateabv", SqlDbType.Char, 2).Value = project.ProjectStateAbv
                .Parameters.Add("@pipostalcode", SqlDbType.VarChar, 14).Value = project.ProjectPostalCode
                .Parameters.Add("@x", SqlDbType.Decimal)
                .Parameters("@x").Value = common.nullIfBlank(project.X)
                .Parameters.Add("@y", SqlDbType.Decimal)
                .Parameters("@y").Value = common.nullIfBlank(project.Y)
                .Parameters.Add("@latitude", SqlDbType.Decimal)
                .Parameters("@latitude").Value = common.nullIfBlank(project.Latitude)
                .Parameters.Add("@longitude", SqlDbType.Decimal)
                .Parameters("@longitude").Value = common.nullIfBlank(project.Longitude)
                .Parameters.Add("@huc12", SqlDbType.VarChar, 12)
                .Parameters("@huc12").Value = common.nullIfBlank(project.HUC12)
                .Parameters.Add("@deliagencyid", SqlDbType.Int).Value = common.nullIfBlank(project.DelegatedAgencyID)
                .Parameters.Add("@projectafflid", SqlDbType.Int).Value = common.nullIfBlank(project.ProjectAfflID)
                .Parameters.Add("@comments", SqlDbType.VarChar, 1000).Value = project.Comments
                .Parameters.Add("@userid", SqlDbType.VarChar, 64).Value = userid
                .ExecuteNonQuery()
            End With
            result = True

        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result
    End Function

    Private Function SaveSwConstruct(swc As SWConstruct, userid As String, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_UpdateSWConstruct")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@piid", SqlDbType.Int).Value = swc.PIID
                .Parameters.Add("@permitid", SqlDbType.Int).Value = swc.PermitID
                .Parameters.Add("@projecttypeid", SqlDbType.TinyInt).Value = swc.ProjectTypeID
                .Parameters.Add("@otherprojecttype", SqlDbType.VarChar, 50).Value = common.nullIfBlank(swc.OtherProjectType)
                .Parameters.Add("@constructcounty", SqlDbType.VarChar, 50).Value = swc.ConstructCounty
                .Parameters.Add("@constructmunicipality", SqlDbType.VarChar, 50).Value = common.nullIfBlank(swc.ConstructMunicipality)
                .Parameters.Add("@isswpppprepared", SqlDbType.Char, 1).Value = swc.IsSWPPPPrepared
                .Parameters.Add("@totallandarea", SqlDbType.Decimal)

                .Parameters("@totallandarea").Value = swc.TotalLandArea
                .Parameters.Add("@estimatedarea", SqlDbType.Decimal)

                .Parameters("@estimatedarea").Value = swc.EstimatedArea
                .Parameters.Add("@constructstartdate", SqlDbType.Date).Value = swc.ConstructStartDate
                .Parameters.Add("@constructenddate", SqlDbType.Date).Value = swc.ConstructEndDate
                .Parameters.Add("@userid", SqlDbType.VarChar, 64).Value = userid
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result
    End Function

    Private Function SavePersonOrg(ByVal porg As NOIPersonOrg, userid As String, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_UpdatePersonAndAddress")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@afflid", SqlDbType.Int).Value = porg.ProgAfflID
                .Parameters.Add("@piid", SqlDbType.Int).Value = porg.PIID
                .Parameters.Add("@personorgtypecode", SqlDbType.Char, 1).Value = porg.PersonOrgTypeCode
                .Parameters.Add("@orgname", SqlDbType.VarChar, 80).Value = common.nullIfBlank(porg.OrgName)
                .Parameters.Add("@l_name", SqlDbType.VarChar, 30).Value = porg.LName
                .Parameters.Add("@f_name", SqlDbType.VarChar, 20).Value = porg.FName
                .Parameters.Add("@m_name", SqlDbType.VarChar, 20).Value = common.nullIfBlank(porg.MName)
                .Parameters.Add("@nameprefix", SqlDbType.VarChar, 5).Value = common.nullIfBlank(porg.NamePrefix)
                .Parameters.Add("@namesuffix", SqlDbType.VarChar, 5).Value = common.nullIfBlank(porg.NameSuffix)
                .Parameters.Add("@address_1", SqlDbType.VarChar, 50).Value = porg.Address1
                .Parameters.Add("@address_2", SqlDbType.VarChar, 50).Value = common.nullIfBlank(porg.Address2)
                .Parameters.Add("@city", SqlDbType.VarChar, 30).Value = porg.City
                .Parameters.Add("@stateabv", SqlDbType.Char, 2).Value = porg.StateAbv
                .Parameters.Add("@postalcode", SqlDbType.VarChar, 14).Value = porg.PostalCode
                .Parameters.Add("@phone", SqlDbType.VarChar, 16).Value = common.nullIfBlank(porg.Phone)
                .Parameters.Add("@phoneext", SqlDbType.VarChar, 5).Value = common.nullIfBlank(porg.PhoneExt)
                .Parameters.Add("@mobile", SqlDbType.VarChar, 16).Value = common.nullIfBlank(porg.Mobile)
                .Parameters.Add("@ecommaddress", SqlDbType.VarChar, 100).Value = common.nullIfBlank(porg.EmailAddress)
                .Parameters.Add("@startdate", SqlDbType.Date).Value = porg.StartDate
                .Parameters.Add("@comment", SqlDbType.VarChar, 1000).Value = common.nullIfBlank(porg.Comments)
                .Parameters.Add("@active", SqlDbType.Char, 1).Value = porg.Active
                If porg.EndDate IsNot Nothing Then
                    .Parameters.Add("@enddate", SqlDbType.Date).Value = porg.EndDate
                Else
                    .Parameters.Add("@enddate", SqlDbType.Date).Value = DBNull.Value
                End If
                .Parameters.Add("@userid", SqlDbType.VarChar, 64).Value = userid
                .ExecuteNonQuery()
            End With

            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result
    End Function

    Private Function InsertTaxParcel(ByVal tp As NOITaxParcel, piid As Integer, userid As String, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_InsertTaxParcelByPiid")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@taxparcel", SqlDbType.VarChar, 100).Value = tp.TaxParcelNumber
                .Parameters.Add("@county", SqlDbType.Char, 1).Value = tp.TaxParcelCounty
                .Parameters.Add("@piid", SqlDbType.Int).Value = piid
                .Parameters.Add("@userid", SqlDbType.VarChar, 64).Value = userid
                .ExecuteNonQuery()
            End With

            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function

    Private Function DeleteTaxParcel(ByVal tp As NOITaxParcel, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_DeleteTaxParcelByTaxID")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@taxid", SqlDbType.Int).Value = tp.ProjectTaxParcelID
                .Parameters.Add("@piid", SqlDbType.Int).Value = tp.PIID
                .ExecuteNonQuery()
            End With

            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function

    Private Function InsertSWBMP(ByVal bmp As SWConstructBMP, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_InsertSWBMP")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@permitid", SqlDbType.Int).Value = bmp.PermitID
                .Parameters.Add("@swbmpid", SqlDbType.TinyInt).Value = bmp.SWBMPID
                .Parameters.Add("@swbmpothername", SqlDbType.VarChar, 50).Value = common.nullIfBlank(bmp.SWBMPOtherName)
                .Parameters.Add("@swbmpqty", SqlDbType.Int).Value = bmp.SWBMPQty
                .ExecuteNonQuery()
            End With

            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function

    Private Function DeleteSWBMP(ByVal bmp As SWConstructBMP, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_DeleteSWBMP")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@permitid", SqlDbType.Int).Value = bmp.PermitID
                .Parameters.Add("@swbmpid", SqlDbType.TinyInt).Value = bmp.SWBMPID
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function

    Private Function InsertAPChemical(ByVal apchemical As NOIAPChemicals, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_InsertAPChemical")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@permitid", SqlDbType.Int).Value = apchemical.PermitID
                .Parameters.Add("@ingredient", SqlDbType.VarChar, 200).Value = apchemical.Ingredient
                .Parameters.Add("@pesticidepatternid", SqlDbType.Int).Value = apchemical.PesticidePatternID
                .Parameters.Add("@applicationrate", SqlDbType.Decimal).Value = apchemical.ApplicationRate
                .Parameters.Add("@applicationrateunit", SqlDbType.VarChar, 50).Value = apchemical.ApplicationRateUnit
                .Parameters.Add("@annlavgqty", SqlDbType.Decimal).Value = apchemical.AnnlAvgQty
                .Parameters.Add("@annlavgqtyunit", SqlDbType.VarChar, 50).Value = apchemical.AnnlAvgQtyUnit
                .Parameters.Add("@annlavgarea", SqlDbType.Decimal).Value = apchemical.AnnlAvgArea
                .Parameters.Add("@annlavgareaunit", SqlDbType.VarChar, 50).Value = apchemical.AnnlAvgAreaUnit
                .ExecuteNonQuery()
            End With

            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function

    Private Function DeleteApChemical(ByVal apchemical As NOIAPChemicals, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_DeleteAPChemical")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@apchemicalid", SqlDbType.Int).Value = apchemical.APChemicalID
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result
    End Function


    Private Function InsertSIC(ByVal sic As NOIPiSIC, piid As Integer, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("sptblPiSIC_Add")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@PiID", SqlDbType.Int).Value = piid
                .Parameters.Add("@SIC", SqlDbType.VarChar, 4).Value = sic.SIC
                .Parameters.Add("@RankCode", SqlDbType.Char, 1).Value = sic.RankCode
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = True
            Throw ex
        End Try
        Return result
    End Function

    Private Function DeleteSIC(ByVal sic As NOIPiSIC, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("sptblPiSIC_Delete")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@PiID", SqlDbType.Int).Value = sic.PIID
                .Parameters.Add("@SIC", SqlDbType.VarChar, 4).Value = sic.SIC
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function


    Private Function InsertNAICS(ByVal naics As NOIPiNAICS, piid As Integer, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("sptblPiNAICS_Add")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@PiID", SqlDbType.Int).Value = piid
                .Parameters.Add("@NAICS", SqlDbType.VarChar, 6).Value = naics.NAICS
                .Parameters.Add("@RankCode", SqlDbType.Char, 1).Value = naics.RankCode
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = True
            Throw ex
        End Try
        Return result
    End Function

    Private Function DeleteNAICS(ByVal naics As NOIPiNAICS, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("sptblPiNAICS_Delete")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@PiID", SqlDbType.Int).Value = naics.PIID
                .Parameters.Add("@NAICS", SqlDbType.VarChar, 6).Value = naics.NAICS
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function


    Private Function InsertOutfall(ByVal outfall As NOIOutfall, piid As Integer, permitid As Integer, userid As String, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_tblUnitInsert")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@unitname", SqlDbType.VarChar, 100).Value = outfall.UnitName
                .Parameters.Add("@unittypeid", SqlDbType.Int).Value = outfall.UnitTypeID
                .Parameters.Add("@piid", SqlDbType.Int).Value = piid
                .Parameters.Add("@permitid", SqlDbType.Int).Value = permitid
                .Parameters.Add("@x", SqlDbType.Decimal).Value = outfall.X
                .Parameters.Add("@y", SqlDbType.Decimal).Value = outfall.Y
                .Parameters.Add("@latitude", SqlDbType.Decimal).Value = outfall.Latitude
                .Parameters.Add("@longitude", SqlDbType.Decimal).Value = outfall.Longitude
                .Parameters.Add("@huc12", SqlDbType.VarChar, 12).Value = outfall.HUC12
                .Parameters.Add("@userid", SqlDbType.VarChar, 64).Value = userid
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = True
            Throw ex
        End Try
        Return result
    End Function

    Private Function DeleteOutfall(ByVal outfall As NOIOutfall, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("sptblUnit_Delete")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@UnitID", SqlDbType.Int).Value = outfall.UnitID
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function

    Private Function SaveAPAnnualThreshold(annualthreshold As NOIAPAnnualThreshold, userid As String, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("spNOI_UpdateAnnualThreshold")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@piid", SqlDbType.Int).Value = annualthreshold.PIID
                .Parameters.Add("@permitid", SqlDbType.Int).Value = annualthreshold.PermitID
                .Parameters.Add("@entitytypeid", SqlDbType.Int).Value = annualthreshold.EntityTypeID
                .Parameters.Add("@insectpestcontrol", SqlDbType.Char, 1).Value = annualthreshold.InsectPestControl
                .Parameters.Add("@weedpestcontrol", SqlDbType.Char, 1).Value = annualthreshold.WeedPestControl
                .Parameters.Add("@animalpestcontrol", SqlDbType.Char, 1).Value = annualthreshold.AnimalPestControl
                .Parameters.Add("@forestcanopypestcontrol", SqlDbType.Char, 1).Value = annualthreshold.ForestCanopyPestControl
                .Parameters.Add("@notexceededthreshold", SqlDbType.Char, 1).Value = annualthreshold.NotExceededThreshold
                .Parameters.Add("@commercialapplicatorid", SqlDbType.VarChar, 50).Value = common.nullIfBlank(annualthreshold.CommercialApplicatorID)
                .Parameters.Add("@userid", SqlDbType.VarChar, 64).Value = userid
                .ExecuteNonQuery()
            End With
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result
    End Function



    Private Function InsertPermitStatus(ByVal ps As PermitStatus, ByVal UserID As String, tran As SqlTransaction) As String
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("sptblPEvents_AddForNOI")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@PermitID", SqlDbType.Int).Value = ps.PermitID
                .Parameters.Add("@PEventDate", SqlDbType.DateTime).Value = ps.PEventDate
                .Parameters.Add("@PEventCode", SqlDbType.SmallInt).Value = ps.PEventCode
                .Parameters.Add("@PEventComment", SqlDbType.VarChar, 1000).Value = common.nullIfBlank(ps.PEventComment)
                .Parameters.Add("@UserID", SqlDbType.VarChar, 64).Value = UserID
                .Parameters.Add("@PEventsID", SqlDbType.Int).Direction = ParameterDirection.Output
                .ExecuteNonQuery()
            End With


            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result

    End Function

    Private Function DeletePermitStatus(ByVal ps As PermitStatus, tran As SqlTransaction) As Boolean
        Dim result As Boolean = False
        Try
            Dim comm As New SqlCommand("sptblPEvents_Delete")
            With comm
                .Transaction = tran
                .Connection = tran.Connection
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@PEventsID", SqlDbType.Int).Value = ps.PEventsID
                .ExecuteNonQuery()
            End With
            result = True


        Catch ex As Exception
            result = False
            Throw ex
        End Try
        Return result
    End Function

    Public Function SaveCopermitteeNOI(Project As NOIProjectInternal, userid As String) As Boolean
        Dim conn As SqlConnection = db.Database.Connection
        Dim tran As SqlTransaction = Nothing
        Dim poresult As Boolean = True


        Try
            conn.Open()
            tran = conn.BeginTransaction
            Dim copermittee As NOIPersonOrg = Project.PersonOrg.Single(Function(e) e.AfflType = EISSqlAfflType.Copermittee)
            If copermittee.HasChanged Then
                poresult = SavePersonOrg(copermittee, userid, tran)
            End If
            If poresult Then
                tran.Commit()
            Else
                Throw New ApplicationException("Saving the General NOI is unsuccessful.")
            End If

        Catch ex As Exception
            tran.Rollback()
            poresult = False
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
        Return poresult
    End Function

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
        'GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
