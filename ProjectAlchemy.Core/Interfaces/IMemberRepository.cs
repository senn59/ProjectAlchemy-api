using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IMemberRepository
{
    public Task<List<ProjectOverview>> GetProjects(string userId);
    public Task<List<InvitationUserView>> GetInvitations(string userId);
}