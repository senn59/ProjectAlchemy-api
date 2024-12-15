using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IMemberRepository
{
    public Task<List<ProjectOverview>> GetProjects(string userId);
}