using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IProjectRepository
{
    public Task<Project?> Get(string id);
    public Task<Project> Create(Project project);
    public Task<bool> HasIssue(string projectId, int issueKey);
    public Task<Member?> GetMember(string projectId, string userId);
}