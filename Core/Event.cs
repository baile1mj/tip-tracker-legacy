using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Gets or sets the tips earned from working the event.
        /// </summary>
        public List<Tip> Tips { get; set; } = new List<Tip>();

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} ({Date:M/d/yyyy})";
        }

        /// <summary>
        /// Creates a shallow copy of this instance.
        /// </summary>
        /// <returns>A new <see cref="Event"/> that is a copy of this instance.</returns>
        public Event Clone()
        {
            return new Event
            {
                Name = Name,
                Date = Date,
                Tips = new List<Tip>(Tips)
            };
        }
    }
}