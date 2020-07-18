Imports System.IO
Imports TipTracker.Utilities

Public Class frmMain
    Private ReadOnly _defaultDataDirectory As String

    Public Sub New()
        InitializeComponent()

        Try
            Dim globalFile As New GlobalSettingsFile(MachineSettings.GetGlobalFilePath())
            Dim globalSettings As GlobalSettings = globalFile.ReadGlobalSettings()

            _defaultDataDirectory = globalSettings.DefaultDataDirectory
        Catch ex As Exception
            'Do nothing if this fails since we'll default to the Documents folder.
        End Try

        'The path may not be valid so set up a valid one.
        If Not Directory.Exists(_defaultDataDirectory) Then
            _defaultDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnDataFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataFile.Click
        frmDataFile.Show()
    End Sub

    Private Sub btnGlobalFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGlobalFile.Click
        Dim globalSettingsFile As New GlobalSettingsFile(MachineSettings.GetGlobalFilePath())
        Dim globalFileEditor As New frmGlobalFile(globalSettingsFile)
        globalFileEditor.Show()
    End Sub

    Private Sub btnDecodeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecodeFile.Click
        Dim strSourceFile As String
        Using dlgOpen As New OpenFileDialog() With {
            .CheckFileExists = True,
            .CheckPathExists = True,
            .DefaultExt = ".ttd",
            .Filter = "Tip Tracker Data File (*.ttd)|*.ttd|Tip Tracker Global File (*.dat)|*.dat",
            .InitialDirectory = _defaultDataDirectory,
            .RestoreDirectory = True,
            .Title = "Select File to Decode"}

            If dlgOpen.ShowDialog <> DialogResult.OK Then Exit Sub

            strSourceFile = dlgOpen.FileName
        End Using

        Dim strDestinationFile As String
        Using dlgSave As New SaveFileDialog() With {
            .AddExtension = True,
            .DefaultExt = ".xml",
            .Filter = "XML File (*.xml)|*.xml|Plain Text File (*.txt)|*.txt",
            .InitialDirectory = _defaultDataDirectory,
            .OverwritePrompt = True,
            .RestoreDirectory = True,
            .SupportMultiDottedExtensions = True,
            .Title = "Save Decoded File"}

            If dlgSave.ShowDialog <> DialogResult.OK Then Exit Sub

            strDestinationFile = dlgSave.FileName
        End Using

        Try
            Dim obfuscator As New FileObfuscator()
            Dim fileContent As String = File.ReadAllText(strSourceFile)
            Dim plainTextBytes As Byte() = obfuscator.DeObfuscate(fileContent)

            File.WriteAllBytes(strDestinationFile, plainTextBytes)
        Catch ex As Exception
            MessageBox.Show($"Failed to decode file.  The process resulted in an exception: ""{ex.Message}"".",
                "File Decoding Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEncodeFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEncodeFile.Click
        Dim strSourceFile As String = ""
        Using dlgOpen As New OpenFileDialog() With {
            .CheckFileExists = True,
            .CheckPathExists = True,
            .DefaultExt = ".txt",
            .Filter = "XML File (*.xml)|*.xml|Plain Text File (*.txt)|*.txt",
            .InitialDirectory = _defaultDataDirectory,
            .RestoreDirectory = True,
            .Title = "Select File to Encode"}

            If dlgOpen.ShowDialog <> DialogResult.OK Then Exit Sub

            strSourceFile = dlgOpen.FileName
        End Using

        Dim strDestinationFile As String = ""
        Using dlgSave As New SaveFileDialog With {
            .AddExtension = True,
            .DefaultExt = ".ttd",
            .Filter = "Tip Tracker Data File (*.ttd)|*.ttd|Tip Tracker Global File (*.dat)|*.dat",
            .InitialDirectory = _defaultDataDirectory,
            .OverwritePrompt = True,
            .RestoreDirectory = True,
            .SupportMultiDottedExtensions = True,
            .Title = "Save Encoded File"}

            If dlgSave.ShowDialog <> DialogResult.OK Then Exit Sub

            strDestinationFile = dlgSave.FileName
        End Using

        Dim sourceFileContents As Byte()

        Try
            Dim obfuscatedContent As String
            Dim obfuscator As New FileObfuscator()

            sourceFileContents = File.ReadAllBytes(strSourceFile)
            obfuscatedContent = obfuscator.Obfuscate(sourceFileContents)
            File.WriteAllText(strDestinationFile, obfuscatedContent)
        Catch ex As Exception
            MessageBox.Show($"Failed to encode file.  The process resulted in an exception: ""{ex.Message}"".",
                "File Encoding Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnForceGlobalFileReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForceGlobalFileReset.Click
        Dim userPromptResult As DialogResult = MessageBox.Show("It is recommended that you close all instances " &
            "of Tip Tracker before continuing.  Please click OK when you are ready to proceed.", "Information",
            MessageBoxButtons.OKCancel)

        If userPromptResult <> DialogResult.OK Then Exit Sub

        Try
            MachineSettings.ClearGlobalFilePath()
            MessageBox.Show("The global file path has been removed.  Please run Tip Tracker to configure the " &
                "global file path.", "Reset Successful", MessageBoxButtons.OK)
        Catch ex As Exception
            MessageBox.Show("Unable to reset the global file path.  You must reset the path manually.",
                "Cannot Reset Path", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class