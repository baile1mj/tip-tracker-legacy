namespace TipTracker.Core.Reporting
{
    /// <summary>
    /// Represents a total for a tip type that will be included in a report.
    /// </summary>
    public class TipTypeTotal
    {
        /// <summary>
        /// Gets the type of tip being totaled.
        /// </summary>
        public TipType Type { get; }

        /// <summary>
        /// Gets the classification of the type being totaled.
        /// </summary>
        public TipClassification Classification { get; }

        /// <summary>
        /// Gets a value indicating whether tips of this type are claimed tips.
        /// </summary>
        public bool IsClaimed { get; }

        /// <summary>
        /// Gets the count of all tips of this type.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets the total amount of all tips of this type.
        /// </summary>
        public decimal Total { get; }

        /// <summary>
        /// Creates a new instance of the type total class.
        /// </summary>
        /// <param name="type">The type being totaled.</param>
        /// <param name="count">The number of tips of this type.</param>
        /// <param name="total">The total of all tips of this type.</param>
        public TipTypeTotal(TipType type, int count, decimal total)
        {
            Type = type;
            Classification = type.Classification;
            IsClaimed = type.IsClaimedTip;
            Count = count;
            Total = total;
        }
        
        /// <summary>
        /// Converts this instance to a loosely typed instance for reporting.
        /// </summary>
        /// <returns>An object representing this instance that can be passed to the reporting engine.</returns>
        public object ToAnonymous()
        {
            return new
            {
                Type = Type.Name,
                Classification = Classification.Description,
                IsClaimed = IsClaimed,
                Count = Count,
                Total = Total
            };
        }
    }
}