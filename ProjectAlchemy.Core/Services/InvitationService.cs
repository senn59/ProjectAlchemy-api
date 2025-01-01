using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class InvitationService(IInvitationRepository repo)
{
    public async Task Invite(string inviterId, string email, string projectId)
    {
        //TODO: auth
        var invitation = new Invitation
        {
            Email = email,
            Status = InvitationStatus.Sent
        };
        await repo.CreateInvitation(invitation, projectId);
    }
}