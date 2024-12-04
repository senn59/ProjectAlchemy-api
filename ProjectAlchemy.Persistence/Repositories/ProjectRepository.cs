using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence.Repositories;

public class ProjectRepository: IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Project?> Get(string id)
    {
        var project = await _context.Projects.FindAsync(id);
        return project == null ? null : ProjectEntity.ToProject(project);
    }

    public async Task Create(Project project)
    {
        var entity = ProjectEntity.FromProject(project);
        await _context.Projects.AddAsync(entity);
    }

    public async Task<bool> HasMember(string projectId, string userId)
    {
        return await _context.Members.AnyAsync(m => m.ProjectId == projectId && m.UserId == userId);
    }

    public async Task<Member?> GetMember(string projectId, string userId)
    {
        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.ProjectId == projectId && m.UserId == userId);
        return member == null ? null : MemberEntity.ToMember(member);
    }
}