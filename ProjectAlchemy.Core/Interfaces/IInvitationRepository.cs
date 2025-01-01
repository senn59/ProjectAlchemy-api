using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Interfaces;

public interface IInvitationRepository
{
   public Task Create(string email, string projectId);
   public Task<List<Invitation>> GetAll(string userId);
   public Task Update(string invitationId, InvitationStatus status);
   public Task Delete(string invitationId);
}