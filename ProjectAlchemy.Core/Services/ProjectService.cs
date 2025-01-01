using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class ProjectService(IProjectRepository repository, IAuthorizationService authService)
{
    public const int MaxNameLength = 30;

    public async Task<Project> Get(string projectId, string userid)
    {
        await authService.Authorize(Permission.ReadProject, userid, projectId);
        var project = await repository.Get(projectId);
        if (project == null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    public async Task<Project> Create(string projectName, string userId)
    {
        var creator = new Member
        {
            UserId = userId,
            Type = MemberType.Owner
        };
        
        var project = new Project
        {
            Id = Guid.NewGuid().ToString(),
            Name = projectName,
            Issues = [],
            Lanes = LaneService.GetDefaultLanes(),
            Members = [creator]
        };
        
        return await repository.Create(project);
    }
}