Imports System.IO

Namespace Data

    Public Class MachineSettings
        Const _SETTINGS_FILE As String = "GlobalFile.location"
        Private Shared ReadOnly Directory As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Tip Tracker")

        ''' <summary>
        ''' Gets the path to the global data file.
        ''' </summary>
        ''' <returns>a string containing the path to the global file</returns>
        ''' <remarks></remarks>
        Public Shared Function GetGlobalFilePath() As String
            Dim locationFilePath As String = Path.Combine(Directory, _SETTINGS_FILE)
            Dim reader As StreamReader
            Dim location As String = ""

            If File.Exists(locationFilePath) Then
                Try
                    reader = New StreamReader(Path.Combine(Directory, _SETTINGS_FILE))
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
        Public Shared Sub SetGlobalFilePath(ByVal filePath As String)
            If Not IO.Directory.Exists(Directory) Then
                IO.Directory.CreateDirectory(Directory)
            End If

            WriteGlobalFilePath(filePath)
        End Sub

        ''' <summary>
        ''' Clears the global file path to force a new file to be selected.
        ''' </summary>
        Public Shared Sub ClearGlobalFilePath()
            WriteGlobalFilePath(String.Empty)
        End Sub

        ''' <summary>
        ''' Writes a new path to the machine settings file.
        ''' </summary>
        ''' <param name="filePath">The path to write.</param>
        Private Shared Sub WriteGlobalFilePath(filePath As String)
            Dim locationFilePath As String = Path.Combine(Directory, _SETTINGS_FILE)

            Try
                File.WriteAllText(locationFilePath, filePath)
            Catch ex As Exception
                Throw New Exception("Could not write the changes to the file")
            End Try
        End Sub
    End Class
End Namespace