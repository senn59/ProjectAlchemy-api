using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence.Repositories;

public class InvitationRepository: IInvitationRepository
{
    private readonly AppDbContext _context;
    
    public InvitationRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task Create(string email, string projectId)
    {
        _ = await _context.Invitations.AddAsync(new InvitationEntity
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            ProjectId = projectId
        });
    }

    public async Task<List<string>> GetInvitedEmails(string projectId)
    {
        return await _context.Invitations
            .Where(i => i.ProjectId == projectId)
            .Select(i => i.Email)
            .ToListAsync();
    }

    public async Task<InvitationDetails?> GetInfo(string invitationId)
    {
        var entity = await _context.Invitations.FindAsync(invitationId);
        return entity == null ? null : InvitationEntity.ToInvitationInfo(entity);
    }

    public Task Delete(string invitationId)
    {
        throw new NotImplementedException();
    }
}