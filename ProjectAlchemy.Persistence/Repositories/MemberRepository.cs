using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
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
            .Where(m => m.UserId == userId)
            .Select(m => new ProjectOverview
            {
                ProjectId = m.Project.Id,
                ProjectName = m.Project.Name,
                MemberType = m.Type
            })
            .ToListAsync();
    }

    public async Task<List<InvitationUserView>> GetInvitations(string userId)
    {
        return await _context.Invitations
            .Where(i => i.Email == userId)
            .Select(i => new InvitationUserView
            {
                InviteId = i.Id,
                ProjectName = i.Project.Name
            })
            .ToListAsync();
    }
}