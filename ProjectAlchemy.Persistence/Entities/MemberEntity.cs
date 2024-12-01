using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

public class MemberEntity
{
    public int Id { get; set; }
    public string userId { get; init; }
    public MemberType Type { get; init; }
    public string ProjectId { get; init; }
    
    public ProjectEntity Project { get; init; }

    public static Member? ToMember(MemberEntity entity)
    {
        return new Member(entity.userId, entity.Type);
    }

    public static MemberEntity FromMember(Member member)
    {
        return new MemberEntity()
        {
            userId = member.UserId,
            Type = member.Type
        };
    }
}