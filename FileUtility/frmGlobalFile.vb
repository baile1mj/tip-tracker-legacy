Imports TipTracker.Common.Data.GlobalSettings

Public Class frmGlobalFile
    Private ReadOnly _globalSettingsFile As GlobalSettingsFile
    Private _globalSettings As GlobalSettings

    Public Sub New(ByVal globalSettingsFile As GlobalSettingsFile)
        InitializeComponent()

        If IsNothing(globalSettingsFile) Then
            Throw New ArgumentNullException(NameOf(globalSettingsFile), "The global settings file instance cannot be null.")
        End If

        _globalSettingsFile = globalSettingsFile
    End Sub

    Private Sub frmGlobalFile_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            _globalSettings = _globalSettingsFile.ReadGlobalSettings()
        Catch ex As Exception
            MessageBox.Show($"An error occurred while loading the global settings file: ""{ex.Message}"".",
                "Error Loading Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try

        SettingsBindingSource.DataSource = _globalSettings.GlobalDataSet
        SettingsBindingSource.DataMember = _globalSettings.GlobalDataSet.Settings.TableName

        ServersBindingSource.DataSource = _globalSettings.GlobalDataSet
        ServersBindingSource.DataMember = _globalSettings.GlobalDataSet.Servers.TableName
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFile()
    End Sub

    Private Function SaveFile() As Boolean
        Dim isSuccessful As Boolean = True
        Try
            _globalSettingsFile.WriteGlobalSettings(_globalSettings)
        Catch ex As Exception
            MessageBox.Show($"An error occurred when saving the file: ""{ex.Message}"".", "Error Saving File",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            isSuccessful = False
        End Try

        Return isSuccessful
    End Function

    Private Sub SaveXMLToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveXMLToolStripMenuItem.Click
        Dim strFileName As String

        Using dlgSave As New SaveFileDialog() With {
            .AddExtension = True,
            .DefaultExt = ".xml",
            .FileName = "Global Settings",
            .Filter = "XML File (*.xml)|*.xml",
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            .OverwritePrompt = True,
            .RestoreDirectory = True,
            .SupportMultiDottedExtensions = True,
            .Title = "Save File"}

            If dlgSave.ShowDialog <> DialogResult.OK Then Exit Sub

            strFileName = dlgSave.FileName
        End Using

        Try
            _globalSettings.GlobalDataSet.WriteXml(strFileName)
        Catch ex As Exception
            MessageBox.Show($"An error occurred while saving the XML: ""{ex.Message}"".", "Error Saving XML",
                MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub frmDataFile_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not _globalSettings.GlobalDataSet.HasChanges Then Exit Sub

        Dim userResponse As DialogResult = MessageBox.Show("Do you wish to save your changes to the global settings?",
            "Save Changes", MessageBoxButtons.YesNoCancel)

        If userResponse = DialogResult.Yes Then
            Dim isSuccessful As Boolean = SaveFile()
            e.Cancel = Not isSuccessful
        ElseIf userResponse = DialogResult.Cancel Then
            e.Cancel = True
        End If
    End Sub

End Class
