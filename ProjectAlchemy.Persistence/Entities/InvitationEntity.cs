using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Persistence.Entities;

public class InvitationEntity
{
    [Required]
    [MaxLength(36)]
    public string Id { get; set; }
    [Required]
    [MaxLength(255)]
    public required string Email { get; init; }
    
    public ProjectEntity Project { get; init; }
    [MaxLength(36)]
    public string ProjectId { get; init; }

    public static InvitationInfo ToInvitationInfo(InvitationEntity entity)
    {
        return new InvitationInfo
        {
            Id = entity.Id,
            Email = entity.Email,
            ProjectId = entity.ProjectId
        };
    }
}