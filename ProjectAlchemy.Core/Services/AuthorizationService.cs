using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class AuthorizationService(IProjectRepository projectRepository): IAuthorizationService
{
    private readonly IReadOnlyCollection<Permission> _collaboratorPermissions =
    [
        Permission.ReadProject,
        Permission.CreateIssues,
        Permission.ReadIssues,
        Permission.UpdateIssues
    ];

    public async Task Authorize(Permission permission, Guid userId, Guid projectId)
    {
        var member = await projectRepository.GetMember(projectId, userId);
        if (member == null)
        {
            throw new NotAuthorizedException();
        }

        switch (member.Type)
        {
            case MemberType.Owner:
                return;
            case MemberType.Collaborator:
                if (!_collaboratorPermissions.Contains(permission))
                {
                    throw new NotAuthorizedException();
                }
                break;
            default:
                throw new NotAuthorizedException();
        }
    }
    
    public async Task Authorize(Permission permission, Guid userId, Guid projectId, int issueKey)
    {
        if (!await projectRepository.HasIssue(projectId, issueKey))
        {
            throw new NotFoundException();
        }
        await Authorize(permission, userId, projectId);
    }
}