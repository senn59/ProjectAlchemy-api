namespace ProjectAlchemy.Core.Domain;

public class Member
{
    public string UserId { get; private set; }
    public MemberType Type { get; private set; }


    private readonly ICollection<MemberType> _canDelete = [MemberType.Owner];
    private readonly ICollection<MemberType> _canUpdate = [MemberType.Owner, MemberType.Collaborator];

    public Member(string userId, MemberType type)
    {
        UserId = userId;
        Type = type;
    }

    public bool CanDeleteIssues()
    {
        return _canDelete.Contains(Type);
    }
    
    public bool CanUpdateIssues()
    {
        return _canUpdate.Contains(Type);
    }
}