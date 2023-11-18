namespace Core.Models;

/// <summary>
/// The collection of methods for determining which date is applied to a tip.
/// </summary>
public enum DateDetermination
{
    /// <summary>
    /// The tip is recorded for a particular business date.
    /// </summary>
    BusinessDate = 0,

    /// <summary>
    /// The tip's date is determined by the business date on which an event occurred.
    /// </summary>
    Event = 1,

    /// <summary>
    /// The tip is recorded on the first day of the pay period.
    /// </summary>
    PayPeriodStart = 2,

    /// <summary>
    /// The tip is recorded on the last day of the pay period.
    /// </summary>
    PayPeriodEnd = 3
}

/// <summary>
/// The collection of reasons that a tip is tracked.
/// </summary>
public enum TrackingReason
{
    /// <summary>
    /// The tip is owed to a server.
    /// </summary>
    Owed = 0,

    /// <summary>
    /// The tip is claimed as income for a server.
    /// </summary>
    Claimed = 1,

    /// <summary>
    /// The tip is reported for informational purposes only.
    /// </summary>
    Reference = 2
}