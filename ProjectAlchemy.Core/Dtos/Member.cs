using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class Member
{
    public required string UserId { get; init; } 
    public required MemberType Type { get; init; }
}