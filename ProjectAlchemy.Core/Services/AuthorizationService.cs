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
}