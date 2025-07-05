using System.ComponentModel;

namespace TipTracker.Ui.ViewData;

/// <summary>
/// Compares <see cref="TipView"/> instances to allow sorting by instance properties.
/// </summary>
public class TipViewSortComparer : SortComparerBase<TipView>
{
    /// <summary>
    /// Creates a new instance of the comparer, initialized to compare on the specified property.
    /// </summary>
    /// <param name="prop">The <see cref="TipView"/> property on which to compare instances.</param>
    public TipViewSortComparer(PropertyDescriptor prop) : base(prop)
    { }

    /// <inheritdoc />
    protected override Func<TipView, TipView, int> GetComparerDelegate()
    {
        switch (ComparisonProperty.Name)
        {
            case nameof(TipView.Amount):
                return TipView.CompareByAmount;
            case nameof(TipView.ServerNumber):
                return TipView.CompareByServerNumber;
            case nameof(TipView.LastName):
                return TipView.CompareByServerLastName;
            case nameof(TipView.FirstName):
                return TipView.CompareByServerFirstName;
            default:
                return TipView.CompareByEntryOrder;
        }
    }

    /// <summary>
    /// Creates a new instance of the comparer, initialized to compare on the specified property.
    /// </summary>
    /// <param name="prop">The <see cref="TipView"/> property on which to compare instances.</param>
    /// <returns>The new comparer.</returns>
    public static IComparer<TipView> Create(PropertyDescriptor prop)
    {
        return new TipViewSortComparer(prop);
    }

    /// <summary>
    /// The default comparer to use for <see cref="TipView"/> instances.
    /// </summary>
    public static readonly TipViewSortComparer DefaultComparer = new(GetDefaultProperty());

    /// <summary>
    /// Gets the default property to use when comparing <see cref="TipView"/> instances.
    /// </summary>
    /// <returns></returns>
    public static PropertyDescriptor GetDefaultProperty()
    {
        return TypeDescriptor
            .GetProperties(typeof(TipView))
            .Find(nameof(TipView.Number), false)!;
    }
}