using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Core.Domain;

public class Project
{
    public const int MaxNameLength = 30;
    private List<Issue> _issues;
    private List<Member> _members;
    private List<Lane> _lanes;
    
    [Required]
    [StringLength(MaxNameLength, MinimumLength = 1)]
    public string Name { get; set; }
    public IReadOnlyList<Issue> Issues { get; set; }
    public IReadOnlyList<Lane> Lanes { get; set; }
    public IReadOnlyList<Member> Members { get; set; }
}