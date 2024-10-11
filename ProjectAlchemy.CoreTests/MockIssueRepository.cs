using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.CoreTests;

public class MockIssueRepository: IIssueRepository
{
    private List<Issue> _issues = new List<Issue>();
    public MockIssueRepository()
    {
        
    }
    
    public Issue GetById(int id)
    {
        return _issues.First(x => x.Id == id);
    }

    public Issue Create(Issue item)
    {
        _issues.Add(item);
        return item;
    }

    public List<Issue> GetAll()
    {
        throw new NotImplementedException();
    }

    public Issue Update(Issue item)
    {
        return item;
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}