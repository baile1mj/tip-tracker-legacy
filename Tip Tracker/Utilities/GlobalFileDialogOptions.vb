Namespace Utilities
    ''' <summary>
    ''' Contains common properties for file dialogs related to saving and loading the global settings file.
    ''' </summary>
    Public NotInheritable Class GlobalFileDialogOptions
        Private Const _TITLE As String = "Global Settings File"

        ''' <summary>
        ''' The default file extension to use in the dialog.
        ''' </summary>
        Public Const DefaultExtension As String = "dat"

        ''' <summary>
        ''' The default file name to populate in the dialog.
        ''' </summary>
        Public Const FileName As String = "ttgsf.dat"

        ''' <summary>
        ''' The string to use for filtering file types.
        ''' </summary>
        Public Const Filter As String = "DAT Files (*.dat)|*.dat"

        ''' <summary>
        ''' The collection of actions that can be performed on the file.
        ''' </summary>
        Public Enum Action
            Open
            Create
        End Enum

        ''' <summary>
        ''' The default directory to suggest when no other directory is specified.
        ''' </summary>
        Public Shared ReadOnly DefaultDirectory As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        ''' <summary>
        ''' Builds a title for the dialog based on the action being taken.
        ''' </summary>
        ''' <param name="action">The action that will be taken on the file.</param>
        ''' <returns>A title for the dialog.</returns>
        Public Shared Function BuildDialogTitle(ByVal action As Action) As String
            Return $"{action} {_TITLE}"
        End Function
    End Class
End Namespace