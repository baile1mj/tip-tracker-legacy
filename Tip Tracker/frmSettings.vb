Imports System.IO

''' <summary>
''' Provides a user interface for working with application settings.
''' </summary>
Public Class frmSettings

    ''' <summary>
    ''' Gets the default directory selected by the user.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DefaultDirectory As String
        Get
            Return txtDefaultDataDirectory.Text
        End Get
    End Property

    ''' <summary>
    ''' Creates a new instance of the form.
    ''' </summary>
    ''' <param name="defaultDirectory">The existing default directory to populate.</param>
    Public Sub New(ByVal defaultDirectory As String)
        InitializeComponent()
        txtDefaultDataDirectory.Text = defaultDirectory
    End Sub

    ''' <summary>
    ''' Handles the event that is raised when the user clicks the <see cref="btnSetDefaultDirectory"/> button.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">Arguments related to the event.</param>
    Private Sub btnSetDefaultDirectory_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetDefaultDirectory.Click
        Dim initialDirectory As String

        If String.IsNullOrEmpty(DefaultDirectory) Then
            initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Else
            initialDirectory = DefaultDirectory
        End If

        Using dlgBrowseFolders As New FolderBrowserDialog()
            With dlgBrowseFolders
                .Description = "Select the folder to be used for storing the data files."
                .RootFolder = Environment.SpecialFolder.Desktop
                .ShowNewFolderButton = True
                .SelectedPath = initialDirectory
            End With

            If dlgBrowseFolders.ShowDialog <> DialogResult.OK Then
                Exit Sub
            End If

            txtDefaultDataDirectory.Text = dlgBrowseFolders.SelectedPath
        End Using
    End Sub

    ''' <summary>
    ''' Handles the event that is raised when the user clicks the <see cref="btnCancel"/> button.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">Arguments related to the event.</param>
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    ''' <summary>
    ''' Handles the event that is raised when the user clicks the <see cref="btnOK"/> button.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">Arguments related to the event.</param>
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        If DefaultDirectory = "" Then
            MessageBox.Show("You must select a default data directory.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class