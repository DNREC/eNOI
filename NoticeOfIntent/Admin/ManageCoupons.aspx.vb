Imports System.Security
Imports System.Security.Cryptography

Public Class ManageCoupons
    Inherits FormBasePage


    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvExemptions_GetData() As IQueryable(Of NoticeOfIntent.NOIFeeExemption)
        Dim bal As New NOIBAL
        Return bal.GetAllExemptionCodes(logInVS.reportid).OrderBy(Function(b) b.SubmissionID).ThenByDescending(Function(a) a.ExemptionID)
    End Function

    Public Sub lvExemptions_InsertItem()
        Dim bal As New NOIBAL
        Dim userid As String = logInVS.user.userid

        Dim item = New NoticeOfIntent.NOIFeeExemption()
        Dim uniqcode As String = String.Empty
        Do
            uniqcode = GetUniqueKey()
        Loop Until (bal.GetAllExemptionCodes(logInVS.reportid).Where(Function(a) a.ExemptionCode = uniqcode).Count = 0)

        item.ExemptionCode = uniqcode
        item.ProgramID = logInVS.reportid
        item.LastChgBy = userid
        item.CreatedBy = userid
        item.EntityState = EntityState.Added
        TryUpdateModel(item)
        If ModelState.IsValid Then
            ' Save changes here
            bal.SaveExemptionCode(item)
        End If
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Public Sub lvExemptions_DeleteItem(ByVal ExemptionID As Integer)
        Dim bal As New NOIBAL
        bal.DeleteExemptionCode(ExemptionID)
    End Sub


    Public Function GetUniqueKey() As String
        Dim maxSize As Integer = 8
        Dim minSize As Integer = 5
        Dim chars As Char() = New Char(62) {}
        Dim a As String
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
        chars = a.ToCharArray()
        Dim size As Integer = maxSize
        Dim data As Byte() = New Byte(0) {}
        Dim crypto As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
        crypto.GetNonZeroBytes(data)
        size = maxSize
        data = New Byte(size - 1) {}
        crypto.GetNonZeroBytes(data)
        Dim result As StringBuilder = New StringBuilder(size)
        For Each b As Byte In data
            result.Append(chars(b Mod (chars.Length - 1)))
        Next

        Return result.ToString()
    End Function


    Private Sub lvExemptions_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lvExemptions.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim currentdataitem As NOIFeeExemption = e.Item.DataItem

            If currentdataitem.SubmissionID IsNot Nothing Then
                lbtnDelete.Enabled = False
            End If

        End If
    End Sub
End Class