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
}