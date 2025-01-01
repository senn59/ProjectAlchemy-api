using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class AuthorizationService(IProjectRepository projectRepository): IAuthorizationService
{
    private readonly IReadOnlyCollection<MemberType> _canUpdate = [MemberType.Collaborator, MemberType.Owner];
    private readonly IReadOnlyCollection<MemberType> _canDelete = [MemberType.Owner];
    private readonly IReadOnlyCollection<MemberType> _canInvite = [MemberType.Owner];
    
    public async Task AuthorizeProjectAccess(string userId, string projectId)
    {
        if (! await projectRepository.HasMember(projectId, userId))
        {
            throw new NotAuthorizedException();
        }
    }

    public async Task AuthorizeIssueDeletion(string userId, string projectId, int issueKey)
    {
        if (!await projectRepository.HasIssue(projectId, issueKey))
        {
            throw new NotFoundException();
        }
        
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null || !_canDelete.Contains(member.Type))
        {
            throw new NotAuthorizedException();
        }
    }
    
    public async Task AuthorizeIssueAccess(string userId, string projectId, int issueKey)
    {
        if (!await projectRepository.HasIssue(projectId, issueKey))
        {
            throw new NotFoundException();
        }
        
        if (!await projectRepository.HasMember(projectId, userId))
        {
            throw new NotAuthorizedException();
        }
    }
    
    public async Task AuthorizeIssueUpdate(string userId, string projectId, int issueKey)
    {
        if (!await projectRepository.HasIssue(projectId, issueKey))
        {
            throw new NotFoundException();
        }
        
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null || !_canUpdate.Contains(member.Type))
        {
            throw new NotAuthorizedException();
        }
    }

    public async Task AuthorizeProjectInvitation(string userId, string projectId)
    {
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null || !_canInvite.Contains(member.Type))
        {
            throw new NotAuthorizedException();
        }
    }
}