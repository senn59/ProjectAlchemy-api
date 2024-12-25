using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Web.Dtos;
using ProjectAlchemy.Web.Utilities;

namespace ProjectAlchemy.Web.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController(ProjectService projectService, UserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ProjectOverview>> GetProjectList()
    {
        var userId = JwtHelper.GetId(User);
        var projects = await userService.GetUserProjectsList(userId);
        return projects;
    }

    [HttpGet("{id}")]
    public async Task<ProjectView> GetProject(string id)
    {
        var userId = JwtHelper.GetId(User);
        var project = await projectService.Get(id, userId);
        return ProjectView.FromProject(project);
    }

    [HttpPost]
    public async Task<ProjectView> CreateProject(CreateProjectRequest request)
    {
        var userId = JwtHelper.GetId(User);
        var project = await projectService.Create(request.Name, userId);
        return ProjectView.FromProject(project);
    }
}
