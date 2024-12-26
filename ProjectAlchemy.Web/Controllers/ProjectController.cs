using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Services;
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
        return await userService.GetUserProjectsList(userId);
    }

    [HttpGet("{id}")]
    public async Task<Project> GetProject(string id)
    {
        var userId = JwtHelper.GetId(User);
        return await projectService.Get(id, userId);
    }

    [HttpPost]
    public async Task<Project> CreateProject(ProjectCreate request)
    {
        var userId = JwtHelper.GetId(User);
        return await projectService.Create(request.Name, userId);
    }
}
