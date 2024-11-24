namespace ProjectAlchemy.Core.Domain;

public class Member
{
    public string UserId { get; private set; }
    public MemberType Type { get; private set; }

    public Member(string userId, MemberType type)
    {
        UserId = userId;
        Type = type;
    }
}