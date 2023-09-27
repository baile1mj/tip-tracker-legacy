namespace TipTracker
{
    internal class Result
    {
        public bool WasSuccessful { get; private set; } = true;

        public string ErrorMessage { get; private set; } = string.Empty;

        public static Result FromSuccess()
        {
            return new Result();
        }

        public static Result FromError(string message)
        {
            GuardAgainst.EmptyString(message, nameof(message));

            return new Result
            {
                WasSuccessful = false,
                ErrorMessage = message
            };
        }
    }
}
