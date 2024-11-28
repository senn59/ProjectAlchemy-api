using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class AuthorizationService
{
    
    private readonly IProjectRepository _projectRepository;
    private readonly IIssueRepository _issueRepository;
    
    
    public AuthorizationService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async void AuthorizeProjectAccess(string userId, string projectId)
    {
        var members = await _projectRepository.GetMembers(projectId);
        if (members.All(m => m.UserId != userId))
        {
            throw new UnauthorizedAccessException();
        }
    }
    
    public async void AuthorizeIssueDeletion(string userId, string projectId)
    {
        AuthorizeProjectAccess(userId, projectId);
        //TODO: get member type and check if owner
    }
}