using ProjectAlchemy.Core.Dto;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class UserService
{
    private readonly IMemberRepository _repository;
    public UserService(IMemberRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ProjectOverview>> GetUserProjectsList(string userid)
    {
        return await _repository.GetProjects(userid);
    }
}