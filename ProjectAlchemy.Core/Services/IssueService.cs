using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class IssueService
{
    private readonly IIssueRepository _issueRepository;
    private readonly AuthorizationService _authService;
    private readonly IProjectRepository _projectRepository;

    public IssueService(IIssueRepository issueRepository, AuthorizationService authService, IProjectRepository projectRepository)
    {
        _issueRepository = issueRepository;
        _authService = authService;
        _projectRepository = projectRepository;
    }

    public async Task<Issue> Create(Issue item, string userId, string projectId)
    {
        await _authService.AuthorizeProjectAccess(userId, projectId);
        return await _issueRepository.Create(item, projectId);
    }

    public async Task<Issue> GetById(int issueId, string userId, string projectId )
    {
        await _authService.AuthorizeProjectAccess(userId, projectId);
        await AssertIssueInProject(issueId, projectId);
        
        var member = await _projectRepository.GetMember(projectId, userId);
        if (!member.CanUpdateIssues())
        {
            throw new NotAuthorizedException();
        }
        
        var issue = await _issueRepository.GetById(issueId);
        if (issue == null)
        {
            throw new NotFoundException();
        }

        return issue;
    }

    public async Task<Issue> Update(Issue item, string userId, string projectId)
    {
        await _authService.AuthorizeProjectAccess(userId, projectId);
        await AssertIssueInProject(item.Id, projectId);
        
        var member = await _projectRepository.GetMember(projectId, userId);
        if (!member.CanUpdateIssues())
        {
            throw new NotAuthorizedException();
        }
        return await _issueRepository.Update(item);
    }
    
    public async Task DeleteById(int issueId, string userId, string projectId)
    {
        await _authService.AuthorizeProjectAccess(userId, projectId);
        await AssertIssueInProject(issueId, projectId);
        
        var member = await _projectRepository.GetMember(projectId, userId);
        if (!member.CanDeleteIssues())
        {
            throw new NotAuthorizedException();
        }
        _issueRepository.DeleteById(issueId);
    }

    private async Task AssertIssueInProject(int issueId, string projectId)
    {
        if (! await _issueRepository.IsInProject(issueId, projectId))
        {
            throw new NotFoundException();
        }
    }
}