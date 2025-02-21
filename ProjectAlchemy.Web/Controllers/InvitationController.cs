using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Utilities;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api")]
public class InvitationController(InvitationService inviteService, UserService userService) : ControllerBase
{
    [HttpGet("invitations")]
    public async Task<List<InvitationUserView>> GetUserInvitations()
    {
        var email = JwtHelper.GetEmail(User);
        return await userService.GetInvitations(email);
    }
    
    [HttpPost("invitations/{invitationId:guid}/accept")]
    public async Task<Guid> PostAcceptInvitation(Guid invitationId)
    {
        var userId = JwtHelper.GetId(User);
        var email = JwtHelper.GetEmail(User);
        return await inviteService.Accept(invitationId, email, userId);
    }
    
    [HttpPost("invitations/{invitationId:guid}/reject")]
    public async Task PostRejectInvitation(Guid invitationId)
    {
        var email = JwtHelper.GetEmail(User);
        await inviteService.Reject(invitationId, email);
    }
    
    [HttpGet("projects/{projectId:guid}/invitations")]
    public async Task<List<InvitationOutgoingView>> GetProjectInvitations(Guid projectId)
    {
        var userId = JwtHelper.GetId(User);
        return await inviteService.GetInvitedEmails(projectId, userId);
    }

    [HttpPost("projects/{projectId:guid}/invitations")]
    public async Task<InvitationOutgoingView> Post(InvitationRequest request, Guid projectId)
    {
        var userId = JwtHelper.GetId(User);
        return await inviteService.Invite(userId, request.Email, projectId);
    }
    
    [HttpDelete("projects/{projectId:guid}/invitations/{invitationId:guid}")]
    public async Task Delete(Guid invitationId, Guid projectId)
    {
        var userId = JwtHelper.GetId(User);
        await inviteService.Cancel(invitationId, userId, projectId);
    }
}
