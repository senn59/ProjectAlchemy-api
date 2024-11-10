using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Dtos;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/issues")]
public class IssueController : ControllerBase
{
    private readonly ILogger<IssueController> _logger;
    private readonly IssueService _IssueService;

    public IssueController(ILogger<IssueController> logger, IssueService IssueService)
    {
        _logger = logger;
        _IssueService = IssueService;
    }

    [HttpGet(Name = "Get all issues")]
    public IEnumerable<PartialIssue> Get()
    {
        var items = _IssueService.GetAll();
        return items.Select(PartialIssue.FromIssue);
    }

    [HttpGet("{id:int}",Name = "Get issue")]
    public IssueResponse Get(int id)
    {
        var item =  _IssueService.GetById(id);
        return IssueResponse.FromIssue(item);
    }

    [HttpPost(Name = "Create issue")]
    public async Task<PartialIssue> Post(CreateIssueRequest request)
    {
        var converted = CreateIssueRequest.ToIssue(request);
        var item = await _IssueService.Create(converted);
        return PartialIssue.FromIssue(item);
    }

    [HttpPut("{id:int}", Name = "Update issue")]
    public async Task<PartialIssue> Put(UpdateIssueRequest request, int id)
    {
        var issue = _IssueService.GetById(id);
        issue.SetName(request.Name);
        issue.SetDescription(request.Description);
        issue.SetType(request.Type);
        var updated = await _IssueService.Update(issue);
        return PartialIssue.FromIssue(updated);
    }
    
    [HttpPatch("{id:int}", Name = "Patch issue")]
    public async Task<PartialIssue> Patch([FromBody] JsonPatchDocument<IssuePatch> patchDoc, int id)
    {
        var issue = _IssueService.GetById(id);
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
        var updated = await _IssueService.Update(issue);
        return PartialIssue.FromIssue(updated);
    }
    
    [HttpDelete("{id:int}", Name = "Delete issue")]
    public void Delete(int id)
    {
        _IssueService.DeleteById(id);
    }
}