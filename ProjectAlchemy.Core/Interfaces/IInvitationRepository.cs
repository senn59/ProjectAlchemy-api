using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IInvitationRepository
{
   public Task<InvitationOutgoingView> Create(string email, Guid projectId);
   public Task<List<InvitationOutgoingView>> GetInvitedEmails(Guid projectId);
   public Task<InvitationDetails?> GetInfo(Guid invitationId);
   public Task Delete(Guid invitationId);
}