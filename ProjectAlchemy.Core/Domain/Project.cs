using ProjectAlchemy.Core.Helpers;

namespace ProjectAlchemy.Core.Domain;

public class Project
{
    public const int MaxNameLength = 30;
    private readonly List<Issue> _issues;
    private readonly List<Lane> _lanes;
    private readonly List<Member> _members;

    public string Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyList<Issue> Issues => _issues.AsReadOnly();
    public IReadOnlyList<Lane> Lanes => _lanes.AsReadOnly();
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    public Project(string name, List<Issue> issues, List<Member> members, List<Lane> lanes, string? id = null)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Name = name;
        SetName(name);
        _issues = issues;
        _members = members;
        _lanes = lanes;
    }

    public void AddMember(Member adder, Member toAdd)
    {
        if (!_members.Contains(adder))
        {
            throw new Exception("You cannot add a new member to this project.");
        }
        
        if (adder.Type != MemberType.Owner)
        {
            throw new Exception("You do not have permission to add a new member to this project.");
        }

        if (toAdd.Type == MemberType.Owner)
        {
            throw new Exception("You cannot add a member as owner");
        }
        
        _members.Add(toAdd);
    }

    public void CanAdd(Issue issue, Member member)
    {
        if (!_members.Contains(member))
        {
            throw new Exception("You are unauthorized to add new issues to this project.");
        }
        _issues.Add(issue);
    }
    
    public void SetName(string name)
    {
        name = name.Trim();
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Guard.AgainstLength(name, nameof(name), MaxNameLength);
        Name = name;
    }
}