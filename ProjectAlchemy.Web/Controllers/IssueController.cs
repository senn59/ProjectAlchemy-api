using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Dtos;
using ProjectAlchemy.Web.Utilities;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/projects/{projectId}/issues")]
public class IssueController : ControllerBase
{
    private readonly ILogger<IssueController> _logger;
    private readonly IssueService _issueService;
    private readonly LaneService _laneService;

    public IssueController(ILogger<IssueController> logger, IssueService issueService, LaneService laneService)
    {
        _logger = logger;
        _issueService = issueService;
        _laneService = laneService;
    }

    [HttpGet("{id:int}")]
    public async Task<IssueResponse> Get(string projectId, int id)
    {
        var userId = JwtHelper.GetId(User);
        var item =  await _issueService.GetById(id, userId, projectId);
        return IssueResponse.FromIssue(item);
    }

    [HttpPost]
    public async Task<PartialIssue> Post(CreateIssueRequest request, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        var lane = await _laneService.GetLaneById(request.laneId, projectId, userId);
        var issue = CreateIssueRequest.ToIssue(request, lane);
        var createdIssue = await _issueService.Create(issue, userId, projectId);
        return PartialIssue.FromIssue(createdIssue);
    }

    [HttpPut("{id:int}")]
    public async Task<PartialIssue> Put(UpdateIssueRequest request, int id, string projectId)
    {
        var userId = JwtHelper.GetId(User); 
        var issue = await _issueService.GetById(id, userId, projectId);
        issue.SetName(request.Name);
        issue.SetDescription(request.Description);
        issue.SetType(request.Type);
        var updated = await _issueService.Update(issue, userId, projectId);
        return PartialIssue.FromIssue(updated);
    }
    
    [HttpPatch("{id:int}")]
    public async Task<PartialIssue> Patch([FromBody] JsonPatchDocument<IssuePatch> patchDoc, int id, string projectId)
    {
        var userId = JwtHelper.GetId(User); 
        var issue = await _issueService.GetById(id, userId, projectId);
        var issuePatch = new IssuePatch()
        {
            Name = issue.Name,
            Description = issue.Description,
            Type = issue.Type
        };
        patchDoc.ApplyTo(issuePatch, ModelState);
        issue.SetName(issuePatch.Name);
        issue.SetDescription(issuePatch.Description);
        issue.SetType(issuePatch.Type);
        var updated = await _issueService.Update(issue, userId, projectId);
        return PartialIssue.FromIssue(updated);
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id, string projectId)
    {
        var userId = JwtHelper.GetId(User);
        await _issueService.DeleteById(id, userId, projectId);
    }
}