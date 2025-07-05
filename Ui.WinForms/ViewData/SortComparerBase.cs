using System.ComponentModel;

namespace TipTracker.Ui.ViewData;

/// <summary>
/// Provides a generic base class for delegating the comparison of two instances
/// of the specified type.
/// </summary>
/// <typeparam name="T">The type of instances to compare.</typeparam>
public abstract class SortComparerBase<T> : IComparer<T>
{
    /// <summary>
    /// Gets the property on which comparisons will be made.
    /// </summary>
    protected PropertyDescriptor ComparisonProperty { get; }

    /// <summary>
    /// Creates a new instance of the generic base class.
    /// </summary>
    /// <param name="comparisonProperty">The property on which the comparison will be made.</param>
    protected SortComparerBase(PropertyDescriptor comparisonProperty)
    {
        ComparisonProperty = comparisonProperty;
    }
    
    /// <summary>
    /// Gets the delegated method to use to perform the comparison.
    /// </summary>
    /// <returns></returns>
    protected abstract Func<T, T, int> GetComparerDelegate();

    /// <inheritdoc />
    public int Compare(T? x, T? y)
    {
        if (x == null ^ y == null) { return x == null ? -1 : 1; }
        if (x == null && y == null) { return 0; }

        return GetComparerDelegate().Invoke(x!, y!);
    }

}