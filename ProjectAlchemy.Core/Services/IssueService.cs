using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class IssueService
{
    private readonly IIssueRepository _issueRepository;
    private readonly AuthorizationService _authService;

    public IssueService(IIssueRepository issueRepository, AuthorizationService authService)
    {
        _issueRepository = issueRepository;
        _authService = authService;
    }

    public async Task<Issue> Create(Issue item, string userId, string projectId)
    {
        _authService.AuthorizeProjectAccess(userId, projectId);
        return await _issueRepository.Create(item, projectId);
    }

    public async Task<Issue> GetById(int id, string userId, string projectId )
    {
        _authService.AuthorizeProjectAccess(userId, projectId);
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
    
    public void DeleteById(int id, string userId, string projectId)
    {
        _authService.AuthorizeProjectAccess(userId, projectId);
        
        _issueRepository.DeleteById(id);
    }
}