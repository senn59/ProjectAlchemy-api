namespace ProjectAlchemy.Core.Dtos;

public class InvitationDetails
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public Guid ProjectId { get; set; }
}