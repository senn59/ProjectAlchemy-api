using FluentAssertions;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Helpers;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Tests.Unit;

public class ValidationHelperTests
{
    [Fact]
    public void TooLongStringNameThrowsArgumentException()
    {
        var newProjectRequest = new ProjectCreate
        {
            Name = new string('*', ProjectService.MaxNameLength + 1)
        };
        
        var act = () => ValidationHelper.Validate(newProjectRequest);

        act.Should().Throw<InvalidArgumentException>();
    }
    
    [Fact]
    public void EmptyStringNameThrowsArgumentException()
    {
        var newProjectRequest = new ProjectCreate
        {
            Name = new string("")
        };
        
        var act = () => ValidationHelper.Validate(newProjectRequest);

        act.Should().Throw<InvalidArgumentException>();
    }
    
    [Fact]
    public void PassingValidObjectSucceeds()
    {
        var newProjectRequest = new IssuePatch
        {
            Name = new string('*', IssueService.MaxNameLength),
            Description = new string('*', IssueService.MaxDescriptionLength),
            Lane = new Lane { Name = "Test Lane" },
            Type = IssueType.Task
        };
        
        var act = () => ValidationHelper.Validate(newProjectRequest);

        act.Should().NotThrow();
    }
}