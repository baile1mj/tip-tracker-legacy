namespace Core.Tests;

public class TestBase
{
    protected Exception? TryInvoke(Action? delegated)
    {
        try
        {
            delegated?.Invoke();
            return null;
        }
        catch(Exception ex)
        {
            return ex;
        }
    }
}