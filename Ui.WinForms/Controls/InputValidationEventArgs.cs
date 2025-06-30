namespace TipTracker.Ui.Controls;

/// <summary>
/// Arguments associated with a data input event that triggers validation when the event is fired.
/// </summary>
/// <typeparam name="T">The type of input to validate.</typeparam>
public class InputValidationEventArgs<T> : EventArgs
{
    /// <summary>
    /// Gets the input that was entered.
    /// </summary>
    public T Input { get; }

    /// <summary>
    /// True if the input was valid; otherwise, false.
    /// </summary>
    public bool IsInputValid { get; private set; } = true;

    /// <summary>
    /// Creates a new instance of the event args class.
    /// </summary>
    /// <param name="input">The input associated with the event.</param>
    public InputValidationEventArgs(T input)
    {
        Input = input;
    }
    
    /// <summary>
    /// Flags the entered input as invalid.
    /// </summary>
    public void Invalidate()
    {
        IsInputValid = false;
    }
}

/// <summary>
/// Static factory class to allow type inference for the <see cref="InputValidationEventArgs{T}"/>
/// type.
/// </summary>
public static class InputValidationEventArgs
{
    /// <summary>
    /// Creates a new <see cref="InputValidationEventArgs{T}"/> instance for the specified input value.
    /// </summary>
    /// <typeparam name="T">The type of input to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>A generic event args instance containing the input.</returns>
    public static InputValidationEventArgs<T> ForValue<T>(T value)
    {
        return new InputValidationEventArgs<T>(value);
    }
}