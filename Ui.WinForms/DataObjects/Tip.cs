namespace TipTracker.Ui.DataObjects;

/// <summary>
/// Contains data about a tip.
/// </summary>
public class Tip
{
    /// <summary>
    /// Gets the server who claims this tip.
    /// </summary>
    public Server Server { get; }

    /// <summary>
    /// Gets the amount of the tip.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Creates a new instance of the tip.
    /// </summary>
    /// <param name="server">The server who claims the tip.</param>
    /// <param name="amount">The amount of the tip.</param>
    public Tip(Server server, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(server);

        Server = server;
        Amount = amount;
    }

}