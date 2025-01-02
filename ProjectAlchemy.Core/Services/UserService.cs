using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class UserService(IMemberRepository repository)
{
    public async Task<List<ProjectOverview>> GetProjects(string userid)
    {
        return await repository.GetProjects(userid);
    }
    
    public async Task<List<InvitationUserView>> GetInvitations(string userid)
    {
        return await repository.GetInvitations(userid);
    }
}