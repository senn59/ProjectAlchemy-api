using FluentAssertions;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Tests.Unit;

public class MemberTests
{
    [Fact]
    public void OwnerCanUpdateIssuesReturnsTrue()
    {
        var member = new Member("1", MemberType.Owner);
        
        member.CanUpdateIssues().Should().BeTrue();
    }

    [Fact]
    public void OwnerCanDeleteIssuesReturnsTrue()
    {
        var member = new Member("1", MemberType.Owner);
        
        member.CanDeleteIssues().Should().BeTrue();
    }

    [Fact]
    public void CollaboratorCanUpdateIssuesReturnsTrue()
    {
        var member = new Member("1", MemberType.Collaborator);
        
        member.CanUpdateIssues().Should().BeTrue();
    }

    [Fact]
    public void CollaboratorCanDeleteIssuesReturnsFalse()
    {
        var member = new Member("1", MemberType.Collaborator);
        
        member.CanDeleteIssues().Should().BeFalse();
    }
}