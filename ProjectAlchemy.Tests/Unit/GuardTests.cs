using FluentAssertions;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Helpers;

namespace ProjectAlchemy.Tests.Unit;

public class GuardTests
{
    [Fact]
    public void ThrowsErrorWhenStringIsShorterThanMinimumLength()
    {
        const string str = "12345";
        
        var action = () => Guard.AgainstLength(str, nameof(str), 40, 8);
        
        action.Should().Throw<InvalidArgument>();
    }
    
    [Fact]
    public void ThrowsErrorWhenStringExceedsMaximumLength()
    {
        const string str = "12345";
        
        var action = () => Guard.AgainstLength(str, nameof(str), 2);
        
        action.Should().Throw<InvalidArgument>();
    }
    
    [Fact]
    public void ThrowsErrorWhenStringIsEmpty()
    {
        const string str = "";
        
        var action = () => Guard.AgainstNullOrEmpty(str, nameof(str));
        
        action.Should().Throw<InvalidArgument>();
    }
    
    [Fact]
    public void ThrowsErrorWhenStringIsNull()
    {
        const string? str = null;
        
        var action = () => Guard.AgainstNullOrEmpty(str, nameof(str));
        
        action.Should().Throw<InvalidArgument>();
    }
    
    [Fact]
    public void DoesNotThrowWhenStringMeetsDefaultMinimumLength()
    {
        const string str = "1";
        
        var action = () => Guard.AgainstLength(str, nameof(str), 1);
        
        action.Should().NotThrow();
    }
}