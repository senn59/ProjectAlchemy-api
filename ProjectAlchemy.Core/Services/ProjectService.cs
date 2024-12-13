using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class ProjectService
{
    private readonly IProjectRepository _repository;
    private readonly AuthorizationService _authService;
    
    private List<Lane> _defaultLanes = [new Lane("To do"), new Lane("In progress"), new Lane("Done")];
    
    public ProjectService(IProjectRepository repository, AuthorizationService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    public async Task<Project> Get(string projectId, string userid)
    {
        await _authService.AuthorizeProjectAccess(userid, projectId);
        var project = await _repository.Get(projectId);
        if (project == null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    public async Task<Project> Create(string projectName, string userId)
    {
        var creator = new Member(userId, MemberType.Owner);
        var project = new Project(projectName, [], [creator], _defaultLanes);
        return await _repository.Create(project);
    }
}