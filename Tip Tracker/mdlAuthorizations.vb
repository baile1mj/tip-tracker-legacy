Module mdlAuthorizations
    Private Const intManageTemplateServers As Integer = 1
    Private Const intManageGlobalUsers As Integer = 2
    Private Const intChangeGlobalSettings As Integer = 3
    Private Const intFinalizeTips As Integer = 4
    Private Const intChangeWorkingDate As Integer = 5
    Private Const intPrintReports As Integer = 6
    Private Const intManageSpecialFunctions As Integer = 7
    Private Const intManageFileServers As Integer = 8
    Private Const intOptimizeFile As Integer = 9
    Private Const intImportTips As Integer = 10
    Private Const intExportTips As Integer = 11
    Private Const intAddEditTips As Integer = 12

    Friend Function GetAuthString(ByVal ManageTemplateServers As Boolean, ByVal ManageGlobalUsers As Boolean, _
        ByVal ChangeGlobalSettings As Boolean, ByVal FinalizeTips As Boolean, ByVal ChangeWorkingDate As Boolean, _
        ByVal PrintReports As Boolean, ByVal ManageSpecialFunctions As Boolean, ByVal ManageFileServers As Boolean, _
        ByVal OptimizeFile As Boolean, ByVal ImportTips As Boolean, ByVal ExportTips As Boolean, _
        ByVal AddEditTips As Boolean) As String

        Dim strAuthString As String = ""

        If ManageTemplateServers = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ManageGlobalUsers = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ChangeGlobalSettings = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If FinalizeTips = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ChangeWorkingDate = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If PrintReports = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ManageSpecialFunctions = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ManageFileServers = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If OptimizeFile = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ImportTips = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ExportTips = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If AddEditTips = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        Return strAuthString
    End Function

    Friend Function CanManageTemplateServers(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intManageTemplateServers)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanManageGlobalUsers(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intManageGlobalUsers)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanChangeGlobalSettings(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intChangeGlobalSettings)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanFinalizeTips(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intFinalizeTips)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanChangeWorkingDate(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intChangeWorkingDate)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanPrintReports(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intPrintReports)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanManageSpecialFunctions(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intManageSpecialFunctions)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanManageFileServers(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intManageFileServers)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanOptimizeFile(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intOptimizeFile)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanImportTips(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intImportTips)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanExportTips(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intExportTips)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Friend Function CanAddEditTips(ByVal AuthString As String) As Boolean
        Dim c As Char

        Try
            c = GetChar(AuthString, intAddEditTips)
        Catch ex As Exception
            Return False
        End Try

        Select Case c.ToString
            Case "1"
                Return True
            Case Else
                Return False
        End Select
    End Function

End Module
