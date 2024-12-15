namespace ProjectAlchemy.Core.Exceptions;

public class InvalidArgumentException : Exception
{
    public InvalidArgumentException(string message): base(message) { }
    public InvalidArgumentException() { }
};
