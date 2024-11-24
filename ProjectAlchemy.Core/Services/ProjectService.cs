using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dto;
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

    public async Task<Project> Get(int projectId)
    {
        return await _repository.Get(projectId);
    }

    public async Task<Project> Add(string name, string userId)
    {
        var creator = new Member(userId, MemberType.Owner);
        var project = new Project([], [creator], _defaultLanes);
        return await _repository.Create(project);
    }
}