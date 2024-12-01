using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dto;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Persistence.Repositories;

public class MemberRepository: IMemberRepository
{
    private readonly AppDbContext _context;
    
    public MemberRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProjectOverview>> GetProjects(string userId)
    {
        return await _context.Members
            .Where(m => m.userId == userId)
            .Join(
                _context.Projects,
                m => m.ProjectId,
                project => project.Id,
                (m, project) => new ProjectOverview()
                {
                    ProjectId = m.ProjectId,
                    ProjectName = project.Name,
                    MemberType = m.Type,
                }).ToListAsync();
    }
}