using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class UserInvitation
{
    public required string InvitationId { get; set; }
    public required string ProjectName { get; set; }
}