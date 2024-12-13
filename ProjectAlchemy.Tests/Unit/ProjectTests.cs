using System.Diagnostics;
using FluentAssertions;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;

namespace ProjectAlchemy.Tests.Unit;

public class ProjectTests
{
    [Fact]
    public void CreatingAProjectWithNoMemberSpecifiedThrowsError()
    {
        var action = () => new Project("test", [], [], []);
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void CreatingAProjectWithNoOwnersThrowsError()
    {
        List<Member> members = [new Member("1", MemberType.Collaborator), new Member("2", MemberType.Collaborator)];
        var action = () => new Project("test", [], members, []);
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void CreatingProjectWithOwnerSucceeds()
    {
        var owner = new Member("1", MemberType.Owner); 
        var project = new Project("test", [], [owner], []);
        project.Name.Should().BeEquivalentTo("test");
        project.Members.Should().BeEquivalentTo([owner]);
        project.Issues.Should().BeEmpty();
        project.Lanes.Should().BeEmpty();
    }
    
    [Fact]
    public void CollaboratorAddingANewMemberToProjectThrowsError()
    {
        var owner = new Member("1", MemberType.Owner);
        var collaborator = new Member("2", MemberType.Collaborator);
        var project = new Project("test", [], [owner, collaborator], []);
        var action = () => project.TryAddMember(collaborator, new Member("3", MemberType.Collaborator));
        action.Should().Throw<NotAuthorizedException>();
    }
    
    [Fact]
    public void OwnerAddingNewMemberSucceeds()
    {
        var owner = new Member("1", MemberType.Owner);
        var collaborator = new Member("2", MemberType.Collaborator);
        var project = new Project("test", [], [owner], []);
        project.TryAddMember(owner, collaborator);
        project.Members.Should().BeEquivalentTo([owner, collaborator]);
    }
    
    [Fact]
    public void OwnerAddingAnotherOwnerThrowsError()
    {
        var owner = new Member("1", MemberType.Owner);
        var secondOwner = new Member("2", MemberType.Owner);
        var project = new Project("test", [], [owner], []);
        var action = () => project.TryAddMember(owner, secondOwner);
        action.Should().Throw<ArgumentException>();
    }
}