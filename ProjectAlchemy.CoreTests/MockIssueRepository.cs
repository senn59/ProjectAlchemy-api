using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.CoreTests;

public class MockIssueRepository: IIssueRepository
{
    private List<Issue> _issues = new List<Issue>();
    public MockIssueRepository()
    {
        
    }
    
    public async Task<Issue> GetById(int id)
    {
        var issue = _issues.FirstOrDefault(x => x.Id == id);
        if (issue == null)
        {
            throw new NotFoundException();
        }

        return issue;
    }

    public async Task<Issue> Create(Issue item)
    {
        _issues.Add(item);
        return item;
    }

    public List<Issue> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Issue> Update(Issue item)
    {
        return item;
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}