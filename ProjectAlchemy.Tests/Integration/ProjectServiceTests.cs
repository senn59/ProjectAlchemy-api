using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

namespace ProjectAlchemy.Tests.Integration;

public class ProjectServiceTests: IDisposable
{
    private readonly AppDbContext _context;
    private readonly ProjectService _service;
    
    public ProjectServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("projectServiceTests")
            .Options;
        _context = new AppDbContext(options);
        var projectRepository = new ProjectRepository(_context);
        _service = new ProjectService(projectRepository, new AuthorizationService(projectRepository));
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
    [Fact]
    public async Task TryingToAccessProjectWhereUserIsNotAMemberThrowsUnauthorizedException()
    {
        var memberOneProject = await _service.Create("test", "1");
        //ensure a 2nd member exists
        await _service.Create("test2", "2");

        var action = () => _service.Get(memberOneProject.Id, "2");
        await action.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task UserCanCreateProjectAndAccessItLaterOn()
    {
        var project = await _service.Create("test", "1");
        var retrieved = await _service.Get(project.Id, "1");
        retrieved.Should().BeEquivalentTo(project);
    }
}