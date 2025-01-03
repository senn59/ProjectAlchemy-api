using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

namespace ProjectAlchemy.Tests.Integration;

public class InvitationServiceTests: IDisposable
{
    private readonly AppDbContext _context;
    private readonly InvitationService _inviteService;
    private readonly ProjectService _projectService;
    private readonly UserService _userService;
    private readonly Guid _ownerId = Guid.NewGuid();
    private Project _project;
    
    public InvitationServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("laneServiceTests")
            .Options;
        _context = new AppDbContext(options);
        var inviteRepo = new InvitationRepository(_context);
        var projectRepo = new ProjectRepository(_context);
        var authService = new AuthorizationService(projectRepo);
        var memberRepo = new MemberRepository(_context);
        _inviteService = new InvitationService(inviteRepo, authService, projectRepo);
        _projectService = new ProjectService(projectRepo, authService);
        _userService = new UserService(memberRepo);
        CreateProject().GetAwaiter().GetResult();
    }

    private async Task CreateProject()
    {
        _project = await _projectService.Create("test", _ownerId);
        
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task TryingToSendInvitationWorks()
    {
        await _inviteService.Invite(_ownerId, "test@test.com", _project.Id);
        var invites = await _inviteService.GetInvitedEmails(_project.Id, _ownerId);
        var userInvitation =  await _userService.GetInvitations("test@test.com");

        invites.Should().HaveCount(1);
        invites[0].Email.Should().Be("test@test.com");
        userInvitation.Should().HaveCount(1);
        invites[0].InvitationId.Should().Be(userInvitation[0].InvitationId);
    }
    
    [Fact]
    public async Task RejectingInvitationDeletesTheInvitationForEveryone()
    {
        var invite = await _inviteService.Invite(_ownerId, "test@test.com", _project.Id);
        
        await _inviteService.Reject(invite.InvitationId, "test@test.com");
        
        var invites = await _inviteService.GetInvitedEmails(_project.Id, _ownerId);
        var userInvitations =  await _userService.GetInvitations("test@test.com");
        invites.Should().BeEmpty();
        userInvitations.Should().BeEmpty();
    }
    
    [Fact]
    public async Task AcceptingTheInvitationAddsUserToProject()
    {
        var newMemberId = Guid.NewGuid();
        var invite = await _inviteService.Invite(_ownerId, "test@test.com", _project.Id);
        
        var projectId = await _inviteService.Accept(invite.InvitationId, "test@test.com", newMemberId);

        var project = await _projectService.Get(projectId, newMemberId);
        project.Members.Should().HaveCount(2);
        projectId.Should().Be(_project.Id);
        project.Members.Should().ContainEquivalentOf(new Member
        {
            Type = MemberType.Collaborator,
            UserId = newMemberId
        });
    }
    
    [Fact]
    public async Task AcceptingTheSameInviteTwiceThrowsNotFound()
    {
        var newMemberId = Guid.NewGuid();
        var invite = await _inviteService.Invite(_ownerId, "test@test.com", _project.Id);
        
        var acceptFirst = () => _inviteService.Accept(invite.InvitationId, "test@test.com", newMemberId);
        await acceptFirst.Should().NotThrowAsync();
        await acceptFirst.Should().ThrowExactlyAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task SendingAnInvitationToTheSameEmailTwiceThrowsAlreadyExisting()
    {
        var invite = () =>  _inviteService.Invite(_ownerId, "test@test.com", _project.Id);

        await invite.Should().NotThrowAsync();
        await invite.Should().ThrowExactlyAsync<AlreadyExistsException>();
    }
    
    [Fact]
    public async Task JoiningAProjectTwiceDoesNotAddAnAdditionalMember()
    {
        var newMemberId = Guid.NewGuid();
        var invite = await _inviteService.Invite(_ownerId, "test@test.com", _project.Id);
        await _inviteService.Accept(invite.InvitationId, "test@test.com", newMemberId);
        var secondInvite = await _inviteService.Invite(_ownerId, "test@test.com", _project.Id);
        await _inviteService.Accept(secondInvite.InvitationId, "test@test.com", newMemberId);
        
        var project = await _projectService.Get(_project.Id, newMemberId);
        project.Members.Should().HaveCount(2);
        project.Members.Select(m => m.UserId).Should().OnlyHaveUniqueItems();
    }
    
    [Fact]
    public async Task ProjectOwnerCancelingInvitationWorks()
    {
        var newMemberId = Guid.NewGuid();
        
        var invite = await _inviteService.Invite(_ownerId, "test@test.com", _project.Id);
        await _inviteService.Cancel(invite.InvitationId, _ownerId, _project.Id);
        
        var tryToAcceptCancelledInvite = () => _inviteService.Accept(invite.InvitationId, "test@test.com", newMemberId);
        
        var invites = await _inviteService.GetInvitedEmails(_project.Id, _ownerId);
        invites.Should().BeEmpty();
        await tryToAcceptCancelledInvite.Should().ThrowExactlyAsync<NotFoundException>();
    }
}