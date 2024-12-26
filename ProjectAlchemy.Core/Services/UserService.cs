using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class UserService(IMemberRepository repository)
{
    public async Task<List<ProjectOverview>> GetUserProjectsList(string userid)
    {
        return await repository.GetProjects(userid);
    }
}