using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IInvitationRepository
{
   public Task CreateInvitation(Invitation invitation, string projectId);
   public Task<List<Invitation>> GetAll(string userId);
   public Task UpdateInvitation(Invitation invitation);
}