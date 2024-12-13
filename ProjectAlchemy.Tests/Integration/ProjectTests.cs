using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

namespace ProjectAlchemy.Tests.Integration;

public class ProjectTests: IDisposable
{
    private readonly AppDbContext _context;
    private readonly ProjectService _service;
    
    public ProjectTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("db")
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
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}