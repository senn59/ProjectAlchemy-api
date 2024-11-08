namespace ProjectAlchemy.Core.Helpers;

public class Guard
{
    public static void StringAgainstLength(string argument, string argumentName, int maxLength, int minLength = 0)
    {
        if (argument.Length < minLength || argument.Length > maxLength)
        {
            throw new ArgumentException($"{argumentName} must be between {minLength} and {maxLength}");
        }
    }

    public static void StringAgainstNullOrEmpty(string? argument, string argumentName)
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new ArgumentNullException(argumentName);
        }
    }
}