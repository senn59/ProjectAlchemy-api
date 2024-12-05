using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Dto;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Dtos;
using ProjectAlchemy.Web.Utilities;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<IssueController> _logger;
    private readonly IssueService _issueService;
    private readonly ProjectService _projectService;
    private readonly UserService _userService;

    public ProjectController(ILogger<IssueController> logger, IssueService issueService, ProjectService projectService, UserService userService)
    {
        _logger = logger;
        _issueService = issueService;
        _projectService = projectService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProjectOverview>> GetProjectList()
    {
        var userId = JwtHelper.GetId(User);
        var projects = await _userService.GetUserProjectsList(userId);
        return projects;
    }

    [HttpGet("{id}")]
    public async Task<ProjectView> GetProject(string id)
    {
        var userId = JwtHelper.GetId(User);
        var project = await _projectService.Get(id, userId);
        return ProjectView.FromProject(project);
    }

    [HttpPost]
    public async Task<ProjectView> CreateProject(CreateProjectRequest request)
    {
        var userId = JwtHelper.GetId(User);
        var project = await _projectService.Create(request.Name, userId);
        return ProjectView.FromProject(project);
    }
}
