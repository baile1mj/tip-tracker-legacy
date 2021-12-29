namespace TipTracker.Core
{
    /// <summary>
    /// Represents a type of tip.
    /// </summary>
    public class TipType
    {
        /// <summary>
        /// Gets the name of the tip type.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the classification for this tip type.
        /// </summary>
        public TipClassification Classification { get; }

        /// <summary>
        /// Gets a value indicating whether tips of this type are payable (or pre-paid) to the server.
        /// </summary>
        public bool IsPayable => Classification == TipClassification.ChargeTips;

        /// <summary>
        /// Gets a value indicating whether tips of this type are claimed by (or on behalf of)
        /// the server for tax purposes.  Claimed tips apply to the pay period and not a specific
        /// date within the pay period.
        /// </summary>
        public bool IsClaimedTip => !IsPayable;

        /// <summary>
        /// Gets a value indicating whether the user can specify the business date for tips of this type.
        /// </summary>
        public bool CanSpecifyDate => IsPayable && !IsEventOriginated;

        /// <summary>
        /// Gets a value indicating whether tips of this type originate from events.
        /// </summary>
        public bool IsEventOriginated { get; }

        /// <summary>
        /// Creates a new <see cref="TipType"/> instance.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="classification">The classification for the tip type.</param>
        /// <param name="canSpecifyDate">True if the user can specify the date for tips of this type; false if the date
        /// is calculated.</param>
        /// <param name="isEventOriginated">True if tips of this type originate from events; otherwise, false.</param>
        public TipType(string name, TipClassification classification, bool isEventOriginated)
        {
            Name = name;
            Classification = classification;
            IsEventOriginated = isEventOriginated;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}