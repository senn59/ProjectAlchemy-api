using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class InvitationUserView
{
    public required string InviteId { get; set; }
    public required string ProjectName { get; set; }
}