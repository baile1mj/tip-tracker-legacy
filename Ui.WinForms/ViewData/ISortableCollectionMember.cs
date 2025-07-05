namespace TipTracker.Ui.ViewData;

/// <summary>
/// Defines the interface for all UI object that are displayed as sortable collections
/// of data items.
/// </summary>
public interface ISortableCollectionMember
{
    /// <summary>
    /// Gets the ordinal identifier for the object in an unsorted collection of
    /// objects of the same type.
    /// </summary>
    int OrdinalId { get; }
}