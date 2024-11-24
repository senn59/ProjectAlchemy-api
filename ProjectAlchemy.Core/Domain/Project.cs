using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Core.Domain;

public class Project
{
    public const int MaxNameLength = 30;
    private readonly List<Issue> _issues;
    private readonly List<Lane> _lanes;
    private readonly List<Member> _members;
    
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    [StringLength(MaxNameLength)]
    public string Name { get; private set; }
    public IReadOnlyList<Issue> Issues => _issues.AsReadOnly();
    public IReadOnlyList<Lane> Lanes => _lanes.AsReadOnly();
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    public Project(string name, List<Issue> issues, List<Member> members, List<Lane> lanes)
    {
        Name = name.Trim();
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
        _members.Add(toAdd);
    }
}