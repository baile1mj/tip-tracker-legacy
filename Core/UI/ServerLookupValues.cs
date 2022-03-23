using System.Collections.Generic;

namespace TipTracker.Core.UI
{
    /// <summary>
    /// Contains the set of values to display in the UI when a lookup list is provided to the user.
    /// </summary>
    public class ServerLookup
    {
        /// <summary>
        /// Gets the text to display when looking up a server by first name.
        /// </summary>
        public string ByFirstName { get; }

        /// <summary>
        /// Gets the text to display when looking up a server by last name.
        /// </summary>
        public string ByLastName { get; }

        /// <summary>
        /// Gets the text to display when looking up the server by their POS ID number.
        /// </summary>
        public string ByPosId { get; }

        /// <summary>
        /// Gets the collection of values that may be displayed in the UI for the server.
        /// </summary>
        public IEnumerable<string> Values
        {
            get
            {
                yield return ByFirstName;
                yield return ByLastName;
                yield return ByPosId;
            }
        }

        /// <summary>
        /// Gets the server represented by the lookup values.
        /// </summary>
        public Server Server { get; }

        /// <summary>
        /// Creates a new instance of the lookup values for a specified server.
        /// </summary>
        /// <param name="server">The server represented by the lookup values.</param>
        public ServerLookup(Server server)
        {
            ByFirstName = $"{server.FirstName} {server.LastName} ({server.PosId})";
            ByLastName = server.ToString();
            ByPosId = $"{server.PosId} ({server.LastName}, {server.FirstName})";
            Server = server;
        }
    }
}