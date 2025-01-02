using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Helpers;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Utilities;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
public class InvitationController(InvitationService inviteService, UserService userService) : ControllerBase
{
    [HttpGet]
    [Route("api/invitations")]
    public async Task<List<UserInvitation>> GetUserInvitations(string projectId, int key)
    {
        var userId = JwtHelper.GetId(User);
        return await userService.GetInvitations(userId);
    }
    
    [HttpGet]
    [Route("api/projects/{projectId}/invitations")]
    public async Task<List<string>> GetProjectInvitations(string projectId, int key)
    {
        var userId = JwtHelper.GetId(User);
        return await inviteService.GetInvitedEmails(projectId, userId);
    }

    [HttpPost]
    [Route("api/projects/{projectId}/invitations")]
    public async Task Post(InviteRequest request, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        await inviteService.Invite(userId, request.Email, projectId);
    }
    
    [HttpDelete("{invitationId:string}")]
    [Route("api/projects/{projectId}/invitations")]
    public async Task Delete(string invitationId, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        await inviteService.Cancel(invitationId, userId, projectId);
    }
}
