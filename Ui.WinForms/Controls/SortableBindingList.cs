using System.ComponentModel;
using TipTracker.Ui.ViewData;

namespace TipTracker.Ui.Controls;

/// <summary>
/// Provides a generic collection that supports data binding and sorting.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class SortableBindingList<T> : BindingList<T> where T : ISortableCollectionMember
{
    private readonly Func<PropertyDescriptor, IComparer<T>> _comparerFactory;

    /// <summary>
    /// Creates a new instance of the binding list class initialized with the specified list.
    /// </summary>
    /// <param name="unorderedValues">The values to store in the binding list.</param>
    /// <param name="comparerFactory">A factory method used to create a comparer for instances of <see cref="T"/>.</param>
    public SortableBindingList(IList<T> unorderedValues, Func<PropertyDescriptor, IComparer<T>> comparerFactory)
        : base(unorderedValues)
    {
        _comparerFactory = comparerFactory;
        ListChanged += HandleNewItems;
    }

    /// <summary>
    /// Listens for the <see cref="BindingList{T}.ListChanged"/> event and adds new items to the internal unordered list.
    /// </summary>
    /// <param name="sender">The instance firing the event.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void HandleNewItems(object? sender, ListChangedEventArgs e)
    {
        if (e.ListChangedType != ListChangedType.ItemAdded) { return; }
    }

    /// <summary>
    /// Sorts the items in the binding list using the order in the specified list.
    /// </summary>
    /// <param name="orderedItems">A list containing the items in the desired order.</param>
    private void SortItems(IList<T> orderedItems)
    {
        for (var newIndex = 0; newIndex < orderedItems.Count; newIndex++)
        {
            var newItem = orderedItems[newIndex];
            var oldIndex = IndexOf(newItem);

            if (oldIndex != newIndex)
            {
                SetItem(newIndex, newItem);
            }
        }
    }

    /// <inheritdoc />
    protected override bool SupportsSortingCore => true;

    /// <inheritdoc />
    protected override void RemoveSortCore()
    {
        SortItems(Items.OrderBy(x => x.OrdinalId).ToList());
    }

    /// <inheritdoc />
    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        var comparer = _comparerFactory.Invoke(prop);
        var sortedList = direction == ListSortDirection.Ascending
            ? Items.Order(comparer).ToArray()
            : Items.OrderDescending(comparer).ToArray();
        SortItems(sortedList);
    }
}