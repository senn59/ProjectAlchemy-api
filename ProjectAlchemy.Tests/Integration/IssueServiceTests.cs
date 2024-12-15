using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

namespace ProjectAlchemy.Tests.Integration;

public class IssueServiceTests: IDisposable
{
    private readonly AppDbContext _context;
    private readonly IssueService _issueService;
    private readonly ProjectService _projectService;
    private Project _project = null!;
    private const string UserId = "1";
    
    public IssueServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("issueServiceTests")
            .Options;
        _context = new AppDbContext(options);
        var issueRepo = new IssueRepository(_context);
        var projectRepo = new ProjectRepository(_context);
        var authService = new AuthorizationService(projectRepo);
        _issueService = new IssueService(issueRepo, authService, projectRepo);
        _projectService = new ProjectService(projectRepo, authService);
        _ = CreateProject();
    }

    private async Task CreateProject()
    {
        _project = await _projectService.Create("test", "1");
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task CreateIssueSucceedsAndIssueIsAvailableToRetrieveAndAppearsInProject()
    {
        var issue = new Issue("test", IssueType.Task, _project.Lanes.First());
        
        var created = await _issueService.Create(issue, UserId, _project.Id);
        var retrieved = await _issueService.GetById(created.Id, UserId, _project.Id);
        var project = await _projectService.Get(_project.Id, UserId);
        
        created.Should().BeEquivalentTo(issue, options => options.Excluding(i => i.Id));
        retrieved.Should().BeEquivalentTo(created);
        project.Issues.First().Should().BeEquivalentTo(issue, options => options.Excluding(i => i.Id));
    }
    
    [Fact]
    public async Task DeletingNonExistingIssueThrowsNotFound()
    {
        var issue = new Issue("test", IssueType.Task, _project.Lanes.First());
        await _issueService.Create(issue, UserId, _project.Id);
        
        var action = () => _issueService.DeleteById(9999, UserId, _project.Id);

        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task DeletingExistingIssueAndRetrievingItThrowsNotFound()
    {
        var issue = new Issue("test", IssueType.Task, _project.Lanes.First());
        var created = await _issueService.Create(issue, UserId, _project.Id);
        await _issueService.DeleteById(created.Id, UserId, _project.Id);
        
        var action = () => _issueService.GetById(created.Id, UserId, _project.Id);
        
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task UpdatingIssueSucceeds()
    {
        var issue = new Issue("test", IssueType.Task, _project.Lanes.First());
        var created = await _issueService.Create(issue, UserId, _project.Id);
        
        created.SetName("nottest");
        created.SetDescription("not empty");
        created.SetType(IssueType.Bug);
        var updated = await _issueService.Update(created, UserId, _project.Id);
        
        created.Should().BeEquivalentTo(updated);
        updated.Should().NotBeEquivalentTo(issue, options => options.Excluding(i => i.Id));
    }
}