using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class InvitationService(IInvitationRepository repo, IAuthorizationService authorizationService, IProjectRepository projectRepository)
{
    public async Task<InvitationOutgoingView> Invite(Guid inviterId, string emailToInvite, Guid projectId)
    {
        await authorizationService.Authorize(Permission.InviteMembers, inviterId, projectId);
        return await repo.Create(emailToInvite, projectId);
    }

    public async Task Accept(Guid invitationId, string email, Guid userId)
    {
        var details = await repo.GetInfo(invitationId);
        if (details == null || details.Email != email)
        {
            throw new NotFoundException();
        }

        await projectRepository.AddMember(details.ProjectId, new Member()
        {
            UserId = userId,
            Type = MemberType.Collaborator
        });
        
        await repo.Delete(invitationId);
    }

    public async Task Reject(Guid invitationId, string email)
    {
        var info = await repo.GetInfo(invitationId);
        if (info == null || info.Email != email)
        {
            throw new NotFoundException();
        }
        
        await repo.Delete(invitationId);
    }

    public async Task Cancel(Guid invitationId, Guid userId, Guid projectId)
    {
        await authorizationService.Authorize(Permission.InviteMembers, userId, projectId);
        await repo.Delete(invitationId);
    }

    public async Task<List<InvitationOutgoingView>> GetInvitedEmails(Guid projectId, Guid userId)
    {
        await authorizationService.Authorize(Permission.InviteMembers, userId, projectId);
        return await repo.GetInvitedEmails(projectId);
    }
}