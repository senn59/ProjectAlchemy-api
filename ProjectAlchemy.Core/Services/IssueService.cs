using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class IssueService(IIssueRepository issueRepository, AuthorizationService authService)
{
    public async Task<Issue> Create(Issue item, string userId, string projectId)
    {
        await authService.AuthorizeProjectAccess(userId, projectId);
        return await issueRepository.Create(item, projectId);
    }

    public async Task<Issue> GetById(int issueId, string userId, string projectId )
    {
        await authService.AuthorizeIssueAccess(userId, projectId, issueId);
        
        var issue = await issueRepository.GetById(issueId);
        if (issue == null)
        {
            throw new NotFoundException();
        }

        return issue;
    }

    public async Task<Issue> Update(Issue item, string userId, string projectId)
    {
        await authService.AuthorizeIssueUpdate(userId, projectId, item.Id);
        return await issueRepository.Update(item, projectId);
    }
    
    public async Task DeleteById(int issueId, string userId, string projectId)
    {
        await authService.AuthorizeIssueDeletion(userId, projectId, issueId);
        await issueRepository.DeleteById(issueId);
    }
}