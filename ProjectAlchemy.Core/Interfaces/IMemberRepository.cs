using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IMemberRepository
{
    public Task<List<ProjectOverview>> GetProjects(Guid userId);
    public Task<List<InvitationUserView>> GetInvitations(string email);
}