using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Persistence.Entities;

public class InvitationEntity
{
    [Required]
    [MaxLength(36)]
    public string Id { get; set; }
    [Required]
    public required string Email { get; set; }
    
    public ProjectEntity Project { get; set; }
    public string projectId { get; set; }
}