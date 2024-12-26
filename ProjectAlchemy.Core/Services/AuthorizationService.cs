using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class AuthorizationService(IProjectRepository projectRepository)
{
    public async Task AuthorizeProjectAccess(string userId, string projectId)
    {
        if (! await projectRepository.HasMember(projectId, userId))
        {
            throw new NotAuthorizedException();
        }
    }

    public async Task AuthorizeIssueDeletion(string userId, string projectId, int issueId)
    {
        if (!await projectRepository.HasIssue(projectId, issueId))
        {
            throw new NotFoundException();
        }
        
        var member = await projectRepository.GetMember(userId, projectId);
        if (member == null)
        {
            throw new NotAuthorizedException();
        }
        
        if (member.Type != MemberType.Owner)
        {
            throw new NotAuthorizedException();
        }
    }
    
    public async Task AuthorizeIssueAccess(string userId, string projectId, int issueId)
    {
        if (!await projectRepository.HasIssue(projectId, issueId))
        {
            throw new NotFoundException();
        }
        
        if (!await projectRepository.HasMember(projectId, userId))
        {
            throw new NotAuthorizedException();
        }
    }
    
    public async Task AuthorizeIssueUpdate(string userId, string projectId, int issueId)
    {
        if (!await projectRepository.HasIssue(projectId, issueId))
        {
            throw new NotFoundException();
        }
        
        var member = await projectRepository.GetMember(userId, projectId);
        if (member == null)
        {
            throw new NotAuthorizedException();
        }

        List<MemberType> allowed = [MemberType.Owner, MemberType.Collaborator];
        
        if (!allowed.Contains(member.Type))
        {
            throw new NotAuthorizedException();
        }
    }
}