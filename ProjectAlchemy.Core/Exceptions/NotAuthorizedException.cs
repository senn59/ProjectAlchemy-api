namespace ProjectAlchemy.Core.Exceptions;

public class NotAuthorizedException : Exception
{
    public NotAuthorizedException() : base("Not authorized") {}
    public NotAuthorizedException(string message) : base(message) {}
};