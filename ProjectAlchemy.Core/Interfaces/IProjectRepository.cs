using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IProjectRepository
{
    public Task<Project?> Get(Guid id);
    public Task<Project> Create(Project project);
    public Task<bool> HasIssue(Guid projectId, int issueKey);
    public Task<Member?> GetMember(Guid projectId, Guid userId);
    public Task AddMember(Guid projectId, Member member);
}