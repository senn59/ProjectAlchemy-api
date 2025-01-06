using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Utilities;
using IAuthorizationService = ProjectAlchemy.Core.Interfaces.IAuthorizationService;

namespace ProjectAlchemy.Web.Websockets;

[Authorize]
public class ProjectHub: Hub
{
    private readonly IAuthorizationService _authService;

    public ProjectHub(IAuthorizationService authorizationService)
    {
        _authService = authorizationService;
    }

    public override async Task OnConnectedAsync()
    {
        var user = Context.GetHttpContext()?.User;
        var projectId = Context.GetHttpContext()?.Request.Query["projectId"].ToString();
        if (user == null || string.IsNullOrEmpty(projectId))
        {
            Context.Abort();
            return;
        }
        var userId = JwtHelper.GetId(user);
        var projectGuid = Guid.Parse(projectId);
        
        await _authService.Authorize(Permission.ReadProject, userId, projectGuid);
        await Groups.AddToGroupAsync(Context.ConnectionId, projectGuid.ToString());
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var projectId = Context.GetHttpContext()?.Request.Query["projectId"].ToString();
        if (projectId != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}