using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class IssueService(IIssueRepository issueRepository)
{
    private IIssueRepository _issueRepository = issueRepository;

    public Issue Create(Issue item)
    {
        return _issueRepository.Create(item);
    }

    public Issue GetById(int id)
    {
        return _issueRepository.GetById(id);
    }

    public List<Issue> GetAll()
    {
        return _issueRepository.GetAll();
    }

    public Issue Update(Issue item)
    {
        return _issueRepository.Update(item);
    }
    
    public void DeleteById(int id)
    {
        _issueRepository.DeleteById(id);
    }
}