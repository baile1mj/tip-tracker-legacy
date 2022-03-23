Imports System.Linq
Imports TipTracker.Core
Imports TipTracker.Core.UI

Namespace Utilities
    ''' <summary>
    ''' Provides functionality to map autocomplete strings to <see cref="Server"/> instances.
    ''' </summary>
    Public Class ServerAutoCompleteCollection
        Inherits AutoCompleteStringCollection
        Private ReadOnly _autoCompleteLookup As New Dictionary(Of String, Server)(StringComparer.OrdinalIgnoreCase)

        ''' <summary>
        ''' Creates a new collection of autocomplete values for a specified set of servers.
        ''' </summary>
        ''' <param name="servers">The servers for which to create the autocomplete source.</param>
        Public Sub New(servers As IEnumerable(Of Server))
            Dim lookups = servers.Select(Function(s) New ServerLookup(s))

            For Each lookup In lookups
                _autoCompleteLookup(lookup.ByLastName) = lookup.Server
                _autoCompleteLookup(lookup.ByFirstName) = lookup.Server
                _autoCompleteLookup(lookup.ByPosId) = lookup.Server

                AddRange(lookup.Values.ToArray())
            Next
        End Sub

        ''' <inheritdoc />
        Public Shadows Function Contains(value As String) As Boolean
            Return _autoCompleteLookup.ContainsKey(value)
        End Function

        ''' <summary>
        ''' Gets the server represented by an autocomplete collection value.
        ''' </summary>
        ''' <param name="lookupValue">The string representing the server.</param>
        ''' <returns>The server represented by the lookup value.</returns>
        Public Function GetServer(lookupValue As String) As Server
            Return _autoCompleteLookup(lookupValue)
        End Function
    End Class
End Namespace