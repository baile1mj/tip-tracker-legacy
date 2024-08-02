using Core.Validation;

namespace Core.Tests.Validation;

public class GuardAgainstShould : TestBase
{
    [Fact]
    public void CorrectlyHandleNonPopulatedStringValues()
    {
        string testNullArg = null;
        string testEmptyString = string.Empty;

        var nullStringResult = TryInvoke(() => GuardAgainst.NullOrEmptyString(testNullArg));
        var emptyStringResult = TryInvoke(() => GuardAgainst.NullOrEmptyString(testEmptyString));

        Assert.IsType<ArgumentNullException>(nullStringResult);
        Assert.Equal(nameof(testNullArg), (nullStringResult as ArgumentNullException)?.ParamName);

        Assert.IsType<ArgumentNullException>(emptyStringResult);
        Assert.Equal(nameof(testEmptyString), (emptyStringResult as ArgumentNullException)?.ParamName);
    }

    [Fact]
    public void CorrectlyHandlePopulatedStringValues()
    {
        var testString = "Hello";
        var result = TryInvoke(() => GuardAgainst.NullOrEmptyString(testString));

        Assert.Null(result);
    }
}