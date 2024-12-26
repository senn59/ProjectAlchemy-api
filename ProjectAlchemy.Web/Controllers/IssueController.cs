using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Utilities;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/projects/{projectId}/issues")]
public class IssueController(IssueService issueService, LaneService laneService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<Issue> Get(string projectId, int id)
    {
        var userId = JwtHelper.GetId(User);
        var issue =  await issueService.GetById(id, userId, projectId);
        return issue;
    }

    [HttpPost]
    public async Task<IssuePartial> Post(IssueCreate request, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        return await issueService.Create(request, userId, projectId);
    }
    
    [HttpPatch("{id:int}")]
    public async Task<Issue> Patch([FromBody] JsonPatchDocument<IssuePatch> patchDoc, int id, string projectId)
    {
        var userId = JwtHelper.GetId(User); 
        var issue = await issueService.GetById(id, userId, projectId);
        
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
        
        return await issueService.Update(issue, userId, projectId);
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        await issueService.DeleteById(id, userId, projectId);
    }
}