using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Helpers;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;
using Xunit.Abstractions;

namespace ProjectAlchemy.Tests.Integration;

public class IssueServiceTests: IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AppDbContext _context;
    private readonly IssueService _issueService;
    private readonly ProjectService _projectService;
    private Project _project = null!;
    private readonly Guid _userId = Guid.NewGuid();
    
    public IssueServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);
        var issueRepo = new IssueRepository(_context);
        var projectRepo = new ProjectRepository(_context);
        var authService = new AuthorizationService(projectRepo);
        _issueService = new IssueService(issueRepo, authService);
        _projectService = new ProjectService(projectRepo, authService);
        CreateProject().GetAwaiter().GetResult();
    }

    private async Task CreateProject()
    {
        _project = await _projectService.Create("test", _userId, "test@test.com");
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

    [Fact]
    public async Task LinkingMultipleIssuesTogetherProperlyLinksThemAll()
    {
        List<IssueLink> linkObjects = [];
        for (var i = 0; i < 5; i++)
        {
            var newIssue = new IssueCreate
            {
                Name = "Test" + i,
                LaneId = _project.Lanes.First().Id,
                Type = IssueType.Task
            };
            var created = await _issueService.Create(newIssue, _userId, _project.Id);
            linkObjects.Add(new IssueLink
            {
                Key = created.Key,
                Name = created.Name,
                Type = created.Type
            });
        }
        _project = await _projectService.Get(_project.Id, _userId);
        var toLink = _project.Issues.Skip(1).Select(i => i.Key).ToList();
        var sourceIssue = _project.Issues.First();
        
        await _issueService.LinkIssues(sourceIssue.Key, toLink, _userId, _project.Id);
        
        var retrieved = await _issueService.GetByKey(sourceIssue.Key, _userId, _project.Id);
        retrieved.RelatedIssues.Should().BeEquivalentTo(linkObjects.Skip(1));
        foreach (var i in linkObjects.Skip(1))
        {
            var issue = await _issueService.GetByKey(i.Key, _userId, _project.Id);
            issue.RelatedIssues.Should().BeEquivalentTo([linkObjects.First()]);
        }
    }
}