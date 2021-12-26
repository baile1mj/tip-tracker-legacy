using System;

namespace TipTracker.Core
{
    /// <summary>
    /// Represents a tip earned or claimed by a server.
    /// </summary>
    public class Tip
    {
        /// <summary>
        /// Gets or sets the server who earned the tip.
        /// </summary>
        public Server EarnedBy { get; set; }

        /// <summary>
        /// Gets or sets the tip amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date the tip was earned.
        /// </summary>
        public DateTime EarnedOn { get; set; }

        /// <summary>
        /// Gets or sets the type of tip earned.
        /// </summary>
        public TipType Type { get; set; }

        /// <summary>
        /// Gets or sets the event for which the tip was earned.
        /// </summary>
        public Event Event { get; set; }
    }
}