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
    public async Task<IssueResponse> Get(string projectId, int id)
    {
        var userId = JwtHelper.GetId(User);
        var item =  await issueService.GetById(id, userId, projectId);
        return IssueResponse.FromIssue(item);
    }

    [HttpPost]
    public async Task<PartialIssue> Post(CreateIssueRequest request, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        var lane = await laneService.GetById(request.LaneId, projectId, userId);
        var issue = CreateIssueRequest.ToIssue(request, lane);
        var createdIssue = await issueService.Create(issue, userId, projectId);
        return PartialIssue.FromIssue(createdIssue);
    }
    
    [HttpPatch("{id:int}")]
    public async Task<IssueResponse> Patch([FromBody] JsonPatchDocument<IssuePatch> patchDoc, int id, string projectId)
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
        issue.SetName(issuePatch.Name);
        issue.SetDescription(issuePatch.Description);
        issue.SetType(issuePatch.Type);
        issue.SetLane(issuePatch.Lane);
        var updated = await issueService.Update(issue, userId, projectId);
        return IssueResponse.FromIssue(updated);
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        await issueService.DeleteById(id, userId, projectId);
    }
}