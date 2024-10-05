using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Dtos;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/Issue")]
public class IssueController : ControllerBase
{
    private readonly ILogger<IssueController> _logger;
    private readonly IssueService _IssueService;

    public IssueController(ILogger<IssueController> logger, IssueService IssueService)
    {
        _logger = logger;
        _IssueService = IssueService;
    }

    [HttpGet(Name = "Get all work items")]
    public IEnumerable<IssueResponse> Get()
    {
        var items = _IssueService.GetAll();
        return items.Select(i => IssueResponse.FromIssue(i));
    }

    [HttpGet("{id:int}",Name = "Get work item")]
    public IssueResponse Get(int id)
    {
        var item =  _IssueService.GetById(id);
        return IssueResponse.FromIssue(item);
    }

    [HttpPost(Name = "Create work item")]
    public IssueResponse Post(CreateIssueRequest request)
    {
        var converted = CreateIssueRequest.ToIssue(request);
        var item = _IssueService.Create(converted);
        return IssueResponse.FromIssue(item);
    }

    [HttpPut("{id:int}", Name = "Update work item")]
    public IssueResponse Put(UpdateIssueRequest request, int id)
    {
        var converted = new Issue(id, request.Name, request.Description);
        var item = _IssueService.Update(converted);
        return IssueResponse.FromIssue(item);
    }
}