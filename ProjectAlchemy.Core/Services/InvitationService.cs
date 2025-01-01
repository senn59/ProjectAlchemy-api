using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class InvitationService(IInvitationRepository repo, IAuthorizationService authorizationService, IProjectRepository projectRepository)
{
    public async Task Invite(string inviterId, string emailToInvite, string projectId)
    {
        await authorizationService.Authorize(Permission.InviteMembers, inviterId, projectId);
        await repo.Create(emailToInvite, projectId);
    }

    public async Task Accept(string invitationId, string email, string userId)
    {
        var info = await repo.GetInfo(invitationId);
        if (info == null || info.Email != email)
        {
            throw new NotFoundException();
        }

        await projectRepository.AddMember(info.ProjectId, new Member()
        {
            UserId = userId,
            Type = MemberType.Collaborator
        });
        
        await repo.Delete(invitationId);
    }

    public async Task Reject(string invitationId)
    {
        await repo.Delete(invitationId);
    }

    public async Task Cancel(string invitationId, string userId, string projectId)
    {
        await authorizationService.Authorize(Permission.InviteMembers, userId, projectId);
        await repo.Delete(invitationId);
    }

    public async Task<List<string>> GetInvitedEmails(string projectId)
    {
        return await repo.GetInvitedEmails(projectId);
    }
}