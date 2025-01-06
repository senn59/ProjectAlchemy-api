using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class UserService(IMemberRepository repository)
{
    public async Task<List<ProjectOverview>> GetProjects(Guid userId)
    {
        return await repository.GetProjects(userId);
    }
    
    public async Task<List<InvitationUserView>> GetInvitations(string email)
    {
        return await repository.GetInvitations(email);
    }
}