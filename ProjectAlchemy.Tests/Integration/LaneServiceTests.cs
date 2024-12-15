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
            .UseInMemoryDatabase("laneServiceTests")
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
        var project = await _projectService.Create("test", "1");
        var lane = project.Lanes.First();
        
        var action = () => _laneService.GetById(lane.Id, project.Id, "2");
        
        await action.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task RetrievingLaneFromValidProjectWhereUserIsAMemberSucceeds()
    {
        var project = await _projectService.Create("test", "1");
        
        var laneFromReturn = project.Lanes.First();
        var lane = await _laneService.GetById(laneFromReturn.Id, project.Id, "1");
        
        lane.Should().BeEquivalentTo(laneFromReturn);
    }
}