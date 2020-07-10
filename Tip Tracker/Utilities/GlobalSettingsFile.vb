Imports System.IO

Namespace Utilities
    ''' <summary>
    ''' Provides methods for reading and writing the global settings file.
    ''' </summary>
    Public Class GlobalSettingsFile

        Public ReadOnly FilePath As String

        ''' <summary>
        ''' Creates a new instance of the class.
        ''' </summary>
        ''' <param name="filePath">The path to the global settings file.</param>
        Public Sub New(ByVal filePath As String)
            If String.IsNullOrEmpty(filePath) Then
                Throw New ArgumentException("The file path cannot be empty or null.", NameOf(filePath))
            End If

            Me.FilePath = filePath
        End Sub

        ''' <summary>
        ''' Gets a value indicating whether the file exists.
        ''' </summary>
        ''' <returns></returns>
        Public Function Exists() As Boolean
            Return File.Exists(FilePath)
        End Function

        ''' <summary>
        ''' Creates a new empty settings file.
        ''' </summary>
        Public Sub CreateNew()
            Write(New GlobalSettings())
        End Sub

        ''' <summary>
        ''' Writes settings to the file.
        ''' </summary>
        ''' <param name="globalSettings">The settings to write.</param>
        Public Sub Write(ByVal globalSettings As GlobalSettings)
            Dim obfuscator As New FileObfuscator()
            Dim dataStream As New MemoryStream()

            globalSettings.GlobalDataSet.WriteXml(dataStream)
            dataStream.Seek(0, SeekOrigin.Begin)

            Dim fileContents As String = obfuscator.Obfuscate(dataStream.ToArray())

            File.WriteAllText(FilePath, fileContents)
        End Sub

        ''' <summary>
        ''' Reads settings from the file.
        ''' </summary>
        ''' <returns>The settings contained in the file.</returns>
        Public Function Read() As GlobalSettings
            Dim fileContents As String = File.ReadAllText(FilePath)
            Dim obfuscator As New FileObfuscator()
            Dim contentBytes As Byte()

            'We should be able to de-obfuscate the file, but we'll fall back to the legacy scheme if that fails.
            Try
                contentBytes = obfuscator.DeObfuscate(fileContents)
            Catch exception As Exception
                Try
                    contentBytes = obfuscator.LegacyDeObfuscate(fileContents)
                Catch legacyException As Exception
                    'If all de-obfuscation attempts fail, return a null object and let the caller figure out what to do.
                    Return Nothing
                End Try
            End Try

            Dim dataStream As New MemoryStream(contentBytes)
            Dim globalDataSet As New GlobalDataSet()

            globalDataSet.ReadXml(dataStream)

            Return New GlobalSettings(globalDataSet)
        End Function

        ''' <summary>
        ''' Gets a value indicating whether the current global settings match the settings
        ''' in the file.
        ''' </summary>
        ''' <param name="globalSettings">The current settings to check.</param>
        ''' <returns>True if the settings still match those in the file; otherwise, false.</returns>
        Public Function IsChanged(ByVal globalSettings As GlobalSettings) As Boolean
            Dim obfuscator As New FileObfuscator()
            Dim dataStream As New MemoryStream()

            globalSettings.GlobalDataSet.WriteXml(dataStream)
            dataStream.Seek(0, SeekOrigin.Begin)

            Dim settingsContents As String = obfuscator.Obfuscate(dataStream.ToArray())
            Dim fileContents As String = File.ReadAllText(FilePath)

            Return Not settingsContents.Equals(fileContents)
        End Function
    End Class
End Namespace