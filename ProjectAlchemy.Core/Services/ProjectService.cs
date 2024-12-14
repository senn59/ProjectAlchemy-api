using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class ProjectService(IProjectRepository repository, AuthorizationService authService)
{
    private readonly IReadOnlyList<Lane> _defaultLanes = [new("To do"), new("In progress"), new("Done")];

    public async Task<Project> Get(string projectId, string userid)
    {
        await authService.AuthorizeProjectAccess(userid, projectId);
        var project = await repository.Get(projectId);
        if (project == null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    public async Task<Project> Create(string projectName, string userId)
    {
        var creator = new Member(userId, MemberType.Owner);
        var project = new Project(projectName, [], [creator], _defaultLanes.ToList());
        return await repository.Create(project);
    }
}