using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Exceptions;
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
        var project = await _context.Projects
            .Include(p => p.Members)
            .Include(p => p.Issues.Where(i => !i.Deleted))
            .Include(p => p.Lanes)
            .FirstOrDefaultAsync(p => p.Id == id);
        return project == null ? null : ProjectEntity.ToProject(project);
    }

    public async Task<Project> Create(Project project)
    {
        var entity = ProjectEntity.FromProject(project);
        foreach (var lane in entity.Lanes)
        {
            lane.Id = Guid.NewGuid().ToString();
        }
        await _context.Projects.AddAsync(entity);
        await _context.SaveChangesAsync();
        return ProjectEntity.ToProject(entity);
    }

    public async Task<bool> HasMember(string projectId, string userId)
    {
        return await _context.Members.AnyAsync(m => m.ProjectId == projectId && m.UserId == userId);
    }

    public async Task<bool> HasIssue(string projectId, int issueKey)
    {
        return await _context.Issues.AnyAsync(i => i.ProjectId == projectId && i.Key == issueKey && !i.Deleted); 
    }

    public async Task<Member?> GetMember(string projectId, string userId)
    {
        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.ProjectId == projectId && m.UserId == userId);
        return member == null ? null : MemberEntity.ToMember(member);
    }

    public async Task AddMember(string projectId, Member member)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project == null)
        {
            throw new NotFoundException();
        }
        project.Members.Add(MemberEntity.FromMember(member));
        _context.Update(project);
        await _context.SaveChangesAsync();
    }
}