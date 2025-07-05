using TipTracker.Ui.DataObjects;

namespace TipTracker.Ui.Controls;

/// <summary>
/// Arguments associated with the event that is fired when a control is querying
/// servers by server number.
/// </summary>
public class ServerQueryEventArgs : EventArgs
{
    /// <summary>
    /// Gets the server number being queried.
    /// </summary>
    public string ServerNumber { get; }

    /// <summary>
    /// Gets or sets the result of the query.
    /// </summary>
    public Server? LookupResult { get; set; }

    /// <summary>
    /// Creates a new instance of the event args class.
    /// </summary>
    /// <param name="serverNumber">The server number for the server being queried.</param>
    public ServerQueryEventArgs(string serverNumber)
    {
        ServerNumber = serverNumber;
    }
}