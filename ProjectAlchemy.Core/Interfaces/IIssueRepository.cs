using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IIssueRepository
{
    public Task<Issue?> GetById(int issueId, string projectId);
    public Task<IssuePartial> Create(IssueCreate issue, string projectId);
    public Task<Issue> Update(Issue updated, string projectId);
    public Task DeleteById(int id);
}