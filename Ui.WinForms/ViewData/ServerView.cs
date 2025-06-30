using TipTracker.Ui.DataObjects;

namespace TipTracker.Ui.ViewData;

/// <summary>
/// Represents a server to display on the UI form.
/// </summary>
public class ServerView
{
    private readonly Server _server;

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
    /// <param name="server">The underlying business object containing the server's data.</param>
    public ServerView(Server server)
    {
        ArgumentNullException.ThrowIfNull(server);
        _server = server;
    }
}