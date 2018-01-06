Public Class clsAuthorizations
    Private Const ManageServers As Integer = 1
    Private Const ManageUsers As Integer = 2
    Private Const ChangeSettings As Integer = 3
    Private Const ChangeWorkingDate As Integer = 4
    Private Const ImportTips As Integer = 5
    Private Const ExportTips As Integer = 6
    Private Const ManageSpecFunc As Integer = 7
    Private Const AddCashTips As Integer = 8
    Private Const AddSFTips As Integer = 9

    Friend ReadOnly Property CanManageServers(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, ManageServers).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanManageUsers(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, ManageUsers).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanChangeSettings(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, ChangeSettings).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanChangeWorkingDate(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, ChangeWorkingDate).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanImportTips(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, ImportTips).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanExportTips(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, ExportTips).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanManageSpecFunc(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, ManageSpecFunc).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanAddCashTips(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, AddCashTips).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend ReadOnly Property CanAddSFTips(ByVal AuthString As String) As Boolean
        Get
            Select Case GetChar(AuthString, AddSFTips).ToString
                Case "1"
                    Return True
                Case "0"
                    Return False
            End Select
        End Get
    End Property

    Friend Function CreateAuthString(ByVal ManageServers As Boolean, ByVal ManageUsers As Boolean, _
    ByVal ChangeSettings As Boolean, ByVal ChangeWorkingDate As Boolean, ByVal ImportTips As Boolean, _
    ByVal ExportTips As Boolean, ByVal ManageSpecFunc As Boolean, ByVal AddCashTips As Boolean, _
    ByVal AddSFTips As Boolean) As String

        Dim strAuthString As String = ""

        If ManageServers = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ManageUsers = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ChangeSettings = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If ChangeWorkingDate = True Then
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

        If ManageSpecFunc = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If AddCashTips = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        If AddSFTips = True Then
            strAuthString += "1"
        Else
            strAuthString += "0"
        End If

        Return strAuthString
    End Function


End Class
