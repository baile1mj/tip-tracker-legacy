Namespace Utilities
    ''' <summary>
    ''' Represents the settings shared by all users within an organization.
    ''' </summary>
    Public Class GlobalSettings
        ''' <summary>
        ''' Gets the data set that stores the global settings.
        ''' </summary>
        ''' <returns>The data set containing the shared settings.</returns>
        Public ReadOnly Property GlobalDataSet As GlobalDataSet

        ''' <summary>
        ''' Gets the default directory for data files.
        ''' </summary>
        ''' <returns>The default data directory.</returns>
        Public Property DefaultDataDirectory As String
            Get
                Return GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value").ToString
            End Get

            Set(ByVal value As String)
                GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value") = value
            End Set
        End Property

        ''' <summary>
        ''' Creates a new instance of the settings class.
        ''' </summary>
        Public Sub New()
            Me.New(New GlobalDataSet())
        End Sub

        ''' <summary>
        ''' Creates a new instance of the settings class.
        ''' </summary>
        ''' <param name="dataSet">The data set containing the settings for this instance.</param>
        Public Sub New(ByVal dataSet As GlobalDataSet)
            If IsNothing(dataSet) Then
                Throw New ArgumentException("The settings data set cannot be null.", NameOf(dataSet))
            End If

            Const DEFAULT_DIRECTORY_SETTING As String = "DefaultDirectory"

            'Remove any unknown settings.
            For Each row As DataRow In dataSet.Settings.Rows
                If row("Setting").ToString() <> DEFAULT_DIRECTORY_SETTING Then
                    row.Delete()
                End If
            Next

            'Make sure there is an entry for the default directory.
            If dataSet.Settings.Rows.Count = 0 Then
                dataSet.Settings.Rows.Add(DEFAULT_DIRECTORY_SETTING, String.Empty)
            End If

            'If any servers are missing a value for "suppress chit", set the value to false.
            Const SUPPRESS_CHIT_COLUMN_NAME As String = "SuppressChit"

            For Each row As DataRow In dataSet.Servers.Rows
                If IsDBNull(SUPPRESS_CHIT_COLUMN_NAME) Then
                    row(SUPPRESS_CHIT_COLUMN_NAME) = False
                End If
            Next

            _GlobalDataSet = dataSet
        End Sub

        ''' <summary>
        ''' Gets a value indicating whether any servers are defined in the settings.
        ''' </summary>
        ''' <returns>True if there are servers defined; otherwise, false.</returns>
        Public Function HasServers() As Boolean
            Return GlobalDataSet.Servers.Rows.Count > 0
        End Function
    End Class
End Namespace