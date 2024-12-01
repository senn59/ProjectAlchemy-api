using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IProjectRepository
{
    public Task<Project?> Get(string id);
    public Task Create(Project project);
    public Task<bool> HasMember(string projectId, string userId);
    public Task<Member?> GetMember(string projectId, string userId);
}