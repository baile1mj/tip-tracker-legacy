using System;

namespace TipTracker.Core
{
    /// <summary>
    /// Represents an event for which a server may earn tips.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the date the event occurred.
        /// </summary>
        public DateTime Date { get; set; }
    }
}