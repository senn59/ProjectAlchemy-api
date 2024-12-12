using FluentAssertions;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Helpers;

namespace ProjectAlchemy.CoreTests;

public class GuardTests
{
    [Test]
    public void TooShortStringThrowsError()
    {
        const string str = "12345";
        var action = () => Guard.AgainstLength(str, nameof(str), 40, 8);
        action.Should().Throw<InvalidArgument>();
    }
    
    [Test]
    public void TooLongStringThrowsError()
    {
        const string str = "12345";
        var action = () => Guard.AgainstLength(str, nameof(str), 2);
        action.Should().Throw<InvalidArgument>();
    }
    
    [Test]
    public void EmptyStringThrowsError()
    {
        const string str = "";
        var action = () => Guard.AgainstNullOrEmpty(str, nameof(str));
        action.Should().Throw<InvalidArgument>();
    }
    
    [Test]
    public void NullThrowsError()
    {
        const string? str = null;
        var action = () => Guard.AgainstNullOrEmpty(str, nameof(str));
        action.Should().Throw<InvalidArgument>();
    }
    
    [Test]
    public void DefaultMinimumLengthIs0()
    {
        const string str = "1";
        var action = () => Guard.AgainstLength(str, nameof(str), 1);
        action.Should().NotThrow();
    }
}