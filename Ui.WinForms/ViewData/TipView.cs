using TipTracker.Ui.DataObjects;

namespace TipTracker.Ui.ViewData;

/// <summary>
/// Represents a tip to display on the UI form.
/// </summary>
public class TipView : ISortableCollectionMember
{
    private readonly Tip _tip;

    /// <summary>
    /// Gets the value indicating the order in which this tip was entered relative to other tips.
    /// </summary>
    public int OrdinalId { get; }

    /// <summary>
    /// Gets the server number for the server who claims this tip.
    /// </summary>
    public string ServerNumber => _tip.Server.PosId;

    /// <summary>
    /// Gets the last name of the server who claims this tip.
    /// </summary>
    public string LastName => _tip.Server.LastName;

    /// <summary>
    /// Gets the first name of the server who claims this tip.
    /// </summary>
    public string FirstName => _tip.Server.FirstName;

    /// <summary>
    /// Gets the amount of the tip.
    /// </summary>
    public decimal Amount => _tip.Amount;

    /// <summary>
    /// Creates a new tip for the UI view.
    /// </summary>
    /// <param name="ordinalId">The ordinal identifier for the tip.</param>
    /// <param name="server">The server to whom the tip belongs.</param>
    /// <param name="amount">The amount of the tip.</param>
    public TipView(int ordinalId, Server server, decimal amount)
        : this(ordinalId, new Tip(server, amount)) 
    { }

    /// <summary>
    /// Creates a new tip for the UI view.
    /// </summary>
    /// <param name="ordinalId">The ordinal identifier for the tip.</param>
    /// <param name="tip">The <see cref="Tip"/> instance to display in the UI.</param>
    public TipView(int ordinalId, Tip tip)
    {
        OrdinalId = ordinalId;
        _tip = tip;
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="OrdinalId"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByEntryOrder(TipView x, TipView y)
    {
        return y.OrdinalId - x.OrdinalId;
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="TipView.Amount"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByAmount(TipView x, TipView y)
    {
        return decimal.Compare(x.Amount, y.Amount);
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="TipView.ServerNumber"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByServerNumber(TipView x, TipView y)
    {
        return Server.CompareByNumber(x._tip.Server, y._tip.Server);
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="TipView.LastName"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByServerLastName(TipView x, TipView y)
    {
        return Server.CompareByLastName(x._tip.Server, y._tip.Server);
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="TipView.FirstName"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByServerFirstName(TipView x, TipView y)
    {
        return Server.CompareByFirstName(x._tip.Server, y._tip.Server);
    }
}