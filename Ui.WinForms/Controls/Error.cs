namespace TipTracker.Ui.Controls;

/// <summary>
/// Represents an error message to display in the UI.
/// </summary>
public class Error
{
    private const string DEFAULT_MESSAGE = "An unspecified error has occurred.";
    private const string DEFAULT_SUMMARY = "Error";

    /// <summary>
    /// Gets the error message to display.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the error summary.
    /// </summary>
    public string Summary { get; }

    /// <summary>
    /// Creates a new generic error.
    /// </summary>
    private Error() : this(DEFAULT_MESSAGE, DEFAULT_SUMMARY)
    { }

    /// <summary>
    /// Creates a new error with a specified message and summary.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="summary">The error summary.</param>
    public Error(string message, string summary)
    {
        Message = !string.IsNullOrWhiteSpace(message) ? message : DEFAULT_MESSAGE;
        Summary = !string.IsNullOrWhiteSpace(summary) ? summary : DEFAULT_SUMMARY;
    }

    /// <summary>
    /// Provides a pass-through that ensures an error instance is never null.
    /// </summary>
    /// <param name="instance">The instance for which to guarantee non-null.</param>
    /// <returns>The provided error, if not null or a default error instance.</returns>
    public static Error Get(Error instance)
    {
        instance ??= new();
        return instance;
    }
}