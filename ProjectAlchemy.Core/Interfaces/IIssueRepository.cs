using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IIssueRepository
{
    public Task<Issue?> GetById(int id);
    public Task<Issue> Create(Issue item, string projectId);
    public List<Issue> GetAll();
    public Task<Issue> Update(Issue item);
    public void DeleteById(int id);
}