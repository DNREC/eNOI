Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class common


#Region "Public Method Null if blank"
    ''' <summary>
    ''' return DB null if val is blank, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function nullIfBlank(ByVal val As String) As Object
        If String.IsNullOrEmpty(val) Then
            Return DBNull.Value
        End If

        Return val
    End Function

    ''' <summary>
    ''' return DB null if val is blank, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function nullIfBlank(ByVal val As Char) As Object
        If IsNothing(val) OrElse val = "" OrElse Asc(val) = 0 Then
            Return DBNull.Value
        End If

        Return val
    End Function

    ''' <summary>
    ''' return DB null if val is blank, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function nullIfBlank(ByVal val As Date) As Object
        If val = CDate(Nothing) Then
            Return DBNull.Value
        End If

        Return val
    End Function

    ''' <summary>
    ''' return DB null if val is blank, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function nullIfBlank(ByVal val() As Byte) As Object
        If val Is Nothing Then
            Return DBNull.Value
        End If

        Return val
    End Function

    ''' <summary>
    ''' return DB null if val is blank, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function nullIfBlank(ByVal val As Integer) As Object
        If IsNothing(val) Then
            Return DBNull.Value
        End If

        Return val
    End Function

    ''' <summary>
    ''' return DB null if val is blank, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function nullIfBlank(ByVal val As Double) As Object
        If IsNothing(val) Then
            Return DBNull.Value
        End If

        Return val
    End Function

    ''' <summary>
    ''' return DB null if val is blank, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function nullIfBlank(ByVal val As Long) As Object
        If IsNothing(val) Then
            Return DBNull.Value
        End If

        Return val
    End Function



    Public Shared Function nullIfBlank(ByVal val As Boolean) As Object
        If IsNothing(val) Then
            Return DBNull.Value
        End If
        Return val
    End Function

    Public Shared Function nullIfBlank(ByVal val As Decimal?) As Object
        If IsNothing(val) Then
            Return DBNull.Value
        End If
        Return val
    End Function
#End Region

#Region "Public Method blank if null"

    ''' <summary>
    ''' Return 0 if val is dbnull, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBNullToInt16(ByVal val As Object) As Int16
        If IsDBNull(val) Then
            Return 0
        Else
            Return Convert.ToInt16(val)
        End If
    End Function

    ''' <summary>
    ''' Return 0 if val is dbnull, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBNullToInt32(ByVal val As Object, Optional ByVal defaultvalue As Integer = 0) As Int32
        If IsDBNull(val) Then
            Return defaultvalue
        Else
            Return Convert.ToInt32(val)
        End If
    End Function

    ''' <summary>
    ''' Return 0 if val is dbnull, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBNullToInt64(ByVal val As Object) As Int64
        If IsDBNull(val) Then
            Return 0
        Else
            Return Convert.ToInt64(val)
        End If
    End Function

    ''' <summary>
    ''' Return String.Empty if val is dbnull, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBNullToStr(ByVal val As Object) As String
        If IsDBNull(val) Then
            Return String.Empty
        Else
            Return Convert.ToString(val)
        End If
    End Function

    ''' <summary>
    ''' Return True/False if val is dbnull, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBNullToBool(ByVal val As Object) As Boolean
        If IsDBNull(val) Then
            Return False
        Else
            Return Convert.ToBoolean(val)
        End If
    End Function

    ''' <summary>
    ''' Return date.MinValue if val is dbnull, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBNullToDate(ByVal val As Object) As Date
        If IsDBNull(val) Then
            Return Date.MinValue
        Else
            Return Convert.ToDateTime(val)
        End If
    End Function

    Public Shared Function DBNullToDouble(ByVal val As Object) As Double
        If IsDBNull(val) Then
            Return 0.0
        Else
            Return Convert.ToDouble(val)
        End If
    End Function

    Public Shared Function DBNullToDecimal(ByVal val As Object) As Decimal
        If IsDBNull(val) Then
            Return 0.0
        Else
            Return Convert.ToDecimal(val)
        End If
    End Function

    ''' <summary>
    ''' Return 0 if val is dbnull, otherwise returns val
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBNullToLong(ByVal val As Object) As Long
        If IsDBNull(val) Then
            Return 0
        Else
            Return Long.Parse(val)
        End If
    End Function


#End Region

#Region "Public Method blank if nothing"
    Public Shared Function NothingToString(ByVal val As Object) As String
        If val Is Nothing Then
            Return String.Empty
        Else
            Return val.ToString()
        End If
    End Function

#End Region

#Region "Error Methods"
    ''' <summary>
    ''' returns a string of all the error messages of the exception and inner exception
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getError(ByVal e As Exception) As String
        Dim errorMsg As New StringBuilder("Error: " & e.Message)

        While (e.InnerException IsNot Nothing)
            errorMsg.Append(vbCrLf)
            errorMsg.Append(vbCrLf)
            errorMsg.Append("Inner Error:")
            errorMsg.Append(e.InnerException.Message)
            e = e.InnerException
        End While

        Return errorMsg.ToString()
    End Function

    Public Shared Function getErrorStackTrace(ByVal e As Exception) As String
        Dim i As Integer = 1
        Dim errorMsg As New StringBuilder("Error " & i.ToString & " : " & e.StackTrace())

        If e.Source = "EntityFramework" Then
            If TypeOf e Is Validation.DbEntityValidationException Then
                For Each evr As Validation.DbEntityValidationResult In CType(e, Validation.DbEntityValidationException).EntityValidationErrors
                    For Each ve As Validation.DbValidationError In evr.ValidationErrors
                        errorMsg.Append(vbCrLf)
                        errorMsg.Append(ve.PropertyName & "-" & ve.ErrorMessage)
                    Next
                Next
            End If

        End If

        While (e.InnerException IsNot Nothing)
            i += 1
            errorMsg.Append(vbCrLf)
            errorMsg.Append("Inner Error " & i.ToString() & " : ")
            errorMsg.Append(e.InnerException.StackTrace())
            e = e.InnerException
        End While




        Return errorMsg.ToString()
    End Function

    Public Shared Function GetEntityState(ByVal entityState As EntityState) As System.Data.Entity.EntityState
        Select Case entityState
            Case NoticeOfIntent.EntityState.Added
                Return System.Data.Entity.EntityState.Added
            Case NoticeOfIntent.EntityState.Modified
                Return System.Data.Entity.EntityState.Modified
            Case NoticeOfIntent.EntityState.Deleted
                Return System.Data.Entity.EntityState.Deleted
            Case NoticeOfIntent.EntityState.Unchanged
                Return System.Data.Entity.EntityState.Unchanged
            Case Else
                Return System.Data.Entity.EntityState.Detached
        End Select

    End Function

    Public Shared Sub ApplyStateChanges(ByRef db As DbContext)
        For Each entry As DbEntityEntry(Of IEntity) In db.ChangeTracker.Entries(Of IEntity)()
            Dim entity As IEntity = entry.Entity
            entry.State = GetEntityState(entity.EntityState)
        Next
    End Sub

    Friend Shared Function nullIfBlank(endDate As Date?) As Object
        Throw New NotImplementedException()
    End Function


#End Region

End Class
