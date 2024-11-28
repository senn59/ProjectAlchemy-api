using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dto;

namespace ProjectAlchemy.Core.Interfaces;

public interface IProjectRepository
{
    public Task<List<ProjectOverview>> GetAll(string userId);
    public Task<Project?> Get(string id);
    public Task<Project> Create(Project project);
    public Task<List<Member>> GetMembers(string projectId);
}