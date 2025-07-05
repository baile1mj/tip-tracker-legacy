using TipTracker.Ui.DataObjects;

namespace TipTracker.Ui.ViewData;

/// <summary>
/// Represents a server to display on the UI form.
/// </summary>
public class ServerView : ISortableCollectionMember
{
    private readonly Server _server;

    /// <inheritdoc />
    public int OrdinalId { get; }

    /// <summary>
    /// Gets the server's server number.
    /// </summary>
    public string Number => _server.PosId;

    /// <summary>
    /// Gets the server's last name.
    /// </summary>
    public string LastName => _server.LastName;

    /// <summary>
    /// Gets the server's first name.
    /// </summary>
    public string FirstName => _server.FirstName;

    /// <summary>
    /// Creates a new instance of the view data model.
    /// </summary>
    /// <param name="ordinalId">The ordinal identifier for the server.</param>
    /// <param name="server">The underlying business object containing the server's data.</param>
    public ServerView(int ordinalId, Server server)
    {
        ArgumentNullException.ThrowIfNull(server);
        this.OrdinalId = ordinalId;
        _server = server;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _server.ToString();
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="ServerView.Number"/> property.</summary>
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
    public static int CompareByNumber(ServerView x, ServerView y)
    {
        return Server.CompareByNumber(x._server, y._server);
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="ServerView.LastName"/> property.</summary>
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
    public static int CompareByLastName(ServerView x, ServerView y)
    {
        return Server.CompareByLastName(x._server, y._server);
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="ServerView.FirstName"/> property.</summary>
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
    public static int CompareByFirstName(ServerView x, ServerView y)
    {
        return Server.CompareByFirstName(x._server, y._server);
    }
}