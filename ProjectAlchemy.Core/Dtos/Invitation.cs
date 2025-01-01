using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class Invitation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Email { get; set; }
    public required InvitationStatus Status { get; set; }
}