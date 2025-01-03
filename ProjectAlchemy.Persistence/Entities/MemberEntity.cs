using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Persistence.Entities;

public class MemberEntity
{
    [Key]
    public int Id { get; init; }
    [Required]
    [StringLength(36)]
    public required Guid UserId { get; init; }
    public required MemberType Type { get; init; }
    
    [MaxLength(200)]
    public Guid ProjectId { get; init; }
    public ProjectEntity Project { get; init; }

    public static Member ToMember(MemberEntity entity)
    {
        return new Member
        {
            UserId = entity.UserId,
            Type = entity.Type
        };
    }

    public static MemberEntity FromMember(Member member)
    {
        return new MemberEntity
        {
            UserId = member.UserId,
            Type = member.Type
        };
    }
}