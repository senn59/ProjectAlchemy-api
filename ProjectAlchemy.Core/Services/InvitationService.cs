using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class InvitationService(IInvitationRepository repo, IAuthorizationService authorizationService)
{
    public async Task Invite(string inviterId, string emailToInvite, string projectId)
    {
        await authorizationService.AuthorizeProjectInvitation(inviterId, projectId);
        await repo.Create(emailToInvite, projectId);
    }

    public async Task Accept(string invitationId)
    {
        await repo.Update(invitationId, InvitationStatus.Accepted);
    }

    public async Task Reject(string invitationId)
    {
        await repo.Delete(invitationId);
    }

    public async Task<List<Invitation>> GetAll(string userId)
    {
        return await repo.GetAll(userId);
    }
}