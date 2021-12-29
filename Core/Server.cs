using System.Collections.Generic;

namespace TipTracker.Core
{
    /// <summary>
    /// Represents a server who receives tips.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Gets or sets the value (usually numeric) used to identify the server in the
        /// point-of-sale system.
        /// </summary>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the server's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the server's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip generating a tip chit
        /// for this server.
        /// </summary>
        public bool SuppressChit { get; set; }

        /// <summary>
        /// Gets the collection of tips earned by the server.
        /// </summary>
        public List<Tip> Tips { get; } = new List<Tip>();

        /// <summary>
        /// Creates a shallow copy of this instance.
        /// </summary>
        /// <returns>The copy of this server.</returns>
        public Server Clone()
        {
            var clone = new Server
            {
                PosId = PosId,
                FirstName = FirstName,
                LastName = LastName,
                SuppressChit = SuppressChit
            };

            clone.Tips.AddRange(Tips);

            return clone;
        }

        /// <summary>
        /// Creates a shallow copy of this instance with the specified tips.
        /// </summary>
        /// <param name="tips">The tips to add to the instance.</param>
        /// <returns>The copy of this server.</returns>
        public Server Clone(IEnumerable<Tip> tips)
        {
            var clone = this.Clone();
            clone.Tips.Clear();
            clone.Tips.AddRange(tips);

            return clone;
        }

        /// <summary>
        /// Gets a value indicating whether another server is the same as this server.
        /// </summary>
        /// <param name="other">The other server to check.</param>
        /// <returns>True if the two servers are the same; otherwise, false.</returns>
        public bool Is(Server other)
        {
            return other?.PosId == PosId;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is equivalent to another instance and that
        /// the information about the server is identical between the two instances.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Matches(Server other)
        {
            return other != null 
                && Is(other)
                && FirstName == other.FirstName
                && LastName == other.LastName
                && SuppressChit == other.SuppressChit;
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return $"{LastName}, {FirstName} ({PosId})";
        }
    }
}