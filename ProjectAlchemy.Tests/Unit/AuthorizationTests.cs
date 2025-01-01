
using FluentAssertions;
using Moq;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Tests.Unit;

public class AuthorizationTests
{
    [Fact]
    public async Task ValidMemberTryingToAccessProjectSucceeds()
    {
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.GetMember("project", "member")).ReturnsAsync(new Member
        {
            UserId = "member",
            Type = MemberType.Collaborator
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.ReadProject, "member", "project");
        
        await act.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task InvalidMemberTryingToAccessProjectThrowsNotAuthorizedException()
    {
        var validUser = Guid.NewGuid().ToString();
        var projectId = Guid.NewGuid().ToString();
        var invalidUser = Guid.NewGuid().ToString();
        var mock = new Mock<IProjectRepository>();
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.ReadProject, projectId, invalidUser);

        await act.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task ProjectOwnerTryingToAccessIssueInAnotherProjectThrowsNotFound()
    {
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue("project", 1)).ReturnsAsync(false);
        mock.Setup(p => p.GetMember("project", "owner")).ReturnsAsync(new Member
        {
            UserId = "owner",
            Type = MemberType.Owner
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.DeleteIssues, "owner", "project", 1);

        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task CollaboratorWantingToDeleteIssueThrowsNotAuthorizedException()
    {
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue("project", 1)).ReturnsAsync(true);
        mock.Setup(p => p.GetMember("project", "owner")).ReturnsAsync(new Member
        {
            UserId = "collaborator",
            Type = MemberType.Collaborator
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.DeleteIssues, "collaborator", "project", 1);
        
        await act.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task OwnerWantingToDeleteIssueSucceeds()
    {
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue("project", 1)).ReturnsAsync(true);
        mock.Setup(p => p.GetMember("project", "owner")).ReturnsAsync(new Member
        {
            UserId = "owner",
            Type = MemberType.Owner
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.DeleteIssues, "owner", "project", 1);
        
        await act.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task CollaboratorWantingToUpdateIssueSucceeds()
    {
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue("project", 1)).ReturnsAsync(true);
        mock.Setup(p => p.GetMember("project", "collaborator")).ReturnsAsync(new Member
        {
            UserId = "collaborator",
            Type = MemberType.Collaborator
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.UpdateIssues, "collaborator", "project", 1);

        await act.Should().NotThrowAsync();
    }
}