using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Exceptions;
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
    
    public async Task<InvitationOutgoingView> Create(string email, Guid projectId)
    {
        var alreadyExists = await _context.Invitations.AnyAsync(i => i.Email == email && i.ProjectId == projectId);
        if (alreadyExists)
        {
            throw new AlreadyExistsException();
        }
        
        var invitation = await _context.Invitations.AddAsync(new InvitationEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            ProjectId = projectId
        });
        
        await _context.SaveChangesAsync();
        return new InvitationOutgoingView
        {
            InvitationId = invitation.Entity.Id,
            Email = invitation.Entity.Email
        };
    }

    public async Task<List<InvitationOutgoingView>> GetInvitedEmails(Guid projectId)
    {
        return await _context.Invitations
            .Where(i => i.ProjectId == projectId)
            .Select(i => new InvitationOutgoingView
            {
                InvitationId = i.Id,
                Email = i.Email
            })
            .ToListAsync();
    }

    public async Task<InvitationDetails?> GetInfo(Guid invitationId)
    {
        var entity = await _context.Invitations.FindAsync(invitationId);
        return entity == null ? null : InvitationEntity.ToInvitationInfo(entity);
    }

    public async Task Delete(Guid invitationId)
    {
        var entity = await _context.Invitations.FindAsync(invitationId);
        if (entity == null)
        {
            return;
        }
        _context.Invitations.Remove(entity);
        await _context.SaveChangesAsync();
    }
}