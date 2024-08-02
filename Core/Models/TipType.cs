namespace Core.Models;

/// <summary>
/// Defines a type of tip to track.
/// </summary>
public class TipType
{
    /// <summary>
    /// Gets the name of the tip type.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the reason for tracking tips of this type.
    /// </summary>
    public TrackingReason TrackingReason { get; }
 
    /// <summary>
    /// Gets a value indicating how the date is determined for tips of this type.
    /// </summary>
    public DateDetermination Determination { get; }

    /// <summary>
    /// Gets a value indicating whether the date for tips of this type is derived or specified.
    /// </summary>
    public bool IsDateDerived => Determination == DateDetermination.BusinessDate;

    /// <summary>
    /// Gets a value indicating whether tips of this type originate from an event.
    /// </summary>
    public bool IsEventTip => Determination == DateDetermination.Event;

    /// <summary>
    /// Creates a new tip type.
    /// </summary>
    /// <param name="name">The name of the tip type.</param>
    /// <param name="trackingReason">The reason for tracking tips of this type.</param>
    /// <param name="dateDetermination">How the date is determined for tips of this type.</param>
    public TipType(string name, TrackingReason trackingReason, DateDetermination dateDetermination)
    {
        Determination = dateDetermination;
        TrackingReason = trackingReason;
        Name = name;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Name;
    }
}