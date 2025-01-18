using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IIssueRepository
{
    public Task<Issue?> GetByKey(int issueKey, Guid projectId);
    public Task<IssuePartial> Create(IssueCreate issue, Guid projectId);
    public Task<Issue> Update(Issue updated, Guid projectId);
    public Task DeleteByKey(int key, Guid projectId);
    public Task LinkIssues(int issueKey, IEnumerable<int> issueKeysToLink, Guid projectId);
}