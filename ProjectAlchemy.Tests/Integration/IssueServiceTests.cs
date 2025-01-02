using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Helpers;
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
    private readonly Guid _userId = Guid.NewGuid();
    
    public IssueServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("issueServiceTests")
            .Options;
        _context = new AppDbContext(options);
        var issueRepo = new IssueRepository(_context);
        var projectRepo = new ProjectRepository(_context);
        var authService = new AuthorizationService(projectRepo);
        _issueService = new IssueService(issueRepo, authService);
        _projectService = new ProjectService(projectRepo, authService);
        _ = CreateProject();
    }

    private async Task CreateProject()
    {
        _project = await _projectService.Create("test", _userId);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task CreateIssueSucceedsAndIssueIsAvailableToRetrieveAndAppearsInProject()
    {
        var issue = new IssueCreate
        {
            Name = "test",
            Type = IssueType.Task,
            LaneId = _project.Lanes.First().Id
        };
        
        var created = await _issueService.Create(issue, _userId, _project.Id);
        var retrieved = await _issueService.GetByKey(created.Key, _userId, _project.Id);

        created.Name.Should().BeEquivalentTo(issue.Name);
        created.Type.Should().HaveSameValueAs(issue.Type);
        retrieved.Should().BeEquivalentTo(created);
    }
    
    [Fact]
    public async Task DeletingNonExistingIssueThrowsNotFound()
    {
        var issue = new IssueCreate
        {
            Name = "Test",
            Type = IssueType.Task,
            LaneId = _project.Lanes.First().Id
        };
        var created = await _issueService.Create(issue, _userId, _project.Id);
        
        var action = () => _issueService.DeleteByKey(9999, _userId, _project.Id);

        created.Key.Should().NotBe(9999);
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task DeletingExistingIssueAndRetrievingItThrowsNotFound()
    {
        var issue = new IssueCreate
        {
            Name = "Test",
            Type = IssueType.Task,
            LaneId = _project.Lanes.First().Id
        };
        var created = await _issueService.Create(issue, _userId, _project.Id);
        await _issueService.DeleteByKey(created.Key, _userId, _project.Id);
        
        var action = () => _issueService.GetByKey(created.Key, _userId, _project.Id);
        
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task UpdatingIssueSucceeds()
    {
        var issue = new IssueCreate
        {
            Name = "Test",
            Type = IssueType.Task,
            LaneId = _project.Lanes.First().Id
        };
        var created = await _issueService.Create(issue, _userId, _project.Id);
        var retrieved = await _issueService.GetByKey(created.Key, _userId, _project.Id);

        retrieved.Name = "nottest";
        retrieved.Description = "not empty";
        retrieved.Type = IssueType.Bug;
        ValidationHelper.Validate(retrieved);
        var updated = await _issueService.Update(retrieved, _userId, _project.Id);
        
        updated.Should().BeEquivalentTo(retrieved);
        created.Name.Should().NotBeEquivalentTo(updated.Name);
        created.Type.Should().NotHaveSameValueAs(updated.Type);
    }
}