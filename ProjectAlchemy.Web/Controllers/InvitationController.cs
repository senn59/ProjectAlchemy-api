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
        var userId = JwtHelper.GetId(User);
        return await userService.GetInvitations(userId);
    }
    
    [HttpPost("invitations/{invitationId}/accept")]
    public async Task<List<InvitationUserView>> PostAcceptInvitation(string invitationId)
    {
        var userId = JwtHelper.GetId(User);
        var email = JwtHelper.GetEmail(User);
        await inviteService.Accept(invitationId, email, userId);
        return await userService.GetInvitations(userId);
    }
    
    [HttpPost("invitations/{invitationId}/reject")]
    public async Task<List<InvitationUserView>> PostRejectInvitation(string invitationId)
    {
        var userId = JwtHelper.GetId(User);
        var email = JwtHelper.GetEmail(User);
        await inviteService.Reject(invitationId, email);
        return await userService.GetInvitations(userId);
    }
    
    [HttpGet("projects/{projectId}/invitations")]
    public async Task<List<string>> GetProjectInvitations(string projectId)
    {
        var userId = JwtHelper.GetId(User);
        return await inviteService.GetInvitedEmails(projectId, userId);
    }

    [HttpPost("projects/{projectId}/invitations")]
    public async Task Post(InvitationRequest request, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        await inviteService.Invite(userId, request.Email, projectId);
    }
    
    [HttpDelete("projects/{projectId}/invitations/{invitationId}")]
    public async Task Delete(string invitationId, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        await inviteService.Cancel(invitationId, userId, projectId);
    }
}
