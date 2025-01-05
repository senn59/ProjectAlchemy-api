using Microsoft.AspNetCore.SignalR;
using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Web.Websockets;

public class ProjectNotifier
{
    private readonly IHubContext<ProjectHub> _hubContext;

    public ProjectNotifier(IHubContext<ProjectHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyNewIssue(Guid projectId, IssuePartial issue)
    {
        await _hubContext.Clients.Group(projectId.ToString()).SendAsync("IssueNew", issue);
    }

    public async Task NotifyUpdatedIssue(Guid projectId, Issue issue)
    {
        await _hubContext.Clients.Group(projectId.ToString()).SendAsync("IssueUpdate", issue);
    }
}