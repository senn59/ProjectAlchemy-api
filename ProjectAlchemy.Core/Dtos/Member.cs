using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class Member
{
    public required Guid UserId { get; init; } 
    public required MemberType Type { get; init; }
}