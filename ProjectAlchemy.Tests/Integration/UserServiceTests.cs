using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

namespace ProjectAlchemy.Tests.Integration;

public class UserServiceTests: IDisposable
{
    private readonly AppDbContext _context;
    private readonly UserService _userService;
    private readonly ProjectService _projectService;
    private readonly Guid _userId = Guid.NewGuid();
    
    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);
        var memberRepo = new MemberRepository(_context);
        var projectRepo = new ProjectRepository(_context);
        _userService = new UserService(memberRepo);
        _projectService = new ProjectService(projectRepo, new AuthorizationService(projectRepo));
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task RequestingProjectsOfUnknownMemberReturnsEmptyList()
    {
        var projects = await _userService.GetProjects(_userId);
        
        projects.Should().BeEmpty();
    }
    
    [Fact]
    public async Task MemberListOfKnownMemberWithProjectsReturnsValidList()
    {
        var firstProject = await _projectService.Create("test", _userId, "test@test.com");
        var secondProject = await _projectService.Create("project2", _userId, "test@test.com");
        
        var projects = await _userService.GetProjects(_userId);
        var firstProjectOverview = new ProjectOverview()
        {
            ProjectId = firstProject.Id,
            MemberType = MemberType.Owner,
            ProjectName = "test"
        };
        
        var secondProjectOverview = new ProjectOverview()
        {
            ProjectId = secondProject.Id,
            MemberType = MemberType.Owner,
            ProjectName = "project2"
        };
        
        projects.Should().BeEquivalentTo([firstProjectOverview, secondProjectOverview]);
    }
}