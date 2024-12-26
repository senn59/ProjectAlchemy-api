using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Persistence.Entities;

public class MemberEntity
{
    public int Id { get; init; }
    [Required]
    [StringLength(36)]
    public required string UserId { get; init; }
    public required MemberType Type { get; init; }
    
    [MaxLength(200)]
    public string? ProjectId { get; init; }
    public ProjectEntity? Project { get; init; }

    public static Member ToMember(MemberEntity entity)
    {
        return new Member(entity.UserId, entity.Type);
    }

    public static MemberEntity FromMember(Member member)
    {
        return new MemberEntity()
        {
            UserId = member.UserId,
            Type = member.Type
        };
    }
}