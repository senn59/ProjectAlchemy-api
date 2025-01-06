using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class ProjectService(IProjectRepository repository, IAuthorizationService authService)
{
    public const int MaxNameLength = 30;
    private readonly IReadOnlyList<Lane> _defaultLanes = [
        new() {Name = "To do"},
        new() {Name = "In progress"},
        new() {Name = "Done"}
    ];

    public async Task<Project> Get(Guid projectId, Guid userId)
    {
        await authService.Authorize(Permission.ReadProject, userId, projectId);
        var project = await repository.Get(projectId);
        if (project == null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    public async Task<Project> Create(string projectName, Guid userId, string email)
    {
        var creator = new Member
        {
            UserId = userId,
            Type = MemberType.Owner,
            UserName = email.Split("@").First()
        };
        
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = projectName,
            Issues = [],
            Lanes = _defaultLanes,
            Members = [creator]
        };
        
        return await repository.Create(project);
    }
}