using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Dtos.Project;

namespace ProjectAlchemy.Core.Interfaces;

public interface IProjectRepository
{
    public Task<Project?> Get(string id);
    public Task<Project> Create(Project project);
    public Task<bool> HasMember(string projectId, string userId);
    public Task<Member?> GetMember(string projectId, string userId);
}