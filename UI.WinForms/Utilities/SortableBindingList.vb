Imports System.ComponentModel
Imports System.Linq

Namespace Utilities
    ''' <summary>
    ''' Provides a generic collection that supports data binding and automatic sorting by
    ''' object property values.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Class SortableBindingList(Of T As Class)
        Inherits BindingList(Of T)

        ''' <inheritdoc />
        Protected Overrides ReadOnly Property IsSortedCore As Boolean

        ''' <inheritdoc />
        Protected Overrides ReadOnly Property SortDirectionCore As ListSortDirection

        ''' <inheritdoc />
        Protected Overrides ReadOnly Property SortPropertyCore As PropertyDescriptor

        ''' <inheritdoc />
        Protected Overrides ReadOnly Property SupportsSortingCore As Boolean = True

        ''' <summary>
        ''' Creates a new instance of the sortable binding list class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Creates a new instance of the sortable binding list class with an initial collection
        ''' of items.
        ''' </summary>
        ''' <param name="list">The items to store in the list.</param>
        Public Sub New(ByVal list As IList(Of T))
            MyBase.New(list)
        End Sub

        ''' <inheritdoc />
        Protected Overrides Sub RemoveSortCore()
            _SortDirectionCore = ListSortDirection.Ascending
            _SortPropertyCore = Nothing
            _IsSortedCore = False
        End Sub

        ''' <inheritdoc />
        Protected Overrides Sub ApplySortCore(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection)
            Dim isPropComparable As Boolean = GetType(IComparable).IsAssignableFrom(prop.PropertyType)

            'IComparable.Compare will tell us how the values should be ordered.
            If Not isPropComparable Then
                Throw New NotSupportedException($"Cannot sort by {prop.Name}.  The type {prop.PropertyType.Name} " &
                    $"does not implement {NameOf(IComparable)}.")
            End If

            _SortPropertyCore = prop
            _SortDirectionCore = direction

            Dim sortedList As IEnumerable(Of T)

            If (SortDirectionCore = ListSortDirection.Ascending) Then
                sortedList = Items.OrderBy(Function(ByVal i As T) prop.GetValue(i)).ToList()
            Else
                sortedList = Items.OrderByDescending(Function(ByVal i As T) prop.GetValue(i)).ToList()
            End If

            'Remove and replace the existing, unsorted items.
            Items.Clear()

            For Each sortedItem As T In sortedList
                Items.Add(sortedItem)
            Next

            _IsSortedCore = True

            'Fire an event to tell the UI that the contents have changed.
            OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        End Sub
    End Class
End Namespace