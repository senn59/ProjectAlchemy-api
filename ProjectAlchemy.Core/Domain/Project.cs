using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Core.Domain;

public class Project
{
    public const int MaxNameLength = 30;
    private List<Issue> _issues;
    private List<Lane> _lanes;
    private List<Member> _members;
    
    [Required]
    [StringLength(MaxNameLength, MinimumLength = 1)]
    public string Name { get; set; }
    public IReadOnlyList<Issue> Issues => _issues.AsReadOnly();
    public IReadOnlyList<Lane> Lanes => _lanes.AsReadOnly();
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    public Project(List<Issue> issues, List<Member> members, List<Lane> lanes)
    {
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