using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

namespace ProjectAlchemy.CoreTests;

public class Tests
{
    private AppDbContext _context;
    private ProjectService _service;
    
    [SetUp]
    public void Init()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("db")
            .Options;
        _context = new AppDbContext(options);
        var projectRepository = new ProjectRepository(_context);
        _service = new ProjectService(projectRepository, new AuthorizationService(projectRepository));
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
    [Test]
    public async Task TryingToAccessProjectWhereUserIsNotAMemberThrowsUnauthorizedException()
    {
        var memberOneProject = await _service.Create("test", "1");
        //ensure a 2nd member exists
        await _service.Create("test2", "2");

        var action = () => _service.Get(memberOneProject.Id, "2");
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}