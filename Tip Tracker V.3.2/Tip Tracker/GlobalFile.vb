Imports System.IO
Imports System.IO.File


Public Module GlobalFile
    Const _SETTINGS_FILE As String = "GlobalFile.location"
    Private _directory As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Tip Tracker")

    ''' <summary>
    ''' Gets the path to the global data file.
    ''' </summary>
    ''' <returns>a string containing the path to the global file</returns>
    ''' <remarks></remarks>
    Public Function GetGlobalFilePath() As String
        Dim locationFilePath As String = Path.Combine(_directory, _SETTINGS_FILE)
        Dim reader As StreamReader
        Dim location As String = ""

        If File.Exists(locationFilePath) Then
            Try
                reader = New StreamReader(Path.Combine(_directory, _SETTINGS_FILE))
                location = reader.ReadToEnd().Trim()
                reader.Close()
                reader.Dispose()
            Catch ex As Exception
                Throw New Exception("Could not read from the file.")
            End Try
        End If

        Return location
    End Function

    ''' <summary>
    ''' Sets the global file path.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetGlobalFilePath(ByVal filePath As String)
        If Not Directory.Exists(_directory) Then
            Directory.CreateDirectory(_directory)
        End If

        Dim locationFilePath As String = path.Combine(_directory, _SETTINGS_FILE)
        Dim writer As New StreamWriter(locationFilePath, False)

        Try
            writer.Write(filePath)
            writer.Flush()
            writer.Close()
            writer.Dispose()
        Catch ex As Exception
            Throw New Exception("Could not write the changes to the file")
        End Try
    End Sub

End Module
