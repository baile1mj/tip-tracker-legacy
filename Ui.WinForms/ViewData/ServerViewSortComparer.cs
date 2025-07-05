using System.ComponentModel;

namespace TipTracker.Ui.ViewData;

/// <summary>
/// Compares <see cref="ServerView"/> instances to allow sorting by instance properties.
/// </summary>
public class ServerViewSortComparer : SortComparerBase<ServerView>
{
    /// <summary>
    /// Creates a new instance of the comparer initialized to compare on the specified property.
    /// </summary>
    /// <param name="prop">The <see cref="ServerView"/> property on which to compare instances.</param>
    public ServerViewSortComparer(PropertyDescriptor prop) : base(prop)
    { }

    /// <inheritdoc />
    protected override Func<ServerView, ServerView, int> GetComparerDelegate()
    {
        switch (ComparisonProperty.Name)
        {
            case nameof(ServerView.LastName):
                return ServerView.CompareByLastName;
            case nameof(ServerView.FirstName):
                return ServerView.CompareByFirstName;
            default:
                return ServerView.CompareByNumber;
        }
    }

    /// <summary>
    /// Creates a new instance of the comparer, initialized to compare on the specified property.
    /// </summary>
    /// <param name="prop">The <see cref="ServerView"/> property on which to compare instances.</param>
    /// <returns>The new comparer.</returns>
    public static IComparer<ServerView> Create(PropertyDescriptor prop)
    {
        return new ServerViewSortComparer(prop);
    }

    /// <summary>
    /// The default comparer to use for <see cref="ServerView"/> instances.
    /// </summary>
    public static readonly ServerViewSortComparer DefaultComparer = new(GetDefaultProperty());

    /// <summary>
    /// Gets the default property to use when comparing <see cref="ServerView"/> instances.
    /// </summary>
    private static PropertyDescriptor GetDefaultProperty()
    {
        return TypeDescriptor.GetProperties(typeof(ServerView)).Find(nameof(ServerView.Number), false)!;
    }
}