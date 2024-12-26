using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Dtos.Issue;

namespace ProjectAlchemy.Core.Interfaces;

public interface IIssueRepository
{
    public Task<Issue?> GetById(int id);
    public Task<Issue> Create(CreateIssue issue, string projectId);
    public Task<Issue> Update(Issue updated, string projectId);
    public Task DeleteById(int id);
}