Imports System.IO
Imports Tip_Tracker.Utilities

''' <summary>
''' Provides a user interface for getting the file path for the global settings file.
''' </summary>
Public Class frmConfigurator

    Private ReadOnly _initialDirectory As String

    ''' <summary>
    ''' Gets the path selected by the user.
    ''' </summary>
    ''' <returns>The selected global settings file pat.</returns>
    Public ReadOnly Property GlobalSettingsFilePath As String

    ''' <summary>
    ''' Creates a new instance of the form.
    ''' </summary>
    ''' <param name="filePath">The path to the existing global settings file.</param>
    Public Sub New(ByVal filePath As String)
        InitializeComponent()

        If Not String.IsNullOrEmpty(filePath) Then
            _initialDirectory = Path.GetDirectoryName(filePath)
        Else
            _initialDirectory = GlobalFileDialogOptions.DefaultDirectory
        End If
    End Sub

    ''' <summary>
    ''' Handles the event that is raised when the user clicks the New button.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">Arguments related to the event.</param>
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        Using dlgSaveFile As New SaveFileDialog
            With dlgSaveFile
                .InitialDirectory = _initialDirectory
                .DefaultExt = GlobalFileDialogOptions.DefaultExtension
                .FileName = GlobalFileDialogOptions.FileName
                .Filter = GlobalFileDialogOptions.Filter
                .Title = GlobalFileDialogOptions.BuildDialogTitle(GlobalFileDialogOptions.Action.Create)
            End With

            If dlgSaveFile.ShowDialog = DialogResult.OK Then
                txtGlobalFilePath.Text = dlgSaveFile.FileName
                _GlobalSettingsFilePath = dlgSaveFile.FileName
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Handles the event that is raised when the user clicks the Existing button.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">Arguments related to the event.</param>
    Private Sub btnExisting_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExisting.Click
        Using dlgOpenFile As New OpenFileDialog
            With dlgOpenFile
                .InitialDirectory = _initialDirectory
                .DefaultExt = GlobalFileDialogOptions.DefaultExtension
                .FileName = GlobalFileDialogOptions.FileName
                .Filter = GlobalFileDialogOptions.Filter
                .Title = GlobalFileDialogOptions.BuildDialogTitle(GlobalFileDialogOptions.Action.Open)
            End With

            If dlgOpenFile.ShowDialog = DialogResult.OK Then
                txtGlobalFilePath.Text = dlgOpenFile.FileName
                _GlobalSettingsFilePath = dlgOpenFile.FileName
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Responds to the event that is raised when the user clicks the OK button.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">Arguments related to the event.</param>
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        If String.IsNullOrEmpty(GlobalSettingsFilePath) Then
            MessageBox.Show("You must either create a new global settings file or open an existing one.", "File Selection Required", MessageBoxButtons.OK)
            Exit Sub
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    ''' <summary>
    ''' Handles the event that is raised when the user clicks the Cancel button.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">Arguments related to the event.</param>
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class