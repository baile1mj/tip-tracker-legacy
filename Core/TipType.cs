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
        /// Gets a value indicating whether the user can specify the business date for tips of this type.
        /// </summary>
        public bool CanSpecifyDate { get; }

        /// <summary>
        /// Gets a value indicating whether tips of this type originate from events.
        /// </summary>
        public bool IsEventOriginated { get; }

        /// <summary>
        /// Creates a new <see cref="TipType"/> instance.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="canSpecifyDate">True if the user can specify the date for tips of this type; false if the date
        /// is calculated.</param>
        /// <param name="isEventOriginated">True if tips of this type originate from events; otherwise, false.</param>
        public TipType(string name, bool canSpecifyDate, bool isEventOriginated)
        {
            Name = name;
            CanSpecifyDate = canSpecifyDate;
            IsEventOriginated = isEventOriginated;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}