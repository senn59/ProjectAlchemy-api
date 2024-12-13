using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class AuthorizationService
{
    private readonly IProjectRepository _projectRepository;
    
    public AuthorizationService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task AuthorizeProjectAccess(string userId, string projectId)
    {
        if (! await _projectRepository.HasMember(projectId, userId))
        {
            throw new NotAuthorizedException();
        }
    }
}