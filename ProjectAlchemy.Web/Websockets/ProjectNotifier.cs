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

    public async Task NotifyIssueNew(Guid projectId, IssuePartial issue)
    {
        await _hubContext.Clients.Group(projectId.ToString()).SendAsync("IssueNew", issue);
    }

    public async Task NotifyIssueUpdate(Guid projectId, Issue issue)
    {
        await _hubContext.Clients.Group(projectId.ToString()).SendAsync("IssueUpdate", issue);
    }
    
    public async Task NotifyIssueDelete(Guid projectId, int issueKey)
    {
        await _hubContext.Clients.Group(projectId.ToString()).SendAsync("IssueDelete", issueKey);
    }
}