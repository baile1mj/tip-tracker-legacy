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

        /// <summary>
        /// Gets a value indicating whether two <see cref="Event"/> instances represent
        /// the same event.
        /// </summary>
        /// <param name="first">The first instance to check.</param>
        /// <param name="second">The second instance to check.</param>
        /// <returns>True if the two events are equivalent; otherwise, false.</returns>
        public static bool AreEquivalent(Event first, Event second)
        {
            return first?.Name == second?.Name;
        }
    }
}