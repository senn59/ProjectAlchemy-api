using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

namespace ProjectAlchemy.Tests.Integration;

public class LaneServiceTests: IDisposable
{
    private readonly AppDbContext _context;
    private readonly LaneService _laneService;
    private readonly ProjectService _projectService;
    
    public LaneServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);
        var laneRepo = new LaneRepository(_context);
        var projectRepo = new ProjectRepository(_context);
        var authService = new AuthorizationService(projectRepo);
        _laneService = new LaneService(laneRepo, authService);
        _projectService = new ProjectService(projectRepo, authService);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task RetrievingLaneFromProjectWhereUserIsNotAMemberThrowsNotAuthorized()
    {
        var projectId = Guid.NewGuid();
        var project = await _projectService.Create("test", projectId);
        var lane = project.Lanes.First();
        
        var action = () => _laneService.GetById(lane.Id, project.Id, Guid.NewGuid());
        
        await action.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task RetrievingLaneFromValidProjectWhereUserIsAMemberSucceeds()
    {
        var projectId = Guid.NewGuid();
        var project = await _projectService.Create("test", projectId);
        
        var laneFromReturn = project.Lanes.First();
        var lane = await _laneService.GetById(laneFromReturn.Id, project.Id, projectId);
        
        lane.Should().BeEquivalentTo(laneFromReturn);
    }
}