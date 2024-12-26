using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Dtos.Project;

namespace ProjectAlchemy.Core.Interfaces;

public interface IMemberRepository
{
    public Task<List<ProjectOverview>> GetProjects(string userId);
}