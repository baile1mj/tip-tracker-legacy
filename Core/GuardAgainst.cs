namespace TipTracker;

public static class GuardAgainst
{
    public static void EmptyString(string value, string parameterName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("String value cannot be empty.", parameterName);
        }
    }

    public static void Null(object value, string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentException("Value cannot be null.", parameterName);
        }
    }
}