using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dto;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class ProjectService(IProjectRepository _repository)
{
    public async Task<List<ProjectOverview>> GetUserProjectsList(string userid)
    {
        return await _repository.GetAll(userid);
    }

    public async Task<Project> Get(int projectId)
    {
        return await _repository.Get(projectId);
    }

    public async Task<Project> Add(Project project)
    {
        return await _repository.Create(project);
    }
}