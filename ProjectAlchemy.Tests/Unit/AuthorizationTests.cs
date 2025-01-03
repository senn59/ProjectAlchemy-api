
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
        var projectId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.GetMember(projectId, memberId)).ReturnsAsync(new Member
        {
            UserId = memberId,
            Type = MemberType.Collaborator
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.ReadProject, memberId, projectId);
        
        await act.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task InvalidMemberTryingToAccessProjectThrowsNotAuthorizedException()
    {
        var projectId = Guid.NewGuid();
        var invalidUser = Guid.NewGuid();
        var mock = new Mock<IProjectRepository>();
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.ReadProject, projectId, invalidUser);

        await act.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task ProjectOwnerTryingToAccessIssueInAnotherProjectThrowsNotFound()
    {
        var projectId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue(projectId, 1)).ReturnsAsync(false);
        mock.Setup(p => p.GetMember(projectId, memberId)).ReturnsAsync(new Member
        {
            UserId = memberId,
            Type = MemberType.Owner
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.DeleteIssues, memberId, projectId, 1);

        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task CollaboratorWantingToDeleteIssueThrowsNotAuthorizedException()
    {
        var projectId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue(projectId, 1)).ReturnsAsync(true);
        mock.Setup(p => p.GetMember(projectId, memberId)).ReturnsAsync(new Member
        {
            UserId = memberId,
            Type = MemberType.Collaborator
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.DeleteIssues, memberId, projectId, 1);
        
        await act.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task OwnerWantingToDeleteIssueSucceeds()
    {
        var projectId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue(projectId, 1)).ReturnsAsync(true);
        mock.Setup(p => p.GetMember(projectId, memberId)).ReturnsAsync(new Member
        {
            UserId = memberId,
            Type = MemberType.Owner
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.DeleteIssues, memberId, projectId, 1);
        
        await act.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task CollaboratorWantingToUpdateIssueSucceeds()
    {
        var projectId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var mock = new Mock<IProjectRepository>();
        mock.Setup(p => p.HasIssue(projectId, 1)).ReturnsAsync(true);
        mock.Setup(p => p.GetMember(projectId, memberId)).ReturnsAsync(new Member
        {
            UserId = memberId,
            Type = MemberType.Collaborator
        });
        var service = new AuthorizationService(mock.Object);
        
        var act = () => service.Authorize(Permission.UpdateIssues, memberId, projectId, 1);

        await act.Should().NotThrowAsync();
    }
}