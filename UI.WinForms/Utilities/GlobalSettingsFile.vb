Namespace Utilities
    ''' <summary>
    ''' Provides methods for reading and writing the global settings file.
    ''' </summary>
    Public Class GlobalSettingsFile
        Inherits ObfuscatedDataSetFile(Of GlobalDataSet)

        ''' <summary>
        ''' Creates a new instance of the class.
        ''' </summary>
        ''' <param name="filePath">The path to the global settings file.</param>
        Public Sub New(ByVal filePath As String)
            MyBase.New(filePath)
        End Sub

        ''' <summary>
        ''' Creates a new empty settings file.
        ''' </summary>
        Public Sub CreateNew()
            WriteGlobalSettings(New GlobalSettings())
        End Sub

        ''' <summary>
        ''' Writes settings to the file.
        ''' </summary>
        ''' <param name="globalSettings">The settings to write.</param>
        Public Sub WriteGlobalSettings(ByVal globalSettings As GlobalSettings)
            Write(globalSettings.GlobalDataSet)
            globalSettings.GlobalDataSet.AcceptChanges()
        End Sub

        ''' <summary>
        ''' Reads settings from the file.
        ''' </summary>
        ''' <returns>The settings contained in the file.</returns>
        Public Function ReadGlobalSettings() As GlobalSettings
            Dim globalDataSet As GlobalDataSet = Read()
            globalDataSet.AcceptChanges()
            Return New GlobalSettings(globalDataSet)
        End Function

        ''' <summary>
        ''' Gets a value indicating whether the current global settings match the settings
        ''' in the file.
        ''' </summary>
        ''' <param name="globalSettings">The current settings to check.</param>
        ''' <returns>True if the settings still match those in the file; otherwise, false.</returns>
        Public Overloads Function IsChanged(ByVal globalSettings As GlobalSettings) As Boolean
            Return IsChanged(globalSettings.GlobalDataSet)
        End Function
    End Class
End Namespace