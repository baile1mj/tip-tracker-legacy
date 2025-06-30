namespace TipTracker.Ui.Controls;

/// <summary>
/// Arguments associated with an event that triggers validation on a control's properties
/// when the event is fired.
/// </summary>
public class PropertyValidationEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name of the property that was invalid.
    /// </summary>
    public string PropertyName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether all properties passed validation.
    /// </summary>
    public bool IsValid => string.IsNullOrEmpty(PropertyName);

    /// <summary>
    /// Indicates that validation failed for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property that is invalid.</param>
    public void ValidationFailedFor(string propertyName)
    {
        ArgumentException.ThrowIfNullOrEmpty("The property name must be specified.", nameof(propertyName));
        PropertyName = propertyName;
    }
}