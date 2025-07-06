using TipTracker.Ui.ViewData;

namespace TipTracker.Ui.Controls;

/// <summary>
/// Contains extension methods for the <see cref="DataGridView"/>
/// </summary>
public static class DataGridViewExtensions
{
    /// <summary>
    /// Ensures the currently selected item remains selected after the DataGridView's items have been sorted.
    /// </summary>
    /// <typeparam name="T">The type of items contained in the DataGridView and its internal binding sources.</typeparam>
    /// <param name="gridView">The DataGridView for which to preserve the selection</param>
    /// <exception cref="ArgumentException">If the DataGridView's DataSource is not a BindingSource the BindingSource's
    /// DataSource is not a <see cref="SortableBindingList{T}"/>.</exception>
    public static void PreserveSelectionOnSort<T>(this DataGridView gridView) where T : ISortableCollectionMember
    {
        if (gridView.DataSource is not BindingSource bindingSource)
        {
            throw new ArgumentException($"Cannot preserve selection on data source of type {gridView.DataSource.GetType()}");
        }

        if (bindingSource.DataSource is not SortableBindingList<T> bindingList)
        {
            throw new ArgumentException($"Cannot preserve selection on data source of type {bindingSource.DataSource.GetType()}");
        }

        object currentSelection = null;

        // Event fires before sorting, so grab the current selection.
        bindingList.SortModeChanging += (s, e) =>
        {
            currentSelection = bindingSource.Current;
        };

        // Event fires after sorting, so re-set the selection.
        gridView.Sorted += (s, e) =>
        {
            if (currentSelection == null) { return; }
            bindingSource.Position = bindingSource.List.IndexOf(currentSelection);
            currentSelection = null;
        };
    }
}