using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class IssueService(
    IIssueRepository issueRepository,
    AuthorizationService authService,
    IProjectRepository projectRepository)
{
    public async Task<Issue> Create(Issue item, string userId, string projectId)
    {
        await authService.AuthorizeProjectAccess(userId, projectId);
        return await issueRepository.Create(item, projectId);
    }

    public async Task<Issue> GetById(int issueId, string userId, string projectId )
    {
        await authService.AuthorizeProjectAccess(userId, projectId);
        await AssertIssueInProject(issueId, projectId);
        
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null || !member.CanUpdateIssues())
        {
            throw new NotAuthorizedException();
        }
        
        var issue = await issueRepository.GetById(issueId);
        if (issue == null)
        {
            throw new NotFoundException();
        }

        return issue;
    }

    public async Task<Issue> Update(Issue item, string userId, string projectId)
    {
        await authService.AuthorizeProjectAccess(userId, projectId);
        await AssertIssueInProject(item.Id, projectId);
        
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null || !member.CanUpdateIssues())
        {
            throw new NotAuthorizedException();
        }
        return await issueRepository.Update(item);
    }
    
    public async Task DeleteById(int issueId, string userId, string projectId)
    {
        await authService.AuthorizeProjectAccess(userId, projectId);
        await AssertIssueInProject(issueId, projectId);
        
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null || !member.CanDeleteIssues())
        {
            throw new NotAuthorizedException();
        }
        await issueRepository.DeleteById(issueId);
    }

    private async Task AssertIssueInProject(int issueId, string projectId)
    {
        if (! await issueRepository.IsInProject(issueId, projectId))
        {
            throw new NotFoundException();
        }
    }
}