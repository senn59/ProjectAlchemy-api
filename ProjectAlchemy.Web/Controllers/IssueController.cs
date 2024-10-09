using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Domain;
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
    public IEnumerable<IssuePreviewResponse> Get()
    {
        var items = _IssueService.GetAll();
        return items.Select(IssuePreviewResponse.FromIssue);
    }

    [HttpGet("{id:int}",Name = "Get issue")]
    public IssuePreview Get(int id)
    {
        var item =  _IssueService.GetById(id);
        return IssuePreview.FromIssue(item);
    }

    [HttpPost(Name = "Create issue")]
    public IssuePreviewResponse Post(CreateIssueRequest request)
    {
        var converted = CreateIssueRequest.ToIssue(request);
        var item = _IssueService.Create(converted);
        return IssuePreviewResponse.FromIssue(item);
    }

    [HttpPut("{id:int}", Name = "Update issue")]
    public IssuePreviewResponse Put(UpdateIssueRequest request, int id)
    {
        var converted = new Issue(id, request.Name, request.Type, request.Description);
        var item = _IssueService.Update(converted);
        return IssuePreviewResponse.FromIssue(item);
    }
    
    [HttpDelete("{id:int}", Name = "Delete issue")]
    public void Delete(int id)
    {
        _IssueService.DeleteById(id);
    }
}