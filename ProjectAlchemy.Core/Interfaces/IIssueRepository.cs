using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IIssueRepository
{
    public Task<Issue?> GetById(int id);
    public Task<Issue> Create(Issue item, string projectId);
    public Task<Issue> Update(Issue updated, string projectId);
    public Task<bool> IsInProject(int issueId, string projectId);
    public Task DeleteById(int id);
}