using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dto;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class ProjectService
{
    private readonly IProjectRepository _repository;
    
    private List<Lane> _defaultLanes = [new Lane("To do"), new Lane("In progress"), new Lane("Done")];
    
    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ProjectOverview>> GetUserProjectsList(string userid)
    {
        return await _repository.GetAll(userid);
    }

    public async Task<Project> Get(int projectId, string userid)
    {
        //TODO: Refactor so we first get the members before retrieving the entire entity
        var project = await _repository.Get(projectId);
        if (project == null)
        {
            throw new NotFoundException();
        }

        if (project.Members.Any(m => m.UserId == userid) == false)
        {
            throw new Exception("You do not have access to this project");
        }

        return project;
    }

    public async Task<Project> Create(string name, string userId)
    {
        var creator = new Member(userId, MemberType.Owner);
        var project = new Project(name, [], [creator], _defaultLanes);
        return await _repository.Create(project);
    }

    public Issue AddIssue(Issue issue, string userId)
    {
        return issue;
    }
}