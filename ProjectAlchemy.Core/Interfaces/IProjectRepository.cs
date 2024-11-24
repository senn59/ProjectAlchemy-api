using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dto;

namespace ProjectAlchemy.Core.Interfaces;

public interface IProjectRepository
{
    public Task<List<ProjectOverview>> GetAll(string userId);
    public Task<Project> Get(int id);
    public Task<Project> Create(Project project);
}