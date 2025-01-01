using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IInvitationRepository
{
   public Task Create(string email, string projectId);
   public Task<List<string>> GetInvitedEmails(string projectId);
   public Task<InvitationInfo?> GetInfo(string invitationId);
   public Task Delete(string invitationId);
}