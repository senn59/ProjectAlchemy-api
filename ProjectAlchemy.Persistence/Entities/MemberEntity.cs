using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

public class MemberEntity
{
    public string userId { get; init; }
    public MemberType Type { get; init; }
    public int ProjectId { get; init; }
    
    public ProjectEntity Project { get; init; }
}