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
        var member1 = Guid.NewGuid();
        var member2 = Guid.NewGuid();
        var memberOneProject = await _service.Create("test", member1);
        await _service.Create("test2", member2);

        var action = () => _service.Get(memberOneProject.Id, member2);
        
        await action.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    [Fact]
    public async Task UserCanCreateProjectAndAccessItLaterOn()
    {
        var memberId = Guid.NewGuid();
        var project = await _service.Create("test", memberId);
        var retrieved = await _service.Get(project.Id, memberId);
        
        retrieved.Should().BeEquivalentTo(project);
    }
}