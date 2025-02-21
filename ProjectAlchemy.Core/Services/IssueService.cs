using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class IssueService(IIssueRepository issueRepository, IAuthorizationService authService)
{
    public const int MaxNameLength = 30;
    public const int MaxDescriptionLength = 200;
    
    public async Task<IssuePartial> Create(IssueCreate issue, Guid userId, Guid projectId)
    {
        await authService.Authorize(Permission.CreateIssues, userId, projectId);
        return await issueRepository.Create(issue, projectId);
    }

    public async Task<Issue> GetByKey(int issueKey, Guid userId, Guid projectId )
    {
        await authService.Authorize(Permission.ReadIssues, userId, projectId, issueKey);
        
        var issue = await issueRepository.GetByKey(issueKey, projectId);
        if (issue == null)
        {
            throw new NotFoundException();
        }

        return issue;
    }

    public async Task<Issue> Update(Issue item, Guid userId, Guid projectId)
    {
        await authService.Authorize(Permission.UpdateIssues, userId, projectId, item.Key);
        return await issueRepository.Update(item, projectId);
    }
    
    public async Task DeleteByKey(int issueKey, Guid userId, Guid projectId)
    {
        await authService.Authorize(Permission.DeleteIssues, userId, projectId, issueKey);
        await issueRepository.DeleteByKey(issueKey, projectId);
    }

    public async Task LinkIssues(int issueKey, IEnumerable<int> issueKeysToLink, Guid userId, Guid projectId)
    {
        await authService.Authorize(Permission.CreateIssues, userId, projectId, issueKey);
        await issueRepository.LinkIssues(issueKey, issueKeysToLink, projectId);
    }
}