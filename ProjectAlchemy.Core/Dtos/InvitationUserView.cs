using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class InvitationUserView
{
    public required Guid InvitationId { get; set; }
    public required string ProjectName { get; set; }
}