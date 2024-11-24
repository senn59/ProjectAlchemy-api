using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class IssueService(IIssueRepository issueRepository)
{
    private IIssueRepository _issueRepository = issueRepository;

    public async Task<Issue> Create(Issue item)
    {
        return await _issueRepository.Create(item);
    }

    public async Task<Issue> GetById(int id)
    {
        var issue = await _issueRepository.GetById(id);
        if (issue == null)
        {
            throw new NotFoundException();
        }

        return issue;
    }

    public List<Issue> GetAll()
    {
        return _issueRepository.GetAll();
    }

    public async Task<Issue> Update(Issue item)
    {
        return await _issueRepository.Update(item);
    }
    
    public void DeleteById(int id)
    {
        _issueRepository.DeleteById(id);
    }
}