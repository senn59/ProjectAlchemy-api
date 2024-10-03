using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Services;

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
    public List<WorkItemResponse> Get()
    {
        return _workItemService.GetAll();
    }

    [HttpGet("{id:int}",Name = "Get work item")]
    public WorkItemResponse Get(int id)
    {
        return _workItemService.GetById(id);
    }

    [HttpPost(Name = "Create work item")]
    public void Post(CreateWorkItemRequest request)
    {
        _workItemService.Create(request);
    }

    [HttpPut("{id:int}", Name = "Update work item")]
    public void Put(UpdateWorkItemRequest request, int id)
    {
        throw new NotImplementedException();
    }
}