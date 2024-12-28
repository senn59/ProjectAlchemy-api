using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class AuthorizationService(IProjectRepository projectRepository): IAuthorizationService
{
    private readonly IReadOnlyCollection<MemberType> _canUpdate = [MemberType.Collaborator, MemberType.Owner];
    private readonly IReadOnlyCollection<MemberType> _canDelete = [MemberType.Owner];
    
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
        
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null)
        {
            throw new NotAuthorizedException();
        }
        
        if (!_canDelete.Contains(member.Type))
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
        
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null)
        {
            throw new NotAuthorizedException();
        }

        if (!_canUpdate.Contains(member.Type))
        {
            throw new NotAuthorizedException();
        }
    }
}