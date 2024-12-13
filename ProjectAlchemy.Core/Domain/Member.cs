namespace ProjectAlchemy.Core.Domain;

public class Member(string userId, MemberType type)
{
    public string UserId { get; private set; } = userId;
    public MemberType Type { get; } = type;


    private readonly ICollection<MemberType> _canDelete = [MemberType.Owner];
    private readonly ICollection<MemberType> _canUpdate = [MemberType.Owner, MemberType.Collaborator];

    public bool CanDeleteIssues()
    {
        return _canDelete.Contains(Type);
    }
    
    public bool CanUpdateIssues()
    {
        return _canUpdate.Contains(Type);
    }
}