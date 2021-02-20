using System.Collections.Generic;

namespace TipTracker.Core
{
    /// <summary>
    /// Represents the classification of a tip type.
    /// </summary>
    public sealed class TipClassification
    {
        /// <summary>
        /// Gets the description of the type.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the position for this classification when sorting tips.
        /// </summary>
        public int SortIndex { get; }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="description">The description of the classification.</param>
        /// <param name="sortIndex">The position of this instance when sorting.</param>
        private TipClassification(string description, int sortIndex)
        {
            Description = description;
            SortIndex = sortIndex;
        }

        /// <summary>
        /// Represents charge tips, which are payable at the end of the pay period.
        /// </summary>
        public static readonly TipClassification ChargeTips = new TipClassification("Charge Tips", 0);

        /// <summary>
        /// Represents cash tips, which are only claimed (not paid) at the end of the pay period.
        /// </summary>
        public static readonly TipClassification CashTips = new TipClassification("Cash Tips", 1);

        /// <summary>
        /// Gets the collection of defined tip classes.
        /// </summary>
        public static IEnumerable<TipClassification> Classes
        {
            get
            {
                yield return ChargeTips;
                yield return CashTips;
            }
        }
    }
}