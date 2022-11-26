using System;
using System.Collections.Generic;

namespace TipTracker.Core
{
    /// <summary>
    /// Contains data about a pay period.
    /// </summary>
    public class PayPeriod
    {
        private readonly List<Server> _servers = new List<Server>();
        private DateTime _businessDate;

        /// <summary>
        /// Gets the first day of the pay period.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// Gets the last day of the pay period.
        /// </summary>
        public DateTime End { get; }

        /// <summary>
        /// Gets or sets the current business date.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the specified business date falls outside the pay period.</exception>
        public DateTime BusinessDate
        {
            get => _businessDate;
            set
            {
                if (value < Start || value > End)
                {
                    throw new InvalidOperationException("The current date cannot be outside the pay period dates.");
                }

                _businessDate = value.Date;
            }
        }

        /// <summary>
        /// Gets the collection of servers for the pay period.
        /// </summary>
        public IReadOnlyList<Server> Servers => _servers.AsReadOnly();

        /// <summary>
        /// Creates a new instance of the pay period.
        /// </summary>
        /// <param name="startDate">The first day of the pay period.</param>
        /// <param name="endDate">The last day of the pay period.</param>
        /// <param name="servers">The collection of servers who received tips in the period.</param>
        public PayPeriod(DateTime startDate, DateTime endDate, List<Server> servers)
            : this(startDate, endDate, startDate, servers)
        { }

        /// <summary>
        /// Creates a new instance of the pay period.
        /// </summary>
        /// <param name="startDate">The first day of the pay period.</param>
        /// <param name="endDate">The last day of the pay period.</param>
        /// <param name="businessDate">The current business date for the pay period.</param>
        /// <param name="servers">The collection of servers who received tips in the period.</param>
        public PayPeriod(DateTime startDate, DateTime endDate, DateTime businessDate, List<Server> servers)
        {
            // Ignore the time portion since it will only throw things off.
            startDate = startDate.Date;
            endDate = endDate.Date;
            businessDate = businessDate.Date;

            if (endDate < startDate)
            {
                throw new InvalidOperationException("The pay period cannot end before it begins.");
            }

            if (servers != null)
            {
                _servers.AddRange(servers);
            }

            Start = startDate;
            End = endDate;
            BusinessDate = businessDate;
        }
    }
}