using TipTracker.Ui.DataObjects;

namespace TipTracker.Ui.ViewData;

/// <summary>
/// Represents a tip to display on the UI form.
/// </summary>
public class TipView
{
    private readonly Tip _tip;

    /// <summary>
    /// Gets the value indicating the order in which this tip was entered relative to other tips.
    /// </summary>
    public int Number { get; }

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
    /// <param name="number">The ordinal identifier for the tip.</param>
    /// <param name="server">The server to whom the tip belongs.</param>
    /// <param name="amount">The amount of the tip.</param>
    public TipView(int number, Server server, decimal amount)
        : this(number, new Tip(server, amount)) 
    { }

    /// <summary>
    /// Creates a new tip for the UI view.
    /// </summary>
    /// <param name="number">The ordinal identifier for the tip.</param>
    /// <param name="tip">The <see cref="Tip"/> instance to display in the UI.</param>
    public TipView(int number, Tip tip)
    {
        Number = number;
        _tip = tip;
    }
}