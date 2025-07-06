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
    private IComparer<T>? _currentComparer;
    private PropertyDescriptor? _sortProperty;
    private ListSortDirection _sortDirection;

    /// <summary>
    /// Creates a new instance of the binding list class with no items.
    /// </summary>
    /// <param name="comparerFactory">A factory method used to create a comparer for instances of <see cref="T"/>.</param>
    public SortableBindingList(Func<PropertyDescriptor, IComparer<T>> comparerFactory)
        : this(new List<T>(), comparerFactory)
    { }

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
        // If there's not a new item or the list is unsorted, there's nothing to do.
        if (e.ListChangedType != ListChangedType.ItemAdded || !IsSortedCore) { return; }

        int belongsAt;
        var newItem = Items[e.NewIndex];

        for (belongsAt = 0; belongsAt < Items.Count; belongsAt++)
        {
            // We don't need to compare the new item to itself.
            if (newItem.Equals(Items[belongsAt])) { continue; }

            // Compare the new item to this item to see if the new item should take its place.
            var comparison = _currentComparer.Compare(newItem, Items[belongsAt]);
            if (SortDirectionCore == ListSortDirection.Descending) { comparison = -comparison; }

            if (comparison < 0) { break; }  // new item belongs here
        }

        // Item is already where it belongs.
        if (belongsAt == e.NewIndex) { return; }

        // Now pull the item out of its current position and put it where it belongs.
        Items.RemoveAt(e.NewIndex);

        if (belongsAt >= Items.Count)
        {
            Items.Add(newItem);
        }
        else
        {
            Items.Insert(belongsAt, newItem);
        }
    }

    /// <summary>
    /// Sorts the items in the binding list using the order in the specified list.
    /// </summary>
    private void SortItems()
    {
        T[] sortedList;

        if (_currentComparer == null)
        {
            sortedList = Items.OrderBy(i => i.OrdinalId).ToArray();
        }
        else if (_sortDirection == ListSortDirection.Ascending)
        {
            sortedList = Items.Order(_currentComparer).ToArray();
        }
        else
        {
            sortedList = Items.OrderDescending(_currentComparer).ToArray();
        }

        for (var newIndex = 0; newIndex < sortedList.Length; newIndex++)
        {
            var newItem = sortedList[newIndex];
            var oldIndex = IndexOf(newItem);

            if (oldIndex != newIndex)
            {
                SetItem(newIndex, newItem);
            }
        }
    }

    /// <summary>
    /// The event that is raised before the sort mode changes.
    /// </summary>
    public event EventHandler SortModeChanging = delegate { };

    /// <summary>
    /// Raises the <see cref="SortModeChanging"/> event.
    /// </summary>
    public void OnSortModeChanging()
    {
        SortModeChanging.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    protected override bool IsSortedCore => _currentComparer != null;

    /// <inheritdoc />
    protected override PropertyDescriptor? SortPropertyCore => _sortProperty;

    /// <inheritdoc />
    protected override ListSortDirection SortDirectionCore => _sortDirection;

    /// <inheritdoc />
    protected override bool SupportsSortingCore => true;

    /// <inheritdoc />
    protected override void RemoveSortCore()
    {
        OnSortModeChanging();
        _currentComparer = null;
        _sortDirection = ListSortDirection.Ascending;
        _sortProperty = null;
        SortItems();
    }

    /// <inheritdoc />
    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        OnSortModeChanging();
        _currentComparer = _comparerFactory.Invoke(prop);
        _sortProperty = prop;
        _sortDirection = direction;
        SortItems();
    }
}