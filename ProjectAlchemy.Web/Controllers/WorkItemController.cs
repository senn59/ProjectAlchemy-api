using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Dtos;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/workItem")]
public class WorkItemController : ControllerBase
{
    private readonly ILogger<WorkItemController> _logger;
    private readonly WorkItemService _workItemService;

    public WorkItemController(ILogger<WorkItemController> logger, WorkItemService workItemService)
    {
        _logger = logger;
        _workItemService = workItemService;
    }

    [HttpGet(Name = "Get all work items")]
    public IEnumerable<WorkItemResponse> Get()
    {
        var items = _workItemService.GetAll();
        return items.Select(i => WorkItemResponse.FromWorkItem(i));
    }

    [HttpGet("{id:int}",Name = "Get work item")]
    public WorkItemResponse Get(int id)
    {
        var item =  _workItemService.GetById(id);
        return WorkItemResponse.FromWorkItem(item);
    }

    [HttpPost(Name = "Create work item")]
    public WorkItemResponse Post(CreateWorkItemRequest request)
    {
        var converted = CreateWorkItemRequest.ToWorkItem(request);
        var item = _workItemService.Create(converted);
        return WorkItemResponse.FromWorkItem(item);
    }

    [HttpPut("{id:int}", Name = "Update work item")]
    public WorkItemResponse Put(UpdateWorkItemRequest request, int id)
    {
        var converted = new WorkItem(id, request.Name, request.Description);
        var item = _workItemService.Update(converted);
        return WorkItemResponse.FromWorkItem(item);
    }
}