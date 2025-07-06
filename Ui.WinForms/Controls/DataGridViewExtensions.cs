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

    /// <summary>
    /// Adds a handler to the DataGridView to select the appropriate row on right-click and to display
    /// an associated context menu.
    /// </summary>
    /// <param name="gridView">The DataGridView for which to handle right-clicks.</param>
    /// <param name="contextMenu">The context menu to display.</param>
    public static void AddRightClickHandling(this DataGridView gridView, ContextMenuStrip? contextMenu = null)
    {
        gridView.MouseClick += (sender, e) =>
        {
            // Left clicks can be ignored.
            if (e.Button != MouseButtons.Right) { return; }

            var hitTestInfo = gridView.HitTest(e.X, e.Y);

            // Ignore any right-clicks that aren't on a row.
            if (hitTestInfo == null || hitTestInfo.RowIndex == -1) { return; }

            // Set the selected row, then show the context menu.
            ((BindingSource)gridView.DataSource).Position = hitTestInfo.RowIndex;
            contextMenu?.Show(gridView, e.Location);
        };
    }
}