using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Helpers;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Utilities;
using ProjectAlchemy.Web.Websockets;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/issues")]
public class IssueController(IssueService issueService, ProjectNotifier notifier) : ControllerBase
{
    [HttpGet("{key:int}")]
    public async Task<Issue> Get(Guid projectId, int key)
    {
        var userId = JwtHelper.GetId(User);
        var issue = await issueService.GetByKey(key, userId, projectId);
        return issue;
    }

    [HttpPost]
    public async Task<IssuePartial> Post(IssueCreate request, Guid projectId)
    {
        var userId = JwtHelper.GetId(User);
        var created = await issueService.Create(request, userId, projectId);
        await notifier.NotifyIssueNew(projectId, created);
        return created;
    }
    
    [HttpPatch("{key:int}")]
    public async Task<Issue> Patch([FromBody] JsonPatchDocument<IssuePatch> patchDoc, int key, Guid projectId)
    {
        var userId = JwtHelper.GetId(User); 
        var issue = await issueService.GetByKey(key, userId, projectId);
        
        var issuePatch = new IssuePatch()
        {
            Name = issue.Name,
            Description = issue.Description,
            Type = issue.Type,
            Lane = issue.Lane
        };
        
        patchDoc.ApplyTo(issuePatch, ModelState);
        issue.Name = issuePatch.Name;
        issue.Description = issuePatch.Description;
        issue.Type = issuePatch.Type;
        issue.Lane = issuePatch.Lane;
        
        ValidationHelper.Validate(issue);
        var updated = await issueService.Update(issue, userId, projectId);
        await notifier.NotifyIssueUpdate(projectId, updated);
        return updated;
    }
    
    [HttpDelete("{key:int}")]
    public async Task Delete(int key, Guid projectId)
    {
        var userId = JwtHelper.GetId(User);
        await issueService.DeleteByKey(key, userId, projectId);
        await notifier.NotifyIssueDelete(projectId, key);
    }
}