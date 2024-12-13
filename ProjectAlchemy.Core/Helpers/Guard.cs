using ProjectAlchemy.Core.Exceptions;

namespace ProjectAlchemy.Core.Helpers;

public static class Guard
{
    public static void AgainstLength(string argument, string argumentName, int maxLength)
    {
        if (argument.Length > maxLength)
        {
            throw new InvalidArgument($"{argumentName} has a maximum length of {maxLength}.");
        }
    }
    
    public static void AgainstLength(string argument, string argumentName, int minLength, int maxLength)
    {
        if (argument.Length < minLength || argument.Length > maxLength)
        {
            throw new InvalidArgument($"{argumentName} must be between {minLength} and {maxLength}");
        }
    }
    
    public static void AgainstNullOrEmpty(string? argument, string argumentName)
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new InvalidArgument(argumentName);
        }
    }
}
